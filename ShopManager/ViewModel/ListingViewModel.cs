using GalaSoft.MvvmLight.CommandWpf;
using ShopManager.Helper;
using ShopManager.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    class ListingViewModel : ObservableObject
    {
        #region Fields
        private const int ERROR_MESSAGE_TIMESPAN = 2000;

        private ICommand saveListingQuantityCommand;
        private ICommand cancelSaveListingQuantityCommand;
        private List<Listing> loadedListings = new List<Listing>();
        private Listing temporarySelectedListing = new Listing();
        private Listing selectedListing;
        private string errorMessage;
        #endregion

        #region Constructor
        public ListingViewModel()
        {
            FetchListingsFromEtsy();
        }
        #endregion

        #region Properties
        public ICommand SaveListingQuantityCommand
        {
            get
            {
                if (saveListingQuantityCommand == null)
                {
                    saveListingQuantityCommand = new RelayCommand(
                        () => SaveListingQuantityToEtsy(),
                        () => TemporarySelectedListing != null
                        );
                }
                return saveListingQuantityCommand;
            }
        }

        public ICommand CancelSaveListingQuantityCommand
        {
            get
            {
                if (cancelSaveListingQuantityCommand == null)
                {
                    cancelSaveListingQuantityCommand = new RelayCommand(
                        () => UnloadSelectedListing(),
                        () => TemporarySelectedListing != null
                        );
                }
                return cancelSaveListingQuantityCommand;
            }
        }

        public List<Listing> LoadedListings
        {
            get { return loadedListings; }
            set
            {
                if (loadedListings != value)
                {
                    loadedListings = value;
                    OnPropertyChanged(nameof(LoadedListings));
                }
            }
        }

        public Listing TemporarySelectedListing
        {
            get { return temporarySelectedListing; }
            set
            {
                temporarySelectedListing = value;
                OnPropertyChanged(nameof(TemporarySelectedListing));
            }
        }

        public Listing SelectedListing
        {
            get { return selectedListing; }
            set
            {
                if (selectedListing != value)
                {
                    selectedListing = value;
                    if (value != null)
                    {
                        if (TemporarySelectedListing == null)
                            TemporarySelectedListing = new Listing();
                        value.CopyTo(TemporarySelectedListing);
                    }
                    OnPropertyChanged(nameof(SelectedListing));
                }
            }
        }

        public string ErrorMessage
        {
            get { return errorMessage; }
            set
            {
                if (errorMessage != value)
                {
                    errorMessage = value;
                    OnPropertyChanged(nameof(ErrorMessage));
                }
            }
        }
        #endregion

        #region Private Methods
        private async void FetchListingsFromEtsy()
        {
            var listingsResponse = await EtsyApiConnector.GetListings();
            List<Listing> listings = new List<Listing>(listingsResponse.results);
            LoadedListings = listings;
        }

        private async void SaveListingQuantityToEtsy()
        {
            if (await EtsyApiConnector.PutListingQuantityUpdate(TemporarySelectedListing))
            {
                UpdateSelectedListingInLoadedListings();
                UnloadSelectedListing();
            }
            else
                DisplayEtsySavingError();
        }

        private void UnloadSelectedListing()
        {
            TemporarySelectedListing = null;
            SelectedListing = null;
        }

        private void UpdateSelectedListingInLoadedListings()
        {
            var oldIndex = LoadedListings.FindIndex(x => x.Listing_id == TemporarySelectedListing.Listing_id);
            if (oldIndex != -1)
                TemporarySelectedListing.CopyTo(LoadedListings[oldIndex]);
        }

        private void DisplayEtsySavingError()
        {
            ErrorMessage = "Listing could not be saved to etsy";
            Task resetErrorMessage = new Task(() => { Thread.Sleep(ERROR_MESSAGE_TIMESPAN); ErrorMessage = ""; });
            resetErrorMessage.Start();
        }
        #endregion
    }
}
