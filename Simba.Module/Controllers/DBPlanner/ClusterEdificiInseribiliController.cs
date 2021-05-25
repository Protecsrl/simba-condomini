using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;

namespace CAMS.Module.Controllers.DBPlanner
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ClusterEdificiInseribiliController : ViewController
    {
        public ClusterEdificiInseribiliController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            var listViewId = "ClusterEdificiInseribili_ListView_Pianifica";
            if (View is ListView)
            {
 
            }


        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void SetClusterImpianti_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

        }
    }
}
