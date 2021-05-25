namespace CAMS.Module.Controllers.DBTask.ParametriPopUp
{
    partial class ParametriPivotController
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

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

        #region Component Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.popWinMethodCreateFiltro = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.AcMethodCaricaPivot = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.AcMethodResetPivot = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popWinMethodEditFiltro = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popWinMethodCreateFiltro
            // 
            this.popWinMethodCreateFiltro.AcceptButtonCaption = null;
            this.popWinMethodCreateFiltro.CancelButtonCaption = null;
            this.popWinMethodCreateFiltro.Caption = "Nuovo Filtro";
            this.popWinMethodCreateFiltro.Category = "MyCategory";
            this.popWinMethodCreateFiltro.ConfirmationMessage = null;
            this.popWinMethodCreateFiltro.Id = "popWinMethodCreateFiltro";
            this.popWinMethodCreateFiltro.ImageName = "FilterQuery";
            this.popWinMethodCreateFiltro.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.popWinMethodCreateFiltro.TargetObjectsCriteria = "";
            this.popWinMethodCreateFiltro.ToolTip = null;
            this.popWinMethodCreateFiltro.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popWinMethodCreateFiltro_CustomizePopupWindowParams);
            this.popWinMethodCreateFiltro.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popWinMethodCreateFiltro_Execute);
            // 
            // AcMethodCaricaPivot
            // 
            this.AcMethodCaricaPivot.Caption = "Carica Pivot";
            this.AcMethodCaricaPivot.Category = "MyCategory";
            this.AcMethodCaricaPivot.ConfirmationMessage = "Qusta attività richiederà qualche secondo di elaborazione, sei sicuro che vuoi pr" +
    "ocedere?";
            this.AcMethodCaricaPivot.Id = "AcMethodCaricaPivot";
            this.AcMethodCaricaPivot.ImageName = "PivotTable";
            this.AcMethodCaricaPivot.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.AcMethodCaricaPivot.TargetObjectsCriteria = "";
            this.AcMethodCaricaPivot.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AcMethodCaricaPivot.ToolTip = "Elabora i dati e carica la Pivot.";
            this.AcMethodCaricaPivot.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AcMethodCaricaPivot.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AcMethodCaricaPivot_Execute);
            // 
            // AcMethodResetPivot
            // 
            this.AcMethodResetPivot.Caption = "Reset Pivot";
            this.AcMethodResetPivot.Category = "MyCategory";
            this.AcMethodResetPivot.ConfirmationMessage = "Reset della Pivot, sei sicuro che vuoi procedere?";
            this.AcMethodResetPivot.Id = "AcMethodResetPivot";
            this.AcMethodResetPivot.ImageName = "Reset2_32x32";
            this.AcMethodResetPivot.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.AcMethodResetPivot.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AcMethodResetPivot.ToolTip = "Elabora i dati e carica la Pivot.";
            this.AcMethodResetPivot.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AcMethodResetPivot.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AcMethodResetPivot_Execute);
            // 
            // popWinMethodEditFiltro
            // 
            this.popWinMethodEditFiltro.AcceptButtonCaption = null;
            this.popWinMethodEditFiltro.CancelButtonCaption = null;
            this.popWinMethodEditFiltro.Caption = "Edit Filtro";
            this.popWinMethodEditFiltro.Category = "MyCategory";
            this.popWinMethodEditFiltro.ConfirmationMessage = null;
            this.popWinMethodEditFiltro.Id = "popWinMethodEditFiltro";
            this.popWinMethodEditFiltro.ImageName = "EditFilter";
            this.popWinMethodEditFiltro.ToolTip = null;
            this.popWinMethodEditFiltro.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popWinMethodEditFiltro_CustomizePopupWindowParams);
            this.popWinMethodEditFiltro.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popWinMethodEditFiltro_Execute);
            // 
            // ParametriPivotController
            // 
            this.Actions.Add(this.popWinMethodCreateFiltro);
            this.Actions.Add(this.AcMethodCaricaPivot);
            this.Actions.Add(this.AcMethodResetPivot);
            this.Actions.Add(this.popWinMethodEditFiltro);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.ParametriPopUp.ParametriPivot);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popWinMethodCreateFiltro;
        private DevExpress.ExpressApp.Actions.SimpleAction AcMethodCaricaPivot;
        private DevExpress.ExpressApp.Actions.SimpleAction AcMethodResetPivot;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popWinMethodEditFiltro;
    }
}
