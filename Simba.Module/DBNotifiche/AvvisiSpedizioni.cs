using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.SystemModule.Notifications;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
 
namespace CAMS.Module.DBNotifiche
{
    [NavigationItem("Notifications")]
    [ImageName("Notifications.Sheduler_with_notifications")]
    [System.ComponentModel.DefaultProperty("Subject")]

    public class AvvisiSpedizioni : XPObject, ISupportNotifications
    {
        public AvvisiSpedizioni(Session session) : base(session)
        {
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        private TaskImpl task = new TaskImpl();
        private DateTime? alarmTime;//
        private TimeSpan? remindIn;//
        private IList<PostponeTime> postponeTimes;
        protected override void OnLoading()
        {
            task.IsLoading = true;
            base.OnLoading();
        }
        protected override void OnLoaded()
        {
            base.OnLoaded();
            task.DateCompleted = dateCompleted;
            task.IsLoading = false;
        }

        public void MarkCompleted()
        {
            DevExpress.Persistent.Base.General.TaskStatus oldStatus = task.Status;
            task.MarkCompleted();
            OnChanged("Status", oldStatus, task.Status);
        }


        private void SetAlarmTime(DateTime? startDate, TimeSpan remindTime)
        {
            alarmTime = ((startDate - DateTime.MinValue) > remindTime) ? startDate - remindTime : DateTime.MinValue;
        }
        [Persistent("DateCompleted")]
        private DateTime dateCompleted
        {
            get { return task.DateCompleted; }
            set
            {
                DateTime oldValue = task.DateCompleted;
                task.DateCompleted = value;
                OnChanged("dateCompleted", oldValue, task.DateCompleted);
            }
        }
        private IList<PostponeTime> CreatePostponeTimes()
        {
            IList<PostponeTime> result = PostponeTime.CreateDefaultPostponeTimesList();
            result.Add(new PostponeTime("None", null, "None"));
            result.Add(new PostponeTime("AtStartTime", TimeSpan.Zero, "At Start Time"));
            result.Add(new PostponeTime("AtOneTime", TimeSpan.FromSeconds(60), "a Un Minuto"));
            PostponeTime.SortPostponeTimesList(result);
            return result;
        }


        [Size(25)]
        [DbType("varchar(4000)")]
        public string Subject
        {
            get { return task.Subject; }
            set
            {
                string oldValue = task.Subject;
                task.Subject = value;
                OnChanged("Subject", oldValue, task.Subject);
            }
        }
        [Size(SizeAttribute.Unlimited), ObjectValidatorIgnoreIssue(typeof(ObjectValidatorLargeNonDelayedMember))]
        public string Description
        {
            get { return task.Description; }
            set
            {
                string oldValue = task.Description;
                task.Description = value;
                OnChanged("Description", oldValue, task.Description);
            }
        }
        private string fUtenteCreatoRichiesta;
        [Size(25), Persistent("UTENTEINSERIMENTO"), System.ComponentModel.DisplayName("Utente")]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }
        }
        [Persistent("OidRDL")]
        [DevExpress.Xpo.DisplayName("Codice RdL")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Delayed(true)]
        public int RdLUnivoco
        {
            get { return GetDelayedPropertyValue<int>("RdLUnivoco"); }
            set { SetDelayedPropertyValue<int>("RdLUnivoco", value); }
        }

        [Persistent("OidSmistamento")]
        [DevExpress.Xpo.DisplayName("Codice Smistamento")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Delayed(true)]
        public int OidSmistamento
        {
            get { return GetDelayedPropertyValue<int>("OidSmistamento"); }
            set { SetDelayedPropertyValue<int>("OidSmistamento", value); }
        }


        [Persistent("OidRisorsaTeam")]
        [DevExpress.Xpo.DisplayName("Codice RisorsaTeam")]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [Delayed(true)]
        public int OidRisorsaTeam
        {
            get { return GetDelayedPropertyValue<int>("OidRisorsaTeam"); }
            set { SetDelayedPropertyValue<int>("OidRisorsaTeam", value); }
        }



