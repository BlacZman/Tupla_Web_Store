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
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.CompanyData;
using Tupla.Data.Core.PictureData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly ICompany db;
        private readonly ICompanyPicture picdb;
        private readonly IWebHostEnvironment env;
        private CompanyPicture CompanyPicInfo { get; set; }
        public EditModel(UserManager<Areas.Identity.Data.User> userManager,
            ICompany db, 
            ICompanyPicture picdb,
            IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.db = db;
            this.picdb = picdb;
            this.env = env;
        }

        [BindProperty]
        public Company Company { get; set; }
        public string imgDisplay { get; set; }
        public IFormFile Imgfile { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var companyId = user.CompanyID;
            if (companyId == null) return RedirectToPage("./Create");
            else
            {
                Company = db.GetById(companyId);
                CompanyPicInfo = picdb.GetIconById((int)companyId);
                imgDisplay = CompanyPicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + CompanyPicInfo.Path;
                return Page();
            }
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            db.Update(Company);
            await db.CommitAsync();

            //Image uploading
            if (Imgfile != null)
            {
                CompanyPicInfo = CompanyPicInfo == null ? new CompanyPicture() : CompanyPicInfo;
                //Upload to file system
                string uploadsFolder = Path.Combine(env.WebRootPath, "img");
                string uniqueFileName = Path.Combine("org", Guid.NewGuid().ToString() + "_" + Imgfile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //Update database
                CompanyPicInfo.Path = uniqueFileName;
                CompanyPicInfo.imageType = ImageType.Icon;
                CompanyPicInfo.CompanyId = Company.companyId;
                picdb.AddIcon(CompanyPicInfo);
                await picdb.CommitAsync();
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Imgfile.CopyToAsync(fileStream);
                }
            }

            TempData["CompanyStatus"] = "Your company profile has been saved!";
            return RedirectToPage("./Index");
        }
    }
}
