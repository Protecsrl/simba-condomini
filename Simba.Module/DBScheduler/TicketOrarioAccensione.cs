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
    [DefaultClassOptions, Persistent("TICKETORARI")]
    [NavigationItem("Agenda")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ticket Orari")]
    [ImageName("SwitchTimeScalesTo")]

    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("OrariAccensioni_noComplete", "StatusNotifica != 'Completed'", "Notifiche Attive", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("OrariAccensioni_Complete", "StatusNotifica = 'Completed'", "Notifiche Completate", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutte Orari", "", "Tutto", Index = 2)]
    public class TicketOrarioAccensione : XPObject
    {
        public TicketOrarioAccensione() : base() { }
        public TicketOrarioAccensione(Session session) : base(session) { }
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

        [Persistent("IMMOBILE")]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }

        [DataSourceCriteria("Immobile.Oid = '@This.Immobile.Oid'")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }

       
        private Asset fApparato;  ///[UltimoStatoSmistamento.Oid] > 1
        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        [DataSourceCriteria("Impianto.Oid = '@This.Impianto.Oid'")]
        [Delayed(true)]
        public Asset Apparato
        {
            get            {                return GetDelayedPropertyValue<Asset>("Apparato");            }
            set            {                SetDelayedPropertyValue<Asset>("Apparato", value);            }
        }

        [Persistent("ORARICIRCUITI"), System.ComponentModel.DisplayName("CircuitiOrari")]
        [DataSourceCriteria("Immobile.Oid = '@This.Immobile.Oid'")]
        [Delayed(true)]
        public CircuitiOrari CircuitiOrari
        {
            get { return GetDelayedPropertyValue<CircuitiOrari>("CircuitiOrari"); }
            set { SetDelayedPropertyValue<CircuitiOrari>("CircuitiOrari", value); }
        }

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Circuito")]
        // [RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        //[Delayed(true)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private StatoAutorizzativo fStatoAutorizzativo;
        [Persistent("AUTORIZZAZIONE"), System.ComponentModel.DisplayName("Autorizzazione")]
        [ExplicitLoading()]
        [Delayed(true)]
        public StatoAutorizzativo StatoAutorizzativo
        {
            get { return GetDelayedPropertyValue<StatoAutorizzativo>("StatoAutorizzativo"); }
            set { SetDelayedPropertyValue<StatoAutorizzativo>("StatoAutorizzativo", value); }
        }

        private TipoTiketOrari fTipoTiketOrari;
        [Persistent("TIPO"), DevExpress.Xpo.DisplayName("Tipologia")]
        // [RuleRequiredField("RuleReq.RegistroLavori.TipologiaCosto", DefaultContexts.Save, "La Tipologia è un campo obbligatorio")]
        public TipoTiketOrari TipoTiketOrari
        {
            get { return fTipoTiketOrari; }
            set { SetPropertyValue<TipoTiketOrari>("TipoTiketOrari", ref fTipoTiketOrari, value); }
        }


        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        //private DateTime fDataDa;
        [Persistent("DATA_DA"), XafDisplayName("Data Inizio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di inizio validità periodo", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DataDa
        {
            get { return GetDelayedPropertyValue<DateTime>("DataDa"); }
            set { SetDelayedPropertyValue<DateTime>("DataDa", value); }
        }
        //private DateTime fDataDa;
        [Persistent("DATA_A"), XafDisplayName("Data Fine")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di fine validità periodo", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public DateTime DataA
        {
            get { return GetDelayedPropertyValue<DateTime>("DataA"); }
            set { SetDelayedPropertyValue<DateTime>("DataA", value); }
        }


        [Association(@"TicketOrarioAccensione-dett"), System.ComponentModel.DisplayName("Ticket Orari oAccensione")]
        //   [Appearance("RdL.Documentis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<TicketOrarioAccensioneDett> TicketOrarioAccensioneDett
        {
            get
            {
                return GetCollection<TicketOrarioAccensioneDett>("TicketOrarioAccensioneDett");
            }
        }
    }
}
