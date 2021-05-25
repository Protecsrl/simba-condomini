using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using RS;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegistroSpedizioniDettControlle1 : ViewController
    {
        public RegistroSpedizioniDettControlle1()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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

        private void RdLInvioMail_Execute(object sender, SimpleActionExecuteEventArgs e)
        {


            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (View is DetailView)
            {
                RegistroSpedizioniDett vRegistroSpedizioniDett = (RegistroSpedizioniDett)View.CurrentObject;
                if (vRegistroSpedizioniDett.RegistroRdL.Oid != -1)
                {
                    int OidRegRdL = vRegistroSpedizioniDett.RegistroRdL.Oid;
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

        private void ControllaStato_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            string smsid = string.Empty;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    var SelRegSpDett = xpObjectSpace.CreateObject<RegistroSpedizioniDett>();
                    smsid = SelRegSpDett.SMSID;
                    setStatusSMS(smsid, SelRegSpDett);
                }

                if (View is ListView)
                {

                    var lstIdSelezionati = (((ListView)View).Editor).GetSelectedObjects().
                        Cast<RegistroSpedizioniDett>().Select(s => new { sms_Id = s.SMSID, Oid = s.Oid } ).ToList();
                    foreach (var sms_id in lstIdSelezionati)
                    {
                        setStatusSMS(sms_id.sms_Id, xpObjectSpace.GetObjectByKey< RegistroSpedizioniDett>(sms_id.Oid));
                    }

                }
            }
        }

        void setStatusSMS(string SMSID, RegistroSpedizioniDett RegSpDett)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            EsitoInvioMailSMS vEsitoInvioMailSMS = EsitoInvioMailSMS.ERROR;
            SMSCConnection smsc_connection = null;
            try
            {
                smsc_connection = new SMSCHTTPConnection();
            }
            catch (SMSCException smsc_ex)
            {
                System.Console.WriteLine("errore in connessione a Aruba: " + smsc_ex.Message);   //System.err.println(“errore in connessione a Aruba: “+		smsc_ex.getMessage());
                return;
            }
            // operazioni sulla connessione
            try
            {
                List<MessageStatus> smsstatus = smsc_connection.getMessageStatus(SMSID);
                foreach (MessageStatus message_status in smsstatus)
                {
                    System.Console.WriteLine("destinatario :" + message_status.Recipient + ": ");
                    if (message_status.IsError)
                    {
                        System.Console.WriteLine("errore!");
                        vEsitoInvioMailSMS = EsitoInvioMailSMS.ERROR;
                    }
                    else
                    {
                        if (message_status.Status == MessageStatus.SMSStatus.DLVRD)
                        {
                            System.Console.WriteLine("consegnato.");
                            vEsitoInvioMailSMS = EsitoInvioMailSMS.CONSEGNATO_SMSGen;

                            var SMSdettID = RegSpDett.RegSpedizioniSMSDetts.ToList().
                                            Where(w => w.DestinatarioSMS == message_status.Recipient.ToString())
                                            .Select(s=>s.Oid);
                            RegistroSpedizioniSMSDett rssms = xpObjectSpace.GetObjectByKey<RegistroSpedizioniSMSDett>(SMSdettID);
                            rssms.EsitoInvioMailSMS = (EsitoInvioMailSMS)message_status.Status;
                            rssms.DataRicezione = message_status.DeliveryDate;
                            rssms.Save();
                        }
                        else
                        {
                            System.Console.WriteLine("in attesa...");
                            vEsitoInvioMailSMS = EsitoInvioMailSMS.INATTESA_SMSGen;
                            var SMSdettID = RegSpDett.RegSpedizioniSMSDetts.ToList().
                                         Where(w => w.DestinatarioSMS == message_status.Recipient.ToString())
                                         .Select(s => s.Oid);
                            RegistroSpedizioniSMSDett rssms = xpObjectSpace.GetObjectByKey<RegistroSpedizioniSMSDett>(SMSdettID);
                            rssms.EsitoInvioMailSMS = (EsitoInvioMailSMS)message_status.Status;
                            rssms.Save();
                        }
                    }
                }
            }

            catch (SMSCRemoteException smscre)
            {
                System.Console.WriteLine("Exception from Aruba: " + smscre.Message);
                vEsitoInvioMailSMS = EsitoInvioMailSMS.ERROR;
            }

            smsc_connection.logout();  // nel caso di HTTP il metodo non lancia mai eccezioni        
            string SMS_ID = string.Empty;
            if (xpObjectSpace != null)
            {
                RegSpDett.EsitoInvioMailSMS = vEsitoInvioMailSMS;
                xpObjectSpace.CommitChanges();
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
    
    }
}
