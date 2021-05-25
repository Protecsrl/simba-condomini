using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.DBPlant.Coefficienti;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;

using DevExpress.ExpressApp.Xpo;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CAMS.Module.Controllers
{
    public partial class ImpiantoController : ViewController
    {
        private const string NuovoImpianto_DetailView = "NuovoImpianto_DetailView";

        private const string ApparatoKTrasferimento_DetailView = "ApparatoKTrasferimento_DetailView";
        private const string ApparatoKUtenza_DetailView = "ApparatoKUtenza_DetailView";

        public Dictionary<String, ServizioLibrary> lstImpiantiLibrary { get; set; }
        private Asset NuovoApparato;
        private bool needClearSelection;
        //private string listViewId = "StdApparatoInseribili_ListView";
        bool TastoVisibleCrea { get; set; }
        bool TastoVisibleWrite { get; set; }
        public ImpiantoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }

        protected override void OnActivated()
        {
            base.OnActivated();
            TastoVisibleCrea = false;
            TastoVisibleWrite = false;
            using (UtilController uc = new UtilController())
            {
                IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
                TastoVisibleCrea = uc.GetIsGrantedCreate(xpObjectSpace, "Impianto", "C");
                TastoVisibleWrite = uc.GetIsGrantedCreate(xpObjectSpace, "Impianto", "W");
            }
            if (Frame != null && Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>() != null)
            {
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
                Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);
            }
            ImpiantoClonaImpianto.Active["ImpiantoClonaImpianto"] = false;
            ImpiantoCreaDalTipo.Active["ImpiantoCreaDalTipo"] = false;

            if (View is ListView)
            {
                if (View.Id.Contains("Edificio_Impianti_ListView"))
                {
                    if (TastoVisibleCrea)
                    {
                        ImpiantoClonaImpianto.Active["ImpiantoClonaImpianto"] = true;
                        ImpiantoCreaDalTipo.Active["ImpiantoCreaDalTipo"] = true;
                    }

                }
                else if (View.Id.Contains("Impianti_ListView"))
                {
                    if (TastoVisibleCrea)
                    {
                        ImpiantoClonaImpianto.Active["ImpiantoClonaImpianto"] = true;
                        ImpiantoCreaDalTipo.Active["ImpiantoCreaDalTipo"] = true;
                    }

                }

            }

        }
        protected override void OnViewControlsCreated()
        {
            InserisciApparati.Active["AttivaInserisciApparato"] = false;
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    if (View.Id == "Impianto_DetailView")
                    {
                        InserisciApparati.Active["AttivaInserisciApparato"] = true;
                    }
                }
            }
            base.OnViewControlsCreated();
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
        private void ImpiantoController_ViewControlsCreated(object sender, EventArgs e)
        {
        }
        private void CurrentRequestPage_Load(object sender, EventArgs e)
        {
            if (needClearSelection)
            {
                needClearSelection = false;
            }
        }

        private void ImpiantoKUtenza_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                var view = Application.CreateDetailView(xpObjectSpace, ApparatoKUtenza_DetailView, false, NuovoApparato);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void ImpiantoKUtenza_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            var ImpiantiSelezionatiOid = (((ListView)View).Editor).GetSelectedObjects().Cast<Servizio>().Select(i => i.Oid).ToList();
            for (var vImp = 0; vImp < ImpiantiSelezionatiOid.Count; vImp++)
            {
                var Imp = xpObjectSpace.GetObjectByKey<Servizio>(ImpiantiSelezionatiOid[vImp]);
                for (var i = 0; i < Imp.APPARATOes.Count; i++)
                {
                }
            }

            xpObjectSpace.CommitChanges();

            RefreshDati();
        }



        private void ImpiantoClonaImpianto_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                var NuovoIm = xpObjectSpace.CreateObject<NuovoImpianto>();

                var ImpiantoSelezionato = xpObjectSpace.GetObject((((ListView)View).Editor).GetSelectedObjects().Cast<Servizio>().First());

                NuovoIm.Descrizione = "copia " + ImpiantoSelezionato.Descrizione;
                NuovoIm.CodDescrizione = string.Format("{0}_{1}", ImpiantoSelezionato.Immobile.CodDescrizione, ImpiantoSelezionato.Sistema.CodDescrizione);
                NuovoIm.VecchioImpianto = ImpiantoSelezionato;

                var view = Application.CreateDetailView(xpObjectSpace, NuovoImpianto_DetailView, true, NuovoIm);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void ImpiantoClona_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var NuovoIm = ((NuovoImpianto)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));
            using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
            {
                //var db = new Classi.DB();
                db.CloneImpianto(Classi.SetVarSessione.CorrenteUser, NuovoIm.VecchioImpianto.Oid, NuovoIm.CodDescrizione, NuovoIm.Descrizione);
                //db.Dispose();
                db.AggiornaTempi(NuovoIm.VecchioImpianto.Immobile.Oid, "IMMOBILE");
            }

            RefreshDati();
        }

        private void ImpiantoCreaDalTipo_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            if (xpObjectSpace != null)
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id == "Edificio_DetailView")
                {
                    if (dvParent.ViewEditMode == ViewEditMode.Edit)
                    {
                        Immobile ed = (Immobile)dvParent.CurrentObject;
                        var NuovoImpianto = xpObjectSpace.CreateObject<Servizio>();
                        NuovoImpianto.CreaDaLibreriaImpianti = true;
                        NuovoImpianto.Immobile = xpObjectSpace.GetObject<Immobile>(ed);
                        const string ImpiantoDaLiberiaImpianto_DetailView = "Impianto_DetailView_DaLibreriaImpianto";
                        var view = Application.CreateDetailView(xpObjectSpace, ImpiantoDaLiberiaImpianto_DetailView, true, NuovoImpianto);
                        view.ViewEditMode = ViewEditMode.Edit;
                        e.View = view;
                    }

                }
                else
                {
                    var NuovoImpianto = xpObjectSpace.CreateObject<Servizio>();
                    NuovoImpianto.CreaDaLibreriaImpianti = true;
                    string ImpiantoDaLiberiaImpianto_DetailView = "Impianto_DetailView_DaLibreriaImpianto";
                    var view = Application.CreateDetailView(xpObjectSpace, ImpiantoDaLiberiaImpianto_DetailView, true, NuovoImpianto);
                    view.ViewEditMode = ViewEditMode.Edit;
                    e.View = view;
                }

            }
        }
        private void ImpiantoCreaDalTipo_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {


            RefreshDati();
        }

        private void RefreshDati()
        {
            try
            {
                needClearSelection = true;

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

        private void InserisciApparati_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var os = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            Session Sess = ((XPObjectSpace)ObjectSpace).Session;
            var app = ((List<StdAsset>)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).SelectedObjects.Cast<StdAsset>().ToList<StdAsset>()));
            var impiantoCurr = (Servizio)View.CurrentObject;
            foreach (StdAsset a in app)
            {
                var MaxId = Sess.Query<Asset>().Select(s => s.Oid).Max();
                string cod_apparato = string.Format("{0}_{1}", impiantoCurr.CodDescrizione, MaxId);
                var nuovoApp = os.CreateObject<Asset>();
                nuovoApp.Descrizione = a.Descrizione;
                nuovoApp.Servizio = impiantoCurr;
                nuovoApp.StdAsset = os.GetObject<StdAsset>(a);
                nuovoApp.CodDescrizione = cod_apparato;
                nuovoApp.Tag = MaxId.ToString();
                nuovoApp.Quantita = 1;
                nuovoApp.DateInService = DateTime.Now;
                nuovoApp.Abilitato = FlgAbilitato.Si;


                var AppCoeff = os.CreateObject<AssetkTempo>();

                var lstKcon = new XPCollection<KCondizione>(impiantoCurr.Session);
                var kc = lstKcon.FirstOrDefault(cond => cond.Default == KDefault.Si);
                AppCoeff.KCondizioneOid = kc.Oid;
                AppCoeff.KCondizioneDesc = kc.Descrizione;
                AppCoeff.KCondizioneValore = kc.Valore;

                var lstKgua = new XPCollection<KGuasto>(impiantoCurr.Session);
                var kg = lstKgua.FirstOrDefault(cond => cond.Default == KDefault.Si);
                AppCoeff.KGuastoOid = kg.Oid;
                AppCoeff.KGuastoDesc = kg.Descrizione;
                AppCoeff.KGuastoValore = kg.Valore;

                var lstKubi = new XPCollection<KUbicazione>(impiantoCurr.Session);
                var ku = lstKubi.FirstOrDefault(cond => cond.Default == KDefault.Si);
                AppCoeff.KUbicazioneOid = ku.Oid;
                AppCoeff.KUbicazioneDesc = ku.Descrizione;
                AppCoeff.KUbicazioneValore = ku.Valore;
                AppCoeff.Utente = Application.Security.UserName;
                AppCoeff.DataAggiornamento = DateTime.Now;
                AppCoeff.Save();
                os.CommitChanges();

                var CoefTotale = double.Parse(AppCoeff.EvaluateAlias("CoefficienteTotale").ToString());
                nuovoApp.TotaleCoefficienti = CoefTotale;
                nuovoApp.AssetkTempo = AppCoeff;
                nuovoApp.InsertSchedeMPsuAsset(ref nuovoApp, a.Oid);
                nuovoApp.Save();
            }

            os.CommitChanges();
            os.Refresh();
        }

        private void InserisciApparati_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                if (View is DetailView)
                    if (View.Id == "Impianto_DetailView")
                        if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                        {
                            var ImpiantoSelezionato = (Servizio)View.CurrentObject;

                            var lstSchedeMP = new XPCollection<SchedaMp>(ImpiantoSelezionato.Session);
                            var lstDaEscludere = ImpiantoSelezionato.APPARATOes.Select(ld => ld.StdAsset);

                            var tmpLst = lstSchedeMP.Where(smp => smp.Sistema == ImpiantoSelezionato.Sistema)
                                .Select(smp => smp.StdAsset).Distinct().ToList();


                            if (lstDaEscludere.Count() > 0)
                                tmpLst = lstSchedeMP.Where(smp => smp.Sistema == ImpiantoSelezionato.Sistema)
                                  .Select(smp => smp.StdAsset).Distinct().Where(std => !lstDaEscludere.Contains(std)).ToList();
                               

                            var crtapp = string.Empty;
                            foreach (StdAsset std in tmpLst)
                            {
                                crtapp += std.Oid + ",";
                            }

                            var crTApparati = string.Format("Oid in ({0})", crtapp.Substring(0, crtapp.Length - 1));
                            string listViewId = "StdApparato_ListView_InseribiliImpianto";
                            var StdApparatiSelezionabili = (CollectionSource)Application.CreateCollectionSource(xpObjectSpace, typeof(StdAsset), listViewId);
                            StdApparatiSelezionabili.Criteria["Filtro_ListaStdApparatiInseribili"] = CriteriaOperator.Parse(crTApparati);
                            var view = Application.CreateListView(listViewId, StdApparatiSelezionabili, false);
                            e.View = view;
                            e.IsSizeable = true;
                        }
            }
        }


    }
}


//private void ImpiantoKTrasferimento_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
//       {
//           var xpObjectSpace = Application.CreateObjectSpace();

//           if (xpObjectSpace != null)
//           {
//               NuovoApparato = xpObjectSpace.CreateObject<Apparato>();

//               var view = Application.CreateDetailView(xpObjectSpace, ApparatoKTrasferimento_DetailView, false, NuovoApparato);
//               view.ViewEditMode = ViewEditMode.Edit;
//               e.View = view;
//           }
//       }
//       private void ImpiantoKTrasferimento_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
//       {
//           RefreshDati();
//       }