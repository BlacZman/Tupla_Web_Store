using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tupla.Data.Core.PromotionData
{
    public class EventPromotion
    {
        [Key]
        [Display(Name = "Event ID")]
        public int EventId { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [Display(Name = "Event name")]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Event_name { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event start date")]
        public DateTime Event_start_date { get; set; }
        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Event end date")]
        public DateTime Event_end_date { get; set; }
    }
}
