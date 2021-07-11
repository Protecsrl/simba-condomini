using DevExpress.Xpo;
using Simba.Businness.Models;
using Simba.DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Regioni : BusinnessBase
    {
        public List<Simba.DataLayer.Database.Regione> GetAll()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.Database.Regione>();
                return data.ToList();
            }
        }
    }
}
