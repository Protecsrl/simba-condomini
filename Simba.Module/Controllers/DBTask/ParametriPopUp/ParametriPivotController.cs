using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.DBAux;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask.ParametriPopUp;
using CAMS.Module.DBTask.Vista;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Layout;
using DevExpress.ExpressApp.Model.NodeGenerators;
using DevExpress.ExpressApp.SystemModule;
using DevExpress.ExpressApp.Templates;
using DevExpress.ExpressApp.Utils;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.Controllers.DBTask.ParametriPopUp
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class ParametriPivotController : ViewController
    {
        public ParametriPivotController()
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


        // buono
        private void AcMethodCaricaPivot_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ParametriPivot ParametriPivot = ((DetailView)View).CurrentObject as ParametriPivot;
            ParametriPivot.Save();
            View.ObjectSpace.CommitChanges();
            if (!(ParametriPivot.Data_DA < ParametriPivot.Data_A && ParametriPivot.Data_DA > DateTime.MinValue))
            {
                SetMessaggioWeb("Date non Valorizzate", "Date non Valorizzate", InformationType.Warning);
            }

            if (View.ObjectSpace != null && ParametriPivot != null)
            {
                ParametriPivot.ActionMethodCarica();
            }
        }
        // buono
        private void AcMethodResetPivot_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            ParametriPivot ParametriPivot = ((DetailView)View).CurrentObject as ParametriPivot;
            //ParametriPivot.Save();
            //View.ObjectSpace.CommitChanges();            //var xpObjectSpace = Application.CreateObjectSpace();
            if (View.ObjectSpace != null && ParametriPivot != null)
            {
                ParametriPivot.ActionMethodReset();
            }

        }
        //  buono
        private void popWinMethodCreateFiltro_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //+(((e.PopupWindow)).View).SelectedObjects    Count = 3   System.Collections.IList 
            //{ System.Collections.ArrayList.ReadOnlyArrayList}
            //var xpObjectSpace = Application.CreateObjectSpace();
            if (View is DetailView)
            {
                DetailView dv = (DetailView)View;
                ParametriPivot ParPivot = (ParametriPivot)dv.CurrentObject;
                if (e.PopupWindowViewSelectedObjects.Count > 0)
                {
                    List<int> listOidCommesse = new List<int>();
                    //int[] listOidCommesse = new int[];
                    List<string> listNomeCommesse = new List<string>();
                    foreach (Contratti Comm in (((e.PopupWindow)).View).SelectedObjects)
                    {
                        listOidCommesse.Add(Comm.Oid);
                        listNomeCommesse.Add(Comm.Descrizione.ToString());
                    }

                    FilteringCriterion NuovoFiltro = View.ObjectSpace.CreateObject<FilteringCriterion>();
                    NuovoFiltro.Description = string.Format("Filtro {0} {1}", Application.Security.UserName.ToString(), DateTime.Now.ToString("MMdd H:mm:ss"));
                    NuovoFiltro.myObjectType = typeof(RdLListViewSinottico);
                    CriteriaOperator opFiltro = new InOperator("OidCommessa", listOidCommesse.ToArray());
                    NuovoFiltro.Criterion = opFiltro.ToString();
                    NuovoFiltro.DescrizioneFiltro = string.Join(" ;", listNomeCommesse.ToArray());
                    DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser user =
                        View.ObjectSpace.GetObject<DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser>
                        ((DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser)Application.Security.User);
                    NuovoFiltro.SecurityUser = user;
                    if (!string.IsNullOrEmpty(ParPivot.NuovoFiltro))  //ParPivot.NuovoFiltro)!IsNullOrEmpty(NuovoFiltro)
                        NuovoFiltro.Description = ParPivot.NuovoFiltro;

                    NuovoFiltro.Save();
                    View.ObjectSpace.CommitChanges();

                    ParPivot.DescrizioneFiltroCommesse = string.Join(" ;", listNomeCommesse.ToArray());
                    ParPivot.Save();
                    View.ObjectSpace.CommitChanges();

                    View.ObjectSpace.Refresh();
                }
                //Size(SizeAttribute.Unlimited
            }
        }


        private void popWinMethodCreateFiltro_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                Session sess = ((XPObjectSpace)xpObjectSpace).Session;

                //List<Commesse> mm = new XPQuery<Commesse>(sess).Where(w => w.Abilitato == FlgAbilitato.Si).ToList();
                //foreach (Commesse Comm in mm)
                //    Parametro.Commesse.Add(Comm);
                //Parametro.Save();
                var listViewId = "Commesse_ListView_Filtro_Pivot";
                var clTicketLv = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(Contratti), listViewId);
                var view = Application.CreateListView(listViewId, clTicketLv, true);
                e.View = view;
                e.IsSizeable = true;

            }
        }

        private void popWinMethodEditFiltro_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (View.ObjectSpace != null)
                View.ObjectSpace.CommitChanges();

            View.ObjectSpace.Refresh();
        }

        private void popWinMethodEditFiltro_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                Session sess = ((XPObjectSpace)xpObjectSpace).Session;
                var listViewId = "FilteringCriterion_ListView_Filtro_Pivot";
                var clTicketLv = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(FilteringCriterion), listViewId);

                GroupOperator criteriaOP = new GroupOperator(GroupOperatorType.And);
                CriteriaOperator opFiltro = CriteriaOperator.Parse("SecurityUser = ?", SecuritySystem.CurrentUserId);
                criteriaOP.Operands.Add(opFiltro);
                opFiltro = CriteriaOperator.Parse("myObjectType = ?", typeof(RdLListViewSinottico));   //.Where(w => w.myObjectType == typeof(RdLListViewSinottico))
                criteriaOP.Operands.Add(opFiltro);

                clTicketLv.Criteria["FilteringCriterion_ListView_Filtro_Pivot_Filtro"] = criteriaOP;

                var view = Application.CreateListView(listViewId, clTicketLv, true);
                e.View = view;
                e.IsSizeable = true;
            }
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
            //messageOptions.Type = InformationType.Error;            //messageOptions.Web.Position = InformationPosition.Bottom;            //messageOptions.Win.Type = WinMessageType.Alert;
            if (Pulsanti)
            {
                //options.OkDelegate = () =>
                //{
                //    //IObjectSpace os = View.ObjectSpace;
                //    RdL rdl = (RdL)View.CurrentObject;
                //    rdl.DataSopralluogo = DateTime.Now;
                //    rdl.DataAzioniTampone = DateTime.Now;
                //    rdl.DataInizioLavori = DateTime.Now;
                //    rdl.Save();
                //    //IObjectSpace os = Application.CreateObjectSpace(typeof(Test));
                //    //DetailView detailView = Application.CreateDetailView(os, os.FindObject<Test>(new BinaryOperator(nameof(Test.Oid), test.Oid)));
                //    //Application.ShowViewStrategy.ShowViewInPopupWindow(detailView);
                //};
            }


            Application.ShowViewStrategy.ShowMessage(options);
        }
    }
}
