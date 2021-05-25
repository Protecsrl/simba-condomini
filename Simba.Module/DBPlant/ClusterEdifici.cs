using System;
using CAMS.Module.DBPlanner;
using CAMS.Module.DBPlant.Vista;
using DevExpress.Xpo;
using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;

using CAMS.Module.Classi;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using System.Drawing;
using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions,
    Persistent("CLUSTEREDIFICI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gruppo Stabili")]
    [ImageName("Cluster")]
    [RuleCriteria("RuleInfo.ClusterEdifici.TMPCOE", DefaultContexts.Save, @"(TMPCOE < ((USLG/4)- 30)) Or Presidiato <> 'No'",
    CustomMessageTemplate = "Informazione:Tempo di Percorrenza tra CO e Edifici({TMPCOE}) prossimo alla soglia di worning.", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleWarning.ClusterEdifici.TMPCOE", DefaultContexts.Save, @"(TMPCOE < USLG/4) Or Presidiato <> 'No'",
    CustomMessageTemplate = "Attenzione:Tempo di Percorrenza tra CO e Edifici({TMPCOE}) Maggiore della meta del tempo standard di lavoro giornaliero.\n\r      Si consiglia di configurare un Cluster piu piccolo in modo da ottenere dei tempi medi di percorrenza minori", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]

    [RuleCriteria("RuleError.ClusterEdifici.TMPCOE", DefaultContexts.Save, @"(TMPCOE < ((USLG-TMPE-15)*2)) Or Presidiato <> 'No'",
    CustomMessageTemplate = "Tempo di trasferimento tra Centro Operativo e Edifici({TMPCOE}) non consentito!.\n\r      Costruire un Cluster piu piccolo in modo da ottenere dei tempi medi di percorrenza minori", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]
    [RuleCriteria("RuleInfo.ClusterEdifici.TMPE", DefaultContexts.Save, @"(TMPE < ((USLG-(TMPCOE*2)-15)/2.5)) Or Presidiato <> 'No'",
    CustomMessageTemplate = "Informazione:Tempo di trasferimento tra Edifici({TMPE}) prossimo alla soglia di worning.", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Information)]

    [RuleCriteria("RuleWarning.ClusterEdifici.TMPE", DefaultContexts.Save, @"(TMPE < ((USLG-(TMPCOE*2)-15)/2)) Or Presidiato <> 'No'",
    CustomMessageTemplate = "Attenzione1:Tempo di trasferimento tra Edifici({TMPE}) Maggiore della meta del tempo standard di lavoro giornaliero.\n\r    Si consiglia di configurare un Cluster piu piccolo in modo da ottenere dei tempi medi di percorrenza minori",
    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]
    [RuleCriteria("RuleError.ClusterEdifici.TMPE", DefaultContexts.Save, @"(TMPE < (USLG-(TMPCOE*2)-15)) Or Presidiato <> 'No'",
    CustomMessageTemplate = "Tempo di trasferimento tra Edifici({TMPE}) compresi nel cluster non consentito!.\n\r      Costruire un Cluster piu piccolo in modo da ottenere dei tempi medi di percorrenza minori",
    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]

    [RuleCriteria("RuleInfo.ClusterEdifici.PresidioNONDefinito", DefaultContexts.Save, @"Presidiato = 'Si' Or Presidiato = 'No'",
    CustomMessageTemplate = "Informazione:Il Presidio deve essere Si oppure No, altrimenti il Cluster non è Valido!", SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]


    [Appearance("editabile.Tutto", TargetItems = "*", Context = "Any", Criteria = "checkApp", Enabled = false)]
    [Appearance("Visibile.ClusterEdifici.StatoScenario", TargetItems = "StatoScenario", Context = "Any", Criteria = "Oid = -1", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Visibile.ClusterEdifici.TMPE_TMPCOE", TargetItems = "TMPCOE;TMPE;DMPE;DMPCOE", Context = "Any", Criteria = "Presidiato == 'Si'", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Visibile.ClusterEdifici.miso", TargetItems = "QuantitaApparati;QuantitaImpianto;QuantitaEdifici", Context = "Any", Criteria = "Edificis.Count == 0", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Visibile.ParametriScenario.Visibili", TargetItems = "TMPCOE;TMPE;USLS_USLG_CLuster", Context = "Any", Criteria = "Presidiato == 'Si' And LivelloAccorpamento = 'Scenario'", Visibility = ViewItemVisibility.Hide)]
    // USLS_USLG_Scenario
    [Appearance("Visibile.Parametri.Visibili", TargetItems = "TMPCOE;TMPE;USLS_USLG_CLuster;USLS_USLG_Scenario", Context = "Any", Criteria = "Presidiato == 'Si' And LivelloAccorpamento = 'Cluster'", Visibility = ViewItemVisibility.Hide)]

    // USLS_USLG_Scenario
    [Appearance("Visibile.Parametriprenoecluster.Visibili", TargetItems = "USLS_USLG_Scenario", Context = "Any", Criteria = "Presidiato == 'Si' And LivelloAccorpamento = 'Cluster'", Visibility = ViewItemVisibility.Hide)]

    [Appearance("ClusterEdifici.BackColor.Salmon",TargetItems = "*", BackColor = "Salmon", FontColor = "Black", Priority = 1, Criteria = "SommaTempo = 0")]
    [NavigationItem("Planner")]
    public class ClusterEdifici : XPObject
    {
        private string vUSLS = string.Empty;
        private string vUSLG = string.Empty;
        public ClusterEdifici() : base() { }
        public ClusterEdifici(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.LivelloAccorpamento = Classi.LivelloAccorpamento.Cluster;
                this.Presidiato = Classi.Presidiato.No;
                //  var mm = Evaluate("Scenario");
                if (this.Scenario != null)
                {
                    this.CentroOperativo = this.Scenario.CentroOperativo;
                    string nrcl = this.Scenario.ClusterEdificis.Count.ToString();
                    this.Descrizione = "Nuovo CLUSTER nr " + nrcl;
                    //this.Presidiato;
                    //this.LivelloAccorpamento
                    //this.TMPCOE;
                    //this.TMPE,
                    //this.USLS_USLG_Scenario
                    //this.USLS_USLG_CLuster
                }
            }

        }

        [System.ComponentModel.Browsable(false)]
        internal bool checkApp
        {
            get
            {
                if (fScenario != null)
                {
                    return false; // fScenario.GetCurrStatoPianificazione() > 2;
                }
                return false;
            }
        }

        private string fDescrizione;
        [Size(150),     Persistent("DESCRIZIONE"),        DisplayName("Descrizione")]
        [DbType("varchar(150)")]
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

        private string fColore;
        [Persistent("COLORE"),
        VisibleInListView(false),
        DisplayName("Colore")]
        [Appearance("AbilitaModificaLivelloColore", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
        [Appearance("Visibile.ClusterEdifici.Colore", Visibility = ViewItemVisibility.Hide)]
        public string Colore
        {
            get
            {
                return fColore;
            }
            set
            {
                SetPropertyValue<string>("Colore", ref fColore, value);
            }
        }


        #region PARAMETRI SCHEDULAZIONE

        private LivelloAccorpamento fLivelloAccorpamento;
        [Size(15),        Persistent("LACCORPAMENTO"),        DisplayName("Livello Distribuzione")]
        // [Appearance("RdL.Abilita.Immobile", Criteria = "not (Presidiato  is null)", FontColor = "Black", Enabled = false)]
        [Appearance("RdL.Immobile.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(LivelloAccorpamento)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        public LivelloAccorpamento LivelloAccorpamento
        {
            get
            {
                return fLivelloAccorpamento;
            }
            set
            {
                SetPropertyValue<LivelloAccorpamento>("LivelloAccorpamento", ref fLivelloAccorpamento, value);
            }
        }
        private Presidiato fPresidiato;
        [Size(15), Persistent("PRESIDIO"), DisplayName("Presidiato")]
        [ImmediatePostData]
        [Appearance("Cluster.Abilita.Presidio", Criteria = "(LivelloAccorpamento  is null) OR (not (CentroOperativo is null))", FontColor = "Black", Enabled = false)]
        [Appearance("Cluster.Presidio.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Presidiato)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        public Presidiato Presidiato
        {
            get
            {
                return fPresidiato;
            }
            set
            {
                SetPropertyValue<Presidiato>("Presidiato", ref fPresidiato, value);
               
            }
        }

        private TipoStraordinario fStraordinario;
        [Size(15), Persistent("STRAORDINARIO"), DisplayName("Straordinario Consentito")]
        [ImmediatePostData]
        public TipoStraordinario Straordinario
        {
            get
            {
                return fStraordinario;
            }
            set
            {
                SetPropertyValue<TipoStraordinario>("Straordinario", ref fStraordinario, value);
                 
            }
        }

        #endregion

        #region parametri Cluster Edifici
        // a che serve??????????????  -> alla planner per la pianificazione!!!!!!!!!!!
        [Persistent("CENTROOPERATIVO"), Association(@"Cluster_CO"), DisplayName("Centro Operativo di Cluster")]
        [DataSourceCriteria("'@This.Scenario.CentroOperativo.AreaDiPolo' = AreaDiPolo")]      
        [Appearance("ClusterEdifici.CentroOperativo.scenario", Criteria = "Scenario == null Or '@This.Scenario.CentroOperativo.AreaDiPolo.Polo.Oid' = 1", Enabled = false)]
        [ToolTip("Seleziona il Centro Operativo pertinente alla schedulazione della manutenzione programmata")]
        [ImmediatePostData(true)]
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
        [Size(5), Persistent("TMPCOE"), VisibleInListView(false), DisplayName("Tempo Medio Percorrenza tra CO e Edifici")]
        [Appearance("AbilitaModificaTMPCOE", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Tempo Medio Percorrenza tra CO e Edifici", "Cluster Edifici", DevExpress.Persistent.Base.ToolTipIconType.Information)]
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
        [Size(5), Persistent("TMPE"), VisibleInListView(false), DisplayName("Tempo Medio Percorrenza tra Edifici")]
        [Appearance("AbilitaModificaTMPE", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Tempo Medio Percorrenza tra CO e Edifici", "Cluster Edifici", DevExpress.Persistent.Base.ToolTipIconType.Information)]
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

        private double tDMPCOE;
        [Size(5), Persistent("DMPCOE"), VisibleInListView(false), DisplayName("Distanza Media Percorrenza tra CO e Edifici")]
        [Appearance("AbilitaModificaDMPCOE", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
        //[DbType("number")]   commetato x passaggio a MSSQL
        public double DMPCOE
        {
            get
            {
                return tDMPCOE;
            }
            set
            {
                SetPropertyValue<double>("DMPCOE", ref tDMPCOE, value);
            }
        }

        private double tDMPE;
        [Size(5), Persistent("DMPE"), VisibleInListView(false), DisplayName("Distanza Media Percorrenza tra Edifici")]
        [Appearance("AbilitaModificaDMPE", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
        //[DbType("number")]   commetato x passaggio a MSSQL
        public double DMPE
        {
            get
            {
                return tDMPE;
            }
            set
            {
                SetPropertyValue<double>("DMPE", ref tDMPE, value);
            }
        }
        // riporto tempi di lavoro
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

        // riporto tempi di lavoro
        [PersistentAlias("'USLS:' + CentroOperativo.USLS + ', ' + 'USLG:' + CentroOperativo.USLG"), DisplayName("USL S/G di Cluster Edifici")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("USLG (Unità Standard Lavoro Giornaliero), USLS (Unità Standard Lavoro Settimanale)", "Parametri di Cluster Edifici", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string USLS_USLG_CLuster
        {
            get
            {
                if (CentroOperativo == null) return null;
                var tempObject = EvaluateAlias("USLS_USLG_CLuster");
                if (tempObject != null)
                    return (string)tempObject.ToString();
                else
                    return null;
                //return string.Format("USLS: {0}, USLG: {1}", CentroOperativo.USLS, CentroOperativo.USLG);
            }
        }


        private TipoAssociazioneTRisorsa fTipoAssociazioneTRisorsa;
        [Persistent("TIPOASSOCIAZIONETEAM"), DisplayName("Tipo Associazione TRisorsa")]
        public TipoAssociazioneTRisorsa TipoAssociazioneTRisorsa
        {
            get
            {
                return fTipoAssociazioneTRisorsa;
            }
            set
            {
                SetPropertyValue<TipoAssociazioneTRisorsa>("TipoAssociazioneTRisorsa", ref fTipoAssociazioneTRisorsa, value);
            }
        }
        #endregion

        #region Associazioni Figlio

        [Association(@"ClusterEdifici_Edificio", typeof(Immobile)), DisplayName("Immobile")]
        //[DataSourceCriteria("Associato = false And SumTempoMp > 0 And Abilitato == 1 And AbilitazioneEreditata == 1")]
        [DataSourceCriteria("Associato = false And Abilitato == 1 And AbilitazioneEreditata == 1")]
        [ImmediatePostData(true)]
        public XPCollection<Immobile> Edificis
        {
            get
            {
                return GetCollection<Immobile>("Edificis");
            }
        }

        #region Filtro per combo  Immobile ??? impianto
        //private XPCollection<Impianto> fListaFiltrataImpiantos;
        //[PersistentAlias("fListaFiltrataImpiantos"), DisplayName("Dettaglio Impianti da edifici")]
        //[MemberDesignTimeVisibility(true)]
        ////[Appearance("CLUSTEREDIFICI.ListaFiltrataImpianto", Enabled = false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<Impianto> ListaFiltrataImpiantos
        //{
        //    get
        //    {
        //        if (fListaFiltrataImpiantos == null && Edificis != null)
        //        {
        //            fListaFiltrataImpiantos = new XPCollection<Impianto>(Session);
        //            RefreshfListaFiltrataRdLs();
        //        }
        //        return fListaFiltrataImpiantos;
        //    }
        //}


        //private void RefreshfListaFiltrataRdLs()
        //{
        //    if (fListaFiltrataImpiantos == null)
        //    {
        //        return;
        //    }
        //    if (Edificis == null)
        //    {
        //        return;
        //    }
        //    var EdificiOid = string.Empty;
        //    foreach (var ele in Edificis)
        //    {
        //        EdificiOid = ele.Oid.ToString() + ",";
        //    }
        //    if (EdificiOid == string.Empty)
        //    {
        //        EdificiOid = "0";
        //    }
        //    EdificiOid = EdificiOid.Substring(0, EdificiOid.Length - 1);

        //    var ParCriteria = string.Format("Immobile.Oid in({0})", EdificiOid);
        //    fListaFiltrataImpiantos.Criteria = CriteriaOperator.Parse(ParCriteria);
        //    OnChanged("ListaFiltrataRdLs");
        //}

        #endregion

        [Association(@"ClusterEdifici_ClusterEdificiRisorseTeam", typeof(ClusterEdificiRisorseTeam)), DisplayName("ClusterEdifici RisorseTeam")]
        public XPCollection<ClusterEdificiRisorseTeam> ClusterEdificiRisorseTeams
        {
            get
            {
                return GetCollection<ClusterEdificiRisorseTeam>("ClusterEdificiRisorseTeams");
            }
        }


        [Association(@"ClusterEdifici_MansioneCarico", typeof(ClusterEdificiMansioneCarico)),
        DevExpress.Xpo.DisplayName("Carico per Mansione")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ClusterEdificiMansioneCarico> ClusterEdificiMansioneCaricos
        {
            get
            {
                return GetCollection<ClusterEdificiMansioneCarico>("ClusterEdificiMansioneCaricos");
            }
        }
        #endregion

        #region non Persentint
        [Size(15), NonPersistent, DisplayName("Par. Pianificazione")]
        [VisibleInDetailView(false)]
        [Appearance("AbilitaModificaLivelloAvviso", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
        public string Avviso
        {
            get
            {
                var tempObject = getParametriPianificazione();
                if (tempObject != null)
                {
                    return tempObject.ToString();
                }
                else
                {
                    return "Non Impostato";
                }
            }
        }
        private string getParametriPianificazione()
        {
            if (Scenario != null)
                if (Scenario.CentroOperativo != null)
                    if (Scenario.CentroOperativo.USLS != null)
                    {
                        if (!string.IsNullOrEmpty(Scenario.CentroOperativo.USLS.ToString()))
                        {
                            if (!string.IsNullOrWhiteSpace(Scenario.CentroOperativo.USLS.ToString()))
                            {
                                var sUlss = Scenario.CentroOperativo.USLS.ToString();
                                vUSLS = sUlss;
                                var sUlsg = Scenario.CentroOperativo.USLG.ToString();
                                vUSLG = sUlsg;
                                if (Presidiato.ToString().Contains("Si"))
                                {
                                    return string.Format("Unita Standard Lavorativa Settimana/Giornaliera: {0} / {1}, Tempo di trasferimento applicato 15%", sUlss, sUlsg);
                                }
                                else
                                {
                                    return string.Format("Unita Standard Lavorativa Settimana/Giornaliera: {0} / {1}, Tempo di trasferimento applicato TMPCOE {2},TMPE{3}:", sUlss, sUlsg, TMPCOE, TMPE);
                                }
                            }
                        }
                        return string.Format("Unita Standard Lavorativa Settimana/Giornaliera non impostata");
                    }
            return string.Empty;
        }

        #region parametri scenario

        private Scenario fScenario;
        [Association(@"SCENARIORefCLUSTEREDIFICI"), Persistent("SCENARIO"), DisplayName("Scenario")]
        [DataSourceCriteria("CheckApp = false")]
        [Appearance("ClusterEdificio.Scenario.Abilita", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Scenario Scenario
        {
            get
            {
                return fScenario;
            }
            set
            {
                SetPropertyValue<Scenario>("Scenario", ref fScenario, value);
                //if )
                //{
                //    var TempObj = value;
                //    if (TempObj != null && TempObj.Oid != -1)
                //    {
                //        if (!IsSaving)
                //        {
                //        }
                //    }
                //    OnChanged("USLS");
                //    OnChanged("USLG");
                //}
            }
        }


        [PersistentAlias("Scenario.StatoScenario"), DisplayName("Stato Scenario")]
        [Appearance("ClusterEdificio.StatoScenario.Abilita", Enabled = false)]
        public StatoScenarioClusterEdificio StatoScenario
        {
            get
            {
                var tempObject = EvaluateAlias("StatoScenario");
                if (tempObject != null)
                {
                    return (StatoScenarioClusterEdificio)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("Scenario.CentroOperativo"), DisplayName("Centro Operativo di Scenario")]
        [ImmediatePostData(true)]
        public CentroOperativo CentroOperativoScenario
        {
            get
            {
                if (Scenario == null)
                    return null;
                var tempObject = EvaluateAlias("CentroOperativoScenario");
                if (tempObject != null)
                {
                    return (CentroOperativo)tempObject;
                }
                else
                {
                    return null;
                }

            }

        }

        //private int tTMPCOE_Scenario;
        //[Size(5), Persistent("TMPCOE_SCENARIO"), VisibleInListView(false), DisplayName("TMP.COS.E")]
        //[Appearance("AbilitaModificaTMPSCOE", Enabled = false, Criteria = "checkApp", Context = "DetailView")]
        //[ToolTip("Tempo Medio Percorrenza tra CO di Scenario e Edifici", "Scenario", DevExpress.Utils.ToolTipIconType.Question)]
        //public int TMPCOE_Scenario
        //{
        //    get
        //    {
        //        return tTMPCOE_Scenario;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("TMPCOE_Scenario", ref tTMPCOE_Scenario, value);
        //    }
        //}

        [PersistentAlias("'USLS:' + Scenario.CentroOperativo.USLS + ', ' + 'USLG:' + Scenario.CentroOperativo.USLG"), DisplayName("USL S/G di Scenario")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("USLG (Unità Standard Lavoro Giornaliero), USLS (Unità Standard Lavoro Settimanale)", "Parametri di Scenario", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInLookupListView(false)]
        public string USLS_USLG_Scenario
        {
            get
            {
                if (CentroOperativo == null) return null;
                return string.Format("USLS: {0}, USLG: {1}", Scenario.CentroOperativo.USLS, Scenario.CentroOperativo.USLG);
            }
        }


        #endregion

        #region calcolo quantita
 
        //[PersistentAlias("Edificis.Count")]
        //[DevExpress.Xpo.DisplayName("Quantità Edifici")]
        //[ModelDefault("DisplayFormat", "{0:N}")]
        //[ModelDefault("EditMask", "N")]
        //public int QuantitaEdifici
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("QuantitaEdifici");
        //        if (tempObject != null)
        //        {
        //            return (int)tempObject;
        //        }
        //        else { return 0; }
        //    }
        //}
        [Persistent("TOT_EDIFICI")]
        [DevExpress.Xpo.DisplayName("Quantità Edifici")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int ContaEdifici
        {
            get { return GetDelayedPropertyValue<int>("ContaEdifici"); }
            set { SetDelayedPropertyValue<int>("ContaEdifici", value); }

        }

      
        //[PersistentAlias("Edificis.Sum(Impianti.Count)")]
        //[DevExpress.Xpo.DisplayName("Quantità Impianti")]
        //[ModelDefault("DisplayFormat", "{0:N}")]
        //[ModelDefault("EditMask", "N")]
        //public int QuantitaImpianto
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("QuantitaImpianto");
        //        if (tempObject != null)
        //        {
        //            return (int)tempObject;
        //        }
        //        else { return 0; }
        //    }
        //}
        [Persistent("TOT_IMPIANTI")]
        [DevExpress.Xpo.DisplayName("Quantità Impianti")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int ContaImpianti
        {
            get { return GetDelayedPropertyValue<int>("ContaImpianti"); }
            set { SetDelayedPropertyValue<int>("ContaImpianti", value); }

        }
   
        //[PersistentAlias("Edificis.Sum(Impianti.Sum(APPARATOes.Count))")]
        //[DevExpress.Xpo.DisplayName("Quantità Apparati")]
        //[ModelDefault("DisplayFormat", "{0:N}")]
        //[ModelDefault("EditMask", "N")]
        //public int QuantitaApparati
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("QuantitaApparati");
        //        if (tempObject != null)
        //        {
        //            return (int)tempObject;
        //        }
        //        else { return 0; }
        //    }
        //}
        [Persistent("TOT_APPARATI")]
        [DevExpress.Xpo.DisplayName("Quantità Apparati")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int ContaApparati
        {
            get { return GetDelayedPropertyValue<int>("ContaApparati"); }
            set { SetDelayedPropertyValue<int>("ContaApparati", value); }

        }
     
        //[PersistentAlias("Edificis.Sum(EdificioMansioneCaricos.Sum(Carico))")]
        //[DevExpress.Xpo.DisplayName("Somma Tempi")]
        //[ModelDefault("DisplayFormat", "{0:N}")]
        //[ModelDefault("EditMask", "N")]
        //[VisibleInListView(false)]
        //public int SommaTempo
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("SommaTempo");
        //        if (tempObject != null)
        //        {
        //            return (int)tempObject;
        //        }
        //        else { return 0; }
        //    }
        //}

        [Persistent("TOT_CARICOTEMPI")]
        [DevExpress.Xpo.DisplayName("Somma Tempi")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int SommaTempo
        {
            get { return GetDelayedPropertyValue<int>("SommaTempo"); }
            set { SetDelayedPropertyValue<int>("SommaTempo", value); }

        }
        #endregion

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
                    if (newValue != null && propertyName == "Scenario")
                    {
                        Scenario newV = (Scenario)(newValue);
                        if (newV != null)
                        {
                            if (this.CentroOperativo == null)
                            {
                                this.CentroOperativo = newV.CentroOperativo;
                            }

                            if (this.Descrizione == null)
                            {
                                string nrcl = this.Scenario.ClusterEdificis.Count.ToString();
                                this.Descrizione = "Nuovo CLuster nr " + nrcl;
                            }
                        }
                    } 
                }
            }
            
        }


    }
}
