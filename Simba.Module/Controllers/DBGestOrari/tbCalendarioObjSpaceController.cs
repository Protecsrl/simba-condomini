using CAMS.Module.DBGestOrari;
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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace CAMS.Module.Controllers.DBGestOrari
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class tbCalendarioObjSpaceController : ViewController
    {
        private DateTime Adesso = DateTime.Now;

        bool suppressOnChanged = false;
        bool suppressOnCommitting = false;
        bool suppressOnCommitted = false;

        bool IsNuovaRdL = false;
        bool CambiataRisorsa = false;

        public tbCalendarioObjSpaceController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //View.ObjectSpace.ObjectChanged += ObjectSpace_ObjectChanged;
            //View.ObjectSpace.Committing += ObjectSpace_Committing;
            //View.ObjectSpace.Committed += ObjectSpace_Committed;
            //setLog(SecuritySystem.CurrentUserName, "A inizio  di  OnActivated:");
            //ObjectSpace.ObjectDeleted += ObjectSpace_ObjectDeleted;
            //ObjectSpace.Committing += ObjectSpace_Committing;
            //ObjectSpace.Committed += ObjectSpace_Committed;
            //ObjectSpace.Reloaded += ObjectSpace_Reloaded;
            suppressOnChanged = false;
            suppressOnCommitted = false;
            suppressOnCommitting = false;
            //Adesso = SetVarSessione.dataAdessoDebug;
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







    }
}
