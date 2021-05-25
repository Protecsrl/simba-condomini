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
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBPlant;

namespace CAMS.Module.Controllers.DBPlant
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ApparatoListViewController : ViewController
    {
        public ApparatoListViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem += LViewApparatoLVController_CustomDetailView; 

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
                Frame.GetController<DevExpress.ExpressApp.SystemModule.ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewApparatoLVController_CustomDetailView;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void saApparatoLViewEdit_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                    ApparatoListView alv = (ApparatoListView)View.CurrentObject;
                    int OidApp = 0;

                    if (int.TryParse(alv.Codice, out OidApp))
                    {
                        Asset Apparato = xpObjectSpace.GetObjectByKey<Asset>(OidApp);
                        if (Apparato != null)
                        {
                            DetailView dview = Application.CreateDetailView(xpObjectSpace, "Apparato_DetailView", true, Apparato);
                            dview.ViewEditMode = ViewEditMode.Edit;
                            e.ShowViewParameters.CreatedView = dview;
                            //view.Caption = view.Caption + " - Inserimento Documenti";
                            e.ShowViewParameters.Context = TemplateContext.View;
                            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                            e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                        }
                    }
                    else
                    {
                        MessageOptions options = new MessageOptions();
                        options.Duration = 3000;
                        options.Message = string.Format("Apparato non Corretto!! non è possibile eseguire l'operazione");
                        options.Web.Position = InformationPosition.Top;
                        options.Type = InformationType.Success;
                        options.Win.Caption = "Avviso";
                        //options.CancelDelegate = CancelDelegate;
                        //options.OkDelegate = OkDelegate;
                        Application.ShowViewStrategy.ShowMessage(options);
                    }

                }
            }

        }


        void LViewApparatoLVController_CustomDetailView(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //Application.IsDelayedDetailViewDataLoadingEnabled = true;
                e.Handled = true;
                DetailView NewDv;

                ApparatoListView alv = (ApparatoListView)View.CurrentObject;
                int OidApp = 0;

                if (int.TryParse(alv.Codice, out OidApp))
                {
                    Asset Apparato = xpObjectSpace.GetObjectByKey<Asset>(OidApp);
                    if (Apparato != null)
                    {
                        DetailView dview = Application.CreateDetailView(xpObjectSpace, "Apparato_DetailView", true, Apparato);
                        //dview.ViewEditMode = ViewEditMode.Edit;
                        dview.ViewEditMode = ViewEditMode.View;
                        e.InnerArgs.ShowViewParameters.CreatedView = dview;
                        e.InnerArgs.ShowViewParameters.Context = TemplateContext.View;
                        e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.InnerArgs.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                        //e.ShowViewParameters.CreatedView = dview;
                        ////view.Caption = view.Caption + " - Inserimento Documenti";
                        //e.ShowViewParameters.Context = TemplateContext.View;
                        //e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }
                }
                else
                {
                    MessageOptions options = new MessageOptions();
                    options.Duration = 3000;
                    options.Message = string.Format("Apparato non Corretto!! non è possibile eseguire l'operazione");
                    options.Web.Position = InformationPosition.Top;
                    options.Type = InformationType.Success;
                    options.Win.Caption = "Avviso";
                    //options.CancelDelegate = CancelDelegate;
                    //options.OkDelegate = OkDelegate;
                    Application.ShowViewStrategy.ShowMessage(options);
                }              
            }
        }
    
    }
}
