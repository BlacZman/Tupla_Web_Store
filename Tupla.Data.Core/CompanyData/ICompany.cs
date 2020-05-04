using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Tupla.Data.Core.CompanyData
{
    public interface ICompany
    {
        IEnumerable<Company> GetCompaniesByName(string name);
        Company GetById(int? id);
        Company Update(Company updateCustomer);
        Company Add(Company newCustomer);
        void Delete(Company deleteCustomer);
        int Commit();
        Task<int> CommitAsync();
    }
}
