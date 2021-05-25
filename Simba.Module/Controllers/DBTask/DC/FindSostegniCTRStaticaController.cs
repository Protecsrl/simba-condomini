using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using CAMS.Module.DBMaps.DC;
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
using CAMS.Module.Classi;
using CAMS.Module.DBAgenda;
using CAMS.Module.DBTask;
using CAMS.Module.DBTask.DC;
using CAMS.Module.DBTask.Vista;

using CAMS.Module.DBPlanner;
using DevExpress.ExpressApp.Xpo;

namespace CAMS.Module.Controllers.DBTask.DC
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class FindSostegniCTRStaticaController : ViewController
    {
        public FindSostegniCTRStaticaController()
        {
            InitializeComponent();
            // Target required Views (via the TargetXXX properties) and create their Actions.
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            // Perform various tasks depending on the target View.
            // reset variabili report
            //Rdlcodici = string.Empty;
            //objects.Clear();
            Application.ObjectSpaceCreated += Application_NonPersistentObjectSpace;
            //acVisualizzaDettagliRdLListViev.Items.Clear();
        }
        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();
            // Access and customize the target View control.
        }
        protected override void OnDeactivated()
        {
            // Unsubscribe from previously subscribed events and release other references and resources.
            try
            {
                Application.ObjectSpaceCreated -= Application_NonPersistentObjectSpace;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomProcessSelectedItem -= LViewController_CustomDetailView;
                //Frame.GetController<ListViewProcessCurrentObjectController>().CustomizeShowViewParameters -= LViewController_CustomizeShowViewParameters;
            }
            catch { }
            // Unsubscribe from previously subscribed events and release other references and resources.
            base.OnDeactivated();
        }
        private void Application_NonPersistentObjectSpace(Object sender, ObjectSpaceCreatedEventArgs e)
        {
            if (View.Id.Contains("FindSostegniCTRStatica_ListView"))
            {
                if (e.ObjectSpace is NonPersistentObjectSpace)
                {
                    ((NonPersistentObjectSpace)e.ObjectSpace).ObjectsGetting += DCSostegniCTRStatica_objectSpace_ObjectsGetting;
                }
            }
        }

        private static int OidCommessa = 0;
        private static int anno = DateTime.Now.Year;
        private static int[] stati = new[] { 0, 1, 2, 3 };
        private void saFindSostegniCTRStatica_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            if (View is DetailView)
            {
                bool SpediscieMail = false;
                string RisorseAssociate = string.Empty;
                //var xpObjectSpace = View is DevExpress.ExpressApp.ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                IObjectSpace xpObjectSpace = View.ObjectSpace;
                FindSostegniCTRStatica current = View.CurrentObject as FindSostegniCTRStatica;
                if (View is DetailView)
                {
                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(SostegniCTRStatica));
                    //objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

                    CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(SostegniCTRStatica),
                                                                                "SostegniCTRStatica_ListView");
                    //  filtro
                    DetailView dv = (DetailView)View;
                    RdL rdl = (RdL)dv.CurrentObject;
                    var ParCriteria = string.Empty;

                    ListView lvk = Application.CreateListView("SostegniCTRStatica_ListView", DCRisorseTeamRdL_Lookup, true);
                    var dc = Application.CreateController<DialogController>();
                    //e..SaveOnAccept = false;
                    //e.View = lvk;

                }

                    //}
                }

        }


        //private static BindingList<SostegniCTRStatica> objects = new BindingList<SostegniCTRStatica>();
        void DCSostegniCTRStatica_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            if (e.ObjectType.FullName == "CAMS.Module.DBMaps.DC.SostegniCTRStatica")
            {
                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                DevExpress.Xpo.Session Sess = ((XPObjectSpace)ObjectSpace).Session;
                if (View is DetailView)
                {
                    //BindingList<SostegniCTRStatica> objs = new BindingList<SostegniCTRStatica>();
                    DevExpress.Xpo.XPQuery<RdL> qRDL = new DevExpress.Xpo.XPQuery<RdL>(Sess);
                    DevExpress.Xpo.XPQuery<MpAttivitaPianificateDett> qMPPianDet = new DevExpress.Xpo.XPQuery<MpAttivitaPianificateDett>(Sess);
                    int[] statiEseguito = new[] { 4, 5, 8, 9 };
                    List<SostegniCTRStatica> ListaPali = qMPPianDet
                                             .Join(qRDL,
                                             pd => pd.RdL.Oid,
                                             rdl => rdl.Oid,
                                             (pd, rdl) => new { PD = pd, RDL = rdl }
                                             )
                                              .Where(w => new int[] { 1, 5 }.Contains(w.RDL.Categoria.Oid)
                                              && new int[] { 1238, 1870 }.Contains(w.RDL.Asset.StdAsset.Oid)
                                              && new int[] { 1, 2, 3, 4, 6, 10, 11 }.Contains(w.RDL.UltimoStatoSmistamento.Oid)
                                              && w.RDL.Immobile.Contratti.Oid == OidCommessa &&
                                              w.RDL.DataPianificata.Year == anno &&
                                              stati.Contains(w.RDL.UltimoStatoSmistamento.Oid)  //(stati == 0 || (stati == 4 && w.RDL.UltimoStatoSmistamento.Oid == 4) || (stati == 1 && w.RDL.UltimoStatoSmistamento.Oid != 4))
                                              && w.RDL.Asset.Abilitato == FlgAbilitato.Si
                                              && new int[] { 3856, 3857, 3858, 3859, 3855, 3860, 3968, 3969, 3970, 3971, 3972, 3973 }.Contains(w.PD.MpAttPianificate.ApparatoSchedaMP.SchedaMp.Oid))
                                            .Select(s => new SostegniCTRStatica
                                            {
                                                Oid = Guid.NewGuid(),
                                                Title = s.RDL.DataPianificata.Year.ToString(),
                                                Latitude = Convert.ToDouble(s.RDL.Asset.GeoLocalizzazione.Latitude),
                                                Longitude = Convert.ToDouble(s.RDL.Asset.GeoLocalizzazione.Longitude),
                                                IndividualMarkerIcon = @"C:\AssemblaEAMS\EAMS\CAMS.Web\Images\CS.png",
                                                Anno = s.RDL.DataPianificata.Year.ToString(),
                                                Stato = statiEseguito.Contains<int>(s.RDL.UltimoStatoSmistamento.Oid) ? TipoVerificaStatica.Eseguito : TipoVerificaStatica.Pianificato,
                                            }).Distinct()
                                  .ToList<SostegniCTRStatica>();

                    e.Objects = new BindingList<SostegniCTRStatica>(ListaPali.ToList());
                    //e.Objects = objects;

                    #region mio
                    //foreach (var dr in customers)
                    //{
                    //    if (dr.CentroOperativoOid)
                    //    {
                    //        idColore = 1;
                    //    }
                    //    else if (dr.RisorseTeamOid)
                    //    {
                    //        idColore = 2;
                    //    }
                    //    else
                    //    {
                    //        idColore = 0;
                    //    }
                    //    DCRdL objdcrdl = new DCRdL()
                    //    {
                    //        ID = dr.ID,//Guid.NewGuid, /*obj.ID = Newid++;*/
                    //                   //RdL = dr,
                    //        RdLCodice = dr.RdLCodice,
                    //        RdLDescrizione = dr.RdLDescrizione,
                    //        RdLSollecito = string.Format(dr.RdLSollecito),
                    //        RichiedenteDescrizione = dr.RichiedenteDescrizione,
                    //        EdificioDescrizione = dr.EdificioDescrizione,
                    //        ImpiantoDescrizione = dr.ImpiantoDescrizione,
                    //        IndirizzoDescrizione = dr.IndirizzoDescrizione,
                    //        DataPianificata = dr.DataPianificata,
                    //        idColore = idColore
                    //    };

                    //    objects.Add(objdcrdl);
                    //}
                    #endregion

                }

            }
        }


        //RdLListView list_RdLListView_Selezionati = (((ListView)View).Editor).GetSelectedObjects().
        //                                                    Cast<RdLListView>().ToList<RdLListView>().First();
        //RdL RDLSelezionata = xpObjectSpace.GetObjectByKey<RdL>(list_RdLListView_Selezionati.Codice);
        //OidRdL = RDLSelezionata.Oid;
        //                int OidEdificio = RDLSelezionata.Immobile.Oid;
        //                // --- imposto la rdl sulla notifica emergenza  
        //                if (RDLSelezionata != null)
        //                {
        //                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(DCRisorseTeamRdL));
        //objectSpace.ObjectsGetting += DCRisorseTeamRdL_objectSpace_ObjectsGetting;

        //                    CollectionSource DCRisorseTeamRdL_Lookup = (CollectionSource)Application.CreateCollectionSource(objectSpace, typeof(DCRisorseTeamRdL),
        //                                                                                "DCRisorseTeamRdL_LookupListView");
        //SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Ordinamento", DevExpress.Xpo.DB.SortingDirection.Descending);
        //DCRisorseTeamRdL_Lookup.Sorting.Add(srtProperty);

        //                    ListView lvk = Application.CreateListView("DCRisorseTeamRdL_LookupListView", DCRisorseTeamRdL_Lookup, true);

        //var dc = Application.CreateController<DialogController>();
        //e.DialogController.SaveOnAccept = false;
        //                    e.View = lvk;
        //                    //objectSpace.ObjectsGetting -= DCRisorseTeamRdL_objectSpace_ObjectsGetting;

        //                }

}
}
