using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbaCondomini.Models
{
    public class TicketCommunicationClassification
    {
        public TicketCommunicationClassification(int oid, string nome, string descrizione)
        {
            this.Oid = oid;
            this.Nome = nome;
            this.Descrizione = descrizione;
        }
        public int Oid
        {
            get;
            private set;
        }
        public string Nome
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