namespace CAMS.Module.Controllers.DBPlant
{
    partial class ApparatoCaratteristicheTecnicheController
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
            this.pupStoricizzaCaratt_ListView = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupStoricizzaCaratt_ListView
            // 
            this.pupStoricizzaCaratt_ListView.AcceptButtonCaption = null;
            this.pupStoricizzaCaratt_ListView.CancelButtonCaption = null;
            this.pupStoricizzaCaratt_ListView.Caption = "Storicizza";
            this.pupStoricizzaCaratt_ListView.Category = "Edit";
            this.pupStoricizzaCaratt_ListView.ConfirmationMessage = "Vuoi Storicizzare la Caratteristica?";
            this.pupStoricizzaCaratt_ListView.Id = "pupStoricizzaCaratt_ListView";
            this.pupStoricizzaCaratt_ListView.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupStoricizzaCaratt_ListView.ToolTip = null;
            this.pupStoricizzaCaratt_ListView.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupStoricizzaCaratt_ListView.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupStoricizzaCaratt_ListView_CustomizePopupWindowParams);
            this.pupStoricizzaCaratt_ListView.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupStoricizzaCaratt_ListView_Execute);
            // 
            // ApparatoCaratteristicheTecnicheController
            // 
            this.Actions.Add(this.pupStoricizzaCaratt_ListView);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.AsettCaratteristicheTecniche);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupStoricizzaCaratt_ListView;
    }
}
