using CAMS.Module.DBPlanner;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Linq;

namespace CAMS.Module.Controllers.DBPlanner
{
    // For more typical usage scenarios, be sure to check out http://documentation.devexpress.com/#Xaf/clsDevExpressExpressAppViewControllertopic.
    public partial class MPDataInizialeController : ViewController
    {
        public MPDataInizialeController()
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
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }

        private void SelDataIniziale_Execute(object sender, ParametrizedActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                {
                    if (View.Id == "MPDataIniziale_DetailView_Pianifica")
                    {
                        if (e.ParameterCurrentValue == null)
                        {
                            var dv = View as DetailView;
                            if (dv.ViewEditMode == ViewEditMode.Edit)
                            {
                                MPDataIniziale df = (MPDataIniziale)dv.CurrentObject;
                                df.Save();
                            }
                        }
                        View.ObjectSpace.CommitChanges();
                    }
                }

                if (View is ListView)
                {
                    if (View.Id == "RegPianificazioneMP_MPDataIniziales_ListView")
                    {
                        ListView lv = View as ListView;
                        DetailView dv = (DetailView)View.ObjectSpace.Owner;
                        if (dv.ViewEditMode == ViewEditMode.Edit)
                        {
                            RegPianificazioneMP RegPianificazioneMP = (RegPianificazioneMP)dv.CurrentObject;
                            int anno = RegPianificazioneMP.AnnoSchedulazione.Anno;
                            var paramValue = (DateTime)e.ParameterCurrentValue;
                            if (anno == paramValue.Year)
                            {
                                var lstMPDataInizialeSel = (((ListView)View).Editor).GetSelectedObjects().Cast<MPDataIniziale>().ToList<MPDataIniziale>();
                                foreach (MPDataIniziale DataIniziale in lstMPDataInizialeSel)
                                {
                                    MPDataIniziale MPDataIni = View.ObjectSpace.GetObject<MPDataIniziale>(DataIniziale);
                                  //  MPDataIni.DataUltimaAttivita = paramValue;

                                    foreach (MPDataInizialeDettaglio DataIniDettaglio in MPDataIni.MPDataInizialeDettaglios)
                                    {
                                        DataIniDettaglio.Data = paramValue;
                                        DataIniDettaglio.Save();
                                    }
                                    MPDataIni.Save();
                                }

                            }
                        }
                    }
                    View.ObjectSpace.CommitChanges();
                }
            }
        }


    }
}

