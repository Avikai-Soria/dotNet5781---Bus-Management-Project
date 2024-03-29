﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_03A_6877_2459
{
    enum Area_C { General,North,South,Center,Jerusalem, Tel_Aviv, Haifa, Beer_Sheva }
    class BusLine : IComparable<BusLine>
    {
        int m_busLine_Id;
        BusLine_Station m_first_Station;
        BusLine_Station m_last_Station;
        Area_C m_area;
        List<BusLine_Station> m_stations;
        static Random r = new Random(DateTime.Now.Millisecond);


        public int BusLine_Id { get => m_busLine_Id; set => m_busLine_Id = value; }
        internal List<BusLine_Station> Stations { get => m_stations; set => m_stations = value; }
        internal Area_C Area { get => m_area; set => m_area = value; }

        /// <summary>
        /// Normal constructor, recives id only
        /// </summary>
        /// <param name="id"></param> The id of the bus
        public BusLine(int id)
        {
            BusLine_Id = id;
            Area = (Area_C)r.Next(0, 8);    //0<=value<8
            Stations = new List<BusLine_Station>();
        }
        public BusLine(int id, BusLine_Station first, BusLine_Station last)
        {
            BusLine_Id = id;
            Area = (Area_C)r.Next(0, 8);    //0<=value<8
            Stations = new List<BusLine_Station>();
            Stations.Add(first);
            Stations.Add(last);
            m_first_Station = Stations.First();
            m_last_Station = Stations.Last();
        }
        /// <summary>
        /// A very specific constructor for subline
        /// </summary>
        /// <param name="list"></param> The sub list
        public BusLine(List<BusLine_Station> list, Area_C area)
        {
            BusLine_Id = 0; // We didn't really recive any demands for id so this will do
            Stations = list;
            m_first_Station = list.First();
            m_last_Station = list.Last();
            Area = area;
        }
        /// <summary>
        /// Implementing IComparable<BusLine>
        /// </summary>
        /// <param name="that"></param> The bus that is compared against
        /// <returns></returns>
        public int CompareTo(BusLine that)
        {
            // Wanna return positive number if current bus is better, a.k.a shorter
            // return 0 in case they are basically the same
            // return negative number in case other BusLine is more efficient
            return (that.Duration(that.m_first_Station, that.m_last_Station) - Duration(m_first_Station, m_last_Station));    // Proud of myself for this one
        }
        public override String ToString()                                       // Used for printing values of busline
        {
            String to_return = "Busline number: " + BusLine_Id + '\n' + "Area: " + Area + '\n' + "Stations in this line: " + '\n';
            foreach (BusLine_Station check in Stations)
                to_return += check.ToString();
                return (to_return);
        }
        public int Count()
        {
            return Stations.Count;
        }
        /// <summary>
        /// This function can add a new Busline Station to the busline
        /// </summary>
        /// <param name="to_Add"> This is the new BusLine Station
        /// </param>
        /// <param name="where"></param> This will decide where is it added to.
        public void Add(BusLine_Station to_Add, int index)
        {
            Stations.Insert(index, to_Add);
            m_first_Station = Stations.First();       // Taking care in case the element is a new first element
            m_last_Station = Stations.Last();         // Taking care in case the element is a new last element
        }
        /// <summary>
        /// This function removes a busline station from busline
        /// </summary>
        /// <param name="to_Remove"></param>
        public void Remove(BusLine_Station to_Remove)
        {
            foreach (BusLine_Station check in Stations)
            {
                if (to_Remove.BusStationKey == check.BusStationKey)
                {
                    Stations.Remove(check);
                    break;
                }
            }
            if (Stations.Count < 2)
                throw new ArgumentOutOfRangeException("A bus must have at least 2 stations!");
            m_first_Station = Stations.First();       // Taking care in case the element is a new first element
            m_last_Station = Stations.Last();         // Taking care in case the element is a new last element
        }
        /// <summary>
        /// This function checks whether a Busline Station is located in the BusLine or not
        /// </summary>
        /// <param name="check"></param> This is the bus we're checking, whether it's in the list or not
        public bool Check(BusLine_Station check)
        {
            foreach (BusLine_Station bus in Stations)
            {
                if (bus.BusStationKey == check.BusStationKey)
                {
                    return true;    // The Busline Station was found! 
                }
            }
            return false;           // Busline station was not found.
        }
        public bool Check_Id(int check)
        {
            foreach (BusLine_Station bus in Stations)
            {
                if (bus.BusStationKey == check)
                {
                    return true;    // The Busline Station was found! 
                }
            }
            return false;           // Busline station was not found.
        }
        /// <summary>
        /// This function return the distance between 2 stations on this line
        /// </summary>
        /// <param name="first"></param>    First station
        /// <param name="second"></param>   Second station
        /// <returns>
        /// The distance between both of the stations in kilometers
        /// </returns>
        public int Distance(BusLine_Station first, BusLine_Station second)
        {
            if (!Check(first) || !Check(second))
                throw new ArgumentException("One of the busline stations is not located in the BusLine");
            int distance=0;
            IEnumerator<BusLine_Station> a =Stations.GetEnumerator();
            while (a.Current != first && a.Current != second)
                a.MoveNext();   // Will try to find the first element
            a.MoveNext();       // I want to start calculating the distance from the next station
            while(a.Current != first && a.Current != second)
            {
                distance += a.Current.Distance;     // Add distance of mid-station
                a.MoveNext();                       // Move onto the next station
            }
            distance += a.Current.Distance;         // Add distance of the last station
            return distance;
        }
        /// <summary>
        /// This function return the duration between 2 stations on this line
        /// </summary>
        /// <param name="first"></param>    First station
        /// <param name="second"></param>   Second station
        /// <returns>
        /// The duration between both of the stations in minutes
        /// </returns>
        public int Duration(BusLine_Station first, BusLine_Station second)
        {
            if (!Check(first) || !Check(second))
                throw new ArgumentException("One of the busline stations is not located in the BusLine");
            int duration = 0;
            IEnumerator<BusLine_Station> a = Stations.GetEnumerator();
            a.MoveNext();
            while (a.Current != first && a.Current != second)
                a.MoveNext();   // Will try to find the first element
            a.MoveNext();       // I want to start calculating the duration from the next station
            while (a.Current != first && a.Current != second)
            {
                duration += a.Current.Duration;     // Add duration of mid-station
                a.MoveNext();                       // Move onto the next station
            }
            duration += a.Current.Duration;         // Add duration of the last station
            return duration;
        }
        /// <summary>
        /// Returns overall duration of the busline
        /// </summary>
        public int Overall_Duration()
        {
            return Duration(m_first_Station, m_last_Station);
        }
        /// <summary>
        /// This functions returns a bus that contains a subline of the current line
        /// </summary>
        /// <param name="first"></param>    The first station 
        /// <param name="second"></param>   The second station
        /// <returns></returns>             A bus that contains a subline of the current line
         public BusLine subLine(BusLine_Station first, BusLine_Station second)
         {
            if (!Check(first) || !Check(second))
                throw new ArgumentException("One of the busline stations is not located in the BusLine");
            int first_index = Stations.FindIndex(t => t.BusStationKey == first.BusStationKey);    // This is the index of first
            int second_index = Stations.FindIndex(t => t.BusStationKey == second.BusStationKey);  // This is the index of second
            if(first_index>second_index)                                                            // I want the first index to be smaller than the second
            {
                int temp = first_index;
                first_index = second_index;
                second_index = temp;
            }
            List<BusLine_Station> sublist = new List<BusLine_Station>(Stations.GetRange(first_index, second_index-first_index+1));
            BusLine subBus = new BusLine(sublist, Area);    // to make later
            return subBus;
        }
        /// <summary>
        /// Checking if 2 Busliens have opposite start and end stations
        /// </summary>
        /// <param name="check"></param> A bus to compare against
        /// <returns></returns>         True if they're reversed, false otherwise
        public bool Is_Reversed(BusLine check)
        {
            if (Stations.Count == 0 || check.Stations.Count == 0)
                return false;
            return (m_first_Station == check.m_last_Station && m_last_Station == check.m_first_Station);
        }
    }

}
