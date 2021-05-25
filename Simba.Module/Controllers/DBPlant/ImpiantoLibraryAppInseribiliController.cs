using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using CAMS.Module.DBPlant;


namespace CAMS.Module.Controllers.DBPlant
{
    public partial class ImpiantoLibraryAppInseribiliController : ViewController
    {
        public ImpiantoLibraryAppInseribiliController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (Frame != null && Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>() != null)
            {
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
            }
            if (View.Id.Contains("ImpiantoLibrary_ImpiantoLibraryAppInseribilis_ListView"))
            {
                for (var i = 2; i < 10; i++)
                {
                    InsApparatiInLibraryImp.Items.Add((new ChoiceActionItem() { Id = i.ToString(), Caption = i.ToString() + " Copie", Data = i }));
                }
            }
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void InsApparatiInLibraryImp_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var NrApparati = int.Parse(e.SelectedChoiceActionItem.Data.ToString());


            var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<ServizioLibraryAppInseribili>().ToList();
            ServizioLibrary ServizioLibraryCorrente = null;
            for (var i = 0; i < NrApparati; i++)
            {
                foreach (ServizioLibraryAppInseribili OgniAppInseribile in lstApparatiSelezionati)
                {
                    var NuovoApparatoInLibreria = ObjectSpace.CreateObject<ServizioLibraryDettaglio>();

                    if (ServizioLibraryCorrente == null)
                    {
                        ServizioLibraryCorrente = ObjectSpace.FindObject<ServizioLibrary>
                        (new BinaryOperator(ServizioLibrary.Fields.Oid.PropertyName.ToString(), OgniAppInseribile.ImpiantoLibrary.Oid));
                    }

                    NuovoApparatoInLibreria.Sistema = OgniAppInseribile.Sistema;
                    NuovoApparatoInLibreria.StdApparato = OgniAppInseribile.StdApparato;
                    NuovoApparatoInLibreria.Utente = Application.Security.UserName;
                    NuovoApparatoInLibreria.DataAggiornamento = DateTime.Now;
                    NuovoApparatoInLibreria.KDimensione = OgniAppInseribile.KDimensione;
                    NuovoApparatoInLibreria.Quantita = 1;

                    ServizioLibraryCorrente.SERVIZIOLIBRARYDETTAGLIOs.Add(NuovoApparatoInLibreria);
                }
            }
            ObjectSpace.CommitChanges();
            e.SelectedChoiceActionItem.DataItems.Clear();
            e.SelectedChoiceActionItem.BeginGroup = true;
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
