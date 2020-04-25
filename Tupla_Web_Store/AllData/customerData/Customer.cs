using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tupla_Web_Store.AllData.customerData
{
    public class Customer
    {
        /*data validation
        https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1
        https://www.tutorialspoint.com/asp.net/asp.net_validators.htm
        */
        [Required, StringLength(25), Key]
        public string Username { get; set; }
        [Required, StringLength(40)]
        public string Password { get; set; }
        [Required, StringLength(40)]
        public string First_name { get; set; }
        [Required, StringLength(40)]
        public string Last_name { get; set; }
        [Required, StringLength(40), EmailAddress]
        public string Email { get; set; }
        [Required, Phone]
        public string Telephone { get; set; }
        public Gender Gender { get; set; }
        public DateTime Date_of_birth { get; set; }
        [StringLength(255)]
        public string Address { get; set; }
        [StringLength(5)]
        public string Zipcode { get; set; }
        [StringLength(20)]
        public string Country { get; set; }
        public int CompanyID { get; set; }
        public DateTime UserCreateDate { get; set; }
    }
}
