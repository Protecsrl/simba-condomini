namespace CAMS.Module.Controllers.DBPlant
{
    partial class ImpiantoLibraryAppInseribiliController
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
            this.InsApparatiInLibraryImp = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // InsApparatiInLibraryImp
            // 
            this.InsApparatiInLibraryImp.Caption = "Inserimento Apparati In Libreria";
            this.InsApparatiInLibraryImp.ConfirmationMessage = null;
            this.InsApparatiInLibraryImp.DefaultItemMode = DevExpress.ExpressApp.Actions.DefaultItemMode.LastExecutedItem;
            this.InsApparatiInLibraryImp.Id = "InsApparatiInLibraryImp";
            choiceActionItem1.BeginGroup = true;
            choiceActionItem1.Caption = "1 Copia";
            choiceActionItem1.Data = "1";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            this.InsApparatiInLibraryImp.Items.Add(choiceActionItem1);
            this.InsApparatiInLibraryImp.ItemType = DevExpress.ExpressApp.Actions.SingleChoiceActionItemType.ItemIsOperation;
            this.InsApparatiInLibraryImp.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.InsApparatiInLibraryImp.TargetObjectsCriteriaMode = DevExpress.ExpressApp.Actions.TargetObjectsCriteriaMode.TrueForAll;
            this.InsApparatiInLibraryImp.TargetObjectType = typeof(CAMS.Module.DBPlant.ServizioLibraryAppInseribili);
            this.InsApparatiInLibraryImp.TargetViewId = "ImpiantoLibrary_ImpiantoLibraryAppInseribilis_ListView";
            this.InsApparatiInLibraryImp.ToolTip = "Seleziona e inserisci apparati in Libreria";
            this.InsApparatiInLibraryImp.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.InsApparatiInLibraryImp_Execute);
            // 
            // ImpiantoLibraryAppInseribiliController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.ServizioLibraryAppInseribili);

        }

        #endregion

        public DevExpress.ExpressApp.Actions.SingleChoiceAction InsApparatiInLibraryImp;

    }
}
