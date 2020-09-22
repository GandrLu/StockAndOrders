using GalaSoft.MvvmLight.Command;
using ShopManager.Helper;
using ShopManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    class CustomerViewModel : ObservableObject
    {
        private Customer currentCustomer;
        private List<Customer> loadedCustomers;
        private ICommand saveProductCommand;

        public CustomerViewModel()
        {
            loadedCustomers = new List<Customer>();
        }

        public Customer CurrentCustomer
        {
            get { return currentCustomer; }
            set
            {
                if (value != currentCustomer)
                {
                    currentCustomer = value;
                    OnPropertyChanged("CurrentCustomer");
                }
            }
        }

        public List<Customer> LoadedCustomers { get; set; }

        public ICommand SaveCustomerCommand
        {
            get
            {
                if (saveProductCommand == null)
                {
                    saveProductCommand = new RelayCommand(
                        () => SaveCustomer(),
                        () => (CurrentCustomer != null)
                        );
                }
                return saveProductCommand;
            }
        }

        private void SaveCustomer()
        {
            Console.WriteLine("SAVE CUSTOMER");
        }

        private void StoreNewCustomer()
        {
            
        }
    }
}
