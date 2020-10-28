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
            List<Bus> list_of_buses;
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
                        DateTime date;
                        while (!DateTime.TryParse(Console.ReadLine(), out date))
                        {
                            Console.WriteLine("Invalid input was entered, please insert a proper date.");
                        }
                        break;
                    case 2:     // Select bus for travel
                        Console.WriteLine("Case 2");
                        break;
                    case 3:     // Maintain a bus
                        Console.WriteLine("Case 3");
                        break;
                    case 4:     // Display mileage
                        Console.WriteLine("Case 4");
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
