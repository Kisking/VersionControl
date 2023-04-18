using MintaZH.Abstractions;
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

            }
        }
    }
}
