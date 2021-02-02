using BLApi;
using BO;
using DalApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    sealed internal class BLImp : IBL
    {
        IDAL dl = DLFactory.GetDL();
        private static BLImp instance = null;
        #region Sinelton
        public static BLImp Instance
        {
            get
            {
                if (instance == null)
                    instance = new BLImp();
                return instance;
            }
        }
        #endregion
        //CRUD Logic:
        // Create - add new instance
        // Request - ask for an instance or for a collection
        // Update - update properties of an instance
        // Delete - delete an instance

        #region Bus
        Bus busDoBoAdapter(DO.Bus busDO)
        {
            Bus busBO = new Bus();
            busDO.CopyPropertiesTo(busBO);
            busBO.Print = "License Number: " + busBO.LicenseNum + "\nCreation date: " + busBO.FromDate + "\nTotal milage: " + busBO.TotalTrip
                + "\nFuel remained: " + busBO.FuelRemain + "\nBus's status: " + busBO.Status;
            return busBO;
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from busDO in dl.GetBuses()
                   select busDoBoAdapter(busDO);
        }
        #endregion

        #region Station
        public void AddStation(Station stationBO)
        {
            DO.Station stationDO = new DO.Station();
            Guid guid = stationDO.StationID;    // The next function will kill the guid, so I'm storing it.
            stationBO.CopyPropertiesTo(stationDO);  
            stationDO.StationID = guid;
            try
            {
                dl.AddStation(stationDO);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.code, ex.text);
            }
        }
        public IEnumerable<Station> GetStations()
        {
            return from stationDO in dl.GetStations()
                   select stationDoBoAdapter(stationDO);
        }

        public void UpdateStation(Station stationBO)
        {
            DO.Station stationDO = new DO.Station();
            stationBO.CopyPropertiesTo(stationDO);
            try
            {
                dl.UpdateStation(stationDO);
            }
            catch (DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.code, ex.text);
            }
        }
        public void DeleteStation(Station station)
        {
            foreach(Line line in station.Lines)
            {
                List<Station> stations = LineStationsToStations(line.Stations);
                Station to_remove = stations.FirstOrDefault(p => p.StationID == station.StationID);
                if (to_remove == null)
                    throw new BadStationIdException(station.StationID, "Station code number doesn't exist");
                stations.Remove(to_remove);
                UpdateLine(line, stations);
            }
            try
            {
                dl.DeleteStation(station.StationID); 
            }
            catch(DO.BadStationIdException ex)
            {
                throw new BO.BadStationIdException(ex.code, ex.text);
            }
        }

        Station stationDoBoAdapter(DO.Station stationDO)
        {
            Station stationBO = new Station();
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.Lines = GetLinesByStation(stationBO.StationID);
            stationBO.Print = "Code: " + stationBO.Code + "\nName: " + stationBO.Name + "\nLongitude: " + stationBO.Longitude
                + "\nLattitude: " + stationBO.Lattitude + "\nLines passing by: ";
            foreach (Line line in stationBO.Lines)
                stationBO.Print += line.LineNumber + " ";
            return stationBO;
        }
        #endregion

        #region Line
        /// <summary>
        /// First creating a simple DO line, then creating all the needed linestation objects and then creating adjstation for each couple
        /// </summary>
        /// <param name="lineBO"> A simple lineBO, with only code and area</param>
        /// <param name="stations">A list of stations that the line is going thorugh</param>
        public void AddLine(Line lineBO, List<Station> stations)
        {
            lineBO.FirstStation = stations.First().StationID;
            lineBO.LastStation = stations.Last().StationID;
            DO.Line lineDO = new DO.Line(); // Gets a new ID
            Guid guid = lineDO.Id;
            lineBO.CopyPropertiesTo(lineDO);
            lineDO.Id = lineBO.Id = guid;   // lineDO and lineBO now both have valid ID
            try
            {
                dl.AddLine(lineDO);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.id, ex.v);
            }
            List<DO.LineStation> lineStationsDO = GenerateLineStations(lineBO, stations);
            try
            {
                foreach (DO.LineStation linestation in lineStationsDO)
                {
                    dl.AddLineStation(linestation);
                }
            }
            catch (DO.BadLineStationIdException ex) 
            {
                throw new BO.BadLineStationIdException(ex.lineId, ex.stationId, ex.v);
            }
            List<DO.AdjacentStations> adjacentStationsDO = GenerateAdjStations(stations);
            try
            {
                foreach (DO.AdjacentStations adjacentstation in adjacentStationsDO)
                {
                    dl.AddAdjStations(adjacentstation);
                }
            }
            catch(DO.BadAdjStationsException ex)
            {
                throw new BO.BadAdjStationsException(ex.currStation, ex.nextStation, ex.v);
            }
            List<DO.LineTrip> lineTripsDO = GenerateLineTrips(lineBO.Id);
            try
            {
                foreach (DO.LineTrip lineTrip in lineTripsDO)
                {
                    dl.AddLineTrip(lineTrip);
                }
            }
            catch (DO.BadLineTripIdException ex)
            {
                throw new BO.BadLineTripIdException(ex.lineId, ex.v);
            }

        }

        public IEnumerable<Line> GetLines()
        {
            return from lineDO in dl.GetLines()
                   select lineDoBoAdapter(lineDO);
        }
        public void UpdateLine(Line lineBO, List<Station> stations)
        {
            lineBO.FirstStation = stations.First().StationID;
            lineBO.LastStation = stations.Last().StationID;
            DO.Line lineDO = new DO.Line();     // Gets a new ID
            lineBO.CopyPropertiesTo(lineDO);    // Give back the original ID which we want
            try
            {
                dl.UpdateLine(lineDO);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.id, ex.v);
            }
            List<DO.LineStation> oldlist = GetLineStationsByLine(lineBO.Id);
            try
            {
                foreach (DO.LineStation lineStation in oldlist)
                {
                    dl.DeleteLineStation(lineStation);  // Deleting all the old line stations
                }
            }
            catch (DO.BadLineStationIdException ex)
            {
                throw new BO.BadLineStationIdException(ex.lineId, ex.stationId, ex.v);
            }

            List<DO.LineStation> lineStationsDO = GenerateLineStations(lineBO, stations);   // Generates new linestations
            try
            {
                foreach (DO.LineStation linestation in lineStationsDO)
                {
                    dl.AddLineStation(linestation);
                }
            }
            catch (DO.BadLineStationIdException ex)
            {
                throw new BO.BadLineStationIdException(ex.lineId, ex.stationId, ex.v);
            }
            List<DO.AdjacentStations> adjacentStationsDO = GenerateAdjStations(stations);   // Generates new adj stations if needed
            try
            {
                foreach (DO.AdjacentStations adjacentstation in adjacentStationsDO)
                {
                    dl.AddAdjStations(adjacentstation);
                }
            }
            catch (DO.BadAdjStationsException ex)                                           
            {
                throw new BO.BadAdjStationsException(ex.currStation, ex.nextStation, ex.v);
            }
        }
        public void DeleteLine(Line line)
        {
            List<DO.LineStation> lineStations_to_remove = GetLineStationsByLine(line.Id);
            try
            {
                foreach (DO.LineStation lineStation in lineStations_to_remove)
                {
                    dl.DeleteLineStation(lineStation);  // Deleting all the old line stations
                }
            }
            catch (DO.BadLineStationIdException ex)
            {
                throw new BO.BadLineStationIdException(ex.lineId, ex.stationId, ex.v);
            }
            dl.DeleteLineTrip(line.Id);
            try
            {
                dl.DeleteLine(line.Id);
            }
            catch (DO.BadLineIdException ex)
            {
                throw new BO.BadLineIdException(ex.id, ex.v);
            }
        }

        Line lineDoBoAdapter(DO.Line lineDO)
        {
            Line lineBO = new Line();
            lineDO.CopyPropertiesTo(lineBO);
            lineBO.Stations = getLineStations(lineBO.Id);
            lineBO.StartAts = getStartTimes(lineBO.Id);
            lineBO.OverallDistance = 0;
            lineBO.OverallDuration = new TimeSpan(0, 0, 0);
            for(int i=0; i<lineBO.Stations.Count-1; i++)
            {
                lineBO.OverallDistance += (double) lineBO.Stations[i].Distance;
                lineBO.OverallDuration += (TimeSpan)lineBO.Stations[i].Duration;
            }
            lineBO.Print = "Line number: " + lineBO.LineNumber + "\nArea: " + lineBO.Area + "\nOverall distance: " + lineBO.OverallDistance
                + "\nOverall duration: " + lineBO.OverallDuration + "\nTaking off at: ";
            foreach(TimeSpan time in lineBO.StartAts)
            {
                lineBO.Print +="\n" + time + " ";
            }
            return lineBO;
        }
        #endregion

        #region LineStations
        public void UpdateLineStation(LineStation lineStation)
        {
            DO.AdjacentStations adjStations = dl.GetAdjStation(lineStation.Station, lineStation.NextStation);
            adjStations.Distance = (double)lineStation.Distance;
            adjStations.Time = (TimeSpan)lineStation.Duration;
            try
            {
                dl.UpdateAdjacentStations(adjStations);
            }
            catch (DO.BadAdjStationsException ex)
            {
                throw new BO.BadAdjStationsException(ex.currStation, ex.nextStation, ex.v);
            }
        }
        /// <summary>
        /// This method should mostly serve lineDoBoAdapter
        /// </summary>
        /// <param name="lineId"> The line's id which we want to take linestations of </param>
        /// <returns></returns>
        List<LineStation> getLineStations(Guid lineId)
        {
            return (from lineStation in dl.GetLineStations() // Every linestation exists
                    where lineStation.LineId == lineId
                    let station = dl.GetStation(lineStation.Station)  // The actual station that have this ID
                    let adjStations = dl.GetAdjStation(lineStation.Station, lineStation.NextStation)
                    select new LineStation()
                    {
                        LineId = lineStation.LineId,
                        Station = lineStation.Station,
                        StationName = station.Name,
                        StationCode = station.Code,
                        LineStationIndex = lineStation.LineStationIndex,
                        PrevStation = lineStation.PrevStation,
                        NextStation = lineStation.NextStation,
                        Distance = adjStations?.Distance,
                        Duration = adjStations?.Time,
                        Print = "Name: " + station.Name + "\nCode: " + station.Code + "\nIndex: " + lineStation.LineStationIndex + "\nDistance from next station: " +
                        adjStations?.Distance + "\nDuration from next station: " + adjStations?.Time
                    }
                    ).ToList();
        }
        #endregion
        #region Simulation
        public void StartSimulator(TimeSpan startTime, int rate, Action<TimeSpan> updateTime)
        {
            MyStopwatch.Instance.m_action += updateTime;
            MyStopwatch.Instance.StartCouting(startTime, rate);
        }

        public void StopSimulator()
        {
            MyStopwatch.Instance.StopCounting();
        }
        #endregion
        #region Tool methods
        private List<Line> GetLinesByStation(Guid id)
        {
            List<Line> lines = GetLines().ToList();
            return (from line in lines
                    where line.Stations.Find(p => p.Station == id) != null
                    select line).ToList();
        }
        private List<DO.LineStation> GetLineStationsByLine(Guid lineid)
        {
            List<DO.LineStation> lineStations = dl.GetLineStations().ToList();
            return (from linestation in lineStations
                    where linestation.LineId == lineid
                    select linestation).ToList();
        }
        private List<DO.LineStation> GenerateLineStations(Line lineBO, List<Station> stations)
        {
            int index = 1;
            List<DO.LineStation> lineStations = (from station in stations
                                                select new DO.LineStation()
                                                {
                                                    LineId=lineBO.Id,
                                                    Station=station.StationID,
                                                    LineStationIndex=index++
                                                }).ToList();
            for (int j = 0; j < lineStations.Count; j++)                 // Initializing prev and next station in each linestation
            {
                if (j > 0) // Can't add prev station to the first station
                    lineStations[j].PrevStation = lineStations[j - 1].Station;
                else
                    lineStations[j].PrevStation = null;
                if (j < lineStations.Count - 1) // Can't add next station to the last station
                    lineStations[j].NextStation = lineStations[j + 1].Station;
                else
                    lineStations[j].NextStation = null;
            }
            return lineStations;
        }
        private List<DO.AdjacentStations> GenerateAdjStations(List<Station> stations)
        {
            List<DO.AdjacentStations> adjacentStations = new List<DO.AdjacentStations>();
            Random r = new Random(1);
            for (int j = 0; j < stations.Count - 1; j++)
            {
                // Checking in case the adj station object already exists
                if ((from adj in dl.GetAdjStations()
                     where adj.Station1 == stations[j].StationID && adj.Station2 == stations[j + 1].StationID
                     select adj).Any() == false) // In case no such adj exists between 2 stations
                {
                    adjacentStations.Add(new DO.AdjacentStations()
                    {
                        Station1 = stations[j].StationID,
                        Station2 = stations[j + 1].StationID,
                        Distance = r.Next(50, 150),             // Distance between 50km to 250km
                        Time = new TimeSpan(r.Next(1, 2), r.Next(0, 60), 0) // Time between 1-2 hours
                    });
                }
            }
            return adjacentStations;

        }
        private List<Station> LineStationsToStations(List<LineStation> linestations)
        {
            List<Station> stations = new List<Station>();
            foreach(LineStation lineStation in linestations)
            {
                DO.Station stationDO = dl.GetStation(lineStation.Station);
                Station stationBO = stationDoBoAdapter(stationDO);
                stations.Add(stationBO);
            }
            return stations;
        }
        private List<DO.LineTrip> GenerateLineTrips(Guid id)
        {
            Random r = new Random();
            List<DO.LineTrip> lineTrips = new List<DO.LineTrip>();
            for (int k = 0; k < 5; k++) 
                lineTrips.Add(new DO.LineTrip()
                {
                    LineId = id,
                    StartAt = new TimeSpan(10 + 2 * k, r.Next(0, 59), 0)
                });
            return lineTrips;
        }
        private List<TimeSpan> getStartTimes(Guid id)
        {
            return (from linetrip in dl.GetLineTrips()
                    where linetrip.LineId == id
                    select linetrip.StartAt).ToList();
        }
        #endregion

    }
}