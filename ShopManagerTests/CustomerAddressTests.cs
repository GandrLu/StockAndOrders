using Microsoft.VisualStudio.TestTools.UnitTesting;
using ShopManager.Model;

namespace ShopManagerTests
{
    [TestClass]
    public class CustomerAddressTests
    {
        private Address address;
        private Customer customer;

        [TestMethod]
        public void NewAddress()
        {
            address = new Address("Main Street", "1", 12345, "Maine");

            Assert.IsNotNull(address);
            Assert.AreEqual(address.City, "Maine", address.City + " should be Maine");
        }

        [TestMethod]
        public void NewCustomer()
        {
            customer = new Customer(123, "John", "Doe", address);
            customer.Id = 321;
            customer.Firstname = "Jeff";
            customer.Surname = "Mayer";

            Assert.IsNotNull(customer);
            Assert.AreEqual(customer.Firstname, "Jeff", customer.Firstname + " should be Jeff");
            Assert.AreEqual(customer.Surname, "Mayer", customer.Surname + " should be Mayer");
        }
    }
}
