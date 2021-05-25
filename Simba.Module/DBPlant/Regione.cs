using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("REGIONE")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Regione")]
    [NavigationItem("Tabelle Anagrafiche")]
    [ImageName("Action_Navigation")]
    public class Regione : XPObject
    {
        public Regione()
            : base()
        {
        }

        public Regione(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fCodRegione;
        [Size(2), Persistent("CODICEREGIONE"),DisplayName("Codice Regione")]
        [DbType("varchar(2)")]
        public string CodRegione
        {
            get
            {
                return fCodRegione;
            }
            set
            {
                SetPropertyValue<string>("CodRegione", ref fCodRegione, value);
            }
        }


        private string fDescrizione;
        [Size(1000),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(1000)")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }

        [Association(@"Provincia_Regione", typeof(Provincia)),
        DisplayName("Provincia")]
        public XPCollection<Provincia> Provincias
        {
            get
            {
                return GetCollection<Provincia>("Provincias");
            }
        }

        private string fNazione;
        [Size(500), Persistent("NAZIONE"), DisplayName("Nazione")]
        [DbType("varchar(500)")]
        public string Nazione
        {
            get
            {
                return fNazione;
            }
            set
            {
                SetPropertyValue<string>("Nazione", ref fNazione, value);
            }
        }

    }
}
