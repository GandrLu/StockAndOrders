using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Model
{
    public class Address
    {
        #region Fields
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
        #endregion

        #region Properties
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
