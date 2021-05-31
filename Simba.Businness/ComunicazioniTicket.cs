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
    public class ComunicazioniTicket : BusinnessBase
    {
        public int getNewId()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var mc = uw.Query<Communications>().
                Max(c => c.Number);
                var mt = uw.Query<Simba.DataLayer.simba_condomini.Ticket>().
                Max(t => t.Number);
                var max = mc > mt ? mc : mt;
                return max + 1;
            }
        }

        public LookupItem getEnvironmenti(int userId)
        {
            return new LookupItem()
            {
                Id = 1,
                Text = "app2 cond 3"
            };
        }
    }
}
