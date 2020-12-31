using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class DataSource
    {
        static Random r = new Random(1);

        public static List<Bus> s_buses;
        public static List<Line> s_lines;
        public static List<LineStation> s_lineStations;

        static DataSource()
        {
            InitBusList();
        }
        private static void InitBusList()
        {
            s_buses = new List<Bus>();
            for (int i = 0; i < 20; i++) 
            {
                Bus bus = new Bus();
                bus.LicenseNum = i;
                bus.FromDate = new DateTime(r.Next(1960, 2020), r.Next(1, 12), r.Next(1, 30));
                bus.TotalTrip = 0;  // Assuming all busses haven't done any travels yet
                bus.FuelRemain = 1200;  // Assuming all busses have full fuel.
                bus.Status = BusStatus.Ready;
                s_buses.Add(bus);
            }

        }
    }
}
    
