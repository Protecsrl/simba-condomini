using CAMS.Module.DBPlant;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;


namespace CAMS.Module.DBKPI
{
    [DefaultClassOptions, Persistent("V_BASE_KPI_SOPERA_SMISTA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI Avanzamneto Lavori")]
    [NavigationItem(false)]
    [ImageName("StackedLine")]
    public class kpiStatoSmistamentoOperativo : XPLiteObject
    {

        public kpiStatoSmistamentoOperativo() : base() { }

        public kpiStatoSmistamentoOperativo(Session session) : base(session) { }

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

        private string fTipoStato;
        [Persistent("TIPOSTATO"), DisplayName("TipoStato")]
        [DbType("varchar(250)")]
        public string TipoStato
        {
            get
            {
                return fTipoStato;
            }
            set
            {
                SetPropertyValue<string>("TipoStato", ref fTipoStato, value);
            }
        }
        private string fStato;
        [Persistent("STATO"), DisplayName("Stato")]
        [DbType("varchar(250)")]
        public string Stato
        {
            get
            {
                return fStato;
            }
            set
            {
                SetPropertyValue<string>("Stato", ref fStato, value);
            }
        }

        private string fSSmistamento;
        [Persistent("STATOSMISTAMENTO"), DisplayName("Stato Smistamento")]
        [DbType("varchar(100)")]
        public string SSmistamento
        {
            get
            {
                return fSSmistamento;
            }
            set
            {
                SetPropertyValue<string>("SSmistamento", ref fSSmistamento, value);
            }
        }
        private string fSOperativo;
        [Persistent("STATOOPERATIVO"), DisplayName("Stato Operativo")]
        [DbType("varchar(100)")]
        public string SOperativo
        {
            get
            {
                return fSOperativo;
            }
            set
            {
                SetPropertyValue<string>("SOperativo", ref fSOperativo, value);
            }
        }

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        //[RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
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



        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataOra;
        [Persistent("DATAORA"), DisplayName("Data Cambio Stato")]
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
        private RegistroRdL fRegistroRdL;
        [Persistent("REGRDL"), DisplayName("RdL")]
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
        private DateTime fDataCreazione;
        [Persistent("DATA_CREAZIONE_RDL"), DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
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
        private int fDeltaTempo;
        [Persistent("DELTATEMPO"), DisplayName("Tempo (min)")]
        [ModelDefault("DisplayFormat", "{0:D}")]
        //[DbType("number")]   commetato x passaggio a MSSQL
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

    }
}




