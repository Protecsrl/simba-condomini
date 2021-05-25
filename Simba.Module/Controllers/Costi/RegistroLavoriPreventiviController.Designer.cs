namespace CAMS.Module.Controllers.Costi
{
    partial class RegistroLavoriPreventiviController
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
            this.pupWinApprovazioneClienteP = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupWinApprovazioneClienteP
            // 
            this.pupWinApprovazioneClienteP.AcceptButtonCaption = null;
            this.pupWinApprovazioneClienteP.CancelButtonCaption = null;
            this.pupWinApprovazioneClienteP.Caption = "Approva";
            this.pupWinApprovazioneClienteP.Category = "Edit";
            this.pupWinApprovazioneClienteP.ConfirmationMessage = null;
            this.pupWinApprovazioneClienteP.Id = "pupWinApprovazioneClienteP";
            this.pupWinApprovazioneClienteP.ImageName = "BO_Invoice";
            this.pupWinApprovazioneClienteP.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinApprovazioneClienteP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinApprovazioneClienteP.TargetObjectsCriteria = "Abilitato == 1";
            this.pupWinApprovazioneClienteP.ToolTip = "Approvazione del Cliente al Preventivo";
            this.pupWinApprovazioneClienteP.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinApprovazioneClienteP_CustomizePopupWindowParams);
            this.pupWinApprovazioneClienteP.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinApprovazioneClienteP_Execute);
            // 
            // RegistroLavoriPreventiviController
            // 
            this.Actions.Add(this.pupWinApprovazioneClienteP);
            this.TargetObjectType = typeof(CAMS.Module.Costi.RegistroLavoriPreventivi);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinApprovazioneClienteP;
    }
}
