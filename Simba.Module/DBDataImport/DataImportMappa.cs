using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("DATAIMPORTMAPPA")]
    [System.ComponentModel.DisplayName("Mappa File")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mappa File")]
    [ImageName("GroupFieldCollection")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [VisibleInDashboards(false)]
    public class DataImportMappa : XPObject
    {
        public DataImportMappa() : base() { }

        public DataImportMappa(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        //        FOGLIO CAMPO   TIPO CAMPO  RIGA COLONNA
        #region ARGOMENTI DI FOGLIO EXCELL 
        private string fNomeFILE;
        [Size(100), Persistent("NOMEFILE"), DisplayName("Nome FILE")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.NomeFoglio", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        [DbType("varchar(100)")]
        public string NomeFILE
        {
            get { return fNomeFILE; }
            set { SetPropertyValue<string>("NomeFILE", ref fNomeFILE, value); }
        }

        #endregion

        private string fNomeApp;
        [Size(200), Persistent("NOMEAPP")]
        [DbType("varchar(200)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string NomeApp
        {
            get { return fNomeApp; }
            set { SetPropertyValue<string>("NomeApp", ref fNomeApp, value); }
        }

        private string fDescrizione;
        [Size(1000), Persistent("DESCRIZIONE")]
        [DbType("varchar(1000)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }


        [Association(@"DataImportMappa.DataImportMappaCampi", typeof(DataImportMappaCampi)), Aggregated]
        [DisplayName("Elenco campi")]
        public XPCollection<DataImportMappaCampi> DataImportMappaCampis
        {
            get
            {
                return GetCollection<DataImportMappaCampi>("DataImportMappaCampis");
            }
        }
    }
}

