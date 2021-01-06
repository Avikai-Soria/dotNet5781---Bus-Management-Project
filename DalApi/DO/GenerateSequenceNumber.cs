using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    internal static class GenerateSequenceNumber
    {
        private static int s_lineId = 1;
        private static int s_stationCode = 1;
        private static int s_busOnTripId = 1;
        public static int GetLineId()
        {
            return s_lineId++;
        }
        public static int GetStationCode()
        {
            return s_stationCode++;
        }
        public static int GetBusOnTripId()
        {
            return s_busOnTripId++;
        }
    }
    
}
