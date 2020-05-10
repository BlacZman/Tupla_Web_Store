using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tupla.Data.Core.Shopping.TransactionData;

namespace Tupla.Data.Context
{
    public class SqlTransactionData : ITransaction
    {
        private readonly TuplaContext db;

        public SqlTransactionData(TuplaContext db)
        {
            this.db = db;
        }
        public Transac Add(Transac newTransaction)
        {
            db.Add(newTransaction);
            return newTransaction;
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

        public void Delete(Transac deleteTransaction)
        {
            db.Remove(deleteTransaction);
        }

        public Transac GetById(int Id)
        {
            return db.Transaction.FirstOrDefault(r => r.OrderId == Id);
        }

        public async Task<Transac> GetByIdAsync(int Id)
        {
            return await db.Transaction.FirstOrDefaultAsync(r => r.OrderId == Id);
        }

        public IEnumerable<Transac> GetByUsername(string username)
        {
            var query = from r in db.Transaction
                        where string.IsNullOrEmpty(username) || r.Username == username
                        orderby r.OrderDate descending
                        select r;
            return query;
        }
    }
}
