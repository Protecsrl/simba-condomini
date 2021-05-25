using System;
using DevExpress.XtraReports.UI;
using CAMS.Module.PredefinedReadonlyReports;

namespace CAMS.Module
{
    public partial class XtraReportRegistroRdL : DevExpress.XtraReports.UI.XtraReport
    {
        public XtraReportRegistroRdL()
        {
            InitializeComponent();
        }

        private void subReportRdL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            ((subReportRdL)((XRSubreport)sender).ReportSource).RegRdlID.Value = Convert.ToInt32(GetCurrentColumnValue("Oid"));
        }
    }
}
