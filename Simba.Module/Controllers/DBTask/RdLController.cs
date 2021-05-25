using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBMaps;
using CAMS.Module.DBPlant;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.Guasti;
using CAMS.Module.DBTask.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace CAMS.Module.Controllers.DBTask
{
    public partial class RdLController : ViewController
    {
        private const string RegistroRdLCreaDaRdL_DetailView = "RegistroRdLCreaDaRdL_DetailView";

        public RdLController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            ///  da modificare per inserire il filtro per scenario
            //Frame.GetController<DevExpress.ExpressApp.SystemModule.FilterController>().SetFilterAction.Active.SetItemValue("Active", true);
            ///
            //COMMENTO A.N
            if (View is ListView)
            {
                var Lv = (ListView)View;
                if (Lv.Id.Contains("RdL_ListView_DaAssegnare"))
                {
                    bool isRdLGranted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                      typeof(RegNotificheEmergenze), SecurityOperations.Write, null, "Descrizione")); //selectedObject

                    if (isRdLGranted)
                        CreaRdLNotificaEmergenza.Active.SetItemValue("Active", true);
                }
                else
                    CreaRdLNotificaEmergenza.Active.SetItemValue("Active", false);
            }

            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                if (dv.CurrentObject != null)
                {
                    if (dv.Id.Contains("RdL_DetailView") && dv.ViewEditMode == ViewEditMode.Edit) //CanChangeCurrentObject
                    {
                        bool VisualizzaTasto = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                             typeof(RdL), SecurityOperations.Write, View.CurrentObject, "Apparato"));
                        pupWinApparato.Active.SetItemValue("Active", VisualizzaTasto);

                        bool isRdLGranted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                           typeof(RegNotificheEmergenze), SecurityOperations.Write, null, "Descrizione"));
                        if (isRdLGranted)
                        {
                            RdL RdLSel = (RdL)dv.CurrentObject;
                            if (RdLSel.UltimoStatoSmistamento.Oid == 1)
                            {
                                CreaRdLNotificaEmergenza.Active.SetItemValue("Active", true);
                            }
                            else
                                CreaRdLNotificaEmergenza.Active.SetItemValue("Active", false);
                        }
                        else
                            CreaRdLNotificaEmergenza.Active.SetItemValue("Active", false);
                    }
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;

                bool VisualizzaTasto = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                    typeof(RdL), SecurityOperations.Write, View.CurrentObject, "Apparato"));

                bool VisualizzaTastoLocale = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                  typeof(RdL), SecurityOperations.Write, View.CurrentObject, "Locale"));

                bool isRdLGranted = SecuritySystem.IsGranted(new PermissionRequest(ObjectSpace,
                                    typeof(RegNotificheEmergenze), SecurityOperations.Write, null, "Descrizione"));
                if (isRdLGranted)
                {

                    //if (CreaRdLNotificaEmergenza.Active.ResultValue == true)
                    //    CreaRdLNotificaEmergenza.Active.SetItemValue("Active", false);

                    if (dv.Id.Contains("RdL_DetailView") && dv.ViewEditMode == ViewEditMode.Edit) //CanChangeCurrentObject
                    {
                        if (dv.CurrentObject != null)
                        {
                            RdL RdLSel = (RdL)dv.CurrentObject;

                            if (RdLSel.UltimoStatoSmistamento != null)
                                if (RdLSel.UltimoStatoSmistamento.Oid == 1)
                                    CreaRdLNotificaEmergenza.Active.SetItemValue("Active", true);

                        }
                    }
                }



                //---------------------------------------------------------------
                if (dv.Id.Contains("RdL_DetailView"))
                    if (dv.ViewEditMode == ViewEditMode.View) //CanChangeCurrentObject
                    {
                        if (dv.CurrentObject != null)
                        {

                            pupWinApparato.Active.SetItemValue("Active", false);
                            pupWinEdificio.Active.SetItemValue("Active", false);
                            pupWinApparatoMap.Active.SetItemValue("Active", false);
                            pupWinLocale.Active.SetItemValue("Active", false);

                            acDelApparato.Active.SetItemValue("Active", false);
                            acDelLocale.Active.SetItemValue("Active", false);
                            acDelEdificio.Active.SetItemValue("Active", false);
                            pupWinEditRichiedente.Active.SetItemValue("Active", false);
                            pupWinNewRichiedenteRdL.Active.SetItemValue("Active", false);
                            //pupWinNewRichiedenteRdL.Active.SetItemValue("Active", false);

                        }
                    }
                    else
                    {
                        pupWinApparato.Active.SetItemValue("Active", VisualizzaTasto);
                        pupWinEdificio.Active.SetItemValue("Active", VisualizzaTasto);
                        pupWinApparatoMap.Active.SetItemValue("Active", VisualizzaTasto);
                        pupWinLocale.Active.SetItemValue("Active", VisualizzaTastoLocale);
                        //pupWinNewRichiedenteRdL.Active.SetItemValue("Active", true);
                        acDelApparato.Active.SetItemValue("Active", VisualizzaTasto);
                        acDelLocale.Active.SetItemValue("Active", VisualizzaTastoLocale);
                        acDelEdificio.Active.SetItemValue("Active", VisualizzaTasto);

                        bool ModificaRichiedente = false;
                        if (dv.CurrentObject != null)
                        {
                            RdL rdl = dv.CurrentObject as RdL;
                            if (rdl.Immobile != null && rdl.Immobile.Contratti != null && rdl.Immobile.Contratti.AttivaModificaRichiedentein_RdLNew)
                                ModificaRichiedente = true;
                        }
                        pupWinNewRichiedenteRdL.Active.SetItemValue("Active", ModificaRichiedente);
                        pupWinEditRichiedente.Active.SetItemValue("Active", ModificaRichiedente);

                        //pupWinApparato.Active.SetItemValue("Active", VisualizzaTasto);

                    }
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
            //Debug.WriteLine(View.ToString() + DateTime.Now.ToString());
        }

        private RegistroRdL GetRegistroRdL;
        private IObjectSpace xpObjectSpace;
        private bool needClearSelection;
        private DetailView dvOld;

        private void CambioRisorsa_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                var lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>();

                var lstRDLEdifici = lstRDLSelezionati.GroupBy(a => a.Servizio.Immobile).Select(g => g.First()).ToList();

                if (lstRDLEdifici.Count > 1)
                {
                }

                GetRegistroRdL = RegistroRdL.CreateFrom(xpObjectSpace, lstRDLSelezionati);

                var RdlImpianto = lstRDLEdifici[0].Servizio.Oid;
                var db = new Classi.DB();
                //for (var i = 0; i < GetRegistroRdL.ListaRisorseTeamSelezionabili.Count; i++)
                //{
                //    // var OidRisorseTeam = GetRegistroRdL.ListaRisorseTeamSelezionabili[i].Oid;
                //    // var Distanze = db.GetDistanzeImpiantoRisorseTeam(OidRisorseTeam, RdlImpianto);
                //    //// GetRegistroRdL.ListaRisorseTeamSelezionabili[i].DistanzaImpianto = Distanze[0];
                //    // GetRegistroRdL.ListaRisorseTeamSelezionabili[i].AssegnazioneImpianto = Distanze[1];
                //    // GetRegistroRdL.ListaRisorseTeamSelezionabili[i].UltimoImpianto = Distanze[2];
                //    // var Temp = 0;
                //    // GetRegistroRdL.ListaRisorseTeamSelezionabili[i].NumeroAttivitaSospese = int.TryParse(Distanze[3], out Temp) ? Temp : 0;
                //    // GetRegistroRdL.ListaRisorseTeamSelezionabili[i].NumeroAttivitaAgenda = int.TryParse(Distanze[4], out Temp) ? Temp : 0;
                //}

                var view = Application.CreateDetailView(xpObjectSpace, RegistroRdLCreaDaRdL_DetailView, false, GetRegistroRdL);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void CambioRisorsa_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var db = new Classi.DB();
            var Messaggio = string.Empty;
            if (xpObjectSpace != null)
            {
                List<RisorseTeam> lstRisorseTeam = null;

                foreach (PropertyEditor editor in ((DetailView)e.PopupWindow.View).GetItems<PropertyEditor>())
                {
                    if (editor != null && editor.GetType() == typeof(ListPropertyEditor))
                    {
                        var popupListView = ((ListPropertyEditor)editor).ListView;
                        lstRisorseTeam = popupListView.SelectedObjects.Cast<RisorseTeam>().ToList();
                    }
                }

                if (lstRisorseTeam == null || lstRisorseTeam.Count == 0)
                {
                    throw new UserFriendlyException("Occorre selezionare almeno una risorsa team");
                }
                if (lstRisorseTeam.Count > 1)
                {
                    throw new UserFriendlyException("E' necessario selezionare una sola risorsa team");
                }
                var RisorseTeamSelezionata = lstRisorseTeam[0];

                var lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>();
                RisorseTeamSelezionata.LinkRdLFrom(GetRegistroRdL);

                var IDs = lstRDLSelezionati.Select(r => r.Oid).ToList();
                var lstRdLReloaded = xpObjectSpace.GetObjects<RdL>().Where(r => IDs.Contains(r.Oid));

                foreach (RdL r in lstRdLReloaded)
                {
                    Messaggio = db.RdlCambioRisorsaTeam(r.Oid, RisorseTeamSelezionata.Oid);
                }

                RefreshDati();
                db.Dispose();
            }


            foreach (RdL obj in View.SelectedObjects)
            {
                var IdRdl = obj.Oid;
                var IdRisorseteam = (int)(obj.RisorseTeam).Oid;
                Messaggio = db.RdlCambioRisorsaTeam(IdRdl, IdRisorseteam);
            }
            db.Dispose();
        }

        private void RefreshDati()
        {
            try
            {
                needClearSelection = true;

                if (View is DetailView)
                {
                    View.ObjectSpace.ReloadObject(View.CurrentObject);
                }
                else
                {
                    (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
                }
                View.ObjectSpace.Refresh();
                View.Refresh();
            }
            catch
            {
            }
        }

        private void ConferaRefresh()
        {
            ObjectSpace.Refresh();
            if (View is DetailView)
            {
                View.ObjectSpace.ReloadObject(View.CurrentObject);
            }
            else
            {
                (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
                View.Refresh();
                View.ObjectSpace.ReloadObject(View.CurrentObject);
            }
        }

        #region INSERISCI MULTI TEAM PER RDL IN EMERGENZA
        int OidRdL = 0;
        private void CreaRdLNotificaEmergenza_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    if (((DetailView)View).CurrentObject != null) //                        if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        //RdLListView list_RdLListView_Selezionati = (((ListView)View).Editor).GetSelectedObjects().
                        //                                           Cast<RdLListView>().ToList<RdLListView>().First();
                        OidRdL = ((XPObject)((DetailView)View).CurrentObject).Oid;
                        RdL RDLSelezionata = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);        //OidRdL = RDLSelezionata.Oid;
                        int OidEdificio = RDLSelezionata.Immobile.Oid;
                        // --- imposto la rdl sulla notifica emergenza  
                        if (RDLSelezionata != null)
                        {
                            NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                            objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                            CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                                        "DCRisorseTeamRdL_LookupListView");
                            SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Ordinamento", DevExpress.Xpo.DB.SortingDirection.Descending);
                            DCRisorseTeamRdL_Lookup.Sorting.Add(srtProperty);

                            ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);

                            var dc = Application.CreateController<DialogController>();
                            e.DialogController.SaveOnAccept = false;
                            e.View = lvk;
                            //objectSpace.ObjectsGetting -= DCRisorseTeamRdL_objectSpace_ObjectsGetting;
                        }
                    }
                }
            }

            #region   vecchio codice

            //if (xpObjectSpace != null)
            //{
            //    if (View is DetailView)
            //    {
            //        DetailView dv = (DetailView)View;
            //        RdL lstRDLSelezionati = (RdL)dv.CurrentObject;
            //        string listViewId = "TRisorseEmergenza_ListView";
            //        var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(TRisorseEmergenza), listViewId);
            //        string Filtro = string.Format("Immobile.Oid = {0}", lstRDLSelezionati.Immobile.Oid);
            //        ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
            //        var view = Application.CreateListView(listViewId, ListTRisorseEmergenza, false);
            //        var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
            //        e.DialogController.SaveOnAccept = false;
            //        e.View = view;
            //        //e.View = view;
            //        //e.IsSizeable = true;
            //    }

            //    if (View is ListView)
            //        if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
            //        {
            //            RdL lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().First();
            //            var NuovoRegEmergenze = xpObjectSpace.CreateObject<RegNotificheEmergenze>();
            //            // RegNotificheEmergenze reg = (RegNotificheEmergenze)((DetailView)View).CurrentObject;
            //            NuovoRegEmergenze.RdL = xpObjectSpace.GetObject<RdL>(lstRDLSelezionati);
            //            int OidEdificio = NuovoRegEmergenze.RdL.Immobile.Oid;
            //            // --- imposto la rdl sulla notifica emergenza  
            //            var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(TRisorseEmergenza), "TRisorseEmergenza_ListView");
            //            string Filtro = string.Format("Immobile.Oid = {0}", lstRDLSelezionati.Immobile.Oid);
            //            ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
            //            var view = Application.CreateListView("TRisorseEmergenza_ListView", ListTRisorseEmergenza, false);
            //            e.View = view;
            //            e.IsSizeable = true;
            //        }
            //}

            #endregion
        }

        private void CreaRdLNotificaEmergenza_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is DetailView) //       if (View is ListView)
            {
                bool SpediscieMail = false;
                string RisorseAssociate = string.Empty;
                //IObjectSpace xpObjectSpace = View.ObjectSpace;

                List<DCRisorseTeamRdL> lst_DCRisorseTeamRdL_Selezionate = ((List<DCRisorseTeamRdL>)((((DevExpress.ExpressApp.Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<DCRisorseTeamRdL>().ToList<DCRisorseTeamRdL>()));
                if (xpObjectSpace != null && lst_DCRisorseTeamRdL_Selezionate.Count > 0)
                {

                    if (((XPObject)((DetailView)View).CurrentObject) != null)// if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        //RdLListView vRdLListView = xpObjectSpace.GetObject<RdLListView>(
                        //                               (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListView>()
                        //                                                         .ToList<RdLListView>().First()
                        //                                );
                        OidRdL = ((XPObject)((DetailView)View).CurrentObject).Oid;
                        RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
                        if (rdl != null)
                        {
                            //RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(vRdLListView.Codice);
                            rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10);
                            rdl.DataAggiornamento = DateTime.Now;

                            rdl.RegistroRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10);
                            rdl.RegistroRdL.DataAggiornamento = DateTime.Now;
                            rdl.Save();

                            RegNotificheEmergenze RegNotificaEmergenza = xpObjectSpace.CreateObject<RegNotificheEmergenze>();
                            RegNotificaEmergenza.RdL = rdl;
                            RegNotificaEmergenza.RegistroRdL = rdl.RegistroRdL;
                            RegNotificaEmergenza.RegStatoNotifica = RegStatiNotificaEmergenza.daAssegnare;
                            RegNotificaEmergenza.DataCreazione = DateTime.Now;
                            RegNotificaEmergenza.DataAggiornamento = DateTime.Now;

                            foreach (DCRisorseTeamRdL item in lst_DCRisorseTeamRdL_Selezionate)
                            {
                                RisorseAssociate = string.Format("Avviso Risorse Associate in Emergenza: {0}; \r\n {1}({2})", RisorseAssociate, item.RisorsaCapo, item.OidRisorsaTeam);
                                RegNotificaEmergenza.NotificheEmergenzes.Add(new NotificheEmergenze(RegNotificaEmergenza.Session)
                                {
                                    DataCreazione = DateTime.Now,
                                    Team = xpObjectSpace.GetObjectByKey<RisorseTeam>(item.OidRisorsaTeam),
                                    CodiceNotifica = Guid.NewGuid(),
                                    StatoNotifica = StatiNotificaEmergenza.NonVisualizzato,
                                    DataAggiornamento = DateTime.Now
                                });
                            }

                            RegNotificaEmergenza.Save();
                            SpediscieMail = true;
                            //CreateRegistroSmistamentoDett_Emergenza(rdl, RisorseAssociate.ToString());
                            xpObjectSpace.CommitChanges();
                            SetMessaggioWebRisorse(RisorseAssociate, "Avviso Risorse Associate in Emergenza", InformationType.Success);
                            //  invia mail
                            #region  ---------------  Trasmetto messaggio se necessario    -----------------------------
                            string AlertMessaggio = string.Empty;
                            string Titolo = string.Empty;
                            InformationType InformationTypeMsg = InformationType.Info;
                            if (SpediscieMail)
                            {
                                try
                                {
                                    string Messaggio = string.Empty;
                                    using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                                    {
                                        im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName,
                                                            rdl.RegistroRdL.Oid, ref Messaggio);
                                    }
                                    if (!string.IsNullOrEmpty(Messaggio))
                                    {
                                        Titolo = Titolo + ",Trasmissione Avviso Eseguita!!";
                                        AlertMessaggio = AlertMessaggio + string.Format("Messaggio Trasmesso: {0}", Messaggio);
                                        SetMessaggioWeb(AlertMessaggio, Titolo, InformationTypeMsg);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    Titolo = "Trasmissione Avviso non Eseguita!!";
                                    AlertMessaggio = AlertMessaggio + string.Format("Descrizione Errore:", ex.Message);
                                    //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                                    InformationTypeMsg = InformationType.Error;
                                    SetMessaggioWeb(AlertMessaggio, Titolo, InformationTypeMsg);
                                }
                            }
                            #endregion
                            //View.ObjectSpace.ReloadObject(/*vRdLListView*/);
                            View.Refresh(true);
                        }

                    }
                }
            }



            #region VECCHIO CODE
            //if (xpObjectSpace != null)
            //{
            //    var lstRisorseSelezionate = ((List<TRisorseEmergenza>)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).SelectedObjects.Cast<TRisorseEmergenza>().ToList<TRisorseEmergenza>()));
            //    //var NuovoRegEmergenze = null;  
            //    RdL rdl = null;
            //    if (View is DetailView)
            //    {
            //        DetailView dv = (DetailView)View;
            //        rdl = xpObjectSpace.GetObject<RdL>((RdL)dv.CurrentObject);
            //    }
            //    if (View is ListView)
            //        if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
            //        {
            //            rdl = xpObjectSpace.GetObject<RdL>(
            //                (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().First()
            //                 );
            //        }

            //    if (rdl != null)
            //    {
            //        RegNotificheEmergenze NuovoRegEmergenze = xpObjectSpace.CreateObject<RegNotificheEmergenze>();
            //        NuovoRegEmergenze.RdL = rdl;
            //        NuovoRegEmergenze.RegistroRdL = rdl.RegistroRdL;
            //        NuovoRegEmergenze.RegStatoNotifica = RegStatiNotificaEmergenza.daAssegnare;
            //        NuovoRegEmergenze.DataCreazione = DateTime.Now;
            //        NuovoRegEmergenze.DataAggiornamento = DateTime.Now;
            //        foreach (TRisorseEmergenza r in lstRisorseSelezionate)
            //        {
            //            NuovoRegEmergenze.NotificheEmergenzes.Add(new NotificheEmergenze(NuovoRegEmergenze.Session)
            //            {
            //                DataCreazione = DateTime.Now,
            //                Team = xpObjectSpace.GetObject<RisorseTeam>(r.RisorsaTeam),
            //                CodiceNotifica = Guid.NewGuid(),
            //                StatoNotifica = StatiNotificaEmergenza.NonVisualizzato,
            //                DataAggiornamento = DateTime.Now
            //            });
            //            // DB db = new DB();  db.InserisciNotificaEmergenza(NuovoRegEmergenze.Oid, r.RisorsaTeam.Oid, r.RisorsaCapo.Oid);
            //        }
            //        NuovoRegEmergenze.Save();

            //        rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10);
            //        rdl.RegistroRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10);
            //        rdl.Save();
            //    }
            //    // ------------------------
            //    xpObjectSpace.CommitChanges();
            //    using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
            //    {
            //        db.AssegnaInEmergenzaRegRdL(rdl.RegistroRdL.Oid, "RdLE");
            //    }

            //    View.Refresh(true);
            //}
            #endregion
        }

        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            #region VECCHIO CODE
            //if (View is ListView)
            //{
            //    ListView lv = (ListView)View;
            //    BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();

            //    RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
            //    if (rdl.Immobile != null)
            //    {
            //        using (DB db = new DB())
            //        {
            //            int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
            //            int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
            //            objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRA);
            //        }
            //    }
            //    //e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r =>
            //    //    r.Ordinamento)
            //    //    .ThenBy(r => r.NumerorAttivitaTotaliTT)
            //    //    .ThenBy(r => r.Distanzakm)
            //    //    .ThenBy(r => r.NumerorAttivitaTotaliMP).ToList());
            //    // e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r => r.Ordinamento).ToList());
            //    e.Objects = objects;
            //}
            #endregion
            if (View is DetailView)
            {
                BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
                RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
                if (rdl.Immobile != null)
                {
                    int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
                    using (DB db = new DB())
                    {
                        int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                        objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRA);
                    }
                }
                e.Objects = objects;
            }
        }
        private void SetMessaggioWebRisorse(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 5000;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Left;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
            //options.OkDelegate = () =>
            //{
            //};
            Application.ShowViewStrategy.ShowMessage(options);
        }

        private void SetMessaggioWeb(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 5000;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Top;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
            //options.OkDelegate = () =>
            //{
            //};
            Application.ShowViewStrategy.ShowMessage(options);
        }


        #endregion
        private void pupWinEdificioTree_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //if (View is DetailView)
                //{
                //    DetailView dv = (DetailView)View;
                //    RdL lstRDLSelezionati = (RdL)dv.CurrentObject;
                //    string listViewId = "EdificioTree_ListView";
                //    CollectionSourceBase ListEdificiTree = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(EdificioTree), listViewId);
                //    ListEdificiTree.Criteria[DevExpress.ExpressApp.SystemModule.FilterController.FullTextSearchCriteriaName] = CollectionSourceBase.EmptyCollectionCriteria;

                //    var view = Application.CreateListView(listViewId, ListEdificiTree, false);
                //    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                //    e.DialogController.SaveOnAccept = false;
                //    e.View = view;
                //    //e.IsSizeable = true;
                //}

                //if (View is ListView)
                //if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                //{

                //    RdL lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().First();
                //    var PopupTree = xpObjectSpace.CreateObject<EdificioTree>();            

                //    string listViewId = "EdificioTree_ListView";
                //    var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(EdificioTree), listViewId);
                //     var view = Application.CreateListView(listViewId, ListTRisorseEmergenza, false);
                //    e.View = view;
                //    e.IsSizeable = true;
                //    //e.IsSizeable = true;
                //}
            }

        }

        private void pupWinEdificioTree_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    Type TipoObjCurr = (((e.PopupWindow)).View).SelectedObjects[0].GetType();
                    if (TipoObjCurr.FullName.Contains(".Vista.EdificioTree"))
                    {
                        RdL curRdL = (RdL)dv.CurrentObject;
                        // elimino dati preinseriti
                        curRdL.SetMemberValue("Apparato", null);
                        curRdL.SetMemberValue("Impianto", null);
                        curRdL.SetMemberValue("Immobile", null);
                        EdificioTree TreeObjCurr = (EdificioTree)(((e.PopupWindow)).View).SelectedObjects[0];
                        string Oggetto = TreeObjCurr.Tipo;
                        int OidOggetto = TreeObjCurr.OidOggetto;
                        switch (Oggetto)
                        {
                            case "Apparato":
                                Asset ApparatoCurr = xpObjectSpace.GetObjectByKey<Asset>(OidOggetto);
                                curRdL.SetMemberValue("Apparato", ApparatoCurr);
                                curRdL.SetMemberValue("Impianto", ApparatoCurr.Servizio);
                                curRdL.SetMemberValue("Immobile", ApparatoCurr.Servizio.Immobile);
                                break;
                            case "Impianto":
                                Servizio ImpCurr = xpObjectSpace.GetObjectByKey<Servizio>(OidOggetto);
                                curRdL.SetMemberValue("Impianto", ImpCurr);
                                curRdL.SetMemberValue("Immobile", ImpCurr.Immobile);
                                break;
                            case "Immobile":
                                Immobile EdCurr = xpObjectSpace.GetObjectByKey<Immobile>(OidOggetto);
                                curRdL.SetMemberValue("Immobile", EdCurr);
                                break;
                            default:// altro non pervenuto

                                break;
                        }
                    }
                }
        }

        private void AggiungiSollecitooNote(string Tipo, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {

                    DetailView dv = (DetailView)View;
                    RdL RdL = xpObjectSpace.GetObject<RdL>((RdL)dv.CurrentObject);
                    if (RdL.RdLNotes.Count == 0)
                    {
                        var RdLNote = xpObjectSpace.CreateObject<RdLNote>();
                        RdL.RdLNotes.Add(RdLNote);
                        //string listVRdLNoteiewId = "RdLSolleciti_DetailView_Gestione";
                        //var dview = Application.CreateDetailView(xpObjectSpace, listViewId, false, RdlSollecito);
                        //dview.ViewEditMode = ViewEditMode.Edit;
                        //e.ShowViewParameters.CreatedView = dview;
                        //e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        // e.ShowViewParameters.CreateAllControllers = true;
                        //var view = Application.CreateListView(listViewId, RegEmergenze, false);
                        //var dc = Application.CreateController<DialogController>();
                        //e.DialogController.SaveOnAccept = false;
                        //e.View = view;
                    }




                }
                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        //RdL lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().First();
                        //var NuovoRegEmergenze = xpObjectSpace.CreateObject<RegNotificheEmergenze>();
                        //// --- imposto la rdl sulla notifica emergenza
                        //NuovoRegEmergenze.RdL = xpObjectSpace.GetObject<RdL>(lstRDLSelezionati);

                        //// ---
                        //var dview = Application.CreateDetailView(xpObjectSpace, "RegNotificheEmergenze_DetailView_RdLDis", true, NuovoRegEmergenze);
                        ////dview.LayoutManager.
                        //dview.ViewEditMode = ViewEditMode.Edit;
                        //e.ShowViewParameters.CreatedView = dview;
                        //e.ShowViewParameters.TargetWindow = TargetWindow.Default;

                    }
            }

            Debug.WriteLine(" prova casa ");
        }

        private bool visibile = false;
        private void acDettagliRdL_Execute(object sender, DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                var Tipo = e.SelectedChoiceActionItem.Data.ToString();
                if (View is DetailView)
                {
                    //if (Tipo == "1") // iserisce una nota
                    //{
                    //    DetailView dv = (DetailView)View;
                    //    RdL RdL = (RdL)dv.CurrentObject;
                    //    visibile = !visibile;
                    //    RdL.RdLNotesVisibile = visibile;
                    //    RdL.Save();
                    //    View.ObjectSpace.CommitChanges();

                    //}
                    //else
                    //{

                    //}
                    View.ObjectSpace.Refresh();
                }
            }
        }

        private void popupApparatoGroup_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                string listViewId = "Apparato_ListView_RicercaTree";
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL lstRDLSelezionati = (RdL)dv.CurrentObject;
                    var ListEdificiTree = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(Asset), listViewId);
                    //string Filtro = string.Format("Immobile = {0}", lstRDLSelezionati.Immobile.Oid);// filtrare per immobile che è della RdL
                    //ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
                    var view = Application.CreateListView(listViewId, ListEdificiTree, false);
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                    //e.View = view;
                    //e.IsSizeable = true;
                }

                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {

                        RdL lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().First();
                        var PopupTree = xpObjectSpace.CreateObject<Asset>();
                        // RegNotificheEmergenze reg = (RegNotificheEmergenze)((DetailView)View).CurrentObject;
                        //PopupTree = xpObjectSpace.GetObject<EdificioTree>(lstRDLSelezionati);
                        // --- imposto la rdl sulla notifica emergenza                 
                        var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(Asset), listViewId);
                        //string Filtro = string.Format("", 1);// filtrare per immobile che è della RdL
                        //ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
                        var view = Application.CreateListView(listViewId, ListTRisorseEmergenza, false);
                        e.View = view;
                        e.IsSizeable = true;
                        //e.IsSizeable = true;
                    }
            }
        }

        private void popupApparatoGroup_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    Asset AppObjCurr = (Asset)(((e.PopupWindow)).View).SelectedObjects[0];
                    if (AppObjCurr != null)
                    {
                        Asset Apparatopup = (Asset)(((e.PopupWindow)).View).SelectedObjects[0];
                        Asset ApparatoCurr = xpObjectSpace.GetObject<Asset>(Apparatopup);

                        RdL curRdL = (RdL)dv.CurrentObject;
                        curRdL.SetMemberValue("Apparato", ApparatoCurr);
                        curRdL.SetMemberValue("Impianto", ApparatoCurr.Servizio);
                        curRdL.SetMemberValue("Immobile", ApparatoCurr.Servizio.Immobile);
                    }


                }
        }

        private void pupWinRichiedenteTree_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;

            if (xpObjectSpace != null)
                if ((((e.PopupWindow)).View).SelectedObjects.Count > 0)
                    if (View is DetailView)
                    {
                        DetailView dv = (DetailView)View;
                        Type TipoObjCurr = (((e.PopupWindow)).View).SelectedObjects[0].GetType();
                        string nomepopup = ((e.PopupWindow)).View.Id;//	"RichiedenteEdificioImpAppTree_LookupListView"	string
                        if (nomepopup.Contains("RichiedenteEdificioImpAppTree_LookupListView"))
                        {
                            RdL curRdL = (RdL)dv.CurrentObject;
                            //(((e.PopupWindow)).View).SelectedObjects[0]	{CAMS.Module.DBTask.Vista.RichiedenteEdificioImpAppTree(Apparato3158)}	
                            //object {CAMS.Module.DBTask.Vista.RichiedenteEdificioImpAppTree}
                            RichiedenteEdificioImpAppTree TreeObjCurr = (RichiedenteEdificioImpAppTree)(((e.PopupWindow)).View).SelectedObjects[0];
                            string Oggetto = TreeObjCurr.Tipo;
                            int OidOggetto = TreeObjCurr.OidOggetto;
                            switch (Oggetto)
                            {
                                case "Richiedente":
                                    //  sOidOggetto = TreeObjCurr.Oid.Replace(Oggetto,"");
                                    //int.TryParse(sOidOggetto,out OidOggetto);
                                    Richiedente RichiedenteCurr = xpObjectSpace.GetObjectByKey<Richiedente>(OidOggetto);
                                    curRdL.SetMemberValue("Richiedente", RichiedenteCurr);

                                    break;

                                case "Apparato":
                                    //  sOidOggetto = TreeObjCurr.Oid.Replace(Oggetto,"");
                                    //int.TryParse(sOidOggetto,out OidOggetto);
                                    Asset ApparatoCurr = xpObjectSpace.GetObjectByKey<Asset>(OidOggetto);
                                    curRdL.SetMemberValue("Apparato", ApparatoCurr);
                                    curRdL.SetMemberValue("Impianto", ApparatoCurr.Servizio);
                                    curRdL.SetMemberValue("Immobile", ApparatoCurr.Servizio.Immobile);
                                    curRdL.SetMemberValue("Richiedente", GetRichiedentebyTree(TreeObjCurr));

                                    break;
                                case "Impianto":
                                    Servizio ImpCurr = xpObjectSpace.GetObjectByKey<Servizio>(OidOggetto);
                                    curRdL.SetMemberValue("Impianto", ImpCurr);
                                    curRdL.SetMemberValue("Immobile", ImpCurr.Immobile);
                                    curRdL.SetMemberValue("Richiedente", GetRichiedentebyTree(TreeObjCurr));
                                    break;
                                case "Immobile":
                                    Immobile EdCurr = xpObjectSpace.GetObjectByKey<Immobile>(OidOggetto);
                                    curRdL.SetMemberValue("Immobile", EdCurr);
                                    curRdL.SetMemberValue("Richiedente", GetRichiedentebyTree(TreeObjCurr));
                                    break;
                                default:// altro non pervenuto

                                    break;
                            }
                        }
                    }
        }
        private Richiedente GetRichiedentebyTree(RichiedenteEdificioImpAppTree TreeObjCurr)
        {
            //   select * from V_RICH_ED_IM_APP where oid = 'Apparato564';//select * from V_RICH_ED_IM_APP where oid = 'Impianto29';--padre
            //select * from V_RICH_ED_IM_APP where oid = 'Edificio6';//select * from V_RICH_ED_IM_APP where oid = 'Richiedente6';

            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            Richiedente GetRichiedente = null;
            if (xpObjectSpace != null)
            {
                RichiedenteEdificioImpAppTree TipoTreePadre = TreeObjCurr.TreePadre;
                while (true)
                {
                    string TipoPadre = TipoTreePadre.Tipo;
                    if (TipoPadre.StartsWith("Richiedente"))
                    {
                        int OIDRichiedente = TipoTreePadre.OidOggetto;
                        GetRichiedente = Sess.GetObjectByKey<Richiedente>(OIDRichiedente);
                        break;
                    }
                    try
                    {
                        TipoTreePadre = TipoTreePadre.TreePadre;
                    }
                    catch (Exception e)
                    {
                        break;
                    }

                }
            }
            return GetRichiedente;
        }

        private void pupWinRichiedenteTree_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                string listViewId = "RichiedenteEdificioImpAppTree_LookupListView";
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL lstRDLSelezionati = (RdL)dv.CurrentObject;
                    var ListEdificiTree = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(RichiedenteEdificioImpAppTree), listViewId);
                    //string Filtro = string.Format("Immobile = {0}", lstRDLSelezionati.Immobile.Oid);// filtrare per immobile che è della RdL
                    //ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
                    var view = Application.CreateListView(listViewId, ListEdificiTree, false);
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;

                    e.View = view;
                    //e.View = view;
                    //e.IsSizeable = true;
                }

                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {

                        RdL lstRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().First();
                        var PopupTree = xpObjectSpace.CreateObject<Asset>();
                        // RegNotificheEmergenze reg = (RegNotificheEmergenze)((DetailView)View).CurrentObject;
                        //PopupTree = xpObjectSpace.GetObject<EdificioTree>(lstRDLSelezionati);
                        // --- imposto la rdl sulla notifica emergenza                 
                        var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(Asset), listViewId);
                        //string Filtro = string.Format("", 1);// filtrare per immobile che è della RdL
                        //ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
                        var view = Application.CreateListView(listViewId, ListTRisorseEmergenza, false);
                        e.View = view;
                        e.IsSizeable = true;
                        //e.IsSizeable = true;
                    }
            }


        }

        private void prPuPEdificiTree_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            //************************************************************************
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            if (string.IsNullOrEmpty(e.ParameterCurrentValue.ToString()) || string.IsNullOrWhiteSpace(e.ParameterCurrentValue.ToString()))
            {
                throw new Exception(string.Format("inserire un parametro di ricerca di almeno tre caratteri"));
            }
            else if (e.ParameterCurrentValue.ToString().Length < 3)
            {
                throw new Exception(string.Format("inserire un parametro di ricerca di almeno tre caratteri"));
            }

            if (xpObjectSpace != null)
            {
                string listViewId = "EdificioTree_ListView";
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL lstRDLSelezionati = (RdL)dv.CurrentObject;
                    CollectionSource ListEdificiTree = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(EdificioTree), listViewId);

                    string paramValue = e.ParameterCurrentValue as string;
                    ListEdificiTree.Criteria["m"] = CriteriaOperator.Parse(string.Format("Contains([Name], '{0}')", paramValue.ToString()));

                    var view = Application.CreateListView(listViewId, ListEdificiTree, false);
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.ShowViewParameters.CreatedView = view;
                    e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;

                }
            }
            //************************************************************************
            //if (xpObjectSpace != null)
            //    if (View is DetailView)
            //    {
            //        DetailView dv = (DetailView)View;
            //        Type TipoObjCurr = (((e.PopupWindow)).View).SelectedObjects[0].GetType();
            //        if (TipoObjCurr.FullName.Contains(".Vista.EdificioTree"))
            //        {
            //            RdL curRdL = (RdL)dv.CurrentObject;
            //            EdificioTree TreeObjCurr = (EdificioTree)(((e.PopupWindow)).View).SelectedObjects[0];
            //            string Oggetto = TreeObjCurr.Tipo;
            //            int OidOggetto = TreeObjCurr.OidOggetto;
            //            switch (Oggetto)
            //            {
            //                case "Apparato":
            //                    //  sOidOggetto = TreeObjCurr.Oid.Replace(Oggetto,"");
            //                    //int.TryParse(sOidOggetto,out OidOggetto);
            //                    Apparato ApparatoCurr = xpObjectSpace.GetObjectByKey<Apparato>(OidOggetto);
            //                    curRdL.SetMemberValue("Apparato", ApparatoCurr);
            //                    curRdL.SetMemberValue("Impianto", ApparatoCurr.Impianto);
            //                    curRdL.SetMemberValue("Immobile", ApparatoCurr.Impianto.Immobile);
            //                    break;
            //                case "Impianto":
            //                    Impianto ImpCurr = xpObjectSpace.GetObjectByKey<Impianto>(OidOggetto);
            //                    curRdL.SetMemberValue("Impianto", ImpCurr);
            //                    curRdL.SetMemberValue("Immobile", ImpCurr.Immobile);
            //                    break;
            //                case "Immobile":
            //                    Immobile EdCurr = xpObjectSpace.GetObjectByKey<Immobile>(OidOggetto);
            //                    curRdL.SetMemberValue("Immobile", EdCurr);
            //                    break;
            //                default:// altro non pervenuto

            //                    break;
            //            }
            //        }
            //    }
        }


        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //        IObjectSpace objectSpace =
            //DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));
            //        DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData = objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
            //            CriteriaOperator.Parse("[DisplayName] = 'Report Attività Manutenzione'")); //'Contacts Report'

            xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                // hendol del report
                // filtro da imposare nel report   
                string CommessaSG = string.Empty;
                string CriterioAdd = string.Empty;
                int CommessaOID = 0;
                int CategoriaOID = 0;
                Type ObjectType = typeof(CAMS.Module.DBTask.RdL); ;

                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = xpObjectSpace.GetObject<RdL>((RdL)dv.CurrentObject);
                    try
                    {
                        if (rdl.Asset.Servizio.Immobile.Contratti != null)
                        {
                            CommessaSG = rdl.Immobile.Contratti.WBS.ToString();
                            CommessaOID = rdl.Asset.Servizio.Immobile.Contratti.Oid;
                            CategoriaOID = rdl.Categoria.Oid;
                        }
                    }
                    catch
                    { }
                    //CriterioAdd = string.Format("[Codice] = {0}", rdl.RegistroRdL.Oid);


                    //var OidReport =   Sess.Query<RdL>().Where(w => w.Richiedente != null).Select(s=> s.
                    ///  @@@@@@@@@@@@@@@@@@@   ma modificare ovunque @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                    CriterioAdd = string.Format("[codiceRdL] = {0}", rdl.Oid);
                    //CriterioAdd = string.Format("[CodRegRdL] = {0}", rdl.RegistroRdL.Oid);



                    criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));

                }
                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        int ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;
                        if (ObjCount < 1000)
                        {
                            var ArObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>()
                                           .ToList<RdL>().Select(s => s.RegistroRdL.Oid).Distinct().ToArray<int>();
                            string sOids = String.Join(",", ArObjSel);
                            //CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", ArObjSel));
                            CriterioAdd = string.Format("[CodRegRdL] In ({0})", String.Join(",", ArObjSel));
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                        }
                        else
                        {
                            var ArObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<RdL>().ToList<RdL>().Select(s => s.RegistroRdL.Oid).Distinct().ToList<int>();
                            int numita = ObjCount / 1000;
                            for (var i = 1; i < numita + 1; i++)
                            {
                                int maxind = ArObjSel.Count;
                                int limitemin = (i * 1000) - 1000;
                                int limitemax = i * 1000;
                                if (limitemax > maxind)
                                    limitemax = maxind;
                                var output = ArObjSel.GetRange(limitemin, limitemax).ToArray();
                                string sOids = String.Join(",", output);
                                //CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", output));
                                CriterioAdd = string.Format("[CodRegRdL] In ({0})", String.Join(",", output));
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                            }
                        }
                    }

                string DisplayReportPersonale = "";
                try
                {

                    DisplayReportPersonale = Sess.Query<SetReportCommessa>()
                        .Where(w => w.Categoria.Oid == CategoriaOID && w.Commesse.Oid == CommessaOID && w.ObjectType == ObjectType)
                           .Select(s => s.DisplayReport)
                           .FirstOrDefault();
                }
                catch
                {
                    DisplayReportPersonale = "";
                }


                string CriterioReport = string.Empty;
                GroupOperator criteriaReport = new GroupOperator(GroupOperatorType.And);
                criteriaReport.Operands.Clear();

                string handle = "";

                if (!string.IsNullOrEmpty(DisplayReportPersonale))
                {
                    IObjectSpace objectSpace =
                                DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.
                                CreateObjectSpace(typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));


                    CriterioReport = string.Format("[DisplayName] = '{0}'", DisplayReportPersonale);
                    criteriaReport.Operands.Add(CriteriaOperator.Parse(CriterioReport));


                    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                                objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(criteriaReport);
                    //   CriteriaOperator.Parse("[DisplayName] = 'Interventi di Manutenzione'"));  //   CriteriaOperator.Parse("[DisplayName] = 'Interventi di Manutenzione'"));  
                    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                }
                else
                {
                    IObjectSpace objectSpace =
DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(
typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));

                    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                        objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
                        CriteriaOperator.Parse("[DisplayName] = 'Report Interventi di Manutenzione'")); //' Report Interventi di Manutenzione --> ex <<sub Report Interventi Manutenzione>>

                    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                }


                Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle, criteriaOP2);
            }




        }


        #region RISORSE TEAM SELEZIONE DA CONTROLLO IN DETAGLIO RDL GESTIONE



        #endregion
        #region ricerca immobile
        private void pupWinEdificio_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View is DetailView && e != null)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                if (e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    Immobile newEdificio = (Immobile)e.PopupWindowViewSelectedObjects[0];
                    if (newEdificio != null && newEdificio != rdl.Immobile)
                    {
                        rdl.SetMemberValue("Immobile", newEdificio);
                    }

                }
            }
        }

        private void pupWinEdificio_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                string listViewId = "Edificio_LookupListView_GestioneRDL";
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    //var ParCriteria = string.Empty;

                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    CollectionSource ListRisorseTeamLookUp = (CollectionSource)
                        Application.CreateCollectionSource(xpObjectSpace, typeof(Immobile), listViewId, true, CollectionSourceMode.Normal);
                    GroupOperator criteriaAnd = new GroupOperator(GroupOperatorType.And);
                    //criteriaAnd.Operands.Add(CriteriaOperator.Parse(" [Attivo] = 'Si' And [Attivo da Gerarchia] = 'Si'"));
                    criteriaAnd.Operands.Add(CriteriaOperator.Parse("Abilitato == 1 And AbilitazioneEreditata == 1"));
                    ListRisorseTeamLookUp.Criteria["soloAbilitati"] = criteriaAnd;
                    //SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Descrizione", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //ListRisorseTeamLookUp.Sorting.Add(srtProperty);
                    if(ListRisorseTeamLookUp.GetCount()==1)
                    {
                        //Immobile _Edificio = Sess.Query<Immobile>().Where(w => w.Descrizione.Contains("Tivoli")).Select(s => s).First();
                        //rdl.Immobile = _Edificio;

                        //Application.Exit();
                     
                    }
                    ListView view = Application.CreateListView(listViewId, ListRisorseTeamLookUp, false);

                    ((ListView)view).Model.FilterEnabled = false;
                    GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                    criteriaOP2.Operands.Clear();
                    ((ListView)view).Model.Filter = "";

                    string CriteriaOggetto = string.Empty;
                    string CriterioAdd = string.Empty;
                    List<string> searchCriteria = new List<string>();

                    if (!string.IsNullOrEmpty(rdl.RicercaImmobile))
                    { //  string add = string.Format("Contains(Upper({0}),'{1}')", searchCriteria[i], paramValue);
                      //BinaryOperator binop = new BinaryOperator("[Descrizione]", string.Format("{0}%", rdl.RicercaEdificio.ToUpper()), BinaryOperatorType.Like);
                      //CriteriaOperator op = new FunctionOperator(FunctionOperatorType.Contains,
                      //   new FunctionOperator(FunctionOperatorType.Upper, new OperandProperty("[Descrizione]"))
                      //   , new OperandValue(rdl.RicercaEdificio.ToUpper()));

                        string Filtro = string.Empty;
                        if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                        {
                            Filtro = string.Format("Contains([Descrizione],'{0}') Or Contains([Indirizzo.FullName],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Indirizzo.Title],'{0}') Or Contains([Indirizzo.Strada],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Indirizzo.Comuni.DenomIta],'{0}') Or Contains([Indirizzo.Provincia.Descrizione],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Indirizzo.Cap],'{0}') Or Contains([Commesse.CentroCosto],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Commesse.WBS],'{0}') Or Contains([Commesse.Cliente.Descrizione],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Commesse.Descrizione],'{0}') Or Contains([Commesse.ClienteReferenteContratto.Denominazione],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Commesse.CodDescrizione],'{0}') Or Contains([Commesse.CodContratto],'{0}')", rdl.RicercaImmobile.ToString());

                        }
                        else
                        {
                            Filtro = string.Format("Contains([Descrizione],'{0}') Or Contains([Indirizzo.FullName],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Indirizzo.Title],'{0}') Or Contains([Indirizzo.Strada],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Indirizzo.Comuni.DenomIta],'{0}') Or Contains([Indirizzo.Provincia.Descrizione],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Indirizzo.Cap],'{0}') Or Contains([Commesse.CentroCosto],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Commesse.WBS],'{0}') Or Contains([Commesse.Cliente.Descrizione],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Commesse.Descrizione],'{0}') Or Contains([Commesse.ClienteReferenteContratto.Denominazione],'{0}')", rdl.RicercaImmobile.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Commesse.CodDescrizione],'{0}') Or Contains([Commesse.CodContratto],'{0}')", rdl.RicercaImmobile.ToString());
                        }
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        ((ListView)view).Model.FilterEnabled = true;
                        ((ListView)view).Model.Filter = criteriaOP2.ToString();
                        //((ListView)view).Model.Filter = op.ToString();
                        view.Caption = "Ricerca per Edifici che contengono " + rdl.RicercaImmobile;
                    }
                    else
                    {
                        ((ListView)view).Model.Filter = "";
                        view.Caption = "Ricerca x tutti gli Edifici";
                    }


                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                }


            }
        }
        #endregion

        private void acDelRisorsaTeam_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                rdl.SetMemberValue("RisorseTeam", null);
            }
        }

        private void acDelEdificio_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                rdl.SetMemberValue("Immobile", null);
                rdl.SetMemberValue("Impianto", null);
                rdl.SetMemberValue("Apparato", null);

                rdl.SetMemberValue("Piano", null);
                rdl.SetMemberValue("Locale", null);
                rdl.SetMemberValue("Richiedente", null);
            }
        }


        private void pupWinEditRichiedente_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    string sEmail = ((Richiedente)(((e.PopupWindow)).View).SelectedObjects[0]).Mail;
                    string sSMS = ((Richiedente)(((e.PopupWindow)).View).SelectedObjects[0]).PhoneMobString;
                    rdl.SetMemberValue("Email", sEmail);
                    rdl.SetMemberValue("PhoneString", sSMS);
                }
            }
        }

        private void pupWinEditRichiedente_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is DetailView)
            {
                RdL vRdL = (RdL)View.CurrentObject;
                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                Richiedente r = vRdL.Richiedente;
                if (xpObjectSpace != null)
                {
                    try
                    {
                        var view = Application.CreateDetailView(xpObjectSpace, "Richiedente_DetailView_EditbyRdL", false, r);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.DialogController.SaveOnAccept = false;
                        e.View = view;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                    }
                }
            }

        }

        private void acDelLocale_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                rdl.SetMemberValue("Locale", null);
            }
        }

        private void pupWinLocale_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                if (e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    Locali newLocale = (Locali)e.PopupWindowViewSelectedObjects[0];
                    if (newLocale != null)
                    {
                        rdl.SetMemberValue("Locale", newLocale);

                        //if (newLocale.Piano != null)
                        //    rdl.SetMemberValue("Piano", newLocale.Piano);
                    }
                }

            }
        }

        private void pupWinLocale_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                string listViewId = "Locali_LookupListView_GestioneRDL";
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;

                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;

                    CollectionSource ListRisorseTeamLookUp = (CollectionSource)
                        Application.CreateCollectionSource(xpObjectSpace, typeof(Locali), listViewId, true, CollectionSourceMode.Normal);


                    string viewCaption = "Ricerca Locali";
                    if (rdl.Piano != null && rdl.Immobile != null)
                    {
                        ListRisorseTeamLookUp.Criteria["lookupLocaliRdL"] = CriteriaOperator.Parse("[Piano.Oid] = ?", rdl.Piano.Oid);
                        viewCaption = string.Format("Locali per Immobile: {0} e Piano {1}", rdl.Immobile.Descrizione, rdl.Piano.Descrizione);
                    }
                    else
                    {
                        if (rdl.Immobile != null)
                        {
                            ListRisorseTeamLookUp.Criteria["lookupLocaliRdL"] = CriteriaOperator.Parse("[Piano.Immobile.Oid] = ?", rdl.Immobile.Oid);
                            viewCaption = string.Format("Locali per Immobile: {0}", rdl.Immobile.Descrizione);
                        }
                    }

                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Descrizione", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    ListRisorseTeamLookUp.Sorting.Add(srtProperty);

                    ListView view = Application.CreateListView(listViewId, ListRisorseTeamLookUp, false);
                    view.Caption = viewCaption;

                    GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                    string CriteriaOggetto = string.Empty;
                    string CriterioAdd = string.Empty;
                    List<string> searchCriteria = new List<string>();
                    //Locali l = xpObjectSpace.GetObjectByKey<Locali>(0);
                    //l.Piano.Immobile.Descrizione;

                    if (!string.IsNullOrEmpty(rdl.RicercaLocale))
                    { //  string add = string.Format("Contains(Upper({0}),'{1}')", searchCriteria[i], paramValue);
                        string Filtro = string.Format("Contains([Descrizione],'{0}') Or Contains([CodDescrizione],'{0}')", rdl.RicercaLocale.ToString());
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([AreaMacro],'{0}') Or Contains([AreaOmogenea],'{0}')", rdl.RicercaLocale.ToString());
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([LocaliCategoria.Descrizione],'{0}') Or Contains([LocaliUso.Descrizione],'{0}')", rdl.RicercaLocale.ToString());
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        if (rdl.Piano == null)
                        {
                            Filtro = string.Format("Contains([Piano.Descrizione],'{0}') Or Contains([Piano.CategoriadiPiano.Descrizione],'{0}')", rdl.RicercaLocale.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            Filtro = string.Format("Contains([Piano.CategoriadiPiano.CodDescrizione],'{0}')", rdl.RicercaLocale.ToString());
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        }
                        //Filtro = string.Format("Contains(Upper([Piano.Immobile.CodDescrizione]),'{0}') Or Contains(Upper([Piano.Immobile.Descrizione]),'{0}')", rdl.RicercaLocale.ToUpper());
                        //criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        ((ListView)view).Model.Filter = criteriaOP2.ToString();
                    }

                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                }


            }
        }

        private void acDelApparato_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RdL rdl = (RdL)dv.CurrentObject;
                rdl.SetMemberValue("Apparato", null);
            }

        }


        private void pupWinApparato_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {

                if (View is DetailView)
                {
                    GroupOperator criteriaOP1 = new GroupOperator(GroupOperatorType.And);
                    criteriaOP1.Operands.Clear();
                    string CriteriaOggetto = string.Empty;
                    string CriterioAdd = string.Empty;
                    List<string> searchCriteria = new List<string>();
                    //DetailView dv = (DetailView)View;
                    //RdL rdl = (RdL)dv.CurrentObject;
                    RdL rdl = ((DetailView)View).CurrentObject as RdL;
                    //Session Sess = ((XPObjectSpace)ObjectSpace).Session;

                    string listViewId = "Apparato_LookupListView_GestioneRDL";
                    if (rdl.Servizio != null)
                        if (rdl.Servizio.ServizioGeoreferenziato == FlgAbilitato.Si)
                        {
                            listViewId = "Apparato_LookupListView_GestioneRDL_Geo";
                        }

                    string Filtro = "Ricerca Apparati";
                    string viewCaption = string.Empty;
                    criteriaOP1.Operands.Add(CriteriaOperator.Parse("[Abilitato] = ?", FlgAbilitato.Si));
                    criteriaOP1.Operands.Add(CriteriaOperator.Parse("[AbilitazioneEreditata] = ?", FlgAbilitato.Si));

                    if (rdl.Immobile != null)      ///   filtro immobile
                    {//"Abilitato = 'No' Or AbilitazioneEreditata == 'No'       
                        Filtro = string.Empty;
                        Filtro = string.Format("Impianto.Immobile.Oid = {0}", rdl.Immobile.Oid);
                        criteriaOP1.Operands.Add(CriteriaOperator.Parse(Filtro));
                        viewCaption = string.Format("Ricerca Apparati x Immobile {0}", rdl.Immobile.Descrizione);
                    }
                    //if (rdl.Piano != null)
                    //{
                    //    Filtro = string.Empty;
                    //    Filtro = string.Format("Locale.Piano.Oid = {0}", rdl.Piano.Oid);
                    //    criteriaOP1.Operands.Add(CriteriaOperator.Parse(Filtro));
                    //}
                    //if (rdl.Locale != null)
                    //{
                    //    Filtro = string.Empty;
                    //    Filtro = string.Format("Locale.Oid = {0}", rdl.Locale.Oid);
                    //    criteriaOP1.Operands.Add(CriteriaOperator.Parse(Filtro));
                    //    Filtro = string.Format("Locale.Piano.Oid = {0}", rdl.Locale.Piano.Oid);                       
                    //    criteriaOP1.Operands.Add(CriteriaOperator.Parse(Filtro));
                    //}
                    if (rdl.Servizio != null)
                    {
                        Filtro = string.Empty;
                        Filtro = string.Format("Impianto.Oid = {0}", rdl.Servizio.Oid);
                        criteriaOP1.Operands.Add(CriteriaOperator.Parse(Filtro));
                        viewCaption = string.Format("Ricerca Apparati x Immobile: {0} e Impianto: {1}", rdl.Immobile.Descrizione, rdl.Servizio.Descrizione);
                    }

                    CollectionSource ListApparatiLookUp = (CollectionSource)
                        Application.CreateCollectionSource(xpObjectSpace, typeof(Asset), listViewId, true, CollectionSourceMode.Normal);
                    ListApparatiLookUp.Criteria["ApparatoLookUp"] = criteriaOP1;
                    //SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Descrizione", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //ListApparatiLookUp.Sorting.Add(srtProperty);
                    ListView view = Application.CreateListView(listViewId, ListApparatiLookUp, false);
                    view.Caption = viewCaption;

                    GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                    criteriaOP2.Operands.Clear();
                    ((ListView)view).Model.Filter = "";
                    ((ListView)view).Model.FilterEnabled = false;
                    if (!string.IsNullOrEmpty(rdl.RicercaApparato))
                    {
                        ((ListView)view).Model.FilterEnabled = true;
                        Filtro = string.Empty;
                        string stRicercaApparato = rdl.GetMemberValue("RicercaApparato").ToString();
                        Filtro = string.Format("Contains([Descrizione],'{0}') Or Contains([CodDescrizione],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([Marca],'{0}') Or Contains([GeoLocalizzazione.Title],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([Modello],'{0}') Or Contains([CarattTecniche],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([Note],'{0}') Or Contains([Matricola],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([Tag],'{0}') Or Contains([FluidoPrimario],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        //Filtro = string.Format("Contains(Upper([Locale.Descrizione]),'{0}') Or Contains(Upper([Locale.CodDescrizione]),'{0}')", stRicercaApparato);
                        //criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([StdApparato.Descrizione],'{0}') Or Contains([StdApparato.CodDescrizione],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([ApparatoPadre.Descrizione],'{0}') Or Contains([ApparatoPadre.CodDescrizione],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([ApparatoPadre.Marca],'{0}') Or Contains([ApparatoPadre.GeoLocalizzazione.Title],'{0}')", stRicercaApparato);
                        criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                        if (rdl.Servizio != null)
                            if (rdl.Servizio.ServizioGeoreferenziato == FlgAbilitato.Si)
                            {
                                Filtro = string.Format("Contains([Strada.Strada],'{0}') Or Contains([Strada.Cap],'{0}')", stRicercaApparato);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));

                                Filtro = string.Format("Contains([ApparatoSostegno.Descrizione],'{0}') Or Contains([ApparatoSostegno.CodDescrizione],'{0}')", stRicercaApparato);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                                Filtro = string.Format("Contains([ApparatoSostegno.Strada.Strada],'{0}') Or Contains([ApparatoSostegno.Strada.Cap],'{0}')", stRicercaApparato);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                                Filtro = string.Format("Contains([ApparatoSostegno.Marca],'{0}') Or Contains([ApparatoSostegno.GeoLocalizzazione.Title],'{0}')", stRicercaApparato);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                                Filtro = string.Format("Contains([ApparatoPadre.Strada.Strada],'{0}') Or Contains([ApparatoPadre.Strada.Cap],'{0}')", stRicercaApparato);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(Filtro));
                            }

                        ((ListView)view).Model.Filter = criteriaOP2.ToString();
                    }
                    else
                    {
                        ((ListView)view).Model.Filter = "";
                    }

                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                }
            }
        }

        private void pupWinApparato_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    if (dv.CurrentObject != null && (((e.PopupWindow)).View).SelectedObjects.Count > 0)
                    {
                        RdL rdl = (RdL)dv.CurrentObject;
                        Asset ObjCurr = (Asset)(((e.PopupWindow)).View).SelectedObjects[0];

                        if (ObjCurr != rdl.Asset)
                        {
                            rdl.SetMemberValue("Apparato", ObjCurr);
                        }
                    }
                }
            }
        }


        private void pupWinApparatoMap_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {

                    GroupOperator criteriaEdificioImpianto = new GroupOperator(GroupOperatorType.And);
                    GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                    string DescrizioneImpianto = string.Empty;
                    string CaptionFiltroRicerca = string.Empty;
                    string Filtro = string.Empty;
                    //string CaptionView = string.Empty;
                    List<string> CaptionView = new List<string>();
                    criteriaEdificioImpianto.Operands.Clear();

                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;

                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;

                    string listViewId = "ApparatoMap_LookupListView";
                    if (rdl.Servizio != null)
                    {
                        DescrizioneImpianto = rdl.Servizio.Descrizione;
                        if (rdl.Immobile != null)
                        {
                            Filtro = string.Format("OidEdificio = {0}", ((Immobile)rdl.GetMemberValue("Immobile")).Oid);
                            criteriaEdificioImpianto.Operands.Add(CriteriaOperator.Parse(Filtro));
                            CaptionView.Add(rdl.Immobile.Descrizione);
                        }
                        if (rdl.Servizio != null)
                        {
                            Filtro = string.Format("OidImpianto= {0}", ((Servizio)rdl.GetMemberValue("Impianto")).Oid);
                            criteriaEdificioImpianto.Operands.Add(CriteriaOperator.Parse(Filtro));
                            CaptionView.Add(rdl.Servizio.Descrizione);
                        }
                    }

                    if (!string.IsNullOrEmpty(rdl.RicercaApparato))
                    {

                        CaptionView.Add(rdl.RicercaApparato);
                        string stRicercaApparato = rdl.RicercaApparato.ToString();
                        //Filtro = string.Format("Contains(Upper([Descrizione]),'{0}') Or Contains(Upper([Edificio_Descrizione]),'{0}')", stRicercaApparato);
                        CriteriaOperator crp = CriteriaOperator.Parse("Contains([Descrizione],?) Or Contains([Edificio_Descrizione],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);

                        crp = CriteriaOperator.Parse("Contains([Impianto_Descrizione],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);

                        crp = CriteriaOperator.Parse("Contains([StradaInProssimita],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);

                        crp = CriteriaOperator.Parse("Contains([Strada_Descrizione],?) Or Contains([AppPadreDescrizione],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);


                        crp = CriteriaOperator.Parse("Contains([Modello],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);

                        crp = CriteriaOperator.Parse("Contains([Zona],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);

                        crp = CriteriaOperator.Parse("Contains([Targhetta],?) Or Contains([Entitaapparato],?)", stRicercaApparato);
                        criteriaRicercaTestuale.Operands.Add(crp);
                    }
                    else
                    {
                        Filtro = string.Format("[OidApparatoSostegno] = 0");  // solo pali e quadri
                        CriteriaOperator cr = CriteriaOperator.Parse(Filtro);
                        criteriaRicercaTestuale.Operands.Add(CriteriaOperator.Parse(Filtro));
                    }
                    CollectionSource ListApparatiLookUp = (CollectionSource)
                           Application.CreateCollectionSource(xpObjectSpace, typeof(AssetoMap), listViewId, false, CollectionSourceMode.Normal);
                    ListApparatiLookUp.Criteria["criteriaEdificioImpianto"] = criteriaEdificioImpianto;
                    ListApparatiLookUp.Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;
                    //ListApparatiLookUp.DataAccessMode = CollectionSourceDataAccessMode.Client;
                    ListApparatiLookUp.DisplayableProperties = "Title";
                    //SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Descrizione", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    //ListApparatiLookUp.Sorting.Add(srtProperty);

                    var view = Application.CreateListView(listViewId, ListApparatiLookUp, false);



                    view.Caption = string.Format("{0} {1}", view.Caption, String.Join(" - ", CaptionView));
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                }
            }
        }

        private void pupWinApparatoMap_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    try
                    {
                        AssetoMap ObjCurr = (AssetoMap)(((e.PopupWindow)).View).SelectedObjects[0];
                        int OidApparato = ObjCurr.OidApparato;
                        Asset app = xpObjectSpace.GetObjectByKey<Asset>(OidApparato);
                        if (app != rdl.Asset)
                        {
                            rdl.SetMemberValue("Asset", app);
                            rdl.SetMemberValue("RicercaApparato", app.CodDescrizione);

                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void MigrazioneMPinTT_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpaceRRdl = Application.CreateObjectSpace();
            if (xpObjectSpaceRRdl != null)
            {
                RdL rdl = null;
                if (View is DetailView)
                {
                    rdl = xpObjectSpaceRRdl.GetObject<RdL>(((DetailView)View).CurrentObject as RdL);
                }
                if (View is ListView)
                {
                    rdl = xpObjectSpaceRRdl.GetObject<RdL>(((ListView)View).CurrentObject as RdL);
                }
                if (rdl != null)
                {


                    int numRdL = rdl.RegistroRdL.RdLes.Count;
                    if (numRdL > 0 && (rdl.Categoria.Oid == 1 || rdl.Categoria.Oid == 5))
                    {
                        RegistroRdL rrdl = xpObjectSpaceRRdl.CreateObject<RegistroRdL>();
                        rrdl.Asset = xpObjectSpaceRRdl.GetObject<Asset>(rdl.Asset);//
                        rrdl.Categoria = xpObjectSpaceRRdl.GetObject<Categoria>(rdl.Categoria);//
                        rrdl.DATA_CREAZIONE_RDL = rdl.DataRichiesta;

                        rrdl.DataPianificata = rdl.DataPianificata;//  data pianificata  data richieste da bonificare su db 15012018
                        rrdl.DataAssegnazioneOdl = rdl.DataAssegnazioneOdl;//  data pianificata  data richieste da bonificare  15012018

                        //rrdl.DataAssegnazioneOdl = rdl.DataPianificata;///   ************************************
                        rrdl.DataAzioniTampone = rdl.DataAzioniTampone;
                        rrdl.DataFermo = rdl.DataFermo;
                        rrdl.DataAzioniTampone = rdl.DataAzioniTampone;
                        rrdl.DataFermo = rdl.DataFermo;//
                        rrdl.DataInizioLavori = rdl.DataInizioLavori;//
                        rrdl.DataPrevistoArrivo = rdl.DataPrevistoArrivo;
                        //rrdl.DataRiavvio = rdl.DataRiavvio;//
                        //rrdl.DataSopralluogo = rdl.DataSopralluogo;//
                        //1	MANUTENZIONE PROGRAMMATA     //5	MANUTENZIONE PROGRAMMATA SPOT
                        //2	CONDUZIONE            //3	MANUTENZIONE A CONDIZIONE            //4	MANUTENZIONE GUASTO            
                        string Descrizione = string.Format("Reg.TT({0}) {1}", 0.ToString(), rdl.Descrizione);
                        if (Descrizione.Length > 3999)
                            Descrizione = Descrizione.Substring(1, 3996) + "...";
                        rrdl.Descrizione = Descrizione; // 

                        rrdl.Priorita = xpObjectSpaceRRdl.GetObject<Urgenza>(rdl.Urgenza);//
                        //rrdl.Problema = xpObjectSpaceRRdl.GetObject<ApparatoProblema>(rdl.Problema);//
                        //rrdl.ProblemaCausa = xpObjectSpaceRRdl.GetObject<ProblemaCausa>(rdl.ProblemaCausa);//
                        rrdl.Prob = xpObjectSpaceRRdl.GetObject<Problemi>(rdl.Prob);//
                        rrdl.Causa = xpObjectSpaceRRdl.GetObject<Cause>(rdl.Causa);//
                        if (rdl.RisorseTeam != null)
                            rrdl.RisorseTeam = xpObjectSpaceRRdl.GetObject<RisorseTeam>(rdl.RisorseTeam);
                        //rrdl.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObject<StatoOperativo>(rdl.UltimoStatoOperativo);
                        rrdl.UltimoStatoSmistamento = xpObjectSpaceRRdl.GetObject<StatoSmistamento>(rdl.UltimoStatoSmistamento);
                        rrdl.Utente = SecuritySystem.CurrentUserName;
                        rrdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                        //rrdl.AutorizzazioniRegistroRdLs
                        //rrdl.Save();
                        //xpObjectSpaceRRdl.CommitChanges();
                        #region autorizzazioni di visualizzazione
                        Contratti cm = rdl.Immobile.Contratti;
                        if (cm.MostraDataOraFermo)
                        {
                            rrdl.MostraDataOraFermo = true;
                            rrdl.MostraDataOraRiavvio = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraFermo = false;
                            rrdl.MostraDataOraRiavvio = false;
                        }
                        if (cm.MostraDataOraSopralluogo)
                        {
                            rrdl.MostraDataOraSopralluogo = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraSopralluogo = false;
                        }

                        if (cm.MostraDataOraAzioniTampone)
                        {
                            rrdl.MostraDataOraAzioniTampone = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraAzioniTampone = false;
                        }

                        if (cm.MostraDataOraInizioLavori)
                        {
                            rrdl.MostraDataOraInizioLavori = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraInizioLavori = false;
                        }

                        if (cm.MostraDataOraCompletamento)
                        {
                            rrdl.MostraDataOraCompletamento = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraCompletamento = false;
                        }

                        if (cm.MostraElencoCauseRimedi)
                        {
                            rrdl.MostraElencoCauseRimedi = true;
                        }
                        else
                        {
                            rrdl.MostraElencoCauseRimedi = false;
                        }

                        #endregion
                        rrdl.Save();
                        //xpObjectSpaceRRdl.CommitChanges();                     
                        //Aggiorna  RdL   ###########################################à   AGGIORNA RDL
                        rdl.DataAggiornamento = DateTime.Now;
                        rdl.UtenteUltimo = SecuritySystem.CurrentUserName; ;
                        // imposto la nuova richiesta  coma da prendere in carico
                        rdl.UltimoStatoSmistamento = xpObjectSpaceRRdl.GetObjectByKey<StatoSmistamento>(2); //2	Assegnata
                        rdl.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObjectByKey<StatoOperativo>(19); //Assegnata-Da prendere in carico
                        rdl.RegistroRdL = rrdl;
                        rdl.Save();
                        //  crea OdL  ######  CREA ODL   CREA ODL   ######  CREA ODL   CREA ODL   ######  CREA ODL   CREA ODL   ######  CREA ODL   CREA ODL   
                        OdL newOdL = xpObjectSpaceRRdl.CreateObject<OdL>();
                        newOdL.RegistroRdL = rrdl;
                        string odlDescrizione = rdl.Descrizione;
                        if (odlDescrizione.Length > 240)
                            odlDescrizione = odlDescrizione.Substring(1, 239) + "...";

                        newOdL.Descrizione = odlDescrizione;

                        newOdL.TipoOdL = xpObjectSpaceRRdl.GetObjectByKey<TipoOdL>(1);//xpObjSpaceOdL.GetObjectByKey<>(1);// misto
                        newOdL.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObjectByKey<StatoOperativo>(19); //Assegnata-Da prendere in carico
                        newOdL.DataEmissione = rdl.DataAssegnazioneOdl;
                        newOdL.Save();
                        //Aggiunta update di Rdl con nuovo ID ODL
                        rdl.OdL = newOdL;
                        rdl.Save();
                        xpObjectSpaceRRdl.CommitChanges();
                        //1	MANUTENZIONE PROGRAMMATA     //5	MANUTENZIONE PROGRAMMATA SPOT
                        //2	CONDUZIONE            //3	MANUTENZIONE A CONDIZIONE            //4	MANUTENZIONE GUASTO       
                        if (rrdl.Categoria.Oid == 1 || rrdl.Categoria.Oid == 5)
                        {
                            Descrizione = string.Format("Reg.MP({0}) {1}", rrdl.Oid, rdl.Descrizione);
                            if (Descrizione.Length > 3999)
                                Descrizione = Descrizione.Substring(1, 3996) + "...";
                            rrdl.Descrizione = Descrizione;
                        }
                        else
                        {
                            Descrizione = string.Format("Reg.TT({0}) {1}", rrdl.Oid, rdl.Descrizione);
                            if (Descrizione.Length > 3999)
                                Descrizione = Descrizione.Substring(1, 3996) + "...";
                            rrdl.Descrizione = Descrizione; //
                        }
                        rrdl.Save();
                        xpObjectSpaceRRdl.CommitChanges();

                        // vai alla nuova RdL separata
                        var view = Application.CreateDetailView(xpObjectSpaceRRdl, "RdL_DetailView_Gestione", true, rdl);
                        view.Caption = string.Format("Nuova RdL (separata)");
                        view.ViewEditMode = ViewEditMode.Edit;

                        e.ShowViewParameters.CreatedView = view;
                        e.ShowViewParameters.Context = TemplateContext.View;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }
                }
            }
        }

        private void saSepara_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            IObjectSpace xpObjectSpaceRRdl = Application.CreateObjectSpace();
            if (xpObjectSpaceRRdl != null)
            {
                RdL rdl = null;
                if (View is DetailView)
                {
                    rdl = xpObjectSpaceRRdl.GetObject<RdL>(((DetailView)View).CurrentObject as RdL);
                }
                if (View is ListView)
                {
                    rdl = xpObjectSpaceRRdl.GetObject<RdL>(((ListView)View).CurrentObject as RdL);
                }
                if (rdl != null)
                {
                    int numRdL = rdl.RegistroRdL.RdLes.Count;
                    if (numRdL > 0 && (rdl.Categoria.Oid == 1 || rdl.Categoria.Oid == 5))
                    {
                        RegistroRdL rrdl = xpObjectSpaceRRdl.CreateObject<RegistroRdL>();
                        rrdl.Asset = xpObjectSpaceRRdl.GetObject<Asset>(rdl.Asset);//
                        rrdl.Categoria = xpObjectSpaceRRdl.GetObject<Categoria>(rdl.Categoria);//
                        rrdl.DATA_CREAZIONE_RDL = rdl.DataRichiesta;

                        rrdl.DataPianificata = rdl.DataPianificata;//  data pianificata  data richieste da bonificare su db 15012018
                        rrdl.DataAssegnazioneOdl = rdl.DataAssegnazioneOdl;//  data pianificata  data richieste da bonificare  15012018

                        //rrdl.DataAssegnazioneOdl = rdl.DataPianificata;///   ************************************
                        rrdl.DataAzioniTampone = rdl.DataAzioniTampone;
                        rrdl.DataFermo = rdl.DataFermo;
                        rrdl.DataAzioniTampone = rdl.DataAzioniTampone;
                        rrdl.DataFermo = rdl.DataFermo;//
                        rrdl.DataInizioLavori = rdl.DataInizioLavori;//
                        rrdl.DataPrevistoArrivo = rdl.DataPrevistoArrivo;
                        //rrdl.DataRiavvio = rdl.DataRiavvio;//
                        //rrdl.DataSopralluogo = rdl.DataSopralluogo;//
                        string Descrizione = string.Format("Reg.TT({0}) {1}", 0.ToString(), rdl.Descrizione);
                        if (Descrizione.Length > 3999)
                            Descrizione = Descrizione.Substring(1, 3996) + "...";
                        rrdl.Descrizione = Descrizione; // 

                        rrdl.Priorita = xpObjectSpaceRRdl.GetObject<Urgenza>(rdl.Urgenza);//
                        //rrdl.Problema = xpObjectSpaceRRdl.GetObject<ApparatoProblema>(rdl.Problema);//
                        //rrdl.ProblemaCausa = xpObjectSpaceRRdl.GetObject<ProblemaCausa>(rdl.ProblemaCausa);//
                        rrdl.Prob = xpObjectSpaceRRdl.GetObject<Problemi>(rdl.Prob);//
                        rrdl.Causa = xpObjectSpaceRRdl.GetObject<Cause>(rdl.Causa);//
                        if (rdl.RisorseTeam != null)
                            rrdl.RisorseTeam = xpObjectSpaceRRdl.GetObject<RisorseTeam>(rdl.RisorseTeam);
                        //rrdl.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObject<StatoOperativo>(rdl.UltimoStatoOperativo);
                        rrdl.UltimoStatoSmistamento = xpObjectSpaceRRdl.GetObject<StatoSmistamento>(rdl.UltimoStatoSmistamento);
                        rrdl.Utente = SecuritySystem.CurrentUserName;
                        rrdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                        //rrdl.Save();
                        //xpObjectSpaceRRdl.CommitChanges();

                        #region autorizzazioni di visualizzazione
                        Contratti cm = rdl.Immobile.Contratti;
                        if (cm.MostraDataOraFermo)
                        {
                            rrdl.MostraDataOraFermo = true;
                            rrdl.MostraDataOraRiavvio = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraFermo = false;
                            rrdl.MostraDataOraRiavvio = false;
                        }
                        if (cm.MostraDataOraSopralluogo)
                        {
                            rrdl.MostraDataOraSopralluogo = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraSopralluogo = false;
                        }

                        if (cm.MostraDataOraAzioniTampone)
                        {
                            rrdl.MostraDataOraAzioniTampone = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraAzioniTampone = false;
                        }

                        if (cm.MostraDataOraInizioLavori)
                        {
                            rrdl.MostraDataOraInizioLavori = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraInizioLavori = false;
                        }

                        if (cm.MostraDataOraCompletamento)
                        {
                            rrdl.MostraDataOraCompletamento = true;
                        }
                        else
                        {
                            rrdl.MostraDataOraCompletamento = false;
                        }

                        if (cm.MostraElencoCauseRimedi)
                        {
                            rrdl.MostraElencoCauseRimedi = true;
                        }
                        else
                        {
                            rrdl.MostraElencoCauseRimedi = false;
                        }

                        #endregion
                        rrdl.Save();
                        //xpObjectSpaceRRdl.CommitChanges();
                        #region vecchio codice registro autorizzazioni
                        //Commesse cm = xpObjectSpaceRRdl.GetObject<Commesse>(rdl.Immobile.Commesse);
                        //if (cm.MostraDataOraFermo)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraDataOraFermo;
                        //    autrrdl.Save();
                        //    autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraDataOraRiavvio;
                        //    autrrdl.Save();

                        //}
                        //if (cm.MostraDataOraSopralluogo)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraDataOraSopralluogo;
                        //    autrrdl.Save();
                        //}

                        //if (cm.MostraDataOraAzioniTampone)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraDataOraAzioniTampone;
                        //    autrrdl.Save();
                        //}
                        //if (cm.MostraDataOraInizioLavori)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraDataOraInizioLavori;
                        //    autrrdl.Save();
                        //}

                        //if (cm.MostraDataOraCompletamento)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraDataOraCompletamento;
                        //    autrrdl.Save();
                        //}
                        //if (cm.MostraElencoCauseRimedi)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraElencoCauseRimedi;
                        //    autrrdl.Save();
                        //}
                        //if (cm.MostraSoddisfazioneCliente)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraSoddisfazioneCliente;
                        //    autrrdl.Save();
                        //}
                        #endregion
                        //xpObjectSpaceRRdl.CommitChanges();
                        //if (cm.MostraSoddisfazioneCliente)
                        //{
                        //    AutorizzazioniRegistroRdL autrrdl = xpObjectSpaceRRdl.CreateObject<AutorizzazioniRegistroRdL>();
                        //    autrrdl.RegistroRdL = rrdl;
                        //    autrrdl.TipoAutorizzazioniRegRdL = Classi.TipoAutorizzazioniRegRdL.MostraSoddisfazioneCliente;
                        //    autrrdl.Save();
                        //}
                        //xpObjectSpaceRRdl.CommitChanges();

                        //Aggiorna  RdL   ###########################################à   AGGIORNA RDL
                        rdl.DataAggiornamento = DateTime.Now;
                        rdl.UtenteUltimo = SecuritySystem.CurrentUserName; ;
                        // imposto la nuova richiesta  coma da prendere in carico
                        rdl.UltimoStatoSmistamento = xpObjectSpaceRRdl.GetObjectByKey<StatoSmistamento>(2); //2	Assegnata
                        rdl.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObjectByKey<StatoOperativo>(19); //Assegnata-Da prendere in carico
                        rdl.RegistroRdL = rrdl;
                        rdl.Save();
                        //  crea OdL  ######  CREA ODL   CREA ODL   ######  CREA ODL   CREA ODL   ######  CREA ODL   CREA ODL   ######  CREA ODL   CREA ODL   
                        OdL newOdL = xpObjectSpaceRRdl.CreateObject<OdL>();
                        newOdL.RegistroRdL = rrdl;
                        string odlDescrizione = rdl.Descrizione;
                        if (odlDescrizione.Length > 240)
                            odlDescrizione = odlDescrizione.Substring(1, 239) + "...";

                        newOdL.Descrizione = odlDescrizione;

                        newOdL.TipoOdL = xpObjectSpaceRRdl.GetObjectByKey<TipoOdL>(1);//xpObjSpaceOdL.GetObjectByKey<>(1);// misto
                        newOdL.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObjectByKey<StatoOperativo>(19); //Assegnata-Da prendere in carico
                        newOdL.DataEmissione = rdl.DataAssegnazioneOdl;
                        newOdL.Save();
                        //Aggiunta update di Rdl con nuovo ID ODL
                        rdl.OdL = newOdL;
                        rdl.Save();

                        xpObjectSpaceRRdl.CommitChanges();

                        //1	MANUTENZIONE PROGRAMMATA     //5	MANUTENZIONE PROGRAMMATA SPOT
                        //2	CONDUZIONE            //3	MANUTENZIONE A CONDIZIONE            //4	MANUTENZIONE GUASTO  
                        if (rrdl.Categoria.Oid == 1 || rrdl.Categoria.Oid == 5)
                        {
                            Descrizione = string.Format("Reg.MP({0}) {1}", rrdl.Oid, rdl.Descrizione);
                            if (Descrizione.Length > 3999)
                                Descrizione = Descrizione.Substring(1, 3996) + "...";
                            rrdl.Descrizione = Descrizione;
                        }
                        else
                        {
                            Descrizione = string.Format("Reg.TT({0}) {1}", rrdl.Oid, rdl.Descrizione);
                            if (Descrizione.Length > 3999)
                                Descrizione = Descrizione.Substring(1, 3996) + "...";
                            rrdl.Descrizione = Descrizione; //
                        }
                        rrdl.Save();
                        xpObjectSpaceRRdl.CommitChanges();


                        // vai alla nuova RdL separata
                        var view = Application.CreateDetailView(xpObjectSpaceRRdl, "RdL_DetailView_Gestione", true, rdl);
                        view.Caption = string.Format("Nuova RdL (separata)");
                        view.ViewEditMode = ViewEditMode.Edit;

                        e.ShowViewParameters.CreatedView = view;
                        e.ShowViewParameters.Context = TemplateContext.View;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }
                }
            }


        }

        private void saAnnullamentoMP_Execute(object sender, SimpleActionExecuteEventArgs e)
        {


            IObjectSpace xpObjectSpaceRRdl = Application.CreateObjectSpace();
            if (xpObjectSpaceRRdl != null)
            {
                RdL rdl = null;
                if (View is DetailView)
                {
                    rdl = xpObjectSpaceRRdl.GetObject<RdL>(((DetailView)View).CurrentObject as RdL);
                }
                if (View is ListView)
                {
                    rdl = xpObjectSpaceRRdl.GetObject<RdL>(((ListView)View).CurrentObject as RdL);
                }
                if (rdl != null)
                {


                    int numRdL = rdl.RegistroRdL.RdLes.Count;
                    if (numRdL > 0 && (rdl.Categoria.Oid == 1 || rdl.Categoria.Oid == 5))
                    {
                        //Aggiorna  RdL   ###########################################à   AGGIORNA RDL
                        rdl.DataAggiornamento = DateTime.Now;
                        rdl.UtenteUltimo = SecuritySystem.CurrentUserName; ;
                        // imposto la nuova richiesta  coma da prendere in carico
                        rdl.UltimoStatoSmistamento = xpObjectSpaceRRdl.GetObjectByKey<StatoSmistamento>(7); //2	Anullata
                        rdl.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObjectByKey<StatoOperativo>(13); //Assegnata-Da prendere in carico
                        rdl.Save();
                        xpObjectSpaceRRdl.CommitChanges();
                        xpObjectSpaceRRdl.Refresh();

                    }
                }
            }
        }

        private void pupWinNewRichiedenteRdL_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            RdL rdl = ((DetailView)View).CurrentObject as RdL;
            if (View.Id.Contains("RdL_DetailView"))
            {
                Richiedente Richiedente = View.ObjectSpace.CreateObject<Richiedente>();
                Richiedente.Commesse = rdl.Immobile.Contratti;
                var view = Application.CreateDetailView(View.ObjectSpace, "Richiedente_DetailView_NewRdL", false, Richiedente);

                view.Caption = string.Format("Richiedente x Richiesta di Lavoro");
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
                e.DialogController.SaveOnAccept = false;
            }
        }

        private void pupWinNewRichiedenteRdL_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //RdL rdl = ((DetailView)View).CurrentObject as RdL;
            //Richiedente Richiedente = ((Richiedente)(((e.PopupWindow)).View).SelectedObjects[0]);
            //rdl.SetMemberValue("Richiedente", Richiedente);
            if (View is DetailView)
            {
                RdL rdl = ((DetailView)View).CurrentObject as RdL;
                if (e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    Richiedente newRichiedente = (Richiedente)e.PopupWindowViewSelectedObjects[0];
                    if (newRichiedente != null)
                    {
                        rdl.SetMemberValue("Richiedente", newRichiedente);
                    }
                }

            }


        }

        private void pupWinucApparatoMapRdL_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    try
                    {
                        ucApparatoMapRdL ObjCurr = (ucApparatoMapRdL)(((e.PopupWindow)).View).SelectedObjects[0];
                        int OidApparato = ObjCurr.OidApparato;
                        Asset app = xpObjectSpace.GetObjectByKey<Asset>(OidApparato);
                        if (app != rdl.Asset)
                        {
                            rdl.SetMemberValue("Asset", app);
                            rdl.SetMemberValue("RicercaApparato", app.CodDescrizione);
                        }
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void pupWinucApparatoMapRdL_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    GroupOperator criteriaEdificioImpianto = new GroupOperator(GroupOperatorType.And);
                    GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                    string DescrizioneImpianto = string.Empty;
                    string CaptionFiltroRicerca = string.Empty;
                    string Filtro = string.Empty;
                    //string CaptionView = string.Empty;
                    List<string> CaptionView = new List<string>();
                    criteriaEdificioImpianto.Operands.Clear();

                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;                   
                    var _ucApparatoMapRdL = xpObjectSpace.CreateObject<ucApparatoMapRdL>();
                    //RegAPPROLavCons.RegistroLavoriConsuntivi = xpObjectSpace.GetObjectByKey<RegistroLavoriConsuntivi>(RegLavCons.Oid);
                    if (_ucApparatoMapRdL != null)
                    {
                        int oidedificio = ((Immobile)rdl.GetMemberValue("Immobile")).Oid;
                        int oidImpianto = ((Servizio)rdl.GetMemberValue("Impianto")).Oid;
                        _ucApparatoMapRdL.Immobile = xpObjectSpace.GetObjectByKey<Immobile>(oidedificio);
                        _ucApparatoMapRdL.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(oidImpianto);
       
                        var view = Application.CreateDetailView(xpObjectSpace, "Custom1ucApparatoMapRdL_DetailView", false, _ucApparatoMapRdL);
                        //view.ViewEditMode = ViewEditMode.Edit;
                        view.Caption = string.Format("{0} {1}", view.Caption, String.Join(" - ", CaptionView));
                        
                        e.DialogController.SaveOnAccept = false;
                        e.View = view;
                        e.Maximized = true;
                    }
                  
                    //var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    //e.DialogController.SaveOnAccept = false;
                    //e.View = view;
                }
            }
        }
    }
}

