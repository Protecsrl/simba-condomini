using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Comuni : BusinnessBase
    {
        public List<DataLayer.simba_condomini.Comuni> GetAll(int? provincia)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<DataLayer.simba_condomini.Comuni>()
                .Where(e => e.PROVINCIA.Oid == provincia || !provincia.HasValue).ToList();
                return data;
            }
        }
    }
}
