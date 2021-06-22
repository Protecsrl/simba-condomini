using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Simba.Businness.Models
{
    public class AddComunicazione
    {

        public AddComunicazione()
        {

        }

        public AddComunicazione(int oid, int number, string owner, string codice, int stato,
        string titolo, string descrizione, string note,
        int locale, int edificio, int condominio,
        HttpPostedFileBase files)
        {
            this.Oid = oid;
            this.Number = number;
            this.Owner = owner;
            this.Codice = codice;
            this.Titolo = titolo;
            this.Descrizione = descrizione;
            this.Note = note;
            this.Edificio = edificio;
            this.Locale = locale;
            this.Condominio = condominio;
            this.Files = files;

        }
        public int Oid { get; set; }
        public int Number { get; set; }
        public string Codice { get; set; }
        public string Owner { get; set; }

        [Required(ErrorMessage = "È richiesto il titolo")]
        public string Titolo { get; set; }
        public string Descrizione { get; set; }
        public string Note { get; set; }
        public int Condominio { get; set; }
        public int Edificio { get; set; }
        public int Locale { get; set; }
        public HttpPostedFileBase Files { get; set; }
    }
}
