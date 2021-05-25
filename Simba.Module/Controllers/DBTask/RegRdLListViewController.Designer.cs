namespace CAMS.Module.Controllers.DBTask
{
    partial class RegRdLListViewController
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
            this.acReportRegRdLListView = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acVisualizzaDettagliRegRdLListViev = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.EditRegRdLLVAction = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // acReportRegRdLListView
            // 
            this.acReportRegRdLListView.Caption = "Report";
            this.acReportRegRdLListView.Category = "Reports";
            this.acReportRegRdLListView.ConfirmationMessage = null;
            this.acReportRegRdLListView.Id = "acReportRegRdLListView";
            this.acReportRegRdLListView.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.acReportRegRdLListView.TargetViewId = "";
            this.acReportRegRdLListView.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.acReportRegRdLListView.ToolTip = null;
            this.acReportRegRdLListView.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.acReportRegRdLListView.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acReportRegRdLListView_Execute);
            // 
            // acVisualizzaDettagliRegRdLListViev
            // 
            this.acVisualizzaDettagliRegRdLListViev.Caption = "Visualizza Dettagli";
            this.acVisualizzaDettagliRegRdLListViev.Category = "View";
            this.acVisualizzaDettagliRegRdLListViev.ConfirmationMessage = null;
            this.acVisualizzaDettagliRegRdLListViev.Id = "acVisualizzaDettagliRegRdLListViev";
            this.acVisualizzaDettagliRegRdLListViev.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.acVisualizzaDettagliRegRdLListViev.ToolTip = null;
            this.acVisualizzaDettagliRegRdLListViev.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.acVisualizzaDettagliRegRdLListViev_Execute);
            // 
            // EditRegRdLLVAction
            // 
            this.EditRegRdLLVAction.Caption = "Edit RegRdL";
            this.EditRegRdLLVAction.Category = "Edit";
            this.EditRegRdLLVAction.ConfirmationMessage = null;
            this.EditRegRdLLVAction.Id = "EditRegRdLLVAction";
            this.EditRegRdLLVAction.ImageName = "MenuBar_Edit";
            this.EditRegRdLLVAction.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.EditRegRdLLVAction.ToolTip = "Modifica il Registro di RdL Selezionato";
            this.EditRegRdLLVAction.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.EditRegRdLLVAction_Execute);
            // 
            // RegRdLListViewController
            // 
            this.Actions.Add(this.acReportRegRdLListView);
            this.Actions.Add(this.acVisualizzaDettagliRegRdLListViev);
            this.Actions.Add(this.EditRegRdLLVAction);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RegRdLListView);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acReportRegRdLListView;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction acVisualizzaDettagliRegRdLListViev;
        private DevExpress.ExpressApp.Actions.SimpleAction EditRegRdLLVAction;
    }
}
