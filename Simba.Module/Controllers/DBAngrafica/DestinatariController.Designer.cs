namespace CAMS.Module.Controllers.DBAngrafica
{
    partial class DestinatariController
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
            this.DestinatariClonaDestinatario = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // DestinatariClonaDestinatario
            // 
            this.DestinatariClonaDestinatario.AcceptButtonCaption = "Crea";
            this.DestinatariClonaDestinatario.CancelButtonCaption = null;
            this.DestinatariClonaDestinatario.Caption = "Clona Destinatario";
            this.DestinatariClonaDestinatario.Category = "RecordEdit";
            this.DestinatariClonaDestinatario.ConfirmationMessage = null;
            this.DestinatariClonaDestinatario.Id = "ClonaDestinatario";
            this.DestinatariClonaDestinatario.ImageName = "Action_CloneMerge_Clone_Object";
            this.DestinatariClonaDestinatario.IsSizeable = false;
            this.DestinatariClonaDestinatario.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.DestinatariClonaDestinatario.TargetObjectType = typeof(CAMS.Module.DBAngrafica.Destinatari);
            this.DestinatariClonaDestinatario.TargetViewId = "Destinatari_ListView";
            this.DestinatariClonaDestinatario.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.DestinatariClonaDestinatario.ToolTip = "Clona il destinatario selezionato";
            this.DestinatariClonaDestinatario.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.DestinatariClonaDestinatario.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.DestinatariClonaDestinatario_Execute);
            this.DestinatariClonaDestinatario.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.DestinatariClonaDestinatario_CustomizePopupWindowParams);
            
            // 
            // DestinatariController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBAngrafica.Destinatari);

            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
           
        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction DestinatariClonaDestinatario;
    }
}
