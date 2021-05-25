using CAMS.Module.Classi;
//using CAMS.Module.ClassiEsempio;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBControlliNormativi;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CAMS.Module.Controllers.DBControlliNormativi
{
    //NewMail
    public partial class LogEmailCtrlNormController : ViewController
    {
        bool TastoacButtonInvioWrite { get; set; }

        public LogEmailCtrlNormController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            TastoacButtonInvioWrite = false;
            using (CAMS.Module.Classi.UtilController uc = new CAMS.Module.Classi.UtilController())
            {
                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                TastoacButtonInvioWrite = uc.GetIsGrantedCreate(xpObjectSpace,"LogEmailCtrlNorm", "W");
            }
            ButtonInvio.Active.SetItemValue("Active", TastoacButtonInvioWrite);   
          
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void ButtonInvio_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
           // var cloneObjectController = Frame.GetController<CloneObjectViewController>(); 
            
            Session Sess = ((XPObjectSpace) ObjectSpace).Session;
            string Messaggio = "";
            if (xpObjectSpace != null)
            {
                var lstEmailSelezionate = (((ListView)View).Editor).GetSelectedObjects().Cast<LogEmailCtrlNorm>().ToList<LogEmailCtrlNorm>();

                foreach (LogEmailCtrlNorm EmailCorr in lstEmailSelezionate)
                {
                    try
                    {
                        LogEmailCtrlNorm EmailCorrente = xpObjectSpace.GetObject(EmailCorr);
                        List<CAMS.Module.DBAngrafica.Destinatari> LogDestinatari = EmailCorrente.LogEmailCtrlNormRifDestinataris.
                                                                                  Select(s=> s.Destinatari).ToList<CAMS.Module.DBAngrafica.Destinatari>();                      
                        ParametriNET par = xpObjectSpace.GetObjectByKey<ParametriNET>(1);
                        StringBuilder CorpoMail = new StringBuilder(EmailCorrente.Corpo.ToString(), 32000);

                         string  PathNameFileSave="";
                         string oggetto = string.Format("{0}-{1}","Re-Invio",EmailCorrente.Oggetto.ToString());
                        using (var ml = new CAMS.Module.Classi.Mail(par.ServerSMTP, par.PortaSMTP, par.UserSMTP, par.PwSMTP, par.MailFrom, par.MailToCC))
                        {

                            Messaggio = ml.ReInvioMailLogContrPeriodici(LogDestinatari, EmailCorrente.Oid, oggetto, CorpoMail, out PathNameFileSave);
                        }  //               ml.Dispose();
                        if (!string.IsNullOrWhiteSpace(PathNameFileSave))
                        {
                            LogEmailCtrlNorm NuovoLog = xpObjectSpace.CreateObject<LogEmailCtrlNorm>();
                            //LogEmailCtrlNorm NuovoLog = MappatoreDb.InstantiateLogEmailCtrlNorm(xpObjectSpace, CorpoMail.ToString());

                            NuovoLog.EsitoInvioMailSMS = EmailCorrente.EsitoInvioMailSMS;
                            NuovoLog.Corpo = CorpoMail.ToString();
                            NuovoLog.Oggetto = oggetto;
                            NuovoLog.EmailDest = EmailCorrente.EmailDest.ToString();
                            NuovoLog.MailCC = EmailCorrente.MailCC.ToString();
                            NuovoLog.MailFrom = EmailCorrente.MailFrom.ToString();
                            NuovoLog.Mittente = EmailCorrente.Mittente.ToString();
                            NuovoLog.FileMail = PathNameFileSave;
                            NuovoLog.DataInvio = DateTime.Now.ToString();
                            NuovoLog.InsertLogEmailCtrlNormRifDestinatari(ref NuovoLog, EmailCorrente.LogEmailCtrlNormRifDestinataris.ToList<LogEmailCtrlNormRifDestinatari>());
                            NuovoLog.InsertControlliNormativiRifLog(ref NuovoLog, EmailCorrente.ControlliNormativiRifLogS.ToList<ControlliNormativiRifLog>());
                                                 
                            NuovoLog.Save();
                            xpObjectSpace.CommitChanges();
                            View.Refresh();
                            View.ObjectSpace.Refresh();
                        }
                        else
                        {
                            if (xpObjectSpace != null)
                            {
                                var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                                Mess.Messaggio = Messaggio;
                                var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
                                view.ViewEditMode = ViewEditMode.View;
                                e.ShowViewParameters.CreatedView = view;
                                view.Caption = view.Caption + " - Esito Trasmissione Avviso Mail";
                                e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                                e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                                e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                            }
                        }


                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
                    }
                }
            }
        }
    
    }
}
