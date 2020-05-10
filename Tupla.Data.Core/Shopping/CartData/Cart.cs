using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.PlatformData;

namespace Tupla.Data.Core.Shopping.CartData
{
    public class Cart
    {
        [Key, Column(Order = 1)]
        public int GameId { get; set; }
        [Key, Column(Order = 2)]
        public int PlatformId { get; set; }
        [ForeignKey("GameId,PlatformId")]
        public PlatformOfGame PlatformOfGame { get; set; }
        [Key, Column(Order = 3)]
        [ForeignKey("Customers")]
        [MaxLength(256)]
        public string CartId { get; set; }
        public Customer Customers { get; set; }
        [Range(1,100)]
        public int Quantity { get; set; }
    }
}
