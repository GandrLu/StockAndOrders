using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Model
{
    public class ShippingMethod
    {
        private int id;
        private string name;

        public ShippingMethod(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }

    public class Order
    {
        private int id;
        private Customer customer;
        private Address billingAddress;
        private Address shippingAddress;
        private List<Item> orderedItems;

        public Order(int id, Customer customer, Address billingAddress, Address shippingAddress, List<Item> orderedItems)
        {
            Id = id;
            Customer = customer;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            OrderedItems = orderedItems;
        }

        public int Id { get => id; set => id = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public Address BillingAddress { get => billingAddress; set => billingAddress = value; }
        public Address ShippingAddress { get => shippingAddress; set => shippingAddress = value; }
        public List<Item> OrderedItems { get => orderedItems; set => orderedItems = value; }
    }
}
