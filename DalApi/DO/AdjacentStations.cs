using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class AdjacentStations
    {
        public Guid Station1 { get; set; }
        public Guid Station2 { get; set; }
        public double Distance { get; set; }
        public TimeSpan Time { get; set; }
    }
}
