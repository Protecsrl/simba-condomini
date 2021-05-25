
using CAMS.Module.DBTask.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListViewSGController : ViewController
    {
        //private SimpleAction EditRdLLVSGAction;
        public RdLListViewSGController()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListViewSG);
            //
            //EditRdLLVSGAction = new SimpleAction(this, "Modifica RdL", PredefinedCategory.Edit);
            //EditRdLLVSGAction.ToolTip = "Modifica la Richiesta di Lavoro Selezionata";
            //EditRdLLVSGAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            //EditRdLLVSGAction.ImageName = "MenuBar_Edit";// "BO_Contact";
            //EditRdLLVSGAction.Execute += EditRdLLVSGAction_Execute;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem += LViewController_CustomDetailView;

            //if (View.Id.Contains("ImpiantoLibrary_ImpiantoLibraryAppInseribilis_ListView"))
            //{
            //    for (var i = 2; i < 10; i++)
            //    {
            if (View is ListView)
            {
                var lv = View as ListView;
                if (lv.Id == "RdLListViewSG_ListView")
                {
                    scaReportSG.Items.Add((new ChoiceActionItem() { Id = "1", Caption = "Report MP", Data = "[DisplayName] = 'Interventi di Manutenzione MP'" }));
                    scaReportSG.Items.Add((new ChoiceActionItem() { Id = "2", Caption = "Report TT", Data = "[DisplayName] = 'Interventi di Manutenzione'" }));
                }


            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.                                     "Interventi di Manutenzione MP"
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            //try
            //{
            //    Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
            //}
            //catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        //void LViewController_CustomDetailView(object sender, CustomProcessListViewSelectedItemEventArgs e)
        //{
        //    IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
        //    if (xpObjectSpace != null)
        //    {
        //        e.Handled = true;
        //        DetailView NewDv;
        //        RdLListViewSG GetRdL_ListView = xpObjectSpace.GetObject<RdLListViewSG>((RdLListViewSG)View.CurrentObject);
        //        // NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
        //        RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetRdL_ListView.Codice);
        //        NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);
        //        if (NewDv != null)
        //        {
        //            NewDv.ViewEditMode = ViewEditMode.View;
        //            e.InnerArgs.ShowViewParameters.CreatedView = NewDv;
        //            e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.Default;
        //        }
        //    }
        //}



        //void EditRdLLVSGAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{

        //    IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
        //    if (xpObjectSpace != null)
        //    {
        //        //e.Handled = true;
        //        DetailView NewDv;
        //        RdLListViewSG GetRdL_ListView = xpObjectSpace.GetObject<RdLListViewSG>((RdLListViewSG)View.CurrentObject);
        //        // NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
        //        RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetRdL_ListView.Codice);
        //        NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);
        //        if (NewDv != null)
        //        {
        //            NewDv.ViewEditMode = ViewEditMode.Edit;
        //            e.ShowViewParameters.CreatedView = NewDv;
        //            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
        //        }
        //    }
        //}

        private void scaReportSG_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            string FiltroReport = e.SelectedChoiceActionItem.Data.ToString();
            string idReport = e.SelectedChoiceActionItem.Id.ToString();
            string TitoloReport = e.SelectedChoiceActionItem.Caption.ToString();
            string handle = "";
            if (xpObjectSpace != null)
            {
                string CommessaSG = string.Empty;
                string CriterioAdd = string.Empty;
                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);

                if (View is ListView)
                {
                    List<RdLListViewSG> GetSelectedObj = (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListViewSG>().ToList();
                    int ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;

                    if (idReport == "1")
                    {
                        GetSelectedObj = (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListViewSG>().Where(w => w.OidCategoria == 1).ToList();
                        ObjCount = (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListViewSG>()
                                   .Where(w => w.OidCategoria == 1).Select(s => s.Codice).Count();
                    }
                    else
                    {
                        GetSelectedObj = (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListViewSG>().Where(w => w.OidCategoria != 1).ToList();
                        ObjCount = (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListViewSG>()
                               .Where(w => w.OidCategoria != 1).Select(s => s.Codice).Count();
                    }

                    if (ObjCount > 0)
                    {
                        //ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;
                        if (ObjCount < 1000)
                        {
                            var ArObjSel = GetSelectedObj.Select(s => s.Codice).Distinct().ToArray<int>();
                            string sOids = String.Join(",", ArObjSel);
                            CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", ArObjSel));
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                        }
                        else
                        {
                            var ArObjSel = GetSelectedObj.Select(s => s.Codice).Distinct().ToList<int>();
                            int numita = ObjCount / 1000;
                            for (var i = 1; i < numita + 1; i++)
                            {
                                int maxind = ArObjSel.Count;
                                int limitemin = (i * 1000) - 1000;
                                int limitemax = i * 1000;
                                if (limitemax > maxind)
                                    limitemax = maxind;
                                var output = ArObjSel.GetRange(limitemin, limitemax).ToArray();
                                string sOids = String.Join(",", output);
                                CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", output));
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                            }
                        }

                        IObjectSpace objectSpace = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.
                                                                   CreateObjectSpace(typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));
                        DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                            objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(CriteriaOperator.Parse(FiltroReport)); //'Contacts Report'"[DisplayName] = 'Interventi di Manutenzione'"
                        handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);

                        Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle, criteriaOP2);

                    }
                    else
                    {
                        string Msg = string.Format("Nessun Record Selezionato di tipo: {0}", TitoloReport);
                        throw new UserFriendlyException(Msg);
                    }
                }


            }

        }

    }
}
