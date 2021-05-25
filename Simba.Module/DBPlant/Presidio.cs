using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("PRESIDIO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Presidio")]
    [NavigationItem("Tabelle Anagrafiche")]
    [ImageName("Action_Navigation")]
    public class Presidio : XPObject
    {
        public Presidio()
            : base()
        {
        }

        public Presidio(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fCodice;
        [Size(20), Persistent("CODICE"), DisplayName("Codice")]
        [DbType("varchar(2)")]
        public string Codice
        {
            get
            {
                return fCodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fCodice, value);
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

        //[Association(@"Provincia_Regione", typeof(Provincia)),
        //DisplayName("Provincia")]
        //public XPCollection<Provincia> Provincias
        //{
        //    get
        //    {
        //        return GetCollection<Provincia>("Provincias");
        //    }
        //}

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

    }
}
