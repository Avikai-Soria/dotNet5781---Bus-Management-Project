using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public class LineTrip
    {
        //  public int Id { get; set; } Don't need this anymore
        public Guid LineId { get; set; }
        public TimeSpan StartAt { get; set; }
        //public TimeSpan Frequency { get; set; }   Same
        //public TimeSpan FinishAt { get; set; }    Same
    }
}
