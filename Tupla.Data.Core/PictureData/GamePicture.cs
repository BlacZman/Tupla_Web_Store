using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.GameData;

namespace Tupla.Data.Core.PictureData
{
    public class GamePicture
    {
        [Key]
        [Display(Name = "Path")]
        public string Path { get; set; }
        [Required]
        [Display(Name = "Image type")]
        [DataType(DataType.Text)]
        public ImageType imageType { get; set; }
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
    }
}
