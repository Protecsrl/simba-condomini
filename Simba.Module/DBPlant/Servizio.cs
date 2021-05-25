using CAMS.Module.Classi;
using CAMS.Module.Costi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.DBDocument;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant.Coefficienti;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.PropertyEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections;
using System.Diagnostics;
using System.Drawing;
using System.Linq;

//Abilitato
namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("SERVIZIO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impianti")]
    [ImageName("Action_EditModel")]
    [Appearance("Servizio.inCreazione.noVisibile", TargetItems = "APPARATOes;ControlliNormativis;DestinatariControlliNormativis;Documentis;ServizioMansioneCaricos;RegistroCostis;RegMisures;VisibileListaAppInseribili;", Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Servizio.dopoCreazione.noEdit", TargetItems = "Immobile", Criteria = @"Oid != -1", Enabled = false)]

    // [Appearance("Servizio.BackColor.Salmon", TargetItems = "*", BackColor = "Salmon", FontColor = "Black", Priority = 1, Criteria = "SumTempoMp = 0")]
    [Appearance("Servizio.BackColor.Salmon", TargetItems = "*", FontStyle = FontStyle.Bold, FontColor = "Blue", Priority = 1, Criteria = "NumApp = 0")]

    [Appearance("Servizio.Abilitato.No", TargetItems = "*;Abilitato;DateUnService", Enabled = false, Criteria = "Abilitato = 'No' Or Immobile.Abilitato = 'No'")]
    [Appearance("Servizio.Abilitato.BackColor", TargetItems = "*;Abilitato;DateUnService",
        FontStyle = FontStyle.Strikeout, FontColor = "Salmon", Priority = 1,
        Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]

    [RuleCombinationOfPropertiesIsUnique("Unique.Servizio.Descrizione", DefaultContexts.Save, "Immobile,CodDescrizione, Descrizione")]
    [DeferredDeletion(true)]

    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion

    [NavigationItem("Patrimonio")]
    public class Servizio : XPObject
    {
        private const string NA = "N/A";
        private const string FormattazioneCodice = "{0:00}";
        public Servizio() : base() { }
        public Servizio(Session session) : base(session) { }

        #region Metodi ed eventi
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                if (this.Immobile != null)
                {
                    this.Descrizione = "Nuovo Servizio";
                    this.CodDescrizione = this.Immobile.CodDescrizione + "_";
                }
                this.Abilitato = FlgAbilitato.Si;
                this.AbilitazioneEreditata = FlgAbilitato.Si;
            }


        }
        protected override void OnDeleting()
        {
            base.OnDeleting();
            //SetVarSessione.OidImmobileDaAggiornare = Immobile.Oid;
            //foreach (object aggregated in new ArrayList(APPARATOes))
            //{
            //    Session.Delete(aggregated);
            //}
        }
        protected override void OnSaving()
        {
            //if (!IsDeleted)
            //{
            //    if (LibreriaDiServizio != null)
            //    {
            //        // CreateFrom(LibreriaDiServizio);
            //    }
            //    if (CodDescrizione == NA || String.IsNullOrEmpty(CodDescrizione))
            //    {
            //        //Sistema SisLImp = LibreriaDiServizio.Sistema;
            //        //this.Sistema = SisLImp;
            //        //CodDescrizione = String.Format("{0}{1}_{2}", Commesse.CodDescrizione, Immobile.CodDescrizione, SisLImp.CodDescrizione);
            //        //CodDescrizione += String.Format(FormattazioneCodice,
            //        //                  Convert.ToInt32(Session.Evaluate<Servizio>(CriteriaOperator.Parse("Count"),
            //        //                    new BinaryOperator(
            //        //                    new OperandProperty("CodDescrizione"), new OperandValue(String.Format("{0}{1}_%", Commesse.CodDescrizione, Immobile.CodDescrizione)),
            //        //                    BinaryOperatorType.Like))) + 1);

            //        var DataAggiornamento = DateTime.Now;

            //        //for (var i = 0; i < APPARATOes.Count; i++)
            //        //{
            //        //    APPARATOes[i].CodDescrizione = String.Format("{0}_{1}_{2}",
            //        //                              CodDescrizione, APPARATOes[i].StdApparato.CodDescrizione,
            //        //                              String.Format(Apparato.FormattazioneCodice, i + 1));

            //        //    APPARATOes[i].DataAggiornamento = DataAggiornamento;
            //        //    APPARATOes[i].Utente = SecuritySystem.CurrentUserName.ToString();
            //        //}
            //    }
            //    //if (NumeroCopie > 0)
            //    //{
            //    //    CopyFrom(this, NumeroCopie);

            //    //}

            //    Debug.Print(this.Immobile.Descrizione);
            //}
        }
        /// Clona un Servizio dal un'altro Servizio
        /// <param name="servizioSelezionato"></param>
        public Servizio CloneFrom(Servizio servizioSelezionato)
        {
            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

            Descrizione = servizioSelezionato.Descrizione;
            Immobile = servizioSelezionato.Immobile;
            Zona = servizioSelezionato.Zona;
            // Commesse = ServizioSelezionato.Commesse;
            Sistema = servizioSelezionato.Sistema;

            foreach (Asset apparato in servizioSelezionato.APPARATOes)
            {
                var NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                NuovoApparato.CloneFrom(apparato);

                NuovoApparato.InsertSchedeMPsuAsset(ref NuovoApparato, apparato.StdAsset.Oid);

                APPARATOes.Add(NuovoApparato);
            }

            return this;
        }
        /// Clona l'Servizio nVolte
        /// <param name="servizioSelezionato">Servizio selezionato</param><param name="nCopie">Numero di volte di quanto copiare il servizio</param>
        public void CopyFrom(Servizio servizioSelezionato, uint nCopie)
        {
            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

            var Conta = Convert.ToInt32(Session.Evaluate<Servizio>(CriteriaOperator.Parse("Count"),
                                    new BinaryOperator(
                                    new OperandProperty("CodDescrizione"), new OperandValue(String.Format("{0}{1}_%", Commesse.CodDescrizione, Immobile.CodDescrizione)),
                                    BinaryOperatorType.Like))) + 1;

            for (var i = 0u; i < nCopie; i++)
            {
                var NuovoServizio = xpObjectSpace.CreateObject<Servizio>();

                NuovoServizio.CodDescrizione = String.Format("{0}{1}_{2}", Commesse.CodDescrizione, Immobile.CodDescrizione, Sistema.CodDescrizione);
                NuovoServizio.CodDescrizione += String.Format(FormattazioneCodice,
                                 Conta + i + 1);


                NuovoServizio.Descrizione = String.Concat(servizioSelezionato.Descrizione, String.Format(" - Copia {0}", i + 1));
                NuovoServizio.Immobile = servizioSelezionato.Immobile;
                NuovoServizio.Zona = servizioSelezionato.Zona;
                //   NuovoServizio.Commesse = ServizioSelezionato.Commesse;
                NuovoServizio.Sistema = servizioSelezionato.Sistema;

                foreach (Asset apparato in servizioSelezionato.APPARATOes)
                {
                    var NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                    NuovoApparato.CloneFrom(apparato);
                    NuovoApparato.CodDescrizione = String.Format("{0}_{1}_{2}",
                                        NuovoServizio.CodDescrizione, NuovoApparato.StdAsset.CodDescrizione,
                                        String.Format(Asset.FormattazioneCodice, i + 1));

                    NuovoApparato.InsertSchedeMPsuAsset(ref NuovoApparato, apparato.StdAsset.Oid);

                    NuovoServizio.APPARATOes.Add(NuovoApparato);
                }
            }
        }
        /// Crea un Servizio da libreria
        /// <param name="ServizioLibrarySelezionato"></param><returns></returns>
        public Servizio CreateFrom(ServizioLibrary ServizioLibrarySelezionato)
        {
            Sistema = ServizioLibrarySelezionato.Sistema;
            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

            if (ServizioLibrarySelezionato.QuantitaApparati > 0)
            {
                var lstImpiantiDettaglio = ServizioLibrarySelezionato.SERVIZIOLIBRARYDETTAGLIOs.Where(r => r.StdApparato != null);

                //xpObjectSpace.GetObjects<KCondizione>().FirstOrDefault(r => r.Default == KDefault.Si);
                //xpObjectSpace.GetObjects<KDimensione>().FirstOrDefault(r => r.Default == KDefault.Si);
                //xpObjectSpace.GetObjects<KTrasferimento>().FirstOrDefault(r => r.Default == KDefault.Si);
                //xpObjectSpace.GetObjects<KUbicazione>().FirstOrDefault(r => r.Default == KDefault.Si);
                //xpObjectSpace.GetObjects<KUtenza>().FirstOrDefault(r => r.Default == KDefault.Si);
                //xpObjectSpace.GetObjects<KGuasto>().FirstOrDefault(r => r.Default == KDefault.Si);

                foreach (ServizioLibraryDettaglio servizioDettaglio in lstImpiantiDettaglio)
                {
                    // qui creo coefficienti
                    var AppCoeff = xpObjectSpace.CreateObject<AssetkTempo>();

                    //var lstKcon = new XPCollection<KCondizione>(this.Session);
                    //var kc = lstKcon.FirstOrDefault(cond => cond.Default == KDefault.Si);
                    //AppCoeff.KCondizioneOid = kc.Oid;
                    //AppCoeff.KCondizioneDesc = kc.Descrizione;
                    //AppCoeff.KCondizioneValore = kc.Valore;

                    //var lstKgua = new XPCollection<KGuasto>(this.Session);
                    //var kg = lstKgua.FirstOrDefault(cond => cond.Default == KDefault.Si);
                    //AppCoeff.KGuastoOid = kg.Oid;
                    //AppCoeff.KGuastoDesc = kg.Descrizione;
                    //AppCoeff.KGuastoValore = kg.Valore;

                    //var lstKubi = new XPCollection<KUbicazione>(this.Session);
                    //var ku = lstKubi.FirstOrDefault(cond => cond.Default == KDefault.Si);
                    //AppCoeff.KUbicazioneOid = ku.Oid;
                    //AppCoeff.KUbicazioneDesc = ku.Descrizione;
                    //AppCoeff.KUbicazioneValore = ku.Valore;

                    //var lstKubi = new XPCollection<KUbicazione>(this.Session).FirstOrDefault;
                    //var ku = lstKubi.FirstOrDefault(cond => cond.Default == KDefault.Si);
                    //AppCoeff.KUbicazioneOid = ku.Oid;
                    //AppCoeff.KUbicazioneDesc = ku.Descrizione;
                    //AppCoeff.KUbicazioneValore = ku.Valore;

                    var CoefTotale = double.Parse(AppCoeff.EvaluateAlias("CoefficienteTotale").ToString());

                    // qui creo lapparato
                    var NuovoApparato = xpObjectSpace.CreateObject<Asset>();
                    NuovoApparato.Descrizione = servizioDettaglio.StdApparato.Descrizione;
                    NuovoApparato.Quantita = servizioDettaglio.Quantita;
                    NuovoApparato.StdAsset = servizioDettaglio.StdApparato;
                    NuovoApparato.TotaleCoefficienti = CoefTotale;
                    NuovoApparato.AssetkTempo = AppCoeff;

                    /// ----------------------------------------------------------  qui da sistemare coefficienti

                    NuovoApparato.InsertSchedeMPsuAsset(ref NuovoApparato, servizioDettaglio.StdApparato.Oid);
                    APPARATOes.Add(NuovoApparato);
                }
            }

            return this;
        }

        protected override void OnSaved()
        {
            base.OnSaved();
            if (CreaDaLibreriaImpianti == true)
            {
                if (LibreriaDiServizio != null)
                {
                    using (DB db = new DB())
                    {
                        int NCopie = int.Parse(NumeroCopie.ToString());
                        db.CreaServiziobyServizioLibrary(Oid, NCopie, LibreriaDiServizio.Oid);
                    }
                }
            }
        }

        protected override void OnDeleted()
        {
            base.OnDeleted();
            Session.CommitTransaction();
            var db = new DB();
            db.AggiornaTempi(SetVarSessione.OidEdificioDaAggiornare, "IMMOBILE");
            SetVarSessione.OidEdificioDaAggiornare = 0;
        }
        #endregion

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(250)]
        [DbType("varchar(250)")]
        [RuleRequiredField("Servizio.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //  [RuleUniqueValue("UniqReg.Descrizione", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, CustomMessageTemplate = "Questo Campo deve essere Univoco")]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(50), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        // Appearance("Servizio.CodDescrizione", Enabled = false)]
        [RuleRequiredField("Servizio.CodDescrizione", DefaultContexts.Save, "Il Cod. Descrizione è un campo obbligatorio")]
        // [RuleUniqueValue("UniqReg.CodDescrizione", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, CustomMessageTemplate = "Questo Campo deve essere Univoco")]
        [DbType("varchar(50)")]
        public string CodDescrizione
        {
            get
            {
                //if (!IsLoading && !IsSaving && fCodDescrizione == null)
                //{
                //    fCodDescrizione = NA;
                //}
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

        [PersistentAlias("Iif(Immobile is not null,Immobile.Commesse,null)"), DevExpress.Xpo.DisplayName("Commessa")]
        [Appearance("Servizio.Commesse", Enabled = false, Criteria = "not(Immobile is null)", Context = "DetailView")]
        public Contratti Commesse
        {
            get
            {
                return (Contratti)EvaluateAlias("Commesse");
                //var tempObject = EvaluateAlias("Commesse");
                //if (tempObject != null)
                //{
                //    return (Commesse)tempObject;
                //}
                //else
                //{
                //    return null;
                //}
            }
        }


        private Immobile fImmobile;
        [Association(@"Immobile_Impianti")]
        [Persistent("Immobile"), DevExpress.Xpo.DisplayName("Immobile")]
        [RuleRequiredField("Servizio.Immobile", DefaultContexts.Save, "L'Immobile è un campo obbligatorio")]
        [Appearance("Servizio.Immobile", Enabled = false, Criteria = "Commesse is null", Context = "DetailView")]
        [DataSourceCriteria("Commesse = '@This.Commesse'")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }

            
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"),
        DevExpress.Xpo.DisplayName("Unità Tecnologica")]
        [RuleRequiredField("Servizio.Sistema", DefaultContexts.Save, "Il sistema è un campo obbligatorio", TargetCriteria = "CreaDaLibreriaImpianti = false")]
        [Appearance("Servizio.Sistema", Enabled = false, Criteria = "Sistema != null", Context = "DetailView")]
        [DataSourceCriteria("SistemaClassi.SistemaTecnologico = '@This.Immobile.SistemaTecnologico'")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Sistema Sistema
        {
            get { return GetDelayedPropertyValue<Sistema>("Sistema"); }
            set { SetDelayedPropertyValue<Sistema>("Sistema", value); }
        }
        private string fZona;
        [Size(150), Persistent("ZONA"), DevExpress.Xpo.DisplayName("Zona")]
        [DbType("varchar(150)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Zona
        {
            get { return GetDelayedPropertyValue<string>("Zona"); }
            set { SetDelayedPropertyValue<string>("Zona", value); }
            //get
            //{
            //    return fZona;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Zona", ref fZona, value);
            //}
        }


        [Persistent("COUNTAPPARATI")]
        private int fNumApp;

        [PersistentAlias("fNumApp")]
        [DevExpress.Xpo.DisplayName(@"N° Apparati")]
        public int NumApp
        {
            get
            {
                return fNumApp;
            }
        }

        [Persistent("SUMTEMPOSCHEDEMP")]
        private int fSumTempoMp;
        [PersistentAlias("fSumTempoMp")]
        [DevExpress.Xpo.DisplayName(@"Somma Tempo Attività [min.]")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DetailViewLayoutAttribute(LayoutColumnPosition.Left)]
        public int SumTempoMp
        {
            get
            {
                return fSumTempoMp;
            }
        }

        [Persistent("COUNTAPPARATIGEO")]
        private int fNumAppGeo;

        [PersistentAlias("fNumAppGeo")]
        [DevExpress.Xpo.DisplayName(@"N° Apparati Geolocalizzati")]
        public int NumAppGeo
        {
            get
            {
                return fNumAppGeo;
            }
        }



        [Size(1000), Persistent("DATASHEET"), DevExpress.Xpo.DisplayName("Data Sheet")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData DataSheet
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("DataSheet");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("DataSheet", value);
            }
        }

        private string fKUtenza;
        //[NonPersistent]
        [PersistentAlias("Iif(Oid > 0, [<Apparato>][^.Oid == Servizio].Single(ApparatokTempo.KUtenzaDesc),'NA')")]
        [VisibleInLookupListView(false), VisibleInListView(true), VisibleInDetailView(true)]
        public string KUtenza
        {
            get
            {
                object tempObject = EvaluateAlias("KUtenza");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
                //try
                //{
                //    fKUtenza = Session.Query<Apparato>().Where(w => w.Servizio == this)                        
                //        .Select(s => string.Format("{0} {1}", 
                //            s.ApparatokTempo.KUtenzaDesc,
                //            s.ApparatokTempo.KUtenzaValore.ToString()))
                //        .Distinct().First();

                //    //fKUtenza = APPARATOes.Select(s => string.Format("{0} {1}", s.ApparatokTempo.KUtenzaDesc, s.ApparatokTempo.KUtenzaValore.ToString())).First();
                //}
                //catch
                //{
                //    return "nd";
                //}
                //return fKUtenza;
            }

        }

        #region abilitazione Immobile
        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }
        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [Appearance("Servizio.Abilita.DateUnService", Criteria = "Abilitato = 'Si'", Enabled = false)]
        [RuleRequiredField("Servizio.Abilita.DateUnService.Obblig", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "Abilitato = 'No'")]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public DateTime DateUnService
        {
            get { return GetDelayedPropertyValue<DateTime>("DateUnService"); }
            set { SetDelayedPropertyValue<DateTime>("DateUnService", value); }

            //get
            //{
            //    return fDateUnService;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DateUnService", ref fDateUnService, value);
            //}
        }

        private FlgAbilitato fAbilitazioneEreditata;
        [Persistent("ABILITAZIONETRASMESSA"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo da Gerarchia")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [Appearance("Servizio.AbilitazioneEreditata", FontColor = "Black", Enabled = false)]
        public FlgAbilitato AbilitazioneEreditata
        {
            get
            {
                return fAbilitazioneEreditata;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("AbilitazioneEreditata", ref fAbilitazioneEreditata, value);
            }
        }

        #endregion

        #region CLUSTEREDIFICI E SCENRIO

        [Persistent("SCENARIO"), DevExpress.Xpo.DisplayName("Scenario")]
        [MemberDesignTimeVisibility(false)]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Scenario Scenario
        {
            get
            {
                return GetDelayedPropertyValue<Scenario>("Scenario");
            }
            set
            {
                SetDelayedPropertyValue<Scenario>("Scenario", value);
            }
        }

        #endregion

        #region associazioni con altre classi


        #endregion

        #region PARAMETRI SCHEDULAZIONE
        //[PersistentAlias("ClusterEdifici.Presidiato"),
        //DevExpress.Xpo.DisplayName("Presidiato")]
        //[VisibleInListView(false)]
        //public Presidiato Presidiato
        //{
        //    get
        //    {

        //        var tempObject = EvaluateAlias("Presidiato");
        //        if (tempObject != null)
        //        {
        //            return (Presidiato)tempObject;
        //        }
        //        else
        //        {
        //            return Presidiato.NonDefinito;
        //        }
        //    }
        //}
        #endregion

        #region campi per PopUp
        ServizioLibrary fLibreriaDiServizio;
        [RuleRequiredField("Servizio.LibreriaDiServizio", DefaultContexts.Save, "Il tipo di liberia è un campo obbligatorio", TargetCriteria = "CreaDaLibreriaImpianti = true")]
        [NonPersistent]
        [DataSourceCriteria("Sistema.SistemaClassi.SistemaTecnologico = '@This.Immobile.SistemaTecnologico'")]
        [Appearance("Servizio.LibreriaDiServizio.visibil", Criteria = "CreaDaLibreriaImpianti = false", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide, Context = "DetailView")]
        [VisibleInListView(false)]
        public ServizioLibrary LibreriaDiServizio //{ get; set; }
        {
            get
            {

                return fLibreriaDiServizio;
            }
            set
            {
                SetPropertyValue<ServizioLibrary>("LibreriaDiServizio", ref fLibreriaDiServizio, value);
            }
        }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public uint NumeroCopie { get; set; }

        bool fCreaDaLibreriaImpianti;
        [System.ComponentModel.DefaultValue(false), System.ComponentModel.Browsable(false)]
        [NonPersistent]
        public bool CreaDaLibreriaImpianti // { get; set; }
        {
            get
            {
                return fCreaDaLibreriaImpianti;
            }
            set
            {
                SetPropertyValue<bool>("CreaDaLibreriaImpianti", ref fCreaDaLibreriaImpianti, value);
            }
        }

        [System.ComponentModel.DefaultValue(false)]
        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public bool VisibileListaAppInseribili { get; set; }


        #endregion

        #region associazioni con altre classi
        [Association(@"SERVIZIORefASSET", typeof(Asset)), Aggregated, DevExpress.Xpo.DisplayName("Apparato")]
        [ExplicitLoading()]
        public XPCollection<Asset> APPARATOes
        {
            get
            {
                return GetCollection<Asset>("APPARATOes");
            }
        }


     [Persistent("APPARATO_DEFAULT"), DevExpress.Xpo.DisplayName("Apparato non definito")]
     [VisibleInListView(false),VisibleInLookupListView(true),VisibleInDetailView(true)]
        [Delayed(true)]
        public Asset ApparatoDefault
        {
            get
            {
                return GetDelayedPropertyValue<Asset>("ApparatoDefault");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("ApparatoDefault", value);
            }
        }

        [Association(@"Servizio_MansioneCarico", typeof(ServizioMansioneCarico)),
             DevExpress.Xpo.DisplayName("Carico per Mansione")]
        [Appearance("Servizio.ServizioMansioneCarico", Enabled = false)]
        [VisibleInListView(false),
        VisibleInLookupListView(false),
        VisibleInDetailView(true)]
        [ExplicitLoading()]
        public XPCollection<ServizioMansioneCarico> ServizioMansioneCaricos
        {
            get
            {
                return GetCollection<ServizioMansioneCarico>("ServizioMansioneCaricos");
            }
        }



        [Association(@"RegistroLavori_Servizio", typeof(RegistroLavori)),
        DevExpress.Xpo.DisplayName("Registro Costi")]
        [ExplicitLoading()]
        public XPCollection<RegistroLavori> RegistroLavoris
        {
            get
            {
                return GetCollection<RegistroLavori>("RegistroLavoris");
            }
        }

        [Association(@"ControlliNormativi_Servizio", typeof(ControlliNormativi)),
               DevExpress.Xpo.DisplayName("Avvisi Periodici")]
        [ExplicitLoading()]
        public XPCollection<ControlliNormativi> ControlliNormativis
        {
            get
            {
                return GetCollection<ControlliNormativi>("ControlliNormativis");
            }
        }

        [Association(@"RegMisure_Servizio", typeof(RegMisure)),
               DevExpress.Xpo.DisplayName("Registro Misure")]
        [ExplicitLoading()]
        public XPCollection<RegMisure> RegMisures
        {
            get
            {
                return GetCollection<RegMisure>("RegMisures");
            }
        }

        [Association(@"DestinatariControlliNormativi_Servizio", typeof(DestinatariControlliNormativi)),
               DevExpress.Xpo.DisplayName("Destinatari Avvisi")]
        [ExplicitLoading()]
        public XPCollection<DestinatariControlliNormativi> DestinatariControlliNormativis
        {
            get
            {
                return GetCollection<DestinatariControlliNormativi>("DestinatariControlliNormativis");
            }
        }

        [Association(@"Documenti_Servizio", typeof(Documenti)), DevExpress.Xpo.DisplayName("Documenti")]
        [ExplicitLoading()]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }


        [Association(@"Planimetrie_Servizio", typeof(Planimetrie)), DevExpress.Xpo.DisplayName("Planimetrie")]
        [ExplicitLoading()]
        public XPCollection<Planimetrie> Planimetries
        {
            get
            {
                return GetCollection<Planimetrie>("Planimetries");
            }
        }
        #endregion

        #region
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "Abilitato")
                {
                    FlgAbilitato newV = (FlgAbilitato)(newValue);
                    if (newV == FlgAbilitato.Si)
                    {
                        this.DateUnService = DateTime.MinValue;
                    }

                    foreach (Asset Ap in this.APPARATOes)
                    {
                        Ap.AbilitazioneEreditata = newV;

                        foreach (AssetSchedaMP ApSK in Ap.AppSchedaMpes)
                        {
                            ApSK.AbilitazioneEreditata = newV;
                        }
                    }


                }
                if (this.Oid == -1)
                {
                    if (newValue != null && propertyName == "Immobile")
                    {
                        Immobile newV = (Immobile)(newValue);
                        if (newV != null)
                        {
                            if (this.CodDescrizione == null)
                            {
                                this.CodDescrizione = newV.CodDescrizione;
                            }

                            if (this.Descrizione == null)
                            {
                                string nrcl = newV.Impianti.Count.ToString();
                                this.Descrizione = "Nuovo Servizio  nr " + nrcl + "("+CodDescrizione+")";
                            }
                        }
                    }
                }
            }
        }


        #endregion
        #region utente e data aggiornamento

        


        private string f_Utente;
        [Persistent("UTENTE"), Size(100), DevExpress.Xpo.DisplayName("Utente")]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(100)")]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }

            //get
            //{
            //    return f_Utente;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Utente", ref f_Utente, value);
            //}
        }

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public DateTime? DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime?>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime?>("DataAggiornamento", value); }
            //get
            //{            //    return fDataAggiornamento;            //}
            //set
            //{            //    SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);            //}
        }

        #endregion


        private string fGruppo;
        [Persistent("GRUPPO"), DevExpress.Xpo.DisplayName("Gruppo")]
        [DbType("varchar(20)")]
       
        //[VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        public string Gruppo
        {
            get {
                return fGruppo;

              
            }
            set { SetPropertyValue<string>("Gruppo", ref fGruppo, value); }

        }


        private StatoSirvizioServizio fStatoSirvizioServizio;
        [Persistent("STATOSERVIZIOSERVIZIO"), DevExpress.Xpo.DisplayName("Stato Servizio Servizio")]
        //[ExplicitLoading()]//, 
        //[Delayed(true)]
        public StatoSirvizioServizio StatoSirvizioServizio
        {
            get
            {
                return fStatoSirvizioServizio;
            }
            set
            {
                SetPropertyValue<StatoSirvizioServizio>("StatoSirvizioServizio", ref fStatoSirvizioServizio, value);
            }
        }

        private StatoTLCServizio fStatoTLCServizio;
        [Persistent("STATOTLC"), DevExpress.Xpo.DisplayName("Stato Servizio TLC")]
        //[ExplicitLoading()]//, 
        //[Delayed(true)]
        public StatoTLCServizio StatoTLCServizio
        {
            get
            {
                return fStatoTLCServizio;
            }
            set
            {
                SetPropertyValue<StatoTLCServizio>("StatoTLCServizio", ref fStatoTLCServizio, value);
            }
        }


        private FlgAbilitato fServizioGeoreferenziato;
        [Persistent("SERVIZIOGEREFERENZIATO"), DevExpress.Xpo.DisplayName("Servizio Georeferenziato")]
        [VisibleInListView(false)]
        [ExplicitLoading()]//, Delayed(true)
        public FlgAbilitato ServizioGeoreferenziato
        {
            get
            {
                return fServizioGeoreferenziato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("ServizioGeoreferenziato", ref fServizioGeoreferenziato, value);
            }
        }

        [NonPersistent]
        private XPCollection<AssetoMap> fApparatoMaps;
        [DevExpress.ExpressApp.DC.XafDisplayName("Mappa")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [Appearance("GeoLocalizzazione.GeoLocalizzazioneMaps.visible", Criteria = "ServizioGeoreferenziato != 'Si'", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<AssetoMap> ApparatoMaps
        {
            get
            {
                if (this.Oid == -1) return null;
                if (this.NumApp == 0) return null;
                if (this.ServizioGeoreferenziato == FlgAbilitato.No) return null;
                string ParCriteria = string.Format("OidServizio == {0}", Evaluate("Oid"));
                fApparatoMaps = new XPCollection<AssetoMap>(Session, CriteriaOperator.Parse(ParCriteria));

                return fApparatoMaps;
            }
        }


    }
}


 