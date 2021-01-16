using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class Station
    {
        public Guid StationID { get; set; }
        public int Code { get; set; }
        public string Name { get; set; }
        public double Longitude { get; set; }
        public double Lattitude { get; set; }
        public List<Line> Lines { get; set; }   // This are all the lines that go through this bus
        public string Print { get; set; }

    }
}
