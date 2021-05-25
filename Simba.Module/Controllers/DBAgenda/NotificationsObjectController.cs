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
using DevExpress.ExpressApp.Notifications;
using System.Diagnostics;
using CAMS.Module.DBAgenda;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base.General;
using CAMS.Module.Classi;
using CAMS.Module.DBNotifiche;

namespace CAMS.Module.Controllers.DBAgenda
{
    //C:\AssemblaPRT17\EAMS\CAMS.Module\Controllers\DBAgenda\NotificationsObjectController.cs
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class NotificationsObjectController : ObjectViewController<DetailView, NotificationsObject>
    {
        private NotificationsService service;
        private SimpleAction VisualizzaRdLAction;
        int oidrdl = 0;
        int RdLDaAprire = 0;

        public NotificationsObjectController()
        {
            ////InitializeComponent();
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            service = Application.Modules.FindModule<NotificationsModule>().NotificationsService;
            NotificationsDialogViewController notificationsDialogViewController = Frame.GetController<NotificationsDialogViewController>();
            //var MM = service.NotificationsProviders; //      Provider della Nodifica

            if (service != null && notificationsDialogViewController != null)
            {
                notificationsDialogViewController.DismissAll.Active.SetItemValue("enable", false);
                notificationsDialogViewController.Dismiss.Active.SetItemValue("enable", true);
                //--------------------------------------------------------------------------
                notificationsDialogViewController.Dismiss.Executing += Dismiss_Executing; // prima che esegue
                notificationsDialogViewController.Dismiss.Executed += Dismiss_Executed;  // dopoche esegue
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            //if (true)
            //{
            //    //NotificaRdL nrdl = spacexpObjectSpace.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
            //    //RdL rdl = nrdl.RdL; //space.GetObject<RdL>(nrdl.RdL)
            //}
        }
        protected override void OnDeactivated()
        {
            try
            {
                NotificationsDialogViewController notificationsDialogViewController =
                                                  Frame.GetController<NotificationsDialogViewController>();
                notificationsDialogViewController.Dismiss.Executing -= Dismiss_Executing; // prima che esegue
                notificationsDialogViewController.Dismiss.Executed -= Dismiss_Executed;  // dopoche esegue
               
                service.ItemsProcessed -= Service_ItemsProcessed;
            }
            catch { }

            #region  codice escluso
            //NotificationsDialogViewController notificationsDialogViewController = Frame.GetController<NotificationsDialogViewController>();
            //if (notificationsDialogViewController != null)
            //{
            //    notificationsDialogViewController.Dismiss.Executing -= Dismiss_Executing;
            //    notificationsDialogViewController.Dismiss.Executed -= Dismiss_Executed;

            //    if (oidrdl > 0)
            //    {
            //        IObjectSpace xpObjectSpace = Application.CreateObjectSpace();

            //        NotificaRdL GetNotificaRdL = xpObjectSpace.GetObject<NotificaRdL>((NotificaRdL)View.CurrentObject);
            //        RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(oidrdl);  //GetNotificaRdL.RdL.Oid

            //        var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);

            //        view.Caption = string.Format("Richiesta di Lavoro");
            //        view.ViewEditMode = ViewEditMode.Edit;
            //        Application.MainWindow.SetView(view);
            //    }
            //}
            #endregion
            base.OnDeactivated();
        }
        // delegati di accettazione
        private void Dismiss_Executing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //if()
            //e.Cancel = true;
            service.ItemsProcessed += Service_ItemsProcessed;
        }
        private void Dismiss_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
        {
            service.ItemsProcessed -= Service_ItemsProcessed;
        }

        // tacitazione avviso
        private void Service_ItemsProcessed(object sender, NotificationItemsEventArgs e)
        {
            //  statoAutorizzativo = 0 [quando si crea la RdL] 
            //  statoAutorizzativo = 1 [quando si crea la notifica]
            //  statoAutorizzativo = 2 [quando dichiara il tecnico]
            //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
            //System.Text.StringBuilder Messaggio = new System.Text.StringBuilder("", 32000000);
            string Messaggio = string.Empty;
            //e.Handled = true;
            IObjectSpace spacexpObjectSpace = Application.CreateObjectSpace(typeof(NotificaRdL));     //IObjectSpace xpojs = Application.CreateObjectSpace(); 
            foreach (INotificationItem item in e.NotificationItems)
            {
                if (item.NotificationSource is NotificaRdL)
                {
                    #region per RdL Notifica
                    NotificaRdL nrdl = spacexpObjectSpace.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
                    RdL rdl = nrdl.RdL; //space.GetObject<RdL>(nrdl.RdL)
                    if ((rdl.StatoAutorizzativo.Oid == 3))  //|| (CAMS.Module.Classi.SetVarSessione.IsAdminRuolo)
                    {
                        int OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1;
                        int addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;                             persintalias
                        nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
                        nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
                        nrdl.LabelListView = (LabelListView)nrdl.Label;
                        nrdl.MessaggioUtente = string.Format("La Sala Operativa ha tacitato gli avvisi in data ({0})", DateTime.Now);
                        // annullo la data di avviso 
                        nrdl.StatusNotifica = TaskStatus.Completed;
                        ((ISupportNotifications)nrdl).AlarmTime = null;

                        //*******************************************************************************
                        nrdl.Save();
                        spacexpObjectSpace.CommitChanges();// salvo i cambiamenti nel datatabase oraCLE
                    }
                    else
                    {
                        Messaggio = string.Format("la seguente RdL {0} è nello stato {1}, non prevede l'accettazione della sala Operativa"
                                                         , nrdl.Oid, nrdl.LabelListView.ToString());

                    }
                    #endregion
                }
                if (item.NotificationSource is AvvisiSpedizioni)
                {
                    #region pert avvisis spedizioni
                    AvvisiSpedizioni avvSpedizioni = spacexpObjectSpace.GetObjectByKey<AvvisiSpedizioni>(item.NotificationSource.UniqueId);
                    int oidRdL = avvSpedizioni.RdLUnivoco;
                     Messaggio = string.Format("l'avviso di spedizione relativo alla RdL {0} è stato tacitato."    , oidRdL );
                    #endregion
                }
            }
            SetMessaggioWeb(Messaggio);
        }

        private void SetMessaggioWeb(string Messaggio)
        {
            MessageOptions options = new MessageOptions() { Duration = 4000, Message = Messaggio.ToString() };
            options.Web.Position = InformationPosition.Top;
            options.Type = InformationType.Success;
            options.Win.Caption = "Messaggio";             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
            Application.ShowViewStrategy.ShowMessage(options);
        }

           public int SetaddTempo(int OidSAtorizzativoNuovo, CAMS.Module.DBTask.RdL rdl)
        {
            string strTempo = rdl.Asset.Servizio.Immobile.Contratti.TempoLivelloAutorizzazioneGuasto;
            int addTempo = 5;
            if (strTempo != null)
            {
                string[] splitTo = strTempo.Split(new Char[] { ';' });
                try
                {
                    addTempo = Convert.ToInt32(splitTo[OidSAtorizzativoNuovo]);//   addTempo = Convert.ToInt32(splitTo[0]);
                }
                catch
                {
                    addTempo = 5;
                }
            }

            return addTempo;
        }

        public int SetLabelNotifica(CAMS.Module.DBTask.RdL RdL,
            CAMS.Module.DBAgenda.NotificaRdL nrdl,
            int oidAutorizzativoNuovo)  //  CAMS.Module.DBTask.StatoAutorizzativo sa)
        {
            /*//      imposta il nuovo tepo di allarme per il popap    
                   //  statoAutorizzativo = 0 [quando si crea la RdL] 
                    //  statoAutorizzativo = 1 [quando si crea la notifica] 
                    //  statoAutorizzativo = 2 [quando dichiara il tecnico]         **@@
                    //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]   
                    //  statoAutorizzativo = 4 [quando in trasferimento il tecnico] **@@   
               * */
            if (RdL != null) //Evaluate("RdL") != null)
            {
                int oidSmistamento = RdL.UltimoStatoSmistamento != null ? RdL.UltimoStatoSmistamento.Oid : 0;
                int oidOperativo = RdL.UltimoStatoOperativo != null ? RdL.UltimoStatoOperativo.Oid : 0;
                //int oidAutorizzativo = sa != null ? sa.Oid : 0;
                if (oidSmistamento == 1)
                    return 0;

                if (oidSmistamento == 2) // in attesa di assegnazione
                {
                    switch (oidAutorizzativoNuovo)
                    {
                        case 1: // trasferimento = 4                   
                            return 1;
                            break;
                        case 2:     //  in sito = 5
                            return 2;
                            break;
                        case 3:     //  sospeso =              
                            return 3;
                            break;
                        case 4:     //  sospeso =              
                            return 4;
                            break;
                        default:// altro non pervenuto
                            return 0;
                            break;
                    }
                }
                if (oidSmistamento == 3) // in lavorazione
                {

                    switch (oidOperativo)
                    {
                        case 1: // trasferimento = 4
                        case 3:
                        case 4:
                        case 5:
                            return 4;
                            break;
                        case 2:     //  in sito = 5
                            return 5;
                            break;
                        case 6:     //  sospeso = 
                        case 7:
                        case 8:
                        case 9:
                        case 10:
                            if (nrdl.DateCompleted == null)
                                return 6;
                            if (nrdl.DateCompleted != null)
                                return 8;
                            break;
                        default:// altro non pervenuto
                            return 0;
                            break;
                    }
                }

                if (oidSmistamento == 4) // in chiso
                {
                    return 7;
                }
                if (oidSmistamento == 10) // in chiso
                {
                    return 9;
                }
            }
            else
            {
                return 0;
            }

            return 0;
        }

    }

}


