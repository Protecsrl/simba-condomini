using System;
using DevExpress.Xpo;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.Data.Filtering;


namespace CAMS.Module.DBHelp
{
    /// <summary>
    /// A base custom Application Model element extension for the View Item node.
    /// </summary>
    public interface IModelCustomUserControlViewItem : IModelViewItem
    {
    }

    /// <summary>
    /// A base custom View Item that hosts a custom user control (http://documentation.devexpress.com/#Xaf/CustomDocument2612) to show it in the XAF View.
    /// </summary>
    [ViewItem(typeof(IModelCustomUserControlViewItem))]
    public abstract class CustomUserControlViewItem : ViewItem, IComplexViewItem
    {
        public CustomUserControlViewItem(IModelViewItem model, Type objectType)
            : base(objectType, model != null ? model.Id : string.Empty)
        {
        }
        private IObjectSpace theObjectSpace;
        private XafApplication theApplication;
        public IObjectSpace ObjectSpace
        {
            get
            {
                return theObjectSpace;
            }
        }
        public XafApplication Application
        {
            get
            {
                return theApplication;
            }
        }
        public void Setup(IObjectSpace objectSpace, XafApplication application)
        {
            theObjectSpace = objectSpace;
            theApplication = application;
        }
        protected override void OnControlCreated()
        {
            base.OnControlCreated();
            XpoSessionAwareControlInitializer.Initialize(Control as IXpoSessionAwareControl, theObjectSpace);
        }
    }
    /// <summary>
    /// This interface is designed to provide persistent data from the XAF application to custom user controls or forms (the interface should be implemented by them).
    /// </summary>
    public interface IXpoSessionAwareControl
    {
        void UpdateDataSource(Session session);
    }
    public static class XpoSessionAwareControlInitializer
    {
        public static void Initialize(IXpoSessionAwareControl control, IObjectSpace objectSpace)
        {
            Guard.ArgumentNotNull(control, "control");
            Guard.ArgumentNotNull(objectSpace, "objectSpace");

            var persistentDataType = typeof(DevExpress.Persistent.BaseImpl.Task);
            objectSpace.GetObjects(persistentDataType, CriteriaOperator.Parse("Status = 'InProgress'"));

            var xpObjectSpace = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)objectSpace);
            control.UpdateDataSource(xpObjectSpace.Session);

            objectSpace.Reloaded += delegate(object sender, EventArgs args)
            {
                control.UpdateDataSource(xpObjectSpace.Session);
            };
        }
        public static void Initialize(IXpoSessionAwareControl sessionAwareControl, XafApplication theApplication)
        {
            Initialize(sessionAwareControl, theApplication != null ? theApplication.CreateObjectSpace() : null);
        }
    }
}
