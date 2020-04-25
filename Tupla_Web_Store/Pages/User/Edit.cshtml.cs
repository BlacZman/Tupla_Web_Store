using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla_Web_Store.AllData.customerData;

namespace Tupla_Web_Store.Pages.User
{
    public class EditModel : PageModel
    {
        private readonly ICustomer customerData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Customer User { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public EditModel(ICustomer customerData, IHtmlHelper htmlHelper)
        {
            this.customerData = customerData;
            this.htmlHelper = htmlHelper;
        }
        public IActionResult OnGet(string Username)
        {
            Genders = htmlHelper.GetEnumSelectList<Gender>();
            User = customerData.GetByUsername(Username);
            if (User == null)
            {
                return RedirectToPage("../NotFound");
            }
            return Page();
        }
        public IActionResult OnPost()
        {
            if(!ModelState.IsValid)
            {
                Genders = htmlHelper.GetEnumSelectList<Gender>();
                return Page();
            }
            customerData.Update(User);
            TempData["Edit"] = "Your profile has been saved!";
            customerData.Commit();
            return RedirectToPage("./u", new { Username = User.Username });
        }
    }
}