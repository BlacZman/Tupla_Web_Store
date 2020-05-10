using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CompanyData;

namespace Tupla.Data.Core.PictureData
{
    public class CompanyPicture
    {
        [Key]
        [Display(Name = "Path")]
        public string Path { get; set; }
        [Required]
        [Display(Name = "Image type")]
        [DataType(DataType.Text)]
        public ImageType imageType { get; set; }
        [Required]
        [ForeignKey("Company")]
        public int CompanyId { get; set; }
        public Company Company { get; set; }
    }
}
