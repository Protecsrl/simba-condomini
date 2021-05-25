using CAMS.Module.Classi;
using CAMS.Module.DBClienti;
using CAMS.Module.DBDocument;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("CONTRATTI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Contratti")]
    [ImageName("BO_Contract")]
    [NavigationItem("Contratti")]
    [Appearance("Contratti.scadute.BKColor.Red", TargetItems = "*", FontStyle = System.Drawing.FontStyle.Strikeout, FontColor = "Salmon", Priority = 2, Criteria = "Oid > 0 And (Abilitato = 'No' Or DataAl <= LocalDateTimeToday())")]
    [RuleCriteria("RuleCriteriaObject_RuleCriteria3", DefaultContexts.Save, @"DataAl > DataDal", SkipNullOrEmptyValues = false, CustomMessageTemplate = @"la data di inizio deve essere magiore della data di fine")]

    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion


    public class Contratti : XPObject
    {
        public Contratti()
            : base()
        {
        }
        public Contratti(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {

                MostraDataOraFermo = true;
                MostraDataOraRiavvio = true;

                MostraDataOraSopralluogo = true;
                MostraDataOraAzioniTampone = true;
                MostraDataOraInizioLavori = true;
                MostraDataOraCompletamento = true;
                // configurazione livelli autorizzativi
                LivelloAutorizzazioneGuasto = 0; // non sono previsti livelli autorizzativi e quindi notifiche
                BloccoLivelloAutorizzazioneGuasto = false;
                TempoLivelloAutorizzazioneGuasto = "10;10;10";// tre livelli autorizzativi da 10 min ogni uno
            }
        }

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(50)]
        [DbType("varchar(50)")]
        [RuleRequiredField("RReqField.Contratti.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private string fDescrizione;
        [Size(250), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [RuleRequiredField("RReqField.Contratti.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        [DbType("varchar(250)")]
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
        private string fCentroCosto;
        [Size(50),
        Persistent("CENTROCOSTO"),
        DisplayName("Centro di Costo")]
        [DbType("varchar(50)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CentroCosto
        {
            get
            {
                return fCentroCosto;
            }
            set
            {
                SetPropertyValue<string>("CentroCosto", ref fCentroCosto, value);
            }
        }

        private Clienti fCliente;
        [Size(150), Persistent("CLIENTE"), DisplayName("Cliente")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Clienti Cliente
        {
            get { return GetDelayedPropertyValue<Clienti>("Cliente"); }
            set { SetDelayedPropertyValue<Clienti>("Cliente", value); }

        }

        private string fCodContratto;
        [Size(100),
        Persistent("CODCONTRATTO"),
        DisplayName("Descrizione Contratto")]
        [DbType("varchar(100)")]
        public string CodContratto
        {
            get
            {
                return fCodContratto;
            }
            set
            {
                SetPropertyValue<string>("CodContratto", ref fCodContratto, value);
            }
        }



        private DateTime fDataDal;
        [Persistent("DATADAL"), DisplayName("Data Dal:")]
        [RuleRequiredField("Contratti.DataDal", "Save", @"Data Obligata", SkipNullOrEmptyValues = false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("commessa_Abilita.DataDal", Criteria = "DataDal >= LocalDateTimeToday()", BackColor = "Red", FontColor = "Black")]
        public DateTime DataDal
        {
            get
            {
                return fDataDal;
            }
            set
            {
                SetPropertyValue<DateTime>("DataDal", ref fDataDal, value);
            }
        }
        private DateTime fDataAl;
        [Persistent("DATAAL"), DisplayName("Data Al:")]
        [RuleRequiredField("Contratti.fDataAl", "Save", @"Data Obligata", SkipNullOrEmptyValues = false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("contratto_Abilito.DataAl", Criteria = "DataAl <= LocalDateTimeToday()", BackColor = "Red", FontColor = "Black")]
        public DateTime DataAl
        {
            get
            {
                return fDataAl;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAl", ref fDataAl, value);
            }
        }


        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }
        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [Appearance("Contrattiedificio.Abilita.DateUnService", Criteria = "Abilitato = 'Si'", Enabled = false)]
        //[RuleRequiredField("immobile.Abilita.DateUnService.Obblig", DefaultContexts.Save,  TargetCriteria = "Abilitato = 'No'")]
        [RuleRequiredField("Contratti.Abilita.Obbligata", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Abilitato] == 'No'")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public DateTime DateUnService
        {
            get
            {
                return fDateUnService;
            }
            set
            {
                SetPropertyValue<DateTime>("DateUnService", ref fDateUnService, value);
            }
        }

        
        //private DateTime fDateInserimento;
        //[Persistent("DATAINSERIMENTO"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        //[VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        //[EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        
        //[Delayed(true)]      
        //public DateTime DateInserimento
        //{
        //    get { return GetDelayedPropertyValue<DateTime>("DateInserimento"); }
        //    set { SetDelayedPropertyValue<DateTime>("DateInserimento", value); }
        //}

        //private string fUtenteCreatoRichiesta;
        //[Size(25), Persistent("UTENTEINSERIMENTO"), System.ComponentModel.DisplayName("Utente")]
        //[DbType("varchar(25)")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public string UtenteCreatoRichiesta
        //{
        //    get { return GetDelayedPropertyValue<string>("UtenteCreatoRichiesta"); }
        //    set { SetDelayedPropertyValue<string>("UtenteCreatoRichiesta", value); }
        //}

        //private string fUtenteUltimo;
        //[Persistent("ULTIMOUTENTE"), Size(100), System.ComponentModel.DisplayName("Ultimo Utente")]
        //[DbType("varchar(100)")]
        //[Delayed(true)]
        //[VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        //public string UtenteUltimo
        //{
        //    get { return GetDelayedPropertyValue<string>("UtenteUltimo"); }
        //    set { SetDelayedPropertyValue<string>("UtenteUltimo", value); }
           
        //}

        //private DateTime fDataAggiornamento;
        //[Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[System.ComponentModel.Browsable(true)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public DateTime DataAggiornamento
        //{
        //    get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
        //    set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        //    //get
        //    //{
        //    //    return fDataAggiornamento;
        //    //}
        //    //set
        //    //{
        //    //    SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
        //    //}
        //}


        private ClientiContatti fClienteReferenteContratto;
        [Size(100), Persistent("CLIENTICONTATTI"), DisplayName("Referente per il Cliente")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public ClientiContatti ClienteReferenteContratto
        {
            get { return GetDelayedPropertyValue<ClientiContatti>("ClienteReferenteContratto"); }
            set { SetDelayedPropertyValue<ClientiContatti>("ClienteReferenteContratto", value); }

        }

        private ReferenteContratto fReferenteContratto;
        [Size(100), Persistent("REFERENTECONTRATTO"), DisplayName("Project Manager")]
        [DataSourceCriteria("TipoReferenteCofely = 'PM'")]
        [ExplicitLoading]
        [Delayed(true)]
        public ReferenteContratto ReferenteContratto
        {
            get { return GetDelayedPropertyValue<ReferenteContratto>("fReferenteContratto"); }
            set { SetDelayedPropertyValue<ReferenteContratto>("fReferenteContratto", value); }

        }

        //private ReferenteContratto fReferenteAssistente;
        //[Size(100), Persistent("REFERENTEASSISTENTE"), DisplayName("Referente Assistente")]
        //[DataSourceCriteria("TipoReferenteCofely = 'AS'")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public ReferenteContratto ReferenteAssistenteCofely
        //{
        //    get { return GetDelayedPropertyValue<ReferenteContratto>("ReferenteAssistenteCofely"); }
        //    set { SetDelayedPropertyValue<ReferenteContratto>("ReferenteAssistenteCofely", value); }

        //}

        //private ReferenteContratto fReferenteOperativoCofely;
        //[Size(100), Persistent("REFERENTEOPERATIVO"), DisplayName("Referente Operativo")]
        //[DataSourceCriteria("TipoReferenteCofely = 'Operativo'")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[ExplicitLoading]
        //[Delayed(true)]
        //public ReferenteContratto ReferenteOperativo
        //{
        //    get { return GetDelayedPropertyValue<ReferenteContratto>("ReferenteOperativo"); }
        //    set { SetDelayedPropertyValue<ReferenteContratto>("ReferenteOperativo", value); }
        //    //get
        //    //{
        //    //    return fReferenteOperativoCofely;
        //    //}
        //    //set
        //    //{
        //    //    SetPropertyValue<ReferenteCofely>("ReferenteOperativoCofely", ref fReferenteOperativoCofely, value);
        //    //}
        //}

        //private ReferenteCofely fTerzoResponsabileCofely;
        //[Size(100), Persistent("TERZORESPONSABILECOFELY"), DisplayName("Terzo Responsabile")]
        //[DataSourceCriteria("TipoReferenteCofely = 'TerzoResponsabile'")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public ReferenteCofely TerzoResponsabile
        //{
        //    get { return GetDelayedPropertyValue<ReferenteCofely>("TerzoResponsabileCofely"); }
        //    set { SetDelayedPropertyValue<ReferenteCofely>("TerzoResponsabileCofely", value); }

        //    //get
        //    //{
        //    //    return fTerzoResponsabileCofely;
        //    //}
        //    //set
        //    //{
        //    //    SetPropertyValue<ReferenteCofely>("TerzoResponsabileCofely", ref fTerzoResponsabileCofely, value);
        //    //}
        //}

        private string fWBS;
        [Persistent("WBS"), Size(100), DevExpress.Xpo.DisplayName("Cod WBS")]
        [DbType("varchar(100)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading]
        public string WBS
        {
            get
            {
                return fWBS;
            }
            set
            {
                SetPropertyValue<string>("WBS", ref fWBS, value);
            }
        }


        private AreaDiPolo fAreaDiPolo;
        [RuleRequiredField("RReqField.contratti.AreaDiPolo", DefaultContexts.Save, "Area di Polo è un campo obbligatorio")]
        [Association(@"AreaDiPolo_Contratti"), DisplayName("Area di Polo")]
        [Persistent("AREADIPOLO")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public AreaDiPolo AreaDiPolo
        {
            get { return GetDelayedPropertyValue<AreaDiPolo>("AreaDiPolo"); }
            set { SetDelayedPropertyValue<AreaDiPolo>("AreaDiPolo", value); }

            //get
            //{
            //    return fAreaDiPolo;
            //}
            //set
            //{
            //    SetPropertyValue<AreaDiPolo>("AreaDiPolo", ref fAreaDiPolo, value);
            //}
        }

        #region parametri DI VISUALIZZAZIONE DATE OPERATIVE E PIANI E COLONNE LISTVIEW RDL

        private bool fMostraDataOraFermo;
        [Persistent("M_DATAFERMO"), DisplayName("Mostra Data Ora Fermo")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraDataOraFermo
        {
            get
            {
                return fMostraDataOraFermo;
            }
            set
            {
                SetPropertyValue<bool>("MostraDataOraRiavvio", ref fMostraDataOraFermo, value);
            }
        }

        private bool fMostraDataOraRiavvio;
        [Persistent("M_DATARIAVVIO"), DisplayName("Mostra Data Ora Riavvio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraDataOraRiavvio
        {
            get
            {
                return fMostraDataOraRiavvio;
            }
            set
            {
                SetPropertyValue<bool>("MostraDataOraRiavvio", ref fMostraDataOraRiavvio, value);
            }
        }

        private bool fMostraDataOraInizioLavori;
        [Persistent("M_DATA_INIZIO_LAVORI"), DisplayName("Mostra Data Ora Inizio Lavori")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraDataOraInizioLavori
        {
            get
            {
                return fMostraDataOraInizioLavori;
            }
            set
            {
                SetPropertyValue<bool>("MostraDataOraInizioLavori", ref fMostraDataOraInizioLavori, value);
            }
        }

        private bool fMostraDataOraAzioniTampone;
        [Persistent("M_DATA_AZIONI_TAMPONE"), DisplayName("Mostra Data Ora Azioni Tampone")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraDataOraAzioniTampone
        {
            get
            {
                return fMostraDataOraAzioniTampone;
            }
            set
            {
                SetPropertyValue<bool>("MostraDataOraAzioniTampone", ref fMostraDataOraAzioniTampone, value);
            }
        }



        private bool fMostraDataOraSopralluogo;
        [Persistent("M_DATA_SOPRALLUOGO"), DisplayName("Mostra Data Ora Sopralluogo")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraDataOraSopralluogo
        {
            get
            {
                return fMostraDataOraSopralluogo;
            }
            set
            {
                SetPropertyValue<bool>("MostraDataOraSopralluogo", ref fMostraDataOraSopralluogo, value);
            }
        }

        //private bool fArrivoInSito_come_DataOraSopralluogo;
        //[Persistent("M_DATA_SOPRALLUOGO"), DisplayName("Imposta la Data di sopralluogo con l'arrivo in sito")]
        //[VisibleInListView(false), VisibleInLookupListView(false)]
        //[ExplicitLoading()]
        //public bool MostraDataOraSopralluogo
        //{
        //    get
        //    {
        //        return fMostraDataOraSopralluogo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<bool>("MostraDataOraSopralluogo", ref fMostraDataOraSopralluogo, value);
        //    }
        //}

        private bool fMostraDataOraCompletamento;
        [Persistent("M_DATACOMPLETAMENTO"), DisplayName("Mostra Data Ora Completamento")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraDataOraCompletamento
        {
            get
            {
                return fMostraDataOraCompletamento;
            }
            set
            {
                SetPropertyValue<bool>("MostraDataOraCompletamento", ref fMostraDataOraCompletamento, value);
            }
        }

        private bool fMostraPianiLocali;
        [Persistent("M_PIANILOCALI"), DisplayName("Mostra Piani Locali")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraPianiLocali
        {
            get
            {
                return fMostraPianiLocali;
            }
            set
            {
                SetPropertyValue<bool>("MostraPianiLocali", ref fMostraPianiLocali, value);
            }
        }

        private bool fMostraCodProgRdL;
        [Persistent("M_CODPROGRDL"), DisplayName("Mostra Progressivo RdL")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraCodProgRdL
        {
            get
            {
                return fMostraCodProgRdL;
            }
            set
            {
                SetPropertyValue<bool>("MostraCodProgRdL", ref fMostraCodProgRdL, value);
            }
        }

        private bool fMostraSoddisfazioneCliente;
        [Persistent("M_GRADOSODDISFAZIONE"), DisplayName("Mostra Grado Soddisfazione RdL")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraSoddisfazioneCliente
        {
            get
            {
                return fMostraSoddisfazioneCliente;
            }
            set
            {
                SetPropertyValue<bool>("MostraSoddisfazioneCliente", ref fMostraSoddisfazioneCliente, value);
            }
        }


        private bool fAttivaAutoAssegnazioneTeam;
        [Persistent("M_AUTOASSEGNAZIONETEAM"), DisplayName("Attiva Auto Assegnazione Team")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AttivaAutoAssegnazioneTeam
        {
            get
            {
                return fAttivaAutoAssegnazioneTeam;
            }
            set
            {
                SetPropertyValue<bool>("AttivaAutoAssegnazioneTeam", ref fAttivaAutoAssegnazioneTeam, value);
            }
        }

        private bool fAttivaModificaRichiedentein_RdLNew;
        [Persistent("A_MODRICHIEDENTE_NEWRDL"), DisplayName("Attiva Modifica Richiedente in New")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AttivaModificaRichiedentein_RdLNew
        {
            get
            {
                return fAttivaModificaRichiedentein_RdLNew;
            }
            set
            {
                SetPropertyValue<bool>("AttivaModificaRichiedentein_RdLNew", ref fAttivaModificaRichiedentein_RdLNew, value);
            }
        }


        #region lvello autorizzativo presa in carico

        private bool fAttivaGestioneNotificheRdL;
        [Persistent("M_ATTIVAGESTIONENOTIFICHERDL"), DisplayName("Attiva Gestione Notifiche RdL")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AttivaGestioneNotificheRdL
        {
            get
            {
                return fAttivaGestioneNotificheRdL;
            }
            set
            {
                SetPropertyValue<bool>("AttivaGestioneNotificheRdL", ref fAttivaGestioneNotificheRdL, value);
            }
        }


        private int fLivelloAutorizzazioneGuasto;
        [Persistent("M_LIVELLOAUTORIZZATIVOGUASTO"), DisplayName("Livello Autorizzazione Guasto RdL")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public int LivelloAutorizzazioneGuasto
        {
            get
            {
                return fLivelloAutorizzazioneGuasto;
            }
            set
            {
                SetPropertyValue<int>("LivelloAutorizzazioneGuasto", ref fLivelloAutorizzazioneGuasto, value);
            }
        }


        private bool fBloccoLivelloAutorizzazioneGuasto;
        [Persistent("M_BLOCCOLIVELLOAUTORIZZGUASTO"), DisplayName("Blocco Livello Autorizzazione Guasto RdL")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool BloccoLivelloAutorizzazioneGuasto
        {
            get
            {
                return fBloccoLivelloAutorizzazioneGuasto;
            }
            set
            {
                SetPropertyValue<bool>("BloccoLivelloAutorizzazioneGuasto", ref fBloccoLivelloAutorizzazioneGuasto, value);
            }
        }

        private string fTempoLivelloAutorizzazioneGuasto;
        [Persistent("M_TEMPOLIVELLOAUTORIZZOGUASTO"), DisplayName("Tempo Livello Autorizzazione Guasto RdL")]
        [ToolTip("separare con punto e virgola i valori di livelli previsti")]
        [DbType("varchar(50)"), Size(50)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public string TempoLivelloAutorizzazioneGuasto
        {
            get
            {
                return fTempoLivelloAutorizzazioneGuasto;
            }
            set
            {
                SetPropertyValue<string>("TempoLivelloAutorizzazioneGuasto", ref fTempoLivelloAutorizzazioneGuasto, value);
            }
        }

        private int fLivelloAutorizzativoPresainCarico;
        [Persistent("M_LIV_AUTORIZ_PRESACARICO"), DisplayName("Livello Autorizzativo di Presa in Carico")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public int LivelloAutorizzativoPresainCarico
        {
            get
            {
                return fLivelloAutorizzativoPresainCarico;
            }
            set
            {
                SetPropertyValue<int>("LivelloAutorizzativoPresainCarico", ref fLivelloAutorizzativoPresainCarico, value);
            }
        }

        //private bool fPosizioneDispositivoSmartphone;
        [Persistent("GEOPOSIZIONE"), DisplayName("Posizione da Dispositivo Smartphone")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public bool PosizioneDispositivoSmartphone
        {
            get { return GetDelayedPropertyValue<bool>("PosizioneDispositivoSmartphone"); }
            set { SetDelayedPropertyValue<bool>("PosizioneDispositivoSmartphone", value); }
        }

        [Persistent("MOSTRASLAAPP"), DisplayName("Mostra SLA Smartphone")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public MostraSLA MostraSLASmartphone
        {
            get { return GetDelayedPropertyValue<MostraSLA>("MostraSLASmartphone"); }
            set { SetDelayedPropertyValue<MostraSLA>("MostraSLASmartphone", value); }
        }

        //[Persistent("SLAPRIORITADEFAUL"), DisplayName("SLA Tipo Priorità Default")]
        //[VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public MostraSLA SLA Tipo Priorità Default
        //{
        //    get { return GetDelayedPropertyValue<Priorita>("SLA Tipo Priorità Default"); }
        //    set { SetDelayedPropertyValue<MostraSLA>("SLA Tipo Priorità Default", value); }
        //}
        //[Persistent("SLATINTERVENTODEFAUL"), DisplayName("SLA Tipo Intervento Default")]
        //[VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public MostraSLA MostraSLASmartphone
        //{
        //    get { return GetDelayedPropertyValue<MostraSLA>("MostraSLASmartphone"); }
        //    set { SetDelayedPropertyValue<MostraSLA>("MostraSLASmartphone", value); }
        //}


        #endregion

        #endregion

        #region flag visualizzazione problema-causa-rimedio
        private bool fMostraElencoCauseRimedi;
        [Persistent("M_PRO_CAU_RIM"), DisplayName("Mostra Elenco Cause Rimedi")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool MostraElencoCauseRimedi
        {
            get
            {
                return fMostraElencoCauseRimedi;
            }
            set
            {
                SetPropertyValue<bool>("MostraElencoCauseRimedi", ref fMostraElencoCauseRimedi, value);
            }
        }

        private bool fAbilitaJT;
        [Persistent("M_ABILITA_JT"), DisplayName("Abilita JT")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaJT
        {
            get
            {
                return fAbilitaJT;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaJT", ref fAbilitaJT, value);
            }
        }

        private bool fAbilitaProblemaDefault;
        [Persistent("M_ABILITA_PROBLEMADAFAULT"), DisplayName("Abilita ProblemaDefault")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaProblemaDefault
        {
            get
            {
                return fAbilitaProblemaDefault;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaProblemaDefault", ref fAbilitaProblemaDefault, value);
            }
        }
        private bool fAbilitaTestoCompletamentoObligatorio;
        [Persistent("M_TESTOCOMPLETAOBLIGATO"), DisplayName("Abilita Testo Completamento Obligatorio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaTestoCompletamentoObligatorio
        {
            get
            {
                return fAbilitaTestoCompletamentoObligatorio;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaTestoCompletamentoObligatorio", ref fAbilitaTestoCompletamentoObligatorio, value);
            }
        }
        private bool fAbilitaRifiuto;
        [Persistent("M_ABILITA_RIFIUTO"), DisplayName("Abilita Rifiuto")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaRifiuto
        {
            get
            {
                return fAbilitaRifiuto;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaRifiuto", ref fAbilitaRifiuto, value);
            }
        }

        private bool fAbilitaSospendieFineLavoro;
        [Persistent("M_SOSPENDIFINELAVORO"), DisplayName("Abilita Sospendi e Fine Lavoro")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaSospendieFineLavoro
        {
            get
            {
                return fAbilitaSospendieFineLavoro;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaSospendieFineLavoro", ref fAbilitaSospendieFineLavoro, value);
            }
        }

        private bool fAbilitaRichiedenteOpzioni;
        [Persistent("ABILITA_RICHIEDENTEOPZIONI"), DisplayName("Abilita Richiedente Opzioni")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public bool AbilitaRichiedenteOpzioni
        {
            get
            {
                return fAbilitaRichiedenteOpzioni;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaRichiedenteOpzioni", ref fAbilitaRichiedenteOpzioni, value);
            }
        }

        private bool fAbilitaOption1;
        [Persistent("M_ABILITA_OPTION1"), DisplayName("Abilita Option1")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaOption1
        {
            get
            {
                return fAbilitaOption1;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaOption1", ref fAbilitaOption1, value);
            }
        }

        private bool fAbilitaOption2;
        [Persistent("M_ABILITA_OPTION2"), DisplayName("Abilita Option2")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool AbilitaOption2
        {
            get
            {
                return fAbilitaOption2;
            }
            set
            {
                SetPropertyValue<bool>("AbilitaOption2", ref fAbilitaOption2, value);
            }
        }

        [PersistentAlias("Edificios.Count")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Nr Edifici")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(true)]
        [ExplicitLoading()]
        public string NrEdifici
        {
            get
            {
                if (Edificios == null) return null;
                return this.Edificios.Count.ToString();
            }
        }


        #endregion
        #region associazioni


        [Association(@"Documenti_Commessa", typeof(Documenti)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Documenti")]
        //[Association(@"Documenti_RdL"     , typeof(Documenti)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Documenti")]
        //[ExplicitLoading()]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }




        [Association(@"Contratti_Edificio", typeof(Immobile)), Aggregated, DevExpress.ExpressApp.DC.XafDisplayName("Edifici")]
        [ExplicitLoading]
        public XPCollection<Immobile> Edificios
        {
            get
            {
                return GetCollection<Immobile>("Edificios");
            }
        }


        [Association("Contratti_ContrattiUrgenza"), DevExpress.Xpo.Aggregated]
        [DisplayName("Priorità")]
        public XPCollection<ContrattiUrgenza> ContrattiPrioritas
        {
            get { return GetCollection<ContrattiUrgenza>("ContrattiPrioritas"); }
        }

        [Association("Contratti_ContrattiTIntervento"), DevExpress.Xpo.Aggregated]
        [DisplayName("Tipo Intervento")]
        public XPCollection<ContrattoTipoIntervento> ContrattiTInterventos
        {
            get { return GetCollection<ContrattoTipoIntervento>("ContrattiTInterventos"); }
        }

        #endregion

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "Abilitato")
                {
                    FlgAbilitato newV = (FlgAbilitato)(newValue);
                    if (newV == FlgAbilitato.Si)
                    {
                        this.DateUnService = DateTime.MinValue;
                    }
                    else
                    {
                        this.DateUnService = DateTime.Now;
                    }
                    AggiornaAbilitatoErede(newV);

                }
            }
        }

        void AggiornaAbilitatoErede(FlgAbilitato newV)
        {
            foreach (Immobile ed in this.Edificios)
            {
                ed.AbilitazioneEreditata = newV;

                foreach (Servizio im in ed.Impianti)
                {
                    im.AbilitazioneEreditata = newV;

                    foreach (Asset Ap in im.APPARATOes)
                    {
                        Ap.AbilitazioneEreditata = newV;

                        foreach (AssetSchedaMP ApSK in Ap.AppSchedaMpes)
                        {
                            ApSK.AbilitazioneEreditata = newV;
                        }
                    }
                }

                foreach (Piani pi in ed.Pianies)
                {
                    pi.AbilitazioneEreditata = newV;
                    foreach (Locali lo in pi.Localies)
                    {
                        lo.AbilitazioneEreditata = newV;
                    }
                }

            }
        }

    }
}
