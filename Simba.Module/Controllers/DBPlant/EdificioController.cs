using CAMS.Module.Classi;
using CAMS.Module.DBAux;
using CAMS.Module.DBPlant;
using CAMS.Module.DBPlant.Vista;
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

namespace CAMS.Module.Controllers
{
    public partial class EdificioController : ViewController
    {
        private const string NuovoEdificio_DetailView = "NuovoEdificio_DetailView";
        bool TastoVisible = false;
        public EdificioController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated(); // 10.25.42.182. porta 800

            //EdificioClona
            using (UtilController uc = new UtilController())
            {
                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                if (uc.GetIsGrantedCreate(xpObjectSpace, "Immobile", "C"))
                    TastoVisible = true;
            }
            this.EdificioClona.Active.SetItemValue("Active", TastoVisible);

            this.acAggiornaValoriEdificio.Active.SetItemValue("Active", TastoVisible);
           


            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                Immobile e = dv.CurrentObject as Immobile;
                if (dv.ViewEditMode == ViewEditMode.Edit)
                {
                    this.acAggiornaValoriEdificio.Active.SetItemValue("Active", true);
                    this.acInserisciDocumenti.Active.SetItemValue("Active", false);
                }
                else
                {
                    this.acAggiornaValoriEdificio.Active.SetItemValue("Active", false);
                    this.acInserisciDocumenti.Active.SetItemValue("Active", false);
                }

                scaImpiantoMappa.Items.Clear();

                IObjectSpace xpObjectSpace = Application.CreateObjectSpace();


                GroupOperator criteriaOR = new GroupOperator(GroupOperatorType.Or);

//                select count(*)
// FROM[EAMS_OL_SL].[dbo].APPARATO_MAP s
//where s.OIDAPPARATOSTDCLASSI in(257, 236, 255, 237)
                var listImpiantoApparatoMap = xpObjectSpace.GetObjectsQuery<AssetoMap>()
                  .Where(w => w.OidEdificio == e.Oid)
                   .Where(w => new int[] { 257, 236, 255, 237 }.Contains( w.OidStdApparatoClassi))
                  //.GroupBy(x => new {s.Oid, s.Descrizione})
                  .Select(s => new { codiceImpianto = s.OidImpianto, desc = s.Impianto_Descrizione })
                  .Distinct();

