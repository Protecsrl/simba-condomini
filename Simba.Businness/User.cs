using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class User : BusinnessBase
    {
        public Simba.DataLayer.simba_condomini.User GetUser()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(5);
                return user;
            }
        }
    }
}
