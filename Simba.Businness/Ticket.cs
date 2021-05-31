using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System.Collections.Generic;
using System.Linq;

namespace Simba.Businness
{
    public class Ticket : BusinnessBase
    {
        public List<Simba.DataLayer.simba_condomini.Ticket> GetUserTicket(int? userId)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.Ticket>().
                Where(c => !userId.HasValue || c.User.Oid == userId.Value).
                ToList();
                return data;
            }
        }

        public List<Simba.DataLayer.simba_condomini.TicketStatuses> GetTicketStatuses(int idTicket){
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<Simba.DataLayer.simba_condomini.TicketStatuses>().
                Where(c => c.IdTicket.Oid == idTicket).
                ToList();
                return data;
            }
        }

        public void SaveTicket(Simba.DataLayer.simba_condomini.Ticket ticket)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                ticket.Save();
                uw.CommitChanges();
            }
        }
    }
}
