
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.UtentiRuoliCommesse
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Utenti-Ruoli-Commesse")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Utenti-Ruoli-Commesse")]
    [DefaultClassOptions, Persistent("UTENTIRUOLICOMMESSE")]
    [VisibleInDashboards(false)]

    public class UtentiRuoliCommesse : XPObject
    {
        public UtentiRuoliCommesse()
           : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public UtentiRuoliCommesse(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }

        private string fUserName;
        [Persistent("USERNAME"), XafDisplayName("UserName")]
        [Delayed(true)]
        [DbType("varchar(100)")]
        [VisibleInListView(true)]
        public string UserName
        {
            get { return GetDelayedPropertyValue<string>("UserName"); }
            set { SetDelayedPropertyValue<string>("UserName", value); }
        }

        private string fOidUserName;
        [Persistent("OIDUSERNAME"), XafDisplayName("OIdUserName")]
        [Delayed(true)]
        [DbType("varchar(100)")]
        [VisibleInListView(true)]
        public string OidUserName
        {
            get { return GetDelayedPropertyValue<string>("OidUserName"); }
            set { SetDelayedPropertyValue<string>("OidUserName", value); }
        }

        private bool fIsActive;
        [Persistent("ISACTIVE"), XafDisplayName("Attivo/Non Attivo")]
        [Delayed(true)]
        [VisibleInListView(true)]
        public bool IsActive
        {
            get { return GetDelayedPropertyValue<bool>("IsActive"); }
            set { SetDelayedPropertyValue<bool>("IsActive", value); }
        }

        private string fRuoli;
        [Persistent("RUOLI"), XafDisplayName("Ruoli")]
        [Delayed(true)]
        [DbType("varchar(4000)")]
        [VisibleInListView(true)]
        public string Ruoli
        {
            get { return GetDelayedPropertyValue<string>("Ruoli"); }
            set { SetDelayedPropertyValue<string>("Ruoli", value); }
        }


        private string fTipoRuoli;
        [Persistent("TIPORUOLI"), XafDisplayName("Tipologia Ruoli")]
        [Delayed(true)]
        [DbType("varchar(100)")]
        [VisibleInListView(true)]
        public string TipoRuoli
        {
            get { return GetDelayedPropertyValue<string>("TipoRuoli"); }
            set { SetDelayedPropertyValue<string>("TipoRuoli", value); }
        }


        private int fNrCommesse;
        [Persistent("NRCOMMESSE"), XafDisplayName("Nr Commesse")]
        [Delayed(true)]
        [VisibleInListView(true)]
        public int NrCommesse
        {
            get { return GetDelayedPropertyValue<int>("NrCommesse"); }
            set { SetDelayedPropertyValue<int>("NrCommesse", value); }
        }

        private string fCommesse;
        [Persistent("COMMESSE"), XafDisplayName("Commesse")]
        [Delayed(true)]
        [DbType("varchar(4000)")]
        [VisibleInListView(true)]
        public string Commesse
        {
            get { return GetDelayedPropertyValue<string>("Commesse"); }
            set { SetDelayedPropertyValue<string>("Commesse", value); }
        }



        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [Delayed(true)]
        [VisibleInListView(true)]        
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }

        private string fSessioneWEB;
        [Persistent("SESSIONEWEB"),
        Size(100),
        DisplayName("Sessione WEB")]
        [DbType("varchar(100)")]
        [VisibleInListView(true)]
        [Delayed(true)]
        //[System.ComponentModel.Browsable(false)]
        public string SessioneWEB
        {
            get { return GetDelayedPropertyValue<string>("SessioneWEB"); }
            set { SetDelayedPropertyValue<string>("SessioneWEB", value); }
        }

    }
}