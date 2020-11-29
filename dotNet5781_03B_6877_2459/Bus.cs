using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03B_6877_2459
{
    enum State
    {
        Ready,
        Busy,
        On_Refueling,
        On_Maintenance
    }
    class Bus
    {
        string id;
        DateTime creationDate;
        DateTime lastMaintenance;
        int overallMileage; // kilometraj
        int fuel;           // 1200 fuel is full
        int currentMileage; // May not pass 20000
        State status = State.Ready;
        public string Id
        {
            get => id;
            set => id = value;
        }
        internal State Status { get => status; set => status = value; }

        public Bus(string idg, DateTime creationDateg, int overallMileageg, int fuelg, int currentMileageg)
        {
            id = idg;
            creationDate = creationDateg;
            lastMaintenance = creationDateg;
            overallMileage = overallMileageg;
            fuel = fuelg;
            currentMileage = currentMileageg;
        }
        public override String ToString()
        {
            String to_return = "Bus id: " + id + "\n" + "Activation date: " + creationDate.ToShortDateString() + "\n" + "Last Maintenance date: " + lastMaintenance.ToShortDateString() + "\nThe bus's overall mileage is: "
                + overallMileage + "\n" + "Current fuel is: " + fuel + "\n" + "The bus's current mileage is: " + currentMileage + "\n" +
                "The bus's status is: " + Status + "\n";
            return (to_return);
        }
        public bool IsQualified()
        {
            if (currentMileage > 20000)                             // The bus have passed the allowed millage
                return false;
            if ((DateTime.Now - lastMaintenance).TotalDays > 365)   // It's been a year since the last maintaenance
                return false;
            return true;
        }
        public bool IsCapable(int travel)
        {
            if (fuel - travel < 0)                 // Not enough fuel for the travel
                return false;
            if (currentMileage + travel > 20000)   // The bus will pass the allowed millage during the travel.
                return false;
            return true;
        }
        public void PerformTravel(int travel)
        {
            overallMileage += travel;
            currentMileage += travel;
            fuel -= travel;
        }
        public void PerformRefueling()
        {
            fuel = 1200;
        }
        public void PerformMaintenance()
        {
            currentMileage = 0;
            lastMaintenance = DateTime.Now;
        }
    }
}
