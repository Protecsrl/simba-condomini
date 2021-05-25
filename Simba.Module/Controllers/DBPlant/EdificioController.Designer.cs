namespace CAMS.Module.Controllers
{
    partial class EdificioController
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
            this.EdificioClona = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acAggiornaValoriEdificio = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acInserisciDocumenti = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.scaImpiantoMappa = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.popWinInsertEdificioinCluster = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // EdificioClona
            // 
            this.EdificioClona.AcceptButtonCaption = "Crea";
            this.EdificioClona.CancelButtonCaption = null;
            this.EdificioClona.Caption = "Clona immobile";
            this.EdificioClona.ConfirmationMessage = null;
            this.EdificioClona.Id = "EdificioClona";
            this.EdificioClona.ImageName = "Action_CloneMerge_Clone_Object";
            this.EdificioClona.IsSizeable = false;
            this.EdificioClona.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.EdificioClona.TargetViewId = "Edificio_ListView";
            this.EdificioClona.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.EdificioClona.ToolTip = "Clona l\'immobile selezionato";
            this.EdificioClona.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.EdificioClona.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.EdificioClona_CustomizePopupWindowParams);
            this.EdificioClona.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.EdificioClona_Execute);
            // 
            // acAggiornaValoriEdificio
            // 
            this.acAggiornaValoriEdificio.Caption = "Aggiorna Valori";
            this.acAggiornaValoriEdificio.Category = "RecordEdit";
            this.acAggiornaValoriEdificio.ConfirmationMessage = "Verifica e Aggiorna i Tempi di Lavorazione.";
            this.acAggiornaValoriEdificio.Id = "acAggiornaValoriEdificio";
            this.acAggiornaValoriEdificio.ImageName = "Action_Grand_Totals_Column";
            this.acAggiornaValoriEdificio.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acAggiornaValoriEdificio.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.acAggiornaValoriEdificio.ToolTip = "Aggiorna Dati Asset e Tempi Lavorazione";
            this.acAggiornaValoriEdificio.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acAggiornaValoriEdificio_Execute);
            // 
            // acInserisciDocumenti
            // 
            this.acInserisciDocumenti.Caption = "Inserisci Documenti";
            this.acInserisciDocumenti.ConfirmationMessage = null;
            this.acInserisciDocumenti.Id = "acInserisciDocumenti";
            this.acInserisciDocumenti.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acInserisciDocumenti.ToolTip = null;
            this.acInserisciDocumenti.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acInserisciDocumenti.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acInserisciDocumenti_Execute);
            // 
            // scaImpiantoMappa
            // 
            this.scaImpiantoMappa.Caption = "Asset Mappa";
            this.scaImpiantoMappa.ConfirmationMessage = null;
            this.scaImpiantoMappa.Id = "scaImpiantoMappa";
            this.scaImpiantoMappa.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.scaImpiantoMappa.ToolTip = null;
            this.scaImpiantoMappa.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaImpiantoMappa_Execute);
            // 
            // popWinInsertEdificioinCluster
            // 
            this.popWinInsertEdificioinCluster.AcceptButtonCaption = null;
            this.popWinInsertEdificioinCluster.CancelButtonCaption = null;
            this.popWinInsertEdificioinCluster.Caption = "Associa Edifici";
            this.popWinInsertEdificioinCluster.ConfirmationMessage = "Vuoi Associare nuovi edifici al ClusterEdifici?";
            this.popWinInsertEdificioinCluster.Id = "popWinInsertEdificioinCluster";
            this.popWinInsertEdificioinCluster.TargetViewId = "ClusterEdifici_Edificis_ListView";
            this.popWinInsertEdificioinCluster.ToolTip = null;
            this.popWinInsertEdificioinCluster.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popWinInsertEdificioinCluster_CustomizePopupWindowParams);
            this.popWinInsertEdificioinCluster.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popWinInsertEdificioinCluster_Execute);
            // 
            // EdificioController
            // 
            this.Actions.Add(this.EdificioClona);
            this.Actions.Add(this.acAggiornaValoriEdificio);
            this.Actions.Add(this.acInserisciDocumenti);
            this.Actions.Add(this.scaImpiantoMappa);
            this.Actions.Add(this.popWinInsertEdificioinCluster);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.Immobile);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction EdificioClona;
        private DevExpress.ExpressApp.Actions.SimpleAction acAggiornaValoriEdificio;
        private DevExpress.ExpressApp.Actions.SimpleAction acInserisciDocumenti;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaImpiantoMappa;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popWinInsertEdificioinCluster;
    }
}
