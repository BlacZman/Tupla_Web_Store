using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Context;
using Tupla.Data.Core.CompanyData;
using Tupla.Data.Core.PictureData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly SignInManager<Areas.Identity.Data.User> signInManager;
        private readonly ICompany db;
        private readonly ICompanyPicture picdb;
        private readonly IWebHostEnvironment env;

        public CreateModel(UserManager<Areas.Identity.Data.User> userManager,
            SignInManager<Areas.Identity.Data.User> signInManager, 
            ICompany db,
            ICompanyPicture picdb,
            IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
            this.picdb = picdb;
            this.env = env;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            await Task.Run(()=> 
            {
                companyId = user.CompanyID;
                imgDisplay = "~/img/notfound.jpg";
            });
            if (companyId == null) return Page();
            else return RedirectToPage("./Index");
        }

        [BindProperty]
        public Company Company { get; set; }
        public int? companyId { get; set; }
        public string imgDisplay { get; set; }
        public IFormFile Imgfile { get; set; }
        public CompanyPicture CompanyPicInfo { get; set; }
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return Page();
            }
            await Task.Run(() =>
            {
                Company.company_create_date = DateTime.Now;
            });
            db.Add(Company);
            await db.CommitAsync();

            await Task.Run(() =>
            {
                user.CompanyID = Company.companyId;
            });

            if (Imgfile != null)
            {
                CompanyPicInfo = CompanyPicInfo == null ? new CompanyPicture() : CompanyPicInfo;
                //Upload to file system
                string uploadsFolder = Path.Combine(env.WebRootPath, "img");
                string uniqueFileName = Path.Combine("g", Guid.NewGuid().ToString() + "_" + Imgfile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //Update database
                await Task.Run(() =>
                {
                    CompanyPicInfo.Path = uniqueFileName;
                    CompanyPicInfo.imageType = ImageType.Icon;
                    CompanyPicInfo.CompanyId = Company.companyId;
                });
                picdb.AddIcon(CompanyPicInfo);
                await picdb.CommitAsync();
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Imgfile.CopyToAsync(fileStream);
                }
            }

            await userManager.UpdateAsync(user);
            await signInManager.RefreshSignInAsync(user);

            TempData["CompanyStatus"] = "Your company profile has been created!";
            return RedirectToPage("./Index");
        }
    }
}
