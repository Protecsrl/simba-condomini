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
using CAMS.Module.DBTask.Vista;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using CAMS.Module.DBPlanner;
using CAMS.Module.Classi;
using CAMS.Module.DBTask.ParametriPopUp;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListViewMPController : ViewController
    {
        public RdLListViewMPController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem += LViewController_CustomDetailView;
        }
        protected override void OnViewControlsCreated()
        {
            
            base.OnViewControlsCreated();
            // Access and customize the target View control.

        }
        protected override void OnDeactivated()
        {
            try
            {
                Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomizeShowViewParameters -= LViewController_CustomizeShowViewParameters;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        void LViewController_CustomDetailView(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                e.Handled = true;
                DetailView NewDv;

                RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(((RdLListViewMP)View.CurrentObject).Codice);

                NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.View;
                    e.InnerArgs.ShowViewParameters.CreatedView = NewDv;
                    e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }
        }


    }
}
