﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DO
{
    public enum BusStatus
    {
        Ready,
        Busy,
        Refueling,
        Maintained
    }
    public class Bus
    {
        public string LicenseNum { get; set; }
        public DateTime FromDate { get; set; }
        public double TotalTrip { get; set; }
        public double FuelRemain { get; set; }
        public BusStatus Status { get; set; }
    }
}