#region BackUP prima del 01072020 - attivazione send mail
//public partial class NotificationsObjectController : ObjectViewController<DetailView, NotificationsObject>
//{
//    private NotificationsService service;
//    private SimpleAction VisualizzaRdLAction;
//    int oidrdl = 0;
//    int RdLDaAprire = 0;

//    public NotificationsObjectController()
//    {
//        ////InitializeComponent();
//        //// Target required Views (via the TargetXXX properties) and create their Actions.
//        //VisualizzaRdLAction = new SimpleAction(this, "Approva Tutte Le Date", PredefinedCategory.Edit);
//        //VisualizzaRdLAction.SelectionDependencyType = SelectionDependencyType.RequireMultipleObjects;
//        //VisualizzaRdLAction.ImageName = "State_Task_Completed";
//        //VisualizzaRdLAction.ToolTip = "VApprova Tutte Le Date";
//        //VisualizzaRdLAction.Caption = "Approva Tutte Le Date";
//        ////VisualizzaRdLAction.Executing += ApprovaTutteLeDateAction_Executing;
//        ////VisualizzaRdLAction.Executed += ApprovaTutteLeDateAction_Executed;
//        //VisualizzaRdLAction.Execute += ApprovaTutteLeDateAction_Execute;
//    }
//    protected override void OnActivated()
//    {
//        base.OnActivated();

