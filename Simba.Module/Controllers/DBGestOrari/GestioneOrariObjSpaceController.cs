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
    public partial class GestioneOrariObjSpaceController : ViewController
    {
        private DateTime Adesso = DateTime.Now;

        bool suppressOnChanged = false;
        bool suppressOnCommitting = false;
        bool suppressOnCommitted = false;

        bool IsNuovaRdL = false;
        bool CambiataRisorsa = false;

        public GestioneOrariObjSpaceController()
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

            //ObjectSpace.ObjectDeleted += ObjectSpace_ObjectDeleted; 
            //ObjectSpace.Reloaded += ObjectSpace_Reloaded;
            suppressOnChanged = false;
            suppressOnCommitted = false;
            suppressOnCommitting = false;
            //Adesso = SetVarSessione.dataAdessoDebug;
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
            suppressOnChanged = false;
            suppressOnCommitted = false;
            suppressOnCommitting = false;

            base.OnDeactivated();
        }


        void ObjectSpace_Committing(object sender, CancelEventArgs e)
        {
            //AddLog(IObjectSpace xpObjectSpace, string UserName, string Descrizione, string SessioneWEB)
            IObjectSpace xpo = Application.CreateObjectSpace();
            CAMS.Module.Classi.Logger.AddLog(xpo, SecuritySystem.CurrentUserName, "A1 INIZIO GestioneOrariObjSpaceController ObjectSpace_Committing", View.ObjectSpace.TypesInfo.ToString());
            if (!suppressOnCommitting)
            {
                try
                {
                    if (Adesso == null)
                        Adesso = DateTime.Now;
                    IsNuovaRdL = false;
                    suppressOnCommitting = true;
                    suppressOnChanged = true;
                    //   System.Diagnostics.Debug.WriteLine("inizio try ObjectSpace_Committing v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                    System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                    //setLog(SecuritySystem.CurrentUserName, "inizio ObjectSpace_Committing v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                    string View_id = View.Id;
                    List<string> sbMessaggio = new List<string>();

                    IObjectSpace os = (IObjectSpace)sender;
                    DevExpress.Xpo.Session osSess = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)os).Session;
                    for (int i = os.ModifiedObjects.Count - 1; i >= 0; i--)
                    {
                        object item = os.ModifiedObjects[i];
                        if (typeof(tbcalendario).IsAssignableFrom(item.GetType()))
                        {
                            GestioneOrari curGestioneOrari = View.CurrentObject as GestioneOrari;
                            curGestioneOrari.SAutorizzativo = os.GetObjectByKey<StatoAutorizzativo>(9);
                            SetMessaggioWeb("calendario Oid" + curGestioneOrari.Oid.ToString(), "Titolo", InformationType.Info, false);
                        }

                        if (typeof(GestioneOrari).IsAssignableFrom(item.GetType()))
                        {
                            //this.SmistamentoOid_ObjectsCache.Clear();
                            //this.OperativoObjectsCache.Clear();
                            //this.RisorsaTeamObjectsCache.Clear();

                            GestioneOrari GesOrari = (GestioneOrari)item;
                            if (GesOrari != null)
                            {
                                //rdl.DataAggiornamento = DateTime.Now;
                                if (GesOrari.Oid == -1)
                                {
                                    //IsNuovaRdL = true;
                                    //rdl.DataCreazione = DateTime.Now;
                                    //if (rdl.DataRichiesta == rdl.vDataRichiesta)
                                    //{
                                    //    rdl.DataRichiesta = rdl.DataCreazione;
                                    //}
                                }

                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing creato registro v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                                //************************************************************************************************************************
                                //    CASO IN CUI LA RDL è GIA STATA CREATA E QUINDI DI MODIFICA      
                                if (GesOrari.Oid > 0) //   non e nuova è una modifica ( compreso assegazione e quindi notifica )
                                {

                                    //    ISTANZIO SESSIONE  sql = string.Format("select RDL.RisorseTeam from RDL where RDL.Oid = {0}", rdl.Oid);                      

                                    GestioneOrari oldDB_GesOrari = xpo.GetObject<GestioneOrari>(GesOrari);//  recupero da DB prima di salvare
                                }  //  FINE IF RdL.Oid>0 (se non è appena creata è sempre cosi)
                                else
                                {
                                    //old_RisorseTeam = null;
                                    //old_StatoOperativo = null;
                                }
                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing modifica registro v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                ///  fine rdl.Oid>0 ----------------------------------------------------
                                #region AZIONE CHE SI FANNO SEMPRE

                                GesOrari.Save();

                                if (Adesso == null)
                                    Adesso = DateTime.Now;

                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo salva rdl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                #endregion
                            }
                        }
                    }
                    if (Adesso == null)
                        Adesso = DateTime.Now;
                }
                catch
                {
                    suppressOnChanged = false;
                    suppressOnCommitted = false;
                    suppressOnCommitting = false;
                }
                finally
                {
                    suppressOnChanged = false;
                    suppressOnCommitted = false;
                    suppressOnCommitting = false;
                }

                CAMS.Module.Classi.Logger.AddLog(xpo, SecuritySystem.CurrentUserName, "fine GestioneOrariObjSpaceController ObjectSpace_Committing v:" + View.Id.ToString() + " tempo ", (DateTime.Now - Adesso).TotalSeconds.ToString());

            }
        }

        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            IObjectSpace xpo = Application.CreateObjectSpace();
            IObjectSpace xpObjectSpaceGestioneOrari = (IObjectSpace)sender;
            CAMS.Module.Classi.Logger.AddLog(xpo, SecuritySystem.CurrentUserName, "INIZIO GestioneOrariObjSpaceController ObjectSpace_Committed dopo salva GestioneOrari " + View.Id.ToString() + " tempo ", (DateTime.Now - Adesso).TotalSeconds.ToString());
            if (!suppressOnCommitted)
            {
                suppressOnChanged = true;
                suppressOnCommitted = true;
                suppressOnCommitting = true;
                try
                {
                    string View_id = "";
                    if (Adesso == null)
                        Adesso = DateTime.Now;

                    DateTime ultimadata = DateTime.MinValue;                    //int idmin = 0;    //int oidRegSmistamento = 0;
                    GestioneOrari gesOrari = (GestioneOrari)View.CurrentObject;
                    if (gesOrari != null)
                    {
                        View_id = View.Id;
                        #region crea record inserimento avviso notifica spedizione mail
                        int[] tempoInt = new int[] { 60, 120, 240 };
                        #endregion
                        if (gesOrari == null)
                        {
                            #region CREO REGISTRO RDL SE NULLO   ( DA AGGIORNARE POI IL NUMERO OID IN DESCRIZIONE)
                            //Commesse cm = rdl.Immobile.Commesse;
                            try
                            {

                            }
                            catch
                            {

                            }
                            //rdl.RegistroRdL = new RegistroRdL(((XPObjectSpace)xpObjectSpaceRdL).Session)
                            //{
                            //    Descrizione = rdl.Descrizione,
                            //    Apparato = rdl.Apparato,
                            //    Categoria = rdl.Categoria,
                            //    Priorita = rdl.Priorita,
                            //    Prob = rdl.Prob,
                            //    //Causa = vCausa,
                            //    UltimoStatoSmistamento = rdl.UltimoStatoSmistamento,
                            //    old_SSmistamento_Oid = rdl.UltimoStatoSmistamento.Oid,
                            //    DATA_CREAZIONE_RDL = rdl.DataCreazione,
                            //    DataPianificata = rdl.DataPianificata,
                            //    DataAssegnazioneOdl = rdl.DataAssegnazioneOdl,
                            //    DataAzioniTampone = rdl.DataAzioniTampone,
                            //    DataFermo = rdl.DataFermo,
                            //    DataInizioLavori = rdl.DataInizioLavori,
                            //    DataPrevistoArrivo = rdl.DataPrevistoArrivo,
                            //    Utente = SecuritySystem.CurrentUserName,
                            //    UtenteUltimo = SecuritySystem.CurrentUserName,
                            //    MostraDataOraFermo = cm.MostraDataOraFermo ? true : false,
                            //    MostraDataOraRiavvio = cm.MostraDataOraFermo ? true : false,
                            //    MostraDataOraSopralluogo = cm.MostraDataOraSopralluogo ? true : false,
                            //    MostraDataOraAzioniTampone = cm.MostraDataOraAzioniTampone ? true : false,
                            //    MostraDataOraInizioLavori = cm.MostraDataOraInizioLavori ? true : false,
                            //    MostraDataOraCompletamento = cm.MostraDataOraCompletamento ? true : false,
                            //    MostraElencoCauseRimedi = cm.MostraElencoCauseRimedi ? true : false,

                            //};
                            gesOrari.Save();

                            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os)); v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                            #endregion
                        }

                        if (gesOrari != null)
                        {
                            #region modifiche delle proprietà del regedl

                            #endregion
                        }



                        System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA DI AGG RISORSA v:" + View_id + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                        //--------------------

                        #region invia messaggio
                        // cambio qualunque proprieta
                        string Messaggio = string.Empty;

                        //bool isGranted = SecuritySystem.IsGranted(new
                        //     DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpaceRdL, typeof(RegoleAutoAssegnazioneRdL),
                        //   DevExpress.ExpressApp.Security.SecurityOperations.Read));

                        //--------------------------------------------------------------------- 
                        #endregion


                        //xpObjectSpaceRdL.CommitChanges();

                    }

                }
                catch
                {
                    suppressOnChanged = false;
                    suppressOnCommitted = false;
                    suppressOnCommitting = false;
                }
                finally
                {
                    suppressOnChanged = false;
                    suppressOnCommitted = false;
                    suppressOnCommitting = false;
                }
                suppressOnChanged = false;
                suppressOnCommitted = false;
                suppressOnCommitting = false;
            }
            CAMS.Module.Classi.Logger.AddLog(xpo, SecuritySystem.CurrentUserName, "fine GestioneOrariObjSpaceController ObjectSpace_Committed GestioneOrari " + View.Id.ToString() + " tempo ", (DateTime.Now - Adesso).TotalSeconds.ToString());
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
                                //Priorita newPriorita = (Priorita)(e.NewValue);
                                //int minuti = newPriorita.Val;
                                //int da = Convert.ToInt32(minuti / 2);
                                //int a = Convert.ToInt32((minuti - (minuti * 0.1)));             // Possibili valori di numeroCasuale: {1, 2, 3, 4, 5, 6}
                                //Random random = new Random();
                                //int numeroCasuale = random.Next(da, a);
                                //if (numeroCasuale < 16)
                                //    numeroCasuale = 16;
                                //if (numeroCasuale > 16)  //    if (this.Problema != null)
                                //{
                                //    //this.fDataSopralluogo = DateTime.Now.AddMinutes(numeroCasuale);
                                //    //this.fDataAzioniTampone = DateTime.Now.AddMinutes(numeroCasuale);
                                //    //this.fDataInizioLavori = DateTime.Now.AddMinutes(numeroCasuale);
                                //}
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

        private void saAddCircuito_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            //IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                //string aaa = "fdgh";
                if (View is DetailView)
                {
                    Session Sess = ((XPObjectSpace)xpObjectSpace).Session;

                    //GestioneOrari CurrentGestioneOrari = xpObjectSpace.GetObjectByKey<GestioneOrari>();
                    int oidCircuitoSelezionato = ((GestioneOrari)View.CurrentObject).Circuito.Oid;
                    if (oidCircuitoSelezionato > 0)
                    {
                        tbcircuiti CurrentCircuiti = xpObjectSpace.GetObjectByKey<tbcircuiti>(oidCircuitoSelezionato);
                        GestioneOrari CurrentGestioneOrari = xpObjectSpace.GetObjectByKey<GestioneOrari>(((GestioneOrari)View.CurrentObject).Oid);
                        //XPQuery<GestioneOrariCircuiti> customers = Sess.Query<GestioneOrariCircuiti>();
                        XPQuery<GestioneOrariCircuiti> qGestioneOrariCircuiti = new XPQuery<GestioneOrariCircuiti>(Sess);
                        int Conta = qGestioneOrariCircuiti.Where(w => w.Circuiti.Oid == CurrentCircuiti.Oid && w.GestioneOrari.Oid == CurrentGestioneOrari.Oid).Count();
                        if (Conta == 0)
                        {
                            GestioneOrariCircuiti GestioneOrariCircuiti = xpObjectSpace.CreateObject<GestioneOrariCircuiti>();
                            GestioneOrariCircuiti.SetMemberValue("GestioneOrari", CurrentGestioneOrari);
                            GestioneOrariCircuiti.SetMemberValue("Circuiti", CurrentCircuiti);
                            GestioneOrariCircuiti.Save();
                            xpObjectSpace.CommitChanges();
                        }
                    }
                }

            }

        }

        private void sAGOrariNomeGiorno_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            GestioneOrari cGestioneOrari = ((GestioneOrari)View.CurrentObject);
            cGestioneOrari.Save();
            try
            {
                Validator.RuleSet.Validate(ObjectSpace, View.CurrentObject, "AzioneModificaGOrari");
                SetMessaggioWeb("Messaggio sAGOrariNomeGiorno_Execute ok", "Titolo", InformationType.Info, false);

                PropertyEditor editor = ((DetailView)View).FindItem("tbCalendarios") as PropertyEditor;
                ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("tbCalendarios") as ListPropertyEditor;

                //foreach (PropertyEditor editor in ((DetailView)View).GetItems<PropertyEditor>())
                //{
                if (editor.GetType() == typeof(ListPropertyEditor))
                {
                    //ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("Tasks") as ListPropertyEditor;
                    //ListPropertyEditor listPropertyEditor = editor as ListPropertyEditor;
                    var nestedListView = ((ListPropertyEditor)editor).ListView;
                    //var lstRisorseTeam = popupListView.Items[0].CurrentObject;

                    if (nestedListView != null)
                    {
                        if (nestedListView.Id == "GestioneOrari_tbCalendarios_ListView")
                        {
                            if (nestedListView.Control != null)
                            {
                                #region  imposta enum giorno settimana
                                List<DayOfWeek> OidNomeGioneSettimana = new List<DayOfWeek> { };
                                switch (cGestioneOrari.GiornoSettimana)
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
                                if (cGestioneOrari.GestioneOrariCircuitis.Count > 0)
                                    OidCircuiti = cGestioneOrari.GestioneOrariCircuitis.Select(S => S.Circuiti.Oid).ToList();
                                if (cGestioneOrari.Circuito != null)
                                    OidCircuiti.Add(cGestioneOrari.Circuito.Oid);

                                AggiornaCalendario(cGestioneOrari, nestedListView, OidCircuiti, tipoFiltro.FiltraNomeGiorno, DateTime.MinValue, OidNomeGioneSettimana);

                                 
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
        private void sAGOrariData_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            GestioneOrari cGestioneOrari = ((GestioneOrari)View.CurrentObject);
            cGestioneOrari.Save();
            try
            {
                Validator.RuleSet.Validate(ObjectSpace, View.CurrentObject, "AzioneModificaGOrari");
                Validator.RuleSet.Validate(ObjectSpace, View.CurrentObject, "AzioneModificaGOrari_Data");
                Validator.RuleSet.Validate(ObjectSpace, View.CurrentObject, "AzioneModificaGOrari_FascieNulle");

                // se le fascie sono tutte a zero - non procedo con la modifica
                string MessaggioUtente = GetVerificaValoriFasceZERO(cGestioneOrari);
                if (MessaggioUtente == string.Empty)
                {
                    SetMessaggioWeb(MessaggioUtente, "Titolo", InformationType.Info, false);
                    Validator.RuleSet.Validate(ObjectSpace, View.CurrentObject, "AzioneModificaGOrari_Data");
                }
                // 
                PropertyEditor editor = ((DetailView)View).FindItem("tbCalendarios") as PropertyEditor;
                ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("tbCalendarios") as ListPropertyEditor;

                //foreach (PropertyEditor editor in ((DetailView)View).GetItems<PropertyEditor>())
                //{
                if (editor.GetType() == typeof(ListPropertyEditor))
                {
                    //ListPropertyEditor listPropertyEditor = ((DetailView)View).FindItem("Tasks") as ListPropertyEditor;
                    //ListPropertyEditor listPropertyEditor = editor as ListPropertyEditor;
                    var nestedListView = ((ListPropertyEditor)editor).ListView;
                    //var lstRisorseTeam = popupListView.Items[0].CurrentObject;

                    if (nestedListView != null)
                    {
                        if (nestedListView.Id == "GestioneOrari_tbCalendarios_ListView")
                        {
                            if (nestedListView.Control != null)
                            {
                                //ProcessListPropertyEditor(listPropertyEditor);
                                //ListView nestedListView = listPropertyEditor.ListView;
                                List<int> OidCircuiti = new List<int> { 0 };
                                if (cGestioneOrari.GestioneOrariCircuitis.Count > 0)
                                    OidCircuiti = cGestioneOrari.GestioneOrariCircuitis.Select(S => S.Circuiti.Oid).ToList();
                                if (cGestioneOrari.Circuito != null)
                                    OidCircuiti.Add(cGestioneOrari.Circuito.Oid);

                                //((GestioneOrari)View.CurrentObject)

                                AggiornaCalendario(cGestioneOrari, nestedListView, OidCircuiti, tipoFiltro.FiltraData, cGestioneOrari.F1_ModDataOra, GetInteraSettimana());

                                //var mm = nestedListView.CollectionSource.List.Cast<tbcalendario>()
                                //                                    .Where(w => w.data == cGestioneOrari.F1_ModDataOra &&
                                //                                    (OidCircuiti.Contains(w.Circuiti.Oid))
                                //                                    )
                                //                                    .ToList();

                                //foreach (tbcalendario cal in nestedListView.CollectionSource.List.Cast<tbcalendario>()
                                //                            .Where(w => w.data == cGestioneOrari.F1_ModDataOra && OidCircuiti.Contains(w.Circuiti.Oid)))
                                //{
                                //    //cal.idcircuito = 1;
                                //    cal.idTicketEAMS = cGestioneOrari.Oid;
                                //    cal.versione = cal.versione + 1;
                                //    cal.flag_eccezione = 1;
                                //    cal.SecurityUser = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)SecuritySystem.CurrentUser;
                                //    cal.f1startTipoSetOrarioU = cGestioneOrari.f1startTipoSetOrario;
                                //    cal.f1endTipoSetOrarioU = cGestioneOrari.f1endTipoSetOrario;
                                //    cal.f2startTipoSetOrarioU = cGestioneOrari.f2startTipoSetOrario;
                                //    cal.f2endTipoSetOrarioU = cGestioneOrari.f2endTipoSetOrario;
                                //    cal.f3startTipoSetOrarioU = cGestioneOrari.f3startTipoSetOrario;
                                //    cal.f3endTipoSetOrarioU = cGestioneOrari.f3endTipoSetOrario;
                                //    cal.f4startTipoSetOrarioU = cGestioneOrari.f4startTipoSetOrario;
                                //    cal.f4endTipoSetOrarioU = cGestioneOrari.f4endTipoSetOrario;

                                //    cal.Save();
                                //}
                                //foreach (tbcalendario cal in cGestioneOrari.tbCalendarios
                                //                      .Where(w => w.data == cGestioneOrari.F1_ModDataOra && OidCircuiti.Contains(w.Circuiti.Oid)))
                                //{
                                //    //cal.idcircuito = cGestioneOrari.Circuito.circuito.;
                                //    cal.idTicketEAMS = cGestioneOrari.Oid;
                                //    cal.versione = cal.versione + 1;
                                //    cal.flag_eccezione = 1;
                                //    cal.SecurityUser = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)SecuritySystem.CurrentUser;
                                //    cal.f1startTipoSetOrarioU = cGestioneOrari.f1startTipoSetOrario;
                                //    cal.f1endTipoSetOrarioU = cGestioneOrari.f1endTipoSetOrario;
                                //    cal.f2startTipoSetOrarioU = cGestioneOrari.f2startTipoSetOrario;
                                //    cal.f2endTipoSetOrarioU = cGestioneOrari.f2endTipoSetOrario;
                                //    cal.f3startTipoSetOrarioU = cGestioneOrari.f3startTipoSetOrario;
                                //    cal.f3endTipoSetOrarioU = cGestioneOrari.f3endTipoSetOrario;
                                //    cal.f4startTipoSetOrarioU = cGestioneOrari.f4startTipoSetOrario;
                                //    cal.f4endTipoSetOrarioU = cGestioneOrari.f4endTipoSetOrario;
                                //    cal.Save();
                                //    //cal.Reload();
                                //    //nestedListView.Refresh();
                                //}
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
        private string GetVerificaValoriFasceZERO(GestioneOrari cGestioneOrari)
        {
            string msg = string.Empty;
            if (cGestioneOrari.f1startTipoSetOrario == TipoSetOrario.oxx_xx || cGestioneOrari.f1endTipoSetOrario == TipoSetOrario.oxx_xx)
                msg = "Fascie Orarie tutte nulle";
            return msg;
        }

        private List<DayOfWeek> GetInteraSettimana()
        {
            List<DayOfWeek> OidNomeGioneSettimana = new List<DayOfWeek> { };
            OidNomeGioneSettimana.Add(DayOfWeek.Monday);
            OidNomeGioneSettimana.Add(DayOfWeek.Tuesday);
            OidNomeGioneSettimana.Add(DayOfWeek.Wednesday);
            OidNomeGioneSettimana.Add(DayOfWeek.Thursday);
            OidNomeGioneSettimana.Add(DayOfWeek.Friday);
            OidNomeGioneSettimana.Add(DayOfWeek.Saturday);
            OidNomeGioneSettimana.Add(DayOfWeek.Sunday);
            return OidNomeGioneSettimana;
        }

        private void AggiornaCalendario(GestioneOrari cGestioneOrari, ListView nestedListView, List<int> OidCircuiti, tipoFiltro filtro_tipoFiltro
            , DateTime FiltroData = new DateTime(), List<DayOfWeek> OidNomeGioneSettimana = null)
        {

            SecuritySystemUser userGO = View.ObjectSpace.GetObject<SecuritySystemUser>((SecuritySystemUser)Application.Security.User);
            foreach (tbcalendario cal in nestedListView.CollectionSource.List.Cast<tbcalendario>()
                                        .Where(w => OidCircuiti.Contains(w.Circuiti.Oid))
                                        .Where(w => OidNomeGioneSettimana.Contains(w.data.DayOfWeek) || !(filtro_tipoFiltro == tipoFiltro.FiltraNomeGiorno))
                                        .Where(w => w.data == cGestioneOrari.F1_ModDataOra || !(filtro_tipoFiltro == tipoFiltro.FiltraData))
                                        )
            {
                //cal.idcircuito = 1;
                cal.idTicketEAMS = cGestioneOrari.Oid;
                cal.versione = cal.versione + 1;
                cal.flag_eccezione = 1;
                cal.SecurityUser = userGO;
                cal.f1startTipoSetOrarioU = cGestioneOrari.f1startTipoSetOrario;
                cal.f1endTipoSetOrarioU = cGestioneOrari.f1endTipoSetOrario;
                cal.f2startTipoSetOrarioU = cGestioneOrari.f2startTipoSetOrario;
                cal.f2endTipoSetOrarioU = cGestioneOrari.f2endTipoSetOrario;
                cal.f3startTipoSetOrarioU = cGestioneOrari.f3startTipoSetOrario;
                cal.f3endTipoSetOrarioU = cGestioneOrari.f3endTipoSetOrario;
                cal.f4startTipoSetOrarioU = cGestioneOrari.f4startTipoSetOrario;
                cal.f4endTipoSetOrarioU = cGestioneOrari.f4endTipoSetOrario;

                cal.Save();
            }
            foreach (tbcalendario cal in cGestioneOrari.tbCalendarios
                                         .Where(w => OidCircuiti.Contains(w.Circuiti.Oid))
                                        .Where(w => OidNomeGioneSettimana.Contains(w.data.DayOfWeek) || !(filtro_tipoFiltro == tipoFiltro.FiltraNomeGiorno))
                                        .Where(w => w.data == cGestioneOrari.F1_ModDataOra || !(filtro_tipoFiltro == tipoFiltro.FiltraData))
                                               )
            {///  inserire utente 
                //cal.idcircuito = 1;
                cal.idTicketEAMS = cGestioneOrari.Oid;
                cal.versione = cal.versione + 1;
                cal.flag_eccezione = 1;
                cal.SecurityUser = userGO;
                cal.f1startTipoSetOrarioU = cGestioneOrari.f1startTipoSetOrario;
                cal.f1endTipoSetOrarioU = cGestioneOrari.f1endTipoSetOrario;
                cal.f2startTipoSetOrarioU = cGestioneOrari.f2startTipoSetOrario;
                cal.f2endTipoSetOrarioU = cGestioneOrari.f2endTipoSetOrario;
                cal.f3startTipoSetOrarioU = cGestioneOrari.f3startTipoSetOrario;
                cal.f3endTipoSetOrarioU = cGestioneOrari.f3endTipoSetOrario;
                cal.f4startTipoSetOrarioU = cGestioneOrari.f4startTipoSetOrario;
                cal.f4endTipoSetOrarioU = cGestioneOrari.f4endTipoSetOrario;
                cal.Save();
                //cal.Reload();
                //nestedListView.Refresh();
            }
        }



        private void sAConfermaGOrario_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session session = ((XPObjectSpace)xpObjectSpace).Session;
            GestioneOrari cGestioneOrari = ((GestioneOrari)View.CurrentObject);
            int SAutorizzativo_Oid = cGestioneOrari.SAutorizzativo.Oid;
            List<tbcalendario> CalendarioUtenteList = new List<tbcalendario>();
            int tempodiRemindIn = 180;
            string Subject = string.Empty;
            try
            {
                cGestioneOrari.Save();
                View.ObjectSpace.CommitChanges();
                switch (SAutorizzativo_Oid)
                {
                    case 8: //   Impostazione di Filtro
                        #region passaggio stato autorizzativo da 8 a 9 (da impostazione filtro -->> in attesa di approvazione)
                        /// CARICA CALENDARIO UTENTE - le modifiche che ha fatto
                        cGestioneOrari.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(9);
                        cGestioneOrari.Save();
                        xpObjectSpace.CommitChanges();
                        SetMessaggioWeb("Confermata Fine Impostazione Filtro. Inizio fase di Richiesta Modifica Orari", "Azione Eseguita", InformationType.Info, false);
                        #endregion
                        break;

                    case 9:  //   Modifica Orari 
                        #region passaggio stato autorizzativoda 9 a 10 (da impostazione filtro a in attesa di approvazione)
                        //if (cGestioneOrari.SAutorizzativo.Oid == 9)
                        //{
                        /// CARICA CALENDARIO UTENTE - le modifiche che ha fatto
                        cGestioneOrari.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(10);
                        CalendarioUtenteList = cGestioneOrari.tbCalendarios.Where(w => w.idTicketEAMS == cGestioneOrari.Oid).ToList();
                        foreach (tbcalendario cal in CalendarioUtenteList)
                        {
                            tbCalendarioUtente cGestioneOraritbCalendarioUtente = xpObjectSpace.CreateObject<tbCalendarioUtente>();
                            cGestioneOraritbCalendarioUtente.SetMemberValue("GestioneOrari", cGestioneOrari);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("keyAppic", cal.keyAppic);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("data", cal.data);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("Circuiti", cal.Circuiti);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f1startTipoSetOrarioU", cal.f1startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f1endTipoSetOrarioU", cal.f1endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f2startTipoSetOrarioU", cal.f2startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f2endTipoSetOrarioU", cal.f2endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f3startTipoSetOrarioU", cal.f3startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f3endTipoSetOrarioU", cal.f3endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f4startTipoSetOrarioU", cal.f4startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f4endTipoSetOrarioU", cal.f4endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f5startTipoSetOrarioU", cal.f5startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f5endTipoSetOrarioU", cal.f5endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("statoEAMS", "INS");
                            cGestioneOraritbCalendarioUtente.SetMemberValue("wday", cal.wday);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("idTicketEAMS", cal.idTicketEAMS);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("idticket", cal.idticket);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("datains", cal.datains);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("versione", cal.versione);
                            cGestioneOraritbCalendarioUtente.Save();
                        }
                        //////////
                        cGestioneOrari.Save();
                        xpObjectSpace.CommitChanges();

                        //}
                        break;
                    #endregion

                    case 10:// in attesa di approvazione	
                        #region passaggio stato autorizzativoda 10 a 11 (da impostazione filtro a in attesa di approvazione)
                        //if (cGestioneOrari.SAutorizzativo.Oid == 10)
                        //{
                        /// CARICA CALENDARIO UTENTE - le modifiche che ha fatto
                        cGestioneOrari.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(11);
                        CalendarioUtenteList = cGestioneOrari.tbCalendarios.Where(w => w.idTicketEAMS == cGestioneOrari.Oid).ToList();
                        foreach (tbcalendario cal in CalendarioUtenteList)
                        {
                            tbCalendarioUtente cGestioneOraritbCalendarioUtente = xpObjectSpace.CreateObject<tbCalendarioUtente>();
                            cGestioneOraritbCalendarioUtente.SetMemberValue("GestioneOrari", cGestioneOrari);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("keyAppic", cal.keyAppic);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("data", cal.data);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("Circuiti", cal.Circuiti);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f1startTipoSetOrarioU", cal.f1startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f1endTipoSetOrarioU", cal.f1endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f2startTipoSetOrarioU", cal.f2startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f2endTipoSetOrarioU", cal.f2endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f3startTipoSetOrarioU", cal.f3startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f3endTipoSetOrarioU", cal.f3endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f4startTipoSetOrarioU", cal.f4startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f4endTipoSetOrarioU", cal.f4endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f5startTipoSetOrarioU", cal.f5startTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("f5endTipoSetOrarioU", cal.f5endTipoSetOrarioU);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("statoEAMS", "APP");
                            cGestioneOraritbCalendarioUtente.SetMemberValue("wday", cal.wday);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("idTicketEAMS", cal.idTicketEAMS);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("idticket", cal.idticket);
                            cGestioneOraritbCalendarioUtente.SetMemberValue("datains", cal.datains);

                            cGestioneOraritbCalendarioUtente.SetMemberValue("versione", cal.versione);
                            cGestioneOraritbCalendarioUtente.Save();
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
                            , cGestioneOrari.Oid.ToString(), cGestioneOrari.SAutorizzativo.Descrizione, cGestioneOrari.Circuito.circuito, SecuritySystem.CurrentUserName);
                        InsertAvvisoSpedizione(xpObjectSpace, cGestioneOrari, tempodiRemindIn, 13, cGestioneOrari.Descrizione, Subject);
                        //      OidSmistamento = 13;  // Gestione Orari - In attesa di Approvazione

                        // creo popup  di avviso all'approvatore
                        tempodiRemindIn = 120;
                        Subject = string.Format("Notifica Avviso Gestione Orari {0}, Stato: {1}, Circuito: {2}, Utente: {3}"
                            , cGestioneOrari.Oid.ToString(), cGestioneOrari.SAutorizzativo.Descrizione, cGestioneOrari.Circuito.circuito, SecuritySystem.CurrentUserName);
                        InsertAvvisoSpedizione(xpObjectSpace, cGestioneOrari, tempodiRemindIn, 14, cGestioneOrari.Descrizione, Subject);
                        //      OidSmistamento = 14;  // Gestione Orari - In attesa di Approvazione
                        #endregion
                        ///
                        cGestioneOrari.Save();
                        xpObjectSpace.CommitChanges();
                        //}
                        break;
                    #endregion
                    case 12: // Approvato
                             //--> disabilitare tuttele modifiche
                             //--> questo stato è finale per utente non utilizzativo
                             //-- > DA QUESTO STATO PRENDE LA STORE PROCEDURA CHE PASSA ALLA TABELLA SCAMBIO DI APPIC


                        //XPObjectSpace XPOSpaceRdL = (XPObjectSpace)sender;

                        break;

                }
            }
            catch
            {

            }
        }

        private void InsertAvvisoSpedizione(IObjectSpace xpObjectSpace, GestioneOrari cGestioneOrari, int tempodiRemindIn, int OidSmistamento, string Descrizione, string Subject)
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
            NotificaSpedizione.RdLUnivoco = cGestioneOrari.Oid;
            NotificaSpedizione.Status = myTaskStatus.Predisposto;
            NotificaSpedizione.Utente = userGO.UserName;
            NotificaSpedizione.Abilitato = FlgAbilitato.Si;
            NotificaSpedizione.DataCreazione = DateTime.Now;
            NotificaSpedizione.DataAggiornamento = DateTime.Now;
            NotificaSpedizione.Save();
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
    public enum tipoFiltro
    {
        FiltraData,
        FiltraNomeGiorno
    }

}

//GestioneOrari GOraridiApprovazione = xpObjectSpace.CreateObject<GestioneOrari>();
//GOraridiApprovazione.SecurityUser = userGO;
//GOraridiApprovazione.Stagione = cGestioneOrari.Stagione;
//GOraridiApprovazione.dataora_dal = cGestioneOrari.dataora_dal;
//GOraridiApprovazione.dataora_Al = cGestioneOrari.dataora_dal;
//GOraridiApprovazione.F1_ModDataOra = cGestioneOrari.dataora_dal;
//GOraridiApprovazione.Circuito = cGestioneOrari.Circuito;
//GOraridiApprovazione.SAutorizzativo = xpObjectSpace.GetObjectByKey<StatoAutorizzativo>(10);
//GOraridiApprovazione.GestioneOrariUtente = cGestioneOrari;
//GOraridiApprovazione.Save();
//xpObjectSpace.CommitChanges();
//foreach (GestioneOrariCircuiti cir in cGestioneOrari.GestioneOrariCircuitis)
//{
//    GestioneOrariCircuiti GestioneOrariCircuiti = xpObjectSpace.CreateObject<GestioneOrariCircuiti>();
//    GestioneOrariCircuiti.SetMemberValue("GestioneOrari", GOraridiApprovazione);
//    GestioneOrariCircuiti.SetMemberValue("Circuiti", cir.Circuiti);
//    GestioneOrariCircuiti.Save();
//}
//GOraridiApprovazione.Save();

////private void ProcessListPropertyEditor(ListPropertyEditor listPropertyEditor)
////{
////    ListView nestedListView = listPropertyEditor.ListView;

////    foreach (tbcalendario cal in nestedListView.CollectionSource.List.Cast<tbcalendario>())
////    {
////        cal.idcircuito = 1;
////    }

////    PerformLogicWithCurrentListViewObject(nestedListView.CurrentObject);
////    PerformLogicInNestedListViewController(listPropertyEditor.Frame);
////    //nestedListView.CurrentObjectChanged += nestedListView_CurrentObjectChanged;
////}
////private void PerformLogicWithCurrentListViewObject(Object obj)
////{
////    // Use the object in the nested List View as required.
////    string aa = "a";
////}
////private void PerformLogicInNestedListViewController(Frame nestedFrame)
////{
////    // Use the nested Frame as required.
////    string aa = "a";
////}
