using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.GameData;

namespace Tupla.Data.Core.Tag
{
    public class GameTag
    {
        [Key, Column(Order = 1)]
        [Required]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Key, Column(Order = 2)]
        [Required]
        [ForeignKey("Tag")]
        public int TagId { get; set; }
        public Tag Tag { get; set; }
        [Key, Column(Order = 3)]
        [Required]
        [ForeignKey("Customers")]
        public string Username { get; set; }
        public Customer Customers { get; set; }
    }
}
