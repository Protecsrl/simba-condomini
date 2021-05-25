using CAMS.Module.Classi;

using CAMS.Module.DBALibrary;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System.Linq;

namespace CAMS.Module.Controllers.DBALibrary
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class StdApparatoController : ViewController
    {
        public StdApparatoController()
        {
            InitializeComponent();
            RegisterActions(components);
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
         
            if (View is DetailView)
            {
                var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                if (xpObjectSpace != null)
                {
                    var StdApparato = (StdAsset)View.CurrentObject;
                    if (StdApparato.Oid == -1)
                    {
                        StdApparato.KCondizione = xpObjectSpace.FindObject<KCondizione>(CriteriaOperator.Parse("Default == 'Si'")) != null ? xpObjectSpace.FindObject<KCondizione>(CriteriaOperator.Parse("Default == 'Si'")) : null;//.Where(d => d.Default == KDefault.Si);
                        StdApparato.KUtenza = xpObjectSpace.FindObject<KUtenza>(CriteriaOperator.Parse("Default == 'Si'")) != null ? xpObjectSpace.FindObject<KUtenza>(CriteriaOperator.Parse("Default == 'Si'")) : null;
                        StdApparato.KUbicazione = xpObjectSpace.FindObject<KUbicazione>(CriteriaOperator.Parse("Default == 'Si'")) != null ? xpObjectSpace.FindObject<KUbicazione>(CriteriaOperator.Parse("Default == 'Si'")) : null;
                        StdApparato.KGuasto = xpObjectSpace.FindObject<KGuasto>(CriteriaOperator.Parse("Default == 'Si'")) != null ? xpObjectSpace.FindObject<KGuasto>(CriteriaOperator.Parse("Default == 'Si'")) : null;
                        StdApparato.KOttimizzazione = xpObjectSpace.FindObject<KOttimizzazione>(CriteriaOperator.Parse("Default == 'Si'")) != null ? xpObjectSpace.FindObject<KOttimizzazione>(CriteriaOperator.Parse("Default == 'Si'")) : null;
                        StdApparato.KTrasferimento = xpObjectSpace.FindObject<KTrasferimento>(CriteriaOperator.Parse("Default == 'Si'")) != null ? xpObjectSpace.FindObject<KTrasferimento>(CriteriaOperator.Parse("Default == 'Si'")) : null;



                        if (StdApparato.KDimensiones != null)
                        {    // Retrieve all Accessory objects expre
                            int clkD1 = Sess.QueryInTransaction<KDimensione>()
                                    .Where(d => d.StandardApparato == StdApparato && d.Default == KDefault.Si).ToList().Count();
                            int clkD = Sess.Query<KDimensione>()
                                        .Where(d => d.StandardApparato == StdApparato && d.Default == KDefault.Si).ToList().Count();
                            if (clkD < 1 || clkD1 < 1)
                            {

                                StdApparato.KDimensiones.Add(new KDimensione(Sess)
                                {
                                    Descrizione = "Valore di Default",
                                    Default = KDefault.Si,
                                    StandardApparato = StdApparato,
                                    Valore = 1
                                });
                              
                            }
                        }

                    }
                }
            }
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
