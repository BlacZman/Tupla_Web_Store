using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.CreditCard;

namespace Tupla.Data.Context
{
    public class SqlCreditCardData : ICreditCard
    {
        private readonly TuplaContext db;

        public SqlCreditCardData(TuplaContext db)
        {
            this.db = db;
        }
        public CreditCard Add(CreditCard addCreditCard)
        {
            db.Add(addCreditCard);
            return addCreditCard;
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

        public void Delete(CreditCard delCreditCard)
        {
            db.Remove(delCreditCard);
        }

        public CreditCard GetById(string CreditId)
        {
            return db.CreditCard.FirstOrDefault(r => r.CreditId == CreditId);
        }

        public IEnumerable<CreditCard> GetCreditCardByUsername(string username)
        {
            var query = from r in db.CreditCard
                        where string.IsNullOrEmpty(username) || r.Username == username
                        orderby r.CreditId
                        select r;
            return query;
        }
    }
}
