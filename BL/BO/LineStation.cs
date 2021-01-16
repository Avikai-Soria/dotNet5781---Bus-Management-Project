using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public Guid LineId { get; set; }
        public Guid Station { get; set; }
        public string StationName { get; set; }
        public int StationCode { get; set; }
        public int LineStationIndex { get; set; }
        public Guid? PrevStation { get; set; }
        public Guid? NextStation { get; set; }
        public double? Distance { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Print { get; set; }
    }
}
