namespace CAMS.Module.Controllers.DBTask
{
    partial class RisorseController
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
            this.CreaRisorsaTeam = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.AssociaaRisorseTeam = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // CreaRisorsaTeam
            // 
            this.CreaRisorsaTeam.Caption = "Crea Risorsa Team";
            this.CreaRisorsaTeam.ConfirmationMessage = null;
            this.CreaRisorsaTeam.Id = "CreaRisorsaTeam";
            this.CreaRisorsaTeam.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.CreaRisorsaTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.CreaRisorsaTeam.TargetObjectsCriteria = "";
            this.CreaRisorsaTeam.TargetObjectType = typeof(CAMS.Module.DBTask.Risorse);
            this.CreaRisorsaTeam.TargetViewId = "Risorse_DetailView";
            this.CreaRisorsaTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.CreaRisorsaTeam.ToolTip = null;
            this.CreaRisorsaTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.CreaRisorsaTeam.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.CreaRisorsaTeam_Execute);
            // 
            // AssociaaRisorseTeam
            // 
            this.AssociaaRisorseTeam.Caption = "Associa a Team di Risorse ";
            this.AssociaaRisorseTeam.ConfirmationMessage = "Vuoi Associare questa risorsa come coppia Linkata \r\nal Team selezionato?";
            this.AssociaaRisorseTeam.Id = "AssociaaRisorseTeam";
            this.AssociaaRisorseTeam.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.AssociaaRisorseTeam.TargetObjectsCriteria = "";
            this.AssociaaRisorseTeam.TargetObjectType = typeof(CAMS.Module.DBTask.Risorse);
            this.AssociaaRisorseTeam.TargetViewId = "Risorse_DetailView";
            this.AssociaaRisorseTeam.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AssociaaRisorseTeam.ToolTip = "Vuoi Associare a Team  Come coppia LinKata?";
            this.AssociaaRisorseTeam.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AssociaaRisorseTeam.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.AssociaaRisorseTeam_Execute);
            // 
            // RisorseController
            // 
            this.Actions.Add(this.CreaRisorsaTeam);
            this.Actions.Add(this.AssociaaRisorseTeam);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Risorse);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction CreaRisorsaTeam;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction AssociaaRisorseTeam;
    }
}
