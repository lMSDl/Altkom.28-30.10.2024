using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inheritance.Models
{
    internal class Circle : Shape2D
    {
        public float Radius { get; private set; }

        public Circle(float radius) : base("okrąg", 2*radius, 2 * radius)
        {
            Radius = radius;
        }

        public override float CalculateArea()
        {
            return (float)(Math.PI * Math.Pow(Radius, 2));
        }


        public override string GetName()
        {
            return $"{Name} o promieniu {Radius}";
        }
    }
}
