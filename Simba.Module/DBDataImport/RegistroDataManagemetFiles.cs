using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using System.Threading.Tasks;
#pragma warning restore CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.ComponentModel;
namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDATAMAN_FILES")]
    [System.ComponentModel.DisplayName("Mappa Campi")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "File dati di Aggiornamento")]
    [ImageName("GroupFieldCollection")]
    [System.ComponentModel.DefaultProperty("NomeCampo")]
    [VisibleInDashboards(false)]
    public class RegistroDataManagemetFiles : XPObject
    {
        public RegistroDataManagemetFiles() : base()
        {
        }

        public RegistroDataManagemetFiles(Session session) : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        //kkk
        [Association(@"RegistroDataManagemet.RegistroDataManagemetFiles")]
        [Persistent("REGDATAMAN"), DevExpress.Xpo.DisplayName("Registro Gestione Dati")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.RegistroDataImportTentativi", DefaultContexts.Save, "Il Registro Tentativo Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public RegistroDataManagemet RegistroDataManagemet
        {
            get { return GetDelayedPropertyValue<RegistroDataManagemet>("RegistroDataManagemet"); }
            set { SetDelayedPropertyValue<RegistroDataManagemet>("RegistroDataManagemet", value); }
        }
        //kkk
        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.RegistroDataManagemetFiles.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }
        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Abilitato
        {
            get { return fAbilitato; }
            set { SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value); }
        }

        [Persistent("FILEINPUT"), DevExpress.Xpo.DisplayName("File Imput")]
        [Delayed(true)]
        public FileData FileImportDataTipizzato
        {
            get { return GetDelayedPropertyValue<FileData>("FileImportDataTipizzato"); }
            set { SetDelayedPropertyValue<FileData>("FileImportDataTipizzato", value); }
        }

        private string fNomeFoglio;
        [Size(35), Persistent("NOMEFOGLIO"), DevExpress.Xpo.DisplayName("Nome Foglio")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.NomeFoglio", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        [DbType("varchar(35)")]
        public string NomeFoglio
        {
            get { return fNomeFoglio; }
            set { SetPropertyValue<string>("NomeFoglio", ref fNomeFoglio, value); }
        }

        [Persistent("FILEOUT"), DevExpress.Xpo.DisplayName("File Result")]
        [Delayed(true)]
        public FileData FileResult
        {
            get { return GetDelayedPropertyValue<FileData>("FileResult"); }
            set { SetDelayedPropertyValue<FileData>("FileResult", value); }
        }
        private StatoElaborazioneJob fStatoElaborazioneImport;
        [Persistent("STATOELABORAZIONE"), Size(250), DevExpress.Xpo.DisplayName("Stato Elaborazione")]
        //[RuleRequiredField("RReqField.RegistroDataImportTentativi.TipoDiAcquisizione", DefaultContexts.Save, "Il Tipo Di Acquisizione è un campo obbligatorio")]
        public StatoElaborazioneJob StatoElaborazioneImport
        {
            get { return fStatoElaborazioneImport; }
            set { SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneImport", ref fStatoElaborazioneImport, value); }
        }
        [Association(@"RegistroDataManagemetFiles.RegistroDataManagemetFileResult", typeof(RegistroDataManagemetFileResult)), Aggregated]
        [DevExpress.Xpo.DisplayName("File")]
        public XPCollection<RegistroDataManagemetFileResult> RegistroDataManagemetFileResults
        {
            get
            {
                return GetCollection<RegistroDataManagemetFileResult>("RegistroDataManagemetFileResults");
            }
        }

    }
}
