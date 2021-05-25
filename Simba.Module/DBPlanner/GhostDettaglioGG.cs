using System;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;

using System.Linq;
using System.Collections.Generic;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPGHOSTDETTAGLIOGG")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ghost Giornaliero")]
    [ImageName("BO_Employee")]
    //[DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    // [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [NavigationItem(false)]
    public class GhostDettaglioGG : XPObject
    {
        public GhostDettaglioGG()
            : base()
        {
        }

        public GhostDettaglioGG(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [NonPersistent, DisplayName("Descrizione")]
        public string Descrizione
        {
            get
            {
                var NomeTmp = this.Oid.ToString();
                if (GhostDettaglio != null)
                    NomeTmp = string.Format("{0}Giorno {1}", GhostDettaglio.Descrizione, NrGiorniSaturi);
                return NomeTmp;
            }
        }

        #region tempi di carico  (totale,insito,traferimento)
        private int fCarico;
        [Persistent("CARICO"), DisplayName("Carico")]
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

        private int fCaricoInSito;
        [Persistent("CARICO_INSITO"), DisplayName("Carico In Sito")]
        public int CaricoInSito
        {
            get
            {
                return fCaricoInSito;
            }
            set
            {
                SetPropertyValue<int>("CaricoInSito", ref fCaricoInSito, value);
            }
        }
        private int fCaricoTrasferimento;
        [Persistent("CARICO_TRASFERIMENTO"), DisplayName("Carico Trasferimento")]
        public int CaricoTrasferimento
        {
            get
            {
                return fCaricoTrasferimento;
            }
            set
            {
                SetPropertyValue<int>("CaricoTrasferimento", ref fCaricoTrasferimento, value);
            }
        }

        private Saturazione fSaturazione;
        [Persistent("SATURAZIONE"), DisplayName("Saturo")]
        public Saturazione Saturazione
        {
            get
            {
                return fSaturazione;
            }
            set
            {
                SetPropertyValue<Saturazione>("Saturazione", ref fSaturazione, value);
            }
        }

        private string fSaturo;
        [Persistent("SATURO"), Size(2), DisplayName("Saturo")]
        [DbType("varchar(2)")]
        public string Saturo
        {
            get
            {
                return fSaturo;
            }
            set
            {
                SetPropertyValue<string>("Saturo", ref fSaturo, value);
            }
        }
        #endregion

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


        private int fNrGiorniSaturi;
        [Persistent("NR_GIORNISATURI"), Size(5), DisplayName("Nr. Giorni Saturi")]
        [Appearance("GhostDettaglio.fNrGiorniSaturi", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int NrGiorniSaturi
        {
            get
            {
                return fNrGiorniSaturi;
            }
            set
            {
                SetPropertyValue<int>("NrGiorniSaturi", ref fNrGiorniSaturi, value);
            }
        }


        private GhostDettaglio fGhostDettaglio;
        [MemberDesignTimeVisibility(false), Association(@"GhostDettaglio_GG", typeof(GhostDettaglio))]
        [Persistent("MPGHOSTDETTAGLIO"), DisplayName("Ghost Settimanale")]
        [Appearance("GhostDettaglioGG.GhostDettaglio", Enabled = false)]
        public GhostDettaglio GhostDettaglio
        {
            get
            {
                return fGhostDettaglio;
            }
            set
            {
                SetPropertyValue<GhostDettaglio>("GhostDettaglio", ref fGhostDettaglio, value);
            }
        }

        //private Immobile fEdificio;
        //[Persistent("IMMOBILE"), DisplayName("Immobile")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public Immobile Immobile
        //{
        //    get
        //    {
        //        return fEdificio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Immobile>("Immobile", ref fEdificio, value);
        //    }
        //}


        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        //[ Association(@"TeamRisorse_GhostGG", typeof(GhostDettaglioGG)) ]
        [Persistent("RISORSATEAM"),   System.ComponentModel.DisplayName("Team Operativo")]
        //[DataSourceProperty("ListaFiltraComboRisorseTeam")]
        [ExplicitLoading]
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



        [Association("Ghost_GG_AttivitaPianiDett", typeof(MpAttivitaPianificateDett)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName("Elenco Attività Pianificate Dettaglio")]
        public XPCollection<MpAttivitaPianificateDett> MpAttivitaPianificateDetts
        {
            get
            {
                return GetCollection<MpAttivitaPianificateDett>("MpAttivitaPianificateDetts");
            }
        }

        [DevExpress.Xpo.DisplayName("Nr Attività Dettaglio"), VisibleInLookupListView(false)]
        [PersistentAlias("MpAttivitaPianificateDetts.Count")]
        public int nrAttDett
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("nrAttDett");
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
        [Persistent("DATAFISSA"), DisplayName("Data Cadenza"),
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

        private int fIdCoppiaLinkata;
        [Persistent("IDCOPPIALINKATA")]
        public int IdCoppiaLinkata
        {
            get
            {
                return fIdCoppiaLinkata;
            }
            set
            {
                SetPropertyValue<int>("IdCoppiaLinkata", ref fIdCoppiaLinkata, value);
            }
        }
    }
}










