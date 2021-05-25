namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLListViewSGController
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
            this.scaReportSG = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // scaReportSG
            // 
            this.scaReportSG.Caption = "Report";
            this.scaReportSG.ConfirmationMessage = null;
            this.scaReportSG.Id = "scaReportSG";
            this.scaReportSG.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.scaReportSG.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.scaReportSG.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.scaReportSG.ToolTip = null;
            this.scaReportSG.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.scaReportSG.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaReportSG_Execute);
            // 
            // RdLListViewSGController
            // 
            this.Actions.Add(this.scaReportSG);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaReportSG;
    }
}
