using DevExpress.Xpo;
using DevExpress.Xpo.DB;
using Simba.DataLayer.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness
{
    public class ComunicazioniTicket : BusinnessBase
    {
        public int getNewId(int idCondominio)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var mc = uw.Query<Communications>().
                Max(c => c.Number);
                var mt = uw.Query<Simba.DataLayer.Database.Ticket>().
                Max(t => t.Number);
                var max = mc > mt ? mc : mt;
                return max + 1;
            }
        }

        public string GetNewCodice(int idCondominio){
            int n = getNewId(idCondominio);
            string codice =string.Concat(idCondominio.ToString().PadLeft(6, '0'), n.ToString().PadLeft(6, '0'));
            return codice;
        }

        public LookupItem getEnvironmenti(int userId)
        {
            return new LookupItem(1, "app2 cond 3");
        }
    }
}
