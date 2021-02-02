using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DO;

namespace DS
{
    public static class DataSource
    {
        static Random r = new Random(1);

        public static List<Bus> s_buses;
        public static List<Station> s_stations;
        public static List<Line> s_lines;
        public static List<LineStation> s_lineStations;
        public static List<AdjacentStations> s_adjacentStations;
        public static List<LineTrip> s_lineTrips;
        #region Station's names
        public static string[] stationsNames = // 50 different names of stations 
        {"קרית עקרון/כביש 411", "צומת חולדה/כביש 411","גרינשפן/יגאל אלון","השומר/האבות","משה שרת/יעקב קנר",
        "הדייגים/הנחשול","יוסף בורג/משואות יצחק","יוסף בורג/כתריאל רפפורט","וייצמן/דרך יצחק רבין","שדרות משה דיין/יוסף לישנסקי",
        "השר חיים שפירא/יוסף ספיר","השר חיים שפירא/הרב שלום ג'רופי","שדרות יעקב/יוסף הנשיא","שדרות יעקב/עזרא","לייב יוספזון/יעקב ברמן",
        "הרב יעקב ברמן/הרב יהודה צבי מלצר","ברמן/מלצר","הנשיא הראשון/מכון ויצמן","הנשיא הראשון/קיפניס","הירדן/הערבה","הירדן/חרוד",
        "האלונים/הדקל","האלונים א/הדקל","פארק תעשיות שילת","עיריית מודיעין מכבים רעות","חיים ברלב/מרדכי מקלף","אלמוג סנטר/אפרים קישון",
        "גן חק''ל/רבאט","קניון צ. רמלה לוד/דוכיפת","היצירה/התקווה","עמל/היצירה","פרנקל/ויתקין","ישראל פרנקל/דוב הוז","יוספטל/הדס",
        "אהרון בוגנים/משה שרת","גרשון ש''ץ/שמחה הולצברג","הולצברג/שץ","אשכול/הרב שפירא","יהודה שטיין/קרן היסוד","שמשון הגיבור/המסגד","ביאליק/חניתה",
        "ביאליק/ירמיהו","א.ס לוי/סעדיה מרדכי","רזיאל/זכריה","חרוד א","חרוד/הירדן","הירדן/דן","עוזי חיטמן/שושנה דמארי","עוזי חיטמן/דליה רביקוביץ",
        "עוזי חיטמן/חנוך לוין"};
        #endregion

        /// <summary>
        /// Initializing all the lists
        /// </summary>
        static DataSource()
        {
            s_lineStations = new List<LineStation>();
            s_adjacentStations = new List<AdjacentStations>();
            s_lineTrips = new List<LineTrip>();
            InitBusList();
            InitStationList();
            InitLinesList();
        }
        /// <summary>
        /// Initializing 20 ready busses, with random datetimes
        /// </summary>
        private static void InitBusList()
        {
            s_buses = new List<Bus>();
            for (int i = 0; i < 20; i++) 
            {
                Bus bus = new Bus();
                bus.LicenseNum = i.ToString();
                bus.FromDate = new DateTime(r.Next(1960, 2020), r.Next(1, 12), r.Next(1, 30));
                bus.TotalTrip = 0;  // Assuming all busses haven't done any travels yet
                bus.FuelRemain = 1200;  // Assuming all busses have full fuel.
                bus.Status = BusStatus.Ready;
                s_buses.Add(bus);
            }
        }
        /// <summary>
        /// Initializing 50 stations in the list
        /// </summary>
        private static void InitStationList()
        {
            s_stations = new List<Station>();
            for(int i=0;i<50;i++)
            {
                Station station = new Station();
                station.Lattitude = r.NextDouble() + 30;    // Make codinates more realistic if there's time left
                station.Longitude = r.NextDouble() + 30;
                station.Name = stationsNames[i];
                s_stations.Add(station);
            }
        }
        /// <summary>
        /// Initiating 10 lines, each will contain at least 10 line station and up to 20, and initializing the needed adj stations
        /// </summary>
        private static void InitLinesList()
        {
            s_lines = new List<Line>();
            for(int i=0; i<10; i++)
            {
                Line line = new Line();
                line.LineNumber = i + 1;
                line.Area = (Areas)r.Next(0, 7);
                List<Station> randomStations = RandomizeStations();
                #region LineStation Initialization
                List<LineStation> randomLineStations = new List<LineStation>(); // Initializing between 10-20 stations in this list
                int index = 1;                                                  // This one will count which index we're at for each Line station
                randomLineStations = (from station in randomStations            // Initializing only those 3 properties in linestation, the other 2 later
                                      select new LineStation()
                                      {
                                          LineId = line.Id,
                                          Station=station.StationID,
                                          LineStationIndex=index++,
                                      }).ToList();
                for(int j=0; j<randomLineStations.Count; j++)                 // Initializing prev and next station in each linestation
                {
                    if (j > 0) // Can't add prev station to the first station
                        randomLineStations[j].PrevStation = randomLineStations[j - 1].Station;
                    else
                        randomLineStations[j].PrevStation = null;
                    if (j < randomLineStations.Count - 1) // Can't add next station to the last station
                        randomLineStations[j].NextStation = randomLineStations[j + 1].Station;
                    else
                        randomLineStations[j].NextStation = null;
                }
                s_lineStations.AddRange(randomLineStations);
                #endregion
                #region AdjacentStation initialization 
                for(int j=0; j<randomStations.Count-1; j++)
                {
                    // Checking in case the adj station object already exists
                    if (s_adjacentStations.Find(adj => adj.Station1 == randomStations[j].StationID && adj.Station2 == randomStations[j + 1].StationID) == null)
                    {
                        s_adjacentStations.Add(new AdjacentStations()
                        {
                            Station1 = randomStations[j].StationID,
                            Station2 = randomStations[j + 1].StationID,
                            Distance = r.Next(50, 150),             // Distance between 50km to 250km
                            Time = new TimeSpan(r.Next(1, 2), r.Next(0,60), 0) // Time between 1-2 hours
                        });
                    }
                }
                #endregion
                #region LineTrip Initialization
                for (int k = 0; k < 5; k++)
                    s_lineTrips.Add(new LineTrip()
                    {
                        LineId = line.Id,
                        StartAt = new TimeSpan(10 + i + 2 * k, r.Next(0, 59), 0)
                    });
                #endregion
                line.FirstStation = randomStations.First().StationID;
                line.LastStation = randomStations.Last().StationID;
                s_lines.Add(line);
            }
        }
        #region tool methods
        /// <summary>
        /// This method will serve intializing the lines, as lines needs to contain at least 10 stations
        /// </summary>
        /// <returns></returns>
        private static List<Station> RandomizeStations()
        {
            List<Station> stations = new List<Station>();
            int random_amount = r.Next(10, 20);                     // This parameter will decide how many stations will be returned
            int random_station = r.Next(0, s_stations.Count()-1);   // This parameter will randomize which station to add next
            for (int i = 0; i < random_amount; i++)
            {
                Station station = s_stations[random_station];
                if (stations.Contains(station))                     // in case the station was already inserted
                    i--;
                else
                    stations.Add(station);
                random_station = r.Next(0, s_stations.Count() - 1);
            }
            return stations;
        }
        #endregion
    }
}
    
