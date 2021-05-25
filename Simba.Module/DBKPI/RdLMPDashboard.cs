using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;
namespace CAMS.Module.DBCallCenter
{
    [DefaultClassOptions, Persistent("RDLMPDBOARD")]  
    [Indices("DescSLA01","DescSLA02", "DataPianificata")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL MP DashBoard")]
    [ImageName("ShowTestReport")]
    [NavigationItem("Segnalazioni")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    public class RdLMPDashboard : XPObject
    {
        public RdLMPDashboard() : base() { }
        public RdLMPDashboard(Session session) : base(session) { }
        //public override void AfterConstruction()
        //{
        //}

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInListView(false)]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }


        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }

        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public Asset Apparato
        {
            get { return GetDelayedPropertyValue<Asset>("Apparato"); }
            set { SetDelayedPropertyValue<Asset>("Apparato", value); }
        }

        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public Richiedente Richiedente
        {
            get { return GetDelayedPropertyValue<Richiedente>("Richiedente"); }
            set { SetDelayedPropertyValue<Richiedente>("Richiedente", value); }
        }

        #region Piano e stanza
        [VisibleInDashboards(false)]
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [Delayed(true)]
        public Piani Piano
        {
            get { return GetDelayedPropertyValue<Piani>("Piano"); }
            set { SetDelayedPropertyValue<Piani>("Piano", value); }
        }

        [Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public Locali Locale
        {
            get { return GetDelayedPropertyValue<Locali>("Locale"); }
            set { SetDelayedPropertyValue<Locali>("Locale", value); }
        }

        #endregion
        [Persistent("CATEGORIA"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public Categoria Categoria
        {
            get { return GetDelayedPropertyValue<Categoria>("Categoria"); }
            set { SetDelayedPropertyValue<Categoria>("Categoria", value); }
        }

        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorità")]
        [Delayed(true)]
        public Urgenza Priorita
        {
            get { return GetDelayedPropertyValue<Urgenza>("Priorita"); }
            set { SetDelayedPropertyValue<Urgenza>("Priorita", value); }
        }

        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        [Delayed(true)]
        [VisibleInDashboards(false)]
        public StatoSmistamento StatoSmistamento
        {
            get { return GetDelayedPropertyValue<StatoSmistamento>("StatoSmistamento"); }
            set { SetDelayedPropertyValue<StatoSmistamento>("StatoSmistamento", value); }
        }

        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]   
        [Delayed(true)]
        public DateTime DataPianificata
        {
            get { return GetDelayedPropertyValue<DateTime>("DataPianificata"); }
            set { SetDelayedPropertyValue<DateTime>("DataPianificata", value); }

        }

        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Delayed(true)]
        public DateTime DataCompletamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataCompletamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataCompletamento", value); }

        }

        private string fDescrizione;
        [Size(1024), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        [DbType("varchar(1024)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

         [Size(8), Persistent("ANNOMESE"), System.ComponentModel.DisplayName("AnnoMese")]
        [DbType("varchar(8)")]        
        [Delayed(true)]
        public string AnnoMese
        {
            get { return GetDelayedPropertyValue<string>("AnnoMese"); }
            set { SetDelayedPropertyValue<string>("AnnoMese", value); }
        }

         [ Persistent("COUNT"), System.ComponentModel.DisplayName("Conta")]
        [Delayed(true)]
        public int Count
        {
            get { return GetDelayedPropertyValue<int>("Count"); }
            set { SetDelayedPropertyValue<int>("Count", value); }
        }

        //[Persistent("RDL"), DisplayName("RdL")]
        //[Delayed(true)]
        //public RdL RdL
        //{
        //    get { return GetDelayedPropertyValue<RdL>("RdL"); }
        //    set { SetDelayedPropertyValue<RdL>("RdL", value); }
        //}



        [Size(1024), Persistent("DESCSLA01"), System.ComponentModel.DisplayName("SLA 01")]
        [DbType("varchar(1024)")]   
        [Delayed(true)]
        public string DescSLA01
        {
            get { return GetDelayedPropertyValue<string>("DescSLA01"); }
            set { SetDelayedPropertyValue<string>("DescSLA01", value); }
        }

        private string fDescSLA02;
        [Size(1024), Persistent("DESCSLA02"), System.ComponentModel.DisplayName("SLA 02")]
        [DbType("varchar(1024)")]  
        [Delayed(true)]
        public string DescSLA02
        {
            get { return GetDelayedPropertyValue<string>("DescSLA02"); }
            set { SetDelayedPropertyValue<string>("DescSLA02", value); }
        }

    }
}
