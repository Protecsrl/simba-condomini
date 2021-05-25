
using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LIST_CL")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Elenco Interventi")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone                     "OidSmistamento = 1", "In Attesa di Assegnazione", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente.OidSmistamento_1", "OidSmistamento = 1", "In Attesa di Assegnazione", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente", "", "Tutto", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente.OidSmistamento_2", "OidSmistamento = 2", "Assegnata", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente.OidSmistamento_3", "OidSmistamento = 3", "Emessa In lavorazione", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente.OidSmistamento_6", "OidSmistamento = 6", "Sospesa SO", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente.OidSmistamento_10", "OidSmistamento = 10", "In Emergenza da Assegnare", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente.OidSmistamento_11", "OidSmistamento = 11", "Gestione da Sala Operativa", Index = 6)]


    #endregion

    public class RdLListViewCliente : XPLiteObject
    {
        public RdLListViewCliente() : base() { }
        public RdLListViewCliente(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Appearance("RdLListViewCliente.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        [Appearance("RdLListViewCliente.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        [Appearance("RdLListViewCliente.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdLListViewCliente.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdLListViewCliente.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]

        //[Appearance("RdLListViewCliente.StatoSmistamento.Enabled", TargetItems = "StatoSmistamento", Enabled = false)]
        //[Appearance("RdLListViewCliente.DataRichiesta.Enabled", TargetItems = "DataRichiesta", Enabled = false)]

        public int Codice
        {
            get { return fcodice; }
            set { SetPropertyValue<int>("Codice", ref fcodice, value); }
        }

        private int fCodRegRdL;
        [Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int CodRegRdL
        {
            get { return fCodRegRdL; }
            set { SetPropertyValue<int>("CodRegRdL", ref fCodRegRdL, value); }
        }


        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string StatoSmistamento
        {
            get { return fStatoSmistamento; }
            set { SetPropertyValue<string>("StatoSmistamento", ref fStatoSmistamento, value); }
        }



        private string fContratto;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Contratto")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(400)")]
        public string Contratto
        {
            get { return fContratto; }
            set { SetPropertyValue<string>("Contratto", ref fContratto, value); }
        }


        private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        public string Immobile
        {
            get { return fEdificio; }
            set { SetPropertyValue<string>("Immobile", ref fEdificio, value); }
        }

        private string fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Servizio
        {
            get { return fServizio; }
            set { SetPropertyValue<string>("Servizio", ref fServizio, value); }
        }

        private string fAsset;
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Asset
        {
            get { return fAsset; }
            set { SetPropertyValue<string>("Asset", ref fAsset, value); }
        }

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Intervento")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Team
        {
            get { return fTeam; }
            set { SetPropertyValue<string>("Team", ref fTeam, value); }
        }


        //        private string fRitardo;
        //[Persistent("RITARDO"), System.ComponentModel.DisplayName("Ritardo")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        //[Appearance("RdLGuasto.Ritardo.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([Ritardo],'(1)')")]
        //[Appearance("RdLGuasto.Ritardo.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([Ritardo],'(2)')")]
        //[Appearance("RdLGuasto.Ritardo.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([Ritardo],'(3)')")]
        //[Appearance("RdLGuasto.Ritardo.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([Ritardo],'(4)')")]
        //public string Ritardo
        //{
        //    get
        //    {
        //        return fRitardo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Ritardo", ref fRitardo, value);
        //    }
        //}

        private string fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)")]
        public string Richiedente
        {
            get { return fRichiedente; }
            set { SetPropertyValue<string>("Richiedente", ref fRichiedente, value); }
        }

        private string fUtenteInserimento;
        [Size(4000), Persistent("UTENTEINSERIMENTO"), System.ComponentModel.DisplayName("Utente Inserimento")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        public string UtenteInserimento
        {
            get { return fUtenteInserimento; }
            set { SetPropertyValue<string>("UtenteInserimento", ref fUtenteInserimento, value); }
        }



        private string fSolleciti;
        [Persistent("SOLLECITI"), System.ComponentModel.DisplayName("Solleciti")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(10)")]
        public string Solleciti
        {
            get { return fSolleciti; }
            set { SetPropertyValue<string>("Solleciti", ref fSolleciti, value); }
        }

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataRichiesta
        {
            get { return fDataRichiesta; }
            set { SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value); }
        }


        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataPianificata
        {
            get { return fDataPianificata; }
            set { SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value); }
        }

        private DateTime fDataSopralluogo;
        [Persistent("DATASOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Sopralluogo ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataSopralluogo
        {
            get { return fDataSopralluogo; }
            set { SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value); }
        }


        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(200)")]
        public string Priorita
        {
            get { return fPriorita; }
            set { SetPropertyValue<string>("Priorita", ref fPriorita, value); }
        }


        private string fCategoria;
        [Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Categoria
        {
            get { return fCategoria; }
            set { SetPropertyValue<string>("Categoria", ref fCategoria, value); }
        }

        private string fPiano;
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(400)")]
        public string Piano
        {
            get { return fPiano; }
            set { SetPropertyValue<string>("Piano", ref fPiano, value); }
        }


        private string fLocale;
        [Persistent("LOCALE"), System.ComponentModel.DisplayName("Locale")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(400)")]
        public string Locale
        {
            get { return fLocale; }
            set { SetPropertyValue<string>("Locale", ref fLocale, value); }
        }

        private string fNoteCompletamento;
        [Size(4000), Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("Note Completamento")]
        [DbType("varchar(4000)")]
        [VisibleInListView(true)]
        public string NoteCompletamento
        {
            get { return fNoteCompletamento; }
            set { SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value); }
        }


        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Completamento Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataCompletamento
        {
            get { return fDataCompletamento; }
            set { SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value); }
        }



        #region OIDEDIFICIO, OIDREFERENTECOFELY, OIDAREADIPOLO

        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        //oidareadipolo
        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCOMMESSA")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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

        private int fOidSmistamento;
        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] // pm
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
