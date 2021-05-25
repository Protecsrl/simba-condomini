using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.DBPlant;

using CAMS.Module.DBALibrary;


using CAMS.Module.Classi;
using System.Diagnostics;


namespace CAMS.Module.Controllers
{
    public partial class ImpiantoLibraryController : ViewController
    {
        private const string ImpiantoLibraryDettaglioAssocia_DetailView = "ImpiantoLibraryDettaglioAssocia_DetailView";

        public ImpiantoLibraryController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();

            if (View.Id.Contains("ImpiantoLibrary_IMPIANTOLIBRARYDETTAGLIOs_ListView"))
            {
                for (var i = 2; i < 10; i++)
                {
                    ClonaApparatoInLibreriaImpianti.Items.Add((new ChoiceActionItem() { Id = i.ToString(), Caption = i.ToString() + " Copie", Data = i }));
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            //Debug.WriteLine(View.Id.ToString());
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
        //private void ImpiantoLibraryController_ViewControlsCreated(object sender, EventArgs e)
        //{
        //}

        private ServizioLibraryDettaglio NuovoImpiantoLibraryDettaglio;

        private void ImpiantoLibraryDettaglioAssociaImpianto_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                NuovoImpiantoLibraryDettaglio = xpObjectSpace.CreateObject<ServizioLibraryDettaglio>();
                var view = Application.CreateDetailView(xpObjectSpace, ImpiantoLibraryDettaglioAssocia_DetailView, false, NuovoImpiantoLibraryDettaglio);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }
        private void ImpiantoLibraryDettaglioAssociaImpianto_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            if (NuovoImpiantoLibraryDettaglio.Quantita < 1)
            {
                throw new Exception(@"La quantità deve essere maggiore di 0");
            }

            var xpObjectSpace = Application.CreateObjectSpace();

            var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<ServizioLibraryDettaglio>().ToList();
            ServizioLibrary ImpiantoLibraryCorrente = null;
            Sistema sistema = null;

            var lstStdApparato = xpObjectSpace.GetObjects<StdAsset>();

            foreach (ServizioLibraryDettaglio apparato in lstApparatiSelezionati)
            {
                var NuovoApparto = xpObjectSpace.CreateObject<ServizioLibraryDettaglio>();

                if (ImpiantoLibraryCorrente == null)
                {
                    ImpiantoLibraryCorrente = xpObjectSpace.FindObject<ServizioLibrary>
                        (new BinaryOperator(ServizioLibrary.Fields.Oid.PropertyName.ToString(), apparato.OidImpiantoLibrary));
                }
                if (sistema == null)
                {
                    sistema = xpObjectSpace.FindObject<Sistema>
                        (new BinaryOperator(Sistema.Fields.Oid.PropertyName.ToString(), apparato.Sistema.Oid));
                }
                NuovoApparto.Sistema = sistema;
                NuovoApparto.StdApparato = lstStdApparato.FirstOrDefault(app => app.Oid == apparato.StdApparato.Oid);
                NuovoApparto.KDimensione = NuovoImpiantoLibraryDettaglio.KDimensione;
                NuovoApparto.Quantita = NuovoImpiantoLibraryDettaglio.Quantita;

                ImpiantoLibraryCorrente.SERVIZIOLIBRARYDETTAGLIOs.Add(NuovoApparto);
            }

            xpObjectSpace.CommitChanges();

            RefreshDati();
        }

        private void ClonaApparatoInLibreriaImpianti_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var NrApparati = int.Parse(e.SelectedChoiceActionItem.Data.ToString());

            var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<ServizioLibraryDettaglio>().ToList();
            ServizioLibrary ImpiantoLibraryCorrente = null;

            for (var i = 0; i < NrApparati; i++)
            {
                foreach (ServizioLibraryDettaglio OgniAppdaCopiare in lstApparatiSelezionati)
                {
                    var NuovoApparatoInLibreria = ObjectSpace.CreateObject<ServizioLibraryDettaglio>();

                    if (ImpiantoLibraryCorrente == null)
                    {
                        ImpiantoLibraryCorrente = ObjectSpace.FindObject<ServizioLibrary>
                            (new BinaryOperator(ServizioLibrary.Fields.Oid.PropertyName.ToString(), OgniAppdaCopiare.ImpiantoLibrary.Oid));
                    }

                    NuovoApparatoInLibreria.Sistema = OgniAppdaCopiare.Sistema;
                    NuovoApparatoInLibreria.StdApparato = OgniAppdaCopiare.StdApparato;
                    NuovoApparatoInLibreria.Utente = Application.Security.UserName;
                    NuovoApparatoInLibreria.DataAggiornamento = DateTime.Now;
                    NuovoApparatoInLibreria.KDimensione = OgniAppdaCopiare.KDimensione;
                    NuovoApparatoInLibreria.Quantita = 1;

                    ImpiantoLibraryCorrente.SERVIZIOLIBRARYDETTAGLIOs.Add(NuovoApparatoInLibreria);
                }
            }
            ObjectSpace.CommitChanges();
            RefreshDati();
        }

        /// <summary>        ///  disabilitato
        /// </summary>
        private void ImpLibraryDettAssociaImpNr_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            int.Parse(e.SelectedChoiceActionItem.Data.ToString());


            var xpObjectSpace = Application.CreateObjectSpace();

            var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<ServizioLibraryDettaglio>().ToList();
            ServizioLibrary ImpiantoLibraryCorrente = null;
            Sistema sistema = null;

            var lstStdApparato = xpObjectSpace.GetObjects<StdAsset>();
            foreach (ServizioLibraryDettaglio apparato in lstApparatiSelezionati)
            {
                var NuovoAppartoInLibreria = xpObjectSpace.CreateObject<ServizioLibraryDettaglio>();

                if (ImpiantoLibraryCorrente == null)
                {
                    ImpiantoLibraryCorrente = xpObjectSpace.FindObject<ServizioLibrary>
                        (new BinaryOperator(ServizioLibrary.Fields.Oid.PropertyName.ToString(), apparato.OidImpiantoLibrary));
                }
                if (sistema == null)
                {
                    sistema = xpObjectSpace.FindObject<Sistema>
                        (new BinaryOperator(Sistema.Fields.Oid.PropertyName.ToString(), apparato.Sistema.Oid));
                }
                NuovoAppartoInLibreria.Sistema = sistema;
                NuovoAppartoInLibreria.StdApparato = lstStdApparato.FirstOrDefault(app => app.Oid == apparato.StdApparato.Oid);
                var Criteria = string.Format("StandardApparato.Oid = {0} And [Default] = '{1}'", apparato.StdApparato.Oid, KDefault.Si);
                var objKDimensione = xpObjectSpace.FindObject<KDimensione>(CriteriaOperator.Parse(Criteria));

                NuovoAppartoInLibreria.KDimensione = objKDimensione;
                NuovoAppartoInLibreria.Quantita = 1;

                ImpiantoLibraryCorrente.SERVIZIOLIBRARYDETTAGLIOs.Add(NuovoAppartoInLibreria);
            }

            xpObjectSpace.CommitChanges();

            RefreshDati();
        }


        private void RefreshDati()
        {
            try
            {
                if (View is DetailView)
                {
                    View.ObjectSpace.ReloadObject(View.CurrentObject);
                }
                else
                {
                    (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
                }
                View.ObjectSpace.Refresh();
                View.Refresh();
                base.Frame.View.Refresh();
            }
            catch
            {
            }
        }
    }
}
