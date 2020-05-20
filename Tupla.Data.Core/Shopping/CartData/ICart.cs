using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.Shopping.CartData
{
    public interface ICart
    {
        Cart GetById(int GameId, int PlatformId, string username);
        Task<Cart> GetByIdAsync(int GameId, int PlatformId, string username);
        IEnumerable<Cart> GetByCartId(string id);
        Cart Add(Cart newCart);
        Cart Update(Cart updatedCart);
        void Delete(Cart deleteCart);
        void DeleteItemInCart(string id);
        int Commit();
        Task<int> CommitAsync();
    }
}
