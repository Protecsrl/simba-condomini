using CAMS.Module.DBTask.Guasti;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using System.Diagnostics;
using System.Linq;

namespace CAMS.Module.Controllers.DBTask.Guasti
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ApparatoProblemaController : ViewController
    {
        public ApparatoProblemaController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
               //if (View is DetailView)
               //  if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
               //      if (View.Id == "ApparatoProblema_DetailView")
               //      {
               //          ApparatoProblema ApparatoProblemaCorrente = (ApparatoProblema)View.CurrentObject;
               //          if(ApparatoProblemaCorrente.Oid==-1)
               //          {
               //              if (ApparatoProblemaCorrente.ProblemaCausas.Count() > 0)
               //              { 
                             
               //              }
               //          }
               //      }


        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            //if (View is DetailView)
            //    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
            //        if (View.Id == "ApparatoProblema_DetailView")
            //        {
            //            ApparatoProblema ApparatoProblemaCorrente = (ApparatoProblema)View.CurrentObject;
            //            if (ApparatoProblemaCorrente.Oid == -1)
            //            {
            //                if (ApparatoProblemaCorrente.ProblemaCausas.Count() > 0)
            //                {
            //                    Debug.WriteLine("spegni nuovo");
            //                }
            //            }
            //        }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