//        service = Application.Modules.FindModule<NotificationsModule>().NotificationsService;
//        NotificationsDialogViewController notificationsDialogViewController = Frame.GetController<NotificationsDialogViewController>();
//        var MM = service.NotificationsProviders;

//        if (service != null && notificationsDialogViewController != null)
//        {
//            notificationsDialogViewController.DismissAll.Active.SetItemValue("enable", false);
//            notificationsDialogViewController.Dismiss.Active.SetItemValue("enable", true);
//            //--------------------------------------------------------------------------
//            notificationsDialogViewController.Dismiss.Executing += Dismiss_Executing;
//            notificationsDialogViewController.Dismiss.Executed += Dismiss_Executed;
//            //notificationsDialogViewController.Actions.Add(VisualizzaRdLAction);
//            //VisualizzaRdLAction.Executing += VisualizzaRdLAction_Executing;
//            //VisualizzaRdLAction.Executed += VisualizzaRdLAction_Executed;
//        }
//    }
//    protected override void OnViewControlsCreated()
//    {
//        base.OnViewControlsCreated();
//        // Access and customize the target View control.
//        if (true)
//        {
//            //var v = View;
//            //NotificaRdL nrdl = spacexpObjectSpace.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
//            //RdL rdl = nrdl.RdL; //space.GetObject<RdL>(nrdl.RdL)

//        }
//    }
//    protected override void OnDeactivated()
//    {
//        #region  codice escluso
//        //NotificationsDialogViewController notificationsDialogViewController = Frame.GetController<NotificationsDialogViewController>();
//        //if (notificationsDialogViewController != null)
//        //{
//        //    notificationsDialogViewController.Dismiss.Executing -= Dismiss_Executing;
//        //    notificationsDialogViewController.Dismiss.Executed -= Dismiss_Executed;

//        //    if (oidrdl > 0)
//        //    {
//        //        IObjectSpace xpObjectSpace = Application.CreateObjectSpace();

//        //        NotificaRdL GetNotificaRdL = xpObjectSpace.GetObject<NotificaRdL>((NotificaRdL)View.CurrentObject);
//        //        RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(oidrdl);  //GetNotificaRdL.RdL.Oid

//        //        var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);

//        //        view.Caption = string.Format("Richiesta di Lavoro");
//        //        view.ViewEditMode = ViewEditMode.Edit;
//        //        Application.MainWindow.SetView(view);
//        //    }
//        //}
//        #endregion
//        base.OnDeactivated();
//    }
//    // delegati di accettazione
//    private void Dismiss_Executing(object sender, System.ComponentModel.CancelEventArgs e)
//    {
//        //if()
//        //e.Cancel = true;
//        service.ItemsProcessed += Service_ItemsProcessed;
//    }
//    private void Dismiss_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
//    {
//        service.ItemsProcessed -= Service_ItemsProcessed;
//    }


//    private void ApprovaTutteLeDateAction_Executing(object sender, System.ComponentModel.CancelEventArgs e)
//    {
//        service.ItemsProcessed += ApprovaService_ItemsProcessed;
//    }
//    private void ApprovaTutteLeDateAction_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
//    {
//        service.ItemsProcessed -= ApprovaService_ItemsProcessed;
//    }

