namespace CAMS.Module.Controllers
{
    partial class VisualizzaDettagliController
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
            this.acVisualizzaDettagliAll = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.popupWinRigistroOperativoRisorse = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // acVisualizzaDettagliAll
            // 
            this.acVisualizzaDettagliAll.Caption = "Visualizza Dettagli";
            this.acVisualizzaDettagliAll.Category = "View";
            this.acVisualizzaDettagliAll.ConfirmationMessage = null;
            this.acVisualizzaDettagliAll.Id = "acVisualizzaDettagliAll";
            this.acVisualizzaDettagliAll.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.acVisualizzaDettagliAll.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.acVisualizzaDettagliAll.ToolTip = null;
            this.acVisualizzaDettagliAll.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.acVisualizzaDettagli_Execute);
            // 
            // popupWinRigistroOperativoRisorse
            // 
            this.popupWinRigistroOperativoRisorse.AcceptButtonCaption = null;
            this.popupWinRigistroOperativoRisorse.CancelButtonCaption = null;
            this.popupWinRigistroOperativoRisorse.Caption = "Registro Operativo Risorse";
            this.popupWinRigistroOperativoRisorse.ConfirmationMessage = null;
            this.popupWinRigistroOperativoRisorse.Id = "popupWinRigistroOperativoRisorse";
            this.popupWinRigistroOperativoRisorse.TargetViewId = "RisorseTeam_DetailView;Risorse_DetailView";
            this.popupWinRigistroOperativoRisorse.ToolTip = null;
            this.popupWinRigistroOperativoRisorse.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popupWinRigistroOperativoRisorse_CustomizePopupWindowParams);
            this.popupWinRigistroOperativoRisorse.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popupWinRigistroOperativoRisorse_Execute);
            // 
            // VisualizzaDettagliController
            // 
            this.Actions.Add(this.acVisualizzaDettagliAll);
            this.Actions.Add(this.popupWinRigistroOperativoRisorse);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction acVisualizzaDettagliAll;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popupWinRigistroOperativoRisorse;
    }
}
