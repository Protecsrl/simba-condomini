using DevExpress.Xpo;
using Simba.Businness.Security;
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

                FakeVariebiles d = new FakeVariebiles();
                var user = uw.GetObjectByKey<DataLayer.simba_condomini.User>(d.UserId);
                return user;
            }
        }
    }
}
