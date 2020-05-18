using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using Tupla.Data.Core.CustomerData;

namespace Tupla.Data.Core.Shopping.TransactionData
{
    public class Transac
    {
        [Key]
        public int OrderId { get; set; }
        [AllowNull]
        [ForeignKey("Customers")]
        [MaxLength(256)]
        public string Username { get; set; }
        public Customer Customers { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
    }
}
