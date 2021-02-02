using BO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    //CRUD Logic:
    // Create - add new instance
    // Request - ask for an instance or for a collection
    // Update - update properties of an instance
    // Delete - delete an instance
    public interface IBL
    {
        #region Busses
        IEnumerable<Bus> GetBuses();
        #endregion
        #region Stations
        IEnumerable<Station> GetStations();
        void AddStation(Station station);
        void UpdateStation(Station station);
        void DeleteStation(Station station);
        #endregion
        #region Lines
        void AddLine(Line line, List<Station> stations);
        IEnumerable<Line> GetLines();
        void UpdateLine(Line line, List<Station> stations);
        void DeleteLine(Line line);
        #endregion
        #region LineStation
        void UpdateLineStation(LineStation lineStation);
        #endregion
        #region Simulation
        void StartSimulator(TimeSpan startTime, int rate, Action<TimeSpan> updateTime);
        void StopSimulator();
        #endregion

    }
}
