using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockAndOrders.Model
{
    public class Price
    {
        public int amount { get; set; }
        public int divisor { get; set; }
        public string currency_code { get; set; }
        public string currency_formatted_short { get; set; }
        public string currency_formatted_long { get; set; }
        public string currency_formatted_raw { get; set; }
    }

    public class Offering
    {
        public long offering_id { get; set; }
        public Price price { get; set; }
        public int quantity { get; set; }
        public int is_enabled { get; set; }
        public int is_deleted { get; set; }
    }

    public class ProductData
    {
        public long product_id { get; set; }
        public string sku { get; set; }
        public List<object> property_values { get; set; }
        public List<Offering> offerings { get; set; }
        public int is_deleted { get; set; }
    }

    public class Transaction
    {
        public int Transaction_id { get; set; }
        public string Title { get; set; }
        public string description { get; set; }
        public int seller_user_id { get; set; }
        public int buyer_user_id { get; set; }
        public int creation_tsz { get; set; }
        public int paid_tsz { get; set; }
        public int shipped_tsz { get; set; }
        public string Price { get; set; }
        public string Currency_code { get; set; }
        public int Quantity { get; set; }
        public List<string> tags { get; set; }
        public List<string> materials { get; set; }
        public int image_listing_id { get; set; }
        public int receipt_id { get; set; }
        public string shipping_cost { get; set; }
        public bool is_digital { get; set; }
        public string file_data { get; set; }
        public int listing_id { get; set; }
        public bool is_quick_sale { get; set; }
        public object seller_feedback_id { get; set; }
        public object buyer_feedback_id { get; set; }
        public string transaction_type { get; set; }
        public string url { get; set; }
        public List<object> variations { get; set; }
        public ProductData product_data { get; set; }
    }
}
