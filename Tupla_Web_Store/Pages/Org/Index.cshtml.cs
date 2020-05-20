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
using Tupla.Data.Core.PictureData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly UserManager<Tupla_Web_Store.Areas.Identity.Data.User> _userManager;
        private readonly ICompany db;
        private readonly IGame dbGame;
        private readonly ICompanyPicture picdb;
        private readonly IGamePicture picgamedb;

        public IndexModel(UserManager<Areas.Identity.Data.User> userManager,
            ICompany db,
            IGame dbGame,
            ICompanyPicture picdb,
            IGamePicture picgamedb)
        {
            _userManager = userManager;
            this.db = db;
            this.dbGame = dbGame;
            this.picdb = picdb;
            this.picgamedb = picgamedb;
        }
        [TempData]
        public string CompanyStatus  { get; set; }
        public Company Company { get; set; }
        public string imgDisplay { get; set; }
        public Dictionary<Game, string> piclist { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            var companyId = user.CompanyID;
            await Task.Run(()=> 
            {
                Company = db.GetById(companyId);
            });
            if (Company == null) return RedirectToPage("./Create");
            else
            {
                await Task.Run(() =>
                {
                    var CompanyPicInfo = picdb.GetIconById((int)companyId);
                    imgDisplay = CompanyPicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + CompanyPicInfo.Path;
                    piclist = new Dictionary<Game, string> { };
                    IEnumerable<Game> Game = dbGame.GetGamesByCompanyId((int)companyId);
                    foreach (var item in Game)
                    {
                        var GamePicInfo = picgamedb.GetIconById(item.GameId);
                        var imgDisplayGame = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                        piclist.Add(item, imgDisplayGame);
                    }
                });
                return Page();
            }
        }
    }
}
