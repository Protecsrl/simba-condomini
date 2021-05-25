
using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDTIMPORTXLS")]
    [System.ComponentModel.DisplayName("Report Excel Import")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Report Excel Import")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [ImageName("GroupFieldCollection")]
    [NavigationItem("Data Import")]
    [VisibleInDashboards(false)]
    public class ResultImportXLS : XPObject
    {
        public ResultImportXLS() : base() { }

        public ResultImportXLS(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association(@"RegistroDataImportTentativi.ResultImportXLS")]
        [Persistent("REGDATAIMPORTTENTATIVI"), DisplayName("RegistroDataImportTentativi")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.RegistroDataImportTentativi", DefaultContexts.Save, "Il Registro Tentativo Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public RegistroDataImportTentativi RegistroDataImportTentativi
        {
            get { return GetDelayedPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi"); }
            set { SetDelayedPropertyValue<RegistroDataImportTentativi>("RegistroDataImportTentativi", value); }
        }

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE")]
        [DbType("varchar(4000)")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private string fNomeFoglio;
        [Size(150), Persistent("NOMEFOGLIO"), DisplayName("Nome Foglio")]
        //[RuleRequiredField("RReqField.ResultImportXLS.NomeFoglio", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        [DbType("varchar(150)")]
        public string NomeFoglio
        {
            get
            {
                return fNomeFoglio;
            }
            set
            {
                SetPropertyValue<string>("NomeFoglio", ref fNomeFoglio, value);
            }
        }

        private TipoEsitoImport fTipoEsitoImport;
        [Persistent("TIPOESITOIMPORT"), DisplayName("non Conformità"), ToolTip("non conformità di importazione o di costituzione dell'anagrafica")]
        //[RuleRequiredField("RReqField.ResultImportXLS.TipoEsitoImport", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public TipoEsitoImport TipoEsitoImport
        {
            get { return fTipoEsitoImport; }
            set { SetPropertyValue<TipoEsitoImport>("TipoEsitoImport", ref fTipoEsitoImport, value); }
        }

        //private TipoCampo fTipoCampo;
        //[Persistent("TIPOCADENZE"), DisplayName("Tipo Campo"), ToolTip("il Tipo Campo è legato alla testata o alla lista")]
        ////[RuleRequiredField("RReqField.ResultImportXLS.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        //public TipoCampo TipoCampo
        //{
        //    get
        //    {
        //        return fTipoCampo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<TipoCampo>("TipoCampo", ref fTipoCampo, value);
        //    }
        //}

        private string fNomeCampo;
        [Size(150), Persistent("NOMECAMPO"), DisplayName("Nome Campo")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.NomeCampo", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        [DbType("varchar(150)")]
        public string NomeCampo
        {
            get
            {
                return fNomeCampo;
            }
            set
            {
                SetPropertyValue<string>("NomeCampo", ref fNomeCampo, value);
            }
        }

        private int fRiga;
        [Persistent("RIGA"), DisplayName("Riga")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La riga di occorrenza dell'errore", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public int Riga
        {
            get { return fRiga; }
            set { SetPropertyValue<int>("Riga", ref fRiga, value); }
        }


        private string fColonna;
        [Persistent("COLONNA"), DisplayName("Colonna")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La riga colonna deve essere inserita nel formato A1 dove A = colonna e 1=prima riga", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[RuleRequiredField("RReqField.ReportExcelDettagli.RigaColonna", DefaultContexts.Save, "Riga Colonna è obbligatorio")]
        [DbType("varchar(10)")]
        public string Colonna
        {
            get { return fColonna; }
            set { SetPropertyValue<string>("Colonna", ref fColonna, value); }
        }



    }
}
