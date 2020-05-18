using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Transactions;
using Tupla.Data.Core.CodeData;
using Tupla.Data.Core.CompanyData;
using Tupla.Data.Core.CreditCard;
using Tupla.Data.Core.CustomerData;
using Tupla.Data.Core.GameData;
using Tupla.Data.Core.PictureData;
using Tupla.Data.Core.PlatformData;
using Tupla.Data.Core.PromotionData;
using Tupla.Data.Core.ReviewData;
using Tupla.Data.Core.Shopping.CartData;
using Tupla.Data.Core.Shopping.OrderDetailData;
using Tupla.Data.Core.Shopping.TransactionData;
using Tupla.Data.Core.Tag;
using Tupla.Data.Core.WishList;

namespace Tupla.Data.Context
{
    public class TuplaContext : DbContext
    {
        public TuplaContext(DbContextOptions<TuplaContext> options) : base (options)
        {

        }
        public DbSet<Company> Company { get; set; }
        public DbSet<Game> Game { get; set; }
        //Update
        public DbSet<GamePicture> GamePicture { get; set; }
        public DbSet<CompanyPicture> CompanyPicture { get; set; }
        public DbSet<CustomerPicture> CustomerPicture { get; set; }
        //Update
        public DbSet<Platform> Platform { get; set; }
        public DbSet<PlatformOfGame> PlatformOfGame { get; set; }
        public DbSet<Tag> Tag { get; set; }
        public DbSet<GameTag> GameTag { get; set; }
        //Update
        public DbSet<WishList> WishList { get; set; }
        public DbSet<CreditCard> CreditCard { get; set; }
        //Update
        public DbSet<Cart> Cart { get; set; }
        public DbSet<OrderDetail> OrderDetail { get; set; }
        public DbSet<Transac> Transaction { get; set; }
        //Review
        public DbSet<Review> Review { get; set; }
        //Code
        public DbSet<Code> Code { get; set; }
        //Promotion
        public DbSet<EventPromotion> EventPromotion { get; set; }
        public DbSet<Promotion> Promotion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<PlatformOfGame>()
                .HasKey(c => new { c.GameId, c.PlatformId });
            modelBuilder.Entity<GameTag>()
                .HasKey(c => new { c.GameId, c.TagId, c.Username });
            modelBuilder.Entity<WishList>().
                HasKey(c => new { c.GameId, c.Username });
            modelBuilder.Entity<OrderDetail>().
                HasKey(c => new { c.OrderId, c.GameId, c.PlatformId });
            modelBuilder.Entity<Cart>().
                HasKey(c => new { c.GameId, c.PlatformId, c.CartId });
            modelBuilder.Entity<Review>().
               HasKey(c => new { c.OrderId, c.GameId, c.PlatformId });
        }
    }
}
