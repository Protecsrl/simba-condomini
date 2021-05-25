using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using System.ComponentModel;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLMultiRisorseTeamController : ViewController
    {
        public RdLMultiRisorseTeamController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        private void acDelRisorsaTeamAltro_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdLMultiRisorseTeam rdl = (RdLMultiRisorseTeam)dv.CurrentObject;
                rdl.SetMemberValue("RisorseTeam", null);
            }
        }

        private void pWinRisorseTeamAltre_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
               IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView && e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    DetailView dv = (DetailView)View;
                    RdLMultiRisorseTeam rdlMultiRisorseTeam = (RdLMultiRisorseTeam)dv.CurrentObject;

                    int OidObjCurr = ((DCRisorseTeamRdL)(((e.PopupWindow)).View).SelectedObjects[0]).OidRisorsaTeam;
                    RisorseTeam RT = xpObjectSpace.GetObjectByKey<RisorseTeam>(OidObjCurr);
                    rdlMultiRisorseTeam.SetMemberValue("RisorseTeam", RT);
                }
                else
                {
                    MessageOptions options = new MessageOptions() { Duration = 3000, Message = "Nessun Oggetto Selezionato" };
                    options.Web.Position = InformationPosition.Top;
                    options.Type = InformationType.Success;
                    options.Win.Caption = "Avvertenza";
                    //options.CancelDelegate = CancelDelegate;
                    //options.OkDelegate = OkDelegate;
                    Application.ShowViewStrategy.ShowMessage(options);
                }  
            } 
        }

        private void pWinRisorseTeamAltre_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                    objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                    CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                                "DCRisorseTeamRdL_LookupListView");
                    //  filtro 
                    ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);
                    var dc = Application.CreateController<DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = lvk;
                }                 
            }
        }

        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (View is DetailView)
            {
                BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
                DetailView dv = (DetailView)View;
                RdLMultiRisorseTeam _RdLMultiRisorseTeam = (RdLMultiRisorseTeam)dv.CurrentObject;
                if (_RdLMultiRisorseTeam.RdL != null)
                {
                    RdL rdl = _RdLMultiRisorseTeam.RdL;
                    using (DB db = new DB())
                    {
                        int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                        objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser,0);
                    }
                }

                //e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r =>
                //    r.Ordinamento)
                //    .ThenBy(r => r.NumerorAttivitaTotaliTT)
                //    .ThenBy(r => r.Distanzakm)
                //    .ThenBy(r => r.NumerorAttivitaTotaliMP).ToList());
                e.Objects = objects;
            }
        }
    }
}
