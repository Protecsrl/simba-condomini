
namespace CAMS.Module.Controllers.DBGestOrari
{
    partial class GestioneOrariCircuitiController
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
            this.pupWinAddCircuito = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupWinAddCircuito
            // 
            this.pupWinAddCircuito.AcceptButtonCaption = null;
            this.pupWinAddCircuito.CancelButtonCaption = null;
            this.pupWinAddCircuito.Caption = "Add Multi-Circuito";
            this.pupWinAddCircuito.ConfirmationMessage = null;
            this.pupWinAddCircuito.Id = "pupWinAddCircuito";
            this.pupWinAddCircuito.ImageName = "Actions_AddCircled";
            this.pupWinAddCircuito.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.pupWinAddCircuito.TargetViewId = "GestioneNuoviOrari_GestioneOrariCircuitis_ListView;GestioneOrari_GestioneOrariCir" +
    "cuitis_ListView";
            this.pupWinAddCircuito.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupWinAddCircuito.ToolTip = null;
            this.pupWinAddCircuito.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupWinAddCircuito.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinAddCircuito_CustomizePopupWindowParams);
            this.pupWinAddCircuito.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinAddCircuito_Execute);
            // 
            // GestioneOrariCircuitiController
            // 
            this.Actions.Add(this.pupWinAddCircuito);
            this.TargetObjectType = typeof(CAMS.Module.DBGestOrari.GestioneOrariCircuiti);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinAddCircuito;
    }
}
