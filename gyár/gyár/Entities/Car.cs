using gyár.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace gyár.Entities
{
    public class Car : Toy
    {
        protected override void DrawImage(Graphics g)
        {
            Image imageFiles = Image.FromFile("C:\\Users\\kinga\\Asztali gép\\IRF/car.png");
            g.DrawImage(imageFiles, new Rectangle(0, 0, Width, Height));
        }
    }
}
