using ShopManager.Helper;
using ShopManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.ViewModel
{
    class ReceiptViewModel : ObservableObject
    {
        private List<Receipt> loadedReceipts;
        public List<Receipt> LoadedReceipts
        {
            get => loadedReceipts;
            set
            {
                if (loadedReceipts != value)
                {
                    loadedReceipts = value;
                    OnPropertyChanged("LoadedReceipts");
                }
            }
        }

        public async void FetchReceiptsFromEtsy()
        {
            var receiptsResponse = await EtsyApiConnector.GetReceipts();
            List<Receipt> receipts = new List<Receipt>(receiptsResponse.results);
            LoadedReceipts = receipts;
        }
    }
}
