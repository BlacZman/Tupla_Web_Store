using System.ComponentModel.DataAnnotations;

namespace Tupla.Data.Core.Tag
{
    public class DelTagFormModel
    {
        [Required]
        public int? gameid { get; set; }
        [Required]
        public int? tagid { get; set; }
    }
}
