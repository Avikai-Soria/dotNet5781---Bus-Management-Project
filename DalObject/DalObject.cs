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
            if (DataSource.s_stations.FirstOrDefault(p => p.StationID == station.StationID) != null)
                throw new DO.BadStationIdException(station.StationID, "Duplicate station ID number");
            DataSource.s_stations.Add(station.Clone());
        }

        public Station GetStation(Guid id)
        {
            Station station = DataSource.s_stations.Find(p => p.StationID == id);

            if (station != null)
                return station.Clone();
            else
                throw new DO.BadStationIdException(id, $"bad station code: {id}");
        }

        public IEnumerable<Station> GetStations()
        {
            return from station in DataSource.s_stations
                   select station.Clone();
        }

        public void UpdateStation(Station station)
        {
            Station station1 = DataSource.s_stations.Find(p => p.StationID == station.StationID);
            if (station1 == null)
                throw new DO.BadStationIdException(station.StationID, "Station ID number doesn't exists");
            station1.Name = station.Name;
            station1.Code = station.Code;
            station1.Longitude = station.Longitude;
            station1.Lattitude = station.Lattitude;
        }

        public void DeleteStation(Guid id)
        {
            Station station = DataSource.s_stations.FirstOrDefault(p => p.StationID == id);
            if (station == null)
                throw new DO.BadStationIdException(id, "Station code number doesn't exist");
            DataSource.s_stations.Remove(station); // Need to change bus state later to "removed" if I have time.
        }

        #endregion

        #region Line CRUD implementations
        public void AddLine(Line lineDO)
        {
            if (DataSource.s_lines.FirstOrDefault(p => p.Id == lineDO.Id) != null)
                throw new DO.BadLineIdException(lineDO.Id, "Duplicate line ID number");
            DataSource.s_lines.Add(lineDO.Clone());
        }
        public IEnumerable<Line> GetLines()
        {
            return from line in DataSource.s_lines
                   select line.Clone();
        }
        public void UpdateLine(Line lineDO)
        {
            Line line = DataSource.s_lines.Find(p => p.Id == lineDO.Id);
            if (line == null)
                throw new DO.BadStationIdException(line.Id, "Line ID number doesn't exists");
            line.LineNumber = lineDO.LineNumber;
            line.Area = lineDO.Area;
            line.FirstStation = lineDO.FirstStation;
            line.LastStation = lineDO.LastStation;
        }
        //public IEnumerable<Line> GetLinesByStation(int code)
        //{
        //    return from line in DataSource.s_lines
        //           where line.
        //           select line.Clone();
        //}
        public void DeleteLine(Guid id)
        {
            Line line = DataSource.s_lines.FirstOrDefault(p => p.Id == id);
            if (line == null)
                throw new DO.BadStationIdException(id, "Line ID number doesn't exist");
            DataSource.s_lines.Remove(line);
        }
        #endregion

        #region LineStation CRUD implementations
        public void AddLineStation(LineStation linestation)
        {
            if (DataSource.s_lineStations.FirstOrDefault(p => p.LineId == linestation.LineId && p.Station == linestation.Station) != null)
                throw new DO.BadLineStationIdException(linestation.LineId, linestation.Station, "Duplicate combination of line ID and station ID number");
            DataSource.s_lineStations.Add(linestation.Clone());
        }
        public IEnumerable<LineStation> GetLineStations()
        {
            return from lineStations in DataSource.s_lineStations
                   select lineStations.Clone();
        }
        public void DeleteLineStation(LineStation lineStation)
        {
            LineStation lineStation1 = DataSource.s_lineStations.FirstOrDefault(p => p.LineId == lineStation.LineId && p.Station == lineStation.Station);
            if (lineStation1 == null)
                throw new DO.BadLineStationIdException(lineStation.LineId, lineStation.Station, "Combination of line ID and station ID number doesn't exist");
            DataSource.s_lineStations.Remove(lineStation1);
        }

        #endregion

        #region AdjacentStations CRUD implementations
        public void AddAdjStations(AdjacentStations adjacentStations)
        {
            if (DataSource.s_adjacentStations.FirstOrDefault(p => p.Station1 == adjacentStations.Station1 && p.Station2 == adjacentStations.Station2) != null)
                throw new DO.BadAdjStationsException(adjacentStations.Station1, adjacentStations.Station2,
                    "AdjacentStation already exists between the stations");
            DataSource.s_adjacentStations.Add(adjacentStations.Clone());
        }
        public AdjacentStations GetAdjStation(Guid? currStation, Guid? nextStation)
        {
            if (currStation == null || nextStation == null) 
            {
                return null; // There's no such adj station between no stations
            }
            AdjacentStations adjacent = (from adjstation in DataSource.s_adjacentStations
                                         where adjstation.Station1 == currStation && adjstation.Station2 == nextStation
                                         select adjstation).FirstOrDefault(); // Get the adj station between the 2 stations recived
            if (adjacent != null)
                return adjacent.Clone();
            else
                throw new BadAdjStationsException(currStation, nextStation, $"No such adj exists between: {currStation} and {nextStation}");
        }

        public IEnumerable<AdjacentStations> GetAdjStations()
        {
            return from adjacentstation in DataSource.s_adjacentStations
                   select adjacentstation.Clone();
        }

        


        #endregion

    }
}
