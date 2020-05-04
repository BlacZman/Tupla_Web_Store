using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Context;
using Tupla.Data.Core.CompanyData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly SignInManager<Areas.Identity.Data.User> signInManager;
        private readonly ICompany db;

        public CreateModel(UserManager<Areas.Identity.Data.User> userManager,
            SignInManager<Areas.Identity.Data.User> signInManager, 
            ICompany db)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.db = db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            companyId = user.CompanyID;
            if (companyId == null) return Page();
            else return RedirectToPage("./Index");
        }

        [BindProperty]
        public Company Company { get; set; }
        public int? companyId { get; private set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            var user = await userManager.GetUserAsync(User);
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Company.company_create_date = DateTime.Now;
            db.Add(Company);
            await db.CommitAsync();
            user.CompanyID = Company.companyId;

            await userManager.UpdateAsync(user);
            await signInManager.RefreshSignInAsync(user);

            TempData["CompanyStatus"] = "Your company profile has been created!";
            return RedirectToPage("./Index");
        }
    }
}
