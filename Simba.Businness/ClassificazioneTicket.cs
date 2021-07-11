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
    public class ClassificazioneTicket : BusinnessBase
    {
        public List<TicketClassification> GetAll()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var data = uw.Query<TicketClassification>().ToList();
                return data;
            }
        }
    }
}
