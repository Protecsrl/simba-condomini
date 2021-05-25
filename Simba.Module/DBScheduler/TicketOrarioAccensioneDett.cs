using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule.Notifications;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
//using DevExpress.ExpressApp.Demos;
//using DevExpress.XtraScheduler.Xml;
using DevExpress.XtraScheduler.Xml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
//using DevExpress.XtraScheduler;
//using DevExpress.XtraScheduler.Xml;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace CAMS.Module.DBScheduler
{
    [DefaultClassOptions, Persistent("TICKETORARIDETT")]
    [NavigationItem("Agenda")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "dettaglio Ticket Orari")]
    [ImageName("SwitchTimeScalesTo")]

    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("OrariAccensioni_noComplete", "StatusNotifica != 'Completed'", "Notifiche Attive", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("OrariAccensioni_Complete", "StatusNotifica = 'Completed'", "Notifiche Completate", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutte Orari", "", "Tutto", Index = 2)]

    public class TicketOrarioAccensioneDett : XPObject
    {
        public TicketOrarioAccensioneDett() : base() { }
        public TicketOrarioAccensioneDett(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this != null)
                if (this.Oid == -1) // crea RdL
                {

                }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (this != null)
            {

            }
        }


        //private RegistroRdL fRegistroRdL;
        [Association(@"TicketOrarioAccensione-dett"), Persistent("TICKETORARI"), System.ComponentModel.DisplayName("Registro RdL")]
        //[System.ComponentModel.Browsable(false)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public TicketOrarioAccensione TicketOrarioAccensione
        {
            get { return GetDelayedPropertyValue<TicketOrarioAccensione>("TicketOrarioAccensione"); }
            set { SetDelayedPropertyValue<TicketOrarioAccensione>("TicketOrarioAccensione", value); }
        }

        private TipoGiornoSettimana fNomeGiornoOrari;
        [Persistent("TIPO"), DevExpress.Xpo.DisplayName("NomeGiorno")]
        public TipoGiornoSettimana NomeGiornoOrari
        {
            get { return fNomeGiornoOrari; }
            set { SetPropertyValue<TipoGiornoSettimana>("NomeGiornoOrari", ref fNomeGiornoOrari, value); }
        }

        private TimeSpan timeDalle;
        [Persistent("TIMEDALLE"), DevExpress.Xpo.DisplayName("dalle")]
        public TimeSpan TimeDalle
        {
            get { return timeDalle; }
            set { SetPropertyValue("TimeDalle", ref timeDalle, value); }
        }

        private TimeSpan timeAlle;
        [Persistent("TIMEALLE"), DevExpress.Xpo.DisplayName("Alle")]
        public TimeSpan TimeAlle
        {
            get { return timeAlle; }
            set { SetPropertyValue("TimeAlle", ref timeAlle, value); }
        }
    }
}
