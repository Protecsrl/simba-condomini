namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLListViewAzioniMassiveController
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
            this.PupWinCompletamento = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.singleChoiceAction1 = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // PupWinCompletamento
            // 
            this.PupWinCompletamento.AcceptButtonCaption = null;
            this.PupWinCompletamento.CancelButtonCaption = "Completamento";
            this.PupWinCompletamento.Caption = "Completamento Massivo";
            this.PupWinCompletamento.Category = "Edit";
            this.PupWinCompletamento.ConfirmationMessage = null;
            this.PupWinCompletamento.Id = "PupWinCompletamento";
            this.PupWinCompletamento.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.PupWinCompletamento.TargetViewId = "RdLListViewAzioniMassive_ListView";
            this.PupWinCompletamento.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.PupWinCompletamento.ToolTip = null;
            this.PupWinCompletamento.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.PupWinCompletamento.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.PupWinCompletamento_CustomizePopupWindowParams);
            this.PupWinCompletamento.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PupWinCompletamento_Execute);
            // 
            // singleChoiceAction1
            // 
            this.singleChoiceAction1.Caption = null;
            this.singleChoiceAction1.ConfirmationMessage = null;
            this.singleChoiceAction1.Id = "74e85549-ca70-4a65-9a93-c056e155d8c1";
            this.singleChoiceAction1.ToolTip = null;
            // 
            // RdLListViewAzioniMassiveController
            // 
            this.Actions.Add(this.PupWinCompletamento);
            this.Actions.Add(this.singleChoiceAction1);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListViewAzioniMassive);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PupWinCompletamento;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction singleChoiceAction1;
    }
}
