using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopManager.Model
{
    class JsonResult<T>
    {
        public int count { set; get; }
        public T[] results { set; get; }
        public Params @params { get; set; }
        public string type { get; set; }
        public Pagination pagination { get; set; }
    }

    public class Category
    {
        private int id;
        private string name;

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
    }

    public class Item
    {
        private int id;
        private int quantity;
        private float price;
        private string name;
        private string description;
        private string color;
        private Category category;

        public Item(int id, string name, string description, string color, Category category, float price, int quantity)
        {
            Id = id;
            Name = name;
            Description = description;
            Color = color;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public int Id { get => id; set => id = value; }
        public int Quantity { get => quantity; set => quantity = value; }
        public float Price { get => price; set => price = value; }
        public string Name { get => name; set => name = value; }
        public string Description { get => description; set => description = value; }
        public string Color { get => color; set => color = value; }
        public Category Category { get => category; set => category = value; }
    }

    public class Listing
    {
        public int listing_id { get; set; }
        //public string state { get; set; }
        //public int user_id { get; set; }
        ////public object category_id { get; set; }
        public string title { get; set; }
        public string description { get; set; }
        ////public int creation_tsz { get; set; }
        ////public int ending_tsz { get; set; }
        //public int original_creation_tsz { get; set; }
        //public int last_modified_tsz { get; set; }
        public string price { get; set; }
        public string currency_code { get; set; }
        public int quantity { get; set; }
        //public List<string> sku { get; set; }
        //public List<string> tags { get; set; }
        //public List<string> materials { get; set; }
        //public object shop_section_id { get; set; }
        //public object featured_rank { get; set; }
        //public int state_tsz { get; set; }
        //public string url { get; set; }
        //public int views { get; set; }
        //public int num_favorers { get; set; }
        public long shipping_template_id { get; set; }
        //public int processing_min { get; set; }
        //public int processing_max { get; set; }

        //enum(i_did, collective, someone_else)
        public string who_made { get; set; }
        public string is_supply { get; set; }
        //enum(made_to_order, 2020_2020, 2010_2019, 2001_2009, before_2001, 2000_2000, 1990s, 1980s, 1970s, 1960s, 1950s, 1940s, 1930s, 1920s, 1910s, 1900s, 1800s, 1700s, before_1700)
        public string when_made { get; set; }

        //public object item_weight { get; set; }
        //public string item_weight_unit { get; set; }
        //public object item_length { get; set; }
        //public object item_width { get; set; }
        //public object item_height { get; set; }
        //public string item_dimensions_unit { get; set; }
        //public bool is_private { get; set; }
        //public object recipient { get; set; }
        //public object occasion { get; set; }
        //public object style { get; set; }
        //public bool non_taxable { get; set; }
        //public bool is_customizable { get; set; }
        public bool is_digital { get; set; }
        //public string file_data { get; set; }
        //public bool can_write_inventory { get; set; }
        public bool should_auto_renew { get; set; }
        //public string language { get; set; }
        //public bool has_variations { get; set; }
        public int taxonomy_id { get; set; }
        public List<string> taxonomy_path { get; set; }
        //public bool used_manufacturer { get; set; }
        //public bool is_vintage { get; set; }
    }

    public class Params
    {
        public string shop_id { get; set; }
        public int limit { get; set; }
        public int offset { get; set; }
        public object page { get; set; }
    }

    public class Pagination
    {
        public int effective_limit { get; set; }
        public int effective_offset { get; set; }
        public object next_offset { get; set; }
        public int effective_page { get; set; }
        public object next_page { get; set; }
    }

    public class Root
    {
        public int count { get; set; }
        public List<Listing> listing { get; set; }
        public Params @params { get; set; }
        public string type { get; set; }
        public Pagination pagination { get; set; }
    }

    public enum CurrencyCode
    {
        EUR,
        USD,
    }
}
