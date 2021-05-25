using System;
using System.Linq;
using System.Text;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Security.Strategy;

namespace CAMS.Module.DBKPI.ParametriPopUp
{
    [DefaultClassOptions]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "File DownLoad")]
    //[ImageName("BO_Contact")]
    //[DefaultProperty("DisplayMemberNameForLookupEditorsOfThisType")]
    //[DefaultListViewOptions(MasterDetailMode.ListViewOnly, false, NewItemRowPosition.None)]
    [Persistent("DOWNLOADFILE")]
    // Specify more UI options using a declarative approach (https://documentation.devexpress.com/#eXpressAppFramework/CustomDocument112701).
    public class DownLoadFileKPI : XPObject
    { // Inherit from a different class to provide a custom primary key, concurrency and deletion behavior, etc. (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument113146.aspx).
        public DownLoadFileKPI(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Session.IsNewObject(this))
            {
                var a = this.Session.GetObjectByKey<SecuritySystemUser>(SecuritySystem.CurrentUserId);
                this.SecurityUser = (SecuritySystemUser)a;//  this.Session.;
                this.Data = DateTime.Now;
            }
        }
        private FileData _PersistentProperty;
        [XafDisplayName("File Download"), ToolTip("Clicca qui per scaricare il file")]
        //[ModelDefault("EditMask", "(000)-00"), Index(0), VisibleInListView(false)]
        [Persistent("DownLoadFileKPI")]
        //[RuleRequiredField(DefaultContexts.Save)]
        public FileData PersistentProperty
        {
            get { return _PersistentProperty; }
            set { SetPropertyValue("PersistentProperty", ref _PersistentProperty, value); }
        }

        private string _Messaggio;
        [Appearance("DownLoadFileKPI.Messaggio.FontColorRed",    FontColor = "Red")]
        [XafDisplayName("Avviso"), ToolTip("Messaggio per aggiornare il file Excel")]
        [Persistent("Messaggio")]
        public string Messaggio
        {
            get { return _Messaggio; }
            set { SetPropertyValue("Messaggio", ref _Messaggio, value); }
        }

        private SecuritySystemUser fSecurityUser;
        [Persistent("SECURITYUSERID"), XafDisplayName("Utente")]
        [RuleRequiredField("RReqField.downloadFilekpi.SecurityUser", DefaultContexts.Save, "Utente è un campo obbligatorio")]
        public SecuritySystemUser SecurityUser
        {
            get { return fSecurityUser; }
            set { SetPropertyValue<SecuritySystemUser>("SecurityUser", ref fSecurityUser, value); }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        private DateTime fData;
        [Persistent("DATA"), XafDisplayName("Data")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }

        //[Action(Caption = "My UI Action", ConfirmationMessage = "Are you sure?", ImageName = "Attention", AutoCommit = true)]
        //public void ActionMethod() {
        //    // Trigger a custom business logic for the current record in the UI (https://documentation.devexpress.com/eXpressAppFramework/CustomDocument112619.aspx).
        //    this.PersistentProperty = "Paid";
        //}
    }
}