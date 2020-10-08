using ShopManager.Helper;
using ShopManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.ViewModel
{
    class ListingViewModel : ObservableObject
    {
        private List<Listing> loadedListings = new List<Listing>();

        public List<Listing> LoadedListings
        {
            get { return loadedListings; }
            set
            {
                if (loadedListings != value)
                {
                    loadedListings = value;
                    OnPropertyChanged("LoadedListings");
                }
            }
        }

        public async void FetchListingsFromEtsy()
        {
            var listingsResponse = await EtsyApiConnector.GetItems();
            List<Listing> listings = new List<Listing>(listingsResponse.results);
            LoadedListings = listings;
        }
    }
}
