namespace CAMS.Module.Controllers.DBPlanner
{
    partial class RegMPSchedaMPController
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
            this.pupWinAggregazioneRegRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinAggregazioneRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupWinAggregazioneRegRdL
            // 
            this.pupWinAggregazioneRegRdL.AcceptButtonCaption = null;
            this.pupWinAggregazioneRegRdL.CancelButtonCaption = null;
            this.pupWinAggregazioneRegRdL.Caption = " Agg Reg RdL";
            this.pupWinAggregazioneRegRdL.ConfirmationMessage = null;
            this.pupWinAggregazioneRegRdL.Id = "pupWinAggregazioneRegRdL";
            this.pupWinAggregazioneRegRdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.pupWinAggregazioneRegRdL.ToolTip = null;
            this.pupWinAggregazioneRegRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinAggregazioneRegRdL_CustomizePopupWindowParams);
            this.pupWinAggregazioneRegRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinAggregazioneRegRdL_Execute);
            // 
            // pupWinAggregazioneRdL
            // 
            this.pupWinAggregazioneRdL.AcceptButtonCaption = null;
            this.pupWinAggregazioneRdL.CancelButtonCaption = null;
            this.pupWinAggregazioneRdL.Caption = " Agg RdL";
            this.pupWinAggregazioneRdL.ConfirmationMessage = null;
            this.pupWinAggregazioneRdL.Id = "pupWinAggregazioneRdL";
            this.pupWinAggregazioneRdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.pupWinAggregazioneRdL.ToolTip = null;
            this.pupWinAggregazioneRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinAggregazioneRdL_CustomizePopupWindowParams);
            this.pupWinAggregazioneRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinAggregazioneRdL_Execute);
            // 
            // RegMPSchedaMPController
            // 
            this.Actions.Add(this.pupWinAggregazioneRegRdL);
            this.Actions.Add(this.pupWinAggregazioneRdL);
            this.TargetObjectType = typeof(CAMS.Module.DBPlanner.RegMPSchedaMP);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinAggregazioneRegRdL;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinAggregazioneRdL;
    }
}
