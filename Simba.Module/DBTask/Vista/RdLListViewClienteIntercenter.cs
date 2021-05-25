using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LIST_CL_INTERCENT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Elenco  MOR")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
//    STATO DESCRIZIONESTATO
//IN Inserita-
//Inserita NULL
//PV Presa Visione/Esecuzione-
//SO  Sospesa
//RE  Respinta
//ES  Eseguita
//LA  In lavorazione -
    #region filtro tampone                     "OidSmistamento = 1", "In Attesa di Assegnazione", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewClienteIntercenter.OidSmistamento_1", "StatoSmistamento = 'Inserita'", "IN Inserita", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewCliente", "", "Tutto", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewClienteIntercenter.OidSmistamento_2", "StatoSmistamento = 'Presa Visione/Esecuzione'", "PV Presa Visione/Esecuzione", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewClienteIntercenter.OidSmistamento_3", "StatoSmistamento = 'In lavorazione'", "LA  In lavorazione", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewClienteIntercenter.OidSmistamento_6", "StatoSmistamento = 'Sospesa'", "SO  Sospesa", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewClienteIntercenter.OidSmistamento_10", "StatoSmistamento = 'Eseguita'", "ES  Eseguita", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewClienteIntercenter.OidSmistamento_11", "StatoSmistamento = 'Respinta'", "RE  Respinta", Index = 6)]


    #endregion

    public class RdLListViewClienteIntercenter : XPLiteObject
    {
        public RdLListViewClienteIntercenter() : base() { }
        public RdLListViewClienteIntercenter(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Appearance("RdLListViewClienteIntercenter.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        [Appearance("RdLListViewClienteIntercenter.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        [Appearance("RdLListViewClienteIntercenter.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdLListViewClienteIntercenter.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdLListViewClienteIntercenter.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
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

        private int fCodRegRdL;
        [Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("RdLListViewClienteIntercenter.CodRegRdL.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        [Appearance("RdLListViewClienteIntercenter.CodRegRdL.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        [Appearance("RdLListViewClienteIntercenter.CodRegRdL.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdLListViewClienteIntercenter.CodRegRdL.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdLListViewClienteIntercenter.CodRegRdL.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]

        public int CodRegRdL
        {
            get
            {
                return fCodRegRdL;
            }
            set
            {
                SetPropertyValue<int>("CodRegRdL", ref fCodRegRdL, value);
            }
        }

        private string fMOR;
        [Persistent("MOR"), System.ComponentModel.DisplayName("MOR"),Size(15)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        public string MOR
        {
            get
            {
                return fMOR;
            }
            set
            {
                SetPropertyValue<string>("MOR", ref fMOR, value);
            }
        }

        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string StatoSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<string>("StatoSmistamento", ref fStatoSmistamento, value);
            }
        }



        private string fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(400)")]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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

        private string fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
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

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Intervento")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
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


        private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(250)")]
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

               

        private string fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)")]
        public string Richiedente
        {
            get
            {
                return fRichiedente;
            }
            set
            {
                SetPropertyValue<string>("Richiedente", ref fRichiedente, value);
            }
        }

        private string fLocale;
        [Size(250), Persistent("LOCALE"), System.ComponentModel.DisplayName("Locale")]
        //[DbType("varchar(4000)")]
        [VisibleInListView(true)]
        public string Locale
        {
            get
            {
                return fLocale;
            }
            set
            {
                SetPropertyValue<string>("Locale", ref fLocale, value);
            }
        }

        private string fUtenteInserimento;
        [Size(100), Persistent("UTENTEINSERIMENTO"), System.ComponentModel.DisplayName("Utente Inserimento")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        public string UtenteInserimento
        {
            get
            {
                return fUtenteInserimento;
            }
            set
            {
                SetPropertyValue<string>("UtenteInserimento", ref fUtenteInserimento, value);
            }
        }



        private string fSolleciti;
        [Persistent("SOLLECITI"), System.ComponentModel.DisplayName("Solleciti")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(10)")]
        public string Solleciti
        {
            get
            {
                return fSolleciti;
            }
            set
            {
                SetPropertyValue<string>("Solleciti", ref fSolleciti, value);
            }
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
            get
            {
                return fDataRichiesta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value);
            }
        }


        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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

        //private DateTime fDataSopralluogo;
        //[Persistent("DATASOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data Sopralluogo ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public DateTime DataSopralluogo
        //{
        //    get
        //    {
        //        return fDataSopralluogo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value);
        //    }
        //}


        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(200)")]
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


        private string fCategoria;
        [Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(true)]
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

        private string fNoteCompletamento;
        [Size(4000), Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("Note Completamento")]
        [DbType("varchar(4000)")]
        [VisibleInListView(true)]
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
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
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
