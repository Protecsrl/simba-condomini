using CAMS.Module.Classi;
using CAMS.Module.DBAgenda;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.Vista;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CAMS.Module.Controllers.DBAgenda
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class NotificaRdLController : ViewController
    {
        //private SimpleAction MarkCompletedAction;
        private SimpleAction AccettazioneSO;
        private SimpleAction VisualizzaRdLAction;

        public NotificaRdLController()
        {
            InitializeComponent();
            TargetObjectType = typeof(NotificaRdL);
            //MarkCompletedAction = new SimpleAction(this, "Accettazione Orario", PredefinedCategory.Edit);
            //MarkCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            //MarkCompletedAction.ImageName = "State_Task_Completed";
            //MarkCompletedAction.ToolTip = "Accettazione Orario";
            //MarkCompletedAction.Execute += MarkCompletedAction_Execute;
            AccettazioneSO = new SimpleAction(this, "Accettazione Orario", PredefinedCategory.ListView);
            AccettazioneSO.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            AccettazioneSO.TargetObjectsCriteria = "Oid>0 And Label = 2";
            AccettazioneSO.ImageName = "State_Task_Completed";
            AccettazioneSO.ToolTip = "Accettazione Orario";
            AccettazioneSO.Execute += AccettazioneSO_Execute;

            VisualizzaRdLAction = new SimpleAction(this, "Vai a Rdl", PredefinedCategory.Edit);
            VisualizzaRdLAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            //VisualizzaRdLAction.TargetObjectsCriteria = "Rdl is not null";
            VisualizzaRdLAction.ImageName = "ModelEditor_Actions_ActionToContainerMapping";
            VisualizzaRdLAction.ToolTip = "Entra nella Rdl";
            VisualizzaRdLAction.Execute += VisualizzaRdLAction_Execute;

        }


        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                NotificaRdL nrdl = (NotificaRdL)dv.CurrentObject;
                this.popGetDCRdL.Active.SetItemValue("Active", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
                if (nrdl.RdL != null)
                {
                    if (nrdl.Resources.Count > 0)
                    {
                        string NuovaCaption = string.Format("Notifica di Avviso Assegnata al Team:{0} {1}({2}) - CO:{3}",
                            nrdl.Resources.First().RisorseTeam.RisorsaCapo.Nome,
                             nrdl.Resources.First().RisorseTeam.RisorsaCapo.Cognome,
                              nrdl.Resources.First().RisorseTeam.RisorsaCapo.Matricola
                              , nrdl.Resources.First().RisorseTeam.RisorsaCapo.CentroOperativo.CodDescrizione);
                        View.Caption = NuovaCaption;

                        //nrdl.Resources.Add();
                    }
                }
                if (nrdl.RdL == null)
                {
                    IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    int CO_Oid = 0;
                    int Conta = 0;
                    if (nrdl.Resources.Count > 0)
                        CO_Oid = nrdl.Resources.First().RisorseTeam.RisorsaCapo.CentroOperativo.Oid;

                    if (CO_Oid == 0)
                    {
                        //XPQuery<RdL> customersQuery = new XPQuery<RdL>(Sess);
                        Conta = new XPQuery<RdL>(Sess)
                                              .Where(w => w.Immobile.Contratti.LivelloAutorizzazioneGuasto > 0)
                                              .Where(w => w.UltimoStatoSmistamento.Oid == 1)
                                              .Where(w => w.Categoria.Oid == 4).Count();
                    }
                    else
                    {
                        Conta = new XPQuery<RdL>(Sess)
                         .Where(w => w.Immobile.CentroOperativoBase.Oid == CO_Oid)
                                          .Where(w => w.Immobile.Contratti.LivelloAutorizzazioneGuasto > 0)
                                          .Where(w => w.UltimoStatoSmistamento.Oid == 1)
                                          .Where(w => w.Categoria.Oid == 4).Count();
                    }

                    if (Conta == 0)
                    {
                        SetMessaggioWebInterventi("Non Ci sono Interventi in attesa di assegnazione per questa risorsa",
                            "Numero interventi", InformationType.Info);
                    }
                    else
                    {
                        SetMessaggioWebInterventi(string.Format("Ci sono nr. {0} Interventi da assegnare!", Conta),
                            "Numero interventi", InformationType.Info);
                    }
                }
            }
            if (View.Id == "AppuntamentiRisorse_NotificaRdLs_ListView" || View.Id == "NotificaRdL_ListView_SK")
            {//((DetailView)View).ViewEditMode == ViewEditMode.Edit
                VisualizzaRdLAction.Category = "ListView";
                VisualizzaRdLAction.Active.SetItemValue("Active", true);
            }
            else
            {
                VisualizzaRdLAction.Category = "Edit";
                VisualizzaRdLAction.Active.SetItemValue("Active", true);
            }

            this.acInizioTrasferimento.Active.SetItemValue("Active", false);
            this.acDchiarazioneArrivo.Active.SetItemValue("Active", false);
            if (CAMS.Module.Classi.SetVarSessione.IsAdminRuolo)
            {
                this.acInizioTrasferimento.Active.SetItemValue("Active", true);
                this.acDchiarazioneArrivo.Active.SetItemValue("Active", true);
            }

        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                NotificaRdL nrdl = (NotificaRdL)dv.CurrentObject;
                this.popGetDCRdL.Active.SetItemValue("Active", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);

                //Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", true);
                //if (nrdl.Resources.Count() > 0)
                //{
                //    Frame.GetController<LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                //    //Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
                //}
            }
        }
        protected override void OnDeactivated()
        {
            //objectSpace.ObjectsGetting -= DCRdL_objectSpace_ObjectsGetting;
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }




        #region get RdL da associare alla risorsa
        int CentroOperativoOid = 0;
        int AreaDiPoloOid = 0;
        int RisorseTeamOid = 0;

        private void popGetDCRdL_CustomizePopupWindowParams(object sender, DevExpress.ExpressApp.Actions.CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    NotificaRdL nrdl = ((DetailView)View).CurrentObject as NotificaRdL;
                    if (nrdl.Resources.Count > 0)
                    {
                        if (nrdl.Resources[0].RisorseTeam != null)
                        {
                            RisorseTeam rt = nrdl.Resources[0].RisorseTeam;
                            RisorseTeamOid = rt.Oid;
                            try
                            {
                                CentroOperativoOid = xpObjectSpace.GetObjectByKey<RisorseTeam>(RisorseTeamOid).RisorsaCapo.CentroOperativo.Oid;
                                AreaDiPoloOid = xpObjectSpace.GetObjectByKey<RisorseTeam>(RisorseTeamOid).RisorsaCapo.CentroOperativo.AreaDiPolo.Oid;
                            }
                            catch { }
                        }
                    }

                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRdL));
                    objectSpace.ObjectsGetting += DCRdL_objectSpace_ObjectsGetting;

                    CollectionSource DCRdL_Lookup = (CollectionSource)Application.
                                        CreateCollectionSource(objectSpace, typeof(DCRdL), "DCRdL_LookupListView");
                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("idColore", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    DCRdL_Lookup.Sorting.Add(srtProperty);

                    ListView lvk = Application.CreateListView("DCRdL_LookupListView", DCRdL_Lookup, true);

                    var dc = Application.CreateController<DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = lvk;

                }
            }
        }

        private void popGetDCRdL_Execute(object sender, DevExpress.ExpressApp.Actions.PopupWindowShowActionExecuteEventArgs e)
        {
            //System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
            //string sbMessaggio  = string.Empty;
            if (View is DetailView)
            {
                DetailView dv = View as DetailView;
                var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                List<DCRdL> lst_DCRdL_Selezionate = ((List<DCRdL>)((((Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<DCRdL>().ToList<DCRdL>()));
                if (xpObjectSpace != null && lst_DCRdL_Selezionate.Count > 0)
                {
                    if (View.ObjectTypeInfo.Type == typeof(NotificaRdL))//.Editor).GetSelectedObjects().Count > 0)
                    {
                        NotificaRdL nrdl = (NotificaRdL)dv.CurrentObject;
                        int oidrdl = lst_DCRdL_Selezionate.Select(s => s.ID).First();
                        RdL rdl = View.ObjectSpace.GetObjectByKey<RdL>(oidrdl);
                        if (rdl != null && nrdl != null)
                        {
                            //using (Util u = new Util()) //{    nrdl = u.SetNotificaRdL(nrdl, rdl, location, 10, null, ref sbMessaggio);//}
                            #region inizializza NOTIFICA
                            try
                            {
                                nrdl.RdL = rdl;
                                string location = lst_DCRdL_Selezionate.Select(s => s.IndirizzoDescrizione).First();
                                nrdl.Location = location;// indirizzo

                                nrdl.Subject = string.Format("RdL/RegdL: {0}/{1} - Immobile: {2}",
                                                                        rdl.Oid, rdl.RegistroRdL.Oid, rdl.Immobile.Descrizione);
                                nrdl.Description = string.Format("{0}", rdl.Descrizione);
                                //  persintalias
                                nrdl.LabelListView = LabelListView.in_attesa_di_dichiarazione_tecnico;// = 1

                                nrdl.StartDate = DateTime.Now.AddMinutes(2); // - TimeSpan.FromDays(1) data di inizio avviso di popup    -  obj.StartOn = DateTime.Now - TimeSpan.FromDays(1);
                                int addTempo = SetaddTempo(0, rdl);
                                nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);// data scadenza   //--obj.EndOn = obj.StartDate.AddMinutes(2); 
                                ///nrdl.DateCompleted =   // data fine scadenza
                                nrdl.RemindIn = TimeSpan.Zero;//TimeSpan.FromMinutes(addTempo);
                                nrdl.Status = 1; //DevExpress.Persistent.Base.General.TaskStatus.NotStarted;
                                //obj.AlarmTime = DateTime.Now.Add(TimeSpan.FromMinutes(addTempo));  obj.AlarmTime = obj.StartDate - obj.RemindIn.Value;   AlarmTime    obj.StartDate - obj.RemindIn.Value;
                                ((DevExpress.Persistent.Base.General.ISupportNotifications)nrdl)
                                .AlarmTime = nrdl.DueDate; // tempo in cui scatta l'allarme di avviso popup

                                nrdl.StatusNotifica = TaskStatus.NotStarted;
                                ////// dati rdl 
                                //if (nrdl.StartOn < DateTime.Now.AddMinutes(1))
                                //    nrdl.StartOn = DateTime.Now.AddMinutes(1);/// rdl.DataPianificata;//   data pianificata di inizio lavoro
                                int minutidiintervento = 60;
                                //if (rdl.Problema != null)
                                //    minutidiintervento = Convert.ToInt32(rdl.Problema.Problemi.Valore);
                                if (rdl.Prob != null)
                                    minutidiintervento = Convert.ToInt32(rdl.Prob.Valore);
                                nrdl.EndOn = nrdl.StartOn.AddMinutes(minutidiintervento);// data pianificata di fine lavoro

                                if (rdl.DataPianificata != nrdl.StartOn)
                                    nrdl.MessaggioUtente = string.Format("La Data di Pianificazione ({0}) della RdL({1}) verrà Aggiornata con la nuova data ({2})", rdl.DataPianificata, rdl.Oid, nrdl.StartOn);
                                SetMessaggioWebInterventi("Eseguita selezione RdL {0}, Ricordati di Salvare!!!",
                                                      "Selezione Intervento", InformationType.Info);
                            }
                            catch (Exception eccezione)
                            {
                                //sbMessaggio.AppendLine("Errore");
                                //sbMessaggio.AppendLine("The process failed: {0}" + eccezione.ToString());
                                SetMessaggioWebInterventi("The process failed: {0}!!!" + eccezione.ToString(),
                                                   "Errore", InformationType.Error);
                            }

                            #endregion


                        }
                    }
                    View.Refresh();
                }
                else
                {
                    SetMessaggioWebInterventi("nessuna selezione eseguita!!!", "Selezione", InformationType.Warning);
                }
                //SetMessaggioWeb(sbMessaggio.ToString());
            }
        }

        void DCRdL_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (View is DetailView)
            {
                BindingList<DCRdL> objects = new BindingList<DCRdL>();
                int idColore = 0;// 0= bianco:stessa AreaDiPoloOid, 1=verde:stesso co di base, 2= giallo  conduttore    CentroOperativoOid == 

                XPQuery<RdL> customersQuery = new XPQuery<RdL>(Sess);
                var customers = customersQuery
                .Where(w => w.UltimoStatoSmistamento.Oid == 1)
                .Where(w => new[] { 4 }.Contains(w.Categoria.Oid))//  .Where(w => new[] { 2, 3, 4 }.Contains(w.Categoria.Oid)) 4=guasto
                    //.Where(w => w.Apparato.Impianto.Immobile.CentroOperativoBase.AreaDiPolo.Oid == AreaDiPoloOid)
                .Where(w => w.Asset.Servizio.Immobile.CentroOperativoBase.Oid == CentroOperativoOid)
                      .Select(s => new
                      {
                          ID = s.Oid,
                          RdLCodice = s.Codice.ToString(),
                          RdLDescrizione = s.Descrizione,
                          RdLSollecito = s.RdLNotes.Count() > 0 ? "Si" : "No",//.ToString(),
                          RichiedenteDescrizione = string.Format("{0} tel.:{1} - {2}", s.Richiedente.NomeCognome, s.Richiedente.TelefonoRichiedente, s.Richiedente.PhoneMobString),
                          EdificioDescrizione = s.Asset.Servizio.Immobile.Descrizione,
                          ImpiantoDescrizione = s.Servizio.Descrizione,
                          IndirizzoDescrizione = s.Immobile.Indirizzo.FullName,
                          DataPianificata = s.DataPianificata.ToString("dd/MM/yyyy HH:mm"),
                          CentroOperativoOid = s.Asset.Servizio.Immobile.CentroOperativoBase.Oid == CentroOperativoOid,
                          RisorseTeamOid = s.Asset.Servizio.Immobile.Conduttoris.Where(w => w.RisorseTeam.Oid == RisorseTeamOid).Count() > 0
                      });

                foreach (var dr in customers)
                {
                    if (dr.CentroOperativoOid)
                    {
                        idColore = 1;
                    }
                    else if (dr.RisorseTeamOid)
                    {
                        idColore = 2;
                    }
                    else
                    {
                        idColore = 0;
                    }
                    DCRdL objdcrdl = new DCRdL()
                                   {
                                       ID = dr.ID,//Guid.NewGuid, /*obj.ID = Newid++;*/
                                       //RdL = dr,
                                       RdLCodice = dr.RdLCodice,
                                       RdLDescrizione = dr.RdLDescrizione,
                                       RdLSollecito = string.Format(dr.RdLSollecito),
                                       RichiedenteDescrizione = dr.RichiedenteDescrizione,
                                       EdificioDescrizione = dr.EdificioDescrizione,
                                       ImpiantoDescrizione = dr.ImpiantoDescrizione,
                                       IndirizzoDescrizione = dr.IndirizzoDescrizione,
                                       DataPianificata = dr.DataPianificata,
                                       idColore = idColore
                                   };

                    objects.Add(objdcrdl);
                }

                e.Objects = objects;
            }
        }

        #endregion



        private void AccettazioneSO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //Accettazione Orario Soddisfazione.
            /*//      imposta il nuovo tepo di allarme per il popap    
 *          //  statoAutorizzativo = 0 [quando si crea la RdL]                  ****@@ inizio
            //  statoAutorizzativo = 1 [quando si crea la notifica]            
            //  statoAutorizzativo = 2 [quando dichiara il tecnico]            
            //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]       ****@@
 *          //  statoAutorizzativo = 4 [quando in trasferimento il tecnico]                        */
            ChengeNotificaRdL(sender, e, "ASO");
        }

        private void VisualizzaRdLAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();

            NotificaRdL GetNotificaRdL = xpObjectSpace.GetObject<NotificaRdL>((NotificaRdL)View.CurrentObject);
            RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetNotificaRdL.RdL.Oid);

            var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);

            view.Caption = string.Format("Richiesta di Lavoro");
            view.ViewEditMode = ViewEditMode.Edit;
            Application.MainWindow.SetView(view);

        }


        private void acDchiarazioneArrivo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChengeNotificaRdL(sender, e, "DAR");
        }

        private void acInizioTrasferimento_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChengeNotificaRdL(sender, e, "ITR");
        }

        private void acAccettaDataSO_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ChengeNotificaRdL(sender, e, "ASO");
        }

        private void ChengeNotificaRdL(object sender, SimpleActionExecuteEventArgs e, string tipoCambio)
        {
            //  statoAutorizzativo = 0 [quando si crea la RdL]           //  statoAutorizzativo = 1 [quando si crea la notifica]
            //  statoAutorizzativo = 2 [quando dichiara il tecnico]      //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
            //System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000000);
            string sbMessaggio = string.Empty;

            if (View is DetailView)
            {
                DetailView dv = View as DetailView;
                if (View.ObjectTypeInfo.Type == typeof(NotificaRdL))//.Editor).GetSelectedObjects().Count > 0)
                {
                    NotificaRdL nrdl = (NotificaRdL)dv.CurrentObject;
                    sbMessaggio = SetNotificaValori(tipoCambio, sbMessaggio, nrdl);
                }
            }

            if (View is ListView)
            {
                int Conta = (((ListView)View).Editor).GetSelectedObjects().Count;
                if (Conta > 0)
                {
                    //var curObj = (XPLiteObject)(((ListView)View).Editor).GetSelectedObjects()[0];
                    if (View.Id == "NotificaRdL_ListView" || View.Id == "NotificaRdL_ListView_SK")
                    {
                        // var lstSelControlliNormativiRifLog = (((ListView)View).Editor).GetSelectedObjects().Cast<ControlliNormativiRifLog>().ToList<ControlliNormativiRifLog>();
                        List<NotificaRdL> curObj = (((ListView)View).Editor).GetSelectedObjects().Cast<NotificaRdL>().ToList();
                        foreach (NotificaRdL cur in curObj)
                        {
                            NotificaRdL nrdl = (NotificaRdL)cur;
                            sbMessaggio = SetNotificaValori(tipoCambio, sbMessaggio, nrdl);
                            View.ObjectSpace.ReloadObject(nrdl);
                        }

                        View.Refresh();
                    }
                }
            }
        }

        private string SetNotificaValori(string tipoCambio, string sbMessaggio, NotificaRdL nrdl)
        {
            IObjectSpace xpObjectSpace = null;
            if (View is ListView)
            {
                //xpObjectSpace = Application.CreateObjectSpace();
                xpObjectSpace = View.ObjectSpace;
            }
            else
            {
                xpObjectSpace = View.ObjectSpace;
            }
            if (xpObjectSpace != null)
            {
                nrdl.RdL.Reload();// aggiorna la RdL caricata nella notifica, poiche è delay (ritardata)
                RdL rdl = nrdl.RdL;//xpObjectSpace.GetObjectByKey<RdL>(rdl.Oid)
                if (rdl != null)
                {
                    int addTempo = 10;
                    bool SpediscieMail = false;
                    int StatoxRegRdl = 0;
                    string location = "";
                    StatoAutorizzativo SAutorizzazione = rdl.StatoAutorizzativo;
                    DateTime DataDichiaratadiArrivo = DateTime.MinValue;
                    try
                    {
                        int OidAutorizzaNuovo = 0;
                        switch (tipoCambio)
                        {
                            case "DAR":   // il TECNICO HA DICHIARATO ORARIO DI ARRIVO   
                                #region in caso di TECNICO HA DICHIARATO ORARIO DI ARRIVO
                                nrdl.StatusNotifica = TaskStatus.InProgress;
                                DataDichiaratadiArrivo = DateTime.Now.AddMinutes(125);
                                OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1; //xpObjectSpace.GetObjectByKey<CAMS.Module.DBTask.StatoAutorizzativo>(2);                            
                                addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;
                                // persintalias
                                nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
                                //nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
                                //nrdl.LabelListView = (LabelListView)nrdl.Label;
                                //  imposto le date di avviso
                                nrdl.StartDate = DateTime.Now.AddMinutes(2); // DATA DI INIZIO TIMER DI ALLARME
                                nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);// DATA SCADENZA ALLARME 
                                ((ISupportNotifications)nrdl).AlarmTime = nrdl.StartDate.AddMinutes(addTempo);
                                nrdl.MessaggioUtente = string.Format("il Tecnico ha Dichiarato la seguente data di arrivo {0}, " +
                                    "\r\n  Accettare per confermare e aggiornare l'attuale data pianificata({1}) .", DataDichiaratadiArrivo, rdl.DataPianificata);
                                // rdl  AGGIORNA RDL
                                rdl.StatoAutorizzativo = View.ObjectSpace.GetObjectByKey<StatoAutorizzativo>(2);
                                rdl.DataPrevistoArrivo = DateTime.Now.AddMinutes(125);
                                rdl.DataAggiornamento = DateTime.Now;
                                SpediscieMail = true;
                                sbMessaggio = sbMessaggio + string.Format("RdL ({0}), il Tecnico ha dichiarato {1}", rdl.Oid, rdl.DataPrevistoArrivo);
                                // AGGIORNA REG RDL    @@@   allinea REGISTRO RDL 
                                StatoxRegRdl = 2;
                                #endregion

                                break;
                            case "ASO":  // la SO ha ACCETTATO L'ORARIO DEL TECNICO
                                #region in caso di ACCETTATO L'ORARIO DEL TECNICO
                                nrdl.StatusNotifica = TaskStatus.InProgress;
                                OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1; //xpObjectSpace.GetObjectByKey<CAMS.Module.DBTask.StatoAutorizzativo>(2);                            
                                addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;
                                nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
                                nrdl.MessaggioUtente = string.Format("Autorizzando!, La data di Arrivo dichiarata dal tecnico({0}) aggiornerà la precedente data data pianificata({1})",
                                    rdl.DataPrevistoArrivo, rdl.DataPianificata);
                                //  imposto le date di avviso                
                                nrdl.StartDate = DateTime.Now.AddMinutes(2); // DATA DI INIZIO TIMER DI ALLARME
                                nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);// DATA SCADENZA ALLARME 
                                ((ISupportNotifications)nrdl).AlarmTime = nrdl.StartDate.AddMinutes(addTempo);
                                //inposto la DataArrivoDichiarata come datapianificata
                                DateTime DataPrevistoArrivoDichTecnico = rdl.DataPrevistoArrivo;
                                if (nrdl.EndOn != null && nrdl.StartOn != null)
                                {
                                    int minute = Math.Abs((nrdl.EndOn - nrdl.StartOn).Minutes);
                                    nrdl.StartOn = DataPrevistoArrivoDichTecnico;
                                    if (minute < 15)
                                        minute = 15;
                                    nrdl.EndOn = nrdl.StartOn.AddMinutes(minute);
                                }

                                // imposto la rdl
                                rdl.DataPianificata = nrdl.StartOn;
                                rdl.DataPianificataEnd = nrdl.EndOn;
                                rdl.StatoAutorizzativo = View.ObjectSpace.GetObjectByKey<StatoAutorizzativo>(3);
                                rdl.DataAggiornamento = DateTime.Now;
                                SpediscieMail = true;
                                string line = string.Format("RdL ({0}), è stata Accettata la Data Dichiarata dal Tecnico({1}), e Aggiornata la Data Pianificata", rdl.Oid, rdl.DataPrevistoArrivo);
                                sbMessaggio = sbMessaggio + line;
                                // AGGIORNA REG RDL    @@@   allinea REGISTRO RDL 
                                StatoxRegRdl = 3;
                                #endregion
                                break;
                            case "ITR":  // IL TECNICO HA PRESO IN CARICO L'INTERVENTO (IN TRASFERIMENTO)
                                #region in caso di ACCETTATO L'ORARIO DEL TECNICO
                                OidAutorizzaNuovo = rdl.StatoAutorizzativo.Oid + 1; //xpObjectSpace.GetObjectByKey<CAMS.Module.DBTask.StatoAutorizzativo>(2);                            
                                addTempo = SetaddTempo(OidAutorizzaNuovo, rdl);   //  ricava il deltatempo dalla sringa 0:30;1:20;2:30;3:40;
                                // persintalias
                                nrdl.LabelListView = (LabelListView)SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);
                                //nrdl.Label = SetLabelNotifica(rdl, nrdl, OidAutorizzaNuovo);// in attesa della dichiarazione del tecnico #############
                                //nrdl.LabelListView = (LabelListView)nrdl.Label;

                                nrdl.MessaggioUtente = string.Format("Il Tecnico ha Iniziato il Trasferimento alla data ({0})", DateTime.Now);
                                //  imposto le date di avviso

                                nrdl.StatusNotifica = TaskStatus.Completed;
                                ((ISupportNotifications)nrdl).AlarmTime = null;
                                // AGGIORNO RDL   AGGIORNO RDL 
                                rdl.StatoAutorizzativo = View.ObjectSpace.GetObjectByKey<StatoAutorizzativo>(4);
                                rdl.UltimoStatoSmistamento = View.ObjectSpace.GetObjectByKey<StatoSmistamento>(3); // emessa in lavorazione
                                rdl.UltimoStatoOperativo = View.ObjectSpace.GetObjectByKey<StatoOperativo>(1);// IN ATTESA DI ESSERE PRESA IN CARICO
                                rdl.DataAggiornamento = DateTime.Now;
                                SpediscieMail = true;

                                StatoxRegRdl = 4;
                                #endregion
                                break;

                            default:// altro non pervenuto
                                ((ISupportNotifications)nrdl).AlarmTime = null;
                                break;
                        }

                        if (StatoxRegRdl > 0)
                        {
                            rdl.RegistroRdL.DataPianificata = nrdl.StartOn;
                            rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;
                            rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;
                            rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                            rdl.RegistroRdL.DataAggiornamento = rdl.DataAggiornamento;
                            if (StatoxRegRdl == 2)
                                rdl.RegistroRdL.DataPrevistoArrivo = rdl.DataPrevistoArrivo;
                        }
                    }
                    catch (Exception exce)
                    {
                        sbMessaggio = "Errore";
                        sbMessaggio = sbMessaggio + string.Format("The process failed: {0}", exce.ToString());
                    }
                    nrdl.Save();
                    xpObjectSpace.CommitChanges();

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
                            if (!string.IsNullOrEmpty(Messaggio))
                            {
                                string Titolo = "Trasmissione Avviso Eseguita!!";
                                string AlertMessaggio = string.Format("Messaggio di Spedizione - {0}", Messaggio);
                                SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                            }
                        }
                        catch (Exception ex)
                        {

                            SetMessaggioWeb(string.Format("Messaggio di eccezione: {0}", ex.Message),
                                "Trasmissione Avviso non Eseguita!!", InformationType.Error);
                            //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                        }

                    }
                    #endregion

                }
            }
            return sbMessaggio;
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




        #region CAMBIA RISORSA

        int OidRdLCambiaRisorsaDC = 0;

        private void pupCambiaRisorsaDC_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //System.Text.StringBuilder sbMessaggio = new System.Text.StringBuilder("", 32000);
            if (View is DetailView && View.ObjectTypeInfo.Type == typeof(NotificaRdL))
            {
                DetailView dv = View as DetailView;
                var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                List<DCRisorseTeamRdL> lst_DCRisorseTeamRdL_Selezionate = ((List<DCRisorseTeamRdL>)((((Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<DCRisorseTeamRdL>().ToList<DCRisorseTeamRdL>()));

                if (xpObjectSpace != null && lst_DCRisorseTeamRdL_Selezionate.Count > 0)
                {


                    int OidRisorsaTeam = lst_DCRisorseTeamRdL_Selezionate.Select(s => s.OidRisorsaTeam).First();
                    AppuntamentiRisorse AppRisorse = xpObjectSpace.FindObject<AppuntamentiRisorse>
                        (DevExpress.Data.Filtering.CriteriaOperator.Parse("RisorseTeam.Oid = ?", OidRisorsaTeam));
                    //AppuntamentiRisorse AppRisorse = xpObjectSpace.GetObjectByKey<AppuntamentiRisorse>(OidRisorsaTeam); ;
                    NotificaRdL nrdl = (NotificaRdL)dv.CurrentObject;
                    //string location = nrdl.Location;
                    if (AppRisorse != null && nrdl !=null)
                    {
                        int i = 0;
                        while (nrdl.Resources.Count > 0)
                        {
                            nrdl.Resources.Remove(nrdl.Resources[0]);
                            i++;
                        }

                        nrdl.Resources.Add(AppRisorse);
                        nrdl.UpdateResourceIds();
                        #region cambio risorsa
                        RdL rdl = nrdl.RdL;
                        int addTempo = SetaddTempo(0, rdl);
                        nrdl.LabelListView = LabelListView.in_attesa_di_dichiarazione_tecnico;
                        nrdl.Status = 1;
                        nrdl.StatusNotifica = TaskStatus.WaitingForSomeoneElse;
                        nrdl.StartDate = DateTime.Now.AddMinutes(2);
                        nrdl.DueDate = nrdl.StartDate.AddMinutes(addTempo);
                        nrdl.RemindIn = TimeSpan.Zero;

                        ((DevExpress.Persistent.Base.General.ISupportNotifications)nrdl)
                                           .AlarmTime = DateTime.Now.AddMinutes(addTempo);

                        if (nrdl.StartOn < DateTime.Now.AddMinutes(1))
                            nrdl.StartOn = DateTime.Now.AddMinutes(1);/// rdl.DataPianificata;//   data pianificata di inizio lavoro
                        int minutidiintervento = 60;
                        //if (rdl.Problema != null)
                        //    minutidiintervento = Convert.ToInt32(rdl.Problema.Problemi.Valore);
                        if (rdl.Prob != null)
                            minutidiintervento = Convert.ToInt32(rdl.Prob.Valore);
                        nrdl.EndOn = nrdl.StartOn.AddMinutes(minutidiintervento);// data pianificata di fine lavoro
                        if (rdl.DataPianificata != nrdl.StartOn)
                            nrdl.MessaggioUtente = string.Format("Cambio Risorsa: da {0} a {1}.La Data di Pianificazione sarà aggiornata da {2} a {3} ( di RdL)",
                                AppRisorse.Caption, rdl.RisorseTeam.FullName,
                                rdl.DataPianificata, rdl.Oid, nrdl.StartOn);
                        //sbMessaggio.AppendLine("Cambio Risorsa Eseguita, Salvare per confermare!");
                        #endregion
                        //using (Util u = new Util())
                        //{
                        //    nrdl = u.SetNotificaRdL(nrdl, nrdl.RdL, location, 10, null, ref sbMessaggio);
                        //    nrdl.MessaggioUtente = nrdl.MessaggioUtente + " Cambio Risorsa Eseguita!!";
                        //}

                        nrdl.Save();
                        xpObjectSpace.CommitChanges();
                    }                   
                    SetMessaggioWebInterventi("Eseguita Cambio Risorsa assegnata all'intervento!!!", "Selezione", InformationType.Info);
                }
                else
                {
                    SetMessaggioWebInterventi("nessuna selezione eseguita!!!", "Selezione", InformationType.Warning);
                }
                //SetMessaggioWeb(sbMessaggio.ToString());
            }

        }

        private void pupCambiaRisorsaDC_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    NotificaRdL nrdl = ((DetailView)View).CurrentObject as NotificaRdL;
                    if (nrdl.RdL != null)
                    {
                        OidRdLCambiaRisorsaDC = nrdl.RdL.Oid;

                        NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
                        objectSpace.ObjectsGetting += CambiaRisorsaDC_objectSpace_ObjectsGetting;

                        CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
                                                                                    "DCRisorseTeamRdL_LookupListView");
                        SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Ordinamento", DevExpress.Xpo.DB.SortingDirection.Ascending);
                        DCRisorseTeamRdL_Lookup.Sorting.Add(srtProperty);

                        ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);

                        var dc = Application.CreateController<DialogController>();
                        e.DialogController.SaveOnAccept = false;
                        e.View = lvk;
                    }
                }
            }
        }


        void CambiaRisorsaDC_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            int RTeamOid = 0;
            if (View is DetailView)
            {
                BindingList<DCRisorseTeamRdL> objects = new BindingList<DCRisorseTeamRdL>();
                if (OidRdLCambiaRisorsaDC > 0)
                {
                    RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(OidRdLCambiaRisorsaDC);
                    RTeamOid = rdl.RisorseTeam.Oid;
                    if (rdl.RisorseTeam != null)
                    {
                        using (DB db = new DB())
                        {
                            int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                            objects = db.GetTeamRisorse_for_RdL(rdl.Immobile.Oid, rdl.UltimoStatoSmistamento.Oid, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, RTeamOid);
                        }
                    }
                }
                //    e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r =>
                //    r.Ordinamento)
                //    .ThenBy(r => r.NumerorAttivitaTotaliTT)
                //    .ThenBy(r => r.Distanzakm)
                //    .ThenBy(r => r.NumerorAttivitaTotaliMP).ToList());
                //    e.Objects = new BindingList<DCRisorseTeamRdL>(objects.OrderBy(r => r.Ordinamento).ToList());
                e.Objects = objects;// objects.Where(s => s.OidRisorsaTeam != RTeamOid).ToList();//
            }
        }
        #endregion

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

        private void SetMessaggioWebInterventi(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
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






