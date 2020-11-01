using GalaSoft.MvvmLight.CommandWpf;
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
        private static string etsyAppKey;
        private static string etsyAppSecret;
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
                EtsyAppKey = (string)Settings.Default["EtsyAppKey"];
                EtsyAppSecret = (string)Settings.Default["EtsyAppSecret"];
                EtsyVerificationCode = (string)Settings.Default["EtsyVerificationCode"];
                EtsyAccessToken = (string)Settings.Default["EtsyAccessToken"];
                EtsyAccessTokenSecret = (string)Settings.Default["EtsyAccessTokenSecret"];

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

        public string EtsyAppKey { get => etsyAppKey; set => etsyAppKey = value; }
        public string EtsyAppSecret { get => etsyAppSecret; set => etsyAppSecret = value; }
        public string EtsyVerificationCode { get => etsyVerificationCode; set => etsyVerificationCode = value; }

        public ICommand SaveSettingsCommand
        {
            get
            {
                if (saveSettingsCommand == null)
                {
                    saveSettingsCommand = new RelayCommand(
                        () => SaveSettings(),
                        () => (EtsyVerificationCode != null)
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
            Settings.Default["EtsyAppKey"] = EtsyAppKey;
            Settings.Default["EtsyAppSecret"] = EtsyAppSecret;
            Settings.Default["EtsyVerificationCode"] = EtsyVerificationCode;
            Settings.Default.Save();
        }

        public void SaveSettings(string etsyAppKey, string etsyAppSecret, string etsyVerificationCode)
        {
            Settings.Default["EtsyAppKey"] = etsyAppKey;
            Settings.Default["EtsyAppSecret"] = etsyAppSecret;
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
            settings += (string)Settings.Default["EtsyAppKey"];
            settings += (string)Settings.Default["EtsyAppSecret"];
            settings += (string)Settings.Default["EtsyVerificationCode"];
            return settings;
        }
    }
}
