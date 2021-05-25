using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.DBTask;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using System.Globalization;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.Model;

namespace CAMS.Module.DBPlanner
{

    [DefaultClassOptions, Persistent("MPATTIVITAPIANIFICATEDETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Attività Pianificate Dettaglio")]
    [ImageName("WeekEnd")]
    [NavigationItem(false)]
    public class MpAttivitaPianificateDett : XPObject
    {
        public MpAttivitaPianificateDett() : base() { }

        public MpAttivitaPianificateDett(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();


        }

        private MpAttivitaPianificate fMpAttPianificate;
        [Persistent("MPATTIVITAPIANIFICATE"), XafDisplayName("Attività Pianificate")]
        [Association("AttivitaPianificate_AttivitaPianificateDett", typeof(MpAttivitaPianificate))]
        public MpAttivitaPianificate MpAttPianificate
        {
            get
            {

                return fMpAttPianificate;
            }
            set
            {
                SetPropertyValue<MpAttivitaPianificate>("MpAttPianificate", ref fMpAttPianificate, value);
            }
        }
        #region associazioni relative alle attività pianificate dettaglio

        [NonPersistent]
        private XPCollection<SchedaMpPassi> fSchedaMpPassis;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Passi Attività MP")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<SchedaMpPassi> SchedaMpPassis
        {
            get
            {
                if (this.Oid == -1) return null;
                if (this.MpAttPianificate != null && this.MpAttPianificate.ApparatoSchedaMP != null && this.MpAttPianificate.ApparatoSchedaMP.SchedaMp != null)
                {
                    string ParCriteria = string.Format("SchedaMp.Oid == {0}", Evaluate("MpAttPianificate.ApparatoSchedaMP.SchedaMp.Oid"));

                    fSchedaMpPassis = new XPCollection<SchedaMpPassi>(Session, CriteriaOperator.Parse(ParCriteria));
                    //fSchedaMpPassis.Criteria = CriteriaOperator.Parse(ParCriteria);                    
                }
                return fSchedaMpPassis;
            }
        }
        [PersistentAlias("MpAttPianificate.ApparatoSchedaMP.CodSchedaMp + '(' + MpAttPianificate.ApparatoSchedaMP.CodUni + ')'")]
        [XafDisplayName("Cod. Attività MP")]
        public string CodSchedaMPUni
        {
            get
            {
                object tempObject = EvaluateAlias("CodSchedaMPUni");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        [PersistentAlias("MpAttPianificate.ApparatoSchedaMP.DescrizioneManutenzione")]
        [XafDisplayName("Desc. Attività MP")]
        public string DescSchedaMP
        {
            get
            {
                object tempObject = EvaluateAlias("DescSchedaMP");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        #endregion

        private DateTime fData;
        [Persistent("DATAFISSA"), XafDisplayName("Data"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
        [ImmediatePostData]
        public DateTime Data
        {
            get
            {
                return fData;
            }

            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }

        //private RdL fRdL;
        //[Persistent("RDL"), XafDisplayName("RdL Associate")]
        //[Association("RdL_AttivitaPianificateDett", typeof(RdL))]
        //public RdL RdL
        //{
        //    get
        //    {
        //        return fRdL;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RdL>("RdL", ref fRdL, value);
        //    }
        //}
        private RdL fRdL;
        [Persistent("RDL"), XafDisplayName("RdL Associate")]
        [Association("RdL_AttivitaPianificateDett", typeof(RdL))]
        [VisibleInListView(false)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public RdL RdL
        {
            get
            {
                return GetDelayedPropertyValue<RdL>("RdL");
            }
            set
            {
                SetDelayedPropertyValue<RdL>("RdL", value);
            }

        }


        private string fAgg_RegRdl;
        [Persistent("AGG_REGRDL"), Size(250), XafDisplayName("Agg RegRdl")]
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
        [Persistent("AGG_RDL"), Size(250), XafDisplayName("Agg Rdl")]
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



        #region legame con GHOST
        private Ghost fGhost;
        [Persistent("MPGHOST"), XafDisplayName("Ghost")]
        [Association("Ghost_ANNO_AttivitaPianiDett", typeof(Ghost))]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("AllowClear", "False")]
        [Delayed(true)]
        public Ghost Ghost
        {
            get
            {
                return GetDelayedPropertyValue<Ghost>("Ghost");
                //return fGhost;
            }
            set
            {
                SetDelayedPropertyValue<Ghost>("Ghost", value);
            }
        }

        private GhostDettaglio fGhostDettaglio; //MemberDesignTimeVisibility(true)
        [Persistent("MPGHOSTDETTAGLIO"), XafDisplayName("Ghost Settimanale")]
        [Association("Ghost_SETT_AttivitaPianiDett", typeof(GhostDettaglio))]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("AllowClear", "False")]
        [Delayed(true)]
        public GhostDettaglio GhostDettaglio
        {
            get
            {
                return GetDelayedPropertyValue<GhostDettaglio>("GhostDettaglio");
                //return fGhostDettaglio;
            }
            set
            {
                SetDelayedPropertyValue<GhostDettaglio>("GhostDettaglio", value);
                //SetPropertyValue("GhostDettaglio", ref fGhostDettaglio, value);
            }
        }

        private GhostDettaglioGG fGhostDettaglioGG;
        [Persistent("MPGHOSTDETTAGLIOGG"), XafDisplayName("Ghost Giornaliero")]
        [Association("Ghost_GG_AttivitaPianiDett", typeof(GhostDettaglioGG))]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("AllowClear", "False")]
        [Delayed(true)]
        public GhostDettaglioGG GhostDettaglioGG
        {
            get
            {
                return GetDelayedPropertyValue<GhostDettaglioGG>("GhostDettaglioGG");
                //return fGhostDettaglioGG;
            }
            set
            {
                SetDelayedPropertyValue<GhostDettaglioGG>("GhostDettaglioGG", value);
                //SetPropertyValue("GhostDettaglioGG", ref fGhostDettaglioGG, value);
            }
        }

        private CentroOperativo fCentroOperativo;
        [Persistent("CENTROOPERATIVO"), XafDisplayName("CentroOperativo")]
        [ModelDefault("AllowEdit", "False")]
        [ModelDefault("AllowClear", "False")]
        [Delayed(true)]
        public CentroOperativo CentroOperativo
        {
            get { return GetDelayedPropertyValue<CentroOperativo>("CentroOperativo"); }
            set { SetDelayedPropertyValue<CentroOperativo>("CentroOperativo", value); }
        }
        //public CentroOperativo CentroOperativo
        //{
        //    get
        //    {
        //        return fCentroOperativo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<CentroOperativo>("CentroOperativo", ref fCentroOperativo, value);
        //    }
        //}

        private Double fUSLG;
        [Size(10), Persistent("USLG"), XafDisplayName("Unità Standard Lavoro Giornaliero")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public Double USLG
        {
            get { return GetDelayedPropertyValue<Double>("USLG"); }
            set { SetDelayedPropertyValue<Double>("USLG", value); }
        }
        //public Double USLG
        //{
        //    get
        //    {
        //        return fUSLG;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Double>("USLG", ref fUSLG, value);
        //    }
        //}

        #endregion

        #region  carico e capacita
        private int fTempoCaricoGiorno;
        [Persistent("TEMPO_CARICO_GIORNO"), DevExpress.ExpressApp.DC.XafDisplayName("Carico Giornaliero")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public int TempoCaricoGiorno
        {
            get { return GetDelayedPropertyValue<int>("TempoCaricoGiorno"); }
            set { SetDelayedPropertyValue<int>("TempoCaricoGiorno", value); }
        }
        //public int TempoCaricoGiorno
        //{
        //    get
        //    {
        //        return fTempoCaricoGiorno;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("TempoCaricoGiorno", ref fTempoCaricoGiorno, value);
        //    }
        //}

        [PersistentAlias("TempoCaricoGiorno / USLG"), DevExpress.ExpressApp.DC.XafDisplayName("Uomini/Giorno Richiesti")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public double PersoneggRichieste
        {
            get
            {
                object tempObject = EvaluateAlias("PersoneggRichieste");
                if (tempObject != null)
                {
                    double tmp = (double)tempObject;
                    return Math.Round(tmp, 2);
                }
                else
                {
                    return 0;
                }

            }

        }

        private int fTempoCaricoTrasferimento;
        [Persistent("TEMPO_CARICO_TRASFERIMENTO"), DevExpress.ExpressApp.DC.XafDisplayName("Carico Trasferimento")]
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
        [Persistent("TEMPO_CARICO_INSITO"), DevExpress.ExpressApp.DC.XafDisplayName("Carico in Sito")]
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
        //
        private int fCarico;
        [Persistent("CARICO"), XafDisplayName("Carico Totale Attività")]
        [Appearance("AttivitaDettaglio.Carico", Enabled = false)]
        public int Carico
        {
            get
            {
                return fCarico;
            }
            set
            {
                SetPropertyValue<int>("Carico", ref fCarico, value);
            }
        }

        //private int fCaricoInSito;
        //[Persistent("CARICO_INSITO"), DisplayName("Carico In Sito")]
        //[Appearance("AttivitaDettaglio.CaricoInSito", Enabled = false)]
        //public int CaricoInSito
        //{
        //    get
        //    {
        //        return fCaricoInSito;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CaricoInSito", ref fCaricoInSito, value);
        //    }
        //}
        #endregion
        #region carico nei trasferimenti
        private int fCaricoTrasferimentoCOE;
        [Persistent("CARICO_TRASF_CO_E"), XafDisplayName("Carico Trasferimento da CO a Sito")]
        [Appearance("GhostDettaglioGG.CaricoTrasferimentoCOE", Enabled = false)]
        [VisibleInListView(false)]
        public int CaricoTrasferimentoCOE
        {
            get
            {
                return fCaricoTrasferimentoCOE;
            }
            set
            {
                SetPropertyValue<int>("CaricoTrasferimentoCOE", ref fCaricoTrasferimentoCOE, value);
            }
        }

        private int fCaricoTrasferimentoECO;
        [Persistent("CARICO_TRASF_E_CO"), XafDisplayName("Carico Trasferimentoda da Sito a CO")]
        [Appearance("GhostDettaglioGG.fCaricoTrasferimentoECO", Enabled = false)]
        [VisibleInListView(false)]
        public int CaricoTrasferimentoECO
        {
            get
            {
                return fCaricoTrasferimentoECO;
            }
            set
            {
                SetPropertyValue<int>("CaricoTrasferimentoECO", ref fCaricoTrasferimentoECO, value);
            }
        }

        private int fCaricoTrasferimentoEE;
        [Persistent("CARICO_TRASF_E_E"), XafDisplayName("Carico Trasferimentoda da Sito a Sito")]
        [Appearance("GhostDettaglioGG.fCaricoTrasferimentoEE", Enabled = false)]
        [VisibleInListView(false)]
        public int CaricoTrasferimentoEE
        {
            get
            {
                return fCaricoTrasferimentoEE;
            }
            set
            {
                SetPropertyValue<int>("CaricoTrasferimentoEE", ref fCaricoTrasferimentoEE, value);
            }
        }
        #endregion

        #region Alias (Apparto,schedaappartomp,mansione,frequenza)

        [Persistent("SCENARIO"), XafDisplayName("Scenario")]
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



        [Persistent("CLUSTEREDIFICI"), XafDisplayName("Cluster")]
        [VisibleInListView(false)]
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
        [Persistent("IMMOBILE"), XafDisplayName("Immobile")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }


        private Servizio fServizio;
        [Persistent("SERVIZIO"), XafDisplayName("Servizio")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }

        private Asset fApparato;
        [Persistent("ASSET"), XafDisplayName("Asset")]
        [Delayed(true)]
        public Asset Asset
        {
            get { return GetDelayedPropertyValue<Asset>("Asset"); }
            set { SetDelayedPropertyValue<Asset>("Asset", value); }
        }
        //public Apparato Apparato
        //{
        //    get
        //    {
        //        return fApparato;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Apparato>("Apparato", ref fApparato, value);
        //    }
        //}

        private Mansioni fMansione;
        [Persistent("MANSIONE"), XafDisplayName("Mansione")]
        [Delayed(true)]
        public Mansioni Mansione
        {
            get { return GetDelayedPropertyValue<Mansioni>("Mansione"); }
            set { SetDelayedPropertyValue<Mansioni>("Mansione", value); }
        }
        //public Mansioni Mansione
        //{
        //    get
        //    {
        //        return fMansione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Mansioni>("Mansione", ref fMansione, value);
        //    }
        //}


        private TipoNumeroManutentori fNumMan;
        [Persistent("NUMMAN"), XafDisplayName("Coppia Linkata")]
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

        private Frequenze fFrequenza;
        [Persistent("FREQUENZA"), XafDisplayName("Frequenza")]
        [Delayed(true)]
        public Frequenze Frequenza
        {
            get { return GetDelayedPropertyValue<Frequenze>("Frequenza"); }
            set { SetDelayedPropertyValue<Frequenze>("Frequenza", value); }
        }
        //public Frequenze Frequenza
        //{
        //    get
        //    {
        //        return fFrequenza;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Frequenze>("Frequenza", ref fFrequenza, value);
        //    }
        //}

        private TipoStatoOperativoAttivitaDettaglio fSOperativo;
        [Persistent("SOPERATIVO"), XafDisplayName("Stato Operativo")]
        [VisibleInListView(false)]
        public TipoStatoOperativoAttivitaDettaglio SOperativo
        {
            get
            {
                return fSOperativo;
            }
            set
            {
                SetPropertyValue<TipoStatoOperativoAttivitaDettaglio>("SOperativo", ref fSOperativo, value);
            }
        }


        //MemberDesignTimeVisibility(false),
        private RegPianificazioneMP fRegPianificazione;
        [Association(@"RegPianiMP_AttivitaPianiDettaglio", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANIFICAZIONE"), XafDisplayName("Registro Pianificazione MP")]
        [Browsable(false)]
        [Delayed(true)]
        public RegPianificazioneMP RegPianificazione
        {
            get { return GetDelayedPropertyValue<RegPianificazioneMP>("RegPianificazione"); }
            set { SetDelayedPropertyValue<RegPianificazioneMP>("RegPianificazione", value); }
        }
        //public RegPianificazioneMP RegPianificazione
        //{
        //    get
        //    {
        //        return fRegPianificazione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegPianificazioneMP>("RegPianificazione", ref fRegPianificazione, value);
        //    }
        //}

        private string fUtente;
        [Size(100),
        Persistent("UTENTE"), XafDisplayName("Utente")]
        [Browsable(false)]
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


        private int fAnno;
        [PersistentAlias("GetYear(Data)"), System.ComponentModel.DisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false)]
        public int Anno
        {
            get
            {
                if (this.Oid == -1) return 0;
                var tempObject = EvaluateAlias("Anno");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
                return fAnno;
            }
        }

        private int fMese;
        [PersistentAlias("GetMonth(Data)"), System.ComponentModel.DisplayName("Mese")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false)]
        public int Mese
        {
            get
            {

                //Calendar c = new Calendar();
                //c.GetWeekOfYear();
                var tempObject = EvaluateAlias("Mese");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
                return fMese;
            }
        }



        [PersistentAlias("Iif(Data is not null, ToInt(Ceiling(ToFloat(GetDayOfYear(Data) - GetDayOfWeek(Data) - 1) / 7) + 1), -1)")]
        [System.ComponentModel.DisplayName("Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false)]
        public int Settimana
        {
            get
            {
                var tempObject = EvaluateAlias("Settimana");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 1;
                }
            }
        }

        #endregion
    }
}




