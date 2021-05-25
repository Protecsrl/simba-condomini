
namespace CAMS.Module.Controllers.DBGestOrari
{
    partial class GestioneOrariObjSpaceController
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
            this.saAddCircuito = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.sAGOrariNomeGiorno = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.sAGOrariData = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.sAConfermaGOrario = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saAddCircuito
            // 
            this.saAddCircuito.Caption = "Add Circuito";
            this.saAddCircuito.Category = "MyHiddenCategory";
            this.saAddCircuito.ConfirmationMessage = "Aggiungere circuito?";
            this.saAddCircuito.Id = "saAddCircuito";
            this.saAddCircuito.ToolTip = "Aggiungi Circuito alla lista";
            this.saAddCircuito.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saAddCircuito_Execute);
            // 
            // sAGOrariNomeGiorno
            // 
            this.sAGOrariNomeGiorno.Caption = "Modifica x Giorno della Settimana";
            this.sAGOrariNomeGiorno.Category = "MyHiddenCategory";
            this.sAGOrariNomeGiorno.ConfirmationMessage = null;
            this.sAGOrariNomeGiorno.Id = "sAGOrariNomeGiorno";
            this.sAGOrariNomeGiorno.ToolTip = "Modifica tutti gli Orari del Calendario che sono del Giorno Della Settimana Impos" +
    "tato";
            this.sAGOrariNomeGiorno.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.sAGOrariNomeGiorno_Execute);
            // 
            // sAGOrariData
            // 
            this.sAGOrariData.Caption = "Modifica x Data Fissa";
            this.sAGOrariData.Category = "MyHiddenCategory";
            this.sAGOrariData.ConfirmationMessage = null;
            this.sAGOrariData.Id = "sAGOrariData";
            this.sAGOrariData.ToolTip = "Modifica tutti gli Orari del Calendario nella Data Impostata";
            this.sAGOrariData.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.sAGOrariData_Execute);
            // 
            // sAConfermaGOrario
            // 
            this.sAConfermaGOrario.Caption = "Conferma";
            this.sAConfermaGOrario.Category = "MyHiddenCategory";
            this.sAConfermaGOrario.ConfirmationMessage = "sAConfermaGOrario";
            this.sAConfermaGOrario.Id = "sAConfermaGOrario";
            this.sAConfermaGOrario.ToolTip = "Conferma lo stato della Gestione Orario e Passa Allo stato successivo";
            this.sAConfermaGOrario.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.sAConfermaGOrario_Execute);
            // 
            // GestioneOrariObjSpaceController
            // 
            this.Actions.Add(this.saAddCircuito);
            this.Actions.Add(this.sAGOrariNomeGiorno);
            this.Actions.Add(this.sAGOrariData);
            this.Actions.Add(this.sAConfermaGOrario);
            this.TargetObjectType = typeof(CAMS.Module.DBGestOrari.GestioneOrari);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saAddCircuito;
        private DevExpress.ExpressApp.Actions.SimpleAction sAGOrariNomeGiorno;
        private DevExpress.ExpressApp.Actions.SimpleAction sAGOrariData;
        private DevExpress.ExpressApp.Actions.SimpleAction sAConfermaGOrario;
    }
}
