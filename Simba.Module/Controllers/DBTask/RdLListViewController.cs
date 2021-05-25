using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListViewController : ViewController
    {
        int OidRdL = 0;
        public RdLListViewController()
        {
            InitializeComponent();
            this.TargetObjectType = typeof(CAMS.Module.DBTask.Vista.RdLListView);
            //
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // reset variabili report
            Rdlcodici = string.Empty;
            objects.Clear();
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;

            acVisualizzaDettagliRdLListViev.Items.Clear();

            #region se è una LISTVIEW
            if (View is ListView)
            {
                var Lv = (ListView)View;

                bool TastoVisibleWrite = false;
                using (UtilController uc = new UtilController())
                {
                    var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                    TastoVisibleWrite = uc.GetIsGrantedCreate(xpObjectSpace, "RegNotificheEmergenze", "W");
                }
                acCreaRegRdLNotificaEmergenza.Active.SetItemValue("Active", TastoVisibleWrite);


                scaReportRdLListView.Items.Add((new ChoiceActionItem()
                {
                    Id = 1.ToString(),
                    Caption = "Stampa Report",
                    Data = 1.ToString()
                }));
            }
            #endregion


        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.

        }
        protected override void OnDeactivated()
        {
            try
            {
                Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomizeShowViewParameters -= LViewController_CustomizeShowViewParameters;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }





        void EditRdLLVAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //e.Handled = true;
                DetailView NewDv;
                RdLListView GetRdL_ListView = xpObjectSpace.GetObject<RdLListView>((RdLListView)View.CurrentObject);
                // NewDv = GetDetailViewPersonalizzata(GetRegistroRdL, xpObjectSpace);
                RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetRdL_ListView.Codice);
                NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.Edit;
                    e.ShowViewParameters.CreatedView = NewDv;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }
        }

        private void acReportRdLListView_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //---------------------------------------------------------------------------
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            // reset variabili report
            Rdlcodici = string.Empty;
            RegRdlcodici = string.Empty;
            objects.Clear();
            if (xpObjectSpace != null) // (reportData != null)
            {
                // filtro da imposare nel report         
                string CriterioAdd = string.Empty;
                string CommessaSG = string.Empty;
                int CommessaOID = 0;
                int CategoriaOID = 0;
                Type ObjectType = typeof(CAMS.Module.DBTask.RdL);
                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                #region se è list view elabora i criteri di ricerca
                if (View is ListView)
                {
                    #region Parametri calcolo Report
                    int PrimoCodiceRdL = (((ListView)View).Editor).GetSelectedObjects()
                                                .Cast<RdLListView>().ToList<RdLListView>().Select(s => s.Codice).First();

                    RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(PrimoCodiceRdL);
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
                    #endregion

                    int ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;

                    //foreach (string Codice in (((ListView)View).Editor).GetSelectedObjects())
                    //{

                    //}

                    if (ObjCount > 0)
                    {
                        if (ObjCount < 10000)
                        {
                            var ArObjSel = (((ListView)View).Editor).GetSelectedObjects()
                                                .Cast<RdLListView>().ToList<RdLListView>()
                                               .Select(s => s.Codice).Distinct().ToArray<int>();// codice è codiceRdL!!!

                            string sOids = String.Join(",", ArObjSel);
                            //CriterioAdd = string.Format("[Codice] In ({0})", String.Join(",", ArObjSel));
                            CriterioAdd = string.Format("[codiceRdL] In ({0})", String.Join(",", ArObjSel));
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                        }
                        else
                        {
                            var ArObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListView>()
                                        .ToList<RdLListView>()
                                        .Select(s => s.Codice).Distinct().ToList<int>(); // codice è codiceRdL!!!

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
                                CriterioAdd = string.Format("[codiceRdL] In ({0})", String.Join(",", output));
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                            }
                        }
                    }
                }
                #endregion
                string DisplayReportPersonale = "";
                try
                {

                    DisplayReportPersonale = Sess.Query<CAMS.Module.DBAngrafica.SetReportCommessa>()
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

                IObjectSpace objectSpace =
                    DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportObjectSpaceProvider.CreateObjectSpace(
                    typeof(DevExpress.Persistent.BaseImpl.ReportDataV2));

                if (!string.IsNullOrEmpty(DisplayReportPersonale))// se è personalizzato
                {
                    CriterioReport = string.Format("[DisplayName] = '{0}'", DisplayReportPersonale);
                    criteriaReport.Operands.Add(CriteriaOperator.Parse(CriterioReport));

                    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                                objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(criteriaReport);
                    //   CriteriaOperator.Parse("[DisplayName] = 'Interventi di Manutenzione'"));  //   CriteriaOperator.Parse("[DisplayName] = 'Interventi di Manutenzione'"));  
                    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                    // apri report
                    Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle, criteriaOP2);
                }
                else
                {
                    foreach (RdLListView cur in View.SelectedObjects)
                    {
                        if (Rdlcodici.Length < 3990)
                            Rdlcodici = string.Concat(Rdlcodici, cur.Codice.ToString() + ",");
                    }
                    var vCodRegRdL = View.SelectedObjects.Cast<RdLListView>().Select(s => s.CodRegRdL).Distinct();
                    foreach (int curCodRegRdL in vCodRegRdL)
                    {
                        //if (RegRdlcodici.Length < 3990)
                        RegRdlcodici = string.Concat(RegRdlcodici, curCodRegRdL.ToString() + ",");
                    }
                    DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                       objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
                       CriteriaOperator.Parse("[DisplayName] = 'Report RdL di Manutenzione'")); //' Report Interventi di Manutenzione --> ex <<sub Report Interventi Manutenzione>>

                    //DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                    //   objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
                    // CriteriaOperator.Parse("[DisplayName] = 'DCReportRdL'"));

                    handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                    // apri report
                    Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle);
                }



            }
        }
        private void Application_ObjectSpaceCreated(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (View.Id.Contains("RdLListView_ListView"))
            {
                if (e.ObjectSpace is NonPersistentObjectSpace)
                {
                    ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += DCRdLListReport_objectSpace_ObjectsGetting;
                }
            }
        }


        private static string Rdlcodici = string.Empty;
        private static string RegRdlcodici = string.Empty;
        private static BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();

        void DCRdLListReport_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType.FullName == "CAMS.Module.DBTask.DC.DCRdLListReport")
            {
                if (View is ListView)
                {
                    //BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
                    if (objects != null)
                        if (objects.Count == 0)
                        {
                            using (DB db = new DB())
                            {
                                //objects = db.GetReport_RdL(Rdlcodici.ToString());   db.GetReport_RdL(RegRdlcodici.ToString());   GetREPORT_REGRDL
                                objects = db.GetREPORT_REGRDL(RegRdlcodici.ToString());
                            }
                            e.Objects = objects;
                        }
                        else
                        {
                            e.Objects = objects;
                        }
                }
            }
        }





        #region action Visualizza Dettagli @@@@@@
        private void acVisualizzaDettagliRdLListViev_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            CAMS.Module.Classi.SetVarSessione.Esegui_DeSelezionaDati = false;
            //if (xpObjectSpace != null)
            //{
            //    DetailView dv = (DetailView)View;
            //    RdL RdL = (RdL_ListView)dv.CurrentObject;
            //    ListView lv = null; //                string listViewId = string.Empty;

            //    string ViewSelezionato = e.SelectedChoiceActionItem.Id.ToString();
            //    string FiltroSelezionato = e.SelectedChoiceActionItem.Data.ToString();
            //    if (ViewSelezionato.Contains("RegistroRdL_DetailView"))
            //    {
            //        var listv = Application.FindModelView(ViewSelezionato);
            //        DetailView dvw = GetDetailViewbyMenu(typeof(CAMS.Module.DBTask.RegistroRdL), ViewSelezionato, RdL.RegistroRdL.Oid);//Application.CreateListView(listViewId, clTicketLv, true);
            //        e.ShowViewParameters.CreatedView = dvw;
            //    }
            //    else if (ViewSelezionato.Contains("_ListView_"))
            //    {
            //        var listv = Application.FindModelView(ViewSelezionato);
            //        Type oggetto = ((((DevExpress.ExpressApp.Model.IModelListView)(listv))).AsObjectView.ModelClass).TypeInfo.Type;
            //        lv = GetListViewbyMenu(oggetto, ViewSelezionato, FiltroSelezionato);//Application.CreateListView(listViewId, clTicketLv, true);
            //        e.ShowViewParameters.CreatedView = lv;
            //    }

            //    // e.ShowViewParameters.CreatedView = lv;
            //    e.ShowViewParameters.TargetWindow = TargetWindow.Current;
            //}
        }

        private ListView GetListViewbyMenu(Type Oggetto, string ListViewId, string Filtro)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            var clTicketLv = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, Oggetto, ListViewId);
            clTicketLv.Criteria.Clear();
            clTicketLv.Criteria["Filtro_Visualizza"] = CriteriaOperator.Parse(Filtro);//string.Format("RegistroRdL.Oid = {0}", RdL.RegistroRdL.Oid)
            return Application.CreateListView(ListViewId, clTicketLv, true);
        }

        private DetailView GetDetailViewbyMenu(Type Oggetto, string ListViewId, int Oid)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            var DettaglioObj = xpObjectSpace.GetObjectByKey(Oggetto, Oid);
            var view = Application.CreateDetailView(xpObjectSpace, ListViewId, true, DettaglioObj);
            view.ViewEditMode = ViewEditMode.View;
            return view;
        }
        #endregion

        private void acCreaRegRdLNotificaEmergenza_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View is ListView)
            {
                bool SpediscieMail = false;
                string RisorseAssociate = string.Empty;
                //var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                IObjectSpace xpObjectSpace = View.ObjectSpace;

                List<DCRisorseTeamRdL> lst_DCRisorseTeamRdL_Selezionate = ((List<DCRisorseTeamRdL>)((((DevExpress.ExpressApp.Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<DCRisorseTeamRdL>().ToList<DCRisorseTeamRdL>()));
                if (xpObjectSpace != null && lst_DCRisorseTeamRdL_Selezionate.Count > 0)
                {
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        RdLListView vRdLListView = xpObjectSpace.GetObject<RdLListView>(
                                                       (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListView>()
                                                                                 .ToList<RdLListView>().First()
                                                        );
                        if (vRdLListView != null)
                        {
                            RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(vRdLListView.Codice);
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
                            View.ObjectSpace.ReloadObject(vRdLListView);
                            View.Refresh(true);
                        }

                    }
                }
            }

        }

        private void acCreaRegRdLNotificaEmergenza_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        RdLListView list_RdLListView_Selezionati = (((ListView)View).Editor).GetSelectedObjects().
                                                                   Cast<RdLListView>().ToList<RdLListView>().First();
                        RdL RDLSelezionata = xpObjectSpace.GetObjectByKey<RdL>(list_RdLListView_Selezionati.Codice);
                        OidRdL = RDLSelezionata.Oid;
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
        }


        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is ListView)
            {
                ListView lv = (ListView)View;
                BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();

                RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
                if (rdl.Immobile != null)
                {
                    using (DB db = new DB())
                    {
                        int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
                        int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                        objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRA);
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

        private void scaReportRdLListView_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {

        }
    }
}



