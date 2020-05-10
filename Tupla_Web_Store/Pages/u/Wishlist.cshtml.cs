using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.WishList;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.u
{
    public class WishlistModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly IWishList wishlistdb;
        private readonly IGame gamedb;
        private readonly IGamePicture gamepicdb;

        public WishlistModel(UserManager<Areas.Identity.Data.User> userManager,
            IWishList wishlistdb,
            IGame gamedb,
            IGamePicture gamepicdb)
        {
            this.userManager = userManager;
            this.wishlistdb = wishlistdb;
            this.gamedb = gamedb;
            this.gamepicdb = gamepicdb;
        }

        public Dictionary<Game, string> piclist { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            await Task.Run(() =>
            {
                piclist = new Dictionary<Game, string> { };
                var username = userManager.GetUserName(User);
                IEnumerable<WishList> WishList = wishlistdb.GetWishListsByUsername(username);
                foreach (var item in WishList)
                {
                    var Game = gamedb.GetById(item.GameId);
                    var GamePicInfo = gamepicdb.GetIconById(item.GameId);
                    var imgDisplayGame = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                    piclist.Add(Game, imgDisplayGame);
                }
            });
            return Page();
        }
    }
}