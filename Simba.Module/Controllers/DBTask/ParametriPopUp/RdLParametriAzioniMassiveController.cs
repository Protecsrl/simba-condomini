using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.ParametriPopUp;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.Controllers.DBTask.ParametriPopUp
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLParametriAzioniMassiveController : ViewController
    {
        public RdLParametriAzioniMassiveController()
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

        private void PopWinMassivoAssegnaTRisorse_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                int OidRisorsaTeamSelezionata = ((DCRisorseTeamRdL)(((e.PopupWindow)).View).SelectedObjects[0]).OidRisorsaTeam;
                //var Parametro = ((RdLParametriAzioniMassive)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));
                RdLParametriAzioniMassive ParametroAzMassive = View.CurrentObject as RdLParametriAzioniMassive;

                if (e.PopupWindow.View.SelectedObjects == null || e.PopupWindow.View.SelectedObjects.Count == 0)
                {
                    throw new UserFriendlyException("Occorre selezionare almeno una risorsa team");
                }
                if (e.PopupWindow.View.SelectedObjects.Count > 1)
                {
                    throw new UserFriendlyException("E' possibile selezionare una sola risorsa team");
                }

                RisorseTeam RT = xpObjectSpace.GetObjectByKey<RisorseTeam>(OidRisorsaTeamSelezionata);
                ParametroAzMassive.RisorseTeamNew = RT;
                ParametroAzMassive.Save();
                //string NomeRisorse = "";     
            }
        }

        int OidEdificio = 0;
        int OidCObase = 0;
        int OidUltimoStatoSmistamento = 2;
        int oidGhostSelezionato = 0;
        int oidRegistroPianificazioneMP = 0;


        private void PopWinMassivoAssegnaTRisorse_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    var cur = View.CurrentObject as RdLParametriAzioniMassive;

                    RisorseTeam RTOld = cur.RisorseTeamOld;
                    StatoSmistamento STSMIST = cur.UltimoStatoSmistamento;
                    CentroOperativo co = cur.CentroOperativo;
                    string RicercaRisorseTeam = cur.RicercaRisorseTeam;
                    OidUltimoStatoSmistamento = STSMIST.Oid;

                    if (cur != null)// && dvParent.ViewEditMode == ViewEditMode.Edit)
                    {
                        NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                        objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;
                        CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                                    "DCRisorseTeamRdL_LookupListView_MP");
                        var ParCriteria = string.Empty;
                        ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView_MP", DCRisorseTeamRdL_Lookup, true);
                        //-----------  filtro  -->
                        if (!string.IsNullOrEmpty(RicercaRisorseTeam))
                        { //   Azienda Mansione  Telefono
                            string Filtro1 = string.Format("[Ordinamento] = 2");
                            //string Filtro2 = string.Format("Contains(Upper([Azienda]),'{0}')", RicercaRisorseTeam.ToUpper());
                            //string Filtro3 = string.Format("Contains(Upper([Mansione]),'{0}')", RicercaRisorseTeam.ToUpper());
                            //string Filtro4 = string.Format("Contains(Upper([CentroOperativo]),'{0}')", co..ToUpper());
                            //string AllFilter = string.Format("{0} Or {1} Or {2} Or {3}", Filtro1, Filtro2, Filtro3, Filtro4);
                            ((ListView)lvk).Model.Filter = Filtro1;
                        }

                        var dc = Application.CreateController<DialogController>();
                        e.DialogController.SaveOnAccept = false;
                        e.View = lvk;
                    }

                }
            }
        }

        #region seleziona risorsa team
        static List<DCRisorseTeamRdL> DCRTeam { get; set; }
        TipoGhost tipoDiGhost = TipoGhost.ItinerantePerScenario;
        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            //if (View is DetailView)
            //{
            BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
            //DetailView dv = (DetailView)View;
            //RdL rdl = (RdL)dv.CurrentObject;
            if (OidEdificio != 0 && false)
            {

            }
            else
            {
                objects = GetObjRTeam(2);

            }
            e.Objects = objects;
            //}
        }


        //     (int OidEdificio, int IsSmartphone, int iOidCentroOperativoBase, string UserNameCorrente, int OidRegoleAutoAssegnazione, int OidRTeamRemove = 0)
        BindingList<DCRisorseTeamRdL> GetObjRTeam(int IsSmartphone)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)xpObjectSpace).Session;
            bool Tutti = false;
            int Oid_Risorsa_Old = 0;
            int Oid_co = 0;
            List<CentroOperativo> TuttiCOdiArea = new List<CentroOperativo>();
            List<int> OidTuttiCOdiArea = new List<int>();
            //CentroOperativo co;
            if (OidUltimoStatoSmistamento != 2)
                Tutti = true;
            else
                Tutti = false;
            //if (ghost.TipoAssociazioneTRisorsa == TipoAssociazioneTRisorsa.Smartphone)
            //    SoloSmartphone = true;
            var cur = View.CurrentObject as RdLParametriAzioniMassive;
            if (cur.RisorseTeamOld != null)
            {
                CentroOperativo co = cur.RisorseTeamOld.CentroOperativo;
                Oid_co = co.Oid;
                AreaDiPolo adp = co.AreaDiPolo;
                Oid_Risorsa_Old = cur.RisorseTeamOld.Oid;
                TuttiCOdiArea = adp.CentroOperativos.ToList();
                //OidTuttiCOdiArea = adp.CentroOperativos.Where(v => v.Oid == co.Oid).Select(s => s.Oid).ToList();
                OidTuttiCOdiArea = adp.CentroOperativos.Select(s => s.Oid).ToList();
            }
            else
            {
                CentroOperativo co = cur.CentroOperativo;
                Oid_co = co.Oid;
                AreaDiPolo adp = co.AreaDiPolo;
                Oid_Risorsa_Old = 0;
                TuttiCOdiArea = adp.CentroOperativos.ToList();
                //OidTuttiCOdiArea = adp.CentroOperativos.Where(v => v.Oid == co.Oid).Select(s => s.Oid).ToList();
                OidTuttiCOdiArea = adp.CentroOperativos.Select(s => s.Oid).ToList();
            }
            // centri operativi di area 
            //List<RisorseTeam> t_RTeamUsate = new XPQuery<GhostDettaglio>(Sess, true).Where(w => w.RisorseTeam != null).Select(s => s.RisorseTeam).ToList();
            ////List<RisorseTeam> RTeamUsate = new XPQuery<GhostDettaglio>(Sess, true).Where(w => w.RisorseTeam != null).Select(s => s.RisorseTeam).ToList();
            //List<int> OidRTeamUsate = (RTeamUsate.Select(s => s.Oid).ToArray().Union(RTeamUsate.Select(s => s.Oid).ToArray())).ToList();
            if (DCRTeam == null)
            {
                List<DCRisorseTeamRdL> objs = new XPQuery<RisorseTeam>(Sess)
                            .Where(w => OidTuttiCOdiArea.Contains(w.RisorsaCapo.CentroOperativo.Oid))
                              .Where(w => w.RisorsaCapo.TipoQualifica == TipoQualifica.Operaio)
                              .Where(w => w.Oid != Oid_Risorsa_Old)
                              .Where(w => w.UserName != null || Tutti)
                                //.Where(w => SoloSmartphone ? w.UserName != null : w.UserName == null)
                                .Select(rt => new DCRisorseTeamRdL
                                {
                                    Oid = Guid.NewGuid(),
                                    OidCentroOperativo = rt.RisorsaCapo.CentroOperativo.Oid,
                                    OidRisorsaTeam = rt.Oid,
                                    NumeroAttivitaAgenda = rt.NumeroAttivitaAgenda,
                                    NumeroAttivitaSospese = rt.NumeroAttivitaSospese,
                                    NumeroAttivitaEmergenza = 0,
                                    Conduttore = rt.RisorsaCapo.Oid > 0 ? true : false,
                                    CoppiaLinkata = (TipoNumeroManutentori)rt.AssRisorseTeam.Count,
                                    Ordinamento = 0,
                                    CentroOperativo = rt.RisorsaCapo.CentroOperativo.Descrizione,
                                    UltimoStatoOperativo = "nd",
                                    RisorsaCapo = rt.RisorsaCapo.FullName,
                                    Mansione = rt.RisorsaCapo.Mansione.Descrizione,
                                    Telefono = rt.RisorsaCapo.Telefono.ToString(),
                                    RegistroRdL = "nd",
                                    DistanzaImpianto = "0",
                                    UltimoEdificio = "nd",
                                    InterventosuEdificio = "0",
                                    Url = "",
                                    Azienda = rt.RisorsaCapo.Azienda,
                                    Aggiornamento = "nd"
                                }).ToList();

                DCRTeam = objs;
            }

            System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL> objects = new System.ComponentModel.BindingList<CAMS.Module.DBTask.DC.DCRisorseTeamRdL>();

            foreach (DCRisorseTeamRdL rt_ord in DCRTeam)
            {
                rt_ord.Conduttore = false;
                //if (oidCOSchedulazionelist.Contains(rt_ord.OidCentroOperativo))
                //    rt_ord.Ordinamento = 3;

                if (Oid_co == rt_ord.OidCentroOperativo)
                {
                    rt_ord.Conduttore = true;
                    rt_ord.Ordinamento = 2;
                }
                objects.Add(rt_ord);
            }
            DCRTeam = null;
            return objects;
        }


        #endregion







    }
}
