using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class Locali
    {
        public List<DataLayer.simba_condomini.Environment> GetAll(int edificio)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<DataLayer.simba_condomini.Environment>()
                .Where(e => e.Building.Id == edificio).ToList();
                return data;
            }
        }
    }
}
