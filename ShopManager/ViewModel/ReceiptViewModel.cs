using GalaSoft.MvvmLight.CommandWpf;
using ShopManager.Helper;
using ShopManager.Model;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopManager.ViewModel
{
    class ReceiptViewModel : ObservableObject
    {
        private List<Receipt> loadedReceipts = new List<Receipt>();
        private List<Order> loadedOrders = new List<Order>();
        private Receipt currentReceipt;
        private Receipt selectedReceipt = new Receipt();
        private RelayCommand saveReceiptCommand;
        private RelayCommand cancelSaveReceiptCommand;

        public ReceiptViewModel()
        {
            FetchReceiptsFromEtsy();
        }

        public List<Receipt> LoadedReceipts
        {
            get => loadedReceipts;
            set
            {
                if (loadedReceipts != value)
                {
                    loadedReceipts = value;
                    OnPropertyChanged(nameof(LoadedReceipts));
                }
            }
        }

        public List<Order> LoadedOrders
        {
            get => loadedOrders;
            set
            {
                if (loadedOrders != value)
                {
                    loadedOrders = value;
                    OnPropertyChanged(nameof(LoadedOrders));
                }
            }
        }

        public Receipt CurrentReceipt
        {
            get => currentReceipt;
            set
            {
                if (currentReceipt != value)
                {
                    currentReceipt = value;
                    if (value != null)
                    {
                        if (SelectedReceipt == null)
                            SelectedReceipt = new Receipt();
                        value.CopyTo(SelectedReceipt);
                    }
                    OnPropertyChanged(nameof(CurrentReceipt));
                }
            }
        }

        public Receipt SelectedReceipt
        {
            get { return selectedReceipt; }
            set
            {
                selectedReceipt = value;
                OnPropertyChanged(nameof(SelectedReceipt));
            }
        }

        public RelayCommand SaveReceiptCommand
        {
            get
            {
                if (saveReceiptCommand == null)
                {
                    saveReceiptCommand = new RelayCommand(
                        () => SaveReceiptToEtsy(),
                        () => SelectedReceipt != null                        
                        );
                }
                return saveReceiptCommand;
            }
        }
        public RelayCommand CancelSaveReceiptCommand
        {
            get
            {
                if (cancelSaveReceiptCommand == null)
                {
                    cancelSaveReceiptCommand = new RelayCommand(
                        () => UnloadSelectedReceipt(),
                        () => SelectedReceipt != null
                        );
                }
                return cancelSaveReceiptCommand;
            }
        }

        private async void SaveReceiptToEtsy()
        {
            if (await EtsyApiConnector.PostTrackingData(SelectedReceipt))
            {
                UpdateSelectedReceiptInLoadedReceipts();
                SelectedReceipt = null;
            }
            //TODO: Display error
        }

        private void UnloadSelectedReceipt()
        {
            SelectedReceipt = null;
            CurrentReceipt = null;
        }

        private bool CheckValidReceipt()
        {
            return false;
        }

        private void UpdateSelectedReceiptInLoadedReceipts()
        {
            var oldIndex = LoadedReceipts.FindIndex(findBy => findBy.Receipt_id == SelectedReceipt.Receipt_id);
            if (oldIndex != -1)
                SelectedReceipt.CopyTo(LoadedReceipts[oldIndex]);
        }

        private async void FetchReceiptsFromEtsy()
        {
            var receiptsResponse = await EtsyApiConnector.GetReceipts();
            List<Receipt> receipts = new List<Receipt>(receiptsResponse.results);
            LoadedReceipts = receipts;
        }
    }
}
