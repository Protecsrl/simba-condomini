using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness.Models
{
    public class TicketStatus
    {
        public TicketStatus(DateTime date, string descrizioneStato, string descrizione)
        {
            this.Descrizione = descrizione;
            this.Date = date;
            this.DescrizioneStato = descrizioneStato;
        }
        public DateTime Date { get; private set; }
        public string DescrizioneStato { get; private set; }
        public string Descrizione { get; private set; }
    }
}