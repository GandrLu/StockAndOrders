using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Model
{
    public class Address
    {
        private int housenumber;
        private int postalCode;
        private string street;
        private string city;

        public Address(int housenumber, int postalCode, string street, string city)
        {
            Housenumber = housenumber;
            PostalCode = postalCode;
            Street = street;
            City = city;
        }

        public int Housenumber { get => housenumber; set => housenumber = value; }
        public int PostalCode { get => postalCode; set => postalCode = value; }
        public string Street { get => street; set => street = value; }
        public string City { get => city; set => city = value; }
    }
}
