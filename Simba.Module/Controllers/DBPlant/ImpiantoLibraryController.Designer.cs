namespace CAMS.Module.Controllers.DBPlant
{
    partial class ImpiantoLibraryController
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
            this.InserisciApparatiLibrary = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // InserisciApparatiLibrary
            // 
            this.InserisciApparatiLibrary.AcceptButtonCaption = null;
            this.InserisciApparatiLibrary.CancelButtonCaption = null;
            this.InserisciApparatiLibrary.Caption = "Inserisci Library Apparati";
            this.InserisciApparatiLibrary.ConfirmationMessage = null;
            this.InserisciApparatiLibrary.Id = "InserisciApparatiLibrary";
            this.InserisciApparatiLibrary.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.InserisciApparatiLibrary.TargetViewId = "ImpiantoLibrary_DetailView";
            this.InserisciApparatiLibrary.ToolTip = null;
            this.InserisciApparatiLibrary.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.InserisciApparatiLibrary_CustomizePopupWindowParams);
            this.InserisciApparatiLibrary.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.InserisciApparatiLibrary_Execute);
            // 
            // ImpiantoLibraryController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.ServizioLibrary);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction InserisciApparatiLibrary;
    }
}
