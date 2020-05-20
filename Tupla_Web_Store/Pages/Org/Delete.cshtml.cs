using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.CompanyData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class DeleteModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly ICompany db;

        public DeleteModel(UserManager<Areas.Identity.Data.User> userManager, 
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

        public async Task<IActionResult> OnPostAsync()
        {
            if (Company == null) return RedirectToPage("../Index");
            db.Delete(Company);
            await db.CommitAsync();

            TempData["CompanyDelete"] = "Your company has been removed!";
            return RedirectToPage("../Index");
        }
    }
}
