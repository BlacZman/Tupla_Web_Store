using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tupla.Data.Core.CustomerData
{
    public interface ICustomer
    {
        IEnumerable<Customer> GetCustomersByUsername(string username);
        Customer GetByUsername(string username);
        Customer Update(Customer newCustomer);
        Customer UpdateEmail(Customer newCustomer);
        Customer UpdatePassword(Customer newCustomer);
        Customer Add(Customer newCustomer);
        int Commit();
    }
}
