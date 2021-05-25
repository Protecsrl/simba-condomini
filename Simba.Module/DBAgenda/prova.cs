using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System;
using System.Collections.Generic;
using System.Text;
using DevExpress.Persistent.Base;
using System.ComponentModel;
//using DevExpress.ExpressApp.Demos;
using DevExpress.Xpo;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp;
//using DevExpress.XtraScheduler;
//using DevExpress.XtraScheduler.Xml;
using System.Drawing;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp.SystemModule.Notifications;
using DevExpress.ExpressApp.Model;
using System.Linq;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp.Actions;
using CAMS.Module.DBTask;
//namespace CAMS.Module.DBAgenda
//{
//    class TaskWithNotifications
//    {
//    }
//}
//namespace CAMS.Module.DBAgenda{
//[NavigationItem("Interventi")]
//[Hint(Hints.TaskNotificationsHint)]
//[ImageName("Notifications.Task_with_notifications")]
//public class TaskWithNotifications : BaseObject, ISupportNotifications {

namespace CAMS.Module.DBAgenda
{
    [DefaultClassOptions, Persistent("RDLNOT")]
    [NavigationItem("Interventi")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "eventi interventi")]
    [ImageName("Notifications.Task_with_notifications")]
    public class prova : XPObject, ISupportNotifications
    {
        public prova(Session session) : base(session) { }
        public prova() : base() { }

        private TaskImpl task = new TaskImpl();
        private DateTime? alarmTime;
        private TimeSpan? remindIn;
        private IList<PostponeTime> postponeTimes;
        private void SetAlarmTime(DateTime? startDate, TimeSpan remindTime)
        {
            alarmTime = ((startDate - DateTime.MinValue) > remindTime) ? startDate - remindTime : DateTime.MinValue;
        }

        private IList<PostponeTime> CreatePostponeTimes()
        {
            IList<PostponeTime> result = PostponeTime.CreateDefaultPostponeTimesList();
            result.Add(new PostponeTime("None", null, "None"));
            result.Add(new PostponeTime("AtStartTime", TimeSpan.Zero, "At Start Time"));
            result.Add(new PostponeTime("Atmio", TimeSpan.Zero, "At Start Time mio"));
            PostponeTime.SortPostponeTimesList(result);
            return result;
        }
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
            TaskStatus oldStatus = task.Status;
            task.MarkCompleted();
            OnChanged("Status", oldStatus, task.Status);
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
        public TaskStatus Status
        {
            get { return task.Status; }
            set
            {
                TaskStatus oldValue = task.Status;
                task.Status = value;
                OnChanged("Status", oldValue, task.Status);
            }
        }
        public Int32 PercentCompleted
        {
            get { return task.PercentCompleted; }
            set
            {
                Int32 oldValue = task.PercentCompleted;
                task.PercentCompleted = value;
                OnChanged("PercentCompleted", oldValue, task.PercentCompleted);
            }
        }
        public DateTime DateCompleted
        {
            get { return dateCompleted; }
        }

        private RdL fRdL;
        [Persistent("RDL"), DevExpress.ExpressApp.DC.XafDisplayName("RdL")]
        [ExplicitLoading()]
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

        #region  ogetti di notifica
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


}


