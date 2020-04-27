using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Tupla.Data.Core.CustomerData
{
    public class Customer
    {
        /*data validation
        https://docs.microsoft.com/en-us/aspnet/core/mvc/models/validation?view=aspnetcore-3.1
        https://www.tutorialspoint.com/asp.net/asp.net_validators.htm
        */
        [Required, StringLength(25), Key, DataType(DataType.Text)]
        public string Username { get; set; }
        [Required, StringLength(40), DataType(DataType.Password)]
        public string Password { get; set; }
        [Required, StringLength(40), DataType(DataType.Text)]
        public string First_name { get; set; }
        [Required, StringLength(40), DataType(DataType.Text)]
        public string Last_name { get; set; }
        [Required, StringLength(40), DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        [Required, DataType(DataType.PhoneNumber)]
        public string Telephone { get; set; }
        public Gender Gender { get; set; }
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:d}")]
        [DataType(DataType.Date)]
        public DateTime Date_of_birth { get; set; }
        [StringLength(255), DataType(DataType.Text)]
        public string Address { get; set; }
        [DataType(DataType.PostalCode)]
        public string Zipcode { get; set; }
        [StringLength(20), DataType(DataType.Text)]
        public string Country { get; set; }
        public int CompanyID { get; set; }
        [DataType(DataType.Date)]
        public DateTime UserCreateDate { get; set; }
    }
}
