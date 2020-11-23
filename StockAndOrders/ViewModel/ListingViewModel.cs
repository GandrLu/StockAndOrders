using GalaSoft.MvvmLight.CommandWpf;
using StockAndOrders.Helper;
using StockAndOrders.Repositories;
using StockAndOrders.Model;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockAndOrders.ViewModel
{
    public class ListingViewModel : ObservableObject
    {
        #region Fields
        private const int ERROR_MESSAGE_TIMESPAN = 2000;

        private ICommand saveListingQuantityCommand;
        private ICommand cancelSaveListingQuantityCommand;
        private List<Listing> loadedListings = new List<Listing>();
        private Listing selectedListing = new Listing();
        private Listing currentListing;
        private string errorMessage;
        #endregion

        #region Constructor
        public ListingViewModel(IListingRepository listingRepository)
        {
            LoadListings(listingRepository);
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
                        () => SelectedListing != null
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
                        () => SelectedListing != null
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

        public Listing SelectedListing
        {
            get { return selectedListing; }
            set
            {
                selectedListing = value;
                OnPropertyChanged(nameof(SelectedListing));
            }
        }

        public Listing CurrentListing
        {
            get { return currentListing; }
            set
            {
                if (currentListing != value)
                {
                    currentListing = value;
                    if (value != null)
                    {
                        if (SelectedListing == null)
                            SelectedListing = new Listing();
                        value.CopyTo(SelectedListing);
                    }
                    OnPropertyChanged(nameof(CurrentListing));
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
        private async void LoadListings(IListingRepository listingRepository)
        {
            var listingsResponse = await listingRepository.GetListings();
            LoadedListings = (List<Listing>)listingsResponse;
        }

        private async void SaveListingQuantityToEtsy()
        {
            if (await EtsyApiConnector.Instance.PutListingQuantityUpdate(SelectedListing))
            {
                UpdateSelectedListingInLoadedListings();
                UnloadSelectedListing();
            }
            else
                DisplayEtsySavingError();
        }

        private void UnloadSelectedListing()
        {
            SelectedListing = null;
            CurrentListing = null;
        }

        private void UpdateSelectedListingInLoadedListings()
        {
            var oldIndex = LoadedListings.FindIndex(x => x.Listing_id == SelectedListing.Listing_id);
            if (oldIndex != -1)
                SelectedListing.CopyTo(LoadedListings[oldIndex]);
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
