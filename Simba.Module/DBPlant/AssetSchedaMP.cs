using System;
using System.Drawing;
using CAMS.Module.DBPlanner;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;


namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("APPARATOSCHEDAMP")]
    [RuleCombinationOfPropertiesIsUnique("UniqueApparatoSchedaMP", DefaultContexts.Save, "Apparato, SchedaMp")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Documenti manutenzione degli asset")]


    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 ", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 ", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion
    [Appearance("Apparato.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
    FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]


    [ImageName("ModelEditor_Categorized")]
    [NavigationItem("Patrimonio")]

    public class AssetSchedaMP : XPObject
    {
        public AssetSchedaMP() : base() { }
        public AssetSchedaMP(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Abilitato = FlgAbilitato.Si;

            this.AbilitazioneEreditata = FlgAbilitato.Si;
        }

        private Asset fAsset;
        //  [(false),
        [Association(@"ASSETRefASSSCHEDAMP"), Persistent("ASSET"), DisplayName("Asset")]
        [ExplicitLoading()]
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

        private SchedaMp fSchedaMp;
        [Persistent("SCHEDAMP"),       DisplayName("Scheda MP")]
        [DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]
        [ImmediatePostData]
        [ExplicitLoading()]
        public SchedaMp SchedaMp
        {
            get
            {
                return fSchedaMp;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMp, value);
            }
        }

        private string fCodSchedaMp; 
        [Persistent("CODSCHEDEMP"), Size(30), DisplayName("Cod. Attività")]
        [DbType("varchar(30)")]
        [ImmediatePostData]
         [ToolTip("Codice Descrizione della Scheda di Manutenzione Programmata")]
        [Appearance("ApparatoSchedaMP.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string CodSchedaMp
        {
            get
            {
                return fCodSchedaMp;
            }
            set
            {
                SetPropertyValue<string>("CodSchedaMp", ref fCodSchedaMp, value);
            }
        }

        private string fCodUni;
        [Size(20),
        Persistent("COD_UNI")]
        [DbType("varchar(20)")]
        [RuleRequiredField("RuleReq.ApparatoSchedaMP.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]
        public string CodUni
        {
            get
            {
                return fCodUni;
            }
            set
            {
                SetPropertyValue<string>("CodUni", ref fCodUni, value);
            }
        }

        private SchedeMPOwner fSchedeMPOwner;
        [Persistent("SCHEDEMPOWNER"),
        DisplayName("Gestore Scheda MP")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public SchedeMPOwner SchedeMPOwner
        {
            get
            {
                return fSchedeMPOwner;
            }
            set
            {
                SetPropertyValue<SchedeMPOwner>("SchedeMPOwner", ref fSchedeMPOwner, value);
            }
        }

        private SistemaTecnologico fSistemaTecnologico;
        [Persistent("SISTEMATECNOLOGICO"),
        DisplayName("Sistema Tecnologico")]
        [RuleRequiredField("RReqField.ApparatoSchedaMP.SistemaTecnologico", DefaultContexts.Save, "Sistema Tecnologico è un campo obbligatorio")]
        [Appearance("ApparatoSchedaMP.Abilita.SistemaTecnologico", Criteria = "not (SistemaClassi  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public SistemaTecnologico SistemaTecnologico
        {
            get
            {
                return fSistemaTecnologico;
            }
            set
            {
                SetPropertyValue<SistemaTecnologico>("SistemaTecnologico", ref fSistemaTecnologico, value);
            }
        }

        private SistemaClassi fSistemaClassi;
        [Persistent("SISTEMACLASSI"),
        DisplayName("Classi Unità Tecnologica")]
        [RuleRequiredField("RReqField.ApparatoSchedaMP.SistemaClassi", DefaultContexts.Save, "Sistema Classi è un campo obbligatorio")]
        [Appearance("ApparatoSchedaMP.Abilita.SistemaClassi", Criteria = "(SistemaTecnologico  is null) OR (not (Sistema is null))", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("SistemaTecnologico = '@This.SistemaTecnologico'")]
        [ExplicitLoading()]
        public SistemaClassi SistemaClassi
        {
            get
            {
                return fSistemaClassi;
            }
            set
            {
                SetPropertyValue<SistemaClassi>("SistemaClassi", ref fSistemaClassi, value);
            }
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"),
        DisplayName("Unità Tecnologica")]
        [RuleRequiredField("RReqField.ApparatoSchedaMP.Sistema", DefaultContexts.Save, "Sistema è un campo obbligatorio")]
        [Appearance("ApparatoSchedaMP.Abilita.Sistema", Criteria = "(SistemaClassi  is null) OR (not (StdApparatoClassi is null))", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("SistemaClassi = '@This.SistemaClassi'")]
        [ExplicitLoading()]
        public Sistema Sistema
        {
            get
            {
                return fSistema;
            }
            set
            {
                SetPropertyValue<Sistema>("Sistema", ref fSistema, value);
            }
        }

        private StdApparatoClassi fStdApparatoClassi;
        [Persistent("APPARATOSTDCLASSI"),
        DisplayName("Classi Tipo Apparato")]
        [RuleRequiredField("RReqField.ApparatoSchedaMP.StdApparatoClassi", DefaultContexts.Save, "StdApparatoClassi è un campo obbligatorio")]
        [Appearance("ApparatoSchedaMP.Abilita.StdApparatoClassi", Criteria = "(Sistema is null) OR (not (StdApparato is null))", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Sistema = '@This.Sistema'")]
        [ExplicitLoading()]
        public StdApparatoClassi StdAssetClassi
        {
            get
            {
                return fStdApparatoClassi;
            }
            set
            {
                SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }
        }

        private Categoria fCategoria;
        [Persistent("CATEGORIA"),
        DisplayName("Categoria Servizio")]
        [RuleRequiredField("RReqField.ApparatoSchedaMp.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        [ExplicitLoading()]
        public Categoria Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<Categoria>("Categoria", ref fCategoria, value);
            }
        }

        private StdAsset fStdApparato;
        [Persistent("APPARATOSTD"),   DisplayName("Tipo Apparato")]
        [RuleRequiredField("RReqFieldObject.ApparatoSchedaMp.StdApparato", DefaultContexts.Save, "La Tipologia di Apparato è un campo obligatorio")]
        [ToolTip("Valorizzando il tipo di Apparecchiatura saranno inseriti i valori di Default ai K Correttivi")]
        [Appearance("ApparatoSchedaMp.Abilita.StdApparato", Criteria = "(StdApparatoClassi is null)", Enabled = false)]
        [DataSourceCriteria("StdApparatoClassi = '@This.StdApparatoClassi'")]
        [ExplicitLoading()]
        public StdAsset StdAsset
        {
            get
            {
                return fStdApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value);
            }
        }

        private string fSottoComponente;
        [Persistent("SOTTOCOMPONENTE"),
        Size(100),
        DisplayName("SottoComponente")]
        [DbType("varchar(100)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SottoComponente
        {
            get
            {
                return fSottoComponente;
            }
            set
            {
                SetPropertyValue<string>("SottoComponente", ref fSottoComponente, value);
            }
        }

        private string fDescrizioneManutenzione;
        [Persistent("DESCRIZIONEMANUTENZIONE"), Size(4000), DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        [RuleRequiredField("RReqFieldObject.ApparatoSchedaMp.Manutenzione", DefaultContexts.Save, "La Descrizione Manutenzione è un campo obbligatorio")]
        public string DescrizioneManutenzione
        {
            get
            {
                return fDescrizioneManutenzione;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneManutenzione", ref fDescrizioneManutenzione, value);
            }
        }

        private TipoCogenzaNormativa fCogenzaNormativa;
        [Persistent("NORMACOGENTE"), DisplayName("Norma Cogente")]
        [ImmediatePostData]
        public TipoCogenzaNormativa CogenzaNormativa
        {
            get
            {
                return fCogenzaNormativa;
            }
            set
            {
                SetPropertyValue<TipoCogenzaNormativa>("CogenzaNormativa", ref fCogenzaNormativa, value);

            }
        }

        #region PARAMETRI SCHEDULAZIONE
        private Frequenze fFrequenzaOpt;
        [Persistent("FREQUENZAOPT"),
        DisplayName("Frequenza")]
        [RuleRequiredField("RReqFieldObject.ApparatoSchedaMp.FrequenzaOpt", DefaultContexts.Save, "La Frequenza è un campo obbligatorio")]
        [ExplicitLoading()]
        public Frequenze FrequenzaOpt
        {
            get
            {
                return fFrequenzaOpt;
            }
            set
            {
                SetPropertyValue<Frequenze>("Frequenze", ref fFrequenzaOpt, value);
            }
        }

        private Mansioni fMansioniOpt;
        [Persistent("MANSIONIOPT"),
        DisplayName("Mansione")]
        public Mansioni MansioniOpt
        {
            get
            {
                return fMansioniOpt;
            }
            set
            {
                SetPropertyValue<Mansioni>("Mansioni", ref fMansioniOpt, value);
            }
        }

        private int fTempoOpt = 0;
        [Persistent("TEMPOOPT"),
        DisplayName("Tempo [min.]")]
        public int TempoOpt
        {
            get
            {
                return fTempoOpt;
            }
            set
            {
                SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value);
            }
        }

        #region Dati a supporto del Polo Energia
        private RiferimentiNormativi fRiferimentiNormativi;
        [Persistent("RIFERIMENTINORMATIVI"),
        DisplayName("Riferimenti Normativi")]
        public RiferimentiNormativi RiferimentiNormativi
        {
            get
            {
                return fRiferimentiNormativi;
            }
            set
            {
                SetPropertyValue<RiferimentiNormativi>("RiferimentiNormativi", ref fRiferimentiNormativi, value);
            }
        } 

        private Insourcing fInsourcing;
        [Persistent("INSOURCING"), DisplayName("Insourcing")]
        public Insourcing Insourcing
        {
            get
            {
                return fInsourcing;
            }
            set
            {
                SetPropertyValue<Insourcing>("Insourcing", ref fInsourcing, value);
            }
        }

        private TipologiaIntervento fTipologiaIntervento;
        [Persistent("TIPOLOGIAINTERVENTO"), DisplayName("Tipologia Intervento")]
        public TipologiaIntervento TipologiaIntervento
        {
            get
            {
                return fTipologiaIntervento;
            }
            set
            {
                SetPropertyValue<TipologiaIntervento>("TipologiaIntervento", ref fTipologiaIntervento, value);
            }
        }

        private StatoComponente fStatoComponente;
        [Persistent("STATOCOMPONENTE"), DisplayName("Stato Componente")]
        public StatoComponente StatoComponente
        {
            get
            {
                return fStatoComponente;
            }
            set
            {
                SetPropertyValue<StatoComponente>("StatoComponente", ref fStatoComponente, value);
            }
        }


        [Persistent("MANUALEAPPARATO"), DisplayName("Manuale Uso e Manutenzione dell'Apparato")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData ManualeAsset
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("ManualeApparato");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("ManualeApparato", value);
            }
        }
        #endregion

        

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"),
        DisplayName("Attivo")]
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


        private FlgAbilitato fAbilitazioneEreditata;
        [Persistent("ABILITAZIONETRASMESSA"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo da Gerarchia")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [Appearance("appSkMP.AbilitazioneEreditata", FontColor = "Black", Enabled = false)]
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


        private TipoNumeroManutentori fNumMan;// = TipoNumeroManutentori.unaPersona;
        [Persistent("NUMMAN"), DisplayName("N° Addetto")]
        [ImmediatePostData]      
        public TipoNumeroManutentori NumMan
        {
            get
            {
                return fNumMan;
            }
            set
            {
                if (SetPropertyValue<TipoNumeroManutentori>("NumMan", ref fNumMan, value))
                {
                    OnChanged("TempoTotAnno");
                }
            }
        }


        #region schedulaZIONE MEMO
        private Frequenze fSKFrequenzaOpt;
        [Persistent("SKFREQUENZAOPT"),  DisplayName("SKFrequenza")]
            [System.ComponentModel.Browsable(false)]
            public Frequenze SKFrequenzaOpt
        {
            get
            {
                return fSKFrequenzaOpt;
            }
            set
            {
                SetPropertyValue<Frequenze>("SKFrequenze", ref fSKFrequenzaOpt, value);
            }
        }

        private Mansioni fSKMansioniOpt;
        [Persistent("SKMANSIONIOPT"),   DisplayName("SKMansione")]
        [System.ComponentModel.Browsable(false)]
            public Mansioni SKMansioniOpt
        {
            get
            {
                return fSKMansioniOpt;
            }
            set
            {
                SetPropertyValue<Mansioni>("SKMansioni", ref fSKMansioniOpt, value);
            }
        }

        private int fSKNumMan;// = 1;
        [Persistent("SKNUMMAN"),  DisplayName("SKN° Addetto")]
        [System.ComponentModel.Browsable(false)]
            public int SKNumMan
        {
            get
            {
                return fSKNumMan;
            }
            set
            {
                SetPropertyValue<int>("SKNumMan", ref fSKNumMan, value);
            }
        }

        private int fSKTempoOpt = 0;
        [Persistent("SKTEMPOOPT"),        DisplayName("SKTempo [min.]")]
        [System.ComponentModel.Browsable(false)]
            public int SKTempoOpt
        {
            get
            {
                return fSKTempoOpt;
            }
            set
            {
                SetPropertyValue<int>("SKTempoOpt", ref fSKTempoOpt, value);
            }
        }
        #endregion
        #endregion

        ///  tempo totale ottimizzato in base all'ANNO  Iif('@This.Apparato' is null,null, StandardApparato = '@This.Apparato.StdApparato')
        [PersistentAlias("Iif(SchedaMp.FrequenzaOpt.CadenzeAnno is not null,SchedaMp.FrequenzaOpt.CadenzeAnno,1) * " +
                             "Iif(SchedaMp.TempoOpt is not null,SchedaMp.TempoOpt, 1) * " +
                             "Iif(SchedaMp.NumMan is not null, SchedaMp.NumMan, 1) * " +
                             "Iif(Apparato.Quantita is not null, Apparato.Quantita , 1)"),
        DisplayName("Tot Tempo Anno")]
        [Appearance("TempoTotOPTMaggiorediOPT.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "TempoTotOPT = 0")]
        public double TempoTotOPT
        {
            get
            {
                var tempObject = EvaluateAlias("TempoTotOPT");
                if (tempObject != null)
                {
                    double d = 0;
                    if (double.TryParse(tempObject.ToString(), out d))
                    {
                        return d;
                    }
                    return 0;
                    //return risult;

                    //var ca = this.FrequenzaOpt.CadenzeAnno;
                    //double topt = this.TempoOpt;
                    //double nm = 1;
                    //if (this.NumMan == TipoNumeroManutentori.duePersone) nm = 2;
                    //double q = Apparato.Quantita;
                    //var fat1 = ca * topt * nm * q;

                    //double Fatt = 0;

                    //if (Apparato.TotaleCoefficienti != null)
                    //{
                    //    Fatt = Apparato.TotaleCoefficienti;
                    //}
                    ////Fatt = 1 + Fatt;

                    //var risult = (double)fat1 * Fatt;
                    //return risult;
                }
                else
                {
                    return 0;
                }
            }
        }

        //[PersistentAlias("Apparato.Impianto.Immobile"),  DisplayName("Immobile")]
        //public Immobile Immobile
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("Immobile");
        //        if (tempObject != null)
        //        {
        //            return (Immobile)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}


        [Persistent("CLUSTEREDIFICI"),     DevExpress.Xpo.DisplayName("Cluster Edifici")]
        [Appearance("ApparatoSchedaMP.ClusterEdifici")]//, Enabled = false)]
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

        [Persistent("SCENARIO"),     DevExpress.Xpo.DisplayName("Scenario")]
        [Appearance("ApparatoSchedaMP.Scenario")]//, Enabled = false)]
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

        private RegPianificazioneMP fRegistroPianificazioneMP;
        [MemberDesignTimeVisibility(false)]
        [Persistent("MPREGPIANIFICAZIONE"),        DisplayName("Registro Pianificazione MP")]
        public RegPianificazioneMP RegistroPianificazioneMP
        {
            get
            {
                return fRegistroPianificazioneMP;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP", ref fRegistroPianificazioneMP, value);
            }
        }

        private bool fSuddividiAtt;
        [Persistent("SUDDIVIDIATT"), DisplayName("Suddividi Attività")]
        public bool SuddividiAtt
        {
            get
            {
                return fSuddividiAtt;
            }
            set
            {
                SetPropertyValue<bool>("SuddividiAtt", ref fSuddividiAtt, value);
            }
        }

        private DateTime fDataUltimaEsecuzione;
        [Persistent("DATAULTIMAESECUZIONE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataUltimaEsecuzione
        {
            get
            {
                return fDataUltimaEsecuzione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataUltimaEsecuzione", ref fDataUltimaEsecuzione, value);
            }
        }

        private string fAgg_RegRdl;
        [Persistent("AGG_REGRDL"), Size(250), DisplayName("Agg RegRdl")]
        [DbType("varchar(250)")]
        [ImmediatePostData]
        [ToolTip("Descrizione della aggregazione da elaborare in creazione Registro di Lavoro")]
        //[Appearance("ApparatoSchedaMP.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Agg_RegRdl
        {
            get
            {
                return fAgg_RegRdl;
            }
            set
            {
                SetPropertyValue<string>("Agg_RegRdl", ref fAgg_RegRdl, value);
            }
        }

        private string fAgg_Rdl;
        [Persistent("AGG_RDL"), Size(250), DisplayName("Agg Rdl")]
        [DbType("varchar(250)")]
        [ImmediatePostData]
        [ToolTip("Descrizione della aggregazione da elaborare in creazione Richiesta di Lavoro")]
        //[Appearance("ApparatoSchedaMP.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Agg_Rdl
        {
            get
            {
                return fAgg_Rdl;
            }
            set
            {
                SetPropertyValue<string>("Agg_Rdl", ref fAgg_Rdl, value);
            }
        }



        #region data aggiornamento + utente
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
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

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        [System.ComponentModel.Browsable(false)]

        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }
        #endregion

        #region proprieta di default


        public override string ToString()
        {
            return string.Format("{0}({1}) - {2}", CodSchedaMp, CodUni, DescrizioneManutenzione);
        }
        #endregion
    }
}
