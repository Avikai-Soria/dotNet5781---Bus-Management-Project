using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_00_6877_2459
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome6877();
            Welcome2459();
            Console.ReadKey();
        }

        static partial void Welcome2459();
        private static void Welcome6877()
        {
            Console.Write("Enter your name: ");
            string a = Console.ReadLine();
            Console.WriteLine(a + ", welcome to my first console application");
        }
    }
}
