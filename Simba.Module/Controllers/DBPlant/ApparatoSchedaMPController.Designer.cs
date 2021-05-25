namespace CAMS.Module.Controllers.DBPlant
{
    partial class ApparatoSchedaMPController
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
            this.pupWinInsetSchedeMP = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupWinInsetSchedeMP
            // 
            this.pupWinInsetSchedeMP.AcceptButtonCaption = null;
            this.pupWinInsetSchedeMP.CancelButtonCaption = null;
            this.pupWinInsetSchedeMP.Caption = "Inserisci Schede MP";
            this.pupWinInsetSchedeMP.Category = "Edit";
            this.pupWinInsetSchedeMP.ConfirmationMessage = null;
            this.pupWinInsetSchedeMP.Id = "pupWinInsetSchedeMP";
            this.pupWinInsetSchedeMP.TargetViewId = "Apparato_AppSchedaMpes_ListView";
            this.pupWinInsetSchedeMP.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupWinInsetSchedeMP.ToolTip = null;
            this.pupWinInsetSchedeMP.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupWinInsetSchedeMP.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinInsetSchedeMP_CustomizePopupWindowParams);
            this.pupWinInsetSchedeMP.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinInsetSchedeMP_Execute);
            // 
            // ApparatoSchedaMPController
            // 
            this.Actions.Add(this.pupWinInsetSchedeMP);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.AssetSchedaMP);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinInsetSchedeMP;
    }
}
