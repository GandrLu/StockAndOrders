namespace ShopManager.Model
{
    public class JsonResult<T>
    {
        public int count { set; get; }
        public T[] results { set; get; }
        public Params @params { get; set; }
        public string type { get; set; }
        public Pagination pagination { get; set; }
    }
}
