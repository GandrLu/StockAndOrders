using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Model
{
    public class ShippingMethod
    {
        private int id;
        private string name;

        public ShippingMethod(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }

    public class Order
    {
        private int id;
        private Customer customer;
        private Address billingAddress;
        private Address shippingAddress;
        private List<Item> orderedItems;

        public Order(int id, Customer customer, Address billingAddress, Address shippingAddress, List<Item> orderedItems)
        {
            Id = id;
            Customer = customer;
            BillingAddress = billingAddress;
            ShippingAddress = shippingAddress;
            OrderedItems = orderedItems;
        }

        public int Id { get => id; set => id = value; }
        public Customer Customer { get => customer; set => customer = value; }
        public Address BillingAddress { get => billingAddress; set => billingAddress = value; }
        public Address ShippingAddress { get => shippingAddress; set => shippingAddress = value; }
        public List<Item> OrderedItems { get => orderedItems; set => orderedItems = value; }
    }

    public class Shipment
    {
        public object receipt_shipping_id { get; set; }
        public int mailing_date { get; set; }
        public string carrier_name { get; set; }
        public string tracking_code { get; set; }
        public string major_tracking_state { get; set; }
        public string current_step { get; set; }
        public object current_step_date { get; set; }
        public object mail_class { get; set; }
        public string buyer_note { get; set; }
        public int notification_date { get; set; }
        public bool is_etsy_only_tracking { get; set; }
        public string tracking_url { get; set; }
    }

    public class ShippingDetails
    {
        public bool can_mark_as_shipped { get; set; }
        public bool was_shipped { get; set; }
        public bool is_future_shipment { get; set; }
        public int shipment_date { get; set; }
        public bool has_free_shipping_discount { get; set; }
        public string not_shipped_state_display { get; set; }
        public string shipping_method { get; set; }
        public bool is_estimated_delivery_date_relevant { get; set; }
        public string estimated_delivery_date { get; set; }
        public string delivery_date { get; set; }
        public bool is_delivered { get; set; }
        public bool is_german_user { get; set; }
    }

    public class Receipt
    {
        public int Receipt_id { get; set; }
        //public int receipt_type { get; set; }
        public int Order_id { get; set; }
        //public int seller_user_id { get; set; }
        //public int buyer_user_id { get; set; }
        public int Creation_tsz { get; set; }
        //public bool can_refund { get; set; }
        //public int last_modified_tsz { get; set; }
        //public string name { get; set; }
        //public string first_line { get; set; }
        //public string second_line { get; set; }
        //public string city { get; set; }
        //public string state { get; set; }
        //public string zip { get; set; }
        public string Formatted_address { get; set; }
        //public int country_id { get; set; }
        //public string payment_method { get; set; }
        //public string payment_email { get; set; }
        public object Message_from_seller { get; set; }
        public string Message_from_buyer { get; set; }
        public bool Was_paid { get; set; }
        //public string total_tax_cost { get; set; }
        //public string total_vat_cost { get; set; }
        public string Total_price { get; set; }
        public string total_shipping_cost { get; set; }
        public string Currency_code { get; set; }
        public object Message_from_payment { get; set; }
        public bool Was_shipped { get; set; }
        //public string buyer_email { get; set; }
        //public string seller_email { get; set; }
        //public bool is_gift { get; set; }
        //public bool needs_gift_wrap { get; set; }
        //public string gift_message { get; set; }
        //public string discount_amt { get; set; }
        //public string subtotal { get; set; }
        public string Grandtotal { get; set; }
        //public string adjusted_grandtotal { get; set; }
        //public string buyer_adjusted_grandtotal { get; set; }
        public List<Shipment> Shipments { get; set; }
        public int Shipped_date { get; set; }
        public bool Is_overdue { get; set; }
        public int Days_from_due_date { get; set; }
        public ShippingDetails Shipping_details { get; set; }
        //public string transparent_price_message { get; set; }
        //public bool show_channel_badge { get; set; }
        //public string channel_badge_suffix_string { get; set; }
        //public bool is_dead { get; set; }
    }
}
