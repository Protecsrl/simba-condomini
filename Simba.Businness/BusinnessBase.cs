using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class BusinnessBase
    {
        public BusinnessBase()
        {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.None, true);
        }
    }
}
