using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.ReviewData
{
    public interface IReview
    {
        Review GetById(int OrderId, int GameId, int PlatformId);
        Task<Review> GetByIdAsync(int OrderId, int GameId, int PlatformId);
        IEnumerable<Review> GetByGameId(int GameId);
        Review Add(Review newReview);
        Review Update(Review updatedReview);
        void Delete(Review deleteReview);
        int Commit();
        Task<int> CommitAsync();
    }
}
