using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask.DC;
using System.ComponentModel;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using CAMS.Module.DBAudit.DC;
using CAMS.Module.DBTask.Vista;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBAux;
using DevExpress.ExpressApp.Security;

namespace CAMS.Module.Controllers.DBTask
{

    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLNonPersistentController : ViewController
    {
        private IObjectSpace additionalObjectSpace;
        private  bool VisualizzaDCTeamRisorse = false;
        public RdLNonPersistentController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
            VisualizzaDCTeamRisorse = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                  typeof(RdL), SecurityOperations.Write, View.CurrentObject, "RisorseTeam")); //selectedObject

            pupWinRisorseTeamDC.Active.SetItemValue("Active", VisualizzaDCTeamRisorse);
        }


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    pupWinRisorseTeam.Active.SetItemValue("Active", VisualizzaDCTeamRisorse);
                    acDelRisorsaTeam.Active.SetItemValue("Active", VisualizzaDCTeamRisorse);
                }
                else
                {
                    pupWinRisorseTeam.Active.SetItemValue("Active", false);
                    acDelRisorsaTeam.Active.SetItemValue("Active", false);
                }
            }
        }
        protected override void OnDeactivated()
        {
            VisualizzaDCTeamRisorse = false;
            try
            {
                Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void acDelRisorsaTeam_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                rdl.SetMemberValue("RisorseTeam", null);
            }
        }

        private void pupWinRisorseTeamDC_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {

                if (View is DetailView && e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    int OidObjCurr = ((DCRisorseTeamRdL)(((e.PopupWindow)).View).SelectedObjects[0]).OidRisorsaTeam;
                    RisorseTeam RT = xpObjectSpace.GetObjectByKey<RisorseTeam>(OidObjCurr);
                    rdl.SetMemberValue("RisorseTeam", RT);

                    View.Refresh();
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

        private void pupWinRisorseTeamDC_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                            "DCRisorseTeamRdL_LookupListView");
                //  filtro
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                var ParCriteria = string.Empty;

                ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);
                //-----------  filtro
                //var view = Application.CreateListView(listViewId, ListRisorseTeamLookUp, false);

                //        // ListRisorseTeamLookUp.Collection
                if (!string.IsNullOrEmpty(rdl.RicercaRisorseTeam))
                { //   Azienda Mansione  Telefono

                    string Filtro1 = string.Empty;
                    string Filtro2 = string.Empty;
                    string Filtro3 = string.Empty;
                    string Filtro4 = string.Empty;
                    string AllFilter = string.Empty;

                    if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                    {
                         Filtro1 = string.Format("Contains([RisorsaCapo],'{0}')", rdl.RicercaRisorseTeam.ToString());
                         Filtro2 = string.Format("Contains([Azienda],'{0}')", rdl.RicercaRisorseTeam.ToString());
                         Filtro3 = string.Format("Contains([Mansione],'{0}')", rdl.RicercaRisorseTeam.ToString());
                         Filtro4 = string.Format("Contains([CentroOperativo],'{0}')", rdl.RicercaRisorseTeam.ToString());
                       
                    }
                    else
                    {
                         Filtro1 = string.Format("Contains(Upper([RisorsaCapo]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
                         Filtro2 = string.Format("Contains(Upper([Azienda]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
                         Filtro3 = string.Format("Contains(Upper([Mansione]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
                         Filtro4 = string.Format("Contains(Upper([CentroOperativo]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
                    
                    }
                    AllFilter = string.Format("{0} Or {1} Or {2} Or {3}", Filtro1, Filtro2, Filtro3, Filtro4);
                    ((ListView)lvk).Model.Filter = AllFilter;
                }

                var dc = Application.CreateController<DialogController>();
                e.DialogController.SaveOnAccept = false;
                e.View = lvk;
                e.Maximized = true;

                //objectSpace.ObjectsGetting -= DCRisorseTeamRdL_objectSpace_ObjectsGetting;
            }
        }
      
        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (View is DetailView)
            {
                BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                if (rdl.Immobile != null)
                {
                    int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
                    using (DB db = new DB())
                    {
                        int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                        objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRA);
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

        private void acStoricoRdL_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.
                    CreateObjectSpace(typeof(DCRegistroSmistamentoDettaglio));
                objectSpace.ObjectsGetting += DCStoricoRdL_objectSpace_ObjectsGetting;

                CollectionSource DCRegistroSmistamentoDettaglio_Lookup = (CollectionSource)
                    Application.CreateCollectionSource(objectSpace, typeof(DCRegistroSmistamentoDettaglio),
                                                                            "DCRegistroSmistamentoDettaglio_ListView");
                //  filtro
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                var ParCriteria = string.Empty;

                ListView viewlvk = Application.CreateListView("DCRegistroSmistamentoDettaglio_ListView",
                    DCRegistroSmistamentoDettaglio_Lookup, true);

                e.ShowViewParameters.CreatedView = viewlvk;

                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;  //TargetWindow.Default;
                //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                DialogController dc = Application.CreateController<DialogController>();
                //dc.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
                e.ShowViewParameters.Controllers.Add(dc);

            }
        }

        void DCStoricoRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (View is DetailView)
            {
                BindingList<DCRegistroSmistamentoDettaglio> objects = new BindingList<DCRegistroSmistamentoDettaglio>();
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                if (rdl != null)
                {
                    using (DB db = new DB())
                    {
                        objects = db.GetStorico_RdL(rdl.Oid, 25, DateTime.Now.AddDays(30), CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                    }
                }
                e.Objects = objects;
            }
        }

        private void acDCReport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            //private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
            //{
            //        IObjectSpace objectSpace =
            //DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));
            //        DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData = objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
            //            CriteriaOperator.Parse("[DisplayName] = 'Report Attività Manutenzione'")); //'Contacts Report'

            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                // hendol del report
                // filtro da imposare nel report   
                string CommessaSG = string.Empty;
                string CriterioAdd = string.Empty;
                int CommessaOID = 0;
                int CategoriaOID = 0;
                Type ObjectType = typeof(CAMS.Module.DBTask.RdL); ;

                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = xpObjectSpace.GetObject<RdL>((RdL)dv.CurrentObject);
                    try
                    {
                        if (rdl.Asset.Servizio.Immobile.Contratti != null)
                        {
                            CommessaSG = rdl.Immobile.Contratti.WBS.ToString();
                            CommessaOID = rdl.Asset.Servizio.Immobile.Contratti.Oid;
                            CategoriaOID = rdl.Categoria.Oid;
                        }
                    }
                    catch
                    { }
                    //CriterioAdd = string.Format("[Codice] = {0}", rdl.RegistroRdL.Oid);


                    //var OidReport =   Sess.Query<RdL>().Where(w => w.Richiedente != null).Select(s=> s.
                    ///  @@@@@@@@@@@@@@@@@@@   ma modificare ovunque @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    CriterioAdd = string.Format("[codiceRdL] = {0}", rdl.Oid);
                    //CriterioAdd = string.Format("[CodRegRdL] = {0}", rdl.RegistroRdL.Oid);



                    criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));

                }
                //if (View is ListView)    prova adesso
                //    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                //    {
                //        int ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;
                //        if (ObjCount < 1000)
                //        {
                //            var ArObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>()
                //                           .ToList<RdL>().Select(s => s.RegistroRdL.Oid).Distinct().ToArray<int>();
                //            string sOids = String.Join(",", ArObjSel);
                //            //CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", ArObjSel));
                //            CriterioAdd = string.Format("[CodRegRdL] In ({0})", String.Join(",", ArObjSel));
                //            criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                //        }
                //        else
                //        {
                //            var ArObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().Select(s => s.RegistroRdL.Oid).Distinct().ToList<int>();
                //            int numita = ObjCount / 1000;
                //            for (var i = 1; i < numita + 1; i++)
                //            {
                //                int maxind = ArObjSel.Count;
                //                int limitemin = (i * 1000) - 1000;
                //                int limitemax = i * 1000;
                //                if (limitemax > maxind)
                //                    limitemax = maxind;
                //                var output = ArObjSel.GetRange(limitemin, limitemax).ToArray();
                //                string sOids = String.Join(",", output);
                //                //CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", output));
                //                CriterioAdd = string.Format("[CodRegRdL] In ({0})", String.Join(",", output));
                //                criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                //            }
                //        }
                //    }

                string DisplayReportPersonale = "";
                try
                {

                    DisplayReportPersonale = Sess.Query<SetReportCommessa>()
                        .Where(w => w.Categoria.Oid == CategoriaOID && w.Commesse.Oid == CommessaOID && w.ObjectType == ObjectType)
                           .Select(s => s.DisplayReport)
                           .FirstOrDefault();
                }
                catch
                {
                    DisplayReportPersonale = "";
                }


                string CriterioReport = string.Empty;
                GroupOperator criteriaReport = new GroupOperator(GroupOperatorType.And);
                criteriaReport.Operands.Clear();

                string handle = "";

                IObjectSpace objectSpace =
                    DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(
                    typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));

                if (!string.IsNullOrEmpty(DisplayReportPersonale))
                {
                    CriterioReport = string.Format("[DisplayName] = '{0}'", DisplayReportPersonale);
                    criteriaReport.Operands.Add(CriteriaOperator.Parse(CriterioReport));

                    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                                objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(criteriaReport);
                    //   CriteriaOperator.Parse("[DisplayName] = 'Interventi di Manutenzione'"));  //   CriteriaOperator.Parse("[DisplayName] = 'Interventi di Manutenzione'"));  
                    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                    // apri report
                    Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle, criteriaOP2);
                }
                else
                {

                    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                       objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
                      CriteriaOperator.Parse("[DisplayName] = 'Report RdL di Manutenzione'"));
                    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                    // apri report
                    Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle);
                }


                //Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle, criteriaOP2);
            }




            //}

        }


        private void Application_ObjectSpaceCreated(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (View.Id.Contains("RdL_DetailView_Gestione") || View.Id.Contains("RdL_DetailView_Cliente"))
            {
                if (e.ObjectSpace is NonPersistentObjectSpace)
                {
                    ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += DCRdLListReport_objectSpace_ObjectsGetting;
                }
            }
        }


        void DCRdLListReport_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            string Rdlcodici = string.Empty;
            string RegRdlcodici = string.Empty;
            if (e.ObjectType.FullName == "CAMS.Module.DBTask.DC.DCRdLListReport")
            {
                if (View is DetailView)
                {
                    RdL cur = ((DetailView)View).CurrentObject as RdL;
                    //foreach (RdLListView cur in View.SelectedObjects)
                    //{
                    //Rdlcodici = cur.Oid.ToString() + ",";
                    RegRdlcodici = cur.RegistroRdL.Oid.ToString() + ",";
                    //}

                    BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
                    using (DB db = new DB())
                    {
                        //objects = db.GetReport_RdL(RegRdlcodici.ToString());
                        objects = db.GetREPORT_REGRDL(RegRdlcodici.ToString());
                    }
                    e.Objects = objects;
                }
            }
        }



    }
}




