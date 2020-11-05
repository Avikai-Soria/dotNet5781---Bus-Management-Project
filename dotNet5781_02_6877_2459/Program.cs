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
            Bus_Station a = new Bus_Station(1234567, 31.234567, 34.56789, "רח' פלוני אלמוני 12, תל חורף");
            Console.WriteLine(a);
            Console.ReadKey();
        }
    }
}
