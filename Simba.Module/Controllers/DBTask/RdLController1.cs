using CAMS.Module.Classi;
using CAMS.Module.Costi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.Guasti;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLController1 : ViewController
    {
        public RdLController1()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (View is ListView)
            {
                //var Lv = (ListView)View;
                //if (Lv.Id.Contains("RdL_ListView_SospeseSO"))
                //{
                //    //AnnullaRdL.Active.SetItemValue("Active", true);
                //    //SospendiRdL.Active.SetItemValue("Active", false);
                //    MigrazioneMPinTT.Active.SetItemValue("Active", true);
                //}
                //if (Lv.Id.Contains("RegistroRdL_RdLes_ListView"))
                //{
                //    //AnnullaRdL.Active.SetItemValue("Active", false);
                //    //SospendiRdL.Active.SetItemValue("Active", false);
                //    MigrazioneMPinTT.Active.SetItemValue("Active", false);
                //}
            }
            if (View is DetailView)
            {
                var dv = (DetailView)View;
                acRegistroCostiRicavi.Active.SetItemValue("Active", dv.ViewEditMode == ViewEditMode.Edit);
                if (((RdL)View.CurrentObject).Oid > 0)
                {
                    RdL vRdL = (RdL)View.CurrentObject;   //    1	MANUTENZIONE PROGRAMMATA  5	MANUTENZIONE PROGRAMMATA SPOT 

                    int num = vRdL.RegistroRdL.RdLes.Count;
                    if (num > 0 && (vRdL.Categoria.Oid == 1 || vRdL.Categoria.Oid == 5))
                    {

                        SetMessaggioWeb("Attenzione questa Richiesta di Lavoro è parte di un Registro di Lavoro di Manutenzione Programmata, per modificarla è necessario Separarla.", "Attenzione ", InformationType.Info);
                    }
                }



            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            if (View is DetailView)
            {
                var dv = (DetailView)View;
                acRegistroCostiRicavi.Active.SetItemValue("Active", dv.ViewEditMode == ViewEditMode.Edit);
                // gestione modifica MP solo annolla
                
                if (((RdL)View.CurrentObject).Oid > 0)
                {
                    RdL vRdL = (RdL)View.CurrentObject;   //    1	MANUTENZIONE PROGRAMMATA  5	MANUTENZIONE PROGRAMMATA SPOT 
                    int num = vRdL.RegistroRdL.RdLes.Count;
                    if (num > 0 && (vRdL.Categoria.Oid == 1 || vRdL.Categoria.Oid == 5))
                    {
                        if (vRdL.UltimoStatoSmistamento.Oid != vRdL.old_SSmistamento_Oid && vRdL.UltimoStatoSmistamento.Oid == 3)
                            SetMessaggioWeb("Attenzione modifica di stato smistamento non consentita. separare la RdL.", "Attenzione ", InformationType.Warning);

                    } 
                }
                
                
                

            }

        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }


        private void RdLInvioMail_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            //if (View is ListView)
            //{
            //    xpObjectSpace = Application.CreateObjectSpace();
            //}
            //else
            //{
            //    xpObjectSpace = View.ObjectSpace;
            //}

            if (View is DetailView)
            {
                RdL vRdL = (RdL)View.CurrentObject;
                if (vRdL.Oid != -1)
                {
                    int OidRegRdL = vRdL.RegistroRdL.Oid;
                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    string Messaggio = string.Empty;
                    System.Data.DataTable TMail = new System.Data.DataTable("TMail");

                    if (xpObjectSpace != null)
                    {
                        try
                        {
                            using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                            {
                                im.InviaMessaggiRdL(Classi.SetVarSessione.OracleConnString, Application.Security.UserName, OidRegRdL, ref  Messaggio);
                            }
                            if (!string.IsNullOrEmpty(Messaggio))
                            {
                                string Titolo = "Trasmissione Avviso Eseguita!!";
                                string AlertMessaggio = string.Format("Messaggio Trasmesso: {0}", Messaggio);
                                SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                        }
                    }

                    //if (xpObjectSpace != null)
                    //{
                    //    //var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                    //    //Mess.Messaggio = Messaggio.ToString();
                    //    //var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
                    //    //view.ViewEditMode = ViewEditMode.View;
                    //    //e.ShowViewParameters.CreatedView = view;
                    //    //view.Caption = view.Caption + " - Esito Trasmissione Avviso Mail";
                    //    //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    //    //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                    //    //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    //}

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
        private void RdLInvioSMS_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is DetailView)
            {
                RdL vRdL = (RdL)View.CurrentObject;
                if (vRdL.Oid != -1)
                {
                    int OidRegRdL = vRdL.RegistroRdL.Oid;
                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    string Messaggio = string.Empty;
                    System.Data.DataTable TMail = new System.Data.DataTable("TMail");

                    if (xpObjectSpace != null)
                    {
                        try
                        {///  https://hosting.aruba.it/servizio-sms/sviluppatori-api-sdk-sms-aruba.aspx
                            using (SMSEAMS imSMS = new SMSEAMS())
                            {
                                Messaggio = imSMS.SendSMSNew(Application.Security.UserName);
                            }
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                        }
                    }

                    if (xpObjectSpace != null)
                    {
                        //var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                        //Mess.Messaggio = Messaggio.ToString();
                        //var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
                        //view.ViewEditMode = ViewEditMode.View;
                        //e.ShowViewParameters.CreatedView = view;
                        //view.Caption = view.Caption + " - Esito Trasmissione Avviso Mail";
                        //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                        //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                        //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }

                }
            }
        }

        private void acRegistroCostiRicavi_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    RdL rdl = (RdL)((DetailView)View).CurrentObject;

                    var NuovoRegistroCosti = xpObjectSpace.CreateObject<RegistroLavori>();
                    NuovoRegistroCosti.Immobile = xpObjectSpace.GetObject<Immobile>(rdl.Immobile);
                    NuovoRegistroCosti.Servizio = xpObjectSpace.GetObject<Servizio>(rdl.Servizio);
                    NuovoRegistroCosti.RegistroRdL = xpObjectSpace.GetObject<RegistroRdL>(rdl.RegistroRdL);
                    NuovoRegistroCosti.Descrizione = "Costi ricavi relativo alla Rdl " + rdl.Oid;

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




        private void saShowMessaggioSLA_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                DetailView Dv = (DetailView)View;
                RdL rdl = (RdL)Dv.CurrentObject;
                if (rdl.Immobile != null && rdl.Categoria != null && rdl.Servizio != null)
                {
                    SetMessaggioCommessa Messaggio = xpObjectSpace.FindObject<SetMessaggioCommessa>(
                                         CriteriaOperator.Parse("Commesse.Oid = ? And Categoria.Oid = ? And Impianto.Oid = ?",
                                         rdl.Immobile.Contratti.Oid,
                                         rdl.Categoria.Oid,
                                         rdl.Servizio.Oid)
                                         , true);

                    if (Messaggio != null)
                    {
                        var view = Application.CreateDetailView(xpObjectSpace, "SetMessaggioCommessa_DetailView_RdL", false, Messaggio);
                        view.ViewEditMode = ViewEditMode.View;
                        e.ShowViewParameters.CreatedView = view;
                        view.Caption = view.Caption + " - Esito Trasmissione Avviso Mail";
                        e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                        e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                        e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }
                }

            }
        }






    }
}
