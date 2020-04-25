using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Tupla_Web_Store.AllData.customerData;

namespace Tupla_Web_Store.Pages.User
{
    public class uModel : PageModel
    {
        private readonly ICustomer customerData;
        public Customer User { get; set; }
        [TempData]
        public string Edit { get; set; }
        public uModel(ICustomer customerData)
        {
            this.customerData = customerData;
        }
        public IActionResult OnGet(string Username)
        {
            User = customerData.GetByUsername(Username);
            if(User == null)
            {
                return RedirectToPage("../NotFound");
            }
            return Page();
        }
    }
}