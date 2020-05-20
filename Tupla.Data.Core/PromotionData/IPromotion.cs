using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.PromotionData
{
    public interface IPromotion
    {
        Promotion GetById(int PromotionId);
        IEnumerable<Promotion> GetByActivePlatform(int gameid, int platformid);
        IEnumerable<Promotion> GetBySpecificDatePlatform(int gameid, int platformid, DateTime date);
        IEnumerable<Promotion> GetAllActivePromotion(int gameid);
        Promotion Add(Promotion newPromotion);
        void Delete(Promotion deletePromotion);
        int Commit();
        Task<int> CommitAsync();
    }
}
