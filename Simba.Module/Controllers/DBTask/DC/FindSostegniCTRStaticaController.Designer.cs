namespace CAMS.Module.Controllers.DBTask.DC
{
    partial class FindSostegniCTRStaticaController
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
            this.saFindSostegniCTRStatica = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saFindSostegniCTRStatica
            // 
            this.saFindSostegniCTRStatica.Caption = "Find Sostegni CTRStatica";
            this.saFindSostegniCTRStatica.ConfirmationMessage = null;
            this.saFindSostegniCTRStatica.Id = "saFindSostegniCTRStatica";
            this.saFindSostegniCTRStatica.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saFindSostegniCTRStatica.ToolTip = null;
            this.saFindSostegniCTRStatica.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saFindSostegniCTRStatica.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saFindSostegniCTRStatica_Execute);
            // 
            // FindSostegniCTRStaticaController
            // 
            this.Actions.Add(this.saFindSostegniCTRStatica);
            this.TargetObjectType = typeof(CAMS.Module.DBMaps.DC.FindSostegniCTRStatica);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saFindSostegniCTRStatica;
    }
}
