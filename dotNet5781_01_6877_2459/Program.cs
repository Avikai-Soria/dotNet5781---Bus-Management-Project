using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6877_2459
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bus> list_of_buses = new List<Bus>();
            int choice;
            Console.WriteLine("Welcome user! What action would you like to do today?");
            do
            {
                Console.WriteLine("Press 1 if you want to insert a new bus into the list.");
                Console.WriteLine("Press 2 if you want to select a bus for a new travel.");
                Console.WriteLine("Press 3 if you want to maintain a bus.");
                Console.WriteLine("Press 4 if you want to display mileage details of all the buses.");
                Console.WriteLine("Press 5 if you want to quit this program.");
                string input = Console.ReadLine();
                while (!Int32.TryParse(input, out choice) || ((choice < 0) || (choice > 5)))
                {
                    Console.WriteLine("Invalid input was entered, please insert a number between 1 to 5 again.");
                    input = Console.ReadLine();
                }
                switch (choice)
                {
                    case 1:     // Insert a new bus to the line
                        Console.WriteLine("Case 1");
                        Console.WriteLine("Please insert activation date in a form of dd/mm/yyyy.");
                        DateTime date;          // IMPORTANT
                        while (!DateTime.TryParse(Console.ReadLine(), out date))
                        {
                            Console.WriteLine("Invalid input was entered, please insert a proper date.");
                        }
                        string id;              // IMPORTANT
                        if ((date.Year) < 2018)
                        {
                            Console.WriteLine("Please insert 7 digits of the license number."); // Assuming first digit can't be 0.
                            int testid;
                            id = Console.ReadLine();
                            while (!Int32.TryParse(id, out testid) || ((testid < 1000000) || (testid > 9999999)))
                            {
                                Console.WriteLine("Invalid input was entered, make sure you insert only 7 digits.");
                                id = Console.ReadLine();
                            }
                            id = id.Substring(0, 2) + '-' + id.Substring(2, 3) + '-' + id.Substring(5, 2);
                            Console.WriteLine(id);
                        }
                        else
                        {
                            Console.WriteLine("Please insert 8 digits of the license number."); // Assuming first digit can't be 0.
                            int testid;
                            id = Console.ReadLine();
                            while (!Int32.TryParse(id, out testid) || ((testid < 10000000) || (testid > 99999999)))
                            {
                                Console.WriteLine("Invalid input was entered, make sure you insert only 7 digits.");
                                id = Console.ReadLine();
                            }
                            id = id.Substring(0, 3) + '-' + id.Substring(3, 2) + '-' + id.Substring(5, 3);
                            Console.WriteLine(id);
                        }
                        int overallMileage;         // IMPORTANT
                        Console.WriteLine("If you want to insert overall Mileage, insert a number. Otherwise insert anything else. Default value is 0.");
                        if (!Int32.TryParse(Console.ReadLine(), out overallMileage)) 
                        {
                            overallMileage = 0;
                        }
                        int fuel;                   // IMPORTANT
                        Console.WriteLine("If you want to insert fuel, insert a number between 0 to 1200. Otherwise insert anything else. Default value is 1200.");
                        if (!Int32.TryParse(Console.ReadLine(), out fuel) || fuel > 1200 || fuel < 0) 
                        {
                            fuel = 1200;
                        }
                        int currentMileage;         // IMPORTANT
                        Console.WriteLine("If you want to insert overall Mileage, insert a number. Otherwise insert anything else. Default value is 0.");
                        if (!Int32.TryParse(Console.ReadLine(), out currentMileage))
                        {
                            currentMileage = 0;
                        }
                        Bus current_bus = new Bus(id, date, overallMileage, fuel, currentMileage);
                        Console.WriteLine(current_bus);
                        list_of_buses.Add(current_bus);
                        break;
                    case 2:     // Select bus for travel
                        Console.WriteLine("Case 2");
                        break;
                    case 3:     // Maintain a bus
                        Console.WriteLine("Case 3");
                        break;
                    case 4:     // Display mileage
                        Console.WriteLine("Case 4");
                        foreach(Bus current in list_of_buses)
                            Console.WriteLine(current);
                        break;
                    case 5:     // End
                        Console.WriteLine("Case 5");
                        break;
                    default:
                        Console.WriteLine("Invalid input was entered, please select again");
                        break;
                }
            } while (choice != 5);
            Console.ReadKey(); // End of the program
        }
    }
}
