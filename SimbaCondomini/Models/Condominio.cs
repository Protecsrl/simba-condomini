using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SimbaCondomini.Models
{
    public class Condominio
    {
        public Condominio(int oid, int comune, string nomeCondominio, string indirizzo, string partitaIva, double latitudine, double longitudine)
        {
            this.Comune = comune;
            this.Indirizzo = indirizzo;
            this.Latitudine = latitudine;
            this.Longitudine = longitudine;
            this.NomeCondominio = nomeCondominio;
            this.Oid = oid;
            this.PartitaIva = partitaIva;
        }
        public int Oid
        {
            get;
            private set;
        }
        public int Comune
        {
            get;
            private set;
        }
        public string NomeCondominio
        {
            get;
            private set;
        }
        public string Indirizzo
        {
            get;
            private set;
        }
        public string PartitaIva
        {
            get;
            private set;
        }
        public double Latitudine
        {
            get;
            private set;
        }
        public double Longitudine
        {
            get;
            private set;
        }
    }
}