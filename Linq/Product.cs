using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Linq
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }

        public Product(int id, string name, string category, decimal price, int quantity)
        {
            ID = id;
            Name = name;
            Category = category;
            Price = price;
            Quantity = quantity;
        }

        public override string ToString()
        {
            return $"{ID}: {Name} - {Category} - ${Price} - Quantity: {Quantity}";
        }
    }
}
