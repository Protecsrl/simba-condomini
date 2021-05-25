using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMS.Module.Classi.Report
{
    public class StampaReportToFile : IDisposable
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }


        //    public void InviaReport(string Connessione, string UserNameCorrente, string RegRdLcodici, BindingList<DCRdLListReport> objects)
        //    {
        //        //IObjectSpace xpObjectSpace = Application.CreateObjectSpace()  ;
        //        //Session Sess = ((XPObjectSpace)ObjectSpace).Session;
        //        private static BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
        //    //    string CriterioAdd = string.Empty;
        //    //    string CommessaSG = string.Empty;
        //    //    int CommessaOID = 0;
        //    //    int CategoriaOID = 0;
        //    //    Type ObjectType = typeof(CAMS.Module.DBTask.RdL);
        //    //    GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
        //    //    string CriterioReport = string.Empty;
        //    //    GroupOperator criteriaReport = new GroupOperator(GroupOperatorType.And);

        //    //        string handle = "";
        //    //    IObjectSpace objectSpace =
        //    //        DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(
        //    //        typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));
        //    //    string Rdlcodici = string.Empty;



        //    //        //var qOidRegRdL = new XPQuery<RdL>(Sess)
        //    //        // .Where(w => ArObjSel_RegRdLListView.Contains<int>(w.RegistroRdL.Oid))
        //    //        // .Select(s => s.RegistroRdL.Oid).ToArray<int>();

        //    //    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
        //    //       objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
        //    //       CriteriaOperator.Parse("[DisplayName] = 'Report RdL di Manutenzione'"));
        //    //    //DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =      objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(                        // CriteriaOperator.Parse("[DisplayName] = 'DCReportRdL'"));

        //    //    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);




        //    //        #region  questo è il codice per produrre direttamente il file in pdf senza aprire la visualizzazione a 
        //    //        //DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData = 
        //    //        //    objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(new BinaryOperator("DisplayName", "test"));
        //    //        string reportContainerHandle =
        //    //            DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
        //    //    DevExpress.ExpressApp.ReportsV2.IReportContainer reportContainer =
        //    //        DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerByHandle(reportContainerHandle);
        //    //    DevExpress.XtraReports.UI.XtraReport report = reportContainer.Report;
        //    //    DevExpress.ExpressApp.ReportsV2.ReportsModuleV2 reportsModule = DevExpress.ExpressApp.ReportsV2.ReportsModuleV2.FindReportsModule(Application.Modules);
        //    //        if (reportsModule != null && reportsModule.ReportsDataSourceHelper != null)
        //    //        {
        //    //            reportsModule.ReportsDataSourceHelper.SetupBeforePrint(report);
        //    //            System.IO.Stream pdfStream = new System.IO.MemoryStream();
        //    //    DevExpress.XtraPrinting.PdfExportOptions options = new DevExpress.XtraPrinting.PdfExportOptions();
        //    //    options.ShowPrintDialogOnOpen = true;
        //    //            //options.DocumentOptions.
        //    //            options.ShowPrintDialogOnOpen = true;
        //    //            //report.ExportToPdf(pdfStream);
        //    //            report.ExportToPdf(CAMS.Module.Classi.SetVarSessione.WebCADPath + "mio.pdf");
        //    //        }
        //    //#endregion

        //}

    }
}

