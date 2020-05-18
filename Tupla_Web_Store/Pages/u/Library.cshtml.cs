using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.CodeData;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.Shopping.TransactionData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.u
{
    [Authorize]
    public class LibraryModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ITransaction transdb;
        private readonly ICode codedb;
        private readonly IGame gamedb;
        private readonly IPlatform platformdb;

        public LibraryModel(UserManager<User> userManager,
            ITransaction transdb,
            ICode codedb,
            IGame gamedb,
            IPlatform platformdb)
        {
            this.userManager = userManager;
            this.transdb = transdb;
            this.codedb = codedb;
            this.gamedb = gamedb;
            this.platformdb = platformdb;
        }
        public Dictionary<Game, IEnumerable<Code>> cdkeys { get; set; }

        public async Task OnGetAsync()
        {
            await Task.Run(()=> 
            {
                cdkeys = new Dictionary<Game, IEnumerable<Code>> { };
                var username = userManager.GetUserName(User);
                var thistrans = transdb.GetByUsername(username);
                foreach (var order in thistrans)
                {
                    var codelist = codedb.GetCodeByOrderId(order.OrderId);
                    foreach (var code in codelist)
                    {
                        var game = gamedb.GetById(code.GameId);
                        if (!cdkeys.ContainsKey(game))
                        {
                            IEnumerable<Code> newlist = new List<Code> { code };
                            cdkeys.Add(game, newlist);
                        }
                        else
                        {
                            cdkeys[game] = cdkeys[game].Append(code);
                        }
                    }
                }
            });
        }
    }
}