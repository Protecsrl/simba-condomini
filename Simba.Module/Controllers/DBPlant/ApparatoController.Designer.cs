namespace CAMS.Module.Controllers
{
    partial class ApparatoController
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem2 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem3 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem4 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem5 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem6 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem7 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem8 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem9 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem10 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.ApparatoAssociaImpianto = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ApparatoAssociaImpiantoNr = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.ApparatoClonaApparato = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.saSostituisciApparato = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.scaFiltroTipoApparato = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.saVisualizza = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // ApparatoAssociaImpianto
            // 
            this.ApparatoAssociaImpianto.AcceptButtonCaption = "Associa";
            this.ApparatoAssociaImpianto.CancelButtonCaption = null;
            this.ApparatoAssociaImpianto.Caption = "Associa apparati";
            this.ApparatoAssociaImpianto.ConfirmationMessage = null;
            this.ApparatoAssociaImpianto.Id = "ApparatoAssociaImpianto";
            this.ApparatoAssociaImpianto.ImageName = "Action_LinkUnlink_Link";
            this.ApparatoAssociaImpianto.IsSizeable = false;
            this.ApparatoAssociaImpianto.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.ApparatoAssociaImpianto.TargetObjectType = typeof(CAMS.Module.DBPlant.Asset);
            this.ApparatoAssociaImpianto.TargetViewId = "no-Impianto_ListaApparatiInseribili_ListView";
            this.ApparatoAssociaImpianto.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ApparatoAssociaImpianto.ToolTip = "Associa apparati";
            this.ApparatoAssociaImpianto.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.ApparatoAssociaImpianto.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ApparatoAssociaImpianto_CustomizePopupWindowParams);
            this.ApparatoAssociaImpianto.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ApparatoAssociaImpianto_Execute);
            // 
            // ApparatoAssociaImpiantoNr
            // 
            this.ApparatoAssociaImpiantoNr.Caption = "Associa Apparato a Asset(nr)";
            this.ApparatoAssociaImpiantoNr.ConfirmationMessage = null;
            this.ApparatoAssociaImpiantoNr.Id = "ApparatoAssociaImpiantoNr";
            this.ApparatoAssociaImpiantoNr.ImageName = "Action_LinkUnlink_Link";
            choiceActionItem1.Caption = "1";
            choiceActionItem1.Data = "1";
            choiceActionItem1.Id = "1";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "2";
            choiceActionItem2.Data = "2";
            choiceActionItem2.Id = "2";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.Caption = "3";
            choiceActionItem3.Data = "3";
            choiceActionItem3.Id = "3";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            choiceActionItem4.Caption = "4";
            choiceActionItem4.Data = "4";
            choiceActionItem4.Id = "4";
            choiceActionItem4.ImageName = null;
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;
            choiceActionItem5.Caption = "5";
            choiceActionItem5.Data = "5";
            choiceActionItem5.Id = "5";
            choiceActionItem5.ImageName = null;
            choiceActionItem5.Shortcut = null;
            choiceActionItem5.ToolTip = null;
            choiceActionItem6.Caption = "6";
            choiceActionItem6.Data = "6";
            choiceActionItem6.Id = "6";
            choiceActionItem6.ImageName = null;
            choiceActionItem6.Shortcut = null;
            choiceActionItem6.ToolTip = null;
            choiceActionItem7.Caption = "7";
            choiceActionItem7.Data = "7";
            choiceActionItem7.Id = "7";
            choiceActionItem7.ImageName = null;
            choiceActionItem7.Shortcut = null;
            choiceActionItem7.ToolTip = null;
            choiceActionItem8.Caption = "8";
            choiceActionItem8.Data = "8";
            choiceActionItem8.Id = "8";
            choiceActionItem8.ImageName = null;
            choiceActionItem8.Shortcut = null;
            choiceActionItem8.ToolTip = null;
            choiceActionItem9.Caption = "9";
            choiceActionItem9.Data = "9";
            choiceActionItem9.Id = "9";
            choiceActionItem9.ImageName = null;
            choiceActionItem9.Shortcut = null;
            choiceActionItem9.ToolTip = null;
            choiceActionItem10.Caption = "10";
            choiceActionItem10.Data = "10";
            choiceActionItem10.Id = "10";
            choiceActionItem10.ImageName = null;
            choiceActionItem10.Shortcut = null;
            choiceActionItem10.ToolTip = null;
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem1);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem2);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem3);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem4);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem5);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem6);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem7);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem8);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem9);
            this.ApparatoAssociaImpiantoNr.Items.Add(choiceActionItem10);
            this.ApparatoAssociaImpiantoNr.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.ApparatoAssociaImpiantoNr.ShowItemsOnClick = true;
            this.ApparatoAssociaImpiantoNr.TargetViewId = "Impianto_ListaApparatiInseribili_ListView";
            this.ApparatoAssociaImpiantoNr.ToolTip = "Seleziona Tipo Apparato\r\nSeleziona numero di copie riechieste\r\nEsegue la copia e " +
    "associa all impianto.";
            this.ApparatoAssociaImpiantoNr.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.ApparatoAssociaImpiantoNr_Execute);
            // 
            // ApparatoClonaApparato
            // 
            this.ApparatoClonaApparato.AcceptButtonCaption = "Crea";
            this.ApparatoClonaApparato.CancelButtonCaption = null;
            this.ApparatoClonaApparato.Caption = "Clona Apparato";
            this.ApparatoClonaApparato.Category = "RecordEdit";
            this.ApparatoClonaApparato.ConfirmationMessage = null;
            this.ApparatoClonaApparato.Id = "ClonaApparato";
            this.ApparatoClonaApparato.ImageName = "Action_CloneMerge_Clone_Object";
            this.ApparatoClonaApparato.IsSizeable = false;
            this.ApparatoClonaApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.ApparatoClonaApparato.TargetObjectType = typeof(CAMS.Module.DBPlant.Asset);
            this.ApparatoClonaApparato.TargetViewId = "Impianto_APPARATOes_ListView";
            this.ApparatoClonaApparato.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ApparatoClonaApparato.ToolTip = "Clona l\'apparato selezionato";
            this.ApparatoClonaApparato.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.ApparatoClonaApparato.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ApparatoClonaApparato_CustomizePopupWindowParams);
            this.ApparatoClonaApparato.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ApparatoClonaApparato_Execute);
            // 
            // saSostituisciApparato
            // 
            this.saSostituisciApparato.Caption = "Sostituisci Apparato";
            this.saSostituisciApparato.Category = "Save";
            this.saSostituisciApparato.ConfirmationMessage = "Attenzione Verrà Sostituito Questo Apparato con Uno Nuovo. Vuoi Continuare?";
            this.saSostituisciApparato.Id = "saSostituisciApparato";
            this.saSostituisciApparato.ImageName = "Action_CloneMerge_Merge_Object";
            this.saSostituisciApparato.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.saSostituisciApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saSostituisciApparato.TargetObjectsCriteria = "ApparatoSostituitoDa is null And ApparatoSostituito is null";
            this.saSostituisciApparato.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saSostituisciApparato.ToolTip = "Sostituisci l\'apparato selezionato";
            this.saSostituisciApparato.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saSostituisciApparato.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saSostituisciApparato_Execute);
            // 
            // scaFiltroTipoApparato
            // 
            this.scaFiltroTipoApparato.Caption = "sca Filtro Tipo Apparato";
            this.scaFiltroTipoApparato.ConfirmationMessage = null;
            this.scaFiltroTipoApparato.Id = "scaFiltroTipoApparato";
            this.scaFiltroTipoApparato.TargetViewId = "Impianto_APPARATOes_ListView";
            this.scaFiltroTipoApparato.ToolTip = null;
            this.scaFiltroTipoApparato.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaFiltroTipoApparato_Execute);
            // 
            // saVisualizza
            // 
            this.saVisualizza.Caption = "Visualizza";
            this.saVisualizza.Category = "Edit";
            this.saVisualizza.ConfirmationMessage = null;
            this.saVisualizza.Id = "saVisualizza";
            this.saVisualizza.ImageName = "Preview_16x16";
            this.saVisualizza.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saVisualizza.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saVisualizza.TargetViewId = "ApparatoMap_ApparatoinSostegnos_ListView";
            this.saVisualizza.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saVisualizza.ToolTip = "Visualizza Apparato in Dettaglio";
            this.saVisualizza.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saVisualizza.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saVisualizza_Execute);
            // 
            // ApparatoController
            // 
            this.Actions.Add(this.ApparatoAssociaImpianto);
            this.Actions.Add(this.ApparatoAssociaImpiantoNr);
            this.Actions.Add(this.ApparatoClonaApparato);
            this.Actions.Add(this.saSostituisciApparato);
            this.Actions.Add(this.scaFiltroTipoApparato);
            this.Actions.Add(this.saVisualizza);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.Asset);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.ViewControlsCreated += new System.EventHandler(this.ApparatoController_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ApparatoAssociaImpianto;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction ApparatoAssociaImpiantoNr;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ApparatoClonaApparato;
        private DevExpress.ExpressApp.Actions.SimpleAction saSostituisciApparato;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaFiltroTipoApparato;
        private DevExpress.ExpressApp.Actions.SimpleAction saVisualizza;
    }
}
