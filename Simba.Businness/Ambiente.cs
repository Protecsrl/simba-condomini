using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Environment = Simba.DataLayer.simba_condomini.Environment;

namespace Simba.Businness
{
    public class Ambiente: BusinnessBase
    {
        public List<Ambiente> GetByEdificio(int edificio)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Environment>()
                .Where(b => b.Building.Id == edificio).ToList();
                return data;
            }
        }
    }
}