//private void pupWinRisorseTeamDC_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
//{
//    //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
//    // NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
//    if (View is DetailView)
//    {
//        //NonPersistentObjectSpace objectSpace = new NonPersistentObjectSpace(XafTypesInfo.Instance);
//        NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
//        objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;
//        //  e.View = Application.CreateListView(objectSpace, typeof(DCRisorseTeamRdL), true); /// DCRisorseTeamRdL_LookupListView

//        CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
//                                                                    "DCRisorseTeamRdL_LookupListView");
//        //  filtro
//        DetailView dv = (DetailView)View;
//        RdL rdl = (RdL)dv.CurrentObject;
//        var ParCriteria = string.Empty;
//        //SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Conduttore", DevExpress.Xpo.DB.SortingDirection.Ascending);
//        //DCRisorseTeamRdL_Lookup.Sorting.Add(srtProperty);
//        //srtProperty = new DevExpress.Xpo.SortProperty("Ordinamento", DevExpress.Xpo.DB.SortingDirection.Ascending);
//        //DCRisorseTeamRdL_Lookup.Sorting.Add(srtProperty);
//        ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);

//        //lvk.CollectionSource.Sorting.Add(new DevExpress.Xpo.SortProperty("Conduttore", DevExpress.Xpo.DB.SortingDirection.Ascending));
//        //lvk.CollectionSource.Sorting.Add(new DevExpress.Xpo.SortProperty("Ordinamento", DevExpress.Xpo.DB.SortingDirection.Ascending));

//        //if (!string.IsNullOrEmpty(rdl.RicercaRisorseTeam))
//        //{ //   Azienda Mansione  Telefono
//        //    ((ListView)lvk).Model.Filter = "";
//        //    string Filtro1 = string.Format("Contains(Upper([RisorsaCapo]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
//        //    string Filtro2 = string.Format("Contains(Upper([Azienda]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
//        //    string Filtro3 = string.Format("Contains(Upper([Mansione]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
//        //    string Filtro4 = string.Format("Contains(Upper([Telefono]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
//        //    string AllFilter = string.Format("{0} Or {1} Or {2} Or {3}", Filtro1, Filtro2, Filtro3, Filtro4);
//        //    ((ListView)lvk).Model.Filter = AllFilter;
//        //}
//        var dc = Application.CreateController<DialogController>();
//        e.DialogController.SaveOnAccept = false;
//        e.View = lvk;

//        //e.View = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);
//    }
//}