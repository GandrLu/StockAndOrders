﻿using OAuth;
using ShopManager.Model;
using ShopManager.Properties;
using ShopManager.View;
using ShopManager.ViewModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Text.Json;

namespace ShopManager.Helper
{
    class EtsyApiConnector
    {
        private const string CONSUMER_KEY = "fill in key";
        private const string CONSUMER_SECRET = "fill in secret";

        private const string REQUEST_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/request_token?scope=transactions_r listings_r";
        private const string ACCESS_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/access_token";
        private const string GET_TRANSACTIONS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/transactions";
        private const string GET_ACTIVELISTINGS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/listings/draft";


        private static OAuth.Manager oAuth = new OAuth.Manager();
        private static HttpClient client = new HttpClient();

        static EtsyApiConnector()
        {
            oAuth["consumer_key"] = CONSUMER_KEY;
            oAuth["consumer_secret"] = CONSUMER_SECRET;
            oAuth["token"] = SettingsViewModel.EtsyAccessToken;
            oAuth["token_secret"] = SettingsViewModel.EtsyAccessTokenSecret;
        }

        public static bool AcquireRequestToken()
        {
            oAuth["token"] = "";
            oAuth["token_secret"] = "";
            OAuthResponse tokenResponse = oAuth.AcquireRequestToken(REQUEST_TOKEN_URI, "GET");
            
            if (tokenResponse != null)
            {
                System.Diagnostics.Process.Start(UnescapeAndExtractUri(tokenResponse.AllText));
                return true;
            }
            else
                return false;
        }

        public static void AcquireAccessToken()
        {
            string code = (string)Settings.Default["EtsyVerificationCode"];
            var response = oAuth.AcquireAccessToken(ACCESS_TOKEN_URI, "GET", code);
            SettingsViewModel.EtsyAccessToken = response["oauth_token"];
            SettingsViewModel.EtsyAccessTokenSecret = response["oauth_token_secret"];
        }

        public static async Task GetTransactions()
        {
            string header = oAuth.GenerateAuthzHeader(GET_TRANSACTIONS_URI, "GET");
            client.DefaultRequestHeaders.Add("Authorization", header);
            var response = await client.GetStringAsync(GET_TRANSACTIONS_URI);
            Console.WriteLine(response);
        }

        public static async Task<JsonResult<Listing>> GetItems()
        {
            string header = oAuth.GenerateAuthzHeader(GET_ACTIVELISTINGS_URI, "GET");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", header);

            var response = await client.GetStringAsync(GET_ACTIVELISTINGS_URI);
            Console.WriteLine(response);

            JsonResult<Listing> items = JsonSerializer.Deserialize<JsonResult<Listing>>(response);
            return items;
        }

        #region Helper
        private static string UnescapeAndExtractUri(string uri)
        {
            string unescapedUrl = Uri.UnescapeDataString(uri);
            return unescapedUrl.Remove(0, "login_url=".Length);
        }
        #endregion
    }
}
