namespace CAMS.Module.Controllers
{
    partial class CreaUserRuoloController
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
            this.popupWinSelezionaEdificio = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupWinSelezionaEdificio
            // 
            this.popupWinSelezionaEdificio.AcceptButtonCaption = null;
            this.popupWinSelezionaEdificio.CancelButtonCaption = null;
            this.popupWinSelezionaEdificio.Caption = "popup Win Seleziona Immobile";
            this.popupWinSelezionaEdificio.ConfirmationMessage = null;
            this.popupWinSelezionaEdificio.Id = "popupWinSelezionaEdificio";
            this.popupWinSelezionaEdificio.ToolTip = null;
            this.popupWinSelezionaEdificio.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWinSelezionaEdificio_CustomizePopupWindowParams);
            this.popupWinSelezionaEdificio.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWinSelezionaEdificio_Execute);
            this.popupWinSelezionaEdificio.Cancel += new System.EventHandler(this.popupWinSelezionaEdificio_Cancel);
            // 
            // CreaUserRuoloController
            // 
            this.Actions.Add(this.popupWinSelezionaEdificio);
            this.TargetObjectType = typeof(CAMS.Module.DBRuoloUtente.ClonaRuoloUser);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWinSelezionaEdificio;
    }
}
