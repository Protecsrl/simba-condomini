using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;

namespace CAMS.Module.Controllers.DBAux
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FilteringCriterionController : ViewController
    {
        public FilteringCriterionController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            // Perform various tasks depending on the target View.
            //if (!CAMS.Module.Classi.SetVarSessione.IsAdminRuolo)
            //{
            //    CriteriaOperator op = CriteriaOperator.Parse(
            //                   string.Format("SecurityUser.UserName == '{0}'", Application.Security.UserName));

            //    ((ListView)View).CollectionSource.BeginUpdateCriteria();
            //    ((ListView)View).CollectionSource.Criteria.Clear();
            //    ((ListView)View).CollectionSource.Criteria["Filtro_FilytriPersonalizzati"] = op;
            //    ((ListView)View).CollectionSource.EndUpdateCriteria();
            //}

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
    }
}
