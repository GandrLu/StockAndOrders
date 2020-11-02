﻿using System.Windows;
using ShopManager.Helper;
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
        }

        private void SetupSettings()
        {
            settingsViewModel = new SettingsViewModel();
            if (!settingsViewModel.IsAppConfigured)
                ShowVerificationCodeDialog();
            tiSettings.DataContext = settingsViewModel;
        }
        #endregion

        #region Helper Methods
        private void ShowVerificationCodeDialog()
        {
            VerificationCodeDialogView verificationCodeDialog = new VerificationCodeDialogView();
            //EtsyApiConnector.AcquireRequestToken();
            verificationCodeDialog.ShowDialog();
        }
        #endregion
    }
}
