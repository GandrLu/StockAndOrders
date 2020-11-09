using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAndOrders.Model
{
    public class Address
    {
        #region Fields
        private int id;
        private string postalCode;
        private string housenumber;
        private string street;
        private string city;
        #endregion

        #region Constructors
        public Address()
        {

        }

        public Address(string street, string housenumber, string postalCode, string city)
        {
            PostalCode = postalCode;
            Housenumber = housenumber;
            Street = street;
            City = city;
        }

        public Address(int id, string street, string housenumber, string postalCode, string city) : this(street, housenumber, postalCode, city)
        {
            ID = id;
        }
        #endregion

        #region Properties
        public int ID { get => id; set => id = value; }
        public string PostalCode { get => postalCode; set => postalCode = value; }
        public string Housenumber { get => housenumber; set => housenumber = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
        #endregion

        public override string ToString()
        {
            return Street + ", " + Housenumber + ", " + PostalCode + ", " + City;
        }
    }
}
