namespace CAMS.Module.Controllers
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
            DevExpress.ExpressApp.Actions.ChoiceActionItem choiceActionItem1 = new DevExpress.ExpressApp.Actions.ChoiceActionItem();
            this.ImpiantoLibraryDettaglioAssociaImpianto = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ClonaApparatoInLibreriaImpianti = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // ImpiantoLibraryDettaglioAssociaImpianto
            // 
            this.ImpiantoLibraryDettaglioAssociaImpianto.AcceptButtonCaption = "Associa";
            this.ImpiantoLibraryDettaglioAssociaImpianto.CancelButtonCaption = null;
            this.ImpiantoLibraryDettaglioAssociaImpianto.Caption = "Associa apparati";
            this.ImpiantoLibraryDettaglioAssociaImpianto.ConfirmationMessage = null;
            this.ImpiantoLibraryDettaglioAssociaImpianto.Id = "ImpiantoLibraryDettaglioAssociaImpianto";
            this.ImpiantoLibraryDettaglioAssociaImpianto.ImageName = "Action_LinkUnlink_Link";
            this.ImpiantoLibraryDettaglioAssociaImpianto.IsSizeable = false;
            this.ImpiantoLibraryDettaglioAssociaImpianto.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.ImpiantoLibraryDettaglioAssociaImpianto.TargetObjectType = typeof(CAMS.Module.DBPlant.ServizioLibraryDettaglio);
            this.ImpiantoLibraryDettaglioAssociaImpianto.TargetViewId = "NO-ImpiantoLibraryDettaglioInseribili_ListView";
            this.ImpiantoLibraryDettaglioAssociaImpianto.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ImpiantoLibraryDettaglioAssociaImpianto.ToolTip = "Associa apparati";
            this.ImpiantoLibraryDettaglioAssociaImpianto.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.ImpiantoLibraryDettaglioAssociaImpianto.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ImpiantoLibraryDettaglioAssociaImpianto_CustomizePopupWindowParams);
            this.ImpiantoLibraryDettaglioAssociaImpianto.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ImpiantoLibraryDettaglioAssociaImpianto_Execute);
            // 
            // ClonaApparatoInLibreriaImpianti
            // 
            this.ClonaApparatoInLibreriaImpianti.Caption = "Clona Apparato In Libreria Impianti";
            this.ClonaApparatoInLibreriaImpianti.ConfirmationMessage = null;
            this.ClonaApparatoInLibreriaImpianti.Id = "ClonaApparatoInLibreriaImpianti";
            choiceActionItem1.Caption = "1 Copia";
            choiceActionItem1.Data = "1";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            this.ClonaApparatoInLibreriaImpianti.Items.Add(choiceActionItem1);
            this.ClonaApparatoInLibreriaImpianti.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.ClonaApparatoInLibreriaImpianti.TargetViewId = "ImpiantoLibrary_IMPIANTOLIBRARYDETTAGLIOs_ListView";
            this.ClonaApparatoInLibreriaImpianti.ToolTip = null;
            this.ClonaApparatoInLibreriaImpianti.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.ClonaApparatoInLibreriaImpianti_Execute);
            // 
            // ImpiantoLibraryController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.ServizioLibraryDettaglio);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            //this.ViewControlsCreated += new System.EventHandler(this.ImpiantoLibraryController_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ImpiantoLibraryDettaglioAssociaImpianto;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction ClonaApparatoInLibreriaImpianti;
    }
}
