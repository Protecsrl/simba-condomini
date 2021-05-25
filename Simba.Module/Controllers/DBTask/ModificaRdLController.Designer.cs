namespace CAMS.Module.Controllers.DBTask
{
    partial class ModificaRdLController
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
            this.pupModificaRdL_WinRisorseTeam = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acModificaRdL_DelRisorsaTeam = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // pupModificaRdL_WinRisorseTeam
            // 
            this.pupModificaRdL_WinRisorseTeam.AcceptButtonCaption = "Seleziona";
            this.pupModificaRdL_WinRisorseTeam.CancelButtonCaption = "Annulla";
            this.pupModificaRdL_WinRisorseTeam.Caption = "Team";
            this.pupModificaRdL_WinRisorseTeam.Category = "MyHiddenCategory";
            this.pupModificaRdL_WinRisorseTeam.ConfirmationMessage = null;
            this.pupModificaRdL_WinRisorseTeam.Id = "pupModificaRdL_WinRisorseTeam";
            this.pupModificaRdL_WinRisorseTeam.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupModificaRdL_WinRisorseTeam.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.pupModificaRdL_WinRisorseTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupModificaRdL_WinRisorseTeam.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11)";
            this.pupModificaRdL_WinRisorseTeam.TargetViewId = "";
            this.pupModificaRdL_WinRisorseTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupModificaRdL_WinRisorseTeam.ToolTip = null;
            this.pupModificaRdL_WinRisorseTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupModificaRdL_WinRisorseTeam.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupModificaRdL_WinRisorseTeam_CustomizePopupWindowParams);
            this.pupModificaRdL_WinRisorseTeam.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupModificaRdL_WinRisorseTeam_Execute);
            // 
            // acModificaRdL_DelRisorsaTeam
            // 
            this.acModificaRdL_DelRisorsaTeam.Caption = "Del Risorsa Team";
            this.acModificaRdL_DelRisorsaTeam.Category = "MyHiddenCategory";
            this.acModificaRdL_DelRisorsaTeam.ConfirmationMessage = null;
            this.acModificaRdL_DelRisorsaTeam.Id = "acModificaRdL_DelRisorsaTeam";
            this.acModificaRdL_DelRisorsaTeam.ImageName = "Action_Clear";
            this.acModificaRdL_DelRisorsaTeam.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acModificaRdL_DelRisorsaTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acModificaRdL_DelRisorsaTeam.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11)";
            this.acModificaRdL_DelRisorsaTeam.TargetViewId = "";
            this.acModificaRdL_DelRisorsaTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acModificaRdL_DelRisorsaTeam.ToolTip = null;
            this.acModificaRdL_DelRisorsaTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acModificaRdL_DelRisorsaTeam.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acModificaRdL_DelRisorsaTeam_Execute);
            // 
            // ModificaRdLController
            // 
            this.Actions.Add(this.pupModificaRdL_WinRisorseTeam);
            this.Actions.Add(this.acModificaRdL_DelRisorsaTeam);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.ParametriPopUp.ModificaRdL);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupModificaRdL_WinRisorseTeam;
        private DevExpress.ExpressApp.Actions.SimpleAction acModificaRdL_DelRisorsaTeam;
    }
}
