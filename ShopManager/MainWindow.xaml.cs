using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
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

        private void FetchEtsyData()
        {
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
        private void updateDGReceipts(object sender, PropertyChangedEventArgs e)
        {
            dgReceipts.ItemsSource = null;
            dgReceipts.ItemsSource = receiptViewModel.LoadedOrders;
        }

        private void updateListingDetails(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count == 0)
            {
                dpListingDetail.DataContext = null;
                listingViewModel.SelectedListing = null;
            }
            else
            {
                IList<DataGridCellInfo> selectedCells = e.AddedCells;
                dpListingDetail.DataContext = selectedCells[0].Item;
                listingViewModel.SelectedListing = (Listing)selectedCells[0].Item;
            }
        }

        private void updateReceiptsDetails(object sender, SelectedCellsChangedEventArgs e)
        {
            if (e.AddedCells.Count <= 0)
                return;
            IList<DataGridCellInfo> selectedCells = e.AddedCells;
            tiOrders.DataContext = (Order)selectedCells[0].Item;
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
