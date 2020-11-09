namespace StockAndOrders.Model
{
    public class Pagination
    {
        public int effective_limit { get; set; }
        public int effective_offset { get; set; }
        public object next_offset { get; set; }
        public int effective_page { get; set; }
        public object next_page { get; set; }
    }
}
