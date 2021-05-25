using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System.Diagnostics;

namespace CAMS.Module.Controllers.DBControlliNormativi
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class DestinatariControlliNormativiController : ViewController
    {
        public DestinatariControlliNormativiController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Debug.WriteLine(View.Id);
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            Debug.WriteLine(View.Id);

            if (View.Id == "Destinatari_DestinatariControlliNormativis_ListView")
            {
                ListView lv = (ListView)View;
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id == "Destinatari_DetailView")
                {
                    if (dvParent.ViewEditMode == ViewEditMode.Edit)
                    {
                        if (dvParent.FindItem("DestinatariControlliNormativis") != null)
                        {
                           // ViewItem pe = dvParent.FindItem("DestinatariControlliNormativis");

                           // lv.CollectionSource.Criteria["FiltroPAM_PMP"] = CriteriaOperator.Parse("Edificio In (2)");
                        }
                    }

                }
            }

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
