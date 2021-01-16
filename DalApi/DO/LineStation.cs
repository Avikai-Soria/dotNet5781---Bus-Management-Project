using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineStation
    {
        public Guid LineId { get; set; }
        public Guid Station { get; set; }
        public int LineStationIndex { get; set; }
        public Guid? PrevStation { get; set; }
        public Guid? NextStation { get; set; }
    }
}
