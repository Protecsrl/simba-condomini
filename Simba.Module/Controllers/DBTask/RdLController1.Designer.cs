namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLController1
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
            this.RdLInvioMail = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.RdLInvioSMS = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acRegistroCostiRicavi = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saShowMessaggioSLA = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // RdLInvioMail
            // 
            this.RdLInvioMail.Caption = "RdLInvioMail";
            this.RdLInvioMail.ConfirmationMessage = null;
            this.RdLInvioMail.Id = "RdLInvioMail";
            this.RdLInvioMail.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.RdLInvioMail.TargetViewId = "no";
            this.RdLInvioMail.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.RdLInvioMail.ToolTip = null;
            this.RdLInvioMail.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.RdLInvioMail.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.RdLInvioMail_Execute);
            // 
            // RdLInvioSMS
            // 
            this.RdLInvioSMS.Caption = "RdL InvioSMS";
            this.RdLInvioSMS.ConfirmationMessage = null;
            this.RdLInvioSMS.Id = "RdLInvioSMS";
            this.RdLInvioSMS.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.RdLInvioSMS.TargetViewId = "no";
            this.RdLInvioSMS.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.RdLInvioSMS.ToolTip = null;
            this.RdLInvioSMS.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.RdLInvioSMS.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.RdLInvioSMS_Execute);
            // 
            // acRegistroCostiRicavi
            // 
            this.acRegistroCostiRicavi.Caption = "nuovo Registro Lavori";
            this.acRegistroCostiRicavi.ConfirmationMessage = null;
            this.acRegistroCostiRicavi.Id = "acRegistroCostiRicavi";
            this.acRegistroCostiRicavi.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In (4,3,11,8,9)";
            this.acRegistroCostiRicavi.TargetViewId = "RdL_DetailView_Gestione";
            this.acRegistroCostiRicavi.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acRegistroCostiRicavi.ToolTip = null;
            this.acRegistroCostiRicavi.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acRegistroCostiRicavi.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acRegistroCostiRicavi_Execute);
            // 
            // saShowMessaggioSLA
            // 
            this.saShowMessaggioSLA.Caption = "Show Messaggio";
            this.saShowMessaggioSLA.Category = "MyHiddenCategory";
            this.saShowMessaggioSLA.ConfirmationMessage = null;
            this.saShowMessaggioSLA.Id = "saShowMessaggioSLA";
            this.saShowMessaggioSLA.ImageName = "note-32x32";
            this.saShowMessaggioSLA.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saShowMessaggioSLA.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saShowMessaggioSLA.TargetObjectsCriteria = "[<SetMessaggioCommessa>][^.Categoria.Oid = Categoria.Oid And ^.Asset.Oid = Imp" +
    "ianto.Oid].Exists";
            this.saShowMessaggioSLA.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saShowMessaggioSLA.ToolTip = null;
            this.saShowMessaggioSLA.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saShowMessaggioSLA.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saShowMessaggioSLA_Execute);
            // 
            // RdLController1
            // 
            this.Actions.Add(this.RdLInvioMail);
            this.Actions.Add(this.RdLInvioSMS);
            this.Actions.Add(this.acRegistroCostiRicavi);
            this.Actions.Add(this.saShowMessaggioSLA);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RdL);
            this.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction RdLInvioMail;
        private DevExpress.ExpressApp.Actions.SimpleAction RdLInvioSMS;
        private DevExpress.ExpressApp.Actions.SimpleAction acRegistroCostiRicavi;
        private DevExpress.ExpressApp.Actions.SimpleAction saShowMessaggioSLA;
    }
}
