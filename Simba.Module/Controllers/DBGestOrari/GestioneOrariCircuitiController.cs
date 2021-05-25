using CAMS.Module.DBGestOrari;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.Controllers.DBGestOrari
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GestioneOrariCircuitiController : ViewController
    {
        public GestioneOrariCircuitiController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View is ListView)
            {
                setVisibleTastoAddCircuito();
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View is ListView)
            {
                setVisibleTastoAddCircuito();
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void pupWinAddCircuito_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //List<tbcircuiti> lstCircuiti = ((List<tbcircuiti>)(((DevExpress.ExpressApp.Frame)e.PopupWindow).View).SelectedObjects);
            //int AppOid = (((ListView)View).Editor).GetSelectedObjects().Cast<Apparato>().First().Oid;
            if (e.PopupWindowViewSelectedObjects.Count > 0)
            {
                var lstCircuiti = e.PopupWindowViewSelectedObjects;
                //((ReportExcel)(View.CurrentObject)).SetMemberValue("CampoObjectType", pn);   .Cast<tbcircuiti>()

                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                if (View is ListView)
                {
                    ListView Lv = (ListView)View;
                    if (Lv.Id == "GestioneNuoviOrari_GestioneOrariCircuitis_ListView" || Lv.Id == "GestioneOrari_GestioneOrariCircuitis_ListView")
                    {
                        DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id == "GestioneNuoviOrari_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                        {
                            GestioneNuoviOrari vGestioneNuoviOrari = (GestioneNuoviOrari)dvParent.CurrentObject;
                            if (vGestioneNuoviOrari != null)
                            {
                                vGestioneNuoviOrari.AggiornaCircuiti = true;
                                foreach (tbcircuiti vCirc in e.PopupWindowViewSelectedObjects)
                                {
                                    GestioneOrariCircuiti vGOC = xpObjectSpace.CreateObject<GestioneOrariCircuiti>();
                                    vGOC.Circuiti = xpObjectSpace.GetObject<tbcircuiti>(vCirc);
                                    vGOC.GestioneNuoviOrari = xpObjectSpace.GetObject<GestioneNuoviOrari>(vGestioneNuoviOrari);
                                    vGOC.Save();
                                }
                                xpObjectSpace.CommitChanges();
                                Lv.ObjectSpace.Refresh();
                                //Lv.Refresh();
                            }
                        }
                        if (dvParent.Id == "GestioneOrari_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                        {
                            GestioneOrari vGestioneOrari = (GestioneOrari)dvParent.CurrentObject;
                            if (vGestioneOrari != null)
                            {
                                foreach (tbcircuiti vCirc in e.PopupWindowViewSelectedObjects)
                                {
                                    GestioneOrariCircuiti vGOC = xpObjectSpace.CreateObject<GestioneOrariCircuiti>();
                                    vGOC.Circuiti = xpObjectSpace.GetObject<tbcircuiti>(vCirc);
                                    vGOC.GestioneOrari = xpObjectSpace.GetObject<GestioneOrari>(vGestioneOrari);
                                    vGOC.Save();
                                }
                                xpObjectSpace.CommitChanges();
                                Lv.ObjectSpace.Refresh();
                                //Lv.Refresh();
                            }
                        }
                    }
                }
            }
        }

        private void pupWinAddCircuito_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //  Scenario_ClusterImp_ListView
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                    string Stagione = string.Empty;
                    ListView Lv = (ListView)View;
                    if (Lv.Id == "GestioneNuoviOrari_GestioneOrariCircuitis_ListView" || Lv.Id == "GestioneOrari_GestioneOrariCircuitis_ListView")
                    {
                        DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id == "GestioneNuoviOrari_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                        {
                            GestioneNuoviOrari vGestioneNuoviOrari = (GestioneNuoviOrari)dvParent.CurrentObject;
                            if (vGestioneNuoviOrari != null)
                            {
                                Stagione = vGestioneNuoviOrari.Stagione;
                            }
                        }
                        if (dvParent.Id == "GestioneOrari_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                        {
                            GestioneOrari vGestioneOrari = (GestioneOrari)dvParent.CurrentObject;
                            if (vGestioneOrari != null)
                            {
                                Stagione = vGestioneOrari.Stagione;
                            }
                        }

                        CollectionSource LstClusterEdificiSelezionabili = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(tbcircuiti), "tbcircuiti_ListView_Add");
                        LstClusterEdificiSelezionabili.Criteria["Filtro_GestioneNuoviOrari.Stagione"] = CriteriaOperator.Parse("stagione = ?", Stagione);

                        var view = Application.CreateListView("tbcircuiti_ListView_Add", LstClusterEdificiSelezionabili, false);
                        var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                        e.DialogController.SaveOnAccept = false;
                        e.View = view;
                    }
                }
            }
        }

        private void setVisibleTastoAddCircuito()
        {
            if (View is ListView)
            {
                ListView Lv = (ListView)View;
                //string aa = Lv.EditView.ViewEditMode.ToString();
                if (Lv.Id == "GestioneNuoviOrari_GestioneOrariCircuitis_ListView" || Lv.Id == "GestioneOrari_GestioneOrariCircuitis_ListView")
                {
                    DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                    if (dvParent.Id == "GestioneNuoviOrari_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                    {
                        if (dvParent.ViewEditMode == ViewEditMode.Edit)
                            this.pupWinAddCircuito.Active.SetItemValue("Active", true );
                        else
                            this.pupWinAddCircuito.Active.SetItemValue("Active", false);
                    }
                }
            }
        }
        
    }
}
