using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.Shopping.CartData;

namespace Tupla.Data.Context
{
    public class SqlCartData : ICart
    {
        private readonly TuplaContext db;

        public SqlCartData(TuplaContext db)
        {
            this.db = db;
        }
        public Cart Add(Cart newCart)
        {
            db.Add(newCart);
            return newCart;
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

        public void Delete(Cart deleteCart)
        {
            db.Remove(deleteCart);
        }

        public void DeleteItemInCart(string id)
        {
            var allitemincart = this.GetByCartId(id);
            foreach (var item in allitemincart)
            {
                this.Delete(item);
            }
        }

        public IEnumerable<Cart> GetByCartId(string id)
        {
            var query = from r in db.Cart
                        where r.CartId == id
                        orderby r.GameId, r.PlatformId
                        select r;
            return query;
        }

        public Cart GetById(int GameId, int PlatformId, string username)
        {
            return db.Cart.FirstOrDefault(r => r.GameId == GameId && r.PlatformId == PlatformId && r.CartId == username);
        }

        public async Task<Cart> GetByIdAsync(int GameId, int PlatformId, string username)
        {
            return await db.Cart.FirstOrDefaultAsync(r => r.GameId == GameId && r.PlatformId == PlatformId && r.CartId == username);
        }

        public Cart Update(Cart updatedCart)
        {
            var entity = db.Cart.Attach(updatedCart);
            entity.State = EntityState.Modified;
            return updatedCart;
        }
    }
}
