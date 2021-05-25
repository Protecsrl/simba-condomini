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
using CAMS.Module.Costi;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Xpo;

namespace CAMS.Module.Controllers.Costi
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegistroCostiController : ViewController
    {
        public RegistroCostiController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            //if (View is DetailView)
            //{
            //    var dv = View as DetailView;
            //    RegistroCosti curObj = (RegistroCosti)dv.CurrentObject;
            //    TipologiaCosto curObjTipologiaCosto = curObj.TipologiaCosto;
            //    double Fondo = curObjTipologiaCosto.TotaleFondo;

            //    IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            //    Session Sess = ((XPObjectSpace)ObjectSpace).Session;

            //    var ListCosti = xpObjectSpace.GetObjects<RegistroCostiDettaglio>().Where(w => w.RegistroCosti.TipologiaCosto == curObjTipologiaCosto)
            //                                                               .Select(s => new { s.ImpManodopera, s.ImpMateriale }).ToList();
            //    double CostoManodopera = ListCosti.Sum(s => s.ImpManodopera);
            //    double CostoMateriali = ListCosti.Sum(s => s.ImpMateriale);
            //    double res = Fondo - CostoMateriali - CostoManodopera;
            //    curObj.RisiduoFondo = res;
            //}
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

        private void saGetResidioFondo_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            
           // var dv = View as DetailView;
           // RegistroCosti curObj = (RegistroCosti)dv.CurrentObject;
           // TipologiaCosto curObjTipologiaCosto = curObj.TipologiaCosto;
           // double Fondo = curObjTipologiaCosto.TotaleFondo;

           //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
           // Session Sess = ((XPObjectSpace)ObjectSpace).Session;

           // var ListCosti = xpObjectSpace.GetObjects<RegistroCostiDettaglio>().Where(w => w.RegistroCosti.TipologiaCosto == curObjTipologiaCosto)
           //                                                            .Select(s => new { s.ImpManodopera, s.ImpMateriale }).ToList();
           // double CostoManodopera = ListCosti.Sum(s => s.ImpManodopera);
           // double CostoMateriali = ListCosti.Sum(s => s.ImpMateriale);
           // double res = Fondo - CostoMateriali - CostoManodopera;
           // curObj.RisiduoFondo = res;

        }
    }
}
