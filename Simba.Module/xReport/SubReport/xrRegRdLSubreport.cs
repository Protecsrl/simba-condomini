using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace CAMS.Module.xReport.SubReport
{
    public partial class xrRegRdLSubreport : DevExpress.XtraReports.UI.XtraReport
    {
        public xrRegRdLSubreport()
        {
            InitializeComponent();
        }

        private void xrRegRdLSubreport_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            int CatMan_Oid = 0;
            if (GetCurrentColumnValue("OidCategoria") != null)
            {
                bool result = Int32.TryParse(GetCurrentColumnValue("OidCategoria").ToString(), out CatMan_Oid);
                if (!result)
                {
                    CatMan_Oid = 0;            //Console.WriteLine("Converted '{0}' to {1}.", value, number);         
                }

                //   MyReportParametersObject reportParametersObject = ((XafReport)xafReport1).ReportParametersObject as MyReportParametersObject;
                //  var par = subReportRdL.Parameters("parameter1").Value;

                //3	MANUTENZIONE A CONDIZIONE      //4	MANUTENZIONE GUASTO       //5	MANUTENZIONE PROGRAMMATA SPOT
                this.Detail.Visible = true; // 
                //this.xrPanPCR.Visible = true; // problemi cause rimedio
                //this.xrPanMP.Visible = true; // attività pianificate dettaglio
                this.SubBandMP.Visible = true;
                this.SubBandPCR.Visible = true;
                this.SubBandMC.Visible = true;
            
                switch (CatMan_Oid)
                {
                    case 1://1	MANUTENZIONE PROGRAMMATA
                    case 2://2	CONDUZIONE
                    case 3://3	MANUTENZIONE A CONDIZIONE
                    case 5://5	MANUTENZIONE PROGRAMMATA SPOT
                        this.SubBandPCR.Visible = false; // rdl attività pianificate dettaglio . passi
                        this.SubBandMC.Visible = false;
                        break;
                    case 4://4	MANUTENZIONE GUASTO
                        this.Detail.Visible = false;
                        this.SubBandMP.Visible = false;
                        break;
                    default:// altro non pervenuto
                        this.SubBandMP.Visible = false;
                        this.SubBandPCR.Visible = false; 
                        this.SubBandMC.Visible = false;
                        break;
                }
            }

            if (GetCurrentColumnValue("ApparatoPadre") != null)
            {
                this.SubBandApparatoLegato.Visible = true;
            }
            else
            {
                this.SubBandApparatoLegato.Visible = false;
            }


        }
    }
}
