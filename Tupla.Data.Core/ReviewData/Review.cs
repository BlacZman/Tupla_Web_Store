using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.Shopping.OrderDetailData;

namespace Tupla.Data.Core.ReviewData
{
    public class Review
    {
        [Key, Column(Order = 1)]
        public int OrderId { get; set; }
        [Key, Column(Order = 2)]
        public int GameId { get; set; }
        [Key, Column(Order = 3)]
        public int PlatformId { get; set; }
        [ForeignKey("OrderId,GameId,PlatformId")]
        public OrderDetail OrderDetail { get; set; }
        public bool Recommended { get; set; }
        [DataType(DataType.MultilineText)]
        public string Review_Detail { get; set; }
    }
}
