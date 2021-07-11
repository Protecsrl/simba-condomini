using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Comuni : BusinnessBase
    {
        public List<Simba.DataLayer.Database.Comuni> GetAll(int? provincia)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Comuni>()
                .Where(e => e.PROVINCIA.Oid == provincia || !provincia.HasValue).ToList();
                return data;
            }
        }
    }
}
