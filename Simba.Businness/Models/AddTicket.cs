using DevExpress.Xpo;
using Simba.DataLayer.simba_condomini;
using System;
using System.Collections.Generic;
using System.Web;

namespace Simba.Businness.Models
{
    public class AddTicket
    {
        public AddTicket(int oid, int number, string owner, int classeTicket, int stato,
        List<LookupItem> classeTicketList, string titolo, string descrizione, string note,
        LookupItem locale, LookupItem edificio, LookupItem scala,  List<TicketStatus> storicoStati,
        HttpPostedFileBase files)
        {
            ClasseTicketList = new List<LookupItem>();
            this.Oid = oid;
            this.Number = number;
            this.Owner = owner;
            this.ClasseTicket = classeTicket;
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.Note = note;
            this.Edificio = edificio;
            this.Locale = locale;
            this.StoricoStati = storicoStati;
            this.Files = files;

        }

        public int Oid { get; set; }
        public int Number { get; set; }
        public string Owner { get; set; }
        public int ClasseTicket { get; set; }

        public int Stato { get; set; }
        public List<LookupItem> ClasseTicketList { get; set; }
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public LookupItem Condominio { get; set; }
        public LookupItem Edificio { get; set; }
        public LookupItem Locale { get; set; }
        public List<TicketStatus> StoricoStati { get; set; }
        public HttpPostedFileBase Files { get; set; }

        public Simba.DataLayer.simba_condomini.Ticket ToXpoModel(AddTicket obj)
        {
            ConnectionHelper.Connect(DevExpress.Xpo.DB.AutoCreateOption.None, true);
            return new Simba.DataLayer.simba_condomini.Ticket(XpoDefault.Session)
            {
                Data = DateTime.Now,
                DateUpdate = DateTime.Now,
                Descrizione = obj.Descrizione,
                Note = obj.Note,
                Number = obj.Number,
                Oid = obj.Oid
            };
        }
    }
}