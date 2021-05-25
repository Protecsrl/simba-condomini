using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;


namespace CAMS.Module.DBTask.AppsCAMS
{
    [DefaultClassOptions,
    Persistent("NOTIFICHEEMERGENZE")]
   // [System.ComponentModel.DefaultProperty("Notifiche Emergenze")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Notifiche Emergenze")]
    [ImageName("SwitchTimeScalesTo")]
    [NavigationItem(false)]
    public class NotificheEmergenze : XPObject
    {
        public NotificheEmergenze()
            : base()
        {
        }
        public NotificheEmergenze(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            CodiceNotifica = Guid.NewGuid();
        }

        private RisorseTeam fRisorsaTeam;
        [Persistent("RISORSETEAM"),        Association(@"Notifiche_TeamRisorse"),        DisplayName("Team")]
        [ExplicitLoading]
        public RisorseTeam Team
        {
            get
            {
                return fRisorsaTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("Team", ref fRisorsaTeam, value);
            }
        }


        private Guid fCodiceNotifica;
        [Persistent("CODICENOTIFICA"), MemberDesignTimeVisibility(false),
        DisplayName("Codice Notifica")]
        public Guid CodiceNotifica
        {
            get
            {
                return fCodiceNotifica;
            }
            set
            {
                SetPropertyValue<Guid>("CodiceNotifica", ref fCodiceNotifica, value);
            }
        }

        private StatiNotificaEmergenza fStatoNotifica;
        [Persistent("STATONOTIFICA"),
        DisplayName("Stato Notifica")]
        public StatiNotificaEmergenza StatoNotifica
        {
            get
            {
                return fStatoNotifica;
            }
            set
            {
                SetPropertyValue<StatiNotificaEmergenza>("StatoNotifica", ref fStatoNotifica, value);
            }
        }

        private DateTime? fDataCreazione;
        [Persistent("DATACREAZIONE"),
        DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        //[Appearance("RegNotificheEmergenze.DataCreazione", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime? DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataCreazione", ref fDataCreazione, value);
            }
        }

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        //[Appearance("Shill.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime? DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        private RegNotificheEmergenze fRegNotificheEmergenze;
        [MemberDesignTimeVisibility(true),
        Association(@"Reg_NotificheEmergenze", typeof(RegNotificheEmergenze))]
        [Persistent("NOTIFICHEEMERGENZEREG"),
        DisplayName("Registro Notifiche")]
        public RegNotificheEmergenze RegNotificheEmergenze
        {
            get
            {
                return fRegNotificheEmergenze;
            }
            set
            {
                SetPropertyValue("RegNotificheEmergenze", ref fRegNotificheEmergenze, value);
            }
        }


        public override string ToString()
        {
            if (this == null) return null;
            if (this.Oid == -1) return null;
            if (this.Team == null) return null;

            return string.Format("{0}({1})", Team.FullName, DataCreazione.ToString());
          
        }


    }
}
