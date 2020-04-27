using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Tupla.Data.Core.CustomerData;

namespace Tupla_Web_Store.Pages.User
{
    public class CreateModel : PageModel
    {
        private readonly ICustomer customerData;
        private readonly IHtmlHelper htmlHelper;

        [BindProperty]
        public Customer User { get; set; }
        public IEnumerable<SelectListItem> Genders { get; set; }
        public CreateModel(ICustomer customerData, IHtmlHelper htmlHelper)
        {
            this.customerData = customerData;
            this.htmlHelper = htmlHelper;
        }
        public void OnGet()
        {
            Genders = htmlHelper.GetEnumSelectList<Gender>();
            User = new Customer();
        }
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                Genders = htmlHelper.GetEnumSelectList<Gender>();
                return Page();
            }
            User.UserCreateDate = DateTime.Now;
            customerData.Add(User);
            TempData["Edit"] = "Your account have been created!";
            customerData.Commit();
            return RedirectToPage("./u", new { Username = User.Username });
        }
    }
}