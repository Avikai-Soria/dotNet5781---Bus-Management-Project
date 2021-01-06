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
                + "\nFuel remained: " + busBO.FuelRemain + "\n Bus's status: " + busBO.Status;
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
            stationBO.Print = "Code: " + stationBO.Code + "\nName: " + stationBO.Name + "\nLongitude: " + stationBO.Longitude 
                + "\nLattitude: " + stationBO.Lattitude;
            return stationBO;
        }
        public IEnumerable<Station> GetStations()
        {
            return from stationDO in dl.GetStations()
                   select stationDoBoAdapter(stationDO);
        }
        #endregion
    }
}