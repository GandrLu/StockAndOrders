using Org.BouncyCastle.Asn1.X509;
using StockAndOrders.Helper;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace StockAndOrders.Model
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
        public int? notification_date { get; set; }
        public bool is_etsy_only_tracking { get; set; }
        public string tracking_url { get; set; }

        public void CopyTo(Shipment target)
        {
            if (target == null)
                throw new ArgumentNullException();

            target.receipt_shipping_id = receipt_shipping_id;
            target.mailing_date = mailing_date;
            target.carrier_name = carrier_name;
            target.tracking_code = tracking_code;
            target.major_tracking_state = major_tracking_state;
            target.current_step = current_step;
            target.current_step_date = current_step_date;
            target.mail_class = mail_class;
            target.buyer_note = buyer_note;
            target.notification_date = notification_date;
            target.is_etsy_only_tracking = is_etsy_only_tracking;
            target.tracking_url = tracking_url;
        }
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

    public class Receipt : ObservableObject
    {
        private int receipt_id;
        private string formatted_address;
        private bool was_shipped;
        private List<Shipment> shipments;
        private string message_from_buyer;
        private List<Transaction> transactions;

        public int Receipt_id
        {
            get
            {
                return receipt_id;
            }
            set
            {
                if (value != receipt_id)
                {
                    receipt_id = value;
                    OnPropertyChanged(nameof(Receipt_id));
                }
            }
        }

        //public int receipt_type { get; set; }
        public int Order_id { get; set; }
        //public int seller_user_id { get; set; }
        //public int buyer_user_id { get; set; }
        public int Creation_tsz { get; set; }
        //public bool can_refund { get; set; }
        //public int last_modified_tsz { get; set; }
        public string Name { get; set; }
        //public string first_line { get; set; }
        //public string second_line { get; set; }
        //public string city { get; set; }
        //public string state { get; set; }
        //public string zip { get; set; }
        public string Formatted_address
        {
            get
            {
                return formatted_address;
            }
            set
            {
                if (value != formatted_address)
                {
                    formatted_address = value;
                    OnPropertyChanged(nameof(Formatted_address));
                }
            }
        }

        //public int country_id { get; set; }
        //public string payment_method { get; set; }
        //public string payment_email { get; set; }
        public object Message_from_seller { get; set; }
        public string Message_from_buyer
        {
            get
            {
                return message_from_buyer;
            }
            set
            {
                if (value != message_from_buyer)
                {
                    message_from_buyer = value;
                    OnPropertyChanged(nameof(Message_from_buyer));
                }
            }
        }

        public bool Was_paid { get; set; }
        //public string total_tax_cost { get; set; }
        //public string total_vat_cost { get; set; }
        public string Total_price { get; set; }
        public string total_shipping_cost { get; set; }
        public string Currency_code { get; set; }
        public object Message_from_payment { get; set; }
        public bool Was_shipped
        {
            get
            {
                return was_shipped;
            }
            set
            {
                if (value != was_shipped)
                {
                    was_shipped = value;
                    OnPropertyChanged(nameof(Was_shipped));
                }
            }
        }


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
        public List<Shipment> Shipments
        {
            get
            {
                return shipments;
            }
            set
            {
                if (value != shipments)
                {
                    shipments = value;
                    OnPropertyChanged(nameof(Shipments));
                }
            }
        }

        public int Shipped_date { get; set; }
        public bool Is_overdue { get; set; }
        public int Days_from_due_date { get; set; }
        public ShippingDetails Shipping_details { get; set; }
        //public string transparent_price_message { get; set; }
        //public bool show_channel_badge { get; set; }
        //public string channel_badge_suffix_string { get; set; }
        //public bool is_dead { get; set; }
        public List<Transaction> Transactions
        {
            get
            {
                return transactions;
            }
            set
            {
                if (value != transactions)
                {
                    transactions = value;
                    OnPropertyChanged(nameof(Transactions));
                }
            }
        }

        public void CopyTo(Receipt target)
        {
            if (target == null)
                throw new ArgumentNullException();

            target.Receipt_id = Receipt_id;
            target.Formatted_address = Formatted_address;
            target.Was_shipped = Was_shipped;
            target.Message_from_buyer = Message_from_buyer;
            target.Transactions = Transactions;

            var shipmentsToCopy = new List<Shipment>();
            foreach (var shipment in Shipments)
            {
                var copiedShipment = new Shipment();
                shipment.CopyTo(copiedShipment);
                shipmentsToCopy.Add(copiedShipment);
            }
            target.Shipments = shipmentsToCopy;
        }
    }
}
