using OAuth;
using StockAndOrders.Model;
using StockAndOrders.Properties;
using StockAndOrders.Repositories;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;
using System.Collections;
using System.Collections.Generic;

namespace StockAndOrders.Helper
{
    public sealed class EtsyApiConnector : IReceiptRepository, IListingRepository
    {
        private static readonly EtsyApiConnector instance = new EtsyApiConnector();

        private const string REQUEST_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/request_token?scope=";
        private const string SCOPES = "transactions_r transactions_w listings_r listings_w";
        private const string ACCESS_TOKEN_URI = "https://openapi.etsy.com/v2/oauth/access_token";
        // TODO: Get name of shop dynamically
        private const string GET_TRANSACTIONS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/transactions";
        // Fetch inactive listings for tests
        private const string GET_ACTIVELISTINGS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/listings/inactive";
        private const string GET_RECEIPTS_URI = "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/receipts?includes=Transactions";
        private const string PUT_LISTING_URI = "https://openapi.etsy.com/v2/listings/Listing_id";
        private const string POST_TRACKINGDATA_URI= "https://openapi.etsy.com/v2/shops/dixKeramikwerkstatt/receipts/Receipt_id/tracking";
        private const string DEFAULT_CARRIER_NAME = "dhl-germany";
        private const bool DEFAULT_SEND_SHIPNOTIFICATION = true;

        private OAuth.Manager oAuth = new OAuth.Manager();
        private HttpClient client = new HttpClient();
        private JsonSerializerOptions jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        // Explicit static constructor to tell C# compiler not to mark type as beforefieldinit
        static EtsyApiConnector() { }

        private EtsyApiConnector()
        {
            oAuth["consumer_key"] = (string)Settings.Default["EtsyAppKey"];
            oAuth["consumer_secret"] = (string)Settings.Default["EtsyAppSecret"];
            oAuth["token"] = (string)Settings.Default["EtsyAccessToken"];
            oAuth["token_secret"] = (string)Settings.Default["EtsyAccessTokenSecret"];
            if (oAuth["consumer_key"] == "" || oAuth["consumer_secret"] == "")
            {
                throw new Exception("Not all necessary settings are set!");
            }
        }

        public static EtsyApiConnector Instance
        {
            get
            {
                return instance;
            }
        }

        public bool AcquireRequestToken()
        {
            oAuth["token"] = "";
            oAuth["token_secret"] = "";
            OAuthResponse responseTokenURI = oAuth.AcquireRequestToken(REQUEST_TOKEN_URI + SCOPES, "GET");

            if (responseTokenURI != null)
            {
                // Start default webbrowser to open authentification webpage
                System.Diagnostics.Process.Start(UnescapeAndExtractUri(responseTokenURI.AllText));
                return true;
            }
            else
                return false;
        }

        public void AcquireAccessToken()
        {
            string code = (string)Settings.Default["EtsyVerificationCode"];
            var response = oAuth.AcquireAccessToken(ACCESS_TOKEN_URI, "GET", code);
            Settings.Default.EtsyAccessToken = response["oauth_token"];
            Settings.Default.EtsyAccessTokenSecret = response["oauth_token_secret"];
            Settings.Default.Save();
            oAuth["token"] = response["oauth_token"];
            oAuth["token_secret"] = response["oauth_token_secret"];
        }

        public async Task<IList> GetListings()
        {
            try
            {
                SetHeader(GET_ACTIVELISTINGS_URI);
                var response = await client.GetStringAsync(GET_ACTIVELISTINGS_URI);

                JsonResult<Listing> items = JsonSerializer.Deserialize<JsonResult<Listing>>(response, jsonSerializerOptions);
                return new List<Listing>(items.results);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Etsy connection failed: " + e);
                throw;
            }
        }

        public async Task<IList> GetReceipts()
        {
            try
            {
                SetHeader(GET_RECEIPTS_URI);
                var response = await client.GetStringAsync(GET_RECEIPTS_URI);

                JsonResult<Receipt> receipts = JsonSerializer.Deserialize<JsonResult<Receipt>>(response, jsonSerializerOptions);
                return new List<Receipt>(receipts.results);
                //return receipts;
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine("Etsy connection failed: " + e);
                throw;
            }
        }

        public async Task<bool> PutListingQuantityUpdate(Listing listing)
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

        public async Task<bool> PostTrackingData(Receipt receipt)
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
        private string UnescapeAndExtractUri(string uri)
        {
            string unescapedUrl = Uri.UnescapeDataString(uri);
            return unescapedUrl.Remove(0, "login_url=".Length);
        }

        private void SetHeader(string uri, string method = "GET")
        {
            string header = oAuth.GenerateAuthzHeader(uri, method);
            client.DefaultRequestHeaders.Remove("Authorization");
            client.DefaultRequestHeaders.Add("Authorization", header);
        }
        #endregion
    }
}
