namespace CAMS.Module.Controllers.DBControlliNormativi
{
    partial class ControlliNormativiRifLogController
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
            this.ReInviaMail = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ReInviaMail
            // 
            this.ReInviaMail.Caption = "Re Invia Mail";
            this.ReInviaMail.ConfirmationMessage = "Vuoi Re-Inviare Questo Avviso Mail?";
            this.ReInviaMail.Id = "ReInviaMail";
            this.ReInviaMail.ImageName = "NewMail";
            this.ReInviaMail.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.ReInviaMail.TargetObjectType = typeof(CAMS.Module.DBControlliNormativi.ControlliNormativiRifLog);
            this.ReInviaMail.TargetViewId = "ControlliNormativi_ControlliNormativiRifLogS_ListView";
            this.ReInviaMail.ToolTip = null;
            this.ReInviaMail.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ReInviaMail_Execute);
            // 
            // ControlliNormativiRifLogController
            // 
            this.Actions.Add(this.ReInviaMail);
            this.TargetObjectType = typeof(CAMS.Module.DBControlliNormativi.ControlliNormativiRifLog);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction ReInviaMail;
    }
}
