using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Tupla.Data.Core.CompanyData;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.Tag;

namespace Tupla.Data.Context
{
    public class TuplaContext : DbContext
    {
        public TuplaContext(DbContextOptions<TuplaContext> options) : base (options)
        {

        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Game> Game { get; set; }
        public DbSet<GamePicture> GamePicture { get; set; }
        public DbSet<CompanyPicture> CompanyPicture { get; set; }
        public DbSet<CustomerPicture> CustomerPicture { get; set; }
        public DbSet<Platform> Platform { get; set; }
        public DbSet<PlatformOfGame> PlatformOfGame { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<GameTag> GameTag { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformOfGame>()
                .HasKey(c => new { c.GameId, c.PlatformId });
            modelBuilder.Entity<GameTag>()
                .HasKey(c => new { c.GameId, c.TagId, c.Username });
        }
    }
}
