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
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.Shopping.CartData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.c
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ICart cartdb;
        private readonly IGame gamedb;
        private readonly IGamePicture gamepicdb;

        public IndexModel(UserManager<User> userManager,
            ICart cartdb,
            IGame gamedb,
            IGamePicture gamepicdb)
        {
            this.userManager = userManager;
            this.cartdb = cartdb;
            this.gamedb = gamedb;
            this.gamepicdb = gamepicdb;
        }

        public IEnumerable<Tupla.Data.Core.Shopping.CartData.Cart> listcartitem { get; set; }
        public Dictionary<Game, CartPlatformModel> piclist { get; set; }
        [BindProperty]
        public Tupla.Data.Core.Shopping.CartData.Cart ModifyCart { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            
            await Task.Run(() =>
            {
                piclist = new Dictionary<Game, CartPlatformModel> { };
                var username = userManager.GetUserName(User);
                listcartitem = cartdb.GetByCartId(username);
                foreach (var item in listcartitem)
                {
                    var cartplatform = new CartPlatformModel();
                    var Game = gamedb.GetById(item.GameId);
                    var GamePicInfo = gamepicdb.GetIconById(item.GameId);
                    var imgDisplayGame = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                    cartplatform.picture = imgDisplayGame;
                    cartplatform.cartitem = item;
                    piclist.Add(Game, cartplatform);
                }
            });
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            int gameid = 0;
            int.TryParse(Request.Form["gameid"], out gameid);
            int platformid = 0;
            int.TryParse(Request.Form["platformid"], out platformid);
            var username = userManager.GetUserName(User);
            var changequan = Request.Form["changequan"];
            if (changequan == "yes")
            {
                
            }
            else
            {
                if (platformid != 0 && gameid != 0)
                {
                    var cartitem = cartdb.GetById(gameid, platformid, username);
                    if (cartitem == null)
                    {
                        await Task.Run(() =>
                        {
                            cartitem = new Tupla.Data.Core.Shopping.CartData.Cart();
                            cartitem.CartId = username;
                            cartitem.GameId = gameid;
                            cartitem.PlatformId = platformid;
                            cartitem.Quantity = 1;
                        });
                        cartdb.Add(cartitem);
                        await cartdb.CommitAsync();
                    }
                    else
                    {
                        await Task.Run(() =>
                        {
                            var quantityold = cartitem.Quantity;
                            cartitem.Quantity = quantityold + 1;
                        });
                        cartdb.Update(cartitem);
                        await cartdb.CommitAsync();
                    }
                }
            }
            await Task.Run(() =>
            {
                piclist = new Dictionary<Game, CartPlatformModel> { };
                var username = userManager.GetUserName(User);
                listcartitem = cartdb.GetByCartId(username);
                foreach (var item in listcartitem)
                {
                    var cartplatform = new CartPlatformModel();
                    var Game = gamedb.GetById(item.GameId);
                    var GamePicInfo = gamepicdb.GetIconById(item.GameId);
                    var imgDisplayGame = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                    cartplatform.picture = imgDisplayGame;
                    cartplatform.cartitem = item;
                    piclist.Add(Game, cartplatform);
                }
            });
            return RedirectToPage();
        }

        public class CartPlatformModel
        {
            public Tupla.Data.Core.Shopping.CartData.Cart cartitem { get; set; }
            public string picture { get; set; }
        }
    }
}
