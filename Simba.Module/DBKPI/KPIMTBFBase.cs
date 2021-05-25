using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBKPI
{
    // TABELLA SCRITTURA PRINCIPALE
    [DefaultClassOptions, Persistent("KPI_MTBF")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI MTBF")]
    [ImageName("StackedLine")]
    [NavigationItem("KPI")]

    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    public class KPIMTBFBase : XPObject
    {
        public KPIMTBFBase() : base() { }
        public KPIMTBFBase(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

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

        private Contratti fCommesse;
        [Persistent("CONTRATTO"), DevExpress.Xpo.DisplayName("Commessa")]
        [ExplicitLoading()]
        public Contratti Commesse
        {
            get
            {
                return fCommesse;
            }
            set
            {
                SetPropertyValue<Contratti>("Commesse", ref fCommesse, value);
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
                SetPropertyValue<Servizio>("Servizio", ref fServizio, value);
            }
        }

        private Asset fAsset;
        [Persistent("ASSET"), DisplayName("Asset")]
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


        private RdL fRdL;
        [Persistent("RDL"), DisplayName("RdL")]
        [ExplicitLoading()]
        public RdL RdL
        {
            get
            {
                return fRdL;
            }
            set
            {
                SetPropertyValue<RdL>("RdL", ref fRdL, value);
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataRichiesta
        {
            get
            {
                return fDataRichiesta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value);
            }
        }


        private DateTime fDataCompletamento;
        [Persistent("DATE_COMPLETED"), DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

        private DateTime fdataFermo;
        [Persistent("DATAFERMO"), DisplayName("Data Fermo")]
        public DateTime dataFermo
        {
            get
            {
                return fdataFermo;
            }
            set
            {
                SetPropertyValue<DateTime>("dataFermo", ref fdataFermo, value);
            }
        }

        private DateTime fdataRiavvio;
        [Persistent("DATARIAVVIO"), DisplayName("Data Riavvio")]
        public DateTime dataRiavvio
        {
            get
            {
                return fdataRiavvio;
            }
            set
            {
                SetPropertyValue<DateTime>("dataRiavvio", ref fdataRiavvio, value);
            }
        }

        private tipoMTBF ftipoMTBF;
        [Persistent("TIPOMTBF"), DisplayName("Tipo Mean Time Between To Failure")]
        public tipoMTBF tipoMTBF
        {
            get
            {
                return ftipoMTBF;
            }
            set
            {
                SetPropertyValue<tipoMTBF>("tipoMTBF", ref ftipoMTBF, value);
            }
        }

        private int ftempoMTTF;
        [Persistent("TEMPOMTTF"), DisplayName("Mean Time to Failure")]
        public int tempoMTTF
        {
            get
            {
                return ftempoMTTF;
            }
            set
            {
                SetPropertyValue<int>("tempoMTTF", ref ftempoMTTF, value);
            }
        }

        private int ftempoMTTR;
        [Persistent("TEMPOMTTR"), DisplayName("Mean Time to Repaire")]
        public int tempoMTTR
        {
            get
            {
                return ftempoMTTR;
            }
            set
            {
                SetPropertyValue<int>("tempoMTTR", ref ftempoMTTR, value);
            }
        }

        private int ftempoMTBF;
        [Persistent("TEMPOMTBF"), DisplayName("Mean Time Between To Failure")]
        public int tempoMTBF
        {
            get
            {
                return ftempoMTBF;
            }
            set
            {
                SetPropertyValue<int>("tempoMTBF", ref ftempoMTBF, value);
            }
        }


    }
}



