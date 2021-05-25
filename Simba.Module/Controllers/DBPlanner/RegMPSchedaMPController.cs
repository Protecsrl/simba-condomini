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
using CAMS.Module.DBPlanner.DC;
using DevExpress.ExpressApp.Xpo;
using System.ComponentModel;
using DevExpress.Xpo;
using CAMS.Module.DBPlanner;
using CAMS.Module.Classi;

namespace CAMS.Module.Controllers.DBPlanner
{
    // For more typical usage scenarios, be sure to check out https://documentation.devexpress.com/eXpressAppFramework/clsDevExpressExpressAppViewControllertopic.aspx.
    public partial class RegMPSchedaMPController : ViewController
    {
        public RegMPSchedaMPController()
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
        List<RegMPSchedaMP> ListRegMPSchedaMP = null;
        bool truexRegRdL_falsexRdL = false;

        private void pupWinAggregazioneRegRdL_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            truexRegRdL_falsexRdL = true;
            if (xpObjectSpace != null)
            {
                ListRegMPSchedaMP = (((ListView)View).Editor).GetSelectedObjects().Cast<RegMPSchedaMP>().ToList();
                if (ListRegMPSchedaMP.Count > 0)
                {
                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(ParametroDescrizione));
                    objectSpace.ObjectsGetting += ParametroDescrizione_objectSpace_ObjectsGetting;

                    CollectionSource DCParametroDescrizion = (CollectionSource)Application.
                                        CreateCollectionSource(objectSpace, typeof(ParametroDescrizione), "ParametroDescrizione_LookupListView");
                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Descrizione", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    DCParametroDescrizion.Sorting.Add(srtProperty); 
                    ListView lvk = Application.CreateListView("ParametroDescrizione_LookupListView", DCParametroDescrizion, true);
                    var dc = Application.CreateController<DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = lvk;
                }
                else
                {
                    SetMessaggioWeb("non sono stati segnalati interventi da aggiornare", "Attenzione", InformationType.Info);               
                }
            }
            
        }
        private void pupWinAggregazioneRegRdL_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace =  View.ObjectSpace;
            bool faiinfo = false;
            List<string> listMessaggio = new List<string>();
            if (View is ListView)
            {
                ListView lv = View as ListView;
                List<ParametroDescrizione> lst_ParametroDescrizione_Selezionate = ((List<ParametroDescrizione>)((((Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<ParametroDescrizione>()
                                           .ToList<ParametroDescrizione>()));

                if (xpObjectSpace != null && lst_ParametroDescrizione_Selezionate.Count > 0)
                {
                    if (View.ObjectTypeInfo.Type == typeof(RegMPSchedaMP))//.Editor).GetSelectedObjects().Count > 0)
                    {
                        //RegMPSchedaMP regSchedaMP = (RegMPSchedaMP)dv.CurrentObject;
                        int nrSelezionato = (((ListView)View).Editor).GetSelectedObjects().Count;

                        if (nrSelezionato > 0)
                        {
                            List<RegMPSchedaMP> List = (((ListView)View).Editor).GetSelectedObjects().Cast<RegMPSchedaMP>().ToList();
                            foreach (var dr in List)
                            {
                                RegMPSchedaMP regmpsk = xpObjectSpace.GetObject<RegMPSchedaMP>(dr);
                                try
                                {
                                    regmpsk.Agg_RegRdl = lst_ParametroDescrizione_Selezionate.First().Descrizione.ToString();
                                    regmpsk.Save();
                                    listMessaggio.Add(regmpsk.Agg_RegRdl);

                                }
                                catch (Exception eccezione)
                                {
                                    listMessaggio.Add("Errore di Aggiornamento, errore:" + eccezione.Message);
                                    SetMessaggioWeb(string.Join("; ", listMessaggio.ToArray()), "Attenzione", InformationType.Warning);
                                    break;
                                }
                            }
                        }
                        View.ObjectSpace.CommitChanges();
                        SetMessaggioWeb("eseguito: " + string.Join("; ", listMessaggio.ToArray()), "Eseguito", InformationType.Success);
                    }
                    else
                    {
                        faiinfo = true;
                    }

                }
                else
                {
                    faiinfo = true;
                }
            }
            if( faiinfo )
                SetMessaggioWeb("nessuna selezione", "Avviso", InformationType.Info);
        }

        private void pupWinAggregazioneRdL_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            truexRegRdL_falsexRdL = false;
            if (xpObjectSpace != null)
            {
                ListRegMPSchedaMP = (((ListView)View).Editor).GetSelectedObjects().Cast<RegMPSchedaMP>().ToList();
                if (ListRegMPSchedaMP.Count > 0)
                {
                    NonPersistentObjectSpace objectSpace = (NonPersistentObjectSpace)Application.CreateObjectSpace(typeof(ParametroDescrizione));
                    objectSpace.ObjectsGetting += ParametroDescrizione_objectSpace_ObjectsGetting;

                    CollectionSource DCParametroDescrizion = (CollectionSource)Application.
                                        CreateCollectionSource(objectSpace, typeof(ParametroDescrizione), "ParametroDescrizione_LookupListView");
                    SortProperty srtProperty = new DevExpress.Xpo.SortProperty("Descrizione", DevExpress.Xpo.DB.SortingDirection.Ascending);
                    DCParametroDescrizion.Sorting.Add(srtProperty);
                    ListView lvk = Application.CreateListView("ParametroDescrizione_LookupListView", DCParametroDescrizion, true);
                    var dc = Application.CreateController<DialogController>();
                    e.DialogController.SaveOnAccept = false;
                    e.View = lvk;
                }
                else
                {
                    SetMessaggioWeb("non sono stati segnalati interventi da aggiornare", "Attenzione", InformationType.Info);
                }
            }
        }

        private void pupWinAggregazioneRdL_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = View.ObjectSpace;
            bool faiinfo = false;
            List<string> listMessaggio = new List<string>();
            if (View is ListView)
            {
                List<ParametroDescrizione> lst_ParametroDescrizione_Selezionate = ((List<ParametroDescrizione>)((((Frame)
                                           (e.PopupWindow)).View).SelectedObjects.Cast<ParametroDescrizione>()
                                           .ToList<ParametroDescrizione>()));

                if (xpObjectSpace != null && lst_ParametroDescrizione_Selezionate.Count > 0)
                {
                    if (View.ObjectTypeInfo.Type == typeof(RegMPSchedaMP))//.Editor).GetSelectedObjects().Count > 0)
                    {
                        int nrSelezionato = (((ListView)View).Editor).GetSelectedObjects().Count;

                        if (nrSelezionato > 0)
                        {
                            List<RegMPSchedaMP> List = (((ListView)View).Editor).GetSelectedObjects().Cast<RegMPSchedaMP>().ToList();
                            foreach (var dr in List)
                            {
                                RegMPSchedaMP regmpsk = xpObjectSpace.GetObject<RegMPSchedaMP>(dr);
                                try
                                {
                                    regmpsk.Agg_Rdl = lst_ParametroDescrizione_Selezionate.First().Descrizione.ToString();
                                    regmpsk.Save();
                                    listMessaggio.Add(regmpsk.Agg_RegRdl);

                                }
                                catch (Exception eccezione)
                                {
                                    listMessaggio.Add("Errore di Aggiornamento, errore:" + eccezione.Message);
                                    SetMessaggioWeb(string.Join("; ", listMessaggio.ToArray()), "Attenzione", InformationType.Warning);
                                    break;
                                }
                            }
                        }
                        View.ObjectSpace.CommitChanges();
                        SetMessaggioWeb("eseguito: " + string.Join("; ", listMessaggio.ToArray()), "Eseguito", InformationType.Success);
                    }
                    else
                    {
                        faiinfo = true;
                    }

                }
                else
                {
                    faiinfo = true;
                }
            }
            if (faiinfo)
                SetMessaggioWeb("nessuna selezione", "Avviso", InformationType.Info);
        }

        void ParametroDescrizione_objectSpace_ObjectsGetting(object sender, ObjectsGettingEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            if (View.Id == "RegPianificazioneMP_RegMPSchedaMPs_ListView")
            {
                RegPianificazioneMP RegPianMP = ListRegMPSchedaMP.First().RegistroPianificazioneMP;
                BindingList<ParametroDescrizione> objects = new BindingList<ParametroDescrizione>();
                int Newid = 0;
                if (truexRegRdL_falsexRdL)
                {
                    var vAgg_RegRdl = new XPQuery<RegMPSchedaMP>(Sess, true)
                       .Where(w => w.RegistroPianificazioneMP == RegPianMP)
                        .Where(w => w.Abilitato == FlgAbilitato.Si)
                             .Select(s => new
                             {
                                 Agg_RegRdl = s.Agg_RegRdl,
                             })
                             .Distinct().ToList();
                    foreach (var dr in vAgg_RegRdl)
                    {
                        ParametroDescrizione objdcrdl = new ParametroDescrizione()
                        {
                            ID = Newid++,
                            Descrizione = dr.Agg_RegRdl // vAgg_RegRdl
                        };
                        objects.Add(objdcrdl);
                    }                  
                }
                else
                {
                    var vAgg_Rdl = new XPQuery<RegMPSchedaMP>(Sess, true)
                      .Where(w => w.RegistroPianificazioneMP == RegPianMP)
                       .Where(w => w.Abilitato == FlgAbilitato.Si)
                            .Select(s => new
                            {
                                Agg_Rdl = s.Agg_Rdl
                            })
                            .Distinct().ToList();
                    foreach (var dr in vAgg_Rdl)
                    {
                        ParametroDescrizione objdcrdl = new ParametroDescrizione()
                        {
                            ID = Newid++,
                            Descrizione = dr.Agg_Rdl // vAgg_RegRdl
                        };
                        objects.Add(objdcrdl);
                    }  
                }
                e.Objects = objects;
            }
        }
        
        private void SetMessaggioWeb(string Messaggio, string Titolo, InformationType InformationTypeMsg = InformationType.Info)
        {
            MessageOptions options = new MessageOptions();
            options.Duration = 5000;
            options.Message = Messaggio.ToString();
            options.Web.Position = InformationPosition.Top;
            options.Type = InformationTypeMsg;//            InformationType.Info;
            options.Win.Caption = Titolo;             //options.CancelDelegate = CancelDelegate;              //options.OkDelegate = OkDelegate;
            //options.OkDelegate = () =>
            //{

            //};
            Application.ShowViewStrategy.ShowMessage(options);
        }

    }
}
