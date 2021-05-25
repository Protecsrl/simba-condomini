using CAMS.Module.DBALibrary;
using CAMS.Module.DBAux;
using CAMS.Module.DBPlant;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBTask;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.Controllers.DBPlant
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ApparatoMapController : ViewController
    {
        public ApparatoMapController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                
                if (View is ListView)
                {

                    if (((ListView)View).Id.Contains("ApparatoMap_ListView"))
                    {
                        GroupOperator criteriadiEdificioeImpianto = new GroupOperator(GroupOperatorType.And);
                        ListView lv = (ListView)View;

                        var Collcr = lv.CollectionSource.Criteria;
                        int conta = Collcr.Keys.Count;
                        if (conta == 0)
                        {
                            DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                     xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                     ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);

                            FilteringCriterion NuovoFiltro = xpObjectSpace.FindObject<FilteringCriterion>(CriteriaOperator.Parse("myObjectType = ? And SecurityUser = ? And Description = ?",
                                                                                           typeof(AssetoMap), user, "Ultimo Filtro Mappa "));
                            //lv.CollectionSource.Criteria["criteriaEdificioImpianto"] = CriteriaOperator.Parse(NuovoFiltro.Criterion);
                            lv.CollectionSource.SetCriteria("criteriaEdificioImpianto", NuovoFiltro.Criterion);
                            criteriadiEdificioeImpianto.Operands.Add(NuovoFiltro.Criterion);

                            CriteriaOperator criteriaRicercaTestuale = CriteriaOperator.Parse("[OidApparatoSostegno] = 0");
                            //ListApparatiMapLV.Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;
                            lv.CollectionSource.SetCriteria("criteriaRicercaTestuale", criteriaRicercaTestuale.ToString());
                        }
     
                    }
                }
            }
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

        private void scaFilterApparatoMap_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            string DataFilter = e.SelectedChoiceActionItem.Data.ToString();
            GroupOperator GrOperator = new GroupOperator(GroupOperatorType.And);
            CriteriaOperator cr = CriteriaOperator.Parse(DataFilter);
            GrOperator.Operands.Add(cr); 
        }

        private void paImpiantoMap_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {

                if (View is ListView)
                {
                    if (((ListView)View).Id.Contains("Impianto_ApparatoMaps_ListView"))
                    {
                        GroupOperator criteriaIMPIANTO = new GroupOperator(GroupOperatorType.And);


                        ((ListView)View).CollectionSource.Criteria.Clear();

                        var dvParent = (DetailView)(View.ObjectSpace).Owner;
                        int oidimpianto = 0;
                        if (dvParent.Id.Contains("Impianto"))
                        {
                            Servizio imp = (Servizio)dvParent.CurrentObject;
                            oidimpianto = imp.Oid;
                            string FiltroImpianto = string.Format("[OidImpianto] = {0}", imp.Oid);
                            CriteriaOperator cr = CriteriaOperator.Parse(FiltroImpianto);
                            criteriaIMPIANTO.Operands.Add(cr);
                            ((ListView)View).CollectionSource.Criteria["FiltroImpianto"] = criteriaIMPIANTO;
                        }
                    }

                    GroupOperator criteriaOR = new GroupOperator(GroupOperatorType.Or);
                    if (e.ParameterCurrentValue != null)
                    {
                        string par = string.Empty;
                        string Filtro = string.Empty;
                        if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
                        { 
                         par = e.ParameterCurrentValue.ToString();
                        //S00718
                         Filtro = string.Format("Contains([Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([Edificio_Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Impianto_Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([StdApparatoDescrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([StdApparatoClassiDescrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([StradaInProssimita],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Strada_Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([AppPadreDescrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Targhetta],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Zona],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        }
                        else { 
                        par = e.ParameterCurrentValue.ToString().ToUpper();
                        //S00718
                        Filtro = string.Format("Contains([Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([Edificio_Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Impianto_Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([StdApparatoDescrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([StdApparatoClassiDescrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([StradaInProssimita],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Strada_Descrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        Filtro = string.Format("Contains([AppPadreDescrizione],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Targhetta],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));
                        Filtro = string.Format("Contains([Zona],'{0}')", par);
                        criteriaOR.Operands.Add(CriteriaOperator.Parse(Filtro));

                        }

                        ((ListView)View).CollectionSource.Criteria["FiltroParametroTesto"] = criteriaOR;

                    }

                }
            }
        }

        private void saCreaRdLbyMap_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                    NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                    //NuovoRdL.Priorita = xpObjectSpace.GetObjectByKey<Priorita>(2);//  
                    NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                    //NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                    //NuovoRdL.DataRichiesta = DateTime.Now;
                    //NuovoRdL.DataAggiornamento = DateTime.Now;
                    //NuovoRdL.DataCreazione = DateTime.Now;
                    // date
                    DateTime DataAdesso = DateTime.Now;
                    NuovoRdL.SetMemberValue("DataCreazione", DataAdesso);
                    NuovoRdL.SetMemberValue("DataRichiesta", DataAdesso);
                    NuovoRdL.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(15));
                    NuovoRdL.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(30));
                    NuovoRdL.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(16));
                    NuovoRdL.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(16));
                    NuovoRdL.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(16));
                    NuovoRdL.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));
                    NuovoRdL.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);
                    // date                    
                    string caseSwitch = View.Id;
                    int OidEdificio = 0;
                    int OidImpianto = 0;
                    int OidApparato = 0;

                    switch (caseSwitch)
                    {
                        case "ApparatoMap_DetailView":
                            AssetoMap AppMap = (AssetoMap)View.CurrentObject;
                            OidEdificio = AppMap.OidEdificio;
                            OidImpianto = AppMap.OidImpianto;
                            OidApparato = AppMap.OidApparato;
                           
                            break;
                    }
                    if (OidEdificio > 0)
                    {
                        NuovoRdL.Immobile = xpObjectSpace.GetObjectByKey<Immobile>(OidEdificio);
                        int idPriorita = 0;
                        Urgenza pr = xpObjectSpace.GetObjectsQuery<ContrattiUrgenza>()
                            .Where(w => w.Contratti.Oid == NuovoRdL.Immobile.Contratti.Oid)
                             .Where(w => w.Default == Classi.FlgAbilitato.Si)
                             .Select(s=>s.Urgenza)
                             .FirstOrDefault();
                            NuovoRdL.Urgenza = pr;

                        TipoIntervento ti = xpObjectSpace.GetObjectsQuery<ContrattoTipoIntervento>()
                        .Where(w => w.Commesse.Oid == NuovoRdL.Immobile.Contratti.Oid)
                         .Where(w => w.Default == Classi.FlgAbilitato.Si)
                         .Select(s => s.TipoIntervento)
                         .FirstOrDefault();
                        NuovoRdL.TipoIntervento = ti;

                        if (OidImpianto > 0)
                        {
                            NuovoRdL.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(OidImpianto);
                            if (OidApparato > 0)
                            {
                                NuovoRdL.Asset = xpObjectSpace.GetObjectByKey<Asset>(OidApparato);
                            }
                        }
                    }
                    var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);
                    view.Caption = string.Format("Nuova Richiesta di Lavoro");
                    view.ViewEditMode = ViewEditMode.Edit;
                      //Application.MainWindow.SetView(view);
                    e.ShowViewParameters.CreatedView = view;

                    e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                  
                     

                }
            }


        }

        #region pulsanti in lookup
        private void pupWinRicercaStrada_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //(new System.Collections.Generic.Mscorlib_CollectionDebugView<string>((((DevExpress.ExpressApp.ListView)(View)).CollectionSource).
            //   Criteria.Keys as System.Collections.Generic.List<string>)).Items[1]	"criteriaRicercaTestuale"	string
            // IPerson selectedPerson = (IPerson)View.ObjectSpace.GetObject(e.PopupWindowViewCurrentObject);
            if (View is ListView)
            {
                List<string> CaptionView = new List<string>();
                ListView lview = View as ListView;
                GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                string Filtro = string.Empty;
                //List<Strade> st = (List<Strade>);
                //for (int i = 0; i < e.PopupWindowViewSelectedObjects.Count; i++)
                //{
                //    Strade item = ((Strade)e.PopupWindowViewSelectedObjects[i]);
                //    Filtro = string.Format("[OidStrada] = {0}", item.Oid);
                //    CriteriaOperator cr = CriteriaOperator.Parse(Filtro);
                //    criteriaRicercaTestuale.Operands.Add(cr);
                //    Filtro = string.Format("Contains(Upper([StradaInProssimita]),'{0}')", item.Strada);
                //    cr = CriteriaOperator.Parse(Filtro);
                //    criteriaRicercaTestuale.Operands.Add(cr);
                //}
                foreach (var item in e.PopupWindowViewSelectedObjects.Cast<Strade>().Select(s => new { s.Oid, s.Strada }))
                {
                    string strada = item.Strada.ToString();
                    Filtro = string.Format("[OidStrada] = {0}", item.Oid);
                    CriteriaOperator cr = CriteriaOperator.Parse(Filtro);
                    criteriaRicercaTestuale.Operands.Add(cr);
                    CriteriaOperator filter = CriteriaOperator.Parse("Contains([StradaInProssimita],?)", strada);
                    criteriaRicercaTestuale.Operands.Add(filter);
                }


                (lview.CollectionSource).Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;
            }

        }

        private void pupWinRicercaStrada_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                    List<string> CaptionView = new List<string>();
                    ListView lview = View as ListView;
                    string Filtro = string.Empty;
                    GroupOperator criteriaEdificioxStrade = new GroupOperator(GroupOperatorType.And);

                    if (((ListView)View).Id.Equals("ApparatoMap_LookupListView"))
                    {
                        DetailView dvOwner = (DetailView)lview.CollectionSource.ObjectSpace.Owner;
                        RdL rdl = dvOwner.CurrentObject as RdL;
                        Servizio im = rdl.Servizio;
                        Immobile ed = rdl.Immobile;
                        CaptionView.Add(rdl.Immobile.Descrizione);
                        CriteriaOperator cr = CriteriaOperator.Parse("[Comune.Oid] = ?", ed.Indirizzo.Comuni.Oid);
                        criteriaEdificioxStrade.Operands.Add(cr);
                    }

                    string listViewId = "Strade_LookupListView";
                    CollectionSource ListStradeLookUp = (CollectionSource)
                                            Application.CreateCollectionSource(xpObjectSpace, typeof(Strade), listViewId, true, CollectionSourceMode.Normal);
                    ListStradeLookUp.Criteria["criteriaEdificioxStrade"] = criteriaEdificioxStrade;

                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Strada", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    ListStradeLookUp.Sorting.Add(srtProperty);

                    var view = Application.CreateListView(listViewId, ListStradeLookUp, false);
                    view.Caption = string.Format("{0} {1}", view.Caption, String.Join(" - ", CaptionView));
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;
                    //     ((CAMS.Module.DBTask.RdL)(((object[])(((DevExpress.ExpressApp.DetailView)(lview.CollectionSource.ObjectSpace.Owner)).SelectedObjects))[0])).Immobile
                    //                      {CAMS.Module.DBPlant.Immobile(3980)}	
                    //+		((CAMS.Module.DBTask.RdL)(((object[])(((DevExpress.ExpressApp.DetailView)(lview.CollectionSource.ObjectSpace.Owner)).SelectedObjects))[0])).Impianto	{CAMS.Module.DBPlant.Impianto(3904)}	CAMS.Module.DBPlant.Impianto

                    //GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                    //GroupOperator criteriaEdificioImpianto = new GroupOperator(GroupOperatorType.And);
                    //string DescrizioneImpianto = string.Empty;
                    //string CaptionFiltroRicerca = string.Empty;

                    //string CaptionView = string.Empty;
                    //List<string> CaptionView = new List<string>();
                    //criteriaEdificioImpianto.Operands.Clear();
                }
            }
        }

        private void pupWinRicercaTipoApparato_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                    List<string> CaptionView = new List<string>();
                    ListView lview = View as ListView;
                    DetailView dvOwner = (DetailView)lview.CollectionSource.ObjectSpace.Owner;
                    RdL rdl = dvOwner.CurrentObject as RdL;

                    Servizio im = rdl.Servizio;
                    CaptionView.Add(rdl.Servizio.Sistema.Descrizione);

                    CaptionView.Add(rdl.Servizio.Descrizione);
                    GroupOperator criteriaSistemaxTipoApparati = new GroupOperator(GroupOperatorType.And);
                    CriteriaOperator cr = CriteriaOperator.Parse("[StdApparatoClassi.Sistema.Oid] = ?", rdl.Servizio.Sistema.Oid);
                    criteriaSistemaxTipoApparati.Operands.Add(cr);

                    string listViewId = "StdApparato_LookupListView";
                    CollectionSource ListStradeLookUp = (CollectionSource)
                                            Application.CreateCollectionSource(xpObjectSpace, typeof(StdAsset), listViewId, true, CollectionSourceMode.Normal);
                    ListStradeLookUp.Criteria["criteriaEdificioxStrade"] = criteriaSistemaxTipoApparati;

                    var view = Application.CreateListView(listViewId, ListStradeLookUp, false);
                    view.Caption = string.Format("{0} {1}", view.Caption, String.Join(" - ", CaptionView));
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;

                }
            }
        }

        private void pupWinRicercaTipoApparato_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View is ListView)
            {
                List<string> CaptionView = new List<string>();
                ListView lview = View as ListView;
                GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                string Filtro = string.Empty;

                foreach (var item in e.PopupWindowViewSelectedObjects.Cast<StdAsset>().Select(s => new { s.Oid }))
                {
                    string oid = item.Oid.ToString();
                    CriteriaOperator cr = CriteriaOperator.Parse("[OidStdApparato] = ?", item.Oid);
                    criteriaRicercaTestuale.Operands.Add(cr);

                }
 
                (lview.CollectionSource).Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;
            }
        }

        #endregion

        private void pupWinRicercaTipoApparatoLV_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            string FIltroApplicato = "";
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {

                    GroupOperator GrOperatorTotClientServer = new GroupOperator(GroupOperatorType.And);
                    CriteriaOperator TotCriteria = ((ListView)View).CollectionSource.GetTotalCriteria();
                    GrOperatorTotClientServer.Operands.Add(TotCriteria);

                    int oidImpianto = 0;
                    if (((ListView)View).CollectionSource.GetCount() > 0)
                    {
                        oidImpianto = ((ListView)View).CollectionSource.List.Cast<AssetoMap>()
                                                        .Select(s => s.OidImpianto).First();
                    }

                    List<string> CaptionView = new List<string>();
                    ListView lview = View as ListView;
                    GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                    criteriaRicercaTestuale.Operands.Clear();
                    string Filtro = string.Empty;

                    foreach (var item in e.PopupWindowViewSelectedObjects.Cast<StdAsset>().Select(s => new { s.Oid }))
                    {
                        string oid = item.Oid.ToString();
                         CriteriaOperator cr = CriteriaOperator.Parse("Contains([OidStdApparato], ?)", item.Oid.ToString()); 
                         criteriaRicercaTestuale.Operands.Add(cr);
                        //------------------------------------  Filtro = string.Format("Contains(Upper([Note]),'{0}') Or
 
                        //string oid = item.Oid.ToString();
                        Filtro = string.Format("[OidStdApparato] = {0}", item.Oid);
                        Filtro = string.Format("Contains([OidStdApparato],'{0}')", oid);
                        Filtro = string.Format("Contains([PLinSostegno],'{0}')", oid);
                        //CriteriaOperator cr = CriteriaOperator.Parse(Filtro);                    ////////CriteriaOperator cr = CriteriaOperator.Parse("[OidStdApparato] = ?", item.Oid);
                        criteriaRicercaTestuale.Operands.Add(cr);

                    }
    

                    if (criteriaRicercaTestuale.Operands.Count() == 0)
                    {
                        this.SetMessaggioWeb("nessun Filtro Applicato", "Informazione", InformationType.Info);
                    }
                    else
                    {
                        this.SetMessaggioWeb("Filtro Applicato" + criteriaRicercaTestuale.ToString(), "Informazione", InformationType.Info);

                        (((ListView)View).CollectionSource).Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;
                    }
                }
            }
        }

        private void pupWinRicercaTipoApparatoLV_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                     GroupOperator criteriaSistemaxTipoApparati = new GroupOperator(GroupOperatorType.And);
                    List<string> CaptionView = new List<string>();
                    ListView lv = View as ListView;

                    if (lv.CollectionSource.GetCount() > 0)
                    {
                        int oidImpianto = lv.CollectionSource.List.Cast<AssetoMap>()
                                          .Select(s => s.OidImpianto).First();

                        Servizio imp = xpObjectSpace.GetObjectByKey<Servizio>(oidImpianto);

                        CaptionView.Add(imp.Immobile.Descrizione);

                        int OidSistema = imp.Sistema.Oid;

                        CriteriaOperator crSistema = CriteriaOperator.Parse("[StdApparatoClassi.Sistema.Oid] = ?", OidSistema);
                        criteriaSistemaxTipoApparati.Operands.Add(crSistema);

                        int[] listOid = new XPQuery<Asset>(Sess)
                       .Where(w => w.Servizio.Oid == oidImpianto)
                       .Select(s => s.StdAsset.Oid)
                       .Distinct().ToArray<int>();

                        GroupOperator criteriaTipoApparati = new GroupOperator(GroupOperatorType.Or);
                        foreach (int std in listOid)
                        {
                            CriteriaOperator cr = CriteriaOperator.Parse("[Oid] = ? ", std);
                            criteriaTipoApparati.Operands.Add(cr);
                        }
                        criteriaSistemaxTipoApparati.Operands.Add(criteriaTipoApparati);
                    }
                    else
                    {
                        DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                                            xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                                            ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);

                        FilteringCriterion NuovoFiltro = xpObjectSpace.FindObject<FilteringCriterion>(CriteriaOperator.Parse("ObjectType = ? And SecurityUser = ? And Description = ?",
                                                                                       typeof(AssetoMap), user, "Ultimo Filtro Mappa "));

                        CriteriaOperator cr = CriteriaOperator.Parse(NuovoFiltro.Criterion);

                        GroupOperator criteriaImpianto = new GroupOperator(GroupOperatorType.And);
                        criteriaImpianto.Operands.Add(NuovoFiltro.Criterion);
                        IObjectSpace xObjSpace = Application.CreateObjectSpace();

                        int contaa = xObjSpace.GetObjects<AssetoMap>(cr).Count();
                        int OidImpianto = xObjSpace.GetObjects<AssetoMap>(cr).Select(s => s.OidImpianto).First();

                        cr = CriteriaOperator.Parse("Oid = ?", OidImpianto);
                        int OidSistema = xpObjectSpace.GetObjects<Servizio>(cr).Select(s => s.Sistema.Oid).First();

                        CriteriaOperator crStdApparatoClassi = CriteriaOperator.Parse("[StdApparatoClassi.Sistema.Oid] = ?", OidSistema);
                        criteriaSistemaxTipoApparati.Operands.Add(crStdApparatoClassi);
                    }

                    string listViewId = "StdApparato_LookupListView";
                    CollectionSource ListStradeLookUp = (CollectionSource)
                                            Application.CreateCollectionSource(xpObjectSpace, typeof(StdAsset), listViewId, true, CollectionSourceMode.Normal);
                    ListStradeLookUp.Criteria["criteriaEdificioxStrade"] = criteriaSistemaxTipoApparati;

                    var view = Application.CreateListView(listViewId, ListStradeLookUp, false);
                    view.Caption = string.Format("{0} {1}", view.Caption, String.Join(" - ", CaptionView));
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = view;

                }
            }
        }

        private void pupWinRicercaStradaLV_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View is ListView)
            {

                List<string> CaptionView = new List<string>();
                ListView lview = View as ListView;
                GroupOperator criteriaRicercaTestuale = new GroupOperator(GroupOperatorType.Or);
                string Filtro = string.Empty;
                foreach (var item in e.PopupWindowViewSelectedObjects.Cast<Strade>().Select(s => new { s.Oid, s.Strada }))
                {
                    string strada = item.Strada.ToString().ToUpper();
                    Filtro = string.Format("[OidStrada] = {0}", item.Oid);
                    CriteriaOperator cr = CriteriaOperator.Parse(Filtro);
                    criteriaRicercaTestuale.Operands.Add(cr);
                    CriteriaOperator filter = CriteriaOperator.Parse("Contains(Upper([StradaInProssimita]),?)", strada);
                    criteriaRicercaTestuale.Operands.Add(filter);
                }
                (lview.CollectionSource).Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;
            }
        }

        private void pupWinRicercaStradaLV_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is ListView)
                {
                    List<string> CaptionView = new List<string>();
                    ListView lview = View as ListView;
                    string Filtro = string.Empty;
                    GroupOperator criteriaEdificioxStrade = new GroupOperator(GroupOperatorType.And);
                    if (((ListView)View).Id.Contains("ApparatoMap_ListView"))
                    {
                        ListView lv = (ListView)View;

                        if (lv.CollectionSource.GetCount() > 0)
                        {
                            int oidImpianto = lv.CollectionSource.List.Cast<AssetoMap>()
                                              .Select(s => s.OidImpianto).First();

                            Servizio imp = xpObjectSpace.GetObjectByKey<Servizio>(oidImpianto);

                            CaptionView.Add(imp.Immobile.Descrizione);
                            CriteriaOperator cr = CriteriaOperator.Parse("[Comune.Oid] = ?", imp.Immobile.Indirizzo.Comuni.Oid);
                            criteriaEdificioxStrade.Operands.Add(cr);
                        }
                        else
                        {
                            DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                                                xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                                                ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);

                            FilteringCriterion NuovoFiltro = xpObjectSpace.FindObject<FilteringCriterion>(CriteriaOperator.Parse("myObjectType = ? And SecurityUser = ? And Description = ?",
                                                                                           typeof(AssetoMap), user, "Ultimo Filtro Mappa "));

                            CriteriaOperator cr = CriteriaOperator.Parse(NuovoFiltro.Criterion);

                            int OidImpianto = xpObjectSpace.GetObjects<CAMS.Module.DBPlant.Vista.AssetoMap>(cr).Select(s => s.OidImpianto).First();

                            cr = CriteriaOperator.Parse("Oid = ?", OidImpianto);
                            int OidComuni = xpObjectSpace.GetObjects<Servizio>(cr).Select(s => s.Immobile.Indirizzo.Comuni.Oid).First();
                            CriteriaOperator cr1 = CriteriaOperator.Parse("[Comune.Oid] = ?", OidComuni);
                            criteriaEdificioxStrade.Operands.Add(cr1);
                        }

                        string listViewId = "Strade_LookupListView";
                        CollectionSource ListStradeLookUp = (CollectionSource)
                                                Application.CreateCollectionSource(xpObjectSpace, typeof(Strade), listViewId, true, CollectionSourceMode.Normal);
                        ListStradeLookUp.Criteria["criteriaEdificioxStrade"] = criteriaEdificioxStrade;

                        SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Strada", DevExpress.Xpo.DB.SortingDirection.Ascending);
                        ListStradeLookUp.Sorting.Add(srtProperty);

                        var view = Application.CreateListView(listViewId, ListStradeLookUp, false);
                        view.Caption = string.Format("{0} {1}", view.Caption, String.Join(" - ", CaptionView));
                        var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                        e.DialogController.SaveOnAccept = false;
                        e.View = view;

                    }
                }
            }

        }

        private void saCreaRdLapparatofigli_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View.Id == "ApparatoMap_ApparatiFiglis_ListView")
                {
                    if (((ListView)View).SelectedObjects.Count > 0)
                    {

                        var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                        NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                        NuovoRdL.Urgenza = xpObjectSpace.GetObjectByKey<Urgenza>(2);
                        NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                        NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                        NuovoRdL.DataRichiesta = DateTime.Now;
                        NuovoRdL.DataAggiornamento = DateTime.Now;
                        NuovoRdL.DataCreazione = DateTime.Now;
                        string caseSwitch = View.Id;
                        int OidEdificio = 0;
                        int OidImpianto = 0;
                        int OidApparato = 0;

                        switch (caseSwitch)
                        {
                            case "ApparatoMap_ApparatiFiglis_ListView":
                                //ApparatoMap AppMap = (ApparatoMap)View.CurrentObject;
                                AssetoMap AppMap = (AssetoMap)((ListView)View).SelectedObjects[0];
                                OidEdificio = AppMap.OidEdificio;
                                OidImpianto = AppMap.OidImpianto;
                                OidApparato = AppMap.OidApparato;
                                break;
                        }
                        if (OidEdificio > 0)
                        {
                            NuovoRdL.Immobile = xpObjectSpace.GetObjectByKey<Immobile>(OidEdificio);
                            if (OidImpianto > 0)
                            {
                                NuovoRdL.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(OidImpianto);
                                if (OidApparato > 0)
                                {
                                    NuovoRdL.Asset = xpObjectSpace.GetObjectByKey<Asset>(OidApparato);
                                }
                            }
                        }
                        var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);

                        view.Caption = string.Format("Nuova Richiesta di Lavoro");
                        view.ViewEditMode = ViewEditMode.Edit;
                        Frame.SetView(view);
                        e.ShowViewParameters.CreatedView = view;
                        //e.ShowViewParameters.Context
                        //e.ShowViewParameters.Context = TemplateContext.ApplicationWindow;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;

                    }
                }
            }
        }

        private void saCreaRdLbyMapAppPadre_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                    NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                    NuovoRdL.Urgenza = xpObjectSpace.GetObjectByKey<Urgenza>(2);
                    NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                    NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                    //NuovoRdL.DataRichiesta = DateTime.Now;
                    //NuovoRdL.DataAggiornamento = DateTime.Now;
                    //NuovoRdL.DataCreazione = DateTime.Now;
                    // date
                    DateTime DataAdesso = DateTime.Now;
                    NuovoRdL.SetMemberValue("DataCreazione", DataAdesso);
                    NuovoRdL.SetMemberValue("DataRichiesta", DataAdesso);
                    NuovoRdL.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(15));
                    NuovoRdL.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(30));
                    NuovoRdL.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(16));
                    NuovoRdL.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(16));
                    NuovoRdL.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(16));
                    NuovoRdL.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));
                    NuovoRdL.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);
                    // date

                    string caseSwitch = View.Id;
                    int OidEdificio = 0;
                    int OidImpianto = 0;
                    int OidApparato = 0;

                    switch (caseSwitch)
                    {
                        case "ApparatoMap_DetailView":
                            AssetoMap AppMap = (AssetoMap)View.CurrentObject;
                            OidEdificio = AppMap.OidEdificio;
                            OidImpianto = AppMap.OidImpianto;
                            OidApparato = AppMap.OidApparatoPadre;
                            break;
                    }
                    if (OidEdificio > 0)
                    {
                        NuovoRdL.Immobile = xpObjectSpace.GetObjectByKey<Immobile>(OidEdificio);
                        if (OidImpianto > 0)
                        {
                            NuovoRdL.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(OidImpianto);
                            if (OidApparato > 0)
                            {
                                NuovoRdL.Asset = xpObjectSpace.GetObjectByKey<Asset>(OidApparato);
                            }
                        }
                    }
                    var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);
                    view.Caption = string.Format("Nuova Richiesta di Lavoro");
                    view.ViewEditMode = ViewEditMode.Edit;
                    e.ShowViewParameters.CreatedView = view;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                    //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;
                }
            }
        }

        private void saCreaRdLbyMapAppSostegno_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                    NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                    NuovoRdL.Urgenza = xpObjectSpace.GetObjectByKey<Urgenza>(2);
                    NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                    NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                    NuovoRdL.DataRichiesta = DateTime.Now;
                    NuovoRdL.DataAggiornamento = DateTime.Now;
                    NuovoRdL.DataCreazione = DateTime.Now;
                    string caseSwitch = View.Id;
                    int OidEdificio = 0;
                    int OidImpianto = 0;
                    int OidApparato = 0;

                    switch (caseSwitch)
                    {
                        case "ApparatoMap_DetailView":
                            AssetoMap AppMap = (AssetoMap)View.CurrentObject;
                            OidEdificio = AppMap.OidEdificio;
                            OidImpianto = AppMap.OidImpianto;
                            OidApparato = AppMap.OidApparatoSostegno;
                            break;
                    }
                    if (OidEdificio > 0)
                    {
                        NuovoRdL.Immobile = xpObjectSpace.GetObjectByKey<Immobile>(OidEdificio);
                        if (OidImpianto > 0)
                        {
                            NuovoRdL.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(OidImpianto);
                            if (OidApparato > 0)
                            {
                                NuovoRdL.Asset = xpObjectSpace.GetObjectByKey<Asset>(OidApparato);
                            }
                        }
                    }
                    var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);
                    view.Caption = string.Format("Nuova Richiesta di Lavoro");
                    view.ViewEditMode = ViewEditMode.Edit;

                    e.ShowViewParameters.CreatedView = view;
                    //e.ShowViewParameters.Context
                    //e.ShowViewParameters.Context = TemplateContext.ApplicationWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Current;

                }
            }
        }





        private void saApparatiinSostegno_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    List<string> CaptionView = new List<string>();
                    AssetoMap AppMap = (AssetoMap)View.CurrentObject;
                    int OidApparato = AppMap.OidApparato;
                    CaptionView.Add(AppMap.Descrizione);
                    string listViewId = "Apparato_LookupListView_ListSostegno";

                    GroupOperator criteriaApparatiFigli = new GroupOperator(GroupOperatorType.And);
                    CriteriaOperator cr = CriteriaOperator.Parse("[ApparatoSostegno.Oid] = ?", OidApparato);
                    criteriaApparatiFigli.Operands.Add(cr);

                    CollectionSource ListApparatoSostegnoLookUp = (CollectionSource)
                                         Application.CreateCollectionSource(xpObjectSpace, typeof(Asset), listViewId, true, CollectionSourceMode.Normal);
                    ListApparatoSostegnoLookUp.Criteria["criteriaApparatoSostegno"] = criteriaApparatiFigli;

                    var viewLookUp = Application.CreateListView(listViewId, ListApparatoSostegnoLookUp, false);
                    viewLookUp.Caption = string.Format("{0} {1}", viewLookUp.Caption, String.Join(" - ", CaptionView));

                    e.ShowViewParameters.CreatedView = viewLookUp;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow;
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;

                    DialogController dc = Application.CreateController<DialogController>();
                    dc.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
                    e.ShowViewParameters.Controllers.Add(dc);

                }
            }
        }

        void dc_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            DevExpress.Xpo.Session Sess = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)xpObjectSpace).Session;

            //String TitoloMessaggio = string.Empty;
            //int conta = e.AcceptActionArgs.SelectedObjects.Count;//(e.PopupWindowViewSelectedObjects.Count > 0).Select(s => new { s.Oid })
            if (e.AcceptActionArgs.SelectedObjects.Count > 0)
            {
                Asset AppMapSuSupporto = e.AcceptActionArgs.SelectedObjects.Cast<Asset>().First();

                var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                NuovoRdL.Urgenza = xpObjectSpace.GetObjectByKey<Urgenza>(2);
                NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);

                NuovoRdL.Asset = xpObjectSpace.GetObject<Asset>(AppMapSuSupporto);
                NuovoRdL.Servizio = xpObjectSpace.GetObject<Servizio>(AppMapSuSupporto.Servizio);
                NuovoRdL.Immobile = xpObjectSpace.GetObject<Immobile>(AppMapSuSupporto.Servizio.Immobile);

                NuovoRdL.TipologiaSpedizione = CAMS.Module.Classi.TipologiaSpedizioneRdL.No;
                NuovoRdL.Soddisfazione = Classi.Soddisfazione.INDIFFERENTE;

                DateTime DataAdesso = DateTime.Now;
                NuovoRdL.SetMemberValue("DataCreazione", DataAdesso);
                NuovoRdL.SetMemberValue("DataRichiesta", DataAdesso);
                NuovoRdL.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(15));
                NuovoRdL.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(30));
                NuovoRdL.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(16));
                NuovoRdL.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(16));
                NuovoRdL.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(16));
                NuovoRdL.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));
                NuovoRdL.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);

                var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);
                view.Caption = string.Format("Nuova Richiesta di Lavoro");
                view.ViewEditMode = ViewEditMode.Edit;
                e.ShowViewParameters.CreatedView = view;
                e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                //Application.MainWindow.SetView(view);
            }


        }

        private void saVediPadre_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    List<string> CaptionView = new List<string>();
                    AssetoMap AppMap = (AssetoMap)View.CurrentObject;
                    
                    CaptionView.Add(AppMap.AppPadreDescrizione);
                    string listViewId = "ApparatoMap_DetailView";

                    if (AppMap.OidApparatoPadre.ToString() == "0")
                    {
                        SetMessaggioWeb("non ci sono Apparati da Visualizzare", "Informazione", InformationType.Warning);
                    }
                    else
                    {
                        //int oidmapapparatopadre =  xpObjectSpace.GetObjectsQuery.<ApparatoMap>()
                        //    .Where(x => x.OidApparato == AppMap.OidApparatoPadre)
                        //    .Select(x => x.Oid);


                        int oidapparatomappadre = xpObjectSpace.GetObjectsQuery<AssetoMap>()
                        .Where(w => w.OidApparato == AppMap.OidApparatoPadre)                         
                        .Select(s => s.Oid)
                        .FirstOrDefault();

                        AssetoMap AppMapPadre = xpObjectSpace.GetObjectByKey<AssetoMap>(oidapparatomappadre);
                        //ApparatoMap AppMapPadre = xpObjectSpace.GetObjectByKey<ApparatoMap>(AppMap.OidApparatoPadre.ToString());
                        var view_DetailView = Application.CreateDetailView(xpObjectSpace, listViewId, true, AppMapPadre);
                        view_DetailView.Caption = string.Format("{0} {1}", view_DetailView.Caption, String.Join(" - ", CaptionView));

                        e.ShowViewParameters.CreatedView = view_DetailView;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.ShowViewParameters.Context = TemplateContext.View;
                    }

                }
            }
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

        private void paRicercaGoogleMappa_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {

        }
    }
}


