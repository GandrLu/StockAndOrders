using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Model
{
    public class Customer
    {
        private static List<Customer> customers = new List<Customer>();
        private int id;
        private string firstname;
        private string surname;
        private Address address;

        public Customer()
        {
            Id = Customers.Count;
            Customers.Add(this);
        }

        public Customer(string firstname, string surname, Address address)
        {
            Firstname = firstname;
            Surname = surname;
            Address = address;
            
            Id = Customers.Count;
            Customers.Add(this);
        }

        public Customer(int id, string firstname, string surname, Address address) : this(firstname, surname, address)
        {
            Id = id;
        }

        public static List<Customer> Customers { get => customers; set => customers = value; }
        public int Id { get => id; set => id = value; }
        public string Firstname { get => firstname; set => firstname = value; }
        public string Surname { get => surname; set => surname = value; }
        public Address Address { get => address; set => address = value; }

        public override string ToString()
        {
            return Id + ", " + Firstname + ", " + Surname + ", " + Address.ToString();
        }
    }
}
