namespace CAMS.Module.Controllers.DBTask
{
    partial class RdLController
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
            this.CreaRdLNotificaEmergenza = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinEdificio = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelEdificio = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinEditRichiedente = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelLocale = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinLocale = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinApparato = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.acDelApparato = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinApparatoMap = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.MigrazioneMPinTT = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saAnnullamentoMP = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saSepara = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinNewRichiedenteRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinucApparatoMapRdL = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            // 
            // CreaRdLNotificaEmergenza
            // 
            this.CreaRdLNotificaEmergenza.AcceptButtonCaption = "Conferma";
            this.CreaRdLNotificaEmergenza.ActionMeaning = DevExpress.ExpressApp.Actions.ActionMeaning.Accept;
            this.CreaRdLNotificaEmergenza.CancelButtonCaption = "Annulla";
            this.CreaRdLNotificaEmergenza.Caption = "Assegna In Emergenza";
            this.CreaRdLNotificaEmergenza.Category = "RecordEdit";
            this.CreaRdLNotificaEmergenza.ConfirmationMessage = "Vuoi Creare Una Procedura di Emegenza per questa RdL?";
            this.CreaRdLNotificaEmergenza.Id = "CreaRdLNotificaEmergenza";
            this.CreaRdLNotificaEmergenza.ImageName = "TeamRisorse";
            this.CreaRdLNotificaEmergenza.IsSizeable = false;
            this.CreaRdLNotificaEmergenza.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.CreaRdLNotificaEmergenza.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.CreaRdLNotificaEmergenza.TargetObjectsCriteria = "Oid > 0 And [UltimoStatoSmistamento.Oid] = 1 And  Categoria.Oid = 4";
            this.CreaRdLNotificaEmergenza.TargetViewId = "RdL_DetailView_Gestione";
            this.CreaRdLNotificaEmergenza.ToolTip = "Assegna In Emergenza al Team";
            this.CreaRdLNotificaEmergenza.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.CreaRdLNotificaEmergenza.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.CreaRdLNotificaEmergenza_CustomizePopupWindowParams);
            this.CreaRdLNotificaEmergenza.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.CreaRdLNotificaEmergenza_Execute);
            // 
            // pupWinEdificio
            // 
            this.pupWinEdificio.AcceptButtonCaption = null;
            this.pupWinEdificio.CancelButtonCaption = null;
            this.pupWinEdificio.Caption = "Immobile";
            this.pupWinEdificio.Category = "MyHiddenCategory";
            this.pupWinEdificio.ConfirmationMessage = null;
            this.pupWinEdificio.Id = "pupWinEdificio";
            this.pupWinEdificio.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinEdificio.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinEdificio.TargetObjectsCriteria = "[Oid]=-1";
            this.pupWinEdificio.TargetViewId = "RdL_DetailView;RdL_DetailView_Gestione;RdL_DetailView_NuovoTT;RdL_DetailView_Nuov" +
    "oTT_CC";
            this.pupWinEdificio.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinEdificio.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinEdificio.ToolTip = "Ricerca Immobile";
            this.pupWinEdificio.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinEdificio.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinEdificio_CustomizePopupWindowParams);
            this.pupWinEdificio.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinEdificio_Execute);
            // 
            // acDelEdificio
            // 
            this.acDelEdificio.Caption = "Del Immobile";
            this.acDelEdificio.Category = "MyHiddenCategory";
            this.acDelEdificio.ConfirmationMessage = null;
            this.acDelEdificio.Id = "acDelEdificio";
            this.acDelEdificio.ImageName = "Action_Clear";
            this.acDelEdificio.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelEdificio.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelEdificio.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelEdificio.TargetViewId = "RdL_DetailView;RdL_DetailView_Gestione;RdL_DetailView_NuovoTT;RdL_DetailView_Nuov" +
    "oTT_CC";
            this.acDelEdificio.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.acDelEdificio.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelEdificio.ToolTip = "Reset Immobile";
            this.acDelEdificio.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDelEdificio.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDelEdificio_Execute);
            // 
            // pupWinEditRichiedente
            // 
            this.pupWinEditRichiedente.AcceptButtonCaption = null;
            this.pupWinEditRichiedente.CancelButtonCaption = null;
            this.pupWinEditRichiedente.Caption = "Edit";
            this.pupWinEditRichiedente.Category = "MyHiddenCategory";
            this.pupWinEditRichiedente.ConfirmationMessage = null;
            this.pupWinEditRichiedente.Id = "pupWinEditRichiedente";
            this.pupWinEditRichiedente.ImageName = "Action_Edit";
            this.pupWinEditRichiedente.TargetObjectsCriteria = "[Oid]=-1 And !IsNullOrEmpty(Immobile)";
            this.pupWinEditRichiedente.TargetViewId = "RdL_DetailView;RdL_DetailView_Gestione";
            this.pupWinEditRichiedente.ToolTip = null;
            this.pupWinEditRichiedente.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinEditRichiedente_CustomizePopupWindowParams);
            this.pupWinEditRichiedente.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinEditRichiedente_Execute);
            // 
            // acDelLocale
            // 
            this.acDelLocale.Caption = "Del";
            this.acDelLocale.Category = "MyHiddenCategory";
            this.acDelLocale.ConfirmationMessage = null;
            this.acDelLocale.Id = "acDelLocale";
            this.acDelLocale.ImageName = "Action_Clear";
            this.acDelLocale.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelLocale.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelLocale.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelLocale.TargetViewId = "RdL_DetailView;RdL_DetailView_NuovoTT;RdL_DetailView_Gestione";
            this.acDelLocale.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelLocale.ToolTip = null;
            this.acDelLocale.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDelLocale.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDelLocale_Execute);
            // 
            // pupWinLocale
            // 
            this.pupWinLocale.AcceptButtonCaption = null;
            this.pupWinLocale.CancelButtonCaption = null;
            this.pupWinLocale.Caption = "Locale";
            this.pupWinLocale.Category = "MyHiddenCategory";
            this.pupWinLocale.ConfirmationMessage = null;
            this.pupWinLocale.Id = "pupWinLocale";
            this.pupWinLocale.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinLocale.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinLocale.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinLocale.TargetObjectsCriteria = "[Oid]=-1 And !IsNullOrEmpty(Immobile)";
            this.pupWinLocale.TargetViewId = "RdL_DetailView;RdL_DetailView_NuovoTT;RdL_DetailView_Gestione";
            this.pupWinLocale.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinLocale.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinLocale.ToolTip = null;
            this.pupWinLocale.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinLocale.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinLocale_CustomizePopupWindowParams);
            this.pupWinLocale.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinLocale_Execute);
            // 
            // pupWinApparato
            // 
            this.pupWinApparato.AcceptButtonCaption = null;
            this.pupWinApparato.CancelButtonCaption = null;
            this.pupWinApparato.Caption = "Apparato";
            this.pupWinApparato.Category = "MyHiddenCategory";
            this.pupWinApparato.ConfirmationMessage = null;
            this.pupWinApparato.Id = "pupWinApparato";
            this.pupWinApparato.ImageName = "GrigliaLente_x20";
            this.pupWinApparato.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinApparato.TargetObjectsCriteria = "([Oid]=-1 And Asset is not null) Or (Oid>0 And UltimoStatoSmistamento.Oid In(1" +
    ",11))";
            this.pupWinApparato.TargetViewId = "RdL_DetailView;RdL_DetailView_NuovoTT;RdL_DetailView_Gestione";
            this.pupWinApparato.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinApparato.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinApparato.ToolTip = "Ricerca Apparato";
            this.pupWinApparato.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinApparato.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinApparato_CustomizePopupWindowParams);
            this.pupWinApparato.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinApparato_Execute);
            // 
            // acDelApparato
            // 
            this.acDelApparato.Caption = "ac Del Apparato";
            this.acDelApparato.Category = "MyHiddenCategory";
            this.acDelApparato.ConfirmationMessage = null;
            this.acDelApparato.Id = "acDelApparato";
            this.acDelApparato.ImageName = "Action_Clear";
            this.acDelApparato.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.acDelApparato.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.acDelApparato.TargetObjectsCriteria = "[Oid]=-1";
            this.acDelApparato.TargetViewId = "RdL_DetailView;RdL_DetailView_NuovoTT;RdL_DetailView_Gestione";
            this.acDelApparato.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.acDelApparato.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.acDelApparato.ToolTip = "Reset Apparato";
            this.acDelApparato.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.acDelApparato.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.acDelApparato_Execute);
            // 
            // pupWinApparatoMap
            // 
            this.pupWinApparatoMap.AcceptButtonCaption = null;
            this.pupWinApparatoMap.CancelButtonCaption = "";
            this.pupWinApparatoMap.Caption = "ApparatoMap";
            this.pupWinApparatoMap.Category = "MyHiddenCategory";
            this.pupWinApparatoMap.ConfirmationMessage = null;
            this.pupWinApparatoMap.Id = "pupWinApparatoMap";
            this.pupWinApparatoMap.ImageName = "GeoLente_x24";
            this.pupWinApparatoMap.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinApparatoMap.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinApparatoMap.TargetObjectsCriteria = "[Oid]=-1 And not (Asset  is null) and Asset.NumAppGeo>0";
            this.pupWinApparatoMap.TargetViewId = "RdL_DetailView;RdL_DetailView_NuovoTT;RdL_DetailView_Gestione";
            this.pupWinApparatoMap.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinApparatoMap.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinApparatoMap.ToolTip = "Ricerca Apparato su Mappa";
            this.pupWinApparatoMap.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinApparatoMap.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinApparatoMap_CustomizePopupWindowParams);
            this.pupWinApparatoMap.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinApparatoMap_Execute);
            // 
            // MigrazioneMPinTT
            // 
            this.MigrazioneMPinTT.Caption = "Separa RdL";
            this.MigrazioneMPinTT.Category = "Edit";
            this.MigrazioneMPinTT.ConfirmationMessage = "Vuoi Separare la RdL dal Registro di Accorpamento?";
            this.MigrazioneMPinTT.Id = "MigrazioneMPinTT";
            this.MigrazioneMPinTT.ImageName = "Action_Copy";
            this.MigrazioneMPinTT.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.MigrazioneMPinTT.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.MigrazioneMPinTT.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In (11,7,6,2,1) And [Categoria.Oid] in (1,5)";
            this.MigrazioneMPinTT.TargetViewId = "RdL_DetailView_Gestione";
            this.MigrazioneMPinTT.ToolTip = "Separa RdL ";
            this.MigrazioneMPinTT.TypeOfView = typeof(DevExpress.ExpressApp.View);
            this.MigrazioneMPinTT.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.MigrazioneMPinTT_Execute);
            // 
            // saAnnullamentoMP
            // 
            this.saAnnullamentoMP.Caption = "Annullamento MP";
            this.saAnnullamentoMP.Category = "Edit";
            this.saAnnullamentoMP.ConfirmationMessage = "Confermi che vuoi Annullare la RdL?";
            this.saAnnullamentoMP.Id = "saAnnullamentoMP";
            this.saAnnullamentoMP.ImageName = "State_Validation_Skipped";
            this.saAnnullamentoMP.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saAnnullamentoMP.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saAnnullamentoMP.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In (11,7,6,2,1) And [Categoria.Oid] in (1,5)  And Is" +
    "MP_count";
            this.saAnnullamentoMP.TargetViewId = "RegistroRdL_RdLes_ListView";
            this.saAnnullamentoMP.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saAnnullamentoMP.ToolTip = null;
            this.saAnnullamentoMP.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saAnnullamentoMP.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saAnnullamentoMP_Execute);
            // 
            // saSepara
            // 
            this.saSepara.Caption = "Separa";
            this.saSepara.Category = "Edit";
            this.saSepara.ConfirmationMessage = "Confermi che vuoi Separare la RdL In altro Registro?";
            this.saSepara.Id = "saSepara";
            this.saSepara.ImageName = "Action_Copy";
            this.saSepara.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saSepara.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saSepara.TargetObjectsCriteria = "[UltimoStatoSmistamento.Oid] In (11,7,6,2,1) And [Categoria.Oid] in (1,5) And IsM" +
    "P_count";
            this.saSepara.TargetViewId = "RegistroRdL_RdLes_ListView";
            this.saSepara.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.saSepara.ToolTip = null;
            this.saSepara.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.saSepara.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saSepara_Execute);
            // 
            // pupWinNewRichiedenteRdL
            // 
            this.pupWinNewRichiedenteRdL.AcceptButtonCaption = null;
            this.pupWinNewRichiedenteRdL.CancelButtonCaption = null;
            this.pupWinNewRichiedenteRdL.Caption = "New";
            this.pupWinNewRichiedenteRdL.Category = "MyHiddenCategory";
            this.pupWinNewRichiedenteRdL.ConfirmationMessage = null;
            this.pupWinNewRichiedenteRdL.Id = "pupWinNewRichiedenteRdL";
            this.pupWinNewRichiedenteRdL.ImageName = "NewContact";
            this.pupWinNewRichiedenteRdL.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinNewRichiedenteRdL.TargetObjectsCriteria = "[Oid]=-1 And !IsNullOrEmpty(Immobile)";
            this.pupWinNewRichiedenteRdL.TargetViewId = "RdL_DetailView;RdL_DetailView_NuovoTT;RdL_DetailView_NuovoTT_CC;RdL_DetailView_Ge" +
    "stione";
            this.pupWinNewRichiedenteRdL.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinNewRichiedenteRdL.ToolTip = "Nuovo Richiedente";
            this.pupWinNewRichiedenteRdL.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinNewRichiedenteRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinNewRichiedenteRdL_CustomizePopupWindowParams);
            this.pupWinNewRichiedenteRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinNewRichiedenteRdL_Execute);
            // 
            // pupWinucApparatoMapRdL
            // 
            this.pupWinucApparatoMapRdL.AcceptButtonCaption = null;
            this.pupWinucApparatoMapRdL.CancelButtonCaption = null;
            this.pupWinucApparatoMapRdL.Caption = "Dintorni Map";
            this.pupWinucApparatoMapRdL.Category = "MyHiddenCategory";
            this.pupWinucApparatoMapRdL.ConfirmationMessage = null;
            this.pupWinucApparatoMapRdL.Id = "pupWinucApparatoMapRdL";
            this.pupWinucApparatoMapRdL.ImageName = "GeolocLent_x24";
            this.pupWinucApparatoMapRdL.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.pupWinucApparatoMapRdL.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.pupWinucApparatoMapRdL.TargetObjectsCriteria = "[Oid]=-1 And not (Asset  is null) and Asset.NumAppGeo>0";
            this.pupWinucApparatoMapRdL.TargetViewId = "RdL_DetailView_NuovoTT;RdL_DetailView;";
            this.pupWinucApparatoMapRdL.TargetViewNesting = DevExpress.ExpressApp.Nesting.Root;
            this.pupWinucApparatoMapRdL.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.pupWinucApparatoMapRdL.ToolTip = "Ricerca Apparato su Mappa nei Dintorni";
            this.pupWinucApparatoMapRdL.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.pupWinucApparatoMapRdL.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinucApparatoMapRdL_CustomizePopupWindowParams);
            this.pupWinucApparatoMapRdL.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinucApparatoMapRdL_Execute);
            // 
            // RdLController
            // 
            this.Actions.Add(this.CreaRdLNotificaEmergenza);
            this.Actions.Add(this.pupWinEdificio);
            this.Actions.Add(this.acDelEdificio);
            this.Actions.Add(this.pupWinEditRichiedente);
            this.Actions.Add(this.acDelLocale);
            this.Actions.Add(this.pupWinLocale);
            this.Actions.Add(this.pupWinApparato);
            this.Actions.Add(this.acDelApparato);
            this.Actions.Add(this.pupWinApparatoMap);
            this.Actions.Add(this.MigrazioneMPinTT);
            this.Actions.Add(this.saAnnullamentoMP);
            this.Actions.Add(this.saSepara);
            this.Actions.Add(this.pupWinNewRichiedenteRdL);
            this.Actions.Add(this.pupWinucApparatoMapRdL);
            this.TargetObjectType = typeof(CAMS.Module.DBTask.RdL);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.PopupWindowShowAction CreaRdLNotificaEmergenza;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinEdificio;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelEdificio;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinEditRichiedente;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelLocale;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinLocale;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinApparato;
        private DevExpress.ExpressApp.Actions.SimpleAction acDelApparato;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinApparatoMap;
        private DevExpress.ExpressApp.Actions.SimpleAction MigrazioneMPinTT;
        private DevExpress.ExpressApp.Actions.SimpleAction saAnnullamentoMP;
        private DevExpress.ExpressApp.Actions.SimpleAction saSepara;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinNewRichiedenteRdL;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinucApparatoMapRdL;
    }
}
