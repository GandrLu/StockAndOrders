using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopManager.Model;
using System.Collections.Generic;

namespace ShopManagerTests
{
    [TestClass]
    public class ItemOrderTests
    {
        private Category defaultCategory;
        private Item item;
        private ShippingMethod shippingMethod;
        private Order order;

        [TestMethod]
        public void CreateItemAndCategory()
        {
            defaultCategory = new Category(123, "Default");
            item = new Item(123, "Tasse", "300ml mit Füßen", "Blau", defaultCategory, 15.5f, 29);

            Assert.IsNotNull(item);
        }

        [TestMethod]
        public void CreateOrderAndShippingMethod()
        {
            Address address = new Address("Main Street", "1", 12345, "Maine");
            Customer customer = new Customer(123, "John", "Doe", address);

            shippingMethod = new ShippingMethod(123, "DHL");
            List<Item> orderedItems = new List<Item>();
            orderedItems.Add(item);
            order = new Order(123, customer, address, address, orderedItems);

            Assert.IsNotNull(order);
        }
    }
}
