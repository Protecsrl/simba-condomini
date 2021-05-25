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
//namespace CAMS.Module.DBDataImport
//{
//    class RegistroDataManagemetMapData
//    {
//    }
//}
namespace CAMS.Module.DBDataImport
{
    [DefaultClassOptions, Persistent("REGDATAMAN_MAPDATA")]
    [System.ComponentModel.DisplayName("Mappa Campi")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mappa Campi")]
    [ImageName("GroupFieldCollection")]
    [System.ComponentModel.DefaultProperty("NomeCampo")]
    [VisibleInDashboards(false)]
    public class RegistroDataManagemetMapData : XPObject
    {
        public RegistroDataManagemetMapData() : base() { }

        public RegistroDataManagemetMapData(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }


        private RegistroDataManagemet fRegistroDataManagemet;
        [Association(@"RegistroDataManagemet.RegistroDataManagemetMapData")]
        [Persistent("DATAIMPORTMAPPA"), DevExpress.ExpressApp.DC.XafDisplayName("Gestore Dati")]
        [ExplicitLoading()]
        public RegistroDataManagemet RegistroDataManagemet
        {
            get { return fRegistroDataManagemet; }
            set { SetPropertyValue<RegistroDataManagemet>("RegistroDataManagemet", ref fRegistroDataManagemet, value); }
        }
        //        FOGLIO CAMPO   TIPO CAMPO  RIGA COLONNA
        #region ARGOMENTI DI FOGLIO EXCELL 

        //private string fNomeFoglio;
        //[Size(35), Persistent("NOMEFOGLIO"), DisplayName("Nome Foglio")]
        ////[RuleRequiredField("RReqField.ReportExcelDettagli.NomeFoglio", DefaultContexts.Save, "il Nome Foglio è obbligatorio")]
        //[DbType("varchar(35)")]
        //public string NomeFoglio
        //{
        //    get { return fNomeFoglio; }
        //    set { SetPropertyValue<string>("NomeFoglio", ref fNomeFoglio, value); }
        //}

        private TipoDatoSystem fTipoCampo;
        [Persistent("TIPOCAMPO"), DisplayName("Tipo Campo"), ToolTip("il Tipo Campo è legato alla testata o alla lista")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public TipoDatoSystem TipoCampo
        {
            get { return fTipoCampo; }
            set { SetPropertyValue<TipoDatoSystem>("TipoCampo", ref fTipoCampo, value); }
        }

      
        private string fRiga;
        [Size(5), Persistent("RIGA"), DisplayName("Riga di inizio dati")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Riga di inizio dati di Aggiornamento", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [RuleRequiredField("RReqField.RegistroDataManagemetMapData.Riga", DefaultContexts.Save, "Riga   è obbligatorio")]
        [DbType("varchar(5)")]
        public string Riga
        {
            get { return fRiga; }
            set { SetPropertyValue<string>("Riga", ref fRiga, value); }
        }

     
        private string fColonna;
        [Size(5), Persistent("COLONNA"), DisplayName("Colonna di Cella")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Colonna di inizio dati di Aggiornamento", "Avviso", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [RuleRequiredField("RReqField.RegistroDataManagemetMapData.Colonna", DefaultContexts.Save, "Colonna   è obbligatorio")]
        [DbType("varchar(5)")]
        public string Colonna
        {
            get { return fColonna; }
            set { SetPropertyValue<string>("Colonna", ref fColonna, value); }
        }

        #endregion

        private string fNomeCampo;
        [Size(150), Persistent("NOMECAMPO"), DisplayName("Nome Campo")]
        [RuleRequiredField("RReqField.RegistroDataManagemetMapData.NomeCampo", DefaultContexts.Save, "il Nome Campo è obbligatorio")]
        [DbType("varchar(150)")]
        public string NomeCampo
        {
            get { return fNomeCampo; }
            set { SetPropertyValue<string>("NomeCampo", ref fNomeCampo, value); }
        }
        private string fNomeCampoDB;
        [Size(50), Persistent("NOMECAMPODB"), DisplayName("Nome Campo DB")]
        [DbType("varchar(50)")]
        public string NomeCampoDB
        {
            get { return fNomeCampoDB; }
            set { SetPropertyValue<string>("NomeCampoDB", ref fNomeCampoDB, value); }
        }
         

    }
}
