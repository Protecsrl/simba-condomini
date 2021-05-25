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
using CAMS.Module.DBTask.Vista;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using CAMS.Module.DBPlanner;
using CAMS.Module.Classi;
using CAMS.Module.DBTask.ParametriPopUp;

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListViewGuastoController : ViewController
    {
        public RdLListViewGuastoController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem += LViewController_CustomDetailView;
        }
        protected override void OnViewControlsCreated()
        {

            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            try
            {
                Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomizeShowViewParameters -= LViewController_CustomizeShowViewParameters;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        void LViewController_CustomDetailView(object sender, CustomProcessListViewSelectedItemEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                e.Handled = true;
                DetailView NewDv;

                RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(((RdLListViewGuasto)View.CurrentObject).Codice);

                NewDv = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.View;
                    e.InnerArgs.ShowViewParameters.CreatedView = NewDv;
                    e.InnerArgs.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }
        }

        private void popupAvSmistamentoGuasto_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            bool SpediscieMail = false; string Messaggio = string.Empty; int OidRegolaAssegnazionexTRisorse = 0;
            if (xpObjectSpace is XPObjectSpace && xpObjectSpace != null)
            {
                ModificaRdL ModificaRdL = (ModificaRdL)e.PopupWindowViewCurrentObject;
                ModificaRdL modificaRdL = (ModificaRdL)e.PopupWindowViewCurrentObject;
                ModificaRdL.Save();
                List<ModificaRdL> allModificaRdL = new List<ModificaRdL>();
                allModificaRdL.Add(modificaRdL);
                RuleSetValidationResult result1 = Validator.RuleSet.ValidateAllTargets(xpObjectSpace, allModificaRdL);
                            
                RdL rdl = xpObjectSpace.GetObjectByKey<RdL>(ModificaRdL.Codice);
                if (rdl != null && result1.State == ValidationState.Valid)
                {
                    #region salava
                    try
                    {

                        if (rdl.UltimoStatoSmistamento != ModificaRdL.UltimoStatoSmistamento)
                        {
                            rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(ModificaRdL.UltimoStatoSmistamento.Oid);
                            rdl.UltimoStatoOperativo = xpObjectSpace.GetObject<StatoOperativo>(ModificaRdL.UltimoStatoOperativo);
                            SpediscieMail = true;
                        }
                  

                        if (ModificaRdL.UltimoStatoSmistamento.Oid!=10)
                        {
                            #region SE è STATO TOLTO LO STATO SMISTAMENTO IN EMERGENZA PER SMARTPHONE
                            CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze notEmerRdL = xpObjectSpace.FindObject<CAMS.Module.DBTask.AppsCAMS.RegNotificheEmergenze>(CriteriaOperator.Parse("RdL.Oid = ?", rdl.Oid));
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

                        if (rdl.SopralluogoEseguito != ModificaRdL.SopralluogoEseguito)
                            rdl.SopralluogoEseguito = ModificaRdL.SopralluogoEseguito;

                        if (rdl.DataSopralluogo != ModificaRdL.DataSopralluogo)
                            rdl.DataSopralluogo = ModificaRdL.DataSopralluogo;
                        rdl.RegistroRdL.DataSopralluogo = rdl.DataSopralluogo;

                        int oidReisorsaRDL = 0;
                        int oidReisorsaMRDL = 0;
                        if (rdl.RisorseTeam != null)
                            oidReisorsaRDL = rdl.RisorseTeam.Oid;

                        if (ModificaRdL.RisorseTeam != null)
                            oidReisorsaMRDL = ModificaRdL.RisorseTeam.Oid;

                        if (oidReisorsaRDL != oidReisorsaMRDL)
                        {
                            rdl.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(ModificaRdL.RisorseTeam);
                            SpediscieMail = true;
                        }

                        if (rdl.DataPianificata != ModificaRdL.DataPianificata)
                        {
                            rdl.DataPianificata = ModificaRdL.DataPianificata;
                            DateTime fine = ModificaRdL.DataPianificata.AddMinutes(ModificaRdL.StimatempoLavoro);
                            rdl.DataPianificataEnd = fine;
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid == 2 || ModificaRdL.UltimoStatoSmistamento.Oid == 11)
                        {
                            rdl.RisorseTeam = xpObjectSpace.GetObject<RisorseTeam>(ModificaRdL.RisorseTeam);
                            SpediscieMail = true;
                        }

                        if (ModificaRdL.UltimoStatoSmistamento.Oid == 4)
                        {
                            rdl.DataCompletamento = ModificaRdL.DataCompletamento;
                            rdl.NoteCompletamento = ModificaRdL.NoteCompletamento;
                            SpediscieMail = true;
                        }

                        rdl.DataAggiornamento = DateTime.Now;

                        //   RegistroRdL
                        if (rdl.RegistroRdL != null)
                        {
                            if (rdl.RegistroRdL.DataPianificata != rdl.DataPianificata)
                                rdl.RegistroRdL.DataPianificata = rdl.DataPianificata;

                            if (rdl.RegistroRdL.RisorseTeam != rdl.RisorseTeam)
                                rdl.RegistroRdL.RisorseTeam = rdl.RisorseTeam;

                            if (rdl.RegistroRdL.UltimoStatoSmistamento != rdl.UltimoStatoSmistamento)
                            {
                                if (rdl.RegistroRdL.RdLes.Count == 1)
                                {
                                    rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento;

                                    if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                        rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;

                                    rdl.RegistroRdL.DataCompletamento = rdl.DataCompletamento;
                                    rdl.RegistroRdL.NoteCompletamento = rdl.NoteCompletamento;


                                }
                                else
                                {
                                    int nrRdL = rdl.RegistroRdL.RdLes.Count;
                                    int tutte_chiuse = rdl.RegistroRdL.RdLes.Where(w => w.UltimoStatoSmistamento == rdl.UltimoStatoSmistamento).Count();
                                    if (nrRdL == tutte_chiuse)
                                    {
                                        rdl.RegistroRdL.UltimoStatoSmistamento = rdl.UltimoStatoSmistamento; // chiudi intero registro


                                        if (rdl.RegistroRdL.UltimoStatoOperativo != rdl.UltimoStatoOperativo)
                                            rdl.RegistroRdL.UltimoStatoOperativo = rdl.UltimoStatoOperativo;
                                    }
                                }

                            }

                            if (rdl.RegistroRdL.DataPrevistoArrivo != rdl.DataPrevistoArrivo)
                                rdl.RegistroRdL.DataPrevistoArrivo = rdl.DataPrevistoArrivo;

                            rdl.RegistroRdL.DataAggiornamento = rdl.DataAggiornamento;
                            rdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                            rdl.RegistroRdL.UtenteUltimo = SecuritySystem.CurrentUserName;
                        }

                        rdl.Save();
                        xpObjectSpace.CommitChanges();
                    }
                    catch (Exception ex)
                    {
                        string Titolo = "Aggiornamento non Eseguito!!";
                        Messaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                        SetMessaggioWeb(Messaggio, Titolo, InformationType.Warning);
                        SpediscieMail = false;
                    }
                    #endregion

                    //SpediscieMail = Module.Classi.PopUpAvavanzamentoSmistamentoRdLExecute.
                    //    SetpAvSmistamentoRdLExecute(rdl.Oid, ModificaRdL, View.ObjectSpace,ref Messaggio);
                    try
                    {
                        //SpediscieMail = ModificaRdL.SetpAvSmistamentoRdLExecute(rdl.Oid, ModificaRdL, View.ObjectSpace, ref Messaggio);

                        ((RdLListViewGuasto)View.CurrentObject).Reload();

                        if (SpediscieMail)
                        {

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
                    }
                    catch (Exception ex)
                    {
                        string Titolo = "Trasmissione Avviso non Eseguita!!";
                        string AlertMessaggio = string.Format("Messaggio di Errore - {0}", ex.Message);
                        SetMessaggioWeb(AlertMessaggio, Titolo, InformationType.Warning);
                    }
                }
                else
                {
                   var msg =  result1.Results.Where(w => w.State == ValidationState.Invalid).Select(s => s.ErrorMessage).ToArray();
                   string mm = string.Join(",", msg);
                    string Titolo1 = "Canvalida non riuscita!!";
                    string AlertMessaggio1 = string.Format("Dati non Validati: {0}", mm);
                    SetMessaggioWeb(AlertMessaggio1, Titolo1, InformationType.Info);
                }
            }
        }

        private void popupAvSmistamentoGuasto_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {

                DetailView NewDv;
                RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(((RdLListViewGuasto)View.CurrentObject).Codice);
                ///
                DateTime dataMinima = DateTime.Now.AddDays(-360);
                DateTime dataMassima = DateTime.Now.AddDays(360);

                if (GetRdL.Categoria.Oid == 1)
                {
                    Session Sess = ((XPObjectSpace)View.ObjectSpace).Session;

                    XPQuery<MpAttivitaPianificateDett> qMpAttivitaPianificateDett = new XPQuery<MpAttivitaPianificateDett>(Sess);
                    TraslazioneSchedulazione qTraslazioneSchedulazione = qMpAttivitaPianificateDett
                     .Where(w => w.RdL.Oid == GetRdL.Oid)
                     .Where(w => w.Asset.Abilitato == Module.Classi.FlgAbilitato.Si)
                     .Select(s => s.Frequenza.TraslazioneSchedulazione).Min();

                    int giorni = ((int)qTraslazioneSchedulazione) * 7;
                    //datacorrente = CurObjectRegistroRdL.DataPianificata;
                    dataMinima = DateTime.Now.AddDays(-giorni);
                    dataMassima = DateTime.Now.AddDays(giorni);

                }
                ////


                DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                                          xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                                          ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);
                ModificaRdL OBJ = xpObjectSpace.FindObject<ModificaRdL>
                      (DevExpress.Data.Filtering.CriteriaOperator.Parse("Utente = ?", user.UserName));

                if (OBJ == null)
                {
                    OBJ = xpObjectSpace.CreateObject<ModificaRdL>();
                }
                //(NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(ChangeRdL));
                OBJ.Utente = SecuritySystem.CurrentUserName;
                OBJ.UltimoStatoSmistamento = GetRdL.UltimoStatoSmistamento;
                OBJ.old_SSmistamento_Oid = GetRdL.UltimoStatoSmistamento.Oid;
                OBJ.SessioneID = CAMS.Module.Classi.SetVarSessione.CodSessioneWeb;
                OBJ.Codice = GetRdL.Oid;
                OBJ.UltimoStatoOperativo = GetRdL.UltimoStatoOperativo;
                OBJ.RisorseTeam = GetRdL.RisorseTeam;
                OBJ.DataCreazione = GetRdL.DataCreazione;
                OBJ.DataPianificata = GetRdL.DataPianificata;
                OBJ.DataSopralluogo = GetRdL.DataSopralluogo;
                OBJ.SopralluogoEseguito = GetRdL.SopralluogoEseguito;
                if (GetRdL.Urgenza != null)
                    OBJ.MaxDataSopralluogo = GetRdL.DataCreazione.AddMinutes(GetRdL.Urgenza.Val);
                OBJ.StimatempoLavoro = 30;
                if (GetRdL.TipoIntervento != null && GetRdL.DataSopralluogo != null && GetRdL.DataSopralluogo > DateTime.MinValue)
                {
                    OBJ.MaxDataCompletamento = GetRdL.DataSopralluogo.AddMinutes(GetRdL.TipoIntervento.Val);
                }
                OBJ.NotaCompletamentoObbligata = GetRdL.Immobile.Contratti.AbilitaTestoCompletamentoObligatorio && GetRdL.Categoria.Oid == 4;
                //IsNullOrEmpty(NoteCompletamento) And RdL.Immobile.Commesse.AbilitaTestoCompletamentoObligatorio And RdL.Categoria.Oid = 4 And [UltimoStatoSmistamento.Oid] = 4

                OBJ.Save();

                NewDv = Application.CreateDetailView(xpObjectSpace, "ModificaRdL_DetailView", true, OBJ);
                if (NewDv != null)
                {
                    NewDv.ViewEditMode = ViewEditMode.Edit;
                    e.View = NewDv;
                }
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
