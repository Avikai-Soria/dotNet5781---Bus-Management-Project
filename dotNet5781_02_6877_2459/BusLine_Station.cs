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
        double m_distance;    // Will count the distance from the previous station
        int m_duration;           // Will count the minutes it takes to travel from the previous station
        public BusLine_Station(int keyg, double latg, double longig, string addressg, double disg, int durg) 
            :base(keyg, latg, longig, addressg) // Simple constructor
        {
            m_distance = disg;
            m_duration = durg;
        }

        public double Distance { get => m_distance; set => m_distance = value; }
        public int Duration { get => m_duration; set => m_duration = value; }
    }
}
