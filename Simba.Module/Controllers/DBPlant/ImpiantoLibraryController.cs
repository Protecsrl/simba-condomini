using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.Controllers.DBPlant
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class ImpiantoLibraryController : ViewController
    {
        public ImpiantoLibraryController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
        }
        protected override void OnViewControlsCreated()
        {
            InserisciApparatiLibrary.Active["AttivaInserisciApparatoLibrary"] = false;
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    if (View.Id == "ImpiantoLibrary_DetailView")
                    {
                        InserisciApparatiLibrary.Active["AttivaInserisciApparatoLibrary"] = true;
                    }
                }
            }
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void InserisciApparatiLibrary_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var os = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            var lstApparatiSelezionati = ((List<ServizioLibraryAppInseribili>)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).SelectedObjects.Cast<ServizioLibraryAppInseribili>().ToList<ServizioLibraryAppInseribili>()));
            //var impiantoCurr = (ImpiantoLibraryDettaglio)View.CurrentObject;
            //var NrApparati = int.Parse(e.SelectedChoiceActionItem.Data.ToString());


            //var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<ImpiantoLibraryAppInseribili>().ToList();

            ServizioLibrary ImpiantoLibraryCorrente = (ServizioLibrary)View.CurrentObject;
            //for (var i = 0; i < lstApparatiSelezionati.Count; i++)
            //{
                foreach (ServizioLibraryAppInseribili OgniAppInseribile in lstApparatiSelezionati)
                {
                    var NuovoApparatoInLibreria = os.CreateObject<ServizioLibraryDettaglio>();

                    if (ImpiantoLibraryCorrente == null)
                    {
                        ImpiantoLibraryCorrente = os.FindObject<ServizioLibrary>
                        (new BinaryOperator(ServizioLibrary.Fields.Oid.PropertyName.ToString(), OgniAppInseribile.ImpiantoLibrary.Oid));
                    }

                    NuovoApparatoInLibreria.Sistema = os.GetObject<Sistema>(OgniAppInseribile.Sistema);
                    NuovoApparatoInLibreria.StdApparato = os.GetObject<StdAsset>(OgniAppInseribile.StdApparato);
                    NuovoApparatoInLibreria.Utente = Application.Security.UserName;
                    NuovoApparatoInLibreria.DataAggiornamento = DateTime.Now;
                    NuovoApparatoInLibreria.KDimensione = os.GetObject<KDimensione>(OgniAppInseribile.KDimensione);
                    NuovoApparatoInLibreria.Quantita = 1;

                    ImpiantoLibraryCorrente.SERVIZIOLIBRARYDETTAGLIOs.Add(NuovoApparatoInLibreria);
                }
            //}
            os.CommitChanges();
        }

        private void InserisciApparatiLibrary_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                //var ImpiantoSelezionato = (Impianto)View.CurrentObject;

                //var lstSchedeMP = new XPCollection<SchedaMp>(ImpiantoSelezionato.Session);
                //var lstDaEscludere = ImpiantoSelezionato.APPARATOes.Select(ld => ld.StdApparato);

                //var tmpLst = lstSchedeMP.Where(smp => smp.Sistema == ImpiantoSelezionato.Sistema)
                //    .Select(smp => smp.Eqstd).Distinct().Where(std => !lstDaEscludere.Contains(std)).ToList();
                //var crtapp = string.Empty;
                //foreach (StdApparato std in tmpLst)
                //{
                //    crtapp += std.Oid + ",";
                //}
                ServizioLibrary il = (ServizioLibrary)View.CurrentObject;
                il.Save();
                View.ObjectSpace.CommitChanges();
                xpObjectSpace.Refresh();
               // xpObjectSpace.ReloadObject(il);
                var crTApparati = string.Format("ImpiantoLibrary in ({0})", ((ServizioLibrary)View.CurrentObject).Oid);
                string listViewId = "ImpiantoLibrary_ImpiantoLibraryAppInseribilis_ListView";
                var ApparatiSelezionabili = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(ServizioLibraryAppInseribili), listViewId);
                ApparatiSelezionabili.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(crTApparati);
                var view = Application.CreateListView(listViewId, ApparatiSelezionabili, false);
                e.View = view;
                e.IsSizeable = true;
            }
        }
    }
}
