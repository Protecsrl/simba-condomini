using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.DBDataImport;
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
using DevExpress.Xpo;
using CAMS.Module.DBTransazioni;
using DevExpress.ExpressApp.Xpo;

namespace CAMS.Module.Controllers.DBDataImport
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegistroDataImportTentativiController : ViewController
    {
        public RegistroDataImportTentativiController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            this.ImportaManuale.Items.Add((
                          new ChoiceActionItem()
                          {
                              Id = "0",
                              Data = "Commessa",
                              Caption = "Commessa"
                          }));
            this.ImportaManuale.Items.Add((
                   new ChoiceActionItem()
                   {
                       Id = "1",
                       Data = "Immobile",
                       Caption = "Immobile"
                   }));
            this.ImportaManuale.Items.Add((
             new ChoiceActionItem()
             {
                 Id = "2",
                 Data = "Impianto",
                 Caption = "Impianto"
             }));

            this.ImportaManuale.Items.Add((
         new ChoiceActionItem()
         {
             Id = "3",
             Data = "Apparato",
             Caption = "Apparato"
         }));
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

        private void ImportaManuale_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var IDImport = int.Parse(e.SelectedChoiceActionItem.Id.ToUpper());
            switch (IDImport)
            {
                case 0:
                    //return 1;
                    break;
                case 1:
                    //return 1;
                    break;
                case 2: // trasferimento = 4                   
                    //return 1;
                    break;
                case 3: // trasferimento = 4                   
                    //return 1;
                    break;
                default:
                    break;
            }
        }

        private void ImportInteraCommessa_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

        }

        private void ImportAutomatico_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();

            RegistroDataImportTentativi RegDImpTentativi = xpObjectSpace.GetObjectByKey<RegistroDataImportTentativi>(((XPObject)View.CurrentObject).Oid);

            RegDImpTentativi.ImportazioneEseguita = Classi.Eseguito.No;
            RegDImpTentativi.StatoElaborazioneImport = Classi.StatoElaborazioneJob.PianificatoSimulazione;
            RegDImpTentativi.StepImportazione = Classi.StepImportazione.Tutti;
            //RegDImpTentativi.TipoAcquisizione = Classi.TipoAcquisizione.Automatico;
            if (RegDImpTentativi.RegistroTransazioni == null)
            {

                RegistroTransazioni RegTransazioni = xpObjectSpace.CreateObject<RegistroTransazioni>();
                RegTransazioni.Abilitato = Classi.FlgAbilitato.Si;
                RegTransazioni.DataPianificata = DateTime.Now.AddMinutes(1);
                RegTransazioni.Descrizione = " Elaborazione Tentativo Importazione";
                RegTransazioni.StatoElaborazioneJob = Classi.StatoElaborazioneJob.PianificatoSimulazione;
                AppTransazioni ApTrans = xpObjectSpace.FindObject<AppTransazioni>(new BinaryOperator("NomeApp", "LanciaDataImport"));
                RegTransazioni.AppTransazioni = ApTrans;
                RegTransazioni.Save();
                RegDImpTentativi.RegistroTransazioni = RegTransazioni;
            }
            RegDImpTentativi.Save();
            xpObjectSpace.CommitChanges();
            //RdL GetRdL = xpObjectSpace.GetObjectByKey<RdL>(GetNotificaRdL.RdL.Oid);

            //var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView_Gestione", true, GetRdL);

            //view.Caption = string.Format("Richiesta di Lavoro");
            //view.ViewEditMode = ViewEditMode.Edit;
            //Application.MainWindow.SetView(view);
        }

        private void ImportManuale_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

        }

        private void saResetStatoImport_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;

            string queryString = "delete from regdtimportxls";
            int rowsAffected = Sess.ExecuteNonQuery(queryString);

            queryString = "delete from regdtimportapparato";
            rowsAffected = Sess.ExecuteNonQuery(queryString);

            queryString = "delete from regdtimportimpianto";
            rowsAffected = Sess.ExecuteNonQuery(queryString);

            queryString = "delete from regdtimportedificio";
            rowsAffected = Sess.ExecuteNonQuery(queryString);

            queryString = " delete from regdtimportcommessa";
            rowsAffected = Sess.ExecuteNonQuery(queryString);

            RegistroDataImportTentativi RegDImpTentativi = xpObjectSpace.GetObjectByKey<RegistroDataImportTentativi>
                (((XPObject)View.CurrentObject).Oid);
            RegDImpTentativi.StatoElaborazioneImport = Classi.StatoElaborazioneJob.PianificatoSimulazione;
            RegDImpTentativi.StepImportazione = Classi.StepImportazione.Tutti;
            RegDImpTentativi.ImportazioneEseguita = Classi.Eseguito.No;
            RegDImpTentativi.Save();
            xpObjectSpace.CommitChanges();
            //xpObjectSpace.Refresh();
            View.ObjectSpace.Refresh();
        }

        private void saClonaTentativo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;

            RegistroDataImportTentativi RegDImpTentativi = xpObjectSpace.GetObjectByKey<RegistroDataImportTentativi>
       (((XPObject)View.CurrentObject).Oid);
            RegistroDataImportTentativi rdit = xpObjectSpace.CreateObject<RegistroDataImportTentativi>();
            rdit.Abilitato = Classi.FlgAbilitato.Si;
            rdit.DataPianificazione = RegDImpTentativi.DataPianificazione;
            rdit.Descrizione = string.Format("copia {0} di {1}", RegDImpTentativi.RegistroDataImport.RegistroDataImportTentativis.Count() + 1,
                                                                RegDImpTentativi.Descrizione);
            rdit.FileDataImport = RegDImpTentativi.FileDataImport;
            //rdit.FilePopolamento = RegDImpTentativi.FilePopolamento;
            rdit.ImportazioneEseguita = Classi.Eseguito.Si;
            rdit.DataPianificazione = RegDImpTentativi.DataPianificazione;
            rdit.OutputFileDataImport = RegDImpTentativi.OutputFileDataImport;
            rdit.RegistroDataImport = RegDImpTentativi.RegistroDataImport;
            rdit.RegistroTransazioni = RegDImpTentativi.RegistroTransazioni;
            rdit.StatoElaborazioneImport = Classi.StatoElaborazioneJob.Sospeso;
            rdit.StepImportazione = Classi.StepImportazione.Tutti;
            rdit.TipoAcquisizione = Classi.TipoAcquisizione.Automatico;
            rdit.Save();
            xpObjectSpace.CommitChanges();
            View.ObjectSpace.Refresh();
        }

        private void scaImpostaStato_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    Classi.StatoElaborazioneJob stato = Classi.StatoElaborazioneJob.Sospeso;

                    if (e.SelectedChoiceActionItem.Data.ToString().Contains("PianificatoSimulazione"))
                        stato = Classi.StatoElaborazioneJob.PianificatoSimulazione;
                    if (e.SelectedChoiceActionItem.Data.ToString().Contains("PianificatoExec"))
                        stato = Classi.StatoElaborazioneJob.PianificatoExec;
                    else if (e.SelectedChoiceActionItem.Data.ToString().Contains("Sospeso"))
                        stato = Classi.StatoElaborazioneJob.Sospeso;
                    else if (e.SelectedChoiceActionItem.Data.ToString().Contains("Bloccato"))
                        stato = Classi.StatoElaborazioneJob.Bloccato;

                    RegistroDataImportTentativi RegDImpTentativi = xpObjectSpace.GetObjectByKey<RegistroDataImportTentativi>
                                                                    (((XPObject)View.CurrentObject).Oid);
                    RegDImpTentativi.StatoElaborazioneImport = stato;
                    RegDImpTentativi.Save();
                    xpObjectSpace.CommitChanges();
                    View.ObjectSpace.Refresh();

                }
            }
        }
    }
}
