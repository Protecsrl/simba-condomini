using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLNoteController : ViewController
    {
        public RdLNoteController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            //Perform various tasks depending on the target View.


        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View is ListView)
            {
                if (View.Id.Contains("RdL_RdLNotes_ListView"))
                {
                    var dvParent = (DetailView)(View.ObjectSpace).Owner;

                    if (dvParent.Id.Contains("RdL_DetailView")) //    if (dvParent.Id == "RdL_DetailView_Cliente")
                    {
                        if (dvParent.ViewEditMode == ViewEditMode.Edit)
                            pupWinAddNoteRdL.Active["pupWinAddNoteRdL"] = true;
                        else
                            pupWinAddNoteRdL.Active["pupWinAddNoteRdL"] = false;
                    }
                }
            }


        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void saAddNote_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //if (View is DetailView)
            //    saSostituisciApparato.Enabled["EditMode"] = ((DetailView)View).ViewEditMode == ViewEditMode.Edit;
            //IObjectSpace xpObjectSpace = Vi.CreateObjectSpace();
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (View.Id == "RdL_RdLNotes_ListView")
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id.Contains("RdL_DetailView"))
                {
                    //IObjectSpace xpObjectSpace = dvParent.ObjectSpace;
                    RdL rdl = (RdL)dvParent.CurrentObject;
                    RdLNote RdLNote = xpObjectSpace.CreateObject<RdLNote>();
                    RdLNote.RdL = xpObjectSpace.GetObjectByKey<RdL>(rdl.Oid);
                    RdLNote.OidCommessa_Richiedente = rdl.Immobile.Contratti.Oid;
                    //NotificaRdL GetNotificaRdL = xpObjectSpace.GetObject<NotificaRdL>((NotificaRdL)View.CurrentObject);
                    // RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetNotificaRdL.RdL.Oid);

                    var view = Application.CreateDetailView(xpObjectSpace, "RdLNote_DetailView", true, RdLNote);

                    view.Caption = string.Format("Richiesta di Lavoro");
                    view.ViewEditMode = ViewEditMode.Edit;
                    //Application.MainWindow.SetView(view);
                    //e.ShowViewParameters.
                    e.ShowViewParameters.CreatedView = view;
                    //aggiunta novelli
                    //xpObjectSpace.CommitChanges();

                    DialogController dc = Application.CreateController<DialogController>();
                    dc.Accepting += new EventHandler<DialogControllerAcceptingEventArgs>(dc_Accepting);
                    e.ShowViewParameters.Controllers.Add(dc);

                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                    e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                }
            }



        }

        void dc_Accepting(object sender, DialogControllerAcceptingEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            DevExpress.Xpo.Session Sess = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)xpObjectSpace).Session;
            String TitoloMessaggio = string.Empty;

            bool SpediscieMail = false;
            int RdLOid = 0;

            int conta = e.AcceptActionArgs.SelectedObjects.Count;//conta l'argonemto

            //IObjectSpace xpOSpaceRdLNote = Application.CreateObjectSpace(typeof(RdLNote));


            RdLNote rdlnote = null;


            //xpObjectSpace.CommitChanges();
            if (e.AcceptActionArgs.CurrentObject != null)
            {

                rdlnote = e.AcceptActionArgs.CurrentObject as RdLNote;//oggetto corrente della detailview ( l'argonemto)
                rdlnote.Save();
                rdlnote.Session.CommitTransaction();
                RdLOid = rdlnote.RdL.Oid;
                RdL rdl = rdlnote.RdL as RdL;
                rdl.RdLNotes.Add(rdlnote);
                rdl.Save();
                rdl.Session.CommitTransaction();
                SpediscieMail = true;


                // //xpObjectSpace.CommitChanges();
                // rdlnote = e.AcceptActionArgs.CurrentObject as RdLNote;//oggetto corrente della detailview ( l'argonemto)
                // RdLOid = rdlnote.RdL.Oid;
                //// xpOSpaceRdLNote.CommitChanges();
                // View.ObjectSpace.CommitChanges();
                // SpediscieMail = true;
            }
            //if (conta > 0)            non serve
            //{
            //    foreach (var item in e.AcceptActionArgs.SelectedObjects.Cast<DevExpress.Xpo.XPObject>())  // prende argomento
            //    {
            //        //int iOid = item.Oid;                    //var mm = xpObjectSpace.GetObjects(View.ObjectTypeInfo.Type, new InOperator("OidImpianto", item.Oid));
            //        //var ParDB = xpObjectSpace.FindObject<ReportExcel>(new BinaryOperator("ObjectType", View.ObjectTypeInfo.Type));
            //        //if (ParDB != null)
            //        //{
            //        //    var ValoreCampoLookUP = item.GetMemberValue(ParDB.CampoJoinLookUp);
            //        //    var cr = CriteriaOperator.Parse(ParDB.CampoObjectType + " == ?", ValoreCampoLookUP);
            //        //    var ListaPoi = xpObjectSpace.GetObjects(View.ObjectTypeInfo.Type, cr); // new InOperator("OidImpianto", item.Oid));

            //        //    }
            //        SpediscieMail = true;

            //    }
            //}
            // xpOSpaceRdl.CommitChanges();

            #region  ---------------  Trasmetto messaggio se necessario    -----------------------------
            // codice commentato 28/09/2019 e aggiunto su RdLObjSpace
            //if (SpediscieMail)
            //{
            //    try
            //    {
            //        //xpObjectSpace.CommitChanges();

            //        RdL myRdL = xpObjectSpace.GetObjectByKey<RdL>(RdLOid);

            //        string Messaggio = string.Empty;
            //        using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
            //        {
            //            im.InviaMessaggiRdLSolleciti(SetVarSessione.OracleConnString, Application.Security.UserName, myRdL.RegistroRdL.Oid, ref Messaggio);
            //        }
            //        // SetMessaggioWeb(string.Format("Descrizione Errore: ", ex.Message), "Trasmissione Avviso non Eseguita!!", InformationType.Error);
            //        if (!string.IsNullOrEmpty(Messaggio))
            //        {
            //            string Titolo = "Trasmissione Avviso Eseguita!!";
            //            string AlertMessaggio = string.Format("Messaggio di Spedizione - {0}", Messaggio);
            //            SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        SetMessaggioWeb(string.Format("Messaggio di Eccezione: {0};", ex.Message),
            //            "Trasmissione Avviso non Eseguita!!", InformationType.Error);
            //        //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
            //    }

            //}
            // codice commentato 28/09/2019

            #endregion

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

        private void pupWinAddNoteRdL_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var dvParent = (DetailView)(View.ObjectSpace).Owner;
            RdL rdl = (RdL)dvParent.CurrentObject;
            bool SpediscieMail = false;
            int myRdL_RegistroRdL_Oid = 0;
            List<RdLNote> allModificaRdLNote = new List<RdLNote>();
            RdLNote ModificaRdLNote = (RdLNote)e.PopupWindowViewCurrentObject;
            allModificaRdLNote.Add(ModificaRdLNote);
            RuleSetValidationResult result1 = Validator.RuleSet.ValidateAllTargets(View.ObjectSpace, allModificaRdLNote);
            if (rdl != null && result1.State == ValidationState.Valid)
            {
                if ((((e.PopupWindow)).View).SelectedObjects[0] != null)
                {
                    RdLNote rdlnote = ((RdLNote)(((e.PopupWindow)).View).SelectedObjects[0]);
                    rdl.RdLNotes.Add(rdlnote);
                    rdl.Save();

                    //    rdlnote.RdL = rdl;
                    //rdlnote.Save();
                    SpediscieMail = true;
                    myRdL_RegistroRdL_Oid = rdl.RegistroRdL.Oid;
                }
                #region  ---------------  Trasmetto messaggio se necessario    -----------------------------

                // codice commentato 28/09/2019 e aggiunto su RdLObjSpace
                //if (SpediscieMail)
                //{
                //    try
                //    {
                //        string Messaggio = string.Empty;
                //        using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                //        {
                //            im.InviaMessaggiRdLSolleciti(SetVarSessione.OracleConnString, Application.Security.UserName, myRdL_RegistroRdL_Oid, ref Messaggio);
                //        }
                //        if (!string.IsNullOrEmpty(Messaggio))
                //        {
                //            string Titolo = "Trasmissione Avviso Eseguita!!";
                //            string AlertMessaggio = string.Format("Messaggio di Spedizione - {0}", Messaggio);
                //            SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                //        }
                //        else
                //        {
                //            string Titolo = "Trasmissione Avviso Eseguita!!";
                //            string AlertMessaggio = string.Format("Salvare la RdL per la conferma Definitiva");
                //            SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);

                //        }
                //    }
                //    catch (Exception ex)
                //    {
                //        SetMessaggioWeb(string.Format("Messaggio di Eccezione: {0};", ex.Message),
                //            "Trasmissione Avviso non Eseguita!!", InformationType.Error);
                //    }
                //}
                // codice commentato 28/09/2019 e aggiunto su RdLObjSpace
                
            //View.ObjectSpace.Refresh();
                #endregion
            }
            else
            {
                var msg = result1.Results.Where(w => w.State == ValidationState.Invalid).Select(s => s.ErrorMessage).ToArray();
                string mm = string.Join(",", msg);
                string Titolo1 = "Canvalida non riuscita!!";
                string AlertMessaggio1 = string.Format("Dati non Validati: {0}", mm);
                SetMessaggioWeb(AlertMessaggio1, Titolo1, InformationType.Warning);
            }

        }

        private void pupWinAddNoteRdL_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (View.Id.Contains("RdL_RdLNotes_ListView"))
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id.Contains("RdL_DetailView"))
                {
                    //IObjectSpace xpObjectSpace = dvParent.ObjectSpace;
                    RdL rdl = (RdL)dvParent.CurrentObject;
                    RdLNote RdLNote = View.ObjectSpace.CreateObject<RdLNote>();

                    RdLNote.OidCommessa_Richiedente = rdl.Immobile.Contratti.Oid;

                    string RdLNoteDetail = "RdLNote_DetailView";
                    var user2 = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User;
                    int IsCliente = user2.Roles.Where(w => w.Name.StartsWith("CLIENTE_")).Count();

                    if (IsCliente > 0)
                    {
                        RdLNoteDetail = "RdLNote_DetailView_Cliente";
                    }
                    var view = Application.CreateDetailView(View.ObjectSpace, RdLNoteDetail, false, RdLNote);

                    view.Caption = string.Format("Richiesta di Lavoro");
                    view.ViewEditMode = ViewEditMode.Edit;
                    //Application.MainWindow.SetView(view);
                    e.View = view;
                    e.DialogController.SaveOnAccept = false;     
                }
            }
        }

        private void pupWinNewRichiedenteNote_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            RdLNote rdl = ((DetailView)View).CurrentObject as RdLNote;
            Richiedente Richiedente = ((Richiedente)(((e.PopupWindow)).View).SelectedObjects[0]);
            rdl.SetMemberValue("Richiedente", Richiedente);
        }

        private void pupWinNewRichiedenteNote_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            RdLNote rdlNote = ((DetailView)View).CurrentObject as RdLNote;
            if (View.Id.Contains("RdLNote_DetailView"))
            {
                Richiedente Richiedente = View.ObjectSpace.CreateObject<Richiedente>();
                //OidCommessa_Richiedente	3803	int
                Richiedente.Commesse = View.ObjectSpace.GetObjectByKey<CAMS.Module.DBPlant.Contratti>(rdlNote.OidCommessa_Richiedente);
                //Richiedente.Commesse = rdlNote.RdL.Immobile.Commesse;
                var view = Application.CreateDetailView(View.ObjectSpace, "Richiedente_DetailView_NewRdL", false, Richiedente);

                view.Caption = string.Format("Richiedente x Richiesta di Lavoro");
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
                e.DialogController.SaveOnAccept = false;
            }
        }

    }
}
