using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;

namespace CAMS.Module.Controllers.DBAngrafica
{
    public partial class DestinatariControlliNormativiController : ViewController
    {
        public DestinatariControlliNormativiController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
          
            base.OnActivated();

            if (Frame != null && Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>() !=null)
            {
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
    }
}
