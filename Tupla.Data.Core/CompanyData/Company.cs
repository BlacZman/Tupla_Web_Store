using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tupla.Data.Core.CompanyData
{
    public class Company
    {
        [Key]
        [Display(Name = "Company ID")]
        public int companyId { get; set; }
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string company_name { get; set; }
        [Required]
        [Display(Name = "Address")]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string company_address { get; set; }
        [Required]
        [Display(Name = "Bank name")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string bank_name { get; set; }
        [Required]
        [Display(Name = "Bank account")]
        [DataType(DataType.Text)]
        [StringLength(25, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string bank_account { get; set; }
        [Required]
        [Display(Name = "Country")]
        [DataType(DataType.Text)]
        [StringLength(30, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string company_country { get; set; }
        [Required]
        [Display(Name = "Zipcode")]
        [DataType(DataType.PostalCode)]
        [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 5)]
        public string company_zipcode { get; set; }
        [Required]
        [Display(Name = "Created since")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:D}")]
        public DateTime company_create_date { get; set; }
    }
}