                GroupOperator GroupAnd = new GroupOperator(GroupOperatorType.And);
                foreach (var i in listImpiantoApparatoMap)
                {
                    CriteriaOperator op = CriteriaOperator.Parse("OidImpianto == ?", i.codiceImpianto);
                    string DataFilter = op.ToString();

                    ChoiceActionItem cai = new ChoiceActionItem()
                       {
                           Id = i.codiceImpianto.ToString(),
                           Caption = i.desc,//
                           Data = DataFilter
                       };
                    var listtipoApparatoMap = xpObjectSpace.GetObjectsQuery<AssetoMap>()
                  .Where(w => w.OidEdificio == e.Oid && w.OidImpianto == i.codiceImpianto)
                  .Select(s => new { oidstdcl = s.OidStdApparatoClassi, desc = s.StdApparatoClassiDescrizione })
                  .Distinct();
                    foreach (var elem in listtipoApparatoMap)
                    {
                        GroupAnd.Operands.Clear();
                        GroupAnd.Operands.Add(op);
                        CriteriaOperator opss = CriteriaOperator.Parse("OidStdApparatoClassi == ?", elem.oidstdcl);
                        GroupAnd.Operands.Add(opss);
                        cai.Items.Add(new ChoiceActionItem()
                        {
                            Id = elem.oidstdcl.ToString(),
                            Caption = elem.desc,//
                            Data = GroupAnd.ToString()
                        });
                    }
                    scaImpiantoMappa.Items.Add(cai);
                }
            }
        }


        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            this.acAggiornaValoriEdificio.Active.SetItemValue("Active", false);
            if (View is ListView)
            {
                //if (!View.Id.ToString().Contains("ClusterEdifici_Edificis_ListView"))
                //{
                //    Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                //    Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
                //}
                if (CAMS.Module.Classi.SetVarSessione.IsAdminRuolo)
                {
                    this.acAggiornaValoriEdificio.Active.SetItemValue("Active", true);
                }
            }
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                if (dv.ViewEditMode == ViewEditMode.Edit)
                {
                    this.acAggiornaValoriEdificio.Active.SetItemValue("Active", true);
                }
            }
        }

        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        //private void EdificioController_ViewControlsCreated(object sender, EventArgs e)
        //{
        //}

        private bool needClearSelection;

        private void EdificioClona_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                var NuovoEd = xpObjectSpace.CreateObject<NuovoEdificio>();
                var EdificioSelezionato = xpObjectSpace.GetObject((((ListView)View).Editor).GetSelectedObjects().Cast<Immobile>().First());

                NuovoEd.Indirizzo = EdificioSelezionato.Indirizzo;
                NuovoEd.Descrizione = "copia " + EdificioSelezionato.Descrizione;
                NuovoEd.CodDescrizione = EdificioSelezionato.Contratti.CodDescrizione + "_";
                //  NuovoEd.NumeroCopie = 0;
                NuovoEd.VecchioEdificio = EdificioSelezionato;

                var view = Application.CreateDetailView(xpObjectSpace, "NuovoEdificio_DetailView", true, xpObjectSpace.GetObject(NuovoEd));
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }
        private void EdificioClona_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var NuovoEd = ((NuovoEdificio)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));
            if (!NuovoEd.VerificaUnivocita && !string.IsNullOrEmpty(NuovoEd.Descrizione) && !string.IsNullOrEmpty(NuovoEd.CodDescrizione))
            {
                var xpObjectSpace = Application.CreateObjectSpace();
                int OidNewEdificio = 0;
                using (Classi.DB db = new Classi.DB())
                {
                    // var db = new Classi.DB();
                    db.CloneEdificio(Classi.SetVarSessione.CorrenteUser, NuovoEd.VecchioEdificio.Oid, NuovoEd.CodDescrizione, NuovoEd.Descrizione, NuovoEd.Indirizzo.Oid, ref   OidNewEdificio);
                    NuovoEd.NewEdificio = NuovoEd.Session.GetObjectByKey<Immobile>(OidNewEdificio);
                }


                if (xpObjectSpace != null)
                {
                    string Messaggio = "269739";
                    try
                    {
                        var ed = xpObjectSpace.GetObjectByKey<Immobile>(OidNewEdificio);
                        Messaggio = string.Format("Immobile Creato:\r\n Descrizione:{0} \r\n Codice Descrizione:{1}", ed.Descrizione, ed.CodDescrizione);
                    }
                    catch
                    {
                        Messaggio = string.Format("Immobile non Creato");
                    }

                    var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                    Mess.Messaggio = Messaggio;
                    var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
                    view.ViewEditMode = ViewEditMode.View;
                    e.ShowViewParameters.CreatedView = view;
                    view.Caption = view.Caption + " - Esito Clonazione Immobile";
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                    e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;

                }

            }
            RefreshDati();
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

        private void acAggiornaValoriEdificio_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {

                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    //Classi.UtenteRuolo ur = new Classi.UtenteRuolo();

                    if (dv.ViewEditMode == ViewEditMode.Edit)
                    {
                        Immobile Immibile = (Immobile)dv.CurrentObject;
                        if (Immibile != null)
                        {
                            using (var db = new CAMS.Module.Classi.DB())
                            {
                                db.AggiornaTempi(Immibile.Oid, "IMMOBILE");
                            }

                            View.ObjectSpace.Refresh();
                        }
                    }
                }
                if (View is ListView)
                    if ((((ListView)View).Editor).GetSelectedObjects().Count > 0)
                    {
                        List<int> lstEdificioSel = (((ListView)View).Editor).GetSelectedObjects().Cast<Immobile>().Select(s => s.Oid).ToList();
                        var utente = Application.Security.User;
                        foreach (int oid in lstEdificioSel)
                        {
                            try
                            {
                                using (var db = new CAMS.Module.Classi.DB())
                                {
                                    db.AggiornaTempi(oid, "IMMOBILE");
                                }
                            }
                            catch (Exception ex)
                            {
                                throw new Exception(ex.ToString());
                            }
                            View.ObjectSpace.Refresh();
                        }
                    }

            }
        }

        private void acInserisciDocumenti_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    Immobile EdDoc = xpObjectSpace.GetObject<Immobile>((Immobile)dv.CurrentObject);
                    if (EdDoc != null)
                    {
                        var view = Application.CreateDetailView(xpObjectSpace, "Edificio_DetailView_Documenti", true, EdDoc);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.ShowViewParameters.CreatedView = view;
                        view.Caption = view.Caption + " - Inserimento Documenti";
                        e.ShowViewParameters.Context = TemplateContext.View;
                        e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                        e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }
                }
            }
        }

        private void scaImpiantoMappa_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            List<string> CaptionView = new List<string>();
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    DetailView dv = (DetailView)View;
                    //int Oidk = int.Parse(e.SelectedChoiceActionItem.Data.ToString());
                    //Impianto Imp = xpObjectSpace.GetObjectByKey<Impianto>(Oidk);
                    //CaptionView.Add(Imp.Descrizione);
                    CaptionView.Add(e.SelectedChoiceActionItem.Caption.ToString());
                    //if (Imp != null)
                    //{
                    string listViewId = "ApparatoMap_ListView"; //ApparatoMap_ListView

                    string filtroImpianto = string.Empty;
                    if (e.SelectedChoiceActionItem.ParentItem != null)
                        filtroImpianto = e.SelectedChoiceActionItem.ParentItem.Data.ToString();
                    else
                        filtroImpianto = e.SelectedChoiceActionItem.Data.ToString();

                    CriteriaOperator criteriaEdificioImpianto = CriteriaOperator.Parse(filtroImpianto);

                    string filtro = e.SelectedChoiceActionItem.Data.ToString();
                    CriteriaOperator criteriaRicercaTestuale = CriteriaOperator.Parse(filtro);

                    CollectionSource ListApparatiMapLV = (CollectionSource)
                    Application.CreateCollectionSource(xpObjectSpace, typeof(AssetoMap), listViewId, false, CollectionSourceMode.Normal);
                    ListApparatiMapLV.Criteria["criteriaEdificioImpianto"] = criteriaEdificioImpianto;

                    ListApparatiMapLV.Criteria["criteriaRicercaTestuale"] = criteriaRicercaTestuale;


                    var lv = Application.CreateListView(listViewId, ListApparatiMapLV, true);
                    lv.Caption = string.Format("{0} {1}", lv.Caption, String.Join(" - ", CaptionView));
                    var dc = Application.CreateController<DevExpress.ExpressApp.SystemModule.DialogController>();
                    #region   creo filtro ultimo di mappa
                    GroupOperator GrOperator = new GroupOperator(GroupOperatorType.And);
                    GrOperator.Operands.Add(criteriaRicercaTestuale);
                    DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                                                                    xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                                                                    ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);
                    FilteringCriterion NuovoFiltro = xpObjectSpace.FindObject<FilteringCriterion>
                        (CriteriaOperator.Parse("myObjectType = ? And SecurityUser = ? And Description = ?",
                                                                                   typeof(AssetoMap), user, "Ultimo Filtro Mappa "));
                    if (NuovoFiltro == null)
                        NuovoFiltro = xpObjectSpace.CreateObject<FilteringCriterion>();

                    NuovoFiltro.Description = string.Format("Ultimo Filtro Mappa ");
                    NuovoFiltro.myObjectType = typeof(AssetoMap);
                    NuovoFiltro.Criterion = GrOperator.ToString();
                    NuovoFiltro.SecurityUser = user;
                    NuovoFiltro.Save();
                    xpObjectSpace.CommitChanges();
                    #endregion
                    e.ShowViewParameters.CreatedView = lv;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                    e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;
                }
            }
        }

        private void popWinInsertEdificioinCluster_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

            if (View.ObjectSpace != null)
            {
                if (View is ListView)
                {
                    ListView LvFiglio = (ListView)View;
                    if (LvFiglio.Id == "ClusterEdifici_Edificis_ListView")
                    {
                        DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id.Contains("ClusterEdifici_DetailView")) // ClusterEdifici_DetailView
                        {
                            try
                            {

                                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                                IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
                                ClusterEdifici vClusterEdifici = (ClusterEdifici)dvParent.CurrentObject;
                                List<Immobile> List = e.PopupWindow.View.SelectedObjects.Cast<Immobile>().ToList<Immobile>();

                                foreach (Immobile objED in List)
                                {
                                    vClusterEdifici.Edificis.Add(objED);
                                    //contaimpianti = new XPQuery<Impianto>(Sess).Where(w => w.Immobile.Oid == objED.Oid).Count();
                                    //contaiapparato = new XPQuery<Apparato>(Sess).Where(w => w.Impianto.Immobile.Oid == objED.Oid).Count();
                                }
                                //vApparato.InsertSchedeMPsuApparato(ref vApparato, null, 0, List);
                                //vApparato.Save();    PersistentAlias("Edificis.Sum(Impianti.Sum(APPARATOes.Count))
                                vClusterEdifici.Save();
                                View.ObjectSpace.CommitChanges();
                                vClusterEdifici = xpObjectSpace.GetObjectByKey<ClusterEdifici>(vClusterEdifici.Oid);
                                List<Immobile> Edificii = vClusterEdifici.Edificis.ToList();
                                vClusterEdifici.ContaEdifici = Edificii.Count();
                                vClusterEdifici.ContaImpianti = Edificii.SelectMany(i => i.Impianti).Count();
                                vClusterEdifici.ContaApparati = Edificii.Sum(s => s.Impianti.Sum(d => d.APPARATOes.Count()));
                                vClusterEdifici.SommaTempo = Edificii.Sum(s => s.EdificioMansioneCaricos.Sum(d => d.Carico));
                                vClusterEdifici.Save();
                                View.ObjectSpace.CommitChanges();
                                //}     //LogTrace(string.Format("The 'PopupWindowShowAction' is executed with {0} parameter(s) for the '{1}' object. {2}", e.PopupWindow.View.SelectedObjects.Count, currentObject.Name, string.IsNullOrEmpty(parameters) ? "" : "\r\n\t\t" + parameters));
                            }
                            catch (Exception ex)
                            {
                                string Messaggio = ex.Message.ToString();
                                MessageOptions options = new MessageOptions();
                                options.Duration = 3000;
                                options.Message = Messaggio.ToString();
                                options.Web.Position = InformationPosition.Top;
                                options.Type = InformationType.Success;
                                options.Win.Caption = "Caption";
                                //options.CancelDelegate = CancelDelegate;
                                //options.OkDelegate = OkDelegate;
                                Application.ShowViewStrategy.ShowMessage(options);
                            }
                        }
                    }
                }
            }
        }

        private void popWinInsertEdificioinCluster_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View.ObjectSpace != null)
            {
                if (View is ListView)
                {
                    ListView LvFiglio = (ListView)View;
                    if (LvFiglio.Id == "ClusterEdifici_Edificis_ListView")
                    {
                        DetailView dvParent = (DetailView)(View.ObjectSpace).Owner;
                        if (dvParent.Id.Contains("ClusterEdifici_DetailView"))// && dvParent.ViewEditMode == ViewEditMode.Edit)ApparatoSchedaMP
                        {
                            try
                            {
                                int vCentroOperativoAreaDiPoloOid = ((ClusterEdifici)dvParent.CurrentObject).CentroOperativo.AreaDiPolo.Oid;
                                int vCentroOperativoOid = ((ClusterEdifici)dvParent.CurrentObject).CentroOperativo.Oid;
                                // Application.FindListViewId(typeof(ClusterEdificiInseribili));xpObjectSpace
                                CollectionSource LstEdificisSelezionabili = (CollectionSource)Application.CreateCollectionSource(
                                                                            View.ObjectSpace, typeof(Immobile), "Edificio_LookupListView");

                                GroupOperator criteriaEdificis = new GroupOperator(GroupOperatorType.And);
                                //CriteriaOperator cr = CriteriaOperator.Parse("Commesse.AreaDiPolo.Oid = ?", vCentroOperativoAreaDiPoloOid);
                                CriteriaOperator cr = CriteriaOperator.Parse("CentroOperativoBase.Oid = ?", vCentroOperativoOid);
                                criteriaEdificis.Operands.Add(cr);
                                cr = CriteriaOperator.Parse("ClusterEdifici is null");
                                criteriaEdificis.Operands.Add(cr);

                                LstEdificisSelezionabili.Criteria["Filtro_ClusterEdifici"] = criteriaEdificis;

                                int[] arOidEdificis = (((ClusterEdifici)dvParent.CurrentObject)).Edificis.Select(s => s.Oid).ToArray();
                                if (arOidEdificis.Count() > 0)
                                    LstEdificisSelezionabili.Criteria["Filtro_GiaPresenti"] = new InOperator("Oid", arOidEdificis).Not();

                                if (LstEdificisSelezionabili.GetCount() == 0)
                                {
                                    string Messaggio = string.Format("Non ci sono edifici da Inserire oppure sono già presenti tutti gli edifici Associabili");
                                    MessageOptions options = new MessageOptions();
                                    options.Duration = 3000;
                                    options.Message = Messaggio.ToString();
                                    options.Web.Position = InformationPosition.Top;
                                    options.Type = InformationType.Success;
                                    options.Win.Caption = "Caption";
                                    //options.CancelDelegate = CancelDelegate;                                    //options.OkDelegate = OkDelegate;
                                    Application.ShowViewStrategy.ShowMessage(options);
                                }
                                var view = Application.CreateListView("Edificio_LookupListView", LstEdificisSelezionabili, false);
                                var dc = Application.CreateController<DialogController>();
                                e.DialogController.SaveOnAccept = false;
                                e.View = view;
                            }
                            catch
                            {
                                string Messaggio = string.Format("edifici non Impostato!! non è possibile eseguire l'operazione");
                                MessageOptions options = new MessageOptions();
                                options.Duration = 3000;
                                options.Message = Messaggio.ToString();
                                options.Web.Position = InformationPosition.Top;
                                options.Type = InformationType.Success;
                                options.Win.Caption = "Caption";
                                //options.CancelDelegate = CancelDelegate;
                                //options.OkDelegate = OkDelegate;
                                Application.ShowViewStrategy.ShowMessage(options);
                            }

                        }
                    }
                }
            }
        }

    }
}
