namespace StockAndOrders.Model
{
    public class Params
    {
        public string shop_id { get; set; }
        public object min_created { get; set; }
        public object max_created { get; set; }
        public object min_last_modified { get; set; }
        public object max_last_modified { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public object page { get; set; }
        public object was_paid { get; set; }
        public object was_shipped { get; set; }
    }


}
