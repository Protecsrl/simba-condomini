using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.DBGestOrari;
using CAMS.Module.DBHelp;
using CAMS.Module.DBPlanner;
using CAMS.Module.DBPlant;
using CAMS.Module.DBRuoloUtente;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.ParametriPopUp;
using CAMS.Module.DBTask.Vista;
using CAMS.Module.Model;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;

namespace CAMS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppWindowControllertopic.aspx.
    public partial class NavigationWindowController : WindowController
    {
        private DevExpress.ExpressApp.SystemModule.ShowNavigationItemController showNavigationItemController;
        public NavigationWindowController()
        {
            InitializeComponent();
            // Target required Windows (via the TargetXXX properties) and create their Actions.
            #region urian test
            //SimpleAction action = new SimpleAction(this, "T551002Action", PredefinedCategory.View);
            //action.TargetViewId = "no";
            //action.Caption = "T551002";
            //action.Execute += action_Execute;
            #endregion
        }
        #region urian test

        //private void action_Execute(object sender, SimpleActionExecuteEventArgs e)
        //{
        //    NonPersistentObjectSpace os = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(npRdLRisorseTeam));
        //    os.CommitChanges();
        //}
        #endregion
        //string  DetailViewNewTTPersonalizzata 
        protected override void OnActivated()
        {
            base.OnActivated();
            var xpObjectSpace = Application.CreateObjectSpace();
            SetVarSessione.VisualizzaSLA = SecuritySystem.IsGranted(new
                               DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpace, typeof(RdLListViewGuasto),
                             DevExpress.ExpressApp.Security.SecurityOperations.Navigate));



        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void NavigationWindowController_Activated(object sender, EventArgs e)
        {
            CAMS.Module.Classi.SetVarSessione.CorrenteUser = Application.Security.UserName;

            showNavigationItemController = Frame.GetController<DevExpress.ExpressApp.SystemModule.ShowNavigationItemController>();
            if (showNavigationItemController != null)
            {
                showNavigationItemController.CustomShowNavigationItem += showNavigationItemController_CustomShowNavigationItem;
                showNavigationItemController.CustomGetStartupNavigationItem += showNavigationItemController_CustomGetStartupNavigationItem;
                showNavigationItemController.NavigationItemCreated += showNavigationItemController_NavigationItemCreated;
            }

        }
        private void NavigationWindowController_Deactivated(object sender, EventArgs e)
        {
            try
            {
                if (showNavigationItemController != null)
                {
                    showNavigationItemController.CustomShowNavigationItem -= showNavigationItemController_CustomShowNavigationItem;
                    showNavigationItemController.CustomGetStartupNavigationItem -= showNavigationItemController_CustomGetStartupNavigationItem;
                    showNavigationItemController.NavigationItemCreated -= showNavigationItemController_NavigationItemCreated;
                }
            }
            catch { }
        }

        private void showNavigationItemController_CustomShowNavigationItem(object sender, DevExpress.ExpressApp.SystemModule.CustomShowNavigationItemEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            Session session = ((XPObjectSpace)xpObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                string RdLNuovoTTGuasto_DetailView = "RdL_DetailView_NuovoTT";
                const string NuovoRegPianoMP_DetailView = "RegPianificazioneMP_DetailView_Nuovo";
                string VoceMenu = e.ActionArguments.SelectedChoiceActionItem.Id.ToString();
                DateTime DataAdesso = DateTime.Now;

                switch (VoceMenu)
                {
                    case "CreaTT":
                        #region
                        string DetalViewNewTT = "RdL_DetailView_NuovoTT_INTERCENTERMO";
                        //RdLNuovoTTGuasto_DetailView

                        int ContaEdifici = session.Query<Immobile>().Count();
                        var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                        //NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                        NuovoRdL.SetMemberValue("Categoria", xpObjectSpace.GetObjectByKey<Categoria>(4));
                        //NuovoRdL.Priorita = xpObjectSpace.GetObjectByKey<Priorita>(2);
                        NuovoRdL.SetMemberValue("Priorita", xpObjectSpace.GetObjectByKey<Urgenza>(2));
                        //NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                        NuovoRdL.SetMemberValue("UltimoStatoSmistamento", xpObjectSpace.GetObjectByKey<StatoSmistamento>(1));
                        //NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                        NuovoRdL.SetMemberValue("TipoIntervento", xpObjectSpace.GetObjectByKey<TipoIntervento>(2));
                        NuovoRdL.SetMemberValue("TipologiaSpedizione", CAMS.Module.Classi.TipologiaSpedizioneRdL.No);
                        NuovoRdL.SetMemberValue("Soddisfazione", Classi.Soddisfazione.INDIFFERENTE);// 2 - null = molto soddisfatto
                        NuovoRdL.SetMemberValue("DataCreazione", DataAdesso);
                        //NuovoRdL.DataRichiesta = DateTime.Now;
                        NuovoRdL.SetMemberValue("DataRichiesta", DataAdesso);
                        NuovoRdL.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(15));
                        NuovoRdL.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(30));
                        NuovoRdL.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(16));
                        NuovoRdL.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(16));
                        NuovoRdL.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(16));
                        NuovoRdL.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));
                        NuovoRdL.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);
                        if (ContaEdifici == 1)
                        {
                            //NuovoRdL.Immobile = session.Query<Immobile>().First();
                            Immobile ed = session.Query<Immobile>().First();
                            NuovoRdL.SetMemberValue("Immobile", ed);
                            //NuovoRdL.Impianto = NuovoRdL.Immobile.Impianti.FirstOrDefault();
                            NuovoRdL.SetMemberValue("Impianto", ed.Impianti.FirstOrDefault());
                            //NuovoRdL.Apparato = NuovoRdL.Impianto.APPARATOes.FirstOrDefault();
                            // NuovoRdL.SetMemberValue("Apparato", ed.Impianti.FirstOrDefault().APPARATOes.FirstOrDefault()); 
                            // tolto il richiedente
                            //int Oid_Richiedente = session.Query<Richiedente>()
                            //                     .Where(w => w.Commesse == NuovoRdL.Immobile.Commesse)
                            //                     .Select(s => s.Oid).FirstOrDefault();
                            //NuovoRdL.SetMemberValue("Richiedente", xpObjectSpace.GetObjectByKey<Richiedente>(Oid_Richiedente)); 

                        }
                        else
                        {
                            var user3 = (SecuritySystemUser)Application.Security.User;
                            var IsAdmin3 = user3.Roles.Where(w => w.IsAdministrative).Count();
                            if (Application.Security.UserName.ToString() == "Sam" || IsAdmin3 > 0)
                            {
                                //int ContaRdL = session.Query<RdL>().Where(w => w.Richiedente != null).Count();
                                //if (ContaRdL > 0)
                                //{
                                //Apparato _Apparato = session.Query<RdL>().Where(w => w.Richiedente != null).Select(s => s.Apparato).First();
                                //NuovoRdL.Apparato = _Apparato;
                                //NuovoRdL.SetMemberValue("Apparato", _Apparato);
                                //NuovoRdL.Impianto = NuovoRdL.Apparato.Impianto;
                                //NuovoRdL.SetMemberValue("Impianto", _Apparato.Impianto); 
                                Immobile _Edificio = session.Query<Immobile>().Where(w => w.Descrizione.Contains("Tivoli")).Select(s => s).First();
                                NuovoRdL.Immobile = _Edificio;
                                //NuovoRdL.SetMemberValue("Immobile", _Apparato.Impianto.Immobile); 
                                NuovoRdL.Descrizione = "Sam esegue una Prova" + DateTime.Now.ToShortDateString();
                                //}
                            }
                        }
                        //if (!SetVarSessione.VisualizzaSLA)   string RdLNuovoTTGuasto_DetailView = "RdL_DetailView_NuovoTT";
                        var viewRdL = Application.CreateDetailView(xpObjectSpace, RdLNuovoTTGuasto_DetailView, true, NuovoRdL);
                        viewRdL.Caption = string.Format("Nuova Richiesta di Lavoro");
                        viewRdL.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = viewRdL;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;
                        #endregion
                        break;
                    //
                    case "CreaTT_CC":  /// questa è nuovo TT per tutti

                        Session sessionCC = ((XPObjectSpace)xpObjectSpace).Session;
                        //int ContaEdificiCC = session.Query<Immobile>().Count();
                        RdL NuovoRdLCC = xpObjectSpace.CreateObject<RdL>();
                        //NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                        NuovoRdLCC.SetMemberValue("Categoria", xpObjectSpace.GetObjectByKey<Categoria>(4));
                        //NuovoRdL.Priorita = xpObjectSpace.GetObjectByKey<Priorita>(2);
                        //NuovoRdLCC.SetMemberValue("Priorita", xpObjectSpace.GetObjectByKey<Priorita>(2));//   questa non puo essere impostata da qui
                        //NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                        NuovoRdLCC.SetMemberValue("UltimoStatoSmistamento", xpObjectSpace.GetObjectByKey<StatoSmistamento>(1));
                        //NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                        //NuovoRdLCC.SetMemberValue("TipoIntervento", xpObjectSpace.GetObjectByKey<TipoIntervento>(2)); // questa non puo essere impostata da qui
                        NuovoRdLCC.SetMemberValue("TipologiaSpedizione", CAMS.Module.Classi.TipologiaSpedizioneRdL.No);
                        NuovoRdLCC.SetMemberValue("Soddisfazione", Classi.Soddisfazione.INDIFFERENTE);// 2 - null = molto soddisfatto
                        //NuovoRdL.DataCreazione = DateTime.Now;
                        NuovoRdLCC.SetMemberValue("DataCreazione", DataAdesso);
                        //NuovoRdL.DataRichiesta = DateTime.Now;
                        NuovoRdLCC.SetMemberValue("DataRichiesta", DataAdesso);
                        int minuti = xpObjectSpace.GetObjectByKey<Urgenza>(2).Val;
                        int da = Convert.ToInt32((minuti - (minuti * 0.1)) / 2);
                        int a = Convert.ToInt32((minuti - (minuti * 0.1)));             // Possibili valori di numeroCasuale: {1, 2, 3, 4, 5, 6}
                        Random random = new Random();
                        int numeroCasuale = random.Next(da, a);
                        if (numeroCasuale < 16)
                            numeroCasuale = 16;
                        NuovoRdLCC.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(numeroCasuale - 10));
                        NuovoRdLCC.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(a));
                        NuovoRdLCC.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(numeroCasuale));
                        NuovoRdLCC.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(numeroCasuale));
                        NuovoRdLCC.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(numeroCasuale));
                        NuovoRdLCC.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));
                        NuovoRdLCC.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);

                        string DetailViewNewTTPersonalizzata = "RdL_DetailView";

                        var user2 = (SecuritySystemUser)Application.Security.User;
                        var IsAdmin = user2.Roles.Where(w => w.IsAdministrative).Count();
                        if (Application.Security.UserName.ToString() == "Sam" || IsAdmin > 0)
                        {
                            Immobile _Edificio = session.Query<Immobile>().Where(w => w.Descrizione.Contains(" di Test ")).Select(s => s).FirstOrDefault();
                            NuovoRdLCC.Immobile = _Edificio;
                        }

                        DetailView viewRdLCC = Application.CreateDetailView(xpObjectSpace, DetailViewNewTTPersonalizzata, true, NuovoRdLCC); //RdL_DetailView_NuovoTT_CC RdL_DetailView  RdL_DetailView_Gestione
                        viewRdLCC.Caption = string.Format("Nuova Richiesta di Lavoro");
                        viewRdLCC.ViewEditMode = ViewEditMode.Edit;//     viewRdLCC.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = viewRdLCC;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;
                        break;
                    case "CreaAvP":
                        var NuovoCN = xpObjectSpace.CreateObject<ControlliNormativi>();
                        NuovoCN.DataInizioControllo = DateTime.Now;
                        NuovoCN.DataPianificataControllo = DateTime.Now;
                        NuovoCN.Nome = "Nuovo Avviso Periodico ....";
                        NuovoCN.StatoControlloNormativo = Classi.StatoControlloNormativo.InCreazione;
                        //NuovoCN.Immobile = xpObjectSpace.GetObjects<Immobile>().FirstOrDefault();
                        //NuovoCN.Impianto = NuovoCN.Immobile.Impianti.FirstOrDefault();
                        var view_cn = Application.CreateDetailView(xpObjectSpace, "ControlliNormativi_DetailView", true, NuovoCN);
                        view_cn.Caption = string.Format("Nuovo Avviso Periodico");
                        view_cn.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = view_cn;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;
                    case "NuovoPianificazioneMP":
                        RegPianificazioneMP NuovoRegPianificazioneMP = xpObjectSpace.CreateObject<RegPianificazioneMP>();
                        NuovoRegPianificazioneMP.AnnoSchedulazione = xpObjectSpace.GetObjectByKey<Anni>(DateTime.Now.Year);
                        var view_PMP = Application.CreateDetailView(xpObjectSpace, "RegPianificazioneMP_DetailView_Nuovo", true, NuovoRegPianificazioneMP);
                        view_PMP.Caption = string.Format("Nuovo Registro Pianificazione");
                        view_PMP.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = view_PMP;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;
                    case "Crea_Scenario":
                        Scenario NuovoScenario = xpObjectSpace.CreateObject<Scenario>();
                        NuovoScenario.Descrizione = "Nuovo scenario ...";
                        var view_Scenario = Application.CreateDetailView(xpObjectSpace, "Scenario_DetailView_Crea", true, NuovoScenario);
                        view_Scenario.Caption = string.Format("Nuovo Scenario Pianificazione Manutenzione");
                        view_Scenario.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = view_Scenario;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;
                    case "CreaRuolo":
                        ClonaRuoloUser NuovoClonaRuoloUser = xpObjectSpace.CreateObject<ClonaRuoloUser>();
                        var viewClonaRuoloUser = Application.CreateDetailView(xpObjectSpace, "ClonaRuoloUser_DetailView", true, NuovoClonaRuoloUser);
                        viewClonaRuoloUser.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = viewClonaRuoloUser;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;
                    case "NuovoSinottico":
                        //GroupOperator GrOperator = new GroupOperator(GroupOperatorType.And);
                        SecuritySystemUser user = xpObjectSpace.GetObject<SecuritySystemUser>
                                                                       ((SecuritySystemUser)Application.Security.User);

                        ParametriPivot Parametro = xpObjectSpace.FindObject<ParametriPivot>(
                                                                      CriteriaOperator.Parse("SecurityUser = ?", user));
                        if (Parametro == null)
                        {
                            ParametriPivot Parametronew = xpObjectSpace.CreateObject<ParametriPivot>();
                            //if (Parametronew.SecurityUser != (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User)
                            //    Parametronew.SecurityUser = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User;

                            //                     .Where(w => w.Commesse == NuovoRdL.Immobile.Commesse)
                            //                     .Select(s => s.Oid).FirstOrDefault();
                            //Parametronew.Commessa = session.Query<Commesse>().FirstOrDefault();
                            Parametronew.Data_DA = DateTime.Now.AddMonths(-3);
                            Parametronew.Data_DA = DateTime.Now.AddMonths(0);
                            Parametronew.Save();
                            xpObjectSpace.CommitChanges();

                            Parametro = Parametronew;
                        }


                        DetailView viewParametro = Application.CreateDetailView(xpObjectSpace, "ParametriPivot_DetailView", true, Parametro);
                        viewParametro.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = viewParametro;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;

                    case "New_GestioneOrari":
                        SecuritySystemUser userGO = xpObjectSpace.GetObject<SecuritySystemUser>
                                                                       ((SecuritySystemUser)Application.Security.User);                    

                        XPQuery<tbcalendario> stagioni = new XPQuery<tbcalendario>(session);
                        string querystagioni = stagioni.Select(s => s.stagione).Distinct().ToList().OrderByDescending(o=>o).FirstOrDefault();     
                        GestioneOrari ParametroGO = xpObjectSpace.CreateObject<GestioneOrari>();

                        ParametroGO.SecurityUser = userGO;
                        ParametroGO.Descrizione = $"inserisci una descrizione del ticket di gestione orario ... (stagione {querystagioni})";
                        ParametroGO.DataInserimento = DateTime.Now.AddDays(0); ;
                        ParametroGO.DataAggiornamento = DateTime.Now.AddDays(0); ;
                        ParametroGO.Stagione = querystagioni;
                        ParametroGO.dataora_dal = DateTime.Now.AddDays(-7);
                        ParametroGO.dataora_Al = DateTime.Now.AddDays(0);
                        ParametroGO.F1_ModDataOra = DateTime.Now.AddDays(-6);
                        //ParametroGO.F1_ModDataOra = DateTime.Now.AddDays(-6);
                        ParametroGO.Circuito = xpObjectSpace.GetObjectsQuery<tbcircuiti>(true).FirstOrDefault();
                        ParametroGO.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(8);// impostazione filtro

                        ParametroGO.f1startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f1endTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f2startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f2endTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f3startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f3endTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f4startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGO.f4endTipoSetOrario = TipoSetOrario.o00_00;

                        ParametroGO.Save();
                        xpObjectSpace.CommitChanges();

                        DetailView viewParametroGO = Application.CreateDetailView(xpObjectSpace, "GestioneOrari_DetailView", true, ParametroGO);
                        viewParametroGO.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = viewParametroGO;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;
                    case "New_GestioneNuoviOrari":
                        SecuritySystemUser userGNO = xpObjectSpace.GetObject<SecuritySystemUser>
                                                                       ((SecuritySystemUser)Application.Security.User);

                        XPQuery<tbcalendario> stagioniGNO = new XPQuery<tbcalendario>(session);
                        string querystagioniGNO = stagioniGNO.Select(s => s.stagione).Distinct().ToList().OrderByDescending(o => o).FirstOrDefault();
                        GestioneNuoviOrari ParametroGNO = xpObjectSpace.CreateObject<GestioneNuoviOrari>();

                        ParametroGNO.SecurityUser = userGNO;
                        ParametroGNO.Descrizione = $"inserisci una descrizione del ticket di NUOVA PROGRAMMAZIONE STAGIONALE ... (stagione {querystagioniGNO})";
                        ParametroGNO.DataInserimento = DateTime.Now.AddDays(0); ;
                        ParametroGNO.DataAggiornamento = DateTime.Now.AddDays(0); ;
                        ParametroGNO.Stagione = querystagioniGNO;
                        //ParametroGNO.dataora_dal = DateTime.Now.AddDays(-7);
                        //ParametroGNO.dataora_Al = DateTime.Now.AddDays(0);
                        //ParametroGNO.F1_ModDataOra = DateTime.Now.AddDays(-6);
                        //ParametroGO.F1_ModDataOra = DateTime.Now.AddDays(-6);
                        ParametroGNO.Circuito = xpObjectSpace.GetObjectsQuery<tbcircuiti>(true).FirstOrDefault();
                        ParametroGNO.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(8);// impostazione filtro

                        ParametroGNO.f1startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f1endTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f2startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f2endTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f3startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f3endTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f4startTipoSetOrario = TipoSetOrario.o00_00;
                        ParametroGNO.f4endTipoSetOrario = TipoSetOrario.o00_00;

                        ParametroGNO.Save();
                        xpObjectSpace.CommitChanges();

                        DetailView viewParametroGNO = Application.CreateDetailView(xpObjectSpace, "GestioneNuoviOrari_DetailView", true, ParametroGNO);
                        viewParametroGNO.ViewEditMode = ViewEditMode.Edit;
                        e.ActionArguments.ShowViewParameters.CreatedView = viewParametroGNO;
                        e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
                        e.Handled = true;

                        break;
                    default:

                        break;
                }
            }
        }

        private void showNavigationItemController_CustomGetStartupNavigationItem(object sender, CustomGetStartupNavigationItemEventArgs e)
        {
            e.Handled = true;
            // StartupNavigationItem   è l'azione che si attiva all'apertura del sistema.
            // e.StartupNavigationItem = e.ActionItems.Find("NewRdL", ChoiceActionItemFindType.Recursive, ChoiceActionItemFindTarget.Any);
            List<NavigationHide> ListNavigationHide = new List<NavigationHide>();
            var xpObjectSpace = Application.CreateObjectSpace();
            Session session = ((XPObjectSpace)xpObjectSpace).Session;
            ListNavigationHide = GetNavigationIDforFalse();
            if (xpObjectSpace != null)
            {
                if (ListNavigationHide != null)
                {
                    foreach (NavigationHide ln in ListNavigationHide.Where(w => w.TipoNavigationItem == TipoNavigationItem.Visualizza))
                    {
                        var Azione = e.ActionItems.Find(ln.NavigationItemID, ChoiceActionItemFindType.Recursive, ChoiceActionItemFindTarget.Any);
                        Azione.Active.SetItemValue("nascondi", false);
                    }
                    foreach (NavigationHide ln in ListNavigationHide.Where(w => w.TipoNavigationItem == TipoNavigationItem.StartPage))
                    {
                        e.StartupNavigationItem = e.ActionItems.Find(ln.NavigationItemID, ChoiceActionItemFindType.Recursive, ChoiceActionItemFindTarget.Any);
                        break;
                    }
                }
            }



        }
        private List<NavigationHide> GetNavigationIDforFalse()
        {
            // è necessario abilitare la classe sul ruolo permition @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
            int ValoreRetur = 0;
            var xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)xpObjectSpace).Session;
            try
            {
                List<NavigationHide> ListNavigationHide = new List<NavigationHide>();
                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.And);
                // caso non Administrator
                DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser u = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User;
                int IsAdmin = u.Roles.Where(w => w.IsAdministrative == true).Count();
                if (IsAdmin == 1)
                {
                    return null;
                }


                XPView xpview = new XPView(Sess, typeof(RuoliPersonalizzati));
                xpview.Properties.AddRange(new ViewProperty[] {
                 new ViewProperty("Oid", SortDirection.None, "[Oid]", true, true),
                new ViewProperty("NavigationItemID", SortDirection.None, "[NavigationItemID]", true, true),
                     new ViewProperty("TipoNavigationItem", SortDirection.None, "[TipoNavigationItem]", true, true),
                new ViewProperty("Attivo", SortDirection.Ascending, "[Attivo]", true, true),
                });
                // verifico se c'e' il ruolo con utente nullo
                var ListaRuoli = u.Roles.Select(w => w.Name).ToList<string>();
                foreach (SecuritySystemRole r in u.Roles)
                {
                    // tutti i casi utente
                    CriteriaOperator opCriteriaRole = new OperandProperty("SecurityRole.Oid");
                    CriteriaOperator opValoreRole = new OperandValue(r.Oid);
                    BinaryOperator criteriaRole = new BinaryOperator(opCriteriaRole, opValoreRole, BinaryOperatorType.Equal);
                    criteriaOP2.Operands.Add(criteriaRole);

                    CriteriaOperator criteriaRoleUser = CriteriaOperator.Parse("SecurityUser is null");
                    criteriaOP2.Operands.Add(criteriaRoleUser);

                    CriteriaOperator criteriaRoleNavigationItemID = CriteriaOperator.Parse("NavigationItemID is not null");
                    criteriaOP2.Operands.Add(criteriaRoleNavigationItemID);

                    xpview.Criteria = criteriaOP2;
                    foreach (ViewRecord rec in xpview)
                    {
                        ValoreRetur = 1;
                        ListNavigationHide.Add(new NavigationHide
                        {
                            NavigationItemID = rec["NavigationItemID"].ToString(),
                            TipoNavigationItem = (TipoNavigationItem)rec["TipoNavigationItem"],
                            Attivo = Convert.ToBoolean(rec["Attivo"])
                        });
                    }
                    if (ValoreRetur == 1) return ListNavigationHide;
                }

                // tutti i casi utente
                criteriaOP2.Operands.Clear();
                xpview.Criteria = null;
                CriteriaOperator opCriteriaUser = new OperandProperty("SecurityUser.Oid");
                CriteriaOperator opValoreUser = new OperandValue(u.Oid);
                BinaryOperator criteriaUser = new BinaryOperator(opCriteriaUser, opValoreUser, BinaryOperatorType.Equal);
                criteriaOP2.Operands.Add(criteriaUser);

                CriteriaOperator criteriaUserNotNull = CriteriaOperator.Parse("SecurityUser is not null");
                criteriaOP2.Operands.Add(criteriaUserNotNull);

                CriteriaOperator criteriaUserNavigationItemID = CriteriaOperator.Parse("NavigationItemID is not null");
                criteriaOP2.Operands.Add(criteriaUserNavigationItemID);

                //xpview.Criteria = criteriaOP2;
                foreach (ViewRecord rec in xpview)
                {
                    ValoreRetur = 1;
                    ListNavigationHide.Add(new NavigationHide
                    {
                        NavigationItemID = rec["NavigationItemID"].ToString(),
                        TipoNavigationItem = (TipoNavigationItem)rec["TipoNavigationItem"],
                        Attivo = Convert.ToBoolean(rec["Attivo"])
                    });
                }
                if (ValoreRetur == 1) return ListNavigationHide;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message.ToString());
                return null;
            }

            //var aaa = Sess.Query<RuoliPersonalizzati>().ToList();
            return null;
        }

        // crea un albero di help per ogni menu @@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
        void showNavigationItemController_NavigationItemCreated(object sender, NavigationItemCreatedEventArgs e)
        {
            ChoiceActionItem navigationItem = e.NavigationItem;
            DevExpress.ExpressApp.Model.IModelObjectView viewNode = ((IModelNavigationItem)e.NavigationItem.Model).View as DevExpress.ExpressApp.Model.IModelObjectView;
            if (viewNode != null)
            {
                DevExpress.ExpressApp.DC.ITypeInfo objectTypeInfo = XafTypesInfo.Instance.FindTypeInfo(viewNode.ModelClass.Name);
                if (objectTypeInfo != null)
                {
                    CriteriaOperator docCriteria = CriteriaOperator.Parse("ObjectType == ?", objectTypeInfo.Type);
                    IObjectSpace myObjectSpace = Application.CreateObjectSpace(typeof(HelpConfiguration));
                    IList<HelpConfiguration> docs = myObjectSpace.GetObjects<HelpConfiguration>(docCriteria);
                    if (docs.Count > 0)
                    {
                        ChoiceActionItem docsGroup = new ChoiceActionItem("CustomDocuments", "Task-Based Help", null) { ImageName = "BO_Report" };
                        navigationItem.Items.Add(docsGroup);
                        foreach (HelpConfiguration doc in docs)
                        {
                            ViewShortcut shortcut = new ViewShortcut(typeof(HelpConfiguration), doc.Oid.ToString(), "HelpDocument_DetailView_FewColumns");
                            ChoiceActionItem docItem = new ChoiceActionItem(doc.Oid.ToString(), doc.Descrizione, shortcut) { ImageName = "Navigation_Item_Report" };
                            docsGroup.Items.Add(docItem);
                        }
                    }
                }
            }
        }

        public bool GetIsCallcenter(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser Utente)
        {
            int IsCC = Utente.Roles.Where(w => w.Name.ToUpper().Contains("CALLCENTER")).Count();
            if (IsCC > 0) return true;
            return false;
        }
        public bool GetDetailViewPersonalizzata(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser Utente)
        {
            int IsCC = Utente.Roles.Where(w => w.Name.ToUpper().Contains("CLIENTE_INTERCENTER")).Count();
            if (IsCC > 0) return true;
            return false;
        }
        private void createSideMenu(string role)
        {
            SingleChoiceAction StandardShowNavigationItemAction =
             Frame.GetController<DevExpress.ExpressApp.SystemModule.ShowNavigationItemController>().ShowNavigationItemAction;


            XmlDocument doc = new XmlDocument();
            XmlNode docNode = doc.CreateXmlDeclaration("1.0", "UTF-8", null);
            doc.AppendChild(docNode);
            XmlNode menuNode = doc.CreateElement("siteMap");
            for (int i = 0; i < StandardShowNavigationItemAction.Items.Count; i++)
            {
                XmlNode groupNodePadre = doc.CreateElement("siteMapNode");
                XmlNode groupNode = doc.CreateElement("siteMapNode");
                XmlAttribute groupAttribute = doc.CreateAttribute("Title");

                groupAttribute.Value = StandardShowNavigationItemAction.Items[i].Caption;
                groupNode.Attributes.Append(groupAttribute);

                for (int j = 0; j < StandardShowNavigationItemAction.Items[i].Items.Count; j++)
                {
                    XmlNode itemNode = null;
                    try
                    {
                        itemNode = doc.CreateElement("siteMapNode");
                        XmlAttribute itemAttribute = doc.CreateAttribute("Title");
                        itemAttribute.Value = StandardShowNavigationItemAction.Items[i].Items[j].Caption;
                        itemNode.Attributes.Append(itemAttribute);
                        if (StandardShowNavigationItemAction.Items[i].Items[j].Data != null)
                        {

                            XmlAttribute url = doc.CreateAttribute("Url");
                            url.Value = ((DevExpress.ExpressApp.ViewShortcut)(StandardShowNavigationItemAction.Items[i].Items[j].Data)).ViewId;
                            itemNode.Attributes.Append(url);

                        }
                    }
                    catch (InvalidCastException e) { continue; }


                    for (int k = 0; k < StandardShowNavigationItemAction.Items[i].Items[j].Items.Count; k++)
                    {
                        XmlNode itemNode2 = null;
                        try
                        {
                            itemNode2 = doc.CreateElement("siteMapNode");
                            XmlAttribute itemAttribute2 = doc.CreateAttribute("Title");

                            itemAttribute2.Value = StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Caption;
                            itemNode2.Attributes.Append(itemAttribute2);

                            if (StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Data != null)
                            {

                                XmlAttribute url2 = doc.CreateAttribute("Url");
                                url2.Value = ((DevExpress.ExpressApp.ViewShortcut)(StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Data)).ViewId;
                                itemNode2.Attributes.Append(url2);

                            }
                        }
                        catch (InvalidCastException e) { continue; }

                        for (int x = 0; x < StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Items.Count; x++)
                        {
                            XmlNode itemNode3 = null;
                            try
                            {
                                itemNode3 = doc.CreateElement("siteMapNode");
                                XmlAttribute itemAttribute3 = doc.CreateAttribute("Title");

                                itemAttribute3.Value = StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Items[x].Caption;
                                itemNode3.Attributes.Append(itemAttribute3);
                                if (StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Items[x].Data != null)
                                {

                                    XmlAttribute url3 = doc.CreateAttribute("Url");
                                    url3.Value = ((DevExpress.ExpressApp.ViewShortcut)(StandardShowNavigationItemAction.Items[i].Items[j].Items[k].Items[x].Data)).ViewId;
                                    itemNode3.Attributes.Append(url3);

                                }
                            }
                            catch (InvalidCastException e) { continue; }

                            itemNode2.AppendChild(itemNode3);
                        }
                        itemNode.AppendChild(itemNode2);
                    }
                    groupNode.AppendChild(itemNode);
                    groupNodePadre.AppendChild(groupNode);
                }
                menuNode.AppendChild(groupNodePadre);
            }
            doc.AppendChild(menuNode);
            string path = CAMS.Module.Classi.SetVarSessione.WebPath;
            var filepath = System.Web.Hosting.HostingEnvironment.MapPath(path);
            doc.Save(String.Format("{0}/SitoHelp/App_Data/Menu/{1}SideMenu.xml", path, role));

        }


    }

}

