using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
using CAMS.Module.Classi;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.ParametriPopUp;
using System.ComponentModel;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ModificaRdLController : ViewController
    {
        public ModificaRdLController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        private void pupModificaRdL_WinRisorseTeam_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {

                if (View is DetailView && e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    DetailView dv = (DetailView)View;
                    //RdL rdl = (RdL)dv.CurrentObject;
                    ModificaRdL ModificaRdL = (ModificaRdL)dv.CurrentObject;
                    int OidObjCurr = ((DCRisorseTeamRdL)(((e.PopupWindow)).View).SelectedObjects[0]).OidRisorsaTeam;
                    RisorseTeam RT = xpObjectSpace.GetObjectByKey<RisorseTeam>(OidObjCurr);
                    ModificaRdL.SetMemberValue("RisorseTeam", RT);

                    View.Refresh();
                }
                else
                {
                    MessageOptions options = new MessageOptions() { Duration = 3000, Message = "Nessun Oggetto Selezionato" };
                    options.Web.Position = InformationPosition.Top;
                    options.Type = InformationType.Success;
                    options.Win.Caption = "Avvertenza";
                    //options.CancelDelegate = CancelDelegate;
                    //options.OkDelegate = OkDelegate;
                    Application.ShowViewStrategy.ShowMessage(options);
                }
            }
        }

        private void pupModificaRdL_WinRisorseTeam_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {

            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                            "DCRisorseTeamRdL_LookupListView");
                //  filtro
                DetailView dv = (DetailView)View;
                ModificaRdL ModificaRdL = (ModificaRdL)dv.CurrentObject;
                var ParCriteria = string.Empty;

                ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);
                //-----------  filtro
                //var view = Application.CreateListView(listViewId, ListRisorseTeamLookUp, false);

                //        // ListRisorseTeamLookUp.Collection
                if (!string.IsNullOrEmpty(ModificaRdL.RicercaRisorseTeam))
                { //   Azienda Mansione  Telefono

                    string Filtro1 = string.Empty;
                    string Filtro2 = string.Empty;
                    string Filtro3 = string.Empty;
                    string Filtro4 = string.Empty;
                    string AllFilter = string.Empty;

                    if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                    {

                         Filtro1 = string.Format("Contains([RisorsaCapo],'{0}')", ModificaRdL.RicercaRisorseTeam.ToString());
                         Filtro2 = string.Format("Contains([Azienda],'{0}')", ModificaRdL.RicercaRisorseTeam.ToString());
                         Filtro3 = string.Format("Contains([Mansione],'{0}')", ModificaRdL.RicercaRisorseTeam.ToString());
                         Filtro4 = string.Format("Contains([CentroOperativo],'{0}')", ModificaRdL.RicercaRisorseTeam.ToString());
                         
                    }
                    else
                    {
                         Filtro1 = string.Format("Contains(Upper([RisorsaCapo]),'{0}')", ModificaRdL.RicercaRisorseTeam.ToUpper());
                         Filtro2 = string.Format("Contains(Upper([Azienda]),'{0}')", ModificaRdL.RicercaRisorseTeam.ToUpper());
                         Filtro3 = string.Format("Contains(Upper([Mansione]),'{0}')", ModificaRdL.RicercaRisorseTeam.ToUpper());
                         Filtro4 = string.Format("Contains(Upper([CentroOperativo]),'{0}')", ModificaRdL.RicercaRisorseTeam.ToUpper());
                                          
                    }
                    AllFilter = string.Format("{0} Or {1} Or {2} Or {3}", Filtro1, Filtro2, Filtro3, Filtro4);
                    ((ListView)lvk).Model.Filter = AllFilter;
                }

                var dc = Application.CreateController<DialogController>();
                e.DialogController.SaveOnAccept = false;
                e.View = lvk;
            }

        }


        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
                    DetailView dv = (DetailView)View;
                    ModificaRdL ModificaRdL = (ModificaRdL)dv.CurrentObject;

                    RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(ModificaRdL.Codice);
                    if (rdl.Immobile != null)
                    {
                        int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
                        using (DB db = new DB())
                        {
                            int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                            int OidEdificio = rdl.Immobile.Oid == null ? 0 : rdl.Immobile.Oid;
                            objects = db.GetTeamRisorse_for_RdL(OidEdificio, ModificaRdL.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRA);
                        }
                    }
                    //e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r =>
                    //    r.Ordinamento)
                    //    .ThenBy(r => r.NumerorAttivitaTotaliTT)
                    //    .ThenBy(r => r.Distanzakm)
                    //    .ThenBy(r => r.NumerorAttivitaTotaliMP).ToList());
                    // e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r => r.Ordinamento).ToList());
                    e.Objects = objects;
                }
            }
        }

        private void acModificaRdL_DelRisorsaTeam_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                ModificaRdL ModificaRdL = (ModificaRdL)dv.CurrentObject;
                ModificaRdL.SetMemberValue("RisorseTeam", null);
            }
        }
    }
}
