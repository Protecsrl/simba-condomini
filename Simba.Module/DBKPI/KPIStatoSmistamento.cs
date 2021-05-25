using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.Model;


namespace CAMS.Module.DBKPI
{
         [DefaultClassOptions, Persistent("V_BASE_KPI_STSM")]    
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI Stato Smistamento")]
    [ImageName("StackedLine")]
    [NavigationItem("KPI")]
    public class KPIStatoSmistamento : XPLiteObject
    {


         public KPIStatoSmistamento()
            : base()
        {
        }

         public KPIStatoSmistamento(Session session)
            : base(session)
        {
        }




        private string fcodice;
        [Key, Size(50), Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        [DbType("varchar(50)")]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }

        private Polo fPolo;
        [Persistent("POLO"), DisplayName("Polo")]
        [ExplicitLoading()]
        public Polo Polo
        {
            get
            {
                return fPolo;
            }
            set
            {
                SetPropertyValue<Polo>("Polo", ref fPolo, value);
            }
        }
        private string fSettimana;
        [Persistent("SETTIMANA"), DisplayName("Settimana")]
        public string Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<string>("Settimana", ref fSettimana, value);
            }
        }
        private string fAnno;
        [Persistent("ANNO"), DisplayName("Anno")]
        public string Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<string>("Anno", ref fAnno, value);
            }
        }
        private string fMese;
        [Persistent("MESE"), DisplayName("Mese")]
        public string Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<string>("Mese", ref fMese, value);
            }
        }

        private AreaDiPolo fAreaDiPolo;
        [Persistent("AREADIPOLO"), DisplayName("Area Di Polo")]
        [ExplicitLoading()]
        public AreaDiPolo AreaDiPolo
        {
            get
            {
                return fAreaDiPolo;
            }
            set
            {
                SetPropertyValue<AreaDiPolo>("AreaDiPolo", ref fAreaDiPolo, value);
            }
            
        }
        private CentroOperativo fCentroOperativo;
        [Persistent("CENTROOPERATIVO"), DisplayName("Centro Operativo")]
        [ExplicitLoading()]
        public CentroOperativo CentroOperativo
        {
            get
            {
                return fCentroOperativo;
            }
            set
            {
                SetPropertyValue<CentroOperativo>("CentroOperativo", ref fCentroOperativo, value);
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
                SetPropertyValue<Immobile>("Immobile", ref fImmobile, value);
            }
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO"), DisplayName("Servizio")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue<Servizio>("Impianto", ref fServizio, value);
            }
        }

        private StatoSmistamento fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), DisplayName("Stato Smistamento")]
        [ExplicitLoading()]
        public StatoSmistamento StatoSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("StatoSmistamento", ref fStatoSmistamento, value);
            }
        }

        private RegistroRdL fRegistroRdL;
        [Persistent("REGRDL"), DisplayName("Registro RdL")]
        [ExplicitLoading()]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataOra;
        [Persistent("DATAORA"), DisplayName("Data e Ora")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOra
        {
            get
            {
                return fDataOra;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra", ref fDataOra, value);
            }
        }

        private int fDeltaTempo;
        [Persistent("DELTATEMPO"), DisplayName("Delta Tempo")]
        [ModelDefault("DisplayFormat", "{0:D}")]
        public int DeltaTempo
        {
            get
            {
                return fDeltaTempo;
            }
            set
            {
                SetPropertyValue<int>("DeltaTempo", ref fDeltaTempo, value);
            }
        }

        [Persistent("CLUSTEREDIFICI"), DisplayName("Cluster Edifici")]
        [MemberDesignTimeVisibility(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
               // return fClusterEdifici;
            }
            set
            {
                SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
              //  SetPropertyValue<ClusterEdifici>("ClusterEdifici", ref fClusterEdifici, value);
            }
        }

        [Persistent("SCENARIO"), DisplayName("Scenario")]
        [MemberDesignTimeVisibility(false)]
        [ExplicitLoading()]
             [Delayed(true)] 
        public Scenario Scenario
        {
            get
            {
                return GetDelayedPropertyValue<Scenario>("Scenario");
                //return fScenario;
            }
            set
            {
                SetDelayedPropertyValue<Scenario>("Scenario", value);
                //SetPropertyValue<Scenario>("Scenario", ref fScenario, value);
            }
        }

        #region Risorse team
        private RisorseTeam fRisorseTeam;
        [Persistent("RISORSATEAM"), DisplayName("Team")]
        [ExplicitLoading()]
        public RisorseTeam RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            }
        }
        
        #endregion

        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), DisplayName("Data Creazione")]
        public DateTime DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
            }
        }       

    }
}
