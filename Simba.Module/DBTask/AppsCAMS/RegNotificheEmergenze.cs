using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;

using CAMS.Module.DBPlant.Vista;
using System.Windows.Forms;
using System.Collections.Generic;
using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.Model;



namespace CAMS.Module.DBTask.AppsCAMS
{
    [DefaultClassOptions,
    Persistent("NOTIFICHEEMERGENZEREG")]
   // [System.ComponentModel.DefaultProperty("Registro Notifiche Emergenze")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Notifiche Emergenze")]
    [ImageName("SwitchTimeScalesTo")]
    [NavigationItem("Emergenze")]
    [Appearance("RdL.MostraRisorsa.Codice.ColoreGiallo", AppearanceItemType.ViewItem, @"[RdL.UltimoStatoSmistamento.Oid] In(1)",
         TargetItems = "CodiceRdL;CodiceRegistroRdL", Context = "ListView", Priority = 1, BackColor = "Yellow")]
    [Appearance("RdL.MostraRisorsa.Codice.ColoreRosso", AppearanceItemType.ViewItem, @"[RdL.UltimoStatoSmistamento.Oid] In(10)",
     TargetItems = "CodiceRdL;CodiceRegistroRdL", Context = "ListView", Priority = 1, BackColor = "Red")]
    [Appearance("RdL.MostraRisorsa.Codice.ColoreVerde", AppearanceItemType.ViewItem, @"[RdL.UltimoStatoSmistamento.Oid] In(2,3,11)",
    TargetItems = "CodiceRdL;CodiceRegistroRdL", Context = "ListView", Priority = 1, BackColor = "LightGreen")]
    [Appearance("RdL.MostraRisorsa.Codice.ColoreArancio", AppearanceItemType.ViewItem, @"[RdL.UltimoStatoSmistamento.Oid] In(4,5)",
  TargetItems = "CodiceRdL;CodiceRegistroRdL", Context = "ListView", Priority = 1, BackColor = "0xfff0f0")]
    [Appearance("RdL.MostraRisorsa.Codice.ColoreBlue", AppearanceItemType.ViewItem, @"[RdL.UltimoStatoSmistamento.Oid] In(6,7)",
TargetItems = "CodiceRdL;CodiceRegistroRdL", Context = "ListView", Priority = 1, BackColor = "LightSteelBlue")]
    [Appearance("RdL.MostraRisorsa.Codice.violet", AppearanceItemType.ViewItem, @"[RdL.UltimoStatoSmistamento.Oid] = 1 And RdL.StatoOperativo.Oid = 101",
TargetItems = "CodiceRdL;CodiceRegistroRdL", Context = "ListView", Priority = 1, BackColor = "#ee82ee")]
      
    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegNotificheEmergenze.daAssegnare", "RegStatoNotifica = 0 Or RegStatoNotifica = 4", "daAssegnare", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegNotificheEmergenze.Assegnato", "RegStatoNotifica = 1", "Assegnato", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegNotificheEmergenze.Annullato", "RegStatoNotifica = 5", "Annullato", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegNotificheEmergenze.Completato", "RegStatoNotifica = 2", "Completato", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegNotificheEmergenze.Rifutato", "RegStatoNotifica = 3", "Rifutato", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegNotificheEmergenze.tutto", "", "Tutto", Index = 5)]


    #endregion

    public class RegNotificheEmergenze : XPObject
    {
        public RegNotificheEmergenze()
            : base()
        {
        }
        public RegNotificheEmergenze(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            var a = RegStatiNotificaEmergenza.Rifutato;
            //var b = RegistroRdL.UltimoStatoSmistamento.Oid
        }


        private RegistroRdL fRegistroRdL;
        [Association(@"Notifiche_RegistroRdL"),
        Persistent("REGRDL"),
        DisplayName("Registro RdL")]
        [VisibleInDetailView(false),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }

        private RdL fRdL;
        //[Association(@"Notifiche_RdL")]
        [Persistent("RDL"),
        DisplayName("RdL")]
        [DataSourceCriteria("[RegistroRdL] Is Null And [RisorseTeam] Is Null And [Categoria.Oid] = 2")]
        public RdL RdL
        {
            get
            {
                
                return fRdL;
            }
            set
            {
                SetPropertyValue<RdL>("RdL", ref fRdL, value);
            }
        }


        [PersistentAlias("RdL.Immobile.Descrizione"), DevExpress.Xpo.DisplayName("Immobile")]
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ExplicitLoading()]
        public string Immobile
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }

            }
        }


        [PersistentAlias("RdL.Immobile.Commesse.Descrizione"), DevExpress.Xpo.DisplayName("Commesse")]
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ExplicitLoading()]
        public string Commesse
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("Commesse");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }

            }
        }


        [PersistentAlias("RdL.Oid"), DevExpress.Xpo.DisplayName("Cod RdL")]
        [VisibleInListView(true), VisibleInDetailView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int CodiceRdL
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("CodiceRdL");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }
        [PersistentAlias("RegistroRdL.Oid"), DevExpress.Xpo.DisplayName("Cod RegistroRdL")]
        [VisibleInListView(true), VisibleInDetailView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int CodiceRegistroRdL
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("CodiceRegistroRdL");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }


        private RegStatiNotificaEmergenza fRegStatoNotifica;
        [Persistent("REGSTATONOTIFICA"),
        DisplayName("Reg Stato Notifica")]
        [Appearance("RegNotificheEmergenze.Abilita.RegStatoNotifica", Enabled = false)] //Criteria = "(Oid == -1)",
        public RegStatiNotificaEmergenza RegStatoNotifica
        {
            get
            {
                return fRegStatoNotifica;
            }
            set
            {
                SetPropertyValue<RegStatiNotificaEmergenza>("RegStatoNotifica", ref fRegStatoNotifica, value);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";


        DateTime fDataChiusura;
        [Persistent("DATACHIUSURA"), DisplayName("Data Chiusura")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Chiusura della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Appearance("RegNotificheEmergenze.DataChiusura", Enabled = false)]
        public DateTime DataChiusura
        {
            get { return fDataChiusura; }
            set { SetPropertyValue<DateTime>("DataChiusura", ref fDataChiusura, value); }
        }

        DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Creazione della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        //[Appearance("RegNotificheEmergenze.DataCreazione", Enabled = false)]
        [VisibleInListView(false)]
        public DateTime DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
            }
        }

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        //[Appearance("Shill.DataAggiornamento", Enabled = false)]
        //[System.ComponentModel.Browsable(false)]
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


        RisorseTeam fRisorsaTeam;
        [Appearance("RegNotificheEmergenze.RisorsaTeam", Enabled = false)]
        //[ Association(@"RegNotifiche_TeamRisorse") ]
        [Persistent("RISORSETEAM"),   DisplayName("Team")]
        // [DataSourceProperty("CoppiaLinkata", DataSourcePropertyIsNullMode.SelectAll)]
        //  [DataSourceCriteria("CoppiaLinkata = 'No'")]
        [ExplicitLoading]
        public RisorseTeam Team
        {
            get
            {
                //int a =  NotificheEmergenzes.Count;
                return fRisorsaTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("Team", ref fRisorsaTeam, value);
            }
        }




        [Association(@"Reg_NotificheEmergenze", typeof(NotificheEmergenze)),Aggregated, DisplayName("Notifiche Emergenze")]     
        public XPCollection<NotificheEmergenze> NotificheEmergenzes { get { return GetCollection<NotificheEmergenze>("NotificheEmergenzes"); } }
        

        [Association(@"Reg_DistanzeRisorse", typeof(RisorseDistanzeRdL)), DisplayName("Risorse e distanze")]
        //[Appearance("RegNotificheEmergenze.RisorseDistanzeRdLs.Abilita", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RisorseDistanzeRdL> RisorseDistanzeRdLs { get { return GetCollection<RisorseDistanzeRdL>("RisorseDistanzeRdLs"); } }


         public override string ToString()
        {
            if (this == null) return null;
            if (this.Oid == -1) return null;
            if (this.RdL == null) return null;

            return string.Format("{0}({1})", this.Oid, this.RdL.Descrizione);
        }

    }



}
