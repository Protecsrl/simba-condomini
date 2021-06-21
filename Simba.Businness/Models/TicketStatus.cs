using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness.Models
{
    public class TicketStatus
    {
        public TicketStatus(int oid, string name, string descrizione)
        {
            this.Oid = oid;
            this.Name = name;
            this.Descrizione = descrizione;
        }

        public int Oid
        {
            get;
            private set;
        }
        public string Name
        {
            get;
            private set;
        }
        public string Descrizione
        {
            get;
            private set;
        }
    }
}
