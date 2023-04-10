using gyár.Abstractions;
using gyár.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace gyár
{
    public partial class Form1 : Form
    {
        private List<Toy> _toys = new List<Toy>();

        private Toy _nextToy;

        private IToyFactory _factory;
        public IToyFactory Factory
        {
            get { return _factory; }
            set
            {
                _factory = value;
                DisplayNext();
            }
        }
        public Form1()
        {
            InitializeComponent();
            Factory = new CarFactory();
        }

        private void createTimer_Tick(object sender, EventArgs e)
        {

        }

        private void conveyorTimer_Tick(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            Factory = new CarFactory();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Factory = new BallFactory();
        }

        private void DisplayNext()
        {
            if (_nextToy != null)
            {
                mainPanel.Controls.Remove(_nextToy);
            }
            _nextToy = Factory.CreateNew();
            _nextToy.Top = label1.Top + label1.Height + 20;
            _nextToy.Left = label1.Left;
            mainPanel.Controls.Add(_nextToy);
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }
    }
}
