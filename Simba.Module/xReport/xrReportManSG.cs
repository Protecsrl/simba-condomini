using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CAMS.Module.xReport
{
    public partial class xrReportManSG : DevExpress.XtraReports.UI.XtraReport
    {
        public xrReportManSG()
        {
            InitializeComponent();
        }

        private void xrReportManSG_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //((xrRegRdLSubreport)((XRSubreport)sender).ReportSource).RegRdlID.Value = Convert.ToInt32(GetCurrentColumnValue("Codice"));
        }

    }
}
