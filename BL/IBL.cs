﻿using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public interface IBL
    {
        IEnumerable<Bus> GetBuses();
        IEnumerable<Station> GetStations();
        IEnumerable<Line> GetLines();
    }
}
