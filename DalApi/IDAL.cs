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
        /// <summary>
        /// Adds a new bus to the list to the xml file
        /// </summary>
        /// <param name="bus"></param>
        void AddBus(Bus bus);
        /// <summary>
        /// Get a bus object by its license number
        /// </summary>
        /// <param name="licensenum"></param>
        /// <returns></returns>
        Bus GetBus(string licensenum);
        /// <summary>
        /// Get all the existing busses
        /// </summary>
        /// <returns></returns>
        IEnumerable<Bus> GetBuses();
        /// <summary>
        /// Updates a bus
        /// </summary>
        /// <param name="bus">The updated value</param>
        void UpdateBus(Bus bus);
        /// <summary>
        /// Deletes a bus from the xml file
        /// </summary>
        /// <param name="licensenum"></param>
        void DeleteBus(string licensenum);

        #endregion

        #region Stations
        /// <summary>
        /// Adds a new station to the xml file
        /// </summary>
        /// <param name="station">the station to add</param>
        void AddStation(Station station);
        /// <summary>
        /// Get a station object by it's code
        /// </summary>
        /// <param name="code">The station's code</param>
        /// <returns>Station object</returns>
        Station GetStation(Guid code);
        /// <summary>
        /// Gets all the existing stations
        /// </summary>
        /// <returns>The stations</returns>
        IEnumerable<Station> GetStations();
        /// <summary>
        /// Updates a station by it's object (instead of it's code for some reason)
        /// </summary>
        /// <param name="station">The station to update</param>
        void UpdateStation(Station station);
        /// <summary>
        /// Deletes a station from the xml file
        /// </summary>
        /// <param name="code">The code of the station we want to delete</param>
        void DeleteStation(Guid code);
        #endregion

        #region Line
        /// <summary>
        /// Adds a new line to the xml file
        /// </summary>
        /// <param name="lineDO">The line object we want to add</param>
        void AddLine(Line lineDO);
        /// <summary>
        /// Gets all the existing lines in the xml file.
        /// </summary>
        /// <returns></returns>
        IEnumerable<Line> GetLines();
        /// <summary>
        /// Updates a line by its code
        /// </summary>
        /// <param name="lineDO">The updated line</param>
        void UpdateLine(Line lineDO);
        /// <summary>
        /// Deletes a line from the xml file by its code
        /// </summary>
        /// <param name="id">The line's code</param>
        void DeleteLine(Guid id);

        #endregion

        #region LineStation
        /// <summary>
        /// Adds a new linestation between 2 stations in a line
        /// </summary>
        /// <param name="linestation">The linestation object</param>
        void AddLineStation(LineStation linestation);
        /// <summary>
        /// Gets all the existing linestation object in the xml
        /// </summary>
        /// <returns>All the linestations</returns>
        IEnumerable<LineStation> GetLineStations();
        /// <summary>
        /// Deletes a linestation from the xml file
        /// </summary>
        /// <param name="lineStation">The linestation object we want to remove (more simple since we don't have a code for each)</param>
        void DeleteLineStation(LineStation lineStation);

        #endregion

        #region AdjacentStations
        /// <summary>
        /// Adds a new adjacent between 2 stations to the xml file
        /// </summary>
        /// <param name="adjacentStations">The adj object to add</param>
        void AddAdjStations(AdjacentStations adjacentStations);
        /// <summary>
        /// Returns adj object between 2 stations
        /// </summary>
        /// <param name="currStation">First station</param>
        /// <param name="nextStation">Next station</param>
        /// <returns>The adj object</returns>
        AdjacentStations GetAdjStation(Guid? currStation, Guid? nextStation);
        /// <summary>
        /// Gets all the existing adj objects
        /// </summary>
        /// <returns>All adj objects</returns>
        IEnumerable<AdjacentStations> GetAdjStations();
        /// <summary>
        /// Updates an adj object, mostly its time and distance
        /// </summary>
        /// <param name="adjStations">The updated object</param>
        void UpdateAdjacentStations(AdjacentStations adjStations);
        #endregion

        #region LineTrip
        /// <summary>
        /// Adds a new start of line to the xml file (not limited for how many there are per line)
        /// </summary>
        /// <param name="lineTrip">The new linetrip to add</param>
        void AddLineTrip(LineTrip lineTrip);
        /// <summary>
        /// Gets all the existing linetrips
        /// </summary>
        /// <returns></returns>
        IEnumerable<LineTrip> GetLineTrips();
        /// <summary>
        /// Deletes a linetrip from the xml file
        /// </summary>
        /// <param name="code">The code of the line we want to remove its linetrips.</param>
        void DeleteLineTrip(Guid code);

        #endregion
    }
}
