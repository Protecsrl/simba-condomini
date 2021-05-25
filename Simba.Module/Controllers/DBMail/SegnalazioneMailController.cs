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
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAgenda;
using CAMS.Module.Classi;
using CAMS.Module.DBAux;
using CAMS.Module.DBTask.AppsCAMS;
using CAMS.Module.DBMail;

namespace CAMS.Module.Controllers.DBMail
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class SegnalazioneMailController : ViewController
    {
        public SegnalazioneMailController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        private DateTime DataAdesso = DateTime.Now;
        private DateTime Adesso = DateTime.Now;

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

        private void simpleAction1_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                Immobile tEdificio = xpObjectSpace.GetObject<Immobile>(((SegnalazioneMail)View.CurrentObject).Immobile);

                Session session = ((XPObjectSpace)xpObjectSpace).Session;
                int ContaEdifici = session.Query<Immobile>().Count();
                var NuovoRdL = xpObjectSpace.CreateObject<RdL>();

                //NuovoRdL.Categoria = xpObjectSpace.GetObjectByKey<Categoria>(4);
                NuovoRdL.SetMemberValue("Categoria", xpObjectSpace.GetObjectByKey<Categoria>(4));

                //NuovoRdL.Priorita = xpObjectSpace.GetObjectByKey<Priorita>(2);
                NuovoRdL.SetMemberValue("Priorita", xpObjectSpace.GetObjectByKey<Urgenza>(2));

                //NuovoRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(1);
                NuovoRdL.SetMemberValue("UltimoStatoSmistamento", xpObjectSpace.GetObjectByKey<StatoSmistamento>(1));

                //NuovoRdL.TipoIntervento = xpObjectSpace.GetObjectByKey<TipoIntervento>(2);
                NuovoRdL.SetMemberValue("TipoIntervento", xpObjectSpace.GetObjectByKey<TipoIntervento>(2));

                NuovoRdL.SetMemberValue("TipologiaSpedizione", CAMS.Module.Classi.TipologiaSpedizioneRdL.No);

                NuovoRdL.SetMemberValue("Soddisfazione", Classi.Soddisfazione.INDIFFERENTE);// 2 - null = molto soddisfatto

                //NuovoRdL.DataCreazione = DateTime.Now;
                NuovoRdL.SetMemberValue("DataCreazione", DataAdesso);

                //NuovoRdL.DataRichiesta = DateTime.Now;
                NuovoRdL.SetMemberValue("DataRichiesta", DataAdesso);

                NuovoRdL.SetMemberValue("DataPianificata", DataAdesso.AddMinutes(15));
                NuovoRdL.SetMemberValue("DataPianificataEnd", DataAdesso.AddMinutes(30));

                NuovoRdL.SetMemberValue("DataSopralluogo", DataAdesso.AddMinutes(16));
                NuovoRdL.SetMemberValue("DataAzioniTampone", DataAdesso.AddMinutes(16));
                NuovoRdL.SetMemberValue("DataInizioLavori", DataAdesso.AddMinutes(16));
                NuovoRdL.SetMemberValue("DataAggiornamento", DataAdesso.AddMinutes(1));

                NuovoRdL.SetMemberValue("UtenteCreatoRichiesta", SecuritySystem.CurrentUserName);

                
                //Immobile ed = session.Query<Immobile>().First();
                NuovoRdL.SetMemberValue("Immobile", tEdificio);
                NuovoRdL.SetMemberValue("Impianto", tEdificio.Impianti.FirstOrDefault());

                Asset _Apparato = session.Query<Asset>().Where(w => w.Servizio == NuovoRdL.Servizio).Select(s => s).First();

                NuovoRdL.SetMemberValue("Apparato", _Apparato);

                Richiedente _Richiedente = session.Query<Richiedente>().Where(w => w.Commesse == NuovoRdL.Immobile.Contratti).Select(s => s).First();
                NuovoRdL.SetMemberValue("Richiedente", _Richiedente);
               
                NuovoRdL.Descrizione = "Sam esegue una Prova vvvvvvvvvvvvvvvvvvvv v vvvvvvvvvvvvvvvvvvvvvvvvvvv" + DateTime.Now.ToShortDateString();

                ex_ObjectSpace_Committing(ref    xpObjectSpace, ref    NuovoRdL);
                ex_ObjectSpace_Committed(ref    xpObjectSpace, ref    NuovoRdL);
                NuovoRdL.Save();
                xpObjectSpace.CommitChanges();

                  #region invia messaggio
                  // cambio qualunque proprieta
                  string Messaggio = string.Empty;
                  if (true)
                  {
                      //if ((View.Id.Contains("RdL_DetailView_Gestione") || View.Id.Contains("RdL_DetailView_NuovoTT")) && View.ObjectTypeInfo.Type == typeof(RdL))
                      //{
                          try
                          {
                              System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA SPEDISCI MAIL SMS v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                               //RdL = NuovoRdL.Oid;
                              using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                              {
                                  im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, NuovoRdL.RegistroRdL.Oid, ref  Messaggio);
                              }
                              if (!string.IsNullOrEmpty(Messaggio))
                              {
                                  string Titolo = "Trasmissione Avviso Eseguita!!";
                                  string AlertMessaggio = string.Format("Messaggio di Spedizione - {0}", Messaggio);
                                  SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Success);
                              }
                              System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed DOPO SPEDISCI MAIL SMS v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());


                          }
                          catch (Exception ex)
                          {
                              string Titolo = "Trasmissione Avviso non Eseguita!!";
                              string AlertMessaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                              SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Warning);
                              //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                          }
                      }

                      
                  //}
                  //nuovoRegistroRdL = false;
                  #endregion
                  //------------------------------------------------------------------





                var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, NuovoRdL);
                view.Caption = string.Format("Nuova Richiesta di Lavoro");
                view.ViewEditMode = ViewEditMode.View;

                e.ShowViewParameters.CreatedView = view;
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;

                //var viewRdL = Application.CreateDetailView(xpObjectSpace, RdLNuovoTTGuasto_DetailView, true, NuovoRdL);
                //viewRdL.Caption = string.Format("Nuova Richiesta di Lavoro");
                //viewRdL.ViewEditMode = ViewEditMode.Edit;
                //e.ActionArguments.ShowViewParameters.CreatedView = viewRdL;
                //e.ActionArguments.ShowViewParameters.TargetWindow = TargetWindow.Current;
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
        
        void ex_ObjectSpace_Committing(ref IObjectSpace os, ref  RdL rdl)
        {

            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

            List<string> sbMessaggio = new List<string>();
            //IObjectSpace os = (IObjectSpace)sender;
            //for (int i = os.ModifiedObjects.Count - 1; i >= 0; i--)
            //{
            //    object item = os.ModifiedObjects[i];
            //    if (typeof(RdL).IsAssignableFrom(item.GetType()))
            //    {
            //        this.SmistamentoOid_ObjectsCache.Clear();
            //        this.OperativoObjectsCache.Clear();
            //        this.RisorsaTeamObjectsCache.Clear();
            //        NuovaModificaSuRDLCorrente = false;
            //        nuovoRegistroRdL = false;

            //RdL rdl = (RdL)item;
            if (rdl.RegistroRdL == null)
            {
                #region caso creazione Registro Prima Del COMMIT
                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing prima os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os)); v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                rdl.RegistroRdL = os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os));
                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo os.GetObject<RegistroRdL>(CreateRegistroRdL(rdl, ref os)); v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
                //NuovaModificaSuRDLCorrente = true;
                rdl.UtenteCreatoRichiesta = SecuritySystem.CurrentUserName;
                #endregion
            }
            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing creato registro v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
            if (rdl.OdL == null)
            {
                OdL odl = os.CreateObject<OdL>();
                string odlDescrizione = rdl.Descrizione;
                if (odlDescrizione.Length > 240)
                    odlDescrizione = odlDescrizione.Substring(1, 239) + "...";
                odl.Descrizione = odlDescrizione;
                odl.RegistroRdL = rdl.RegistroRdL;
                odl.TipoOdL = os.GetObjectByKey<TipoOdL>(1);// misto
                //odl.UltimoStatoOperativo = xpObjSpaceOdL.GetObject<StatoOperativo>(rdl.UltimoStatoOperativo);
                odl.DataEmissione = rdl.DataAssegnazioneOdl;

                odl.Save();
                rdl.OdL = odl;

            }
            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing creato odl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing modifica registro v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
            ///  fine rdl.Oid>0 ----------------------------------------------------

            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing prima salva rdl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
            rdl.Save();
            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committing dopo salva rdl v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
        }

        private RegistroRdL CreateRegistroRdL(RdL rdl, ref IObjectSpace xpObjectSpaceRRdl)
        {
            string Descrizione = string.Empty;
            //IObjectSpace xpObjectSpaceRRdl = Application.CreateObjectSpace();

            RegistroRdL rrdl = xpObjectSpaceRRdl.CreateObject<RegistroRdL>();
            rrdl.Asset = rdl.Asset;// Apparato   xpObjectSpaceRRdl.GetObject<Apparato>(rdl.Apparato);
            rrdl.Categoria = rdl.Categoria;//xpObjectSpaceRRdl.GetObject<Categoria>(rdl.Categoria);//

            rrdl.DATA_CREAZIONE_RDL = rdl.DataRichiesta;

            rrdl.DataPianificata = rdl.DataPianificata;//  data pianificata  data richieste da bonificare su db 15012018
            rrdl.DataAssegnazioneOdl = rdl.DataAssegnazioneOdl;//  data pianificata  data richieste da bonificare  15012018

            //rrdl.DataAssegnazioneOdl = rdl.DataPianificata;///   ************************************
            rrdl.DataAzioniTampone = rdl.DataAzioniTampone;
            rrdl.DataFermo = rdl.DataFermo;
            rrdl.DataAzioniTampone = rdl.DataAzioniTampone;
            rrdl.DataFermo = rdl.DataFermo;//
            rrdl.DataInizioLavori = rdl.DataInizioLavori;//
            rrdl.DataPrevistoArrivo = rdl.DataPrevistoArrivo;
            //rrdl.DataRiavvio = rdl.DataRiavvio;//
            //rrdl.DataSopralluogo = rdl.DataSopralluogo;//
            rrdl.Priorita = rdl.Urgenza;// xpObjectSpaceRRdl.GetObject<Priorita>(rdl.Priorita);//

            //if (rdl.Problema != null)
            //{
            //    rrdl.Problema = rdl.Problema;// xpObjectSpaceRRdl.GetObject<ApparatoProblema>(rdl.Problema);//
            //    if (rdl.ProblemaCausa != null)
            //        rrdl.ProblemaCausa = rdl.ProblemaCausa;// xpObjectSpaceRRdl.GetObject<ProblemaCausa>(rdl.ProblemaCausa);//
            //}
            if (rdl.Prob != null)
            {
                rrdl.Prob = rdl.Prob;// xpObjectSpaceRRdl.GetObject<ApparatoProblema>(rdl.Problema);//
                if (rdl.Causa != null)
                    rrdl.Causa = rdl.Causa;// xpObjectSpaceRRdl.GetObject<ProblemaCausa>(rdl.ProblemaCausa);//
            }

            //if (rdl.RisorseTeam != null)
            //    rrdl.RisorseTeam = xpObjectSpaceRRdl.GetObject<RisorseTeam>(rdl.RisorseTeam);
            //rrdl.UltimoStatoOperativo = xpObjectSpaceRRdl.GetObject<StatoOperativo>(rdl.UltimoStatoOperativo);
            rrdl.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;//xpObjectSpaceRRdl.GetObject<StatoSmistamento>(rdl.UltimoStatoSmistamento);
            rrdl.Utente = SecuritySystem.CurrentUserName;
            rrdl.UtenteUltimo = rrdl.Utente;
            //rrdl.AutorizzazioniRegistroRdLs
            //rrdl.Save();
            //xpObjectSpaceRRdl.CommitChanges();
            //1	MANUTENZIONE PROGRAMMATA     //5	MANUTENZIONE PROGRAMMATA SPOT
            //2	CONDUZIONE            //3	MANUTENZIONE A CONDIZIONE            //4	MANUTENZIONE GUASTO 
            if (rrdl.Categoria.Oid == 1 || rrdl.Categoria.Oid == 5)
            {
                Descrizione = string.Format("Reg.MP({0}) {1}", rrdl.Oid, rdl.Descrizione);
                if (Descrizione.Length > 3999)
                    Descrizione = Descrizione.Substring(1, 3996) + "...";
                rrdl.Descrizione = Descrizione;
            }
            else
            {
                Descrizione = string.Format("Reg.TT({0}) {1}", rrdl.Oid, rdl.Descrizione);
                if (Descrizione.Length > 3999)
                    Descrizione = Descrizione.Substring(1, 3996) + "...";
                rrdl.Descrizione = Descrizione; // string.Format("Reg.TT({0}) {1}", rrdl.Oid, rdl.Descrizione);
            }

            //  crea autorizzazioni        
            #region crea autorizzazioni su registro
            Contratti cm = rdl.Immobile.Contratti;    //Commesse cm = xpObjectSpaceRRdl.GetObject<Commesse>(rdl.Immobile.Commesse);
            if (cm.MostraDataOraFermo)
            {
                rrdl.MostraDataOraFermo = true;
                rrdl.MostraDataOraRiavvio = true;
            }
            else
            {
                rrdl.MostraDataOraFermo = false;
                rrdl.MostraDataOraRiavvio = false;
            }
            if (cm.MostraDataOraSopralluogo)
            {
                rrdl.MostraDataOraSopralluogo = true;
            }
            else
            {
                rrdl.MostraDataOraSopralluogo = false;
            }

            if (cm.MostraDataOraAzioniTampone)
            {
                rrdl.MostraDataOraAzioniTampone = true;
            }
            else
            {
                rrdl.MostraDataOraAzioniTampone = false;
            }

            if (cm.MostraDataOraInizioLavori)
            {
                rrdl.MostraDataOraInizioLavori = true;
            }
            else
            {
                rrdl.MostraDataOraInizioLavori = false;
            }

            if (cm.MostraDataOraCompletamento)
            {
                rrdl.MostraDataOraCompletamento = true;
            }
            else
            {
                rrdl.MostraDataOraCompletamento = false;
            }

            if (cm.MostraElencoCauseRimedi)
            {
                rrdl.MostraElencoCauseRimedi = true;
            }
            else
            {
                rrdl.MostraElencoCauseRimedi = false;
            }
            #endregion

            rrdl.Save();

            return rrdl;
        }

        void ex_ObjectSpace_Committed( ref IObjectSpace xpObjectSpaceRRdl, ref RdL rdl)
        {
            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
            DateTime ultimadata = DateTime.MinValue;
            string Messaggio = string.Empty;
            //RdL rdl = (RdL)View.CurrentObject;
            // aggiorna registro descrizione
            RegistroRdL rrdl = xpObjectSpaceRRdl.GetObject<RegistroRdL>(rdl.RegistroRdL);
            string Descrizione = string.Empty;
            if (rdl.Categoria.Oid == 1 || rdl.Categoria.Oid == 5)
            {
                Descrizione = string.Format("Reg.MP({0}) {1}", rdl.RegistroRdL.Oid, rdl.Descrizione);
                if (Descrizione.Length > 3999)
                    Descrizione = Descrizione.Substring(1, 3996) + "...";
                rrdl.Descrizione = Descrizione;

            }
            else
            {
                Descrizione = string.Format("Reg.TT({0}) {1}", rdl.RegistroRdL.Oid, rdl.Descrizione);
                if (Descrizione.Length > 3999)
                    Descrizione = Descrizione.Substring(1, 3996) + "...";
                rrdl.Descrizione = Descrizione; //
            }
            rrdl.Save();

            // -------------------------
            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA DI AGG RISORSA v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
            AggiornaRisorsaTeam(rdl);
            System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed DOPO DI AGG RISORSA v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

            if (rdl != null)
            {
                //RegoleAutoAssegnazioneRdL
                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA RegoleAutoAssegnazioneRdL v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                bool isGranted = SecuritySystem.IsGranted(new
                     DevExpress.ExpressApp.Security.PermissionRequest(ObjectSpace, typeof(RegoleAutoAssegnazioneRdL),
                   DevExpress.ExpressApp.Security.SecurityOperations.Read));
                #region REGOLA DI AUTOMAZIONE DI ASSEGNAZIONE ---------@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@
                if (isGranted && rdl.Immobile.Contratti.AttivaAutoAssegnazioneTeam && rdl.UltimoStatoSmistamento.Oid == 1)//  solo se è appena creata
                {
                    //  ASSEGNA AUTOMATICAMENTE
                    IObjectSpace xpObjectSpace = Application.CreateObjectSpace();// View.ObjectSpace; 
                    int OidEdificio = 0; int OidImpianto = 0; int OidCategoria = 0;

                    if (rdl.Immobile != null)
                        OidEdificio = rdl.Immobile.Oid;
                    if (rdl.Servizio != null)
                        OidImpianto = rdl.Servizio.Oid;
                    if (rdl.Categoria != null)
                        OidCategoria = rdl.Categoria.Oid;
                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    XPQuery<RegoleAutoAssegnazioneRdL> RegoleQuery = new XPQuery<RegoleAutoAssegnazioneRdL>(Sess);
                    var query = RegoleQuery.Where(w => w.Immobile.Oid == OidEdificio && w.Servizio.Oid == OidImpianto)
                                           .Where(w => w.Categoria.Oid == OidCategoria)
                                           .Where(w => w.CalendarioCadenze != null)
                                           .Where(w => w.TipoRegola == TipoRegola.RegolaAutomatismiAssegnazione) // aggiunto per regola di automazione
                                           .Where(w => w.TipoAssegnazione == TipoAssegnazione.SO || w.TipoAssegnazione == TipoAssegnazione.SM || w.TipoAssegnazione == TipoAssegnazione.SME)
                                           .Select(s => new { s.TipoAssegnazione, s.CalendarioCadenze, s.RisorseTeam, s.FesteNazionali, s.RegoleAutoAssegnazioneRisorseTeams, s.AggiungiRisorsaVicina }).ToList();


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
                    //var rtemas = query.Select(s => s.RegoleAutoAssegnazioneRisorseTeams).ToList();
                    //List<RegoleAutoAssegnazioneRisorseTeam> listRisorseTeams = null;
                    List<RisorseTeam> listRisorseTeams = new List<RisorseTeam>();
                    foreach (var dr in query)
                    {
                        listRisorseTeams = dr.RegoleAutoAssegnazioneRisorseTeams.Select(s => s.RisorseTeam).ToList();
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
                                int nowDayOfWeek = (int)DateTime.Now.DayOfWeek;	//5	= ven
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
                    if (createAotoregolaassegnazione)//allora assegno al tecnico
                    {
                        string messaggioassegnazione = "";
                        string listaRisorse = string.Empty;
                        RdL myRdL = xpObjectSpace.GetObject<RdL>(rdl);
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
                            default:   ////   quandro non definito
                                ListaRTeam_ok = listRisorseTeams;

                                break;

                        }

                        ////----------------------- tipo assegnazione  so , smartphone, emergenza------------------------------
                        switch (vTipoAssegnazione)
                        {
                            case Classi.TipoAssegnazione.SO:
                                switch (vTipoStrategiaAssegnazione)
                                {
                                    case Classi.AggiungiRisorsaVicina.unaRisorsa:
                                        if (rdl.Immobile != null)
                                        {
                                            Console.WriteLine("Current value is {0}", 1);
                                            myRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(11); // ASSEGNATA
                                            myRdL.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                            myRdL.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(vRisorseTeam);
                                            myRdL.DataAssegnazioneOdl = DateTime.Now;
                                            myRdL.DataAggiornamento = DateTime.Now;
                                            myRdL.RegistroRdL.RisorseTeam = myRdL.RisorseTeam;
                                            myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                            myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                            myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;
                                            myRdL.RegistroRdL.DataAssegnazioneOdl = DateTime.Now;
                                            SpedisciMessaggioAssegnazione = true;
                                            messaggioassegnazione = string.Format("Assegnazione Automatica Intervento: RegRdL {0}, in stato Gestione Sala Operativa, alla RisorsaTeam {1}",
                                                myRdL.RegistroRdL.Oid, myRdL.RisorseTeam.FullName);
                                        }

                                        break;
                                    default:// altro non pervenuto
                                        messaggioassegnazione = string.Format("Assegnazione Automatica Intervento: RegRdL {0}, non configurata correttemente", myRdL.RegistroRdL.Oid);
                                        break;
                                }
                                break;
                            case Classi.TipoAssegnazione.SM:
                                Console.WriteLine("Current value is {0}", 2);
                                if (rdl.Immobile != null)
                                {
                                    myRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(2); // ASSEGNATA
                                    myRdL.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                    myRdL.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(vRisorseTeam);
                                    myRdL.DataAssegnazioneOdl = DateTime.Now;
                                    myRdL.DataAggiornamento = DateTime.Now;
                                    myRdL.RegistroRdL.RisorseTeam = myRdL.RisorseTeam;
                                    myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                    myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                    myRdL.RegistroRdL.DataAssegnazioneOdl = DateTime.Now;
                                    myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;
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
                                myRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(10); // ASSEGNATA
                                myRdL.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(19);// IN ATTESA DI ESSERE PRESA IN CARICO
                                //rdl.RisorseTeam = RisorseTeam;
                                myRdL.DataAggiornamento = DateTime.Now;
                                //rdl.RegistroRdL.RisorseTeam = RisorseTeam;
                                myRdL.RegistroRdL.UltimoStatoSmistamento = myRdL.UltimoStatoSmistamento;
                                myRdL.RegistroRdL.UltimoStatoOperativo = myRdL.UltimoStatoOperativo;
                                myRdL.RegistroRdL.DataAggiornamento = DateTime.Now;

                                RegNotificheEmergenze RegNotificaEmergenza = xpObjectSpace.CreateObject<RegNotificheEmergenze>();
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
                                        Team = xpObjectSpace.GetObjectByKey<RisorseTeam>(altreRisorseTeam.Oid),
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
                        myRdL.Save();
                        xpObjectSpace.CommitChanges();
                        rdl.Reload();
                        System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed DOPO RegoleAutoAssegnazioneRdL v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                        #region invia messaggio
                        if (SpedisciMessaggioAssegnazione)
                        {
                            if (View.Id.Contains("RdL_DetailView_NuovoTT") && View.ObjectTypeInfo.Type == typeof(RdL))
                            {
                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed PRIMA SPEDIZIONE ASSEGNAZIONE v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                                try
                                {
                                    //RdL RdL = rdl;
                                    using (CAMSInvioMailCN.SetMail im = new CAMSInvioMailCN.SetMail())
                                    {
                                        im.InviaMessaggiRdL(SetVarSessione.OracleConnString, Application.Security.UserName, rdl.RegistroRdL.Oid, ref  Messaggio);
                                    }
                                    if (!string.IsNullOrEmpty(Messaggio))
                                    {
                                        string Titolo = "Trasmissione Avviso Eseguita!!";
                                        string AlertMessaggio = string.Format("Messaggio diCreazione {0}, - Messaggio di Spedizione - {1}", messaggioassegnazione, Messaggio);
                                        SetMessaggioWebAutoAssegnazione(AlertMessaggio, Titolo, InformationType.Success);
                                    }

                                }
                                catch (Exception ex)
                                {
                                    string Titolo = "Trasmissione Avviso non Eseguita!!";
                                    string AlertMessaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                                    SetMessaggioWebAutoAssegnazione(AlertMessaggio, Titolo, InformationType.Warning);
                                    //throw new Exception(string.Format("Errore: Spedizione Mail non eseguita: " + ex.Message));
                                }
                                System.Diagnostics.Debug.WriteLine("inizio ObjectSpace_Committed DOPO SPEDIZIONE ASSEGNAZIONE v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());

                            }


                        }

                        #endregion   REGOLA DI AUTOASSEGNAZIONE
                        //------------------------------------------------------------------

                    }
                }
                #endregion
                //---------------------------------------------------------------------
                //---  RESET OLD SMISTAMENTO SU NIOVA VIEW
                rdl.old_SSmistamento_Oid = 0;
                System.Diagnostics.Debug.WriteLine("fine procedura ObjectSpace_Committed  v:" + View.Id.ToString() + " tempo " + (DateTime.Now - Adesso).TotalSeconds.ToString());
            }
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


        private void SetMessaggioWebAutoAssegnazione(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 5000;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Left;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
            //options.OkDelegate = () =>
            //{

            //};
            Application.ShowViewStrategy.ShowMessage(options);
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

                /*29 giugno s.pietro e paolo*/
                if (giorno == 29 && mese == 6)
                    return true;

                /*15 agosto*/
                if (giorno == 15 && mese == 8)
                    return true;

                /*2 giugno*/
                if (giorno == 2 && mese == 6)
                    return true;

                /*2 novembre*/
                if (giorno == 2 && mese == 11)
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

        private bool VerificaOrario(TimeSpan t_left, TimeSpan t_right) //(String strleft, String strright)
        {
            DateTime d = DateTime.Now;
            TimeSpan t_center = d.TimeOfDay;
            //TimeSpan t_left = TimeSpan.Parse(strleft);
            //TimeSpan t_right = TimeSpan.Parse(strright);


            bool l = t_center >= t_left;
            bool r = t_center <= t_right;

            bool v = l && r;

            //Debug.WriteLine(v);
            return v;
        }


        void AggiornaRisorsaTeam(RdL rdl)
        {
            int oldstatosmistamentoiid = 0;
            //if (SmistamentoOid_ObjectsCache.Count > 0)
            //    oldstatosmistamentoiid = SmistamentoOid_ObjectsCache[0];

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



    }
}