//    private void ApprovaTutteLeDateAction_Execute(object sender, SimpleActionExecuteEventArgs e)
//    {
//        if (View.Id == "NotificationsObject_DetailView")
//        {
//            var myNotifications = ((DevExpress.ExpressApp.Notifications.NotificationsObject)View.CurrentObject).Notifications;
//            foreach (Notification nt in myNotifications)
//            {
//                long id = nt.ID;
//                if (nt.NotificationSource is NotificaRdL)
//                {
//                    CAMS.Module.DBAgenda.NotificaRdL ntrdl = ((CAMS.Module.DBAgenda.NotificaRdL)(nt.NotificationSource));              //{CAMS.Module.DBAgenda.NotificaRdL(1)}	CAMS.Module.DBAgenda.NotificaRdL
//                    if (ntrdl.Label == 2)
//                    {
//                        //ChengeNotificaRdL(ntrdl, "ASO");
//                    }
//                }
//            }
//        }
//    }
//    // accettazione
//    private void Service_ItemsProcessed(object sender, NotificationItemsEventArgs e)
//    {
//        //  statoAutorizzativo = 0 [quando si crea la RdL] 
//        //  statoAutorizzativo = 1 [quando si crea la notifica]
//        //  statoAutorizzativo = 2 [quando dichiara il tecnico]
//        //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
//        //System.Text.StringBuilder Messaggio = new System.Text.StringBuilder("", 32000000);
//        string Messaggio = string.Empty;
//        e.Handled = true;
//        IObjectSpace spacexpObjectSpace = Application.CreateObjectSpace(typeof(NotificaRdL));     //IObjectSpace xpojs = Application.CreateObjectSpace(); 
//        foreach (INotificationItem item in e.NotificationItems)
//        {
//            if (item.NotificationSource is NotificaRdL)
//            {
//                NotificaRdL nrdl = spacexpObjectSpace.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
//                RdL rdl = nrdl.RdL; //space.GetObject<RdL>(nrdl.RdL)
//                if ((rdl.StatoAutorizzativo.Oid == 3))  //|| (CAMS.Module.Classi.SetVarSessione.IsAdminRuolo)
//                {
//                    //using (Util u = new Util())
//                    //{
//                    //    Messaggio = u.SetNotificaRdL_Changes( ref nrdl, rdl, location,
//                    //        10, rdl.StatoAutorizzativo,
//                    //        DateTime.MinValue, tipoCambio);
//                    //}  ///*************************************************************************
//                    int OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1;
//                    int addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;                             persintalias
//                    nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
//                    nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
//                    nrdl.LabelListView = (LabelListView)nrdl.Label;
//                    nrdl.MessaggioUtente = string.Format("La Sala Operativa ha tacitato gli avvisi in data ({0})", DateTime.Now);
//                    // annullo la data di avviso 
//                    nrdl.StatusNotifica = TaskStatus.Completed;
//                    ((ISupportNotifications)nrdl).AlarmTime = null;

//                    //*******************************************************************************
//                    nrdl.Save();
//                    spacexpObjectSpace.CommitChanges();// salvo i cambiamenti nel datatabase oraCLE
//                }
//                else
//                {
//                    Messaggio = string.Format("la seguente RdL {0} è nello stato {1}, non prevede l'accettazione della sala Operativa"
//                                                     , nrdl.Oid, nrdl.LabelListView.ToString());

//                }
//                #region  ---------------  Trasmetto messaggio se necessario    -----------------------------

//                //if (true)//PropertyObjectsCache.Count > 0)
//                //{
//                //    if ((View.Id.Contains("RdL_DetailView_Gestione") || View.Id.Contains("RdL_DetailView_NuovoTT"))
//                //    && View.ObjectTypeInfo.Type == typeof(RdL))
//                //    {
//                //        try
//                //        {
//                //            //var xpObjectSpace = Application.CreateObjectSpace();
//                //            RdL RdL = rdl;// xpObjectSpace.GetObjectByKey<RdL>(Oid);

//                //            using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
//                //            {
//                //                im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, RdL.RegistroRdL.Oid, ref  Messaggio);
//                //            }
//                //            System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
//                //            sbMessaggio.AppendLine(string.Format("Trasmissione Avviso Eseguita!!"));
//                //            sbMessaggio.AppendLine(string.Format("Messaggio Trasmesso:", Messaggio));
//                //            SetMessaggioWeb(sbMessaggio);
//                //        }
//                //        catch (Exception ex)
//                //        {
//                //            System.Text.StringBuilder Messaggio = new System.Text.StringBuilder("", 32000000);
//                //            Messaggio.AppendLine(string.Format("Trasmissione Avviso non Eseguita!!"));
//                //            Messaggio.AppendLine(string.Format("Descrizione Errore:", ex.Message));
//                //            SetMessaggioWeb(Messaggio);

