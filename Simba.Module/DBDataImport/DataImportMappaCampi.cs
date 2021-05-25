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
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("DATAIMPORTMAPPACAMPI")]
    [System.ComponentModel.DisplayName("Mappa Campi")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mappa Campi")]
    [ImageName("GroupFieldCollection")]
    [System.ComponentModel.DefaultProperty("NomeCampo")]
    [VisibleInDashboards(false)]
    public class DataImportMappaCampi : XPObject
    {
        public DataImportMappaCampi() : base() { }

        public DataImportMappaCampi(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        //        FOGLIO CAMPO   TIPO CAMPO  RIGA COLONNA
        #region ARGOMENTI DI FOGLIO EXCELL 
        private DataImportMappa fDataImportMappa;
        [Association(@"DataImportMappa.DataImportMappaCampi")]
        [Persistent("DATAIMPORTMAPPA"), DevExpress.ExpressApp.DC.XafDisplayName("Mappa")]
        [ExplicitLoading()]
        public DataImportMappa DataImportMappa
        {
            get { return fDataImportMappa; }
            set { SetPropertyValue<DataImportMappa>("DataImportMappa", ref fDataImportMappa, value); }
        }

        private string fNomeFoglio;
        [Size(35), Persistent("NOMEFOGLIO"), DisplayName("Nome Foglio")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.NomeFoglio", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        [DbType("varchar(35)")]
        public string NomeFoglio
        {
            get { return fNomeFoglio; }
            set { SetPropertyValue<string>("NomeFoglio", ref fNomeFoglio, value); }
        }

        private TipoDatoSystem fTipoCampo;
        [Persistent("TIPOCAMPO"), DisplayName("Tipo Campo"), ToolTip("il Tipo Campo è legato alla testata o alla lista")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public TipoDatoSystem TipoCampo
        {
            get { return fTipoCampo; }
            set { SetPropertyValue<TipoDatoSystem>("TipoCampo", ref fTipoCampo, value); }
        }

        private DataImportMappaCampiVincoli fVincoloCampo;
        [Persistent("VINCOLOCAMPO"), DisplayName("Vincolo Campo"), ToolTip("il Vincolo Campo è legato al tipo di Campo")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public DataImportMappaCampiVincoli VincoloCampo
        {
            get { return fVincoloCampo; }
            set { SetPropertyValue<DataImportMappaCampiVincoli>("VincoloCampo", ref fVincoloCampo, value); }
        }

        private string fNomeCampo;
        [Size(150), Persistent("NOMECAMPO"), DisplayName("Nome Campo")]
        [RuleRequiredField("RReqField.DataImportMappaCampi.NomeCampo", DefaultContexts.Save, "il Nome Campo è obbligatorio")]
        [DbType("varchar(150)")]
        public string NomeCampo
        {
            get { return fNomeCampo; }
            set { SetPropertyValue<string>("NomeCampo", ref fNomeCampo, value); }
        }

#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        private string fRiga;
        [Size(5), Persistent("RIGA"), DisplayName("Riga di Cella")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La riga colonna deve essere inserita nel formato A1 dove A = colonna e 1=prima riga", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [RuleRequiredField("RReqField.DataImportMappaCampi.RigaColonna", DefaultContexts.Save, "Riga Colonna è obbligatorio")]
        [DbType("varchar(5)")]
        public string Riga
        {
            get { return fRiga; }
            set { SetPropertyValue<string>("Riga", ref fRiga, value); }
        }

#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        private string fColonna;
        [Size(5), Persistent("COLONNA"), DisplayName("Colonna di Cella")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("La colonna deve essere inserita nel formato A1 dove A = colonna e 1=prima riga", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[RuleRequiredField("RReqField.ReportExcelDettagli.RigaColonna", DefaultContexts.Save, "Riga Colonna è obbligatorio")]
        [DbType("varchar(5)")]
        public string Colonna
        {
            get { return fColonna; }
            set { SetPropertyValue<string>("Colonna", ref fColonna, value); }
        }

        #endregion

        private string fNomeClasseEAMS;
        [Size(50), Persistent("NOMECLASSE"), DisplayName("Nome CLASSE")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.NomeCampo", DefaultContexts.Save, "il Nome Campo di DB è obbligatorio")]
        [DbType("varchar(50)")]
        public string NomeClasseEAMS
        {
            get            {                return fNomeClasseEAMS;            }
            set            {                SetPropertyValue<string>("NomeClasseEAMS", ref fNomeClasseEAMS, value);            }
        }

        private string fNomeCampoDB;
        [Size(50), Persistent("NOMECAMPODB"), DisplayName("Nome Campo DB")]
        [DbType("varchar(50)")]
        public string NomeCampoDB
        {
            get            {                return fNomeCampoDB;            }
            set            {                SetPropertyValue<string>("NomeCampoDB", ref fNomeCampoDB, value);            }
        }

        private string fNomeApp;
        [Size(200), Persistent("NOMEAPP")]
        [DbType("varchar(200)")]
        public string NomeApp
        {
            get            {                return fNomeApp;            }
            set            {                SetPropertyValue<string>("NomeApp", ref fNomeApp, value);            }
        }

        private string fDescrizione;
        [Size(1000), Persistent("DESCRIZIONE")]
        [DbType("varchar(1000)")]
        public string Descrizione
        {
            get            {                return fDescrizione;            }
            set            {                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);            }
        }

    }
}
