using BL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLApi
{
    public static class BLFactory
    {
        public static IBL GetBI()
        {
            return new BLImp(); // Change later to "Instance" probably, will also need 2 different options
        }
    }
}
