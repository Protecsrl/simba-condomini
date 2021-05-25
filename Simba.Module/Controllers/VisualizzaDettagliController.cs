using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.Xpo;
using System;
using System.Linq;
using CAMS.Module.DBAudit.DC;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;
using System.Collections;
using System.ComponentModel;

namespace CAMS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class VisualizzaDettagliController : ViewController
    {
        public VisualizzaDettagliController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            


            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            // Perform various tasks depending on the target View.
            bool VisualizzaTasto = false;
            if (View is DetailView || View is ListView)
            {
                acVisualizzaDettagliAll.Items.Clear();
                string filtro = string.Format("Immobile.Oid = {0}", "0");
                string caseSwitch = View.Id;

                switch (caseSwitch)
                {
                    case "Scenario_ListView":
                    case "Scenario_DetailView":
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "Edificio_ListView", Data = "ClusterEdifici.Scenario.Oid ", Caption = "Edifici Relativi" }));
                        VisualizzaTasto = true;
                        break;
                    case "ClusterEdifici_ListView":
                    case "ClusterEdifici_DetailView":
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "Edificio_ListView", Data = "ClusterEdifici.Oid ", Caption = "Edifici Relativi" }));
                        VisualizzaTasto = true;
                        break;
                    case "Edificio_ListView":
                    case "Edificio_DetailView":
                        filtro = string.Format("Immobile.Oid ");
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "Impianto_ListView", Data = "Immobile.Oid ", Caption = "Impianti Relativi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "Apparato_ListView_VisualizzaDettagli", Data = "Impianto.Immobile.Oid ", Caption = "Apparati Relativi" }));

                        //acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ApparatoMap_ListView", Data = "OidEdificio", Caption = "Apparati Relativi in Mappa" }));

                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ApparatoSchedaMP_ListView", Data = "Apparato.Impianto.Immobile.Oid ", Caption = "Attivita MP Relative" }));
                       
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_ListView", Data = "Immobile.Oid ", Caption = "RdL Relative" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_RegNEmergenzes_ListView_Dettaglio", Data = "RdL.Immobile.Oid ", Caption = "RdL Emergenze" }));
                        //acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "KPIMTBFGuasti_ListView_Variant", Data = "Immobile.Oid ", Caption = "Statistiche Guasto" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RegistroCosti_ListView_Dettaglio", Data = "Immobile.Oid ", Caption = "Registro Costi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ControlliNormativi_ListView_Sfoglia", Data = "Immobile.Oid ", Caption = "Controlli Normativi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RegMisure_ListView", Data = "Immobile.Oid ", Caption = "Registro Misure" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "NavigazioneDocumenti_ListView_Dettaglio", Data = "Immobile.Oid ", Caption = "Documenti" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "EdificioMansioneCarico_ListView", Data = "Immobile.Oid ", Caption = "Carico Mansioni Immobile" }));
                        //EdificioMansioneCarico_ListView

                        VisualizzaTasto = true;
                        break;
                    case "Impianto_ListView":
                    case "Impianto_DetailView":
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "Apparato_ListView_VisualizzaDettagli", Data = "Impianto.Oid ", Caption = "Apparati Relativi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ApparatoSchedaMP_ListView", Data = "Apparato.Impianto.Oid ", Caption = "Attivita MP Relative" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_ListView", Data = "Impianto.Oid ", Caption = "RdL Relative" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_RegNEmergenzes_ListView_Dettaglio", Data = "RdL.Impianto.Oid ", Caption = "RdL Emergenze" }));
                        //acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "KPIMTBFGuasti_ListView_Variant", Data = "Impianto.Oid ", Caption = "Statistiche Guasto" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RegistroCosti_ListView", Data = "Immobile.Oid ", Caption = "Registro Costi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ControlliNormativi_ListView_Sfoglia", Data = "Impianto.Oid ", Caption = "Controlli Normativi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RegMisure_ListView", Data = "Impianto.Oid ", Caption = "Registro Misure" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "NavigazioneDocumenti_ListView_Dettaglio", Data = "Impianto.Oid ", Caption = "Documenti" }));
                        VisualizzaTasto = true;
                        break;                    
                    case "Apparato_ListView":
                    case "Apparato_DetailView":
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ApparatoSchedaMP_ListView", Data = "Apparato.Oid ", Caption = "Attivita MP Relative" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_ListView", Data = "Apparato.Oid ", Caption = "RdL Relative" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_RegNEmergenzes_ListView_Dettaglio", Data = "RdL.Apparato.Oid ", Caption = "RdL Emergenze" }));
                        //acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "KPIMTBFGuasti_ListView_Variant", Data = "Apparato.Oid ", Caption = "Statistiche Guasto" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RegistroCosti_ListView_Dettaglio", Data = "Impianto.Oid ", Caption = "Registro Costi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "ControlliNormativi_ListView_Sfoglia", Data = "Apparato.Oid ", Caption = "Controlli Normativi" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RegMisureDettaglio_ListView", Data = "Apparato.Oid ", Caption = "Registro Misure" }));
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "NavigazioneDocumenti_ListView_Dettaglio", Data = "Apparato.Oid ", Caption = "Documenti" }));
                        VisualizzaTasto = true;
                        break;
                    case "ApparatoSchedaMP_ListView":
                    case "ApparatoSchedaMP_DetailView":
                        filtro = "[<MpAttivitaPianificateDett>][^.Oid = RdL And MpAttPianificate.ApparatoSchedaMP.Oid {0}].Count() > 0  Or [<RdLApparatoSchedeMP>][^.Oid = RdL And ApparatoSchedaMP.Oid {0}].Count() > 0 ";
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_ListView", Data = filtro, Caption = "RdL Relative" }));
                        VisualizzaTasto = true;
                        //f  [<RdLApparatoSchedeMP>][^.Oid = RdL And ApparatoSchedaMP.Oid In(6227,14678,14566,14573) {0}
                        //acViewMap.Items.Add((new ChoiceActionItem() { Id = "Indirizzo_ListView_Cluster_Map", Data = filtro, Caption = "Edifici in Cluster" + CEd.Descrizione }));
                        break;
                    case "Risorse_ListView":
                   // case "Risorse_DetailView":
                    case "Risorse_ListView_Gestione":
                    case "Risorse_DetailView":
                        filtro = "RisorseTeam.RisorsaCapo.Oid "; 
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_ListView", Data = filtro, Caption = "RdL Relative" }));
                      
                              filtro = "Risorse.Oid ";
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RisorsaOrariGionalieri_ListView", Data = filtro, Caption = "Connessione Giornaliera Risorsa" }));


                        //filtro = "OidRisorsa ";
                        //      acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "DCRegistroOperativoRisorsa_ListView", Data = filtro, Caption = "Timesheet Risorsa" }));


                        // VisualizzaTasto = true;  DCRegistroOperativoRisorsa
                       //filtro = "RisorseTeam.RisorsaCapo.Oid ";
                       //acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "KPIStatoOperativo_ListView_Variant", Data = filtro, Caption = "kpi Stato Operativo" }));
                       
                       //    filtro = "RisorseTeam.RisorsaCapo.Oid ";
                       //    acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "KPIStatoSmistamento_ListView_Variant", Data = filtro, Caption = "kpi Stato Smistamento" }));
                       
                       //    filtro = "RisorseTeam.RisorsaCapo.Oid ";
                       //    acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "kpiStatoSmistamentoOperativo_ListView_Variant", Data = filtro, Caption = "kpi Stato Smistamento e Operativo" }));
                       

                       VisualizzaTasto = true;
                        break;
                    case "RisorseTeam_ListView":
                    case "RisorseTeam_DetailView":
                    case "RisorseTeam_ListView_Gestione":
                    case "RisorseTeam_DetailView_Gestione":
                        filtro = "RisorseTeam.Oid ";
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RdL_ListView_Dettaglio", Data = filtro, Caption = "RdL Relative" }));

                        filtro = "Risorse.Oid == [RisorsaCapo]"; // [<RisorseTeam>][Oid {0}].Single(RisorsaCapo.Oid) =      
                        acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "RisorsaOrariGionalieri_ListView", Data = filtro, Caption = "Connessione Giornaliera Risorsa" }));

                        //filtro = "OidRisorsa == [RisorsaCapo]";
                        // acVisualizzaDettagliAll.Items.Add((new ChoiceActionItem() { Id = "DCRegistroOperativoRisorsa_ListView", Data = filtro, Caption = "Timesheet Risorsa" }));

                        VisualizzaTasto = true;
                        break;

                    //case "RdL_DetailView":   DCRegistroOperativoRisorsa
                    //case "RdL_DetailView_Gestione":

                    //    break;
                    //default:  "[<Immobile>][^.Oid = Indirizzo And ClusterEdifici.Oid = {0}].Count() > 0"
                    //    break;
                }
            }
            acVisualizzaDettagliAll.Active.SetItemValue("Active", VisualizzaTasto);

        }

        protected void constructorsCreateExpression()
        {
            // Using constructors
            CriteriaOperator op1 = new BinaryOperator(CreateExpression("^.Age"), CreateExpression("Age"), BinaryOperatorType.Equal);
            op1 = new JoinOperand("Person", op1, Aggregate.Exists, new OperandProperty("Age"));
            op1 = new BinaryOperator(op1, new OperandProperty("Age"), BinaryOperatorType.Equal);

        }
        private static CriteriaOperator CreateExpression(string propertyName)
        {
            CriteriaOperator op = new BinaryOperator(propertyName, 10, BinaryOperatorType.Divide);
            op = new FunctionOperator(FunctionOperatorType.Floor, op);
            return new BinaryOperator(op, new OperandValue(10), BinaryOperatorType.Multiply);
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

        private void acVisualizzaDettagli_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace osp = Application.CreateObjectSpace();
            string FiltroSelezionato = string.Format("Impianto.Oid = {0}", "0");
            string FiltroParziale = string.Format("Impianto.Oid = {0}", "0");
            XPObject ObjSel = null;
            string ViewSelezionato = e.SelectedChoiceActionItem.Id.ToString();
            string Filtro_Pre = e.SelectedChoiceActionItem.Data.ToString();
            //var Li = Application.FindModelView(ViewSelezionato);
            //Type oggetto1 = ((((DevExpress.ExpressApp.Model.IModelListView)(listv))).AsObjectView.ModelClass).TypeInfo.Type;
            // var ObjSel = null;
            ListView lv = null;
            if (View is ListView)
            {
                lv = (ListView)View;
                int numeromax1000 = 0;
                int ObjCount = lv.Editor.GetSelectedObjects().Count;
                // int ImpiantoCont = (((ListView)View).Editor).GetSelectedObjects().ToList().Count();
                if (ObjCount > 0)
                {

                    var oo = lv.Editor.GetSelectedObjects()[0];
                    ObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<XPObject>().First();
                    var ArObjSel = (((ListView)View).Editor).GetSelectedObjects().Cast<XPObject>().Select(s => s.Oid).ToArray<int>();
                    //  var ffff=  lv.Editor.GetSelectedObjects().GetEnumerator();                    
                    // var ImpiantoOids = (((ListView)View).Editor).GetSelectedObjects().Cast<Impianto>().Select(s => s.Oid).ToArray<int>();
                    string sOids = String.Join(",", ArObjSel);
                    FiltroParziale = string.Format(" In ({0})", String.Join(",", ArObjSel));
                    numeromax1000 = int.Parse((ObjCount / 990).ToString());
                }
            }
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                ObjSel = (XPObject)dv.CurrentObject;
                if (ObjSel != null)
                {
                    FiltroParziale = string.Format(" In ({0})", ObjSel.Oid);
                }
            }

            if (ObjSel != null)
            {
                if (!string.IsNullOrEmpty(ViewSelezionato))
                {
                    if (Filtro_Pre.Contains("== ["))
                    {
                        var text = Filtro_Pre;
                        var start = text.IndexOf("[");//add one to not include quote    + 1;
                        var end = text.LastIndexOf("]") - start +1;//		text.Substring(text.IndexOf("["), text.IndexOf("]")-start +2)	"[RisorsaCapo]"	string
                        var result = text.Substring(start, end);
                        var result1 = result.Replace("[", "").Replace("]", "");    

                        FiltroSelezionato = Filtro_Pre.Replace(result, ((XPObject)ObjSel.GetMemberValue(result1)).Oid.ToString());
                    }
                    else if (Filtro_Pre.Contains("^."))
                        FiltroSelezionato = Filtro_Pre.Replace("{0}", FiltroParziale);
                    else
                        FiltroSelezionato = string.Format("{0} {1}", Filtro_Pre, FiltroParziale);

                    var listv = Application.FindModelView(ViewSelezionato);
                    Type oggetto = ((((DevExpress.ExpressApp.Model.IModelListView)(listv))).AsObjectView.ModelClass).TypeInfo.Type;
                    lv = GetListViewbyMenu(oggetto, ViewSelezionato, FiltroSelezionato);
                }
            }

            e.ShowViewParameters.CreatedView = lv;
            e.ShowViewParameters.TargetWindow = TargetWindow.Current;
            e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;

        }



        private ListView GetListViewbyMenu(Type Oggetto, string ListViewId, string Filtro)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            var clTicketLv = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, Oggetto, ListViewId);
            clTicketLv.Criteria.Clear();
            clTicketLv.Criteria["Filtro_Visualizza"] = CriteriaOperator.Parse(Filtro);//string.Format("RegistroRdL.Oid = {0}", RdL.RegistroRdL.Oid)
            ListView view = Application.CreateListView(ListViewId, clTicketLv, true);
            ((ListView)view).Model.Filter = Filtro;

            return view;// Application.CreateListView(ListViewId, clTicketLv, true);
        }

        private void popupWinRigistroOperativoRisorse_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //if (xpObjectSpace != null)
            //{

            //    if (View is DetailView && e.PopupWindowViewSelectedObjects.Count > 0)
            //    {
            //        DetailView dv = (DetailView)View;
            //        RdL rdl = (RdL)dv.CurrentObject;
            //        int OidObjCurr = ((DCRisorseTeamRdL)(((e.PopupWindow)).View).SelectedObjects[0]).OidRisorsaTeam;
            //        RisorseTeam RT = xpObjectSpace.GetObjectByKey<RisorseTeam>(OidObjCurr);
            //        rdl.SetMemberValue("RisorseTeam", RT);

            //        View.Refresh();
            //    }
            //    else
            //    {
            //        MessageOptions options = new MessageOptions() { Duration = 3000, Message = "Nessun Oggetto Selezionato" };
            //        options.Web.Position = InformationPosition.Top;
            //        options.Type = InformationType.Success;
            //        options.Win.Caption = "Avvertenza";
            //        //options.CancelDelegate = CancelDelegate;
            //        //options.OkDelegate = OkDelegate;
            //        Application.ShowViewStrategy.ShowMessage(options);
            //    }
            //}
        }

        private void popupWinRigistroOperativoRisorse_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            if (View is DetailView)
            {
                NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRegistroOperativoRisorsa));
                objectSpace.ObjectsGetting += DCRegistroOperativoRisorsa_objectSpace_ObjectsGetting;

                CollectionSource DCRegistroOperativoRisorsa_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRegistroOperativoRisorsa),
                                                                            "DCRegistroOperativoRisorsa_LookupListView");
                ////  filtro
                //DetailView dv = (DetailView)View;
                //Risorse ris = (Risorse)dv.CurrentObject;
                //var ParCriteria = string.Empty;

                ListView lvk = Application.CreateListView("DCRegistroOperativoRisorsa_LookupListView", DCRegistroOperativoRisorsa_Lookup, true);
                //-----------  filtro
                //var view = Application.CreateListView(listViewId, ListRisorseTeamLookUp, false);

                //        // ListRisorseTeamLookUp.Collection
                if (lvk != null) 
                { //   Azienda Mansione  Telefono
                    string Filtro1 = " [DataOra] >= LocalDateTimeToday() And [DataOra] < LocalDateTimeTomorrow()";/// string.Format("Contains(Upper([RisorsaCapo]),'{0}')", rdl.RicercaRisorseTeam.ToUpper());
                  
                   // string AllFilter = string.Format("{0} Or {1} Or {2} Or {3}", Filtro1, Filtro2, Filtro3, Filtro4);
                    ((ListView)lvk).Model.Filter = Filtro1;
                }

                var dc = Application.CreateController<DialogController>();
                e.DialogController.SaveOnAccept = false;
                e.View = lvk;
                e.Maximized = true;

                //objectSpace.ObjectsGetting -= DCRisorseTeamRdL_objectSpace_ObjectsGetting;
            }
        }


        void DCRegistroOperativoRisorsa_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (View is DetailView)
            {
                BindingList<DCRegistroOperativoRisorsa> objects = new BindingList<DCRegistroOperativoRisorsa>();
                DetailView dv = (DetailView)View;
                int oidRisorsa = 0;
                if (dv.Id == "RisorseTeam_DetailView")
                {
                    RisorseTeam rist = (RisorseTeam)dv.CurrentObject;
                    oidRisorsa = rist.RisorsaCapo.Oid;
                }
                if (dv.Id == "Risorse_DetailView")
                {
                Risorse ris = (Risorse)dv.CurrentObject;
                oidRisorsa = ris.Oid;

                }

                if (oidRisorsa >0)
                {
                    //int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
                    using (DB db = new DB())
                    {
                        //int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                        objects = db.GetOperativoRisorsa(oidRisorsa, DateTime.Now, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                    }
                }
                e.Objects = objects;

                //RisorseTeam rist = (RisorseTeam)dv.CurrentObject;
                //if (rist.Oid != null)
                //{
                //    //int OidRA = Module.Classi.GetOidRegolaAssegnazione.getOidRegolaAssegnazionexTRisorse(rdl, View.ObjectSpace);
                //    using (DB db = new DB())
                //    {
                //        //int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                //        objects = db.GetOperativoRisorsa(rist.RisorsaCapo.Oid, DateTime.Now, CAMS.Module.Classi.SetVarSessione.CorrenteUser);
                //    }
                //}
                //e.Objects = objects;


            }
        }


    }
}
