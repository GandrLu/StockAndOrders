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
        private const string REQUEST_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/request_token?scope=";
        private const string SCOPES = "transactions_r transactions_w listings_r listings_w";
        private const string ACCESS_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/access_token";
        private const string GET_TRANSACTIONS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/transactions";
        // Fetch inactive listings for tests
        private const string GET_ACTIVELISTINGS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/listings/inactive";
        private const string GET_RECEIPTS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/receipts?includes=Transactions";
        private const string PUT_LISTING_URI = "https://openapi.etsy.com/v2/listings/Listing_id";
        private const string POST_TRACKINGDATA_URI= "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/receipts/Receipt_id/tracking";
        private const string DEFAULT_CARRIER_NAME = "dhl-germany";
        private const bool DEFAULT_SEND_SHIPNOTIFICATION = true;

        private static OAuth.Manager staticOAuth = new OAuth.Manager();
        private static HttpClient client = new HttpClient();
        private static JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        static EtsyApiConnector()
        {
            staticOAuth["consumer_key"] = (string)Settings.Default["EtsyAppKey"];
            staticOAuth["consumer_secret"] = (string)Settings.Default["EtsyAppSecret"];
            staticOAuth["token"] = (string)Settings.Default["EtsyAccessToken"];
            staticOAuth["token_secret"] = (string)Settings.Default["EtsyAccessTokenSecret"];
            if (staticOAuth["consumer_key"] == "" || staticOAuth["consumer_secret"] == "" 
                || staticOAuth["token"] == "" || staticOAuth["token_secret"] == "")
            {
                throw new Exception("Not all necessary settings are set!");
            }
        }

        public static bool AcquireRequestToken()
        {
            staticOAuth["token"] = "";
            staticOAuth["token_secret"] = "";
            OAuthResponse responseTokenURI = staticOAuth.AcquireRequestToken(REQUEST_TOKEN_URI + SCOPES, "GET");

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
            Settings.Default["EtsyAccessToken"] = response["oauth_token"];
            Settings.Default["EtsyAccessTokenSecret"] = response["oauth_token_secret"];
        }

        public static async Task GetTransactions()
        {
            SetHeader(GET_TRANSACTIONS_URI);
            var response = await client.GetStringAsync(GET_TRANSACTIONS_URI);
            Console.WriteLine(response);
        }

        public static async Task<JsonResult<Listing>> GetListings()
        {
            try
            {
                SetHeader(GET_ACTIVELISTINGS_URI);
                var response = await client.GetStringAsync(GET_ACTIVELISTINGS_URI);

                JsonResult<Listing> items = JsonSerializer.Deserialize<JsonResult<Listing>>(response, jsonSerializerOptions);
                return items;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Etsy connection failed: " + e);
                throw;
            }
        }

        public static async Task<JsonResult<Receipt>> GetReceipts()
        {
            try
            {
                SetHeader(GET_RECEIPTS_URI);
                var response = await client.GetStringAsync(GET_RECEIPTS_URI);

                JsonResult<Receipt> receipts = JsonSerializer.Deserialize<JsonResult<Receipt>>(response, jsonSerializerOptions);
                return receipts;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Etsy connection failed: " + e);
                throw;
            }
        }

        public static async Task<bool> PutListingQuantityUpdate(Listing listing)
        {
            if ((bool)Settings.Default["IsAppInTestMode"])
                return true;
            try
            {
                string putUri = PUT_LISTING_URI.Replace(nameof(listing.Listing_id), listing.Listing_id.ToString());
                string listingState = (listing.State == "edit") ? "inactive" : listing.State;
                putUri += $"?quantity={listing.Quantity}&state={listingState}";
                SetHeader(putUri, "PUT");

                var response = await client.PutAsync(putUri, null);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Etsy connection failed: " + e);
                throw;
            }
        }

        public static async Task<bool> PostTrackingData(Receipt receipt)
        {
            if ((bool)Settings.Default["IsAppInTestMode"])
                return true;
            try
            {
                string postUri = POST_TRACKINGDATA_URI.Replace(nameof(receipt.Receipt_id), receipt.Receipt_id.ToString());
                if (receipt.Shipments == null)
                    return false;
                postUri += $"?tracking_code={receipt.Shipments[0].tracking_code}" +
                    $"&carrier_name={DEFAULT_CARRIER_NAME}&send_bcc={DEFAULT_SEND_SHIPNOTIFICATION}";
                SetHeader(postUri, "POST");

                var response = await client.PostAsync(postUri, null);
                return response.IsSuccessStatusCode;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Etsy connection failed: " + e);
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
            string header = staticOAuth.GenerateAuthzHeader(uri, method);
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", header);
        }
        #endregion
    }
}
