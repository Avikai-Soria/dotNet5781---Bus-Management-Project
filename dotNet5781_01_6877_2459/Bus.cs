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
        public Bus(string idg, DateTime creationDateg)
        {
            id = idg;
            creationDate = creationDateg;
            overallMileage = 0;     // Empty Mileage
            fuel = 1200;            // Full tank
            currentMileage = 0;     // Empty Mileage
        }
        // funcs to make: 
    }
}
