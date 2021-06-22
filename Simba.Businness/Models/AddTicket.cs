
using DevExpress.ExpressApp.Utils;
using DevExtreme.AspNet.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Simba.Businness.Models
{
    public class AddTicket
    {
        public AddTicket()
        {

        }

        public AddTicket(int oid, int number, string codice, string owner, int stato,
        int classeTicket, string titolo, string descrizione, string note,
        int locale, int edificio, int condominio, List<TicketStatusAssociated> storicoStati,
        HttpPostedFileBase files)
        {
            this.Oid = oid;
            this.Number = number;
            this.Codice = codice;
            this.Owner = owner;
            this.ClasseTicket = classeTicket;
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.Note = note;
            this.Edificio = edificio;
            this.Locale = locale;
            this.StoricoStati = storicoStati;
            this.Condominio = condominio;
            this.Files = files;

            if (storicoStati.Any())
            {
                this.Stato = storicoStati.OrderBy(s => s.Date).First().Oid;
            }

        }

        public int Oid { get; set; }
        public int Number { get; set; }
        public string Codice { get; set; }
        public string Owner { get; set; }
        public int ClasseTicket { get; set; }

        public int Stato { get; set; }


        [Required(ErrorMessage = "È richiesto il titolo")]
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public int Condominio { get; set; }
        public int Edificio { get; set; }
        public int Locale { get; set; }
        public List<TicketStatusAssociated> StoricoStati { get; set; }
        public HttpPostedFileBase Files { get; set; }


    }
}