using System.Windows;
using StockAndOrders.Helper;
using StockAndOrders.View;
using StockAndOrders.ViewModel;

namespace StockAndOrders
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
            if (!SetupSettings())
            {
                App.Current.Shutdown();
                return;
            }
            SetupListingViewModel();
            SetupReceiptsViewModel();
        }
        #endregion

        #region Setup Methods
        private void SetupListingViewModel()
        {
            listingViewModel = new ListingViewModel(EtsyApiConnector.Instance);
            tiListings.DataContext = listingViewModel;
        }

        private void SetupReceiptsViewModel()
        {
            receiptViewModel = new ReceiptViewModel(EtsyApiConnector.Instance);
            tiOrders.DataContext = receiptViewModel;
        }

        private bool SetupSettings()
        {
            settingsViewModel = new SettingsViewModel();
            if (!settingsViewModel.IsAppConfigured)
                if (!(bool)ShowVerificationCodeDialog())
                    return false;
            tiSettings.DataContext = settingsViewModel;
            return true;
        }
        #endregion

        #region Helper Methods
        private bool? ShowVerificationCodeDialog()
        {
            VerificationCodeDialogView verificationCodeDialog = new VerificationCodeDialogView();
            return verificationCodeDialog.ShowDialog();
        }
        #endregion
    }
}
