

using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.SystemModule;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("RDL_LIST_GUASTO_SINOTTICO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Sinottico SLA")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]    //[System.ComponentModel.DefaultProperty("Descrizione")]    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
    [Indices("StatoSmistamento", "Commessa", "Immobile", "Impianto", "Priorita", "TIntervento", "SLASopralluogo", "SLARipristino")]
    #region filtro tampone                     
    //"OidSmistamento = 1", "In Attesa di Assegnazione", Index = 1)]
    [ListViewFilter("RegMisureDettaglioDett_1BM", "IsThisMonth([Data]) Or IsLastMonth([Data])", "ultimo Bimestre", true, Index = 1)]
    [ListViewFilter("RegMisureDettaglioDett_1TrimAnnoinCorso", "DateDiffMonth([Data],Today()) < 4", @"ultimo trimestre", Index = 2)]
    [ListViewFilter("RegMisureDettaglioDett_1SemAnnoinCorso", "DateDiffMonth([Data],Today()) < 7", @"ultimo semestre", Index = 3)]
    [ListViewFilter("RegMisureDettaglioDett_9meseAnnoinCorso", "DateDiffMonth([Data],Today()) < 10", @"ultimi nove mesi", Index = 4)]
    [ListViewFilter("RegMisureDettaglioDett_questoanno", "IsThisYear([Data])", @"questo Anno", Index = 5)]
    [ListViewFilter("RegMisureDettaglioDett_annoscrso", "IsLastYear([Data])", @"Anno scorso", Index = 6)]
    [ListViewFilter("RdLListViewGuastoSinottico.OidSmistamento_1-2-3-10-11", "StatoSmistamento In('In Attesa di Assegnazione','Assegnata','Gestione da Sala Operativa','In Emergenza da Assegnare','Emessa In lavorazione') ", "In Elaborazione SLA",  Index = 7)]
    [ListViewFilter("RdLListViewGuastoSinottico.smistamentoOt", "StatoSmistamento In('Lavorazione Conclusa','Annullato','Sospesa SO') ", "non In Elaborazione SLA",  Index = 8)]
    [ListViewFilter("RdLListViewGuastoSinottico", "", "Tutto", Index = 9)]
