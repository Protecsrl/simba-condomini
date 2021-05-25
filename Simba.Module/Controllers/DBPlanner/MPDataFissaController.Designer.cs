namespace CAMS.Module.Controllers.DBPlanner
{
    partial class MPDataFissaController
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
            this.acCopia_da = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.selDATA = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            this.acCopiadaApparato = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acCopiadaImpianto = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acCopiadaEdificio = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // acCopia_da
            // 
            this.acCopia_da.Caption = "Copia da";
            this.acCopia_da.ConfirmationMessage = null;
            this.acCopia_da.Id = "acCopia_da";
            this.acCopia_da.TargetObjectsCriteria = "Oid != -1";
            this.acCopia_da.TargetViewId = "MPDataFissa_DetailView_Pianifica";
            this.acCopia_da.ToolTip = "Crea una nuova Data fissa Compiando i Parametri da questa Form.";
            this.acCopia_da.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.acCopia_da_Execute);
            // 
            // selDATA
            // 
            this.selDATA.Caption = "Seleziona data Fissa";
            this.selDATA.ConfirmationMessage = "Calcolare Le date in Propagazione della data selezionata? Attenzione se il campo " +
    "data è nullo saranno resettate tutte le date.";
            this.selDATA.Id = "selDATA";
            this.selDATA.NullValuePrompt = null;
            this.selDATA.ShortCaption = "Calcola Data";
            this.selDATA.TargetObjectsCriteria = "[ApparatoSchedaMP] is not null";
            this.selDATA.TargetObjectType = typeof(CAMS.Module.DBPlanner.MPDataFissa);
            this.selDATA.TargetViewId = "MPDataFissa_DetailView_Pianifica";
            this.selDATA.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.selDATA.ToolTip = "Calcola e ricalcola Interventi da data Selezionata";
            this.selDATA.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.selDATA.ValueType = typeof(System.DateTime);
            this.selDATA.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.selDATA_Execute);
            // 
            // acCopiadaApparato
            // 
            this.acCopiadaApparato.Caption = "Copia da Apparato";
            this.acCopiadaApparato.ConfirmationMessage = null;
            this.acCopiadaApparato.Id = "acCopiadaApparato";
            this.acCopiadaApparato.TargetObjectsCriteria = "Oid != -1";
            this.acCopiadaApparato.TargetViewId = "MPDataFissa_DetailView_Pianifica";
            this.acCopiadaApparato.ToolTip = "Crea una nuova Data Fissa di questo Apparato";
            this.acCopiadaApparato.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acCopiadaApparato_Execute);
            // 
            // acCopiadaImpianto
            // 
            this.acCopiadaImpianto.Caption = "Copia da Asset";
            this.acCopiadaImpianto.ConfirmationMessage = null;
            this.acCopiadaImpianto.Id = "acCopiadaImpianto";
            this.acCopiadaImpianto.TargetObjectsCriteria = "Oid != -1";
            this.acCopiadaImpianto.TargetViewId = "MPDataFissa_DetailView_Pianifica";
            this.acCopiadaImpianto.ToolTip = "Crea una nuova Data Fissa di questo Asset";
            this.acCopiadaImpianto.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acCopiadaImpianto_Execute);
            // 
            // acCopiadaEdificio
            // 
            this.acCopiadaEdificio.Caption = "Copia da Immobile";
            this.acCopiadaEdificio.ConfirmationMessage = null;
            this.acCopiadaEdificio.Id = "acCopiadaEdificio";
            this.acCopiadaEdificio.TargetObjectsCriteria = "Oid != -1";
            this.acCopiadaEdificio.TargetViewId = "MPDataFissa_DetailView_Pianifica";
            this.acCopiadaEdificio.ToolTip = "Crea una nuova Data Fissa di questo immobile";
            this.acCopiadaEdificio.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acCopiadaEdificio_Execute);
            // 
            // MPDataFissaController
            // 
            this.Actions.Add(this.acCopia_da);
            this.Actions.Add(this.selDATA);
            this.Actions.Add(this.acCopiadaApparato);
            this.Actions.Add(this.acCopiadaImpianto);
            this.Actions.Add(this.acCopiadaEdificio);
            this.TargetObjectType = typeof(CAMS.Module.DBPlanner.MPDataFissa);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.ViewControlsCreated += new System.EventHandler(this.MPDataFissaController_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction selDATA;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction acCopia_da;
        private DevExpress.ExpressApp.Actions.SimpleAction acCopiadaApparato;
        private DevExpress.ExpressApp.Actions.SimpleAction acCopiadaImpianto;
        private DevExpress.ExpressApp.Actions.SimpleAction acCopiadaEdificio;
    }
}
