using System;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;

namespace CAMS.Module.DBCensimento
{
    [DefaultClassOptions, Persistent("CENSIMENTOBASEAPP")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Censimento Base Apparati")]
    [ImageName("BO_Organization")]
    //[DeferredDeletion(true)]
    //[RuleCombinationOfPropertiesIsUnique("Unique.CENSIMENTOBASE.Descrizione", DefaultContexts.Save, "Commesse,CodDescrizione, Descrizione")]
    //[RuleCriteria("RuleWarning.Ed.Indirizzo.Geo.nonPre", DefaultContexts.Save, @"IndirizzoGeoValorizzato > 0",
    //  CustomMessageTemplate =
    //  "Attenzione:La L'indirizzo Selezionato non ha riferimenti di Georeferenziazione, \n\r inserirli prima di continuare!{IndirizzoGeoValorizzato}",
    //   SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]

    #region Abilitazione

    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("CensimentoBase_SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("CensimentoBase_SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("CensimentoBase_Tutto", "", "Tutto", Index = 2)]

    #endregion
    [NavigationItem("Censimento")]
    public class CensimentoBaseApp : XPObject
    {
        private const string NA = "N/A";
        private const string FormattazioneCodice = "{0:000}";

        public CensimentoBaseApp() : base() { }
        public CensimentoBaseApp(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
      

        //----------------------------
        private int foidcommessa;
        [Persistent("OIDCOMMESSA"), Size(100), DevExpress.Xpo.DisplayName("Oid Commessa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [Delayed(true)]
        public int OidCommessa
        {
            get { return GetDelayedPropertyValue<int>("OidCommessa"); }
            set { SetDelayedPropertyValue<int>("OidCommessa", value); }
        }

        private string fDescrizione_Commessa;
        [Persistent("COMMESSA_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Descrizione Commessa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Descrizione_Commessa
        {
            get { return GetDelayedPropertyValue<string>("Descrizione_Commessa"); }
            set { SetDelayedPropertyValue<string>("Descrizione_Commessa", value); }
        }

        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), DevExpress.Xpo.DisplayName("OidEdificio")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int OidEdificio
        {
            get { return GetDelayedPropertyValue<int>("OidEdificio"); }
            set { SetDelayedPropertyValue<int>("OidEdificio", value); }
        }

        private string fEdificio_Descrizione;
        [Persistent("EDIFICIO_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Descrizione Immobile")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Edificio_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Edificio_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Edificio_Descrizione", value); }
        }

        private int fOidImpianto;
        [Persistent("OIDIMPIANTO"), DevExpress.Xpo.DisplayName("OidImpianto")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int OidImpianto
        {
            get { return GetDelayedPropertyValue<int>("OidImpianto"); }
            set { SetDelayedPropertyValue<int>("OidImpianto", value); }
        }

        private string fImpianto_Descrizione;
        [Persistent("IMPIANTO_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Impianto Descrizione")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Impianto_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Impianto_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Impianto_Descrizione", value); }
        }

        private string fApparato_Descrizione;
        [Persistent("APPARATO_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Apparato Descrizione")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Apparato_Descrizione
        {
            get { return GetDelayedPropertyValue<string>("Apparato_Descrizione"); }
            set { SetDelayedPropertyValue<string>("Apparato_Descrizione", value); }
        }

        private int fOidApparatostd;
        [Persistent("OIDAPPARATOSTD"), DevExpress.Xpo.DisplayName("OidApparatostd")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int OidApparatostd
        {
            get { return GetDelayedPropertyValue<int>("OidApparatostd"); }
            set { SetDelayedPropertyValue<int>("OidApparatostd", value); }
        }

        private int fNApparati;
        [Persistent("NRAPPARATI"), DevExpress.Xpo.DisplayName("Nr Apparati per tipologia")]
        [VisibleInListView(true), VisibleInLookupListView(true), VisibleInDetailView(true)]
        //[DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public int NApparati
        {
            get { return GetDelayedPropertyValue<int>("NApparati"); }
            set { SetDelayedPropertyValue<int>("NApparati", value); }
        }


        private DateTime fDataAggiornamento;
        [Persistent("DATAAGGIORNAMENTO")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(200),
        DisplayName("Utente")]
        [DbType("varchar(200)")]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }









    }
}