//    Gestione da Sala Operativa //Lavorazione Conclusa//In Attesa di Assegnazione//Emessa In lavorazione//Annullato
//Assegnata//In Emergenza da Assegnare//Sospesa SO
    #endregion

    public class RdLListViewGuastoSinottico : XPObject
    {
        public RdLListViewGuastoSinottico() : base() { }
        public RdLListViewGuastoSinottico(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        //private string fIndice;/*Key,*/
        //[ Persistent("IND"), System.ComponentModel.DisplayName("Indice")]
        //[DbType("varchar(1000)")]
        ////public string Indice
        ////{
        ////    get
        ////    {
        ////        return fIndice;
        ////    }
        ////    set
        ////    {
        ////        SetPropertyValue<string>("Indice", ref fIndice, value);
        ////    }
        ////}
        //[Delayed(true)]
        //public string Indice
        //{
        //    get { return GetDelayedPropertyValue<string>("Indice"); }
        //    set { SetDelayedPropertyValue<string>("Indice", value); }
        //}

        //private int fConto;
        [Persistent("CONTO"), System.ComponentModel.DisplayName("Conteggio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]        
        //public int Conto
        //{
        //    get
        //    {
        //        return fConto;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("Conto", ref fConto, value);
        //    }
        //}
        [Delayed(true)]
        public int Conto
        {
            get { return GetDelayedPropertyValue<int>("Conto"); }
            set { SetDelayedPropertyValue<int>("Conto", value); }
        }

        //private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(250)")]
        [Delayed(true)]
        public string StatoSmistamento
        {
            get { return GetDelayedPropertyValue<string>("StatoSmistamento"); }
            set { SetDelayedPropertyValue<string>("StatoSmistamento", value); }
        }

        //private string /*fTIntervento*/;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("StatoOperativo")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string StatoOperativo
        {
            get { return GetDelayedPropertyValue<string>("StatoOperativo"); }
            set { SetDelayedPropertyValue<string>("StatoOperativo", value); }
        }


        [Persistent("AREADIPOLO"), System.ComponentModel.DisplayName("Area di Polo")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string AreadiPolo
        {
            get { return GetDelayedPropertyValue<string>("AreadiPolo"); }
            set { SetDelayedPropertyValue<string>("AreadiPolo", value); }
        }
        //private string fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(400)")]
        //public string Commessa
        //{
        //    get
        //    {
        //        return fCommessa;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Commessa", ref fCommessa, value);
        //    }
        //}
        [Delayed(true)]
        public string Commessa
        {
            get { return GetDelayedPropertyValue<string>("Commessa"); }
            set { SetDelayedPropertyValue<string>("Commessa", value); }
        }

        //private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        //public string Immobile
        //{
        //    get
        //    {
        //        return fEdificio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Immobile", ref fEdificio, value);
        //    }
        //}
        [Delayed(true)]
        public string Immobile
        {
            get { return GetDelayedPropertyValue<string>("Immobile"); }
            set { SetDelayedPropertyValue<string>("Immobile", value); }
        }

        private string fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(400)")]
        //public string Impianto
        //{
        //    get
        //    {
        //        return fServizio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Impianto", ref fServizio, value);
        //    }
        //}
        [Delayed(true)]
        public string Impianto
        {
            get { return GetDelayedPropertyValue<string>("Impianto"); }
            set { SetDelayedPropertyValue<string>("Impianto", value); }
        }



        //private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]       
        //public string Priorita
        //{
        //    get
        //    {
        //        return fPriorita;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Priorita", ref fPriorita, value);
        //    }
        //}
        [Delayed(true)]
        public string Priorita
        {
            get { return GetDelayedPropertyValue<string>("Priorita"); }
            set { SetDelayedPropertyValue<string>("Priorita", value); }
        }



        private string fTIntervento;
        [Persistent("TIPOINTERVENTO"), System.ComponentModel.DisplayName("TIntervento")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]       
        //public string TIntervento
        //{
        //    get
        //    {
        //        return fTIntervento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("TIntervento", ref fTIntervento, value);
        //    }
        //}
        [Delayed(true)]
        public string TIntervento
        {
            get { return GetDelayedPropertyValue<string>("TIntervento"); }
            set { SetDelayedPropertyValue<string>("TIntervento", value); }
        }

        private string fSLASopralluogo;
        [Persistent("SLASOPRALLUOGO"), System.ComponentModel.DisplayName("SLA Tempo Intervento")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]       
        //public string SLASopralluogo
        //{
        //    get
        //    {
        //        return fSLASopralluogo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("SLASopralluogo", ref fSLASopralluogo, value);
        //    }
        //}
        [Delayed(true)]
        public string SLASopralluogo
        {
            get { return GetDelayedPropertyValue<string>("SLASopralluogo"); }
            set { SetDelayedPropertyValue<string>("SLASopralluogo", value); }
        }

        private string fSLARipristino;
        [Persistent("SLARIPRISTINO"), System.ComponentModel.DisplayName("SLA Tempo Ripristino")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]      
        
        //public string SLARipristino
        //{
        //    get
        //    {
        //        return fSLARipristino;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("SLARipristino", ref fSLARipristino, value);
        //    }
        //}
        [Delayed(true)]
        public string SLARipristino
        {
            get { return GetDelayedPropertyValue<string>("SLARipristino"); }
            set { SetDelayedPropertyValue<string>("SLARipristino", value); }
        }

        //private string fMese;
        //[Persistent("MESE"), System.ComponentModel.DisplayName("Mese")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DbType("varchar(2)")]
        ////public string Mese
        ////{
        ////    get
        ////    {
        ////        return fMese;
        ////    }
        ////    set
        ////    {
        ////        SetPropertyValue<string>("Mese", ref fMese, value);
        ////    }
        ////}
        //[Delayed(true)]
        //public string Mese
        //{
        //    get { return GetDelayedPropertyValue<string>("Mese"); }
        //    set { SetDelayedPropertyValue<string>("Mese", value); }
        //}


        //private string fAnno;
        //[Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DbType("varchar(4)")]
        ////public string Anno
        ////{
        ////    get
        ////    {
        ////        return fAnno;
        ////    }
        ////    set
        ////    {
        ////        SetPropertyValue<string>("Anno", ref fAnno, value);
        ////    }
        ////}
        //[Delayed(true)]
        //public string Anno
        //{
        //    get { return GetDelayedPropertyValue<string>("Anno"); }
        //    set { SetDelayedPropertyValue<string>("Anno", value); }
        //}

        [Persistent("DATA"), System.ComponentModel.DisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Delayed(true)]
        public DateTime Data
        {
            get { return GetDelayedPropertyValue<DateTime>("Data"); }
            set { SetDelayedPropertyValue<DateTime>("Data", value); }

        }



    }

}
