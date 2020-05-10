using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CustomerData;

namespace Tupla.Data.Core.PictureData
{
    public class CustomerPicture
    {
        [Key]
        [Display(Name = "Path")]
        public string Path { get; set; }
        [Required]
        [Display(Name = "Image type")]
        [DataType(DataType.Text)]
        public ImageType imageType { get; set; }
        [Required]
        [ForeignKey("Customers")]
        public string Username { get; set; }
        public Customer Customers { get; set; }
    }
}
