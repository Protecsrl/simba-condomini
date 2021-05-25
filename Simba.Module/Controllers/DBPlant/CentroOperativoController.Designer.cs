namespace CAMS.Module.Controllers.DBPlant
{
    partial class CentroOperativoController
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
            this.acSincronizzaUSLSUSLG = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // acSincronizzaUSLSUSLG
            // 
            this.acSincronizzaUSLSUSLG.Caption = "Sincronizza USLS USLG";
            this.acSincronizzaUSLSUSLG.ConfirmationMessage = "vuoi aggiornare i dati?";
            this.acSincronizzaUSLSUSLG.Id = "acSincronizzaUSLSUSLG";
            this.acSincronizzaUSLSUSLG.ToolTip = @"Sincronizza le Unità Standard Lavorative in relazione all\'Area di Polo Aggiornata" +
    "";
            this.acSincronizzaUSLSUSLG.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acSincronizzaUSLSUSLG_Execute);
            // 
            // CentroOperativoController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.CentroOperativo);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acSincronizzaUSLSUSLG;
    }
}
