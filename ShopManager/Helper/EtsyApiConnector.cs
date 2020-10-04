using OAuth;
using ShopManager.View;
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

        public EtsyApiConnector()
        {
            
        }

        public static bool AcquireRequestToken()
        {
            var oAuth = new OAuth.Manager();
            string requestUrl = "https://openapi.etsy.com/v2/oauth/request_token?scope=email_r";
            oAuth["consumer_key"] = CONSUMER_KEY;
            oAuth["consumer_secret"] = CONSUMER_SECRET;
            OAuthResponse tokenResponse = oAuth.AcquireRequestToken(requestUrl, "GET");

            string unescapedAuthUrl = Uri.UnescapeDataString(tokenResponse.AllText);
            unescapedAuthUrl = unescapedAuthUrl.Remove(0, "login_url=".Length);
            System.Diagnostics.Process.Start(unescapedAuthUrl);
            
            if (tokenResponse != null)
                return true;
            else
                return false;
        }
    }
}
