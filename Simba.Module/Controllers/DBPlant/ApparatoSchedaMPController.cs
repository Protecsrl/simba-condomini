using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using CAMS.Module.DBALibrary;
using System.Text;
using DevExpress.ExpressApp.SystemModule;

namespace CAMS.Module.Controllers.DBPlant
{
    public partial class ApparatoSchedaMPController : ViewController
    {
        public ApparatoSchedaMPController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            if (Frame != null && Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>() != null)
            {
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
            }
            this.pupWinInsetSchedeMP.Active.SetItemValue("Active", true);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void pupWinInsetSchedeMP_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {

            StringBuilder Messaggio = new StringBuilder("", 32000);
            if (View.ObjectSpace != null)
            {
                if (View is ListView)
                {
                    ListView LvFiglio = (ListView)View;
                    if (LvFiglio.Id == "Apparato_AppSchedaMpes_ListView")
                    {
                        DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id.Contains("Apparato_DetailView"))// && dvParent.ViewEditMode == ViewEditMode.Edit)ApparatoSchedaMP
                        {
                            try
                            {
                                Asset vApparato = (Asset)dvParent.CurrentObject;
                                List<SchedaMp> List = e.PopupWindow.View.SelectedObjects.Cast<SchedaMp>().ToList<SchedaMp>();
                                //foreach (SchedaMp obj in e.PopupWindow.View.SelectedObjects)       //{
                                vApparato.InsertSchedeMPsuAsset(ref vApparato, null, 0, List);
                                vApparato.Save();
                                View.ObjectSpace.CommitChanges();
                                //}     //LogTrace(string.Format("The 'PopupWindowShowAction' is executed with {0} parameter(s) for the '{1}' object. {2}", e.PopupWindow.View.SelectedObjects.Count, currentObject.Name, string.IsNullOrEmpty(parameters) ? "" : "\r\n\t\t" + parameters));
                            }
                            catch (Exception ex)
                            {
                                Messaggio.AppendLine(ex.Message);

                                MessageOptions options = new MessageOptions();
                                options.Duration = 3000;
                                options.Message = Messaggio.ToString();
                                options.Web.Position = InformationPosition.Top;
                                options.Type = InformationType.Success;
                                options.Win.Caption = "Caption";
                                //options.CancelDelegate = CancelDelegate;
                                //options.OkDelegate = OkDelegate;
                                Application.ShowViewStrategy.ShowMessage(options);
                            }
                        }
                    }
                }
            }
        }

        private void pupWinInsetSchedeMP_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            //            +		View.ObjectSpace.Owner	{DetailView, ID:Apparato_DetailView}	object {DevExpress.ExpressApp.DetailView}
            //+		View	{ListView, ID:Apparato_AppSchedaMpes_ListView}	DevExpress.ExpressApp.View {DevExpress.ExpressApp.ListView}
            //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            StringBuilder Messaggio = new StringBuilder("", 32000);
            if (View.ObjectSpace != null)
            {
                if (View is ListView)
                {
                    ListView LvFiglio = (ListView)View;
                    if (LvFiglio.Id == "Apparato_AppSchedaMpes_ListView")
                    {
                        DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id.Contains("Apparato_DetailView"))// && dvParent.ViewEditMode == ViewEditMode.Edit)ApparatoSchedaMP
                        {
                            try
                            {    //DateTime[] dates = new DateTime[] { new DateTime(2010, 10, 10), DateTime.Today };
                                //// Wrong. An exception is raised.             //XPCollection<Order> col1 = new XPCollection<Order>(session1,
                                //    CriteriaOperator.Parse("[OrderDate] in (?)", dates));
                                //// Right   //XPCollection<Order> col2 = new XPCollection<Order>(session1,       //    new InOperator("OrderDate", dates));

                                int vStdApparatoOid = ((Asset)dvParent.CurrentObject).StdAsset.Oid;
                                // Application.FindListViewId(typeof(ClusterEdificiInseribili));xpObjectSpace
                                CollectionSource LstSchedaMpSelezionabili = (CollectionSource)Application.CreateCollectionSource(
                                                                            View.ObjectSpace, typeof(SchedaMp), "SchedaMp_LookupListView");
                                LstSchedaMpSelezionabili.Criteria["Filtro_StdApparato"] = CriteriaOperator.Parse("StdApparato.Oid = " + vStdApparatoOid);

                                int[] arOidSchedeMP = ((Asset)dvParent.CurrentObject).AppSchedaMpes.Select(s => s.SchedaMp.Oid).ToArray();
                                LstSchedaMpSelezionabili.Criteria["Filtro_GiaPresenti"] = new InOperator("Oid", arOidSchedeMP).Not();

                                if (LstSchedaMpSelezionabili.GetCount() == 0)
                                {
                                    Messaggio.AppendLine(string.Format("Sono già presenti tutte le Attività di manutenzione Associabili Apparato"));
                                    MessageOptions options = new MessageOptions();
                                    options.Duration = 3000;
                                    options.Message = Messaggio.ToString();
                                    options.Web.Position = InformationPosition.Top;
                                    options.Type = InformationType.Success;
                                    options.Win.Caption = "Caption";
                                    //options.CancelDelegate = CancelDelegate;                                    //options.OkDelegate = OkDelegate;
                                    Application.ShowViewStrategy.ShowMessage(options);
                                }
                                var view = Application.CreateListView("SchedaMp_LookupListView", LstSchedaMpSelezionabili, false);
                                var dc = Application.CreateController<DialogController>();
                                e.DialogController.SaveOnAccept = false;
                                e.View = view;
                            }
                            catch
                            {
                                Messaggio.AppendLine(string.Format("Standard Apparato non Impostato!! non è possibile eseguire l'operazione"));

                                MessageOptions options = new MessageOptions();
                                options.Duration = 3000;
                                options.Message = Messaggio.ToString();
                                options.Web.Position = InformationPosition.Top;
                                options.Type = InformationType.Success;
                                options.Win.Caption = "Caption";
                                //options.CancelDelegate = CancelDelegate;
                                //options.OkDelegate = OkDelegate;
                                Application.ShowViewStrategy.ShowMessage(options);
                            }
                            //xpObjectSpace.CommitChanges();
                            //if (Messaggio.Length.Equals(0))


                        }
                    }
                }
            }


        }
    }
}
