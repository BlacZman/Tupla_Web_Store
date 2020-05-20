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
    public class IndexModel : PageModel
    {
        private readonly IGame db;
        private readonly IGamePicture picdb;

        public IndexModel(IGame db,
            IGamePicture picdb)
        {
            this.db = db;
            this.picdb = picdb;
        }

        [BindProperty(SupportsGet = true)]
        public string search { get; set; }
        public Dictionary<Game, string> piclist { get; set; }

        public async Task OnGetAsync()
        {
            await Task.Run(() =>
            {
                piclist = new Dictionary<Game, string> { };
                IEnumerable<Game> Game = db.GetGamesByName(search);
                foreach (var item in Game)
                {
                    var GamePicInfo = picdb.GetIconById(item.GameId);
                    var imgDisplayGame = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                    piclist.Add(item, imgDisplayGame);
                }
            });
            
        }
    }
}
