namespace CAMS.Module.Controllers.DBTask
{
    partial class RisorseTeamController
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
            this.CreaCoppiaLinkata = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.RilasciaRisorse = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.CreaCoppiaLinkataPar = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // CreaCoppiaLinkata
            // 
            this.CreaCoppiaLinkata.Caption = "Crea Coppia Linkata";
            this.CreaCoppiaLinkata.ConfirmationMessage = null;
            this.CreaCoppiaLinkata.Id = "CreaCoppiaLinkata";
            this.CreaCoppiaLinkata.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.CreaCoppiaLinkata.TargetObjectType = typeof(CAMS.Module.DBTask.RisorseTeam);
            this.CreaCoppiaLinkata.TargetViewId = "RisorseTeam_ListView";
            this.CreaCoppiaLinkata.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.CreaCoppiaLinkata.ToolTip = null;
            this.CreaCoppiaLinkata.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.CreaCoppiaLinkata.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreaCoppiaLinkata_Execute);
            // 
            // RilasciaRisorse
            // 
            this.RilasciaRisorse.Caption = "Rilascia Risorsa Coppia Linkata";
            this.RilasciaRisorse.ConfirmationMessage = null;
            this.RilasciaRisorse.Id = "RilasciaRisorse";
            this.RilasciaRisorse.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.RilasciaRisorse.TargetObjectsCriteria = "";
            this.RilasciaRisorse.TargetObjectType = typeof(CAMS.Module.DBTask.RisorseTeam);
            this.RilasciaRisorse.TargetViewId = "RisorseTeam_DetailView";
            this.RilasciaRisorse.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.RilasciaRisorse.ToolTip = null;
            this.RilasciaRisorse.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.RilasciaRisorse.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.RilasciaRisorse_Execute);
            // 
            // CreaCoppiaLinkataPar
            // 
            this.CreaCoppiaLinkataPar.Caption = "Crea Coppia Linkata Con";
            this.CreaCoppiaLinkataPar.ConfirmationMessage = "Creare la coppia LinKata?";
            this.CreaCoppiaLinkataPar.Id = "CreaCoppiaLinkataPar";
            this.CreaCoppiaLinkataPar.TargetObjectsCriteria = "";
            this.CreaCoppiaLinkataPar.TargetObjectType = typeof(CAMS.Module.DBTask.RisorseTeam);
            this.CreaCoppiaLinkataPar.TargetViewId = "RisorseTeam_DetailView";
            this.CreaCoppiaLinkataPar.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.CreaCoppiaLinkataPar.ToolTip = null;
            this.CreaCoppiaLinkataPar.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.CreaCoppiaLinkataPar.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.CreaCoppiaLinkataPar_Execute);
            // 
            // RisorseTeamController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RisorseTeam);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.ViewControlsCreated += new System.EventHandler(this.RisorseTeamController_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreaCoppiaLinkata;
        private DevExpress.ExpressApp.Actions.SimpleAction RilasciaRisorse;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction CreaCoppiaLinkataPar;
    }
}
