using System;
using System.Collections.Generic;
using System.Data.Odbc;
using System.IO;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6877_2459
{
    class Program
    {
        static string fix_id(string id)
        {
            if(id.Length==7)
            {
                id = id.Substring(0, 2) + '-' + id.Substring(2, 3) + '-' + id.Substring(5, 2);
            }
            else
            {
                id = id.Substring(0, 3) + '-' + id.Substring(3, 2) + '-' + id.Substring(5, 3);
            }
            return id;
        }
        static void Main(string[] args)
        {
            List<Bus> list_of_buses = new List<Bus>();
            bool flag;                                          // Will serve case 1
            int choice;                                         // Will serve the switch case
            int sub_choice;                                     // Mostly for case 3
            Random r = new Random(DateTime.Now.Millisecond); // r will serve us through the whole program
            int testid;                                      // This int will be used to test the id digits
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
                     //   Console.WriteLine("Case 1");
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
                            id = Console.ReadLine();
                            while (!Int32.TryParse(id, out testid) || ((testid < 1000000) || (testid > 9999999)))
                            {
                                Console.WriteLine("Invalid input was entered, make sure you insert only 7 digits.");
                                id = Console.ReadLine();
                            }
                            id = fix_id(id);
                            Console.WriteLine(id);
                        }
                        else
                        {
                            Console.WriteLine("Please insert 8 digits of the license number."); // Assuming first digit can't be 0.
                            id = Console.ReadLine();
                            while (!Int32.TryParse(id, out testid) || ((testid < 10000000) || (testid > 99999999)))
                            {
                                Console.WriteLine("Invalid input was entered, make sure you insert only 8 digits.");
                                id = Console.ReadLine();
                            }
                            id = fix_id(id);
                            Console.WriteLine(id);
                        }
                        flag = true; // assuming the id doesn't exists
                        foreach (Bus check in list_of_buses)
                            if (check.Id == id)
                            {
                                flag = false;   // id already exists
                            }
                        if (!flag)
                        {
                            Console.WriteLine("ERROR! This license number already exist in the system.");
                            break;
                        }
                        int overallMileage;    // IMPORTANT
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
                        Console.WriteLine("If you want to insert current Mileage, insert a number lower than or eqeual to " + overallMileage + ". Otherwise insert anything else. Default value is 0.");
                        if (!Int32.TryParse(Console.ReadLine(), out currentMileage) || currentMileage>=overallMileage)
                        {
                            currentMileage = 0;
                        }
                        Bus current_bus = new Bus(id, date, overallMileage, fuel, currentMileage);
                        Console.WriteLine(current_bus);
                        list_of_buses.Add(current_bus);
                        break;

                    case 2:     // Select bus for travel
                        //Console.WriteLine("Case 2");
                        string search_id;
                        Console.WriteLine("Please insert the license number of the bus you would like to use.");
                        Console.WriteLine("Insert only 7 digits or 8 digits");
                        search_id = Console.ReadLine();
                        while (!Int32.TryParse(search_id, out testid) || ((testid < 1000000) || (testid > 99999999)))
                        {
                            Console.WriteLine("Invalid input was entered, make sure you insert only 7 or 8 digits.");
                            search_id = Console.ReadLine();
                        }
                        search_id = fix_id(search_id);
                        Bus current = null;
                        foreach (Bus check in list_of_buses)
                            if (check.Id == search_id)
                                current = check;
                        int travel_r;
                        travel_r = r.Next(0, 1200);        // min drive will be 0 km, max would be 1200 km
                        Console.WriteLine("The travel will last " + travel_r + " kilometers.");
                        if (current == null)
                        {
                            Console.WriteLine("Bus's license number doesn't exist in the list.");
                            break;
                        }
                        if (!current.IsQualified())
                        {
                            Console.WriteLine("The bus is not qualified for a travel.");
                            break;
                        }
                        if(!current.IsCapable(travel_r))
                        {
                            Console.WriteLine("The bus is not capable of completing this travel safely.");
                            break;
                        }
                        current.PerformTravel(travel_r);
                        Console.WriteLine("The travel succeeded! The bus's current details are updated.");
                        break;

                    case 3:     // Maintain a bus
                       // Console.WriteLine("Case 3");
                        string to_fix_id;
                        Console.WriteLine("Please insert the license number of the bus you would like to use.");
                        Console.WriteLine("Insert only 7 digits or 8 digits");
                        to_fix_id = Console.ReadLine();
                        while (!Int32.TryParse(to_fix_id, out testid) || ((testid < 1000000) || (testid > 99999999)))
                        {
                            Console.WriteLine("Invalid input was entered, make sure you insert only 7 or 8 digits.");
                            to_fix_id = Console.ReadLine();
                        }
                        to_fix_id = fix_id(to_fix_id);
                        Bus current_to_fix = null;
                        foreach (Bus check in list_of_buses)
                            if (check.Id == to_fix_id)
                                current_to_fix = check;
                        if (current_to_fix == null)
                        {
                            Console.WriteLine("Bus's license number doesn't exist in the list.");
                            break;
                        }
                        Console.WriteLine("The bus was found! What action would you like to perform?");
                        Console.WriteLine("Insert 1 if you want to perform refueling.");
                        Console.WriteLine("Insert 2 if you want to perform maintenance");
                        while (!Int32.TryParse(Console.ReadLine(), out sub_choice) || ((sub_choice < 1) || (sub_choice > 2)))
                        {
                            Console.WriteLine("Invalid input was entered, please insert either 1 or 2.");
                        }
                        if(sub_choice==1)
                        {
                            current_to_fix.PerformRefueling();
                            Console.WriteLine("The bus was refueled successfully");
                        }
                        else
                        {
                            current_to_fix.PerformMaintenance();
                            Console.WriteLine("The bus was maintinced successfully");
                        }
                        break;
                    case 4:     // Display mileage
                      //  Console.WriteLine("Case 4");
                        foreach(Bus bus_to_print in list_of_buses)
                            Console.WriteLine(bus_to_print);
                        break;
                    case 5:     // End
                        Console.WriteLine("Case 5");
                        Console.WriteLine("End of the program. Cya!");
                        break;
                }
            } while (choice != 5);
            Console.ReadKey(); // End of the program
        }
    }
}
