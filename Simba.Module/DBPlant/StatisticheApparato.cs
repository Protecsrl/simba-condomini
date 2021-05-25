using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.DBDocument;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant.Coefficienti;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("STATISTICHEAPPARATO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Apparati Statistiche")]
    //[Indices("AbilitazioneEreditata;Abilitato")]
    #region filtri
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_Reali", "Abilitato == 1 And AbilitazioneEreditata == 1 And EntitaApparato == 1", "Attivi solo Reali", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_Virtuali", "Abilitato == 1 And AbilitazioneEreditata == 1 And EntitaApparato == 0", "Attivi solo Raggruppamento", Index = 2)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_mp", "Abilitato == 1 And AbilitazioneEreditata == 1 And EntitaApparato == 2", "Attivi solo Virtuale", Index = 3)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", Index = 4)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 5)]
    #endregion

    [ImageName("LoadPageSetup")]
    [NavigationItem("Patrimonio")]
    public class StatisticheApparato : XPObject
    {
        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        public StatisticheApparato() : base() { }
        public StatisticheApparato(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        public StatisticheApparato(Session session, string name) : base(session) {}

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), DbType("varchar(1000)")]  
        [Delayed(true)]
        public string Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Descrizione"); }
            set { SetDelayedPropertyValue<string>("Descrizione", value); }
        }
        //public string Descrizione
        //{
        //    get
        //    {
        //        return fDescrizione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
        //    }
        //}

        [Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }

        [Persistent("SERVIZIO"), DevExpress.Xpo.DisplayName("Servizio")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }

        [Persistent("STDAPPARATO"), DevExpress.Xpo.DisplayName("Tipo Apparato")]
        [Delayed(true)]
        public StdAsset StdApparato
        {
            get { return GetDelayedPropertyValue<StdAsset>("StdApparato"); }
            set { SetDelayedPropertyValue<StdAsset>("StdApparato", value); }
        }

        private int fQuantita;
        [Persistent("QUANTITA"),
        DevExpress.Xpo.DisplayName("Quantità"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [Delayed(true)]
        public int Quantita
        {
            get { return GetDelayedPropertyValue<int>("Quantita"); }
            set { SetDelayedPropertyValue<int>("Quantita", value); }
        }

        private string fMarca;
        [Persistent("MARCA"), Size(100), DevExpress.Xpo.DisplayName("Marca")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        public string Marca
        {
            get { return GetDelayedPropertyValue<string>("Marca"); }
            set { SetDelayedPropertyValue<string>("Marca", value); }
        }

        private string fModello;
        [Persistent("MODELLO"), Size(100), DevExpress.Xpo.DisplayName("Modello")]
        [DbType("varchar(100)")]
        //[VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Modello
        {
            get { return GetDelayedPropertyValue<string>("Modello"); }
            set { SetDelayedPropertyValue<string>("Modello", value); }
        }

        private Locali fLocale;
        [Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
        [Delayed(true)]
        public Locali Locale
        {
            get { return GetDelayedPropertyValue<Locali>("Locale"); }
            set { SetDelayedPropertyValue<Locali>("Locale", value); }
            //get { return fLocale; }
            //set { SetPropertyValue<Locali>("Locale", ref fLocale, value); }
        }

        private double fPotenza;
        [Persistent("POTENZA"), DevExpress.Xpo.DisplayName("Potenza(W)")]
        //[Appearance("ApparatoCaratteristicheTecniche.ValoreIntero", AppearanceItemType.LayoutItem,
        //    @"StdApparatoCaratteristicheTecniche.TipoValoreCaratteristicaTecnica != 'Intero'", Visibility = ViewItemVisibility.Hide)]
        //[VisibleInListView(false)]
        //[ImmediatePostData]
        public double Potenza
        {
            get
            {

                return fPotenza;
            }
            set
            {
                SetPropertyValue<double>("Potenza", ref fPotenza, value);
            }
        }

    }
}
