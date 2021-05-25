namespace CAMS.Module.Controllers.DBTask
{
    partial class RegistroRdLController
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
            this.MigrazioneRegRdLMpinTT = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acRegistroCostiRicaviRegRdL = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acReportRegRdL = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupRegRdL_WinRisorseTeamDC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acRegRdL_DelRisorsaTeam = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.popRegRdL_GetDCRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // MigrazioneRegRdLMpinTT
            // 
            this.MigrazioneRegRdLMpinTT.Caption = "Migrazione MP in TT";
            this.MigrazioneRegRdLMpinTT.ConfirmationMessage = null;
            this.MigrazioneRegRdLMpinTT.Id = "MigrazioneRegRdLMpinTT";
            this.MigrazioneRegRdLMpinTT.TargetViewId = "novisibile";
            this.MigrazioneRegRdLMpinTT.ToolTip = null;
            // 
            // acRegistroCostiRicaviRegRdL
            // 
            this.acRegistroCostiRicaviRegRdL.Caption = "Registro Costi";
            this.acRegistroCostiRicaviRegRdL.ConfirmationMessage = null;
            this.acRegistroCostiRicaviRegRdL.Id = "acRegistroCostiRicaviRegRdL";
            this.acRegistroCostiRicaviRegRdL.TargetViewId = "RegistroRdL_DetailView";
            this.acRegistroCostiRicaviRegRdL.ToolTip = null;
            this.acRegistroCostiRicaviRegRdL.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acRegistroCostiRicaviRegRdL_Execute);
            // 
            // acReportRegRdL
            // 
            this.acReportRegRdL.Caption = "Report";
            this.acReportRegRdL.ConfirmationMessage = null;
            this.acReportRegRdL.Id = "acReportRegRdL";
            this.acReportRegRdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.acReportRegRdL.TargetObjectsCriteria = "Oid > 0";
            this.acReportRegRdL.ToolTip = null;
            this.acReportRegRdL.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acreport_Execute);
            // 
            // pupRegRdL_WinRisorseTeamDC
            // 
            this.pupRegRdL_WinRisorseTeamDC.AcceptButtonCaption = "Seleziona";
            this.pupRegRdL_WinRisorseTeamDC.CancelButtonCaption = "Annulla";
            this.pupRegRdL_WinRisorseTeamDC.Caption = "Team";
            this.pupRegRdL_WinRisorseTeamDC.Category = "MyHiddenCategory";
            this.pupRegRdL_WinRisorseTeamDC.ConfirmationMessage = null;
            this.pupRegRdL_WinRisorseTeamDC.Id = "pupRegRdL_WinRisorseTeamDC";
            this.pupRegRdL_WinRisorseTeamDC.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupRegRdL_WinRisorseTeamDC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.pupRegRdL_WinRisorseTeamDC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupRegRdL_WinRisorseTeamDC.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11) And Oid > 0";
            this.pupRegRdL_WinRisorseTeamDC.TargetViewId = "RegistroRdL_DetailView";
            this.pupRegRdL_WinRisorseTeamDC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupRegRdL_WinRisorseTeamDC.ToolTip = null;
            this.pupRegRdL_WinRisorseTeamDC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupRegRdL_WinRisorseTeamDC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupRegRdL_WinRisorseTeamDC_CustomizePopupWindowParams);
            this.pupRegRdL_WinRisorseTeamDC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupRegRdL_WinRisorseTeamDC_Execute);
            // 
            // acRegRdL_DelRisorsaTeam
            // 
            this.acRegRdL_DelRisorsaTeam.Caption = "Del Risorsa Team";
            this.acRegRdL_DelRisorsaTeam.Category = "MyHiddenCategory";
            this.acRegRdL_DelRisorsaTeam.ConfirmationMessage = null;
            this.acRegRdL_DelRisorsaTeam.Id = "acRegRdL_DelRisorsaTeam";
            this.acRegRdL_DelRisorsaTeam.ImageName = "Action_Clear";
            this.acRegRdL_DelRisorsaTeam.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acRegRdL_DelRisorsaTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acRegRdL_DelRisorsaTeam.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11) And Oid > 0";
            this.acRegRdL_DelRisorsaTeam.TargetViewId = "RegistroRdL_DetailView";
            this.acRegRdL_DelRisorsaTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acRegRdL_DelRisorsaTeam.ToolTip = null;
            this.acRegRdL_DelRisorsaTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acRegRdL_DelRisorsaTeam.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acRegRdL_DelRisorsaTeam_Execute);
            // 
            // popRegRdL_GetDCRdL
            // 
            this.popRegRdL_GetDCRdL.AcceptButtonCaption = null;
            this.popRegRdL_GetDCRdL.CancelButtonCaption = null;
            this.popRegRdL_GetDCRdL.Caption = "Ricerca RdL da Riassegnare";
            this.popRegRdL_GetDCRdL.Category = "Edit";
            this.popRegRdL_GetDCRdL.ConfirmationMessage = null;
            this.popRegRdL_GetDCRdL.Id = "popRegRdL_GetDCRdL";
            this.popRegRdL_GetDCRdL.ImageName = "Action_Search_Object_FindObjectByID";
            this.popRegRdL_GetDCRdL.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.popRegRdL_GetDCRdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.popRegRdL_GetDCRdL.TargetObjectsCriteria = "UltimoStatoSmistamento.Oid In (1,2,11) And Categoria.Oid In(1,5)";
            this.popRegRdL_GetDCRdL.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popRegRdL_GetDCRdL.ToolTip = null;
            this.popRegRdL_GetDCRdL.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popRegRdL_GetDCRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popRegRdL_GetDCRdL_CustomizePopupWindowParams);
            this.popRegRdL_GetDCRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popRegRdL_GetDCRdL_Execute);
            // 
            // RegistroRdLController
            // 
            this.Actions.Add(this.MigrazioneRegRdLMpinTT);
            this.Actions.Add(this.acRegistroCostiRicaviRegRdL);
            this.Actions.Add(this.acReportRegRdL);
            this.Actions.Add(this.pupRegRdL_WinRisorseTeamDC);
            this.Actions.Add(this.acRegRdL_DelRisorsaTeam);
            this.Actions.Add(this.popRegRdL_GetDCRdL);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RegistroRdL);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction MigrazioneRegRdLMpinTT;
        private DevExpress.ExpressApp.Actions.SimpleAction acRegistroCostiRicaviRegRdL;
        private DevExpress.ExpressApp.Actions.SimpleAction acReportRegRdL;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupRegRdL_WinRisorseTeamDC;
        private DevExpress.ExpressApp.Actions.SimpleAction acRegRdL_DelRisorsaTeam;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popRegRdL_GetDCRdL;
    }
}
