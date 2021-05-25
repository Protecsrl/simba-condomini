using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPSTATOPIANIFICA")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Pianificazione")]
    [ImageName("ModelEditor_Settings")]
    [NavigationItem("Planner")]
    [VisibleInDashboards(false)]
    public class MPStatoPianificazione : XPObject
    {
        public MPStatoPianificazione()
            : base()
        {
        }

        public MPStatoPianificazione(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(150)]
        [DbType("varchar(1500)")]
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
        private string fDescrizioneAvanti;
        [Persistent("DESCRIZIONEAVANTI"),
        Size(150)]
        [DbType("varchar(1500)")]
        public string DescrizioneAvanti
        {
            get
            {
                return fDescrizioneAvanti;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneAvanti", ref fDescrizioneAvanti, value);
            }
        }


        private string fDescrizioneAnnulla;
        [Persistent("DESCRIZIONEANNULLA"),
        Size(150)]
        [DbType("varchar(1500)")]
        public string DescrizioneAnnulla
        {
            get
            {
                return fDescrizioneAnnulla;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneAnnulla", ref fDescrizioneAnnulla, value);
            }
        }

        private string fDescrizioneAzione;
        [Persistent("DESCRIZIONEAZIONE"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string DescrizioneAzione
        {
            get
            {
                return fDescrizioneAzione;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneAzione", ref fDescrizioneAzione, value);
            }
        }

        private string fToolTipAvanti;
        [Persistent("TOOLTIPAVANTI"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string ToolTipAvanti
        {
            get
            {
                return fToolTipAvanti;
            }
            set
            {
                SetPropertyValue<string>("ToolTipAvanti", ref fToolTipAvanti, value);
            }
        }

        private string fToolTipAzione;
        [Persistent("TOOLTIPAZIONE"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string ToolTipAzione
        {
            get
            {
                return fToolTipAzione;
            }
            set
            {
                SetPropertyValue<string>("ToolTipAzione", ref fToolTipAzione, value);
            }
        }

        private string fToolTipAnnulla;
        [Persistent("TOOLTIPANNULLA"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string ToolTipAnnulla
        {
            get
            {
                return fToolTipAnnulla;
            }
            set
            {
                SetPropertyValue<string>("ToolTipAnnulla", ref fToolTipAnnulla, value);
            }
        }

        private string fMessageAvanti;
        [Persistent("MESSAGEAVANTI"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string MessageAvanti
        {
            get
            {
                return fMessageAvanti;
            }
            set
            {
                SetPropertyValue<string>("MessageAvanti", ref fMessageAvanti, value);
            }
        }

        private string fMessageAzione;
        [Persistent("MESSAGEAZIONE"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string MessageAzione
        {
            get
            {
                return fMessageAzione;
            }
            set
            {
                SetPropertyValue<string>("MessageAzione", ref fMessageAzione, value);
            }
        }

        private string fMessageAnnulla;
        [Persistent("MESSAGEANNULLA"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string MessageAnnulla
        {
            get
            {
                return fMessageAnnulla;
            }
            set
            {
                SetPropertyValue<string>("MessageAnnulla", ref fMessageAnnulla, value);
            }
        }

        private string fCaptionAvanti;
        [Persistent("CAPTIONAVANTI"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string CaptionAvanti
        {
            get
            {
                return fCaptionAvanti;
            }
            set
            {
                SetPropertyValue<string>("CaptionAvanti", ref fCaptionAvanti, value);
            }
        }

        private string fCaptionAzione;
        [Persistent("CAPTIONAZIONE"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string CaptionAzione
        {
            get
            {
                return fCaptionAzione;
            }
            set
            {
                SetPropertyValue<string>("CaptionAzione", ref fCaptionAzione, value);
            }
        }

        private string fCaptionAnnulla;
        [Persistent("CAPTIONANNULLA"),
        Size(150),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(1500)")]
        public string CaptionAnnulla
        {
            get
            {
                return fCaptionAnnulla;
            }
            set
            {
                SetPropertyValue<string>("CaptionAnnulla", ref fCaptionAnnulla, value);
            }
        }


        private string fEsegui;
        [Persistent("ESEGUI"),
        MemberDesignTimeVisibility(false)]
        [DbType("varchar(50)")]
        public string Esegui
        {
            get
            {
                return fEsegui;
            }
            set
            {
                SetPropertyValue<string>("Esegui", ref fEsegui, value);
            }
        }

        private bool fActiveAvanti;
        [Persistent("ATTIVOAVANTI"),
        MemberDesignTimeVisibility(false)]
        public bool ActiveAvanti
        {
            get
            {
                return fActiveAvanti;
            }
            set
            {
                SetPropertyValue<bool>("ActiveAvanti", ref fActiveAvanti, value);
            }
        }
        private bool fActiveAnnulla;
        [Persistent("ATTIVOANNULLA"),
        MemberDesignTimeVisibility(true)]
        public bool ActiveAnnulla
        {
            get
            {
                return fActiveAnnulla;
            }
            set
            {
                SetPropertyValue<bool>("ActiveAnnulla", ref fActiveAnnulla, value);
            }
        }

        private bool fActiveAzione;
        [Persistent("ATTIVOAZIONE"),
        MemberDesignTimeVisibility(false)]
        public bool ActiveAzione
        {
            get
            {
                return fActiveAzione;
            }
            set
            {
                SetPropertyValue<bool>("ActiveAzione", ref fActiveAzione, value);
            }
        }
        private int fSettimanaSk;
        [Persistent("SETTIMANASK"),
        MemberDesignTimeVisibility(false)]
        public int SettimanaSk
        {
            get
            {
                return fSettimanaSk;
            }
            set
            {
                SetPropertyValue<int>("SettimanaSk", ref fSettimanaSk, value);
            }
        }

        private int fNextAvanti;
        [Persistent("NEXTAVANTI"),
        MemberDesignTimeVisibility(false)]
        public int NextAvanti
        {
            get
            {
                return fNextAvanti;
            }
            set
            {
                SetPropertyValue<int>("NextAvanti", ref fNextAvanti, value);
            }
        }

        private int fNextAzione;
        [Persistent("NEXTAZIONE"),
        MemberDesignTimeVisibility(false)]
        public int NextAzione
        {
            get
            {
                return fNextAzione;
            }
            set
            {
                SetPropertyValue<int>("NextAzione", ref fNextAzione, value);
            }
        }

        private int fNextAnnulla;
        [Persistent("NEXTANNULLA"),
        MemberDesignTimeVisibility(false)]
        public int NextAnnulla
        {
            get
            {
                return fNextAnnulla;
            }
            set
            {
                SetPropertyValue<int>("NextAnnulla", ref fNextAnnulla, value);
            }
        }
    }
}
