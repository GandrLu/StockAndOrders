using ShopManager.Helper;
using ShopManager.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShopManager.ViewModel
{
    class ReceiptViewModel : ObservableObject
    {
        private List<Receipt> loadedReceipts = new List<Receipt>();
        private List<Order> loadedOrders = new List<Order>();
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

        public List<Order> LoadedOrders
        {
            get => loadedOrders;
            set
            {
                if (loadedOrders != value)
                {
                    loadedOrders = value;
                    OnPropertyChanged("LoadedOrders");
                }
            }
        }

        public async void FetchReceiptsFromEtsy()
        {
            var receiptsResponse = await EtsyApiConnector.GetReceipts();
            List<Receipt> receipts = new List<Receipt>(receiptsResponse.results);
            LoadedReceipts = receipts;
            List<Task> bagAddTasks = new List<Task>();

            foreach (var receipt in receipts)
            {
                Order newOrder = new Order(receipt);
                newOrder.Transactions = await FetchTransactionsOfReceipt(receipt.Receipt_id.ToString());
                LoadedOrders.Add(newOrder);
            }
        }

        private async Task<List<Transaction>> FetchTransactionsOfReceipt(string receiptId)
        {
            var result = await EtsyApiConnector.GetTransactionsByReceipt(receiptId);
            return new List<Transaction>(result.results);
        }
    }
}
