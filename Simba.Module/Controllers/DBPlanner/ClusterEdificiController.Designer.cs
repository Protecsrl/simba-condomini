namespace CAMS.Module.Controllers.DBPlanner
{
    partial class ClusterEdificiController
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
            this.cpwAssociaClustereEdifici = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.cpwAssociaRisorseTeam = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // cpwAssociaClustereEdifici
            // 
            this.cpwAssociaClustereEdifici.AcceptButtonCaption = null;
            this.cpwAssociaClustereEdifici.CancelButtonCaption = null;
            this.cpwAssociaClustereEdifici.Caption = "Associa Clustere Edifici";
            this.cpwAssociaClustereEdifici.ConfirmationMessage = null;
            this.cpwAssociaClustereEdifici.Id = "cpwAssociaClustereEdifici";
            this.cpwAssociaClustereEdifici.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.cpwAssociaClustereEdifici.TargetObjectType = typeof(CAMS.Module.DBPlant.ClusterEdifici);
            this.cpwAssociaClustereEdifici.TargetViewId = "Scenario_ClusterEdificis_ListView";
            this.cpwAssociaClustereEdifici.ToolTip = null;
            this.cpwAssociaClustereEdifici.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.cpwAssociaClustereEdifici_CustomizePopupWindowParams);
            this.cpwAssociaClustereEdifici.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.cpwAssociaClustereEdifici_Execute);
            // 
            // cpwAssociaRisorseTeam
            // 
            this.cpwAssociaRisorseTeam.AcceptButtonCaption = null;
            this.cpwAssociaRisorseTeam.CancelButtonCaption = null;
            this.cpwAssociaRisorseTeam.Caption = "Associa Risorse Team ";
            this.cpwAssociaRisorseTeam.ConfirmationMessage = null;
            this.cpwAssociaRisorseTeam.Id = "cpwAssociaRisorseTeam";
            this.cpwAssociaRisorseTeam.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.cpwAssociaRisorseTeam.TargetObjectType = typeof(CAMS.Module.DBPlant.ClusterEdifici);
            this.cpwAssociaRisorseTeam.TargetViewId = "Scenario_ClusterEdificis_ListView";
            this.cpwAssociaRisorseTeam.ToolTip = null;
            this.cpwAssociaRisorseTeam.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.cpwAssociaRisorseTeam_CustomizePopupWindowParams);
            this.cpwAssociaRisorseTeam.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.cpwAssociaRisorseTeam_Execute);
            // 
            // ClusterEdificiController
            // 
            this.Actions.Add(this.cpwAssociaClustereEdifici);
            this.Actions.Add(this.cpwAssociaRisorseTeam);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.ClusterEdifici);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction cpwAssociaClustereEdifici;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction cpwAssociaRisorseTeam;
    }
}
