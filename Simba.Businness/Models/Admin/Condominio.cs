﻿using System;
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
        string partitaIva, string latitudine, string longitudine, string code)
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
            get; set;
        }
        public int Comune
        {
            get; set;
        }
        public string NomeCondominio
        {
            get; set;
        }
        public string Indirizzo
        {
            get; set;
        }
        public string PartitaIva
        {
            get; set;
        }
        public string Latitudine
        {
            get; set;
        }
        public string Longitudine
        {
            get; set;
        }
        public string Code
        {
            get; set;
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
