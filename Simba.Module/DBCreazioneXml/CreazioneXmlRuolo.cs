using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBCreazioneXml
{
    [DefaultClassOptions, Persistent("CREAZIONEXMLRUOLO")]
    [System.ComponentModel.DefaultProperty("Ruolo")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class CreazioneXmlRuolo : XPObject
    {
        public CreazioneXmlRuolo()
            : base()
        {
        }
        public CreazioneXmlRuolo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fRuolo;
        [Size(200),
        Persistent("RUOLO")]
        [DbType("varchar(200)")]
        public string Ruolo
        {
            get
            {
                return fRuolo;
            }
            set
            {
                SetPropertyValue<string>("Ruolo", ref fRuolo, value);
            }
        }
    }
}
