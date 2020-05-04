using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using Tupla.Data.Core.CompanyData;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace Tupla.Data.Context
{
    public class SqlCompanyData : ICompany
    {
        private readonly TuplaContext db;

        public SqlCompanyData(TuplaContext db)
        {
            this.db = db;
        }
        public Company Add(Company newCustomer)
        {
            db.Add(newCustomer);
            return newCustomer;
        }

        public int Commit()
        {
            return db.SaveChanges();
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

        public void Delete(Company deleteCustomer)
        {
            db.Remove(deleteCustomer);
        }

        public Company GetById(int? id)
        {
            return db.Company.FirstOrDefault(r => r.companyId == id);
        }

        public IEnumerable<Company> GetCompaniesByName(string name)
        {
            var query = from r in db.Company
                        where r.company_name.StartsWith(name) || string.IsNullOrEmpty(name)
                        orderby r.company_name
                        select r;
            return query;
        }

        public Company Update(Company updateCustomer)
        {
            var entity = db.Company.Attach(updateCustomer);
            entity.State = EntityState.Modified;
            return updateCustomer;
        }
    }
}
