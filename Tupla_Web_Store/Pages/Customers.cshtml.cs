using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Tupla_Web_Store.AllData.customerData;

namespace Tupla_Web_Store.Pages
{
    public class CustomersModel : PageModel
    {
        private readonly ICustomer customerData;

        [BindProperty(SupportsGet = true)]
        public string SearchUser { get; set; }
        public IEnumerable<Customer> Customers { get; set; }

        public CustomersModel(ICustomer customerData)
        {
            this.customerData = customerData;
        }
        public void OnGet()
        {
            Customers = customerData.GetCustomersByUsername(SearchUser);
        }
    }
}