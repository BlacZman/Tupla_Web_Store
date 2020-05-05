using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.Org
{
    public class DeletegameModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly IGame db;

        public DeletegameModel(UserManager<Areas.Identity.Data.User> userManager,
            IGame db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        [BindProperty]
        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            var user = await userManager.GetUserAsync(User);
            int companyid = (int)user.CompanyID;
            if (id == null)
            {
                return RedirectToPage("../NotFound");
            }

            Game = await db.GetByIdAuthorizeAsync(id, companyid);

            if (Game == null)
            {
                return RedirectToPage("../NotFound");
            }
            Game.HtmlText = HttpUtility.HtmlDecode(Game.HtmlText);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Game == null) return RedirectToPage("../NotFound");
            db.Delete(Game);
            await db.CommitAsync();
            TempData["CompanyStatus"] = "Your game has been removed from marketplace!";

            return RedirectToPage("./Index");
        }
    }
}