//public RegistroSmistamentoDett CreateRegistroSmistamentoDett_Emergenza(RdL rdl, string elencoRisorseTeam) // tipocambio:= "RSTEAM" : "SM";
//{
//    DateTime RSD_ultimadata = DateTime.MinValue;
//    double RSD_dminuti = 0;
//    int RSD_idmin = 0;
//    int RSD_ultimaOid = 0;
//    int conta = rdl.RegistroRdL.RegistroSmistamentoDettaglios.Count;

//    IObjectSpace xpObjSpaceRSD = Application.CreateObjectSpace();

//    if (conta > 0 && rdl.RegistroRdL != null)
//    {
//        var listaRSD = rdl.RegistroRdL.RegistroSmistamentoDettaglios.OrderByDescending(o => o.DataOra).First();

//        RSD_ultimadata = listaRSD.DataOra;
//        RSD_ultimaOid = listaRSD.Oid;
//        RSD_dminuti = (DateTime.Now - RSD_ultimadata).TotalMinutes;
//        RSD_idmin = Convert.ToInt32(RSD_dminuti);
//    }

//    RegistroSmistamentoDett rsm = xpObjSpaceRSD.CreateObject<RegistroSmistamentoDett>();
//    rsm.RegistroRdL = xpObjSpaceRSD.GetObject<RegistroRdL>(rdl.RegistroRdL);

