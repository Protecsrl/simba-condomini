using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;

namespace CAMS.Module.Classi
{
    [NavigationItem(false),System.ComponentModel.DisplayName("Messaggio Avviso Utente")]
    [DefaultClassOptions, NonPersistent]
    public class MessaggioPopUp : XPObject
    {
        public MessaggioPopUp()
            : base()
        {
        }
        public MessaggioPopUp(Session session)
            : base(session)
        {
        }

        private string fMessaggio;
        [NonPersistent,  DisplayName("Messaggio:")]
        [Size(SizeAttribute.Unlimited)]
        public string Messaggio
        {
            get
            {
                return fMessaggio;
            }
            set
            {
                SetPropertyValue<string>("Messaggio", ref fMessaggio, value);
            }
        }
    }
}
