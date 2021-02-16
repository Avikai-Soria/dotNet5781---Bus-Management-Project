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
        /// <summary>
        /// Gets all the existing busses
        /// </summary>
        /// <returns>All busses</returns>
        IEnumerable<Bus> GetBuses();
        #endregion

        #region Stations
        /// <summary>
        /// Adds a new station
        /// </summary>
        /// <param name="station">The station object to add</param>
        void AddStation(Station station);
        /// <summary>
        /// Gets all the existing stations
        /// </summary>
        /// <returns>All the stations</returns>
        IEnumerable<Station> GetStations();
        /// <summary>
        /// Updates a station
        /// </summary>
        /// <param name="station">The updated station object</param>
        void UpdateStation(Station station);
        /// <summary>
        /// Deletes a station
        /// </summary>
        /// <param name="station">The station object we want to delete (not sure why code is not sent instead)</param>
        void DeleteStation(Station station);
        #endregion

        #region Lines
        /// <summary>
        /// Adds a new line, and sets all of its linestatios
        /// </summary>
        /// <param name="line">The line object to add</param>
        /// <param name="stations">The line's linestations</param>
        void AddLine(Line line, List<Station> stations);
        /// <summary>
        /// Gets all the existing lines
        /// </summary>
        /// <returns>All the existing lines</returns>
        IEnumerable<Line> GetLines();
        /// <summary>
        /// Updates a line, and its linestations
        /// </summary>
        /// <param name="line">The line object to update</param>
        /// <param name="stations">The new linestation it has</param>
        void UpdateLine(Line line, List<Station> stations);
        /// <summary>
        /// Deletes a line and its linestations
        /// </summary>
        /// <param name="line">The line we want to delete</param>
        void DeleteLine(Line line);
        #endregion

        #region LineStation
        /// <summary>
        /// Updates a linestation (mostly to edit distance and duration between stations)
        /// </summary>
        /// <param name="lineStation">The updated linestation object</param>
        void UpdateLineStation(LineStation lineStation);
        #endregion

        #region Simulation
        /// <summary>
        /// This function triggers the start of a clock in the simulation
        /// </summary>
        /// <param name="startTime">The time which we want to start the timer from</param>
        /// <param name="rate">The progression rate of the timer</param>
        /// <param name="updateTime">The function that will be updated</param>
        void StartSimulator(TimeSpan startTime, int rate, Action<TimeSpan> updateTime);
        /// <summary>
        /// This function triggers the stop of the clock in the simulation
        /// </summary>
        void StopSimulator();
        /// <summary>
        /// This function returns the cloest 5 line numbers and the durations until they reach a station from a current time
        /// </summary>
        /// <param name="station">The station we want to check which lines are about to pass</param>
        /// <param name="currTime">The current time</param>
        /// <returns>KeyValuePair of a timespan for duration and int for line number</returns>
        IEnumerable<KeyValuePair<TimeSpan, int>> GetIncomingLines(Station station, TimeSpan currTime);
        #endregion

    }
}
