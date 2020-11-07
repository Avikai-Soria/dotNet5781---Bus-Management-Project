using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace dotNet5781_02_6877_2459
{
    enum Area { General,North,South,Center,Jerusalem, Tel_Aviv, Haifa, Beer_Sheva }
    class BusLine
    {
        int m_busline;
        BusLine_Station m_first_Station;
        BusLine_Station m_last_Station;
        Area m_area;
        List<BusLine_Station> m_stations;
    }
}
