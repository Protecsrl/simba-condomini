using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant.ParametriPopUp
{
    [NavigationItem(false),   System.ComponentModel.DisplayName("Parametri Coefficienti")]
    [DefaultClassOptions,   NonPersistent]
    [VisibleInDashboards(false)]
    public class ParametriCoefficientiK : XPObject
    {
        public ParametriCoefficientiK()
            : base()
        {
        }
        public ParametriCoefficientiK(Session session)
            : base(session)
        {
        }

        private string fDescrizione;
        [NonPersistent,
        DisplayName("Descrizione")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }


        private double fValore;
        [NonPersistent,
        DisplayName("Valore")]
        public double Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<double>("Valore", ref fValore, value);
            }
        }

        private int fOidObject;
        [NonPersistent ]
        [MemberDesignTimeVisibility(false)]
        public int OidObject
        {
            get
            {
                return fOidObject;
            }
            set
            {
                SetPropertyValue<int>("OidObject", ref fOidObject, value);
            }
        }

        [MemberDesignTimeVisibility(false)]
        public Asset Apparato { get; set; }
    }
}
