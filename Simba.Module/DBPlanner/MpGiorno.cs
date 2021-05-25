using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.Classi;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPGIORNO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Vincoli Giorni di Cadenza")]
    [ImageName("Period")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MpGiorno : XPObject
    {
        public MpGiorno()
            : base()
        {
        }
        public MpGiorno(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
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

        private TipoMpGiorno fLun;
        [Persistent("LUN"),
        DisplayName("Lun") ]
        public TipoMpGiorno Lun
        {
            get
            {
                return fLun;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Lun", ref fLun, value);
            }
        }
        private TipoMpGiorno fMar;
        [Persistent("MAR"),
        DisplayName("Mar")]
        public TipoMpGiorno Mar
        {
            get
            {
                return fMar;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Mar", ref fMar, value);
            }
        }
        private TipoMpGiorno fMer;
        [Persistent("MER"),
        DisplayName("Mer") ]
        public TipoMpGiorno Mer
        {
            get
            {
                return fMer;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Mer", ref fMer, value);
            }
        }
        private TipoMpGiorno fGio;
        [Persistent("GIO"),
        DisplayName("Gio") ]
        public TipoMpGiorno Gio
        {
            get
            {
                return fGio;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Gio", ref fGio, value);
            }
        }
        private TipoMpGiorno fVen;
        [Persistent("VEN"),
        DisplayName("Ven") ]
        public TipoMpGiorno Ven
        {
            get
            {
                return fVen;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Ven", ref fVen, value);
            }
        }
        private TipoMpGiorno fSab;
        [Persistent("SAB"),
        DisplayName("Sab") ]
        public TipoMpGiorno Sab
        {
            get
            {
                return fSab;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Sab", ref fSab, value);
            }
        }
        private TipoMpGiorno fDom;
        [Persistent("DOM"),
        DisplayName("Dom") ]
        public TipoMpGiorno Dom
        {
            get
            {
                return fDom;
            }
            set
            {
                SetPropertyValue<TipoMpGiorno>("Dom", ref fDom, value);
            }
        }
    }
}
