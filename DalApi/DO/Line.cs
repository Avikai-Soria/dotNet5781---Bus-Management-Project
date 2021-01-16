using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Areas { כללי, צפון, דרום, מרכז, ירושלים, תל_אביב, חיפה, באר_שבע }

    public class Line
    {
        public Guid Id { get; set; } = GenerateSequenceNumber.GetLineId();
        public int LineNumber { get; set; }
        public Areas Area { get; set; }
        public Guid FirstStation { get; set; }
        public Guid LastStation { get; set; }

    }
}
