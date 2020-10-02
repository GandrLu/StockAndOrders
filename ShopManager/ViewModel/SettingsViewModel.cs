using GalaSoft.MvvmLight.Command;
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
        private string databaseServer;
        private string databaseUserId;
        private string databaseSecret;
        private string databaseName;
        private string etsyUserId;
        private string etsyPassword;
        private ICommand saveSettingsCommand;


        public SettingsViewModel()
        {
            try
            {
                DatabaseServer = (string)Settings.Default["DatabaseServer"];
                DatabaseName = (string)Settings.Default["DatabaseName"];
                DatabaseUserId = (string)Settings.Default["DatabaseUserId"];
                DatabaseSecret = (string)Settings.Default["DatabaseSecret"];
                EtsyUserId = (string)Settings.Default["EtsyUserId"];
                EtsyPassword = (string)Settings.Default["EtsyPassword"];
            }
            catch (SettingsPropertyNotFoundException e)
            {
                Console.WriteLine(e + ": Settings could not be found, typing error?");
            }
        }

        public string DatabaseServer { get => databaseServer; set => databaseServer = value; }
        public string DatabaseUserId { get => databaseUserId; set => databaseUserId = value; }
        public string DatabaseSecret { get => databaseSecret; set => databaseSecret = value; }
        public string DatabaseName { get => databaseName; set => databaseName = value; }
        public string EtsyUserId { get => etsyUserId; set => etsyUserId = value; }
        public string EtsyPassword { get => etsyPassword; set => etsyPassword = value; }

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

        public void SaveSettings()
        {
            Settings.Default["DatabaseServer"] = DatabaseServer;
            Settings.Default["DatabaseName"] = DatabaseName;
            Settings.Default["DatabaseUserId"] = DatabaseUserId;
            Settings.Default["DatabaseSecret"] = DatabaseSecret;
            Settings.Default["EtsyUserId"] = EtsyUserId;
            Settings.Default["EtsyPassword"] = EtsyPassword;
            Properties.Settings.Default.Save();
        }

        public void SaveSettings(string databaseServer, string databaseName, 
            string databaseUserId, string databaseSecret, string etsyUserId, string etsyPassword)
        {
            Settings.Default["DatabaseServer"] = databaseServer;
            Settings.Default["DatabaseName"] = databaseName;
            Settings.Default["DatabaseUserId"] = databaseUserId;
            Settings.Default["DatabaseSecret"] = databaseSecret;
            Settings.Default["EtsyUserId"] = etsyUserId;
            Settings.Default["EtsyPassword"] = etsyPassword;
            Properties.Settings.Default.Save();
        }

        public void ReceiveSettings()
        {
            string settings = "Settings: ";
            settings += (string)Settings.Default["DatabaseServer"];
            settings += (string)Settings.Default["DatabaseName"];
            settings += (string)Settings.Default["DatabaseUserId"];
            settings += (string)Settings.Default["DatabaseSecret"];
            settings += (string)Settings.Default["EtsyUserId"];
            settings += (string)Settings.Default["EtsyPassword"];

            Console.WriteLine(settings);
        }
    }
}
