namespace CAMS.Module.Controllers.DBPlanner
{
    partial class MPDataInizialeController
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
            this.SelDataIniziale = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // SelDataIniziale
            // 
            this.SelDataIniziale.Caption = "Set Data Iniziale";
            this.SelDataIniziale.ConfirmationMessage = null;
            this.SelDataIniziale.Id = "SelDataIniziale";
            this.SelDataIniziale.NullValuePrompt = null;
            this.SelDataIniziale.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.SelDataIniziale.ShortCaption = null;
            this.SelDataIniziale.ToolTip = null;
            this.SelDataIniziale.ValueType = typeof(System.DateTime);
            this.SelDataIniziale.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.SelDataIniziale_Execute);
            // 
            // MPDataInizialeController
            // 
            this.Actions.Add(this.SelDataIniziale);
            this.TargetObjectType = typeof(CAMS.Module.DBPlanner.MPDataIniziale);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction SelDataIniziale;
    }
}
