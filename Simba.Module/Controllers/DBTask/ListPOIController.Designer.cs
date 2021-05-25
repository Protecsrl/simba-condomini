namespace CAMS.Module.Controllers.DBTask
{
    partial class ListPOIController
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
            this.scafiltroAnno = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.scafiltroEdificio = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // scafiltroAnno
            // 
            this.scafiltroAnno.Caption = "Anno";
            this.scafiltroAnno.ConfirmationMessage = null;
            this.scafiltroAnno.Id = "scafiltroAnno";
            this.scafiltroAnno.ToolTip = null;
            this.scafiltroAnno.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scafiltroAnno_Execute);
            // 
            // scafiltroEdificio
            // 
            this.scafiltroEdificio.Caption = "scafiltro Immobile";
            this.scafiltroEdificio.ConfirmationMessage = null;
            this.scafiltroEdificio.Id = "scafiltroEdificio";
            this.scafiltroEdificio.ToolTip = null;
            this.scafiltroEdificio.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scafiltroEdificio_Execute);
            // 
            // ListPOIController
            // 
            this.Actions.Add(this.scafiltroAnno);
            this.Actions.Add(this.scafiltroEdificio);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.POI.ListPOI);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction scafiltroAnno;
        private DevExpress.ExpressApp.Actions.SingleChoiceAction scafiltroEdificio;
    }
}
