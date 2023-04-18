using MintaZH.Abstractions;
using MintaZH.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MintaZH
{
    public partial class Form1 : Form
    {
        List<Product> _products = new List<Product>();
        public Form1()
        {
            InitializeComponent();
            ProcessXml();
            DisplayProducts();
        }

        public string LoadXml(string fileName)
        {
            using (StreamReader sr = new StreamReader(fileName, Encoding.Default))
            {
                var output = sr.ReadLine();
                while (!sr.EndOfStream)
                {
                    output += "\n" + sr.ReadLine();
                }
                return output;
            }
        }
        public void ProcessXml()
        {
            var xmlText = LoadXml("Menu.xml");

            var xml = new XmlDocument();
            xml.LoadXml(xmlText);

            foreach (XmlElement item in xml.DocumentElement)
            {
                var name = item.SelectSingleNode("name").InnerText;
                var description = item.SelectSingleNode("description").InnerText;
                var calories = item.SelectSingleNode("calories").InnerText;
                var type = item.SelectSingleNode("type").InnerText;

                if(type.Equals("food"))
                {
                    var food = new Food()
                    {
                        Title = name,
                        Description = description,
                        Calories = int.Parse(calories),
                    };
                _products.Add(food);
                }

                if (type.Equals("drink"))
                {
                    var drink = new Drink()
                    {
                        Title = name,
                        Calories = int.Parse(calories),
                    };
                    _products.Add(drink);
                }
            }
        }
        private void DisplayProducts()
        {
            var orderedProducts =
                from p in _products
                orderby p.Title
                select p;

            Product előző = null;
            foreach ( var product in orderedProducts)
            {
                this.Controls.Add(product);
                if (előző != null)
                    product.Top = előző.Top + előző.Height;
                else
                    product.Top = 0;
                előző = product;
            }
        }
    }
}
