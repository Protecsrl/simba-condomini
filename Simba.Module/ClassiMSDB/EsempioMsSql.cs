
using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using CAMS.Module.DBControlliNormativi;

namespace CAMS.Module.ClassiMSDB
{
    public class EsempioMsSql: LogEmailCtrlNorm
    {
        private string fCorpo2;
        [Persistent("CORPO"), DisplayName("Testo Mail")]
        [Size(SizeAttribute.Unlimited)]
        [DbType("VARCHAR")]
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
