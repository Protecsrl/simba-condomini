using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class Edifici : BusinnessBase
    {
        public List<Building> GetByCondominium(int condominium)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Building>()
                .Where(b=>b.Condominium.Oid == condominium).ToList();
                return data;
            }
        }
    }
}