//                //            //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
//                //        }
//                //    }
//                //    //PropertyObjectsCache.Clear();
//                //}
//                #endregion
//                //var view = Application.CreateDetailView(xpojs, "RdL_DetailView_Gestione", true, objRdL);
//                //view.Caption = string.Format("Richiesta di Lavoro");
//                //view.ViewEditMode = ViewEditMode.Edit;
//                //Application.MainWindow.SetView(view);
//            }
//        }
//        SetMessaggioWeb(Messaggio);
//    }


//    private void SetMessaggioWeb(string Messaggio)
//    {
//        MessageOptions options = new MessageOptions() { Duration = 3000, Message = Messaggio.ToString() };
//        options.Web.Position = InformationPosition.Top;
//        options.Type = InformationType.Success;
//        options.Win.Caption = "Messaggio";             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
//        Application.ShowViewStrategy.ShowMessage(options);
//    }

//    private void ApprovaService_ItemsProcessed(object sender, NotificationItemsEventArgs e)
//    {
//        //  statoAutorizzativo = 0 [quando si crea la RdL] 
//        //  statoAutorizzativo = 1 [quando si crea la notifica]
//        //  statoAutorizzativo = 2 [quando dichiara il tecnico]
//        //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
//        //System.Text.StringBuilder Messaggio = new System.Text.StringBuilder("", 32000000);
//        string Messaggio = string.Empty;
//        //e.Handled = true;
//        IObjectSpace spacexpObjectSpace = Application.CreateObjectSpace(typeof(NotificaRdL));     //IObjectSpace xpojs = Application.CreateObjectSpace(); 
//        foreach (INotificationItem item in e.NotificationItems)
//        {
//            if (item.NotificationSource is NotificaRdL)
//            {
//                NotificaRdL nrdl = spacexpObjectSpace.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
//                RdL rdl = nrdl.RdL; //space.GetObject<RdL>(nrdl.RdL)
//                if ((rdl.StatoAutorizzativo.Oid == 2))  //|| (CAMS.Module.Classi.SetVarSessione.IsAdminRuolo)
//                {

//                    //int idsa = rdl.StatoAutorizzativo.Oid + 1;
//                    //idsa = (rdl.StatoAutorizzativo.Oid + 1) > space.GetObjects<StatoAutorizzativo>().Max(a => a.Oid) ? idsa = 1 : (rdl.StatoAutorizzativo.Oid + 1);
//                    //StatoAutorizzativo sa = space.GetObjectByKey<StatoAutorizzativo>(idsa);
//                    string Stato = "L3";
//                    int idsa = 0;
//                    StatoAutorizzativo sa = null;
//                    DateTime DataArrivoDichiarata = DateTime.MinValue;
//                    string location = "";
//                    string tipoCambio = "";
//                    //using (Util u = new Util())
//                    //{
//                    //    Messaggio = u.SetNotificaRdL_Changes(ref nrdl, rdl, location,
//                    //        10, rdl.StatoAutorizzativo,
//                    //        DateTime.MinValue, tipoCambio);
//                    //}
//                    nrdl.Save();
//                    spacexpObjectSpace.CommitChanges();// salvo i cambiamenti nel datatabase oraCLE
//                }
//                else
//                {
//                    Messaggio = string.Format("la seguente RdL {0} è nello stato {1}, non prevede l'accettazione della sala Operativa"
//                                                     , nrdl.Oid, nrdl.LabelListView.ToString());

//                }
//            }
//            else
//                e.Handled = true;

//        }
//        SetMessaggioWeb(Messaggio);
//    }


//    //private void ChengeNotificaRdL(NotificaRdL nrdl, string tipoCambio)
//    //{
//    //    //  statoAutorizzativo = 0 [quando si crea la RdL] 
//    //    //  statoAutorizzativo = 1 [quando si crea la notifica]
//    //    //  statoAutorizzativo = 2 [quando dichiara il tecnico]
//    //    //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
//    //    System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
//    //    IObjectSpace xpObjectSpace = View.ObjectSpace;
//    //    if (View is DetailView)
//    //    {
//    //        //DetailView dv = View as DetailView;
//    //        //if (View.ObjectTypeInfo.Type == typeof(NotificaRdL))//.Editor).GetSelectedObjects().Count > 0)
//    //        //{  //NotificaRdL nrdl = (NotificaRdL)dv.CurrentObject; RdL rdl = nrdl.RdL;//xpObjectSpace.GetObjectByKey<RdL>(rdl.Oid)
//    //        RdL rdl = nrdl.RdL;
//    //        string location = "";
//    //        if (rdl != null)
//    //        {
//    //            using (Util u = new Util())
//    //            {
//    //                sbMessaggio = u.SetNotificaRdL_Changes(
//    //                    ref nrdl, rdl, location, 10, rdl.StatoAutorizzativo, DateTime.MinValue, tipoCambio);
//    //            }
//    //            nrdl.Save();
//    //            xpObjectSpace.CommitChanges();
//    //        }
//    //        //}
//    //    }
//    //}



