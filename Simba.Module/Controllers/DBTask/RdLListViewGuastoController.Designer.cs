namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLListViewGuastoController
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
            this.popupAvSmistamentoGuasto = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popupAvSmistamentoGuasto
            // 
            this.popupAvSmistamentoGuasto.AcceptButtonCaption = null;
            this.popupAvSmistamentoGuasto.CancelButtonCaption = null;
            this.popupAvSmistamentoGuasto.Caption = "Avanzamento Guasto";
            this.popupAvSmistamentoGuasto.Category = "Edit";
            this.popupAvSmistamentoGuasto.ConfirmationMessage = null;
            this.popupAvSmistamentoGuasto.Id = "popupAvSmistamentoGuasto";
            this.popupAvSmistamentoGuasto.ImageName = "IDE";
            this.popupAvSmistamentoGuasto.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.popupAvSmistamentoGuasto.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popupAvSmistamentoGuasto.TargetObjectsCriteria = "OidSmistamento != 3";
            this.popupAvSmistamentoGuasto.TargetViewId = "";
            this.popupAvSmistamentoGuasto.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.popupAvSmistamentoGuasto.ToolTip = "Avanzamento Stato Smistamento RdL";
            this.popupAvSmistamentoGuasto.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.popupAvSmistamentoGuasto.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupAvSmistamentoGuasto_CustomizePopupWindowParams);
            this.popupAvSmistamentoGuasto.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupAvSmistamentoGuasto_Execute);
            // 
            // RdLListViewGuastoController
            // 
            this.Actions.Add(this.popupAvSmistamentoGuasto);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListViewGuasto);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupAvSmistamentoGuasto;
    }
}
