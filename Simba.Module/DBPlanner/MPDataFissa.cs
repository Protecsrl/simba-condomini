using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;

using DevExpress.Data.Filtering;
using System.Linq;
using CAMS.Module.Classi;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPDATAFISSA")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impostazione data Fissa")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Impostazione Data Fissa")]
    [RuleCombinationOfPropertiesIsUnique("UniqMPDataFissa", DefaultContexts.Save, "RegPianificazioneMP;Apparato,ApparatoSchedaMP", SkipNullOrEmptyValues = false)]
    [ImageName("Today")]
    //[NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDataFissa : XPObject
    {
        public MPDataFissa()
            : base()
        {
        }

        public MPDataFissa(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

            this.Descrizione = "Nuova Data Fissa";
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(100)]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.MPDataFissa.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[ImmediatePostData]
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

        private RegPianificazioneMP fRegPianificazioneMP;
        //[MemberDesignTimeVisibility(false)]
        [Association(@"RegPianoMP_MPDataFissa", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANO"), DevExpress.Xpo.DisplayName("Registro Pianificazione")]
        [Appearance("MPDataFissa.RegPianificazioneMP", Enabled = false)]
        [ImmediatePostData]
        public RegPianificazioneMP RegPianificazioneMP
        {
            get
            {
                return fRegPianificazioneMP;
            }
            set
            {
                //SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value);
                if (SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value))
                {
                    OnChanged("Scenario");
                }
            }
        }

        private Scenario fScenario;
        [PersistentAlias("RegPianificazioneMP.Scenario"), DevExpress.Xpo.DisplayName("Scenario")]// [NonPersistent,  DisplayName("Scenario")]
        [ExplicitLoading()] 
            public Scenario Scenario
        {
            get
            {
                var tempObject = EvaluateAlias("Scenario");
                if (tempObject != null)
                {
                    return (Scenario)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        #region
        [Persistent("CLUSTEREDIFICI"), DevExpress.Xpo.DisplayName("Cluster")]
        [Appearance("MPDataFissa.Abilita.ClusterEdifici", Criteria = "Scenario is null Or not (Immobile is null)", Enabled = false)]
        [RuleRequiredField("RReqField.MPDataFissa.ClusterEdifici", DefaultContexts.Save, "Il Cluster è un campo obbligatorio")]
        [DataSourceProperty("ListaComboCluster", DataSourcePropertyIsNullMode.SelectNothing)]
        //  [DataSourceCriteria("Scenario = '@This.Scenario'")]
        [ImmediatePostData]
        [ExplicitLoading]
        [Delayed(true)]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
            }
            set
            {
                SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
            }
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DevExpress.Xpo.DisplayName("Immobile")]
        [Appearance("MPDataFissa.Abilita.Immobile", Criteria = "ClusterEdifici is null Or not (Impianto is null)", Enabled = false)]
        [RuleRequiredField("RReqField.MPDataFissa.Immobile", DefaultContexts.Save, "L' Immobile è un campo obbligatorio")]
        // [DataSourceCriteria("ClusterEdifici = '@This.ClusterEdifici'")]
        [DataSourceProperty("ListaComboEdificio", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {

                return fImmobile;
            }
            set
            {
                SetPropertyValue("Immobile", ref fImmobile, value);
            }
        }


        private Servizio fServizio;
        [Persistent("SERVIZIO"), DevExpress.Xpo.DisplayName("Servizio")]
        [Appearance("MPDataFissa.Abilita.Impianto", Criteria = "Immobile is null Or not (Apparato is null)", Enabled = false)]
        [RuleRequiredField("RReqField.MPDataFissa.Servizio", DefaultContexts.Save, "L' Impianto è un campo obbligatorio")]
        // [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [DataSourceProperty("ListaComboImpianto", DataSourcePropertyIsNullMode.SelectNothing)]
        [ImmediatePostData]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue("Servizio", ref fServizio, value);
            }
        }
        #endregion

        private Asset fAsset; //[Descrizione] Is Null  ListaComboApp
        [Persistent("ASSET"), DevExpress.Xpo.DisplayName("Asset")]
        [Appearance("MPDataFissa.Apparato", Criteria = "[Impianto] Is Null Or [ApparatoSchedaMP] Is Not Null", Enabled = false)]
        [ImmediatePostData]
        [RuleRequiredField("RReqField.MPDataFissa.Asset", DefaultContexts.Save, "Asset è un campo obbligatorio")]
        [DataSourceProperty("ListaComboApp", DataSourcePropertyIsNullMode.SelectNothing)]
        [ExplicitLoading]
        public Asset Asset
        {
            get
            {

                return fAsset;
            }
            set
            {
                SetPropertyValue<Asset>("Asset", ref fAsset, value);
            }
        }

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPSCHEDAMP"), DevExpress.Xpo.DisplayName("Procedura Attività MP")]
        [RuleRequiredField("RReqField.MPDataFissa.ApparatoSchedaMP", DefaultContexts.Save, "La Procedure Attività MP è un campo obbligatorio")]
        [Appearance("MPDataFissa.ApparatoSchedaMP", Criteria = "[Asset] Is Null Or [Data] Is Not Null", Enabled = false)]
        [DataSourceProperty("ListaComboAppSK", DataSourcePropertyIsNullMode.SelectNothing)]
        //        [DataSourceCriteria("Apparato = '@This.Apparato' And Categoria.Oid = 1 And TipologiaIntervento.Oid In(1,2) And Abilitato = 'Si' And FrequenzaOpt.TipoCadenze !='Giorno' And FrequenzaOpt.CadenzeAnno >= 1")] //And TipologiaIntervento >= 1
        [ImmediatePostData]
        [ExplicitLoading]
        public AssetSchedaMP ApparatoSchedaMP
        {
            get
            {
                return fApparatoSchedaMP;
            }
            set
            {
                SetPropertyValue<AssetSchedaMP>("ApparatoSchedaMP", ref fApparatoSchedaMP, value);
            }
        }

        [DevExpress.Xpo.DisplayName("Tipo Attività"), PersistentAlias("ApparatoSchedaMP.Categoria.Descrizione")]
        public string strCategoria
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("strCategoria");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        [DevExpress.Xpo.DisplayName("Frequenza")]        //  [DevExpress.ExpressApp.DC.Calculated("ApparatoSchedaMP.FrequenzaOpt.Descrizione")]        //
        [PersistentAlias("ApparatoSchedaMP.FrequenzaOpt.Descrizione")]
        public string strFrequenza
        {
            get
            {
                if (this.ApparatoSchedaMP == null)
                    return null;
                //var tempObject = Session.Query<Frequenze>().Where(w => w.Oid == this.ApparatoSchedaMP.FrequenzaOpt.Oid)
                //    .Select(s => s.Descrizione).First().ToString(); 
                var tempObject = EvaluateAlias("strFrequenza");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }

            }
        }

        [DevExpress.Xpo.DisplayName("Mansione"),
        PersistentAlias("ApparatoSchedaMP.MansioniOpt.Descrizione")]
        public string strMansione
        {
            get
            {
                if (IsInvalidated)
                    return null;

                var tempObject = EvaluateAlias("strMansione");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }


        [DevExpress.Xpo.DisplayName("CadenzeAnno"),
        PersistentAlias("ApparatoSchedaMP.FrequenzaOpt.CadenzeAnno")]
        [MemberDesignTimeVisibility(false)]
        public double CadenzeAnno
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("CadenzeAnno");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [DevExpress.Xpo.DisplayName("Num interventi Calcolati"),
        VisibleInLookupListView(false)]
        [PersistentAlias("MPDataFissaDettaglio.Count")]
        public int nrInterventi
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("nrInterventi");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        private DateTime fData;
        [Persistent("DATA"), DevExpress.Xpo.DisplayName("Data Cadenza Obbligata"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "ddd dd/MM/yyyy")]
        [ImmediatePostData]
        [RuleRequiredField("RReqField.MPDataFissa.Data", DefaultContexts.Save, "Data Fissa è un campo obbligatorio")]
        [Appearance("MPDataFissa.MPDataFissa.Data", Enabled = false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        public DateTime Data
        {
            get
            {
                return fData;
            }

            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
                OnChanged("MPDataFissaDettaglio");
                OnChanged("nrInterventi");
            }
        }

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;

        }
        #region
        [Association("MPDataFissa_Dettaglio", typeof(MPDataFissaDettaglio)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName("Cadenze Calcolate")]
        [ImmediatePostData]
        public XPCollection<MPDataFissaDettaglio> MPDataFissaDettaglio
        {
            get
            {
                return GetCollection<MPDataFissaDettaglio>("MPDataFissaDettaglio");
            }
        }
        #endregion

        //private XPCollection<ApparatoSchedaMP> fListaAppSkMpInseribili;
        //[System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        //public XPCollection<ApparatoSchedaMP> ListaAppSkMpInseribili
        //{
        //    get
        //    {
        //        if (Apparato == null)
        //        {
        //            return null;
        //        }
        //        else if (Apparato != null)
        //        {
        //            if (fListaAppSkMpInseribili == null)
        //            {    //         [DataSourceCriteria("Apparato = '@This.Apparato' And Categoria.Oid = 1 And TipologiaIntervento.Oid In(1,2) And Abilitato = 'Si' And FrequenzaOpt.TipoCadenze !='Giorno' And FrequenzaOpt.CadenzeAnno >= 1")] //And TipologiaIntervento >= 1
        //                //  string Filtro = string.Format("Apparato = {0} And Categoria.Oid = 1 And TipologiaIntervento.Oid In(1,2) And Abilitato = 'Si' 
        //                //And FrequenzaOpt.TipoCadenze !='Giorno' And FrequenzaOpt.CadenzeAnno >= 1", this.Apparato.Oid);
        //                //fListaAppSkMpInseribili
        //                List<int> AppSkMPdaTogliere = Session.Query<MPDataFissa>()
        //                       .Where(d => d.Apparato == this.Apparato)
        //                      .Select(s => s.ApparatoSchedaMP.Oid).ToList();

        //                List<int> TQAppSkMPdaTogliere = Session.QueryInTransaction<MPDataFissa>()
        //                        .Where(d => d.Apparato == this.Apparato)
        //                        .Where(w => w.ApparatoSchedaMP != null)
        //                       .Select(s => s.ApparatoSchedaMP.Oid).ToList();

        //                //var OidSel = new XPCollection<ApparatoSchedaMP>(Session)
        //                //.Where(w => w.Abilitato == FlgAbilitato.Si)
        //                //.Where(w => w.Apparato == this.Apparato)
        //                //.Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
        //                //.Where(w => w.Categoria.Oid == 1)
        //                // .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
        //                // .Where(w => !TQAppSkMPdaTogliere.Contains(w.Oid))
        //                //  .Where(w => !AppSkMPdaTogliere.Contains(w.Oid))
        //                // .Select(s => s.Oid).ToList();
        //                List<int> OiddaEliminare = AppSkMPdaTogliere.Concat<int>(TQAppSkMPdaTogliere).ToList();
        //                var OidSel = Session.Query<ApparatoSchedaMP>()
        //               .Where(w => w.Abilitato == FlgAbilitato.Si)
        //               .Where(w => w.Apparato == this.Apparato)
        //               .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
        //               .Where(w => w.Categoria.Oid == 1)
        //                .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
        //                .Where(w => !OiddaEliminare.Contains(w.Oid))
        //                    // .Where(w => !AppSkMPdaTogliere.Contains(w.Oid))
        //                .Select(s => s.Oid).ToList();
        //                //  fListaAppSkMpInseribili.Criteria = CriteriaOperator.Parse(Filtro);

        //                //List<int> AppSkMPdaTogliere = Session.Query<MPDataFissa>()
        //                //        .Where(d => d.Apparato == this.Apparato)
        //                //       .Select(s => s.ApparatoSchedaMP.Oid).ToList();

        //                //List<int> AppSkMPdaToglieret = Session.QueryInTransaction<MPDataFissa>()
        //                //        .Where(d => d.Apparato == this.Apparato)
        //                //        .Where(w => w.ApparatoSchedaMP != null)
        //                ////       .Select(s => s.ApparatoSchedaMP.Oid).ToList();

        //                //List<int> OiddaEliminare = AppSkMPdaTogliere.Concat<int>(AppSkMPdaToglieret).ToList();
        //                //List<int> Oidda = OidSel.Where(w => !OiddaEliminare.Contains(w)).ToList();

        //                fListaAppSkMpInseribili = new XPCollection<ApparatoSchedaMP>(Session);
        //                CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
        //                fListaAppSkMpInseribili.Criteria = new GroupOperator(GroupOperatorType.And, charFiltert);

        //            }

        //        }
        //        return fListaAppSkMpInseribili; // Return the filtered collection of Accessory objects 
        //    }
        //}


        #region Liste Carica Combo Cluster Immobile Impianto Apparato

        private XPCollection<ClusterEdifici> fListaComboCluster;
        [System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ClusterEdifici> ListaComboCluster
        {
            get
            {
                if (RegPianificazioneMP != null)
                {
                    if (fListaComboCluster == null)
                    {   ///   Toglie gli sk prec

                        List<int> TQOidObjdaTogliere = Session.QueryInTransaction<MPDataFissaDettaglio>()
                      .Where(w => w.Asset != null)
                      .Where(w => w.Asset.Servizio.Immobile.ClusterEdifici.Scenario == this.RegPianificazioneMP.Scenario)  //@@@  togli anche gli schedulati in precedenza
                      .Where(w => w.MPDataFissa.RegPianificazioneMP == this.RegPianificazioneMP)
                      .Select(s => s.ApparatoSchedaMP.Oid).Distinct().ToList();


                        /// sel gli app che hanno contaore
                        List<int> OidSel = Session.Query<AssetSchedaMP>()
                       .Where(w => w.Abilitato == FlgAbilitato.Si && w.Asset.Servizio.Immobile.ClusterEdifici.Scenario == this.RegPianificazioneMP.Scenario)
                           .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
                          .Where(w => w.Categoria.Oid == 1)
                       .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
                       .Where(w => w.RegistroPianificazioneMP == this.RegPianificazioneMP)
                       .Where(w => !TQOidObjdaTogliere.Contains(w.Oid))
                       .Select(s => s.Asset.Servizio.Immobile.ClusterEdifici.Oid).Distinct().ToList();

                        CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
                        GroupOperator goc = new GroupOperator(GroupOperatorType.And, charFiltert);
                        fListaComboCluster = new XPCollection<ClusterEdifici>(Session, goc);
                    }

                }
                return fListaComboCluster; // Return the filtered collection of Accessory objects 
            }
        }

        private XPCollection<Immobile> fListaComboEdificio;
        [System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Immobile> ListaComboEdificio
        {
            get
            {
                if (ClusterEdifici != null && RegPianificazioneMP != null)
                {
                    if (fListaComboEdificio == null)
                    {
                        ///   Toglie gli sk prec
                        List<int> TQOidObjdaTogliere = Session.QueryInTransaction<MPDataFissaDettaglio>()
                      .Where(w => w.Asset != null)
                      .Where(w => w.Asset.Servizio.Immobile.ClusterEdifici == this.ClusterEdifici)
                      .Where(w => w.MPDataFissa.RegPianificazioneMP == this.RegPianificazioneMP)
                      .Select(s => s.ApparatoSchedaMP.Oid).Distinct().ToList();

                        List<int> OidSel = Session.Query<AssetSchedaMP>()
                       .Where(w => w.Abilitato == FlgAbilitato.Si && w.Asset.Servizio.Immobile.ClusterEdifici == this.ClusterEdifici)
                     .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
                          .Where(w => w.Categoria.Oid == 1)
                       .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
                           .Where(w => w.RegistroPianificazioneMP == this.RegPianificazioneMP)
                       .Where(w => !TQOidObjdaTogliere.Contains(w.Oid))
                       .Select(s => s.Asset.Servizio.Immobile.Oid).Distinct().ToList();

                        CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
                        GroupOperator goc = new GroupOperator(GroupOperatorType.And, charFiltert);
                        fListaComboEdificio = new XPCollection<Immobile>(Session, goc);
                    }
                }
                return fListaComboEdificio; // Return the filtered collection of Accessory objects 
            }
        }

        private XPCollection<Servizio> fListaComboImpianto;
        [System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Servizio> ListaComboImpianto
        {
            get
            {
                if (ClusterEdifici != null && RegPianificazioneMP != null && Immobile != null)
                {
                    if (fListaComboImpianto == null)
                    {
                        ///   Toglie gli sk prec
                        List<int> TQOidObjdaTogliere = Session.QueryInTransaction<MPDataFissaDettaglio>()
                      .Where(w => w.Asset != null)
                      .Where(w => w.Asset.Servizio.Immobile == this.Immobile)  //@@@  togli anche gli schedulati in precedenza
                      .Where(w => w.MPDataFissa.RegPianificazioneMP == this.RegPianificazioneMP)
                      .Select(s => s.ApparatoSchedaMP.Oid).Distinct().ToList();
                        /// sel gli app che hanno contaore
                        List<int> OidSel = Session.Query<AssetSchedaMP>()
                       .Where(w => w.Abilitato == FlgAbilitato.Si && w.Asset.Servizio.Immobile == this.Immobile)
                         .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
                          .Where(w => w.Categoria.Oid == 1)
                       .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
                           .Where(w => w.RegistroPianificazioneMP == this.RegPianificazioneMP)
                       .Where(w => !TQOidObjdaTogliere.Contains(w.Oid))
                       .Select(s => s.Asset.Servizio.Oid).Distinct().ToList();

                        CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
                        GroupOperator goc = new GroupOperator(GroupOperatorType.And, charFiltert);
                        fListaComboImpianto = new XPCollection<Servizio>(Session, goc);
                    }
                }
                return fListaComboImpianto; // Return the filtered collection of Accessory objects 
            }
        }

        private XPCollection<Asset> fListaComboApp;
        [System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Asset> ListaComboApp
        {
            get
            {
                if (ClusterEdifici != null && RegPianificazioneMP != null && Immobile != null && Servizio != null)
                {
                    if (fListaComboApp == null)
                    {
                        ///   Toglie gli sk prec
                        List<int> TQOidObjdaTogliere = Session.QueryInTransaction<MPDataFissaDettaglio>()
                                .Where(w => w.Asset != null)
                                .Where(w => w.Asset.Servizio == this.Servizio)
                                .Where(w => w.MPDataFissa.RegPianificazioneMP == this.RegPianificazioneMP)
                                .Select(s => s.ApparatoSchedaMP.Oid).Distinct().ToList();

                        /// sel gli app che hanno contaore
                        List<int> OidSel = Session.Query<AssetSchedaMP>()
                       .Where(w => w.Abilitato == FlgAbilitato.Si && w.Asset.Servizio == this.Servizio)
                      .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
                          .Where(w => w.Categoria.Oid == 1)
                       .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
                           .Where(w => w.RegistroPianificazioneMP == this.RegPianificazioneMP)
                       .Where(w => !TQOidObjdaTogliere.Contains(w.Oid))
                       .Select(s => s.Asset.Oid).Distinct().ToList();

                        CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
                        GroupOperator goc = new GroupOperator(GroupOperatorType.And, charFiltert);
                        fListaComboApp = new XPCollection<Asset>(Session, goc);
                    }

                }
                return fListaComboApp; // Return the filtered collection of Accessory objects 
            }
        }


        private XPCollection<AssetSchedaMP> fListaComboAppSK;
        [System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AssetSchedaMP> ListaComboAppSK
        {
            get
            {
                if (ClusterEdifici != null && RegPianificazioneMP != null && Immobile != null && Servizio != null && this.Asset != null)
                {
                    if (fListaComboAppSK == null)
                    {
                        ///   Toglie gli sk prec
                        ///   Toglie gli sk prec
                        List<int> TQOidObjdaTogliere = Session.QueryInTransaction<MPDataFissaDettaglio>()
                                .Where(w => w.Asset != null)
                                .Where(w => w.Asset == this.Asset)
                                .Where(w => w.MPDataFissa.RegPianificazioneMP == this.RegPianificazioneMP)
                                .Select(s => s.ApparatoSchedaMP.Oid).Distinct().ToList();

                        /// sel gli app che hanno contaore
                        List<int> OidSel = Asset.AppSchedaMpes
                           .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
                          .Where(w => w.Categoria.Oid == 1)
                       .Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
                           .Where(w => w.RegistroPianificazioneMP == this.RegPianificazioneMP)
                       .Where(w => !TQOidObjdaTogliere.Contains(w.Oid))
                       .Select(s => s.Oid).Distinct().ToList();

                        // List<int> OidSel = Session.Query<ApparatoSchedaMP>()
                        //.Where(w => w.Abilitato == FlgAbilitato.Si && w.Apparato.Impianto == this.Impianto)
                        // .Where(w => w.TipologiaIntervento.Oid == 1 || w.TipologiaIntervento.Oid == 2)
                        //   .Where(w => w.Categoria.Oid == 1)
                        //.Where(w => w.FrequenzaOpt.CadenzeAnno >= 1 && w.FrequenzaOpt.TipoCadenze != TipoCadenze.Giorno)
                        //.Where(w => !TQOidObjdaTogliere.Contains(w.Oid))
                        //.Select(s => s.Apparato.Oid).Distinct().ToList();

                        CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
                        GroupOperator goc = new GroupOperator(GroupOperatorType.And, charFiltert);
                        fListaComboAppSK = new XPCollection<AssetSchedaMP>(Session, goc);
                    }

                }
                return fListaComboAppSK; // Return the filtered collection of Accessory objects 
            }
        }

        #endregion



        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (this.Oid == -1)
                {
                    if (newValue != null && propertyName == "ClusterEdifici")
                    {
                        this.fListaComboEdificio = null;
                        this.fListaComboImpianto = null;
                        this.fListaComboApp = null;
                        this.fListaComboAppSK = null;
                    }

                    if (newValue != null && propertyName == "Immobile")
                    {
                        this.fListaComboImpianto = null;
                        this.fListaComboApp = null;
                        this.fListaComboAppSK = null;
                    }

                    if (newValue != null && propertyName == "Impianto")
                    {
                        this.fListaComboApp = null;
                        this.fListaComboAppSK = null;
                    }

                    if (newValue != null && propertyName == "Apparato")
                    {
                        this.fListaComboAppSK = null;
                    }
                }
            }


        }


        #region  vecchie

        //private RegVincoli fRegVincoli;
        //[MemberDesignTimeVisibility(false),
        //Association(@"RegVincoli_MPDataFissa", typeof(RegVincoli))]
        //[Persistent("MPREGVINCOLI"), DevExpress.Xpo.DisplayName("Registro Vincoli")]
        //[Appearance("MPDataFissa.RegVincoli", Enabled = false)]
        //[ImmediatePostData]
        //public RegVincoli RegVincoli
        //{
        //    get
        //    {
        //        return fRegVincoli;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegVincoli>("RegVincoli", ref fRegVincoli, value);
        //    }
        //}


        #endregion

        [PersistentAlias("'Cluster(' + ClusterEdifici.Descrizione + '), Immobile(' + Immobile.Descrizione + '), Impianto(' + Impianto.Descrizione + ') Apparato:' + Apparato.Descrizione")]
        [Size(SizeAttribute.Unlimited)]
        public string FullName
        {
            get
            {
                object tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return (string)tempObject;
                   // return string.Format(@"{0},{1}, {2}, {3}", this.ClusterEdifici.Descrizione, this.Immobile.Descrizione, this.Impianto.Descrizione, this.Apparato.Descrizione); 
                }
                else
                {
                    return null;
                }
            }
               // return string.Format("{0},{1}, {2}, {3}", this.ClusterEdifici.Descrizione, this.Immobile.Descrizione, this.Impianto.Descrizione, this.Apparato.Descrizione); }
        }


        public override string ToString()
        {            
            return FullName;
        }
    }
}