//    public int SetaddTempo(int OidSAtorizzativoNuovo, CAMS.Module.DBTask.RdL rdl)
//    {
//        string strTempo = rdl.Apparato.Impianto.Immobile.Commesse.TempoLivelloAutorizzazioneGuasto;
//        int addTempo = 5;
//        if (strTempo != null)
//        {
//            string[] splitTo = strTempo.Split(new Char[] { ';' });
//            try
//            {
//                addTempo = Convert.ToInt32(splitTo[OidSAtorizzativoNuovo]);//   addTempo = Convert.ToInt32(splitTo[0]);
//            }
//            catch
//            {
//                addTempo = 5;
//            }
//        }

//        return addTempo;
//    }


//    public int SetLabelNotifica(CAMS.Module.DBTask.RdL RdL,
//        CAMS.Module.DBAgenda.NotificaRdL nrdl,
//        int oidAutorizzativoNuovo)  //  CAMS.Module.DBTask.StatoAutorizzativo sa)
//    {
//        /*//      imposta il nuovo tepo di allarme per il popap    
//               //  statoAutorizzativo = 0 [quando si crea la RdL] 
//                //  statoAutorizzativo = 1 [quando si crea la notifica] 
//                //  statoAutorizzativo = 2 [quando dichiara il tecnico]         **@@
//                //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]   
//                //  statoAutorizzativo = 4 [quando in trasferimento il tecnico] **@@   
//           * */
//        if (RdL != null) //Evaluate("RdL") != null)
//        {
//            int oidSmistamento = RdL.UltimoStatoSmistamento != null ? RdL.UltimoStatoSmistamento.Oid : 0;
//            int oidOperativo = RdL.UltimoStatoOperativo != null ? RdL.UltimoStatoOperativo.Oid : 0;
//            //int oidAutorizzativo = sa != null ? sa.Oid : 0;
//            if (oidSmistamento == 1)
//                return 0;

//            if (oidSmistamento == 2) // in attesa di assegnazione
//            {
//                switch (oidAutorizzativoNuovo)
//                {
//                    case 1: // trasferimento = 4                   
//                        return 1;
//                        break;
//                    case 2:     //  in sito = 5
//                        return 2;
//                        break;
//                    case 3:     //  sospeso =              
//                        return 3;
//                        break;
//                    case 4:     //  sospeso =              
//                        return 4;
//                        break;
//                    default:// altro non pervenuto
//                        return 0;
//                        break;
//                }
//            }
//            if (oidSmistamento == 3) // in lavorazione
//            {

//                switch (oidOperativo)
//                {
//                    case 1: // trasferimento = 4
//                    case 3:
//                    case 4:
//                    case 5:
//                        return 4;
//                        break;
//                    case 2:     //  in sito = 5
//                        return 5;
//                        break;
//                    case 6:     //  sospeso = 
//                    case 7:
//                    case 8:
//                    case 9:
//                    case 10:
//                        if (nrdl.DateCompleted == null)
//                            return 6;
//                        if (nrdl.DateCompleted != null)
//                            return 8;
//                        break;
//                    default:// altro non pervenuto
//                        return 0;
//                        break;
//                }
//            }

//            if (oidSmistamento == 4) // in chiso
//            {
//                return 7;
//            }
//            if (oidSmistamento == 10) // in chiso
//            {
//                return 9;
//            }
//        }
//        else
//        {
//            return 0;
//        }

//        return 0;
//    }


//}

#endregion BackUP prima del 01072020 - attivazione send mail



#region codice di esempio 
//var  unico = space.GetObjectHandle(item.NotificationSource);
//NotificaRdL nfrdl = (NotificaRdL)space.GetObjectByHandle(item.NotificationSource);
//space.GetObject(item.NotificationSource);
//var  unico = space.GetObjectHandle(item.NotificationSource);
//NotificaRdL nfrdl = (NotificaRdL)space.GetObjectByHandle(item.NotificationSource);  
//private void Service_VediRdL_ItemsProcessed(object sender, NotificationItemsEventArgs e)
//  {
//      IObjectSpace space = Application.CreateObjectSpace(typeof(NotificaRdL));
//      foreach (INotificationItem item in e.NotificationItems)
//      {
//          if (item.NotificationSource is NotificaRdL)
//          {
//              NotificaRdL GetNotificaRdL = space.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
//              IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
//              RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetNotificaRdL.RdL.Oid);

//              var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);

//              view.Caption = string.Format("Richiesta di Lavoro");
//              view.ViewEditMode = ViewEditMode.Edit;
//              Application.MainWindow.SetView(view);
//          }

