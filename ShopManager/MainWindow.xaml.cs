using System.Windows;
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
            SetupDatabase();
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
