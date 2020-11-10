using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    class Program
    {
        static void Main(string[] args)
        {
            BusLine line_1 = new BusLine(0);                            // This BusLine will have all 40 BusLine Station
            BusLine line_1_reverse = new BusLine(0);                    // Reversed busline 1
            BusLine line_2 = new BusLine(1);                            // This will recive only first and last station
            BusLine line_3 = new BusLine(2);                            // This line will recive all even numbers of stations
            BusLine line_4 = new BusLine(3);                            // This line will recive all odd numbers of stations
            BusLine line_5 = new BusLine(4);                            // This line will recive all stations that divide by 5
            BusLine line_6 = new BusLine(5);                            // This line will recive all stations that divide by 6
            BusLine line_7 = new BusLine(6);                            // This line will recive all stations that divide by 7
            BusLine line_8 = new BusLine(7);                            // This line will recive all stations that divide by 8
            BusLine line_9 = new BusLine(8);                            // This line will recive all stations that divide by 9
            BusLine line_10 = new BusLine(9);                          // This line will recive all stations that divide by 10
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
                            try { collection.Add(to_add); }
                            catch (ArgumentException e) { Console.WriteLine(e.Message); }
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
                            Console.WriteLine("Please insert position of the bus in the list you would like to add the station to, between 0 to " + (collection.Count() - 1));
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
                            int station_to_remove;
                            Console.WriteLine("Please insert position of the bus in the list you would like to remove, number between 0 to " + (collection.Count() - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out station_to_remove) || ((station_to_remove < 0)) || (station_to_remove > (collection.Count() - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            collection.Remove(collection[station_to_remove]);
                        }
                        else
                        {
                            int station_to_remove;
                            int removed_from;
                            Console.WriteLine("Please insert station ID you would like to add between 0 to " + (stations.Count - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out station_to_remove) || ((station_to_remove < 0)) || (station_to_remove > (stations.Count - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("Please insert position of the bus in the list you would like to add the station to, between 0 to " + (collection.Count() - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out removed_from) || ((removed_from < 0)) || (removed_from > (collection.Count() - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            try { collection[removed_from].Remove(a[station_to_remove]); }
                            catch (ArgumentOutOfRangeException e) { Console.WriteLine(e.Message); }

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
                            int station_id;
                            Console.WriteLine("Please insert a station ID to look for");
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out station_id))
                            {
                                Console.WriteLine("Invalid input was entered, please insert again.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("The buslines who are passing this station are: ");
                            foreach (BusLine busLine in collection)
                            {
                                if (busLine.Check_Id(station_id))
                                    Console.WriteLine(busLine.BusLine_Id);
                            }
                        }
                        else
                        {
                            int first_id;
                            int second_id;
                            BusLine_Collection possible_lines = new BusLine_Collection();
                            Console.WriteLine("Please insert the first station's ID, between 0 to " + (stations.Count - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out first_id) || ((first_id < 0)) || (first_id > (stations.Count - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            Console.WriteLine("Please insert the second station's ID, between 0 to " + (stations.Count - 1));
                            input = Console.ReadLine();
                            while (!Int32.TryParse(input, out second_id) || ((second_id < 0)) || (second_id > (stations.Count - 1)))
                            {
                                Console.WriteLine("Invalid input was entered, please insert a number in the correct range.");
                                input = Console.ReadLine();
                            }
                            foreach (BusLine busLine in collection)
                                if ((busLine.Check_Id(first_id)) && (busLine.Check_Id(second_id)))
                                    possible_lines.Add(busLine); // At this point all the possible lines should be in the collection
                            foreach(BusLine busLine1 in possible_lines.SortCollection())
                            {
                                Console.WriteLine("BusLine " + busLine1.BusLine_Id + " is available and will take " + busLine1.Duration(a[first_id], a[second_id]) 
                                    + " minutes to complete the ride");
                            }
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
                            foreach (BusLine_Station busLine_Station in stations)
                            {
                                Console.WriteLine(busLine_Station);
                                Console.WriteLine("The Buslines that are passing this station are: ");
                                foreach (BusLine busLine in collection)
                                {
                                    if (busLine.Check(busLine_Station))
                                        Console.WriteLine(busLine.BusLine_Id);
                                }
                            }
                        }
                        break;
                    case 5:     // End
                        Console.WriteLine("End of the program. Cya!");
                        break;
                }
            } while (choice != 5);
            Console.ReadKey();
        }
    }
}
