using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;


namespace CAMS.Module.Controllers.DBPlanner
{
    public partial class MPDataFissaDettaglioController : ViewController
    {
        public MPDataFissaDettaglioController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //if (Frame != null && Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>() != null)
            //{
            //    Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
            //    Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
            //}
            //Frame.GetController<DevExpress.ExpressApp.SystemModule.NewObjectViewController>().Actions["New"].Active["EliminazioneTastoNuovo"] = false;
            //if (  View.Id == "MPDataFissa_MPDataFissaDettaglio_ListView")
            //{
            //    Frame.GetController<DevExpress.ExpressApp.SystemModule.NewObjectViewController>()
            //                   .Actions["New"].Active["EliminazioneTastoNuovo"] = false;
            //}
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }


        private void HideNewButtons(bool tastoNewAttivo)
        {
            //if (View is ListView)
            //{
            //    Frame.GetController<DevExpress.ExpressApp.SystemModule.NewObjectViewController>().Actions["New"].Active["EliminazioneTastoNuovo"] = tastoNewAttivo;
            //}
            //if (View is DetailView)
            //{
            //    Frame.GetController<DevExpress.ExpressApp.SystemModule.NewObjectViewController>().Actions["New"].Active["EliminazioneTastoNuovo"] = tastoNewAttivo;
            //}
        }
    }
}
