
using CAMS.Module.Classi;
using CAMS.Module.DBTransazioni;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
//using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Drawing;

 

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDTIMPORTTENTATIVI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tentativi DataImport")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Data Import")]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]
    //StatoElaborazioneJob.inEsecuzione
    [Appearance("RegistroDataImportTentativi.StatoElaborazioneImport", TargetItems = "*;StatoElaborazioneImport", Priority = 1, Criteria = "StatoElaborazioneImport = 'inEsecuzione'", Enabled = false)]
    [VisibleInDashboards(false)]
    public class RegistroDataImportTentativi : XPObject
    {
        public RegistroDataImportTentativi() : base() { }
        public RegistroDataImportTentativi(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        private RegistroDataImport fRegistroDataImport;
        [Association(@"RegistroDataImport.RegistroDataImportTentativi"),
        Persistent("REGDATAIMPORT"), DevExpress.ExpressApp.DC.XafDisplayName("Registro DataImport Tentativi")]
        [ExplicitLoading()]
        public RegistroDataImport RegistroDataImport
        {
            get { return fRegistroDataImport; }
            set { SetPropertyValue<RegistroDataImport>("RegistroDataImport", ref fRegistroDataImport, value); }
        }

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Abilitato
        {
            get { return fAbilitato; }
            set { SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value); }
        }

        [Persistent("FILEDATAIMPORT"), DevExpress.Xpo.DisplayName("File Data Import")]
        //  [RuleRequiredField("RReqField.RegistroDataImportTentativi.FileDataImport", DefaultContexts.Save, "File Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public FileData FileDataImport
        {
            get { return GetDelayedPropertyValue<FileData>("FileDataImport"); }
            set { SetDelayedPropertyValue<FileData>("FileDataImport", value); }
        }

        private StatoElaborazioneJob fStatoElaborazioneImport;
        [Persistent("STATOELABORAZIONE"), Size(250), DevExpress.Xpo.DisplayName("Stato Elaborazione")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.TipoDiAcquisizione", DefaultContexts.Save, "Il Tipo Di Acquisizione è un campo obbligatorio")]
        public StatoElaborazioneJob StatoElaborazioneImport
        {
            get { return fStatoElaborazioneImport; }
            set { SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneImport", ref fStatoElaborazioneImport, value); }
        }

        private StepImportazione fStepImportazione;
        [Persistent("STEPIMPORTAZIONE"), Size(250), DevExpress.Xpo.DisplayName("Step in Elaborazione")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.TipoDiAcquisizione", DefaultContexts.Save, "Il Tipo Di Acquisizione è un campo obbligatorio")]
        public StepImportazione StepImportazione
        {
            get { return fStepImportazione; }
            set { SetPropertyValue<StepImportazione>("StepImportazione", ref fStepImportazione, value); }
        }

        private TipoAcquisizione fTipoAcquisizione;
        [Persistent("TIPODIACQUISIZIONE"), Size(250), DevExpress.Xpo.DisplayName("Tipo Di Acquisizione")]
        [RuleRequiredField("RReqField.RegistroDataImportTentativi.TipoDiAcquisizione", DefaultContexts.Save, "Il Tipo Di Acquisizione è un campo obbligatorio")]
        public TipoAcquisizione TipoAcquisizione
        {
            get { return fTipoAcquisizione; }
            set { SetPropertyValue<TipoAcquisizione>("TipoAcquisizione", ref fTipoAcquisizione, value); }
        }

        private const string DateAndTimeOfDayEditMaskit = "dd/MM/yyyy H:mm:ss";
        private DateTime fDataPianificazione;
        [Persistent("DATAPIANIFICAZIONE"), DevExpress.Xpo.DisplayName("Data Pianificazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskit + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskit)]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.DataInizioTentativo", DefaultContexts.Save, "La Data Inizio Tentativo è un campo obbligatorio")]
        public DateTime DataPianificazione
        {
            get { return fDataInizioTentativo; }
            set { SetPropertyValue<DateTime>("DataPianificazione", ref fDataPianificazione, value); }
        }



        private DateTime fDataInizioTentativo;
        [Persistent("DATAINIZIOTENTATIVO"), DevExpress.Xpo.DisplayName("Data Inizio Tentativo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskit + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskit)]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.DataInizioTentativo", DefaultContexts.Save, "La Data Inizio Tentativo è un campo obbligatorio")]
        public DateTime DataInizioTentativo
        {
            get { return fDataInizioTentativo; }
            set { SetPropertyValue<DateTime>("DataInizioTentativo", ref fDataInizioTentativo, value); }
        }


        private DateTime fDataFineTentativo;
        [Persistent("DATAFINETENTATIVO"), DevExpress.Xpo.DisplayName("Data Fine Tentativo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskit + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskit)]
        // [RuleRequiredField("RReqField.RegistroDataImportTentativi.DataFineTentativo", DefaultContexts.Save, "La Data Fine Tentativo è un campo obbligatorio")]
        public DateTime DataFineTentativo
        {
            get { return fDataFineTentativo; }
            set { SetPropertyValue<DateTime>("DataFineTentativo", ref fDataFineTentativo, value); }
        }

        private FileData fOutputFileDataImport;
        [Persistent("OTPUTFILEDATAIMPORT"), DevExpress.Xpo.DisplayName("Output File Data Import")]
        [Delayed(true)]
        // [RuleRequiredField("RReqField.RegistroDataImportTentativi.OutputFileDataImport", DefaultContexts.Save, "La Output File Data Import è un campo obbligatorio")]
        public FileData OutputFileDataImport
        {
            get { return GetDelayedPropertyValue<FileData>("OutputFileDataImport"); }
            set { SetDelayedPropertyValue<FileData>("OutputFileDataImport", value); }
        }


        //private FilePopolamentoConforme fFilePopolamento;
        //[Persistent("FILECONFORMEPOPOLAMENTO"), Size(100), DevExpress.Xpo.DisplayName("File Popolamento Conforme SI/NO")]
        //// [RuleRequiredField("RReqField.RegistroDataImportTentativi.FilePopolamento", DefaultContexts.Save, "Il File Popolamento è un campo obbligatorio")]

        //public FilePopolamentoConforme FilePopolamento
        //{
        //    get { return fFilePopolamento; }
        //    set { SetPropertyValue<FilePopolamentoConforme>("FilePopolamento", ref fFilePopolamento, value); }
        //}

        private Eseguito fImportazione;
        [Persistent("IMPORTAZIONEESEGUITA"), Size(100), DevExpress.Xpo.DisplayName("Importazione Eseguita")]
        [DbType("varchar(100)")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.ImportazioneEseguita", DefaultContexts.Save, "La Importazione Eseguita è un campo obbligatorio")]
        public Eseguito ImportazioneEseguita
        {
            get { return fImportazione; }
            set { SetPropertyValue<Eseguito>("ImportazioneEseguita", ref fImportazione, value); }
        }

        [Persistent("TRANSAZIONIREGISTRO"), DevExpress.Xpo.DisplayName("Registro Transazioni")]
        [Delayed(true)]
        public RegistroTransazioni RegistroTransazioni
        {
            get { return GetDelayedPropertyValue<RegistroTransazioni>("RegistroTransazioni"); }
            set { SetDelayedPropertyValue<RegistroTransazioni>("RegistroTransazioni", value); }
        }
       //     da qui comincino le collection	
        [Association(@"RegistroDataImportTentativi.ResultImportCommessa", typeof(ResultImportCommessa))]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Commesse")]
        public XPCollection<ResultImportCommessa> ResultImportCommessas
        {
            get
            {
                return GetCollection<ResultImportCommessa>("ResultImportCommessas");
            }
        }
        
        [Association(@"RegistroDataImportTentativi.ResultImportEdificio", typeof(ResultImportEdificio))]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Edifici")]
        //[ToolTip("Elenco Edifici")]
        public XPCollection<ResultImportEdificio> ResultImportEdificios
        {
            get
            {
                return GetCollection<ResultImportEdificio>("ResultImportEdificios");
            }
        }


        [Association(@"RegistroDataImportTentativi.ResultImportImpianto", typeof(ResultImportImpianto))]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Impianti")]
        //[ToolTip("Elenco Impianti")]
        public XPCollection<ResultImportImpianto> ResultImportImpiantos
        {
            get
            {
                return GetCollection<ResultImportImpianto>("ResultImportImpiantos");
            }
        }
        
        [Association(@"RegistroDataImportTentativi.ResultImportApparato", typeof(ResultImportApparato))]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Apparati")]
        //[ToolTip("Elenco Apparati")]
        public XPCollection<ResultImportApparato> ResultImportApparatos
        {
            get
            {
                return GetCollection<ResultImportApparato>("ResultImportApparatos");
            }
        }

        [Association(@"RegistroDataImportTentativi.ResultImportXLS", typeof(ResultImportXLS))]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Esito")]
        //[ToolTip("Elenco Apparati")]
        public XPCollection<ResultImportXLS> ResultImportXLSs
        {
            get
            {
                return GetCollection<ResultImportXLS>("ResultImportXLSs");
            }
        }
    }
}


