using CAMS.Module.Classi;
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

//   SchedulerStorage.Appointments.ResourceSharing

namespace CAMS.Module.DBScheduler
{
    [DefaultClassOptions, Persistent("ORARIACCENSIONI")]
    [NavigationItem("Agenda")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Calendartio Orari")]
    [ImageName("SwitchTimeScalesTo")] 

   // [Appearance("NotificaRdL.tempoallarme.scaduto", TargetItems = "AlarmTime",
   //         Criteria = @"IsScaduto", FontStyle = FontStyle.Bold, FontColor = "Red")]  //AlarmTime  >= LocalDateTimeToday()

   // [Appearance("NotificaRdL.tempoallarme.nonscaduto", TargetItems = "AlarmTime",
   //         Criteria = @"!IsScaduto", FontStyle = FontStyle.Bold, FontColor = "Black")]

   // //[Appearance("NotificaRdL.Editabili", TargetItems = "RdL", FontColor = "Black", Enabled = false)]
   // [Appearance("NotificaRdL.rdl.Editabili", TargetItems = "RdL", Criteria = @"RdL.UltimoStatoSmistamento.Oid != 1",
   //                                          FontColor = "Blue", Enabled = false)]

   // [Appearance("NotificaRdL.enable",
   //TargetItems = "StartDate;DueDate;DateCompleted;DataCreazione",
   //    Criteria = @"4 = 4", FontColor = "Black", Enabled = false)]

    #region filtri
    //   DataAl <= LocalDateTimeToday()
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("OrariAccensioni_noComplete", "StatusNotifica != 'Completed'", "Notifiche Attive", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("OrariAccensioni_Complete", "StatusNotifica = 'Completed'", "Notifiche Completate", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutte Orari", "", "Tutto", Index = 2)]
    #endregion

    public class OrariAccensioni : XPObject, IEvent, ISupportNotifications
    {
        public OrariAccensioni(Session session)
            : base(session)
        {
            Resources.ListChanged += new ListChangedEventHandler(Resources_ListChanged);
        }
        public OrariAccensioni() : base() { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //StartOn = DateTime.Now;
            //EndOn = StartOn.AddHours(1);

            appointmentImpl.AfterConstruction();
            if (this.Oid == -1)
            {
                this.StatusNotifica = DevExpress.Persistent.Base.General.TaskStatus.NotStarted;
                DataCreazione = DateTime.Now;
                OidTRisorsa_old = 0;
            }
        }

#if MediumTrust
		[Persistent("ResourceIds"), Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
		public String resourceIds;
  
#else
        [Persistent("ResourceIds"), Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        private String resourceIds;

#endif
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        #region variabili e costanti per skeduler
        private TaskImpl task = new TaskImpl();
        private DateTime? alarmTime;
        private TimeSpan? remindIn;
        private IList<PostponeTime> postponeTimes;
        #endregion
        #region variabili e costanti per skeduler
        private EventImpl appointmentImpl = new EventImpl();

        //private Nullable<DateTime> alarmTime;
        #endregion


        private void SetAlarmTime(DateTime? startDate, TimeSpan remindTime)
        {
            alarmTime = ((startDate - DateTime.MinValue) > remindTime) ? startDate - remindTime : DateTime.MinValue;
        }

        private IList<PostponeTime> CreatePostponeTimes()
        {
            IList<PostponeTime> result = PostponeTime.CreateDefaultPostponeTimesList();
            result.Add(new PostponeTime("None", null, "None"));
            result.Add(new PostponeTime("AtStartTime", TimeSpan.Zero, "At Start Time"));
            //result.Add(new PostponeTime("Atmio", TimeSpan.Zero, "At Start Time mio"));
            PostponeTime.SortPostponeTimesList(result);
            return result;
        }
        protected override void OnLoading()
        {
            //appointmentImpl.IsLoading = true;
            Debug.WriteLine(string.Format("OnLoading oid {0}, rdl {1}", this.Oid, this.RdL));
            base.OnLoading();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            //task.DateCompleted = dateCompleted;
            //task.IsLoading = false;
            Debug.WriteLine(string.Format("OnLoaded oid {0}, rdl {1}", this.Oid, this.RdL));

            #region scheduler
            if (Resources.IsLoaded && !Session.IsNewObject(this))
            {
                Resources.Reload();
            }
            #endregion
        }

        #region void scheduler   METODI X SCKEDULER
        private void UpdateResources()
        {
            Resources.SuspendChangedEvents();
            try
            {
                int i = 0;
                this.IsCambioRisorsa = false;
                while (Resources.Count > 0)
                {
                    if (i == 0)
                        OidTRisorsa_old = Resources[0].Oid;
                    Resources.Remove(Resources[0]);
                    i++;
                }
                if (!String.IsNullOrEmpty(resourceIds))
                {
                    XmlDocument xmlDocument = new XmlDocument();
                    xmlDocument.LoadXml(resourceIds);
                    foreach (XmlNode xmlNode in xmlDocument.DocumentElement.ChildNodes)
                    {
                        AppointmentResourceIdXmlLoader loader = new AppointmentResourceIdXmlLoader(xmlNode);
                        Object keyMemberValue = loader.ObjectFromXml();
                        // Resource resource = Session.GetObjectByKey<Resource>(new Guid(keyMemberValue.ToString()));
                        //esempioRisorse resource = Session.GetObjectByKey<esempioRisorse>(new Guid(keyMemberValue.ToString()));
                        CircuitiOrari resource = Session.GetObjectByKey<CircuitiOrari>(int.Parse(keyMemberValue.ToString()));
                        if (resource != null)
                        {
                            Resources.Add(resource);
                        }
                    }
                }
            }
            finally
            {
                Resources.ResumeChangedEvents();
            }
        }
        private void Resources_ListChanged(object sender, ListChangedEventArgs e)
        {
            if ((e.ListChangedType == ListChangedType.ItemAdded) ||
                (e.ListChangedType == ListChangedType.ItemDeleted))
            {
                UpdateResourceIds();
                OnChanged("ResourceId");
                //OnChanged("OidTRisorsa");
            }
        }

        public void UpdateResourceIds()
        {
            ResourceId_Old = resourceIds;

            resourceIds = "<ResourceIds>\r\n";
            foreach (CircuitiOrari resource in Resources)//    foreach (Resource resource in Resources)
            {
                resourceIds += string.Format("<ResourceId Type=\"{0}\" Value=\"{1}\" />\r\n", resource.Id.GetType().FullName, resource.Id);
            }
            resourceIds += "</ResourceIds>";
        }



        private int _oidTRisorsa_old;
        [Persistent("OID_RISORSA_OLD"), DevExpress.Xpo.DisplayName("OidTRisorsa")]
        //[ImmediatePostData(true)]
        [Browsable(false)]
        public int OidTRisorsa_old
        {
            get
            {
                return _oidTRisorsa_old;
            }
            set
            {
                SetPropertyValue<int>("OidTRisorsa_old", ref _oidTRisorsa_old, value);
            }
        }



        #endregion

        #region parametri  SCHEDULER #########################  parametri  SCHEDULER #########################
        [Persistent("LOCATION")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Utente")]
        public string Location
        {
            get { return appointmentImpl.Location; }
            set
            {
                string oldValue = appointmentImpl.Location;
                appointmentImpl.Location = value;
                OnChanged("Location", oldValue, appointmentImpl.Location);
            }
        }

        [Browsable(false)]
        [Persistent("OIDLOCATION"), System.ComponentModel.DisplayName("OIDLOCATION")]
        [Delayed(true)]
        public int OidLocation
        {
            get { return GetDelayedPropertyValue<int>("OidLocation"); }
            set { SetDelayedPropertyValue<int>("OidLocation", value); }

        }

        [Persistent("LABEL")]
        //[Browsable(false)]   //[MemberDesignTimeVisibility(false)]
        [DevExpress.ExpressApp.DC.XafDisplayName("Stato Notifica")]
        [VisibleInListView(false), VisibleInDetailView(false), VisibleInLookupListView(false)]
        [Appearance("notifica.rdl.Enabled", FontColor = "Black", Enabled = false)]
        public int Label
        {
            get
            {
                appointmentImpl.Label = (int)LabelListView;
                return appointmentImpl.Label;
            }
            set
            {
                int oldValue = appointmentImpl.Label;
                appointmentImpl.Label = value;
                OnChanged("Label", oldValue, appointmentImpl.Label);
            }
        }

        private LabelListView fLabelListView;
        [Persistent("LABELDV")]
        //[PersistentAlias("Iif(RdL is not null And RdL.StatoAutorizzativo is not null,RdL.StatoAutorizzativo.Oid,0)")]
        [DevExpress.Xpo.DisplayName("Stato Notifica ")]
        [VisibleInListView(true), VisibleInDetailView(true), VisibleInLookupListView(true)]
        [Appearance("notifica.rdl.LabelListView.Enabled", FontColor = "Black", Enabled = false)]
        public LabelListView LabelListView
        {
            get            {                return fLabelListView;            }
            set            {                SetPropertyValue<LabelListView>("LabelListView", ref fLabelListView, value);            }
        }

        [Persistent("STATUS")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Stato Appuntamento")]
        //[BrowsableAttribute(false)]
        public int Status
        {
            get { return appointmentImpl.Status; }
            set
            {
                //this.RdL.UltimoStatoSmistamento
                int oldValue = appointmentImpl.Status;
                appointmentImpl.Status = value;
                OnChanged("Status", oldValue, appointmentImpl.Status);
            }
        }

        [Browsable(false)]
        [Persistent("TYPE")]
        public int Type
        {
            get { return appointmentImpl.Type; }
            set
            {
                int oldValue = appointmentImpl.Type;
                appointmentImpl.Type = value;
                OnChanged("Type", oldValue, appointmentImpl.Type);
            }
        }

        [Indexed]
        [Persistent("START_ON")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Inizio Orario")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        //[RuleValueComparison("RuleVComp.notificaRdL.StartOn", DefaultContexts.Save, ValueComparisonType.LessThan, "LocalDateTimeToday()", ParametersMode.Expression)]
        [Appearance("App.notificaRdL.StartOn.BackColor.Red", AppearanceItemType.LayoutItem, "StartOn < LocalDateTimeToday()", FontStyle = FontStyle.Bold, FontColor = "Red", Priority = 1)]
        [ImmediatePostData(true)]
        public DateTime StartOn
        {
            get { return appointmentImpl.StartOn; }
            set
            {
                DateTime oldValue = appointmentImpl.StartOn;
                appointmentImpl.StartOn = value;
                OnChanged("StartOn", oldValue, appointmentImpl.StartOn);
            }
        }

        [Indexed]
        [Persistent("END_ON")]
        [XafDisplayName("Fine Orario")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        //[RuleValueComparison("RuleVComp.notificaRdL.EndOn", DefaultContexts.Save, 
        //    ValueComparisonType.GreaterThan,"StartOn", ParametersMode.Expression,
        //    CustomMessageTemplate = @"la data {TargetPropertyName} deve essere maggiore della data {RightOperand}")]
        ////[RuleValueComparison("RuleVComp.notificaRdL.EndOn", "Save",
        ////     ValueComparisonType.GreaterThan, "StartOn",
        ////    "The '{TargetPropertyName}' property has the '{TargetValue}' value. It should be greater than '{RightOperand}'.")]
        [ImmediatePostData(true)]
        public DateTime EndOn
        {
            get { return appointmentImpl.EndOn; }
            set
            {
                DateTime oldValue = appointmentImpl.EndOn;
                appointmentImpl.EndOn = value;
                OnChanged("EndOn", oldValue, appointmentImpl.EndOn);
            }
        }



        //  DI DEFINIZIONE  X SKEDULER
        [NonPersistent]
        //[ Browsable(false)]
        [XafDisplayName("Codice Notifica")]
        public object AppointmentId
        {
            get { return Oid; }
        }

        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        // [ToolTip("Data di Creazione della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        [VisibleInListView(false)]
        //[Delayed(true)]
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


        //  DEFINIZIONE DELLA RISORSA  "[<Immobile>][^.Oid = Indirizzo And ClusterEdifici.Oid = {0}].Count() > 0"   GetOidCentroOperativo
        //  "Oid=-1 And [Categoria.Oid] != 4 And [<ApparatoSchedaMP>][Iif(^.Categoria.Oid = 5,1,^.Categoria.Oid) = Categoria.Oid And ^.Apparato = Apparato].Exists";
        //[DataSourceCriteria("[<RisorseTeam>][^.Oid == Oid].single() And Iif(RdL is not null,RisorsaCapo.CentroOperativo.Oid == '@This.RdL.Apparato.Impianto.Immobile.CentroOperativoBase.Oid', 1=1)]")]
        [Association("OrariAccensioni-CircuitiOrari")]
        //[Association("NotificaRdL-Resource")]
        //[DataSourceCriteria("[<RisorseTeam>][^.Oid == Oid] And Iif('@This.RdL' is not null,'@This.RdL.Apparato.Impianto.Immobile.CentroOperativoBase.Oid' = [<RisorseTeam>][^.Oid == Oid].single(RisorsaCapo.CentroOperativo.Oid),1=1)")]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [ImmediatePostData]
        [XafDisplayName("Circuiti Orari")]
        public XPCollection<CircuitiOrari> Resources //  public XPCollection<Resource> Resources
        {
            get
            {
                return GetCollection<CircuitiOrari>("Resources");
            }
        }



        [NonPersistent(), Browsable(false)]        //[Persistent("RESOURCE_ID")]
        public String ResourceId
        {
            get
            {
                if (resourceIds == null)
                {
                    UpdateResourceIds();
                }
                return resourceIds;
            }
            set
            {
                if (resourceIds != value)
                {
                    resourceIds = value;
                    UpdateResources();
                }
            }
        }

        private String resourceIds_old;
        [Persistent("RESOURCE_IDS_OLD")]     //[NonPersistent()]
        [Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        [Browsable(false)]
        public String ResourceId_Old
        {
            get
            {
                return resourceIds_old;
            }
            set
            {
                SetPropertyValue("ResourceId_Old", ref resourceIds_old, value);
            }
        }
        #endregion

        [Persistent("SUBJECT"), XafDisplayName("Oggetto")]
        [DbType("varchar(4000)")]
        public string Subject
        {
            get { return appointmentImpl.Subject; }
            set
            {
                string oldValue = appointmentImpl.Subject;
                appointmentImpl.Subject = value;
                OnChanged("Subject", oldValue, appointmentImpl.Subject);
            }
        }

        [Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        [Persistent("DESCRIZIONE")]
        [XafDisplayName("Descrizione")]
        public string Description
        {
            get { return appointmentImpl.Description; }
            set
            {
                string oldValue = appointmentImpl.Description;
                appointmentImpl.Description = value;
                OnChanged("Description", oldValue, appointmentImpl.Description);
            }
        }


        [Persistent("STARTDATE"), XafDisplayName("Data Start Avviso")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        public DateTime StartDate
        {
            get { return task.StartDate; }
            set
            {
                DateTime oldValue = task.StartDate;
                task.StartDate = value;
                OnChanged("StartDate", oldValue, task.StartDate);
                if (!IsLoading && oldValue != value && remindIn != null)
                {
                    SetAlarmTime(value, remindIn.Value);
                }
            }
        }

        [Persistent("DUEDATE"), XafDisplayName("Data Scadenza Avviso")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        public DateTime DueDate
        {
            get { return task.DueDate; }
            set
            {
                DateTime oldValue = task.DueDate;
                task.DueDate = value;
                OnChanged("DueDate", oldValue, task.DueDate);
            }
        }

        [Persistent("COMPLETEDDATE"), XafDisplayName("Data Completamento Avviso")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        public DateTime DateCompleted
        {
            get { return task.DateCompleted; }
            set
            {
                DateTime oldValue = task.DateCompleted;
                task.DateCompleted = value;
                OnChanged("DateCompleted", oldValue, task.DateCompleted);
            }
        }



        #region RdL                         RdL

        private RdL fRdL;
        [Persistent("RDL"), DevExpress.ExpressApp.DC.XafDisplayName("RdL")]
        [DataSourceCriteria("[UltimoStatoSmistamento.Oid] == 1")]
        [ExplicitLoading()]
        [Delayed(true)]
        public RdL RdL
        {
            get { return GetDelayedPropertyValue<RdL>("RdL"); }
            set { SetDelayedPropertyValue<RdL>("RdL", value); }
        }


        //private string fMessaggioUtente;
        //[Size(500), Persistent("MESSAGGIO"), System.ComponentModel.DisplayName("nota")]
        //[Appearance("NotificaRdL.MessaggioUtente.Visibility_Hide", AppearanceItemType.LayoutItem, "IsNullOrEmpty(MessaggioUtente)", Visibility = ViewItemVisibility.Hide)]
        //[Appearance("NotificaRdL.MessaggioUtente.EvidenzaObligatorio", AppearanceItemType.ViewItem, "!IsNullOrEmpty(MessaggioUtente)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[DbType("varchar(500)")]
        //[VisibleInListView(false)]
        ////[Delayed(true)]
        //public string MessaggioUtente
        //{
        //    get
        //    {
        //        return fMessaggioUtente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("MessaggioUtente", ref fMessaggioUtente, value);
        //    }
        //}
        #endregion

        #region  PARAMETRI DI NOTIFICA ############ PARAMETRI DI NOTIFICA ############


        [Persistent("STATUSNOTIFICA")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Etichetta Notifica")]
        //[BrowsableAttribute(false)]
        public DevExpress.Persistent.Base.General.TaskStatus StatusNotifica
        {
            get { return task.Status; }
            set
            {
                DevExpress.Persistent.Base.General.TaskStatus oldValue = task.Status;
                task.Status = value;
                OnChanged("Status", oldValue, task.Status);
            }
        }


        [ImmediatePostData]
        [Browsable(false)]
        public bool AllDay
        {
            get { return appointmentImpl.AllDay; }
            set
            {
                bool oldValue = appointmentImpl.AllDay;
                appointmentImpl.AllDay = value;
                OnChanged("AllDay", oldValue, appointmentImpl.AllDay);
            }
        }



        [Browsable(false)]
        [Persistent("TIMESPAN"), DevExpress.ExpressApp.DC.XafDisplayName("Intervallo di Tempo")]
        public TimeSpan? RemindIn
        {
            get { return remindIn; }
            set
            {
                SetPropertyValue("RemindIn", ref remindIn, value);
                if (!IsLoading)
                {
                    if (value != null)
                    {
                        SetAlarmTime(StartDate, value.Value);
                    }
                    else
                    {
                        alarmTime = null;
                    }
                }
            }
        }

        [Persistent("TIMEALARM"), DevExpress.ExpressApp.DC.XafDisplayName("Timer di Allarme")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        public DateTime? AlarmTime
        {
            get { return alarmTime; }
            set
            {
                SetPropertyValue("AlarmTime", ref alarmTime, value);
                if (value == null)
                {
                    remindIn = null;
                    IsPostponed = false;
                }
            }
        }

        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("AllowClear", "False")]
        [DataSourceProperty("PostponeTimeList")]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
        [VisibleInListView(false)]
        public PostponeTime ReminderTime
        {
            get
            {
                if (RemindIn.HasValue)
                {
                    return PostponeTimeList.Where(x => (x.RemindIn != null && x.RemindIn.Value == remindIn.Value)).FirstOrDefault();
                }
                else
                {
                    return PostponeTimeList.Where(x => x.RemindIn == null).FirstOrDefault();
                }
            }
            set
            {
                if (!IsLoading)
                {
                    if (value.RemindIn.HasValue)
                    {
                        RemindIn = value.RemindIn.Value;
                    }
                    else
                    {
                        RemindIn = null;
                    }
                }
            }
        }
        [Browsable(false), NonPersistent]
        public IEnumerable<PostponeTime> PostponeTimeList
        {
            get
            {
                if (postponeTimes == null)
                {
                    postponeTimes = CreatePostponeTimes();
                }
                return postponeTimes;
            }
        }

        [Browsable(false)]
        [NonPersistent]
        public bool IsScaduto // SE è SCADUTO CAMBIA COLORE ALLA RIGA
        {
            get
            {
                return this.alarmTime < DateTime.Now;
            }
        }

        private bool _IsCambioRisorsa;
        [NonPersistent]
        [Browsable(false)]
        public bool IsCambioRisorsa
        {
            get
            {
                return _IsCambioRisorsa;
            }
            set
            {
                SetPropertyValue<bool>("IsCambioRisorsa", ref _IsCambioRisorsa, value);
            }
        }

        /// <summary>
        ///  DA QUI OGGETTI CHE VENGONO VISTI NEL POPUP
        /// </summary>
        [Browsable(false)]
        [NonPersistent]
        [Size(SizeAttribute.Unlimited), VisibleInListView(true)]
        public string NotificationMessage
        {
            get
            {
                // INDICARE LO STEP DI STATO: 1 TEMPO DI DICHIARAZIONE, 2 TEMPO DI ACCETTAZIONE, 3 TEMPO DI TRASFERIMENTO (PRESA IN CARICO)
                string tmp = this.Label != null ? this.Label.ToString() : "0";
                return string.Format("{0}[{1}] - {2}", tmp, this.Oid, this.Subject);
            }
        }
        [NonPersistent]
        [Browsable(false)]
        public object UniqueId
        {
            get { return Oid; }
        }
        [Browsable(false)]
        [Persistent("ISPOSTPONED"), DevExpress.ExpressApp.DC.XafDisplayName("Ritardato")]
        public bool IsPostponed
        {
            get;
            set;
        }

        #endregion
    }


}
