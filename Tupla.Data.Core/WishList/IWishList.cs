using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.WishList
{
    public interface IWishList
    {
        WishList GetById(int gameId, string username);
        IEnumerable<WishList> GetWishListsByUsername(string username);
        WishList Add(WishList addWishList);
        void Delete(WishList delWishList);
        int Commit();
        Task<int> CommitAsync();
    }
}
