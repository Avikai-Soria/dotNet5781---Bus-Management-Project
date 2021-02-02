using DalApi;
using DO;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Dal
{
    public class DalXml : IDAL
    {
        #region singelton
        static readonly DalXml instance = new DalXml();
        static DalXml() { }// static ctor to ensure instance init is done just before first usage
        DalXml() { } // default => private
        public static DalXml Instance { get => instance; }// The public Instance property to use
        #endregion

        #region DS XML Files
        readonly string busesPath = @"BusesXml.xml"; //XMLSerializer
        readonly string stationsPath = @"StationsXml.xml"; //XMLSerializer
        readonly string linesPath = @"LinesXml.xml"; //XMLSerializer
        readonly string lineStationsPath = @"LineStationsXml.xml"; //XMLSerializer

        readonly string adjacentsPath = @"AdjacentsXml.xml"; //XElement
        readonly string lineTripsPath = @"LineTripsXml.xml"; //XElement
        #endregion
        //Implement IDL methods, CRUD
        #region Bus
        public void AddBus(Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            if (ListBuses.FirstOrDefault(s => s.LicenseNum == bus.LicenseNum) != null)
                throw new DO.BadBusIdException(bus.LicenseNum, "Duplicate bus License Number");


            ListBuses.Add(bus); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }

        public Bus GetBus(string licensenum)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus bus = ListBuses.Find(c => c.LicenseNum == licensenum);
            if (bus != null)
                return bus; // No need to clone()
            else
                throw new DO.BadBusIdException(licensenum, $"bad bus id: {licensenum}");
        }

        public IEnumerable<Bus> GetBuses()
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            return (from bus in ListBuses
                    select bus);
        }

        public void UpdateBus(Bus bus)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus bus1 = ListBuses.Find(c => c.LicenseNum == bus.LicenseNum);
            if(bus1!= null)
            {
                ListBuses.Remove(bus1);
                ListBuses.Add(bus);
            }
            else
            {
                throw new DO.BadBusIdException(bus.LicenseNum, $"bad bus id: {bus.LicenseNum}");
            }


            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }

        public void DeleteBus(string licensenum)
        {
            List<Bus> ListBuses = XMLTools.LoadListFromXMLSerializer<Bus>(busesPath);

            Bus bus = ListBuses.Find(c => c.LicenseNum == licensenum);

            if (bus != null)
            {
                ListBuses.Remove(bus);
            }
            else
                throw new DO.BadBusIdException(bus.LicenseNum, $"bad bus id: {bus.LicenseNum}");


            XMLTools.SaveListToXMLSerializer(ListBuses, busesPath);
        }
        #endregion
        #region Station
        public void AddStation(Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            if (ListStations.FirstOrDefault(s => s.StationID == station.StationID) != null)
                throw new DO.BadStationIdException(station.StationID, "Duplicate station ID number");

            ListStations.Add(station); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }

        public Station GetStation(Guid code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station station = ListStations.Find(s => s.StationID == code);
            if (station != null)
                return station; // No need to clone()
            else
                throw new DO.BadStationIdException(code, $"bad station code: {code}");
        }

        public IEnumerable<Station> GetStations()
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            return (from station in ListStations
                    select station);
        }

        public void UpdateStation(Station station)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station station1 = ListStations.Find(c => c.StationID == station.StationID);
            if (station1 != null)
            {
                ListStations.Remove(station1);
                ListStations.Add(station);
            }
            else
            {
                throw new DO.BadStationIdException(station.StationID, "Station ID number doesn't exists");
            }


            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }

        public void DeleteStation(Guid code)
        {
            List<Station> ListStations = XMLTools.LoadListFromXMLSerializer<Station>(stationsPath);

            Station station = ListStations.Find(c => c.StationID == code);

            if (station != null)
            {
                ListStations.Remove(station);
            }
            else
                throw new DO.BadStationIdException(code, "Station code number doesn't exist");


            XMLTools.SaveListToXMLSerializer(ListStations, stationsPath);
        }
        #endregion
        #region Line
        public void AddLine(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);

            if (ListLines.FirstOrDefault(s => s.Id == line.Id) != null)
                throw new DO.BadLineIdException(line.Id, "Duplicate line ID number");

            ListLines.Add(line); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }

        public IEnumerable<Line> GetLines()
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);

            return (from line in ListLines
                    select line);
        }

        public void UpdateLine(Line line)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);

            Line line1 = ListLines.Find(c => c.Id == line.Id);
            if (line1 != null)
            {
                ListLines.Remove(line1);
                ListLines.Add(line);
            }
            else
            {
                throw new DO.BadStationIdException(line.Id, "Line ID number doesn't exists");
            }


            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }

        public void DeleteLine(Guid id)
        {
            List<Line> ListLines = XMLTools.LoadListFromXMLSerializer<Line>(linesPath);

            Line line = ListLines.Find(c => c.Id == id);

            if (line != null)
            {
                ListLines.Remove(line);
            }
            else
                throw new DO.BadStationIdException(id, "Line ID number doesn't exist");


            XMLTools.SaveListToXMLSerializer(ListLines, linesPath);
        }
        #endregion
        #region LineStation
        public void AddLineStation(LineStation linestation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);

            if (ListLineStations.FirstOrDefault(s => s.LineId == linestation.LineId && s.Station == linestation.Station) != null)
                throw new DO.BadLineStationIdException(linestation.LineId, linestation.Station, "Duplicate combination of line ID and station ID number");

            ListLineStations.Add(linestation); //no need to Clone()

            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }

        public IEnumerable<LineStation> GetLineStations()
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);

            return (from linestation in ListLineStations
                    select linestation);
        }

        public void DeleteLineStation(LineStation lineStation)
        {
            List<LineStation> ListLineStations = XMLTools.LoadListFromXMLSerializer<LineStation>(lineStationsPath);

            LineStation lineStation1 = ListLineStations.Find(s => s.LineId == lineStation.LineId && s.Station == lineStation.Station);

            if (lineStation1 != null)
            {
                ListLineStations.Remove(lineStation1);
            }
            else
                throw new DO.BadLineStationIdException(lineStation.LineId, lineStation.Station, "Combination of line ID and station ID number doesn't exist");


            XMLTools.SaveListToXMLSerializer(ListLineStations, lineStationsPath);
        }
        #endregion
        #region AdjacentStations
        public void AddAdjStations(AdjacentStations adjacentStations)
        {
            XElement adjstationsRootElem = XMLTools.LoadListFromXMLElement(adjacentsPath, "ArrayOfAdjacentStations");

            XElement adj1 = (from a in adjstationsRootElem.Elements()
                             where (Guid.Parse(a.Element("Station1").Value) == adjacentStations.Station1) &&
                             (Guid.Parse(a.Element("Station2").Value) == adjacentStations.Station2)
                             select a).FirstOrDefault();

            if (adj1 != null)
                throw new DO.BadAdjStationsException(adjacentStations.Station1, adjacentStations.Station2,
                "AdjacentStation already exists between the stations");

            XElement adjElem =  new XElement("AdjacentStations", 
                                new XElement("Station1", adjacentStations.Station1),
                                new XElement("Station2", adjacentStations.Station2),
                                new XElement("Distance", adjacentStations.Distance),
                                new XElement("Time", adjacentStations.Time.ToString()));

            adjstationsRootElem.Add(adjElem);

            XMLTools.SaveListToXMLElement(adjstationsRootElem, adjacentsPath);
        }

        public AdjacentStations GetAdjStation(Guid? currStation, Guid? nextStation)
        {
            if (currStation == null || nextStation == null)
            {
                return null; // There's no such adj station between no stations
            }

            XElement adjstationsRootElem = XMLTools.LoadListFromXMLElement(adjacentsPath, "ArrayOfAdjacentStations");

            AdjacentStations adjacent = (from adj in adjstationsRootElem.Elements()
                        where (Guid.Parse(adj.Element("Station1").Value) == currStation) &&
                             (Guid.Parse(adj.Element("Station2").Value) == nextStation)
                        select new AdjacentStations()
                        {
                            Station1 = Guid.Parse(adj.Element("Station1").Value),
                            Station2 = Guid.Parse(adj.Element("Station2").Value),
                            Distance = double.Parse(adj.Element("Distance").Value),
                            Time = TimeSpan.ParseExact(adj.Element("Time").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                        }
                        ).FirstOrDefault();

            if (adjacent == null)
                throw new BadAdjStationsException(currStation, nextStation, $"No such adj exists between: {currStation} and {nextStation}");


            return adjacent;
        }

        public IEnumerable<AdjacentStations> GetAdjStations()
        {
            XElement adjstationsRootElem = XMLTools.LoadListFromXMLElement(adjacentsPath, "ArrayOfAdjacentStations");


            return (from adj in adjstationsRootElem.Elements()
                    select new AdjacentStations()
                    {
                        Station1 = Guid.Parse(adj.Element("Station1").Value),
                        Station2 = Guid.Parse(adj.Element("Station2").Value),
                        Distance = double.Parse(adj.Element("Distance").Value),
                        Time = TimeSpan.ParseExact(adj.Element("Time").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                    }
                    );
        }

        public void UpdateAdjacentStations(AdjacentStations adjStations)
        {
            XElement adjstationsRootElem = XMLTools.LoadListFromXMLElement(adjacentsPath, "ArrayOfAdjacentStations");

            XElement adj1 = (from a in adjstationsRootElem.Elements()
                             where (Guid.Parse(a.Element("Station1").Value) == adjStations.Station1) &&
                             (Guid.Parse(a.Element("Station2").Value) == adjStations.Station2)
                             select a).FirstOrDefault();

            if (adj1 != null)
            {
                adj1.Element("Station1").Value = adjStations.Station1.ToString();
                adj1.Element("Station2").Value = adjStations.Station2.ToString();
                adj1.Element("Distance").Value = adjStations.Distance.ToString();
                adj1.Element("Time").Value = adjStations.Time.ToString();


                XMLTools.SaveListToXMLElement(adjstationsRootElem, adjacentsPath);
            }
            else
            {
                throw new BadAdjStationsException(adjStations.Station1, adjStations.Station2,
                    $"No such adj exists between: {adjStations.Station1} and {adjStations.Station2}");
            }
        }
        #endregion
        #region LineTrip
        public void AddLineTrip(LineTrip lineTrip)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath, "ArrayOfLineTrip");

            XElement lineTripElem = new XElement("LineTrip",
                                new XElement("LineId", lineTrip.LineId),
                                new XElement("StartAt", lineTrip.StartAt.ToString()));

            lineTripRootElem.Add(lineTripElem);

            XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripsPath);
        }

        public IEnumerable<LineTrip> GetLineTrips()
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath, "ArrayOfLineTrip");


            return (from lineTrip in lineTripRootElem.Elements()
                    select new LineTrip()
                    {
                        LineId = Guid.Parse(lineTrip.Element("LineId").Value),
                        StartAt = TimeSpan.ParseExact(lineTrip.Element("StartAt").Value, "hh\\:mm\\:ss", CultureInfo.InvariantCulture)
                    }
                    );
        }

        public void DeleteLineTrip(Guid code)
        {
            XElement lineTripRootElem = XMLTools.LoadListFromXMLElement(lineTripsPath, "ArrayOfLineTrip");

            XElement lineTrip = (from lt in lineTripRootElem.Elements()
                            where Guid.Parse(lt.Element("LineId").Value) == code
                            select lt).FirstOrDefault();
            while (lineTrip != null) 
            {
                lineTrip.Remove();

                XMLTools.SaveListToXMLElement(lineTripRootElem, lineTripsPath);

                lineTrip = (from lt in lineTripRootElem.Elements()
                            where Guid.Parse(lt.Element("LineId").Value) == code
                            select lt).FirstOrDefault();
            }
        }
        #endregion

    }
}
