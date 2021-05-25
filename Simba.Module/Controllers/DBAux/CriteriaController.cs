using CAMS.Module.DBAux;
using CAMS.Module.DBTask.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Diagnostics;
using System.Linq;



//using DevExpress.ExpressApp.Chart.Web;
//using DevExpress.ExpressApp.PivotGrid.Web;

//using DevExpress.Web.ASPxPivotGrid;


namespace CAMS.Module.Controllers.DBAux
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    //   public partial class CriteriaController : ViewController
    public partial class CriteriaController : ObjectViewController
    {
        private SingleChoiceAction filteringCriterionAction;

        public CriteriaController()
        {
            //   InitializeComponent();
            /// Target required Views (via the TargetXXX properties) and create their Actions.   PredefinedCategory.Filters    Appearance
            /// AZIONE DEL COMANDO FILTRO PERSONALIZZATO
            TargetViewId = "RdLListViewGuasto_ListView_Completamento;RdLListViewGuasto_ListView_inElaborazioneSLA;RdLListViewGuasto_ListView_SLARipristino;RdLListViewGuasto_ListView;RdLListViewGuasto_ListView_SLASopralluogo;Edificio_ListView;ApparatoListView_ListView;Impianto_ListView;RdLListView_ListView;RdLListView_ListView_CO;RdLListView_ListView_StampaMP;Commesse_ListView_Gestione;Richiedente_ListView;KPI_AvanzamentoLavoriLT_ListView";
            filteringCriterionAction = new SingleChoiceAction(
            this, "Filtri Personalizzati", PredefinedCategory.Appearance);
            filteringCriterionAction.Execute += new DevExpress.ExpressApp.Actions.SingleChoiceActionExecuteEventHandler(this.FilteringCriterionAction_Execute);

            TargetViewType = ViewType.ListView;
        }
        private int GetCountObj(XPQuery<FilteringCriterion> qFilteringCriterion)
        {
            //  @@@@@@@@@@@@   da fare   da controllare   @@@@@@@@@@@@@@@@@@@@ò
            int ValoreRetur = 0;
            try
            {
                GroupOperator criteriaOP2 = new GroupOperator(GroupOperatorType.And);
                // caso non Administrator
                DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser u = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User;
                int IsAdmin = u.Roles.Where(w => w.IsAdministrative == true).Count();
                if (IsAdmin == 0)
                {
                    CriteriaOperator opCriteria = new OperandProperty("SecurityUser.Oid");
                    CriteriaOperator opValore = new OperandValue(u.Oid);
                    BinaryOperator criteria = new BinaryOperator(opCriteria, opValore, BinaryOperatorType.Equal);

                }

                // tutti i casi utente
                CriteriaOperator op = CriteriaOperator.Parse("ToStr([myObjectType]) == ?", View.ObjectTypeInfo.Type.FullName);
                criteriaOP2.Operands.Add(op);
                Session Sess = ((XPObjectSpace)ObjectSpace).Session;

                XPView xpview = new XPView(Sess, typeof(FilteringCriterion));
                xpview.Properties.AddRange(new ViewProperty[] {
                 new ViewProperty("Oid", SortDirection.None, "ToStr([Oid])", true, true),
                new ViewProperty("Criterio", SortDirection.None, "ToStr([Criterion])", true, true),
                new ViewProperty("Descrizione", SortDirection.Ascending, "[Description]", true, true),
                });

                xpview.Criteria = criteriaOP2;
                foreach (ViewRecord r in xpview)
                {
                    ValoreRetur = 1;
                    filteringCriterionAction.Items.Add(new ChoiceActionItem(r["Oid"].ToString(), r["Descrizione"].ToString(), r["Criterio"]));
                    //DescrizioneFiltroCommesse = DescrizioneFiltroCommesse + r[0] + "; ";
                }
               // if (ValoreRetur = 1)
              //  {
                    filteringCriterionAction.Items.Add(new ChoiceActionItem("All", null));
                    filteringCriterionAction.Items.Add(new ChoiceActionItem("*Crea Nuovo Filtro", null));
               // }
                return ValoreRetur;
            }
            catch (Exception ex)
            {
                Debug.Write(ex.Message.ToString());
                return 0;
            }
            //return qFilteringCriterion
            //                .Where(w => w.myObjectType == View.ObjectTypeInfo.Type)
            //                .Where(w => w.SecurityUser == Application.Security).Count();
        }
       
        int IsAdmin = 0;
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            filteringCriterionAction.Items.Clear();
            filteringCriterionAction.Active.SetItemValue("Active", false);
            DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser u = (DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User;
            int IsAdmin = u.Roles.Where(w => w.IsAdministrative == true).Count();

            if (IsAdmin == 0 && View.ObjectTypeInfo.Type == typeof(FilteringCriterion)) //  if (!CAMS.Module.Classi.SetVarSessione.IsAdminRuolo && View.ObjectTypeInfo.Type == typeof(FilteringCriterion))
            {
                //CriteriaOperator op = CriteriaOperator.Parse("SecurityUser.Oid == ?", Application.Security.UserId);
                CriteriaOperator opCriteria = new OperandProperty("SecurityUser.Oid");
                CriteriaOperator opValore = new OperandValue(u.Oid);
                BinaryOperator criteria = new BinaryOperator(opCriteria, opValore, BinaryOperatorType.Equal);

                ((ListView)View).CollectionSource.BeginUpdateCriteria();
                ((ListView)View).CollectionSource.Criteria.Clear();
                ((ListView)View).CollectionSource.Criteria["Filtro_FilytriPersonalizzati"] = criteria;
                ((ListView)View).CollectionSource.EndUpdateCriteria();
            }
            //-------------------------------------------------
            string VistaListView = View.Id;
            switch (VistaListView)
            {
                case "Edificio_ListView":
                case "ApparatoListView_ListView":
                case "Impianto_ListView":
                case "RdLListView_ListView":
                case "RdLListView_ListView_CO":
                case "RdLListView_ListView_StampaMP":
                case "Commesse_ListView_Gestione":
                case "Richiedente_ListView":
                case "KPI_AvanzamentoLavoriLT_ListView":
                case "KPI_AvanzamentoLT_Fasce_ListView":
                case "RdLListViewSinottico_ListView_PivotSinottico":
                case "SchedaMp_ListView_Corto":
                case "ContrattiConsipRegistro_ListView":
                case "NotificaRdL_ListView":
                case "RdLListViewGuasto_ListView_Completamento":
                case "RdLListViewGuasto_ListView_inElaborazioneSLA":
                case "RdLListViewGuasto_ListView_SLARipristino":
                case "RdLListViewGuasto_ListView":
                    filteringCriterionAction.Active.SetItemValue("Active", false);
                    Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                    //int cc= GetCountObj(new XPQuery<ReportExcel>(Sess));
                    if (GetCountObj(new XPQuery<FilteringCriterion>(Sess)) > 0)
                    {
                        //CaricaFiltro(new XPQuery<FilteringCriterion>(Sess));
                        //CaricaFiltro();
                        filteringCriterionAction.Active.SetItemValue("Active", true);
                    }
                    break;
            }

        }


        private void FilteringCriterionAction_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (e.SelectedChoiceActionItem.Caption.Equals("*Crea Nuovo Filtro"))
            {
                var xpObjectSpace = Application.CreateObjectSpace();
                var NuovoFiltro = xpObjectSpace.CreateObject<FilteringCriterion>();

                NuovoFiltro.Description = string.Format("Filtro {0} {1}", Application.Security.UserName.ToString(), DateTime.Now.ToString("MMdd H:mm:ss"));
                NuovoFiltro.myObjectType = View.ObjectTypeInfo.Type;
                string Filtro = ((ListView)View).Model.Filter.ToString();
                NuovoFiltro.Criterion = Filtro;
                DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                    xpObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                    ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);
                NuovoFiltro.SecurityUser = user;

                DetailView view = Application.CreateDetailView(xpObjectSpace, "FilteringCriterion_DetailView_Nuovo", true, NuovoFiltro);
                view.Caption = string.Format("Nuovo Filtro da Creare");
                view.ViewEditMode = ViewEditMode.Edit;

                e.ShowViewParameters.CreatedView = view;
                e.ShowViewParameters.Context = TemplateContext.View;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                e.ShowViewParameters.NewWindowTarget = NewWindowTarget.Separate;


            }
            else
            {
                ((ListView)View).CollectionSource.BeginUpdateCriteria();
                ((ListView)View).CollectionSource.Criteria.Clear();
                ((ListView)View).CollectionSource.Criteria[e.SelectedChoiceActionItem.Caption] =
                   CriteriaEditorHelper.GetCriteriaOperator(
                   e.SelectedChoiceActionItem.Data as string, View.ObjectTypeInfo.Type, ObjectSpace);
                ((ListView)View).CollectionSource.EndUpdateCriteria();
            }
        }

        private void CaricaFiltro(XPQuery<FilteringCriterion> qFilteringCriterion)
        {
            filteringCriterionAction.Items.Clear();
            //CriteriaOperator op = CriteriaOperator.Parse(
            //                      string.Format("SecurityUser.UserName == '{0}'", Application.Security.UserName));  //new BinaryOperator("SecurityUser", user, BinaryOperatorType.Equal);
            //foreach (FilteringCriterion criterion in ObjectSpace.GetObjects<FilteringCriterion>(op))
            //    if (criterion.ObjectType.IsAssignableFrom(View.ObjectTypeInfo.Type))
            //    {
            //        filteringCriterionAction.Items.Add(
            //            new ChoiceActionItem(criterion.Description, criterion.Criterion));
            //    }
            var lista = qFilteringCriterion
                    .Where(w => w.myObjectType == View.ObjectTypeInfo.Type)
                    .Where(w => w.SecurityUser.UserName == Application.Security.UserName)
                   .Select(s => new { Criterio = s.Criterion.ToString(), Descrizione = s.Description });

            foreach (var criterion in lista)
            {

                filteringCriterionAction.Items.Add(new ChoiceActionItem(criterion.Descrizione, criterion.Criterio));
            }

            if (filteringCriterionAction.Items.Count > 0)
            {

                filteringCriterionAction.Items.Add(new ChoiceActionItem("All", null));
                filteringCriterionAction.Items.Add(new ChoiceActionItem("*Crea Nuovo Filtro", null));
            }
        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            filteringCriterionAction.Active.SetItemValue("Active", false);
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
    }
}
