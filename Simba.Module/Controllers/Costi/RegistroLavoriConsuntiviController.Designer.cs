namespace CAMS.Module.Controllers.Costi
{
    partial class RegistroLavoriConsuntiviController
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
            this.pupWinApprovaClienteC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupWinApprovaClienteC
            // 
            this.pupWinApprovaClienteC.AcceptButtonCaption = null;
            this.pupWinApprovaClienteC.CancelButtonCaption = null;
            this.pupWinApprovaClienteC.Caption = "Approva";
            this.pupWinApprovaClienteC.Category = "Edit";
            this.pupWinApprovaClienteC.ConfirmationMessage = null;
            this.pupWinApprovaClienteC.Id = "pupWinApprovaClienteC";
            this.pupWinApprovaClienteC.ImageName = "BO_Invoice";
            this.pupWinApprovaClienteC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinApprovaClienteC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinApprovaClienteC.TargetObjectsCriteria = "Abilitato == 1";
            this.pupWinApprovaClienteC.ToolTip = "Approvazione del Cliente al Consuntivo";
            this.pupWinApprovaClienteC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinApprovaClienteC_CustomizePopupWindowParams);
            this.pupWinApprovaClienteC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinApprovaClienteC_Execute);
            // 
            // RegistroLavoriConsuntiviController
            // 
            this.Actions.Add(this.pupWinApprovaClienteC);
            this.TargetObjectType = typeof(CAMS.Module.Costi.RegistroLavoriConsuntivi);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinApprovaClienteC;
    }
}
