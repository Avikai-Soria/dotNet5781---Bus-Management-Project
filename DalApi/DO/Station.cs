using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class Station
    {
        public Guid StationID { get; set; } = GenerateSequenceNumber.GetStationID();
        public int Code { get; set; } = GenerateSequenceNumber.GetStationCode();
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
    }
}
