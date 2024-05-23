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
            products = new List<Product>();
            LoadData();
            PopulateCategoryComboBox();
            PopulateSortByComboBox();
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

        private void button1_Click(object sender, EventArgs e) // Фільтрує продукти за категорією
        {
            string category = comboBox1.SelectedItem?.ToString();
            if (category != null)
            {
                FilterProducts(category);
            }
        }

        private void button2_Click(object sender, EventArgs e) // Сортує продукти за вибраним параметром
        {
            string sortBy = comboBox2.SelectedItem?.ToString();
            if (sortBy != null)
            {
                SortProducts(sortBy);
            }
        }

        private void button3_Click(object sender, EventArgs e) // Шукає продукти за введеним текстом
        {
            string searchTerm = textBox1.Text;
            SearchProducts(searchTerm);
        }

        private void button4_Click_1(object sender, EventArgs e)
        {
            SaveData();
            MessageBox.Show("Data saved successfully!");
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            var newProduct = new Product(8, "New Product", "New Category", 123.45m, 5);
            products.Add(newProduct);
            DisplayProducts(products);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void button6_Click(object sender, EventArgs e)
        {
            LoadData();
            DisplayProducts(products);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}

