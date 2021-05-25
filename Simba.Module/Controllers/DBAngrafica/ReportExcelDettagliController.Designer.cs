namespace CAMS.Module.Controllers.DBAngrafica
{
    partial class ReportExcelDettagliController
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
            this.pupGetPropertyObjTypeRExcelDett = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupGetPropertyObjTypeRExcelDett
            // 
            this.pupGetPropertyObjTypeRExcelDett.AcceptButtonCaption = null;
            this.pupGetPropertyObjTypeRExcelDett.CancelButtonCaption = null;
            this.pupGetPropertyObjTypeRExcelDett.Caption = "Seleziona Campo";
            this.pupGetPropertyObjTypeRExcelDett.Category = "MyHiddenCategory";
            this.pupGetPropertyObjTypeRExcelDett.ConfirmationMessage = null;
            this.pupGetPropertyObjTypeRExcelDett.Id = "pupGetPropertyObjTypeRExcelDett";
            this.pupGetPropertyObjTypeRExcelDett.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupGetPropertyObjTypeRExcelDett.ToolTip = null;
            this.pupGetPropertyObjTypeRExcelDett.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupGetPropertyObjTypeRExcelDett.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupGetPropertyObjTypeRExcelDett_CustomizePopupWindowParams);
            this.pupGetPropertyObjTypeRExcelDett.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupGetPropertyObjTypeRExcelDett_Execute);
            // 
            // ReportExcelDettagliController
            // 
            this.Actions.Add(this.pupGetPropertyObjTypeRExcelDett);
            this.TargetObjectType = typeof(CAMS.Module.DBAngrafica.ReportExcelDettagli);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupGetPropertyObjTypeRExcelDett;
    }
}
