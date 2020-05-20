using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.CreditCard;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Areas.Identity.Pages.Account.Manage
{
    [Authorize]
    public class CreditcardModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ICreditCard creditdb;

        public CreditcardModel(UserManager<User> userManager,
            ICreditCard creditdb)
        {
            this.userManager = userManager;
            this.creditdb = creditdb;
        }
        [TempData]
        public string StatusMessage { get; set; }
        public IEnumerable<CreditCard> credit { get; set; }
        [BindProperty]
        public CreditCard Credit { get; set; }
        public string username { get; set; }
        public async Task<IActionResult> OnGetAsync()
        {
            await Task.Run(()=>
            {
                username = userManager.GetUserName(User);
                Credit = new CreditCard();
            });
            if (username == null || username == "") return RedirectToPage("~/NotFound");
            credit = creditdb.GetCreditCardByUsername(username);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var creditid = Credit.CreditId;
            var username = Credit.Username;
            Credit = creditdb.GetById(creditid);
            if (Credit == null)
            {
                if(creditid == null || username == null) return RedirectToPage(Url.Content("~/NotFound"));
                await Task.Run(()=> 
                {
                    Credit = new CreditCard();
                    Credit.CreditId = creditid;
                    Credit.Username = username;
                });
                creditdb.Add(Credit);
                await creditdb.CommitAsync();
            }
            else
            {
                creditdb.Delete(Credit);
                await creditdb.CommitAsync();
            }
            StatusMessage = "Your payment method has been updated";
            return RedirectToPage();
        }
    }
}
