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

            Parallel.ForEach(LoadedReceipts, async receipt =>
            {
                Order newOrder = new Order(receipt);
                EtsyApiConnector connector = new EtsyApiConnector();
                var answer = await connector.GetTransactionByReceipt(receipt.Receipt_id.ToString());
                newOrder.Transactions.AddRange(answer.results);
                LoadedOrders.Add(newOrder);
            });
        }

        private async Task<List<Transaction>> FetchTransactionsOfReceipt(string receiptId)
        {
            var result = await EtsyApiConnector.GetTransactionsByReceipt(receiptId);
            return new List<Transaction>(result.results);
        }
    }
}
