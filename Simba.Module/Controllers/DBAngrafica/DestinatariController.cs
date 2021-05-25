using System;
using System.Linq;
using DevExpress.ExpressApp;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.DBAngrafica.ParametriPopUp;
using CAMS.Module.DBAngrafica;

namespace CAMS.Module.Controllers.DBAngrafica
{
    public partial class DestinatariController : ViewController
    {
        public DestinatariController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
         
            base.OnActivated();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }

        private void DestinatariClonaDestinatario_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                var NuovoDes = xpObjectSpace.CreateObject<NuovoDestinatario>();
                var DestinatarioSelezionato = xpObjectSpace.GetObject((((ListView)View).Editor).GetSelectedObjects().Cast<Destinatari>().First());

                NuovoDes.Nome = DestinatarioSelezionato.Nome;
                NuovoDes.Cognome = DestinatarioSelezionato.Cognome;
                NuovoDes.Email = DestinatarioSelezionato.Email;
                NuovoDes.NumeroCopie = 0;
                NuovoDes.VecchioDestinatario = DestinatarioSelezionato;

                var view = Application.CreateDetailView(xpObjectSpace, "NuovoDestinatario_DetailView", true, NuovoDes);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void DestinatariClonaDestinatario_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var NuovoIm = ((NuovoDestinatario)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));


            var db = new Classi.DB();
            db.CloneDestinatario(Classi.SetVarSessione.CorrenteUser, NuovoIm.VecchioDestinatario.Oid, NuovoIm.Email, NuovoIm.Nome, NuovoIm.Cognome);
            db.Dispose();

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
            }
            catch
            {
            }
        }
    }
}
