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
            m_area = (Area)r.Next(0, 8);    //0<=value<8
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
            m_first_Station = m_stations.First();       // Taking care in case the element is a new first element
            m_last_Station = m_stations.Last();         // Taking care in case the element is a new last element
           /*( if (index==0)
            {
                m_first_Station = to_Add;
            }
            if(index==(m_stations.Count)-1)
            {
                m_last_Station = to_Add;
            }*/
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
            m_first_Station = m_stations.First();       // Taking care in case the element is a new first element
            m_last_Station = m_stations.Last();         // Taking care in case the element is a new last element
        }
        /// <summary>
        /// This function checks whether a Busline Station is located in the BusLine or not
        /// </summary>
        /// <param name="check"></param> This is the bus we're checking, whether it's in the list or not
        public bool Check(BusLine_Station check)
        {
            foreach (BusLine_Station bus in m_stations)
            {
                if (bus.BusStationKey == check.BusStationKey)
                {
                    return true;    // The Busline Station was found! 
                }
            }
            return false;           // Busline station was not found.
        }
        public int Distance(BusLine_Station first, BusLine_Station second)
        {
            if (!Check(first) || !Check(second))
                throw new ArgumentException("One of the busline stations is not located in the BusLine");
            // Need to understand how iterators works, and run from first found
            return 2;
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
