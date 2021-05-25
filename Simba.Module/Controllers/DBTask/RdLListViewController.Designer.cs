namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLListViewController
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
            this.acVisualizzaDettagliRdLListViev = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.acReportRdLListView = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acCreaRegRdLNotificaEmergenza = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.scaReportRdLListView = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // acVisualizzaDettagliRdLListViev
            // 
            this.acVisualizzaDettagliRdLListViev.Caption = "Visualizza Dettagli";
            this.acVisualizzaDettagliRdLListViev.Category = "View";
            this.acVisualizzaDettagliRdLListViev.ConfirmationMessage = null;
            this.acVisualizzaDettagliRdLListViev.Id = "acVisualizzaDettagliRdLListViev";
            this.acVisualizzaDettagliRdLListViev.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListView);
            this.acVisualizzaDettagliRdLListViev.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.acVisualizzaDettagliRdLListViev.ToolTip = "Seleziona il Dettaglio da Visualizzare";
            this.acVisualizzaDettagliRdLListViev.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.acVisualizzaDettagliRdLListViev.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.acVisualizzaDettagliRdLListViev_Execute);
            // 
            // acReportRdLListView
            // 
            this.acReportRdLListView.Caption = "Stampa Report";
            this.acReportRdLListView.Category = "Reports";
            this.acReportRdLListView.ConfirmationMessage = null;
            this.acReportRdLListView.Id = "acReportRdLListView";
            this.acReportRdLListView.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.acReportRdLListView.ToolTip = null;
            this.acReportRdLListView.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acReportRdLListView_Execute);
            // 
            // acCreaRegRdLNotificaEmergenza
            // 
            this.acCreaRegRdLNotificaEmergenza.AcceptButtonCaption = "Conferma";
            this.acCreaRegRdLNotificaEmergenza.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.acCreaRegRdLNotificaEmergenza.CancelButtonCaption = "Annulla";
            this.acCreaRegRdLNotificaEmergenza.Caption = "Assegna in Emergenza";
            this.acCreaRegRdLNotificaEmergenza.Category = "Edit";
            this.acCreaRegRdLNotificaEmergenza.ConfirmationMessage = "Vuoi Creare una Procedura di Emergenza per Questa RdL?";
            this.acCreaRegRdLNotificaEmergenza.Id = "acCreaRegRdLNotificaEmergenza";
            this.acCreaRegRdLNotificaEmergenza.ImageName = "TeamRisorse";
            this.acCreaRegRdLNotificaEmergenza.IsSizeable = false;
            this.acCreaRegRdLNotificaEmergenza.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acCreaRegRdLNotificaEmergenza.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acCreaRegRdLNotificaEmergenza.TargetObjectsCriteria = "OidCategoria= 4 And OidSmistamento = 1";
            this.acCreaRegRdLNotificaEmergenza.TargetViewId = "RdLListView_ListView_DaAssegnare";
            this.acCreaRegRdLNotificaEmergenza.ToolTip = "Assegna in Emergenza a piu Team";
            this.acCreaRegRdLNotificaEmergenza.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.acCreaRegRdLNotificaEmergenza_CustomizePopupWindowParams);
            this.acCreaRegRdLNotificaEmergenza.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.acCreaRegRdLNotificaEmergenza_Execute);
            // 
            // scaReportRdLListView
            // 
            this.scaReportRdLListView.Caption = "Stampa Report";
            this.scaReportRdLListView.ConfirmationMessage = null;
            this.scaReportRdLListView.Id = "scaReportRdLListView";
            this.scaReportRdLListView.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.scaReportRdLListView.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.scaReportRdLListView.TargetViewId = "no";
            this.scaReportRdLListView.ToolTip = null;
            this.scaReportRdLListView.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaReportRdLListView_Execute);
            // 
            // RdLListViewController
            // 
            this.Actions.Add(this.acVisualizzaDettagliRdLListViev);
            this.Actions.Add(this.acReportRdLListView);
            this.Actions.Add(this.acCreaRegRdLNotificaEmergenza);
            this.Actions.Add(this.scaReportRdLListView);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListView);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction acVisualizzaDettagliRdLListViev;
        private DevExpress.ExpressApp.Actions.SimpleAction acReportRdLListView;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction acCreaRegRdLNotificaEmergenza;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaReportRdLListView;
    }
}
