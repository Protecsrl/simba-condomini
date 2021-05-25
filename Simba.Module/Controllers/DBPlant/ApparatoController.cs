using System;
using System.Linq;
using DevExpress.ExpressApp;
using DevExpress.Data.Filtering;
using System.Collections.Generic;
using DevExpress.ExpressApp.Actions;
using DevExpress.ExpressApp.Editors;

using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using CAMS.Module.Classi;
using DevExpress.Xpo;
using CAMS.Module.DBPlant.Coefficienti;
using CAMS.Module.DBSpazi;


namespace CAMS.Module.Controllers
{
    public partial class ApparatoController : ViewController
    {
        private const string ApparatoKDimensione_DetailView = "ApparatoKDimensione_DetailView";
        private const string ApparatoKUbicazione_DetailView = "ApparatoKUbicazione_DetailView";
        private const string ImpiantoDettaglioAssocia_DetailView = "ImpiantoDettaglioAssocia_DetailView";
        private const string NuovoApparato_DetailView = "ParametriApparato_DetailView";
        int SelectDefault = 0;
        private Asset NuovoApparato;

        public ApparatoController()
        {
            InitializeComponent();
            RegisterActions(components);
        }
        protected override void OnActivated()
        {
            base.OnActivated();
            System.Diagnostics.Debug.WriteLine("OnActivated() " + this.Name);
            //Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().LinkAction.Active.SetItemValue("LinkAction", false);
            //Frame.GetController<DevExpress.ExpressApp.SystemModule.LinkUnlinkController>().UnlinkAction.Active.SetItemValue("UnlinkAction", false);

            //saGetStrada.Enabled["EditMode"] = View.ViewEditMode == DevExpress.ExpressApp.Editors.ViewEditMode.Edit;
            if (View is DetailView)
                saSostituisciApparato.Enabled["EditMode"] = ((DetailView)View).ViewEditMode == ViewEditMode.Edit;
               
            if (View.Id == "Impianto_APPARATOes_ListView")
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id == "Impianto_DetailView")
                {
                    IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
                    Servizio im = (Servizio)dvParent.CurrentObject;

                    var ListStdEQ = xpObjectSpace.GetObjectsQuery<Asset>()
                        .Where(w => w.Servizio.Oid == im.Oid)
                        .GroupBy(g => new { STDEQ_Oid = g.StdAsset.Oid, STDEQ_Desc = g.StdAsset.Descrizione })
                        .Distinct().ToList();

                    //var lstRDLEdifici = lstRDLSelezionati.GroupBy(a => a.Impianto.Immobile).Select(g => g.First()).ToList();
                    //var aa = im.APPARATOes.Where(w => w.Oid == -1).Count();
                    int i = 0;
                    SelectDefault = 0;
                    foreach (var r in ListStdEQ)
                    {
                        this.scaFiltroTipoApparato.Items.Add((
                            new ChoiceActionItem()
                            {
                                Id = r.Key.STDEQ_Oid.ToString(),
                                Data = string.Format("StdApparato.Oid == {0}", r.Key.STDEQ_Oid),
                                Caption = r.Key.STDEQ_Desc
                            }));

                        if (r.Key.STDEQ_Desc.Contains("Quadro "))
                            SelectDefault = i;
                        i++;
                    }
                    if (scaFiltroTipoApparato.SelectedIndex == -1)
                        if (scaFiltroTipoApparato.Items.Count > SelectDefault && SelectDefault >= 0)
                            scaFiltroTipoApparato.SelectedIndex = SelectDefault;
                }
            }


        }

        protected override void OnViewControlsCreated()
        {
            base.OnViewControlsCreated();

            if (View is DetailView)
                saSostituisciApparato.Enabled["EditMode"] = ((DetailView)View).ViewEditMode == ViewEditMode.Edit;

            if (View.Id == "Impianto_APPARATOes_ListView")
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id == "Impianto_DetailView")
                {
                    //if (scaFiltroTipoApparato.SelectedIndex == -1)
                    //    scaFiltroTipoApparato.SelectedIndex = SelectDefault;
                    if (scaFiltroTipoApparato.SelectedIndex == -1)
                        if (scaFiltroTipoApparato.Items.Count > SelectDefault && SelectDefault >= 0)
                            scaFiltroTipoApparato.SelectedIndex = SelectDefault;

                }
            }
        }
        protected override void OnDeactivated()
        {
            base.OnDeactivated();
        }
        private void ApparatoController_ViewControlsCreated(object sender, EventArgs e)
        {
            var AttivaAppkCondizione = false;
            ApparatoClonaApparato.Active["DisattivaClonaApparato"] = false;
            if (View is DetailView)
            {
                if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                {
                    if (View.Id == "Apparato_DetailView")
                    {
                        AttivaAppkCondizione = true;
                    }
                }
            }
            if (View is ListView)
            {
                if (View.Id == "Impianto_APPARATOes_ListView")
                {
                    var dvParent = (DetailView)(View.ObjectSpace).Owner;

                    if (dvParent.Id == "Impianto_DetailView" && dvParent.ViewEditMode == ViewEditMode.Edit)
                    {
                        AttivaAppkCondizione = true;
                        ApparatoClonaApparato.Active["DisattivaClonaApparato"] = true;

                        Servizio im = (Servizio)dvParent.CurrentObject;
                        var aa = im.APPARATOes.Where(w => w.Oid == -1).Count();
                        if (aa == 2)
                        {
                            int num = im.APPARATOes.Where(w => w.Oid < 0).Count();
                            im.APPARATOes[num - 1].Oid = -1 * (num + 2);
                            im.APPARATOes[num - 1].Save();
                        }

                    }
                }
            }
        }

        private void ApparatoKDimensione_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                var ApparatoSelezionato = (((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().First();
                var view = Application.CreateDetailView(xpObjectSpace, ApparatoKDimensione_DetailView, true, xpObjectSpace.GetObject(ApparatoSelezionato));
                view.ViewEditMode = ViewEditMode.Edit;

                e.View = view;
            }
        }
        private void ApparatoKDimensione_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            //RefreshDati();
        }

        private void ApparatoKUbicazione_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                e.View = Application.CreateDetailView(xpObjectSpace, ApparatoKUbicazione_DetailView, false, NuovoApparato);
            }
        }

        private void ApparatoKCondizione_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();
            var lstIdSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().Select(app => app.Oid).ToList<Int32>();
            xpObjectSpace.GetObjects<Asset>().ToList<Asset>().Where(std => lstIdSelezionati.Contains(std.Oid)).ToList();

            //RefreshDati();
        }

        private void ApparatoAssociaImpianto_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                var view = Application.CreateDetailView(xpObjectSpace, ImpiantoDettaglioAssocia_DetailView, false, NuovoApparato);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }
        private void ApparatoAssociaImpianto_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().ToList();
            Servizio ServizioCorrente = null;

            var lstStdApparato = xpObjectSpace.GetObjects<StdAsset>();

            xpObjectSpace.GetObjects<KCondizione>().FirstOrDefault(r => r.Default == KDefault.Si);
            xpObjectSpace.GetObjects<KTrasferimento>().FirstOrDefault(r => r.Default == KDefault.Si);
            xpObjectSpace.GetObjects<KUbicazione>().FirstOrDefault(r => r.Default == KDefault.Si);
            xpObjectSpace.GetObjects<KUtenza>().FirstOrDefault(r => r.Default == KDefault.Si);
            xpObjectSpace.GetObjects<KGuasto>().FirstOrDefault(r => r.Default == KDefault.Si);
            foreach (Asset apparato in lstApparatiSelezionati)
            {
                var NuovoApparto = xpObjectSpace.CreateObject<Asset>();

                if (ServizioCorrente == null)
                {
                    ServizioCorrente = xpObjectSpace.FindObject<Servizio>
                        (new BinaryOperator(Servizio.Fields.Oid.PropertyName.ToString(), apparato.OidServizio));
                }
                NuovoApparto.CodDescrizione = apparato.CodDescrizione;
                NuovoApparto.StdAsset = lstStdApparato.FirstOrDefault(app => app.Oid == apparato.StdAsset.Oid);

                if (NuovoApparato.StdAsset != null)
                {
                    NuovoApparto.Descrizione = apparato.StdAsset.Descrizione;
                }
                else
                {
                    NuovoApparto.Descrizione = apparato.Descrizione;
                }

                NuovoApparto.Quantita = NuovoApparato.Quantita;
                var Criteria = string.Format("StandardApparato.Oid = {0} And [Default] = '{1}'", apparato.StdAsset.Oid, KDefault.Si);
                xpObjectSpace.FindObject<KDimensione>(CriteriaOperator.Parse(Criteria));
                ServizioCorrente.APPARATOes.Add(NuovoApparto);
            }

            xpObjectSpace.CommitChanges();

            //RefreshDati();
        }

        //private void RefreshDati()
        //{
        //    try
        //    {
        //        SetVarSessione.Esegui_DeSelezionaDati = true;
        //        if (View is DetailView)
        //        {
        //            View.ObjectSpace.ReloadObject(View.CurrentObject);
        //        }
        //        else
        //        {
        //            (View as DevExpress.ExpressApp.ListView).CollectionSource.Reload();
        //        }
        //        View.ObjectSpace.Refresh();
        //        View.Refresh();
        //    }
        //    catch
        //    {
        //    }
        //}
        //private void RefreshDati(XPObject padre, ListView Lv)
        //{
        //    try
        //    {
        //        Lv.CollectionSource.Reload();
        //        View.Refresh();
        //        base.Frame.View.Refresh();
        //    }
        //    catch
        //    {
        //    }
        //}
        private void ApparatoAssociaImpiantoNr_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var NrApparati = int.Parse(e.SelectedChoiceActionItem.Id.ToUpper());

            var xpObjectSpace = Application.CreateObjectSpace();
            var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().ToList();
            Servizio ServizioCorrente = null;



            //xpObjectSpace.GetObjects<KCondizione>().FirstOrDefault(r => r.Default == KDefault.Si);
            //xpObjectSpace.GetObjects<KTrasferimento>().FirstOrDefault(r => r.Default == KDefault.Si);
            //xpObjectSpace.GetObjects<KUbicazione>().FirstOrDefault(r => r.Default == KDefault.Si);
            //xpObjectSpace.GetObjects<KUtenza>().FirstOrDefault(r => r.Default == KDefault.Si);
            //xpObjectSpace.GetObjects<KGuasto>().FirstOrDefault(r => r.Default == KDefault.Si);

            foreach (Asset apparato in lstApparatiSelezionati)
            {
                if (ServizioCorrente == null)
                {
                    ServizioCorrente = xpObjectSpace.FindObject<Servizio>
                              (new BinaryOperator(Servizio.Fields.Oid.PropertyName.ToString(), apparato.OidServizio));
                }
                var lstSchedaMp = xpObjectSpace.GetObjects<SchedaMp>();
                (from sk in lstSchedaMp
                 where (sk.StdAsset.Oid == apparato.StdAsset.Oid) //(sk.Eqstd.Oid 
                 //orderby sk.KGuasto
                 select sk).Take(1);

                var Criteria = string.Format("StandardApparato.Oid = {0} And [Default] = '{1}'", apparato.StdAsset.Oid, KDefault.Si);
                xpObjectSpace.FindObject<KDimensione>(CriteriaOperator.Parse(Criteria));

                var lstStdApparato = xpObjectSpace.GetObjects<StdAsset>();
                var AppSTD = lstStdApparato.FirstOrDefault(app => app.Oid == apparato.StdAsset.Oid);

                for (var i = 0; i < NrApparati; i++)
                {
                    var NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                    ///  descrizione apparato
                    if (apparato.StdAsset != null)
                    {
                        NuovoApparato.StdAsset = AppSTD;
                        NuovoApparato.Descrizione = string.Format("{0} - Copia {1}", apparato.StdAsset.Descrizione, NrApparati);
                    }
                    else
                    {
                        NuovoApparato.StdAsset = AppSTD;
                        NuovoApparato.Descrizione = string.Format("{0} - Copia {1}", apparato.StdAsset.Descrizione, NrApparati);
                    }

                    NuovoApparato.Quantita = 1;
                    var ut = new Util();
                    var AppCodDescrizione = ut.GetCodiceApparato(ServizioCorrente.CodDescrizione, apparato.StdAsset.CodDescrizione, ServizioCorrente.Oid, i, ServizioCorrente.Session);
                    ut.Dispose();
                    NuovoApparato.CodDescrizione = AppCodDescrizione;
                    NuovoApparato.InsertSchedeMPsuAsset(ref NuovoApparato, apparato.StdAsset.Oid);
                    ServizioCorrente.APPARATOes.Add(NuovoApparato);
                }
            }
            xpObjectSpace.CommitChanges();

            //RefreshDati();
        }

        private void AppkCondizione_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            var xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            CAMS.Module.Classi.SetVarSessione.Esegui_DeSelezionaDati = false;
            if (xpObjectSpace != null)
            {
                var Oidk = int.Parse(e.SelectedChoiceActionItem.Data.ToString());

                if (View is ListView)
                {
                    if (View.Id == "Impianto_APPARATOes_ListView")
                    {
                        var lstApparatiSelezionati = (((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().ToList<Asset>();
                        foreach (Asset ApparatoCorrente in lstApparatiSelezionati)
                        {
                            var osApparatoCorrente = xpObjectSpace.GetObject(ApparatoCorrente);
                            AggiornaApparato(osApparatoCorrente, Oidk, "kCondizione", xpObjectSpace);
                        }
                    }
                }
                if (View is DetailView)
                {
                    if (View.Id == "Apparato_DetailView")
                    {
                        if (((DetailView)View).ViewEditMode == ViewEditMode.Edit)
                        {
                            var ApparatoCorrente = (Asset)View.CurrentObject;
                            AggiornaApparato(ApparatoCorrente, Oidk, "kCondizione", xpObjectSpace);
                        }
                    }
                }
                ((ListView)View).Editor.GetSelectedObjects().Clear();

                CAMS.Module.Classi.SetVarSessione.Esegui_DeSelezionaDati = true;
                CAMS.Module.Classi.SetVarSessione.Esegui_DeSelezionaDatiTHISListView = View.Id;
                View.ObjectSpace.Refresh();
            }
        }

        private void AggiornaApparato(Asset ApparatoCorrente, int Oidk, string TipoObj, IObjectSpace xpObjectSpace)
        {
            var AppCoeff = xpObjectSpace.GetObject<AssetkTempo>(ApparatoCorrente.AssetkTempo);
            if (AppCoeff == null)
            {
                AppCoeff = xpObjectSpace.CreateObject<AssetkTempo>();
            }
            if (TipoObj == "kCondizione")
            {
                var kc = xpObjectSpace.GetObjectByKey<KCondizione>(Oidk);
                AppCoeff.KCondizioneOid = kc.Oid;
                AppCoeff.KCondizioneDesc = kc.Descrizione;
                AppCoeff.KCondizioneValore = kc.Valore;
            }

            if (TipoObj == "kDimensione")
            {
                var kc = xpObjectSpace.GetObjectByKey<KDimensione>(Oidk);
                AppCoeff.KDimensioneOid = kc.Oid;
                AppCoeff.KDimensioneDesc = kc.Descrizione;
                AppCoeff.KDimensioneValore = kc.Valore;
            }
            if (TipoObj == "kGuasto")
            {
                var kc = xpObjectSpace.GetObjectByKey<KGuasto>(Oidk);
                AppCoeff.KGuastoOid = kc.Oid;
                AppCoeff.KGuastoDesc = kc.Descrizione;
                AppCoeff.KGuastoValore = kc.Valore;
            }
            if (TipoObj == "kUbicazione")
            {
                var kc = xpObjectSpace.GetObjectByKey<KUbicazione>(Oidk);
                AppCoeff.KUbicazioneOid = kc.Oid;
                AppCoeff.KUbicazioneDesc = kc.Descrizione;
                AppCoeff.KUbicazioneValore = kc.Valore;
            }
            AppCoeff.Utente = Application.Security.UserName;
            AppCoeff.DataAggiornamento = DateTime.Now;
            AppCoeff.Save();
            xpObjectSpace.CommitChanges();
            var CoefTotale = double.Parse(AppCoeff.EvaluateAlias("CoefficienteTotale").ToString());

            ApparatoCorrente.TotaleCoefficienti = CoefTotale;
            ApparatoCorrente.AssetkTempo = AppCoeff;
            ApparatoCorrente.Save();
            xpObjectSpace.CommitChanges();
        }

        private void ApparatoClonaApparato_CustomizePopupWindowParams(object sender, CustomizePopupWindowParamsEventArgs e)
        {
            var xpObjectSpace = Application.CreateObjectSpace();

            if (xpObjectSpace != null)
            {
                var NuovoAp = xpObjectSpace.CreateObject<ParametriApparato>();

                var ApparatoSelezionato = xpObjectSpace.GetObject((((ListView)View).Editor).GetSelectedObjects().Cast<Asset>().First());

                NuovoAp.Descrizione = ApparatoSelezionato.Descrizione;
                NuovoAp.NumeroCopie = 0;
                NuovoAp.VecchioApparato = ApparatoSelezionato;

                var view = Application.CreateDetailView(xpObjectSpace, NuovoApparato_DetailView, true, NuovoAp);
                view.ViewEditMode = ViewEditMode.Edit;
                e.View = view;
            }
        }

        private void ApparatoClonaApparato_Execute(object sender, PopupWindowShowActionExecuteEventArgs e)
        {
            var NuovoAp = ((ParametriApparato)((((DevExpress.ExpressApp.Frame)(e.PopupWindow)).View).CurrentObject));


            var db = new Classi.DB();
            db.CloneApparato(Classi.SetVarSessione.CorrenteUser, NuovoAp.VecchioApparato.Oid, NuovoAp.NumeroCopie, NuovoAp.Descrizione);
            db.Dispose();

            db.AggiornaTempi(NuovoAp.VecchioApparato.Servizio.Oid, "IMPIANTO");

            //RefreshDati();

            if (View is ListView)
            {
                if (View.Id == "Impianto_APPARATOes_ListView")
                {
                    var dvParent = (DetailView)(View.ObjectSpace).Owner;

                    if (dvParent.Id == "Impianto_DetailView")
                    {
                        dvParent.ObjectSpace.Refresh();
                        dvParent.Refresh();
                    }
                }
            }
        }

        private void saSostituisciApparato_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            //IObjectSpace xpObjectSpace = View is ListView ? Application.CreateObjectSpace() : View.ObjectSpace;
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();

            Asset ApparatodaSostituire = (Asset)e.CurrentObject;
            //var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(ApparatodaSostituire);

            //Apparato NuovoApparato = xpObjectSpace.CreateObject<Apparato>();
            //NuovoApparato = NuovoApparato.CloneFrom(ApparatodaSostituire);

            Asset NuovoApparato = SostituisciApparato(ApparatodaSostituire, xpObjectSpace);

            DetailView NewDv = Application.CreateDetailView(xpObjectSpace, "Apparato_DetailView_Gestione", true, NuovoApparato);
            if (NewDv != null)
            {
                NewDv.Caption = string.Format("Asset in Sostituzione dell'Asset: ", ApparatodaSostituire.ToString());
                NewDv.ViewEditMode = ViewEditMode.Edit;
                e.ShowViewParameters.CreatedView = NewDv;
                e.ShowViewParameters.TargetWindow = TargetWindow.Default;
            }

            MessageOptions options = new MessageOptions();
            options.Duration = 3000;
            options.Message = string.Format("il nuovo Apparato assumerà il codice dell'apparato precedente mantenendo una sua propria identità.");
            options.Web.Position = InformationPosition.Top;
            options.Type = InformationType.Success;
            options.Win.Caption = "Avviso";
            //options.CancelDelegate = CancelDelegate;
            //options.OkDelegate = OkDelegate;
            Application.ShowViewStrategy.ShowMessage(options);

        }

        private Asset SostituisciApparato(Asset apparatoSelezionato, IObjectSpace xpObjectSpace)
        {
            Asset NuovoApparato = xpObjectSpace.CreateObject<Asset>();

            NuovoApparato.CodDescrizione = apparatoSelezionato.CodDescrizione;
            NuovoApparato.Descrizione = apparatoSelezionato.Descrizione;
            NuovoApparato.StdAsset = xpObjectSpace.GetObject<StdAsset>(apparatoSelezionato.StdAsset);
            NuovoApparato.Servizio = xpObjectSpace.GetObject<Servizio>(apparatoSelezionato.Servizio);
            NuovoApparato.Locale = xpObjectSpace.GetObject<Locali>(apparatoSelezionato.Locale);
            NuovoApparato.Strada = xpObjectSpace.GetObject<Strade>(apparatoSelezionato.Strada);
            NuovoApparato.GeoLocalizzazione = xpObjectSpace.GetObject<GeoLocalizzazione>(apparatoSelezionato.GeoLocalizzazione);

            NuovoApparato.DataSheet = apparatoSelezionato.DataSheet;
            NuovoApparato.Quantita = apparatoSelezionato.Quantita;

            NuovoApparato.Marca = apparatoSelezionato.Marca;
            NuovoApparato.Modello = apparatoSelezionato.Modello;
            NuovoApparato.CarattTecniche = apparatoSelezionato.CarattTecniche;
            NuovoApparato.Note = apparatoSelezionato.Note;
            NuovoApparato.Matricola = apparatoSelezionato.Matricola;
            NuovoApparato.EntitaAsset = apparatoSelezionato.EntitaAsset;
            NuovoApparato.Tag = apparatoSelezionato.Tag;
   

            NuovoApparato.KeyPlans = apparatoSelezionato.KeyPlans;
            NuovoApparato.AssetoPadre = xpObjectSpace.GetObject<Asset>(apparatoSelezionato.AssetoPadre);
            NuovoApparato.AssetSostegno = xpObjectSpace.GetObject<Asset>(apparatoSelezionato.AssetSostegno);
            NuovoApparato.AssetMP = xpObjectSpace.GetObject<Asset>(apparatoSelezionato.AssetMP);

            NuovoApparato.Abilitato = apparatoSelezionato.Abilitato;
            NuovoApparato.AbilitazioneEreditata = apparatoSelezionato.AbilitazioneEreditata;

            NuovoApparato.AssetkTempo = getApparatokTempo(xpObjectSpace);
            var CoefTotale = double.Parse(NuovoApparato.AssetkTempo.EvaluateAlias("CoefficienteTotale").ToString());
            NuovoApparato.TotaleCoefficienti = CoefTotale;

            //= apparatoSelezionato.ApparatokTempo;
            //NuovoApparato.TotaleCoefficienti = apparatoSelezionato.TotaleCoefficienti;
            NuovoApparato.DataLettura = apparatoSelezionato.DataLettura;
            //NuovoApparato.ValoreUltimaLettura = apparatoSelezionato.ValoreUltimaLettura;
            NuovoApparato.OreMedieSetEsercizio = apparatoSelezionato.OreMedieSetEsercizio;

            NuovoApparato.Utente = SecuritySystem.CurrentUserName.ToString();
            NuovoApparato.DataAggiornamento = DateTime.Now;

            //NuovoApparato.ApparatoSostituito = apparatoSelezionato;
            //NuovoApparato.OidApparatoSostituito = apparatoSelezionato.Oid;
            Asset AppDASostituireConIlNuovo = xpObjectSpace.GetObject<Asset>(apparatoSelezionato);

            NuovoApparato.AssetSostituito = AppDASostituireConIlNuovo;

            AppDASostituireConIlNuovo.Abilitato = FlgAbilitato.No;
            AppDASostituireConIlNuovo.DateUnService = DateTime.Now;
            AppDASostituireConIlNuovo.DataAggiornamento = DateTime.Now;
            AppDASostituireConIlNuovo.AssetSostituitoDa = NuovoApparato;
            AppDASostituireConIlNuovo.CodDescrizione = string.Format("{0}(sost)", AppDASostituireConIlNuovo.CodDescrizione);
            //
            return NuovoApparato;
        }

        private AssetkTempo getApparatokTempo(IObjectSpace xpObjectSpace)
        {
            var AppCoeff = xpObjectSpace.CreateObject<AssetkTempo>();

            //var lstKcon = new XPCollection<KCondizione>(nuovoApparato.Session);
            KCondizione kc = xpObjectSpace.GetObjects<KCondizione>().Where(w => w.Default == KDefault.Si).First();
            //var kc = lstKcon.FirstOrDefault(cond => cond.Default == KDefault.Si);
            AppCoeff.KCondizioneOid = kc.Oid;
            AppCoeff.KCondizioneDesc = kc.Descrizione;
            AppCoeff.KCondizioneValore = kc.Valore;

            //var lstKgua = new XPCollection<KGuasto>(nuovoApparato.Session);
            //var kg = lstKgua.FirstOrDefault(cond => cond.Default == KDefault.Si);
            KGuasto kg = xpObjectSpace.GetObjects<KGuasto>().Where(w => w.Default == KDefault.Si).First();
            AppCoeff.KGuastoOid = kg.Oid;
            AppCoeff.KGuastoDesc = kg.Descrizione;
            AppCoeff.KGuastoValore = kg.Valore;

            //var lstKubi = new XPCollection<KUbicazione>(nuovoApparato.Session);
            //var ku = lstKubi.FirstOrDefault(cond => cond.Default == KDefault.Si);

            KUbicazione ku = xpObjectSpace.GetObjects<KUbicazione>().Where(w => w.Default == KDefault.Si).First();
            AppCoeff.KUbicazioneOid = ku.Oid;
            AppCoeff.KUbicazioneDesc = ku.Descrizione;
            AppCoeff.KUbicazioneValore = ku.Valore;
            AppCoeff.Utente = Application.Security.UserName;
            AppCoeff.DataAggiornamento = DateTime.Now;
            AppCoeff.Save();
            //xpObjectSpace.CommitChanges();


            return AppCoeff;

        }

        private void scaFiltroTipoApparato_Execute(object sender, SingleChoiceActionExecuteEventArgs e)
        {
            if (View.Id == "Impianto_APPARATOes_ListView")
            {
                var dvParent = (DetailView)(View.ObjectSpace).Owner;
                if (dvParent.Id == "Impianto_DetailView")
                {
                    ((ListView)View).CollectionSource.BeginUpdateCriteria();
                    ((ListView)View).CollectionSource.Criteria.Clear();

                    ((ListView)View).CollectionSource.Criteria[e.SelectedChoiceActionItem.Caption] =
                       CriteriaEditorHelper.GetCriteriaOperator(
                       e.SelectedChoiceActionItem.Data as string, View.ObjectTypeInfo.Type, ObjectSpace);

                    ((ListView)View).CollectionSource.EndUpdateCriteria();

                }
            }
        }

        private void saVisualizza_Execute(object sender, SimpleActionExecuteEventArgs e)
        {
            IObjectSpace xpObjectSpace = Application.CreateObjectSpace();
            if (xpObjectSpace != null)
            {
                Asset NuovoApparato = xpObjectSpace.GetObject<Asset>((Asset)e.CurrentObject);

                DetailView NewDv = Application.CreateDetailView(xpObjectSpace, "Apparato_DetailView_Gestione", true, NuovoApparato);
                if (NewDv != null)
                {
                    NewDv.Caption = string.Format("Apparato in visualizzazione dettaglio: ", NuovoApparato.ToString());
                    NewDv.ViewEditMode = ViewEditMode.View;
                    Application.MainWindow.SetView(NewDv);
                    //e.ShowViewParameters.CreatedView = NewDv;
                    //e.ShowViewParameters.TargetWindow = TargetWindow.Default;
                }
            }
        }

    }
}
