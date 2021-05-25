namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLSinotticoGroupController
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
            this.popWinselectCommesse = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.scaSelectCommesse = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // popWinselectCommesse
            // 
            this.popWinselectCommesse.AcceptButtonCaption = null;
            this.popWinselectCommesse.CancelButtonCaption = null;
            this.popWinselectCommesse.Caption = "pop Winselect Commesse";
            this.popWinselectCommesse.ConfirmationMessage = null;
            this.popWinselectCommesse.Id = "popWinselectCommesse";
            this.popWinselectCommesse.TargetObjectsCriteria = "Oid=-1";
            this.popWinselectCommesse.ToolTip = null;
            this.popWinselectCommesse.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popWinselectCommesse_CustomizePopupWindowParams);
            this.popWinselectCommesse.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popWinselectCommesse_Execute);
            // 
            // scaSelectCommesse
            // 
            this.scaSelectCommesse.Caption = "c9b81bba-4ff4-4339-bd72-a9123f443a54";
            this.scaSelectCommesse.ConfirmationMessage = null;
            this.scaSelectCommesse.Id = "c9b81bba-4ff4-4339-bd72-a9123f443a54";
            this.scaSelectCommesse.ToolTip = null;
            // 
            // RdLSinotticoGroupController
            // 
            this.Actions.Add(this.popWinselectCommesse);
            this.Actions.Add(this.scaSelectCommesse);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLSinotticoGroup);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popWinselectCommesse;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaSelectCommesse;
    }
}
