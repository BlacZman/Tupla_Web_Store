using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Tupla.Data.Core.CustomerData;

namespace Tupla_Web_Store.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the User class
    public class User : IdentityUser
    {
        [PersonalData]
        public string First_name { get; set; }
        [PersonalData]
        public string Last_name { get; set; }
        [PersonalData]
        public Gender Gender { get; set; }
        [PersonalData]
        public DateTime Date_of_birth { get; set; }
        [PersonalData]
        public string Address { get; set; }
        [PersonalData]
        public string Zipcode { get; set; }
        [PersonalData]
        public string Country { get; set; }
        public int CompanyID { get; set; }
        [PersonalData]
        public DateTime UserCreateDate { get; set; }
    }
}
