using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.Shopping.TransactionData;

namespace Tupla.Data.Core.Shopping.OrderDetailData
{
    public class OrderDetail
    {
        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Transaction")]
        public int OrderId { get; set; }
        public Transac Transaction { get; set; }
        [Key, Column(Order = 2)]
        public int GameId { get; set; }
        [Key, Column(Order = 3)]
        public int PlatformId { get; set; }
        [ForeignKey("GameId,PlatformId")]
        public PlatformOfGame PlatformOfGame { get; set; }
        public int Quantity { get; set; }
    }
}
