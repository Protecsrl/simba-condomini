using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness.Models.Admin
{
    public class Condominio
    {
        public Condominio()
        {

        }
        public Condominio(
        int oid, int comune, string nomeCondominio, string indirizzo,
        string partitaIva, double latitudine, double longitudine, string code)
        {
            this.Oid = oid;
            this.Comune = comune;
            this.NomeCondominio = nomeCondominio;
            this.Indirizzo = indirizzo;
            this.PartitaIva = partitaIva;
            this.Latitudine = latitudine;
            this.Longitudine = longitudine;
            this.Code = code;
        }
        public int Oid
        {
            get; private set;
        }
        public int Comune
        {
            get; private set;
        }
        public string NomeCondominio
        {
            get; private set;
        }
        public string Indirizzo
        {
            get; private set;
        }
        public string PartitaIva
        {
            get; private set;
        }
        public double Latitudine
        {
            get; private set;
        }
        public double Longitudine
        {
            get; private set;
        }
        public string Code
        {
            get; private set;
        }


        public string Provincia
        {
            get; private set;
        }

        public string Regione
        {
            get; private set;
        }
    }
}
