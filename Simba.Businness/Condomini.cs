using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class Condomini : BusinnessBase
    {

        public List<Condominium> GetAll()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Condominium>().ToList();
                return data;
            }
        }
    }
}
