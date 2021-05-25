using CAMS.Module.DBPlant;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System.Diagnostics;

namespace CAMS.Module.Controllers.DBPlant
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ProvinciaController : ViewController
    {
        public ProvinciaController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //if (View is ListView)
            //{                
            //}
            if (View is DetailView)
            {
                if (View.Id == "Provincia_DetailView")
                {
                    DetailView dv = (DetailView) View;
                    DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;

                      if (dvParent.Id == "Indirizzo_DetailView" && dvParent.ViewEditMode == ViewEditMode.Edit)
                      {
                          Indirizzo Ind = (Indirizzo )dvParent.CurrentObject;
                          Provincia Pr = (Provincia)dv.CurrentObject;
                          Pr.Regione = Ind.Regione;
                      }

                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        Debug.WriteLine(" prova ");
                    }
                }
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
    }
}
