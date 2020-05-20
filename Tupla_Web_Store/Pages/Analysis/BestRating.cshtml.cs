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
    public class BestRatingModel : PageModel
    {
        private readonly TuplaContext context;
        private readonly IGame gamedb;

        public BestRatingModel(TuplaContext context,
            IGame gamedb)
        {
            this.context = context;
            this.gamedb = gamedb;
        }
        public Dictionary<Game, double> test { get; set; }


        public async Task OnGet()
        {
            test = new Dictionary<Game, double> { };
            var calculate = new Dictionary<Game, getReviewModel> { };
            await Task.Run(() =>
            {
                var query = from r in context.Review
                            orderby r.GameId
                            select r;
                foreach (var r in query)
                {
                    var game = gamedb.GetById(r.GameId);
                    int good = r.Recommended ? 1 : 0;
                    int bad = r.Recommended ? 0 : 1;
                    if (calculate.ContainsKey(game))
                    {
                        getReviewModel getReview = new getReviewModel
                        {
                            total = calculate[game].total + 1,
                            good = calculate[game].good + good,
                            bad = calculate[game].bad + bad
                        };
                        calculate[game] = null;
                        calculate[game] = getReview;

                    }
                    else
                    {
                        getReviewModel getReview = new getReviewModel
                        {
                            total = 1,
                            good = good,
                            bad = bad
                        };
                        calculate.Add(game, getReview);
                    }
                    Console.WriteLine("Test: " + game.GameName + " Value:" + good.ToString() + ", " + bad.ToString());
                }
                if (calculate.Any())
                {
                    Console.WriteLine("Test: " + calculate.Count);
                    var boopbeep = calculate.AsEnumerable();
                    foreach (var item in boopbeep)
                    {
                        test.Add(item.Key, (double)item.Value.good / (double)item.Value.total);
                    }
                    var order = from r in test
                                where r.Value >= 0.5
                                orderby r.Value descending, r.Key.Price
                                select r;
                    test = new Dictionary<Game, double> { };
                    foreach (var item in order) test.Add(item.Key, item.Value);
                }
            });

        }
        private class getReviewModel
        {
            public int total { get; set; }
            public int bad { get; set; }
            public int good { get; set; }
        }
    }
}