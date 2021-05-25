using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("PROVINCIA")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")] //Provincia_DetailView_Copy  IsNewObject(This) AND
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Provincia")]
    [Appearance("Provincia.Regione.Disable", AppearanceItemType = "ViewItem", 
                TargetItems = "Regione", Criteria = "Regione <> null",
                    Context = "Provincia_DetailView_Copy", Enabled = false)]
    [NavigationItem(false)]
    [ImageName("Action_Navigation")]
    public class Provincia : XPObject
    {
        public Provincia()
            : base()
        {
        }

        public Provincia(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
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

        private string fSigla;
        [Size(2), Persistent("SIGLA"), DisplayName("Sigla")]
        [DbType("varchar(2)")]
        public string Sigla
        {
            get
            {
                return fSigla;
            }
            set
            {
                SetPropertyValue<string>("Sigla", ref fSigla, value);
            }
        }

        private string fCodProvincia;
        [Size(3), Persistent("CODICEPROVINCIA"), DisplayName("Codice Provincia")]
        [DbType("varchar(3)")]
        public string CodProvincia
        {
            get
            {
                return fCodProvincia;
            }
            set
            {
                SetPropertyValue<string>("CodProvincia", ref fCodProvincia, value);
            }
        }

        //[Association(@"Citta_Provincia", typeof(Citta)),
        //DisplayName("Citta")]
        //public XPCollection<Citta> Cittas
        //{
        //    get
        //    {
        //        return GetCollection<Citta>("Cittas");
        //    }
        //}

        [Association(@"Comuni_Provincia", typeof(Comuni)), DisplayName("Comuni")]
        public XPCollection<Comuni> Comunis
        {
            get
            {
                return GetCollection<Comuni>("Comunis");
            }
        }

        private Regione fRegione;
        [Association(@"Provincia_Regione"),
        Persistent("REGIONE"),
        DisplayName("Regione")]
        public Regione Regione
        {
            get
            {
                return fRegione;
            }
            set
            {
                SetPropertyValue<Regione>("Regione", ref fRegione, value);
            }
        }

        public override string ToString()
        {
            return string.Format ("{0}", this.Sigla);
        }
    }
}
