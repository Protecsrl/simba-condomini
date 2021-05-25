namespace CAMS.Module.Controllers.DBDataImport
{
    partial class RegistroDataImportTentativiController
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
            this.ImportaManuale = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.ImportAutomatico = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saResetStatoImport = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saClonaTentativo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.scaImpostaStato = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // ImportaManuale
            // 
            this.ImportaManuale.Caption = "Importa Manuale";
            this.ImportaManuale.ConfirmationMessage = null;
            this.ImportaManuale.Id = "ImportaManuale";
            this.ImportaManuale.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.ImportaManuale.TargetObjectsCriteria = "TipoAcquisizione = 2";
            this.ImportaManuale.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.ImportaManuale.ToolTip = null;
            this.ImportaManuale.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.ImportaManuale.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.ImportaManuale_Execute);
            // 
            // ImportAutomatico
            // 
            this.ImportAutomatico.Caption = "Import Automatico";
            this.ImportAutomatico.ConfirmationMessage = null;
            this.ImportAutomatico.Id = "ImportAutomatico";
            this.ImportAutomatico.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.ImportAutomatico.TargetObjectsCriteria = "TipoAcquisizione = 1";
            this.ImportAutomatico.ToolTip = null;
            this.ImportAutomatico.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ImportAutomatico_Execute);
            // 
            // saResetStatoImport
            // 
            this.saResetStatoImport.Caption = "Reset";
            this.saResetStatoImport.Category = "RecordEdit";
            this.saResetStatoImport.ConfirmationMessage = "Vuoi resettare lo stato del tentativo di importazione?";
            this.saResetStatoImport.Id = "saResetStatoImport";
            this.saResetStatoImport.TargetObjectsCriteria = "IsCurrentUserInRole(\'Administrators\')";
            this.saResetStatoImport.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saResetStatoImport.ToolTip = "esegui un Reset delle impostazioni di stato e relativa candellazione dei dati.";
            this.saResetStatoImport.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saResetStatoImport.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saResetStatoImport_Execute);
            // 
            // saClonaTentativo
            // 
            this.saClonaTentativo.Caption = "Clona Tentativo";
            this.saClonaTentativo.ConfirmationMessage = "Vuoi Clonare il tentativo di Importazione?";
            this.saClonaTentativo.Id = "saClonaTentativo";
            this.saClonaTentativo.ToolTip = "esegui una copia di questo tentativo di importazione.";
            this.saClonaTentativo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saClonaTentativo_Execute);
            // 
            // scaImpostaStato
            // 
            this.scaImpostaStato.Caption = "Imposta Stato";
            this.scaImpostaStato.ConfirmationMessage = "scaInpostaStato";
            this.scaImpostaStato.Id = "scaImpostaStato";
            choiceActionItem1.Caption = "Pianificato Simulazione";
            choiceActionItem1.Data = "PianificatoSimulazione";
            choiceActionItem1.Id = "PianificatoSimulazione";
            choiceActionItem1.ImageName = null;
            choiceActionItem1.Shortcut = null;
            choiceActionItem1.ToolTip = null;
            choiceActionItem2.Caption = "Pianificato Esecutivo";
            choiceActionItem2.Data = "PianificatoExec";
            choiceActionItem2.Id = "PianificatoExec";
            choiceActionItem2.ImageName = null;
            choiceActionItem2.Shortcut = null;
            choiceActionItem2.ToolTip = null;
            choiceActionItem3.Caption = "Sospeso";
            choiceActionItem3.Data = "Sospeso";
            choiceActionItem3.Id = "Sospeso";
            choiceActionItem3.ImageName = null;
            choiceActionItem3.Shortcut = null;
            choiceActionItem3.ToolTip = null;
            choiceActionItem4.Caption = "Bloccato";
            choiceActionItem4.Data = "Bloccato";
            choiceActionItem4.Id = "Bloccato";
            choiceActionItem4.ImageName = null;
            choiceActionItem4.Shortcut = null;
            choiceActionItem4.ToolTip = null;
            this.scaImpostaStato.Items.Add(choiceActionItem1);
            this.scaImpostaStato.Items.Add(choiceActionItem2);
            this.scaImpostaStato.Items.Add(choiceActionItem3);
            this.scaImpostaStato.Items.Add(choiceActionItem4);
            this.scaImpostaStato.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.scaImpostaStato.ToolTip = null;
            this.scaImpostaStato.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.scaImpostaStato.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaImpostaStato_Execute);
            // 
            // RegistroDataImportTentativiController
            // 
            this.Actions.Add(this.ImportaManuale);
            this.Actions.Add(this.ImportAutomatico);
            this.Actions.Add(this.saResetStatoImport);
            this.Actions.Add(this.saClonaTentativo);
            this.Actions.Add(this.scaImpostaStato);
            this.TargetObjectType = typeof(CAMS.Module.DBDataImport.RegistroDataImportTentativi);

        }

        #endregion
        private DevExpress.ExpressApp.Actions.SingleChoiceAction ImportaManuale;
        private DevExpress.ExpressApp.Actions.SimpleAction ImportAutomatico;
        private DevExpress.ExpressApp.Actions.SimpleAction saResetStatoImport;
        private DevExpress.ExpressApp.Actions.SimpleAction saClonaTentativo;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaImpostaStato;
    }
}
