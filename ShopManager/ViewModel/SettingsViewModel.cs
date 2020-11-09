using GalaSoft.MvvmLight.CommandWpf;
using StockAndOrders.Helper;
using StockAndOrders.Properties;
using System;
using System.Configuration;
using System.Windows.Input;

namespace StockAndOrders.ViewModel
{
    class SettingsViewModel : ObservableObject
    {
        private bool isAppConfigured;
        private bool isAppInTestMode;
        private string etsyAppKey;
        private string etsyAppSecret;
        private string etsyVerificationCode;
        private string etsyAccessToken;
        private string etsyAccessTokenSecret;
        private ICommand saveSettingsCommand;
        private ICommand aquireVerificationCodeCommand;
        private ICommand saveVerificationCodeCommand;

        public SettingsViewModel()
        {
            try
            {
                IsAppConfigured = (bool)Settings.Default["IsAppConfigured"];
                IsAppInTestMode = (bool)Settings.Default["IsAppInTestMode"];
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

        public bool IsAppConfigured
        {
            get => ValidateSettings();
            set
            {
                if (isAppConfigured != value)
                {
                    isAppConfigured = value;
                    Settings.Default[nameof(IsAppConfigured)] = value;
                    Settings.Default.Save();
                }
            }
        }

        public bool IsAppInTestMode
        {
            get => isAppInTestMode;
            set
            {
                isAppInTestMode = value;
                Settings.Default[nameof(IsAppInTestMode)] = value;
                Settings.Default.Save();
                OnPropertyChanged(nameof(IsAppInTestMode));
            }
        }

        public string EtsyAccessToken
        {
            get => etsyAccessToken;
            set
            {
                if (etsyAccessToken != value)
                {
                    etsyAccessToken = value;
                    Settings.Default[nameof(EtsyAccessToken)] = value;
                    Settings.Default.Save();
                    OnPropertyChanged(nameof(EtsyAccessToken));
                }
            }
        }

        public string EtsyAccessTokenSecret
        {
            get => etsyAccessTokenSecret;
            set
            {
                if (etsyAccessTokenSecret != value)
                {
                    etsyAccessTokenSecret = value;
                    Settings.Default.EtsyAccessTokenSecret = value;
                    Settings.Default.Save();
                    OnPropertyChanged(nameof(EtsyAccessTokenSecret));
                }
            }
        }

        public string EtsyAppKey
        {
            get { return etsyAppKey; }
            set
            {
                if (etsyAppKey != value)
                {
                    etsyAppKey = value;
                    Settings.Default.EtsyAppKey = value;
                    Settings.Default.Save();
                    OnPropertyChanged(nameof(EtsyAppKey));
                }
            }
        }

        public string EtsyAppSecret
        {
            get { return etsyAppSecret; }
            set
            {
                if (etsyAppSecret != value)
                {
                    etsyAppSecret = value;
                    Settings.Default[nameof(EtsyAppSecret)] = value;
                    Settings.Default.Save();
                    OnPropertyChanged(nameof(EtsyAppSecret));
                }
            }
        }

        public string EtsyVerificationCode
        {
            get { return etsyVerificationCode; }
            set
            {
                if (etsyVerificationCode != value)
                {
                    etsyVerificationCode = value;
                    Settings.Default.EtsyVerificationCode = value;
                    Settings.Default.Save();
                    OnPropertyChanged(nameof(EtsyVerificationCode));
                }
            }
        }

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

        public ICommand AquireVerificationCodeCommand
        {
            get
            {
                if (aquireVerificationCodeCommand == null)
                {
                    aquireVerificationCodeCommand = new RelayCommand(
                        () => EtsyApiConnector.AcquireRequestToken());
                }
                return aquireVerificationCodeCommand;
            }
        }

        public void SaveSettings()
        {
            Settings.Default["EtsyAppKey"] = EtsyAppKey;
            Settings.Default["EtsyAppSecret"] = EtsyAppSecret;
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

        private bool ValidateSettings()
        {
            if (EtsyAppKey == "")
                return false;
            if (EtsyAppSecret == "")
                return false;
            if (EtsyVerificationCode == "")
                return false;
            return true;
        }
    }
}
