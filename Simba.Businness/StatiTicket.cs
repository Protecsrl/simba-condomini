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
    public class StatiTicket
    {
        public List<TicketStatus> GetAll()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<TicketStatus>().ToList();
                return data;
            }
        }
    }
}
