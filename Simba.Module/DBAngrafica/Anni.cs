using System;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions,   Persistent("ANNI")]
    [NavigationItem(false)]
    [System.ComponentModel.DefaultProperty("Anno")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Anno")]
    [ImageName("ShowTestReport")]
    [VisibleInDashboards(false)]
    public class Anni : XPObject
    {
        public Anni()
            : base()
        {
        }

        public Anni(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Anno = 2014;
        }

        private int fAnno;
        [Persistent("ANNO"),       DisplayName("Anno")]
        //DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0:D")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<int>("Anno", ref fAnno, value);
            }
        }
    }
}
