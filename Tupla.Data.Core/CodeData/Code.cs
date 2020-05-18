using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.Shopping.OrderDetailData;

namespace Tupla.Data.Core.CodeData
{
    public class Code
    {
        [Key]
        public string CodeId { get; set; }
        [Required]
        [Column(Order = 1)]
        public int OrderId { get; set; }
        [Required]
        [Column(Order = 2)]
        public int GameId { get; set; }
        [Required]
        [Column(Order = 3)]
        public int PlatformId { get; set; }
        [ForeignKey("OrderId,GameId,PlatformId")]
        public OrderDetail OrderDetail { get; set; }
    }
}
