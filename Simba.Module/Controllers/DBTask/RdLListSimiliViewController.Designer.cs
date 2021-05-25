namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLListSimiliViewController
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
            this.acEditRdLbySimili = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // acEditRdLbySimili
            // 
            this.acEditRdLbySimili.Caption = "Edit RdL";
            this.acEditRdLbySimili.Category = "Edit";
            this.acEditRdLbySimili.ConfirmationMessage = null;
            this.acEditRdLbySimili.Id = "acEditRdLbySimili";
            this.acEditRdLbySimili.ImageName = "MenuBar_Edit";
            this.acEditRdLbySimili.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acEditRdLbySimili.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListSimiliView);
            this.acEditRdLbySimili.TargetViewId = "RdL_ListaRdLSimilis_ListView";
            this.acEditRdLbySimili.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.acEditRdLbySimili.ToolTip = "Aggiungi un Sollecito o una Nota alla RdL Selezionata";
            this.acEditRdLbySimili.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.acEditRdLbySimili.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acEditRdLbySimili_Execute);
            // 
            // RdLListSimiliViewController
            // 
            this.Actions.Add(this.acEditRdLbySimili);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListSimiliView);
            this.TargetViewId = "RdL_ListaRdLSimilis_ListView";
            this.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.TypeOfView = typeof(DevExpress.ExpressApp.ListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acEditRdLbySimili;
    }
}
