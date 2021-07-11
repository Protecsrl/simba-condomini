using DevExpress.Xpo;
using Simba.DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class Locali : BusinnessBase
    {
        public List<Simba.DataLayer.Database.Environment> GetAll(int? edificio)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Environment>()
                .Where(e => e.Building.Oid == edificio || !edificio.HasValue).ToList();
                return data;
            }
        }
    }
}
