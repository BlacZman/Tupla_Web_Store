using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.g
{
    [AllowAnonymous]
    public class DetailModel : PageModel
    {
        private readonly IGame db;

        public DetailModel(IGame db)
        {
            this.db = db;
        }

        public Game Game { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("../NotFound");
            }

            Game = await db.GetByIdAsync(id);

            if (Game == null)
            {
                return RedirectToPage("../NotFound");
            }
            return Page();
        }
    }
}
