namespace CAMS.Module.Controllers.DBClienti
{
    partial class ContrattiConsipDettagliController
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
            this.scaFiltroStatoDocumento = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.saCaricadaDirectory = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // scaFiltroStatoDocumento
            // 
            this.scaFiltroStatoDocumento.Caption = "Filtro Documento";
            this.scaFiltroStatoDocumento.Category = "Edit";
            this.scaFiltroStatoDocumento.ConfirmationMessage = null;
            this.scaFiltroStatoDocumento.Id = "scaFiltroStatoDocumento";
            this.scaFiltroStatoDocumento.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.scaFiltroStatoDocumento.ToolTip = null;
            this.scaFiltroStatoDocumento.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.scaFiltroStatoDocumento.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaFiltroStatoDocumento_Execute);
            // 
            // saCaricadaDirectory
            // 
            this.saCaricadaDirectory.Caption = "Caricada Directory";
            this.saCaricadaDirectory.Category = "Edit";
            this.saCaricadaDirectory.ConfirmationMessage = null;
            this.saCaricadaDirectory.Id = "saCaricadaDirectory";
            this.saCaricadaDirectory.ToolTip = null;
            this.saCaricadaDirectory.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saCaricadaDirectory_Execute);
            // 
            // ContrattiConsipDettagliController
            // 
            this.Actions.Add(this.scaFiltroStatoDocumento);
            this.Actions.Add(this.saCaricadaDirectory);
            this.TargetObjectType = typeof(CAMS.Module.DBClienti.ContrattiConsipDettagli);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaFiltroStatoDocumento;
        private DevExpress.ExpressApp.Actions.SimpleAction saCaricadaDirectory;
    }
}
