using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
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
using Tupla.Data.Core.Tag;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.g
{
    [AllowAnonymous]
    public class DetailModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly TuplaContext context;
        private readonly IGame db;
        private readonly IGamePicture picdb;
        private readonly IGameTag dbgametag;
        private readonly ITag dbTag;

        public DetailModel(UserManager<Areas.Identity.Data.User> userManager,
            TuplaContext context,
            IGame db,
            IGamePicture picdb,
            IGameTag dbgametag,
            ITag dbTag)
        {
            this.userManager = userManager;
            this.context = context;
            this.db = db;
            this.picdb = picdb;
            this.dbgametag = dbgametag;
            this.dbTag = dbTag;
        }
        [BindProperty]
        public Game Game { get; set; }
        public string imgDisplay { get; set; }
        [BindProperty]
        public AddTagFormModel AddTag { get; set; }
        [BindProperty]
        public DelTagFormModel DeleteTag { get; set; }
        public List<Tag> userTag { get; set; }
        public List<KeyValuePair<string, int>> list { get; set; }
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return RedirectToPage("../NotFound");
            }
            
            Game = db.GetById(id);
            
            if (Game == null)
            {
                return RedirectToPage("../NotFound");
            }
            await Task.Run(async () =>
            {
                var GamePicInfo = picdb.GetIconById(Game.GameId);
                imgDisplay = GamePicInfo == null ? "~/img/notfound.jpg" : @"~/img/" + GamePicInfo.Path;
                IEnumerable<GameTag> populargametag = dbgametag.GetPopularTagOfGame(Game.GameId);
                if(populargametag.Any())
                {
                    Dictionary<string, int> dict = new Dictionary<string, int> { };
                    foreach (var r in populargametag)
                    {
                        var taggy = await dbTag.GetByIdAsync(r.TagId);
                        string strtag = taggy.TagName;
                        if(!dict.ContainsKey(strtag)) dict[strtag] = 1;
                        else dict[strtag] = dict[strtag] + 1;
                    }
                    list = dict.ToList();
                    list.Sort((p1, p2) => p2.Value.CompareTo(p1.Value));
                }
                AddTag = new AddTagFormModel();
                DeleteTag = new DelTagFormModel();
            });
            await Task.Run(async ()=>
            {
                userTag = new List<Tag> { };
                string username = userManager.GetUserName(User);
                IEnumerable<GameTag> userGameTag = dbgametag.GetByUsername(Game.GameId, username);
                if (userGameTag.Any())
                {
                    foreach (var item in userGameTag)
                    {
                        Tag getTag = await dbTag.GetByIdAsync(item.TagId);
                        userTag.Add(getTag);
                    }
                }
            });
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            //Console.WriteLine("I'm in controller!");
            if (AddTag.gameid != null && AddTag.tag != null)
            {
                Game Game = await db.GetByIdAsync(AddTag.gameid);
                if (Game == null) return RedirectToPage(Url.Content("~/NotFound"));
                Tag newTag = new Tag();
                newTag.TagName = AddTag.tag;
                using (var transaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        string username = null;
                        await Task.Run(() =>
                        {
                            username = userManager.GetUserName(User);
                        });
                        if (username == null) return RedirectToPage(Url.Content("~/NotFound"));
                        newTag.Username = username;
                        dbTag.Add(newTag);
                        await dbTag.CommitAsync();
                        //Console.WriteLine("Commit one of them");
                        GameTag gametag = new GameTag();
                        gametag.GameId = Game.GameId;
                        gametag.TagId = newTag.TagId;
                        gametag.Username = username;
                        dbgametag.Add(gametag);
                        await dbgametag.CommitAsync();
                        //Console.WriteLine("Commit all of them");
                        transaction.Commit();
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        transaction.Rollback();
                    }
                }

                return RedirectToPage(Url.Content("~/g/Detail"), new { id = Game.GameId });
            }
            else if(DeleteTag.gameid != null && DeleteTag.tagid != null)
            {
                Console.WriteLine("I'm in controller!");
                Game Game = await db.GetByIdAsync(DeleteTag.gameid);
                if (Game == null) return RedirectToPage(Url.Content("~/NotFound"));
                Tag DelTag = await dbTag.GetByIdAsync((int)DeleteTag.tagid);
                if (DelTag == null) return RedirectToPage(Url.Content("~/NotFound"));
                dbTag.Delete(DelTag);
                await dbTag.CommitAsync();
                return RedirectToPage(Url.Content("~/g/Detail"), new { id = Game.GameId });
            }
            else
            {
                return RedirectToPage(Url.Content("~/NotFound"));
            }
        }
    }
}