//      }
//  }

///////------------------#####################################################################################

//public partial class NotificationsObjectController : ObjectViewController<DetailView, NotificationsObject>
//{
//    private NotificationsService service;

//    public NotificationsObjectController()
//    {
//        // InitializeComponent();

//    }

//    protected override void OnActivated()
//    {
//        base.OnActivated();
//        service = Application.Modules.FindModule<NotificationsModule>().NotificationsService;
//        NotificationsDialogViewController notificationsDialogViewController = Frame.GetController<NotificationsDialogViewController>();
//        if (service != null && notificationsDialogViewController != null)
//        {
//            notificationsDialogViewController.Dismiss.Executing += Dismiss_Executing;
//            notificationsDialogViewController.Dismiss.Executed += Dismiss_Executed;
//        }

//    }

//    private void Dismiss_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
//    {
//        service.ItemsProcessed -= Service_ItemsProcessed;
//    }

//    //private void Dismiss_Executed(object sender, DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
//    //{
//    //    var oggetti_selezionati = View.SelectedObjects;

//    //    int vvv = oggetti_selezionati.Count;

//    //    //var lstRisorseSelezionate = ((List<RisorseDistanzeRdL>)((((DevExpress.ExpressApp.Frame)
//    //    //    (e.PopupWindow)).View).SelectedObjects.Cast<RisorseDistanzeRdL>().ToList<RisorseDistanzeRdL>()));
//    //    Debug.WriteLine(View.SelectedObjects.Count);

//    //    List<NotificaRdL> List = View.SelectedObjects.Cast<NotificaRdL>().ToList<NotificaRdL>();

//    //    var NotificaSelezionata = (NotificaRdL)List[0];
//    //    NotificaSelezionata.dateCompleted = DateTime.Now;
//    //    NotificaSelezionata.RemindIn = null;
//    //    NotificaSelezionata.Status = DevExpress.Persistent.Base.General.TaskStatus.Completed;
//    //    NotificaSelezionata.Save();
//    //    View.ObjectSpace.CommitChanges();

//    //    IObjectSpace xpojs = Application.CreateObjectSpace();  ///  creo la objectspace (cioè la connessione al DB)
//    //    RdL objRdL = xpojs.GetObject<RdL>(NotificaSelezionata.RdL); // recupero la rdl associata alla notifica

//    //    objRdL.StatoAutorizzativo = xpojs.GetObjectByKey<StatoAutorizzativo>(4);// assegno lo stato autorizzativo = Approvata da SO 

//    //    objRdL.Save();// salvo i cambiamenti in memoria (in memoria dell'applicazione)
//    //    xpojs.CommitChanges();// salvo i cambiamenti nel datatabase oraCLE
//    //}

//    //protected override void OnViewControlsCreated()
//    //{
//    //    base.OnViewControlsCreated();
//    //    // Access and customize the target View control.
//    //}
//    protected override void OnDeactivated()
//    {
//        NotificationsDialogViewController notificationsDialogViewController =Frame.GetController<NotificationsDialogViewController>();
//        if (notificationsDialogViewController != null)
//        {
//            notificationsDialogViewController.Dismiss.Executing -= Dismiss_Executing;
//            notificationsDialogViewController.Dismiss.Executed -= Dismiss_Executed;
//        }
//        base.OnDeactivated();
//    }

//    private void Dismiss_Executing(object sender, System.ComponentModel.CancelEventArgs e)
//    {
//        service.ItemsProcessed += Service_ItemsProcessed;
//    }

//    private void Service_ItemsProcessed(object sender, DevExpress.Persistent.Base.General.NotificationItemsEventArgs e)
//    {
//        IObjectSpace space = Application.CreateObjectSpace(typeof(NotificaRdL));
//        foreach (INotificationItem item in e.NotificationItems)
//        {
//            if (item.NotificationSource is NotificaRdL)
//            {
//                 //var  unico = space.GetObjectHandle(item.NotificationSource);
//                //NotificaRdL nfrdl = (NotificaRdL)space.GetObjectByHandle(item.NotificationSource);
//                //space.GetObject(item.NotificationSource);
//                //var  unico = space.GetObjectHandle(item.NotificationSource);
//                //NotificaRdL nfrdl = (NotificaRdL)space.GetObjectByHandle(item.NotificationSource);

//                NotificaRdL nfrdl = space.GetObjectByKey<NotificaRdL>(item.NotificationSource.UniqueId);
//                nfrdl.RemindIn = null;
//                nfrdl.dateCompleted = DateTime.Now;
//                nfrdl.Status = DevExpress.Persistent.Base.General.TaskStatus.Completed;
//                nfrdl.Save();
//                string xvv = "vvvfff";
//                IObjectSpace xpojs = Application.CreateObjectSpace();  ///  creo la objectspace (cioè la connessione al DB)
//                RdL objRdL = xpojs.GetObject<RdL>(nfrdl.RdL); // recupero la rdl associata alla notifica


