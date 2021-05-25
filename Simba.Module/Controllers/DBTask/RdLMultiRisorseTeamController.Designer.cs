namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLMultiRisorseTeamController
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
            this.pWinRisorseTeamAltre = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelRisorsaTeamAltro = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // pWinRisorseTeamAltre
            // 
            this.pWinRisorseTeamAltre.AcceptButtonCaption = "Seleziona";
            this.pWinRisorseTeamAltre.CancelButtonCaption = "Annulla";
            this.pWinRisorseTeamAltre.Caption = "Risorse Team";
            this.pWinRisorseTeamAltre.Category = "MyHiddenCategory";
            this.pWinRisorseTeamAltre.ConfirmationMessage = null;
            this.pWinRisorseTeamAltre.Id = "pWinRisorseTeamAltre";
            this.pWinRisorseTeamAltre.ImageName = "Action_Search_Object_FindObjectByID";
            this.pWinRisorseTeamAltre.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.pWinRisorseTeamAltre.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pWinRisorseTeamAltre.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pWinRisorseTeamAltre.ToolTip = null;
            this.pWinRisorseTeamAltre.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pWinRisorseTeamAltre.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pWinRisorseTeamAltre_CustomizePopupWindowParams);
            this.pWinRisorseTeamAltre.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pWinRisorseTeamAltre_Execute);
            // 
            // acDelRisorsaTeamAltro
            // 
            this.acDelRisorsaTeamAltro.Caption = "Del Risorsa Team";
            this.acDelRisorsaTeamAltro.Category = "MyHiddenCategory";
            this.acDelRisorsaTeamAltro.ConfirmationMessage = null;
            this.acDelRisorsaTeamAltro.Id = "acDelRisorsaTeamAltro";
            this.acDelRisorsaTeamAltro.ImageName = "Action_Clear";
            this.acDelRisorsaTeamAltro.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelRisorsaTeamAltro.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelRisorsaTeamAltro.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In(2,11) And RegNEmergenzes.Count() = 0  And Oid > 0" +
    "";
            this.acDelRisorsaTeamAltro.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelRisorsaTeamAltro.ToolTip = null;
            this.acDelRisorsaTeamAltro.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDelRisorsaTeamAltro.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDelRisorsaTeamAltro_Execute);
            // 
            // RdLMultiRisorseTeamController
            // 
            this.Actions.Add(this.pWinRisorseTeamAltre);
            this.Actions.Add(this.acDelRisorsaTeamAltro);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RdLMultiRisorseTeam);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pWinRisorseTeamAltre;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelRisorsaTeamAltro;
    }
}
