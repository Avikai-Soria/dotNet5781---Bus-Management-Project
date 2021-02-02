using DO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DalApi
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IDAL
    {
        #region Busses
        void AddBus(Bus bus);
        Bus GetBus(string licensenum);
        IEnumerable<Bus> GetBuses();
        void UpdateBus(Bus bus);
        void DeleteBus(string licensenum);

        #endregion

        #region Stations
        void AddStation(Station station);
        Station GetStation(Guid code);
        IEnumerable<Station> GetStations();
        void UpdateStation(Station station);
        void DeleteStation(Guid code);
        #endregion

        #region Line
        void AddLine(Line lineDO);
        IEnumerable<Line> GetLines();
        //IEnumerable<Line> GetLinesByStation(int code); Will probably not use this
        void UpdateLine(Line lineDO);
        void DeleteLine(Guid id);

        #endregion

        #region LineStation
        void AddLineStation(LineStation linestation);
        IEnumerable<LineStation> GetLineStations();
        void DeleteLineStation(LineStation lineStation);

        #endregion

        #region AdjacentStations
        void AddAdjStations(AdjacentStations adjacentStations);
        AdjacentStations GetAdjStation(Guid? currStation, Guid? nextStation);
        IEnumerable<AdjacentStations> GetAdjStations();
        void UpdateAdjacentStations(AdjacentStations adjStations);
        #endregion
        #region LineTrip
        void AddLineTrip(LineTrip lineTrip);
        IEnumerable<LineTrip> GetLineTrips();
        void DeleteLineTrip(Guid code);

        #endregion
    }
}
