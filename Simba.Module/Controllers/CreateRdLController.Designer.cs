namespace CAMS.Module.Controllers
{
    partial class CreateRdLController
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
            this.CreaRdLby = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreaRdLby
            // 
            this.CreaRdLby.Caption = "Crea RdL";
            this.CreaRdLby.ConfirmationMessage = "Vuoi Creare una Richiesta di Intervento ?";
            this.CreaRdLby.Id = "Crea RdL";
            this.CreaRdLby.ImageName = "NewTask";
            this.CreaRdLby.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.CreaRdLby.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.CreaRdLby.TargetObjectsCriteria = "";
            this.CreaRdLby.ToolTip = null;
            this.CreaRdLby.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreaRdLby_Execute);
            // 
            // CreateRdLController
            // 
            this.Actions.Add(this.CreaRdLby);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreaRdLby;
    }
}
