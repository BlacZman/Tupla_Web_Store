using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Tupla.Data.Core.Shopping.CartData;
using Tupla_Web_Store.Areas.Identity.Data;

namespace Tupla_Web_Store.Pages.c
{
    public class DeleteItemModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly ICart cartdb;

        public DeleteItemModel(UserManager<User> userManager,
            ICart cartdb)
        {
            this.userManager = userManager;
            this.cartdb = cartdb;
        }
        public void OnGet()
        {

        }
        public void OnPost()
        {

        }
    }
}