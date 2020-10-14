using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
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
            SetupSettings();
            SetupCustomerViewModel();
            SetupListingViewModel();
            SetupDatabase();
            FetchEtsyData();
        }
        #endregion

        #region Setup Methods
        private void SetupCustomerViewModel()
        {
            customerViewModel = new CustomerViewModel();
            dgCustomer.DataContext = customerViewModel;
        }

        private void SetupListingViewModel()
        {
            listingViewModel = new ListingViewModel();
            tiListings.DataContext = listingViewModel;
            listingViewModel.PropertyChanged += updateLBListings;
            dgListings.SelectedCellsChanged += updateListingDetails;
        }

        private void SetupDatabase()
        {
            DataBaseConnection dbConnection = new DataBaseConnection();
            customerViewModel.LoadedCustomers = dbConnection.SelectAllCustomers();
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
        private void updateLBListings(object sender, EventArgs e)
        {
            dgListings.ItemsSource = null;
            dgListings.ItemsSource = listingViewModel.LoadedListings;
        }

        private void updateListingDetails(object sender, SelectedCellsChangedEventArgs e)
        {
            IList<DataGridCellInfo> selectedCells = e.AddedCells;
            dpListingDetail.DataContext = selectedCells[0].Item;
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
