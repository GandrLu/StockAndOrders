using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using ShopManager.Helper;
using ShopManager.Model;
using ShopManager.Properties;
using ShopManager.Validator;
using ShopManager.View;
using ShopManager.ViewModel;

namespace ShopManager
{
    /// <summary>
    /// View for all tabs.
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Fields
        private CustomerViewModel customerViewModel;
        private ListingViewModel listingViewModel;
        private SettingsViewModel settingsViewModel;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            SetupCustomerViewModel();
            SetupListingViewModel();
            SetupDatabase();
            SetupSettings();
            FetchEtsyData();
        }
        #endregion

        #region Setup Methods
        private void SetupCustomerViewModel()
        {
            customerViewModel = new CustomerViewModel();
            tiCustomers.DataContext = customerViewModel;
            customerViewModel.PropertyChanged += updateLBCustomers;
        }

        private void SetupListingViewModel()
        {
            listingViewModel = new ListingViewModel();
            tiListings.DataContext = listingViewModel;
            listingViewModel.PropertyChanged += updateLBListings;
        }

        private void SetupDatabase()
        {
            DataBaseConnection dbConnection = new DataBaseConnection();
            List<Customer> customers = dbConnection.SelectAllCustomers();
            customerViewModel.LoadedCustomers = dbConnection.SelectAllCustomers();
            lbCustomers.ItemsSource = customerViewModel.LoadedCustomers;
        }

        private void SetupSettings()
        {
            settingsViewModel = new SettingsViewModel();
            if (!SettingsViewModel.IsAppConfigured)
                ShowVerificationCodeDialog();
            FillInSettings();
        }

        private async void FetchEtsyData()
        {
            await EtsyApiConnector.GetTransactions();
            //await EtsyApiConnector.GetItems();
            listingViewModel.FetchListingsFromEtsy();
        }
        #endregion

        #region Button Click Handler
        private void OnSaveSettingsButton_Click(object sender, RoutedEventArgs e)
        {
            settingsViewModel.SaveSettings(tbDatabaseServer.Text, tbDatabaseName.Text, 
                tbDatabaseUser.Text, pbDatabaseSecret.Password, tbEtsyVerificationCode.Text);
        }

        private void OnEtsyVerificationButton_Click(object sender, RoutedEventArgs e)
        {
            EtsyApiConnector.AcquireRequestToken();
        }
        #endregion

        #region Helper Methods
        private void ResetInputForm()
        {
            customerViewModel.CurrentCustomer = customerViewModel.CreateBlankCustomerWithAddress();
            this.DataContext = customerViewModel;
            postalCode.Clear();
        }

        private void updateLBCustomers(object sender, EventArgs e)
        {
            lbCustomers.ItemsSource = null;
            lbCustomers.ItemsSource = customerViewModel.LoadedCustomers;
        }

        private void updateLBListings(object sender, EventArgs e)
        {
            lbListings.ItemsSource = null;
            lbListings.ItemsSource = listingViewModel.LoadedListings;
        }

        private void ShowVerificationCodeDialog()
        {
            VerificationCodeDialogView verificationCodeDialog = new VerificationCodeDialogView();
            EtsyApiConnector.AcquireRequestToken();
            verificationCodeDialog.ShowDialog();
        }

        private void FillInSettings()
        {
            tbDatabaseServer.Text = settingsViewModel.DatabaseServer;
            tbDatabaseName.Text = settingsViewModel.DatabaseName;
            tbDatabaseUser.Text = settingsViewModel.DatabaseUserId;
            pbDatabaseSecret.Password = settingsViewModel.DatabaseSecret;
            tbEtsyVerificationCode.Text = settingsViewModel.EtsyVerificationCode;
        }
        #endregion
    }
}
