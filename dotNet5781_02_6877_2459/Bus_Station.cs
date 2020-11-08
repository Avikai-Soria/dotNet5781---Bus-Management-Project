using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    class Bus_Station
    {
        int m_busStationKey; // must be value between 0 to 999999
        double m_latitude;    // must be value between -90 to 90
        double m_longitude;   // must be value between -180 to 180
        string m_address;    // Optional

        public int BusStationKey // Making sure range is all right
        {
            get => m_busStationKey;
            set
            {
                if (value < 0 || value > 999999)
                {
                    throw new ArgumentOutOfRangeException("Bus station key may contain a maximun of 6 digits");
                }
                m_busStationKey = value;
            }
        }
        public double Latitude // // Making sure range is all right
        {
            get => m_latitude;
            set
            {
                if (value < -90 || value > 90)
                {
                    throw new ArgumentOutOfRangeException("Latitude value must be between -90 and 90");
                }
                m_latitude = value;
            }
        }
        public double Longitude // // Making sure range is all right
        {
            get => m_longitude;
            set
            {
                if (value < -180 || value > 180)
                {
                    throw new ArgumentOutOfRangeException("Longitude value must be between -180 and 180");
                }
                m_longitude = value;
            }
        }
        public Bus_Station(int keyg, string m_addressg)  // Simple constructor
        {
            Random r = new Random(DateTime.Now.Millisecond);
            if (keyg < 0 || keyg > 999999)
            {
                throw new ArgumentOutOfRangeException("Bus station key may contain a maximun of 6 digits");
            }
            /*if (latg < -90 || latg > 90)
            {
                throw new ArgumentOutOfRangeException("Latitude value must be between -90 and 90");
            }
            if (longig < -180 || longig > 180)
            {
                throw new ArgumentOutOfRangeException("Longitude value must be between -180 and 180");
            }
            */
            m_busStationKey = keyg;
            m_latitude = r.Next(31,34)+r.NextDouble();
            m_longitude = r.Next(34, 36) + r.NextDouble();
            m_address = m_addressg;
        }
        public override String ToString()                                       // Used for printing values of bus station
        {
            String to_return = "Bus Station Code: " + m_busStationKey + ",  " + m_latitude + "°N "+ m_longitude + "°E" + '\n';
            return (to_return);
        }
    }
    
    
}
