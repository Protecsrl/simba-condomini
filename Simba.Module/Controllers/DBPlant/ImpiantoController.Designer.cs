namespace CAMS.Module.Controllers
{
    partial class ImpiantoController
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
            this.ImpiantoClonaImpianto = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.ImpiantoCreaDalTipo = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.InserisciApparati = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // ImpiantoClonaImpianto
            // 
            this.ImpiantoClonaImpianto.AcceptButtonCaption = "Crea";
            this.ImpiantoClonaImpianto.CancelButtonCaption = null;
            this.ImpiantoClonaImpianto.Caption = "Clona Asset";
            this.ImpiantoClonaImpianto.Category = "RecordEdit";
            this.ImpiantoClonaImpianto.ConfirmationMessage = null;
            this.ImpiantoClonaImpianto.Id = "ClonaImpianto";
            this.ImpiantoClonaImpianto.ImageName = "Action_CloneMerge_Clone_Object";
            this.ImpiantoClonaImpianto.IsSizeable = false;
            this.ImpiantoClonaImpianto.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.ImpiantoClonaImpianto.TargetObjectType = typeof(CAMS.Module.DBPlant.Servizio);
            this.ImpiantoClonaImpianto.TargetViewId = "Edificio_Impianti_ListView";
            this.ImpiantoClonaImpianto.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ImpiantoClonaImpianto.ToolTip = "Clona l\'impianto selezionato";
            this.ImpiantoClonaImpianto.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.ImpiantoClonaImpianto.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ImpiantoClonaImpianto_CustomizePopupWindowParams);
            this.ImpiantoClonaImpianto.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ImpiantoClona_Execute);
            // 
            // ImpiantoCreaDalTipo
            // 
            this.ImpiantoCreaDalTipo.AcceptButtonCaption = "Crea";
            this.ImpiantoCreaDalTipo.CancelButtonCaption = null;
            this.ImpiantoCreaDalTipo.Caption = "Crea da libreria impianti";
            this.ImpiantoCreaDalTipo.ConfirmationMessage = null;
            this.ImpiantoCreaDalTipo.Id = "ImpiantoCreaDalTipo";
            this.ImpiantoCreaDalTipo.ImageName = "Action_CloneMerge_Merge_Object";
            this.ImpiantoCreaDalTipo.IsSizeable = false;
            this.ImpiantoCreaDalTipo.TargetObjectType = typeof(CAMS.Module.DBPlant.Servizio);
            this.ImpiantoCreaDalTipo.TargetViewId = "Edificio_Impianti_ListView";
            this.ImpiantoCreaDalTipo.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.ImpiantoCreaDalTipo.ToolTip = "Crea Asset dalla libreria impianti";
            this.ImpiantoCreaDalTipo.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.ImpiantoCreaDalTipo.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.ImpiantoCreaDalTipo_CustomizePopupWindowParams);
            this.ImpiantoCreaDalTipo.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.ImpiantoCreaDalTipo_Execute);
            // 
            // InserisciApparati
            // 
            this.InserisciApparati.AcceptButtonCaption = null;
            this.InserisciApparati.CancelButtonCaption = null;
            this.InserisciApparati.Caption = "Inserisci Apparati";
            this.InserisciApparati.ConfirmationMessage = null;
            this.InserisciApparati.Id = "InserisciApparati";
            this.InserisciApparati.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.InserisciApparati.TargetViewId = "Impianto_DetailView";
            this.InserisciApparati.ToolTip = null;
            this.InserisciApparati.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.InserisciApparati_CustomizePopupWindowParams);
            this.InserisciApparati.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.InserisciApparati_Execute);
            // 
            // ImpiantoController
            // 
            this.Actions.Add(this.ImpiantoClonaImpianto);
            this.Actions.Add(this.ImpiantoCreaDalTipo);
            this.Actions.Add(this.InserisciApparati);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.Servizio);
            this.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.ViewControlsCreated += new System.EventHandler(this.ImpiantoController_ViewControlsCreated);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ImpiantoClonaImpianto;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction ImpiantoCreaDalTipo;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction InserisciApparati;
    }
}
