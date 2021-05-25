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
using CAMS.Module.DBAgenda;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.Vista;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;

namespace CAMS.Module.Controllers.DBAgenda
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class TaskWithNotificationsController : ViewController
    {
        private SimpleAction markCompletedAction;
        
        private void MarkCompletedAction_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            prova NoteficheRdL = (prova)View.CurrentObject; // recupero il record selezionato
            //((prova)View.CurrentObject).MarkCompleted();
            NoteficheRdL.MarkCompleted();// chiamo il metodo che mi imposta come completato
            IObjectSpace xpojs = Application.CreateObjectSpace();  ///  creo la objectspace (cioè la connessione al DB)
            RdL objRdL = xpojs.GetObjectByKey<RdL>(NoteficheRdL.RdL); // recupero la rdl associata alla notifica
            //obj.StatoAutorizzativo.Oid = 3;// Approvato da So
            objRdL.StatoAutorizzativo = xpojs.GetObjectByKey<StatoAutorizzativo>(3);// assegno lo stato autorizzativo = 3 
            //obj.Save();
            objRdL.Save();// salvo i cambiamenti in memoria (in memoria dell'applicazione)
            xpojs.CommitChanges();// salvo i cambiamenti nel datatabase oraCLE
        }
        public TaskWithNotificationsController()
        {
            TargetObjectType = typeof(prova);
            markCompletedAction = new SimpleAction(this, "Accettazione Orario", PredefinedCategory.Edit);
            markCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            markCompletedAction.ImageName = "State_Task_Completed";
            markCompletedAction.Execute += MarkCompletedAction_Execute;

            //TargetObjectType = typeof(TaskWithNotifications);
            markCompletedAction = new SimpleAction(this, "Entra nella Rdl", PredefinedCategory.Edit);
            markCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
            markCompletedAction.ImageName = "State_Task_Completed";
            markCompletedAction.Execute += Vai_Execute;
        }
        //public TaskWithNotificationsController()
        //{
        //    InitializeComponent();
        //    // Target required Views (via the TargetXXX properties) and create their Actions.
        //}
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

        private void Vai_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;

            RdLListView GetRdL_ListView = xpObjectSpace.GetObject<RdLListView>((RdLListView)View.CurrentObject);
            RdL NuovoRdL = xpObjectSpace.GetObjectByKey<RdL>(GetRdL_ListView.Codice);
            
            //RdLListSimiliView GetRdL_ListView = xpObjectSpace.GetObject<RdLListSimiliView>((RdLListSimiliView)View.CurrentObject);
            //RdL NuovoRdL = xpObjectSpace.GetObjectByKey<RdL>(RdL_DetailView.Codice);
            // RdL NuovoRdL = xpObjectSpace.GetObjectByKey<RdL>(RdL_DetailView.Codice);
            //RdLListSimiliView GetRdL_ListView = xpObjectSpace.GetObject<RdLListSimiliView>((RdLListSimiliView)View.CurrentObject);
          

            var view = Application.CreateDetailView(xpObjectSpace, "RdL_DetailView", true, NuovoRdL);

            view.Caption = string.Format("Nuova Richiesta di Lavoro");
            view.ViewEditMode = ViewEditMode.Edit;
            Application.MainWindow.SetView(view);
            //Frame.SetView(view);
            //e.ShowViewParameters.CreatedView = view;
            ////e.ShowViewParameters.Context
            ////e.ShowViewParameters.Context = TemplateContext.ApplicationWindow;
            //e.ShowViewParameters.TargetWindow = TargetWindow.Default;
        }
    }
}


//public class TaskWithNotificationsController : ViewController
//{
//    private SimpleAction markCompletedAction;
//    private void MarkCompletedAction_Execute(object sender, SimpleActionExecuteEventArgs e)
//    {
//        ((TaskWithNotifications)View.CurrentObject).MarkCompleted();
//    }
//    public TaskWithNotificationsController()
//    {
//        TargetObjectType = typeof(TaskWithNotifications);
//        markCompletedAction = new SimpleAction(this, "MarkCompleted", PredefinedCategory.Edit);
//        markCompletedAction.SelectionDependencyType = SelectionDependencyType.RequireSingleObject;
//        markCompletedAction.ImageName = "State_Task_Completed";
//        markCompletedAction.Execute += MarkCompletedAction_Execute;
//    }
//}