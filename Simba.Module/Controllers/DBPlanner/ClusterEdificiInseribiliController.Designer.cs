namespace CAMS.Module.Controllers.DBPlanner
{
    partial class ClusterEdificiInseribiliController
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
            this.SetClusterImpianti = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            // 
            // SetClusterImpianti
            // 
            this.SetClusterImpianti.Caption = "Set Cluster Impianti";
            this.SetClusterImpianti.ConfirmationMessage = null;
            this.SetClusterImpianti.Id = "SetClusterImpianti";
            this.SetClusterImpianti.TargetObjectType = typeof(CAMS.Module.DBPlant.ClusterEdificiInseribili);
            this.SetClusterImpianti.TargetViewId = "ClusterEdificiInseribili_ListView_Pianifica";
            this.SetClusterImpianti.ToolTip = null;
            this.SetClusterImpianti.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.SetClusterImpianti.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.SetClusterImpianti_Execute);
            // 
            // ClusterEdificiInseribiliController
            // 
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.ClusterEdificiInseribili);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction SetClusterImpianti;
    }
}
