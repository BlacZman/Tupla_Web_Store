using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tupla.Data.Core.Tag
{
    public class AddTagFormModel
    {
        [Required]
        public int? gameid { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "{0} must be a minimum of {1}")]
        public string tag { get; set; }
    }
}
