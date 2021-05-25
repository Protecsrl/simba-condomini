using DevExpress.ExpressApp;

namespace CAMS.Module.Controllers.DBKPI
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class KPIAvanzamentoLavoriController : ViewController
    {
        public KPIAvanzamentoLavoriController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View. 
            //SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Conduttore", DevExpress.Xpo.DB.SortingDirection.Ascending);

            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("AreaDiPaolo", SortingDirection.Ascending));
            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("CodRegRdL", SortingDirection.Ascending));
            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("DataOraSmistamento", SortingDirection.Ascending));
            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("DataOraOperativo", SortingDirection.Ascending));
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("AreaDiPaolo", SortingDirection.Ascending));
            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("CodRegRdL", SortingDirection.Ascending));
            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("DataOraSmistamento", SortingDirection.Ascending));
            //((ListView)View).CollectionSource.Sorting.Add(new SortProperty("DataOraOperativo", SortingDirection.Ascending));

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