        private FlgAbilitato fSollecito;
        [Persistent("SOLLECITO"), DevExpress.ExpressApp.DC.XafDisplayName("Sollecito")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Sollecito
        {
            get { return fSollecito; }
            set { SetPropertyValue<FlgAbilitato>("Sollecito", ref fSollecito, value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }

        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), DevExpress.Xpo.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataCreazione
        {
            get { return fDataCreazione; }
            set { SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value); }
        }

        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
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

        [Persistent("DATASCADENZA"), DevExpress.ExpressApp.DC.XafDisplayName("Data Scadenza")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
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
        //public DevExpress.Persistent.Base.General.TaskStatus Status
        //{
        //    get { return task.Status; }
        //    set
        //    {
        //        DevExpress.Persistent.Base.General.TaskStatus oldValue = task.Status;
        //        task.Status = value;
        //        OnChanged("Status", oldValue, task.Status);
        //    }
        //}

        public myTaskStatus Status
        {
            get { return (myTaskStatus)task.Status; }
            set
            {
                myTaskStatus oldValue = (myTaskStatus)task.Status;
                task.Status = (DevExpress.Persistent.Base.General.TaskStatus)value;
                OnChanged("Status", oldValue, task.Status);
            }
        }

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Abilitato
        {
            get { return fAbilitato; }
            set { SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value); }
        }
        //public Int32 PercentCompleted
        //{
        //    get { return task.PercentCompleted; }
        //    set
        //    {
        //        Int32 oldValue = task.PercentCompleted;
        //        task.PercentCompleted = value;
        //        OnChanged("PercentCompleted", oldValue, task.PercentCompleted);
        //    }
        //}

        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DateCompleted
        {
            get { return dateCompleted; }
        }

        //private string fOggettoMail;
        //[Size(255), Persistent("OGGETTO_MAIL"), DevExpress.Xpo.DisplayName("Oggetto Mail")]
        //[DbType("varchar(500)")] 
        //public string OggettoMail
        //{
        //    get { return fOggettoMail; }
        //    set { SetPropertyValue<string>("OggettoMail", ref fOggettoMail, value); }
        //}

        //private string fCorpoMail;
        //[Persistent("CORPO_MAIL"), System.ComponentModel.DisplayName("CORPO Mail")]
        //[Size(SizeAttribute.Unlimited)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string CorpoMail
        //{
        //    get { return fCorpoMail; }
        //    set { SetPropertyValue<string>("CorpoMail", ref fCorpoMail, value); }
        //}

        //private string fOggettoSMS;
        //[Size(255), Persistent("OGGETTO_SMS"), DevExpress.Xpo.DisplayName("Oggetto SMS")]
        //[DbType("varchar(500)")]
        //public string OggettoSMS
        //{
        //    get { return fOggettoSMS; }
        //    set { SetPropertyValue<string>("OggettoSMS", ref fOggettoSMS, value); }
        //}


        //private string fCorpoSMS;
        //[Persistent("CORPO_SMS"), System.ComponentModel.DisplayName("CORPO SmS")]
        //[Size(SizeAttribute.Unlimited)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string CorpoSMS
        //{
        //    get { return fCorpoSMS; }
        //    set { SetPropertyValue<string>("CorpoSMS", ref fCorpoSMS, value); }
        //}

        [Association(@"Avvisispedizione_RegistroSpedizioneDett", typeof(RegistroSpedizioniDett)),
            Aggregated, DevExpress.ExpressApp.DC.XafDisplayName("Spedizioni dettaglio")]
        [ExplicitLoading]
        public XPCollection<RegistroSpedizioniDett> RegistroSpedizioniDetts
        {
            get
            {
                return GetCollection<RegistroSpedizioniDett>("RegistroSpedizioniDetts");
            }
        }

        #region
        [ImmediatePostData]
        [NonPersistent]
        [ModelDefault("AllowClear", "False")]
        [DataSourceProperty("PostponeTimeList")]
        [SearchMemberOptions(SearchMemberMode.Exclude)]
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
        [Browsable(false)]
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
        [Browsable(false)]
        [NonPersistent]
        public string NotificationMessage
        {
            get { return Subject; }
        }
        [NonPersistent]
        [Browsable(false)]
        public object UniqueId
        {
            get { return Oid; }
        }
        [Browsable(false)]
        public bool IsPostponed
        {
            get;
            set;
        }
        #endregion



    }
    public class AvvisiSpedizioniController : ViewController
    {
        private SimpleAction markCompletedAction;
        private void MarkCompletedAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ((AvvisiSpedizioni)View.CurrentObject).MarkCompleted();
        }
        public AvvisiSpedizioniController()
        {
            TargetObjectType = typeof(AvvisiSpedizioni);
            markCompletedAction = new SimpleAction(this, "Set_Completato", PredefinedCategory.Edit);
            markCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            markCompletedAction.ImageName = "State_Task_Completed";
            markCompletedAction.Execute += MarkCompletedAction_Execute;
        }
    }
    public enum myTaskStatus
    {
        NonDefinito = 0,
        Predisposto = 1,
        Inviato = 2,
        FallitoInvio = 3,
        FallitoUlterioreInvio = 4
    }
}

