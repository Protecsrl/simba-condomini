namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLNonPersistentController
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
            this.pupWinRisorseTeam = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelRisorsaTeam = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinRisorseTeamDC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acStoricoRdL = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acDCReport = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // pupWinRisorseTeam
            // 
            this.pupWinRisorseTeam.AcceptButtonCaption = "Seleziona";
            this.pupWinRisorseTeam.CancelButtonCaption = "Annulla";
            this.pupWinRisorseTeam.Caption = "Team";
            this.pupWinRisorseTeam.Category = "MyHiddenCategory";
            this.pupWinRisorseTeam.ConfirmationMessage = null;
            this.pupWinRisorseTeam.Id = "pupWinRisorseTeam";
            this.pupWinRisorseTeam.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinRisorseTeam.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.pupWinRisorseTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinRisorseTeam.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11) And Oid > 0 And !IsMP_count";
            this.pupWinRisorseTeam.TargetViewId = "RdL_DetailView;RdL_DetailView_Gestione";
            this.pupWinRisorseTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinRisorseTeam.ToolTip = null;
            this.pupWinRisorseTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinRisorseTeam.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinRisorseTeamDC_CustomizePopupWindowParams);
            this.pupWinRisorseTeam.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinRisorseTeamDC_Execute);
            // 
            // acDelRisorsaTeam
            // 
            this.acDelRisorsaTeam.Caption = "Del Risorsa Team";
            this.acDelRisorsaTeam.Category = "MyHiddenCategory";
            this.acDelRisorsaTeam.ConfirmationMessage = null;
            this.acDelRisorsaTeam.Id = "acDelRisorsaTeam";
            this.acDelRisorsaTeam.ImageName = "Action_Clear";
            this.acDelRisorsaTeam.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelRisorsaTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelRisorsaTeam.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11) And Oid > 0 And !IsMP_count";
            this.acDelRisorsaTeam.TargetViewId = "RdL_DetailView;RdL_DetailView_Gestione";
            this.acDelRisorsaTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelRisorsaTeam.ToolTip = null;
            this.acDelRisorsaTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDelRisorsaTeam.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDelRisorsaTeam_Execute);
            // 
            // pupWinRisorseTeamDC
            // 
            this.pupWinRisorseTeamDC.AcceptButtonCaption = null;
            this.pupWinRisorseTeamDC.CancelButtonCaption = null;
            this.pupWinRisorseTeamDC.Caption = "Risorse Team DC";
            this.pupWinRisorseTeamDC.Category = "Edit";
            this.pupWinRisorseTeamDC.ConfirmationMessage = null;
            this.pupWinRisorseTeamDC.Id = "pupWinRisorseTeamDC";
            this.pupWinRisorseTeamDC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinRisorseTeamDC.TargetViewId = "no";
            this.pupWinRisorseTeamDC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinRisorseTeamDC.ToolTip = null;
            this.pupWinRisorseTeamDC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinRisorseTeamDC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinRisorseTeamDC_CustomizePopupWindowParams);
            this.pupWinRisorseTeamDC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinRisorseTeamDC_Execute);
            // 
            // acStoricoRdL
            // 
            this.acStoricoRdL.Caption = "Storico RdL";
            this.acStoricoRdL.ConfirmationMessage = null;
            this.acStoricoRdL.Id = "acStoricoRdL";
            this.acStoricoRdL.TargetViewId = "no";
            this.acStoricoRdL.ToolTip = null;
            this.acStoricoRdL.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acStoricoRdL_Execute);
            // 
            // acDCReport
            // 
            this.acDCReport.Caption = "Report";
            this.acDCReport.ConfirmationMessage = null;
            this.acDCReport.Id = "acDCReport";
            this.acDCReport.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.acDCReport.TargetObjectsCriteria = "Oid > 0";
            this.acDCReport.TargetViewId = "RdL_DetailView_Gestione;RdL_DetailView_Cliente";
            this.acDCReport.ToolTip = null;
            this.acDCReport.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDCReport_Execute);
            // 
            // RdLNonPersistentController
            // 
            this.Actions.Add(this.pupWinRisorseTeam);
            this.Actions.Add(this.acDelRisorsaTeam);
            this.Actions.Add(this.pupWinRisorseTeamDC);
            this.Actions.Add(this.acStoricoRdL);
            this.Actions.Add(this.acDCReport);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RdL);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinRisorseTeam;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelRisorsaTeam;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinRisorseTeamDC;
        private DevExpress.ExpressApp.Actions.SimpleAction acStoricoRdL;
        private DevExpress.ExpressApp.Actions.SimpleAction acDCReport;
    }
}
