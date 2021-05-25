using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlant;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,    Persistent("MPATTIVITAPIANIFICATE")]
    [Indices("Anno", "Mese", "Settimana")]

    [System.ComponentModel.DefaultProperty("Apparato")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Attività Pianificate")]
    [ImageName("WeekEnd")]
    [Appearance("MpAttivitaPianificate", TargetItems = "Scenario;ClusterEdifici;Impianto", Criteria = "1 = 1", Enabled = false)]
    [NavigationItem("Planner")]

    #region filtro tampone

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.1TrimAnnoinCorso", "[Mese] In(1,2,3) And [Anno] = GetYear(Now())", @"1° Trimestre", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.2TrimAnnoinCorso", "[Mese] In(4,5,6) And [Anno] = GetYear(Now())", @"2° Trimestre", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.3TrimAnnoinCorso", "[Mese] In(7,8,9) And [Anno] = GetYear(Now())", @"3° Trimestre", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.4TrimAnnoinCorso", "[Mese] In(10,11,12) And [Anno] = GetYear(Now())", @"4° Trimestre", Index = 3)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.1SemAnnoinCorso", "[Mese] In(1,2,3,4,5,6) And [Anno] = GetYear(Now())", @"1° Semestre", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.2SemAnnoinCorso", "[Mese] In(7,8,9,10,11,12) And [Anno] = GetYear(Now())", @"2° Semestre", Index = 5)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.AnnoinCorso", "[Anno] = GetYear(Now())", @"Anno in Corso", Index = 6)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.1TrimAnnoScorso", "[Mese] In(1,2,3) And [Anno] = GetYear(AddYears(Now(), -1))", @"1° Trimestre (Anno Scorso)", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.2TrimAnnoScorso", "[Mese] In(4,5,6) And [Anno] = GetYear(AddYears(Now(), -1))", @"2° Trimestre (Anno Scorso)", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.3TrimAnnoScorso", "[Mese] In(7,8,9) And [Anno] = GetYear(AddYears(Now(), -1))", @"3° Trimestre (Anno Scorso)", Index = 9)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.4TrimAnnoScorso", "[Mese] In(10,11,12) And [Anno] = GetYear(AddYears(Now(), -1))", @"4° Trimestre (Anno Scorso)", Index = 10)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.1TrimAnnoProssimo", "[Mese] In(1,2,3) And [Anno] = GetYear(AddYears(Now(), 1))", @"1° Trimestre (Anno Prossimo)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.2TrimAnnoSProssimo", "[Mese] In(4,5,6) And [Anno] = GetYear(AddYears(Now(), 1))", @"2° Trimestre (Anno Prossimo)", Index = 12)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.3TrimAnnoProssimo", "[Mese] In(7,8,9) And [Anno] = GetYear(AddYears(Now(), 1))", @"3° Trimestre (Anno Prossimo)", Index = 13)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificate.4TrimAnnoProssimo", "[Mese] In(10,11,12) And [Anno] = GetYear(AddYears(Now(), 1))", @"4° Trimestre (Anno Prossimo)", Index = 14)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("MpAttivitaPianificateTutto", "", "Tutto", Index = 15)]


    #endregion

    public class MpAttivitaPianificate : XPObject
    {
        public MpAttivitaPianificate()
            : base()
        {
        }
        public MpAttivitaPianificate(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [Persistent("SCENARIO"),
        DisplayName("Scenario")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
         
        [Persistent("CLUSTEREDIFICI"),
        DisplayName("Cluster")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
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
        [Persistent("SERVIZIO"),
        DisplayName("Servizio")]
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
        private Asset fApparato;
        [ Persistent("APPARATO"),
        DisplayName("Apparato")]
        public Asset Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<Asset>("Apparato", ref fApparato, value);
            }
        } 

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPARATOSCHEDAMP"),
       DisplayName("Apparato Scheda MP")]
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



        private int fAnno;
        [Appearance("EnableAnno", Enabled = false, Context = "DetailView")]
        [Persistent("ANNO"),
        DisplayName("Anno"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public int Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<int>("Anno", ref fAnno, value);
            }
        }

        private int fMese;
        [ Persistent("MESE"),
        DisplayName("Mese"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
            }
        }

        private int fSettimana;
        [Persistent("SETTIMANA"),
        DisplayName("Settimana"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public int Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<int>("Settimana", ref fSettimana, value);
            }
        }

        private LivelloAccorpamento fLivelloAccorpamento;
        [NonPersistent,
        DisplayName("Distribuzione")]
        [Appearance("MPAttivitaPiano.LivelloAccorpamento", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public LivelloAccorpamento LivelloAccorpamento
        {
            get
            {
                object Temp = ClusterEdifici;
                if (Temp != null && Oid == -1)
                {
                    fLivelloAccorpamento = ClusterEdifici.LivelloAccorpamento;
                }
                return fLivelloAccorpamento;
            }
        }

        private int fTempoTot = 0;
        [Persistent("TEMPOTOT"), XafDisplayName(@"Tempo Totale"),] //MemberDesignTimeVisibility(false)
        public int TempoTot
        {
            get
            {
                return fTempoTot;
            }
            set
            {
                SetPropertyValue<int>("TempoTot", ref fTempoTot, value);
            }
        }

        private int fTempoCaricoTrasferimento;
        [Persistent("TEMPO_CARICO_TRASFERIMENTO"),XafDisplayName(@"Tempo Trasferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TempoCaricoTrasferimento
        {
            get
            {
                return fTempoCaricoTrasferimento;
            }
            set
            {
                SetPropertyValue<int>("TempoCaricoTrasferimento", ref fTempoCaricoTrasferimento, value);
            }
        }

        private int fTempoCaricoinSito;
        [Persistent("TEMPO_CARICO_INSITO"), XafDisplayName(@"Tempo Attivita in Sito")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TempoCaricoinSito
        {
            get
            {
                return fTempoCaricoinSito;
            }
            set
            {
                SetPropertyValue<int>("TempoCaricoinSito", ref fTempoCaricoinSito, value);
            }
        }

        //private int fTempoAttivitaInSito = 0;
        //[Persistent("TEMPOATTIVITA"), XafDisplayName(@"Tempo Attivita in Sito")]
        //public int TempoAttivitaInSito
        //{
        //    get
        //    {
        //        return fTempoAttivitaInSito;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("TempoAttivitaInSito", ref fTempoAttivitaInSito, value);
        //    }
        //}

        //private int fTempoTrasferimentoInSito ;
        //[Persistent("TEMPOTRASFERIMENTO"), XafDisplayName(@"Tempo Trasferimento in Sito")]
        //public int TempoTrasferimentoInSito
        //{
        //    get
        //    {
        //        return fTempoTrasferimentoInSito;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("TempoTrasferimentoInSito", ref fTempoTrasferimentoInSito, value);
        //    }
        //}

        private TipoGhost fTipoGhost;
        [Persistent("TIPOGHOST"),
        DisplayName("Tipo Ghost")]
        public TipoGhost TipoGhost
        {
            get
            {
                return fTipoGhost;
            }
            set
            {
                SetPropertyValue<TipoGhost>("TipoGhost", ref fTipoGhost, value);
            }
        }

        private TipoNumeroManutentori fCoppiaLinkata;
        [Persistent("COPPIALINKATA"),  DisplayName("Num. Manutentori")]
        public TipoNumeroManutentori CoppiaLinkata
        {
            get
            {
                return fCoppiaLinkata;
            }
            set
            {
                SetPropertyValue<TipoNumeroManutentori>("CoppiaLinkata", ref fCoppiaLinkata, value);
            }
        }
         

        private string fUtente;
        [Size(100),
        Persistent("UTENTE"),
        DisplayName("Utente")]
        [VisibleInListView(false),
        VisibleInLookupListView(false),
        VisibleInDetailView(false)]
        [DbType("varchar(100)")]
        public string Utente
        {
            get
            {
                return fUtente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref fUtente, value);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        [Persistent("DATAUPDATE"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [System.ComponentModel.Browsable(true)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }
        }

        private Mansioni fMansione;
        [Persistent("MANSIONE"),
        DisplayName("Mansione"),
        MemberDesignTimeVisibility(false)]
        public Mansioni Mansione
        {
            get
            {
                return fMansione;
            }
            set
            {
                SetPropertyValue<Mansioni>("Mansione", ref fMansione, value);
            }
        }

        private Frequenze fFrequenza;
        [Persistent("FREQUENZA"),
        MemberDesignTimeVisibility(false)]
        public Frequenze Frequenza
        {
            get
            {
                return fFrequenza;
            }
            set
            {
                SetPropertyValue<Frequenze>("Frequenza", ref fFrequenza, value);
            }
        }

        private Presidiato fPresidiato;
        [Persistent("PRESIDIO"),
        DisplayName("Presidiato"),
        MemberDesignTimeVisibility(false)]
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

        private TipoNumeroManutentori fNumMan;// = 1;
        [Persistent("NUMMAN")]
        [MemberDesignTimeVisibility(false)]
        [VisibleInListView(false),
        VisibleInLookupListView(false),
        VisibleInDetailView(true)]
        public TipoNumeroManutentori NumMan
        {
            get
            {
                return fNumMan;
            }
            set
            {
                SetPropertyValue<TipoNumeroManutentori>("NumMan", ref fNumMan, value);
            }
        }

        [Association(@"AttivitaPianificate_AttivitaPianificateDett", typeof(MpAttivitaPianificateDett)), DisplayName("Attività Pianificate Dettaglio")] //Aggregated
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<MpAttivitaPianificateDett> AttPianificateDetts { get { return GetCollection<MpAttivitaPianificateDett>("AttPianificateDetts"); } }
    }
}

