using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBAgenda;
using CAMS.Module.DBTask;

namespace CAMS.Module.Controllers.DBAgenda
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class AppuntamentiRisorseController : ViewController
    {
        public AppuntamentiRisorseController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //Frame.GetController<NewObjectViewController>().NewObjectAction.Active.SetItemValue("New", false); 
            //Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", true);
            if (View.Id == "NotificaRdL_Resources_ListView")
            {
                //NotificaRdL nrdl = (NotificaRdL)((ListView)View).ObjectSpace.Owner;
                if (((ListView)View).CollectionSource.GetCount() > 0)
                {
                    Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void acUpdateRisorseApp_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is ListView)
            {                
                List<int> OidAppRisorseTeams = xpObjectSpace.GetObjects<AppuntamentiRisorse>().Select(s => s.RisorseTeam.Oid).ToList();

                List<int> OidRisorseTeams = xpObjectSpace.GetObjects<RisorseTeam>()
                    .Where(w => !OidAppRisorseTeams.Contains(w.Oid))
                    .Select(s => s.Oid).ToList();

                foreach (int rt in OidRisorseTeams)
                {
                    AppuntamentiRisorse AppRisorse = xpObjectSpace.CreateObject<AppuntamentiRisorse>();
                    //AppRisorse.Oid =  rt  ;   //AppRisorse.Oid = Convert.ToInt32( rt.ToString());
                    //AppRisorse.OidRisorseTeam = rt;
                    AppRisorse.RisorseTeam = xpObjectSpace.GetObjectByKey<RisorseTeam>(rt);
                    AppRisorse.Save();                     
                }
                xpObjectSpace.CommitChanges();
            }
        }
    }
}
