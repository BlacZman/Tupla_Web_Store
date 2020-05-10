using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CustomerData;

namespace Tupla.Data.Core.CreditCard
{
    public class CreditCard
    {
        [Key]
        [Required]
        [Display(Name = "Credit card ID")]
        [DataType(DataType.CreditCard, ErrorMessage = "You need to enter a valid credit card.")]
        public string CreditId { get; set; }
        [Required]
        [MaxLength(256)]
        [ForeignKey("Customers")]
        public string Username { get; set; }
        public Customer Customers { get; set; }
    }
}
