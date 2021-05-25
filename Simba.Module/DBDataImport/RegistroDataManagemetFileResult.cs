using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
//namespace CAMS.Module.DBDataImport
//{
//    class RegistroDataManagemetFileResult
//    {
//    }
//}
namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDATAMAN_FILESRESULT")]
    [System.ComponentModel.DisplayName("Result Data")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Result Data")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [ImageName("GroupFieldCollection")]
    [NavigationItem("Data Import")]
    [VisibleInDashboards(false)]
    public class RegistroDataManagemetFileResult : XPObject
    {
        public RegistroDataManagemetFileResult() : base() { }

        public RegistroDataManagemetFileResult(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association(@"RegistroDataManagemetFiles.RegistroDataManagemetFileResult")]
        [Persistent("REGDATAMAN_FILES"), DisplayName("File dati di Aggiornamento")]
        //[RuleRequiredField("RuleReq.ResultImportApparato.RegistroDataImportTentativi", DefaultContexts.Save, "Il Registro Tentativo Data Import è un campo obbligatorio")]
        [Delayed(true)]
        public RegistroDataManagemetFiles RegDataManagemetFiles
        {
            get { return GetDelayedPropertyValue<RegistroDataManagemetFiles>("RegDataManagemetFiles"); }
            set { SetDelayedPropertyValue<RegistroDataManagemetFiles>("RegDataManagemetFiles", value); }
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
            get { return fNomeFoglio; }
            set { SetPropertyValue<string>("NomeFoglio", ref fNomeFoglio, value); }
        }

        private TipoEsitoImport fTipoEsitoImport;
        [Persistent("TIPOESITOIMPORT"), DisplayName("non Conformità"), ToolTip("non conformità di importazione o di costituzione dell'anagrafica")]
        //[RuleRequiredField("RReqField.ResultImportXLS.TipoEsitoImport", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public TipoEsitoImport TipoEsitoImport
        {
            get { return fTipoEsitoImport; }
            set { SetPropertyValue<TipoEsitoImport>("TipoEsitoImport", ref fTipoEsitoImport, value); }
        }

        private string fNomeCampo;
        [Size(150), Persistent("NOMECAMPO"), DisplayName("Nome Campo")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.NomeCampo", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        [DbType("varchar(150)")]
        public string NomeCampo
        {
            get { return fNomeCampo; }
            set { SetPropertyValue<string>("NomeCampo", ref fNomeCampo, value); }
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

        private int fOidOggetto;
        [Persistent("OIDOGGETTO"), DisplayName("Oggetto")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La riga colonna deve essere inserita nel formato A1 dove A = colonna e 1=prima riga", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[RuleRequiredField("RReqField.ReportExcelDettagli.RigaColonna", DefaultContexts.Save, "Riga Colonna è obbligatorio")]
        public int OidOggetto
        {
            get { return fOidOggetto; }
            set { SetPropertyValue<int>("OidOggetto", ref fOidOggetto, value); }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }
 
        [Persistent("UTENTE"), Size(100), DisplayName("Utente")]
        [DbType("varchar(100)")]
        [Delayed(true)]
        [VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }  
        }

    }
}

