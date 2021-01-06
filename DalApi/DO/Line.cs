﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum Areas { General, North, South, Center, Jerusalem, Tel_Aviv, Haifa, Beer_Sheva }

    public class Line
    {
        public int Id { get; set; } = GenerateSequenceNumber.GetLineId();
        public int Code { get; set; }
        public Areas Area { get; set; }
        public int FirstStation { get; set; }
        public int LastStation { get; set; }
    }
}