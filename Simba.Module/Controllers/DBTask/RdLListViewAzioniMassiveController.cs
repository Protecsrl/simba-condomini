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
using CAMS.Module.DBTask.ParametriPopUp;
using CAMS.Module.DBTask;
using CAMS.Module.DBKPI.ParametriPopUp;
using CAMS.Module.DBKPI;
#pragma warning disable CS0105 // La direttiva using per 'CAMS.Module.DBKPI.ParametriPopUp' è già presente in questo spazio dei nomi
using CAMS.Module.DBKPI.ParametriPopUp;
#pragma warning restore CS0105 // La direttiva using per 'CAMS.Module.DBKPI.ParametriPopUp' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Data.Filtering' è già presente in questo spazio dei nomi
using DevExpress.Data.Filtering;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Data.Filtering' è già presente in questo spazio dei nomi
using DevExpress.Data.PivotGrid;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.Actions' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Actions;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.Actions' è già presente in questo spazio dei nomi
//using DevExpress.ExpressApp.Chart.Web;
//using DevExpress.ExpressApp.PivotGrid.Web;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Persistent.Base' è già presente in questo spazio dei nomi
using DevExpress.Persistent.Base;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Persistent.Base' è già presente in questo spazio dei nomi
using DevExpress.Persistent.BaseImpl;

//using DevExpress.Web.ASPxPivotGrid;
using DevExpress.XtraPivotGrid;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.Collections;
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.IO;
namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RdLListViewAzioniMassiveController : ViewController
    {
        public RdLListViewAzioniMassiveController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            string a = "";

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

        private void PupWinCompletamento_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            StringBuilder Messaggio = new StringBuilder("", 32000);
            if (xpObjectSpace != null)
            {
                var Parametro = ((RdLParametriAzioniMassive)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));
                List<RdLListViewAzioniMassive> RdLListViewAzioniMassiveSelezionato =
                    (((ListView)View).Editor).GetSelectedObjects().Cast<RdLListViewAzioniMassive>().ToList();


                foreach (RdLListViewAzioniMassive item in RdLListViewAzioniMassiveSelezionato)
                {
                    try
                    {
                        int rdloid = item.Codice;
                        RdL RdL = xpObjectSpace.GetObjectByKey<RdL>(rdloid);
                        if (string.IsNullOrEmpty(Parametro.NoteCompletamento))
                            RdL.NoteCompletamento = null;
                        else
                            RdL.NoteCompletamento = Parametro.NoteCompletamento;

                        if (Parametro.DataCompletamento == DateTime.MinValue || Parametro.DataCompletamento == null)
                            RdL.DataCompletamento = RdL.DataPianificata;
                        else
                            RdL.DataCompletamento = Parametro.DataCompletamento;

                        RdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(4);
                        RdL.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(11);
                        RdL.Save();

                        RegistroRdL RegRdL = xpObjectSpace.GetObjectByKey<RegistroRdL>(RdL.RegistroRdL.Oid);
                        if (string.IsNullOrEmpty(Parametro.NoteCompletamento))
                            RegRdL.NoteCompletamento = null;
                        else
                            RegRdL.NoteCompletamento = Parametro.NoteCompletamento;

                        if (Parametro.DataCompletamento == DateTime.MinValue || Parametro.DataCompletamento == null)
                            RegRdL.DataCompletamento = RdL.DataPianificata;
                        else
                            RegRdL.DataCompletamento = Parametro.DataCompletamento;

                        RegRdL.UltimoStatoSmistamento = xpObjectSpace.GetObjectByKey<StatoSmistamento>(4);
                        RegRdL.UltimoStatoOperativo = xpObjectSpace.GetObjectByKey<StatoOperativo>(11);
                        RegRdL.Save();

                    }
                    catch
                    {
                        string tmp = string.Format("non eseguita azione su RdL:{0} ({1})", item.Codice, item.Descrizione);
                        Messaggio.AppendLine(tmp);
                    }

                    xpObjectSpace.CommitChanges();

                    if (Messaggio.Length.Equals(0))
                        Messaggio.AppendLine(string.Format("Azione Eseguita Regolarmente su {0} RdL", RdLListViewAzioniMassiveSelezionato.Count()));

                    MessageOptions options = new MessageOptions();
                    options.Duration = 3000;
                    options.Message = Messaggio.ToString();
                    options.Web.Position = InformationPosition.Top;
                    options.Type = InformationType.Success;
                    options.Win.Caption = "Caption";
                    //options.CancelDelegate = CancelDelegate;
                    //options.OkDelegate = OkDelegate;

                    Application.ShowViewStrategy.ShowMessage(options);
                }
            }
        }

        private void PupWinCompletamento_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
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
                    Nuovo.DataCompletamento = DateTime.Now;
                    Nuovo.NoteCompletamento = "Attvitata Completata Regolarmente";
                    Nuovo.SecurityUser = user;
                    Parametro = Nuovo;
                }
                var view = Application.CreateDetailView(xpObjectSpace, "RdLParametriAzioniMassive_DetailView", true, Parametro);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

    
  

  
    
    }
}
