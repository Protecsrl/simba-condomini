using DevExpress.ExpressApp;
using DevExpress.XtraReports.UI;
using System;

namespace CAMS.Module.PredefinedReadonlyReports
{
    public partial class subReportRdL : DevExpress.XtraReports.UI.XtraReport
    {
        public subReportRdL()
        {
            InitializeComponent();
        }

        private void Detail_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            // private IObjectSpace theObjectSpace;   
            //     IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            ////if([RegRdlID]
            //if (string.IsNullOrEmpty(xrLabel10.Text.ToString()) || xrLabel10.Text.ToString().Length < 2)
            //    this.Detail.Visible = false;
            //else
            //    this.Detail.Visible = true;
            //XRLabel label = (XRLabel)sender;
            // Get the total value. 
            //if (RRdlOid == 1)
            //{
            //    label.ForeColor = Color.White;
            //    label.BackColor = Color.Red;
            //}
            //else if (total > 1000)
            //{
            //    label.ForeColor = Color.White;
            //    label.BackColor = Color.Green;
            //}
            //else
            //{
            //    label.ForeColor = Color.Black;
            //    label.BackColor = Color.Transparent;
            //}
        }


        private void subReportRdL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //   var mio = this.Report.GetCurrentRow();
            //.ReportSource.Parameters["MasterID"].Value = ((XafReport)xafReport1).ObjectSpace.GetKeyValue(GetCurrentRow());
            //  var CatMan = GetCurrentColumnValue("RegistroRdL");
            //  int CatMan_Oid = 1;// Convert.ToInt32(GetCurrentColumnValue("RegistroRdL"));
            int CatMan_Oid = ((CAMS.Module.DBALibrary.Categoria)(GetCurrentColumnValue("Categoria"))).Oid;
            //   MyReportParametersObject reportParametersObject = ((XafReport)xafReport1).ReportParametersObject as MyReportParametersObject;
            //  var par = subReportRdL.Parameters("parameter1").Value;
            //3	MANUTENZIONE A CONDIZIONE      //4	MANUTENZIONE GUASTO       //5	MANUTENZIONE PROGRAMMATA SPOT
            this.Detail.Visible = true; // problemi cause rimedio
            this.DetailReport.Visible = true; // attività pianificate dettaglio
            this.Detail1.Visible = true; // attività pianificate dettaglio
            this.DetailReport1.Visible = true; // attività pianificate dettaglio . passi
            this.Detail2.Visible = true; // attività pianificate dettaglio . passi

            this.Detail_RdL_AppSkMP.Visible = true; // attività pianificate dettaglio . passi
            this.DetailReport2.Visible = true; // attività pianificate dettaglio . passi

            switch (CatMan_Oid)
            {
                case 1://1	MANUTENZIONE PROGRAMMATA
                    this.Detail.Visible = false;
                    this.Detail_RdL_AppSkMP.Visible = false; // rdl attività pianificate dettaglio . passi
                    this.DetailReport2.Visible = false; // rdl attività pianificate dettaglio . passi
                    break;
                case 2://2	CONDUZIONE
                    this.Detail.Visible = false;
                    this.DetailReport.Visible = false; // attività pianificate dettaglio
                    this.Detail1.Visible = false; // attività pianificate dettaglio
                    this.DetailReport1.Visible = false; // attività pianificate dettaglio . passi
                    this.Detail2.Visible = false; // attività pianificate dettaglio . passi
                    break;
                case 3://3	MANUTENZIONE A CONDIZIONE
                    this.Detail.Visible = false;
                    this.DetailReport.Visible = false; // attività pianificate dettaglio
                    this.Detail1.Visible = false; // attività pianificate dettaglio
                    this.DetailReport1.Visible = false; // attività pianificate dettaglio . passi
                    this.Detail2.Visible = false; // attività pianificate dettaglio . passi
                    break;
                case 4://4	MANUTENZIONE GUASTO
                    this.Detail.Visible = true;
                    this.DetailReport.Visible = false; // attività pianificate dettaglio
                    this.Detail1.Visible = false; // attività pianificate dettaglio
                    this.DetailReport1.Visible = false; // attività pianificate dettaglio . passi
                    this.Detail2.Visible = false; // attività pianificate dettaglio . passi

                     this.Detail_RdL_AppSkMP.Visible = false; // rdl attività pianificate dettaglio . passi
                    this.DetailReport2.Visible = false; // rdl attività pianificate dettaglio . passi
                    break;
                case 5://5	MANUTENZIONE PROGRAMMATA SPOT
                    this.Detail.Visible = false;
                    this.DetailReport.Visible = false; // attività pianificate dettaglio
                    this.Detail1.Visible = false; // attività pianificate dettaglio
                    this.DetailReport1.Visible = false; // attività pianificate dettaglio . passi
                    this.Detail2.Visible = false; // attività pianificate dettaglio . passi
                    break;
                default:// altro non pervenuto

                    break;
            }


        }
    
    
    }
}