//    if (rdl.RisorseTeam != null)
//        rsm.RisorseTeam = xpObjSpaceRSD.GetObject<RisorseTeam>(rdl.RisorseTeam);

//    rsm.StatoSmistamento = xpObjSpaceRSD.GetObject<StatoSmistamento>(rdl.UltimoStatoSmistamento);
//    if (rdl.old_SSmistamento_Oid != 0) // dovrebbe essere 1 se tutto funziona bene
//        rsm.SStatoSmistamento_Old = xpObjSpaceRSD.GetObjectByKey<StatoSmistamento>(rdl.old_SSmistamento_Oid);//   rdl.old_SSmistamento_Oid

//    if (rdl.UltimoStatoOperativo != null)  // dovrebbe essere nullo se tutto funziona bene
//        rsm.StatoOperativo = xpObjSpaceRSD.GetObject<StatoOperativo>(rdl.UltimoStatoOperativo);

//    rsm.UtenteSO = SecuritySystem.CurrentUserName;
//    rsm.DataOra = DateTime.Now;

//    if (RSD_ultimadata != DateTime.MinValue)
//        rsm.DataOra_Old = RSD_ultimadata;

//    if (rdl.UltimoStatoOperativo != null)
//        rsm.DescrizioneVariazioneStatoOperativo = string.Format("Stato Operativo {0}", rsm.StatoOperativo);

