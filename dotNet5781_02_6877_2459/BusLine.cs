using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    enum Area { General,North,South,Center,Jerusalem, Tel_Aviv, Haifa, Beer_Sheva }
    class BusLine : IComparable<BusLine>
    {
        int m_busline;
        BusLine_Station m_first_Station;
        BusLine_Station m_last_Station;
        Area m_area;
        List<BusLine_Station> m_stations;
        public BusLine(int id)
        {
            Random r = new Random();
            m_busline = id;
            m_area = (Area)r.Next(0, 8);
            m_stations = new List<BusLine_Station>();
        }
        public override String ToString()                                       // Used for printing values of busline
        {
            String to_return = "Busline number: " + m_busline + '\n' + "Area: " + m_area + 'n' + "Stations in this line: " + '\n';
            foreach (BusLine_Station check in m_stations)
                to_return += check.ToString();
                return (to_return);
        }
        /// <summary>
        /// This function can add a new Busline Station to the busline
        /// </summary>
        /// <param name="to_Add"> This is the new BusLine Station
        /// </param>
        /// <param name="where"></param> This will decide where is it added to.
        public void Add(BusLine_Station to_Add, int index)
        {
            m_stations.Insert(index, to_Add);
            if (index==0)
            {
                m_first_Station = to_Add;
            }
            if(index==(m_stations.Count)-1)
            {
                m_last_Station = to_Add;
            }
        }
        /// <summary>
        /// This function removes a busline station from busline
        /// </summary>
        /// <param name="to_Remove"></param>
        public void Remove(BusLine_Station to_Remove)
        {
            foreach (BusLine_Station check in m_stations)
            {
                if (to_Remove.BusStationKey == check.BusStationKey)
                {
                    m_stations.Remove(check);
                    break;
                }
            }
        }
        public void Check(BusLine_Station check)
        {

        }
        public double Distance(BusLine_Station first, BusLine_Station second)
        {
            double distance = 2; // To make later
            return distance;
        }
        public int Duration(BusLine_Station first, BusLine_Station second)
        {
            int duration = 2; // To make later
            return duration;
        }
       /* public BusLine subLine(BusLine_Station first, BusLine_Station second)
        {
            BusLine sub;    // to make later
            return sub;
        }*/
        public int CompareTo(BusLine that)
        {
            // Wanna return positive number if current bus is better
            // return 0 in case they are basically the same
            // return negative number in case other BusLine is more efficient
            return 1;
        }
    }

}
