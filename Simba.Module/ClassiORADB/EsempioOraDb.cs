
using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using CAMS.Module.DBControlliNormativi;

namespace CAMS.Module.ClassiORADB
{
    public class EsempioOraDb: LogEmailCtrlNorm
    {
        private string fCorpo2;
        [Persistent("CORPO"), DisplayName("Testo Mail")]
        [Size(SizeAttribute.Unlimited)]
        [DbType("CLOB")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Corpo
        {
            get
            {
                return fCorpo2;
            }
            set
            {
                SetPropertyValue<string>("Corpo", ref fCorpo2, value);
            }
        }
    }
}
