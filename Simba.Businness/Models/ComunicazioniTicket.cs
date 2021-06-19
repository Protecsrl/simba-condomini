using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Simba.Businness.Models
{
    public enum ItemType
    {
        Comunicazione = 0,
        Ticket = 1
    }
    public class ComunicazioniTicket : IComparable<ComunicazioniTicket>
    {
        public ComunicazioniTicket(ItemType type, int oid, string testo, int parentCommunication, string user, string condominium, DateTime dateInsert)
        {
            this.Oid = oid;
            this.Testo = testo;
            this.ParentCommunication = parentCommunication;
            this.User = user;
            this.Condominium = condominium;
            this.DateInsert = dateInsert;
            this.Type = type;
        }

        public ItemType Type
        {
            get;
            private set;
        }

        public int Oid
        {
            get;
            private set;
        }
        public string Testo
        {
            get;
            private set;
        }
        public int ParentCommunication
        {
            get;
            private set;
        }
        public string User
        {
            get;
            private set;
        }
        public string Condominium
        {
            get;
            private set;
        }
        public DateTime DateInsert
        {
            get;
            private set;
        }

        public int CompareTo(ComunicazioniTicket other)
        {
            if (other == null)
                return 1;

            else
                return this.Oid.CompareTo(other.Oid);
        }

        public string FormattedDateInsert
        {
            get { return this.DateInsert.ToString("dd/MM/yyyy hh:mm"); }
        }
    }
}