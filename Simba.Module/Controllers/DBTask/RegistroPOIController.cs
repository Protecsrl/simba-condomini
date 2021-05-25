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
using CAMS.Module.DBTask.POI;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using CAMS.Module.DBAngrafica;
using DevExpress.Spreadsheet;
using System.Collections;
using CAMS.Module.Classi;
using DevExpress.Persistent.BaseImpl;

#pragma warning disable CS0105 // La direttiva using per 'CAMS.Module.Classi' è già presente in questo spazio dei nomi
using CAMS.Module.Classi;
#pragma warning restore CS0105 // La direttiva using per 'CAMS.Module.Classi' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'CAMS.Module.DBAngrafica' è già presente in questo spazio dei nomi
using CAMS.Module.DBAngrafica;
#pragma warning restore CS0105 // La direttiva using per 'CAMS.Module.DBAngrafica' è già presente in questo spazio dei nomi
using CAMS.Module.DBAux;
using CAMS.Module.DBKPI.ParametriPopUp;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask.DC;
#pragma warning disable CS0105 // La direttiva using per 'CAMS.Module.DBTask.POI' è già presente in questo spazio dei nomi
using CAMS.Module.DBTask.POI;
#pragma warning restore CS0105 // La direttiva using per 'CAMS.Module.DBTask.POI' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Data.Filtering' è già presente in questo spazio dei nomi
using DevExpress.Data.Filtering;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Data.Filtering' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.Actions' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Actions;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.Actions' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.SystemModule' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.SystemModule;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.SystemModule' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.Xpo' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.Xpo;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.Xpo' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Persistent.Base' è già presente in questo spazio dei nomi
using DevExpress.Persistent.Base;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Persistent.Base' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Persistent.BaseImpl' è già presente in questo spazio dei nomi
using DevExpress.Persistent.BaseImpl;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Persistent.BaseImpl' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Spreadsheet' è già presente in questo spazio dei nomi
using DevExpress.Spreadsheet;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Spreadsheet' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Xpo' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Xpo' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections' è già presente in questo spazio dei nomi
using System.Collections;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.ComponentModel;
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi

namespace CAMS.Module.Controllers.DBTask
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegistroPOIController : ViewController
    {
        public RegistroPOIController()
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
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void saEseguiPOI_Execute(object sender, SimpleActionExecuteEventArgs e)
        {

        }

        private void saVisualizzaPOI_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                string listViewId = "ListPOI_ListView";
                if (View is ListView)
                {
                    ListView lv = (ListView)View;
                    RegistroPOI RegistroPOI = (RegistroPOI)lv.CurrentObject;

                    CollectionSource ListListPOI = (CollectionSource)
                        Application.CreateCollectionSource(xpObjectSpace, typeof(ListPOI), listViewId, true, CollectionSourceMode.Normal);
                    GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.And);

                    int oidEdificio = RegistroPOI.Immobile.Oid;
                   
                    criteriaOP2.Operands.Add(CriteriaOperator.Parse("OidEdificio = ?", oidEdificio));
                    int oidImpianto = RegistroPOI.Servizio.Oid;
                    
                    criteriaOP2.Operands.Add(CriteriaOperator.Parse("OidImpianto = ?", oidImpianto));
                    int anno = RegistroPOI.Anno;
                   
                    criteriaOP2.Operands.Add(CriteriaOperator.Parse("Anno = ?", anno));
                    //int anno = RegistroPOI.Anno;
                    //criteriaOP2.Operands.Add(CriteriaOperator.Parse("Anno = ?", anno));

                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("CodProcedura", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    ListListPOI.Sorting.Add(srtProperty);
                    ListListPOI.Criteria["Filtro_ListListPOI"] = criteriaOP2;

                    var lview = Application.CreateListView(listViewId, ListListPOI, false);

                    lview.Caption = string.Format("Visualizza POI");

                    e.ShowViewParameters.CreatedView = lview;
                    //e.ShowViewParameters.TargetWindow = TargetWindow.Current;
                    //e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Default;

                    e.ShowViewParameters.Context = TemplateContext.View;
                    e.ShowViewParameters.TargetWindow = TargetWindow.NewModalWindow; 
                    e.ShowViewParameters.Context = TemplateContext.PopupWindow;
                    DialogController dc = Application.CreateController<DialogController>();
                    e.ShowViewParameters.Controllers.Add(dc);

                }


            }
        }

        private void saSendMailPOI_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            SetMessaggioWeb("Report POI non spedito Creato! nessun destinatario.", "Azione non eseguita", InformationType.Warning);

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

        private void saSchedulaPOI_Execute(object sender, DevExpress.ExpressApp.Actions.SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                DetailView Dv = (DetailView)View;
                RegistroPOI RegistroPOI = (RegistroPOI)Dv.CurrentObject;
                if (RegistroPOI != null)
                {
                    //  chiama procedura di cambio stato in SPEDITO                          
                    using (var db = new DB())
                    {
                        if (
                            //RegistroPOI.DataConferma != null
                            RegistroPOI.DataConferma.ToString() != "01/01/0001 00:00:00"
                            )
                                {                             
                                        //Classi.SetVarSessione.OracleConnString = Connessione;
                                        string strMessaggio = 
                                            db.SechedulaPOI("Admin",
                                            RegistroPOI.Servizio.Oid,
                                            RegistroPOI.Trimestre, 
                                            RegistroPOI.DataConferma);
                             
                                }
                    }
                }
            }
        }
    
    }
}
