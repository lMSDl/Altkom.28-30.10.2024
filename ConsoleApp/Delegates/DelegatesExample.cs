using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp.Delegates
{
    internal class DelegatesExample
    {
        delegate void VoidWithoutParameters();
        delegate void VoidWithStringParameter(string @string);
        public delegate bool BoolWithParameters(int x, int y);

        public void Func1()
        {
            Console.WriteLine("1");
        }
        public void Func2(string input)
        {
            Console.WriteLine(input);
        }
        public bool Func3(int a, int b)
        {
            Console.WriteLine("a = " + a + " ,b = " + b);
            return a == b;
        }


        public BoolWithParameters Delegate3 { get; set; }

        public void Test()
        {
            VoidWithoutParameters delegate1 = new VoidWithoutParameters(Func1);

            delegate1.Invoke();

            VoidWithStringParameter delegate2 = Func2;

            delegate2.Invoke("Hello!");

            Delegate3 = Func3;

            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    var result = Delegate3(i, j);
                    if(result)
                        Console.WriteLine("==");
                }
            }

        }

        public bool Check(BoolWithParameters func, int a, int b)
        {
            return func(a , b);
        }
    }
}
