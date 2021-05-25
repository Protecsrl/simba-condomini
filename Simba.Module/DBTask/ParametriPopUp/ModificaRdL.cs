using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.PropertyEditors;
using System.Drawing;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using CAMS.Module.Classi;

namespace CAMS.Module.DBTask.ParametriPopUp
{
    [DefaultClassOptions]
    [Persistent("RDLMODIFICA")]  //[System.ComponentModel.DefaultProperty("Descrizione")]
    [VisibleInDashboards(false)]
    [Indices("Utente")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Aggiorna Richieste Di Lavoro")]
    //[Indices("DataPianificata", "DataRichiesta", "DataCompletamento", "UltimoStatoSmistamento;Categoria")]
    [ImageName("ShowTestReport")]
    [NavigationItem(false)]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    //[Persistent("DatabaseTableName")]

    [Appearance("ModificaRdL.SSmistamento.pCompletamento.noVisibile", AppearanceItemType.LayoutItem,
         @"UltimoStatoSmistamento.Oid In(1,2,3,6,7,10,11)",
        TargetItems = "pCompletamento;NoteCompletamento;DataCompletamento",
       Visibility = ViewItemVisibility.Hide)]

    [Appearance("ModificaRdL.SSmistamento.Risorsa.noVisibile", AppearanceItemType.LayoutItem,
         @"UltimoStatoSmistamento.Oid In(1,7,10)",
        TargetItems = "pRisorsa",
       Visibility = ViewItemVisibility.Hide)]

    [Appearance("ModificaRdL.SSmistamento.DataPianificata.noEditor", AppearanceItemType.ViewItem,
     @"UltimoStatoSmistamento.Oid In(4,5,6,7,8,9)",
    TargetItems = "DataPianificata;StimatempoLavoro",
   Enabled = false)]

    [Appearance("ModificaRdL.pDataOperative.DataSopralluogo", TargetItems = "pDataOperative",
               Criteria = @"Categoria.Oid != 4 Or not(RegistroRdL.MostraDataOraSopralluogo)", Visibility = ViewItemVisibility.Hide)]// Or SopralluogoEseguito != 'Si'

    [Appearance("ModificaRdL.DataSopralluogo", TargetItems = "DataSopralluogo",
               Criteria = @"SopralluogoEseguito != 'Si'", Visibility = ViewItemVisibility.Hide)]// Or SopralluogoEseguito != 'Si'

    [Appearance("RuleInfo.ModificaRdL.DataSopralluogo.Rosso", AppearanceItemType.ViewItem,
      @"[MaxDataSopralluogo] < [DataSopralluogo]", TargetItems = "DataSopralluogo", FontColor = "Red")]

    [RuleCriteria("RuleInfo.ModificaRdL.MaxDataSopralluogo", DefaultContexts.Save, @"[MaxDataSopralluogo] < [DataSopralluogo]",
CustomMessageTemplate = "Informazione: La data Sopralluogo supera la data limite di SLA ({MaxDataSopralluogo}), sei sicuro di voler Salvare?.",
SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleInfo.ModificaRdL.DataSopralluogo-DataAzioneTampone", DefaultContexts.Save, @"[DataSopralluogo] <= [DataAzioneTampone] And [DataAzioneTampone] <= [DataAzioneTampone]",
CustomMessageTemplate = "Informazione: La data Sopralluogo supera la data limite di SLA ({MaxDataSopralluogo}), sei sicuro di voler Salvare?.",
SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleInfo.ModificaRdL.RisorsaTeamst11", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] = 11 And [RisorseTeam] Is Null",
    CustomMessageTemplate = "Selezionare la Risorsa di Assegnazione Intervento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RuleInfo.ModificaRdL.RisorsaTeamst2", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] =2 And [RisorseTeam] Is Null",
    CustomMessageTemplate = "Selezionare la Risorsa di Assegnazione Intervento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]


    [RuleCriteria("RuleInfo.ModificaRdL.MaxDataCompletamento", DefaultContexts.Save, @"[MaxDataCompletamento] < [DataCompletamento]",
CustomMessageTemplate = "Informazione: La data Completamento supera la data limite di SLA ({MaxDataCompletamento}), sei sicuro di voler Salvare?.",
SkipNullOrEmptyValues = false, InvertResult = false, ResultType = ValidationResultType.Information)]

    public class ModificaRdL : BaseObject
    { //     ModificaRdLObjSpaceController                     Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public ModificaRdL(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place your initialization code here (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112834.aspx).
        }

        private string fUtente;
        [Size(50), Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente Intervento")]
        //[RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(50)")]
        [VisibleInListView(false)]
        //[Delayed(true)]
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

        private string fSessioneID;
        [Size(50), Persistent("SESSIONEID"), System.ComponentModel.DisplayName("SessioneID")]
        //[RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(50)")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        public string SessioneID
        {
            get
            {
                return fSessioneID;
            }
            set
            {
                SetPropertyValue<string>("SessioneID", ref fSessioneID, value);
            }
        }

        private int fcodice;
        [Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[Appearance("RdL.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        //[Appearance("RdL.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        //[Appearance("RdL.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        //[Appearance("RdL.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        //[Appearance("RdL.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
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

        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        [RuleRequiredField("RRq.ModificaRdL.UltimoStatoSmistamento", DefaultContexts.Save, "La StatoSmistamento è un campo obbligatorio")]
        //[Appearance("RdL.UltimoStatoSmistamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(UltimoStatoSmistamento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.UltimoStatoSmistamento.Evidenza", AppearanceItemType.LayoutItem, "not(IsNullOrEmpty(UltimoStatoSmistamento))", FontStyle = FontStyle.Bold, BackColor = "Yellow", FontColor = "Black")]
        [DataSourceCriteria("[<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento.Oid == '@This.old_SSmistamento_Oid']")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        //[Delayed(true)]
        public StatoSmistamento UltimoStatoSmistamento
        {
            get
            {
                return fUltimoStatoSmistamento;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("UltimoStatoSmistamento", ref fUltimoStatoSmistamento, value);
            }
        }

        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("Stato Operativo")]
        [DataSourceCriteria("[<StatoSmistamento_SOperativoSO>][^.Oid == StatoOperativoSO.Oid And StatoSmistamento.Oid == '@This.old_SSmistamento_Oid'] ")]
        [ExplicitLoading()]
        [ImmediatePostData(true)]
        public StatoOperativo UltimoStatoOperativo
        {
            get
            {
                return fUltimoStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
            }
        }


        private int _old_SSmistamento_Oid;
        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public int old_SSmistamento_Oid
        {
            get { return _old_SSmistamento_Oid; }
            set { SetPropertyValue<int>("old_SSmistamento_Oid", ref _old_SSmistamento_Oid, value); }

            //set { _old_SSmistamento_Oid = value; }
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fDataPianificata;
        //[Persistent("DATAPIANIFICATA")]
        [NonPersistent]
        [System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_MoficaRdLPianificata)]
        //[Delayed(true)]
        [ImmediatePostData]
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

        private DateTime fDataSopralluogo;
        [NonPersistent]
        [System.ComponentModel.DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Attenzione alla massima data di Sopralluogo in SLA!", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_MoficaRdLPianificata)]
        [ImmediatePostData]
        public DateTime DataSopralluogo
        {
            get
            {
                return fDataSopralluogo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value);
            }
        }

        private Fatto fSopralluogoEseguito;
        [NonPersistent] //Persistent("FATTO_SOPRALLUOGO"),
        [System.ComponentModel.DisplayName("Eseguito Sopralluogo")]
        //[ToolTip("{MaxDataSopralluogo}")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Attenzione alla massima data di Sopralluogo in SLA!", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ImmediatePostData(true)]
        public Fatto SopralluogoEseguito
        {
            get
            {
                return fSopralluogoEseguito;
            }
            set
            {
                SetPropertyValue<Fatto>("SopralluogoEseguito", ref fSopralluogoEseguito, value);
            }
        }

        private DateTime fMaxDataSopralluogo;
        [NonPersistent]
        [System.ComponentModel.DisplayName("max Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Attenzione questa è il limite superiore della data di Sopralluogo conforme allo SLA!", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("ModificaRdL.MaxDataSopralluogo.Evidenza", FontStyle = FontStyle.Italic, FontColor = "Green")]
        public DateTime MaxDataSopralluogo
        {
            get
            {
                return fMaxDataSopralluogo;
            }
            set
            {
                SetPropertyValue<DateTime>("MaxDataSopralluogo", ref fMaxDataSopralluogo, value);
            }
        }

        private DateTime fDataAzioneTampone;
        [NonPersistent]
        [System.ComponentModel.DisplayName("Data AzioneTampone")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Uguale o maggiore della data di Sopralluogo in SLA!", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_MoficaRdLPianificata)]
        [ImmediatePostData]
        public DateTime DataAzioneTampone
        {
            get { return fDataAzioneTampone; }
            set { SetPropertyValue<DateTime>("DataAzioneTampone", ref fDataAzioneTampone, value); }
        }
        private DateTime fDataInizioLavori;
        [NonPersistent]
        [System.ComponentModel.DisplayName("Data Inizio Lavori")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Uguale o maggiore della data di Sopralluogo in SLA!", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_MoficaRdLPianificata)]
        [ImmediatePostData]
        public DateTime DataInizioLavori
        {
            get { return fDataInizioLavori; }
            set { SetPropertyValue<DateTime>("DataInizioLavori", ref fDataInizioLavori, value); }
        }


        private int _StimatempoLavoro;
        [NonPersistent]
        [DevExpress.Xpo.DisplayName("Stima Tempo Attività [min]")]
        [RuleRange("RuleRange._StimatempoLavoro", "Save", 5, 480)]
        [Appearance("RdL.StimatempoLavoro.neroItem", AppearanceItemType.ViewItem, "IsNullOrEmpty(DataPianificata)", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        public int StimatempoLavoro
        {
            get
            {
                return _StimatempoLavoro;
            }
            set
            {
                SetPropertyValue<int>("StimatempoLavoro", ref _StimatempoLavoro, value);
            }
        }

        private DateTime fDataCreazione;
        [NonPersistent]
        [System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[Appearance("ModificaRdL.NoteCompletamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "[UltimoStatoSmistamento.Oid] In(4)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
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

        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazioneAssociation(@"TEAMRISORSE_RdL"),
        //[Persistent("RISORSATEAM"), System.ComponentModel.DisplayName("Team")]
        [NonPersistent()]
        [System.ComponentModel.DisplayName("Team")]
        //[DataSourceCriteria("RisorsaCapo.CentroOperativo == '@This.Immobile.CentroOperativoBase'")]
        //[Appearance("RdL.RisorseTeam.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RisorseTeam) AND ([UltimoStatoSmistamento.Oid] In(2,11))", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.RisorseTeam.nero", AppearanceItemType.LayoutItem, "not IsNullOrEmpty(RisorseTeam)", FontStyle = FontStyle.Bold, FontColor = "Black")]
        //[Appearance("RdL.RisorseTeam.neroItem", AppearanceItemType.ViewItem, "not IsNullOrEmpty(RisorseTeam)", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        //[System.ComponentModel.Browsable(false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public RisorseTeam RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            }
        }

        private string fRicercaRisorseTeam;
        [NonPersistent, Size(25)]//, DisplayName("Filtro"), , Size(25)
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaRisorseTeam
        {
            get;
            set;
        }

        private DateTime fDataCompletamento;        //[Persistent("DATACOMPLETAMENTO"),]
        [NonPersistent]
        [System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_MoficaRdLPianificata)]
        //[Appearance("ModificaRdL.DataCompletamento.DataCompletamento.EvidenzaObligatorio", AppearanceItemType.ViewItem,   "1=1", Visibility = ViewItemVisibility.Hide )]
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

        private DateTime fMaxDataCompletamento;
        [NonPersistent]
        [System.ComponentModel.DisplayName("max Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Attenzione questa è il limite superiore della data di Completamento conforme allo SLA!", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("ModificaRdL.MaxDataCompletamento.Evidenza", FontStyle = FontStyle.Italic, FontColor = "Green")]
        public DateTime MaxDataCompletamento
        {
            get
            {
                return fMaxDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("MaxDataCompletamento", ref fMaxDataCompletamento, value);
            }
        }

        private string fNoteCompletamento;
        [NonPersistent]
        [System.ComponentModel.DisplayName("Note Completamento")]
        [Size(4000)]  //IsNullOrEmpty(NoteCompletamento) And RdL.Immobile.Commesse.AbilitaTestoCompletamentoObligatorio And RdL.Categoria.Oid = 4 And [UltimoStatoSmistamento.Oid] = 4
        [RuleRequiredField("Rulereq.ModificaRdL.NoteCompletamento", DefaultContexts.Save,
            TargetCriteria = "IsNullOrEmpty(NoteCompletamento) And NotaCompletamentoObbligata And [UltimoStatoSmistamento.Oid] = 4",
            CustomMessageTemplate = "La Nota Completamento è un campo obbligatorio")]
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

        [NonPersistent]//, DisplayName("Filtro"), , Size(25)
        [Browsable(false)]
        public bool NotaCompletamentoObbligata
        {
            get;
            set;
        }

        [NonPersistent]
        [System.ComponentModel.DisplayName("Messaggio"), Size(250)]
        public string MessaggioUtente
        {
            get;
            set;
        }
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                if (this.Codice > 0)
                {
                    #region se già esistente
                    switch (propertyName)
                    {
                        case "UltimoStatoSmistamento":
                            if (newValue != null)
                            {
                                StatoSmistamento newUltimoStatoSmistamento = ((StatoSmistamento)(newValue));
                                if (newValue != oldValue)
                                {
                                    #region stato smistamento
                                    switch (newUltimoStatoSmistamento.Oid)
                                    {
                                        case 1:// in assegnata smatphone           fListaFiltraComboRisorseTeam  //  1	In Attesa di Assegnazione
                                            this.UltimoStatoOperativo = null;
                                            this.RisorseTeam = null;
                                            // this.fListaFiltraComboRisorseTeam = null;
                                            break;

                                        case 2:// in assegnata smatphone  //2	Assegnata
                                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);

                                            //this.RegistroRdL.UltimoStatoSmistamento = Session.GetObjectByKey<StatoSmistamento>(2);
                                            //this.RegistroRdL.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);

                                            if (RisorseTeam != null)
                                            {
                                                if (RisorseTeam.RisorsaCapo != null)
                                                {
                                                    if (RisorseTeam.RisorsaCapo.SecurityUser == null)
                                                    {
                                                        this.RisorseTeam = null;
                                                    }
                                                }
                                            }
                                            break;
                                        case 5:
                                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(4);
                                            break;
                                        case 7:
                                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(13);
                                            break;
                                        case 6:
                                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(12);
                                            break;
                                        case 4: //4	Lavorazione Conclusa
                                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(11);
                                            this.DataCompletamento = DateTime.Now;
                                            break;
                                        case 11: //11	Gestione da Sala Operativa
                                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);
                                            // this.fListaFiltraComboRisorseTeam = null;
                                            break;
                                        default:
                                            this.UltimoStatoOperativo = null;
                                            break;
                                    }
                                    #endregion

                                    #region aggiorna registro rdl
                                    //this.RegistroRdL.UltimoStatoSmistamento = newUltimoStatoSmistamento;

                                    #endregion
                                }
                            }
                            break;
                        case "UltimoStatoOperativo":
                            if (newValue != null && newValue != oldValue)
                            {
                                #region aggiorna registro rdl
                                //this.RegistroRdL.UltimoStatoOperativo = ((StatoOperativo)(newValue)); 
                                #endregion

                            }

                            break;
                        case "RisorseTeam":
                            if (newValue != null)
                            {
                                //this.DataAssegnazioneOdl = DateTime.Now;
                                //StatoSmistamento temp = this.UltimoStatoSmistamento;
                                //this.UltimoStatoSmistamento = null;
                                //this.UltimoStatoSmistamento = temp;
                            }
                            else
                            {
                                //this.DataAssegnazioneOdl = DateTime.MinValue;
                            }
                            break;
                        case "DataCompletamento":
                            if (newValue != null)
                            {
                                //this.DataCompletamentoSistema = DateTime.Now;
                            }
                            else
                            {
                                //this.DataCompletamentoSistema = DateTime.MinValue;
                            }
                            break;

                        case "DataPianificata":
                            if (newValue != null)
                            {

                                //this.DataPianificataEnd = ((DateTime)newValue).AddMinutes(30);
                            }
                            else
                            {
                                //this.DataPianificataEnd = DateTime.MinValue;
                            }
                            break;

                        case "DataSopralluogo":
                            if (newValue != null)
                            {
                                this.fDataAzioneTampone = this.DataSopralluogo;
                                this.fDataInizioLavori = this.DataSopralluogo;
                                //this.MessaggioUtente =
                                //    string.Format("Riferimento data Azione Tampone {0} e Inizio Lavori {1}",
                                //    this.DataSopralluogo,
                                //    this.DataSopralluogo);
                            }
                            else
                            {
                                //this.DataPianificataEnd = DateTime.MinValue;
                            }
                            break;

                    }
                    #endregion
                }
            }

        }


        public static bool SetpAvSmistamentoRdLExecute(int OidRdL, ModificaRdL ModificaRdL, IObjectSpace xpObjectSpace, ref string Messaggio)
        {
            int OidRegolaAssegnazionexTRisorse = 0;
            bool SpediscieMail = false;
            if (xpObjectSpace is XPObjectSpace && xpObjectSpace != null)
            {
                RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
                if (rdl != null)
                {
                    try
                    {

                        if (rdl.UltimoStatoSmistamento != ModificaRdL.UltimoStatoSmistamento)
                        {

                            rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(ModificaRdL.UltimoStatoSmistamento.Oid);
                            rdl.UltimoStatoOperativo = xpObjectSpace.GetObject<StatoOperativo>(ModificaRdL.UltimoStatoOperativo);
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid != 10)
                        {
                            #region SE è STATO TOLTO LO STATO SMISTAMENTO IN EMERGENZA PER SMARTPHONE
                            CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze notEmerRdL = xpObjectSpace.FindObject<CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze>(CriteriaOperator.Parse("RdL.Oid = ?", rdl.Oid));
                            if (notEmerRdL != null)
                            {
                                notEmerRdL.RegStatoNotifica = RegStatiNotificaEmergenza.Annullato;
                                notEmerRdL.Team = null;
                                notEmerRdL.DataAggiornamento = DateTime.Now;
                                notEmerRdL.DataChiusura = DateTime.Now;
                                notEmerRdL.Save();
                            }
                            #endregion
                        }


                        if (rdl.SopralluogoEseguito != ModificaRdL.SopralluogoEseguito)
                            rdl.SopralluogoEseguito = ModificaRdL.SopralluogoEseguito;

                        if (rdl.DataSopralluogo != ModificaRdL.DataSopralluogo)
                            rdl.DataSopralluogo = ModificaRdL.DataSopralluogo;

                        int oidReisorsaRDL = 0;
                        int oidReisorsaMRDL = 0;
                        if (rdl.RisorseTeam != null)
                            oidReisorsaRDL = rdl.RisorseTeam.Oid;

                        if (ModificaRdL.RisorseTeam != null)
                            oidReisorsaMRDL = ModificaRdL.RisorseTeam.Oid;

                        if (oidReisorsaRDL != oidReisorsaMRDL)
                        {
                            rdl.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(ModificaRdL.RisorseTeam);
                            SpediscieMail = true;
                        }

                        if (rdl.DataPianificata != ModificaRdL.DataPianificata)
                        {
                            rdl.DataPianificata = ModificaRdL.DataPianificata;
                            DateTime fine = ModificaRdL.DataPianificata.AddMinutes(ModificaRdL.StimatempoLavoro);
                            rdl.DataPianificataEnd = fine;
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid == 2 || ModificaRdL.UltimoStatoSmistamento.Oid == 11)
                        {
                            rdl.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(ModificaRdL.RisorseTeam);
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid == 4)
                        {
                            rdl.DataCompletamento = ModificaRdL.DataCompletamento;
                            rdl.NoteCompletamento = ModificaRdL.NoteCompletamento;
                            SpediscieMail = true;
                        }

                        rdl.DataAggiornamento = DateTime.Now;

                        //   RegistroRdL
                        if (rdl.RegistroRdL != null)
                        {
                            if (rdl.RegistroRdL.DataPianificata != rdl.DataPianificata)
                                rdl.RegistroRdL.DataPianificata = rdl.DataPianificata;

                            if (rdl.RegistroRdL.RisorseTeam != rdl.RisorseTeam)
                                rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;

                            if (rdl.RegistroRdL.UltimoStatoSmistamento != rdl.UltimoStatoSmistamento)
                            {
                                if (rdl.RegistroRdL.RdLes.Count == 1)
                                {
                                    rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                    if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                        rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;

                                    rdl.RegistroRdL.DataCompletamento = rdl.DataCompletamento;
                                    rdl.RegistroRdL.NoteCompletamento = rdl.NoteCompletamento;


                                }
                                else
                                {
                                    int nrRdL = rdl.RegistroRdL.RdLes.Count;
                                    int tutte_chiuse = rdl.RegistroRdL.RdLes.Where(w => w.UltimoStatoSmistamento == rdl.UltimoStatoSmistamento).Count();
                                    if (nrRdL == tutte_chiuse)
                                    {
                                        rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento; // chiudi intero registro


                                        if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                            rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                                    }
                                }

                            }

                            if (rdl.RegistroRdL.DataPrevistoArrivo != rdl.DataPrevistoArrivo)
                                rdl.RegistroRdL.DataPrevistoArrivo = rdl.DataPrevistoArrivo;

                            rdl.RegistroRdL.DataAggiornamento = rdl.DataAggiornamento;
                        }

                        rdl.Save();
                        xpObjectSpace.CommitChanges();



                    }
                    catch (Exception ex)
                    {
                        string Titolo = "Aggiornamento non Eseguito!!";
                        Messaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                        //SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Warning);
                    }


                }

            }

            return SpediscieMail;
        }







    }
}


//private string _PersistentProperty;
//[XafDisplayName("My display name"), ToolTip("My hint message")]
//[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
//[Persistent("DatabaseColumnName"), RuleRequiredField(DefaultContexts.Save)]
//public string PersistentProperty {
//    get { return _PersistentProperty; }
//    set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
//}

//[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
//public void ActionMethod() {
//    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
//    this.PersistentProperty = "Paid";
//}

