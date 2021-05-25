using System;
using CAMS.Module.Costi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions,    Persistent("FORNITORE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Fornitori")]
    [NavigationItem("Gestione Contabilità")]
    [VisibleInDashboards(false)]
    public class Fornitore : XPObject
    {
        public Fornitore()
            : base()
        {
        }

        public Fornitore(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200),
        Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
        [RuleRequiredField("Fornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
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

        private string fCategoria;
        [Size(1000),
        Persistent("CATEGORIA"),
        DisplayName("Categoria")]
        [DbType("varchar(1000)")]
        public string Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<string>("Categoria", ref fCategoria, value);
            }
        }

        private string fIndirizzo;
        [Size(1000),   Persistent("INDIRIZZO"),        DisplayName("Indirizzo")]
        [DbType("varchar(1000)")]
        [ExplicitLoading]
        public string Indirizzo
        {
            get
            {
                return fIndirizzo;
            }
            set
            {
                SetPropertyValue<string>("Indirizzo", ref fIndirizzo, value);
            }
        }

        private string fPiva;
        [Size(1000),
        Persistent("P_IVA"),
        DisplayName("Partita Iva")]
        [DbType("varchar(1000)")]
        public string PartitaIva
        {
            get
            {
                return fPiva;
            }
            set
            {
                SetPropertyValue<string>("PartitaIva", ref fPiva, value);
            }
        }


      //  //[Association(@"RegistroCostiDettaglio_Fornitore", typeof(RegistroCostiDettaglio)),
      //[  DisplayName("Dettaglio Costi")]
      //  public XPCollection<RegistroCostiDettaglio> RegistroCostiDettaglios
      //  {
      //      get
      //      {
      //          return GetCollection<RegistroCostiDettaglio>("RegistroCostiDettaglios");
      //      }
      //  }

        public override string ToString()
        {
            return string.Format("{0}", Descrizione);
        }

    }
}
