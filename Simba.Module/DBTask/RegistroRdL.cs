using CAMS.Module.Costi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBDocument;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.Guasti;
using CAMS.Module.PropertyEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("REGRDL")]    //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro RdL")]
    [Appearance("RegistroRdL.Emergenza.FontColor.Red", TargetItems = "*", FontColor = "Red", FontStyle = FontStyle.Bold, Priority = 2, Criteria = "[UltimoStatoSmistamento.Oid] = 10")]

    [Appearance("RegistroRdL.Mai.Editabili", TargetItems = "Utente;DATA_CREAZIONE_RDL;Codice;DataAssegnazioneOdl;DataPrevistoArrivo", Criteria = "1=1", FontColor = "Black", Enabled = false)]


    //[Appearance("RegistroRdL.VisualizzaxCategoriaGuasto", TargetItems = "DataRiavvio;CausaRimedio;DataFermo", Criteria = @"Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RegistroRdL.TipoIntervento.Enabled", TargetItems = "DataRiavvio;Prob;Causa;Rimedio", Criteria = @"Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]

    [Appearance("RegistroRdL.SSmistamento.Annullata", TargetItems = "*", Criteria = @"UltimoStatoSmistamento.Oid = 7", FontColor = "Blue")]
    [Appearance("RegistroRdL.SSmistamento.AnnullataEnabled", TargetItems = "*", Criteria = @"UltimoStatoSmistamento.Oid = 7 And not(UserIsAdminRuolo)", Enabled = false)]

    [Appearance("RegistroRdL.SSmistamento.Emergenza.noVisibile", TargetItems = "RisorseTeam;DataCompletamento;DataRiavvio;DataSopralluogo;DataAzioniTampone;DataInizioLavori",
        Criteria = @"UltimoStatoSmistamento.Oid = 10", Visibility = ViewItemVisibility.Hide)]
    //
    [Appearance("RegistroRdL.SSmistamento.Assegnata.Enabled", TargetItems = "RdLes",
    Criteria = @"UltimoStatoSmistamento.Oid In(2,3,4,5,6,7,8,9,10)", Enabled = true)]
    //

    //
    [Appearance("RegistroRdL.SOperativo.Enabled", TargetItems = "UltimoStatoOperativo", Criteria = @"UltimoStatoSmistamento.Oid != 11", FontColor = "Black", Enabled = false)]

    //[Appearance("RegistroRdL.inCreazione.noVisibile", 
    //    TargetItems = "CausaRimedio;RisorseTeam;DataAssegnazioneOdl;OdL;TipoOdLDescrizione;UltimoStatoOperativo;;RegistroCambioRisorsa;DataCompletamento;DataRiavvio;DataSopralluogo;DataAzioniTampone;DataInizioLavori;NoteCompletamento;Codice;CodOdL;CodRegRdL",
    //    Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    ///////
    // richiesta completata operativamente   / 
    [Appearance("RegistroRdL.SSmistamento.LavorazioneConclusa",
          TargetItems = "Richiedente;Immobile;Impianto;Apparato;Priorita;Categoria;TipoIntervento;Problema;ProblemaCausa;CausaRimedio;Descrizione;DATA_CREAZIONE_RDL;RisorseTeam;DataAssegnazioneOdl;OdL;TipoOdLDescrizione;DataFermo",
          Criteria = @"UltimoStatoSmistamento.Oid = 4 And not(UserIsAdminRuolo)", FontColor = "Black", Enabled = false)]

    [Appearance("RdL.SSmistamento.RichiestaChiusa", TargetItems = "*", Criteria = @"UltimoStatoSmistamento.Oid = 5 And not(UserIsAdminRuolo)", FontColor = "Black", Enabled = false)]


    [Appearance("RegistroRdL.StatoCompletaLavoro.Visibile", TargetItems = "DataCompletamento;NoteCompletamento", Criteria = @"UltimoStatoSmistamento.Oid != 4", Visibility = ViewItemVisibility.Hide)]


    [RuleCriteria("RC.RegistroRdL.Valida.Completa", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] = 4 And [DataCompletamento] Is Null",
    CustomMessageTemplate = "Per Completare la Lavorazione è necessario impostare la Data di Completamento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]
   
    [RuleCriteria("RC.RegistroRdL.Valida.tRisorsa_SS", DefaultContexts.Save, @"[UltimoStatoSmistamento.Oid] In(11,2) And [RisorseTeam] Is Null",
    CustomMessageTemplate = "Selezionare la Risorsa di Assegnazione Intervento!",
    SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RegistroRdL.Valida.Completa.noProgrammata", DefaultContexts.Save,
        @"[UltimoStatoSmistamento.Oid] = 4 And [Categoria.Oid] <> 1 And [DataCompletamento] > [DATA_CREAZIONE_RDL]",
   CustomMessageTemplate = "Per Completare la Lavorazione è necessario impostare la data di Completamento (maggiore della data di richiesta)!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Warning)]

    #region  Date Operative
    [Appearance("RegistroRdL.DateOperative.DataFermo", TargetItems = "DataFermo", Criteria = @"not(MostraDataOraFermo) Or Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RegistroRdL.DateOperative.DataRiavvio", TargetItems = "DataRiavvio", Criteria = @"Categoria.Oid != 4 Or (not(MostraDataOraFermo) And UltimoStatoSmistamento.Oid != 4)", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RegistroRdL.DateOperative.DataSopralluogo", TargetItems = "DataSopralluogo", Criteria = @"Categoria.Oid != 4 Or not(MostraDataOraSopralluogo)", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RegistroRdL.DateOperative.DataAzioniTampone", TargetItems = "DataAzioniTampone", Criteria = @"Categoria.Oid != 4 Or not(MostraDataOraAzioniTampone)", Visibility = ViewItemVisibility.Hide)]
    [Appearance("RegistroRdL.DateOperative.DataInizioLavori", TargetItems = "DataInizioLavori", Criteria = @"Categoria.Oid != 4 Or not(MostraDataOraInizioLavori)", Visibility = ViewItemVisibility.Hide)]


    #endregion

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "All Data", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.manprog", "Categoria.Oid = 1", "Manutenzione Programmata", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.conduzione", "Categoria.Oid = 2", "Conduzione", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.mancond", "Categoria.Oid = 3", "Manutenzione A Condizione", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.mangu", "Categoria.Oid = 4", "Manutenzione Guasto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Categoria.manprogspot", "Categoria.Oid = 5", "Manutenzione Programmata Spot", Index = 5)]
    // [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    // [ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    #endregion

    [NavigationItem("Ticket")]
    [ImageName("Action_Debug_Step")]
    public class RegistroRdL : XPObject
    {
        public RegistroRdL() : base() { }
        public RegistroRdL(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                DataAssegnazioneOdl = DateTime.Now;
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        //[NonPersistent, XafDisplayName("Codice")]
        //[Appearance("CodRegRdL.Codice.Hide", Criteria = "Oid < 1", Visibility = ViewItemVisibility.Hide)]
        //public string Codice
        //{
        //    get
        //    {
        //        if (this.Oid == -1) return null;
        //        return Oid.ToString();
        //    }
        //}
        [PersistentAlias("Oid"), XafDisplayName("Codice")]
        [Appearance("CodRegRdL.Codice.Hide", Criteria = "Oid < 1", Visibility = ViewItemVisibility.Hide)]
        public string Codice
        {
            get
            {
                //if (this.Oid == -1) return null;
                //return Oid.ToString();
                return EvaluateAlias("Codice").ToString();
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(4000)]
        [RuleRequiredField("RReqField.RegistroOperativo.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private DateTime fDataAssegnazioneOdl;
        [Persistent("DATAASSEGNAZIONEODL"), XafDisplayName("Data Assegnazione OdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Assegnazione dell intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[ExplicitLoading()]
        [Delayed(true)]
        public DateTime DataAssegnazioneOdl
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAssegnazioneOdl"); }
            set { SetDelayedPropertyValue<DateTime>("DataAssegnazioneOdl", value); }

            //get
            //{
            //    return fDataAssegnazioneOdl;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataAssegnazioneOdl", ref fDataAssegnazioneOdl, value);
            //}
        }

        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_RegRdLPianificata_Editor)]//
        //[ExplicitLoading()]
        [Delayed(true)]
        public DateTime DataPianificata
        {
            get { return GetDelayedPropertyValue<DateTime>("DataPianificata"); }
            set { SetDelayedPropertyValue<DateTime>("DataPianificata", value); }

            //get
            //{
            //    return fDataPianificata;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value);
            //}
        }

        private DateTime fDataPrevistoArrivo;
        [Persistent("DATAPREVISTOARRIVO"), System.ComponentModel.DisplayName("Data Previsto Arrivo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [Delayed(true)]
        public DateTime DataPrevistoArrivo
        {
            get { return GetDelayedPropertyValue<DateTime>("DataPrevistoArrivo"); }
            set { SetDelayedPropertyValue<DateTime>("DataPrevistoArrivo", value); }

            //get
            //{
            //    return fDataPrevistoArrivo;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataPrevistoArrivo", ref fDataPrevistoArrivo, value);
            //}
        }

        private DateTime fDATA_CREAZIONE_RDL;
        [Persistent("DATA_CREAZIONE_RDL"), XafDisplayName("Data Creazione RdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Creazione dell intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DATA_CREAZIONE_RDL
        {
            get { return GetDelayedPropertyValue<DateTime>("DATA_CREAZIONE_RDL"); }
            set { SetDelayedPropertyValue<DateTime>("DATA_CREAZIONE_RDL", value); }
        }

        #region Stato Smistamento Selezionabile
        private StatoSmistamento fUltimoStatoSmistamento;
        [Persistent("ULTIMOSTATOSMISTAMENTO"), XafDisplayName("Ultimo Stato Smistamento")]        // [Appearance("RegistroRdL.UltimoStatoSmistamento", Enabled = false)]
        //[ImmediatePostData(true)]
        //[DataSourceCriteria("[<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento == '@This.old_SSmistamento']")]
        //[ExplicitLoading()]
        //[Delayed(true)]
        [RuleRequiredField("RRq.RegRdL.UltimoStatoSmistamento", DefaultContexts.Save, "La StatoSmistamento è un campo obbligatorio")]
        [Appearance("RegRdL.UltimoStatoSmistamento.EvidenzaObligatorio", AppearanceItemType.LayoutItem,
            "IsNullOrEmpty(UltimoStatoSmistamento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RegRdL.UltimoStatoSmistamento.Evidenza", AppearanceItemType.LayoutItem,
            "not(IsNullOrEmpty(UltimoStatoSmistamento))", FontStyle = FontStyle.Bold, BackColor = "Yellow", FontColor = "Black")]
        [DataSourceCriteria("[<StatoSmistamentoCombo>][^.Oid == StatoSmistamentoxCombo.Oid And StatoSmistamento.Oid == '@This.old_SSmistamento_Oid']")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
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

        //private int _old_SSmistamento_Oid;
        //[NonPersistent]
        //[Browsable(false)]
        //public int old_SSmistamento_Oid
        //{
        //    get
        //    {
        //        if (this.Oid > 0 && _old_SSmistamento_Oid == 0)
        //        {
        //            _old_SSmistamento_Oid = Session.Query<RegistroRdL>().Where(d => d.Oid == this.Oid)
        //                .Select(s => s.UltimoStatoSmistamento.Oid).FirstOrDefault();
        //            return _old_SSmistamento_Oid;
        //        }
        //        return _old_SSmistamento_Oid;

        //    }
        //    set { _old_SSmistamento_Oid = value; }
        //}

        private int fold_SSmistamento_Oid;
        [Persistent("OLD_SSMISTAMENTO")]
        [MemberDesignTimeVisibility(false)]
        public int old_SSmistamento_Oid
        {
            get { return fold_SSmistamento_Oid; }
            set { SetPropertyValue<int>("old_SSmistamento_Oid", ref fold_SSmistamento_Oid, value); }
            //get { return GetDelayedPropertyValue<int>("old_SSmistamento_Oid"); }
            //set { SetDelayedPropertyValue<int>("old_SSmistamento_Oid", value); }
        }

        //[NonPersistent]
        //[Browsable(false)]
        //public StatoSmistamento old_SSmistamento
        //{
        //    get
        //    {
        //        if (this.Oid > 0)
        //        {
        //            int OidSSmistamento = Session.Query<RegistroRdL>().Where(d => d.Oid == this.Oid)
        //                                 .Select(s => s.UltimoStatoSmistamento.Oid).FirstOrDefault();
        //            return Session.GetObjectByKey<StatoSmistamento>(OidSSmistamento);
        //        }
        //        return null;
        //    }
        //}



        #endregion

        #region Stato Operativo Selezionabile
        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("ULTIMOSTATOOPERATIVO"), XafDisplayName("Ultimo Stato Operativo")]
        // [DataSourceProperty("ListaFiltraComboSOperativo")]
        [DataSourceCriteria("[<StatoSmistamento_SOperativoSO>][^.Oid == StatoOperativoSO.Oid And StatoSmistamento == '@This.old_SSmistamento'] ")]
        [ImmediatePostData(true)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public StatoOperativo UltimoStatoOperativo
        {
            get { return GetDelayedPropertyValue<StatoOperativo>("UltimoStatoOperativo"); }
            set { SetDelayedPropertyValue<StatoOperativo>("UltimoStatoOperativo", value); }
            //get
            //{
            //    return fUltimoStatoOperativo;
            //}
            //set
            //{
            //    SetPropertyValue<StatoOperativo>("UltimoStatoOperativo", ref fUltimoStatoOperativo, value);
            //}
        }





        #endregion
        private Categoria fCategoria;
        [Persistent("CATEGORIA"), XafDisplayName("Categoria Manutenzione")]
        [Appearance("RdL.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        //[ExplicitLoading()]
        [Delayed(true)]
        public Categoria Categoria
        {
            get { return GetDelayedPropertyValue<Categoria>("Categoria"); }
            set { SetDelayedPropertyValue<Categoria>("Categoria", value); }

            //get
            //{
            //    return fCategoria;
            //}
            //set
            //{
            //    SetPropertyValue<Categoria>("Categoria", ref fCategoria, value);
            //}
        }


        #region #######################################   Risorse team  ####################
        private RisorseTeam fRisorseTeam;
        [Persistent("RISORSATEAM"), XafDisplayName("Team")]
        [Association(@"RegistroRdL_RisorseTeam"), System.ComponentModel.DisplayName("Risorsa Team")]
        [ImmediatePostData(true)]        //[DataSourceProperty("ListaFiltraComboRisorseTeam")]
        [Appearance("RegistroRdL.RisorseTeam.Enabled", Enabled = false)]
        [Appearance("RegistroRdL.RisorseTeam.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(RisorseTeam) AND ([UltimoStatoSmistamento.Oid] In(2,11))", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("RegistroRdL.RisorseTeam.nero", AppearanceItemType.LayoutItem, "not IsNullOrEmpty(RisorseTeam)", FontStyle = FontStyle.Bold, FontColor = "Black")]
        [Appearance("RegistroRdL.RisorseTeam.neroItem", AppearanceItemType.ViewItem, "not IsNullOrEmpty(RisorseTeam)", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        [Delayed(true)]
        public RisorseTeam RisorseTeam
        {
            get { return GetDelayedPropertyValue<RisorseTeam>("RisorseTeam"); }
            set { SetDelayedPropertyValue<RisorseTeam>("RisorseTeam", value); }
            //get
            //{
            //    return fRisorseTeam;
            //}
            //set
            //{
            //    SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            //}
        }

        private string fRicercaRisorseTeam;
        [NonPersistent, Size(25)]//, DisplayName("Filtro"), , Size(25)
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaRisorseTeam
        {
            get;
            set;
        }



        private RegistroRdL fRegRdlSuccessivo;
        [Persistent("REGRDL_SUCCESSIVO"), XafDisplayName("Registro RdL Successivo")]
        [Appearance("RegistroRdL.RegistroRdL.successiva", Enabled = false)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public RegistroRdL RegRdlSuccessivo
        {
            get
            {
                return GetDelayedPropertyValue<RegistroRdL>("RegRdlSuccessivo");
            }
            set
            {
                SetDelayedPropertyValue<RegistroRdL>("RegRdlSuccessivo", value);
            }

            //get
            //{
            //    return fRegRdlSuccessivo;
            //}
            //set
            //{
            //    SetPropertyValue<RegistroRdL>("RegRdlSuccessivo", ref fRegRdlSuccessivo, value);
            //}
        }




        //[NonPersistent]
        //private XPCollection<RisorseTeam> fListaFiltraComboRisorseTeam;
        //[MemberDesignTimeVisibility(false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RisorseTeam> ListaFiltraComboRisorseTeam
        //{
        //    get
        //    {
        //        return null;
        //        if (this.Oid == -1) return null;
        //        if (this.UltimoStatoSmistamento != null && this.Immobile != null && this.Impianto != null)
        //        {
        //            if (fListaFiltraComboRisorseTeam == null && (this.UltimoStatoSmistamento.Oid == 1 || this.UltimoStatoSmistamento.Oid == 2))
        //            {

        //                var ParCriteria = string.Empty;
        //                string OiDRdL = this.RdLes.FirstOrDefault().Oid.ToString();
        //                using (CAMS.Module.Classi.UtilController uc = new CAMS.Module.Classi.UtilController())
        //                {
        //                    ParCriteria = uc.GetFiltraComboRisorseTeam(OiDRdL, this.Immobile);
        //                }
        //                fListaFiltraComboRisorseTeam = new XPCollection<RisorseTeam>(Session);
        //                fListaFiltraComboRisorseTeam.Criteria = CriteriaOperator.Parse(ParCriteria);

        //                OnChanged("ListaFiltraComboRisorseTeam");
        //            }
        //        }
        //        return fListaFiltraComboRisorseTeam;
        //    }
        //}


        #endregion



        #region ############################     Data Ora  OPERATIVE ##################################à
        private DateTime fDataSopralluogo;
        [Persistent("DATA_SOPRALLUOGO"), XafDisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Sopralluogo dell intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(true), VisibleInLookupListView(true)]
        [Delayed(true)]
        public DateTime DataSopralluogo
        {
            get { return GetDelayedPropertyValue<DateTime>("DataSopralluogo"); }
            set { SetDelayedPropertyValue<DateTime>("DataSopralluogo", value); }
            //get
            //{
            //    return fDataSopralluogo;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value);
            //}
        }

        private DateTime fDataAzioniTampone;
        [Persistent("DATA_AZIONI_TAMPONE"), XafDisplayName("Data Azioni Tampone")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data dell'Intervento per la messa in Sicurezza ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(true), VisibleInLookupListView(true)]
        [Delayed(true)]
        public DateTime DataAzioniTampone
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAzioniTampone"); }
            set { SetDelayedPropertyValue<DateTime>("DataAzioniTampone", value); }
            //get
            //{
            //    return fDataAzioniTampone;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataAzioniTampone", ref fDataAzioniTampone, value);
            //}
        }


        private DateTime fDataInizioLavori;
        [Persistent("DATA_INIZIO_LAVORI"), XafDisplayName("Data Inizio Lavori")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Inizio Lavori ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(true), VisibleInLookupListView(true)]
        [Delayed(true)]
        public DateTime DataInizioLavori
        {
            get { return GetDelayedPropertyValue<DateTime>("DataInizioLavori"); }
            set { SetDelayedPropertyValue<DateTime>("DataInizioLavori", value); }

            //get
            //{
            //    return fDataInizioLavori;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataInizioLavori", ref fDataInizioLavori, value);
            //}
        }

        #endregion


        #region @@@@@@@@@@@@@@@@@@@@@@@  FLAG DI VISUALIZZAZIONE @@@@@@@@@@@@@@@@@@

        [Persistent("MOSTRADATAFERMO"), XafDisplayName("Mostra Data Fermo")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraDataOraFermo
        {

            get { return GetDelayedPropertyValue<bool>("MostraDataOraFermo"); }
            set { SetDelayedPropertyValue<bool>("MostraDataOraFermo", value); }

        }

        [Persistent("MOSTRADATARIAVVIO"), XafDisplayName("Mostra Data Riavvio")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraDataOraRiavvio
        {
            get { return GetDelayedPropertyValue<bool>("MostraDataOraRiavvio"); }
            set { SetDelayedPropertyValue<bool>("MostraDataOraRiavvio", value); }

        }
        //    get
        //    {
        //        try
        //        {
        //            var _FLAG = this.AutorizzazioniRegistroRdLs.
        //                   Where(W => W.TipoAutorizzazioniRegRdL == Classi.TipoAutorizzazioniRegRdL.MostraDataOraRiavvio).Count();
        //            if (RdLes.Count > 0 && _FLAG == 1)
        //            {
        //                return true;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //}



        [Persistent("MOSTRADATAINIZIOLAVORI"), XafDisplayName("Abilita Modifiche Data Inizio lavori")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraDataOraInizioLavori
        {
            get { return GetDelayedPropertyValue<bool>("MostraDataOraInizioLavori"); }
            set { SetDelayedPropertyValue<bool>("MostraDataOraInizioLavori", value); }

        }
        //    get
        //    {
        //        try
        //        {
        //            var _FLAG = this.AutorizzazioniRegistroRdLs.
        //                   Where(W => W.TipoAutorizzazioniRegRdL == Classi.TipoAutorizzazioniRegRdL.MostraDataOraInizioLavori).Count();
        //            if (RdLes.Count > 0 && _FLAG == 1)
        //            {
        //                return true;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //}

        [Persistent("MOSTRADATASICUREZZA"), XafDisplayName("Mostra Data Intervento Sicurezza")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraDataOraAzioniTampone
        {
            get { return GetDelayedPropertyValue<bool>("MostraDataOraAzioniTampone"); }
            set { SetDelayedPropertyValue<bool>("MostraDataOraAzioniTampone", value); }

        }
        //    get
        //    {
        //        try
        //        {
        //            var _FLAG = this.AutorizzazioniRegistroRdLs.
        //                   Where(W => W.TipoAutorizzazioniRegRdL == Classi.TipoAutorizzazioniRegRdL.MostraDataOraAzioniTampone).Count();
        //            if (RdLes.Count > 0 && _FLAG == 1)
        //            {
        //                return true;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //}


        private bool fMostraDataSopralluogo;
        [Persistent("MOSTRADATASOPRALLUOGO"), XafDisplayName("Mostra Data Sopralluogo")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraDataOraSopralluogo
        {

            get { return GetDelayedPropertyValue<bool>("MostraDataOraSopralluogo"); }
            set { SetDelayedPropertyValue<bool>("MostraDataOraSopralluogo", value); }

        }
        //    get
        //    {
        //        try
        //        {
        //            var _FLAG = this.AutorizzazioniRegistroRdLs.
        //                   Where(W => W.TipoAutorizzazioniRegRdL == Classi.TipoAutorizzazioniRegRdL.MostraDataOraSopralluogo).Count();
        //            if (RdLes.Count > 0 && _FLAG == 1)
        //            {
        //                return true;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //}






        [Persistent("MOSTRADATACOMPLETAMENTO"), XafDisplayName("Mostra Data Completamento")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraDataOraCompletamento
        {
            get { return GetDelayedPropertyValue<bool>("MostraDataOraCompletamento"); }
            set { SetDelayedPropertyValue<bool>("MostraDataOraCompletamento", value); }

        }
        //    get
        //    {
        //        try
        //        {
        //            var _FLAG = this.AutorizzazioniRegistroRdLs.
        //                   Where(W => W.TipoAutorizzazioniRegRdL == Classi.TipoAutorizzazioniRegRdL.MostraDataOraCompletamento).Count();
        //            if (RdLes.Count > 0 && _FLAG == 1)
        //            {
        //                return true;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //}






        [Persistent("MOSTRAPROBLEMICAUSARIMEDI"), XafDisplayName("Mostra Elenco Cause Rimedi")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public bool MostraElencoCauseRimedi
        {
            get { return GetDelayedPropertyValue<bool>("MostraElencoCauseRimedi"); }
            set { SetDelayedPropertyValue<bool>("MostraElencoCauseRimedi", value); }

        }
        //    get
        //    {
        //        try
        //        {
        //            var _FLAG = this.AutorizzazioniRegistroRdLs.
        //                   Where(W => W.TipoAutorizzazioniRegRdL == Classi.TipoAutorizzazioniRegRdL.MostraElencoCauseRimedi).Count();
        //            if (RdLes.Count > 0 && _FLAG == 1)
        //            {
        //                return true;
        //            }
        //        }
        //        catch
        //        {
        //            return false;
        //        }
        //        return false;
        //    }
        //}



        #endregion





        #region Dati di Completamento
        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), XafDisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public DateTime DataCompletamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataCompletamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataCompletamento", value); }
            //get
            //{
            //    return fDataCompletamento;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            //}
        }

        private string fNoteCompletamento;
        [Persistent("NOTECOMPLETAMENTO"), Size(4000)]
        [DbType("varchar(4000)")]
        [Delayed(true)]
        public string NoteCompletamento
        {
            get { return GetDelayedPropertyValue<string>("NoteCompletamento"); }
            set { SetDelayedPropertyValue<string>("NoteCompletamento", value); }

            //get
            //{
            //    return fNoteCompletamento;
            //}
            //set
            //{
            //    SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value);
            //}
        }
        private DateTime fDataRiavvio;
        [Persistent("DATARIAVVIO"), XafDisplayName("Data Riavvio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data del riavvio successivo al guasto", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DataRiavvio
        {
            get { return GetDelayedPropertyValue<DateTime>("DataRiavvio"); }
            set { SetDelayedPropertyValue<DateTime>("DataRiavvio", value); }
            //get
            //{
            //    return fDataRiavvio;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataRiavvio", ref fDataRiavvio, value);
            //}
        }

        #region   @@@@@@@@@@@@@@@@@@@@@@@@@    PROBLEMA CAUSA RIMEDIO
        //private ApparatoProblema fProblema;
        //[Persistent("PCRAPPPROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        ////[Appearance("RdL.Abilita.Problema", Criteria = "Apparato is null", FontColor = "Black", Enabled = false)]
        //[System.ComponentModel.Browsable(false)]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public ApparatoProblema Problema
        //{
        //    get { return GetDelayedPropertyValue<ApparatoProblema>("Problema"); }
        //    set { SetDelayedPropertyValue<ApparatoProblema>("Problema", value); }
        //}
        [Persistent("PCR_PROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        [Appearance("RegistroRdL.Abilita.Prob", Criteria = "[Apparato] is null Or [Apparato].StdApparato is null Or [Causa] Is Not Null", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true), VisibleInListView(false)]      //  [DataSourceProperty("ListaFiltraApparatoProblemas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        [DataSourceCriteria("[<ApparatoProblema>][^.Oid == Problemi.Oid And StdApparato.Oid == '@This.Apparato.StdApparato.Oid']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Problemi Prob
        {
            get
            {
                return GetDelayedPropertyValue<Problemi>("Prob");
            }
            set
            {
                SetDelayedPropertyValue<Problemi>("Prob", value);
            }
        }



        //private ProblemaCausa fProblemaCausa;
        //[Persistent("PCRPROBCAUSA"), System.ComponentModel.DisplayName("Causa")]
        //[System.ComponentModel.Browsable(false)]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public ProblemaCausa ProblemaCausa
        //{
        //    get { return GetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa"); }
        //    set { SetDelayedPropertyValue<ProblemaCausa>("ProblemaCausa", value); }
        //}

        [Persistent("PCR_CAUSA"), System.ComponentModel.DisplayName("Causa")]
        [Appearance("RegistroRdL.Causa", Enabled = false, Criteria = "IsNullOrEmpty(Prob) Or not IsNullOrEmpty(Rimedio)", FontColor = "Black", Context = "DetailView")]
        [ImmediatePostData(true), VisibleInListView(false)]        // [DataSourceProperty("ListaApparatoProblemaCausas", DevExpress.Persistent.Base.DataSourcePropertyIsNullMode.SelectAll)]
        //[DataSourceCriteria("ApparatoProblema = '@This.Problema'")]  //filtra per apparato
        [DataSourceCriteria("[<ProblemaCausa>][^.Oid == Cause.Oid And ApparatoProblema.Problemi.Oid == '@This.Prob']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Cause Causa
        {
            get { return GetDelayedPropertyValue<Cause>("Causa"); }
            set { SetDelayedPropertyValue<Cause>("Causa", value); }
        }


        //private CausaRimedio fCausaRimedio;
        //[Persistent("PCRCAUSARIMEDIO"), XafDisplayName("Rimedio")]
        ////  [Appearance("RdL.CausaRimedio", Enabled = false, Criteria = "IsNullOrEmpty(ProblemaCausa)", Context = "DetailView")]
        //[System.ComponentModel.Browsable(false)]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public CausaRimedio CausaRimedio
        //{
        //    get { return GetDelayedPropertyValue<CausaRimedio>("CausaRimedio"); }
        //    set { SetDelayedPropertyValue<CausaRimedio>("CausaRimedio", value); } 
        //}
        [Persistent("PCR_RIMEDIO"), System.ComponentModel.DisplayName("Rimedio"),]
        [Appearance("RegistroRdL.Rimedio", Enabled = false, Criteria = "IsNullOrEmpty(Causa)", FontColor = "Black", Context = "DetailView")]
        [ImmediatePostData, VisibleInListView(false)]
        [DataSourceCriteria("[<CausaRimedio>][^.Oid == Rimedi.Oid And ProblemaCausa.Cause.Oid == '@This.Causa']")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Rimedi Rimedio
        {
            get { return GetDelayedPropertyValue<Rimedi>("Rimedio"); }
            set { SetDelayedPropertyValue<Rimedi>("Rimedio", value); }

        }

        #endregion
        private DateTime fDataFermo;
        [Persistent("DATAFERMO"), XafDisplayName("Data Fermo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data del fermo provocato dal guasto", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        // [Appearance("RdL.DataFermo.Hide", Criteria = "Categoria.Oid != 4", Visibility = ViewItemVisibility.Hide)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public DateTime DataFermo
        {
            get { return GetDelayedPropertyValue<DateTime>("DataFermo"); }
            set { SetDelayedPropertyValue<DateTime>("DataFermo", value); }
            //get
            //{
            //    return fDataFermo;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataFermo", ref fDataFermo, value);
            //}
        }
        #endregion
        #region Collection Non Persistent e Alias
        [PersistentAlias("RdLes.Count")]
        [XafDisplayName("Nr RdL")]
        [VisibleInListViewAttribute(true)]
        [ToolTip("Numero di RdL nel Registro")]
        [Appearance("RegRdL.NrRdl.ColorBlack", FontColor = "Black")]
        public int NrRdl
        {
            get
            {
                object tempObject = EvaluateAlias("NrRdl");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else { return 0; }
            }
        }

        private Urgenza fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorità")]
        [RuleRequiredField("RuleReq.RegRdL.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        [Appearance("RegRdL.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Urgenza Priorita
        {
            get { return GetDelayedPropertyValue<Urgenza>("Priorita"); }
            set { SetDelayedPropertyValue<Urgenza>("Priorita", value); }

            //get
            //{
            //    return fPriorita;
            //}
            //set
            //{
            //    SetPropertyValue<Priorita>("Priorita", ref fPriorita, value);
            //}
        }



        private Immobile fImmobile;
        [PersistentAlias("Iif(Apparato is not null,Apparato.Impianto.Immobile,null)"), XafDisplayName("Immobile")]
        [Appearance("RegRdL.Immobile.ColorBlack", FontColor = "Black")]
        public Immobile Immobile
        {
            get
            {
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                {
                    return (Immobile)tempObject;
                }
                else
                {
                    return null;
                }

            }
        }


        private Servizio fImpianto;
        // [NonPersistent, XafDisplayName("Impianto")]
        [PersistentAlias("Iif(Apparato is not null,Apparato.Servizio,null)"), XafDisplayName("Servizio")]
        [Appearance("RegRdL.Impianto.ColorBlack", FontColor = "Black")]
        public Servizio Servizio
        {
            get
            {
                var tempObject = EvaluateAlias("Servizio");
                if (tempObject != null)
                {
                    return (Servizio)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        private Asset fApparato;
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset")]
        [Appearance("RegRdL.Asset.ColorBlack", FontColor = "Black", Enabled = false)]
        //   [DataSourceCriteria("Impianto = '@This.Impianto'")]
        //  [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Asset
        {
            get { return GetDelayedPropertyValue<Asset>("Asset"); }
            set { SetDelayedPropertyValue<Asset>("Asset", value); }
            //get
            //{
            //    return fApparato;
            //}
            //set
            //{
            //    SetPropertyValue<Apparato>("Apparato", ref fApparato, value);
            //}
        }

        //[PersistentAlias("Iif(Apparato is not null,Apparato.Impianto,null)")]  RdLes
        //[PersistentAlias("Iif([RdL][^.Oid = RegistroRdL And Apparato != ^.Apparato].Count() > 0,'+ Apparati','uno Apparato')")]
        [PersistentAlias("Iif(RdLes[Apparato != ^.Apparato].Count() > 0 ,'diversi Asset','un solo Asset')")]

        [XafDisplayName("num Asset")]
        [Appearance("RegRdL.nrAsset.ColorBlack", FontColor = "Black")]
        public string numApparati
        {
            get
            {
                var tempObject = EvaluateAlias("numAsset");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return "na";
                }
            }
        }
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public bool UserIsAdminRuolo
        {
            get
            {
                return CAMS.Module.Classi.SetVarSessione.IsAdminRuolo;
            }
        }
        

        //private string _UrlCompleta = string.Empty;
        //[NonPersistent]
        //[MemberDesignTimeVisibility(false)]
        //[EditorAlias("HyperLinkPropertyEditor")]
        //public string UrlCompleta
        //{
        //    get
        //    {
        //        var tempUrl = "http://" + Classi.SetVarSessione.HostName;
        //        if (Classi.SetVarSessione.ServerPort > 0)
        //        {
        //            tempUrl += ":" + Classi.SetVarSessione.ServerPort.ToString();
        //        }
        //        var db = new Classi.DB();
        //        var Messaggio = string.Empty;
        //        Messaggio = db.linkCompletamento();
        //        return tempUrl + String.Format("{0}?id={1}&username={2}&password={3}", Messaggio, Oid.ToString(), Crypto.Encrypt(SecuritySystem.CurrentUserName, Crypto.CamsSalt), Crypto.Encrypt(string.Empty, Crypto.CamsSalt));
        //    }
        //    set
        //    {
        //        SetPropertyValue("Url", ref _UrlCompleta, value);
        //    }
        //}

        #endregion

        #region Associazioni
        //[Association(@"REGISTRORDLRefDETTAGLIOSMISTAMENTO", typeof(RegistroSmistamentoDett)), XafDisplayName("Registro Smistamento Dettaglio")]
        //[Appearance("RegRdL.RegistroSmistamentoDettaglios.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or RegistroSmistamentoDettaglios.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RegistroSmistamentoDett> RegistroSmistamentoDettaglios
        //{
        //    get
        //    {
        //        return GetCollection<RegistroSmistamentoDett>("RegistroSmistamentoDettaglios");
        //    }
        //}


        //[Association(@"REGISTRORDL_AutorizzazioniRegistroRdL", typeof(AutorizzazioniRegistroRdL)), XafDisplayName("Autorizzazioni Registro")]
        //// [Appearance("RegRdL.AutorizzazioniRegistroRdLs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or AutorizzazioniRegistroRdLs.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //[Browsable(false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<AutorizzazioniRegistroRdL> AutorizzazioniRegistroRdLs
        //{
        //    get
        //    {
        //        return GetCollection<AutorizzazioniRegistroRdL>("AutorizzazioniRegistroRdLs");
        //    }
        //}
        //[Association(@"REGISTRORDLRefDETTAGLIOOPERATIVO", typeof(RegistroOperativoDettaglio)), XafDisplayName("Registro Operativo Dettaglio")]
        //[Appearance("RegRdL.RegistroOperativoDettaglios.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or RegistroOperativoDettaglios.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //public XPCollection<RegistroOperativoDettaglio> RegistroOperativoDettaglios
        //{
        //    get
        //    {
        //        return GetCollection<RegistroOperativoDettaglio>("RegistroOperativoDettaglios");
        //    }
        //}
        [Association(@"Notifiche_RegistroRdL", typeof(RegNotificheEmergenze)), XafDisplayName("Elenco Notifiche Emergenze")]
        [Appearance("RegRdL.RegNEmergenzes.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or Categoria.Oid != 4 Or RegNEmergenzes.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RegNotificheEmergenze> RegNEmergenzes
        {
            get
            {
                return GetCollection<RegNotificheEmergenze>("RegNEmergenzes");
            }
        }
        [Association(@"REGISTRORDLRefRdl", typeof(RdL)), XafDisplayName("RdL")]
        [Appearance("RegRdL.RdLes.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or RdLes.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RdL> RdLes
        {
            get
            {
                return GetCollection<RdL>("RdLes");
            }
        }

        [Association(@"REGISTRORDLRefOdl", typeof(OdL)), XafDisplayName("OdL")]
        [Appearance("RegRdL.OdLes.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or [UltimoStatoSmistamento.Oid] = 1 Or OdLes.Count = 0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<OdL> OdLes
        {
            get
            {
                return GetCollection<OdL>("OdLes");
            }
        }

        // 8	Rendicontazione Operativa  not([UltimoStatoSmistamento.Oid] In(8,9))"  ''. 
        // 9	Rendicontazione Economica
        [Association(@"RegistroLavori_RegistroRdL", typeof(RegistroLavori)), XafDisplayName("Registro Lavori")]
        [Appearance("RegRdL.RegistroLavori.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or not([UltimoStatoSmistamento.Oid] In(8,9))", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroLavori> RegistroLavoris
        {
            get
            {
                return GetCollection<RegistroLavori>("RegistroLavoris");
            }
        }

        [Association(@"RegistroLavoriAltreRegRdL_RegistroRdL", typeof(RegistroLavoriAltreRegRdL)), XafDisplayName("Registro Lavori ")]//Oid = -1 Or not([UltimoStatoSmistamento.Oid] In(8,9))
        //[Appearance("RegRdL.RegistroCostiAltreRegRdLs.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [Browsable(false)]
        public XPCollection<RegistroLavoriAltreRegRdL> RegistroLavoriAltreRegRdLs
        {
            get
            {
                return GetCollection<RegistroLavoriAltreRegRdL>("RegistroLavoriAltreRegRdLs");
            }
        }

        [Association(@"Documenti_RegRdL", typeof(Documenti)), System.ComponentModel.DisplayName("Documenti")]
        [Appearance("RdL.Documentis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }


        #endregion


        #region GEoreferenza
        private double? fLatUltimaPosiz;
        [Size(50),
        Persistent("GEOLAT"),
        XafDisplayName("Latitudine Ultima Posizione"),
        Appearance("ImpLib.GeoLatUltimaPosiz", Enabled = false)]
        [MemberDesignTimeVisibility(false)]
        public double? GeoLatUltimaPosiz
        {
            get
            {
                return fLatUltimaPosiz;
            }
            set
            {
                SetPropertyValue<double?>("GeoLatUltimaPosiz", ref fLatUltimaPosiz, value);
            }
        }

        private double? fLngUltimaPosiz;
        [Size(50),
        Persistent("GEOLNG"),
        XafDisplayName("Longitudine Ultima Posizione"),
        Appearance("ImpLib.GeoLngUltimaPosiz", Enabled = false)]
        [MemberDesignTimeVisibility(false)]
        public double? GeoLngUltimaPosiz
        {
            get
            {
                return fLngUltimaPosiz;
            }
            set
            {
                SetPropertyValue<double?>("GeoLngUltimaPosiz", ref fLngUltimaPosiz, value);
            }
        }

        private string fIndirizzodaGeo;
        [Size(250),
        Persistent("GEOINDIRIZZO"),
        XafDisplayName("Indirizzo Vicino")]
        [DbType("varchar(250)")]
        [MemberDesignTimeVisibility(false)]
        public string IndirizzodaGeo
        {
            get
            {
                return fIndirizzodaGeo;
            }
            set
            {
                SetPropertyValue<string>("IndirizzodaGeo", ref fIndirizzodaGeo, value);
            }
        }

        private string _Url = string.Empty;
        [NonPersistent,
        XafDisplayName("Visualizza Ultima Posizione"),
        ToolTip("Visualizza Posizione (Latitudine({GeoLatUltimaPosiz}),Longitudine({GeoLngUltimaPosiz}))")]
        [Appearance("Risorsa.UltimaPosizione.Url", Enabled = false)]
        [MemberDesignTimeVisibility(false)]
        [EditorAlias("HyperLinkPropertyEditor")]
        public string Url
        {
            get
            {
                var tempLat = Evaluate("GeoLatUltimaPosiz");
                var tempLon = Evaluate("GeoLngUltimaPosiz");
                if (tempLat != null && tempLon != null)
                {
                    return String.Format("http://www.google.com/maps/place/{0},{1}/@{0},{1},{2}",
                        tempLat.ToString().Replace(",", "."), tempLon.ToString().Replace(",", "."), "16");
                }
                return null;
            }
            set
            {
                SetPropertyValue("Url", ref _Url, value);
            }
        }
        #endregion


        #region MetodiVari
        private string ToDeg(double? dblVal, bool isLng)
        {
            if (!dblVal.HasValue)
            {
                return null;
            }
            var d = dblVal.Value;
            var Sign = System.Math.Sign(d);
            var Degrees = System.Math.Sign(d) * ((int)(System.Math.Floor(System.Math.Abs(d))));
            var partial = 60D * (System.Math.Abs(d) % 1D);
            var Minutes = (int)(System.Math.Floor(partial));
            var Seconds = 60D * (partial % 1);
            var posPole = isLng ? "E" : "N";
            var negPole = isLng ? "W" : "S";
            return string.Format("{0}° {1}' {2:F3}\"{3}", System.Math.Abs(Degrees), Minutes, Seconds, Sign > 0 ? posPole : negPole);
        }



        /// <param name="xpObjectSpace"></param>
        /// <param name="lstRDLSelezionati"></param>
        /// <returns></returns>
        public static RegistroRdL CreateFrom(IObjectSpace xpObjectSpace, IEnumerable<RdL> lstRDLSelezionati)
        {
            var newReg = xpObjectSpace.CreateObject<RegistroRdL>();

            var Session = newReg.Session;


            var IDs = lstRDLSelezionati.Select(r => r.Oid).ToList();
            var lstRDL = Session.Query<RdL>().Where(r => IDs.Contains(r.Oid));

            newReg.RdLes.AddRange(lstRDL);

            var statoSmistamento = Session.GetObjectByKey<StatoSmistamento>(2);
            newReg.CambioStato(statoSmistamento);

            return newReg;
        }
        /// Cambio di stato
        /// </summary>
        /// <param name="statoSmistamento"></param>
        public void CambioStato(StatoSmistamento statoSmistamento)
        {
            var now = DateTime.Now;

            foreach (RdL rdlSelezionato in RdLes)
            {
                rdlSelezionato.UltimoStatoSmistamento = statoSmistamento;
            }

            //var regSmistamento = new RegistroSmistamentoDett(Session);
            //regSmistamento.RegistroRdL = this;
            //regSmistamento.StatoSmistamento = statoSmistamento;
            //regSmistamento.DataOra = now;

            UltimoStatoSmistamento = statoSmistamento;
            DataAggiornamento = now;
        }
        #endregion

        #region  UTENTE E AGGIORNAMENTO
        private string f_Utente;
        [Persistent("UTENTE"), Size(100), XafDisplayName("Utente")]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }
            //get
            //{
            //    return f_Utente;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Utente", ref f_Utente, value);
            //}
        }

        private string fUtenteUltimo;
        [Persistent("ULTIMOUTENTE"), Size(100), XafDisplayName("Ultimo Utente")]
        [DbType("varchar(100)")]
        [VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        [Delayed(true)]
        public string UtenteUltimo
        {
            get { return GetDelayedPropertyValue<string>("UtenteUltimo"); }
            set { SetDelayedPropertyValue<string>("UtenteUltimo", value); }
            //get
            //{
            //    return fUtenteUltimo;
            //}
            //set
            //{
            //    SetPropertyValue<string>("UtenteUltimo", ref fUtenteUltimo, value);
            //}
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), XafDisplayName("Data Aggiornamento"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [Appearance("RegistroRdL.DataAggiornamento", Enabled = false)]
        [Delayed(true)]
        public DateTime DataAggiornamento  //DataAggiornamento    UltimaDataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }

            //get
            //{
            //    return fDataAggiornamento;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            //}
        }
        #endregion

        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (!this.IsLoading)
        //    {

        //        if (this.Oid > 1)
        //        {
        //            if (newValue != null && propertyName == "UltimoStatoSmistamento")
        //            {
        //                StatoSmistamento newUltimoStatoSmistamento = ((StatoSmistamento)(newValue));
        //                if (newValue != oldValue && newValue != null)
        //                {
        //                    switch (newUltimoStatoSmistamento.Oid)
        //                    {
        //                        case 2:
        //                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);
        //                            break;
        //                        case 5:
        //                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(4);
        //                            break;
        //                        case 7:
        //                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(13);
        //                            break;
        //                        case 6:
        //                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(12);
        //                            break;
        //                        case 4:
        //                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(11);
        //                            break;
        //                        case 11:
        //                            this.UltimoStatoOperativo = Session.GetObjectByKey<StatoOperativo>(19);
        //                            break;
        //                        default:
        //                            this.UltimoStatoOperativo = null;
        //                            break;
        //                    }
        //                }
        //            }
        //        }

        //    }
        //    //Richiedente

        //}


        public override string ToString()
        {
            if (this.Oid == -1)
                return null;
            if (this.Descrizione != null)
                if (Codice != null)
                    return string.Format("{0}({1})", this.Descrizione.Length < 101 ? this.Descrizione : this.Descrizione.Remove(100) + "...", Codice);
                else
                    return string.Format("{0}", this.Descrizione.Length < 101 ? this.Descrizione : this.Descrizione.Remove(100) + "...");

            return null;
        }
    }
}


//[NonPersistent, XafDisplayName("Smistamento")]
//[DevExpress.Xpo.Size(SizeAttribute.Unlimited), ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
//[VisibleInListViewAttribute(true), VisibleInDetailView(false)]
//[ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
//    ListViewImageEditorCustomHeight = 20, DetailViewImageEditorFixedHeight = 20)]
//[ToolTip("{UltimoStatoSmistamento.SSmistamento}")]
//[ExplicitLoading()]
//public System.Drawing.Image ImageSSmistamento
//{
//    get
//    {
//        return null;
//        if (UltimoStatoSmistamento != null)
//        {
//            var Image = UltimoStatoSmistamento.Icona;
//            if (Image != null)
//            {
//                return UltimoStatoSmistamento.Icona;
//            }
//            else
//            {
//                return null;
//            }
//        }
//        return null;
//    }
//    //  set { SetPropertyValue<System.Drawing.Image>("ImageSSmistamento", value); }
//}

//private string fDettaglio;
//[NonPersistent, System.ComponentModel.DisplayName("Dettaglio")]
//[VisibleInDetailView(false)]
//public string Dettaglio
//{
//    get
//    {
//        if (this == null) return null;
//        if (this.Oid == -1) return null;
//        //Evaluate("Scenario"),
//        int OidCategoria = 0;
//        if (this.Categoria != null)
//            OidCategoria = this.Categoria.Oid;

//        Richiedente Richied=null;
//         if (this.RdLes.Count>0)
//             Richied  = this.RdLes[0].Richiedente;

//        using (Classi.UtilController u = new Classi.UtilController())
//        {
//            fDettaglio = u.GetDettaglioRegRdL("RegistroRdL", OidCategoria, Evaluate("Categoria"), this.Oid.ToString(), Richied, Evaluate("Descrizione"),
//                null, null, Evaluate("NrRdl").ToString());
//        }
//        return fDettaglio;
//    }
//}

//private string fStatoDettaglio;
//[NonPersistent, System.ComponentModel.DisplayName("Dettaglio Stato")]
//[VisibleInDetailView(false)]
//public string StatoDettaglio
//{
//    get
//    {
//        if (this.Oid == -1) return null;
//        var sUSSmistamento = string.Empty;
//        var sUSOperativo = string.Empty;
//        if (Evaluate("UltimoStatoOperativo") != null)
//        {
//            sUSOperativo = ((StatoOperativo)Evaluate("UltimoStatoOperativo")).CodStato.ToString();
//        }
//        if (Evaluate("UltimoStatoSmistamento") != null)
//        {
//            sUSSmistamento = ((StatoSmistamento)Evaluate("UltimoStatoSmistamento")).SSmistamento.ToString();
//        }
//        return string.Format("{0} - {1}", sUSSmistamento, sUSOperativo);
//    }
//    //set
//    //{
//    //    SetPropertyValue<string>("StatoDettaglio", ref fStatoDettaglio, value);
//    //}
//}


//private string fPrioritaDett;
//[NonPersistent, System.ComponentModel.DisplayName("Priorità Tipo Intervento")]
//[VisibleInDetailView(false)]
//public string PrioritaDett
//{
//    get
//    {
//        if (this.Oid == -1) return null;
//        var sPriorita = string.Empty;
//        var sTipoIntervento = string.Empty;
//        var sCategoria = string.Empty;

//        if (Evaluate("Categoria") != null)
//        {
//            sCategoria = ((Categoria)Evaluate("Categoria")).Descrizione;
//        }
//        //if (Evaluate("Priorita") != null)
//        //{
//        //    sPriorita = ((Priorita)Evaluate("Priorita")).Descrizione;
//        //}
//        //if (Evaluate("TipoIntervento") != null)
//        //{
//        //    sTipoIntervento = ((TipoIntervento)Evaluate("TipoIntervento")).Descrizione;
//        //}

//        if (sCategoria.Contains("GUASTO"))
//        {
//            fPrioritaDett = string.Format("{0} {1}", sPriorita, sTipoIntervento);
//        }
//        else
//        {
//            fPrioritaDett = string.Format("{0}", "Programmata");
//        }
//        return fPrioritaDett;
//    }
//    //set
//    //{
//    //    SetPropertyValue<string>("PrioritaDett", ref fPrioritaDett, value);
//    //}
//}






















//[NonPersistent]
//private XPCollection<StatoOperativo> fListaFiltraComboSOperativo;
//[System.ComponentModel.DisplayName("Elenco  SOperativo"), MemberDesignTimeVisibility(false)]
//public XPCollection<StatoOperativo> ListaFiltraComboSOperativo
//{
//    get
//    {   // 1	In Lavorazione - In Trasferimento    // 2	In Lavorazione - In Sito Esecuzione // 3	In Lavorazione- Trasf. Verso CO //  4 In Lavorazione - Trasf. Acquisti
//        // 5	In Lavorazione - Attività Accessoria // 6	Sospeso - Intervento Urgente        // 7	Sospeso - Attività Accessoria   //  8  Sospeso - Inaccesseb. Locali
//        // 9	Sospeso - App. Materiali             //10	Sospeso - Fine Giornata             //19	Assegnata-Da prendere in carico // 11 Completato//12	Sospesa da SO//13	Annullato
//        if (this.Oid == -1) return null;
//        if (fListaFiltraComboSOperativo == null && UltimoStatoOperativo != null && UltimoStatoSmistamento != null)
//        {
//            int intSS = Session.Query<RegistroRdL>().Where(d => d.Oid == this.Oid).FirstOrDefault().UltimoStatoSmistamento.Oid;

//            string ParCriteria = string.Empty;
//            using (CAMS.Module.Classi.UtilController uc = new CAMS.Module.Classi.UtilController())
//            {
//                ParCriteria = uc.GetFiltraComboSOperativo(intSS, this.Session);
//            }
//            fListaFiltraComboSOperativo = new XPCollection<StatoOperativo>(Session);
//            fListaFiltraComboSOperativo.Criteria = CriteriaOperator.Parse(ParCriteria);
//            OnChanged("ListaFiltraComboSOperativo");
//        }
//        return fListaFiltraComboSOperativo;
//    }
//}