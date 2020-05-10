using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.PlatformData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class AddgameModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly IGame db;
        private readonly IGamePicture picdb;
        private readonly IPlatform platformdb;
        private readonly IPlatformOfGame gamePlatformdb;
        private readonly IWebHostEnvironment env;

        private GamePicture GamePicInfo { get; set; }

        public AddgameModel(UserManager<Areas.Identity.Data.User> userManager, 
            IGame db,
            IGamePicture picdb,
            IPlatform platformdb,
            IPlatformOfGame gamePlatformdb,
            IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.db = db;
            this.picdb = picdb;
            this.platformdb = platformdb;
            this.gamePlatformdb = gamePlatformdb;
            this.env = env;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await userManager.GetUserAsync(User);
            int? companyId = null;
            await Task.Run(() =>
            {
                companyId = user.CompanyID;
            });
            if (companyId == null) return RedirectToPage("./Create");
            else
            {
                await Task.Run(() =>
                {
                    imgDisplay = "~/img/notfound.jpg";
                    PlatformList = new SelectList(platformdb.GetAllByName(""), "PlatformId", "Platform_name");
                });
                return Page();
            }
        }

        [BindProperty]
        public Game Game { get; set; }
        [BindProperty]
        public PlatformOfGame newGamePlatform { get; set; }
        public IEnumerable<SelectListItem> PlatformList { get; set; }
        public IFormFile Imgfile { get; set; }
        public string imgDisplay { get; set; }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                PlatformList = new SelectList(platformdb.GetAllByName(""), "PlatformId", "Platform_name");
                return Page();
            }
            
            var user = await userManager.GetUserAsync(User);
            await Task.Run(() =>
            {
                var companyId = user.CompanyID;
                Game.CompanyID = (int)companyId;
                Game.HtmlText = HttpUtility.HtmlEncode(Game.HtmlText);
                Game.Price = Math.Round(Game.Price, 2, MidpointRounding.ToEven);
                Game.ReleaseDate = DateTime.Now;
            });
            db.Add(Game);
            await db.CommitAsync();

            //PlatformOfGame
            newGamePlatform.GameId = Game.GameId;
            gamePlatformdb.Add(newGamePlatform);
            await gamePlatformdb.CommitAsync();
            
            //Image uploading
            if (Imgfile != null)
            {
                GamePicInfo = GamePicInfo == null ? new GamePicture() : GamePicInfo;
                //Upload to file system
                string uploadsFolder = Path.Combine(env.WebRootPath, "img");
                string uniqueFileName = Path.Combine("g", Guid.NewGuid().ToString() + "_" + Imgfile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //Update database
                await Task.Run(() =>
                {
                    GamePicInfo.Path = uniqueFileName;
                    GamePicInfo.imageType = ImageType.Icon;
                    GamePicInfo.GameId = Game.GameId;
                });
                picdb.AddIcon(GamePicInfo);
                await picdb.CommitAsync();
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Imgfile.CopyToAsync(fileStream);
                }
            }
            return RedirectToPage("../g/Index");
        }
    }
}
