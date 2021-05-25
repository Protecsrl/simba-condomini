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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBGestOrari;
using CAMS.Module.DBNotifiche;
using CAMS.Module.DBTask;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Validation.AllContextsView;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace CAMS.Module.Controllers.DBGestOrari
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class GestioneNuoviOrariObjSpaceController : ViewController
    {
        private DateTime Adesso = DateTime.Now;

        bool suppressOnChanged = false;
        bool suppressOnCommitting = false;
        bool suppressOnCommitted = false;

        bool IsNuovaRdL = false;
        bool CambiataRisorsa = false;
        public GestioneNuoviOrariObjSpaceController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            //View.ObjectSpace.Committing += ObjectSpace_Committing;
            //View.ObjectSpace.Committed += ObjectSpace_Committed;

            //ObjectSpace.ObjectDeleted += ObjectSpace_ObjectDeleted; 
            //ObjectSpace.Reloaded += ObjectSpace_Reloaded;
            suppressOnChanged = false;
            suppressOnCommitted = false;
            suppressOnCommitting = false;
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View is DetailView)
            {
                DetailView Dv = View as DetailView;
                GestioneNuoviOrari cGestioneNuoviOrari = Dv.CurrentObject as GestioneNuoviOrari;

                if (cGestioneNuoviOrari.SAutorizzativo.Oid == 8 && Dv.ViewEditMode == ViewEditMode.Edit)
                    this.saGNOrariAddCircuito.Active.SetItemValue("Active", true);
                else
                    this.saGNOrariAddCircuito.Active.SetItemValue("Active", false);

                if (Dv.ViewEditMode == ViewEditMode.Edit)
                {
                    if (cGestioneNuoviOrari.SAutorizzativo.Oid == 8 || cGestioneNuoviOrari.SAutorizzativo.Oid == 9 || (cGestioneNuoviOrari.SAutorizzativo.Oid == 10 && cGestioneNuoviOrari.RuoloAutorizzativo))
                        this.saGNOrariConferma.Active.SetItemValue("Active", true);
                    else
                        this.saGNOrariConferma.Active.SetItemValue("Active", false);
                }
                else
                {
                    this.saGNOrariConferma.Active.SetItemValue("Active", false);
                }
            }
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void saGNOrariAddCircuito_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //string aaa = "fdgh";
                if (View is DetailView)
                {
                    Session Sess = ((XPObjectSpace)xpObjectSpace).Session;

                    //GestioneOrari CurrentGestioneOrari = xpObjectSpace.GetObjectByKey<GestioneOrari>();
                    int oidCircuitoSelezionato = ((GestioneNuoviOrari)View.CurrentObject).Circuito.Oid;
                    if (oidCircuitoSelezionato > 0)
                    {
                        tbcircuiti CurrentCircuiti = xpObjectSpace.GetObjectByKey<tbcircuiti>(oidCircuitoSelezionato);

                        GestioneNuoviOrari CurrentGestioneNuoviOrari = xpObjectSpace.GetObjectByKey<GestioneNuoviOrari>(((GestioneNuoviOrari)View.CurrentObject).Oid);
                        //XPQuery<GestioneOrariCircuiti> customers = Sess.Query<GestioneOrariCircuiti>();
                        XPQuery<GestioneOrariCircuiti> qGestioneOrariCircuiti = new XPQuery<GestioneOrariCircuiti>(Sess);
                        int Conta = qGestioneOrariCircuiti.Where(w => w.Circuiti.Oid == CurrentCircuiti.Oid && w.GestioneOrari.Oid == CurrentGestioneNuoviOrari.Oid).Count();
                        if (Conta == 0)
                        {
                            GestioneOrariCircuiti GestioneOrariCircuiti = xpObjectSpace.CreateObject<GestioneOrariCircuiti>();
                            GestioneOrariCircuiti.SetMemberValue("GestioneNuoviOrari", CurrentGestioneNuoviOrari);
                            GestioneOrariCircuiti.SetMemberValue("Circuiti", CurrentCircuiti);
                            GestioneOrariCircuiti.Save();
                            xpObjectSpace.CommitChanges();
                        }
                    }
                }

            }
        }

        private void saGNOrariConferma_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            /*
            OID STATOAUTORIZZATIVO  FASE ORDINE  MESSAGGIOINFORMATIVO
8   Impostazione di Filtro Impostazione di Filtro  8   Impostazione di Filtro: in questa fase è possibile impostare i filtri di selezione delle date e dei circuiti al quale si potra Richiedere il Cambio Orario
9   Modifica Orari  Modifica Orari  9   Modifica Orari: in questa fase è possibile usare gli strumenti di modifica massiva sul calendario per effettuare la richiesta di cambioorario
10  in attesa di approvazione   in attesa di approvazione   10  questa fase è possibile usare gli strumenti di modifica massiva sul calendario per effettuare la richiesta di cambioorario
11  Approvato con modifiche Approvato con modifiche 11  questa fase è possibile usare gli strumenti di modifica massiva sul calendario per effettuare la richiesta di cambioorario
12  Approvato Approvato   12  questa fase è possibile usare gli strumenti di modifica massiva sul calendario per effettuare la richiesta di cambioorario
13  in lavorazione  in lavorazione  13  questa fase è possibile usare gli strumenti di modifica massiva sul calendario per effettuare la richiesta di cambioorario
14  Annulato Annulato    14  NULL
15  Sospeso Sospeso 15  NULL
16  Completato Completato  16  NULL
                */
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session session = ((XPObjectSpace)xpObjectSpace).Session;
            GestioneNuoviOrari cGestioneNuoviOrari = ((GestioneNuoviOrari)View.CurrentObject);
            int SAutorizzativo_Oid = cGestioneNuoviOrari.SAutorizzativo.Oid;
            List<tbcalendario> CalendarioUtenteList = new List<tbcalendario>();
            int tempodiRemindIn = 180;
            string Subject = string.Empty;
            try
            {
                cGestioneNuoviOrari.Save();
                View.ObjectSpace.CommitChanges();
                switch (SAutorizzativo_Oid)
                {
                    case 8: //   Impostazione di Filtro
                        #region passaggio stato autorizzativo da 8 a 9 (da impostazione filtro -->> in attesa di approvazione)
                        /// CARICA CALENDARIO UTENTE - le modifiche che ha fatto
                        cGestioneNuoviOrari.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(9);
                        cGestioneNuoviOrari.Save();
                        xpObjectSpace.CommitChanges();
                        SetMessaggioWeb("Confermata Fine Impostazione Filtro. Inizio fase di Nuova Programmazione Orari", "Azione Eseguita", InformationType.Info, false);
                        #endregion
                        break;

                    case 9:  //   Modifica Orari 
                        #region passaggio stato autorizzativoda 9 a 10 (da impostazione filtro a in attesa di approvazione)
                        //if (cGestioneOrari.SAutorizzativo.Oid == 9)
                        //{
                        /// CARICA CALENDARIO UTENTE - le modifiche che ha fatto
                        cGestioneNuoviOrari.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(10);
                        //CalendarioUtenteList = cGestioneNuoviOrari.SettimanaTipos.Where(w => w.idTicketEAMS == cGestioneNuoviOrari.Oid).ToList();
                        foreach (SettimanaTipo stt in cGestioneNuoviOrari.SettimanaTipos.Where(w => w.idTicketEAMS == cGestioneNuoviOrari.Oid).ToList())
                        {
                            tbSettimanaTipoUtente cGestioneSettimanaTipoUtente = xpObjectSpace.CreateObject<tbSettimanaTipoUtente>();
                            cGestioneSettimanaTipoUtente.SetMemberValue("GestioneNuoviOrari", cGestioneNuoviOrari);
                            cGestioneSettimanaTipoUtente.SetMemberValue("datains", DateTime.Now);
                            cGestioneSettimanaTipoUtente.SetMemberValue("versione", 1);
                            cGestioneSettimanaTipoUtente.SetMemberValue("Circuiti", stt.Circuito);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f1startTipoSetOrarioU", stt.f1startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f1endTipoSetOrarioU", stt.f1endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f2startTipoSetOrarioU", stt.f2startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f2endTipoSetOrarioU", stt.f2endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f3startTipoSetOrarioU", stt.f3startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f3endTipoSetOrarioU", stt.f3endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f4startTipoSetOrarioU", stt.f4startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f4endTipoSetOrarioU", stt.f4endTipoSetOrarioU);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("f5startTipoSetOrarioU", stt.f5startTipoSetOrarioU);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("f5endTipoSetOrarioU", stt.f5endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("statoEAMS", "INS");
                            //cGestioneSettimanaTipoUtente.SetMemberValue("wday", stt.wday);
                            cGestioneSettimanaTipoUtente.SetMemberValue("idTicketEAMS", stt.idTicketEAMS);
                            cGestioneSettimanaTipoUtente.SetMemberValue("idticket", stt.idticket);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("datains", stt.datains);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("versione", stt.versione);
                            cGestioneSettimanaTipoUtente.Save();
                        }
                        //////////
                        cGestioneNuoviOrari.Save();
                        xpObjectSpace.CommitChanges();

                        //}
                        break;
                    #endregion

                    case 10:// in attesa di approvazione	
                        #region passaggio stato autorizzativoda 10 a 11 (da impostazione filtro a in attesa di approvazione)
                        //if (cGestioneOrari.SAutorizzativo.Oid == 10)
                        //{
                        /// CARICA CALENDARIO UTENTE - le modifiche che ha fatto
                        cGestioneNuoviOrari.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(11);
                        //CalendarioUtenteList = cGestioneNuoviOrari.SettimanaTipos.Where(w => w.idTicketEAMS == cGestioneNuoviOrari.Oid).ToList();
                        foreach (SettimanaTipo stt in cGestioneNuoviOrari.SettimanaTipos.Where(w => w.idTicketEAMS == cGestioneNuoviOrari.Oid).ToList())
                        {
                            tbSettimanaTipoUtente cGestioneSettimanaTipoUtente = xpObjectSpace.CreateObject<tbSettimanaTipoUtente>();
                            cGestioneSettimanaTipoUtente.SetMemberValue("GestioneNuoviOrari", cGestioneNuoviOrari);
                            cGestioneSettimanaTipoUtente.SetMemberValue("datains", DateTime.Now);
                            cGestioneSettimanaTipoUtente.SetMemberValue("versione", 1);
                            cGestioneSettimanaTipoUtente.SetMemberValue("Circuiti", stt.Circuito);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f1startTipoSetOrarioU", stt.f1startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f1endTipoSetOrarioU", stt.f1endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f2startTipoSetOrarioU", stt.f2startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f2endTipoSetOrarioU", stt.f2endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f3startTipoSetOrarioU", stt.f3startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f3endTipoSetOrarioU", stt.f3endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f4startTipoSetOrarioU", stt.f4startTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("f4endTipoSetOrarioU", stt.f4endTipoSetOrarioU);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("f5startTipoSetOrarioU", stt.f5startTipoSetOrarioU);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("f5endTipoSetOrarioU", stt.f5endTipoSetOrarioU);
                            cGestioneSettimanaTipoUtente.SetMemberValue("statoEAMS", "APP");
                            //cGestioneSettimanaTipoUtente.SetMemberValue("wday", stt.wday);
                            cGestioneSettimanaTipoUtente.SetMemberValue("idTicketEAMS", stt.idTicketEAMS);
                            cGestioneSettimanaTipoUtente.SetMemberValue("idticket", stt.idticket);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("datains", stt.datains);
                            //cGestioneSettimanaTipoUtente.SetMemberValue("versione", stt.versione);
                            cGestioneSettimanaTipoUtente.Save();
                        }

                        #region genera avviso crea record inserimento avviso notifica spedizione mail
                        //obj = ObjectSpace.CreateObject<TaskWithNotifications>();
                        //obj.Subject = "Now Task With Reminder";
                        //obj.StartDate = DateTime.Now.AddMinutes(5);
                        //obj.DueDate = obj.StartDate.AddHours(2);
                        //obj.RemindIn = TimeSpan.FromMinutes(5);
                        //obj.Save();        
                        //  creo avviso spedizione mail all approvatore
                        tempodiRemindIn = 180;
                        Subject = string.Format("Notifica Spedizione Gestione Orari {0}, Stato: {1}, Circuito: {2}, Utente: {3}"
                            , cGestioneNuoviOrari.Oid.ToString(), cGestioneNuoviOrari.SAutorizzativo.Descrizione, cGestioneNuoviOrari.Circuito.circuito, SecuritySystem.CurrentUserName);
                        InsertAvvisoSpedizione(xpObjectSpace, cGestioneNuoviOrari, tempodiRemindIn, 13, cGestioneNuoviOrari.Descrizione, Subject);
                        //      OidSmistamento = 13;  // Gestione Orari - In attesa di Approvazione

                        // creo popup  di avviso all'approvatore
                        tempodiRemindIn = 120;
                        Subject = string.Format("Notifica Avviso Gestione Orari {0}, Stato: {1}, Circuito: {2}, Utente: {3}"
                            , cGestioneNuoviOrari.Oid.ToString(), cGestioneNuoviOrari.SAutorizzativo.Descrizione, cGestioneNuoviOrari.Circuito.circuito, SecuritySystem.CurrentUserName);
                        InsertAvvisoSpedizione(xpObjectSpace, cGestioneNuoviOrari, tempodiRemindIn, 14, cGestioneNuoviOrari.Descrizione, Subject);
                        //      OidSmistamento = 14;  // Gestione Orari - In attesa di Approvazione
                        #endregion
                        ///
                        cGestioneNuoviOrari.Save();
                        xpObjectSpace.CommitChanges();
                        //}
                        break;
                    #endregion
                    case 11: // Approvato
                             //--> disabilitare tuttele modifiche
                             //--> questo stato è finale per utente non utilizzativo
                             //-- > DA QUESTO STATO PRENDE LA STORE PROCEDURA CHE PASSA ALLA TABELLA SCAMBIO DI APPIC


                        //XPObjectSpace XPOSpaceRdL = (XPObjectSpace)sender;

                        break;

                }
            }
            catch (Exception ex)
            {
                SetMessaggioWeb("Attenzione qualcosa non va! " + ex.Message, "Titolo", InformationType.Info, false);
            }
        }

        private void saGNOrariModificaFascia_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            GestioneNuoviOrari cGestioneNuoviOrari = ((GestioneNuoviOrari)View.CurrentObject);
            cGestioneNuoviOrari.Save();
            try
            {
                Validator.RuleSet.Validate(ObjectSpace, View.CurrentObject, "AzioneModificaGOrari");
                SetMessaggioWeb("Messaggio sAGOrariNomeGiorno_Execute ok", "Titolo", InformationType.Info, false);

                PropertyEditor editor = ((DetailView)View).FindItem("SettimanaTipos") as PropertyEditor;
                ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("SettimanaTipos") as ListPropertyEditor;

                //foreach (PropertyEditor editor in ((DetailView)View).GetItems<PropertyEditor>())
                //{
                if (editor != null && editor.GetType() == typeof(ListPropertyEditor))
                {
                    //ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("Tasks") as ListPropertyEditor;
                    //ListPropertyEditor listPropertyEditor = editor as ListPropertyEditor;
                    var nestedListView = ((ListPropertyEditor)editor).ListView;
                    //var lstRisorseTeam = popupListView.Items[0].CurrentObject;

                    if (nestedListView != null)
                    {
                        if (nestedListView.Id == "GestioneNuoviOrari_SettimanaTipos_ListView")
                        {
                            if (nestedListView.Control != null)
                            {
                                #region  imposta enum giorno settimana
                                List<DayOfWeek> OidNomeGioneSettimana = new List<DayOfWeek> { };
                                switch (cGestioneNuoviOrari.GiornoSettimana)
                                {
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Lunedi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Monday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Martedi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Tuesday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Mercoledi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Wednesday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Giovedi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Thursday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Vernerdi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Friday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Lunedi_Venerdi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Monday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Tuesday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Wednesday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Thursday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Friday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.LunediMercolediVernerdi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Monday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Wednesday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Friday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.MartediGiovedi:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Tuesday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Thursday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Sabato:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Saturday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.Domenica:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Sunday);
                                        break;
                                    case TipoSelezioneGestioneOrarioGiornoSettimana.SabatoDomenica:
                                        OidNomeGioneSettimana.Add(DayOfWeek.Saturday);
                                        OidNomeGioneSettimana.Add(DayOfWeek.Sunday);
                                        break;

                                }
                                #endregion
                                List<int> OidCircuiti = new List<int> { 0 };
                                if (cGestioneNuoviOrari.GestioneOrariCircuitis.Count > 0)
                                    OidCircuiti = cGestioneNuoviOrari.GestioneOrariCircuitis.Select(S => S.Circuiti.Oid).ToList();
                                if (cGestioneNuoviOrari.Circuito != null)
                                    OidCircuiti.Add(cGestioneNuoviOrari.Circuito.Oid);

                                AggiornaSettimanaTipo(cGestioneNuoviOrari, nestedListView, OidCircuiti, OidNomeGioneSettimana);

                            }
                            else
                            {
                                //listPropertyEditor.ControlCreated += listPropertyEditor_ControlCreated;
                            }
                            nestedListView.Refresh();
                        }
                    }
                }
                //}  
            }
            catch
            {
                SetMessaggioWeb("Messaggio sAGOrariData_Execute validazione fallita !!!!!!!!!!!", "Titolo", InformationType.Info, false);
            }
        }

        private void AggiornaSettimanaTipo(GestioneNuoviOrari cGestioneNuoviOrari, ListView nestedListView, List<int> OidCircuiti, List<DayOfWeek> OidNomeGioneSettimana = null)
        {

            SecuritySystemUser userGO = View.ObjectSpace.GetObject<SecuritySystemUser>((SecuritySystemUser)Application.Security.User);
            foreach (SettimanaTipo cal in nestedListView.CollectionSource.List.Cast<SettimanaTipo>()
                                        .Where(w => OidCircuiti.Contains(w.Circuito.Oid))
                                        .Where(w => OidNomeGioneSettimana.Contains((DayOfWeek)w.TipoGiornoSettimana))
                                        )
            {
                //cal.idcircuito = 1;
                cal.idTicketEAMS = cGestioneNuoviOrari.Oid;
                //cal.versione = cal.versione + 1;
                //cal.flag_eccezione = 1;
                cal.SecurityUser = userGO;
                cal.f1startTipoSetOrarioU = cGestioneNuoviOrari.f1startTipoSetOrario;
                cal.f1endTipoSetOrarioU = cGestioneNuoviOrari.f1endTipoSetOrario;
                cal.f2startTipoSetOrarioU = cGestioneNuoviOrari.f2startTipoSetOrario;
                cal.f2endTipoSetOrarioU = cGestioneNuoviOrari.f2endTipoSetOrario;
                cal.f3startTipoSetOrarioU = cGestioneNuoviOrari.f3startTipoSetOrario;
                cal.f3endTipoSetOrarioU = cGestioneNuoviOrari.f3endTipoSetOrario;
                cal.f4startTipoSetOrarioU = cGestioneNuoviOrari.f4startTipoSetOrario;
                cal.f4endTipoSetOrarioU = cGestioneNuoviOrari.f4endTipoSetOrario;

                cal.Save();
            }

            foreach (SettimanaTipo cal in cGestioneNuoviOrari.SettimanaTipos
                                         .Where(w => OidCircuiti.Contains(w.Circuito.Oid))
                                        .Where(w => OidNomeGioneSettimana.Contains((DayOfWeek)w.TipoGiornoSettimana))

                                               )
            {///  inserire utente 
                //cal.idcircuito = 1;
                cal.idTicketEAMS = cGestioneNuoviOrari.Oid;
                //cal.versione = cal.versione + 1;
                //cal.flag_eccezione = 1;
                cal.SecurityUser = userGO;
                cal.f1startTipoSetOrarioU = cGestioneNuoviOrari.f1startTipoSetOrario;
                cal.f1endTipoSetOrarioU = cGestioneNuoviOrari.f1endTipoSetOrario;
                cal.f2startTipoSetOrarioU = cGestioneNuoviOrari.f2startTipoSetOrario;
                cal.f2endTipoSetOrarioU = cGestioneNuoviOrari.f2endTipoSetOrario;
                cal.f3startTipoSetOrarioU = cGestioneNuoviOrari.f3startTipoSetOrario;
                cal.f3endTipoSetOrarioU = cGestioneNuoviOrari.f3endTipoSetOrario;
                cal.f4startTipoSetOrarioU = cGestioneNuoviOrari.f4startTipoSetOrario;
                cal.f4endTipoSetOrarioU = cGestioneNuoviOrari.f4endTipoSetOrario;
                cal.Save();
                //cal.Reload();
                //nestedListView.Refresh();
            }
        }

        private void InsertAvvisoSpedizione(IObjectSpace xpObjectSpace, GestioneNuoviOrari cGestioneNuoviOrari, int tempodiRemindIn, int OidSmistamento, string Descrizione, string Subject)
        {
            SecuritySystemUser userGO = xpObjectSpace.GetObject<SecuritySystemUser>((SecuritySystemUser)Application.Security.User);
            AvvisiSpedizioni NotificaSpedizione = xpObjectSpace.CreateObject<AvvisiSpedizioni>();
            NotificaSpedizione.Description = Descrizione;
            NotificaSpedizione.Subject = Subject;
            NotificaSpedizione.Sollecito = FlgAbilitato.Si;
            NotificaSpedizione.OidSmistamento = OidSmistamento;  // Gestione Orari - In attesa di Approvazione
            NotificaSpedizione.OidRisorsaTeam = 0;
            NotificaSpedizione.RemindIn = TimeSpan.FromSeconds(tempodiRemindIn);
            NotificaSpedizione.StartDate = DateTime.Now.AddSeconds((int)(tempodiRemindIn * 2.1));
            NotificaSpedizione.DueDate = DateTime.Now.AddSeconds(tempodiRemindIn * 4);
            NotificaSpedizione.RdLUnivoco = cGestioneNuoviOrari.Oid;
            NotificaSpedizione.Status = myTaskStatus.Predisposto;
            NotificaSpedizione.Utente = userGO.UserName;
            NotificaSpedizione.Abilitato = FlgAbilitato.Si;
            NotificaSpedizione.DataCreazione = DateTime.Now;
            NotificaSpedizione.DataAggiornamento = DateTime.Now;
            NotificaSpedizione.Save();
        }


        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            //Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            IObjectSpace xpo = Application.CreateObjectSpace(typeof(LogSystemTrace));
            CAMS.Module.Classi.Logger.AddLog(xpo, SecuritySystem.CurrentUserName, "inizio GestioneOrariObjSpaceController ObjectSpace_ObjectChanged GestioneOrari " + View.Id.ToString() + " tempo ", (DateTime.Now - Adesso).TotalSeconds.ToString());
            Tracing.Tracer.LogText("Some text");
            if (!suppressOnChanged)
            {
                suppressOnChanged = true;
                System.Diagnostics.Debug.WriteLine("INIZIO procedura ObjectSpace_ObjectChanged  v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                if (e != null && !string.IsNullOrEmpty(e.PropertyName))
                {
                    string TipoOggetto = e.Object.GetType().Name; // l'oggetto modificato è nuovo??  puo essere anche Richiedente (perche c'e' il pulsante nuovo)
                    bool IsNuovoOggetto = View.ObjectSpace.IsNewObject(e.Object);//  indica se l'oggetto è nuovo ? 
                    string Sw_propertyName = e.PropertyName; // la proprietà che cambia

                    //foreach (var item in View.ObjectSpace.ModifiedObjects)// qui ci sono tutti gli oggetti modificati anche piu di uno
                    switch (Sw_propertyName)
                    {
                        case "F1_ModDataOra":
                            if (e.NewValue != null)
                            {
                                DateTime NewF1_ModDataOra = (DateTime)e.NewValue;
                                GestioneOrari GrO = e.Object as GestioneOrari;
                                if (NewF1_ModDataOra > GrO.dataora_dal && NewF1_ModDataOra < GrO.dataora_Al)
                                {
                                    SetMessaggioWeb("D", "Titolo", InformationType.Info, false);
                                }

                            }
                            // non fai nulla
                            break;
                        case "RdL":
                            //var item1 = item;
                            break;
                    }
                }
                System.Diagnostics.Debug.WriteLine("fine procedura ObjectSpace_ObjectChanged  v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                suppressOnChanged = false;
            }
            //View.ObjectSpace.CommitChanges();

            CAMS.Module.Classi.Logger.AddLog(xpo, SecuritySystem.CurrentUserName, "fine GestioneOrariObjSpaceController ObjectSpace_ObjectChanged GestioneOrari " + View.Id.ToString() + " tempo ", (DateTime.Now - Adesso).TotalSeconds.ToString());
        }

        private void SetMessaggioWeb(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info, bool Pulsanti = false)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 7000;
            options.CancelDelegate = () => { };
            options.Web.CanCloseOnClick = true;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Right;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
                                                      //messageOptions.Type = InformationType.Error;
                                                      //messageOptions.Web.Position = InformationPosition.Bottom;
                                                      //messageOptions.Win.Type = WinMessageType.Alert;
                                                      //if (Pulsanti)
                                                      //{
                                                      //    options.OkDelegate = () =>
                                                      //    {
                                                      //        //IObjectSpace os = View.ObjectSpace;
                                                      //        RdL rdl = (RdL)View.CurrentObject;
                                                      //        rdl.DataSopralluogo = DateTime.Now;
                                                      //        rdl.DataAzioniTampone = DateTime.Now;
                                                      //        rdl.DataInizioLavori = DateTime.Now;
                                                      //        rdl.Save();
                                                      //        //IObjectSpace os = Application.CreateObjectSpace(typeof(Test));
                                                      //        //DetailView detailView = Application.CreateDetailView(os, os.FindObject<Test>(new BinaryOperator(nameof(Test.Oid), test.Oid)));
                                                      //        //Application.ShowViewStrategy.ShowViewInPopupWindow(detailView);
                                                      //    };
                                                      //}


            Application.ShowViewStrategy.ShowMessage(options);
        }


    }
}
