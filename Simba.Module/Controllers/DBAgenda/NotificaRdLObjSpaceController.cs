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
using CAMS.Module.DBAgenda;
using System.Diagnostics;

namespace CAMS.Module.Controllers.DBAgenda
{
    //            RisorseTeamObjSpaceController                For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class NotificaRdLObjSpaceController : ViewController
    {
        IObjectSpace os = null;
        List<string> PropertyObjectsCache = new List<string>();
        int annullato = 0;
        public NotificaRdLObjSpaceController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            View.ObjectSpace.Committing += ObjectSpace_Committing;
            View.ObjectSpace.Committed += ObjectSpace_Committed;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            try { View.ObjectSpace.Committing -= ObjectSpace_Committing; }
            catch { }
            try { View.ObjectSpace.Committed -= ObjectSpace_Committed; }
            catch { }
            try { View.ObjectSpace.ObjectChanged -= ObjectSpace_ObjectChanged; }
            catch { }
            base.OnDeactivated();
        }


        void ObjectSpace_Committing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            IObjectSpace os = (IObjectSpace)sender;
            for (int i = os.ModifiedObjects.Count - 1; i >= 0; i--)
            {
                object item = os.ModifiedObjects[i];
                Debug.WriteLine("elenco: " + PropertyObjectsCache.ToString());
                if (typeof(NotificaRdL).IsAssignableFrom(item.GetType()))
                {
                    NotificaRdL nrdl = (NotificaRdL)item;
                    if (nrdl.Label == 1)
                    {
                        if (nrdl.Resources.Count > 0)
                        {
                            int oidrtem = 0;
                            int oidrtnotifica = 0;
                            if (nrdl.RdL.RisorseTeam != null)
                                oidrtem = nrdl.RdL.RisorseTeam.Oid;

                            if (nrdl.Resources.Count > 0)
                                oidrtnotifica = nrdl.Resources[0].Oid;

                            if (oidrtem != oidrtnotifica && oidrtnotifica > 0)
                            {
                                nrdl.MessaggioUtente = string.Format("Modificata La Risosrsa Assegnata");
                            }
                        }

                        //nrdl.MessaggioUtente = string.Format("data Pianificata Aggiornata");
                    }
                    // se cambiato da view agenda scheduler
                    //if (View.Id == "NotificaRdL_ListView_SK")
                    //{
                    //    if (os.IsModified)//	true
                    //    {                            
                    //        NotificaRdL Not_RdL = (NotificaRdL)item;
                    //        if (Not_RdL.LabelListView == LabelListView.in_attesa_presa_in_carico)
                    //        {
                    //            annullato = 1;
                    //            SetMessaggioWeb("Non è consentito Modificare un Appuntamento nello stato di in attesa di presa in carico del tecnico!, Operazione Annullata!");
                    //            os.Rollback();
                    //             //SetMessaggioWeb("Non è consentito Modificare un Appuntamento nello stato di in attesa di presa in carico del tecnico!, Operazione Annullata!");
                    //            //View.Refresh();
                    //            //View.ObjectSpace.Refresh();
                    //        }                          
                    //    }
                    //}
                }
            }
        }

        // a 
        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            DateTime ultimadata = DateTime.MinValue;
            NotificaRdL notificardl = (NotificaRdL)View.CurrentObject;
            if (notificardl.GetType() == typeof(NotificaRdL) && annullato == 0)
            {
                //  statoAutorizzativo = 0 [quando si crea la RdL] 
                //  statoAutorizzativo = 1 [quando si crea la notifica]
                //  statoAutorizzativo = 2 [quando dichiara il tecnico]
                //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
                bool SpediscieMail = false;
                //System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
                string sbMessaggio = string.Empty;
                IObjectSpace xpOSpaceRdl = Application.CreateObjectSpace(typeof(RdL));
                NotificaRdL notificarRdL = xpOSpaceRdl.GetObject<NotificaRdL>(((NotificaRdL)notificardl));
                if (notificarRdL.RdL != null)
                {
                    string line = string.Empty;
                    RdL rdl = xpOSpaceRdl.GetObject<RdL>(notificarRdL.RdL);
                    int StatoxRegRdl = 0;
                    int NewLabelAutorizzativa = notificarRdL.Label;
                    switch (NewLabelAutorizzativa)
                    {
                        case 1:   // LABEL = 1 - CREATO NOTIFICA DA PLANNER SCHEDULER
                            rdl.DataPianificata = notificarRdL.StartOn;
                            rdl.DataPianificataEnd = notificarRdL.EndOn;

                            if (notificarRdL.Resources.Count() > 0)
                            {
                                RisorseTeam new_RTeam = xpOSpaceRdl.GetObjectByKey<RisorseTeam>(notificarRdL.Resources.FirstOrDefault().RisorseTeam.Oid);
                                if (new_RTeam != rdl.RisorseTeam)  ///   è stata cambiata la risorsa
                                {
                                    if (rdl.RisorseTeam != null)
                                    {
                                        RisorseTeam old_RTeam = rdl.RisorseTeam;
                                        old_RTeam.RegistroRdL = null;
                                        old_RTeam.Save();
                                    }
                                    rdl.RisorseTeam = new_RTeam;
                                    new_RTeam.RegistroRdL = rdl.RegistroRdL;
                                    new_RTeam.Save();
                                    line = string.Format("Cambio Risorsa Assegnata alla RdL nr.({0}), Assegnata la Risorsa {1}, liberata la risorsa {2}",
                                        rdl.Oid, notificarRdL.Resources[0].Caption, rdl.RisorseTeam.FullName);
                                    sbMessaggio = sbMessaggio + line;
                                }
                                else
                                {
                                    rdl.RisorseTeam = new_RTeam;
                                    new_RTeam.RegistroRdL = rdl.RegistroRdL;
                                    new_RTeam.Save();
                                    line = string.Format("RdL ({0}) è stata Assegnata alla Risorsa {1}", rdl.Oid, rdl.RisorseTeam.FullName);
                                    sbMessaggio = sbMessaggio + line;
                                }
                            
                            }
                            rdl.StatoAutorizzativo = xpOSpaceRdl.GetObjectByKey<StatoAutorizzativo>(1);
                            rdl.UltimoStatoSmistamento = xpOSpaceRdl.GetObjectByKey<StatoSmistamento>(2); // ASSEGNATA
                            rdl.UltimoStatoOperativo = xpOSpaceRdl.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                            rdl.DataAggiornamento = DateTime.Now;
                            SpediscieMail = true;
                            // AGGIORNA REG RDL    @@@   allinea REGISTRO RDL 
                            StatoxRegRdl = 1;
                            break;
                        case 2:   // LABEL=2 - il TECNICO HA DICHIARATO ORARIO DI ARRIVO                  
                            //rdl.StatoAutorizzativo = xpOSpaceRdl.GetObjectByKey<StatoAutorizzativo>(2);
                            //rdl.DataPrevistoArrivo = DateTime.Now.AddMinutes(125);
                            //rdl.DataAggiornamento = DateTime.Now;
                            //SpediscieMail = true;
                            //line = string.Format("RdL ({0}), il Tecnico ha dichiarato {1}", rdl.Oid, rdl.DataPrevistoArrivo);
                            //sbMessaggio = sbMessaggio + line;

                            //// AGGIORNA REG RDL    @@@   allinea REGISTRO RDL 
                            //StatoxRegRdl = 2;
                            break;
                        case 3:   // LABEL=3 -  la SO ha ACCETTATO L'ORARIO DEL TECNICO    
                            //rdl.DataPianificata = notificarRdL.StartOn;
                            //rdl.DataPianificataEnd = notificarRdL.EndOn;
                            //rdl.StatoAutorizzativo = xpOSpaceRdl.GetObjectByKey<StatoAutorizzativo>(3);
                            //rdl.DataAggiornamento = DateTime.Now;
                            //SpediscieMail = true;

                            //line = string.Format("RdL ({0}), è stata Accettata la Data Dichiarata dal Tecnico({1}), e Aggiornata la Data Pianificata", rdl.Oid, rdl.DataPrevistoArrivo);
                            //sbMessaggio = sbMessaggio + line;

                            //// AGGIORNA REG RDL    @@@   allinea REGISTRO RDL 
                            //StatoxRegRdl = 3;
                            break;
                        case 4:  // IL TECNICO HA PRESO IN CARICO L'INTERVENTO (IN TRASFERIMENTO)
                            //rdl.StatoAutorizzativo = xpOSpaceRdl.GetObjectByKey<StatoAutorizzativo>(4);
                            //rdl.UltimoStatoSmistamento = xpOSpaceRdl.GetObjectByKey<StatoSmistamento>(3); // emessa in lavorazione
                            //rdl.UltimoStatoOperativo = xpOSpaceRdl.GetObjectByKey<StatoOperativo>(1);// IN ATTESA DI ESSERE PRESA IN CARICO
                            //rdl.DataAggiornamento = DateTime.Now;
                            //SpediscieMail = true;

                            //StatoxRegRdl = 4;
                            break;

                        default:// altro non pervenuto

                            break;
                    }

                    // ALLINEA REGISTRO RDL
                    // AGGIORNA REG RDL    @@@   allinea REGISTRO RDL 
                    if (StatoxRegRdl > 0)
                    {
                        rdl.RegistroRdL.DataPianificata = notificarRdL.StartOn;
                        rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;
                        rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;
                        rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                        rdl.RegistroRdL.DataAggiornamento = rdl.DataAggiornamento;
                        if (StatoxRegRdl == 2)
                            rdl.RegistroRdL.DataPrevistoArrivo = DateTime.Now;
                    }
                    //----------------------------
                    rdl.Save();
                    xpOSpaceRdl.CommitChanges();

                    #region  ---------------  Trasmetto messaggio se necessario    -----------------------------
                    if (SpediscieMail)
                    {
                        try
                        {
                            string Messaggio = string.Empty;
                            using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                            {
                                im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, rdl.RegistroRdL.Oid, ref  Messaggio);
                            }
                           // SetMessaggioWeb(string.Format("Descrizione Errore: ", ex.Message), "Trasmissione Avviso non Eseguita!!", InformationType.Error);
                            if (!string.IsNullOrEmpty(Messaggio))
                            {
                                string Titolo = "Trasmissione Avviso Eseguita!!";
                                string AlertMessaggio = string.Format("Messaggio di Spedizione - {0}", Messaggio);
                                SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                            }
                        }
                        catch (Exception ex)
                        {
                            SetMessaggioWeb(string.Format("Messaggio di Eccezione: {0};", ex.Message),
                                "Trasmissione Avviso non Eseguita!!", InformationType.Error);                            
                            //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                        }

                    }
                    #endregion
                    
                    View.ObjectSpace.Refresh();
                }
            }
            else
            {
                View.Refresh();
            }
        }



        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            //System.Diagnostics.Debug.WriteLine(View.Id.ToString());
            //System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder(" ");
            if (e != null)
                // if (e.MemberInfo != null)
                // if ((e.MemberInfo).Owner.Type == typeof(RdL)) //	{Name = "RdL" FullName = "CAMS.Module.DBTask.RdL"}	System.Type {System.RuntimeType}
                foreach (var item in View.ObjectSpace.ModifiedObjects)
                {
                    annullato = 0;
                    if (!PropertyObjectsCache.Contains(e.PropertyName))
                    {
                        PropertyObjectsCache.Add(e.PropertyName);
                    }
                }
            //SetMessaggioWeb(sbMessaggio);
        }


        //private void SetMessaggioWeb(System.Text.StringBuilder Messaggio)
        //{
        //    MessageOptions options = new MessageOptions();
        //    options.Duration = 3000;
        //    options.Message = Messaggio.ToString();
        //    options.Web.Position = InformationPosition.Top;
        //    options.Type = InformationType.Success;
        //    options.Win.Caption = "Messaggio";             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
        //    Application.ShowViewStrategy.ShowMessage(options);
        //}

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


//string ris = string.Empty;  //OidTRisorsa
//if (e.PropertyName == "StartOn")
//{
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//    //throw new DevExpress.ExpressApp.UserFriendlyException("Occorre selezionare almeno una risorsa team");
//}

//if (e.PropertyName == "OidTRisorsa")
//{
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//}
//if (e.PropertyName == "TRisorsa_nonValida")
//{
//    //throw new DevExpress.ExpressApp.UserFriendlyException("Occorre selezionare almeno una risorsa team");
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//}

//if (e.PropertyName == "ResourceId_Old")
//{
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//}

//if (e.PropertyName == "ResourceId_Old")
//{
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//}

//if (e.PropertyName == "TRisorsa")
//{
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));

//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//}
//if (e.PropertyName == "TRisorsa_Old")
//{
//    sbMessaggio.AppendLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));

//    Debug.WriteLine(string.Format("{0} - {1} - {2}", e.PropertyName, e.OldValue, e.NewValue));
//}
