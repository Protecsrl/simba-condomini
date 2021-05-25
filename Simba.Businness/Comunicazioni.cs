using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class Comunicazioni : BusinnessBase
    {
        public List<Communications> GetUserCommunication(int? userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Communications>().
                Where(c=>!userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }
    }
}
