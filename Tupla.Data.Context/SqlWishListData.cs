using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.WishList;

namespace Tupla.Data.Context
{
    public class SqlWishListData : IWishList
    {
        private readonly TuplaContext db;

        public SqlWishListData(TuplaContext db)
        {
            this.db = db;
        }
        public WishList Add(WishList addWishList)
        {
            db.Add(addWishList);
            return addWishList;
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

        public void Delete(WishList delWishList)
        {
            db.Remove(delWishList);
        }

        public WishList GetById(int gameId, string username)
        {
            return db.WishList.FirstOrDefault(r => r.GameId == gameId && r.Username == username);
        }

        public IEnumerable<WishList> GetWishListsByUsername(string username)
        {
            var query = from r in db.WishList
                        where string.IsNullOrEmpty(username) || r.Username == username
                        orderby r.GameId
                        select r;
            return query;
        }
    }
}
