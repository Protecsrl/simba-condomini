
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_REGRDL_LIST")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro RdL Sfoglia")]
    [ImageName("Action_Debug_Step")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewTutto", "", "Tutto", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 5)]
    // [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    // [ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    #endregion

    public class RegRdLListView : XPLiteObject
    {
        public RegRdLListView() : base() { }
        public RegRdLListView(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        //Data_e_Ora_Min_EditMask CAMSEditorCostantFormat
        private int fcodice;
        [Key, Persistent("CODICE")]
        [Appearance("RegRdLListView.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        [Appearance("RegRdLListView.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        [Appearance("RegRdLListView.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RegRdLListView.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RegRdLListView.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]  
        public int Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<int>("Codice", ref fcodice, value);
            }
        }
  
        //private int fCodOdL;
        //[Persistent("CODICEODL"), System.ComponentModel.DisplayName("OdL")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public int CodOdL
        //{
        //    get
        //    {
        //        return fCodOdL;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CodOdL", ref fCodOdL, value);
        //    }
        //}

        private string fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Commessa
        {
            get
            {
                return fCommessa;
            }
            set
            {
                SetPropertyValue<string>("Commessa", ref fCommessa, value);
            }
        }

        
        private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Immobile
        {
            get
            {
                return fEdificio;
            }
            set
            {
                SetPropertyValue<string>("Immobile", ref fEdificio, value);
            }
        }

        private string fCodEdificio;
        [Persistent("CODEDIFICIO"), System.ComponentModel.DisplayName("Cod Immobile")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string CodEdificio
        {
            get
            {
                return fCodEdificio;
            }
            set
            {
                SetPropertyValue<string>("CodEdificio", ref fCodEdificio, value);
            }
        }

      


        private string fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Impianto
        {
            get
            {
                return fImpianto;
            }
            set
            {
                SetPropertyValue<string>("Impianto", ref fImpianto, value);
            }
        }

  
        private string fApparato;
        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<string>("Apparato", ref fApparato, value);
            }
        }

        private string fTipoApparato;
        [Persistent("TIPOAPPARATO"), System.ComponentModel.DisplayName("Tipo Apparato")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string TipoApparato
        {
            get
            {
                return fTipoApparato;
            }
            set
            {
                SetPropertyValue<string>("TipoApparato", ref fTipoApparato, value);
            }
        }
        private string fCategoria;
        [Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
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


        private string fDescrizione;
        [Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
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

        private string fUtente;
        [Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Utente
        {
            get
            {
                return fUtente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref fUtente, value);
            }
        }

        #region Date
        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Creazione dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
            }
        }

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataRichiesta
        {
            get
            {
                return fDataRichiesta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value);
            }
        }

        //private DateTime fDataAggiornamento;
        //[Persistent("DATAAGGIORNAMENTO"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data Aggiornamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[System.ComponentModel.Browsable(false)]
        //public DateTime DataAggiornamento
        //{
        //    get
        //    {
        //        return fDataAggiornamento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
        //    }
        //}

        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataPianificata
        {
            get
            {
                return fDataPianificata;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value);
            }
        }

        //private DateTime fDataAssegnazione;
        //[Persistent("DATAASSEGNAZIONE"), System.ComponentModel.DisplayName("Data Assegnazione")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public DateTime DataAssegnazione
        //{
        //    get
        //    {
        //        return fDataAssegnazione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataAssegnazione", ref fDataAssegnazione, value);
        //    }
        //}

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }
        #endregion

        #region  settimana mese anno
        private int fSettimana;
        [Persistent("SETTIMANA"), System.ComponentModel.DisplayName("Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<int>("Settimana", ref fSettimana, value);
            }
        }

        private int fMese;
        [Persistent("MESE"), System.ComponentModel.DisplayName("Mese")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
            }
        }

        private int fAnno;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<int>("Anno", ref fAnno, value);
            }
        }

        #endregion



        private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Team
        {
            get
            {
                return fTeam;
            }
            set
            {
                SetPropertyValue<string>("Team", ref fTeam, value);
            }
        }

        private string fTeamMansione;
        [Persistent("TEAMMANSIONE"), System.ComponentModel.DisplayName("Mansione")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string TeamMansione
        {
            get
            {
                return fTeamMansione;
            }
            set
            {
                SetPropertyValue<string>("TeamMansione", ref fTeamMansione, value);
            }
        }


        private string fStatoSmistamento;
        [Persistent("ULTIMOSTATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string StatoSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<string>("UltimoStatoSmistamento", ref fStatoSmistamento, value);
            }
        }

        private string fStatoOperativo;
        [Persistent("ULTIMOSTATOOPERATIVO"), System.ComponentModel.DisplayName("Stato Operativo")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
       
        public string StatoOperativo
        {
            get
            {
                return fStatoOperativo;
            }
            set
            {
                SetPropertyValue<string>("UltimoStatoOperativo", ref fStatoOperativo, value);
            }
        }
             
        
        //private string fRichiedente;
        //[Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        //[DbType("varchar(4000)")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string Richiedente
        //{
        //    get
        //    {
        //        return fRichiedente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Richiedente", ref fRichiedente, value);
        //    }
        //}

        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Priorita
        {
            get
            {
                return fPriorita;
            }
            set
            {
                SetPropertyValue<string>("Priorita", ref fPriorita, value);
            }
        }

        //private string fPrioritaTIntervento;
        //[Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(50)")]
        //public string PrioritaTIntervento
        //{
        //    get
        //    {
        //        return fPrioritaTIntervento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("PrioritaTIntervento", ref fPrioritaTIntervento, value);
        //    }
        //}
        
        private string fNoteCompletamento;
        [Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("Note Completamento")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string NoteCompletamento
        {
            get
            {
                return fNoteCompletamento;
            }
            set
            {
                SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value);
            }
        }

        #region OIDEDIFICIO, OIDREFERENTECOFELY
        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        //[MemberDesignTimeVisibility(false)]
        public int OidEdificio
        {
            get
            {
                return fOidEdificio;
            }
            set
            {
                SetPropertyValue<int>("OidEdificio", ref fOidEdificio, value);
            }
        }

        private int fOidReferenteCofely;
        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
       // [MemberDesignTimeVisibility(false)]
        public int OidReferenteCofely
        {
            get
            {
                return fOidReferenteCofely;
            }
            set
            {
                SetPropertyValue<int>("OidReferenteCofely", ref fOidReferenteCofely, value);
            }
        }


        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCommessa")] // pm
        //[MemberDesignTimeVisibility(false)]
        public int OidCommessa
        {
            get
            {
                return fOidCommessa;
            }
            set
            {
                SetPropertyValue<int>("OidCommessa", ref fOidCommessa, value);
            }
        }


        private int fOidCategoria;
        [Persistent("OIDCATEGORIA"), System.ComponentModel.DisplayName("OidCategoria")] // pm
        //[MemberDesignTimeVisibility(false)]
        public int OidCategoria
        {
            get
            {
                return fOidCategoria;
            }
            set
            {
                SetPropertyValue<int>("OidCategoria", ref fOidCategoria, value);
            }
        }

        private int fOidSmistamento;
        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] // pm
        //[MemberDesignTimeVisibility(false)]
        public int OidSmistamento
        {
            get
            {
                return fOidSmistamento;
            }
            set
            {
                SetPropertyValue<int>("OidSmistamento", ref fOidSmistamento, value);
            }
        }
        #endregion

    }
}
