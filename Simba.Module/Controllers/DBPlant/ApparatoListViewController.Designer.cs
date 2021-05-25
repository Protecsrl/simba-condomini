namespace CAMS.Module.Controllers.DBPlant
{
    partial class ApparatoListViewController
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
            this.saApparatoLViewEdit = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // saApparatoLViewEdit
            // 
            this.saApparatoLViewEdit.Caption = "sa Apparato LView Edit";
            this.saApparatoLViewEdit.Category = "RecordEdit";
            this.saApparatoLViewEdit.ConfirmationMessage = null;
            this.saApparatoLViewEdit.Id = "saApparatoLViewEdit";
            this.saApparatoLViewEdit.ImageName = "Action_Edit";
            this.saApparatoLViewEdit.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saApparatoLViewEdit.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saApparatoLViewEdit.TargetObjectType = typeof(CAMS.Module.DBPlant.Vista.ApparatoListView);
            this.saApparatoLViewEdit.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saApparatoLViewEdit.ToolTip = "Edit";
            this.saApparatoLViewEdit.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saApparatoLViewEdit.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saApparatoLViewEdit_Execute);
            // 
            // ApparatoListViewController
            // 
            this.Actions.Add(this.saApparatoLViewEdit);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.Vista.ApparatoListView);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction saApparatoLViewEdit;
    }
}
