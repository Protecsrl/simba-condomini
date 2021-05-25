using CAMS.Module.DBPlanner;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;

using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.Controllers.DBPlanner
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ClusterEdificiController : ViewController
    {
        public ClusterEdificiController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void cpwAssociaClustereEdifici_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //  Scenario_ClusterImp_ListView
            if (xpObjectSpace != null)
            {              
                if (View is ListView)
                {
                    ListView Lv = (ListView)View;
                    if (Lv.Id == "Scenario_ClusterEdificis_ListView")
                    {
                        var dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id == "Scenario_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                        {
                            ClusterEdifici vClusterEdifici = (ClusterEdifici)View.CurrentObject;
                                       
                            var listViewId = "ClusterEdificiInseribili_ListView_Pianifica";
                     
                            CollectionSource LstClusterEdificiSelezionabili = (CollectionSource)Application.CreateCollectionSource(
                                                                         xpObjectSpace, typeof(ClusterEdificiInseribili), listViewId);
                            LstClusterEdificiSelezionabili.Criteria["Filtro_ClusterEdifici"] = CriteriaOperator.Parse("ClusterEdifici.Oid = " + vClusterEdifici.Oid);
                                  
                            var view = Application.CreateListView(listViewId, LstClusterEdificiSelezionabili, false);
                            var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                            e.DialogController.SaveOnAccept = false;
                            e.View = view;
                        }
                    }
                }
            }

        }

        private void cpwAssociaClustereEdifici_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).SelectedObjects;
            var lstClEdifici = ((List<ClusterEdificiInseribili>)((((DevExpress.ExpressApp.Frame)
                                                       (e.PopupWindow)).View).SelectedObjects.Cast<ClusterEdificiInseribili>()
                                                       .ToList<ClusterEdificiInseribili>()));
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is ListView)
            {
                ListView Lv = (ListView)View;
                if (Lv.Id == "Scenario_ClusterEdificis_ListView")
                {
                    foreach (ClusterEdificiInseribili cle in lstClEdifici)
                    {
                        var vEd = xpObjectSpace.GetObject<ClusterEdificiInseribili>(cle);
                        ClusterEdifici ced = xpObjectSpace.GetObject<ClusterEdifici>(cle.ClusterEdifici);
                        ced.Edificis.Add(xpObjectSpace.GetObject<Immobile>(vEd.Immobile));                      
                    }
                    xpObjectSpace.CommitChanges();
                    Lv.ObjectSpace.Refresh();
                    Lv.Refresh();
                }
            }

        }

        private void cpwAssociaRisorseTeam_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
               
                if (View is ListView)
                {
                    ListView Lv = (ListView)View;
                    if (Lv.Id == "Scenario_ClusterEdificis_ListView")
                    {
                        var dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id == "Scenario_DetailView")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                        {
                            ClusterEdifici vClusterEdifici = (ClusterEdifici)View.CurrentObject;

                            int vCOOid=0;
                            if (vClusterEdifici.CentroOperativo != null)
                                vCOOid = vClusterEdifici.CentroOperativo.Oid;
                            else
                                vCOOid = vClusterEdifici.Scenario.CentroOperativo.Oid;



                            string listViewId = "RisorseTeam_ListView";
                            CollectionSource LstTeamRisorseAssociabiliCEdifici =
                                                                                 (CollectionSource)Application.CreateCollectionSource(
                                                                                 xpObjectSpace, typeof(RisorseTeam), listViewId);
                            
                            // escludere anche: le risosrse gia associate, mettere anno ???
                            string Filtro = string.Format("CentroOperativo = {0}", vCOOid);


                            LstTeamRisorseAssociabiliCEdifici.Criteria["Filtro_TRisosrseClusterEdifici"] =
                                                                    CriteriaOperator.Parse(Filtro);
                            var view = Application.CreateListView(listViewId, LstTeamRisorseAssociabiliCEdifici, false);
                            
                            //e.DialogController.SaveOnAccept = false;                            
                            e.View = view;
                            
                        }
                    }
                }
            }
        }

        private void cpwAssociaRisorseTeam_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var lstTRisorse= ((List<RisorseTeam>)((((DevExpress.ExpressApp.Frame)
                                                       (e.PopupWindow)).View).SelectedObjects.Cast<RisorseTeam>()
                                                       .ToList<RisorseTeam>()));
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is ListView)
            {
                ListView Lv = (ListView)View;
                if (Lv.Id == "Scenario_ClusterEdificis_ListView")
                {
                    ClusterEdifici vClusterEdifici = (ClusterEdifici)View.CurrentObject;

                    foreach (RisorseTeam vTR in lstTRisorse)
                    {
                        //string Filtro = string.Format("ClusterEdifici = {0} And TeamRisorse = {1}",vTR.RisorsaCapo.CentroOperativo, vTR.Oid);
                        
                        ClusterEdifici CLNew = xpObjectSpace.GetObject<ClusterEdifici>(vClusterEdifici);
                        RisorseTeam RTNew = xpObjectSpace.GetObject<RisorseTeam>(vTR);
                        var sePresente = xpObjectSpace.FindObject<ClusterEdificiRisorseTeam>
                        (GroupOperator.And(new BinaryOperator("ClusterEdifici", CLNew.Oid), new BinaryOperator("RisorsaCapo", RTNew.RisorsaCapo.Oid)));
                       
                        if (sePresente == null)
                        {
                            ClusterEdificiRisorseTeam NuovoCLRTem = xpObjectSpace.CreateObject<ClusterEdificiRisorseTeam>();      

                            NuovoCLRTem.ClusterEdifici = xpObjectSpace.GetObject<ClusterEdifici>(vClusterEdifici);
                            NuovoCLRTem.Oid = vTR.Oid;
                            NuovoCLRTem.Anno = vTR.Anno;
                            //NuovoCLRTem.MaxCapacita = vTR.MaxCapacita;
                            //NuovoCLRTem.MediaCapacita = vTR.MediaCapacita;
                            //NuovoCLRTem.MedianaCapacita = vTR.MedianaCapacita;
                            //NuovoCLRTem.MinCapacita = vTR.MinCapacita;
                            //NuovoCLRTem.ModaCapacita = vTR.ModaCapacita;
                            NuovoCLRTem.RisorsaCapo = xpObjectSpace.GetObject<Risorse>(vTR.RisorsaCapo);
                            NuovoCLRTem.UltimoStatoOperativo = xpObjectSpace.GetObject < StatoOperativo>(vTR.UltimoStatoOperativo);
                            NuovoCLRTem.Ghost = xpObjectSpace.GetObject <Ghost> (vTR.Ghost);

                        
                            NuovoCLRTem.Save();
                           
                            //      //= xpObjectSpace.GetObject((ClusterEdifici) View.CurrentObject);
                            //vRt.ClusterEdifici = cl;
                            // salva e commitr chenfges
                        }
                    }
                    xpObjectSpace.CommitChanges();
                    Lv.ObjectSpace.Refresh();
                    Lv.Refresh();
                }
            }
        }
    }
}
