using CAMS.Module.DBDocument;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;

namespace CAMS.Module.Controllers.DBDocument
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class PlanimetrieController : ViewController
    {
        public PlanimetrieController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            if (View is DetailView)
            {
                DetailView Dv = View as DetailView;
                if (Dv.Id == "Planimetrie_DetailView")
                {
                    if (Dv.ViewEditMode == ViewEditMode.Edit)
                    {
                        Planimetrie pl = Dv.CurrentObject as Planimetrie;
                        object obgTemp2 = pl.GetMemberValue("FileDWFFullName");
                        if (obgTemp2 != null)
                        {
                            string sFileDWFFullName = obgTemp2.ToString();
                            if (!string.IsNullOrEmpty(sFileDWFFullName))
                            {
                                DevExpress.Persistent.BaseImpl.FileData fd = pl.GetFilePiano(sFileDWFFullName);
                                pl.SetMemberValue("FileDWF", fd);
                                pl.SetMemberValue("IsVisibleFileEdit", true);
                            }
                        }
                        else
                        {
                            pl.SetMemberValue("IsVisibleFileEdit", true);
                        }
                        //----------------------
                    }
                    if (((DevExpress.Xpo.XPObject)(Dv.CurrentObject)).Oid == -1)
                    {
                        Planimetrie pl = Dv.CurrentObject as Planimetrie;
                        pl.SetMemberValue("IsVisibleFileEdit", true);
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
