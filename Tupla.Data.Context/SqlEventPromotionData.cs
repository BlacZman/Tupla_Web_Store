using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.PromotionData;

namespace Tupla.Data.Context
{
    public class SqlEventPromotionData : IEventPromotion
    {
        private readonly TuplaContext db;

        public SqlEventPromotionData(TuplaContext db)
        {
            this.db = db;
        }
        public EventPromotion Add(EventPromotion newEventPromotion)
        {
            db.Add(newEventPromotion);
            return newEventPromotion;
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

        public void Delete(EventPromotion deleteEventPromotion)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<EventPromotion> GetAllPre()
        {
            var query = from r in db.EventPromotion
                        where DateTime.Now < r.Event_start_date
                        orderby r.EventId
                        select r;
            return query;
        }

        public EventPromotion GetById(int EventId)
        {
            return db.EventPromotion.FirstOrDefault(r => r.EventId == EventId);
        }
    }
}
