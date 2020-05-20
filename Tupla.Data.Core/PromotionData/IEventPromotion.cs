using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PromotionData
{
    public interface IEventPromotion
    {
        EventPromotion GetById(int EventId);
        IEnumerable<EventPromotion> GetAllPre();
        EventPromotion Add(EventPromotion newEventPromotion);
        void Delete(EventPromotion deleteEventPromotion);
        int Commit();
        Task<int> CommitAsync();
    }
}
