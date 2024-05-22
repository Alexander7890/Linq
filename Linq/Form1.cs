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
            DisplayProducts(products);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        private void LoadData()
        {
            if (File.Exists("products.csv"))
            {
                var lines = File.ReadAllLines("products.csv");
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
            File.WriteAllLines("products.csv", lines);
        }

        private void DisplayProducts(IEnumerable<Product> productsToDisplay)
        {
            dataGridView1.DataSource = productsToDisplay.ToList();
        }

        private void FilterProducts(string category)
        {
            var filteredProducts = products.Where(p => p.Category == category);
            DisplayProducts(filteredProducts);
        }

        private void SortProducts(string sortBy)
        {
            IEnumerable<Product> sortedProducts = sortBy switch
            {
                "Name" => products.OrderBy(p => p.Name),
                "Price" => products.OrderBy(p => p.Price),
                _ => products
            };
            DisplayProducts(sortedProducts);
        }

        private void SearchProducts(string searchTerm)
        {
            var searchedProducts = products.Where(p => p.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
            DisplayProducts(searchedProducts);
        }

        private void buttonFilter_Click(object sender, EventArgs e)
        {
            string category = textBoxCategory.Text;
            FilterProducts(category);
        }

        private void buttonSort_Click(object sender, EventArgs e)
        {
            string sortBy = comboBoxSortBy.SelectedItem.ToString();
            SortProducts(sortBy);
        }

        private void buttonSearch_Click(object sender, EventArgs e)
        {
            string searchTerm = textBoxSearch.Text;
            SearchProducts(searchTerm);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveData();
        }
    }
}

