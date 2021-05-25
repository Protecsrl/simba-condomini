using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using CAMS.Module.PredefinedReadonlyReports;
using CAMS.Module.xReport.SubReport;

namespace CAMS.Module.xReport
{
    public partial class xrRegRdL : DevExpress.XtraReports.UI.XtraReport
    {
        public xrRegRdL()
        {
            InitializeComponent();
        }


        private void subReportRdL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((xrRegRdLSubreport)((XRSubreport)sender).ReportSource).RegRdlID.Value = Convert.ToInt32(GetCurrentColumnValue("Codice"));
        }

    }
}
