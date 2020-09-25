using System;
using System.Collections;
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
using ShopManager.Helper;
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
        private CustomerViewModel customerViewModel;
        
        public MainWindow()
        {
            InitializeComponent();
            customerViewModel = new CustomerViewModel();
            this.DataContext = customerViewModel;
            customerViewModel.PropertyChanged += updateLBCustomers;
            DataBaseConnection dbConnection = new DataBaseConnection();
            List<Customer> customers = dbConnection.SelectAllCustomers();
            customerViewModel.LoadedCustomers = dbConnection.SelectAllCustomers();
			lbCustomers.ItemsSource = customerViewModel.LoadedCustomers;
        }

        private void ResetInputForm()
        {
            customerViewModel.CurrentCustomer = customerViewModel.CreateBlankCustomerWithAddress();
            this.DataContext = customerViewModel;
            postalCode.Clear();
        }

        void updateLBCustomers(object sender, EventArgs e)
        {
            lbCustomers.ItemsSource = null;
            lbCustomers.ItemsSource = customerViewModel.LoadedCustomers;
        }
    }
}
