using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.ComponentModel;
using System.IO;
using System.Linq;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegRdLListViewController : ViewController
    {
        public RegRdLListViewController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            Frame.GetController<DevExpress.ExpressApp.SystemModule.ListViewProcessCurrentObjectController>().CustomProcessSelectedItem += LViewController_CustomDetailView;
            //Frame.GetController<ListViewProcessCurrentObjectController>().CustomizeShowViewParameters += LViewController_CustomizeShowViewParameters;
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            try
            {
                Frame.GetController<DevExpress.ExpressApp.SystemModule.ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomizeShowViewParameters -= LViewController_CustomizeShowViewParameters;
                Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void Application_ObjectSpaceCreated(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (View.Id.Contains("RegRdLListView_ListView"))
            {
                if (e.ObjectSpace is NonPersistentObjectSpace)
                {
                    ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += DCRdLListReport_objectSpace_ObjectsGetting;
                }
            }
        }


        void DCRdLListReport_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType.FullName == "CAMS.Module.DBTask.DC.DCRdLListReport")
            {
                if (View is ListView)
                {
                    //BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
                    if (objects != null)
                    {
                        if (objects.Count == 0)
                        {
                            using (DB db = new DB())
                            {
                                //objects = db.GetReport_RdL(Rdlcodici.ToString());
                                //objects = db.GetReport_RdL(RegRdLcodici.ToString());
                                objects = db.GetREPORT_REGRDL(RegRdLcodici.ToString());  
                            }
                            e.Objects = objects;
                        }
                        else
                        {
                            e.Objects = objects;
                        }
                    }
                    //using (DB db = new DB())
                    //{
                    //    objects = db.GetReport_RdL(Rdlcodici.ToString());
                    //}
                    //e.Objects = objects;
                }
            }
        }

        #region  selezione del registro
        void LViewController_CustomDetailView(object sender, DevExpress.ExpressApp.SystemModule.CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                e.Handled = true;
                DetailView NewDv;

                //var currentObject = View.ObjectSpace.GetObject(View.CurrentObject);
                var currentObjectKey = View.ObjectSpace.GetKeyValue(View.CurrentObject);
                //RegRdLListView GetRegRdL_ListView = xpObjectSpace.GetObject<RegRdLListView>((RegRdLListView)View.CurrentObject);
                RegistroRdL GetRegistroRdL = xpObjectSpace.GetObjectByKey<RegistroRdL>(currentObjectKey);
                NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.View;
                    e.InnerArgs.ShowViewParameters.CreatedView = NewDv;
                    e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }

            #region
            //     void LViewController_CustomDetailView(object sender, CustomProcessListViewSelectedItemEventArgs e) {
            //    IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //    if (xpObjectSpace != null)    {
            //        e.Handled = true;
            //        DetailView NewDv;
            //        RegistroRdL GetRegistroRdL = xpObjectSpace.GetObject<RegistroRdL>((RegistroRdL)View.CurrentObject);
            //        NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
            //        if (NewDv != null)
            //        {
            //            NewDv.ViewEditMode = ViewEditMode.Edit;
            //            e.InnerArgs.ShowViewParameters.CreatedView = NewDv;
            //            e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.Default;
            //        }  }}
            //LViewController_CustomDetailViewExtracted(xpObjectSpace);

            #endregion

        }

        //void LViewController_CustomizeShowViewParameters(object sender, CustomizeShowViewParametersEventArgs e)
        //{
        //    //e.ShowViewParameters.CreatedView = Application.ProcessShortcut(e.ShowViewParameters.CreatedView.CreateShortcut());
        //    //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.MdiChild;
        //    //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
        //}

        #endregion

        private static string Rdlcodici = string.Empty;
        private static string RegRdLcodici = string.Empty;
        private static BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();

        private void acReportRegRdLListView_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            // reset variabili report
            Rdlcodici = string.Empty;
            RegRdLcodici = string.Empty;
            objects.Clear();

            if (xpObjectSpace != null && View is ListView)
            {

                // filtro da imposare nel report         
                string CriterioAdd = string.Empty;
                string CommessaSG = string.Empty;
                int CommessaOID = 0;
                int CategoriaOID = 0;
                Type ObjectType = typeof(CAMS.Module.DBTask.RdL);
                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                #region se è list view elabora i criteri di ricerca
                if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                {
                    #region Parametri calcolo Report
                    //var ArObjSel_RegRdLListView = Array<int>();
                    var SelectedContacts = new ArrayList();
                                            
                    foreach (var selectedObject in e.SelectedObjects)
                    {
                        //var currentObject = View.ObjectSpace.GetObject(View.CurrentObject);
                        int currentObjectKey = (int)View.ObjectSpace.GetKeyValue(selectedObject);
                        SelectedContacts.Add(currentObjectKey);
                    }
                    var ArObjSel_RegRdLListView = SelectedContacts.Cast<int>().ToArray<int>();
                    //var ArObjSel_RegRdLListView =    (((ListView)View).Editor).GetSelectedObjects()
                    //               .Cast<RegRdLListView>().ToList<RegRdLListView>()
                    //              .Select(s => s.Codice).Distinct().ToList();

                    var qOidRdL = new XPQuery<RdL>(Sess)
                                .Where(w => ArObjSel_RegRdLListView.Contains<int>(w.RegistroRdL.Oid))
                                .Select(s => s.Oid).ToArray<int>();

                    try
                    {
                        var ListDataReport = new XPQuery<RdL>(Sess)
                             .Where(w => w.RegistroRdL.Oid == ArObjSel_RegRdLListView.First())
                             .Select(s => new
                             {
                                 CommessaOID = s.Immobile.Contratti.Oid,
                                 CommessaSG = s.Immobile.Contratti.WBS,
                                 CategoriaOID = s.Categoria.Oid
                             }).ToList();

                        if (ListDataReport.Count > 0)
                        {
                            CommessaSG = ListDataReport[0].CommessaSG.ToString();
                            CommessaOID = ListDataReport[0].CommessaOID;
                            CategoriaOID = ListDataReport[0].CategoriaOID;
                        }
                    }
                    catch
                    { }
                    #endregion

                    int ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;
                    if (ObjCount > 0)
                    {
                        if (ObjCount < 1000)
                        {
                            CriterioAdd = string.Format("[codiceRdL] In ({0})", String.Join(",", qOidRdL));
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                        }
                        else
                        {
                            int contaRdL = qOidRdL.Count<int>();
                            for (var i = 1; i < contaRdL; i++)
                            {
                                string output = qOidRdL[i].ToString();
                                CriterioAdd = string.Format("[codiceRdL] = ({0})", output);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                            }
                        }
                    }

                #endregion
                    string DisplayReportPersonale = "";
                    try
                    {

                        DisplayReportPersonale = Sess.Query<CAMS.Module.DBAngrafica.SetReportCommessa>()
                            .Where(w => w.Categoria.Oid == CategoriaOID && w.Commesse.Oid == CommessaOID &&
                                           w.ObjectType == ObjectType)
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

                    if (!string.IsNullOrEmpty(DisplayReportPersonale))// se è personalizzato
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
                        Rdlcodici = string.Empty;
                        RegRdLcodici = string.Empty;
                        objects.Clear();
                        foreach (int cur in qOidRdL)
                        {
                            Rdlcodici = string.Concat(Rdlcodici, cur.ToString() + ","); //cur.Codice.ToString() + ",");
                        }

                        var qOidRegRdL = new XPQuery<RdL>(Sess)
                         .Where(w => ArObjSel_RegRdLListView.Contains<int>(w.RegistroRdL.Oid))
                         .Select(s => s.RegistroRdL.Oid).ToArray<int>();
                        foreach (int cur in qOidRegRdL)
                        {
                            RegRdLcodici = string.Concat(RegRdLcodici, cur.ToString() + ","); //cur.Codice.ToString() + ",");
                        }
                        DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                           objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
                           CriteriaOperator.Parse("[DisplayName] = 'Report RdL di Manutenzione'"));
                        //DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =   
                        ///objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(              
                        ///// CriteriaOperator.Parse("[DisplayName] = 'DCReportRdL'"));
                        
                        handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);

                        // apri report  da riattivare!!!!!!!!!!!!!  17072020
                        Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle);

                        #region  questo è il codice per scaricare direttamente da pagina web il file in pdf senza aprire la visualizzazione anteprima

                        //string reportContainerHandle =
                        //    DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                        //DevExpress.ExpressApp.ReportsV2.IReportContainer reportContainer =
                        //    DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerByHandle(reportContainerHandle);
                        //DevExpress.XtraReports.UI.XtraReport report = reportContainer.Report;
                        //DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModule = DevExpress.ExpressApp.ReportsV2.ReportsModuleV2.FindReportsModule(Application.Modules);
                        //if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
                        //{
                        //    try
                        //    {
                        //        report = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerByHandle(reportDataHandle).Report;
                        //        module.ReportsDataSourceHelper.SetupBeforePrint(report, null, null, false, null, false);
                        //        using (System.IO.MemoryStream ms = new System.IO.MemoryStream())
                        //        {
                        //            report.CreateDocument();
                        //            DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
                        //            options.ShowPrintDialogOnOpen = true;
                        //            report.ExportToPdf(ms, options);
                        //            ms.Seek(0, SeekOrigin.Begin);
                        //            byte[] reportContent = ms.ToArray();
                        //            Response.ContentType = "application/pdf";
                        //            Response.AddHeader("Content-Disposition", "attachment; filename=MyFileName.pdf");
                        //            Response.Clear();
                        //            Response.OutputStream.Write(reportContent, 0, reportContent.Length);
                        //            Response.End();
                        //        }
                        //    }
                        //    finally
                        //    {
                        //        if (report != null) report.Dispose();
                        //    }
                        //}
                        #endregion


                        #region  questo è il codice per produrre direttamente il file in pdf senza aprire la visualizzazione a 
                        //////DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData = 
                        //////    objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(new BinaryOperator("DisplayName", "test"));
                        ////string reportContainerHandle = 
                        ////    DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);

                        ////DevExpress.ExpressApp.ReportsV2.IReportContainer reportContainer =
                        ////    DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerByHandle(reportContainerHandle);

                        ////DevExpress.XtraReports.UI.XtraReport report = reportContainer.Report;

                        ////DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModule = DevExpress.ExpressApp.ReportsV2.ReportsModuleV2.FindReportsModule(Application.Modules);
                        ////if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
                        ////{
                        ////    reportsModule.ReportsDataSourceHelper.SetupBeforePrint(report);
                        ////    System.IO.Stream pdfStream = new System.IO.MemoryStream();
                        ////    DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
                        ////    options.ShowPrintDialogOnOpen = true;
                        ////    //options.DocumentOptions.
                        ////    options.ShowPrintDialogOnOpen = true;
                        ////    //report.ExportToPdf(pdfStream);
                        ////    report.ExportToPdf(CAMS.Module.Classi.SetVarSessione.WebCADPath+"mio.pdf");
                        ////}
                        #endregion




                    }


                }
            }
        }

        private void acVisualizzaDettagliRegRdLListViev_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            CAMS.Module.Classi.SetVarSessione.Esegui_DeSelezionaDati = false;
            //if (xpObjectSpace != null)
            //{
            //    DetailView dv = (DetailView)View;
            //    RdL RdL = (RdL_ListView)dv.CurrentObject;
            //    ListView lv = null; //                string listViewId = string.Empty;

            //    string ViewSelezionato = e.SelectedChoiceActionItem.Id.ToString();
            //    string FiltroSelezionato = e.SelectedChoiceActionItem.Data.ToString();
            //    if (ViewSelezionato.Contains("RegistroRdL_DetailView"))
            //    {
            //        var listv = Application.FindModelView(ViewSelezionato);
            //        DetailView dvw = GetDetailViewbyMenu(typeof(CAMS.Module.DBTask.RegistroRdL), ViewSelezionato, RdL.RegistroRdL.Oid);//Application.CreateListView(listViewId, clTicketLv, true);
            //        e.ShowViewParameters.CreatedView = dvw;
            //    }
            //    else if (ViewSelezionato.Contains("_ListView_"))
            //    {
            //        var listv = Application.FindModelView(ViewSelezionato);
            //        Type oggetto = ((((DevExpress.ExpressApp.Model.IModelListView)(listv))).AsObjectView.ModelClass).TypeInfo.Type;
            //        lv = GetListViewbyMenu(oggetto, ViewSelezionato, FiltroSelezionato);//Application.CreateListView(listViewId, clTicketLv, true);
            //        e.ShowViewParameters.CreatedView = lv;
            //    }

            //    // e.ShowViewParameters.CreatedView = lv;
            //    e.ShowViewParameters.TargetWindow = TargetWindow.Current;
            //}
        }

        private void EditRegRdLLVAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //e.Handled = true;
                DetailView NewDv;
                RegistroRdL GetRegistroRdL = xpObjectSpace.GetObjectByKey<RegistroRdL>(((RegRdLListView)View.CurrentObject).Codice);
                NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.Edit;
                    e.ShowViewParameters.CreatedView = NewDv;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }
        }



        DetailView GetDetailViewPersonalizzata(RegistroRdL GetRegistroRdL, IObjectSpace xpObjectSpace)
        {
            DetailView NewDv;
            string Titolo = "";
            Categoria Categori = GetRegistroRdL.RdLes.Select(s => s.Categoria).FirstOrDefault();

            //NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, ((RegRdLListView)View.CurrentObject).Codice);
            int OidCategori = GetRegistroRdL.RdLes.Select(s => s.Categoria.Oid).FirstOrDefault();
            int nrRdL = GetRegistroRdL.RdLes.Select(s => s.Oid).Count();

            if (nrRdL == 1 && OidCategori == 4)
            {
                RdL GetRdL = xpObjectSpace.GetObject<RdL>(GetRegistroRdL.RdLes.FirstOrDefault());
                NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);

                // NewDv.Caption = Titolo;
            }
            else
            {
                NewDv = Application.CreateDetailView(xpObjectSpace, "RegistroRdL_DetailView", true, GetRegistroRdL);

                //  NewDv.Caption = Titolo;
            }
            return NewDv;
        }



    }
}
