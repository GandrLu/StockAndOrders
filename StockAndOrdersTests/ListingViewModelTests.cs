using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using StockAndOrders.ViewModel;

namespace StockAndOrdersTests
{
    [TestClass]
    public class ListingViewModelTests
    {
        [TestMethod]
        public void TestLoadedListings()
        {
            ListingViewModel listingViewModel = new ListingViewModel();
            Assert.IsTrue(listingViewModel.LoadedListings.Count > 0, "No listings loaded: " + listingViewModel.LoadedListings.Count);
        }
    }
}
