using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.CompanyData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class EditModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly ICompany db;

        public EditModel(UserManager<Areas.Identity.Data.User> userManager,
            ICompany db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        [BindProperty]
        public Company Company { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var companyId = user.CompanyID;
            if (companyId == null) return RedirectToPage("./Create");
            else
            {
                Company = db.GetById(companyId);
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

            TempData["CompanyStatus"] = "Your company profile has been saved!";
            return RedirectToPage("./Index");
        }
    }
}