// foreach (var ac in e.ActionItems)
//    {
//        foreach (var Item in ac.Items)
//        {
//            if (Item.Id == "NewRdL")
//            {
//                //ac.Active.SetItemValue("nascondi", false);
//            }
//        }
//    }
//}

// Perform various tasks depending on the target Window.
//var xpObjectSpace = Application.CreateObjectSpace();
//if (xpObjectSpace != null)
//{
//    // List<CreazioneXmlRuolo> listxml = (List<CreazioneXmlRuolo>)xpObjectSpace.GetObjects<CreazioneXmlRuolo>().Where(r => r.Ruolo.Equals(CAMS.Module.Classi.SetVarSessione.RuoloXml)).ToList();
//    //Session session = ((XPObjectSpace)xpObjectSpace).Session;
//    //List<int> listxmlOid = session.Query<CreazioneXmlRuolo>()
//    //                                  .Where(r => r.Ruolo.Equals(CAMS.Module.Classi.SetVarSessione.RuoloXml))
//    //                                  .Select(s => s.Oid).ToList();
//    // string path = CAMS.Module.Classi.SetVarSessione.WebPath;
//var filepath = System.Web.Hosting.HostingEnvironment.MapPath(@"\SitoHelp\App_Data\Menu");

//    // var filepath1 = String.Format("{0}/SitoHelp/App_Data/Menu/{1}SideMenu.xml", path, "role");
//    //System.Diagnostics.Debug.WriteLine(  String.Format("{0}/SitoHelp/App_Data/Menu/{1}SideMenu.xml", path, "ATM"));
//    //if (File.Exists(filepath1))
//    //{ 
//    //  System.IO.Stream excelStream = new System.IO.FileStream(filepath1, System.IO.FileMode.Open, System.IO.FileAccess.ReadWrite);
//    // excelStream.Close();
//    // excelStream.Dispose();
//    //}

//    //if (false)//*if (listxmlOid.Count > 0)
//    //{
//    //    CreazioneXmlRuolo xml = xpObjectSpace.GetObjectByKey<CreazioneXmlRuolo>(listxmlOid[0]);// listxml.First();
//    //    createSideMenu(xml.Ruolo);
//    //    xml.Delete();
//    //    xpObjectSpace.CommitChanges();
//    //}
//}