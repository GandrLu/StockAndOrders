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
            address = new Address("Main Street", "1", "12345", "Maine");

            Assert.IsNotNull(address);
            Assert.AreEqual(address.City, "Maine", address.City + " should be Maine");
        }

        [TestMethod]
        public void TestBaseConstructor()
        {
            int idShouldBe = 0;
            Customer customer = new Customer();
            
            Assert.AreEqual(idShouldBe, customer.Id);
        }

        [TestMethod]
        public void TestSmallConstructor()
        {
            int idShouldBe = 2;
            Customer customer = new Customer("Peter", "Ahrnt", address);

            Assert.AreEqual(idShouldBe, customer.Id);
        }

        [TestMethod]
        public void TestFullConstructor()
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
