using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CAMS.Module.xReport
{
    public partial class xrSGMan : DevExpress.XtraReports.UI.XtraReport
    {
        public xrSGMan()
        {
            InitializeComponent();
        }
        private void subReportRdL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
        }

        private void xrSubreport1_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((xrReportManSGMP)((XRSubreport)sender).ReportSource).RegRdlID.Value = Convert.ToInt32(GetCurrentColumnValue("Codice"));
           


        }

        private void xrSubreport2_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((xrReportManSGMP)((XRSubreport)sender).ReportSource).RegRdlID.Value = Convert.ToInt32(GetCurrentColumnValue("Codice"));

        }
    }
}
