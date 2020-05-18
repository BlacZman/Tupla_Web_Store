using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.ReviewData;
using Tupla.Data.Core.GameData;

namespace Tupla.Data.Context
{
    public class SqlReviewData : IReview
    {
        private readonly TuplaContext db;

        public SqlReviewData(TuplaContext db)
        {
            this.db = db;
        }
        public Review Add(Review newReview)
        {
            db.Add(newReview);
            return newReview;
        }

        public int Commit()
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public async Task<int> CommitAsync()
        {
            try
            {
                return await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                throw;
            }
        }

        public void Delete(Review deleteReview)
        {
            db.Remove(deleteReview);
        }

        public IEnumerable<Review> GetByGameId(int GameId)
        {
            var query = from r in db.Review
                        where r.GameId == GameId
                        orderby r.OrderId descending
                        select r;
            return query;
        }

        public Review GetById(int OrderId, int GameId, int PlatformId)
        {
            return db.Review.FirstOrDefault(r => r.OrderId == OrderId && r.GameId == GameId && r.PlatformId == PlatformId);
        }

        public async Task<Review> GetByIdAsync(int OrderId, int GameId, int PlatformId)
        {
            return await db.Review.FirstOrDefaultAsync(r => r.OrderId == OrderId && r.GameId == GameId && r.PlatformId == PlatformId);
        }

        public Review Update(Review updatedReview)
        {
            var entity = db.Review.Attach(updatedReview);
            entity.State = EntityState.Modified;
            return updatedReview;
        }
    }
}
