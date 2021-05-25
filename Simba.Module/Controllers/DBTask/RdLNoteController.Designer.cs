namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLNoteController
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
            this.pupWinAddNoteRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinNewRichiedenteNote = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // pupWinAddNoteRdL
            // 
            this.pupWinAddNoteRdL.AcceptButtonCaption = null;
            this.pupWinAddNoteRdL.CancelButtonCaption = null;
            this.pupWinAddNoteRdL.Caption = "Aggiungi Note";
            this.pupWinAddNoteRdL.Category = "ObjectsCreation";
            this.pupWinAddNoteRdL.ConfirmationMessage = null;
            this.pupWinAddNoteRdL.Id = "pupWinAddNoteRdL";
            this.pupWinAddNoteRdL.TargetViewId = "RdL_RdLNotes_ListView;RdL_RdLNotes_ListView_Cliente_Intercenter";
            this.pupWinAddNoteRdL.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupWinAddNoteRdL.ToolTip = null;
            this.pupWinAddNoteRdL.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupWinAddNoteRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinAddNoteRdL_CustomizePopupWindowParams);
            this.pupWinAddNoteRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinAddNoteRdL_Execute);
            // 
            // pupWinNewRichiedenteNote
            // 
            this.pupWinNewRichiedenteNote.AcceptButtonCaption = null;
            this.pupWinNewRichiedenteNote.CancelButtonCaption = null;
            this.pupWinNewRichiedenteNote.Caption = "New";
            this.pupWinNewRichiedenteNote.Category = "MyHiddenCategory";
            this.pupWinNewRichiedenteNote.ConfirmationMessage = null;
            this.pupWinNewRichiedenteNote.Id = "pupWinNewRichiedenteNote";
            this.pupWinNewRichiedenteNote.ImageName = "NewContact";
            this.pupWinNewRichiedenteNote.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinNewRichiedenteNote.TargetObjectsCriteria = "[Oid]=-1 And TipoNotaRdL.IsConRichiedente == true";
            this.pupWinNewRichiedenteNote.TargetViewId = "";
            this.pupWinNewRichiedenteNote.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinNewRichiedenteNote.ToolTip = "Nuovo Richiedente";
            this.pupWinNewRichiedenteNote.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinNewRichiedenteNote.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinNewRichiedenteNote_CustomizePopupWindowParams);
            this.pupWinNewRichiedenteNote.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinNewRichiedenteNote_Execute);
            // 
            // RdLNoteController
            // 
            this.Actions.Add(this.pupWinAddNoteRdL);
            this.Actions.Add(this.pupWinNewRichiedenteNote);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RdLNote);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinAddNoteRdL;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinNewRichiedenteNote;
    }
}