//                objRdL.StatoAutorizzativo = xpojs.GetObjectByKey<StatoAutorizzativo>(4);// assegno lo stato autorizzativo = Approvata da SO 
//                objRdL.Save();// salvo i cambiamenti in memoria (in memoria dell'applicazione)
//                xpojs.CommitChanges();// salvo i cambiamenti nel datatabase oraCLE



//                #region  ---------------  Trasmetto messaggio se necessario    -----------------------------

//                //if (true)//PropertyObjectsCache.Count > 0)
//                //{
//                //    if ((View.Id.Contains("RdL_DetailView_Gestione") || View.Id.Contains("RdL_DetailView_NuovoTT"))
//                //    && View.ObjectTypeInfo.Type == typeof(RdL))
//                //    {
//                //        try
//                //        {
//                //            //var xpObjectSpace = Application.CreateObjectSpace();
//                //            RdL RdL = rdl;// xpObjectSpace.GetObjectByKey<RdL>(Oid);
//                //            string Messaggio = string.Empty;
//                //            using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
//                //            {
//                //                im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, RdL.RegistroRdL.Oid, ref  Messaggio);
//                //            }
//                //            System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
//                //            sbMessaggio.AppendLine(string.Format("Trasmissione Avviso Eseguita!!"));
//                //            sbMessaggio.AppendLine(string.Format("Messaggio Trasmesso:", Messaggio));
//                //            SetMessaggioWeb(sbMessaggio);
//                //        }
//                //        catch (Exception ex)
//                //        {
//                //            System.Text.StringBuilder Messaggio = new System.Text.StringBuilder("", 32000000);
//                //            Messaggio.AppendLine(string.Format("Trasmissione Avviso non Eseguita!!"));
//                //            Messaggio.AppendLine(string.Format("Descrizione Errore:", ex.Message));
//                //            SetMessaggioWeb(Messaggio);

//                //            //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
//                //        }
//                //    }

//                //    PropertyObjectsCache.Clear();
//                //}
//                #endregion




//            }
//        }
//        space.CommitChanges();
//    }

//    private void SetMessaggioWeb(System.Text.StringBuilder Messaggio)
//    {
//        MessageOptions options = new MessageOptions();
//        options.Duration = 3000;
//        options.Message = Messaggio.ToString();
//        options.Web.Position = InformationPosition.Top;
//        options.Type = InformationType.Success;
//        options.Win.Caption = "Messaggio";             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
//        Application.ShowViewStrategy.ShowMessage(options);
//    }

//}



//////////////////////////////

//public class DeleteOnDismissController : ObjectViewController<DetailView, NotificationsObject>
//{
//    //private NotificationsService service;
//    protected override void OnActivated()
//    {
//        base.OnActivated();
//        //        service = Application.Modules.FindModule<NotificationsModule>().NotificationsService;
//        //        NotificationsDialogViewController notificationsDialogViewController =
//        //Frame.GetController<NotificationsDialogViewController>();
//        //        if (service != null && notificationsDialogViewController != null)
//        //        {
//        //            notificationsDialogViewController.Dismiss.Executing += Dismiss_Executing;
//        //            notificationsDialogViewController.Dismiss.Executed += Dismiss_Executed;
//        //        }
//    }
//    protected override void OnDeactivated()
//    {
////        NotificationsDialogViewController notificationsDialogViewController =
////Frame.GetController<NotificationsDialogViewController>();
////        if (notificationsDialogViewController != null)
////        {
////            notificationsDialogViewController.Dismiss.Executing -= Dismiss_Executing;
////            notificationsDialogViewController.Dismiss.Executed -= Dismiss_Executed;
////        }
//        base.OnDeactivated();
//    }
//    //private void Dismiss_Executing(object sender, System.ComponentModel.CancelEventArgs e)
//    //{
//    //    service.ItemsProcessed += Service_ItemsProcessed;
//    //}

//    //private void Service_ItemsProcessed(object sender,DevExpress.Persistent.Base.General.NotificationItemsEventArgs e)
//    //{
//    //    IObjectSpace space = Application.CreateObjectSpace(typeof(MyNotification));
//    //    foreach (INotificationItem item in e.NotificationItems)
//    //    {
//    //        if (item.NotificationSource is MyNotification)
//    //        {
//    //            space.Delete(space.GetObject(item.NotificationSource));
//    //        }
//    //    }
//    //    space.CommitChanges();
//    //}
//    //private void Dismiss_Executed(object sender,DevExpress.ExpressApp.Actions.ActionBaseEventArgs e)
//    //{
//    //    service.ItemsProcessed -= Service_ItemsProcessed;
//    //}
//}
#endregion