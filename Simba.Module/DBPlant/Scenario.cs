using System;
using CAMS.Module.DBPlant.Vista;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.DBPlanner;
using CAMS.Module.Classi;
using DevExpress.Data.Filtering;
using System.Drawing;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp;
using System.Linq;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("SCENARIO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Scenario")]
    [ImageName("Scenario")]
    [NavigationItem("Planner")]
    //ClusterEdificis

    [RuleCriteria("RuleInfo.Scenario.TMPCOE", DefaultContexts.Save, @"(TMPCOE < ((USLG/4)- 30))",
    CustomMessageTemplate = "Informazione:Tempo di Percorrenza tra CO e Edifici({TMPCOE}) prossimo alla soglia di worning.",
    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleWarning.Scenario.TMPCOE", DefaultContexts.Save, @"(TMPCOE < USLG/4)",
    CustomMessageTemplate = "Attenzione:Tempo di Percorrenza tra CO e Edifici({TMPCOE}) Maggiore della meta del tempo standard di lavoro giornaliero.\n\r      Si consiglia di configurare uno Scenario piu piccolo in modo da ottenere dei tempi medi di percorrenza minori"
    , SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]

    [RuleCriteria("RuleError.Scenario.TMPCOE", DefaultContexts.Save, @"(TMPCOE < ((USLG-TMPE-15)*2))",
    CustomMessageTemplate = "Tempo di trasferimento tra Centro Operativo e Edifici({TMPCOE}) non consentito!.\n\r      Costruire uno Scenario piu piccolo in modo da ottenere dei tempi medi di percorrenza minori", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]
    [RuleCriteria("RuleInfo.Scenario.TMPE", DefaultContexts.Save, @"(TMPE < ((USLG-(TMPCOE*2)-15)/2.5))",
    CustomMessageTemplate = "Informazione:Tempo di trasferimento tra Edifici({TMPE}) prossimo alla soglia di worning.", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleWarning.Scenario.TMPE", DefaultContexts.Save, @"(TMPE < ((USLG-(TMPCOE*2)-15)/2))",
    CustomMessageTemplate = "Attenzione1:Tempo di trasferimento tra Edifici({TMPE}) Maggiore della meta del tempo standard di lavoro giornaliero.\n\r    Si consiglia di configurare un Scenario piu piccolo in modo da ottenere dei tempi medi di percorrenza minori",
    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]

    [RuleCriteria("RuleError.Scenario.TMPE", DefaultContexts.Save, @"(TMPE < (USLG-(TMPCOE*2)-15))",
    CustomMessageTemplate = "Tempo di trasferimento tra Edifici({TMPE}) compresi nel cluster non consentito!.\n\r      Costruire uno Scenario piu piccolo in modo da ottenere dei tempi medi di percorrenza minori",
    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RuleError.Scenario.TMPE.valido", DefaultContexts.Save, @"TMPE > 0",
   CustomMessageTemplate = "Tempo di trasferimento tra Edifici({TMPE}) deve essere maggiore di zero",
   SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RuleError.Scenario.TMPCOE.valido", DefaultContexts.Save, @"TMPCOE > 0",
CustomMessageTemplate = "Tempo di trasferimento tra Edifici({TMPCOE}) deve essere maggiore di zero",
SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    [Appearance("Visibile.creazione", TargetItems = "StatoScenario", Context = "Any", Criteria = "Oid == -1", Visibility = ViewItemVisibility.Hide)]

    [Appearance("enabled.ClusterEdifici", TargetItems = "ClusterEdificis", Context = "Any", Criteria = "CentroOperativo == null", Enabled = false)]
    [Appearance("Visibile.Quantita", TargetItems = "QuantitaElementiDipendenti", Context = "DetailView", Criteria = "ClusterEdificis.Count == 0", Visibility = ViewItemVisibility.Hide)]
    [Appearance("schedulato.colare", TargetItems = "*", Context = "Any", Criteria = "CheckApp", FontStyle = FontStyle.Bold, FontColor = "Brown")]


    [Appearance("ClusterEdifici.BackColor.Salmon", TargetItems = "*", BackColor = "Salmon", FontColor = "Black", Priority = 1, Criteria = "SommaTempo = 0")]

    public class Scenario : XPObject
    {
        public Scenario() : base() { }
        public Scenario(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.StatoScenario = StatoScenarioClusterEdificio.Aperto;

        }


        #region Metodi ed Eventi
        protected override void OnLoaded() { base.OnLoaded(); }
        protected override void OnSaving()
        {
            base.OnSaving();
            //if (Session.IsNewObject(this))
            //{
                int numCluster = ClusterEdificis.Count;
                int numEdifici = ClusterEdificis.Sum(s => s.Edificis.Count);
                int numImpianti = ClusterEdificis.Sum(s => s.Edificis.Sum(ss => ss.NumImp));
                int numApparati = ClusterEdificis.Sum(s => s.Edificis.Sum(ss => ss.NumApp));

                Consistenza = string.Format("Cluster {0}, Edifici {1}, Impianti {2}, Apparati {3}", numCluster, numEdifici, numImpianti, numApparati);

            //}
        }
        public int GetCurrStatoPianificazione()
        {

            var Anno = 0;
            var OidStatoCorrente = 0;
            var OidScenario = 0;
            var da_bloccare = RegPianificazioneMPs.Criteria = CriteriaOperator.Parse("MPStatoPianificazione.Oid > 3");
            var conta = RegPianificazioneMPs.Count;
            return conta;
            //foreach (var ele in RegPianificazioneMPs)
            //{
            //   // Anno = ele.AnnoSchedulazione;
            //    OidScenario = ele.Scenario.Oid;
            //    OidStatoCorrente = ele.MPStatoPianificazione.Oid;
            //    if (OidScenario == Oid)
            //    {
            //        return OidStatoCorrente;
            //    }
            //}
            //return 0;
        }

        #endregion


        private string fDescrizione;
        [Size(1000), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [DbType("varchar(1000)")]
        [Appearance("AbilitaModificaDesc", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [RuleRequiredField("RuleReq.Scenario.Descrizione.obbligatorio", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
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

        [Association(@"SCENARIORefCENTRIOPERATIVI"), Persistent("CENTROOPERATIVO"), DisplayName("Centro Operativo")]
        [Appearance("AbilitaModificaCentroOperativo", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [RuleRequiredField("RuleReq.Scenario.obbligatorio", DefaultContexts.Save, "Centro Operativo è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public CentroOperativo CentroOperativo
        {
            get
            {
                return GetDelayedPropertyValue<CentroOperativo>("CentroOperativo");
            }
            set
            {
                SetDelayedPropertyValue<CentroOperativo>("CentroOperativo", value);
            }
        }
        private int tTMPCOE;
        [Size(5), Persistent("TMPCOE"), VisibleInListView(false), DisplayName("TMPE tra CO e Edifici")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Tempo Medio Percorrenza tra CO e Edifici", "Scenario", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public int TMPCOE
        {
            get
            {
                return tTMPCOE;
            }
            set
            {
                SetPropertyValue<int>("TMPCOE", ref tTMPCOE, value);
            }
        }

        private int tTMPE;
        [Size(5), Persistent("TMPE"), VisibleInListView(false), DisplayName("TMP tra Edifici")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Tempo Medio Percorrenza tra CO e Edifici", "Scenario", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public int TMPE
        {
            get
            {
                return tTMPE;
            }
            set
            {
                SetPropertyValue<int>("TMPE", ref tTMPE, value);
            }
        }

        [PersistentAlias("'USLS:' + CentroOperativo.USLS + ', ' + 'USLG:' + CentroOperativo.USLG"), DisplayName("USL S/G di Scenario")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("USLG (Unità Standard Lavoro Giornaliero), USLS (Unità Standard Lavoro Settimanale)", "Parametri di Scenario", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string USLS_USLG_Scenario
        {
            get
            {
                if (CentroOperativo == null) return null;
                return string.Format("USLS: {0}, USLG: {1}", CentroOperativo.USLS, CentroOperativo.USLG);
            }
        }


        [Appearance("AbilitaCheckApp", Visibility = ViewItemVisibility.Hide, Criteria = "true", Context = "DetailView")]
        [DisplayName("Schedulato"), VisibleInLookupListView(false)]
        public bool CheckApp
        {
            get
            {
                return GetCurrStatoPianificazione() > 0; //GetCurrStatoPianificazione() > 9;   false;
            }
        }

        StatoScenarioClusterEdificio fStatoScenario;
        [Size(1000), Persistent("STATOSCENARIO"), DisplayName("Stato Scenario")]
        [Appearance("Scenario.StatoScenario.Abilita", Enabled = false)]
        public StatoScenarioClusterEdificio StatoScenario
        {
            get { return fStatoScenario; }
            set { SetPropertyValue<StatoScenarioClusterEdificio>("StatoScenario", ref fStatoScenario, value); }
        }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public int InseribilesuRegPiano
        {
            get
            {

                int SI = 0;
                using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                {
                    if (db.GetScenarioInseribilesuRegistroPiano(this.Oid) == 1)
                        SI = 1;
                }
                return SI;
            }
        }

        #region associazioni con altre classi



        [Appearance("Scenario.RegPianificazioneMPs", Enabled = false),
         VisibleInListView(false), VisibleInLookupListView(false)]
        [MemberDesignTimeVisibility(true),
        Association(@"RegPianiMP_Scenario", typeof(RegPianificazioneMP)),
        DisplayName("Reg Pianificazione")]
        public XPCollection<RegPianificazioneMP> RegPianificazioneMPs
        {
            get
            {
                return GetCollection<RegPianificazioneMP>("RegPianificazioneMPs");
            }
        }

        [Association(@"SCENARIORefCLUSTEREDIFICI", typeof(ClusterEdifici)), Aggregated, DisplayName("Cluster di Edifici")]
        [Appearance("AbilitaModificaClusterEdi", Enabled = false, Criteria = "CheckApp", Context = "DetailView")]
        [DataSourceCriteria("Scenario Is Null")]
        public XPCollection<ClusterEdifici> ClusterEdificis
        {
            get
            {
                return GetCollection<ClusterEdifici>("ClusterEdificis");
            }
        }

        #endregion

        #region non persistent associato e abilitato  @@@@@@@@@@   da migliorare ?????????????
        //[NonPersistent, DisplayName(@"Quantità Cluster/Edifici/Impianti/Apparati")]


        //[PersistentAlias("ClusterEdificis.Count() + ' - ' + 1")]
        //[PersistentAlias("ClusterEdificis.Sum(QuantitaApparati)")]
        //[DisplayName(@"Quantità Cluster/Edifici/Impianti/Apparati")]
        //[VisibleInListView(false)]
        //public string QuantitaElementiDipendenti
        //{
        //    get
        //    {
        //        //ClusterEdificis.
        //        return (string)EvaluateAlias("QuantitaElementiDipendenti").ToString();


        //    }
        //}

        string fConsistenza;
        [Size(1000), Persistent("CONSISTENZA"), DisplayName("Consistenza")]
        [Appearance("Scenario.Consistenza.Abilita", Enabled = false)]
        public string Consistenza
        {
            get { return fConsistenza; }
            set { SetPropertyValue<string>("Consistenza", ref fConsistenza, value); }
        }


        //[PersistentAlias("ScenarioMansioneCaricos.Sum(Carico)")]
        string fSommaTempo;
        [NonPersistent]
        [DisplayName("Somma Tempi")]
        //[System.ComponentModel.Browsable(false)]
        public string SommaTempo
        {
            get
            {
                if (string.IsNullOrEmpty(fSommaTempo))
                    fSommaTempo = Session.Query<AssetSchedaMP>().Where(w => w.Asset.Servizio.Immobile.ClusterEdifici.Scenario.Oid == this.Oid).Sum(s => s.TempoOpt).ToString();
                return fSommaTempo;
                //object tempObject = EvaluateAlias("SommaTempo");
                //if (tempObject != null)
                //{
                //    return (string)tempObject.ToString();
                //}
                //else { return 0.ToString(); }
            }
        }



        #endregion

        #region utente e data aggiornamento
        private string f_Utente;
        [Persistent("UTENTE"), Size(100), DisplayName("Utente")]
        //[ Appearance("Scenario.Utente", Enabled = false)]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        //  [VisibleInListView(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"), DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        //Appearance("Scenario.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        // [VisibleInListView(false)]
        public DateTime? DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        #endregion

        [Association(@"Scenario_MansioneCarico", typeof(ScenarioMansioneCarico)),
         DevExpress.Xpo.DisplayName("Carico per Mansione")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ScenarioMansioneCarico> ScenarioMansioneCaricos
        {
            get
            {
                return GetCollection<ScenarioMansioneCarico>("ScenarioMansioneCaricos");
            }
        }


        [PersistentAlias("CentroOperativo.USLS")]
        [System.ComponentModel.Browsable(false)]
        public Double USLG
        {
            get
            {
                var tempObject = EvaluateAlias("USLG");
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

    }
}
