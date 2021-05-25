namespace CAMS.Module.Controllers.DBPlanner
{
    partial class ScenarioController
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
            this.AzioneStatoScenario = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acAggiornaTempi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // AzioneStatoScenario
            // 
            this.AzioneStatoScenario.Caption = "Azione Stato Scenario";
            this.AzioneStatoScenario.Category = "RecordEdit";
            this.AzioneStatoScenario.ConfirmationMessage = null;
            this.AzioneStatoScenario.Id = "AzioneStatoScenario";
            this.AzioneStatoScenario.ImageName = "Action_ResetPassword";
            this.AzioneStatoScenario.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.AzioneStatoScenario.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.AzioneStatoScenario.TargetObjectsCriteria = "ClusterEdificis.Count>0 And CheckApp";
            this.AzioneStatoScenario.TargetObjectType = typeof(CAMS.Module.DBPlant.Scenario);
            this.AzioneStatoScenario.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.AzioneStatoScenario.ToolTip = null;
            this.AzioneStatoScenario.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.AzioneStatoScenario.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.AzioneStatoScenario_Execute);
            // 
            // acAggiornaTempi
            // 
            this.acAggiornaTempi.Caption = "Aggiorna Tempi";
            this.acAggiornaTempi.ConfirmationMessage = null;
            this.acAggiornaTempi.Id = "acAggiornaTempi";
            this.acAggiornaTempi.TargetObjectsCriteria = "ClusterEdificis.Count>0";
            this.acAggiornaTempi.TargetObjectType = typeof(CAMS.Module.DBPlant.Scenario);
            this.acAggiornaTempi.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acAggiornaTempi.ToolTip = null;
            this.acAggiornaTempi.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acAggiornaTempi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acAggiornaTempi_Execute);
            // 
            // ScenarioController
            // 
            this.Actions.Add(this.AzioneStatoScenario);
            this.Actions.Add(this.acAggiornaTempi);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.Scenario);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction AzioneStatoScenario;
        private DevExpress.ExpressApp.Actions.SimpleAction acAggiornaTempi;

    }
}
