namespace CAMS.Module.Controllers.DBAgenda
{
    partial class NotificaRdLController
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
            this.popGetDCRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDchiarazioneArrivo = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acInizioTrasferimento = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acAccettaDataSO = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupCambiaRisorsaDC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // popGetDCRdL
            // 
            this.popGetDCRdL.AcceptButtonCaption = null;
            this.popGetDCRdL.CancelButtonCaption = null;
            this.popGetDCRdL.Caption = "Ricerca";
            this.popGetDCRdL.Category = "MyHiddenCategory";
            this.popGetDCRdL.ConfirmationMessage = null;
            this.popGetDCRdL.Id = "popGetDCRdL";
            this.popGetDCRdL.ImageName = "Action_Search_Object_FindObjectByID";
            this.popGetDCRdL.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.popGetDCRdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.popGetDCRdL.TargetObjectsCriteria = "Iif(RdL is null,1,RdL.UltimoStatoSmistamento.Oid)=1";
            this.popGetDCRdL.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.popGetDCRdL.ToolTip = null;
            this.popGetDCRdL.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.popGetDCRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.popGetDCRdL_CustomizePopupWindowParams);
            this.popGetDCRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.popGetDCRdL_Execute);
            // 
            // acDchiarazioneArrivo
            // 
            this.acDchiarazioneArrivo.Caption = "Dchiarazione Arrivo";
            this.acDchiarazioneArrivo.Category = "Edit";
            this.acDchiarazioneArrivo.ConfirmationMessage = null;
            this.acDchiarazioneArrivo.Id = "acDchiarazioneArrivo";
            this.acDchiarazioneArrivo.TargetObjectsCriteria = "Oid>0 And Label = 1";
            this.acDchiarazioneArrivo.TargetObjectType = typeof(CAMS.Module.DBAgenda.NotificaRdL);
            this.acDchiarazioneArrivo.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDchiarazioneArrivo.ToolTip = null;
            this.acDchiarazioneArrivo.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDchiarazioneArrivo.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDchiarazioneArrivo_Execute);
            // 
            // acInizioTrasferimento
            // 
            this.acInizioTrasferimento.Caption = "Inizio Trasferimento";
            this.acInizioTrasferimento.Category = "Edit";
            this.acInizioTrasferimento.ConfirmationMessage = null;
            this.acInizioTrasferimento.Id = "acInizioTrasferimento";
            this.acInizioTrasferimento.TargetObjectsCriteria = "Oid>0 And Label = 3";
            this.acInizioTrasferimento.TargetObjectType = typeof(CAMS.Module.DBAgenda.NotificaRdL);
            this.acInizioTrasferimento.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acInizioTrasferimento.ToolTip = null;
            this.acInizioTrasferimento.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acInizioTrasferimento.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acInizioTrasferimento_Execute);
            // 
            // acAccettaDataSO
            // 
            this.acAccettaDataSO.Caption = "Accetta SO";
            this.acAccettaDataSO.Category = "MyHide";
            this.acAccettaDataSO.ConfirmationMessage = "Vuoi Accettare Data e Ora proposta Dal Tecnico?";
            this.acAccettaDataSO.Id = "acAccettaDataSO";
            this.acAccettaDataSO.ImageName = "State_Task_Completed";
            this.acAccettaDataSO.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.acAccettaDataSO.TargetObjectsCriteria = "Oid>0 And Label = 2";
            this.acAccettaDataSO.ToolTip = null;
            this.acAccettaDataSO.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acAccettaDataSO_Execute);
            // 
            // pupCambiaRisorsaDC
            // 
            this.pupCambiaRisorsaDC.AcceptButtonCaption = null;
            this.pupCambiaRisorsaDC.CancelButtonCaption = null;
            this.pupCambiaRisorsaDC.Caption = "Cambia Risorsa";
            this.pupCambiaRisorsaDC.Category = "MyHiddenCategory";
            this.pupCambiaRisorsaDC.ConfirmationMessage = "Attenzione! Vuoi COntinuare a Cambiare la Risorsa?";
            this.pupCambiaRisorsaDC.Id = "pupCambiaRisorsaDC";
            this.pupCambiaRisorsaDC.ImageName = "BO_Employee";
            this.pupCambiaRisorsaDC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.pupCambiaRisorsaDC.TargetObjectsCriteria = "Iif(RdL is not null,2,RdL.UltimoStatoSmistamento.Oid)=2";
            this.pupCambiaRisorsaDC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupCambiaRisorsaDC.ToolTip = "per Assegnare una diversa risorsa Alla RDL.\r\nSeleziona Nuova Risorsa da Associa a" +
    "ll\'intervento";
            this.pupCambiaRisorsaDC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupCambiaRisorsaDC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupCambiaRisorsaDC_CustomizePopupWindowParams);
            this.pupCambiaRisorsaDC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupCambiaRisorsaDC_Execute);
            // 
            // NotificaRdLController
            // 
            this.Actions.Add(this.popGetDCRdL);
            this.Actions.Add(this.acDchiarazioneArrivo);
            this.Actions.Add(this.acInizioTrasferimento);
            this.Actions.Add(this.acAccettaDataSO);
            this.Actions.Add(this.pupCambiaRisorsaDC);
            this.TargetObjectType = typeof(CAMS.Module.DBAgenda.NotificaRdL);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction popGetDCRdL;
        private DevExpress.ExpressApp.Actions.SimpleAction acDchiarazioneArrivo;
        private DevExpress.ExpressApp.Actions.SimpleAction acInizioTrasferimento;
        private DevExpress.ExpressApp.Actions.SimpleAction acAccettaDataSO;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupCambiaRisorsaDC;

    }
}