//  var ArOidStrade = xpObjectSpace.GetObjects<CAMS.Module.DBPlant.Vista.ApparatoMap>(criteriaIMPIANTO)
// .Where(w => w.OidStrada != null)
//.Select(s => new { s.OidStrada, s.Strada_Descrizione })
//.Distinct().ToList();

//  int contaArOidStrade = ArOidStrade.Count;
//  if (contaArOidStrade > 0)
//  {
//      ChoiceActionItem itemStrade = new ChoiceActionItem() { Data = 1, Caption = "Fitro x Strada" };
//      //scaFilterApparatoMap.Items.Add((new ChoiceActionItem() { Data = 1, Caption = "Fitro x Strada" }));
//      for (var i = 0; i < ArOidStrade.Count; i++)
//      {
//          string DataFilter = string.Format("[OidStrada] In ({0}) And {1}", ArOidStrade[i].OidStrada, criteriaIMPIANTO.ToString());//.OidApparatoPadre);
//          itemStrade.Items.Add((new ChoiceActionItem()
//          {
//              Id = i.ToString(),
//              Caption = ArOidStrade[i].Strada_Descrizione,//
//              Data = DataFilter
//          }));
//          if (i == 1)
//          {
//              GrOperator.Operands.Clear();
//              CriteriaOperator cr = CriteriaOperator.Parse(DataFilter);
//              GrOperator.Operands.Add(cr);
//          }
//      }
//      scaFilterApparatoMap.Items.Add(itemStrade);
//  }