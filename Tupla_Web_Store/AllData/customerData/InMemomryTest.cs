using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tupla_Web_Store.AllData.customerData
{
    public class InMemomryTest : ICustomer
    {
        readonly List<Customer> Customers;
        public InMemomryTest()
        {
            Customers = new List<Customer>()
            {
                new Customer
                {
                    Username = "inky",
                    Password = "1235",
                    First_name = "ink",
                    Last_name = "noob",
                    Email = "inky@gmail.com",
                    Telephone = "0123569874",
                    Gender = Gender.Male,
                    Date_of_birth = new DateTime(2000, 1, 1),
                    Address = "123/45 sad boi",
                    Zipcode = "12356",
                    Country = "Thailand",
                    UserCreateDate = DateTime.Now
                },
                new Customer
                {
                    Username = "noob",
                    Password = "1236",
                    First_name = "123",
                    Last_name = "984",
                    Email = "555@gmail.com",
                    Telephone = "888888888",
                    Gender = Gender.Other,
                    Date_of_birth = new DateTime(1999, 3, 3),
                    Address = "089/3 ok olo",
                    Zipcode = "1150",
                    Country = "Singapore",
                    UserCreateDate = DateTime.Now
                }
            };
        }
        public Customer GetByUsername(string username)
        {
            return Customers.SingleOrDefault(r => r.Username == username);
        }
        public Customer Update(Customer newCustomer)
        {
            var customer = Customers.SingleOrDefault(r => r.Username == newCustomer.Username);
            if(customer != null)
            {
                customer.First_name = newCustomer.First_name;
                customer.Last_name = newCustomer.Last_name;
                customer.Telephone = newCustomer.Telephone;
                customer.Gender = newCustomer.Gender;
                customer.Date_of_birth = newCustomer.Date_of_birth;
                customer.Address = newCustomer.Address;
                customer.Zipcode = newCustomer.Zipcode;
                customer.Country = newCustomer.Country;
            }
            return customer;
        }
        public Customer Add(Customer newCustomer)
        {
            Customers.Add(newCustomer);
            return newCustomer;
        }

        public int Commit()
        {
            return 0;
        }

        public Customer UpdatePassword(Customer newCustomer)
        {
            var customer = Customers.SingleOrDefault(r => r.Username == newCustomer.Username);
            if (customer != null)
            {
                customer.Password = newCustomer.Password;
            }
            return customer;
        }

        public Customer UpdateEmail(Customer newCustomer)
        {
            var customer = Customers.SingleOrDefault(r => r.Username == newCustomer.Username);
            if (customer != null)
            {
                customer.Email = newCustomer.Email;
            }
            return customer;
        }
        public IEnumerable<Customer> GetCustomersByUsername(string username = null)
        {
            return from r in Customers
                   where string.IsNullOrEmpty(username) || r.Username.StartsWith(username)
                   orderby r.Username
                   select r;
        }
    }
}
