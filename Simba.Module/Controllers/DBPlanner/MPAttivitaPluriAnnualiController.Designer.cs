namespace CAMS.Module.Controllers.DBPlanner
{
    partial class MPAttivitaPluriAnnualiController
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
            this.SelDataPluriIniziale = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // SelDataPluriIniziale
            // 
            this.SelDataPluriIniziale.Caption = "Set Data Iniziale";
            this.SelDataPluriIniziale.ConfirmationMessage = null;
            this.SelDataPluriIniziale.Id = "SelDataPluriIniziale";
            this.SelDataPluriIniziale.NullValuePrompt = null;
            this.SelDataPluriIniziale.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireMultipleObjects;
            this.SelDataPluriIniziale.ShortCaption = null;
            this.SelDataPluriIniziale.ToolTip = null;
            this.SelDataPluriIniziale.ValueType = typeof(System.DateTime);
            this.SelDataPluriIniziale.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.SelDataPluriIniziale_Execute);
            // 
            // MPAttivitaPluriAnnualiController
            // 
            this.Actions.Add(this.SelDataPluriIniziale);
            this.TargetObjectType = typeof(CAMS.Module.DBPlanner.MPAttivitaPluriAnnuali);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.ParametrizedAction SelDataPluriIniziale;
    }
}
