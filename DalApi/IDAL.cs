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
        Bus GetBus(int licensenum);
        IEnumerable<Bus> GetBuses();
        void UpdateBus(Bus bus);
        void DeleteBus(int licensenum);

        #endregion

        #region Stations
        void AddStation(Station station);
        Station GetStation(int code);
        IEnumerable<Station> GetStations();
        void UpdateStation(Station station);
        void DeleteStation(int code);
        #endregion

        #region Line
        IEnumerable<Line> GetLines();
        //IEnumerable<Line> GetLinesByStation(int code); Will probably not use this
        #endregion

        #region LineStation
        IEnumerable<LineStation> GetLineStations();

        #endregion

        #region AdjacentStations
        AdjacentStations GetAdjStation(int? currStation, int? nextStation);

        #endregion
    }
}
