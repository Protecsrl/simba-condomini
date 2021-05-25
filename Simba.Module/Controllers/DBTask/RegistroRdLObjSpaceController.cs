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
using CAMS.Module.DBAgenda;
using CAMS.Module.Classi;
using System.ComponentModel;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegistroRdLObjSpaceController : ViewController
    {
        bool suppressOnChanged = false;
        bool suppressOnCommitting = false;
        bool suppressOnCommitted = false;
        private DateTime Adesso = DateTime.Now;
        public RegistroRdLObjSpaceController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
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
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        IObjectSpace os = null;
        //List<StatoSmistamento> SmistamentoObjectsCache = new List<StatoSmistamento>();
        List<int> SmistamentoOid_ObjectsCache = new List<int>();
        List<StatoOperativo> OperativoObjectsCache = new List<StatoOperativo>();
        List<RisorseTeam> RisorsaTeamObjectsCache = new List<RisorseTeam>();
        List<string> PropertyObjectsCache = new List<string>();
        StatoOperativo old_StatoOperativo = null;
        RisorseTeam old_RisorseTeam = null;
        StatoSmistamento old_StatoSmistamento = null;


        bool nuovoRegistroRdL = false;
        bool NuovaModificaSuRDLCorrente = false;

        void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            suppressOnChanged = true;

            List<string> sbMessaggio = new List<string>();
            IObjectSpace os = (IObjectSpace)sender;
            for (int i = os.ModifiedObjects.Count - 1; i >= 0; i--)
            {
                object item = os.ModifiedObjects[i];
                if (typeof(RegistroRdL).IsAssignableFrom(item.GetType()))
                {
                    this.SmistamentoOid_ObjectsCache.Clear();
                    this.OperativoObjectsCache.Clear();
                    this.RisorsaTeamObjectsCache.Clear();
                    NuovaModificaSuRDLCorrente = false;
                    nuovoRegistroRdL = false;
                    RegistroRdL RegRdl = (RegistroRdL)item;// View.CurrentObject;    @@@       IObjectSpace os = (IObjectSpace)sender;
                    RegRdl.DataAggiornamento = DateTime.Now;
                    //old_StatoSmistamento = RegRdl.old_SSmistamento;
                    foreach (RdL rdl in RegRdl.RdLes)
                    {
                        if (rdl != null) //   non e nuova è una modifica ( compreso assegazione e quindi notifica )
                        {
                            if (rdl.UltimoStatoSmistamento.Oid == 7) //7	Annullato 
                                continue;

                            rdl.DataAggiornamento = RegRdl.DataAggiornamento;
                            rdl.UtenteUltimo = SecuritySystem.CurrentUserName;

                            if (rdl.DataPianificata != RegRdl.DataPianificata)
                            {
                                rdl.DataPianificata = RegRdl.DataPianificata;
                            }

                            if (rdl.RisorseTeam != RegRdl.RisorseTeam && RegRdl.RisorseTeam != null)
                            {
                                rdl.RisorseTeam = RegRdl.RisorseTeam;
                            }

                            if (RegRdl.UltimoStatoSmistamento != rdl.UltimoStatoSmistamento) // creo il relativo RegistroRdL della RdL nuova
                            {
                                rdl.UltimoStatoSmistamento = RegRdl.UltimoStatoSmistamento;
                            }

                            if (RegRdl.UltimoStatoOperativo != rdl.UltimoStatoOperativo) // creo il relativo RegistroRdL della RdL nuova
                            {
                                rdl.UltimoStatoOperativo = RegRdl.UltimoStatoOperativo;
                            }


                            if (!string.IsNullOrEmpty(RegRdl.NoteCompletamento))
                                rdl.NoteCompletamento = RegRdl.NoteCompletamento;

                            if (RegRdl.DataCompletamento != null)
                                if (RegRdl.DataCompletamento > DateTime.MinValue )
                                {
                                    rdl.DataCompletamento = RegRdl.DataCompletamento;
                                    rdl.DataCompletamentoSistema = DateTime.Now;
                                }


                            #region crea notifica  se i nuovi smistamento/operativo sono quelli necessari
                            int addTempo = 5;
                            if (rdl.Categoria.Oid == 4  &&
                                rdl.Asset.Servizio.Immobile.Contratti.LivelloAutorizzazioneGuasto > 0)
                            {
                                NotificaRdL nrdl = null;
                                if (rdl.UltimoStatoSmistamento.Oid == 2 && rdl.UltimoStatoOperativo.Oid == 19)
                                {
                                    //  statoAutorizzativo = 0 [quando si crea la RdL] 
                                    //  statoAutorizzativo = 1 [quando si crea la notifica]
                                    //  statoAutorizzativo = 2 [quando dichiara il tecnico]
                                    //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
                                    //  se la commessa è configurata con livello autorizzativo maggiore di 0 allora crea la notifica                                
                                    //int conta = View.ObjectSpace.GetObjects<NotificaRdL>().Where(w => w.RdL == rdl && rdl.StatoAutorizzativo.Oid >= 1).Count();
                                    int conta = os.GetObjects<NotificaRdL>().Where(w => w.RdL == rdl).Count();
                                    if (conta == 0)//  ALLORA è DA CREARE LA NOTIFICA
                                    {
                                        string location = rdl.Asset.Servizio.Immobile.Indirizzo.FullName;
                                        nrdl = os.CreateObject<NotificaRdL>();//  View.ObjectSpace.CreateObject<NotificaRdL>();
                                        nrdl.RdL = rdl;//////// #################àà
                                        using (Util u = new Util())
                                        { //AppuntamentiRisorse resource = View.ObjectSpace.GetObjectByKey<AppuntamentiRisorse>(rdl.RisorseTeam.Oid);
                                            int OidRisorsaTeam = rdl.RisorseTeam.Oid;
                                            AppuntamentiRisorse AppRisorse = os.FindObject<AppuntamentiRisorse>
                                                                            (CriteriaOperator.Parse("RisorseTeam.Oid = ?", OidRisorsaTeam));

                                            addTempo = u.SetaddTempo(0, rdl);         // tempo di ritardo  
                                            nrdl = u.SetNotificaRdL(nrdl, rdl, location, 10, AppRisorse, ref sbMessaggio);  // dati notifica                                      
                                        }
                                        //----------------                                //  crea notifica
                                        nrdl.Save();
                                        // ora aggiorna  stato autorizzativo
                                        rdl.StatoAutorizzativo = os.GetObjectByKey<StatoAutorizzativo>(1);
                                    }
                                    else if (conta > 0) // maggiore di zero quando esiste già la rdl notifiche - 
                                    {// PRENDERE LA MAGGIORE   -  è UN CAMBIO RISORSA O DATA
                                        int oidnrdl = os.GetObjectsQuery<NotificaRdL>().Where(w => w.RdL == rdl).Max(s => s.Oid);
                                        nrdl = os.GetObjectByKey<NotificaRdL>(oidnrdl);
                                        nrdl.RdL = rdl;
                                        using (Util u = new Util())
                                        {
                                            //AppuntamentiRisorse resource = View.ObjectSpace.GetObjectByKey<AppuntamentiRisorse>(rdl.RisorseTeam.Oid);
                                            AppuntamentiRisorse resource = os.FindObject<AppuntamentiRisorse>
                                                   (DevExpress.Data.Filtering.CriteriaOperator.Parse("RisorseTeam.Oid = ?", rdl.RisorseTeam.Oid));
                                            addTempo = u.SetaddTempo(0, rdl);         // tempo di ritardo  
                                            nrdl = u.SetNotificaRdL(nrdl, rdl, string.Empty, 10, resource, ref sbMessaggio);  // dati notifica                                      
                                        }
                                        //----------------                                //  crea notifica

                                        nrdl.Save();
                                        //// ora aggiorna  stato autorizzativo
                                        rdl.StatoAutorizzativo = os.GetObjectByKey<StatoAutorizzativo>(1);
                                        rdl.UltimoStatoOperativo = os.GetObjectByKey<StatoOperativo>(19);
                                    }
                                }

                                if (rdl.UltimoStatoSmistamento.Oid == 3) // in lavorazione --   && rdl.UltimoStatoOperativo.Oid == 19
                                {
                                    if (old_RisorseTeam != rdl.RisorseTeam) // è cambiata la risorsa
                                    {
                                        //  tolto intervento ad una risosrsa e assegnato ad altra risosrsa
                                        int conta = os.GetObjects<NotificaRdL>()
                                                   .Where(w => w.RdL == rdl && rdl.StatoAutorizzativo.Oid == 4).Count();
                                        if (conta > 0)
                                        {
                                            int oid = os.GetObjects<NotificaRdL>()
                                                 .Where(w => w.RdL == rdl && rdl.StatoAutorizzativo.Oid == 4)
                                                 .Max(m => m.Oid);

                                            nrdl = os.GetObjectByKey<NotificaRdL>(oid);
                                            nrdl.RdL = rdl;
                                            using (Util u = new Util())
                                            {

                                                AppuntamentiRisorse resource = os.FindObject<AppuntamentiRisorse>
                                                    (DevExpress.Data.Filtering.CriteriaOperator.Parse("RisorseTeam.Oid = ?", rdl.RisorseTeam.Oid));
                                                addTempo = u.SetaddTempo(0, rdl);         // tempo di ritardo  
                                                nrdl = u.SetNotificaRdL(nrdl, rdl, string.Empty, 10, resource, ref sbMessaggio);  // dati notifica                                      
                                            }
                                            //----------------                                //  crea notifica
                                            // aggiorno lo stato autorizzativo su RdL
                                            rdl.StatoAutorizzativo = os.GetObjectByKey<StatoAutorizzativo>(1);
                                        }
                                    }
                                }

                            }
                            #endregion
                            rdl.Save();
                        }
                    }

                    RegRdl.Save();
                }
            }
            suppressOnChanged = false;
        }


        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            DateTime ultimadata = DateTime.MinValue;
            int idmin = 0;
            int oidRegSmistamento = 0;
            RegistroRdL RegRdl = (RegistroRdL)View.CurrentObject;
            IObjectSpace xpObjectSpaceRdL = (IObjectSpace)sender;
            if (RegRdl != null)
            {
                if (RegRdl.RdLes.Count > 0) //(SmistamentoObjectsCache.Count == 2)
                {
                    foreach (RdL item in RegRdl.RdLes)
                    {
                        AggiornaRisorsaTeam(item);
                    }

                    try
                    {
                        #region invia messaggio  del registro RDL
                        // invio solo se a guasto o straordinaria                       
                        if (RegRdl.Categoria.Oid == 4 || RegRdl.Categoria.Oid == 3)
                        {
                            if (View.Id.Contains("RegistroRdL_DetailView"))
                            {
                                try
                                {
                                    string Messaggio = string.Empty;
                                    using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                                    {
                                        im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, RegRdl.Oid, ref Messaggio);
                                    }
                                    if (!string.IsNullOrEmpty(Messaggio))
                                    {
                                        string Titolo = "Trasmissione Avviso Eseguita!!";
                                        string AlertMessaggio = string.Format("Messaggio di Spedizione - {0}", Messaggio);
                                        SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                                    }
                                }
                                catch (Exception ex)
                                {
                                    string Titolo = "Trasmissione Avviso non Eseguita!!";
                                    string AlertMessaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                                    SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Warning);
                                    //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                                }
                            }

                            #endregion
                        }

                    }
                    catch
                    {
                        //string tmp = string.Format("non eseguita azione su RdL:{0} ({1})", item.Codice, item.Descrizione);
                        //Messaggio.AppendLine(tmp);
                    }

                }
                //if (RegRdl.UltimoStatoSmistamento != null && RegRdl.old_SSmistamento_Oid  != RegRdl.UltimoStatoSmistamento.Oid)
                //{
                //    RegRdl.old_SSmistamento_Oid = RegRdl.UltimoStatoSmistamento.Oid;
                //    RegRdl.Save();
                //    xpObjectSpaceRdL.CommitChanges();
                //}
                View.ObjectSpace.Refresh();  
            }
        }


        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            if (!suppressOnChanged)
            {
                suppressOnChanged = true;
                System.Diagnostics.Debug.WriteLine("INIZIO procedura ObjectSpace_ObjectChanged  v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                if (e != null && !string.IsNullOrEmpty(e.PropertyName))
                {
                    //if (e.MemberInfo != null)
                    //    if ((e.MemberInfo).Owner.Type == typeof(RdL)) 
                    //	{Name = "RdL" FullName = "CAMS.Module.DBTask.RdL"}	System.Type {System.RuntimeType}
                    foreach (var item in View.ObjectSpace.ModifiedObjects)
                    {
                        suppressOnCommitted = false;
                        suppressOnCommitting = false;
                       
                        Session Sess = ((XPObjectSpace)ObjectSpace).Session;

                        string Sw_propertyName = e.PropertyName;
                        switch (Sw_propertyName)
                        {
                            case "UltimoStatoSmistamento":

                                try
                                {
                                    StatoSmistamento newUltimoStatoSmistamento = ((StatoSmistamento)(e.NewValue));
                                    RegistroRdL Rrdl = e.Object as RegistroRdL;
                                    switch (newUltimoStatoSmistamento.Oid)
                                    {
                                        case 1://   1	In Attesa di Assegnazione
                                            Rrdl.UltimoStatoOperativo = null;
                                            Rrdl.RisorseTeam = null;
                                            break;
                                        case 2:
                                            Rrdl.UltimoStatoOperativo = Sess.GetObjectByKey<StatoOperativo>(19);
                                            break;
                                        case 5:
                                            Rrdl.UltimoStatoOperativo = Sess.GetObjectByKey<StatoOperativo>(4);
                                            break;
                                        case 7:
                                            Rrdl.UltimoStatoOperativo = Sess.GetObjectByKey<StatoOperativo>(13);
                                            break;
                                        case 6:
                                            Rrdl.UltimoStatoOperativo = Sess.GetObjectByKey<StatoOperativo>(12);
                                            break;
                                        case 4:
                                            Rrdl.UltimoStatoOperativo = Sess.GetObjectByKey<StatoOperativo>(11);
                                            break;
                                        case 11:
                                            Rrdl.UltimoStatoOperativo = Sess.GetObjectByKey<StatoOperativo>(19);
                                            break;
                                        default:
                                            Rrdl.UltimoStatoOperativo = null;
                                            break;
                                    }
                                }
                                catch
                                {
                                    suppressOnChanged = false;
                                }
                                finally
                                {
                                    suppressOnChanged = false;
                                }

                                break;
                            //case "UtenteUltimo":
                            //    try
                            //    {
                            //        RegistroRdL Rrdl = e.Object as RegistroRdL;
                            //        Rrdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                            //    }
                            //    catch
                            //    {
                            //        suppressOnChanged = false;
                            //    }
                            //    finally
                            //    {
                            //        suppressOnChanged = false;
                            //    }
                            //    break;
                        }
                    }
                }
                suppressOnChanged = false;
            }

        }


        #region metodi chiusura RdL

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

        void AggiornaRisorsaTeam(RdL rdl)
        {
            int oldstatosmistamentoiid = 0;
            if (SmistamentoOid_ObjectsCache.Count > 0)
                oldstatosmistamentoiid = SmistamentoOid_ObjectsCache[0];

            if (oldstatosmistamentoiid == 3) //	3	Emessa In lavorazione // è stato tolto da emessa in lavorazione
            {
                // allora devo toglierlo dallo smartphone
                // significa che ultimo smista è diverso da 3 e che la risorsa ha il registro impostato
                // verifico ancora quindi
                IObjectSpace xpObjSpaceRTeam = Application.CreateObjectSpace();
                RisorseTeam RisorseTeam = xpObjSpaceRTeam.GetObject<RisorseTeam>(rdl.RisorseTeam);
               
                if (rdl.UltimoStatoSmistamento.Oid != 3 && RisorseTeam.RegistroRdL.Oid == rdl.RegistroRdL.Oid) //	3	Emessa In lavorazione // è stato tolto da emessa in lavorazione
                {
                    // si è proprio da togliere   
                    RisorseTeam.RegistroRdL = null;
                    Risorse r = xpObjSpaceRTeam.GetObject<Risorse>(RisorseTeam.RisorsaCapo);
                    r.Disponibilita = false;
                    RisorseTeam.Save();
                    xpObjSpaceRTeam.CommitChanges();
                    // messaggio all'utente
                }
            }
            else
            {
                if (rdl.RisorseTeam != null)
                {
                    if (rdl.RisorseTeam.RegistroRdL != null && rdl.RegistroRdL != null)
                    {
                        IObjectSpace xpObjSpaceRTeam = Application.CreateObjectSpace();
                        RisorseTeam RisorseTeam = xpObjSpaceRTeam.GetObject<RisorseTeam>(rdl.RisorseTeam);
                        if (rdl.UltimoStatoSmistamento.Oid != 3 && RisorseTeam.RegistroRdL.Oid == rdl.RegistroRdL.Oid) //	3	Emessa In lavorazione // è stato tolto da emessa in lavorazione
                        {
                            // si è proprio da togliere   
                            RisorseTeam.RegistroRdL = null;
                            Risorse r = xpObjSpaceRTeam.GetObject<Risorse>(RisorseTeam.RisorsaCapo);
                            r.Disponibilita = false;
                            RisorseTeam.Save();
                            xpObjSpaceRTeam.CommitChanges();
                            // messaggio all'utente
                        }
                    }
                }
            }


        }

        #endregion


    }
}
