namespace CAMS.Module.Controllers.DBRuoloUtente
{
    partial class RuoloUserController
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
            this.cmdClonaRuolo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acCreaRuolo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acElencoMembri = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.simpleAction1 = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // cmdClonaRuolo
            // 
            this.cmdClonaRuolo.Caption = "Clona Ruolo";
            this.cmdClonaRuolo.ConfirmationMessage = null;
            this.cmdClonaRuolo.Id = "cmdClonaRuolo";
            this.cmdClonaRuolo.TargetObjectsCriteria = "";
            this.cmdClonaRuolo.ToolTip = null;
            this.cmdClonaRuolo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.cmdClonaRuolo_Execute);
            // 
            // acCreaRuolo
            // 
            this.acCreaRuolo.Caption = "ac Crea Ruolo";
            this.acCreaRuolo.ConfirmationMessage = null;
            this.acCreaRuolo.Id = "acCreaRuolo";
            this.acCreaRuolo.ToolTip = null;
            this.acCreaRuolo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acCreaRuolo_Execute);
            // 
            // acElencoMembri
            // 
            this.acElencoMembri.Caption = "Elenco Membri";
            this.acElencoMembri.ConfirmationMessage = null;
            this.acElencoMembri.Id = "acElencoMembri";
            this.acElencoMembri.ToolTip = null;
            this.acElencoMembri.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acElencoMembri_Execute);
            // 
            // simpleAction1
            // 
            this.simpleAction1.Caption = null;
            this.simpleAction1.ConfirmationMessage = null;
            this.simpleAction1.Id = "bbf5e874-3ece-42b7-996e-cc9057fd4158";
            this.simpleAction1.ToolTip = null;
            this.simpleAction1.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.simpleAction1_Execute);
            // 
            // RuoloUserController
            // 
            this.Actions.Add(this.cmdClonaRuolo);
            this.Actions.Add(this.acCreaRuolo);
            this.Actions.Add(this.acElencoMembri);
            this.Actions.Add(this.simpleAction1);
            this.TargetObjectType = typeof(CAMS.Module.DBRuoloUtente.ClonaRuoloUser);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction cmdClonaRuolo;
        private DevExpress.ExpressApp.Actions.SimpleAction acCreaRuolo;
        private DevExpress.ExpressApp.Actions.SimpleAction acElencoMembri;
        private DevExpress.ExpressApp.Actions.SimpleAction simpleAction1;
    }
}
