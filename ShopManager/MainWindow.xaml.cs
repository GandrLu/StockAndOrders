using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
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
        private ReceiptViewModel receiptViewModel;
        private SettingsViewModel settingsViewModel;
        #endregion

        #region Constructor
        public MainWindow()
        {
            InitializeComponent();
            SetupSettings();
            SetupListingViewModel();
            SetupReceiptsViewModel();
            SetupDatabase();
            FetchEtsyData();
        }
        #endregion

        #region Setup Methods
        private void SetupListingViewModel()
        {
            listingViewModel = new ListingViewModel();
            tiListings.DataContext = listingViewModel;
            listingViewModel.PropertyChanged += updateDGListings;
            dgListings.SelectedCellsChanged += updateListingDetails;
        }

        private void SetupReceiptsViewModel()
        {
            receiptViewModel = new ReceiptViewModel();
            tiOrders.DataContext = receiptViewModel;
            receiptViewModel.PropertyChanged += updateDGReceipts;
            dgReceipts.SelectedCellsChanged += updateReceiptsDetails;
        }

        private void SetupDatabase()
        {
            DataBaseConnection dbConnection = new DataBaseConnection();
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
            listingViewModel.FetchListingsFromEtsy();
            receiptViewModel.FetchReceiptsFromEtsy();
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
        private void updateDGListings(object sender, PropertyChangedEventArgs e)
        {
            dgListings.ItemsSource = null;
            dgListings.ItemsSource = listingViewModel.LoadedListings;
        }

        private void updateDGReceipts(object sender, PropertyChangedEventArgs e)
        {
            dgReceipts.ItemsSource = null;
            dgReceipts.ItemsSource = receiptViewModel.LoadedReceipts;
        }

        private void updateListingDetails(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count <= 0)
                return;
            IList<DataGridCellInfo> selectedCells = e.AddedCells;
            dpListingDetail.DataContext = selectedCells[0].Item;
        }

        private async void updateReceiptsDetails(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count <= 0)
                return;
            IList<DataGridCellInfo> selectedCells = e.AddedCells;
            tiOrders.DataContext = (Receipt)selectedCells[0].Item;
            string id = ((Receipt)selectedCells[0].Item).Receipt_id.ToString();
            var result = await EtsyApiConnector.GetTransactionsByReceipt(id);
            List<Transaction> transactionslist = new List<Transaction>();
            for (int i = 0; i < 5; i++)
            {
                transactionslist.Add(result.results[0]);
            }
            lbReceiptTransactions.ItemsSource = transactionslist;
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
