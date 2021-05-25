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
using CAMS.Module.DBMisure;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;

namespace CAMS.Module.Controllers.Misure
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegMisureController : ViewController
    {
        public RegMisureController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            if (View is DetailView)
                saPredisponiMisure.Active.SetItemValue("Active", ((DetailView)View).ViewEditMode == ViewEditMode.Edit);
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

        private void saPredisponiMisure_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                RegMisure rm = ((RegMisure)View.CurrentObject);
                //var ms = ;
                foreach (MasterDettaglio item in rm.Master.MasterDettaglios)
                {
                    //int conta = rm.RegMisureDettaglios.Where(w => w.MasterDettaglio == item).Count();
                    int conta = rm.RegMisureDettaglios.Where(w => w.Asset == item.Asset && w.Valore > 0).Count();
                    if (conta == 0)
                    {
                        rm.RegMisureDettaglios.Add(new RegMisureDettaglio(Sess)
                                  {
                                      Asset = item.Asset,
                                      //Immobile = item.Immobile,
                                      //Impianto = item.Impianto,
                                      //StdApparato = item.Apparato.StdApparato,
                                      DataMisura = rm.DataInserimento,
                                      //MasterDettaglio = item
                                  });
                    }
                }
            }
        }
    }
}
