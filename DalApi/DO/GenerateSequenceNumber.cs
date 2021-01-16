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
        public static Guid GetLineId()
        {
            //return s_lineId++;
            return Guid.NewGuid();
        }
        public static Guid GetStationID()
        {
            return Guid.NewGuid();
        }
        public static int GetStationCode()
        {
            return s_stationCode++;
        }
        public static Guid GetBusOnTripId()
        {
            return Guid.NewGuid();
        }
    }
    
}
