using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness.Models
{
    public class TicketStatusAssociated
    {
        public TicketStatusAssociated(int Oid, DateTime date, string descrizioneStato, string descrizione)
        {
            this.Descrizione = descrizione;
            this.Date = date;
            this.DescrizioneStato = descrizioneStato;
            this.Oid = Oid;
        }

        public int Oid { get; private set; }
        public DateTime Date { get; private set; }
        public string DescrizioneStato { get; private set; }
        public string Descrizione { get; private set; }
    }
}