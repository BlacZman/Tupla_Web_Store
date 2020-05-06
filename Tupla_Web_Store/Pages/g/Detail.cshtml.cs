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
using Tupla.Data.Core.PictureData;

namespace Tupla_Web_Store.Pages.g
{
    [AllowAnonymous]
    public class DetailModel : PageModel
    {
        private readonly IGame db;
        private readonly IGamePicture picdb;

        public DetailModel(IGame db,
            IGamePicture picdb)
        {
            this.db = db;
            this.picdb = picdb;
        }

        public Game Game { get; set; }
        public string imgDisplay { get; set; }

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
            await Task.Run(() =>
            {
                var GamePicInfo = picdb.GetIconById(Game.GameId);
                imgDisplay = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
            });
            return Page();
        }
    }
}
