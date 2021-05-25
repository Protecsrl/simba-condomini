namespace CAMS.Module.Controllers.Tree
{
    partial class TreeController
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
            this.acShowDettaglio = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.acFindByName = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            // 
            // acShowDettaglio
            // 
            this.acShowDettaglio.Caption = "Dettaglio";
            this.acShowDettaglio.Category = "RecordEdit";
            this.acShowDettaglio.ConfirmationMessage = null;
            this.acShowDettaglio.Id = "acShowDettaglio";
            this.acShowDettaglio.ImageName = "ActionsInDetailView";
            this.acShowDettaglio.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acShowDettaglio.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acShowDettaglio.TargetViewId = "novisible";
            this.acShowDettaglio.ToolTip = "Apri il Dettaglio";
            this.acShowDettaglio.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acShowDettaglio_Execute);
            // 
            // acFindByName
            // 
            this.acFindByName.Caption = "Cerca nel Nome";
            this.acFindByName.Category = "FullTextSearch";
            this.acFindByName.ConfirmationMessage = null;
            this.acFindByName.Id = "acFindByName";
            this.acFindByName.ImageName = "Action_Search_Object_FindObjectByID";
            this.acFindByName.NullValuePrompt = "parte del testo ....";
            this.acFindByName.ShortCaption = null;
            this.acFindByName.ToolTip = null;
            this.acFindByName.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.acFindByName_Execute);
            // 
            // TreeController
            // 
            this.Actions.Add(this.acShowDettaglio);
            this.Actions.Add(this.acFindByName);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction acShowDettaglio;
        private DevExpress.ExpressApp.Actions.ParametrizedAction acFindByName;
    }
}
