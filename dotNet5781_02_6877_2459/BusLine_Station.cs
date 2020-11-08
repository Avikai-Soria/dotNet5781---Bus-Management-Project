using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    class BusLine_Station : Bus_Station
    {
        int m_distance;             // Will count the distance from the previous station
        int m_duration;             // Will count the minutes it takes to travel from the previous station
        public BusLine_Station(int keyg, string addressg) 
            :base(keyg, addressg) // Simple constructor
        {
            Random r = new Random();
            m_distance = r.Next(0, 31);      // Generates a random number of kilometers 
            m_duration = r.Next(0, 61);      // Generates a random number of minutes
        }

        public int Distance { get => m_distance; set => m_distance = value; }
        public int Duration { get => m_duration; set => m_duration = value; }
    }
}
