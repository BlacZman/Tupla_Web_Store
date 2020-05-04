using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CompanyData;

namespace Tupla.Data.Core.GameData
{
    public class Game
    {
        [Key]
        [Display(Name = "Game ID")]
        public int GameId { get; set; }
        [Required]
        [Display(Name = "Name")]
        [DataType(DataType.Text)]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string GameName { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [StringLength(255, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 3)]
        public string Description { get; set; }
        [Required]
        [Display(Name = "Html text")]
        [DataType(DataType.Text)]
        public string HtmlText { get; set; }
        [Required]
        [DataType(DataType.Currency)]
        [Range(minimum: 1, maximum: 10000, ErrorMessage = "The {0} must be at least {1} and at max {2}.")]
        public decimal Price { get; set; }
        [Required]
        [Display(Name = "Release date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        [Required]
        [ForeignKey("Company")]
        public int CompanyID { get; set; }
        public Company Company { get; set; }
    }
}
