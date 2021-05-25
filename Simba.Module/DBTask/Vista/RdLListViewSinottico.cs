using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
//using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    //                                 v_rdl_list_guasto_sinottico
    [DefaultClassOptions, Persistent("RDL_LIST_SINOTTICO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Richieste di Lavoro Sinottico")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem(false)]
    [Indices("OidCommessa;DataPianificata", "Anno", "Mese", "Anno;Mese", "Settimana", "Anno;Mese;Settimana", "StatoSmistamento", "AreadiPolo", "CentrodiCosto", "StatoSmistamento;AreadiPolo;CentrodiCosto", "Categoria", "DataCreazione", "DataRichiesta", "DataPianificata", "DataCompletamento", "OidCategoria", "OidCommessa", "OidEdificio")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]"Interventi"
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    // [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_Ultimi3mesi", "[DataRichiesta] >= AddMonths(LocalDateTimeThisMonth(), -3) And [DataRichiesta] <= LocalDateTimeThisMonth()", "Ultimi 3 Mesi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_Ultimi3mesi", "DateDiffMonth([DataRichiesta],Today()) In(0,1,2,3)", "Ultimi 3 Mesi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_Ultimi6mesi", "[DataRichiesta] >= AddMonths(LocalDateTimeThisMonth(), -6) And [DataRichiesta] <= LocalDateTimeThisMonth()", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_Ultimi6mesi", "DateDiffMonth([DataRichiesta],Today()) In(0,1,2,3,4,5,6)", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_Ultimi9mesi", "DateDiffMonth([DataRichiesta],Today()) In(0,1,2,3,4,5,6,7,8,9)", "Ultimi 9 Mesi", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_AnnoinCorso", "IsThisYear([DataRichiesta])", "Anno in Corso", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_AnnoScorso", "IsLastYear([DataRichiesta])", "Anno Scorso", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_Anno3indietro", "DateDiffYear([DataRichiesta],Today()) = 3", "3 Anni Scorsi", Index = 5)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewTutto", "", "Tutto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_1TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(1,2,3) And IsThisYear([DataRichiesta])", @"1° Trimestre", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_2TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(4,5,6) And IsThisYear([DataRichiesta])", @"2° Trimestre", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_3TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(7,8,9) And IsThisYear([DataRichiesta])", @"3° Trimestre", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_4TrimAnnoinCorso", "GetMonth([DataRichiesta]) In(10,11,12) And IsThisYear([DataRichiesta])", @"4° Trimestre", Index = 9)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_1TrimAnnoScorso", "GetMonth([DataRichiesta]) In(1,2,3) And IsLastYear([DataRichiesta])", @"1° Trimestre (Anno Scorso)", Index = 10)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_2TrimAnnoScorso", "GetMonth([DataRichiesta]) In(4,5,6) And IsLastYear([DataRichiesta])", @"2° Trimestre (Anno Scorso)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_3TrimAnnoScorso", "GetMonth([DataRichiesta]) In(7,8,9) And IsLastYear([DataRichiesta])", @"3° Trimestre (Anno Scorso)", Index = 12)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_4TrimAnnoScorso", "GetMonth([DataRichiesta]) In(10,11,12) And IsLastYear([DataRichiesta])", @"4° Trimestre (Anno Scorso)", Index = 13)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotView_futuro", "DateDiffMonth([DataRichiesta],LocalDateTimeThisMonth()) < 0", "Mesi prossimi",  Index = 14)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 13)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 14)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 15)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 16)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 17)]

    #endregion

    public class RdLListViewSinottico : XPObject
    {
        public RdLListViewSinottico() : base() { }
        public RdLListViewSinottico(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Immobile { get; set; }

        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Commessa { get; set; }

        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Piano { get; set; }


        [Persistent("STANZA"), System.ComponentModel.DisplayName("Locale")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Stanza { get; set; }

        [Persistent("REPARTO"), System.ComponentModel.DisplayName("Reparto")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Reparto { get; set; }

        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Impianto { get; set; }

        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
          public string Apparato { get; set; }

        [Persistent("TIPOAPPARATO"), System.ComponentModel.DisplayName("Tipo Apparato")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
           public string TipoApparato { get; set; }


        [Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Categoria { get; set; }

        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Team { get; set; }

        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string StatoSmistamento { get; set; }

        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("StatoOperativo")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string StatoOperativo { get; set; }

        #region problema causa rimedio
        [Persistent("PROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Problema { get; set; }

        [Persistent("CAUSA"), System.ComponentModel.DisplayName("Causa")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Causa { get; set; }


        [Persistent("RIMEDIO"), System.ComponentModel.DisplayName("Rimedio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Rimedio { get; set; }
        #endregion

        #region Date
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Creazione dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
          public DateTime DataCreazione { get; set; }

        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataRichiesta { get; set; }

        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)] 
        public DateTime DataPianificata { get; set; }

        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataCompletamento { get; set; }

        #endregion
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [DbType("varchar(1024)")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Richiedente { get; set; }

        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string Priorita { get; set; }

        [Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        public string PrioritaTIntervento { get; set; }

        [Persistent("REFAMMINISTRATIVO"), System.ComponentModel.DisplayName("Amministrazione Cliente")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string RefAmministrativoCliente { get; set; }

        // Area di Polo
        [Persistent("AREADIPOLO"), System.ComponentModel.DisplayName("Area di Polo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
               public string AreadiPolo { get; set; }

        [Persistent("CENTROCOSTO"), System.ComponentModel.DisplayName("Centro di Costo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CentrodiCosto { get; set; }

        #region  settimana mese anno
        [Persistent("SETTIMANA"), System.ComponentModel.DisplayName("Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Settimana { get; set; }


        [Persistent("MESE"), System.ComponentModel.DisplayName("Mese")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
          public int Mese { get; set; }

        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Anno { get; set; }
        #endregion

        [Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Utente { get; set; }

        // SOLLECITI
        [Persistent("SOLLECITI"), System.ComponentModel.DisplayName("Solleciti")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Solleciti { get; set; }

        #region OIDEDIFICIO, OIDREFERENTECOFELY
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [MemberDesignTimeVisibility(false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]   
        public int OidEdificio    { get; set; }
     

        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        // [MemberDesignTimeVisibility(false)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OidReferenteCofely { get; set; }

        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCommessa")] // pm
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[MemberDesignTimeVisibility(false)]
        public int OidCommessa { get; set; }

        [Persistent("OIDCATEGORIA"), System.ComponentModel.DisplayName("OidCategoria")] // pm
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[MemberDesignTimeVisibility(false)]
        public int OidCategoria { get; set; }

        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(false)]
        public int OidSmistamento { get; set; }

        #endregion

    }
}