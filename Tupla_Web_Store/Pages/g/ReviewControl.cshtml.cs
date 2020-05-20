using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.ReviewData;

namespace Tupla_Web_Store.Pages.g
{
    [Authorize]
    public class ReviewControlModel : PageModel
    {
        private readonly IReview reviewdb;

        public ReviewControlModel(IReview reviewdb)
        {
            this.reviewdb = reviewdb;
        }

        [BindProperty]
        public Review yourReview { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("../Index");
        }
        public async Task<IActionResult> OnPost()
        {
            if (yourReview == null) return RedirectToPage("../Index");
            var checkReview = reviewdb.GetById(yourReview.OrderId, yourReview.GameId, yourReview.PlatformId);
            if(checkReview == null)
            {
                reviewdb.Add(yourReview);
                await reviewdb.CommitAsync();
            }
            else
            {
                checkReview.Recommended = yourReview.Recommended;
                checkReview.Review_Detail = yourReview.Review_Detail;
                reviewdb.Update(checkReview);
                await reviewdb.CommitAsync();
            }
            return RedirectToPage("./Detail", new { id = yourReview.GameId});
        }
    }
}