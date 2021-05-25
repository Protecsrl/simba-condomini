
namespace CAMS.Module.Controllers.DBGestOrari
{
    partial class GestioneNuoviOrariObjSpaceController
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
            this.saGNOrariAddCircuito = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saGNOrariConferma = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saGNOrariModificaFascia = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saGNOrariAddCircuito
            // 
            this.saGNOrariAddCircuito.Caption = "Add Singolo Circuito";
            this.saGNOrariAddCircuito.Category = "MyHiddenCategory";
            this.saGNOrariAddCircuito.ConfirmationMessage = null;
            this.saGNOrariAddCircuito.Id = "saGNOrariAddCircuito";
            this.saGNOrariAddCircuito.ToolTip = null;
            this.saGNOrariAddCircuito.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saGNOrariAddCircuito_Execute);
            // 
            // saGNOrariConferma
            // 
            this.saGNOrariConferma.Caption = "Conferma";
            this.saGNOrariConferma.Category = "MyHiddenCategory";
            this.saGNOrariConferma.ConfirmationMessage = null;
            this.saGNOrariConferma.Id = "saGNOrariConferma";
            this.saGNOrariConferma.ToolTip = null;
            this.saGNOrariConferma.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saGNOrariConferma_Execute);
            // 
            // saGNOrariModificaFascia
            // 
            this.saGNOrariModificaFascia.Caption = "Modifica Fascia";
            this.saGNOrariModificaFascia.Category = "MyHiddenCategory";
            this.saGNOrariModificaFascia.ConfirmationMessage = null;
            this.saGNOrariModificaFascia.Id = "saGNOrariModificaFascia";
            this.saGNOrariModificaFascia.ToolTip = null;
            this.saGNOrariModificaFascia.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saGNOrariModificaFascia_Execute);
            // 
            // GestioneNuoviOrariObjSpaceController
            // 
            this.Actions.Add(this.saGNOrariAddCircuito);
            this.Actions.Add(this.saGNOrariConferma);
            this.Actions.Add(this.saGNOrariModificaFascia);
            this.TargetObjectType = typeof(CAMS.Module.DBGestOrari.GestioneNuoviOrari);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saGNOrariAddCircuito;
        private DevExpress.ExpressApp.Actions.SimpleAction saGNOrariConferma;
        private DevExpress.ExpressApp.Actions.SimpleAction saGNOrariModificaFascia;
    }
}
