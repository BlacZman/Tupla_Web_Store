using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Tupla.Data.Core.PlatformData
{
    public class Platform
    {
        [Key]
        [Display(Name = "Platform ID")]
        public int PlatformId { get; set; }
        [Required]
        [Display(Name = "Platform name")]
        [DataType(DataType.Text)]
        public string Platform_name { get; set; }
    }
}
