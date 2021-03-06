﻿using GalaSoft.MvvmLight.Command;
using StockAndOrders.Helper;
using StockAndOrders.Model;
using StockAndOrders.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StockAndOrders.ViewModel
{
    class CustomerViewModel : ObservableObject
    {
        private Customer currentCustomer;
        private List<Customer> loadedCustomers = new List<Customer>();
        private ICommand saveCustomerCommand;

        public CustomerViewModel()
        {
            CurrentCustomer = CreateBlankCustomerWithAddress();
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

        public List<Customer> LoadedCustomers
        {
            get { return loadedCustomers; }
            set
            {
                if (value != loadedCustomers)
                {
                    loadedCustomers = value;
                    OnPropertyChanged("LoadedCustomers");
                }
            }
        }

        public ICommand SaveCustomerCommand
        {
            get
            {
                if (saveCustomerCommand == null)
                {
                    saveCustomerCommand = new RelayCommand(
                        () => SaveCustomer(),
                        () => (CurrentCustomer != null)
                        );
                }
                return saveCustomerCommand;
            }
        }

        private FluentValidation.Results.ValidationResult ValidateCustomerData(Customer data)
        {
            CustomerValidator validator = new CustomerValidator();
            return validator.Validate(data);
        }

        public Customer CreateBlankCustomerWithAddress()
        {
            Customer newCustomer = new Customer();
            newCustomer.Address = new Address();
            return newCustomer;
        }

        private void SaveCustomer()
        {
			if(int.TryParse(CurrentCustomer.Address.PostalCode, out int intPostalCode))
            {
                var validationResult = ValidateCustomerData(CurrentCustomer);

                if (validationResult.IsValid)
                {
                    StoreNewCustomer();
                    CurrentCustomer = CreateBlankCustomerWithAddress();
                }
            }
        }

        private void StoreNewCustomer()
        {
            Console.WriteLine("SAVE CUSTOMER");
			LoadedCustomers.Add(CurrentCustomer);
        }
    }
}
