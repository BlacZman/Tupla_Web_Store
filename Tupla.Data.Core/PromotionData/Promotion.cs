using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.PlatformData;

namespace Tupla.Data.Core.PromotionData
{
    public class Promotion
    {
        [Key]
        public int PromotionId { get; set; }
        [Required]
        [Column(Order = 1)]
        public int GameId { get; set; }
        [Required]
        [Column(Order = 2)]
        public int PlatformId { get; set; }
        [ForeignKey("GameId,PlatformId")]
        public PlatformOfGame PlatformOfGame { get; set; }
        [Required]
        [Range(minimum: 1, maximum: 10000, ErrorMessage = "The {0} must be at least {1} and at max {2}.")]
        public int Percentage { get; set; }
        [Required]
        [ForeignKey("EventPromotion")]
        public int EventId { get; set; }
        public EventPromotion EventPromotion { get; set; }
    }
}
