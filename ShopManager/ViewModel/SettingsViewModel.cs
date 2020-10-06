using GalaSoft.MvvmLight.Command;
using ShopManager.Helper;
using ShopManager.Properties;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace ShopManager.ViewModel
{
    class SettingsViewModel
    {
        private static bool isAppConfigured;
        private static string databaseServer;
        private static string databaseUserId;
        private static string databaseSecret;
        private static string databaseName;
        private static string etsyVerificationCode;
        private static string etsyAccessToken;
        private static string etsyAccessTokenSecret;
        private ICommand saveSettingsCommand;
        private ICommand saveVerificationCodeCommand;

        public SettingsViewModel()
        {
            try
            {
                IsAppConfigured = (bool)Settings.Default["IsAppConfigured"];
                DatabaseServer = (string)Settings.Default["DatabaseServer"];
                DatabaseName = (string)Settings.Default["DatabaseName"];
                DatabaseUserId = (string)Settings.Default["DatabaseUserId"];
                DatabaseSecret = (string)Settings.Default["DatabaseSecret"];
                EtsyVerificationCode = (string)Settings.Default["EtsyVerificationCode"];

                if (EtsyVerificationCode != string.Empty)
                {
                    IsAppConfigured = true;
                }
            }
            catch (SettingsPropertyNotFoundException e)
            {
                Console.WriteLine(e + ": Settings could not be found, typing error?");
            }
        }

        public static bool IsAppConfigured
        {
            get => isAppConfigured;
            set
            {
                isAppConfigured = value;
                Settings.Default ["IsAppConfigured"] = value;
                Settings.Default.Save();
            }
        }

        public static string EtsyAccessToken
        {
            get => etsyAccessToken;
            set
            {
                etsyAccessToken = value;
                Settings.Default["EtsyAccessToken"] = value;
                Settings.Default.Save();
            }
        }

        public static string EtsyAccessTokenSecret
        {
            get => etsyAccessTokenSecret;
            set
            {
                etsyAccessTokenSecret = value;
                Settings.Default["EtsyAccessTokenSecret"] = value;
                Settings.Default.Save();
            }
        }

        public string DatabaseServer { get => databaseServer; set => databaseServer = value; }
        public string DatabaseUserId { get => databaseUserId; set => databaseUserId = value; }
        public string DatabaseSecret { get => databaseSecret; set => databaseSecret = value; }
        public string DatabaseName { get => databaseName; set => databaseName = value; }
        public string EtsyVerificationCode { get => etsyVerificationCode; set => etsyVerificationCode = value; }

        public ICommand SaveSettingsCommand
        {
            get
            {
                if (saveSettingsCommand == null)
                {
                    saveSettingsCommand = new RelayCommand(
                        () => SaveSettings(),
                        () => (DatabaseServer != null)
                        );
                }
                return saveSettingsCommand;
            }
        }

        public ICommand SaveVerificationCodeCommand
        {
            get
            {
                if (saveVerificationCodeCommand == null)
                {
                    saveVerificationCodeCommand = new RelayCommand(
                        () => SaveVerificationCode(),
                        () => (EtsyVerificationCode != null)
                        );
                }
                return saveVerificationCodeCommand;
            }
        }

        public void SaveSettings()
        {
            Settings.Default["DatabaseServer"] = DatabaseServer;
            Settings.Default["DatabaseName"] = DatabaseName;
            Settings.Default["DatabaseUserId"] = DatabaseUserId;
            Settings.Default["DatabaseSecret"] = DatabaseSecret;
            Settings.Default["EtsyVerificationCode"] = EtsyVerificationCode;
            Settings.Default.Save();
        }

        public void SaveSettings(string databaseServer, string databaseName, 
            string databaseUserId, string databaseSecret, string etsyVerificationCode)
        {
            Settings.Default["DatabaseServer"] = databaseServer;
            Settings.Default["DatabaseName"] = databaseName;
            Settings.Default["DatabaseUserId"] = databaseUserId;
            Settings.Default["DatabaseSecret"] = databaseSecret;
            Settings.Default["EtsyVerificationCode"] = etsyVerificationCode;
            Settings.Default.Save();
        }

        public void SaveVerificationCode()
        {
            Settings.Default["EtsyVerificationCode"] = EtsyVerificationCode;
            Settings.Default.Save();
            EtsyApiConnector.AcquireAccessToken();
        }

        public override string ToString()
        {
            string settings = "Settings: ";
            settings += (string)Settings.Default["DatabaseServer"];
            settings += (string)Settings.Default["DatabaseName"];
            settings += (string)Settings.Default["DatabaseUserId"];
            settings += (string)Settings.Default["DatabaseSecret"];
            settings += (string)Settings.Default["EtsyVerificationCode"];
            return settings;
        }
    }
}
