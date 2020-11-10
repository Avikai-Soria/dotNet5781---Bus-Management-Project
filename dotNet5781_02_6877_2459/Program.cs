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
       /* public string Cat(int a, int b, int cat)
        {
            return "cat";
        }*/
        static void Main(string[] args)
        {
            /*BusLine cat = new BusLine(123456);
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
            Console.WriteLine(all);
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
            /*Console.WriteLine(cat.Overall_Duration());
            Console.WriteLine(oppositecat.Overall_Duration());
            Console.WriteLine(zug_cat.Overall_Duration());
            Console.WriteLine(ez_cat.Overall_Duration());*/
            /*
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
            BusLine line_1 = new BusLine(1);                            // This BusLine will have all 40 BusLine Station
            BusLine line_1_reverse = new BusLine(1);                    // Reversed busline 1
            BusLine line_2 = new BusLine(2);                            // This will recive only first and last station
            BusLine line_3 = new BusLine(3);                            // This line will recive all even numbers of stations
            BusLine line_4 = new BusLine(4);                            // This line will recive all odd numbers of stations
            BusLine line_5 = new BusLine(5);                            // This line will recive all stations that divide by 5
            BusLine line_6 = new BusLine(6);                            // This line will recive all stations that divide by 6
            BusLine line_7 = new BusLine(7);                            // This line will recive all stations that divide by 7
            BusLine line_8 = new BusLine(8);                            // This line will recive all stations that divide by 8
            BusLine line_9 = new BusLine(9);                            // This line will recive all stations that divide by 9
            BusLine line_10 = new BusLine(10);                          // This line will recive all stations that divide by 10
            BusLine_Station[] a = new BusLine_Station[40];
            List<BusLine_Station> stations = new List<BusLine_Station>();
            for (int i = 0; i < 40; i++)
            {
                a[i] = new BusLine_Station(i, "a" + i);                 // 40 random stations
                line_1.Add(a[i], i);
                if (i == 0 || i == 39)
                    line_2.Add(a[i], i / 39);
                if (i % 2 == 0)
                    line_3.Add(a[i], i / 2);
                if (i % 2 == 1)
                    line_4.Add(a[i], i / 2);
                if (i % 5 == 0)
                    line_5.Add(a[i], i / 5);
                if (i % 6 == 0)
                    line_6.Add(a[i], i / 6);
                if (i % 7 == 0)
                    line_7.Add(a[i], i / 7);
                if (i % 8 == 0)
                    line_8.Add(a[i], i / 8);
                if (i % 9 == 0)
                    line_9.Add(a[i], i / 9);
                if (i % 10 == 0)
                    line_10.Add(a[i], i / 10);
                //Console.WriteLine(a[i]);
            }
            stations = a.ToList<BusLine_Station>();
            for (int i = 39; i >= 0; i--)
                line_1_reverse.Add(stations[i], 39 - i);
            BusLine_Collection collection = new BusLine_Collection();
            collection.Add(line_1); collection.Add(line_2); collection.Add(line_3); collection.Add(line_4); 
            collection.Add(line_5); collection.Add(line_6); collection.Add(line_7); collection.Add(line_8); collection.Add(line_9); collection.Add(line_10);
            // collection.Add(line_1_reverse); don't want to add this now for the testing
            //Console.WriteLine(collection);            Up to this point, we created a collection that contains 10 BusLines and 40 Stations
            int choice = 0;
            int sub_choice = 0;
            String input;
            do
            {
                Console.WriteLine("Press 1 if you want to add anything.");
                Console.WriteLine("Press 2 if you want to remove anything.");
                Console.WriteLine("Press 3 if you want to search anything.");
                Console.WriteLine("Press 4 if you want to print anything.");
                Console.WriteLine("Press 5 if you want to quit this program.");
                input = Console.ReadLine();
                while (!Int32.TryParse(input, out choice) || ((choice < 1) || (choice > 5)))
                {
                    Console.WriteLine("Invalid input was entered, please insert a number between 1 to 5 again.");
                    input = Console.ReadLine();
                }
                switch (choice)
                {
                    case 1:     // ADD
                        Console.WriteLine("Press 1 if you want to add a new BusLine to the collection.");
                        Console.WriteLine("Press 2 if you want to add a new station to a BusLine.");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out sub_choice) || ((sub_choice < 1) || (sub_choice > 2)))
                        {
                            Console.WriteLine("Invalid input was entered, please insert a number between 1 to 2 again.");
                            input = Console.ReadLine();
                        }
                        if (sub_choice==1)
                        {
                            int bs_id;
                            int first;
                            int last;
                            Console.WriteLine("Insert the new busline's ID");
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out bs_id) || ((bs_id < 0)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number bigger than 0.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("Insert first station, can be a number between 0 to " + (stations.Count-1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out first) || ((first < 0)) || (first>(stations.Count-1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("Insert last station, can be a number between 0 to " + (stations.Count-1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out last) || ((last < 0)) || (last > (stations.Count-1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            BusLine to_add = new BusLine(bs_id, a[first], a[last]);
                            collection.Add(to_add);
                        }
                        else
                        {
                            int station_to_add;
                            int added_to;
                            int where_to;
                            Console.WriteLine("Please insert station ID you would like to add between 0 to " + (stations.Count - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out station_to_add) || ((station_to_add < 0)) || (station_to_add > (stations.Count - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("Please insert bus number you would like to add the station to, between 0 to " + (collection.Count() - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out added_to) || ((added_to < 0)) || (added_to > (collection.Count() - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("Please insert where would you want to insert the station, must be between 0 to "+collection[added_to].Count());
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out where_to) || ((where_to < 0)) || (where_to > (collection[added_to].Count())))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            collection[added_to].Add(a[station_to_add], where_to);
                        }
                        break;

                    case 2:     // REMOVE
                        Console.WriteLine("Press 1 if you want to remove a BusLine from the collection.");
                        Console.WriteLine("Press 2 if you want to remove a station from a BusLine.");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out sub_choice) || ((sub_choice < 1) || (sub_choice > 2)))
                        {
                            Console.WriteLine("Invalid input was entered, please insert a number between 1 to 2 again.");
                            input = Console.ReadLine();
                        }
                        if (sub_choice == 1)
                        {

                        }
                        else
                        {

                        }
                        break;

                    case 3:     // SEARCH
                        Console.WriteLine("Press 1 if you want to seach BusLines that go through a specific station.");
                        Console.WriteLine("Press 2 if you want to search possibilities for travel between 2 stations.");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out sub_choice) || ((sub_choice < 1) || (sub_choice > 2)))
                        {
                            Console.WriteLine("Invalid input was entered, please insert a number between 1 to 2 again.");
                            input = Console.ReadLine();
                        }
                        if (sub_choice == 1)
                        {

                        }
                        else
                        {

                        }
                        break;
                    case 4:     // PRINT
                        Console.WriteLine("Press 1 if you want to print all the BusLines that are in the collection.");
                        Console.WriteLine("Press 2 if you want to print all the stations and the numbers of BusLines that passes them.");
                        input = Console.ReadLine();
                        while (!Int32.TryParse(input, out sub_choice) || ((sub_choice < 1) || (sub_choice > 2)))
                        {
                            Console.WriteLine("Invalid input was entered, please insert a number between 1 to 2 again.");
                            input = Console.ReadLine();
                        }
                        if (sub_choice == 1)
                        {
                            foreach (BusLine bus in collection)
                                Console.WriteLine(bus);
                        }
                        else
                        {

                        }
                        break;
                    case 5:     // End
                        Console.WriteLine("Case 5");
                        Console.WriteLine("End of the program. Cya!");
                        break;
                }
            } while (choice != 5);
            Console.ReadKey();
        }
    }
}
