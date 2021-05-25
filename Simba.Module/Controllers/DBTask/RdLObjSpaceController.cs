using CAMS.Module.Classi;
using CAMS.Module.DBAgenda;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBAux;
using CAMS.Module.DBNotifiche;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBTask.Guasti;
//using DevExpress.CodeRush.StructuralParser;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLObjSpaceController : ViewController
    {
        //IObjectSpace os = null;
        //List<StatoSmistamento> SmistamentoObjectsCache = new List<StatoSmistamento>();
        List<int> SmistamentoOid_ObjectsCache = new List<int>();
        List<StatoOperativo> OperativoObjectsCache = new List<StatoOperativo>();
        List<RisorseTeam> RisorsaTeamObjectsCache = new List<RisorseTeam>();
        List<string> PropertyObjectsCache = new List<string>();
        StatoOperativo old_StatoOperativo = null;
        RisorseTeam old_RisorseTeam = null;
        StatoSmistamento old_StatoSmistamento = null;
        /// <summary>
        ///    nuove dichirazioni   
        /// </summary>
        int old_OidStatoSmistamento = 0;
        bool SpedisciAvvisoPrimaAzione = false;
        bool SpedisciAvvisoAssegnazione = false;

        private DateTime Adesso = DateTime.Now;

        bool suppressOnChanged = false;
        bool suppressOnCommitting = false;
        bool suppressOnCommitted = false;

        bool IsNuovaRdL = false;
        bool CambiataRisorsa = false;

        public RdLObjSpaceController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
            //Adesso = SetVarSessione.dataAdessoDebug;
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            View.ObjectSpace.Committing += ObjectSpace_Committing;
            View.ObjectSpace.Committed += ObjectSpace_Committed;
            setLog(SecuritySystem.CurrentUserName, "A inizio  di  OnActivated:");
            //ObjectSpace.ObjectDeleted += ObjectSpace_ObjectDeleted;
            //ObjectSpace.Committing += ObjectSpace_Committing;
            //ObjectSpace.Committed += ObjectSpace_Committed;
            //ObjectSpace.Reloaded += ObjectSpace_Reloaded;
            suppressOnChanged = false;
            suppressOnCommitted = false;
            suppressOnCommitting = false;
            Adesso = SetVarSessione.dataAdessoDebug;
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
            setLog(SecuritySystem.CurrentUserName, "B1 INIZIO prima di IF ObjectSpace_Committing:");
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
                    setLog(SecuritySystem.CurrentUserName, "inizio ObjectSpace_Committing v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                    string View_id = View.Id;
                    List<string> sbMessaggio = new List<string>();
                    IObjectSpace os = (IObjectSpace)sender;
                    for (int i = os.ModifiedObjects.Count - 1; i >= 0; i--)
                    {
                        object item = os.ModifiedObjects[i];
                        if (typeof(RdL).IsAssignableFrom(item.GetType()))
                        {
                            this.SmistamentoOid_ObjectsCache.Clear();
                            this.OperativoObjectsCache.Clear();
                            this.RisorsaTeamObjectsCache.Clear();

                            RdL rdl = (RdL)item;
                            if (rdl != null)
                            {
                                rdl.DataAggiornamento = DateTime.Now;
                                if (rdl.Oid == -1)
                                {
                                    IsNuovaRdL = true;
                                    rdl.DataCreazione = DateTime.Now;
                                    if (rdl.DataRichiesta == rdl.vDataRichiesta)
                                    {
                                        rdl.DataRichiesta = rdl.DataCreazione;
                                    }
                                }

                                old_OidStatoSmistamento = rdl.old_SSmistamento_Oid;

                                if (rdl.UtenteCreatoRichiesta == null)
                                    rdl.UtenteCreatoRichiesta = SecuritySystem.CurrentUserName;

                                if (rdl.RegistroRdL == null)
                                {
                                    #region CREO REGISTRO RDL SE NULLO   ( DA AGGIORNARE POI IL NUMERO OID IN DESCRIZIONE)
                                    System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing prima os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os)); v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                                    Contratti cm = rdl.Immobile.Contratti;
                                    try
                                    {
                                        Cause vCausa = null;
                                        Problemi vProb = null;
                                        if (rdl.Prob != null)
                                        {
                                            vProb = rdl.Prob;// xpObjectSpaceRRdl.GetObject<ApparatoProblema>(rdl.Problema);//
                                            if (rdl.Causa != null)
                                                vCausa = rdl.Causa;// xpObjectSpaceRRdl.GetObject<ProblemaCausa>(rdl.ProblemaCausa);//
                                        }
                                    }
                                    catch
                                    {
                                        setLog(SecuritySystem.CurrentUserName, "errore su problema causa rimedio");
                                    }
                                    rdl.RegistroRdL = new RegistroRdL(((XPObjectSpace)os).Session)
                                    {
                                        Descrizione = rdl.Descrizione,
                                        Asset = rdl.Asset,
                                        Categoria = rdl.Categoria,
                                        Priorita = rdl.Urgenza,
                                        Prob = rdl.Prob,
                                        //Causa = vCausa,
                                        UltimoStatoSmistamento = rdl.UltimoStatoSmistamento,
                                        old_SSmistamento_Oid = rdl.UltimoStatoSmistamento.Oid,
                                        DATA_CREAZIONE_RDL = rdl.DataCreazione,
                                        DataPianificata = rdl.DataPianificata,
                                        DataAssegnazioneOdl = rdl.DataAssegnazioneOdl,
                                        DataAzioniTampone = rdl.DataAzioniTampone,
                                        DataSopralluogo = rdl.DataSopralluogo, //aggiunto 14/07/2020
                                        DataFermo = rdl.DataFermo,
                                        DataInizioLavori = rdl.DataInizioLavori,
                                        DataPrevistoArrivo = rdl.DataPrevistoArrivo,
                                        Utente = SecuritySystem.CurrentUserName,
                                        UtenteUltimo = SecuritySystem.CurrentUserName,
                                        MostraDataOraFermo = cm.MostraDataOraFermo ? true : false,
                                        MostraDataOraRiavvio = cm.MostraDataOraFermo ? true : false,
                                        MostraDataOraSopralluogo = cm.MostraDataOraSopralluogo ? true : false,
                                        MostraDataOraAzioniTampone = cm.MostraDataOraAzioniTampone ? true : false,
                                        MostraDataOraInizioLavori = cm.MostraDataOraInizioLavori ? true : false,
                                        MostraDataOraCompletamento = cm.MostraDataOraCompletamento ? true : false,
                                        MostraElencoCauseRimedi = cm.MostraElencoCauseRimedi ? true : false,
                                    };

                                    System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os)); v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                    #endregion
                                }

                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing creato registro v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                if (rdl.OdL == null)
                                {
                                    #region CREA ORDINE DI LAVORO SE NULLO SU RDL
                                    setLog(SecuritySystem.CurrentUserName, " creazione odl in ObjectSpace_Committing");  ///  errore su log
                                    string odlDescrizione = rdl.Descrizione;
                                    if (odlDescrizione.Length > 3996)
                                        odlDescrizione = odlDescrizione.Substring(1, 3996) + "...";

                                    rdl.OdL = new OdL(((XPObjectSpace)os).Session)
                                    {
                                        Descrizione = odlDescrizione,
                                        RegistroRdL = rdl.RegistroRdL,
                                        TipoOdL = os.GetObjectByKey<TipoOdL>(1),
                                        DataEmissione = rdl.DataAssegnazioneOdl
                                    };
                                    #endregion
                                }

                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing creato odl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                //************************************************************************************************************************
                                //    CASO IN CUI LA RDL è GIA STATA CREATA E QUINDI DI MODIFICA      
                                if (rdl.Oid > 0) //   non e nuova è una modifica ( compreso assegazione e quindi notifica )
                                {
                                    IObjectSpace xpo = Application.CreateObjectSpace();
                                    //DevExpress.Xpo.Session Sess = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)xpo).Session;
                                    DevExpress.Xpo.Session Sess = ((DevExpress.ExpressApp.Xpo.XPObjectSpace)os).Session;       //    ISTANZIO SESSIONE  sql = string.Format("select RDL.RisorseTeam from RDL where RDL.Oid = {0}", rdl.Oid);                      

                                    RdL oldDB_RdL = xpo.GetObject<RdL>(rdl);//  recupero da DB prima di salvare
                                    old_RisorseTeam = oldDB_RdL.RisorseTeam; //   recupero da DB prima di salvare
                                    int old_RisorseTeamlav = oldDB_RdL.old_RisorseTeam_Oid;
                                    old_StatoOperativo = oldDB_RdL.UltimoStatoOperativo;  //  ------------------

                                    #region GESTIONE DELLE NOTIFICHE DI RDL AttivaGestioneNotificheRdL=VERO  (GESTIONE B2C PINO GAGLIARDUCCI)
                                    int addTempo = 5;
                                    if (!rdl.Asset.Servizio.Immobile.Contratti.AttivaGestioneNotificheRdL) /*!= null &&    rdl.Apparato.Impianto.Immobile.Commesse.AttivaGestioneNotificheRdL)*/
                                        if (rdl.Categoria.Oid == 4 &&
                                            rdl.Asset.Servizio.Immobile.Contratti.LivelloAutorizzazioneGuasto > 0)
                                        {
                                            if (rdl.UltimoStatoSmistamento.Oid == 2 && rdl.UltimoStatoOperativo.Oid == 19)
                                            {
                                                //  statoAutorizzativo = 0 [quando si crea la RdL] 
                                                //  statoAutorizzativo = 1 [quando si crea la notifica]
                                                //  statoAutorizzativo = 2 [quando dichiara il tecnico]
                                                //  statoAutorizzativo = 3 [quando Accetta la Sala Operativa]
                                                //  se la commessa è configurata con livello autorizzativo maggiore di 0 allora crea la notifica                                
                                                //int conta = View.ObjectSpace.GetObjects<NotificaRdL>().Where(w => w.RdL == rdl && rdl.StatoAutorizzativo.Oid >= 1).Count();
                                                int conta = View.ObjectSpace.GetObjects<NotificaRdL>().Where(w => w.RdL == rdl).Count();
                                                if (conta == 0)//  ALLORA è DA CREARE LA NOTIFICA
                                                {
                                                    string location = rdl.Asset.Servizio.Immobile.Indirizzo.FullName;
                                                    NotificaRdL nrdl = View.ObjectSpace.CreateObject<NotificaRdL>();
                                                    nrdl.RdL = rdl;
                                                    using (Util u = new Util())
                                                    { //AppuntamentiRisorse resource = View.ObjectSpace.GetObjectByKey<AppuntamentiRisorse>(rdl.RisorseTeam.Oid);
                                                        int OidRisorsaTeam = rdl.RisorseTeam.Oid;
                                                        AppuntamentiRisorse AppRisorse = View.ObjectSpace.FindObject<AppuntamentiRisorse>
                                                                                        (CriteriaOperator.Parse("RisorseTeam.Oid = ?", OidRisorsaTeam));

                                                        addTempo = u.SetaddTempo(0, rdl);         // tempo di ritardo  
                                                        nrdl = u.SetNotificaRdL(nrdl, rdl, location, 10, AppRisorse, ref sbMessaggio);  // dati notifica                                      
                                                    }
                                                    //----------------                                //  crea notifica
                                                    nrdl.Save();
                                                    // ora aggiorna  stato autorizzativo
                                                    rdl.StatoAutorizzativo = View.ObjectSpace.GetObjectByKey<StatoAutorizzativo>(1);
                                                }
                                                else if (conta > 0) // maggiore di zero quando esiste già la rdl notifiche - 
                                                {// PRENDERE LA MAGGIORE   -  è UN CAMBIO RISORSA O DATA
                                                    int oidnrdl = View.ObjectSpace.GetObjects<NotificaRdL>().Where(w => w.RdL == rdl).Max(s => s.Oid);
                                                    NotificaRdL nrdl = View.ObjectSpace.GetObjectByKey<NotificaRdL>(oidnrdl);
                                                    nrdl.RdL = rdl;
                                                    using (Util u = new Util())
                                                    {
                                                        //AppuntamentiRisorse resource = View.ObjectSpace.GetObjectByKey<AppuntamentiRisorse>(rdl.RisorseTeam.Oid);
                                                        AppuntamentiRisorse resource = View.ObjectSpace.FindObject<AppuntamentiRisorse>
                                                               (DevExpress.Data.Filtering.CriteriaOperator.Parse("RisorseTeam.Oid = ?", rdl.RisorseTeam.Oid));
                                                        addTempo = u.SetaddTempo(0, rdl);         // tempo di ritardo  
                                                        nrdl = u.SetNotificaRdL(nrdl, rdl, string.Empty, 10, resource, ref sbMessaggio);  // dati notifica                                      
                                                    }
                                                    //----------------                                //  crea notifica
                                                    nrdl.Save();
                                                    // ora aggiorna  stato autorizzativo
                                                    rdl.StatoAutorizzativo = View.ObjectSpace.GetObjectByKey<StatoAutorizzativo>(1);
                                                    rdl.UltimoStatoOperativo = View.ObjectSpace.GetObjectByKey<StatoOperativo>(19);
                                                }
                                            }


                                            if (rdl.UltimoStatoSmistamento.Oid == 3) // in lavorazione --   && rdl.UltimoStatoOperativo.Oid == 19
                                            {
                                                if (old_RisorseTeam != rdl.RisorseTeam) // è cambiata la risorsa
                                                {
                                                    //  tolto intervento ad una risosrsa e assegnato ad altra risosrsa
                                                    int conta = View.ObjectSpace.GetObjects<NotificaRdL>()
                                                               .Where(w => w.RdL == rdl && rdl.StatoAutorizzativo.Oid == 4).Count();
                                                    if (conta > 0)
                                                    {
                                                        int oid = View.ObjectSpace.GetObjects<NotificaRdL>()
                                                             .Where(w => w.RdL == rdl && rdl.StatoAutorizzativo.Oid == 4)
                                                             .Max(m => m.Oid);

                                                        NotificaRdL nrdl = View.ObjectSpace.GetObjectByKey<NotificaRdL>(oid);
                                                        nrdl.RdL = rdl;
                                                        using (Util u = new Util())
                                                        {

                                                            AppuntamentiRisorse resource = View.ObjectSpace.FindObject<AppuntamentiRisorse>
                                                                (DevExpress.Data.Filtering.CriteriaOperator.Parse("RisorseTeam.Oid = ?", rdl.RisorseTeam.Oid));
                                                            addTempo = u.SetaddTempo(0, rdl);         // tempo di ritardo  
                                                            nrdl = u.SetNotificaRdL(nrdl, rdl, string.Empty, 10, resource, ref sbMessaggio);  // dati notifica                                      
                                                        }
                                                        //----------------                                //  crea notifica
                                                        // aggiorno lo stato autorizzativo su RdL
                                                        rdl.StatoAutorizzativo = View.ObjectSpace.GetObjectByKey<StatoAutorizzativo>(1);
                                                    }
                                                }
                                            }
                                        }
                                    #endregion

                                    if (rdl.RegistroRdL != null)
                                    {
                                        #region  AGGIORNA DATE DEL REGISTRO RDL    DA      RDL
                                        if (rdl.RegistroRdL.DataSopralluogo != rdl.DataSopralluogo)
                                            rdl.RegistroRdL.DataSopralluogo = rdl.DataSopralluogo;

                                        if (rdl.RegistroRdL.DataCompletamento != rdl.DataCompletamento)
                                            rdl.RegistroRdL.DataCompletamento = rdl.DataCompletamento;

                                        if (rdl.RegistroRdL.DataPianificata != rdl.DataPianificata)
                                            rdl.RegistroRdL.DataPianificata = rdl.DataPianificata;

                                        if (rdl.RegistroRdL.DataAssegnazioneOdl != rdl.DataAssegnazioneOdl)
                                            rdl.RegistroRdL.DataAssegnazioneOdl = rdl.DataAssegnazioneOdl;

                                        if (rdl.RegistroRdL.DataAzioniTampone != rdl.DataAzioniTampone)
                                            rdl.RegistroRdL.DataAzioniTampone = rdl.DataAzioniTampone;

                                        if (rdl.RegistroRdL.DataFermo != rdl.DataFermo)
                                            rdl.RegistroRdL.DataFermo = rdl.DataFermo;

                                        if (rdl.RegistroRdL.DataRiavvio != rdl.DataRiavvio)
                                            rdl.RegistroRdL.DataRiavvio = rdl.DataRiavvio;

                                        //if (rdl.RegistroRdL.DataAzioniTampone != rdl.DataAzioniTampone)
                                        //    rdl.RegistroRdL.DataAzioniTampone = rdl.DataAzioniTampone;

                                        if (rdl.RegistroRdL.DataInizioLavori != rdl.DataInizioLavori)
                                            rdl.RegistroRdL.DataInizioLavori = rdl.DataInizioLavori;

                                        if (rdl.RegistroRdL.DataPrevistoArrivo != rdl.DataPrevistoArrivo)
                                            rdl.RegistroRdL.DataPrevistoArrivo = rdl.DataPrevistoArrivo;

                                        if (rdl.RegistroRdL.UtenteUltimo != SecuritySystem.CurrentUserName)
                                            rdl.RegistroRdL.UtenteUltimo = SecuritySystem.CurrentUserName;

                                        if (rdl.RegistroRdL.DataAggiornamento != rdl.DataAggiornamento)
                                            rdl.RegistroRdL.DataAggiornamento = DateTime.Now;

                                        if (rdl.RegistroRdL.UltimoStatoSmistamento != rdl.UltimoStatoSmistamento)
                                            rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                        if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                            rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;

                                        #endregion

                                        #region AGGIORNA LE- DATE
                                        if (rdl.DataSopralluogo > rdl.DataAzioniTampone)
                                        {
                                            rdl.DataAzioniTampone = rdl.DataSopralluogo;
                                            rdl.RegistroRdL.DataAzioniTampone = rdl.DataSopralluogo;
                                        }

                                        if (rdl.DataSopralluogo > rdl.DataInizioLavori)
                                        {
                                            rdl.DataInizioLavori = rdl.DataSopralluogo;
                                            rdl.RegistroRdL.DataInizioLavori = rdl.DataSopralluogo;
                                        }
                                        #endregion
                                        //   in  smistamento 3 non si puo cambiare la risorsa senza cambiare lo smistamento                                                      
                                        //   E'  CAMBIATO LO SMISTAMENTO
                                        #region  E' CAMBIATO LO STATO SMISTAMENTO
                                        if (old_OidStatoSmistamento != rdl.UltimoStatoSmistamento.Oid || rdl.RegistroRdL.UltimoStatoSmistamento.Oid != old_OidStatoSmistamento) //    if (rdl.RegistroRdL.UltimoStatoSmistamento != rdl.UltimoStatoSmistamento)
                                        {
                                            int nrRdL = rdl.RegistroRdL.RdLes.Count;
                                            int tutte_chiuse = nrRdL;
                                            if (nrRdL > 1)
                                                tutte_chiuse = rdl.RegistroRdL.RdLes.Where(w => w.UltimoStatoSmistamento == rdl.UltimoStatoSmistamento).Count();
                                            //    if (nrRdL == tutte_chiuse)
                                            // if (old_OidStatoSmistamento == 3 && rdl.UltimoStatoSmistamento.Oid != 3 && tutte_chiuse == nrRdL)
                                            if (old_OidStatoSmistamento == 3 && rdl.UltimoStatoSmistamento.Oid != 3) // caso in cui era in lavorazione ad un tecnico
                                            {
                                                rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                                if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                                    rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;

                                                if (rdl.RisorseTeam != null)
                                                {
                                                    #region GESTIONE RISORSA
                                                    // questo scoppia !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!! --- 
                                                    if (rdl.RegistroRdL.RisorseTeam.RegistroRdL != null)
                                                        if (rdl.RegistroRdL.RisorseTeam.RegistroRdL.Oid == rdl.RegistroRdL.Oid)
                                                        {
                                                            rdl.RegistroRdL.RisorseTeam.RegistroRdL = null;
                                                            rdl.RegistroRdL.RisorseTeam.TipoStatoConnessione = TipoStatoConnessione.Connesso;
                                                            if (rdl.RegistroRdL.RisorseTeam.RisorsaCapo != null)
                                                                rdl.RegistroRdL.RisorseTeam.RisorsaCapo.Disponibilita = false;     //Risorse r = xpObjectSpaceRdL.GetObject<Risorse>(RisorseTeam.RisorsaCapo);          //r.Disponibilita = false;
                                                        }
                                                    rdl.RegistroRdL.RisorseTeam.Save();
                                                    rdl.RegistroRdL.Save();
                                                    rdl.Save();
                                                    #endregion
                                                }
                                            }
                                            //else if (old_OidStatoSmistamento == 10 && rdl.UltimoStatoSmistamento.Oid != 10)
                                            //{
                                            //    rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                            //    if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                            //        rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                                            //}
                                            else if (tutte_chiuse == nrRdL)
                                            {
                                                rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                                if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                                    rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                                            }
                                            //  se la risorsa è diversa   -  E' STATA CAMBIATA LA RISORSA                                   

                                            if (rdl.RegistroRdL.RisorseTeam != rdl.RisorseTeam) // se la risorsa vecchia è diversa dalla risorsa nuova 
                                            {  //   1) aggiorno comunque e sempre la RISORSA DEL REGISTRO CON LA RISORSA DELLA RDL
                                                RisorseTeam old_RRdL_RisorseTeam = rdl.RegistroRdL.RisorseTeam;
                                                rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;  //  aggiorno con la nuova risorsa la risorsa di registro
                                                CambiataRisorsa = true;
                                            }
                                        }
                                        #endregion  fine caso di stato smistamento non cambiato
                                    }
                                }  //  FINE IF RdL.Oid>0 (se non è appena creata è sempre cosi)
                                else
                                {
                                    old_RisorseTeam = null;
                                    old_StatoOperativo = null;
                                }
                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing modifica registro v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                ///  fine rdl.Oid>0 ----------------------------------------------------
                               #region AZIONE CHE SI FANNO SEMPRE
                                if (((RdL)(item)).old_SSmistamento_Oid != ((RdL)(item)).UltimoStatoSmistamento.Oid)
                                {
                                    this.SmistamentoOid_ObjectsCache.Add(rdl.old_SSmistamento_Oid);
                                    this.SmistamentoOid_ObjectsCache.Add(rdl.UltimoStatoSmistamento.Oid);
                                }

                                if (old_StatoOperativo != ((RdL)(item)).UltimoStatoOperativo)
                                {
                                    this.OperativoObjectsCache.Add(old_StatoOperativo);
                                    this.OperativoObjectsCache.Add(rdl.UltimoStatoOperativo);
                                }

                                if (old_RisorseTeam != ((RdL)(item)).RisorseTeam)
                                {
                                    this.RisorsaTeamObjectsCache.Add(old_RisorseTeam);
                                    this.RisorsaTeamObjectsCache.Add(rdl.RisorseTeam);
                                    //aggiunte da 05062019 e 14062019
                                    rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;
                                }
                                //xpo.Dispose();
                                //Debug.Write(rtdb.RisorseTeam.ToString());
                                if (PropertyObjectsCache.Count > 0)
                                {
                                    rdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                                    rdl.DataAggiornamento = DateTime.Now;
                                }
                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing prima salva rdl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                                if (rdl.UltimoStatoSmistamento != null)
                                {
                                    rdl.old_SSmistamento_Oid = rdl.UltimoStatoSmistamento.Oid;

                                    if (rdl.RisorseTeam != null)
                                    { rdl.old_RisorseTeam_Oid = rdl.RisorseTeam.Oid; }
                                }


                                rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;

                                rdl.Save();

                                if (Adesso == null)
                                    Adesso = DateTime.Now;

                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo salva rdl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                #endregion
                            }
                        }
                    }
                    if (Adesso == null)
                        Adesso = DateTime.Now;
                    setLog(SecuritySystem.CurrentUserName, "fine ObjectSpace_Committing dopo salva rdl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
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

            }
            setLog(SecuritySystem.CurrentUserName, "B2 FINE dopo di IF ObjectSpace_Committing:");
        }

        void ObjectSpace_Committed(object sender, EventArgs e)
        {
            setLog(SecuritySystem.CurrentUserName, "C1 INIZIO prima di IF ObjectSpace_Committed:");
            setLog(SecuritySystem.CurrentUserName, "C1.1 suppressOnCommitted:" + suppressOnCommitted.ToString());
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
                    System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                    setLog(SecuritySystem.CurrentUserName, "inizio ObjectSpace_Committed v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                    setLog(SecuritySystem.CurrentUserName, "inizio ObjectSpace_Committed old_OidStatoSmistamento:" + old_OidStatoSmistamento.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                    DateTime ultimadata = DateTime.MinValue;                    //int idmin = 0;    //int oidRegSmistamento = 0;
                    IObjectSpace xpObjectSpaceRdL = (IObjectSpace)sender;      //RdL rdl = xpObjectSpaceRdL.GetObject<RdL>((RdL)View.CurrentObject);       //RegistroRdL rrdl = xpObjectSpaceRdL.GetObject<RegistroRdL>(rdl.RegistroRdL);
                    RdL rdl = (RdL)View.CurrentObject;
                    if (rdl != null)
                    {
                        View_id = View.Id;
                        //RegistroRdL rrdl = rdl.RegistroRdL;
                        Contratti cm = rdl.Immobile.Contratti;

                        #region crea record inserimento avviso notifica spedizione mail
                        //obj = ObjectSpace.CreateObject<TaskWithNotifications>();
                        //obj.Subject = "Now Task With Reminder";
                        //obj.StartDate = DateTime.Now.AddMinutes(5);
                        //obj.DueDate = obj.StartDate.AddHours(2);
                        //obj.RemindIn = TimeSpan.FromMinutes(5);
                        //obj.Save();
                        int[] tempoInt = new int[] { 60, 120, 240 };
                        string tempo = cm.TempoLivelloAutorizzazioneGuasto;
                        if (!string.IsNullOrEmpty(tempo))
                        {
                            var stringSeparators = new string[] { ";" };
                            var ReturnLong = tempo.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

                            int temp1 = 60;
                            for (var i = 0; i < 1; i++)
                            {
                                if (int.TryParse(ReturnLong[i], out temp1))
                                {
                                    tempoInt[i] = temp1;

                                }
                            }
                        }
                        int InizioValoreRicorda = tempoInt[0];
                        XPObjectSpace XPOSpaceRdL = (XPObjectSpace)sender;
                        AvvisiSpedizioni NotificaSpedizione = xpObjectSpaceRdL.CreateObject<AvvisiSpedizioni>();
                        NotificaSpedizione.Description = rdl.Descrizione;
                        NotificaSpedizione.Subject = "Notifica Spedizione Agg. Stato Smist. RdL nr:" + rdl.Oid.ToString() +
                            "; Immobile:" + rdl.Immobile.Descrizione.ToString() +
                            "; Impianto:" + rdl.Servizio.Descrizione.ToString() +
                            "; Stato Smistamento :" + rdl.UltimoStatoSmistamento.SSmistamento.ToString();
                        NotificaSpedizione.Sollecito = FlgAbilitato.Si;
                        NotificaSpedizione.OidSmistamento = rdl.UltimoStatoSmistamento.Oid;
                        if (rdl.RisorseTeam == null)
                            NotificaSpedizione.OidRisorsaTeam = 0;
                        else
                            NotificaSpedizione.OidRisorsaTeam = rdl.RisorseTeam.Oid;
                        //dr[dr.GetOrdinal("AGGIORNAMENTO")] != DBNull.Value ? dr.GetString(dr.GetOrdinal("AGGIORNAMENTO")) : string.Empty;

                        NotificaSpedizione.RemindIn = TimeSpan.FromSeconds(InizioValoreRicorda);
                        NotificaSpedizione.StartDate = DateTime.Now.AddSeconds((int)(InizioValoreRicorda * 2.1));
                        NotificaSpedizione.DueDate = DateTime.Now.AddSeconds(InizioValoreRicorda * 4);
                        NotificaSpedizione.RdLUnivoco = rdl.Oid;
                        NotificaSpedizione.Status = myTaskStatus.Predisposto;
                        NotificaSpedizione.Utente = SecuritySystem.CurrentUserName;
                        NotificaSpedizione.Abilitato = FlgAbilitato.Si;
                        NotificaSpedizione.DataCreazione = DateTime.Now;
                        NotificaSpedizione.DataAggiornamento = DateTime.Now;
                        NotificaSpedizione.Save();
                        XPOSpaceRdL.CommitChanges();

                        #endregion

                        if (rdl.RegistroRdL == null)
                        {
                            #region CREO REGISTRO RDL SE NULLO   ( DA AGGIORNARE POI IL NUMERO OID IN DESCRIZIONE)

                            //Commesse cm = rdl.Immobile.Commesse;
                            try
                            {
                                Cause vCausa = null;
                                Problemi vProb = null;
                                if (rdl.Prob != null)
                                {
                                    vProb = rdl.Prob;
                                    if (rdl.Causa != null)
                                        vCausa = rdl.Causa;
                                }
                            }
                            catch
                            {
                                setLog(SecuritySystem.CurrentUserName, "errore su problema causa rimedio");
                            }
                            rdl.RegistroRdL = new RegistroRdL(((XPObjectSpace)xpObjectSpaceRdL).Session)
                            {
                                Descrizione = rdl.Descrizione,
                                Asset = rdl.Asset,
                                Categoria = rdl.Categoria,
                                Priorita = rdl.Urgenza,
                                Prob = rdl.Prob,
                                //Causa = vCausa,
                                UltimoStatoSmistamento = rdl.UltimoStatoSmistamento,
                                old_SSmistamento_Oid = rdl.UltimoStatoSmistamento.Oid,
                                DATA_CREAZIONE_RDL = rdl.DataCreazione,
                                DataPianificata = rdl.DataPianificata,
                                DataAssegnazioneOdl = rdl.DataAssegnazioneOdl,
                                DataAzioniTampone = rdl.DataAzioniTampone,
                                DataFermo = rdl.DataFermo,
                                DataInizioLavori = rdl.DataInizioLavori,
                                DataPrevistoArrivo = rdl.DataPrevistoArrivo,
                                Utente = SecuritySystem.CurrentUserName,
                                UtenteUltimo = SecuritySystem.CurrentUserName,
                                MostraDataOraFermo = cm.MostraDataOraFermo ? true : false,
                                MostraDataOraRiavvio = cm.MostraDataOraFermo ? true : false,
                                MostraDataOraSopralluogo = cm.MostraDataOraSopralluogo ? true : false,
                                MostraDataOraAzioniTampone = cm.MostraDataOraAzioniTampone ? true : false,
                                MostraDataOraInizioLavori = cm.MostraDataOraInizioLavori ? true : false,
                                MostraDataOraCompletamento = cm.MostraDataOraCompletamento ? true : false,
                                MostraElencoCauseRimedi = cm.MostraElencoCauseRimedi ? true : false,

                            };
                            rdl.Save();
                            setLog(SecuritySystem.CurrentUserName, "creazione registro in ObjectSpace_Committed");

                            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os)); v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                            #endregion
                        }

                        if (rdl.RegistroRdL != null)
                        {
                            #region modifiche delle proprietà del regedl
                            if (rdl.RegistroRdL.UtenteUltimo != rdl.UtenteUltimo)
                                rdl.RegistroRdL.UtenteUltimo = rdl.UtenteUltimo;

                            if (rdl.RegistroRdL.DataAggiornamento != rdl.DataAggiornamento)
                                rdl.RegistroRdL.DataAggiornamento = rdl.DataAggiornamento;

                            if (rdl.RegistroRdL.DataCompletamento != rdl.DataCompletamento)
                                rdl.RegistroRdL.DataCompletamento = rdl.DataCompletamento;

                            if (rdl.RegistroRdL.DataAssegnazioneOdl != rdl.DataAssegnazioneOdl)
                                rdl.RegistroRdL.DataAssegnazioneOdl = rdl.DataAssegnazioneOdl;

                            if (rdl.RegistroRdL.DataAzioniTampone != rdl.DataAzioniTampone)
                                rdl.RegistroRdL.DataAzioniTampone = rdl.DataAzioniTampone;

                            if (rdl.RegistroRdL.DataSopralluogo != rdl.DataSopralluogo)
                                rdl.RegistroRdL.DataSopralluogo = rdl.DataSopralluogo;

                            if (rdl.RegistroRdL.DataInizioLavori != rdl.DataInizioLavori)
                                rdl.RegistroRdL.DataInizioLavori = rdl.DataInizioLavori;
                            #endregion
                        }


                        if (rdl.OdL == null)
                        {
                            #region CREA ORDINE DI LAVORO SE NULLO SU RDL   

                            string odlDescrizione = rdl.Descrizione;
                            if (odlDescrizione.Length > 3996)
                                odlDescrizione = odlDescrizione.Substring(1, 3996) + "...";

                            rdl.OdL = new OdL(((XPObjectSpace)xpObjectSpaceRdL).Session)
                            {
                                Descrizione = odlDescrizione,
                                RegistroRdL = rdl.RegistroRdL,
                                TipoOdL = xpObjectSpaceRdL.GetObjectByKey<TipoOdL>(1),
                                DataEmissione = rdl.DataAssegnazioneOdl
                            };
                            rdl.Save();
                            setLog(SecuritySystem.CurrentUserName, "creazione odl in ObjectSpace_Committed"); // ERRORE creazione odl 
                            #endregion
                        }
                        if (rdl.OdL != null)
                        {
                            if (rdl.OdL.DataCompletamento != rdl.DataCompletamento)
                                rdl.OdL.DataCompletamento = rdl.DataCompletamento;

                            if (rdl.OdL.DataEmissione != rdl.DataAssegnazioneOdl)
                                rdl.OdL.DataEmissione = rdl.DataAssegnazioneOdl;

                            rdl.Save();
                        }
                        List<string> MessaggiCreazioneList = new List<string>();
                        List<string> MessaggiAssegnazioneList = new List<string>();

                        #region tutta la proedura post commit
                        if (SmistamentoOid_ObjectsCache.Count == 2)
                        {
                            if (this.SmistamentoOid_ObjectsCache.Contains(0) && this.SmistamentoOid_ObjectsCache.Contains(1))  // aggiorna descrizione Registro RdL quando è appena creato
                            {
                                string Descrizione = string.Empty;
                                if (rdl.Categoria.Oid == 1 || rdl.Categoria.Oid == 5)
                                {
                                    Descrizione = string.Format("Reg.MP({0}) {1}", rdl.RegistroRdL.Oid, rdl.Descrizione);
                                    if (Descrizione.Length > 3999)
                                        Descrizione = Descrizione.Substring(1, 3996) + "...";
                                }
                                else
                                {
                                    Descrizione = string.Format("Reg.TT({0}) {1}", rdl.RegistroRdL.Oid, rdl.Descrizione);
                                    if (Descrizione.Length > 3999)
                                        Descrizione = Descrizione.Substring(1, 3996) + "...";
                                }
                                rdl.RegistroRdL.Descrizione = Descrizione;
                                rdl.RegistroRdL.Save();
                                rdl.Save();
                            }

                            if (this.SmistamentoOid_ObjectsCache.Contains(10))
                            {    //   se trasformo una emergenza smartphone (molti team) su assegnata o sala operativa questo 
                                //       e dopo che è stato committato il CAMBIAMNETO ALLA RDL
                                if (rdl.UltimoStatoSmistamento.Oid != 10)  //in emergenza da assegnare ( dopo il commit quindi prima era 10-in emergenza (diveso da 10))
                                {
                                    #region SE è STATO TOLTO LO STATO SMISTAMENTO IN EMERGENZA PER SMARTPHONE
                                    RegNotificheEmergenze notEmerRdL = xpObjectSpaceRdL.FindObject<RegNotificheEmergenze>(CriteriaOperator.Parse("RdL.Oid = ?", rdl.Oid));
                                    if (notEmerRdL != null)
                                    {
                                        notEmerRdL.RegStatoNotifica = RegStatiNotificaEmergenza.Annullato;
                                        notEmerRdL.Team = null;
                                        notEmerRdL.DataAggiornamento = DateTime.Now;
                                        notEmerRdL.DataChiusura = DateTime.Now;
                                        notEmerRdL.Save();
                                    }
                                    #endregion
                                }
                            }
                        }


                        System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA DI AGG RISORSA v:" + View_id + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                        //--------------------
                        if (SmistamentoOid_ObjectsCache != null)
                            SmistamentoOid_ObjectsCache.Clear();
                        if (RisorsaTeamObjectsCache != null)
                            RisorsaTeamObjectsCache.Clear();// annullo le variazione di risorsa perche le registro gia qui  
                        if (OperativoObjectsCache != null)
                            OperativoObjectsCache.Clear();
                        #region invia messaggio
                        // cambio qualunque proprieta
                        string Messaggio = string.Empty;
                        //nuovo codice 
                        if ((View.Id.Contains("RdL_DetailView_Gestione") || View.Id.Contains("RdL_DetailView_NuovoTT") || View.Id.Contains("RdL_DetailView")) && View.ObjectTypeInfo.Type == typeof(RdL))
                        {
                            string Titolo = string.Empty;
                            string AlertMessaggio = string.Empty;
                            try
                            {
                                //System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA SPEDISCI MAIL SMS v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                //using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                                //{
                                //    im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, rdl.RegistroRdL.Oid, ref Messaggio);
                                //}
                                using (DB db = new DB())
                                {
                                    Messaggio = db.GetMessaggioDestImpostatiDB(SecuritySystem.CurrentUserName, NotificaSpedizione.Oid);
                                }

                                if (!string.IsNullOrEmpty(Messaggio))
                                {
                                    Titolo = "Trasmissione Invio Predisposta!!";
                                    AlertMessaggio = string.Format("{0}", Messaggio);
                                    //SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                                }
                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed DOPO SPEDISCI MAIL SMS v:" + View_id + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                            }
                            catch (Exception ex)
                            {
                                Titolo = "Trasmissione Invio Predisposta non Eseguita!!";
                                AlertMessaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                                setLog(SecuritySystem.CurrentUserName, "errore su messaggio di predisposizione rdl");
                            }
                            MessaggiCreazioneList.Add(Titolo);
                            MessaggiCreazioneList.Add(AlertMessaggio);
                        }
                        #endregion
                        setLog(SecuritySystem.CurrentUserName, " in mezzo (prima delle regole ass) a ObjectSpace_Committed: " + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                        PropertyObjectsCache.Clear();
                        //------------------------------------------------------------------
                        //RegoleAutoAssegnazioneRdL
                        System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA RegoleAutoAssegnazioneRdL v:" + View_id + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                        bool isGranted = SecuritySystem.IsGranted(new
                             DevExpress.ExpressApp.Security.PermissionRequest(xpObjectSpaceRdL, typeof(RegoleAutoAssegnazioneRdL),
                           DevExpress.ExpressApp.Security.SecurityOperations.Read));

                        #region REGOLA DI AUTOMAZIONE DI ASSEGNAZIONE ---- [solo su stato smistamento == 1 ]-----@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                        if (IsNuovaRdL && isGranted && rdl.Immobile.Contratti.AttivaAutoAssegnazioneTeam && rdl.UltimoStatoSmistamento.Oid == 1 && rdl.Servizio != null)//  solo se è appena creata
                        {

                            int OidEdificio = 0; int OidImpianto = 0; int OidCategoria = 0;

                            if (rdl.Immobile != null)
                                OidEdificio = rdl.Immobile.Oid;
                            if (rdl.Servizio != null)
                                OidImpianto = rdl.Servizio.Oid;
                            if (rdl.Categoria != null)
                                OidCategoria = rdl.Categoria.Oid;


                            //  ASSEGNA AUTOMATICAMENTE
                            //IObjectSpace xpObjectSpace = Application.CreateObjectSpace();//  -------------------------   
                            //int OidEdificio = rdl.Immobile.Oid;
                            //int OidImpianto = rdl.Immobile.Impianti.Oid;
                            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                            XPQuery<RegoleAutoAssegnazioneRdL> RegoleQuery = new XPQuery<RegoleAutoAssegnazioneRdL>(Sess);
                            var query = RegoleQuery.Where(w => w.Immobile.Oid == OidEdificio && w.Servizio.Oid == OidImpianto)
                                                   .Where(w => w.Categoria.Oid == OidCategoria)
                                                   .Where(w => w.CalendarioCadenze != null)
                                                   .Where(w => w.StatoTLCImpianto == rdl.Servizio.StatoTLCServizio)  //  aggiunto a seguito dello stato tlc impianto @@@@@@@@@@@@@@
                                                   .Where(w => w.TipoRegola == TipoRegola.RegolaAutomatismiAssegnazione) // aggiunto per regola di automazione
                                                   .Where(w => w.TipoAssegnazione == TipoAssegnazione.SO || w.TipoAssegnazione == TipoAssegnazione.SM || w.TipoAssegnazione == TipoAssegnazione.SME)
                                                   .Select(s => new { OidRegoleAutoAssegnazioneRdL = s.Oid, s.TipoAssegnazione, s.CalendarioCadenze, s.RisorseTeam, s.FesteNazionali, s.RegoleAutoAssegnazioneRisorseTeams, s.AggiungiRisorsaVicina }).ToList();


                            DateTime data = DateTime.Now;
                            bool mesetrovato = false;
                            bool giornotrovato = false;
                            bool OrarioCompreso = false;
                            bool createAotoregolaassegnazione = false;
                            bool SpedisciMessaggioAssegnazione = false;
                            TipoAssegnazione vTipoAssegnazione = Classi.TipoAssegnazione.ND;
                            AggiungiRisorsaVicina vTipoStrategiaAssegnazione = Classi.AggiungiRisorsaVicina.ND;
                            RisorseTeam vRisorseTeam = null;
                            bool vFesteNazionali = false;
                            int OidRegoleAutoAssegnazione = 0;
                            List<RisorseTeam> listRisorseTeams = new List<RisorseTeam>();
                            string MessaggioRegola = string.Empty;
                            if (query.Count > 0)
                            {
                                foreach (var dr in query)
                                {
                                    listRisorseTeams = dr.RegoleAutoAssegnazioneRisorseTeams.Select(s => s.RisorseTeam).ToList();
                                    OidRegoleAutoAssegnazione = dr.OidRegoleAutoAssegnazioneRdL;
                                    listRisorseTeams.Add(dr.RisorseTeam); // aggiunge quella di default
                                    vRisorseTeam = dr.RisorseTeam;      // assegna  quella di default per caso di so e sm
                                    mesetrovato = false;
                                    giornotrovato = false;
                                    OrarioCompreso = false;
                                    createAotoregolaassegnazione = false;
                                    vTipoAssegnazione = dr.TipoAssegnazione;
                                    vTipoStrategiaAssegnazione = dr.AggiungiRisorsaVicina;
                                    //vAggiungiRisorsaVicina = dr.AggiungiRisorsaVicina;                    
                                    vFesteNazionali = dr.FesteNazionali;

                                    foreach (var cc in dr.CalendarioCadenze.CalendarioCadenzeDettaglioS)
                                    {
                                        if (vFesteNazionali) /// anche se è festa nazionale
                                        {
                                            createAotoregolaassegnazione = FestivitaNazionale();
                                            if (createAotoregolaassegnazione)
                                                break;
                                        }
                                        foreach (var mm in cc.GruppoMesi.Mesi)
                                        {
                                            if (mm.Mesi.Mese == data.Month)
                                            {
                                                mesetrovato = true;
                                                if (mesetrovato)
                                                    break;
                                            }
                                        }
                                        if (mesetrovato)
                                        {
                                            int nowDayOfWeek = (int)DateTime.Now.DayOfWeek; //5	= ven
                                            int tipoGGSett = (int)cc.TipoGiornoSettimana;
                                            if (nowDayOfWeek == tipoGGSett) //data
                                            {
                                                giornotrovato = true;
                                                OrarioCompreso = VerificaOrario(cc.TimeDalle, cc.TimeAlle);
                                                if (OrarioCompreso)
                                                {
                                                    createAotoregolaassegnazione = true;
                                                    if (createAotoregolaassegnazione)
                                                        break;
                                                }
                                            }
                                        }
                                    }
                                    if (createAotoregolaassegnazione)//allora assegno al tecnico
                                        break;
                                }
                            }
                            if (createAotoregolaassegnazione)//allora assegno al tecnico    xpObjectSpaceRdL
                            {
                                string messaggioassegnazione = "";
                                string listaRisorse = string.Empty;
                                //RdL myRdL = xpObjectSpace.GetObject<RdL>(rdl);
                                RdL myRdL = rdl;
                                ///  carico il vettore risorese in funzione della strategia
                                ///           //Strategia di assegnazione:
                                //    a una risorsa
                                //    alla Risorsa e alle Risorse in Elenco
                                //    a una delle Risorse in elenco: la piu vicina altrimenti alla piu scarica
                                //    a una delle Risorse in elenco: la piu scarica altrimenti alla piu vicina
                                List<RisorseTeam> ListaRTeam_ok = new List<RisorseTeam>();
                                List<RisorseTeam> ListaRTeam_toAdd = new List<RisorseTeam>();
                                bool trovata = false;
                                string criteriodiAssegnazioneUtilizzato = string.Empty;
                                int nr = 1;
                                switch (vTipoStrategiaAssegnazione)
                                {
                                    case Classi.AggiungiRisorsaVicina.unaRisorsa:
                                    case Classi.AggiungiRisorsaVicina.RisorsaeElencoRisorse:
                                        ListaRTeam_ok = listRisorseTeams; // quelle in elenco (al quale ho aggiunto quella singola)
                                        break;

                                    case Classi.AggiungiRisorsaVicina.UnaRisorsa_piuVicino_o_piuScarica:
                                    case Classi.AggiungiRisorsaVicina.dueRisorse_DuePiuVicine_o_DuePiuScariche:
                                    case Classi.AggiungiRisorsaVicina.treRisorse_TrePiuVicine_o_TrePiuScariche:
                                        if (vTipoStrategiaAssegnazione == Classi.AggiungiRisorsaVicina.dueRisorse_DuePiuVicine_o_DuePiuScariche) nr = 2;
                                        if (vTipoStrategiaAssegnazione == Classi.AggiungiRisorsaVicina.treRisorse_TrePiuVicine_o_TrePiuScariche) nr = 3;
                                        double Pa_lat1 = rdl.Immobile.Indirizzo.Latitude;
                                        double Pa_lon1 = rdl.Immobile.Indirizzo.Longitude;
                                        ListaRTeam_toAdd = GetRTeamVicina(listRisorseTeams, rdl.Immobile.Indirizzo.Latitude,
                                                                                            rdl.Immobile.Indirizzo.Latitude, nr);
                                        if (ListaRTeam_toAdd != null && ListaRTeam_toAdd.Count() > 0)
                                        {
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "piu vicina";
                                        }
                                        else
                                        {
                                            ListaRTeam_toAdd = GetRTeamScarica(listRisorseTeams, nr);// è sempre piena!!!!! di uno  1)
                                            if (ListaRTeam_toAdd != null && ListaRTeam_toAdd.Count() > 0)
                                            {
                                                trovata = true;
                                                criteriodiAssegnazioneUtilizzato = "piu scarica";
                                            }
                                            else
                                            {
                                                Random randm = new Random();
                                                int rand_id = randm.Next(1, listRisorseTeams.Count());
                                                ListaRTeam_toAdd = listRisorseTeams.Skip(rand_id - 1).Take(1).ToList();// è sempre piena!!!!! di uno  1)
                                                trovata = true;
                                                criteriodiAssegnazioneUtilizzato = "casuale";
                                            }

                                        }
                                        ListaRTeam_ok = ListaRTeam_toAdd; // per sme
                                        vRisorseTeam = ListaRTeam_toAdd.First(); // per so e sm
                                        break;

                                    case Classi.AggiungiRisorsaVicina.UnaRisorsa_piuScarica:
                                        ListaRTeam_toAdd = GetRTeamScarica(listRisorseTeams, 1);// è sempre piena!!!!! di uno  1) =
                                        if (ListaRTeam_toAdd != null && ListaRTeam_toAdd.Count() > 0)
                                        {
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "piu scarica";
                                        }
                                        else
                                        {
                                            Random randm = new Random();
                                            int rand_id = randm.Next(1, listRisorseTeams.Count());
                                            ListaRTeam_toAdd = listRisorseTeams.Skip(rand_id - 1).Take(1).ToList();// è sempre piena!!!!! di uno  1)
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "casuale";
                                        }
                                        ListaRTeam_ok = ListaRTeam_toAdd;
                                        vRisorseTeam = ListaRTeam_toAdd.First(); // per so e sm
                                        break;
                                    case Classi.AggiungiRisorsaVicina.dueRisorse_UnaPiuVicino_e_UnaPiuScarica:
                                        ListaRTeam_toAdd = GetRTeamVicina(listRisorseTeams, rdl.Immobile.Indirizzo.Latitude,
                                                                                            rdl.Immobile.Indirizzo.Latitude, nr);
                                        nr = 2;
                                        if (ListaRTeam_toAdd != null && ListaRTeam_toAdd.Count() > 0)
                                        {
                                            ListaRTeam_ok = ListaRTeam_toAdd; // per sme
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "piu vicina";
                                            nr = 1;// ha trovato la vicina quindi prendo una sola scarica
                                        }

                                        ListaRTeam_toAdd = GetRTeamScarica(listRisorseTeams, nr).Where(w => !ListaRTeam_ok.Contains(w)).ToList();// è sempre piena!!!!! di uno  1)
                                        if (ListaRTeam_toAdd != null && ListaRTeam_toAdd.Count() > 0)
                                        {
                                            ListaRTeam_ok.AddRange(ListaRTeam_toAdd);
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "piu scarica";
                                        }
                                        else
                                        {
                                            Random randm = new Random();
                                            List<RisorseTeam> LrtRandom = listRisorseTeams.Where(w => !ListaRTeam_ok.Contains(w)).ToList();
                                            int rand_id = randm.Next(1, LrtRandom.Count());
                                            ListaRTeam_ok.AddRange(LrtRandom.Skip(rand_id - 1).Take(1).ToList());// è sempre piena!!!!! di uno  1)
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "casuale";
                                        }
                                        if (ListaRTeam_ok.Count > 0)
                                            vRisorseTeam = ListaRTeam_ok.First(); // per so e sm
                                        break;
                                    case Classi.AggiungiRisorsaVicina.unaRisorsa_scarica_piuvicina_ottimizzata:
                                        int oidRisorsaTeam = 0;
                                        //if (rdl.Immobile != null) // 11 secco come SmistamentoOid_ObjectsCache 
                                        //{
                                        //PK_DC_SALAOPERATIVA
                                        //procedure GET_TRISORSA_OTTIMIZZATA(   //iregolaautossegnazione  in number,  //iOidEdificio            in number,   //iIsSmartphone           in number,
                                        //iOidCentroOperativoBase in number,  //iusername               in varchar2, //oMessaggio              out varchar2,     //orisorsateam            out number)
                                        using (DB db = new DB())
                                        {
                                            int OidCObase = rdl.Immobile.CentroOperativoBase == null ? 0 : rdl.Immobile.CentroOperativoBase.Oid;
                                            oidRisorsaTeam = db.GetTeamRisorseOttimizata(rdl.Immobile.Oid, 11, OidCObase, CAMS.Module.Classi.SetVarSessione.CorrenteUser, OidRegoleAutoAssegnazione, ref MessaggioRegola);
                                        }

                                        if (oidRisorsaTeam > 0)
                                        {
                                            //RisorseTeam LrtRandom = xpObjectSpace.GetObjectByKey<RisorseTeam>(oidRisorsaTeam);
                                            RisorseTeam rt = xpObjectSpaceRdL.FindObject<RisorseTeam>(new BinaryOperator("Oid", oidRisorsaTeam));
                                            vRisorseTeam = rt;
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "piu scarica";
                                        }
                                        else
                                        {
                                            Random randm = new Random();
                                            List<RisorseTeam> LrtRandom = listRisorseTeams.Where(w => !ListaRTeam_ok.Contains(w)).ToList();
                                            int rand_id = randm.Next(1, LrtRandom.Count());
                                            ListaRTeam_ok.AddRange(LrtRandom.Skip(rand_id - 1).Take(1).ToList());// è sempre piena!!!!! di uno  1)
                                            vRisorseTeam = LrtRandom.Skip(rand_id - 1).Take(1).First<RisorseTeam>();
                                            trovata = true;
                                            criteriodiAssegnazioneUtilizzato = "casuale";
                                        }
                                        //if (ListaRTeam_ok.Count > 0)
                                        //    vRisorseTeam = ListaRTeam_ok.First(); // per so e sm
                                        break;
                                    default:   ////   quandro non definito      unaRisorsa_scarica_piuvicina_ottimizzata
                                        ListaRTeam_ok = listRisorseTeams;

                                        break;

                                }

                                ////----------------------- tipo assegnazione  so , smartphone, emergenza------------------------------
                                #region GESTIONE TIPO ASSEGNAZIONE
                                switch (vTipoAssegnazione)
                                {
                                    case Classi.TipoAssegnazione.SO:

                                        if (rdl.Immobile != null)
                                        {
                                            Console.WriteLine("Current value is {0}", 1);
                                            myRdL.UltimoStatoSmistamento = xpObjectSpaceRdL.GetObjectByKey<StatoSmistamento>(11); // ASSEGNATA
                                            myRdL.UltimoStatoOperativo = xpObjectSpaceRdL.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                            myRdL.RisorseTeam = xpObjectSpaceRdL.GetObject<RisorseTeam>(vRisorseTeam);
                                            myRdL.DataAssegnazioneOdl = DateTime.Now;
                                            myRdL.DataAggiornamento = DateTime.Now;
                                            myRdL.RegistroRdL.RisorseTeam = myRdL.RisorseTeam;
                                            myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                            myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                            myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;
                                            myRdL.RegistroRdL.DataAssegnazioneOdl = DateTime.Now;
                                            myRdL.RegistroRdL.DataSopralluogo = myRdL.DataSopralluogo;
                                            SpedisciMessaggioAssegnazione = true;
                                            messaggioassegnazione = string.Format("Assegnazione Automatica Intervento: RegRdL {0}, in stato Gestione Sala Operativa, alla RisorsaTeam {1}",
                                                myRdL.RegistroRdL.Oid, myRdL.RisorseTeam.FullName);


                                        }
                                        break;



                                    //codice precedente al  071218

                                    //case Classi.TipoAssegnazione.SO:                                   
                                    //switch (vTipoStrategiaAssegnazione)
                                    //    {
                                    //        case Classi.AggiungiRisorsaVicina.unaRisorsa:
                                    //            if (rdl.Immobile != null)
                                    //            {
                                    //                Console.WriteLine("Current value is {0}", 1);
                                    //                myRdL.UltimoStatoSmistamento = xpObjectSpaceRdL.GetObjectByKey<StatoSmistamento>(11); // ASSEGNATA
                                    //                myRdL.UltimoStatoOperativo = xpObjectSpaceRdL.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                    //                myRdL.RisorseTeam = xpObjectSpaceRdL.GetObject<RisorseTeam>(vRisorseTeam);
                                    //                myRdL.DataAssegnazioneOdl = DateTime.Now;
                                    //                myRdL.DataAggiornamento = DateTime.Now;
                                    //                myRdL.RegistroRdL.RisorseTeam = myRdL.RisorseTeam;
                                    //                myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                    //                myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                    //                myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;
                                    //                myRdL.RegistroRdL.DataAssegnazioneOdl = DateTime.Now;
                                    //                SpedisciMessaggioAssegnazione = true;
                                    //                messaggioassegnazione = string.Format("Assegnazione Automatica Intervento: RegRdL {0}, in stato Gestione Sala Operativa, alla RisorsaTeam {1}",
                                    //                    myRdL.RegistroRdL.Oid, myRdL.RisorseTeam.FullName);
                                    //            }

                                    //            break;
                                    //        default:// altro non pervenuto
                                    //            messaggioassegnazione = string.Format("Assegnazione Automatica Intervento: RegRdL {0}, non configurata correttemente", myRdL.RegistroRdL.Oid);
                                    //            break;
                                    //    }
                                    //    break;
                                    // fine codice precedente al  071218
                                    case Classi.TipoAssegnazione.SM:
                                        Console.WriteLine("Current value is {0}", 2);
                                        if (rdl.Immobile != null)
                                        {
                                            myRdL.UltimoStatoSmistamento = xpObjectSpaceRdL.GetObjectByKey<StatoSmistamento>(2); // ASSEGNATA
                                            myRdL.UltimoStatoOperativo = xpObjectSpaceRdL.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                            myRdL.RisorseTeam = xpObjectSpaceRdL.GetObject<RisorseTeam>(vRisorseTeam);
                                            myRdL.DataAssegnazioneOdl = DateTime.Now;
                                            myRdL.DataAggiornamento = DateTime.Now;
                                            myRdL.RegistroRdL.RisorseTeam = myRdL.RisorseTeam;
                                            myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                            myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                            myRdL.RegistroRdL.DataAssegnazioneOdl = DateTime.Now;
                                            myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;
                                            myRdL.RegistroRdL.DataSopralluogo = myRdL.DataSopralluogo;
                                            SpedisciMessaggioAssegnazione = true;
                                            messaggioassegnazione =
                                                string.Format("Assegnazione Automatica Intervento: RegRdL {0}, in stato Assegnata a Smartphone della RisorsaTeam {1} {2}",
                                                 myRdL.RegistroRdL.Oid,
                                                 myRdL.RisorseTeam.FullName,
                                                 criteriodiAssegnazioneUtilizzato);

                                        }

                                        break;
                                    case Classi.TipoAssegnazione.SME:

                                        Console.WriteLine("SME +  unaRisorsa e  RisorsaeElencoRisorse {0}", 3);
                                        // carico tutti i team di assegnazione in  emergenza
                                        myRdL.UltimoStatoSmistamento = xpObjectSpaceRdL.GetObjectByKey<StatoSmistamento>(10); // ASSEGNATA
                                        myRdL.UltimoStatoOperativo = xpObjectSpaceRdL.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                                                                                                         //rdl.RisorseTeam = RisorseTeam;
                                        myRdL.DataAggiornamento = DateTime.Now;
                                        //rdl.RegistroRdL.RisorseTeam = RisorseTeam;
                                        myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                        myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                        myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;
                                        myRdL.RegistroRdL.DataSopralluogo = myRdL.DataSopralluogo;

                                        RegNotificheEmergenze RegNotificaEmergenza = xpObjectSpaceRdL.CreateObject<RegNotificheEmergenze>();
                                        RegNotificaEmergenza.RdL = myRdL;
                                        RegNotificaEmergenza.RegistroRdL = myRdL.RegistroRdL;
                                        RegNotificaEmergenza.RegStatoNotifica = RegStatiNotificaEmergenza.daAssegnare;
                                        RegNotificaEmergenza.DataCreazione = DateTime.Now;
                                        RegNotificaEmergenza.DataAggiornamento = DateTime.Now;
                                        // inserisco le risorse team aggiuntive
                                        //listaRisorse += vRisorseTeam.FullName;
                                        foreach (RisorseTeam item in ListaRTeam_ok)// le comprende tutte
                                        {
                                            RisorseTeam altreRisorseTeam = item;
                                            RegNotificaEmergenza.NotificheEmergenzes.Add(new NotificheEmergenze(RegNotificaEmergenza.Session)
                                            {
                                                DataCreazione = DateTime.Now,
                                                Team = xpObjectSpaceRdL.GetObjectByKey<RisorseTeam>(altreRisorseTeam.Oid),
                                                CodiceNotifica = Guid.NewGuid(),
                                                StatoNotifica = StatiNotificaEmergenza.NonVisualizzato,
                                                DataAggiornamento = DateTime.Now
                                            });
                                            listaRisorse += ", " + altreRisorseTeam.FullName;
                                        }

                                        RegNotificaEmergenza.Save();
                                        SpedisciMessaggioAssegnazione = true;
                                        messaggioassegnazione = string.Format("Assegnazione Automatica Intervento: RegRdL {0}, in stato Pronto Intervento (emergenza) a Smartphone alle RisorsaTeam {1} {2}",
                                        myRdL.RegistroRdL.Oid,
                                       listaRisorse,
                                       criteriodiAssegnazioneUtilizzato);

                                        break;
                                    default:
                                        Console.WriteLine("Sorry, invalid selection.");
                                        break;
                                }
                                #endregion   FINE GESTIONE TIPO ASSEGNAZIONE
                                myRdL.Save();
                                xpObjectSpaceRdL.CommitChanges();

                                //rdl.Reload();
                                System.Diagnostics.Debug.WriteLine("fine ObjectSpace_Committed DOPO RegoleAutoAssegnazioneRdL v:" + View_id + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());


                                #region invia messaggio
                                if (SpedisciMessaggioAssegnazione)
                                {

                                    AvvisiSpedizioni NotificaSpedizioneAss = xpObjectSpaceRdL.CreateObject<AvvisiSpedizioni>();
                                    NotificaSpedizioneAss.Description = rdl.Descrizione;
                                    NotificaSpedizioneAss.Subject = "Notifica Spedizione  Agg. Stato Smist.Assg. Aut. Team. RdL nr:" + rdl.Oid.ToString() +
                                    "; Immobile:" + rdl.Immobile.Descrizione.ToString() +
                                    "; Impianto:" + rdl.Servizio.Descrizione.ToString() +
                                    "; Stato Smistamento :" + rdl.UltimoStatoSmistamento.SSmistamento.ToString();
                                    NotificaSpedizioneAss.Sollecito = FlgAbilitato.Si;
                                    NotificaSpedizioneAss.OidSmistamento = rdl.UltimoStatoSmistamento.Oid;
                                    if (rdl.UltimoStatoSmistamento.Oid == 10)
                                        NotificaSpedizioneAss.OidRisorsaTeam = 0;
                                    else
                                        NotificaSpedizioneAss.OidRisorsaTeam = rdl.RisorseTeam.Oid;
                                    //dr[dr.GetOrdinal("AGGIORNAMENTO")] != DBNull.Value ? dr.GetString(dr.GetOrdinal("AGGIORNAMENTO")) : string.Empty;

                                    NotificaSpedizioneAss.RemindIn = TimeSpan.FromSeconds(InizioValoreRicorda);
                                    NotificaSpedizioneAss.StartDate = DateTime.Now.AddSeconds((int)(InizioValoreRicorda * 2.1));
                                    NotificaSpedizioneAss.DueDate = DateTime.Now.AddSeconds(InizioValoreRicorda * 4);
                                    NotificaSpedizioneAss.RdLUnivoco = rdl.Oid;
                                    NotificaSpedizioneAss.Status = myTaskStatus.Predisposto;
                                    NotificaSpedizioneAss.Utente = SecuritySystem.CurrentUserName;
                                    NotificaSpedizioneAss.Abilitato = FlgAbilitato.Si;
                                    NotificaSpedizioneAss.DataCreazione = DateTime.Now;
                                    NotificaSpedizioneAss.DataAggiornamento = DateTime.Now;
                                    NotificaSpedizioneAss.Save();
                                    XPOSpaceRdL.CommitChanges();


                                    //if ((View.Id.Contains("RdL_DetailView") || View.Id.Contains("RdL_DetailView_NuovoTT")) && View.ObjectTypeInfo.Type == typeof(RdL))
                                    //{
                                    // System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed SECONDA SPEDIZIONE ASSEGNAZIONE v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                    System.Diagnostics.Debug.WriteLine("INIZIO SECONDA SPEDIZIONE DOPO AUTOASSEGNAZIONE REGRDL " + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                    string Titolo = string.Empty;
                                    string AlertMessaggio = string.Empty;
                                    try
                                    {
                                        //RdL RdL = rdl;
                                        using (DB db = new DB())
                                        {
                                            Messaggio = db.GetMessaggioDestImpostatiDB(SecuritySystem.CurrentUserName, NotificaSpedizioneAss.Oid);
                                        }
                                        if (!string.IsNullOrEmpty(Messaggio))
                                        {
                                            Titolo = "Trasmissione Invio Predisposta!!";
                                            AlertMessaggio = string.Format("{0}", Messaggio);
                                            //SetMessaggioWebAutoAssegnazione(AlertMessaggio, Titolo, InformationType.Success);

                                        }

                                    }
                                    catch (Exception ex)
                                    {
                                        Titolo = "Trasmissione Invio Predisposta non Eseguita!!";
                                        AlertMessaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                                        //SetMessaggioWebAutoAssegnazione(AlertMessaggio, Titolo, InformationType.Warning);
                                        //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                                    }
                                    MessaggiAssegnazioneList.Add(Titolo);
                                    MessaggiAssegnazioneList.Add(AlertMessaggio);
                                    //System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed DOPO SPEDIZIONE ASSEGNAZIONE v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                                    System.Diagnostics.Debug.WriteLine("FINE SECONDA SPEDIZIONE DOPO AUTOASSEGNAZIONE REGRDL " + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                                    //}


                                }

                                #endregion   REGOLA DI AUTOASSEGNAZIONE
                                //------------------------------------------------------------------

                            }
                        }
                        #endregion

                        setLog(SecuritySystem.CurrentUserName, " in mezzo (dopo le regole ass) a ObjectSpace_Committed: " + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                        //--------------------------------------------------------------------- 
                        #endregion

                        #region invia messaggio utente e popup
                        if (MessaggiCreazioneList.Count > 0)
                        {
                            if (MessaggiAssegnazioneList.Count > 0)
                            {
                                InformationType info = InformationType.Success;
                                if (MessaggiCreazioneList.Contains("Error") || MessaggiAssegnazioneList.Contains("Error"))
                                    info = InformationType.Warning;

                                string msg = string.Format("Aggiornamento Eseguito! - {0} {1} - \r\n {2} {3}", MessaggiCreazioneList[0], MessaggiCreazioneList[1], MessaggiAssegnazioneList[0], MessaggiAssegnazioneList[1]);
                                SetMessaggioWebAutoAssegnazione(msg, "Messaggio per Utente", InformationType.Info);
                            }
                            else
                            {
                                InformationType info = InformationType.Success;
                                if (MessaggiCreazioneList.Contains("Error"))
                                    info = InformationType.Warning;

                                string msg = string.Format("Aggiornamento Eseguito! - {0} {1}", MessaggiCreazioneList[0], MessaggiCreazioneList[1]);
                                SetMessaggioWebAutoAssegnazione(msg, "Messaggio per Utente", InformationType.Info);
                            }
                        }

                        #endregion

                        if (rdl.UltimoStatoSmistamento.Oid != rdl.old_SSmistamento_Oid || rdl.RegistroRdL.UltimoStatoSmistamento.Oid != old_OidStatoSmistamento)
                        {
                            rdl.old_SSmistamento_Oid = rdl.UltimoStatoSmistamento.Oid;
                            rdl.RegistroRdL.UltimoStatoSmistamento.Oid = rdl.UltimoStatoSmistamento.Oid;
                            if (rdl.UltimoStatoOperativo != null)
                                rdl.RegistroRdL.UltimoStatoOperativo.Oid = rdl.UltimoStatoOperativo.Oid;
                            rdl.Save();
                            xpObjectSpaceRdL.CommitChanges();
                        }

                        xpObjectSpaceRdL.CommitChanges();

                    }

                    setLog(SecuritySystem.CurrentUserName, "fine procedura ObjectSpace_Committed  v:" + View_id + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
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
            setLog(SecuritySystem.CurrentUserName, "C2 FINE dopo di IF ObjectSpace_Committed:");
        }


        void ObjectSpace_ObjectChanged(object sender, ObjectChangedEventArgs e)
        {
            setLog(SecuritySystem.CurrentUserName, "D1 INIZIO prima di IF ObjectSpace_ObjectChanged:");
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
                    switch (TipoOggetto)
                    {
                        case "Richiedente":
                            // non fai nulla
                            break;
                        case "RdL":
                            //var item1 = item;
                            suppressOnCommitted = false;
                            suppressOnCommitting = false;

                            System.Diagnostics.Debug.WriteLine("procedura ObjectSpace_ObjectChanged  foreach (var  v:" + e.PropertyName + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                            System.Diagnostics.Debug.WriteLine(View.Id.ToString());
                            if (!PropertyObjectsCache.Contains(e.PropertyName))
                            {
                                PropertyObjectsCache.Add(e.PropertyName);
                            }
                            System.Diagnostics.Debug.WriteLine("procedura ObjectSpace_ObjectChanged dopo PropertyObjectsCache  v:" + e.PropertyName + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                            System.Diagnostics.Debug.WriteLine("procedura ObjectSpace_ObjectChanged dopo PropertyObjectsCache  v:" + ((DevExpress.ExpressApp.ObjectManipulatingEventArgs)(e)).Object + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());


                            // SULLE MODIFICHE
                            var NUOVOOBJ = View.ObjectSpace.IsNewObject(e.Object);
                            switch (Sw_propertyName)
                            {
                                case "Impianto":
                                    #region case impianto nuova
                                    try
                                    {
                                        Servizio imp = e.NewValue as Servizio;
                                        Servizio old_imp = e.OldValue as Servizio;
                                        //var a = e.MemberInfo.Owner.is
                                        RdL rdl = e.Object as RdL;
                                        #region IMPOSTA LA PRIORITà ed il TIPO INTERVENTO RELATIVO ALL'IMPIANTO
                                        if (rdl.Oid == -1 && imp != null) // rdl.Impianto != null) codice modificato
                                        {
                                            int contapr = imp.Immobile.Contratti.ContrattiPrioritas.Count;
                                            int Comm = imp.Immobile.Contratti.Oid;

                                            string GruppoImpianto = imp.Gruppo.ToString();
                                            if (contapr > 0)
                                            {
                                                Urgenza p = imp.Immobile.Contratti.ContrattiPrioritas
                                                    .Where(a => a.Gruppo == GruppoImpianto && a.Contratti.Oid == Comm && a.Default == FlgAbilitato.Si)
                                                    .Select(s => s.Urgenza)
                                                .FirstOrDefault();
                                                rdl.Urgenza = p;
                                            }

                                            TipoIntervento tp = imp.Immobile.Contratti.ContrattiTInterventos
                                                 .Where(a => a.Commesse.Oid == Comm && a.Default == FlgAbilitato.Si)
                                                  .Select(s => s.TipoIntervento)
                                                .FirstOrDefault();
                                            rdl.TipoIntervento = tp;
                                        }
                                        else
                                        {
                                            rdl.Urgenza = null;
                                            rdl.TipoIntervento = null;
                                        }
                                        #endregion
                                        if (View.Id == "RdL_DetailView")
                                        {
                                            if (e.NewValue != null)
                                            {
                                                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                                                #region AGGIORNAMENTO APPARATO DI DEFAULT CON GUASTO
                                                if (rdl != null && rdl.Categoria.Oid == 4 && rdl.Oid == -1)
                                                    if (imp.ApparatoDefault == null)
                                                    {
                                                        XPQuery<Asset> RegoleQuery = new XPQuery<Asset>(Sess);
                                                        var query = RegoleQuery.Where(w => w.Immobile.Oid == rdl.Immobile.Oid && w.Servizio.Oid == rdl.Servizio.Oid && w.Abilitato == FlgAbilitato.Si && w.AbilitazioneEreditata == FlgAbilitato.Si)
                                                                               .Where(w => w.Descrizione.ToUpper().Contains("NON DEFINITO"))
                                                                               .Select(s => s.Oid).ToList();
                                                        if (query.Count() > 0)
                                                        {
                                                            // ASSOCIO IL NUOCO APPARATO DEFAULT
                                                            IObjectSpace xpObjectSpaceImp = Application.CreateObjectSpace(typeof(Servizio));// View.ObjectSpace;
                                                            Servizio _Impianto = xpObjectSpaceImp.GetObjectByKey<Servizio>(imp.Oid);
                                                            _Impianto.ApparatoDefault = xpObjectSpaceImp.GetObjectByKey<Asset>(query[0]);
                                                            _Impianto.Save();
                                                            xpObjectSpaceImp.CommitChanges();
                                                            imp.Reload();
                                                            //----------------------
                                                            rdl.Asset = imp.ApparatoDefault;
                                                            if (rdl.Asset.Servizio.Immobile.Contratti.AbilitaProblemaDefault)
                                                            {
                                                                if (rdl.Asset.StdAsset != null)
                                                                {
                                                                    if (rdl.Asset.StdAsset.ProblemaDefault != null)
                                                                    {
                                                                        rdl.Prob = Sess.GetObjectByKey<Problemi>(rdl.Asset.StdAsset.ProblemaDefault.Oid);
                                                                    }
                                                                    else
                                                                    {
                                                                        rdl.Prob = Sess.GetObjectByKey<Problemi>(GetStdApparatoProblemaDefault(rdl.Asset.StdAsset).Oid);
                                                                    }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            int oidApparatoDefault = 0;  ///apparato di defaul non c'e' verifichiamo se c'e standard classi e standard
                                                            GroupOperator criteriaAND = new GroupOperator(GroupOperatorType.And);

                                                            IObjectSpace xpObjectSpace = Application.CreateObjectSpace(typeof(Asset));// View.ObjectSpace;

                                                            criteriaAND.Operands.Clear();
                                                            criteriaAND.Operands.Add(CriteriaOperator.Parse("Sistema.Oid == ?", imp.Sistema.Oid));
                                                            criteriaAND.Operands.Add(CriteriaOperator.Parse("Contains(Upper([Descrizione]),?)", "NON DEFINITO"));

                                                            StdApparatoClassi stdApparatoCalassiDefault = xpObjectSpace.FindObject<StdApparatoClassi>(criteriaAND);

                                                            if (stdApparatoCalassiDefault == null)
                                                            {
                                                                StdApparatoClassi _stdApparatoClassioDefault = xpObjectSpace.CreateObject<StdApparatoClassi>();
                                                                _stdApparatoClassioDefault.CodUni = imp.Sistema.CodUni + ".100";
                                                                _stdApparatoClassioDefault.Descrizione = "Non Definito";
                                                                _stdApparatoClassioDefault.Sistema = xpObjectSpace.GetObjectByKey<Sistema>(imp.Sistema.Oid);
                                                                _stdApparatoClassioDefault.Utente = "Admin";
                                                                _stdApparatoClassioDefault.DataAggiornamento = DateTime.Now;
                                                                _stdApparatoClassioDefault.Save();
                                                                xpObjectSpace.CommitChanges();
                                                                stdApparatoCalassiDefault = _stdApparatoClassioDefault;
                                                            }

                                                            criteriaAND.Operands.Clear();
                                                            criteriaAND.Operands.Add(CriteriaOperator.Parse("StdApparatoClassi.Oid == ?", stdApparatoCalassiDefault.Oid));
                                                            criteriaAND.Operands.Add(CriteriaOperator.Parse("Contains(Upper([Descrizione]),?)", "NON DEFINITO"));

                                                            StdAsset stdApparatoDefault = xpObjectSpace.FindObject<StdAsset>(criteriaAND);

                                                            if (stdApparatoDefault == null)
                                                            {
                                                                StdAsset _stdApparatoDefault = xpObjectSpace.CreateObject<StdAsset>();
                                                                _stdApparatoDefault.CodUni = stdApparatoCalassiDefault.CodUni + ".1";

                                                                _stdApparatoDefault.Descrizione = "Non Definito";
                                                                _stdApparatoDefault.CodDescrizione = "ND" + imp.Sistema.CodDescrizione;
                                                                _stdApparatoDefault.StdApparatoClassi = stdApparatoCalassiDefault;
                                                                //_stdApparatoDefault.KDimensiones = xpObjectSpace.GetObjectByKey<Sistema>(imp.Sistema.Oid);
                                                                _stdApparatoDefault.Utente = "Admin";
                                                                _stdApparatoDefault.DataAggiornamento = DateTime.Now;
                                                                _stdApparatoDefault.Save();
                                                                xpObjectSpace.CommitChanges();
                                                                stdApparatoDefault = _stdApparatoDefault;
                                                            }
                                                            Asset ApparatoDefault = xpObjectSpace.CreateObject<Asset>();

                                                            ApparatoDefault.CodDescrizione = imp.Immobile.CodDescrizione + "_" + stdApparatoDefault.CodDescrizione + "_001";
                                                            ApparatoDefault.Descrizione = "Non Definito(" + ApparatoDefault.CodDescrizione + ")";
                                                            ApparatoDefault.StdAsset = stdApparatoDefault;
                                                            ApparatoDefault.Servizio = xpObjectSpace.GetObjectByKey<Servizio>(imp.Oid);
                                                            ApparatoDefault.Quantita = 1;
                                                            ApparatoDefault.EntitaAsset = EntitaAsset.Virtuale;
                                                            ApparatoDefault.DateInService = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
                                                            ApparatoDefault.DateUnService = new DateTime(DateTime.Now.Year + 10, DateTime.Now.Month, DateTime.Now.Day, 00, 00, 00);
                                                            ApparatoDefault.Utente = "Admin";
                                                            ApparatoDefault.DataAggiornamento = DateTime.Now;
                                                            ApparatoDefault.Abilitato = FlgAbilitato.Si;
                                                            ApparatoDefault.AbilitazioneEreditata = FlgAbilitato.Si;
                                                            ApparatoDefault.Save();

                                                            ApparatoDefault.Servizio.ApparatoDefault = ApparatoDefault;
                                                            ApparatoDefault.Save();
                                                            xpObjectSpace.CommitChanges();

                                                            oidApparatoDefault = ApparatoDefault.Oid;
                                                            rdl.Asset = Sess.GetObjectByKey<Asset>(oidApparatoDefault);

                                                            if (rdl.Asset.Servizio.Immobile.Contratti.AbilitaProblemaDefault)
                                                            {
                                                                if (rdl.Asset.StdAsset.ProblemaDefault != null)
                                                                {
                                                                    rdl.Prob = Sess.GetObjectByKey<Problemi>(rdl.Asset.StdAsset.ProblemaDefault.Oid);
                                                                    //rdl.Prob = rdl.Apparato.StdApparato.ProblemaDefault;
                                                                }
                                                                else
                                                                {
                                                                    rdl.Prob = Sess.GetObjectByKey<Problemi>(GetStdApparatoProblemaDefault(rdl.Asset.StdAsset).Oid);
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        rdl.Asset = imp.ApparatoDefault;

                                                        if (rdl.Asset.Servizio.Immobile.Contratti.AbilitaProblemaDefault)
                                                        {
                                                            if (rdl.Asset.StdAsset.ProblemaDefault != null)
                                                            {
                                                                rdl.Prob = Sess.GetObjectByKey<Problemi>(rdl.Asset.StdAsset.ProblemaDefault.Oid);
                                                            }
                                                            else
                                                            {
                                                                rdl.Prob = Sess.GetObjectByKey<Problemi>(GetStdApparatoProblemaDefault(rdl.Asset.StdAsset).Oid);
                                                            }
                                                        }
                                                    }
                                                //    AbilitaProblemaDefault

                                                #endregion s
                                            }
                                            else  // è nullo
                                            {
                                                rdl.Asset = null;
                                            }
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
                                    #endregion
                                    break;
                                case "Asset":
                                    try
                                    {
                                        Asset newApparato = e.NewValue as Asset;
                                        RdL rdl = e.Object as RdL;
                                        if (View.Id == "RdL_DetailView")
                                        {
                                            if (e.NewValue != null)
                                            {
                                                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                                                #region se è apparato CHE E' CAMBIATO
                                                //int OidProblema = 0;
                                                //int OidCausa = 0;
                                                //int OidRimedio = 0;
                                                if (rdl.Categoria != null && rdl.Categoria.Oid == 4 && rdl.Oid == -1)
                                                    if (newApparato.Servizio.Immobile.Contratti.AbilitaProblemaDefault)
                                                    {
                                                        StdAsset stdApp = newApparato.StdAsset;
                                                        if (newApparato.StdAsset == null)
                                                        {
                                                            break;
                                                        }
                                                        if (stdApp.ProblemaDefault == null)
                                                        {
                                                            if (rdl.Asset.StdAsset.ProblemaDefault != null)
                                                            {
                                                                rdl.Prob = Sess.GetObjectByKey<Problemi>(rdl.Asset.StdAsset.ProblemaDefault.Oid);
                                                                //rdl.Prob = rdl.Apparato.StdApparato.ProblemaDefault;
                                                            }
                                                            else
                                                            {
                                                                rdl.Prob = Sess.GetObjectByKey<Problemi>(GetStdApparatoProblemaDefault(rdl.Asset.StdAsset).Oid);
                                                            }
                                                        }
                                                        else
                                                        {
                                                            rdl.Prob = Sess.GetObjectByKey<Problemi>(stdApp.ProblemaDefault.Oid);
                                                            //rdl.Prob = stdApp.ProblemaDefault;
                                                        }
                                                    }
                                                    else
                                                    {
                                                        if (rdl.Prob != null)
                                                            rdl.Prob = null;
                                                        if (rdl.Causa != null)
                                                            rdl.Causa = null;
                                                    }
                                                #endregion
                                            }
                                        }
                                        suppressOnChanged = false;
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

                                case "Piano":
                                    try
                                    {
                                        Piani newPiano = e.NewValue as Piani;
                                        RdL rdl = e.Object as RdL;
                                        if (View.Id == "RdL_DetailView")
                                        {
                                            if (e.NewValue != null && e.NewValue != e.OldValue)
                                            {
                                                rdl.Locale = null;
                                            }
                                            else
                                            {
                                                rdl.Locale = null;
                                            }
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

                                case "Locale":
                                    try
                                    {
                                        Locali newLocale = e.NewValue as Locali;
                                        RdL rdl = e.Object as RdL;
                                        if (View.Id == "RdL_DetailView")
                                        {
                                            if (e.NewValue != null && e.NewValue != e.OldValue)
                                            {
                                                if (rdl.Piano != newLocale.Piano)
                                                {
                                                    rdl.SetMemberValue("Piano", newLocale.Piano);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        suppressOnChanged = false;
                                    }


                                    break;
                                case "Priorita":
                                    if (e.NewValue != null)
                                    {
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
                                    break;
                                case "DataCompletamento":
                                    try
                                    {
                                        if (View.Id.Contains("RdL_DetailView"))
                                        {
                                            if (e.NewValue != null)
                                            {
                                                RdL rdl = e.Object as RdL;
                                                if (rdl.Categoria.Oid == 4) // attività a guasto
                                                {
                                                    string Titolo = "verifica ";
                                                    string Messaggio = "Verificare";
                                                    if (((RdL)e.Object).SopralluogoEseguito != Fatto.Si)
                                                        Messaggio = string.Format("Attenzione! Verificare lo stato Sopralluogo ({0}), deve essere impostato su SI.", rdl.SopralluogoEseguito.ToString());
                                                    else if ((DateTime)e.NewValue < rdl.DataRichiesta)
                                                        Messaggio = string.Format("Attenzione! Verificare che la Data Sopralluogo ({0}), sia maggiore della data di Richiesta: {1}", ((DateTime)e.NewValue).ToString(), rdl.DataRichiesta.ToString());
                                                    else if ((DateTime)e.NewValue < rdl.DataSopralluogo)
                                                        Messaggio = string.Format("Attenzione! Verificare la Data Sopralluogo ({0}), non può essere maggiore della data di Completamento: {1}", ((DateTime)e.NewValue).ToString(), rdl.DataCompletamento);
                                                    else if ((DateTime)e.NewValue < rdl.DataAzioniTampone)
                                                        Messaggio = string.Format("Attenzione! Verificare la Data di Azione a Tampone ({0}), non può essere maggiore della data di Completamento: {1}", ((DateTime)e.NewValue).ToString(), rdl.DataCompletamento);
                                                    else if ((DateTime)e.NewValue < rdl.DataInizioLavori)
                                                        Messaggio = string.Format("Attenzione! Verificare la Data di InizioLavori ({0}), non può essere maggiore della data di Completamento: {1}", ((DateTime)e.NewValue).ToString(), rdl.DataCompletamento);


                                                    if (Messaggio != "Verificare")
                                                        SetMessaggioWeb(Messaggio, Titolo, InformationType.Info);

                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        suppressOnChanged = false;
                                    }

                                    break;

                                case "DataSopralluogo":
                                    try
                                    {
                                        if (View.Id.Contains("RdL_DetailView"))
                                        {
                                            if (e.NewValue != null)
                                            {
                                                RdL rdl = e.Object as RdL;
                                                if (rdl.Categoria.Oid == 4) // attività a guasto
                                                {
                                                    string Titolo = "verifica ";
                                                    string Messaggio = "Verificare";
                                                    if (((RdL)e.Object).SopralluogoEseguito != Fatto.Si)
                                                        Messaggio = string.Format("Attenzione! Verificare lo stato Sopralluogo ({0}), deve essere impostato su SI.", rdl.SopralluogoEseguito.ToString());
                                                    else if ((DateTime)e.NewValue < rdl.DataRichiesta)
                                                        Messaggio = string.Format("Attenzione! Verificare che la Data Sopralluogo ({0}), sia maggiore della data di Richiesta: {1}", ((DateTime)e.NewValue).ToString(), rdl.DataRichiesta.ToString());
                                                    else if (rdl.DataCompletamento != null && (DateTime)e.NewValue > rdl.DataCompletamento && new int[] { 4, 5, 6, 7, 8, 9 }.Contains(rdl.UltimoStatoSmistamento.Oid))
                                                        Messaggio = string.Format("Attenzione! Verificare la Data Sopralluogo ({0}), non può essere maggiore della data di Completamento: {1}", ((DateTime)e.NewValue).ToString(), rdl.DataCompletamento);


                                                    if (Messaggio != "Verificare")
                                                        SetMessaggioWeb(Messaggio, Titolo, InformationType.Info);

                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        suppressOnChanged = false;
                                    }


                                    break;

                                case "SopralluogoEseguito":
                                    try
                                    {
                                        if (View.Id.Contains("RdL_DetailView"))
                                        {
                                            if (e.NewValue != null)
                                            {
                                                RdL rdl = e.Object as RdL;
                                                if (rdl.Categoria.Oid == 4) // attività a guasto
                                                {
                                                    string Titolo = "verifica ";
                                                    string Messaggio = "Verificare";
                                                    if (((RdL)e.Object).SopralluogoEseguito == Fatto.Si)
                                                        Messaggio = string.Format("Attenzione! Fai CLICK su Questo MESSAGGIO se vuoi Impostare con data e ora corrente ({0}) la data di SOPRALLUOGO? ", DateTime.Now.ToString());

                                                    if (Messaggio != "Verificare")
                                                        SetMessaggioWeb(Messaggio, Titolo, InformationType.Info, true);
                                                }
                                            }
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        suppressOnChanged = false;
                                    }


                                    break;

                                case "Categoria":
                                    try
                                    {
                                        //Locali newLocale = e.NewValue as Locali;
                                        //RdL rdl = e.Object as RdL;                                  

                                        if (e.NewValue != null && e.NewValue != e.OldValue)
                                        {
                                            SetMessaggioWebAutoAssegnazione(e.NewValue.ToString(), "Categoria");
                                        }
                                    }
                                    catch
                                    {
                                    }
                                    finally
                                    {
                                        //suppressOnChanged = false;
                                    }

                                    break;
                            }

                            System.Diagnostics.Debug.WriteLine("procedura ObjectSpace_ObjectChanged dopo PropertyObjectsCache  v:" + ((DevExpress.ExpressApp.ObjectManipulatingEventArgs)(e)).Object + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                            System.Diagnostics.Debug.WriteLine("procedura ObjectSpace_ObjectChanged dopo query.Count()  v:" + e.PropertyName + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                            break;
                    }
                }
                System.Diagnostics.Debug.WriteLine("fine procedura ObjectSpace_ObjectChanged  v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                suppressOnChanged = false;
            }

            setLog(SecuritySystem.CurrentUserName, "D2 FINE dopo IF ObjectSpace_ObjectChanged:");
        }

        public string GetRuleName(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser Utente)
        {
            var NomiRuolo = Utente.Roles.Select(s => s.Name).ToArray();
            if (NomiRuolo.Length < 1) return string.Empty;
            return string.Join(";", NomiRuolo);
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
            if (Pulsanti)
            {
                options.OkDelegate = () =>
                          {
                              //IObjectSpace os = View.ObjectSpace;
                              RdL rdl = (RdL)View.CurrentObject;
                              rdl.DataSopralluogo = DateTime.Now;
                              rdl.DataAzioniTampone = DateTime.Now;
                              rdl.DataInizioLavori = DateTime.Now;
                              rdl.Save();
                              //IObjectSpace os = Application.CreateObjectSpace(typeof(Test));
                              //DetailView detailView = Application.CreateDetailView(os, os.FindObject<Test>(new BinaryOperator(nameof(Test.Oid), test.Oid)));
                              //Application.ShowViewStrategy.ShowViewInPopupWindow(detailView);
                          };
            }


            Application.ShowViewStrategy.ShowMessage(options);
        }
        private void SetMessaggioWebAutoAssegnazione(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 7000;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Top;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
                                                      //options.OkDelegate = () =>
                                                      //{

            //};
            Application.ShowViewStrategy.ShowMessage(options);
        }

        private bool VerificaOrario(TimeSpan t_left, TimeSpan t_right) //(String strleft, String strright)
        {
            DateTime d = DateTime.Now;
            TimeSpan t_center = d.TimeOfDay;
            //TimeSpan t_left = TimeSpan.Parse(strleft);
            //TimeSpan t_right = TimeSpan.Parse(strright);


            bool l = t_center >= t_left;
            bool r = t_center <= t_right;

            bool v = l && r;

            Debug.WriteLine(v);
            return v;
        }


        public bool FestivitaNazionale()  // int anno, int mese, int giorno)
        {
            DateTime adesso = DateTime.Now;
            int anno = adesso.Year;
            int mese = adesso.Month;
            int giorno = adesso.Day;
            DateTime dt;
            try
            {
                dt = new DateTime(anno, mese, giorno);
                //if (DayOfWeek.Saturday.Equals(dt.DayOfWeek))
                //    return true;

                //if (DayOfWeek.Sunday.Equals(dt.DayOfWeek))
                //    return true;

                /*capodanno*/
                if (giorno == 1 && mese == 1)
                    return true;

                /*6 gennaio epifania*/
                if (giorno == 6 && mese == 1)
                    return true;

                /*25 aprile*/
                if (giorno == 25 && mese == 4)
                    return true;

                /*1 maggio*/
                if (giorno == 1 && mese == 5)
                    return true;

                ///*29 giugno s.pietro e paolo*/
                //if (giorno == 29 && mese == 6)
                //    return true;

                /*15 agosto*/
                if (giorno == 15 && mese == 8)
                    return true;

                /*2 giugno*/
                if (giorno == 2 && mese == 6)
                    return true;

                /*1 novembre*/
                if (giorno == 1 && mese == 11)
                    return true;

                /*8 dicembre*/
                if (giorno == 8 && mese == 12)
                    return true;

                /*natale*/
                if (giorno == 25 && mese == 12)
                    return true;

                /*s stefano*/
                if (giorno == 26 && mese == 12)
                    return true;
                // pasqua
                DateTime pasquadey = DateTime.MinValue;
                if (Module.Classi.EasterCalculation.GetEasterDate(anno) != null)
                    pasquadey = (DateTime)Module.Classi.EasterCalculation.GetEasterDate(anno);

                if (dt == pasquadey || dt == pasquadey.AddDays(1)) // pasqua
                    return true;

            }
            catch (Exception ex)
            {
                //log.Error(ex);
                return false;
            }

            return false;
        }


        private Problemi GetStdApparatoProblemaDefault(StdAsset stdApp)
        {
            if (stdApp.ProblemaDefault == null)
            {
                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                XPQuery<ApparatoProblema> RegoleQuery = new XPQuery<ApparatoProblema>(Sess);
                var query = RegoleQuery.Where(w => w.StdApparato == stdApp)
                                       .Where(w => w.Problemi.Descrizione.ToUpper().Contains("Non Definito".ToUpper()))
                                       .Select(s => s.Problemi.Oid).ToList();
                if (query.Count() > 0)
                {
                    stdApp.ProblemaDefault = View.ObjectSpace.GetObjectByKey<Problemi>(query[0]);
                    stdApp.Save();
                    IObjectSpace xpObjectSpacestdApp = Application.CreateObjectSpace(typeof(StdAsset));
                    StdAsset _stdApp = xpObjectSpacestdApp.GetObjectByKey<StdAsset>(stdApp.Oid);
                    _stdApp.ProblemaDefault = xpObjectSpacestdApp.GetObjectByKey<Problemi>(query[0]);
                    _stdApp.DataAggiornamento = DateTime.Now;
                    _stdApp.Save();
                    xpObjectSpacestdApp.CommitChanges();

                    return _stdApp.ProblemaDefault;
                    //rdl.Causa = stdApp.CausaDefault;
                }
                else
                {
                    IObjectSpace xpObjectSpaceProblemi = Application.CreateObjectSpace(typeof(Problemi));
                    Problemi problema = xpObjectSpaceProblemi.FindObject<Problemi>(new BinaryOperator("Descrizione", "Non Definito"));
                    if (problema == null)
                    {
                        problema = xpObjectSpaceProblemi.CreateObject<Problemi>();
                        problema.Descrizione = "Non Definito";
                        problema.Valore = 0;
                        problema.Save();
                    }
                    //XPQuery<Cause> CauseQuery = new XPQuery<Cause>(Sess);
                    //int contaCause = ProblemiQuery.Where(w => w.Descrizione == "Non Definito").Count();
                    Cause causa = xpObjectSpaceProblemi.FindObject<Cause>(new BinaryOperator("Descrizione", "Non Definito"));
                    if (causa == null)
                    {
                        causa = xpObjectSpaceProblemi.CreateObject<Cause>();
                        causa.Descrizione = "Non Definito";
                        causa.Valore = 0;
                        causa.Save();
                    }
                    //XPQuery<Rimedi> RimediQuery = new XPQuery<Rimedi>(Sess);
                    //int contaRimedi = ProblemiQuery.Where(w => w.Descrizione == "Non Definito").Count();
                    Rimedi rimedio = xpObjectSpaceProblemi.FindObject<Rimedi>(new BinaryOperator("Descrizione", "Non Definito"));
                    if (rimedio == null)
                    {
                        rimedio = xpObjectSpaceProblemi.CreateObject<Rimedi>();
                        rimedio.Descrizione = "Non Definito";
                        rimedio.Valore = 0;
                        rimedio.Save();
                    }

                    ApparatoProblema ap = xpObjectSpaceProblemi.CreateObject<ApparatoProblema>();
                    ap.Problemi = problema;
                    ap.StdApparato = stdApp;
                    ap.Save();

                    ProblemaCausa pc = xpObjectSpaceProblemi.CreateObject<ProblemaCausa>();
                    pc.ApparatoProblema = ap;
                    pc.Cause = causa;
                    pc.Save();

                    CausaRimedio cr = xpObjectSpaceProblemi.CreateObject<CausaRimedio>();
                    cr.ProblemaCausa = pc;
                    cr.Rimedi = rimedio;
                    cr.Save();
                    //((XPObjectSpace)ObjectSpace)
                    xpObjectSpaceProblemi.CommitChanges();

                    int OidProblema = ap.Problemi.Oid;
                    return ap.Problemi;
                }

            }
            return stdApp.ProblemaDefault;
        }



        #region calcolo distanza e carico risorsa

        public List<RisorseTeam> GetRTeamVicina(List<RisorseTeam> listRisorseTeams, double Pa_lat1, double Pa_lon1, int nrRisorseTeam)
        {
            if (listRisorseTeams.Count() == 0) return null;
            if (Pa_lat1 == 0 && Pa_lon1 == 0 && nrRisorseTeam < 1) return null;
            var Distanze_Rt_Edifici = listRisorseTeams.Where(w => w.RegistroRdL != null)
                                                .Where(w => w.RisorsaCapo.Disponibilita)
               .Where(w => !string.IsNullOrEmpty(w.UserName))
                                                .Where(w => w.RisorsaCapo.GeoLatUltimaPosiz != null && w.RisorsaCapo.GeoLatUltimaPosiz > 0)
                                                .Where(w => w.RisorsaCapo.GeoLngUltimaPosiz != null && w.RisorsaCapo.GeoLngUltimaPosiz > 0)
                                                .GroupBy(g => g)
                                                .Select(s => new
                                                {
                                                    RT = s.Key,
                                                    Distanza = CAMS.Module.Classi.Util.CalcolaDistanzaInkiloMeteri(Pa_lat1, Pa_lon1,
                                                    Convert.ToDouble(s.Key.RisorsaCapo.GeoLatUltimaPosiz),
                                                    Convert.ToDouble(s.Key.RisorsaCapo.GeoLngUltimaPosiz))
                                                })
                                                .OrderBy(o => o.Distanza)
                                                 .Take(nrRisorseTeam)
                                                .ToList()
                                                ;

            foreach (var item in Distanze_Rt_Edifici)
            {
                Console.WriteLine(string.Format("{0}  {1}", item.Distanza, item.RT.FullName));
            }
            //41.9672938, 12.6798324
            //41.89, 12.492
            //17.7623 km
            var dd = CAMS.Module.Classi.Util.CalcolaDistanzaInkiloMeteri(41.8824386, 12.5144895, 41.943898, 12.5603338);
            Console.WriteLine(string.Format(" distanza {0}", dd.ToString()));

            if (Distanze_Rt_Edifici.Count() == 0) return null;
            return Distanze_Rt_Edifici.Select(s => s.RT).ToList<RisorseTeam>();
        }

        public List<RisorseTeam> GetRTeamScarica(List<RisorseTeam> listRisorseTeams, int nrRisorseTeam)
        {
            if (listRisorseTeams.Count() == 0) return null;
            if (nrRisorseTeam < 1) return null;

            var caricoRT = listRisorseTeams.Where(w => w.RegistroRdL != null)
                   .Where(w => w.RisorsaCapo.Disponibilita)
                .Where(w => !string.IsNullOrEmpty(w.UserName))
                   .Where(w => w.RisorsaCapo.GeoLatUltimaPosiz != null && w.RisorsaCapo.GeoLatUltimaPosiz > 0)
                   .Where(w => w.RisorsaCapo.GeoLngUltimaPosiz != null && w.RisorsaCapo.GeoLngUltimaPosiz > 0)
                   .Where(w => w.RegistroRdLs.Any(a => a.Categoria.Oid == 4 && new int[] { 2, 3, 11 }.Contains(a.UltimoStatoSmistamento.Oid)))
                   .GroupBy(g => g)
                    .Select(i => new { RT = i.Key, Count = i.Key.NumeroAttivitaEmergenza + i.Key.NumeroAttivitaAgenda + i.Key.NumeroAttivitaSospese })
                    .OrderBy(o => o.Count).Take(nrRisorseTeam).ToList();

            foreach (var item in caricoRT)
            {
                Console.WriteLine(string.Format("{0}  {1}", item.Count, item.RT.FullName));

            }
            if (caricoRT.Count() == 0) return null;
            return caricoRT.Select(s => s.RT).ToList<RisorseTeam>();
        }


        #endregion
        void setLog(string utente, string descrizione)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            LogSystemTrace logdb = xpObjectSpace.CreateObject<LogSystemTrace>(); // new Event() { Subject = "test" };
            logdb.Utente = utente;  //user.UserName;
            logdb.Descrizione = descrizione;
            logdb.DataAggiornamento = DateTime.Now;
            string corpo = string.Format("suppressOnChanged:{0}, suppressOnCommitting{1}, suppressOnCommitted{2}", suppressOnChanged.ToString(), suppressOnCommitting.ToString(), suppressOnCommitted.ToString());

            logdb.Corpo = corpo;

            logdb.Save();
            xpObjectSpace.CommitChanges();

        }

    }
}

