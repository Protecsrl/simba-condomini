using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using DevExpress.Persistent.Base.General;
using DevExpress.ExpressApp;
using DevExpress.Utils.Base;

namespace CAMS.Module.DBAgenda
{

    //  [DefaultClassOptions, Persistent("V_NOTIFICHEINTERVENTI")]
    //[DevExpress.ExpressApp.Model.ModelDefault("Caption", "Notifiche Interventi RdL")]
    //[ImageName("CustomerInfoCards")]
    //[NavigationItem("Interventi")]
    //public class NotificheInterventiRdL : BaseObject, ISupportNotifications 
    //{
    //      private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

    //      //private int fcodice;
    //      //[Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
    //      //[Browsable(false)]
    //      //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
    //      //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
    //      ////[Appearance("RdL.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
    //      ////[Appearance("RdL.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
    //      ////[Appearance("RdL.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
    //      ////[Appearance("RdL.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
    //      ////[Appearance("RdL.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
    //      //public int Codice
    //      //{
    //      //    get
    //      //    {
    //      //        return fcodice;
    //      //    }
    //      //    set
    //      //    {
    //      //        SetPropertyValue<int>("Codice", ref fcodice, value);
    //      //    }
    //      //}
    //    [Browsable(false)]
    //    public int Id { get; private set; }

    //    //public string Subject { get; set; }
    //    private string fSubject;
    //    [Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
    //    [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
    //    [DbType("varchar(4000)")]
    //  public string Subject { get; set; }
    //    //public string Subject
    //    //{
    //    //    get
    //    //    {
    //    //        return fSubject;
    //    //    }
    //    //    set
    //    //    {
    //    //        SetPropertyValue<string>("Subject", ref fSubject, value);
    //    //    }
    //    //}

    //    [Persistent("DATASCADENZA"), System.ComponentModel.DisplayName("Data scadenza")]
    //    [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
    //    [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
    //    [ToolTip("Data Creazione dell'Intervento ", null, DevExpress.Utils.ToolTipIconType.Information)]
    //    //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
    //    public DateTime DueDate { get; set; }


    //    #region ISupportNotifications members
    //    private DateTime? alarmTime;
    //    [Browsable(false)]
    //    public DateTime? AlarmTime
    //    {
    //        get { return alarmTime; }
    //        set
    //        {
    //            alarmTime = value;
    //            if (value == null)
    //            {
    //                RemindIn = null;
    //                IsPostponed = false;
    //            }
    //        }
    //    }
    //    [Browsable(false)]
    //    public bool IsPostponed { get; set; }
    //    [Browsable(false)]//, NotMapped]
    //    public string NotificationMessage
    //    {
    //        get { return Subject; }
    //    }
    //    public TimeSpan? RemindIn { get; set; }
    //    [Browsable(false)]//, NotMapped]
    //    public object UniqueId
    //    {
    //        get { return Id; }
    //    }
    //    #endregion

    //    #region IXafEntityObject members
    //    public void OnCreated() { }
    //    public void OnLoaded() { }
    //    public void OnSaving()
    //    {
    //        if (RemindIn.HasValue)
    //        {
    //            if ((AlarmTime == null) || (AlarmTime < DueDate - RemindIn.Value))
    //            {
    //                AlarmTime = DueDate - RemindIn.Value;
    //            }
    //        }
    //        else
    //        {
    //            AlarmTime = null;
    //        }
    //        if (AlarmTime == null)
    //        {
    //            RemindIn = null;
    //            IsPostponed = false;
    //        }
    //    }
    //    #endregion


    //}
}
