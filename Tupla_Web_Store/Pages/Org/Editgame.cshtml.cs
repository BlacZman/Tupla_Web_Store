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
using Microsoft.EntityFrameworkCore;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.PlatformData;

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class EditgameModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly IGame db;
        private readonly IGamePicture picdb;
        private readonly IPlatform platformdb;
        private readonly IPlatformOfGame gamePlatformdb;
        private readonly IWebHostEnvironment env;

        private GamePicture GamePicInfo { get; set; }

        public EditgameModel(UserManager<Areas.Identity.Data.User> userManager, 
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

        [BindProperty]
        public Game Game { get; set; }
        public IFormFile Imgfile { get; set; }
        public string imgDisplay { get; set; }
        public IEnumerable<IFormFile> infoImg { get; set; }
        public IEnumerable<GamePicture> oldInfo { get; set; }
        public IEnumerable<SelectListItem> PlatformList { get; set; }
        public IEnumerable<PlatformOfGame> ListOfSupportedPlatform { get; set; }
        [BindProperty]
        public PlatformOfGame newGamePlatform { get; set; }
        [BindProperty]
        public PlatformOfGame delGamePlatform { get; set; }
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
            await Task.Run(() =>
            {
                var GamePicInfo = picdb.GetIconById(Game.GameId);
                Game.HtmlText = HttpUtility.HtmlDecode(Game.HtmlText);
                imgDisplay = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                ListOfSupportedPlatform = gamePlatformdb.GetAllSupportedPlatform(Game.GameId);
                var platformlist = platformdb.GetAllByName("");
                if (ListOfSupportedPlatform.Any())
                {
                    foreach(var r1 in ListOfSupportedPlatform)
                    {
                        platformlist = platformlist.Where(s => s.PlatformId != r1.PlatformId);
                    }
                }
                PlatformList = new SelectList(platformlist, "PlatformId", "Platform_name");
                delGamePlatform = new PlatformOfGame();
                newGamePlatform = new PlatformOfGame();
                //Load info images
                oldInfo = picdb.GetById(Game.GameId, ImageType.Info);
            });
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            //PlatformOfGame
            if(newGamePlatform != null && newGamePlatform.PlatformId != 0)
            {
                newGamePlatform.GameId = Game.GameId;
                gamePlatformdb.Add(newGamePlatform);
                await gamePlatformdb.CommitAsync();
                return RedirectToPage("./EditGame", new { id = Game.GameId });
            }
            else if(delGamePlatform != null && delGamePlatform.PlatformId != 0)
            {
                delGamePlatform = gamePlatformdb.GetById(delGamePlatform.GameId, delGamePlatform.PlatformId);
                if (delGamePlatform == null) return Page();
                gamePlatformdb.Delete(delGamePlatform);
                await gamePlatformdb.CommitAsync();
                return RedirectToPage("./EditGame", new { id = delGamePlatform.GameId });
            }

            //Game
            Game.HtmlText = HttpUtility.HtmlEncode(Game.HtmlText);
            Game.Price = Math.Round(Game.Price, 2, MidpointRounding.ToEven);

            db.Update(Game);
            await db.CommitAsync();

            //Image uploading
            if (Imgfile != null)
            {
                GamePicInfo = GamePicInfo == null ? new GamePicture() : GamePicInfo;
                //Upload to file system
                string uploadsFolder = Path.Combine(env.WebRootPath, "img");
                string uniqueFileName = Path.Combine("g", Guid.NewGuid().ToString() + "_" + Imgfile.FileName);
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                //Update database
                GamePicInfo.Path = uniqueFileName;
                GamePicInfo.imageType = ImageType.Icon;
                GamePicInfo.GameId = Game.GameId;
                picdb.AddIcon(GamePicInfo);
                await picdb.CommitAsync();
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Imgfile.CopyToAsync(fileStream);
                }
            }
            //Info images update
            if (infoImg != null)
            {
                //Remove old info image
                var oldinfo = picdb.GetById(Game.GameId, ImageType.Info);
                foreach (var r in oldinfo)
                {
                    picdb.Delete(r);
                }
                await picdb.CommitAsync();
                foreach (var r in infoImg)
                {
                    await Task.Run(async () =>
                    {
                        string uploadsFolder = Path.Combine(env.WebRootPath, "img");
                        string uniqueFileName = Path.Combine("g", Guid.NewGuid().ToString() + "_" + r.FileName);
                        string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                        var infoImage = new GamePicture
                        {
                            GameId = Game.GameId,
                            imageType = ImageType.Info,
                            Path = uniqueFileName
                        };
                        //update database
                        picdb.Add(infoImage);
                        await picdb.CommitAsync();
                        using (var fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await r.CopyToAsync(fileStream);
                        }
                    });
                }
            }
            TempData["CompanyStatus"] = "Your game has been updated!";

            return RedirectToPage("./Index");
        }
    }
}
