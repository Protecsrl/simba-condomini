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
    [DefaultClassOptions, Persistent("DATAIMPORTMAPPACAMPIVINCOLI")]
    [System.ComponentModel.DisplayName("Mappa Campi Vincoli")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mappa Campi Vincoli")]
    [ImageName("GroupFieldCollection")]
    [System.ComponentModel.DefaultProperty("VincoloCampo")]
    [VisibleInDashboards(false)]
    public class DataImportMappaCampiVincoli: XPObject
    {
        public DataImportMappaCampiVincoli() : base() { }

        public DataImportMappaCampiVincoli(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private TipoDatoSystem fTipoCampo;
        [Persistent("TIPOCAMPO"), DisplayName("Tipo Campo"), ToolTip("il Tipo Campo è legato alla testata o alla lista")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public TipoDatoSystem TipoCampo
        {
            get { return fTipoCampo; }
            set { SetPropertyValue<TipoDatoSystem>("TipoCampo", ref fTipoCampo, value); }
        }

        private string fVincoloCampo;
        [Persistent("VINCOLOCAMPO"), DisplayName("Vincolo Campo"), ToolTip("il Vincolo Campo è legato al tipo di Campo")]
        //[RuleRequiredField("RReqField.ReportExcelDettagli.TipoCampo", DefaultContexts.Save, "il Tipo Campo è obbligatorio")]
        public string VincoloCampo
        {
            get { return fVincoloCampo; }
            set { SetPropertyValue<string>("VincoloCampo", ref fVincoloCampo, value); }
        }
              
    }
}
