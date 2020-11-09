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
            BusLine oppositecat = new BusLine(123456);
            BusLine mendoxai = new BusLine(123456);
            BusLine zug_cat = new BusLine(321);
            BusLine ez_cat = new BusLine(123);
            BusLine_Station[] a = new BusLine_Station[40];
            for (int i = 0; i < 40; i++)
            { 
                a[i] = new BusLine_Station(i, "a"+i); // todo create 40 more like this
                cat.Add(a[i], i);
                if (i % 2 == 0)
                    zug_cat.Add(a[i], i / 2);
                if (i % 2 == 1)
                    ez_cat.Add(a[i], i / 2);
                //Console.WriteLine(a[i]);
            }
            for (int i = 39; i > -1; i--)
                oppositecat.Add(a[i], 39 - i);
            BusLine_Station cattwo = new BusLine_Station(594, "abcd");
            BusLine_Collection all = new BusLine_Collection();
            all.Add(cat);  all.Add(oppositecat); all.Add(zug_cat); all.Add(ez_cat); // all.Add(Mendoxai) Add working
            /*Console.WriteLine(all); Removes working
            Console.WriteLine("Removing 312");
            all.Remove(zug_cat);
            Console.WriteLine(all);
            Console.WriteLine("Removing 123456");
            all.Remove(cat);
            Console.WriteLine(all);
            all.Remove(mendoxai);
            Console.WriteLine(all);*/
            //foreach (BusLine bus in all.GetLineByStation(3)) GetLine works
            //      Console.WriteLine(bus); 
            /* Console.WriteLine(cat.Overall_Duration());
             Console.WriteLine(oppositecat.Overall_Duration());
             Console.WriteLine(zug_cat.Overall_Duration());
             Console.WriteLine(ez_cat.Overall_Duration());

             foreach (BusLine bus in all.SortCollection())
                   Console.WriteLine(bus); */
            //Console.WriteLine(all[3]);
            //Console.WriteLine(cat.Check(cattwo));
            //Console.WriteLine(cat.Check(a[22]));
            //Console.WriteLine(cat);
            //Console.WriteLine(oppositecat);
            //Console.WriteLine(zug_cat);
            //Console.WriteLine(cat.Is_Reversed(oppositecat));// Expected to be true
            //Console.WriteLine(oppositecat.Is_Reversed(cat));// Expected to be true too
            //Console.WriteLine(cat.Is_Reversed(zug_cat));    // Expected to be false
            //Console.WriteLine(zug_cat.Is_Reversed(cat));    // Expected to be false too
            /*Console.WriteLine(cat.Distance(a[35], a[5]));
            Console.WriteLine(cat.Duration(a[35], a[5]));
            Console.WriteLine(cat.subLine(a[35], a[5]));
            Console.WriteLine(cat.CompareTo(cat.subLine(a[35], a[5]))); // Expected to be negative
            Console.WriteLine(cat.subLine(a[35], a[5]).CompareTo(cat)); // Expected to be positive
            Console.WriteLine(cat.CompareTo(cat));                      // Expected to be zero*/
            Console.ReadKey();
        }
    }
}
