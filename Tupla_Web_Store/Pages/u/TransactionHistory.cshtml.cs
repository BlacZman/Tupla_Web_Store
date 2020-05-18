using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.Shopping.TransactionData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.u
{
    [Authorize]
    public class TransactionHistoryModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ITransaction transdb;

        public TransactionHistoryModel(UserManager<User> userManager,
            ITransaction transdb)
        {
            this.userManager = userManager;
            this.transdb = transdb;
        }
        public IEnumerable<Transac> carts { get; set; }
        public void OnGet()
        {
            var username = userManager.GetUserName(User);
            carts = transdb.GetByUsername(username);
        }
    }
}