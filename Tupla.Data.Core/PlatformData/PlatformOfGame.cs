using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using Tupla.Data.Core.GameData;

namespace Tupla.Data.Core.PlatformData
{
    public class PlatformOfGame
    {
        [Key, Column(Order = 1)]
        [ForeignKey("Game")]
        public int GameId { get; set; }
        public Game Game { get; set; }
        [Key, Column(Order = 2)]
        [ForeignKey("Platform")]
        public int PlatformId { get; set; }
        public Platform Platform { get; set; }
        public string Authentication { get; set; }
    }
}
