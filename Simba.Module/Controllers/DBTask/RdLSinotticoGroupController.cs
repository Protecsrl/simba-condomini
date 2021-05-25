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

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLSinotticoGroupController : ViewController
    {
        public RdLSinotticoGroupController()
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

        private void popWinselectCommesse_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void popWinselectCommesse_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //if (View is ListView)
                //{
                //    ListView Lv = (ListView)View;

                //    if (Lv.Id == "Ghost_ListView")
                //    {
                //        var listEditor = ((ListView)View).Editor as ASPxGridListEditor;

                //        var lGhostSelezionato = listEditor.GetSelectedObjects().Cast<Ghost>().ToList();
                //        var GhostSelezionato = (Ghost)lGhostSelezionato[0];
                //        var iGhostID = int.Parse(GhostSelezionato.Oid.ToString());
                //        CAMS.Module.Classi.SetVarSessione.OidMansioneGhost = GhostSelezionato.Oid;

                //        System.Data.DataTable dt = new System.Data.DataTable();
                //        using (DB db = new DB())
                //        {
                //            dt = db.GetTRisorseLiberedaAssociareByMansione(iGhostID);
                //        }

                //        var ListSelects = dt.DefaultView.Cast<System.Data.DataRowView>().Select(rv => rv.Row["RISORSATEAMOID"]);
                //        CriteriaOperator charFilter = new InOperator("Oid", ListSelects);
                //        GroupOperator Criteria = new GroupOperator(GroupOperatorType.And, charFilter);
                //        string ss = Criteria.ToString();

                //        //  var crTRisorse = string.Format("Oid In ({0})", db.GetTRisorseLiberedaAssociareByMansione(iGhostID, iCoppiaLinKata, iTAssoGostTRisorse, CAMS.Module.Classi.SetVarSessione.CorrenteUser));
                //        var listViewId = "RisorseTeam_ListView_AssociaGhost";
                //        var SelezionabiliRisorseTeam = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(RisorseTeam), listViewId);
                //        //  SelezionabiliRisorseTeam.Criteria["Filtro_TRsorses"] = CriteriaOperator.Parse(crTRisorse);
                //        SelezionabiliRisorseTeam.Criteria["filtro"] = new GroupOperator(GroupOperatorType.And, charFilter);
                //        var view = Application.CreateListView(listViewId, SelezionabiliRisorseTeam, false);
                //        var sCoppiaLinkata = GhostSelezionato.NumMan.ToString() == "No" ? string.Empty : "(Coppia Linkata)";
                //        //view.Caption = string.Format("Selezione Team Risorsa {0} da Associare a Ghost: {1}: Min:{2}, Media:{3}, Media:{4}, Mediana:{5}, Moda:{6}",
                //        //                                 sCoppiaLinkata, GhostSelezionato.Descrizione, GhostSelezionato.MinCarico
                //        //                                , GhostSelezionato.MaxCarico, GhostSelezionato.MediaCarico
                //        //                                , GhostSelezionato.MedianaCarico, GhostSelezionato.ModaCarico);
                //        view.Caption = string.Format("Selezione Team Risorsa {0} da Associare a Ghost: {1}",
                //                                  sCoppiaLinkata, GhostSelezionato.Descrizione);
                //        e.View = view;
                //        e.IsSizeable = true;
                //    }


                //    if (Lv.Id == "RegPianificazioneMP_RPMPGhosts_ListView")
                //    {
                //        var dvParent = (DetailView)(View.ObjectSpace).Owner;
                //        if (dvParent.Id == "RegPianificazioneMP_DetailView_Schedula")// && dvParent.ViewEditMode == ViewEditMode.Edit)
                //        {
                //            var listEditor = ((ListView)View).Editor as ASPxGridListEditor;

                //            var lGhostSelezionato = listEditor.GetSelectedObjects().Cast<Ghost>().ToList();
                //            var GhostSelezionato = (Ghost)lGhostSelezionato[0];

                //            oidRegistroPianificazioneMP = GhostSelezionato.RegistroPianificazioneMP.Oid;
                //            oidGhostSelezionato = GhostSelezionato.Oid;

                //            tipoDiGhost = GhostSelezionato.TipoGhost;

                //            GhostSelezionato.RegistroPianificazioneMP.TipoAssociazioneGostRisorseTeam.ToString();
                //            var iTAssoGostTRisorse = GhostSelezionato.RegistroPianificazioneMP.TipoAssociazioneGostRisorseTeam.GetHashCode();

                //            var iGhostID = int.Parse(GhostSelezionato.Oid.ToString());
                //            var iCoppiaLinKata = 1;
                //            var sCoppiaLinKata = GhostSelezionato.NumMan.ToString();
                //            if (sCoppiaLinKata == "Si")
                //            {
                //                iCoppiaLinKata = 2;
                //            }
                //            Apparato apparato = GhostSelezionato.MPAttivitaPianiDetts.Select(s => s.Apparato).First();
                //            OidEdificio = apparato.Impianto.Immobile.Oid;
                //            OidCObase = apparato.Impianto.Immobile.CentroOperativoBase == null ? 0 : apparato.Impianto.Immobile.CentroOperativoBase.Oid;
                //            int oidsm = GhostSelezionato.TipoAssociazioneTRisorsa == Module.Classi.TipoAssociazioneTRisorsa.Smartphone ? 2 : 11;
                //            OidUltimoStatoSmistamento = oidsm;// 11 gestione sala operativa ; 2 smartfone

                //            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                //            objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                //            CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                //                                                                        "DCRisorseTeamRdL_LookupListView_MP");

                //            var ParCriteria = string.Empty;

                //            ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView_MP", DCRisorseTeamRdL_Lookup, true);
                //            //-----------  filtro

                //            if (GhostSelezionato.Mansione != null)
                //            { //   Azienda Mansione  Telefono
                //                string Filtro3 = string.Empty;
                //                if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                //                    Filtro3 = string.Format("Contains([Mansione],'{0}')", GhostSelezionato.Mansione.Descrizione.ToString());
                //                else
                //                    Filtro3 = string.Format("Contains(Upper([Mansione]),'{0}')", GhostSelezionato.Mansione.Descrizione.ToUpper());

                //                string Filtro4 = string.Format("[Ordinamento] > 0");
                //                string AllFilter = string.Format("{0} And {1}", Filtro3, Filtro4);
                //                ((ListView)lvk).Model.Filter = AllFilter;
                //            }
                //            var dc = Application.CreateController<DialogController>();
                //            e.DialogController.SaveOnAccept = false;
                //            e.View = lvk;
                //        }
                //    }
                //}
            }
        }
    }
}
