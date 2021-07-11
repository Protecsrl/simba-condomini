using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Regioni : BusinnessBase
    {
        public List<DataLayer.simba_condomini.Regione> GetAll()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<DataLayer.simba_condomini.Regione>();
                return data.ToList();
            }
        }
    }
}
