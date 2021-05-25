namespace CAMS.Module.Controllers.DBControlliNormativi
{
    partial class LogEmailCtrlNormController
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
            this.ButtonInvio = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ButtonInvio
            // 
            this.ButtonInvio.Caption = "Invio e-mail";
            this.ButtonInvio.ConfirmationMessage = null;
            this.ButtonInvio.Id = "ButtonInvioEmail";
            this.ButtonInvio.TargetObjectType = typeof(CAMS.Module.DBControlliNormativi.LogEmailCtrlNorm);
            this.ButtonInvio.TargetViewId = "";
            this.ButtonInvio.ToolTip = null;
            this.ButtonInvio.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ButtonInvio_Execute);
            // 
            // LogEmailCtrlNormController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBControlliNormativi.LogEmailCtrlNorm);

        }

        #endregion

        public DevExpress.ExpressApp.Actions.SimpleAction ButtonInvio;

    }
}
