using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Costi;
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
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;

namespace CAMS.Module.Controllers.Costi
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegistroLavoriPreventiviController : ViewController
    {
        public RegistroLavoriPreventiviController()
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

        private void pupWinApprovazioneClienteP_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {

        }

        private void pupWinApprovazioneClienteP_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            RegistroLavoriPreventivi RegLavCons = ((ListView)View).CurrentObject as RegistroLavoriPreventivi;
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null && RegLavCons != null)
            {
                var RegAPPROLavCons = xpObjectSpace.CreateObject<RegistroApprovazionePreventiviLavori>();
                RegAPPROLavCons.RegistroLavoriPreventivi = xpObjectSpace.GetObjectByKey<RegistroLavoriPreventivi>(RegLavCons.Oid);
               
                if (RegLavCons.InsDocum != null)
                    RegAPPROLavCons.DocApprovato = xpObjectSpace.GetObjectByKey<FileData>(RegLavCons.InsDocum.Oid);
                
                RegAPPROLavCons.ImpMateriale = RegLavCons.ImpMateriale;
                RegAPPROLavCons.ImpManodopera = RegLavCons.ImpManodopera;
                RegAPPROLavCons.DataApprovazione = DateTime.Now;
                RegAPPROLavCons.UtenteApprovazione = SecuritySystem.CurrentUserName;
                var view = Application.CreateDetailView(xpObjectSpace, "RegistroApprovazionePreventiviLavori_DetailView", true, RegAPPROLavCons);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }
    }
}
