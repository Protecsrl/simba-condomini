using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Province : BusinnessBase
    {
        public List<Simba.DataLayer.Database.Provincia> GetAll(int? regione)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Provincia>()
                .Where(e => e.REGIONE.Oid == regione || !regione.HasValue).ToList();
                return data;
            }
        }
    }
}
