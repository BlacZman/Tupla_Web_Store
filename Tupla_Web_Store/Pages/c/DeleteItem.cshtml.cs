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
        [BindProperty]
        public Cart ModifyCart { get; set; }

        public IActionResult OnGet()
        {
            return RedirectToPage("./Index");
        }
        public async Task<IActionResult> OnPostAsync()
        {
            var username = userManager.GetUserName(User);
            if (username == null) return RedirectToPage("../Index");
            if(ModifyCart == null) return RedirectToPage("../Index");
            var cartitem = cartdb.GetById(ModifyCart.GameId, ModifyCart.PlatformId, username);
            if(cartitem == null) return RedirectToPage("../Index");
            await Task.Run(async ()=> 
            {
                cartdb.Delete(cartitem);
                await cartdb.CommitAsync();
                TempData["StatusItem"] = "Your item has been deleted!";
            });
            return RedirectToPage("./Index");
        }
    }
}