using System;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;

namespace CAMS.Module.Classi
{
    public interface IModelMapsUserControlViewItem : IModelViewItem
    {
    }

    [ViewItem(typeof(IModelMapsUserControlViewItem))]
    public abstract class MapsUserControlViewItem : ViewItem, IComplexViewItem
    {
        private Boolean CaricaControllo = false;

        public MapsUserControlViewItem(IModelViewItem model, Type objectType)
            : base(objectType, model != null ? model.Id : string.Empty)
        {
            CaricaControllo = true;
        }
        private IObjectSpace theObjectSpace;
        private XafApplication theApplication;

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
            if (CaricaControllo == true)
            {
                CaricaControllo = false;
                XpoSessionAwareControlInitializer.Initialize(Control as IXpoSessionAwareControl, theObjectSpace);
            }
        }
    }

    public interface IXpoSessionAwareControl
    {
        void UpdateDataSource(XPObjectSpace xpObjectSpace);
    }

    public static class XpoSessionAwareControlInitializer
    {
        public static void Initialize(IXpoSessionAwareControl control, IObjectSpace objectSpace)
        {
            Guard.ArgumentNotNull(control, "control");
            Guard.ArgumentNotNull(objectSpace, "objectSpace");

            var xpObjectSpace = (XPObjectSpace)objectSpace;
            control.UpdateDataSource(xpObjectSpace);
        }
        public static void Initialize(IXpoSessionAwareControl sessionAwareControl, XafApplication theApplication)
        {
            Initialize(sessionAwareControl, theApplication != null ? theApplication.CreateObjectSpace() : null);
        }
    }
}
