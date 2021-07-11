using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Province : BusinnessBase
    {
        public List<DataLayer.simba_condomini.Provincia> GetAll(int? regione)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<DataLayer.simba_condomini.Provincia>()
                .Where(e => e.REGIONE.Oid == regione || !regione.HasValue).ToList();
                return data;
            }
        }
    }
}
