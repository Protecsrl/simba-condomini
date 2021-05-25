namespace CAMS.Module.Controllers.DBTask.ParametriPopUp
{
    partial class RdLParametriAzioniMassiveController
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
            this.PopWinMassivoAssegnaTRisorse = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // PopWinMassivoAssegnaTRisorse
            // 
            this.PopWinMassivoAssegnaTRisorse.AcceptButtonCaption = null;
            this.PopWinMassivoAssegnaTRisorse.CancelButtonCaption = null;
            this.PopWinMassivoAssegnaTRisorse.Caption = "Ricerca Team";
            this.PopWinMassivoAssegnaTRisorse.ConfirmationMessage = null;
            this.PopWinMassivoAssegnaTRisorse.Id = "PopWinMassivoAssegnaTRisorse";
            this.PopWinMassivoAssegnaTRisorse.TargetObjectType = typeof(CAMS.Module.DBTask.ParametriPopUp.RdLParametriAzioniMassive);
            this.PopWinMassivoAssegnaTRisorse.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.PopWinMassivoAssegnaTRisorse.ToolTip = null;
            this.PopWinMassivoAssegnaTRisorse.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.PopWinMassivoAssegnaTRisorse.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.PopWinMassivoAssegnaTRisorse_CustomizePopupWindowParams);
            this.PopWinMassivoAssegnaTRisorse.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PopWinMassivoAssegnaTRisorse_Execute);
            // 
            // RdLParametriAzioniMassiveController
            // 
            this.Actions.Add(this.PopWinMassivoAssegnaTRisorse);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.ParametriPopUp.RdLParametriAzioniMassive);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PopWinMassivoAssegnaTRisorse;
    }
}
