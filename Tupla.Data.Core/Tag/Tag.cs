using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CustomerData;

namespace Tupla.Data.Core.Tag
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public string TagName { get; set; }
        [Required]
        [ForeignKey("Customers")]
        public string Username { get; set; }
        public Customer Customers { get; set; }
    }
}
