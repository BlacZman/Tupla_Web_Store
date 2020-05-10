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

namespace Tupla_Web_Store.Pages.Org
{
    [Authorize]
    public class EditgameModel : PageModel
    {
        private readonly UserManager<Areas.Identity.Data.User> userManager;
        private readonly IGame db;
        private readonly IGamePicture picdb;
        private readonly IWebHostEnvironment env;

        private GamePicture GamePicInfo { get; set; }

        public EditgameModel(UserManager<Areas.Identity.Data.User> userManager, 
            IGame db,
            IGamePicture picdb,
            IWebHostEnvironment env)
        {
            this.userManager = userManager;
            this.db = db;
            this.picdb = picdb;
            this.env = env;
        }

        [BindProperty]
        public Game Game { get; set; }
        public IFormFile Imgfile { get; set; }
        public string imgDisplay { get; set; }

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
            var GamePicInfo = picdb.GetIconById(Game.GameId);
            Game.HtmlText = HttpUtility.HtmlDecode(Game.HtmlText);
            imgDisplay = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
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
            TempData["CompanyStatus"] = "Your game has been updated!";

            return RedirectToPage("./Index");
        }
    }
}
