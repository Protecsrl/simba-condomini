using CAMS.Module.DBTask;
using CAMS.Module.DBTask.Vista;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;


namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListSimiliViewController : ViewController
    {
        public RdLListSimiliViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<DevExpress.ExpressApp.SystemModule.ListViewProcessCurrentObjectController>()
                .CustomProcessSelectedItem += LViewController_CustomDetailView;    
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
      
        }
        protected override void OnDeactivated()
        {
            try
            {
                Frame.GetController<DevExpress.ExpressApp.SystemModule.ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
            }
            catch { }
            base.OnDeactivated();
        }

        private void acEditRdLbySimili_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
           // RdLListSimiliView record = (RdLListSimiliView)e.CurrentObject;
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                DetailView NewDv;
                RdLListSimiliView GetRdL_ListView = xpObjectSpace.GetObject<RdLListSimiliView>((RdLListSimiliView)View.CurrentObject);
                // NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
                RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetRdL_ListView.Codice);
                NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.Edit;
                    e.ShowViewParameters.CreatedView = NewDv;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }
        }

        void LViewController_CustomDetailView(object sender, DevExpress.ExpressApp.SystemModule.CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                e.Handled = true;
                DetailView NewDv;
                RdLListSimiliView GetRdL_ListView = xpObjectSpace.GetObject<RdLListSimiliView>((RdLListSimiliView)View.CurrentObject);  
                RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetRdL_ListView.Codice);
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
