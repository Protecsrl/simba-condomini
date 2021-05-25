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
using CAMS.Module.DBTask.ParametriPopUp;
using CAMS.Module.DBTask.Vista;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo.Metadata;
using System.Collections;
using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
//DevExpress.ExpressApp.SystemModule


namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegRdLListViewAzioniMassiveController : ViewController
    {
        public RegRdLListViewAzioniMassiveController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
           


            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
            //((ListView)View).
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void PupWinRegRdLAzioniMassive_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //var xpObjectSpace = Application.CreateObjectSpace();
            IObjectSpace xpObjectSpace = View.ObjectSpace;
            string codicereg = string.Empty;
            string desc = string.Empty;
            int contafatte = 0;
            if (xpObjectSpace != null)
            {
                var Parametro = ((RdLParametriAzioniMassive)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));
                int conta = (((ListView)View).Editor).GetSelectedObjects().Count;
                if (conta < 350)
                {
                    foreach (object obj in (((ListView)View).Editor).GetSelectedObjects())
                    {
                        try
                        {
                            Type objectType = ((XpoDataViewRecord)obj).ObjectType;
                            //XPClassInfo classInfo;                                             
                            //object persistentObject = null;
                            //string keyPropertyName = null;
                            //keyPropertyName = classInfo.KeyProperty.Name;
                            object keyPropertyValue = ((XpoDataViewRecord)obj)["Codice"];
                            //RegRdLListViewAzioniMassive persistentObject1 =  (RegRdLListViewAzioniMassive)xpObjectSpace.GetObjectByKey(objectType, keyPropertyValue);
                            RegistroRdL regRdL = xpObjectSpace.GetObjectByKey<RegistroRdL>(keyPropertyValue);
                            desc = regRdL.Descrizione;

                            if (string.IsNullOrEmpty(Parametro.NoteCompletamento))
                                regRdL.NoteCompletamento = null;
                            else
                                regRdL.NoteCompletamento = Parametro.NoteCompletamento;

                            if (Parametro.StessaPianificata)
                            {
                                regRdL.DataCompletamento = regRdL.DataPianificata;
                            }
                            else
                            {
                                if (Parametro.DataCompletamento == DateTime.MinValue || Parametro.DataCompletamento == null)
                                    regRdL.DataCompletamento = regRdL.DataPianificata;
                                else
                                    regRdL.DataCompletamento = Parametro.DataCompletamento;
                            }

                            regRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(4);
                            regRdL.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(11);
                            regRdL.DataAggiornamento = DateTime.Now;
                            regRdL.UtenteUltimo = SecuritySystem.CurrentUserName;
                            regRdL.Save();

                            foreach (RdL rdl in regRdL.RdLes)
                            {
                                if (string.IsNullOrEmpty(Parametro.NoteCompletamento))
                                    rdl.NoteCompletamento = null;
                                else
                                    rdl.NoteCompletamento = Parametro.NoteCompletamento;

                                rdl.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(4);
                                rdl.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(11);

                                if (Parametro.DataCompletamento == DateTime.MinValue || Parametro.DataCompletamento == null)
                                    rdl.DataCompletamento = regRdL.DataPianificata;
                                else
                                    rdl.DataCompletamento = Parametro.DataCompletamento;

                                rdl.DataCompletamentoSistema = DateTime.Now;
                                rdl.DataAggiornamento = DateTime.Now;
                                rdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                                rdl.Save();
                            }

                        }
                        catch
                        {
                            string tmp = string.Format("non eseguita azione su RegRdL:{0} ({1}); l'operazione è stata interrotta!", codicereg, desc);
                            SetMessaggioWeb(tmp, "Azione Fallita", InformationType.Warning);
                            break;
                        }
                        contafatte++;
                        xpObjectSpace.CommitChanges();// se va in eccezione non salva esce dal for
                    }
                    string tmp1 = string.Format("Sono state Aggiornate nr.:{0} Attività!", contafatte);
                    SetMessaggioWeb(tmp1, "Azione Eseguita", InformationType.Info);
                    xpObjectSpace.Refresh();
                }
                else
                {
                    string tmp1 = string.Format("Sono state nr.:{0} Attività!;  LIMITE MAX SUPERATO!!, non selezionare + di 350 righe.", conta);
                    SetMessaggioWeb(tmp1, "Azione non Eseguita Limite superato", InformationType.Info);
                    xpObjectSpace.Refresh();

                }

            }

        }

        private void PupWinRegRdLAzioniMassive_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            //private void PupWinRegRdLAzioniMassive_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
            //       {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                GroupOperator GrOperator = new GroupOperator(GroupOperatorType.And);
                DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                                                              xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                                                              ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);

                RdLParametriAzioniMassive Parametro = xpObjectSpace.FindObject<RdLParametriAzioniMassive>(
                                                              CriteriaOperator.Parse("SecurityUser = ?", user));
                if (Parametro == null)
                {
                    RdLParametriAzioniMassive Nuovo = xpObjectSpace.CreateObject<RdLParametriAzioniMassive>();
                    //Nuovo.DataCompletamento = DateTime.Now;
                    //Nuovo.NoteCompletamento = "Attvitata Completata Regolarmente";
                    Nuovo.StessaPianificata = true;
                    Nuovo.SecurityUser = user;
                    Parametro = Nuovo;
                }
                var view = Application.CreateDetailView(xpObjectSpace, "RdLParametriAzioniMassive_DetailView_Completa", true, Parametro);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
            //}

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

        //        a) da azioni massive RegRdL seleziono alcuni elementi listaRdL
        //b) verifico se in listaRdL ci sono piu Risorse
        //	- si: sonomaggiori di 1 quindi messaggio che deve essere uno.
        //	- no: è una risorsa.quindi apro i parametri_azionimassive

        //        c) aprendo i parametri di azione massive imposto la risorsa su RisorseOld
        //        d) qui ce anche il campo di ricerca e il campo di selezione popwinCercaRisorse
        //        d.1) cerca risorse

        //            d.2) paremetrizzo la finestra di popup con :
        //				- centro operativo di apparatenenza del vecchio risorsa
        //				- area di polo e relativi centri operativi di apparatenenza
        //				- carico il databinder delle risorse team
        //				- aggiorno:
        //					- con conduttore le risorse che sono conduttere con nla stessa risorsa old
        //					- con lo stesso centro operativo ordine = 1
        //                    - con la syessa Area di Polo con Ordine = 2
        //			d.3) filtro le risorse che hanno il testo indicato nella ricerca risorse(nella lookup)

        //        e)seleziono la risorsa: 
        //			e.1) excetuvive
        //				- salvo la risorsa salvata sul parametri azione massiva su risorsa new
        //				- salvo e committo.
        //        f) premendo ok su parametri risorsa massiva:
        //			- f.1) aggiorno tutte le listaRdL con il nuovo Risorsa.
        private void PopWinRegRdLCambiaRisorsaMassivo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View.ObjectSpace;
            string codicereg = string.Empty;
            string desc = string.Empty;
            int contafatte = 0;
            if (xpObjectSpace != null)
            {
                var mio = ((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject).GetType();
                if (mio.FullName == "CAMS.Module.Classi.MessaggioPopUp")
                {
                    string Messaggiot = string.Format("Azione annullata per selezione multipla di risorseteam");
                    SetMessaggioWeb(Messaggiot, "Azione Fallita", InformationType.Warning);
                }
                else
                {

                    var Parametro = ((RdLParametriAzioniMassive)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));
                    if (Parametro != null)
                    {
                        int conta = (((ListView)View).Editor).GetSelectedObjects().Count;
                        if (conta < 350)
                        {
                            foreach (object obj in (((ListView)View).Editor).GetSelectedObjects())
                            {
                                try
                                {
                                    Type objectType = ((XpoDataViewRecord)obj).ObjectType;

                                    object keyPropertyValue = ((XpoDataViewRecord)obj)["Codice"];
                                    RegistroRdL regRdL = xpObjectSpace.GetObjectByKey<RegistroRdL>(keyPropertyValue);
                                    if (Parametro.RisorseTeamNew != null)
                                    {
                                        regRdL.RisorseTeam = View.ObjectSpace.GetObjectByKey<RisorseTeam>(Parametro.RisorseTeamNew.Oid);
                                        if (regRdL.DataAssegnazioneOdl == null)
                                        {
                                            regRdL.DataAssegnazioneOdl = DateTime.Now;
                                        }
                                    }

                                    if (Parametro.UltimoStatoSmistamento != null)
                                        regRdL.UltimoStatoSmistamento = View.ObjectSpace.GetObjectByKey<StatoSmistamento>(Parametro.UltimoStatoSmistamento.Oid);
                                    if (regRdL.UltimoStatoSmistamento.Oid == 1)
                                        regRdL.UltimoStatoOperativo = null;
                                    if (regRdL.UltimoStatoSmistamento.Oid == 6)
                                        regRdL.UltimoStatoOperativo = View.ObjectSpace.GetObjectByKey<StatoOperativo>(12); ;

                                    regRdL.DataAggiornamento = DateTime.Now;
                                            regRdL.UtenteUltimo = SecuritySystem.CurrentUserName;
                                            regRdL.Save();

                                    foreach (RdL rdl in regRdL.RdLes)
                                    {
                                        if (rdl.RisorseTeam != regRdL.RisorseTeam)
                                        {
                                            rdl.RisorseTeam = regRdL.RisorseTeam;
                                            if (rdl.DataAssegnazioneOdl == null)
                                            {
                                                rdl.DataAssegnazioneOdl = DateTime.Now;
                                            }
                                        }
                                        if (rdl.UltimoStatoSmistamento != regRdL.UltimoStatoSmistamento)
                                            rdl.UltimoStatoSmistamento = regRdL.UltimoStatoSmistamento;
                                            if (rdl.UltimoStatoSmistamento.Oid == 1)
                                                rdl.UltimoStatoOperativo = null;
                                        if (rdl.UltimoStatoSmistamento.Oid == 6)
                                            rdl.UltimoStatoOperativo = regRdL.UltimoStatoOperativo;
                                        rdl.DataAggiornamento = DateTime.Now;
                                            rdl.UtenteUltimo = SecuritySystem.CurrentUserName;
                                            rdl.Save();
                                    }
                                    contafatte++;
                                    xpObjectSpace.CommitChanges();// se va in eccezione non salva esce dal for
                                }
                                catch (Exception ex)
                                {
                                    string tmp = string.Format("non eseguita azione su RegRdL:{0} ({1}); l'operazione è stata interrotta!", codicereg, desc + ex.Message);
                                    SetMessaggioWeb(tmp, "Azione Fallita", InformationType.Warning);
                                    break;
                                }
                                //contafatte++;
                                //xpObjectSpace.CommitChanges();// se va in eccezione non salva esce dal for                            
                            }
                            string tmp1 = string.Format("Sono state Aggiornate nr.:{0} Attività!", contafatte);
                            SetMessaggioWeb(tmp1, "Azione Eseguita", InformationType.Info);
                            xpObjectSpace.Refresh();
                        }
                        else
                        {
                            string tmp1 = string.Format("Sono state nr.:{0} Attività!;  LIMITE MAX SUPERATO!!, non selezionare + di 350 righe.", 1);
                            SetMessaggioWeb(tmp1, "Azione non Eseguita Limite superato", InformationType.Info);
                            xpObjectSpace.Refresh();

                        }

                    }
                }
            }
        }

        private void PopWinRegRdLCambiaRisorsaMassivo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            string tipoDetailView = "RegRdLListViewAzioniMassive";
            string Messaggio = string.Empty;
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                int conta = (((ListView)View).Editor).GetSelectedObjects().Count;
                if (conta > 350)
                {    //esci
                    string tmp = string.Format("Numero Max di Elementi Selezionabili Superato!, eseguire una Selezione di Interventi non superiore a 350 elementi; sono stati selezionati nr {0} Interventi", conta);
                    SetMessaggioWeb(tmp, "Azione Fallita", InformationType.Warning);
                }
                else
                {
                    ArrayList SelectedRegRdLMassive = new ArrayList();
                    List<RegRdLListViewAzioniMassive> ListViewAzioniMassive = new List<RegRdLListViewAzioniMassive>();
                    //if ((e.SelectedObjects.Count > 0) &&
                    //    ((e.SelectedObjects[0] is XafDataViewRecord) || (e.SelectedObjects[0] is XafInstantFeedbackRecord)))
                    //{
                    //    foreach (var selectedObject in e.SelectedObjects)
                    //    {
                    //        SelectedContacts.Add((Contact)ObjectSpace.GetObject(selectedObject));
                    //    }
                    //}
                    foreach (object obj in (((ListView)View).Editor).GetSelectedObjects())
                    {
                        try
                        {
                            //SelectedRegRdLMassive.Add((RegRdLListViewAzioniMassive)ObjectSpace.GetObject(obj));
                            ListViewAzioniMassive.Add((RegRdLListViewAzioniMassive)ObjectSpace.GetObject(obj));
                        }
                        catch { }
                    }
                    var vSSm = ListViewAzioniMassive.Select(s => s.StatoSmistamento).Distinct().ToArray();
                    int nrvSSm = vSSm.Count();
                    var vTeams = ListViewAzioniMassive.Select(s => s.Team).Distinct().ToArray();
                    int nrTeams = vTeams.Count();

                    var vCentroOperativo_Edificio = ListViewAzioniMassive.Select(s => s.OidCentroOperativo).Distinct().ToArray();
                    int nrCo = vCentroOperativo_Edificio.Count();
                    if (nrCo == 0)
                    {    //esci
                        Messaggio = string.Format("Selezione non coerente! Verificare che gli edifici relativi agli interventi siano associati ad un centro operativo base; non ci sono centri operativi base associati agli interventi {0} selezionati", conta);
                        SetMessaggioWeb(Messaggio, "Azione Fallita", InformationType.Warning);
                        tipoDetailView = "MessaggioPopUp";
                        //throw new UserFriendlyException("UserFriendlyException message error");
                    }
                    else if (nrCo > 1)
                    {    //esci
                        Messaggio = string.Format("Selezione non coerente!Eseguire una Selezione di Interventi con la stesso Centro Operativo; sono stati selezionati nr {0} Centri Operativi in nr. {1} Interventi", nrCo, conta);
                        SetMessaggioWeb(Messaggio, "Azione Fallita", InformationType.Warning);
                        tipoDetailView = "MessaggioPopUp";
                        //throw new UserFriendlyException("UserFriendlyException message error");
                    }
                    else if (nrTeams > 1)
                    {    //esci
                        Messaggio = string.Format("Selezione non coerente!Eseguire una Selezione di Interventi con la stessa risorsa Team; sono stati selezionati nr {0} Risorse in {1} Interventi", nrTeams, conta);
                        SetMessaggioWeb(Messaggio, "Azione Fallita", InformationType.Warning);
                        tipoDetailView = "MessaggioPopUp";
                        //throw new UserFriendlyException("UserFriendlyException message error");
                    }
                    else if (nrvSSm > 1)
                    {
                        Messaggio = string.Format("Selezione non coerente!Eseguire una Selezione di Interventi con lo stesso Stato Smistamento; sono stati selezionati nr {0} Stati Smistamento diversi in {1} Interventi", nrTeams, conta);
                        SetMessaggioWeb(Messaggio, "Azione Fallita", InformationType.Warning);
                        tipoDetailView = "MessaggioPopUp";
                        //throw new UserFriendlyException("UserFriendlyException message error");
                    }
                    else
                    {
                        tipoDetailView = "RegRdLListViewAzioniMassive";
                    }

                    if (tipoDetailView == "RegRdLListViewAzioniMassive")
                    {
                        GroupOperator GrOperator = new GroupOperator(GroupOperatorType.And);
                        DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                                                                      xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                                                                      ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);

                        RdLParametriAzioniMassive Parametro = xpObjectSpace.FindObject<RdLParametriAzioniMassive>(
                                                                      CriteriaOperator.Parse("SecurityUser = ?", user));

                        RisorseTeam RisorsaOld = xpObjectSpace.GetObjectByKey<RegistroRdL>(ListViewAzioniMassive.First().Codice).RisorseTeam;
                        CentroOperativo CO = xpObjectSpace.GetObjectByKey<RegistroRdL>(ListViewAzioniMassive.First().Codice)
                            .Asset.Servizio.Immobile.CentroOperativoBase; ;
                        StatoSmistamento StatoSmistamentoOld = xpObjectSpace.GetObjectByKey<RegistroRdL>(ListViewAzioniMassive.First().Codice).UltimoStatoSmistamento;
                        if (Parametro == null)
                        {
                            RdLParametriAzioniMassive Nuovo = xpObjectSpace.CreateObject<RdLParametriAzioniMassive>();
                            Parametro = Nuovo;
                        }
                        Parametro.RisorseTeamOld = RisorsaOld;
                        Parametro.StatoSmistamento_old = StatoSmistamentoOld;
                        if (StatoSmistamentoOld.Oid == 1)
                            Parametro.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(11);
                        else
                            Parametro.UltimoStatoSmistamento = StatoSmistamentoOld;
                        Parametro.RisorseTeamNew = null;
                        Parametro.SecurityUser = user;
                        //if (RisorsaOld != null)
                        //    Parametro.CentroOperativo = xpObjectSpace.FindObject<RisorseTeam>
                        //             (new BinaryOperator("Oid", RisorsaOld.Oid)).CentroOperativo;
                        //else
                        if (CO != null)
                            Parametro.CentroOperativo = CO;
                        //Parametro.CentroOperativo = xpObjectSpace.FindObject<CentroOperativo>(new BinaryOperator("Oid", RisorsaOld.Oid));


                        var view = Application.CreateDetailView(xpObjectSpace, "RdLParametriAzioniMassive_DetailView_CambioRisorsa", true, Parametro);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.View = view;

                    }
                    else if (tipoDetailView == "MessaggioPopUp")
                    {
                        var Mess = xpObjectSpace.CreateObject<MessaggioPopUp>();
                        Mess.Messaggio = Messaggio.ToString();
                        var view = Application.CreateDetailView(xpObjectSpace, "MessaggioPopUp_DetailView", true, Mess);
                        view.ViewEditMode = ViewEditMode.View;
                        e.View = view;
                        view.Caption = view.Caption + " - Messaggio di Avviso Utente";
                        //e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                        //e.ShowViewParameters.TargetWindow = TargetWindow.NewWindow;
                        //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;
                    }

                }
            }
        }
    }
}
