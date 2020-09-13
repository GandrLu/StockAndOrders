using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
