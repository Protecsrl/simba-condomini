namespace CAMS.Module.Controllers.DBAgenda
{
    partial class AppuntamentiRisorseController
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
            this.acUpdateRisorseApp = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // acUpdateRisorseApp
            // 
            this.acUpdateRisorseApp.Caption = "Aggiorna Risorse";
            this.acUpdateRisorseApp.ConfirmationMessage = null;
            this.acUpdateRisorseApp.Id = "acUpdateRisorseApp";
            this.acUpdateRisorseApp.ToolTip = null;
            this.acUpdateRisorseApp.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acUpdateRisorseApp_Execute);
            // 
            // AppuntamentiRisorseController
            // 
            this.Actions.Add(this.acUpdateRisorseApp);
            this.TargetObjectType = typeof(CAMS.Module.DBAgenda.AppuntamentiRisorse);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acUpdateRisorseApp;
    }
}
