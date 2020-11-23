using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using StockAndOrders.Model;
using StockAndOrders.Repositories;
using StockAndOrders.ViewModel;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StockAndOrdersTests
{
    [TestClass]
    public class ListingViewModelTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            int expectedLoadedListings = 1;
            var repoMockOneListing = new Mock<IListingRepository>();
            repoMockOneListing.Setup(listingRepo => listingRepo.GetListings())
                .ReturnsAsync(new List<Listing> { new Listing() });

            var vm = new ListingViewModel(repoMockOneListing.Object);
            Assert.AreEqual(vm.LoadedListings.Count, expectedLoadedListings);
        }
    }
}
