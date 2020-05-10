using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.CreditCard
{
    public interface ICreditCard
    {
        CreditCard GetById(string CreditId);
        IEnumerable<CreditCard> GetCreditCardByUsername(string username);
        CreditCard Add(CreditCard addCreditCard);
        void Delete(CreditCard delCreditCard);
        int Commit();
        Task<int> CommitAsync();
    }
}
