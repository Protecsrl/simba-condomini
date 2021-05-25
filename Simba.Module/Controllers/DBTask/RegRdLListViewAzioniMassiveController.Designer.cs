namespace CAMS.Module.Controllers.DBTask
{
    partial class RegRdLListViewAzioniMassiveController
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
            this.PupWinRegRdLAzioniMassive = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.PopWinRegRdLCambiaRisorsaMassivo = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // PupWinRegRdLAzioniMassive
            // 
            this.PupWinRegRdLAzioniMassive.AcceptButtonCaption = "Completa";
            this.PupWinRegRdLAzioniMassive.CancelButtonCaption = "Rinuncia";
            this.PupWinRegRdLAzioniMassive.Caption = "Chiusura Massiva";
            this.PupWinRegRdLAzioniMassive.Category = "Edit";
            this.PupWinRegRdLAzioniMassive.ConfirmationMessage = "Attenzione le Attività Selezionate saranno Completate!! Continuare?";
            this.PupWinRegRdLAzioniMassive.Id = "PupWinRegRdLAzioniMassive";
            this.PupWinRegRdLAzioniMassive.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.PupWinRegRdLAzioniMassive.TargetViewId = "RegRdLListViewAzioniMassive_ListView_Chiusura";
            this.PupWinRegRdLAzioniMassive.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.PupWinRegRdLAzioniMassive.ToolTip = "Completamento Massivo delle Attività";
            this.PupWinRegRdLAzioniMassive.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.PupWinRegRdLAzioniMassive.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.PupWinRegRdLAzioniMassive_CustomizePopupWindowParams);
            this.PupWinRegRdLAzioniMassive.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PupWinRegRdLAzioniMassive_Execute);
            // 
            // PopWinRegRdLCambiaRisorsaMassivo
            // 
            this.PopWinRegRdLCambiaRisorsaMassivo.AcceptButtonCaption = "Esegui";
            this.PopWinRegRdLCambiaRisorsaMassivo.CancelButtonCaption = "Rinuncia";
            this.PopWinRegRdLCambiaRisorsaMassivo.Caption = "Cambio Risorsa";
            this.PopWinRegRdLCambiaRisorsaMassivo.Category = "Edit";
            this.PopWinRegRdLCambiaRisorsaMassivo.ConfirmationMessage = "Attenzione! alle Attività Selezionate saranno Modificate le risorse assegnate. Co" +
    "ntinuare?";
            this.PopWinRegRdLCambiaRisorsaMassivo.Id = "PopWinRegRdLCambiaRisorsaMassivo";
            this.PopWinRegRdLCambiaRisorsaMassivo.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.PopWinRegRdLCambiaRisorsaMassivo.TargetViewId = "RegRdLListViewAzioniMassive_ListView_CambioRisorsa_Assegnata;RegRdLListViewAzioni" +
    "Massive_ListView_CambioRisorsa_daAssegnare;RegRdLListViewAzioniMassive_ListView_" +
    "CambioRisorsa_GestioneSO;";
            this.PopWinRegRdLCambiaRisorsaMassivo.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.PopWinRegRdLCambiaRisorsaMassivo.ToolTip = "Cambio Risorsa";
            this.PopWinRegRdLCambiaRisorsaMassivo.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.PopWinRegRdLCambiaRisorsaMassivo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.PopWinRegRdLCambiaRisorsaMassivo_CustomizePopupWindowParams);
            this.PopWinRegRdLCambiaRisorsaMassivo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.PopWinRegRdLCambiaRisorsaMassivo_Execute);
            // 
            // RegRdLListViewAzioniMassiveController
            // 
            this.Actions.Add(this.PupWinRegRdLAzioniMassive);
            this.Actions.Add(this.PopWinRegRdLCambiaRisorsaMassivo);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RegRdLListViewAzioniMassive);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PupWinRegRdLAzioniMassive;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction PopWinRegRdLCambiaRisorsaMassivo;
    }
}
