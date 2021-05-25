namespace CAMS.Module.Controllers.DBAngrafica
{
    partial class ReportExcelController
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
            this.pupGetPropertyObjTypeLookUpRExcel = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupGetPropertyObjTypeRExcel = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupGetPropertyObjTypeLookUpRExcel
            // 
            this.pupGetPropertyObjTypeLookUpRExcel.AcceptButtonCaption = null;
            this.pupGetPropertyObjTypeLookUpRExcel.CancelButtonCaption = null;
            this.pupGetPropertyObjTypeLookUpRExcel.Caption = "Seleziona Campo di Join Filtro";
            this.pupGetPropertyObjTypeLookUpRExcel.Category = "MyHiddenCategory";
            this.pupGetPropertyObjTypeLookUpRExcel.ConfirmationMessage = null;
            this.pupGetPropertyObjTypeLookUpRExcel.Id = "pupGetPropertyObjTypeLookUpRExcel";
            this.pupGetPropertyObjTypeLookUpRExcel.ToolTip = null;
            this.pupGetPropertyObjTypeLookUpRExcel.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupGetPropertyObjTypeLookUpRExcel_CustomizePopupWindowParams);
            this.pupGetPropertyObjTypeLookUpRExcel.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupGetPropertyObjTypeLookUpRExcel_Execute);
            // 
            // pupGetPropertyObjTypeRExcel
            // 
            this.pupGetPropertyObjTypeRExcel.AcceptButtonCaption = null;
            this.pupGetPropertyObjTypeRExcel.CancelButtonCaption = null;
            this.pupGetPropertyObjTypeRExcel.Caption = "Seleziona Campo di Join";
            this.pupGetPropertyObjTypeRExcel.Category = "MyHiddenCategory";
            this.pupGetPropertyObjTypeRExcel.ConfirmationMessage = null;
            this.pupGetPropertyObjTypeRExcel.Id = "pupGetPropertyObjTypeRExcel";
            this.pupGetPropertyObjTypeRExcel.ToolTip = null;
            this.pupGetPropertyObjTypeRExcel.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupGetPropertyObjTypeRExcel_CustomizePopupWindowParams);
            this.pupGetPropertyObjTypeRExcel.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupGetPropertyObjTypeRExcel_Execute);
            // 
            // ReportExcelController
            // 
            this.Actions.Add(this.pupGetPropertyObjTypeLookUpRExcel);
            this.Actions.Add(this.pupGetPropertyObjTypeRExcel);
            this.TargetObjectType = typeof(CAMS.Module.DBAngrafica.ReportExcel);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupGetPropertyObjTypeLookUpRExcel;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupGetPropertyObjTypeRExcel;
    }
}
