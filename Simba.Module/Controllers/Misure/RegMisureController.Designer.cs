namespace CAMS.Module.Controllers.Misure
{
    partial class RegMisureController
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
            this.saPredisponiMisure = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saPredisponiMisure
            // 
            this.saPredisponiMisure.Caption = "Predisponi Misure";
            this.saPredisponiMisure.ConfirmationMessage = null;
            this.saPredisponiMisure.Id = "saPredisponiMisure";
            this.saPredisponiMisure.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saPredisponiMisure.ToolTip = null;
            this.saPredisponiMisure.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saPredisponiMisure.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saPredisponiMisure_Execute);
            // 
            // RegMisureController
            // 
            this.Actions.Add(this.saPredisponiMisure);
            this.TargetObjectType = typeof(CAMS.Module.DBMisure.RegMisure);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saPredisponiMisure;
    }
}
