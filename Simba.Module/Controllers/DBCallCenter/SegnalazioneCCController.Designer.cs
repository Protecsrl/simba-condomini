namespace CAMS.Module.Controllers.DBCallCenter
{
    partial class SegnalazioneCCController
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
            this.CreaRdL = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinEdificioCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelEdificioCC = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinEditRichiedenteCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelLocaleCC = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinLocaleCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinApparatoCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelApparatoCC = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinApparatoMapCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinNewRichiedenteRdLCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinSearchRichiedenteCC = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelRichiedenteCC = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // CreaRdL
            // 
            this.CreaRdL.Caption = "Crea Rd L";
            this.CreaRdL.ConfirmationMessage = null;
            this.CreaRdL.Id = "CreaRdL";
            this.CreaRdL.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.CreaRdL.ToolTip = null;
            this.CreaRdL.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.CreaRdL.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.CreaRdL_Execute);
            // 
            // pupWinEdificioCC
            // 
            this.pupWinEdificioCC.AcceptButtonCaption = null;
            this.pupWinEdificioCC.CancelButtonCaption = null;
            this.pupWinEdificioCC.Caption = "Immobile";
            this.pupWinEdificioCC.Category = "MyHiddenCategory";
            this.pupWinEdificioCC.ConfirmationMessage = null;
            this.pupWinEdificioCC.Id = "pupWinEdificioCC";
            this.pupWinEdificioCC.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinEdificioCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinEdificioCC.TargetObjectsCriteria = "[Oid]=-1";
            this.pupWinEdificioCC.TargetViewId = "";
            this.pupWinEdificioCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinEdificioCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinEdificioCC.ToolTip = "Ricerca Immobile";
            this.pupWinEdificioCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinEdificioCC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinEdificioCC_CustomizePopupWindowParams);
            this.pupWinEdificioCC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinEdificioCC_Execute);
            // 
            // acDelEdificioCC
            // 
            this.acDelEdificioCC.Caption = "Del Immobile";
            this.acDelEdificioCC.Category = "MyHiddenCategory";
            this.acDelEdificioCC.ConfirmationMessage = null;
            this.acDelEdificioCC.Id = "acDelEdificioCC";
            this.acDelEdificioCC.ImageName = "Action_Clear";
            this.acDelEdificioCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelEdificioCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelEdificioCC.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelEdificioCC.TargetViewId = "";
            this.acDelEdificioCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.acDelEdificioCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelEdificioCC.ToolTip = "Reset Immobile";
            this.acDelEdificioCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDelEdificioCC.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDelEdificioCC_Execute);
            // 
            // pupWinEditRichiedenteCC
            // 
            this.pupWinEditRichiedenteCC.AcceptButtonCaption = null;
            this.pupWinEditRichiedenteCC.CancelButtonCaption = null;
            this.pupWinEditRichiedenteCC.Caption = "Edit";
            this.pupWinEditRichiedenteCC.Category = "MyHiddenCategory";
            this.pupWinEditRichiedenteCC.ConfirmationMessage = null;
            this.pupWinEditRichiedenteCC.Id = "pupWinEditRichiedenteCC";
            this.pupWinEditRichiedenteCC.ImageName = "Action_Edit";
            this.pupWinEditRichiedenteCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinEditRichiedenteCC.ToolTip = null;
            this.pupWinEditRichiedenteCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // acDelLocaleCC
            // 
            this.acDelLocaleCC.Caption = "Del";
            this.acDelLocaleCC.Category = "MyHiddenCategory";
            this.acDelLocaleCC.ConfirmationMessage = null;
            this.acDelLocaleCC.Id = "acDelLocaleCC";
            this.acDelLocaleCC.ImageName = "Action_Clear";
            this.acDelLocaleCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelLocaleCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelLocaleCC.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelLocaleCC.TargetViewId = "";
            this.acDelLocaleCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelLocaleCC.ToolTip = null;
            this.acDelLocaleCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // pupWinLocaleCC
            // 
            this.pupWinLocaleCC.AcceptButtonCaption = null;
            this.pupWinLocaleCC.CancelButtonCaption = null;
            this.pupWinLocaleCC.Caption = "Locale";
            this.pupWinLocaleCC.Category = "MyHiddenCategory";
            this.pupWinLocaleCC.ConfirmationMessage = null;
            this.pupWinLocaleCC.Id = "pupWinLocaleCC";
            this.pupWinLocaleCC.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinLocaleCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinLocaleCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinLocaleCC.TargetObjectsCriteria = "[Oid]=-1";
            this.pupWinLocaleCC.TargetViewId = "";
            this.pupWinLocaleCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinLocaleCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinLocaleCC.ToolTip = null;
            this.pupWinLocaleCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // pupWinApparatoCC
            // 
            this.pupWinApparatoCC.AcceptButtonCaption = null;
            this.pupWinApparatoCC.CancelButtonCaption = null;
            this.pupWinApparatoCC.Caption = "Apparato";
            this.pupWinApparatoCC.Category = "MyHiddenCategory";
            this.pupWinApparatoCC.ConfirmationMessage = null;
            this.pupWinApparatoCC.Id = "pupWinApparatoCC";
            this.pupWinApparatoCC.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinApparatoCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinApparatoCC.TargetObjectsCriteria = "[Oid]=-1";
            this.pupWinApparatoCC.TargetViewId = "";
            this.pupWinApparatoCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinApparatoCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinApparatoCC.ToolTip = "Ricerca Apparato";
            this.pupWinApparatoCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // acDelApparatoCC
            // 
            this.acDelApparatoCC.Caption = "ac Del Apparato CC";
            this.acDelApparatoCC.Category = "MyHiddenCategory";
            this.acDelApparatoCC.ConfirmationMessage = null;
            this.acDelApparatoCC.Id = "acDelApparatoCC";
            this.acDelApparatoCC.ImageName = "Action_Clear";
            this.acDelApparatoCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelApparatoCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelApparatoCC.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelApparatoCC.TargetViewId = "";
            this.acDelApparatoCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.acDelApparatoCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelApparatoCC.ToolTip = "Reset Apparato";
            this.acDelApparatoCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // pupWinApparatoMapCC
            // 
            this.pupWinApparatoMapCC.AcceptButtonCaption = null;
            this.pupWinApparatoMapCC.CancelButtonCaption = "";
            this.pupWinApparatoMapCC.Caption = "ApparatoMap";
            this.pupWinApparatoMapCC.Category = "MyHiddenCategory";
            this.pupWinApparatoMapCC.ConfirmationMessage = null;
            this.pupWinApparatoMapCC.Id = "pupWinApparatoMapCC";
            this.pupWinApparatoMapCC.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinApparatoMapCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinApparatoMapCC.TargetObjectsCriteria = "[Oid]=-1 And not (Asset  is null) and Asset.NumAppGeo>0";
            this.pupWinApparatoMapCC.TargetViewId = "";
            this.pupWinApparatoMapCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinApparatoMapCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinApparatoMapCC.ToolTip = "Ricerca Apparato su Mappa";
            this.pupWinApparatoMapCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // pupWinNewRichiedenteRdLCC
            // 
            this.pupWinNewRichiedenteRdLCC.AcceptButtonCaption = null;
            this.pupWinNewRichiedenteRdLCC.CancelButtonCaption = null;
            this.pupWinNewRichiedenteRdLCC.Caption = "New";
            this.pupWinNewRichiedenteRdLCC.Category = "MyHiddenCategory";
            this.pupWinNewRichiedenteRdLCC.ConfirmationMessage = null;
            this.pupWinNewRichiedenteRdLCC.Id = "pupWinNewRichiedenteRdLCC";
            this.pupWinNewRichiedenteRdLCC.ImageName = "NewContact";
            this.pupWinNewRichiedenteRdLCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinNewRichiedenteRdLCC.TargetObjectsCriteria = "[Oid]=-1";
            this.pupWinNewRichiedenteRdLCC.TargetViewId = "";
            this.pupWinNewRichiedenteRdLCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinNewRichiedenteRdLCC.ToolTip = "Nuovo Richiedente";
            this.pupWinNewRichiedenteRdLCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinNewRichiedenteRdLCC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinEdificioCC_CustomizePopupWindowParams);
            this.pupWinNewRichiedenteRdLCC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinEdificioCC_Execute);
            // 
            // pupWinSearchRichiedenteCC
            // 
            this.pupWinSearchRichiedenteCC.AcceptButtonCaption = null;
            this.pupWinSearchRichiedenteCC.CancelButtonCaption = null;
            this.pupWinSearchRichiedenteCC.Caption = "New";
            this.pupWinSearchRichiedenteCC.Category = "MyHiddenCategory";
            this.pupWinSearchRichiedenteCC.ConfirmationMessage = null;
            this.pupWinSearchRichiedenteCC.Id = "pupWinSearchRichiedenteCC";
            this.pupWinSearchRichiedenteCC.ImageName = "NewContact";
            this.pupWinSearchRichiedenteCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinSearchRichiedenteCC.TargetObjectsCriteria = "[Oid]=-1";
            this.pupWinSearchRichiedenteCC.TargetViewId = "";
            this.pupWinSearchRichiedenteCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinSearchRichiedenteCC.ToolTip = "Nuovo Richiedente";
            this.pupWinSearchRichiedenteCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinSearchRichiedenteCC.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinSearchRichiedenteCC_CustomizePopupWindowParams);
            this.pupWinSearchRichiedenteCC.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinSearchRichiedenteCC_Execute);
            // 
            // acDelRichiedenteCC
            // 
            this.acDelRichiedenteCC.Caption = "Del Richiedente";
            this.acDelRichiedenteCC.Category = "MyHiddenCategory";
            this.acDelRichiedenteCC.ConfirmationMessage = null;
            this.acDelRichiedenteCC.Id = "acDelRichiedenteCC";
            this.acDelRichiedenteCC.ImageName = "Action_Clear";
            this.acDelRichiedenteCC.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelRichiedenteCC.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelRichiedenteCC.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelRichiedenteCC.TargetViewId = "";
            this.acDelRichiedenteCC.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.acDelRichiedenteCC.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelRichiedenteCC.ToolTip = "Reset Apparato";
            this.acDelRichiedenteCC.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            // 
            // SegnalazioneCCController
            // 
            this.Actions.Add(this.CreaRdL);
            this.Actions.Add(this.pupWinEdificioCC);
            this.Actions.Add(this.acDelEdificioCC);
            this.Actions.Add(this.pupWinEditRichiedenteCC);
            this.Actions.Add(this.acDelLocaleCC);
            this.Actions.Add(this.pupWinLocaleCC);
            this.Actions.Add(this.pupWinApparatoCC);
            this.Actions.Add(this.acDelApparatoCC);
            this.Actions.Add(this.pupWinApparatoMapCC);
            this.Actions.Add(this.pupWinNewRichiedenteRdLCC);
            this.Actions.Add(this.pupWinSearchRichiedenteCC);
            this.Actions.Add(this.acDelRichiedenteCC);
            this.TargetObjectType = typeof(CAMS.Module.DBCallCenter.SegnalazioneCC);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SimpleAction CreaRdL;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinEdificioCC;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelEdificioCC;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinEditRichiedenteCC;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelLocaleCC;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinLocaleCC;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinApparatoCC;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelApparatoCC;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinApparatoMapCC;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinNewRichiedenteRdLCC;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinSearchRichiedenteCC;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelRichiedenteCC;
    }
}
