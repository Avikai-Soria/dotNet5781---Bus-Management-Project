﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO
{
    public enum Areas { General, North, South, Center, Jerusalem, Tel_Aviv, Haifa, Beer_Sheva }

    public class Line
    {
        public int Id { get; set; }
        public int Code { get; set; }
        public Areas Area { get; set; }
        //public int FirstStation { get; set; } Probably useless
        //public int LastStation { get; set; }  Probably useless
        public List<LineStation> Stations { get; set; }
        public string Print { get; set; }

    }
}
