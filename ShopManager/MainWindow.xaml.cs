using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using ShopManager.Model;
using ShopManager.Validator;
using ShopManager.ViewModel;

namespace ShopManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private Customer inputCustomer;
        private CustomerViewModel customerViewModel;

        public MainWindow()
        {
            InitializeComponent();
            //inputCustomer = CreateBlankCustomerWithAddress();

            customerViewModel = new CustomerViewModel();
            this.DataContext = customerViewModel;
            customerViewModel.CurrentCustomer = CreateBlankCustomerWithAddress();
        }

        private void btnCreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            if(int.TryParse(postalCode.Text, out int intPostalCode))
            {
                var validationResult = ValidateCustomerData(customerViewModel.CurrentCustomer);

                if (validationResult.IsValid)
                {
                    StoreNewCustomer();
                    ResetInputForm();
                }
            }
        }

        private Customer CreateBlankCustomerWithAddress()
        {
            Customer newCustomer = new Customer();
            newCustomer.Address = new Address();
            return newCustomer;
        }

        private void StoreNewCustomer()
        {
            lbCustomer.Items.Add(customerViewModel.CurrentCustomer);
        }

        private void ResetInputForm()
        {
            customerViewModel.CurrentCustomer = CreateBlankCustomerWithAddress();
            this.DataContext = customerViewModel;
            postalCode.Clear();
        }

        private FluentValidation.Results.ValidationResult ValidateCustomerData(Customer data)
        {
            CustomerValidator validator = new CustomerValidator();
            return validator.Validate(data);
        }
    }
}
