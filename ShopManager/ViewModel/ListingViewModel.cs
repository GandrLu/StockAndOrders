using ShopManager.Helper;
using ShopManager.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.Remoting.Channels;
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
                    foreach (var listing in LoadedListings)
                    {
                        listing.PropertyChanged += SaveListingsToEtsy;
                    }
                    OnPropertyChanged("LoadedListings");
                }
            }
        }

        public async void FetchListingsFromEtsy()
        {
            var listingsResponse = await EtsyApiConnector.GetListings();
            List<Listing> listings = new List<Listing>(listingsResponse.results);
            LoadedListings = listings;
        }

        public async void SaveListingsToEtsy(object sender, PropertyChangedEventArgs e)
        {
            Debug.WriteLine("Store");
        }
    }
}
