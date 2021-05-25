using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.Diagnostics;

/// <summary>
/// Summary description for XtraReport3
/// </summary>
public class XtraReport3 : DevExpress.XtraReports.UI.XtraReport
{
    private DevExpress.XtraReports.UI.DetailBand Detail;
    private DevExpress.XtraReports.UI.TopMarginBand TopMargin;
    private DevExpress.XtraReports.UI.BottomMarginBand BottomMargin;
    private DevExpress.Persistent.Base.ReportsV2.CollectionDataSource collectionDataSource1;
    private GroupHeaderBand GroupHeader1;
    private GroupHeaderBand GroupHeader2;
    private GroupHeaderBand GroupHeader3;
    private XRLabel xrLabel1;
    private XRLabel xrLabel12;
    private XRLabel xrLabel9;
    private PageHeaderBand PageHeader;
    private XRPictureBox xrPictureBox2;
    private XRLabel xrLabel63;
    private XRLabel xrLabel62;
    private SubBand SubBandMP;
    private XRLabel xrLabel43;
    private XRLabel xrLabel49;
    private XRLabel xrLabel17;
    private XRLabel xrLabel36;
    private XRLine xrLine6;
    private XRLabel xrLabel18;
    private XRLabel xrLabel26;
    private XRLabel xrLabel27;
    private PageFooterBand PageFooter;
    private XRPictureBox xrPictureBox1;
    private XRLabel xrLabel35;
    private XRPageInfo xrPageInfo2;
    private GroupFooterBand GroupFooter2;
    private XRLabel xrLabel4;
    private XRLabel xrLabel2;
    private XRLabel xrLabel3;
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    public XtraReport3()
    {
        InitializeComponent();
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary> 
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }
        base.Dispose(disposing);
    }

    #region Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent()
    {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(XtraReport3));
            this.Detail = new DevExpress.XtraReports.UI.DetailBand();
            this.xrLabel18 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel26 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel27 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel12 = new DevExpress.XtraReports.UI.XRLabel();
            this.TopMargin = new DevExpress.XtraReports.UI.TopMarginBand();
            this.BottomMargin = new DevExpress.XtraReports.UI.BottomMarginBand();
            this.GroupHeader1 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel9 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel1 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader2 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel4 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel62 = new DevExpress.XtraReports.UI.XRLabel();
            this.GroupHeader3 = new DevExpress.XtraReports.UI.GroupHeaderBand();
            this.xrLabel43 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel49 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel17 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel36 = new DevExpress.XtraReports.UI.XRLabel();
            this.SubBandMP = new DevExpress.XtraReports.UI.SubBand();
            this.xrLine6 = new DevExpress.XtraReports.UI.XRLine();
            this.PageHeader = new DevExpress.XtraReports.UI.PageHeaderBand();
            this.xrPictureBox2 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel63 = new DevExpress.XtraReports.UI.XRLabel();
            this.PageFooter = new DevExpress.XtraReports.UI.PageFooterBand();
            this.xrPictureBox1 = new DevExpress.XtraReports.UI.XRPictureBox();
            this.xrLabel35 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrPageInfo2 = new DevExpress.XtraReports.UI.XRPageInfo();
            this.GroupFooter2 = new DevExpress.XtraReports.UI.GroupFooterBand();
            this.collectionDataSource1 = new DevExpress.Persistent.Base.ReportsV2.CollectionDataSource();
            this.xrLabel2 = new DevExpress.XtraReports.UI.XRLabel();
            this.xrLabel3 = new DevExpress.XtraReports.UI.XRLabel();
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSource1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this)).BeginInit();
            // 
            // Detail
            // 
            this.Detail.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel18,
            this.xrLabel26,
            this.xrLabel27,
            this.xrLabel12});
            this.Detail.HeightF = 26.66667F;
            this.Detail.Name = "Detail";
            this.Detail.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.Detail.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // xrLabel18
            // 
            this.xrLabel18.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel18.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Italic);
            this.xrLabel18.LocationFloat = new DevExpress.Utils.PointFloat(662.3298F, 0F);
            this.xrLabel18.Name = "xrLabel18";
            this.xrLabel18.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel18.SizeF = new System.Drawing.SizeF(37.67017F, 21.45828F);
            this.xrLabel18.StylePriority.UseBorders = false;
            this.xrLabel18.StylePriority.UseFont = false;
            // 
            // xrLabel26
            // 
            this.xrLabel26.AutoWidth = true;
            this.xrLabel26.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[PassoSchedaMP]")});
            this.xrLabel26.LocationFloat = new DevExpress.Utils.PointFloat(75F, 0F);
            this.xrLabel26.Multiline = true;
            this.xrLabel26.Name = "xrLabel26";
            this.xrLabel26.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel26.SizeF = new System.Drawing.SizeF(572.2529F, 23F);
            this.xrLabel26.Text = "xrLabel24";
            // 
            // xrLabel27
            // 
            this.xrLabel27.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[NrOrdine]")});
            this.xrLabel27.LocationFloat = new DevExpress.Utils.PointFloat(19.12323F, 0F);
            this.xrLabel27.Name = "xrLabel27";
            this.xrLabel27.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel27.SizeF = new System.Drawing.SizeF(42.38353F, 23F);
            this.xrLabel27.Text = "xrLabel9";
            // 
            // xrLabel12
            // 
            this.xrLabel12.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OidCategoria]")});
            this.xrLabel12.LocationFloat = new DevExpress.Utils.PointFloat(725F, 0F);
            this.xrLabel12.Name = "xrLabel12";
            this.xrLabel12.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel12.SizeF = new System.Drawing.SizeF(25.00003F, 25F);
            this.xrLabel12.Text = "xrLabel12";
            this.xrLabel12.Visible = false;
            // 
            // TopMargin
            // 
            this.TopMargin.HeightF = 12F;
            this.TopMargin.Name = "TopMargin";
            this.TopMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.TopMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // BottomMargin
            // 
            this.BottomMargin.HeightF = 10.83333F;
            this.BottomMargin.Name = "BottomMargin";
            this.BottomMargin.Padding = new DevExpress.XtraPrinting.PaddingInfo(0, 0, 0, 0, 100F);
            this.BottomMargin.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopLeft;
            // 
            // GroupHeader1
            // 
            this.GroupHeader1.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel9,
            this.xrLabel1});
            this.GroupHeader1.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("CodRegRdL", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader1.HeightF = 25.2809F;
            this.GroupHeader1.Level = 2;
            this.GroupHeader1.Name = "GroupHeader1";
            // 
            // xrLabel9
            // 
            this.xrLabel9.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OidCategoria]")});
            this.xrLabel9.LocationFloat = new DevExpress.Utils.PointFloat(350F, 0F);
            this.xrLabel9.Name = "xrLabel9";
            this.xrLabel9.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel9.SizeF = new System.Drawing.SizeF(100F, 25F);
            this.xrLabel9.Text = "xrLabel9";
            // 
            // xrLabel1
            // 
            this.xrLabel1.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodRegRdL]")});
            this.xrLabel1.LocationFloat = new DevExpress.Utils.PointFloat(0F, 0F);
            this.xrLabel1.Name = "xrLabel1";
            this.xrLabel1.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel1.SizeF = new System.Drawing.SizeF(100F, 25F);
            this.xrLabel1.Text = "xrLabel1";
            // 
            // GroupHeader2
            // 
            this.GroupHeader2.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel2,
            this.xrLabel4,
            this.xrLabel62});
            this.GroupHeader2.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("codiceRdL", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader2.GroupUnion = DevExpress.XtraReports.UI.GroupUnion.WholePage;
            this.GroupHeader2.HeightF = 177.5F;
            this.GroupHeader2.KeepTogether = true;
            this.GroupHeader2.Level = 1;
            this.GroupHeader2.Name = "GroupHeader2";
            this.GroupHeader2.PageBreak = DevExpress.XtraReports.UI.PageBreak.BeforeBand;
            // 
            // xrLabel4
            // 
            this.xrLabel4.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OidCategoria]")});
            this.xrLabel4.LocationFloat = new DevExpress.Utils.PointFloat(187.5F, 12.5F);
            this.xrLabel4.Name = "xrLabel4";
            this.xrLabel4.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel4.SizeF = new System.Drawing.SizeF(50F, 25F);
            this.xrLabel4.Text = "xrLabel9";
            // 
            // xrLabel62
            // 
            this.xrLabel62.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel62.ForeColor = System.Drawing.Color.DarkRed;
            this.xrLabel62.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
            this.xrLabel62.Name = "xrLabel62";
            this.xrLabel62.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel62.SizeF = new System.Drawing.SizeF(164.1456F, 22.99999F);
            this.xrLabel62.StylePriority.UseFont = false;
            this.xrLabel62.StylePriority.UseForeColor = false;
            this.xrLabel62.Text = "Registro di Lavoro";
            // 
            // GroupHeader3
            // 
            this.GroupHeader3.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLabel3,
            this.xrLabel43,
            this.xrLabel49,
            this.xrLabel17,
            this.xrLabel36});
            this.GroupHeader3.GroupFields.AddRange(new DevExpress.XtraReports.UI.GroupField[] {
            new DevExpress.XtraReports.UI.GroupField("NrOrdine", DevExpress.XtraReports.UI.XRColumnSortOrder.Ascending)});
            this.GroupHeader3.HeightF = 257.5F;
            this.GroupHeader3.Name = "GroupHeader3";
            this.GroupHeader3.SubBands.AddRange(new DevExpress.XtraReports.UI.SubBand[] {
            this.SubBandMP});
            // 
            // xrLabel43
            // 
            this.xrLabel43.BackColor = System.Drawing.Color.Red;
            this.xrLabel43.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[OidCategoria]")});
            this.xrLabel43.LocationFloat = new DevExpress.Utils.PointFloat(169.6918F, 0F);
            this.xrLabel43.Name = "xrLabel43";
            this.xrLabel43.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel43.SizeF = new System.Drawing.SizeF(137.8291F, 18.00011F);
            this.xrLabel43.StylePriority.UseBackColor = false;
            this.xrLabel43.Text = "xrLabel4";
            this.xrLabel43.Visible = false;
            // 
            // xrLabel49
            // 
            this.xrLabel49.Borders = ((DevExpress.XtraPrinting.BorderSide)((((DevExpress.XtraPrinting.BorderSide.Left | DevExpress.XtraPrinting.BorderSide.Top) 
            | DevExpress.XtraPrinting.BorderSide.Right) 
            | DevExpress.XtraPrinting.BorderSide.Bottom)));
            this.xrLabel49.BorderWidth = 4F;
            this.xrLabel49.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[codiceRdL]")});
            this.xrLabel49.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Bold);
            this.xrLabel49.LocationFloat = new DevExpress.Utils.PointFloat(613.772F, 0F);
            this.xrLabel49.Name = "xrLabel49";
            this.xrLabel49.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel49.SizeF = new System.Drawing.SizeF(131.0244F, 37.65F);
            this.xrLabel49.StylePriority.UseBorders = false;
            this.xrLabel49.StylePriority.UseBorderWidth = false;
            this.xrLabel49.StylePriority.UseFont = false;
            this.xrLabel49.StylePriority.UseTextAlignment = false;
            this.xrLabel49.TextAlignment = DevExpress.XtraPrinting.TextAlignment.MiddleCenter;
            // 
            // xrLabel17
            // 
            this.xrLabel17.Font = new System.Drawing.Font("Times New Roman", 10F, System.Drawing.FontStyle.Bold);
            this.xrLabel17.ForeColor = System.Drawing.Color.DarkRed;
            this.xrLabel17.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
            this.xrLabel17.Name = "xrLabel17";
            this.xrLabel17.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel17.SizeF = new System.Drawing.SizeF(143.4524F, 22.99999F);
            this.xrLabel17.StylePriority.UseFont = false;
            this.xrLabel17.StylePriority.UseForeColor = false;
            this.xrLabel17.Text = "Richiesta di Lavoro";
            // 
            // xrLabel36
            // 
            this.xrLabel36.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodRegRdL]")});
            this.xrLabel36.LocationFloat = new DevExpress.Utils.PointFloat(336.4947F, 10F);
            this.xrLabel36.Name = "xrLabel36";
            this.xrLabel36.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel36.SizeF = new System.Drawing.SizeF(25.00003F, 12.99998F);
            this.xrLabel36.Text = "xrLabel36";
            this.xrLabel36.Visible = false;
            // 
            // SubBandMP
            // 
            this.SubBandMP.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrLine6});
            this.SubBandMP.HeightF = 167F;
            this.SubBandMP.Name = "SubBandMP";
            // 
            // xrLine6
            // 
            this.xrLine6.LocationFloat = new DevExpress.Utils.PointFloat(22.87031F, 0F);
            this.xrLine6.Name = "xrLine6";
            this.xrLine6.SizeF = new System.Drawing.SizeF(720.7222F, 2.666669F);
            // 
            // PageHeader
            // 
            this.PageHeader.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox2,
            this.xrLabel63});
            this.PageHeader.HeightF = 41.66667F;
            this.PageHeader.Name = "PageHeader";
            // 
            // xrPictureBox2
            // 
            this.xrPictureBox2.Image = ((System.Drawing.Image)(resources.GetObject("xrPictureBox2.Image")));
            this.xrPictureBox2.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
            this.xrPictureBox2.Name = "xrPictureBox2";
            this.xrPictureBox2.SizeF = new System.Drawing.SizeF(130.088F, 37.50002F);
            this.xrPictureBox2.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrLabel63
            // 
            this.xrLabel63.Font = new System.Drawing.Font("Times New Roman", 9.75F, System.Drawing.FontStyle.Bold);
            this.xrLabel63.LocationFloat = new DevExpress.Utils.PointFloat(154.3818F, 0F);
            this.xrLabel63.Multiline = true;
            this.xrLabel63.Name = "xrLabel63";
            this.xrLabel63.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel63.SizeF = new System.Drawing.SizeF(481.1191F, 27.50004F);
            this.xrLabel63.StylePriority.UseFont = false;
            this.xrLabel63.StylePriority.UseTextAlignment = false;
            this.xrLabel63.Text = "Report Manutenzione";
            this.xrLabel63.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // PageFooter
            // 
            this.PageFooter.Controls.AddRange(new DevExpress.XtraReports.UI.XRControl[] {
            this.xrPictureBox1,
            this.xrLabel35,
            this.xrPageInfo2});
            this.PageFooter.HeightF = 27.5F;
            this.PageFooter.Name = "PageFooter";
            // 
            // xrPictureBox1
            // 
            this.xrPictureBox1.ImageUrl = "C:\\AssemblaPRT17\\EAMS\\CAMS.Module\\Images\\LogoCofelyEngie5.png";
            this.xrPictureBox1.LocationFloat = new DevExpress.Utils.PointFloat(635.5004F, 0F);
            this.xrPictureBox1.Name = "xrPictureBox1";
            this.xrPictureBox1.SizeF = new System.Drawing.SizeF(98.83331F, 25.60638F);
            this.xrPictureBox1.Sizing = DevExpress.XtraPrinting.ImageSizeMode.ZoomImage;
            // 
            // xrLabel35
            // 
            this.xrLabel35.LocationFloat = new DevExpress.Utils.PointFloat(12.5F, 0F);
            this.xrLabel35.Multiline = true;
            this.xrLabel35.Name = "xrLabel35";
            this.xrLabel35.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel35.SizeF = new System.Drawing.SizeF(158.6046F, 25.60638F);
            this.xrLabel35.StylePriority.UseTextAlignment = false;
            this.xrLabel35.Text = "IT&D - BST";
            this.xrLabel35.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            // 
            // xrPageInfo2
            // 
            this.xrPageInfo2.LocationFloat = new DevExpress.Utils.PointFloat(202.2499F, 0F);
            this.xrPageInfo2.Name = "xrPageInfo2";
            this.xrPageInfo2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrPageInfo2.SizeF = new System.Drawing.SizeF(382.1463F, 25.60625F);
            this.xrPageInfo2.StylePriority.UseTextAlignment = false;
            this.xrPageInfo2.TextAlignment = DevExpress.XtraPrinting.TextAlignment.TopCenter;
            this.xrPageInfo2.TextFormatString = "Pagina {0} di  {1}";
            // 
            // GroupFooter2
            // 
            this.GroupFooter2.HeightF = 12.5F;
            this.GroupFooter2.Level = 1;
            this.GroupFooter2.Name = "GroupFooter2";
            // 
            // collectionDataSource1
            // 
            this.collectionDataSource1.Name = "collectionDataSource1";
            this.collectionDataSource1.ObjectTypeName = "CAMS.Module.DBTask.DC.DCRdLListReport";
            this.collectionDataSource1.TopReturnedRecords = 0;
            // 
            // xrLabel2
            // 
            this.xrLabel2.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodRegRdL]")});
            this.xrLabel2.LocationFloat = new DevExpress.Utils.PointFloat(400F, 12.5F);
            this.xrLabel2.Name = "xrLabel2";
            this.xrLabel2.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel2.SizeF = new System.Drawing.SizeF(100F, 25F);
            this.xrLabel2.Text = "xrLabel1";
            // 
            // xrLabel3
            // 
            this.xrLabel3.ExpressionBindings.AddRange(new DevExpress.XtraReports.UI.ExpressionBinding[] {
            new DevExpress.XtraReports.UI.ExpressionBinding("BeforePrint", "Text", "[CodRegRdL]")});
            this.xrLabel3.LocationFloat = new DevExpress.Utils.PointFloat(50F, 50F);
            this.xrLabel3.Name = "xrLabel3";
            this.xrLabel3.Padding = new DevExpress.XtraPrinting.PaddingInfo(2, 2, 0, 0, 100F);
            this.xrLabel3.SizeF = new System.Drawing.SizeF(100F, 25F);
            this.xrLabel3.Text = "xrLabel1";
            // 
            // XtraReport3
            // 
            this.Bands.AddRange(new DevExpress.XtraReports.UI.Band[] {
            this.Detail,
            this.TopMargin,
            this.BottomMargin,
            this.GroupHeader1,
            this.GroupHeader2,
            this.GroupHeader3,
            this.PageHeader,
            this.PageFooter,
            this.GroupFooter2});
            this.ComponentStorage.AddRange(new System.ComponentModel.IComponent[] {
            this.collectionDataSource1});
            this.DataSource = this.collectionDataSource1;
            this.Margins = new System.Drawing.Printing.Margins(38, 43, 12, 11);
            this.SnappingMode = DevExpress.XtraReports.UI.SnappingMode.SnapToGrid;
            this.Version = "17.2";
            this.DataSourceRowChanged += new DevExpress.XtraReports.UI.DataSourceRowEventHandler(this.XtraReport3_DataSourceRowChanged);
            this.BeforePrint += new System.Drawing.Printing.PrintEventHandler(this.XtraReport3_BeforePrint);
            ((System.ComponentModel.ISupportInitialize)(this.collectionDataSource1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this)).EndInit();

    }

    #endregion

    private void XtraReport3_BeforePrint(object sender, System.Drawing.Printing.PrintEventArgs e)
    {
        Debug.WriteLine(this.CurrentRowIndex);
        Debug.WriteLine(this.GetCurrentColumnValue("OidCategoria").ToString());
        int CatMan_Oid = 0;
        //if (this.GetCurrentColumnValue("OidCategoria") != null)
        //{
        //    bool result = Int32.TryParse(this.GetCurrentColumnValue("OidCategoria").ToString(), out CatMan_Oid);
        //    if (!result)
        //    {
        //        CatMan_Oid = 0;            //Console.WriteLine("Converted '{0}' to {1}.", value, number);         
        //    }
        //    //Debug.WriteLine(GetCurrentColumnValue("CategoriaManutenzione").ToString());
        //    //Debug.WriteLine(GetCurrentColumnValue("OidCategoria").ToString());
        //    //   MyReportParametersObject reportParametersObject = ((XafReport)xafReport1).ReportParametersObject as MyReportParametersObject;
        //    //  var par = subReportRdL.Parameters("parameter1").Value;

        //    //3	MANUTENZIONE A CONDIZIONE      //4	MANUTENZIONE GUASTO       //5	MANUTENZIONE PROGRAMMATA SPOT
        //    this.Detail.Visible = true; // 
        //    //this.xrPanPCR.Visible = true; // problemi cause rimedio
        //    //this.xrPanMP.Visible = true; // attività pianificate dettaglio
        //    this.SubBandMP.Visible = true;
        //    this.SubBandPCR.Visible = true;
        //    this.SubBandMC.Visible = true;
        //    //this.SubBandEComponentiMP.Visible = false;
        //    switch (0)
        //    {
        //        case 1://1	MANUTENZIONE PROGRAMMATA
        //            this.SubBandPCR.Visible = false; // rdl attività pianificate dettaglio . passi
        //            this.SubBandMC.Visible = false;

        //            //if (GetCurrentColumnValue("ComponentiManutenzione") != null && GetCurrentColumnValue("ComponentiManutenzione").ToString().ToUpper() != "NA")
        //            //{
        //            //    this.SubBandEComponentiMP.Visible = true;
        //            //}

        //            break;
        //        case 2://2	CONDUZIONE
        //            this.SubBandPCR.Visible = false; // rdl attività pianificate dettaglio . passi
        //            this.SubBandMC.Visible = false;
        //            break;
        //        case 3://3	MANUTENZIONE A CONDIZIONE
        //            this.SubBandPCR.Visible = false; // rdl attività pianificate dettaglio . passi
        //            this.SubBandMC.Visible = false;
        //            break;
        //        case 5://5	MANUTENZIONE PROGRAMMATA SPOT
        //            this.SubBandPCR.Visible = false; // rdl attività pianificate dettaglio . passi
        //            this.SubBandMC.Visible = false;

        //            //if (GetCurrentColumnValue("ComponentiManutenzione") != null && GetCurrentColumnValue("ComponentiManutenzione").ToString().ToUpper() != "NA")
        //            //{
        //            //    this.SubBandEComponentiMP.Visible = true;
        //            //}

        //            break;
        //        case 4://4	MANUTENZIONE GUASTO
        //            this.Detail.Visible = false;
        //            this.SubBandMP.Visible = false;
        //            //this.SubBandEComponentiMP.Visible = false;
        //            //if (GetCurrentColumnValue("Problema") != null && GetCurrentColumnValue("Problema").ToString().ToUpper() != "NA")
        //            //{
        //            //    this.SubBandPCR.Visible = true;
        //            //}
        //            //else
        //            //{
        //            //    this.SubBandPCR.Visible = false;
        //            //}

        //            break;
        //        default:// altro non pervenuto
        //            //this.SubBandMP.Visible = false;
        //            //this.SubBandPCR.Visible = false;
        //            //this.SubBandMC.Visible = false;
        //            //this.SubBandEComponentiMP.Visible = false;
        //            break;
        //    }
        //}

        /////  ApparatoSostegno
        //if (GetCurrentColumnValue("ApparatoPadre") != null && GetCurrentColumnValue("ApparatoPadre").ToString().ToUpper() != "NA")
        //{
        //    this.SubBandApparatoLegato.Visible = true;
        //}
        //else
        //{
        //    this.SubBandApparatoLegato.Visible = false;
        //}

        //if (GetCurrentColumnValue("ComponentiSostegno") != null && GetCurrentColumnValue("ComponentiSostegno").ToString().ToUpper() != "NA")
        //{
        //    this.SubBandEComponentiSostegno.Visible = true;
        //}
        //else
        //{
        //    this.SubBandEComponentiSostegno.Visible = false;
        //}
    }

    private void XtraReport3_DataSourceRowChanged(object sender, DataSourceRowEventArgs e)
    {
        Debug.WriteLine(this.GetCurrentColumnValue("OidCategoria").ToString());
    }
}
