using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
using CAMS.Module.PropertyEditors;
using DevExpress.Persistent.BaseImpl;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;
using System.Drawing;
using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.Model;
using CAMS.Module.DBTask;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
//namespace CAMS.Module.DBTask.MP
//{
//    class RegistroReportMP
//    {
//    }
//}
namespace CAMS.Module.DBTask.MP
{
    [DefaultClassOptions, Persistent("REGREPORTMP"), System.ComponentModel.DisplayName("Registro Report MP")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Report MP")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]

    [Appearance("RuleInfo.RegistroReportMP.DataDa.Rosso", AppearanceItemType.ViewItem,
          @"[DataDa] > [DataA]", TargetItems = "DataDa", FontColor = "Red")]


    [RuleCriteria("RC.RegistroReportMP.ValidaDate.operation", DefaultContexts.Save, @"[DataDa] > [DataA]",
   CustomMessageTemplate = "La Data Pianificata DA non può essere maggiore di Data Pianificata A!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RegistroReportMP.ValidaDataDa.operation", DefaultContexts.Save, @"[DataDa] Is Null",
   CustomMessageTemplate = "Selezionare una Data Pianificata Da!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RegistroReportMP.ValidaDataA.operation", DefaultContexts.Save, @"[DataA] Is Null",
   CustomMessageTemplate = "Selezionare una Data Pianificata A!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RC.RegistroReportMP.ValidaImpianto.operation", DefaultContexts.Save, @"ImpiantiTutti = False And [Impianto] Is Null",
   CustomMessageTemplate = "Selezionare un Impianto!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RC.RegistroReportMP.ValidaEdificio.operation", DefaultContexts.Save, @"EdificiTutti = False And [Immobile] Is Null",
   CustomMessageTemplate = "Selezionare un immobile!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]

    
            [RuleCriteria("RC.RegistroReportMP.ValidaDataPianificazioneReport.operation", DefaultContexts.Save, @"[DataA] > [DataPianificazioneReport] Or [DataPianificazioneReport] Is Null",
   CustomMessageTemplate = "La Data Pianificata di Generazione Report deve essere maggiore della data di fine periodo di stampa!",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    #region filtro tampone
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI_Anno", "", "Tutto", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI_Anno_eseguito", "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", "eseguito o trasmesso", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI_Anno_da_eseguire", "StatoPOI = 'nonDefinito' Or StatoPOI = 'Pianificato'", "da eseguire", Index = 2)]



    #endregion

    public class RegistroReportMP : XPObject
    {
        public RegistroReportMP() : base() { }
        public RegistroReportMP(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //_old_SSmistamento_Oid = 0;
            if (this.Oid == -1) // crea RdL
            {
                this.fDataPianificazioneReport = DateTime.Now;
                //this.StatoPOI = StatoPOI.nonDefinito;
                //this.DataPianificataSchedulazione = DateTime.Now.AddHours(1);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(1000)")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        [Persistent("COMMESSE"), System.ComponentModel.DisplayName("Commessa")]
        [Appearance("RegistroReportMP.Abilita.Commesse", Criteria = "not (Immobile  is null)", FontColor = "Black", Enabled = false)]
        [RuleRequiredField("RuleReq.RegistroReportMP.Commesse", DefaultContexts.Save, "Commessa è un campo obbligatorio")]
        [Appearance("RegistroReportMP.Commesse.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Commesse)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Abilitato = 'Si'")]  // And Commesse.DataAl <= LocalDateTimeToday()
        [Delayed(true)]
        public Contratti Commesse
        {
            get { return GetDelayedPropertyValue<Contratti>("Commesse"); }
            set { SetDelayedPropertyValue<Contratti>("Commesse", value); }
        }

        private bool fEdificiTutti;
        [Persistent("EDIFICIOTUTTI"), System.ComponentModel.DisplayName("Edifici Tutti")]
        [ImmediatePostData]
        public bool EdificiTutti
        {
            get { return fEdificiTutti; }
            set { SetPropertyValue<bool>("EdificiTutti", ref fEdificiTutti, value); }
        }

        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [Appearance("RegistroReportMP.Abilita.Immobile", Criteria = "not (Impianto  is null)", FontColor = "Black", Enabled = false)]
      //  [RuleRequiredField("RuleReq.RegistroReportMP.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        [Appearance("RegistroReportMP.Immobile.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Immobile)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Commesse = '@This.Commesse'")]
        //[DataSourceCriteria("Abilitato = 'Si'")]  // And Commesse.DataAl <= LocalDateTimeToday()
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }

        private bool fImpiantiTutti;
        [Persistent("IMPIANTITUTTI"), System.ComponentModel.DisplayName("Impianti Tutti")]
        [ImmediatePostData]
        public bool ImpiantiTutti
        {
            get { return fImpiantiTutti; }
            set { SetPropertyValue<bool>("ImpiantiTutti", ref fImpiantiTutti, value); }
        }

        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        [Appearance("RegistroReportMP.Abilita.Servizio", Criteria = "Immobile  is null", FontColor = "Black", Enabled = false)]
      //  [RuleRequiredField("RuleReq.RegistroReportMP.Impianto", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        [Appearance("RegistroReportMP.Impianto.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Servizio)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }

        private DateTime fDataDa;
        [Persistent("DATADA"), System.ComponentModel.DisplayName("Data Pianificata Da")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        //[ToolTip("La data DA Report MP.", "", DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        [ImmediatePostData]
        public DateTime DataDa
        {
            get { return fDataDa; }
            set { SetPropertyValue<DateTime>("DataDa", ref fDataDa, value); }
        }

        private DateTime fDataA;
        [Persistent("DATAA"), System.ComponentModel.DisplayName("Data Pianificata A")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        //[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        //[ToolTip("La data A Report MP.", "", DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        [ImmediatePostData]
        public DateTime DataA
        {
            get { return fDataA; }
            set { SetPropertyValue<DateTime>("DataA", ref fDataA, value); }
        }


        private DateTime fDataPianificazioneReport;
        [Persistent("DATAPIANIFICATA")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data Pianificata Stampa Report")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [RuleRequiredField("RuleReq.RegistroReportMP.Impianto", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        //[Appearance("RegistroReportMP.Abilita.DataPianificazioneReport", FontColor = "Black", Enabled = false)]
        public DateTime DataPianificazioneReport
        {
            get { return fDataPianificazioneReport; }
            set { SetPropertyValue<DateTime>("DataPianificazioneReport", ref fDataPianificazioneReport, value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data Aggiornamento")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RegistroReportMP.Abilita.DataAggiornamento", FontColor = "Black", Enabled = false)]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }


        private DateTime fDataElaborazioneReportMP;
        [Persistent("DATAELABORAZIONEREPORTMP")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Data Elaborazione Report MP")]
        [VisibleInListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [Appearance("RegistroReportMP.Abilita.fDataElaborazioneReportMP", FontColor = "Black", Enabled = false)]
        public DateTime DataElaborazioneReportMP
        {
            get { return fDataElaborazioneReportMP; }
            set { SetPropertyValue<DateTime>("DataElaborazioneReportMP", ref fDataElaborazioneReportMP, value); }
        }

        private StatoReportMP fStatoReportMP;
        [Persistent("STATOREPORT")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Stato Smistamento MP")]
        //[Appearance("RegistroReportMP.Abilita.StatoReportMP", FontColor = "Black", Enabled = false)]
        public StatoReportMP StatoReportMP
        {
            get { return fStatoReportMP; }
            set { SetPropertyValue<StatoReportMP>("StatoReportMP", ref fStatoReportMP, value); }
        }

        private StatoElaborazioneJob fStatoElaborazioneJob;
        [Persistent("STATOELABORAZIONEJOB"), DevExpress.Xpo.DisplayName("Stato Elaborazione Job")]
        [Appearance("RegistroReportMP.StatoElaborazioneJob.Enabled", AppearanceItemType.LayoutItem, "1=1", Enabled = false, FontStyle = FontStyle.Bold, FontColor = "Black")]
        public StatoElaborazioneJob StatoElaborazioneJob
        {
            get { return fStatoElaborazioneJob; }
            set { SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneJob", ref fStatoElaborazioneJob, value); }
        }

        [Association(@"RegistroReportMP_File", typeof(RegistroReportMPDett)), DevExpress.Xpo.Aggregated]
        [XafDisplayName("Report Associati")]
        [Appearance("RegistroReportMP.RegistroReportMPDett.Count.HideLayoutItem",
                                    AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RegistroReportMPDett> RegistroReportMPDetts
        {
            get
            {
                return GetCollection<RegistroReportMPDett>("RegistroReportMPDetts");
            }
        }


        private DateTime fDataNow;
        [NonPersistent]
        [MemberDesignTimeVisibility(false)]
        public DateTime DataNow
        {
            get
            {
                return DateTime.Now;
            }

        }
    }

    public enum StatoReportMP
    {
        [XafDisplayName("Tutti")]
        Tutti,
        [XafDisplayName("Da Completare")]
        DaCompletare,
        [XafDisplayName("Completate")]
        Completate
    }

}
