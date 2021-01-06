using DalApi;
using DO;
using DS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal
{
    sealed class DalObject : IDAL
    {
        #region singelton
        static readonly DalObject instance = new DalObject();
        static DalObject() { }// static ctor to ensure instance init is done just before first usage
        DalObject() { } // default => private
        public static DalObject Instance { get => instance; }// The public Instance property to use
        #endregion

        //Implement IDL methods, CRUD
        #region Bus CRUD implementations
        public void AddBus(Bus bus)
        {
            if (DataSource.s_buses.FirstOrDefault(p => p.LicenseNum == bus.LicenseNum) != null)
                throw new DO.BadBusIdException(bus.LicenseNum, "Duplicate bus License Number");
            DataSource.s_buses.Add(bus.Clone());
        }
        public Bus GetBus(int licensenum)
        {
            Bus bus = DataSource.s_buses.Find(p => p.LicenseNum == licensenum);

            if (bus != null)
                return bus.Clone();
            else
                throw new DO.BadBusIdException(licensenum, $"bad bus id: {licensenum}");
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from bus in DataSource.s_buses
                   select bus.Clone();
        }

        public void UpdateBus(Bus bus)
        {
            throw new NotImplementedException();
        }
        public void DeleteBus(int licensenum)
        {
            Bus bus = DataSource.s_buses.FirstOrDefault(p => p.LicenseNum == licensenum);
            if (bus == null)
                throw new DO.BadBusIdException(licensenum, "Bus License Number doesn't exist");
            DataSource.s_buses.Remove(bus); // Need to change bus state later to "removed" if I have time.
        }
        #endregion
        #region Station CRUD implementations
        public void AddStation(Station station)
        {
            throw new NotImplementedException();
        }

        public Station GetStation(int code)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Station> GetStations()
        {
            return from station in DataSource.s_stations
                   select station.Clone();
        }

        public void UpdateStation(Station station)
        {
            throw new NotImplementedException();
        }

        public void DeleteStation(int code)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}
