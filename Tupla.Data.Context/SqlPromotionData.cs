using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PromotionData;

namespace Tupla.Data.Context
{
    public class SqlPromotionData : IPromotion
    {
        private readonly TuplaContext db;

        public SqlPromotionData(TuplaContext db)
        {
            this.db = db;
        }
        public Promotion Add(Promotion newPromotion)
        {
            db.Add(newPromotion);
            return newPromotion;
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

        public void Delete(Promotion deletePromotion)
        {
            db.Remove(deletePromotion);
        }

        public IEnumerable<Promotion> GetAllActivePromotion(int gameid)
        {
            var query = from r in db.Promotion
                        join s in db.EventPromotion
                        on r.EventId equals s.EventId
                        where r.GameId == gameid && s.Event_end_date >= DateTime.Now
                        orderby r.GameId
                        select r;
            return query;
        }

        public IEnumerable<Promotion> GetByActivePlatform(int gameid, int platformid)
        {
            var query = from r in db.Promotion
                        join s in db.EventPromotion
                        on r.EventId equals s.EventId
                        where r.GameId == gameid && r.PlatformId == platformid && s.Event_end_date >= DateTime.Now
                        orderby r.GameId
                        select r;
            return query;
        }

        public Promotion GetById(int PromotionId)
        {
            return db.Promotion.FirstOrDefault(r => r.PromotionId == PromotionId);
        }

        public IEnumerable<Promotion> GetBySpecificDatePlatform(int gameid, int platformid, DateTime date)
        {
            var query = from r in db.Promotion
                        join s in db.EventPromotion
                        on r.EventId equals s.EventId
                        where r.GameId == gameid && r.PlatformId == platformid && date >= s.Event_start_date && date <= s.Event_end_date
                        orderby r.GameId
                        select r;
            return query;
        }
    }
}
