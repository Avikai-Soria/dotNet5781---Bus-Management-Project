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
        Station stationDoBoAdapter(DO.Station stationDO)
        {
            Station stationBO = new Station();
            stationDO.CopyPropertiesTo(stationBO);
            stationBO.Lines = GetLinesByStation(stationBO.Code);
            stationBO.Print = "Code: " + stationBO.Code + "\nName: " + stationBO.Name + "\nLongitude: " + stationBO.Longitude 
                + "\nLattitude: " + stationBO.Lattitude +"\nLines passing by: ";
            foreach (Line line in stationBO.Lines)
                stationBO.Print += line.Code + " ";
            return stationBO;
        }
        public IEnumerable<Station> GetStations()
        {
            return from stationDO in dl.GetStations()
                   select stationDoBoAdapter(stationDO);
        }
        #endregion

        #region Line
        Line lineDoBoAdapter(DO.Line lineDO)
        {
            Line lineBO = new Line();
            lineDO.CopyPropertiesTo(lineBO);
            lineBO.Stations = getLineStations(lineBO.Id);
            lineBO.Print = "ID: " + lineBO.Id + "\nCode: " + lineBO.Code + "\nArea: " + lineBO.Area;
            return lineBO;
        }
        public IEnumerable<Line> GetLines()
        {
            return from lineDO in dl.GetLines()
                   select lineDoBoAdapter(lineDO);
        }
        #endregion

        #region LineStations
        /// <summary>
        /// This method should mostly serve lineDoBoAdapter
        /// </summary>
        /// <param name="lineId"> The line's id which we want to take linestations of </param>
        /// <returns></returns>
        List<LineStation> getLineStations(int lineId)
        {
            return (from lineStation in dl.GetLineStations() // Every linestation exists
                    where lineStation.LineId == lineId
                    let station = dl.GetStation(lineStation.Station)  // The actual station that have this ID
                    let adjStations = dl.GetAdjStation(lineStation.Station, lineStation.NextStation)
                    select new LineStation()
                    {
                        LineId=lineStation.LineId,
                        Station=lineStation.Station,
                        StationName=station.Name,
                        LineStationIndex=lineStation.LineStationIndex,
                        PrevStation=lineStation.PrevStation,
                        NextStation=lineStation.NextStation,
                        Distance=adjStations?.Distance,
                        Duration=adjStations?.Time,
                        Print="Name: "+station.Name+"\nID: "+lineStation.Station+"\nIndex: "+lineStation.LineStationIndex+"\nDistance from next station: " +
                        adjStations?.Distance+"\nDuration from next station: "+adjStations?.Time
                    }
                    ).ToList(); 
        }
        #endregion

        #region Tool methods
        private List<Line> GetLinesByStation(int code)
        {
            List<Line> lines = GetLines().ToList();
            return (from line in lines
                    where line.Stations.Find(p => p.Station == code) != null
                    select line).ToList();
        }
        #endregion
    }
}