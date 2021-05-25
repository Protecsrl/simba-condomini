using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Diagnostics;
using DevExpress.XtraPrinting.BarCode;

namespace CAMS.Module.xReport
{
    public partial class DCRdLListReport : DevExpress.XtraReports.UI.XtraReport
    {
        public DCRdLListReport()
        {
            InitializeComponent();
        }

        public XRBarCode CreateQRCodeBarCode(XRBarCode barCode)//string BarCodeText)
        {
            // Crea un controllo del codice a barre. 
            //XRBarCode barCode = new XRBarCode();
            // Imposta il tipo di codice a barre su QRCode. 
            barCode.Symbology = new QRCodeGenerator();
            // Regola le proprietà principali del codice a barre.
            //barCode.Text = BarCodeText;
            //barCode.Width = 400;
            //barCode.Height = 150;
            // Se la proprietà AutoModule è impostata su false, rimuovere il commento dalla riga successiva. 
            barCode.AutoModule = true;
            // barcode.Module = 3;
            barCode.Alignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // Regola le proprietà specifiche del tipo di codice a barre.
            ((QRCodeGenerator)barCode.Symbology).CompactionMode = QRCodeCompactionMode.Byte;
            ((QRCodeGenerator)barCode.Symbology).ErrorCorrectionLevel = QRCodeErrorCorrectionLevel.H;
            ((QRCodeGenerator)barCode.Symbology).Version = QRCodeVersion.AutoVersion;
            //QRCodeGenerator symb = new QRCodeGenerator();
            //symb.CompactionMode = QRCodeCompactionMode.Byte;
            //symb.ErrorCorrectionLevel  = QRCodeErrorCorrectionLevel.H;
            //symb.Version = QRCodeVersion.AutoVersion;
            //barCode.Symbology = symb;
            return barCode;
        }

        private void GroupHeader_RdL_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
        {
            //#if
            //Debug.WriteLine(GetCurrentColumnValue("CategoriaManutenzione").ToString());
            //Debug.WriteLine(GetCurrentColumnValue("OidCategoria").ToString());

            this.xrBarCode3 = CreateQRCodeBarCode(this.xrBarCode3); //U.ToString());


            int CatMan_Oid = 0;
            if (this.GetCurrentColumnValue("OidCategoria") != null)
            {
                bool result = Int32.TryParse(this.GetCurrentColumnValue("OidCategoria").ToString(), out CatMan_Oid);
                if (!result)
                {
                    CatMan_Oid = 0;            //Console.WriteLine("Converted '{0}' to {1}.", value, number);         
                }

                this.Detail.Visible = true; // 
                this.SubBandMP.Visible = false; //true;  sempre spento
                this.GroupHeader2.Visible = true;
                this.SubBandPCR.Visible = true;
                this.SubBandMC.Visible = true;
                this.SubBandElencoComponentiMP.Visible = true;
                //this.SubBandElencoComponentiSostegno.Visible = true;
                this.SubBandApparatoLegato.Visible = true;
                switch (CatMan_Oid)
                {
                    case 1://1	MANUTENZIONE PROGRAMMATA                            
                    case 2://2	CONDUZIONE                               
                    case 3://3	MANUTENZIONE A CONDIZIONE                          
                    case 5://5	MANUTENZIONE PROGRAMMATA SPOT
                        this.SubBandPCR.Visible = false; // rdl attività pianificate dettaglio . passi
                        this.SubBandMC.Visible = false;
                        //Debug.WriteLine(" non guasto ??????????????????????");
                        break;
                    case 4://4	MANUTENZIONE GUASTO
                        this.Detail.Visible = false;

                        this.SubBandMP.Visible = false;
                        this.GroupHeader2.Visible = false;
                        this.SubBandElencoComponentiMP.Visible = false;
                        //Debug.WriteLine(" questa è guasto!!!!!!!!!!!!!!!! ");
                        break;
                    default:// altro non pervenuto
                        //this.SubBandMP.Visible = false;
                        //this.SubBandPCR.Visible = false;
                        //this.SubBandMC.Visible = false;
                        //this.SubBandElencoComponentiMP.Visible = false;
                        break;
                }


            }

        }

    }
}
