using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    class BusLine_Collection :  IEnumerable
    {
        List<BusLine> m_list;
        /// <summary>
        /// Simple constructor, creats a new list
        /// </summary>
        public BusLine_Collection()
        {
            m_list = new List<BusLine>();
        }
        public override string ToString()
        {
            String to_return="The buslines in this collection are: \n";
            foreach (BusLine bus in m_list)
                to_return += bus.ToString();
            return to_return;
        }
        /// <summary>
        /// Implenting Inumerator
        /// </summary>
        /// <returns></returns>
        public IEnumerator GetEnumerator()
        {
            return m_list.GetEnumerator();
        }
        /// <summary>
        /// This function checks whether a value is located once or twice already in the list or not, then add it if it's valid
        /// </summary>
        /// <param name="to_Add"></param>   A new BusLine to add
        public void Add(BusLine to_Add)
        {
            int count = 0;  // Will count how many times the Busline ID exists in the list
            BusLine dup=null;// = m_list.First();    // This will serve me only in case count=1
            foreach (BusLine check in m_list)
            {
                if (check.BusLine_Id == to_Add.BusLine_Id)
                {
                    count++;
                    dup = check;
                }
            }
            switch (count)
            {
                case 0:
                    m_list.Add(to_Add);
                    break;
                case 1:
                    if (dup.Is_Reversed(to_Add))
                    {
                        m_list.Add(to_Add);
                    }
                    else
                    {
                        throw new ArgumentException("This BusLine number already exists in the list");
                    }
                    break;
                case 2:
                    throw new ArgumentException("This BusLine number already exists in the list");
                default:
                    Console.WriteLine("Congrats, you found an error that wasn't supposed to happen!");
                    break;
            }
        }
        /// <summary>
        /// This functions removes a BusLine from the list
        /// </summary>
        /// <param name="to_Remove"></param>    The BusLine we're about to remove
        public void Remove(BusLine to_Remove)
        {
            m_list.Remove(to_Remove);
            // I'm leaving this green for now in case we actually need to remove ALL the BusLines with same ID
            /*BusLine remove = null;
            foreach (BusLine check in m_list)
            {
                if (check.BusLine_Id == to_Remove.BusLine_Id)
                {
                    remove=check;
                    break;
                }
            }
            if (remove!=null)
               m_list.Remove(to_Remove);   // Removed the first BusLine
            remove = null;
            foreach (BusLine check in m_list)   // Doing the whole proccess again, in case there are 2 buslines with the same ID
            {
                if (check.BusLine_Id == to_Remove.BusLine_Id)
                {
                    remove = check;
                    break;
                }
            }
            if (remove != null)
                m_list.Remove(to_Remove);   // Removed the second BusLine
            */
        }
        /// <summary>
        /// This functions receves a station id and returns a list that includes all the BusLines that include this station.
        /// </summary>
        /// <param name="station_id"></param> The id number of the station we're searching for
        /// <returns></returns>                 Returns a list that includes all the BusLines we searched for
        public List<BusLine> GetLineByStation(int station_id)
        {
            List<BusLine> to_return = new List<BusLine>();
            foreach (BusLine check in m_list)
            {
                if (check.Check_Id(station_id))
                    to_return.Add(check);
            }
            return to_return;
        }
        /// <summary>
        /// A function that returns the same list, but sorted from fastest to slowest
        /// </summary>
        /// <returns></returns> The sorted list
        public List<BusLine> SortCollection()
        {
            List<BusLine> to_return = new List<BusLine>(m_list);    // Don't want to destroy the current list
            to_return.Sort();                                       // Sorting them from least efficient (longest) to most (shortest)
            to_return.Reverse();                                    // Reverse because we wanna return from shortest to longest
            return to_return;
        }
        /// <summary>
        /// Indexer implemention
        /// </summary>
        /// <param name="i"></param> The index
        /// <returns></returns>      The busline in the current index
        public BusLine this[int i]
        {
            get => m_list[i];
            set => m_list[i] = value;
        }
    }
}
