namespace CAMS.Module.Controllers.DBTask.AppsCAMS
{
    partial class RegNotificheEmergenzeController
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
            this.RegNotificaEmergAssegnaTeams = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // RegNotificaEmergAssegnaTeams
            // 
            this.RegNotificaEmergAssegnaTeams.AcceptButtonCaption = "Assegna Risorse";
            this.RegNotificaEmergAssegnaTeams.CancelButtonCaption = null;
            this.RegNotificaEmergAssegnaTeams.Caption = "Assegna Teams";
            this.RegNotificaEmergAssegnaTeams.Category = "RecordEdit";
            this.RegNotificaEmergAssegnaTeams.ConfirmationMessage = null;
            this.RegNotificaEmergAssegnaTeams.Id = "RegNotificaEmergAssegnaTeams";
            this.RegNotificaEmergAssegnaTeams.ImageName = "Action_LinkUnlink_Link";
            this.RegNotificaEmergAssegnaTeams.IsSizeable = false;
            this.RegNotificaEmergAssegnaTeams.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.RegNotificaEmergAssegnaTeams.TargetObjectType = typeof(CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze);
            this.RegNotificaEmergAssegnaTeams.TargetViewId = "";
            this.RegNotificaEmergAssegnaTeams.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.RegNotificaEmergAssegnaTeams.ToolTip = "Assegna team al Registro Emergenze Selezionato";
            this.RegNotificaEmergAssegnaTeams.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.RegNotificaEmergAssegnaTeams.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.RdLAssegnaTeam_CustomizePopupWindowParams);
            this.RegNotificaEmergAssegnaTeams.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.RdLAssegnaTeam_Execute);
            // 
            // RegNotificheEmergenzeController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction RegNotificaEmergAssegnaTeams;

    }
}
