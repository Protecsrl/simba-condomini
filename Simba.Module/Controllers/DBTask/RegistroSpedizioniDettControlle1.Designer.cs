namespace CAMS.Module.Controllers.DBTask
{
    partial class RegistroSpedizioniDettControlle1
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
            this.RegRdLInvioMail = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.ControllaStato = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // RegRdLInvioMail
            // 
            this.RegRdLInvioMail.Caption = "InvioMail";
            this.RegRdLInvioMail.ConfirmationMessage = null;
            this.RegRdLInvioMail.Id = "RegRdLInvioMail";
            this.RegRdLInvioMail.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.RegRdLInvioMail.TargetObjectsCriteria = "[Esito] = \'ErrorediInvio\' Or [Esito] = \'NonInviata\'";
            this.RegRdLInvioMail.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.RegRdLInvioMail.ToolTip = null;
            this.RegRdLInvioMail.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.RegRdLInvioMail.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.RdLInvioMail_Execute);
            // 
            // ControllaStato
            // 
            this.ControllaStato.Caption = "Controlla Stato";
            this.ControllaStato.ConfirmationMessage = null;
            this.ControllaStato.Id = "ControllaStato";
            this.ControllaStato.ToolTip = null;
            this.ControllaStato.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.ControllaStato_Execute);
            // 
            // RegistroSpedizioniDettControlle1
            // 
            this.Actions.Add(this.RegRdLInvioMail);
            this.Actions.Add(this.ControllaStato);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RegistroSpedizioniDett);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction RegRdLInvioMail;
        private DevExpress.ExpressApp.Actions.SimpleAction ControllaStato;
    }
}
