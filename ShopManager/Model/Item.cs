using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopManager.Helper;

namespace ShopManager.Model
{
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

    public class Listing : ObservableObject
    {
        private int listing_id;
        private string title;
        private string description;
        private string price;
        private string currency_code;
        private int quantity;
        private long shipping_template_id;
        private string who_made;
        private string is_supply;
        private string when_made;
        private bool is_digital;
        private bool should_auto_renew;
        private int taxonomy_id;
        private List<string> taxonomy_path;

        public int Listing_id
        {
            get { return listing_id; }
            set
            {
                if (listing_id != value)
                {
                    listing_id = value;
                    OnPropertyChanged("Who_made");
                }
            }
        }

        public string Title
        {
            get { return title; }
            set
            {
                if (title != value)
                {
                    title = value;
                    OnPropertyChanged("Title");
                }
            }
        }
        public string Description
        {
            get { return description; }
            set
            {
                if (description != value)
                {
                    description = value;
                    OnPropertyChanged("Description");
                }
            }
        }
        public string Price
        {
            get { return price; }
            set
            {
                if (price != value)
                {
                    price = value;
                    OnPropertyChanged("Price");
                }
            }
        }

        public string Currency_code
        {
            get { return currency_code; }
            set
            {
                if (currency_code != value)
                {
                    currency_code = value;
                    OnPropertyChanged("Currency_code");
                }
            }
        }

        public int Quantity
        {
            get { return quantity; }
            set
            {
                if (quantity != value)
                {
                    quantity = value;
                    OnPropertyChanged("Quantity");
                }
            }
        }

        public long Shipping_template_id
        {
            get { return shipping_template_id; }
            set
            {
                if (shipping_template_id != value)
                {
                    shipping_template_id = value;
                    OnPropertyChanged("Shipping_template_id");
                }
            }
        }

        //enum(i_did, collective, someone_else)
        public string Who_made
        {
            get { return who_made; }
            set
            {
                if (who_made != value)
                {
                    who_made = value;
                    OnPropertyChanged("Who_made");
                }
            }
        }

        public string Is_supply
        {
            get { return is_supply; }
            set
            {
                if (is_supply != value)
                {
                    is_supply = value;
                    OnPropertyChanged("Is_supply");
                }
            }
        }

        //enum(made_to_order, 2020_2020, 2010_2019, 2001_2009, before_2001, 2000_2000, 1990s, 1980s, 1970s, 1960s, 1950s, 1940s, 1930s, 1920s, 1910s, 1900s, 1800s, 1700s, before_1700)
        public string When_made
        {
            get { return when_made; }
            set
            {
                if (when_made != value)
                {
                    when_made = value;
                    OnPropertyChanged("When_made");
                }
            }
        }

        public bool Is_digital
        {
            get { return is_digital; }
            set
            {
                if (is_digital != value)
                {
                    is_digital = value;
                    OnPropertyChanged("Is_digital");
                }
            }
        }

        public bool Should_auto_renew
        {
            get { return should_auto_renew; }
            set
            {
                if (should_auto_renew != value)
                {
                    should_auto_renew = value;
                    OnPropertyChanged("Should_auto_renew");
                }
            }
        }

        public int Taxonomy_id
        {
            get { return taxonomy_id; }
            set
            {
                if (taxonomy_id != value)
                {
                    taxonomy_id = value;
                    OnPropertyChanged("Taxonomy_id");
                }
            }
        }

        public List<string> Taxonomy_path
        {
            get { return taxonomy_path; }
            set
            {
                if (taxonomy_path != value)
                {
                    taxonomy_path = value;
                    OnPropertyChanged("Taxonomy_path");
                }
            }
        }
    }

    public enum CurrencyCode
    {
        EUR,
        USD,
    }
}
