using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.Vista;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.SystemModule;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListViewClienteIntercenterController : ViewController
    {
        public RdLListViewClienteIntercenterController()
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
            base.OnDeactivated();
        }
        void LViewController_CustomDetailView(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                string detailview = "RdL_DetailView_Cliente_Intercenter";
                //using (UtenteRuolo UR = new UtenteRuolo())
                //{
                //    if (UR.GetIsTipoRuolo((SecuritySystemUser)Application.Security.User, "CLIENTE_INTERCENTER"))
                //        detailview = "RdL_DetailView_Cliente_Intercenter";
                //}

                e.Handled = true;
                DetailView NewDv;
               // RdLListViewClienteIntercenter RdLClienteIntercenter = (RdLListViewClienteIntercenter)View.CurrentObject;
                string STATO = RdLClienteIntercenter.StatoSmistamento;
                //RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(RdLClienteIntercenter.Codice);
                GetRdL.StatoSmistamentoCliente = STATO;
                NewDv = Application.CreateDetailView(xpObjectSpace, detailview, true, GetRdL);// NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Cliente", true, GetRdL);
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
