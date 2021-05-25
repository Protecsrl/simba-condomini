using CAMS.Module.Classi;
using CAMS.Module.Costi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBPlant;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.DC;
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
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
namespace CAMS.Module.Controllers.DBTask
{
    public partial class RegistroRdLController : ViewController
    {
        bool TastoVisibleWrite { get; set; }

        public RegistroRdLController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Application.ObjectSpaceCreated += Application_ObjectSpaceCreated;
            TastoVisibleWrite = false;
            using (UtilController uc = new UtilController())
            {
                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                TastoVisibleWrite = uc.GetIsGrantedCreate(xpObjectSpace, "RegNotificheEmergenze", "W");
            }
            if (View is DetailView)
            {
                bool VisualizzaTasto = ((DetailView)View).ViewEditMode == ViewEditMode.Edit;
                this.acRegRdL_DelRisorsaTeam.Active.SetItemValue("Active", VisualizzaTasto);
                this.pupRegRdL_WinRisorseTeamDC.Active.SetItemValue("Active", VisualizzaTasto);
                //  ------------------
                this.popRegRdL_GetDCRdL.Active.SetItemValue("Active", VisualizzaTasto);

                //popRegRdL_GetDCRdL
            }


            //acCreaRegRdLNotificaEmergenza.Active.SetItemValue("Active", TastoVisibleWrite);
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View is DetailView)
            {
                bool VisualizzaTasto = ((DetailView)View).ViewEditMode == ViewEditMode.Edit;
                this.acRegRdL_DelRisorsaTeam.Active.SetItemValue("Active", VisualizzaTasto);
                this.pupRegRdL_WinRisorseTeamDC.Active.SetItemValue("Active", VisualizzaTasto);
                //  ------------------
                this.popRegRdL_GetDCRdL.Active.SetItemValue("Active", VisualizzaTasto);

                //popRegRdL_GetDCRdL
            }
        }

        protected override void OnDeactivated()
        {
            try
            {
                Application.ObjectSpaceCreated -= Application_ObjectSpaceCreated;
            }
            catch { }

            base.OnDeactivated();
        }
        private static string Rdlcodici = string.Empty;
        private static string RegRdlcodici = string.Empty;
        private static BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
        //  #################  crea notifica emergenza 
        private void acCreaRegRdLNotificaEmergenza_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                var lstRisorseSelezionate = ((List<TRisorseEmergenza>)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).SelectedObjects.Cast<TRisorseEmergenza>().ToList<TRisorseEmergenza>()));
                var NuovoRegEmergenze = xpObjectSpace.CreateObject<RegNotificheEmergenze>();
                RegistroRdL RegRdL = null;
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RegRdL = xpObjectSpace.GetObject<RegistroRdL>((RegistroRdL)dv.CurrentObject);         // rdl = xpObjectSpace.GetObject<RdL>((RdL)dv.CurrentObject);
                }
                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        RegRdL = xpObjectSpace.GetObject<RegistroRdL>(
                            (((ListView)View).Editor).GetSelectedObjects().Cast<RegistroRdL>().ToList<RegistroRdL>().First()
                             );
                    }
                if (RegRdL != null)
                {
                    RdL rdl = RegRdL.RdLes.FirstOrDefault();
                    RegRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10);
                    rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10);
                    NuovoRegEmergenze.RdL = rdl;
                    NuovoRegEmergenze.RegStatoNotifica = RegStatiNotificaEmergenza.daAssegnare;
                    NuovoRegEmergenze.DataCreazione = DateTime.Now;
                    NuovoRegEmergenze.DataAggiornamento = DateTime.Now;
                    foreach (TRisorseEmergenza r in lstRisorseSelezionate)
                    {
                        NuovoRegEmergenze.NotificheEmergenzes.Add(new NotificheEmergenze(NuovoRegEmergenze.Session)
                        {
                            DataCreazione = DateTime.Now,
                            Team = xpObjectSpace.GetObject<RisorseTeam>(r.RisorsaTeam),
                            CodiceNotifica = Guid.NewGuid(),
                            StatoNotifica = StatiNotificaEmergenza.NonVisualizzato,
                            DataAggiornamento = DateTime.Now
                        });
                    }
                }
                // ------------------------
                xpObjectSpace.CommitChanges();
                using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                {
                    db.AssegnaInEmergenzaRegRdL(RegRdL.Oid, "RegRdLE");
                }
                View.Refresh();
            }
        }

        private void acCreaRegRdLNotificaEmergenza_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    RegistroRdL lstRegistroRdLSel = (RegistroRdL)dv.CurrentObject;
                    RdL RDLSelezionata = lstRegistroRdLSel.RdLes.First();
                    int OidEdificio = RDLSelezionata.Immobile.Oid;
                    var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(TRisorseEmergenza), "TRisorseEmergenza_ListView");
                    string Filtro = string.Format("Immobile.Oid = {0}", OidEdificio);
                    ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
                    var view = Application.CreateListView("TRisorseEmergenza_ListView", ListTRisorseEmergenza, false);
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                    //e.View = view;
                    //e.IsSizeable = true;
                }

                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        RegistroRdL lstRegRDLSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<RegistroRdL>().ToList<RegistroRdL>().First();
                        RdL RDLSelezionata = lstRegRDLSelezionati.RdLes.First();
                        int OidEdificio = RDLSelezionata.Immobile.Oid;
                        // --- imposto la rdl sulla notifica emergenza  
                        var ListTRisorseEmergenza = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(TRisorseEmergenza), "TRisorseEmergenza_ListView");
                        string Filtro = string.Format("Immobile.Oid = {0}", OidEdificio);
                        ListTRisorseEmergenza.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(Filtro);
                        var view = Application.CreateListView("TRisorseEmergenza_ListView", ListTRisorseEmergenza, false);
                        e.View = view;
                        e.IsSizeable = true;
                    }
            }
        }

        private void acRegistroCostiRicaviRegRdL_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    RegistroRdL RegRdl = (RegistroRdL)((DetailView)View).CurrentObject;

                    var NuovoRegistroCosti = xpObjectSpace.CreateObject<RegistroLavori>();
                    NuovoRegistroCosti.Immobile = xpObjectSpace.GetObject<Immobile>(RegRdl.Asset.Servizio.Immobile);
                    NuovoRegistroCosti.Servizio = xpObjectSpace.GetObject<Servizio>(RegRdl.Asset.Servizio);
                    NuovoRegistroCosti.RegistroRdL = xpObjectSpace.GetObject<RegistroRdL>(RegRdl);
                    NuovoRegistroCosti.Descrizione = "Costi ricavi relativo alla RegRdl " + RegRdl.Oid;

                    var view = Application.CreateDetailView(xpObjectSpace, "RegistroLavori_DetailView", true, NuovoRegistroCosti);
                    view.Caption = string.Format("Nuovo Registro Lavori");
                    view.ViewEditMode = ViewEditMode.Edit;

                    e.ShowViewParameters.CreatedView = view;
                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                    e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                }
            }

        }

        private void acreport_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            int ObjCount = 0;
            // reset variabili report
            Rdlcodici = string.Empty;
            RegRdlcodici = string.Empty;
            objects.Clear();

            if (xpObjectSpace != null)
            {

                // filtro da imposare nel report         
                string CriterioAdd = string.Empty;
                string CommessaSG = string.Empty;
                int CommessaOID = 0;
                int CategoriaOID = 0;
                var SelectedContacts = new ArrayList();
                Type ObjectType = typeof(CAMS.Module.DBTask.RdL);
                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.Or);
                #region se è list view elabora i criteri di ricerca
                if (View is ListView && (((ListView)View).Editor).GetSelectedObjects().Count > 0)
                {
                    ObjCount = (((ListView)View).Editor).GetSelectedObjects().Count;
                    foreach (var selectedObject in e.SelectedObjects)
                    {
                        //var currentObject = View.ObjectSpace.GetObject(View.CurrentObject);
                        int currentObjectKey = (int)View.ObjectSpace.GetKeyValue(selectedObject);
                        SelectedContacts.Add(currentObjectKey);
                    }
                }
                else if (View is DetailView)
                {
                    ObjCount = 1;
                    int currentObjectKey = (int)View.ObjectSpace.GetKeyValue(((DetailView)View).CurrentObject);
                    SelectedContacts.Add(currentObjectKey);
                }
                //-----------------------------------------------------------------------------------------------

                //-----------------------------------------------------------------------------------------------
                if (ObjCount > 0)
                {
                    #region Parametri calcolo Report
                    var ArObjSel_RegRdLListView = SelectedContacts.Cast<int>().ToArray<int>();

                    var qOidRdL = new XPQuery<RdL>(Sess)
                                .Where(w => ArObjSel_RegRdLListView.Contains<int>(w.RegistroRdL.Oid))
                                .Select(s => s.Oid).ToArray<int>();
                    

                    try
                    {
                        var ListDataReport = new XPQuery<RdL>(Sess)
                             .Where(w => w.RegistroRdL.Oid == ArObjSel_RegRdLListView.First())
                             .Select(s => new
                             {
                                 CommessaOID = s.Immobile.Contratti.Oid,
                                 CommessaSG = s.Immobile.Contratti.WBS,
                                 CategoriaOID = s.Categoria.Oid
                             }).ToList();

                        if (ListDataReport.Count > 0)
                        {
                            CommessaSG = ListDataReport[0].CommessaSG.ToString();
                            CommessaOID = ListDataReport[0].CommessaOID;
                            CategoriaOID = ListDataReport[0].CategoriaOID;
                        }
                    }
                    catch
                    { }
                    #endregion

                    if (ObjCount > 0)
                    {
                        if (ObjCount < 1000)
                        {
                            CriterioAdd = string.Format("[codiceRdL] In ({0})", String.Join(",", qOidRdL));
                            criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                        }
                        else
                        {
                            int contaRdL = qOidRdL.Count<int>();
                            for (var i = 1; i < contaRdL; i++)
                            {
                                string output = qOidRdL[i].ToString();
                                CriterioAdd = string.Format("[codiceRdL] = ({0})", output);
                                criteriaOP2.Operands.Add(CriteriaOperator.Parse(CriterioAdd));
                            }
                        }
                    }

                #endregion
                    string DisplayReportPersonale = "";
                    try
                    {

                        DisplayReportPersonale = Sess.Query<CAMS.Module.DBAngrafica.SetReportCommessa>()
                            .Where(w => w.Categoria.Oid == CategoriaOID && w.Commesse.Oid == CommessaOID &&
                                           w.ObjectType == ObjectType)
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
                        Rdlcodici = string.Empty;
                        RegRdlcodici = string.Empty;
                        objects.Clear();
                        //foreach (int cur in qOidRdL)
                        //{
                        //    Rdlcodici = string.Concat(Rdlcodici, cur.ToString() + ",");
                        //}

                        var qOidRegRdL = new XPQuery<RdL>(Sess)
                       .Where(w => ArObjSel_RegRdLListView.Contains<int>(w.RegistroRdL.Oid))
                       .Select(s => s.RegistroRdL.Oid).Distinct().ToArray<int>();

                        foreach (int cur in qOidRegRdL)
                        {
                            RegRdlcodici = string.Concat(RegRdlcodici, cur.ToString() + ",");
                        }
                        DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =
                           objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(
                           CriteriaOperator.Parse("[DisplayName] = 'Report RdL di Manutenzione'"));
                        //DevExpress.ExpressApp.ReportsV2.IReportDataV2 reportData =      objectSpace.FindObject<DevExpress.Persistent.BaseImpl.ReportDataV2>(                        // CriteriaOperator.Parse("[DisplayName] = 'DCReportRdL'"));

                        handle = DevExpress.ExpressApp.ReportsV2.ReportDataProvider.ReportsStorage.GetReportContainerHandle(reportData);
                        // apri report
                        Frame.GetController<DevExpress.ExpressApp.ReportsV2.ReportServiceController>().ShowPreview(handle);
                    }


                }
            }

        }


        private void Application_ObjectSpaceCreated(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (View.Id.Contains("RegistroRdL_"))
            {
                if (e.ObjectSpace is NonPersistentObjectSpace)
                {
                    ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += DCRdLListReport_objectSpace_ObjectsGetting;
                }
            }
        }


        void DCRdLListReport_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType.FullName == "CAMS.Module.DBTask.DC.DCRdLListReport")
            {        //BindingList<DCRdLListReport> objects = new BindingList<DCRdLListReport>();
                if (objects != null)
                {
                    if (objects.Count == 0)
                    {
                        using (DB db = new DB())
                        {
                            //  objects = db.GetReport_RdL(Rdlcodici.ToString());
                            //objects = db.GetReport_RdL(RegRdlcodici.ToString());
                            objects = db.GetREPORT_REGRDL(RegRdlcodici.ToString());  
                        }
                        e.Objects = objects;
                    }
                    else
                    {
                        e.Objects = objects;
                    }
                }
                //using (DB db = new DB())
                //{
                //    objects = db.GetReport_RdL(Rdlcodici.ToString());
                //}
                //e.Objects = objects;
            }
        }

        private void pupRegRdL_WinRisorseTeamDC_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView && e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    DetailView dv = (DetailView)View;
                    RegistroRdL rdl = (RegistroRdL)dv.CurrentObject;
                    int OidObjCurr = ((DCRisorseTeamRdL)(((e.PopupWindow)).View).SelectedObjects[0]).OidRisorsaTeam;
                    RisorseTeam RT = xpObjectSpace.GetObjectByKey<RisorseTeam>(OidObjCurr);
                    rdl.SetMemberValue("RisorseTeam", RT);

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

        private void pupRegRdL_WinRisorseTeamDC_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                            "DCRisorseTeamRdL_LookupListView");
                //  filtro
                DetailView dv = (DetailView)View;
                RegistroRdL Rrdl = (RegistroRdL)dv.CurrentObject;
                var ParCriteria = string.Empty;

                ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);
                //-----------  filtro
                //var view = Application.CreateListView(listViewId, ListRisorseTeamLookUp, false);

                //        // ListRisorseTeamLookUp.Collection
                if (!string.IsNullOrEmpty(Rrdl.RicercaRisorseTeam))
                { //   Azienda Mansione  Telefono

                    string Filtro1 = string.Empty;
                    string Filtro2 = string.Empty;
                    string Filtro3 = string.Empty;
                    string Filtro4 = string.Empty;
                    string AllFilter = string.Empty;

                    if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                    {

                         Filtro1 = string.Format("Contains([RisorsaCapo],'{0}')", Rrdl.RicercaRisorseTeam.ToString());
                         Filtro2 = string.Format("Contains([Azienda],'{0}')", Rrdl.RicercaRisorseTeam.ToString());
                         Filtro3 = string.Format("Contains([Mansione],'{0}')", Rrdl.RicercaRisorseTeam.ToString());
                         Filtro4 = string.Format("Contains([CentroOperativo],'{0}')", Rrdl.RicercaRisorseTeam.ToString());
                    }
                    else
                    {
                         Filtro1 = string.Format("Contains(Upper([RisorsaCapo]),'{0}')", Rrdl.RicercaRisorseTeam.ToUpper());
                         Filtro2 = string.Format("Contains(Upper([Azienda]),'{0}')", Rrdl.RicercaRisorseTeam.ToUpper());
                         Filtro3 = string.Format("Contains(Upper([Mansione]),'{0}')", Rrdl.RicercaRisorseTeam.ToUpper());
                         Filtro4 = string.Format("Contains(Upper([CentroOperativo]),'{0}')", Rrdl.RicercaRisorseTeam.ToUpper());

                    }
                    
                    AllFilter = string.Format("{0} Or {1} Or {2} Or {3}", Filtro1, Filtro2, Filtro3, Filtro4);
                    
                        
                        ((ListView)lvk).Model.Filter = AllFilter;
                }

                var dc = Application.CreateController<DialogController>();
                e.DialogController.SaveOnAccept = false;
                e.View = lvk;


                //objectSpace.ObjectsGetting -= DCRisorseTeamRdL_objectSpace_ObjectsGetting;
            }
        }

        void DCRisorseTeamRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (View is DetailView)
            {
                BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
                DetailView dv = (DetailView)View;
                RegistroRdL rdl = (RegistroRdL)dv.CurrentObject;
                if (rdl.Immobile != null)
                {
                    int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl.RdLes[0], View.ObjectSpace);
                    using (DB db = new DB())
                    {
                        int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                        objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRA);
                    }
                }
                e.Objects = objects;
            }
        }

        private void acRegRdL_DelRisorsaTeam_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                RegistroRdL Rrdl = (RegistroRdL)dv.CurrentObject;
                Rrdl.SetMemberValue("RisorseTeam", null);
            }
        }
        #region seleziona rdl da associare al registro
        private void popRegRdL_GetDCRdL_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
            //string sbMessaggio  = string.Empty;
            if (View is DetailView)
            {
                DetailView dv = View as DetailView;
                var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                List<DCRdL> lst_DCRdL_Selezionate = ((List<DCRdL>)((((Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<DCRdL>().ToList<DCRdL>()));

                if (xpObjectSpace != null && lst_DCRdL_Selezionate.Count > 0 && View.ObjectTypeInfo.Type == typeof(RegistroRdL))
                {
                    RegistroRdL RRdL = (RegistroRdL)dv.CurrentObject;
                    foreach (var item in lst_DCRdL_Selezionate)
                    {
                        RdL newrdl = (RdL)xpObjectSpace.GetObjectByKey<RdL>(item.ID);
                        OdL odl = null;
                        if (RRdL.OdLes.Count > 0)
                        {
                            odl = RRdL.OdLes[0];
                        }
                        else
                        {
                            odl = (OdL)xpObjectSpace.CreateObject<OdL>();
                        }
                        string odlDescrizione = RRdL.Descrizione;
                        if (odlDescrizione.Length > 240)
                            odlDescrizione = odlDescrizione.Substring(1, 239) + "...";
                        odl.Descrizione = odlDescrizione;
                        odl.RegistroRdL = RRdL;
                        odl.Save();

                        if (newrdl != null && RRdL != null)
                        {
                            try
                            {
                                newrdl.RegistroRdL = RRdL;
                                newrdl.Categoria = xpObjectSpace.GetObject<Categoria>(RRdL.Categoria);//
                                if (RRdL.RisorseTeam != null)
                                    newrdl.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(RRdL.RisorseTeam);
                                else
                                    newrdl.RisorseTeam = null;

                                newrdl.UltimoStatoOperativo = xpObjectSpace.GetObject<StatoOperativo>(RRdL.UltimoStatoOperativo);
                                newrdl.UltimoStatoSmistamento = xpObjectSpace.GetObject<StatoSmistamento>(RRdL.UltimoStatoSmistamento);
                                newrdl.DataAggiornamento = DateTime.Now;
                                newrdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                                newrdl.Save();
                            }
                            catch
                            {
                                SetMessaggioWebInterventi("Eseguito cambio registro", "Cambio Registro RdL", InformationType.Info);
                            }
                        }
                    }
                    RRdL.Save();
                    xpObjectSpace.CommitChanges();
                }
                else
                {
                    SetMessaggioWebInterventi("nessuna selezione eseguita!!!", "Selezione", InformationType.Warning);
                }

                SetMessaggioWebInterventi("Eseguito cambio registro", "Cambio Registro RdL", InformationType.Info);
            }
        }


        private void SetMessaggioWebInterventi(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
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


        private static Asset App;
        private static RisorseTeam RisorseTeam;
        private static RegistroRdL RegistroRdL;
        private void popRegRdL_GetDCRdL_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    RegistroRdL RRdL = ((DetailView)View).CurrentObject as RegistroRdL;
                    if (RRdL.Asset != null)
                    {
                        App = RRdL.Asset;
                        RisorseTeam = RRdL.RisorseTeam;
                        RegistroRdL = RRdL;
                    }

                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRdL));
                    objectSpace.ObjectsGetting += DCRdL_objectSpace_ObjectsGetting;
                    //DCRdL_LookupListView                 DCRdL_ListView
                    CollectionSource DCRdL_Lookup = (CollectionSource)Application.
                                        CreateCollectionSource(objectSpace, typeof(DCRdL), "DCRdL_ListView");
                    if (DCRdL_Lookup.GetCount() == 0)
                    {

                        SetMessaggioWebInterventi("non ci sono RdL aggregabili a questo registro", "Avviso", InformationType.Info);

                    }
                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("idColore", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    DCRdL_Lookup.Sorting.Add(srtProperty);

                    ListView lvk = Application.CreateListView("DCRdL_ListView", DCRdL_Lookup, true);

                    var dc = Application.CreateController<DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = lvk;
                }
            }

        }


        void DCRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            //1	MANUTENZIONE PROGRAMMATA
            //2	CONDUZIONE
            //3	MANUTENZIONE A CONDIZIONE
            //4	MANUTENZIONE GUASTO
            //5	MANUTENZIONE PROGRAMMATA SPOT
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (View is DetailView)
            {
                try
                {
                    //var a = RegistroRdL.RdLes.Select(s => s.Oid);
                    BindingList<DCRdL> objects = new BindingList<DCRdL>();
                    int idColore = 0;// 0= bianco:stessa AreaDiPoloOid, 1=verde:stesso co di base, 2= giallo  conduttore    CentroOperativoOid == 

                    XPQuery<RdL> customersQuery = new XPQuery<RdL>(Sess);
                    var customers = customersQuery
                    .Where(w => w.UltimoStatoSmistamento.Oid == 1)         //.Where(w => new[] { 4 }.Contains(w.Categoria.Oid))//  
                    .Where(w => new[] { 1, 5 }.Contains(w.Categoria.Oid))  //4=guasto //.Where(w => w.Apparato.Impianto.Immobile.CentroOperativoBase.AreaDiPolo.Oid == AreaDiPoloOid)
                    .Where(w => w.Asset.Servizio.Immobile.Oid == App.Immobile.Oid)
                    .Where(w => w.Asset.StdAsset.Oid == App.StdAsset.Oid)
                    .Where(w => w.RegistroRdL.Oid != RegistroRdL.Oid)
                        //.Where(w => !a.Contains(w.Oid))
                          .Select(s => new
                          {
                              ID = s.Oid,
                              RdLCodice = s.Codice.ToString(),
                              RdLDescrizione = s.Descrizione,
                              RdLSollecito = s.RdLNotes.Count() > 0 ? "Si" : "No",//.ToString(),
                              RichiedenteDescrizione = string.Format("{0} tel.:{1} - {2}", s.Richiedente.NomeCognome, s.Richiedente.TelefonoRichiedente, s.Richiedente.PhoneMobString),
                              EdificioDescrizione = s.Asset.Servizio.Immobile.Descrizione,
                              ImpiantoDescrizione = s.Servizio.Descrizione,
                              IndirizzoDescrizione = s.Immobile.Indirizzo.FullName,
                              DataPianificata = s.DataPianificata.ToString("dd/MM/yyyy HH:mm"),
                              CentroOperativoOid = s.Asset.Servizio.Immobile.CentroOperativoBase.Oid == RisorseTeam.CentroOperativo.Oid,
                              RisorseTeamOid = s.Asset.Servizio.Immobile.Conduttoris.Where(w => w.RisorseTeam.Oid == RisorseTeam.Oid).Count() > 0
                          });

                    foreach (var dr in customers)
                    {
                        if (dr.CentroOperativoOid)
                        {
                            idColore = 1;
                        }
                        else if (dr.RisorseTeamOid)
                        {
                            idColore = 2;
                        }
                        else
                        {
                            idColore = 0;
                        }
                        DCRdL objdcrdl = new DCRdL()
                        {
                            ID = dr.ID,//Guid.NewGuid, /*obj.ID = Newid++;*/
                            //RdL = dr,
                            RdLCodice = dr.RdLCodice,
                            RdLDescrizione = dr.RdLDescrizione,
                            RdLSollecito = string.Format(dr.RdLSollecito),
                            RichiedenteDescrizione = dr.RichiedenteDescrizione,
                            EdificioDescrizione = dr.EdificioDescrizione,
                            ImpiantoDescrizione = dr.ImpiantoDescrizione,
                            IndirizzoDescrizione = dr.IndirizzoDescrizione,
                            DataPianificata = dr.DataPianificata,
                            idColore = idColore
                        };

                        objects.Add(objdcrdl);
                    }

                    e.Objects = objects;
                }
                catch
                {
                }
            }
        }

        #endregion
    }
}
