namespace CAMS.Module.Controllers.DBPlant
{
    partial class ApparatoMapController
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
            this.scaFilterApparatoMap = new DevExpress.ExpressApp.Actions.SingleChoiceAction(this.components);
            this.paImpiantoMap = new DevExpress.ExpressApp.Actions.ParametrizedAction(this.components);
            this.saCreaRdLbyMap = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.pupWinRicercaStrada = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinRicercaTipoApparato = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinRicercaStradaLV = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.pupWinRicercaTipoApparatoLV = new DevExpress.ExpressApp.Actions.PopupWindowShowAction(this.components);
            this.saCreaRdLbyMapAppPadre = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            this.saVediPadre = new DevExpress.ExpressApp.Actions.SimpleAction(this.components);
            // 
            // scaFilterApparatoMap
            // 
            this.scaFilterApparatoMap.Caption = "Filter Apparato Padre";
            this.scaFilterApparatoMap.Category = "Filters";
            this.scaFilterApparatoMap.ConfirmationMessage = null;
            this.scaFilterApparatoMap.Id = "scaFilterApparatoMap";
            this.scaFilterApparatoMap.TargetObjectType = typeof(CAMS.Module.DBPlant.Vista.AssetoMap);
            this.scaFilterApparatoMap.ToolTip = null;
            this.scaFilterApparatoMap.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.scaFilterApparatoMap_Execute);
            // 
            // paImpiantoMap
            // 
            this.paImpiantoMap.Caption = "Ricerca Testo";
            this.paImpiantoMap.Category = "Edit";
            this.paImpiantoMap.ConfirmationMessage = null;
            this.paImpiantoMap.Id = "paImpiantoMap";
            this.paImpiantoMap.ImageName = "Action_Search_Object_FindObjectByID";
            this.paImpiantoMap.NullValuePrompt = "inserisci testo";
            this.paImpiantoMap.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.CaptionAndImage;
            this.paImpiantoMap.ShortCaption = null;
            this.paImpiantoMap.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.paImpiantoMap.ToolTip = null;
            this.paImpiantoMap.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.paImpiantoMap.Execute += new DevExpress.ExpressApp.Actions.ParametrizedActionExecuteEventHandler(this.paImpiantoMap_Execute);
            // 
            // saCreaRdLbyMap
            // 
            this.saCreaRdLbyMap.Caption = "Crea RdL";
            this.saCreaRdLbyMap.Category = "MyHiddenCategory";
            this.saCreaRdLbyMap.ConfirmationMessage = "Vuoi Creare RdL per questo Apparato?";
            this.saCreaRdLbyMap.Id = "saCreaRdLbyMap";
            this.saCreaRdLbyMap.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saCreaRdLbyMap.ToolTip = null;
            this.saCreaRdLbyMap.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saCreaRdLbyMap.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saCreaRdLbyMap_Execute);
            // 
            // pupWinRicercaStrada
            // 
            this.pupWinRicercaStrada.AcceptButtonCaption = null;
            this.pupWinRicercaStrada.CancelButtonCaption = null;
            this.pupWinRicercaStrada.Caption = "RicercaStrada";
            this.pupWinRicercaStrada.Category = "FullTextSearch";
            this.pupWinRicercaStrada.ConfirmationMessage = null;
            this.pupWinRicercaStrada.Id = "pupWinRicercaStrada";
            this.pupWinRicercaStrada.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinRicercaStrada.TargetViewId = "ApparatoMap_LookupListView";
            this.pupWinRicercaStrada.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupWinRicercaStrada.ToolTip = null;
            this.pupWinRicercaStrada.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupWinRicercaStrada.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinRicercaStrada_CustomizePopupWindowParams);
            this.pupWinRicercaStrada.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinRicercaStrada_Execute);
            // 
            // pupWinRicercaTipoApparato
            // 
            this.pupWinRicercaTipoApparato.AcceptButtonCaption = null;
            this.pupWinRicercaTipoApparato.CancelButtonCaption = null;
            this.pupWinRicercaTipoApparato.Caption = "Ricerca Tipo Apparato";
            this.pupWinRicercaTipoApparato.Category = "FullTextSearch";
            this.pupWinRicercaTipoApparato.ConfirmationMessage = null;
            this.pupWinRicercaTipoApparato.Id = "pupWinRicercaTipoApparato";
            this.pupWinRicercaTipoApparato.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinRicercaTipoApparato.TargetViewId = "ApparatoMap_LookupListView";
            this.pupWinRicercaTipoApparato.ToolTip = null;
            this.pupWinRicercaTipoApparato.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinRicercaTipoApparato_CustomizePopupWindowParams);
            this.pupWinRicercaTipoApparato.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinRicercaTipoApparato_Execute);
            // 
            // pupWinRicercaStradaLV
            // 
            this.pupWinRicercaStradaLV.AcceptButtonCaption = null;
            this.pupWinRicercaStradaLV.CancelButtonCaption = null;
            this.pupWinRicercaStradaLV.Caption = "RicercaStrada";
            this.pupWinRicercaStradaLV.Category = "Edit";
            this.pupWinRicercaStradaLV.ConfirmationMessage = null;
            this.pupWinRicercaStradaLV.Id = "pupWinRicercaStradaLV";
            this.pupWinRicercaStradaLV.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinRicercaStradaLV.TargetViewId = "ApparatoMap_ListView";
            this.pupWinRicercaStradaLV.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupWinRicercaStradaLV.ToolTip = null;
            this.pupWinRicercaStradaLV.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupWinRicercaStradaLV.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinRicercaStradaLV_CustomizePopupWindowParams);
            this.pupWinRicercaStradaLV.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinRicercaStradaLV_Execute);
            // 
            // pupWinRicercaTipoApparatoLV
            // 
            this.pupWinRicercaTipoApparatoLV.AcceptButtonCaption = null;
            this.pupWinRicercaTipoApparatoLV.CancelButtonCaption = null;
            this.pupWinRicercaTipoApparatoLV.Caption = "Ricerca Tipo Apparato";
            this.pupWinRicercaTipoApparatoLV.Category = "Edit";
            this.pupWinRicercaTipoApparatoLV.ConfirmationMessage = null;
            this.pupWinRicercaTipoApparatoLV.Id = "pupWinRicercaTipoApparatoLV";
            this.pupWinRicercaTipoApparatoLV.ImageName = "Action_Search_Object_FindObjectByID";
            this.pupWinRicercaTipoApparatoLV.TargetViewId = "ApparatoMap_ListView";
            this.pupWinRicercaTipoApparatoLV.TargetViewType = DevExpress.ExpressApp.ViewType.ListView;
            this.pupWinRicercaTipoApparatoLV.ToolTip = null;
            this.pupWinRicercaTipoApparatoLV.TypeOfView = typeof(DevExpress.ExpressApp.ListView);
            this.pupWinRicercaTipoApparatoLV.CustomizePopupWindowParams += new DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventHandler(this.pupWinRicercaTipoApparatoLV_CustomizePopupWindowParams);
            this.pupWinRicercaTipoApparatoLV.Execute += new DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventHandler(this.pupWinRicercaTipoApparatoLV_Execute);
            // 
            // saCreaRdLbyMapAppPadre
            // 
            this.saCreaRdLbyMapAppPadre.Caption = "Crea RdL";
            this.saCreaRdLbyMapAppPadre.Category = "MyHiddenCategory";
            this.saCreaRdLbyMapAppPadre.ConfirmationMessage = "Vuoi Creare RdL per questo Apparato?";
            this.saCreaRdLbyMapAppPadre.Id = "saCreaRdLbyMapAppPadre";
            this.saCreaRdLbyMapAppPadre.TargetObjectsCriteria = "OidApparatoPadre != 0";
            this.saCreaRdLbyMapAppPadre.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saCreaRdLbyMapAppPadre.ToolTip = null;
            this.saCreaRdLbyMapAppPadre.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saCreaRdLbyMapAppPadre.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saCreaRdLbyMapAppPadre_Execute);
            // 
            // saVediPadre
            // 
            this.saVediPadre.Caption = "Vai a Padre";
            this.saVediPadre.Category = "MyHiddenCategory";
            this.saVediPadre.ConfirmationMessage = null;
            this.saVediPadre.Id = "saVediPadre";
            this.saVediPadre.ImageName = "Preview";
            this.saVediPadre.PaintStyle = DevExpress.ExpressApp.Templates.ActionItemPaintStyle.Image;
            this.saVediPadre.SelectionDependencyType = DevExpress.ExpressApp.Actions.SelectionDependencyType.RequireSingleObject;
            this.saVediPadre.TargetViewType = DevExpress.ExpressApp.ViewType.DetailView;
            this.saVediPadre.ToolTip = "Visualizza in dettaglio Apparato Padre";
            this.saVediPadre.TypeOfView = typeof(DevExpress.ExpressApp.DetailView);
            this.saVediPadre.Execute += new DevExpress.ExpressApp.Actions.SimpleActionExecuteEventHandler(this.saVediPadre_Execute);
            // 
            // ApparatoMapController
            // 
            this.Actions.Add(this.scaFilterApparatoMap);
            this.Actions.Add(this.paImpiantoMap);
            this.Actions.Add(this.saCreaRdLbyMap);
            this.Actions.Add(this.pupWinRicercaStrada);
            this.Actions.Add(this.pupWinRicercaTipoApparato);
            this.Actions.Add(this.pupWinRicercaStradaLV);
            this.Actions.Add(this.pupWinRicercaTipoApparatoLV);
            this.Actions.Add(this.saCreaRdLbyMapAppPadre);
            this.Actions.Add(this.saVediPadre);
            this.TargetObjectType = typeof(CAMS.Module.DBPlant.Vista.AssetoMap);

        }

        #endregion

        private DevExpress.ExpressApp.Actions.SingleChoiceAction scaFilterApparatoMap;
        private DevExpress.ExpressApp.Actions.ParametrizedAction paImpiantoMap;
        private DevExpress.ExpressApp.Actions.SimpleAction saCreaRdLbyMap;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinRicercaStrada;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinRicercaTipoApparato;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinRicercaStradaLV;
        private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinRicercaTipoApparatoLV;
        private DevExpress.ExpressApp.Actions.SimpleAction saCreaRdLbyMapAppPadre;
        //private DevExpress.ExpressApp.Actions.PopupWindowShowAction pupWinCreaRdlFigli;
        //private DevExpress.ExpressApp.Actions.SimpleAction saCreaRdLApparatoFiglio;
        private DevExpress.ExpressApp.Actions.SimpleAction saVediPadre;
    }
}
