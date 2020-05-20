using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.PromotionData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class RegisterDiscountModel : PageModel
    {
        private readonly IGame gamedb;
        private readonly IGamePicture gamepicdb;
        private readonly IEventPromotion eventpromotiondb;
        private readonly IPromotion promotiondb;

        public RegisterDiscountModel(IGame gamedb,
            IGamePicture gamepicdb,
            IEventPromotion eventpromotiondb,
            IPromotion promotiondb)
        {
            this.gamedb = gamedb;
            this.gamepicdb = gamepicdb;
            this.eventpromotiondb = eventpromotiondb;
            this.promotiondb = promotiondb;
        }
        public Game Game { get; set; }
        public string imgpath { get; set; }

        public async Task<IActionResult> OnGetAsync(int gameid)
        {
            Game = gamedb.GetById(gameid);
            if (Game == null) return RedirectToPage("./Index");
            var img = gamepicdb.GetIconById(Game.GameId);
            imgpath = img == null ? "~/img/notfound.jpg" : @"~/img/" + img.Path;

            return Page();
        }
    }
}