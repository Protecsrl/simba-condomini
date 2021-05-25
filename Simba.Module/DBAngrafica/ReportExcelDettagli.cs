


using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem(false)]//"Amministrazione"
    [System.ComponentModel.DisplayName("Parametri Report Excel Dettagli")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Parametri Report Excel Dettagli")]
    [DefaultClassOptions, Persistent("PARAMETRIREPORTXLSDETT")]
    [RuleCriteria("RuleInfo.ReportExcelDettagli.RigaColonna", DefaultContexts.Save, @"1=1",
   CustomMessageTemplate = "Informazione: La riga colonna deve essere inserita nel formato A1 dove A = colonna e 1=prima riga",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]
    [ImageName("GroupFieldCollection")]
    [VisibleInDashboards(false)]
    public class ReportExcelDettagli : XPObject
    {
        public ReportExcelDettagli() : base() { }

        public ReportExcelDettagli(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private ReportExcel fReportExcel;
        [Association(@"ReportExcel-dettagli")]
        [Persistent("PARAMETRIREPORTXLS"), DisplayName("Report Excel")]
        [RuleRequiredField("RReqField.ReportExcelDettagli.ReportExcel", DefaultContexts.Save, "il Report Excel è obbligatorio")]
        public ReportExcel ReportExcel
        {
            get
            {
                return fReportExcel;
            }
            set
            {
                SetPropertyValue<ReportExcel>("ReportExcel", ref fReportExcel, value);
            }
        }


        private string fNomeFoglio;
        [Size(150), Persistent("NOMEFOGLIO"), DisplayName("Nome Foglio")]
        [RuleRequiredField("RReqField.ReportExcelDettagli.NomeFoglio", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
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

        private TipoCampo fTipoCampo;
        [Persistent("TIPOCADENZE"), DisplayName("Tipo Campo"), ToolTip("il Tipo Campo è legato alla testata o alla lista")]
        [RuleRequiredField("RReqField.ReportExcelDettagli.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public TipoCampo TipoCampo
        {
            get
            {
                return fTipoCampo;
            }
            set
            {
                SetPropertyValue<TipoCampo>("TipoCampo", ref fTipoCampo, value);
            }
        }

        private string fNomeCampo;
        [Size(150), Persistent("NOMECAMPO"), DisplayName("Nome Campo")]
        [RuleRequiredField("RReqField.ReportExcelDettagli.NomeCampo", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
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
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        private string fRigaColonna;
        [Size(10), Persistent("RC"), DisplayName("Colonna Riga (A1)")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La riga colonna deve essere inserita nel formato A1 dove A = colonna e 1=prima riga", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [RuleRequiredField("RReqField.ReportExcelDettagli.RigaColonna", DefaultContexts.Save, "Riga Colonna è obbligatorio")]
        [DbType("varchar(10)")]
        public string RigaColonna
        {
            get
            {
                return fRigaColonna;
            }
            set
            {
                SetPropertyValue<string>("RigaColonna", ref fRigaColonna, value);
            }
        }




    }
}
