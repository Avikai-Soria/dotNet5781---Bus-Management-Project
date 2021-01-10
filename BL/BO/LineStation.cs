using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public class LineStation
    {
        public int LineId { get; set; }
        public int Station { get; set; }
        public string StationName { get; set; }
        public int LineStationIndex { get; set; }
        public int? PrevStation { get; set; }
        public int? NextStation { get; set; }
        public double? Distance { get; set; }
        public TimeSpan? Duration { get; set; }
        public string Print { get; set; }
    }
}
