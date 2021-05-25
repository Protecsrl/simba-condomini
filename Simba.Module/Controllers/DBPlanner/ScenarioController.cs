using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using System.Linq;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace CAMS.Module.Controllers.DBPlanner
{
    // For more typical usage scenarios, be sure to check 
    // out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ScenarioController : ViewController
    {
        public ScenarioController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            AzioneStatoScenario.Active.SetItemValue("Active", false);
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    AzioneStatoScenario.Active.SetItemValue("Active", true);
                }

            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            AzioneStatoScenario.Active.SetItemValue("Active", false);
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    AzioneStatoScenario.Active.SetItemValue("Active", true);
                }

            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void popupScenarioClusterEdifici_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                    {
                        if (View.Id == "Scenario_DetailView")
                        {
                            Scenario vScenario = (Scenario)View.CurrentObject;

                            var listViewId = "ClusterEdificiInseribili_ListView_Pianifica";// Application.FindListViewId(typeof(ClusterEdificiInseribili));


                            CollectionSource StdApparatiSelezionabili = (CollectionSource)Application.CreateCollectionSource(
                                                          xpObjectSpace, typeof(ClusterEdificiInseribili), listViewId);

                            StdApparatiSelezionabili.Criteria["pippo"] = CriteriaOperator.Parse("Scenario.Oid = " + vScenario.Oid);



                            var view = Application.CreateListView(listViewId, StdApparatiSelezionabili, false);
                            var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                            //dc.Actions.Add(
                            //  e.ShowViewParameters.Controllers.Add(dc);
                            e.DialogController.SaveOnAccept = false;
                            //  e.DialogController.
                            e.View = view;
                            //   e.IsSizeable = true;

                        }
                    }
                }
            }

            //var objMPPBLListSelects = new ArrayList(e.SelectedObjects);
            //var listViewId = Application.FindListViewId(typeof(Risorse));

            //var xpObjectSpace = Application.CreateObjectSpace();
            //if (xpObjectSpace != null)
            //{
            //    var ImpiantoSelezionato = (Impianto)View.CurrentObject;

            //    var lstSchedeMP = new XPCollection<SchedaMp>(ImpiantoSelezionato.Session);
            //    var lstDaEscludere = ImpiantoSelezionato.APPARATOes.Select(ld => ld.StdApparato);

            //    var tmpLst = lstSchedeMP.Where(smp => smp.Sistema == ImpiantoSelezionato.Sistema)
            //        .Select(smp => smp.Eqstd).Distinct().Where(std => !lstDaEscludere.Contains(std)).ToList();
            //    var crtapp = string.Empty;
            //    foreach (StdApparato std in tmpLst)
            //    {
            //        crtapp += std.Oid + ",";
            //    }

            //    var crTApparati = string.Format("Oid in ({0})", crtapp.Substring(0, crtapp.Length - 1));

            //    var StdApparatiSelezionabili = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(StdApparato), listViewId);
            //    StdApparatiSelezionabili.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(crTApparati);
            //    var view = Application.CreateListView(listViewId, StdApparatiSelezionabili, false);
            //    e.View = view;
            //    e.IsSizeable = true;
            //}
        }

        private void popupScenarioClusterEdifici_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void AzioneStatoScenario_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    //if (View.Id == "Scenario_DetailView")
                    //{
                    Scenario vScenario = (Scenario)View.CurrentObject;
                    if (vScenario.StatoScenario == StatoScenarioClusterEdificio.Bloccato)
                    {
                        vScenario.StatoScenario = StatoScenarioClusterEdificio.Aperto;
                    }
                    else
                    {
                        vScenario.StatoScenario = StatoScenarioClusterEdificio.Bloccato;
                    }
                    vScenario.Save();
                    View.ObjectSpace.CommitChanges();
                    //}
                }
            }

        }

        private void acAggiornaTempi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //Session session = ((XPObjectSpace)ObjectSpace).Session;


            if (xpObjectSpace != null)
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    Scenario CurScenario = (Scenario)dv.CurrentObject;
                    int oidScenario = CurScenario.Oid;
                    foreach (ClusterEdifici ced in CurScenario.ClusterEdificis)
                    {
                        foreach (Immobile Edifici in ced.Edificis)
                        {
                            using (DB db = new DB())
                            {
                                db.AggiornaTempi(Edifici.Oid, "IMMOBILE");
                            }
                        }
                    }

                    View.ObjectSpace.Refresh();
                    int numCluster = CurScenario.ClusterEdificis.Count;
                    int numEdifici = CurScenario.ClusterEdificis.Sum(s=> s.Edificis.Count);
                    int numImpianti = CurScenario.ClusterEdificis.Sum(s => s.Edificis.Sum(ss=> ss.NumImp));
                    int numApparati = CurScenario.ClusterEdificis.Sum(s => s.Edificis.Sum(ss => ss.NumApp));
                   
                   CurScenario= xpObjectSpace.GetObjectByKey<Scenario>(oidScenario);
                    CurScenario.Consistenza = string.Format("Cluster {0}, Edifici {1}, Impianti {2}, Apparati {3}", numCluster, numEdifici, numImpianti, numApparati);
                    CurScenario.Save();
                    xpObjectSpace.CommitChanges();
                    View.Refresh();
                }
        }
    }
}
