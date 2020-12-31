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

        Bus busDoBoAdapter(DO.Bus busDO)
        {
            Bus busBO = new Bus();
            busDO.CopyPropertiesTo(busBO);
            return busBO;
        }
        public IEnumerable<Bus> GetBuses()
        {
            return from busDO in dl.GetBuses()
                   select busDoBoAdapter(busDO);
        }
    }
}