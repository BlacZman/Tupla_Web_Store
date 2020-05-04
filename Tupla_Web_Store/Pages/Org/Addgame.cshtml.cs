using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class AddgameModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly IGame db;

        public AddgameModel(UserManager<Areas.Identity.Data.User> userManager, 
            IGame db)
        {
            this.userManager = userManager;
            this.db = db;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            var companyId = user.CompanyID;
            if (companyId == null) return RedirectToPage("./Create");
            else return Page();
        }

        [BindProperty]
        public Game Game { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = await userManager.GetUserAsync(User);
            var companyId = user.CompanyID;
            Game.CompanyID = (int)companyId;
            Game.HtmlText = HttpUtility.HtmlEncode(Game.HtmlText);
            Game.Price = Math.Round(Game.Price, 2, MidpointRounding.ToEven);
            Game.ReleaseDate = DateTime.Now;

            db.Add(Game);
            await db.CommitAsync();

            return RedirectToPage("../g/Index");
        }
    }
}
