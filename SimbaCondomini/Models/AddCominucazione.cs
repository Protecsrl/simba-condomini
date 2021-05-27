using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbaCondomini.Models
{
    public class AddCominucazione
    {
        public int Number { get; set; }
        public string Owner { get; set; }
        public LookupItem ClasseTicket { get; set; }
        public LookupItem[] ClasseTicketList { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public LookupItem Locale { get; set; }
        public LookupItem Scala { get; set; }
        public LookupItem Piano { get; set; }
    }
}