//    if (elencoRisorseTeam != null)
//        rsm.DescrizioneVariazioneRisorsaTeam = string.Format("Risorse Team Assegnate in Emergenza {0} ", elencoRisorseTeam);

//    if (rdl.UltimoStatoSmistamento != null)
//    {
//        string DescrizioneSmistamentoUltimo = rdl.UltimoStatoSmistamento.SSmistamento;
//        string DescrizioneSmistamentoOld = xpObjSpaceRSD.GetObjectByKey<StatoSmistamento>(1).SSmistamento;

//        rsm.Descrizione = string.Format("Variazione Stato Smistamento da {0} a {1}",
//              DescrizioneSmistamentoOld ?? string.Empty,
//               DescrizioneSmistamentoUltimo ?? string.Empty);
//    }                       

//    rsm.Save();
//    xpObjSpaceRSD.CommitChanges();
//    // prendere oid del precedente e aggiornare il tempo
//    if (RSD_ultimaOid > 0 && RSD_idmin > 0)
//    {
//        RegistroSmistamentoDett ultimoRSD = xpObjSpaceRSD.GetObjectByKey<RegistroSmistamentoDett>(RSD_ultimaOid);
//        rsm.DeltaTempoStato = RSD_idmin;
//        rsm.Save();
//        xpObjSpaceRSD.CommitChanges();
//    }

//    return rsm;
//}
