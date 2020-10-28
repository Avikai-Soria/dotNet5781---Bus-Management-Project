using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_01_6877_2459
{
    class Bus
    {
        string id;
        DateTime creationDate;
        int overallMileage; // kilometraj
        int fuel;
        int currentMileage; 
        public Bus(string idg, DateTime creationDateg, int overallMileageg, int fuelg , int currentMileageg)
        {
            id = idg;
            creationDate = creationDateg;
            overallMileage = overallMileageg;
            fuel = fuelg;
            currentMileage = currentMileageg;
        }
        public override String ToString ()
        {
            String to_return = "Bus id: " + id + "\n" + "Activation date: " + creationDate + "\n" + "The bus's overall mileage is: "
                + overallMileage + "\n" + "Current fuel is: " + fuel + "\n" + "The bus's current mileage is: " + currentMileage + "\n" +"\n";
            return (to_return);
        }
        // funcs to make: 
    }
}
