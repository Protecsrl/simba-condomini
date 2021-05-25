using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.Vista;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace CAMS.Module.Controllers
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class CreateRdLController : ViewController
    {
        public CreateRdLController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);

            bool VisualizzaTasto = false;
            if (View is DetailView)
            {
                string caseSwitch = View.Id;
                switch (caseSwitch)
                {
                    case "Edificio_DetailView":
                    case "Impianto_DetailView":
                    case "Apparato_DetailView":
                        VisualizzaTasto = true;
                        using (UtilController uc = new UtilController())
                        {
                            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                            VisualizzaTasto = uc.GetIsGrantedCreate(xpObjectSpace, "RdL", "C");

                            if (VisualizzaTasto == true)
                            {
                                if ((XPObject)((DetailView)View).CurrentObject != null)
                                    if (((XPObject)((DetailView)View).CurrentObject).ClassInfo.FindMember("Abilitato") != null)
                                    {

                                        FlgAbilitato Abilitato = (FlgAbilitato)((XPObject)((DetailView)View).CurrentObject).GetMemberValue("Abilitato");
                                        FlgAbilitato AbilitazioneEreditata = (FlgAbilitato)((XPObject)((DetailView)View).CurrentObject).GetMemberValue("AbilitazioneEreditata");

                                        if (Abilitato == FlgAbilitato.No || AbilitazioneEreditata == FlgAbilitato.No)
                                            VisualizzaTasto = false;
                                    }
                            }
                        }
                        // se 




                        break;
                }
            }
            //CreaRdLby.Active.SetItemValue("Active", VisualizzaTasto);
            if (View is ListView)
            {
                string caseSwitch = View.Id;
                //if (caseSwitch.Contains("RdLListView_ListView"))
                //    caseSwitch = "RdLListView_ListView";
                switch (caseSwitch)
                {
                    case "RdLListView_ListView":
                    case "RegRdLListView_ListView": //
                    case "RdLListViewSG_ListView":
                    case "ApparatoMap_ApparatoinSostegnos_ListView":
                        VisualizzaTasto = true;
                        using (UtilController uc = new UtilController())
                        {
                            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                            VisualizzaTasto = uc.GetIsGrantedCreate(xpObjectSpace, "RdL", "C");
                        }
                        break;
                }
            }
            CreaRdLby.Active.SetItemValue("Active", VisualizzaTasto);
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

        private void CreaRdLby_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                string aaa = "fdgh";
                if (View is DetailView)
                {
                    var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                    //NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                    NuovoRdL.SetMemberValue("Categoria", xpObjectSpace.GetObjectByKey<Categoria>(4));
                    //NuovoRdL.Priorita = xpObjectSpace.GetObjectByKey<Priorita>(2);
                    //NuovoRdL.SetMemberValue("Priorita", xpObjectSpace.GetObjectByKey<Priorita>(2));//   questa non puo essere impostata da qui
                    //NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                    NuovoRdL.SetMemberValue("UltimoStatoSmistamento", xpObjectSpace.GetObjectByKey<StatoSmistamento>(1));
                    //NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                    //NuovoRdL.SetMemberValue("TipoIntervento", xpObjectSpace.GetObjectByKey<TipoIntervento>(2)); //   questa non puo essere impostata da qui
                    //-------------
                    NuovoRdL.SetMemberValue("TipologiaSpedizione", CAMS.Module.Classi.TipologiaSpedizioneRdL.No);
                    // ---   Soddisfazione
                    NuovoRdL.SetMemberValue("Soddisfazione", Classi.Soddisfazione.INDIFFERENTE);// 2 - null = 

                    DateTime DataAdesso = DateTime.Now;
                    NuovoRdL.SetMemberValue("DataCreazione", DataAdesso);
                    NuovoRdL.SetMemberValue("DataRichiesta", DataAdesso);

                    NuovoRdL.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);
                    string caseSwitch = View.Id;
                    Immobile ed = null;
                    Servizio Imp = null;
                    Asset App = null;
                    switch (caseSwitch)
                    {
                        case "Edificio_DetailView":
                            ed = (Immobile)View.CurrentObject;
                            int conta = ed.NumImp;
                            if (ed.NumImp == 1)
                            {
                                Imp = ed.Impianti[0];
                                //if (Imp.NumApp == 1)
                                //{
                                //    App = Imp.APPARATOes[0];
                                //}
                            }
                            break;
                        case "Impianto_DetailView":
                            Imp = (Servizio)View.CurrentObject;
                            ed = Imp.Immobile;
                            //if (Imp.NumApp == 1)
                            //{
                            //    App = Imp.APPARATOes[0];
                            //}
                            break;
                        case "Apparato_DetailView":
                            App = (Asset)View.CurrentObject;
                            ed = App.Servizio.Immobile;
                            Imp = App.Servizio;

                            break;
                    }

                    NuovoRdL.SetMemberValue("Immobile", xpObjectSpace.GetObject<Immobile>(ed));
                    #region  imposta priorità e tipo intervento
                    Urgenza pr = xpObjectSpace.GetObjectsQuery<ContrattiUrgenza>()
                        .Where(w => w.Contratti.Oid == NuovoRdL.Immobile.Contratti.Oid)
                         .Where(w => w.Default == Classi.FlgAbilitato.Si)
                         .Select(s => s.Urgenza)
                         .FirstOrDefault();
                    NuovoRdL.Urgenza = pr;

                    TipoIntervento ti = xpObjectSpace.GetObjectsQuery<ContrattoTipoIntervento>()
                    .Where(w => w.Commesse.Oid == NuovoRdL.Immobile.Contratti.Oid)
                     .Where(w => w.Default == Classi.FlgAbilitato.Si)
                     .Select(s => s.TipoIntervento)
                     .FirstOrDefault();
                    NuovoRdL.TipoIntervento = ti;
                    #endregion

                    #region orari
                    int minuti = pr.Val;
                    int da = Convert.ToInt32((minuti - (minuti * 0.1)) / 2);
                    int a = Convert.ToInt32((minuti - (minuti * 0.1)));             // Possibili valori di numeroCasuale: {1, 2, 3, 4, 5, 6}
                    Random random = new Random();
                    int numeroCasuale = random.Next(da, a);
                    if (numeroCasuale < 16)
                        numeroCasuale = 16;

                    NuovoRdL.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(numeroCasuale - 10));
                    NuovoRdL.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(a));

                    NuovoRdL.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(numeroCasuale));
                    NuovoRdL.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(numeroCasuale));
                    NuovoRdL.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(numeroCasuale));
                    NuovoRdL.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));
                    #endregion

                    //NuovoRdL.Impianto = xpObjectSpace.GetObject<Impianto>(Imp);
                    if (Imp != null)
                        NuovoRdL.SetMemberValue("Impianto", xpObjectSpace.GetObject<Servizio>(Imp));
                    //NuovoRdL.Apparato = xpObjectSpace.GetObject<Apparato>(App);
                    if (App != null)
                        NuovoRdL.SetMemberValue("Apparato", xpObjectSpace.GetObject<Asset>(App));
                    // richiedente

                    var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);
                    view.Caption = string.Format("Nuova Richiesta di Lavoro");
                    view.ViewEditMode = ViewEditMode.Edit;

                    e.ShowViewParameters.CreatedView = view;
                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                    e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                }

                if (View is ListView)
                {
                    if (View.Id == "ApparatoMap_ApparatoinSostegnos_ListView")
                    {
                        int nrSelezionato = (((ListView)View).Editor).GetSelectedObjects().Count;
                        if (nrSelezionato == 1)
                        {
                            int AppOid = (((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().First().Oid;
                            Asset ApparatoFiglio = xpObjectSpace.GetObjectByKey<Asset>(AppOid);

                            RdL NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                            NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                            NuovoRdL.Urgenza = xpObjectSpace.GetObjectByKey<Urgenza>(2);
                            NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                            NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);

                            NuovoRdL.TipologiaSpedizione = CAMS.Module.Classi.TipologiaSpedizioneRdL.No;
                            NuovoRdL.Soddisfazione = Classi.Soddisfazione.INDIFFERENTE;

                            NuovoRdL.Immobile = xpObjectSpace.GetObjectByKey<Immobile>(ApparatoFiglio.Servizio.Immobile.Oid);
                            NuovoRdL.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(ApparatoFiglio.Servizio.Oid);
                            NuovoRdL.Asset = xpObjectSpace.GetObjectByKey<Asset>(ApparatoFiglio.Oid);

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

                            var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", false, NuovoRdL);
                            view.Caption = string.Format("Nuova Richiesta di Lavoro");
                            view.ViewEditMode = ViewEditMode.Edit;

                            Application.MainWindow.SetView(view);

                            //e.ShowViewParameters.CreatedView = view;
                            //e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                            //e.ShowViewParameters.Context = TemplateContext.View;

                            //e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                            //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                        }
                    }
                    else
                    {
                        string OIDCATEGORIA = "0";
                        var curObj = (XPLiteObject)(((ListView)View).Editor).GetSelectedObjects()[0];
                        if (curObj.ClassInfo.FindMember("OidCategoria") != null)
                        {
                            OIDCATEGORIA = (string)curObj.GetMemberValue("OidCategoria").ToString();
                        }
                        //OIDCATEGORIA==4
                        if (OIDCATEGORIA != "4")
                        {
                            //throw new UserFriendlyException("Si possono clonare solo le RdL a Guasto!!");
                            MessageOptions options = new MessageOptions()
                            {
                                Duration = 3000,
                                Message = string.Format("Si possono clonare solo le RdL a Guasto!!")
                            };
                            options.Web.Position = InformationPosition.Top;
                            options.Type = InformationType.Success;
                            options.Win.Caption = "Avviso";      //options.CancelDelegate = CancelDelegate;     //options.OkDelegate = OkDelegate;
                            Application.ShowViewStrategy.ShowMessage(options);

                            return;
                        }



                        int nrSelezionato = (((ListView)View).Editor).GetSelectedObjects().Count;
                        if (nrSelezionato == 1)
                        {

                            string caseSwitch = View.Id;
                            RdL rdl = null;
                            switch (caseSwitch)
                            {
                                case "RdLListView_ListView": //
                                    rdl = xpObjectSpace.GetObjectByKey<RdL>(curObj.GetMemberValue("Codice"));
                                    break;
                                case "RdLListViewSG_ListView": // 
                                    rdl = xpObjectSpace.GetObjectByKey<RdL>(curObj.GetMemberValue("Codice"));
                                    break;
                                case "RegRdLListView_ListView":
                                    RegistroRdL Regrdl = xpObjectSpace.GetObjectByKey<RegistroRdL>(curObj.GetMemberValue("Codice"));  //      xpObjectSpace.GetObjectByKey<RdL>(Oid_di_RegRdLListViewViewSelezionato);
                                    int OidRdL = Regrdl.RdLes.Select(s => s.Oid).FirstOrDefault(); 
                                    rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdL);
                                    break;

                            }

                            //
                            var NuovoRdL = xpObjectSpace.CreateObject<RdL>();
                            NuovoRdL.Categoria = rdl.Categoria;
                            NuovoRdL.Urgenza = rdl.Urgenza;
                            NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1); ;
                            NuovoRdL.TipoIntervento = rdl.TipoIntervento;
                            NuovoRdL.DataRichiesta = DateTime.Now;
                            NuovoRdL.DataAggiornamento = DateTime.Now;
                            NuovoRdL.DataCreazione = DateTime.Now;
                            NuovoRdL.Immobile = rdl.Immobile;
                            NuovoRdL.Servizio = rdl.Servizio;
                            NuovoRdL.Asset = rdl.Asset;
                            NuovoRdL.Richiedente = rdl.Richiedente;

                            var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);
                            view.Caption = string.Format("Nuova Richiesta di Lavoro");
                            view.ViewEditMode = ViewEditMode.Edit;

                            e.ShowViewParameters.CreatedView = view;
                            e.ShowViewParameters.Context = TemplateContext.View;
                            e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                            e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                            //

                        }
                    }
                }
            }
        }

    }
}
