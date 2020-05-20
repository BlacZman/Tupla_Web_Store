using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Context;
using Tupla.Data.Core.GameData;

namespace Tupla_Web_Store.Pages.Analysis
{
    public class MostWishModel : PageModel
    {
        private readonly TuplaContext context;
        private readonly IGame gamedb;

        public MostWishModel(TuplaContext context,
            IGame gamedb)
        {
            this.context = context;
            this.gamedb = gamedb;
        }
        public Dictionary<string, int> test { get; set; }


        public void OnGet()
        {
            test = new Dictionary<string, int> { };
            var query = from r in context.WishList
                        orderby r.GameId
                        select r;
            foreach (var r in query)
            {
                var gamename = gamedb.GetById(r.GameId).GameName;
                if (test.ContainsKey(gamename))
                {
                    test[gamename] = test[gamename] + 1;
                }
                else
                {
                    test.Add(gamename, 1);
                }
            }
            var order = from r in test
                        orderby r.Value descending
                        select r;
            test = new Dictionary<string, int> { };
            foreach (var item in order)
            {
                test.Add(item.Key, item.Value);
            }
        }
    }
}