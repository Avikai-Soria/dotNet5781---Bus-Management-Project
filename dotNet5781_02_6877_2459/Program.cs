using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    class Program
    {
        /// <summary>
        /// cat
        /// </summary>
        /// <param name="a"> a is cat number a</param>
        /// <param name="b"> b is cat number cat</param>
        /// <param name="cat">cat is cat number cat</param>
        /// <returns> returns a cat </returns>
        public string Cat(int a, int b, int cat)
        {
            return "cat";
        }
        static void Main(string[] args)
        {
            BusLine cat = new BusLine(123456);
            BusLine_Station[] a = new BusLine_Station[40];
            for (int i = 0; i < 40; i++)
            { 
                a[i] = new BusLine_Station(i, "a"+i); // todo create 40 more like this
                cat.Add(a[i], i);
                Console.WriteLine(a[i]);
            }
            BusLine_Station cattwo = new BusLine_Station(594, "abcd");
            Console.WriteLine(cat.Check(cattwo));
            Console.WriteLine(cat.Check(a[22]));
            Console.WriteLine(cat);
            Console.WriteLine(cat.Distance(a[35], a[5]));
            Console.WriteLine(cat.Duration(a[35], a[5]));
            Console.WriteLine(cat.subLine(a[35], a[5]));
            Console.WriteLine(cat.CompareTo(cat.subLine(a[35], a[5]))); // Expected to be negative
            Console.WriteLine(cat.subLine(a[35], a[5]).CompareTo(cat)); // Expected to be positive
            Console.WriteLine(cat.CompareTo(cat));                      // Expected to be zero
            Console.ReadKey();
        }
    }
}
