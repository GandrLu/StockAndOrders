using OAuth;
using ShopManager.Properties;
using ShopManager.View;
using ShopManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Helper
{
    class EtsyApiConnector
    {
        private const string CONSUMER_KEY = "fill in key";
        private const string CONSUMER_SECRET = "fill in secret";

        static EtsyApiConnector()
        {
            oAuth = new OAuth.Manager();
            oAuth["consumer_key"] = CONSUMER_KEY;
            oAuth["consumer_secret"] = CONSUMER_SECRET;
        }

        public static bool AcquireRequestToken()
        {
            string requestUrl = "https://openapi.etsy.com/v2/oauth/request_token?scope=transactions_r";
            OAuthResponse tokenResponse = oAuth.AcquireRequestToken(requestUrl, "GET");

            string unescapedAuthUrl = Uri.UnescapeDataString(tokenResponse.AllText);
            unescapedAuthUrl = unescapedAuthUrl.Remove(0, "login_url=".Length);
            System.Diagnostics.Process.Start(unescapedAuthUrl);
            
            if (tokenResponse != null)
                return true;
            else
                return false;
        }

        public static void AcquireAccessToken()
        {
            string uri = "https://openapi.etsy.com/v2/oauth/access_token";
            string code = (string)Settings.Default["EtsyVerificationCode"];
            var response = oAuth.AcquireAccessToken(uri, "GET", code);
            SettingsViewModel.EtsyAccessToken = response["oauth_token"];
            SettingsViewModel.EtsyAccessTokenSecret = response["oauth_token_secret"];
        }
    }
}
