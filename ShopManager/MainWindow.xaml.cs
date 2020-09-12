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

namespace ShopManager
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btnCreateNewCustomer_Click(object sender, RoutedEventArgs e)
        {
            Address newAddress = new Address(street.Text, housenumber.Text, int.Parse(postalCode.Text), city.Text);
            Customer newCustomer = new Customer(Customer.Customers.Count, firstName.Text, surName.Text, newAddress);
            listBox.Items.Add(newCustomer);
        }
    }
}
