using OAuth;
using ShopManager.Model;
using ShopManager.Properties;
using ShopManager.ViewModel;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

namespace ShopManager.Helper
{
    public sealed class EtsyApiConnector
    {
        private const string CONSUMER_KEY = "fill in key";
        private const string CONSUMER_SECRET = "fill in secret";

        private const string REQUEST_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/request_token?scope=transactions_r listings_r";
        private const string ACCESS_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/access_token";
        private const string GET_TRANSACTIONS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/transactions";
        private const string GET_ACTIVELISTINGS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/listings/draft";
        private const string GET_RECEIPTS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/receipts";
        private const string GET_TRANSACTIONBYRECEIPT_URI = "https://openapi.etsy.com/v2/receipts/:receiptId/transactions";

        private OAuth.Manager oAuth = new Manager();
        private static OAuth.Manager staticOAuth = new OAuth.Manager();
        private static HttpClient client = new HttpClient();
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        static EtsyApiConnector()
        {
            staticOAuth["consumer_key"] = CONSUMER_KEY;
            staticOAuth["consumer_secret"] = CONSUMER_SECRET;
            staticOAuth["token"] = SettingsViewModel.EtsyAccessToken;
            staticOAuth["token_secret"] = SettingsViewModel.EtsyAccessTokenSecret;
        }

        public EtsyApiConnector()
        {
            oAuth["consumer_key"] = CONSUMER_KEY;
            oAuth["consumer_secret"] = CONSUMER_SECRET;
            oAuth["token"] = SettingsViewModel.EtsyAccessToken;
            oAuth["token_secret"] = SettingsViewModel.EtsyAccessTokenSecret;
        }

        public static bool AcquireRequestToken()
        {
            staticOAuth["token"] = "";
            staticOAuth["token_secret"] = "";
            OAuthResponse responseTokenURI = staticOAuth.AcquireRequestToken(REQUEST_TOKEN_URI, "GET");

            if (responseTokenURI != null)
            {
                // Start default webbrowser to open authentification webpage
                System.Diagnostics.Process.Start(UnescapeAndExtractUri(responseTokenURI.AllText));
                return true;
            }
            else
                return false;
        }

        public static void AcquireAccessToken()
        {
            string code = (string)Settings.Default["EtsyVerificationCode"];
            var response = staticOAuth.AcquireAccessToken(ACCESS_TOKEN_URI, "GET", code);
            SettingsViewModel.EtsyAccessToken = response["oauth_token"];
            SettingsViewModel.EtsyAccessTokenSecret = response["oauth_token_secret"];
        }

        public static async Task GetTransactions()
        {
            SetHeader(GET_TRANSACTIONS_URI);
            var response = await client.GetStringAsync(GET_TRANSACTIONS_URI);
            Console.WriteLine(response);
        }

        public static async Task<JsonResult<Listing>> GetListings()
        {
            SetHeader(GET_ACTIVELISTINGS_URI);
            var response = await client.GetStringAsync(GET_ACTIVELISTINGS_URI);

            JsonResult<Listing> items = JsonSerializer.Deserialize<JsonResult<Listing>>(response, jsonSerializerOptions);
            return items;
        }

        public static async Task<JsonResult<Receipt>> GetReceipts()
        {
            SetHeader(GET_RECEIPTS_URI);
            var response = await client.GetStringAsync(GET_RECEIPTS_URI);

            JsonResult<Receipt> receipts = JsonSerializer.Deserialize<JsonResult<Receipt>>(response, jsonSerializerOptions);
            return receipts;
        }

        public async Task<JsonResult<Transaction>> GetTransactionByReceipt(string receiptId)
        {
            try
            {
                using (HttpClient localClient = new HttpClient())
                {
                    string requestUri = GET_TRANSACTIONBYRECEIPT_URI.Replace(":receiptId", receiptId);
                    string header = oAuth.GenerateAuthzHeader(requestUri, "GET");
                    localClient.DefaultRequestHeaders.Add("Authorization", header);

                    var response = await localClient.GetStringAsync(requestUri);
                    JsonResult<Transaction> transactions = JsonSerializer.Deserialize<JsonResult<Transaction>>(response, jsonSerializerOptions);
                    return transactions;
                }
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e + " Header: " + client.DefaultRequestHeaders.GetValues("Authorization"));
                throw;
            }

        }

        public static async Task<JsonResult<Transaction>> GetTransactionsByReceipt(string receiptId)
        {
            try
            {
                string requestUri = GET_TRANSACTIONBYRECEIPT_URI.Replace(":receiptId", receiptId);
                SetHeader(requestUri);
                var response = await client.GetStringAsync(requestUri);

                JsonResult<Transaction> transactions = JsonSerializer.Deserialize<JsonResult<Transaction>>(response, jsonSerializerOptions);
                return transactions;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine(e + " Header: " + client.DefaultRequestHeaders.GetValues("Authorization"));
                throw;
            }
        }

        #region Helper
        private static string UnescapeAndExtractUri(string uri)
        {
            string unescapedUrl = Uri.UnescapeDataString(uri);
            return unescapedUrl.Remove(0, "login_url=".Length);
        }

        private static void SetHeader(string uri, string method = "GET")
        {
            string header = staticOAuth.GenerateAuthzHeader(uri, "GET");
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", header);
        }
        #endregion
    }
}
