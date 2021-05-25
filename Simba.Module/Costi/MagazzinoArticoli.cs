using System;
using CAMS.Module.Costi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Model;

//namespace CAMS.Module.Costi
//{
//    class MagazzinoArticoli
//    {
//    }
//}
namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions,
    Persistent("MAGAZZINO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Magazzino Articoli")]
    [NavigationItem("Gestione Contabilità")]
    public class MagazzinoArticoli : XPObject
    {
        public MagazzinoArticoli()
            : base()
        {
        }

        public MagazzinoArticoli(Session session)
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
        [RuleRequiredField("MagazzinoArticoliFornitore.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
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
        Persistent("CATEGORIA"),        DisplayName("Categoria")]
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

        private string fFamiglia;
        [Size(1000), Persistent("FAMIGLIA"), DisplayName("Famiglia")]
        [DbType("varchar(1000)")]
        [ExplicitLoading]
        public string Famiglia
        {
            get
            {
                return fFamiglia;
            }
            set
            {
                SetPropertyValue<string>("Famiglia", ref fFamiglia, value);
            }
        }

        private double fPrezzo;
        [Persistent("PREZZO"), DisplayName("Prezzo")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        public double Prezzo
        {
            get
            {
                return fPrezzo;
            }
            set
            {
                SetPropertyValue<double>("Prezzo", ref fPrezzo, value);
            }
        }



        //private string fPiva;
        //[Size(1000),
        //Persistent("P_IVA"),
        //DisplayName("Partita Iva")]
        //[DbType("varchar(1000)")]
        //public string PartitaIva
        //{
        //    get
        //    {
        //        return fPiva;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("PartitaIva", ref fPiva, value);
        //    }
        //}


        //[Association(@"RegistroCostiDettaglio_Fornitore", typeof(RegistroCostiDettaglio)),
        //DisplayName("Dettaglio Costi")]
        //public XPCollection<RegistroCostiDettaglio> RegistroCostiDettaglios
        //{
        //    get
        //    {
        //        return GetCollection<RegistroCostiDettaglio>("RegistroCostiDettaglios");
        //    }
        //}

        public override string ToString()
        {
            return string.Format("{0}", Descrizione);
        }

    }
}
