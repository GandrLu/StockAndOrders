using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ShopManager.View
{
    /// <summary>
    /// Interaction logic for AuthLoginBrowserView.xaml
    /// </summary>
    public partial class AuthLoginBrowserView : Window
    {
        private string webAddress = "https://github.com/cefsharp/CefSharp/wiki/Frequently-asked-questions";

        public AuthLoginBrowserView()
        {
            InitializeComponent();
            WebBrowser.DataContext = this;
            //App.Current.MainWindow = new MainWindow();
            //App.Current.MainWindow.Show();
            //this.Close();
        }

        public AuthLoginBrowserView(string uri) : this()
        {
            this.WebAddress = uri;
        }

        public string WebAddress { get => webAddress; set => webAddress = value; }
    }
}
