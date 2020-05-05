﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class EditgameModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly IGame db;

        public EditgameModel(UserManager<Areas.Identity.Data.User> userManager, 
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

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            Game.HtmlText = HttpUtility.HtmlEncode(Game.HtmlText);
            Game.Price = Math.Round(Game.Price, 2, MidpointRounding.ToEven);

            db.Update(Game);
            await db.CommitAsync();
            TempData["CompanyStatus"] = "Your game has been updated!";

            return RedirectToPage("./Index");
        }
    }
}
