using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Linq
{
    public partial class Form1 : Form
    {
        private List<Product> products;

        public Form1()
        {
            InitializeComponent();
            products = new List<Product>
        {
            new Product(1, "Laptop", "Electronics", 999.99m, 10),
            new Product(2, "Smartphone", "Electronics", 499.99m, 15),
            new Product(3, "Tablet", "Electronics", 299.99m, 20),
            new Product(4, "Headphones", "Accessories", 199.99m, 30),
            new Product(5, "Charger", "Accessories", 49.99m, 50),
            new Product(6, "Shoes", "Apparel", 79.99m, 40),
            new Product(7, "T-shirt", "Apparel", 19.99m, 100)
        };
            SaveData(); // Save initial data to file
            LoadData();
            DisplayProducts(products);
            PopulateCategoryComboBox();
            PopulateSortByComboBox();
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            if (File.Exists("products.txt"))
            {
                products.Clear();
                var lines = File.ReadAllLines("products.txt");
                foreach (var line in lines)
                {
                    var parts = line.Split(',');
                    var product = new Product(
                        int.Parse(parts[0]),
                        parts[1],
                        parts[2],
                        decimal.Parse(parts[3]),
                        int.Parse(parts[4])
                    );
                    products.Add(product);
                }
            }
        }

        private void SaveData()
        {
            var lines = products.Select(p => $"{p.ID},{p.Name},{p.Category},{p.Price},{p.Quantity}");
            File.WriteAllLines("products.txt", lines);
        }

        private void DisplayProducts(IEnumerable<Product> productsToDisplay)
        {
            dataGridView1.DataSource = productsToDisplay.ToList();
        }

        private void PopulateCategoryComboBox()
        {
            var categories = products.Select(p => p.Category).Distinct().ToList();
            comboBox1.Items.Clear();
            comboBox1.Items.AddRange(categories.ToArray());
        }

        private void PopulateSortByComboBox()
        {
            comboBox2.Items.Clear();
            comboBox2.Items.Add("Name");
            comboBox2.Items.Add("Price");
        }

        private void FilterProducts(string category)
        {
            var filteredProducts = from p in products
                                   where p.Category == category
                                   select p;
            DisplayProducts(filteredProducts);
        }

        private void SortProducts(string sortBy)
        {
            IEnumerable<Product> sortedProducts = null;
            if (sortBy == "Name")
            {
                sortedProducts = from p in products
                                 orderby p.Name
                                 select p;
            }
            else if (sortBy == "Price")
            {
                sortedProducts = from p in products
                                 orderby p.Price
                                 select p;
            }
            DisplayProducts(sortedProducts);
        }

        private void SearchProducts(string searchTerm)
        {
            var searchedProducts = from p in products
                                   where p.Name.StartsWith(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                                         p.Name.Equals(searchTerm, StringComparison.OrdinalIgnoreCase)
                                   select p;
            DisplayProducts(searchedProducts);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            string category = comboBox1.SelectedItem?.ToString();
            if (category != null)
            {
                FilterProducts(category);
            }
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            string sortBy = comboBox2.SelectedItem?.ToString();
            if (sortBy != null)
            {
                SortProducts(sortBy);
            }
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;
            SearchProducts(searchTerm);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

    private void button1_Click(object sender, EventArgs e)
        {
            string category = comboBox1.SelectedItem?.ToString();
            if (category != null)
            {
                FilterProducts(category);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string sortBy = comboBox2.SelectedItem?.ToString();
            if (sortBy != null)
            {
                SortProducts(sortBy);
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string searchTerm = textBox1.Text;
            SearchProducts(searchTerm);
        }
    }
}

