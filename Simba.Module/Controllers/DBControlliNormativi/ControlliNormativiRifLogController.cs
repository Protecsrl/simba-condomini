using CAMS.Module.Classi;
//using CAMS.Module.ClassiEsempio;
using CAMS.Module.ClassiMSDB;
using CAMS.Module.ClassiORADB;
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
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ControlliNormativiRifLogController : ViewController
    {
        public ControlliNormativiRifLogController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View.Id == "ControlliNormativi_ControlliNormativiRifLogS_ListView")
                this.ReInviaMail.Active.SetItemValue("Active", true);
            else
                this.ReInviaMail.Active.SetItemValue("Active", false);

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

        private void ReInviaMail_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            // var cloneObjectController = Frame.GetController<CloneObjectViewController>(); 
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            StringBuilder Messaggio = new StringBuilder("", 32000);
            if (xpObjectSpace != null && View.Id == "ControlliNormativi_ControlliNormativiRifLogS_ListView")
            {
                var lstSelControlliNormativiRifLog = (((ListView)View).Editor).GetSelectedObjects().Cast<ControlliNormativiRifLog>().ToList<ControlliNormativiRifLog>();
                foreach (ControlliNormativiRifLog CoNoRifLog in lstSelControlliNormativiRifLog)
                {
                    try
                    {
                        LogEmailCtrlNorm EmailCorrente = xpObjectSpace.GetObject(CoNoRifLog.LogEmailCtrlNorm);
                        List<CAMS.Module.DBAngrafica.Destinatari> LogDestinatari = EmailCorrente.LogEmailCtrlNormRifDestinataris.
                                                                                  Select(s => s.Destinatari).ToList<CAMS.Module.DBAngrafica.Destinatari>();
                        ParametriNET par = xpObjectSpace.GetObjectByKey<ParametriNET>(1);
                        StringBuilder CorpoMail = new StringBuilder(EmailCorrente.Corpo.ToString(), 32000);
                        //StringBuilder CorpoMail = new StringBuilder(MappatoreDb.GetCorpo2(EmailCorrente), 32000);

                        string PathNameFileSave = "";
                        string oggetto = string.Format("{0}-{1}", "Re-Invio", EmailCorrente.Oggetto.ToString());
                        using (var ml = new CAMS.Module.Classi.Mail(par.ServerSMTP, par.PortaSMTP, par.UserSMTP, par.PwSMTP, par.MailFrom, par.MailToCC))
                        {
                            Messaggio.AppendLine(ml.ReInvioMailLogContrPeriodici(LogDestinatari, EmailCorrente.Oid, oggetto, CorpoMail, out PathNameFileSave));
                        }  //               ml.Dispose();
                        if (!string.IsNullOrWhiteSpace(PathNameFileSave))
                        {
                            LogEmailCtrlNorm NuovoLog = xpObjectSpace.CreateObject<LogEmailCtrlNorm>();
                            //LogEmailCtrlNorm NuovoLog = MappatoreDb.InstantiateLogEmailCtrlNorm(xpObjectSpace, "corpo2")

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

                            //MappatoreDb.SaveCorpo2(NuovoLog);
                            NuovoLog.Save();
                            
                            xpObjectSpace.CommitChanges();
                            View.Refresh();
                            View.ObjectSpace.Refresh();
                        }
                        else
                        {
                            Messaggio.AppendLine("Email Non Inviata Correttamente");
                        }
                    }
                    catch (Exception ex)
                    {
                        throw new Exception(string.Format("Errore: Operazione DB non eseguita Correttamente e con Avvertimenti;" + ex.Message));
                    }
                }

                if (xpObjectSpace != null)
                {
                    var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                    Mess.Messaggio = Messaggio.ToString();
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
    }
}
