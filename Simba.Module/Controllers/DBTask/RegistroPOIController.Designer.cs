namespace CAMS.Module.Controllers.DBTask
{
    partial class RegistroPOIController
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
            this.saVisualizzaPOI = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saSendMailPOI = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saSchedulaPOI = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saVisualizzaPOI
            // 
            this.saVisualizzaPOI.Caption = "Visualizza POI";
            this.saVisualizzaPOI.Category = "Edit";
            this.saVisualizzaPOI.ConfirmationMessage = null;
            this.saVisualizzaPOI.Id = "saVisualizzaPOI";
            this.saVisualizzaPOI.ImageName = "Action_Printing_Preview";
            this.saVisualizzaPOI.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saVisualizzaPOI.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saVisualizzaPOI.TargetViewId = "no";
            this.saVisualizzaPOI.ToolTip = "Visualizza POI";
            this.saVisualizzaPOI.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saVisualizzaPOI_Execute);
            // 
            // saSendMailPOI
            // 
            this.saSendMailPOI.Caption = "NewMail";
            this.saSendMailPOI.Category = "Edit";
            this.saSendMailPOI.ConfirmationMessage = "Vuoi Trasmettere Avviso POI ai destinatari Previsti?";
            this.saSendMailPOI.Id = "saSendMailPOI";
            this.saSendMailPOI.ImageName = "NewMail";
            this.saSendMailPOI.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saSendMailPOI.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saSendMailPOI.ToolTip = "Spedisci avviso POI";
            this.saSendMailPOI.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saSendMailPOI_Execute);
            // 
            // saSchedulaPOI
            // 
            this.saSchedulaPOI.Caption = "sa Schedula POI";
            this.saSchedulaPOI.ConfirmationMessage = null;
            this.saSchedulaPOI.Id = "saSchedulaPOI";
            this.saSchedulaPOI.ToolTip = null;
            this.saSchedulaPOI.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saSchedulaPOI_Execute);
            // 
            // RegistroPOIController
            // 
            this.Actions.Add(this.saVisualizzaPOI);
            this.Actions.Add(this.saSendMailPOI);
            this.Actions.Add(this.saSchedulaPOI);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.POI.RegistroPOI);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saVisualizzaPOI;
        private DevExpress.ExpressApp.Actions.SimpleAction saSendMailPOI;
        private DevExpress.ExpressApp.Actions.SimpleAction saSchedulaPOI;
    }
}
