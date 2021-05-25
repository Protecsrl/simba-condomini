using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("DISTRETTO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Distretto")]
    [NavigationItem("Tabelle Anagrafiche")]
    [ImageName("Action_Navigation")]
    public class Distretto : XPObject
    {
        public Distretto()
            : base()
        {
        }

        public Distretto(Session session)
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

        private string fCodiceOut;
        [Size(50), Persistent("COD_OUT"), DisplayName("CodiceOut")]
        [DbType("varchar(50)")]
        public string CodiceOut
        {
            get
            {
                return fCodiceOut;
            }
            set
            {
                SetPropertyValue<string>("CodiceOut", ref fCodiceOut, value);
            }
        }

    }
}