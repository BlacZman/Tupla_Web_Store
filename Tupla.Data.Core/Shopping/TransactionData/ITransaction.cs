using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.Shopping.TransactionData
{
    public interface ITransaction
    {
        Transac GetById(int Id);
        Task<Transac> GetByIdAsync(int Id);
        IEnumerable<Transac> GetByUsername(string username);
        Transac Add(Transac newTransaction);
        void Delete(Transac deleteTransaction);
        int Commit();
        Task<int> CommitAsync();
    }
}
