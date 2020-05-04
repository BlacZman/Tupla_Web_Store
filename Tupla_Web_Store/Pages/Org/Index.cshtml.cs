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
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<Tupla_Web_Store.Areas.Identity.Data.User> _userManager;
        private readonly ICompany db;
        private readonly IGame dbGame;

        public IndexModel(UserManager<Areas.Identity.Data.User> userManager,
            ICompany db,
            IGame dbGame)
        {
            _userManager = userManager;
            this.db = db;
            this.dbGame = dbGame;
        }
        [TempData]
        public string CompanyStatus  { get; set; }
        public Company Company { get; set; }
        public IEnumerable<Game> Game { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var companyId = user.CompanyID;
            Company = db.GetById(companyId);
            if (Company == null) return RedirectToPage("./Create");
            else
            {
                Game = dbGame.GetGamesByCompanyId((int)companyId);
                return Page();
            }
        }
    }
}
