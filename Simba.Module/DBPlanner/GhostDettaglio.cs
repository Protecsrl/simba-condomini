using System;
using DevExpress.Xpo;
using CAMS.Module.DBALibrary;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using System.Linq;
using System.Collections.Generic;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPGHOSTDETTAGLIO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ghost Settimanali")]
    [ImageName("BO_Employee")]
    [NavigationItem(false)]
    public class GhostDettaglio : XPObject
    {
        public GhostDettaglio()
            : base()
        {
        }

        public GhostDettaglio(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        [NonPersistent,
        DisplayName("Descrizione")]
        public string Descrizione
        {
            get
            {
                var NomeTmp = this.Oid.ToString();
                if (Ghost != null)
                    NomeTmp = string.Format("{0}Dett Settimana {1}", Ghost.Descrizione, Settimana);
                return NomeTmp;
            }
        }

        private Mansioni fMansione;
        [Persistent("MANSIONE"),
        DisplayName("Mansione")]
        [Appearance("GhostDettaglio.Mansione", Enabled = false)]
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

        private TipoNumeroManutentori fNumMan;
        [Persistent("NUMMAN"), DisplayName("Coppia Linkata")]
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
        #region anno mese settimana
        private int fAnno;
        [Persistent("ANNO"),
        DisplayName("Anno")]
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
        [Persistent("MESE"),
        DisplayName("Mese")]
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
        DisplayName("Settimana")]
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
        #endregion

        private int fIndiceAggregazione;
        [Persistent("INDAGGREGAZIONE"),
        DisplayName("Indice Aggregazione")]
        [Appearance("GhostDettaglio.IndiceAggregazione", Enabled = false)]
        public int IndiceAggregazione
        {
            get
            {
                return fIndiceAggregazione;
            }
            set
            {
                SetPropertyValue<int>("IndiceAggregazione", ref fIndiceAggregazione, value);
            }
        }

        #region tempi di carico
        private int fCarico;
        [Persistent("CARICO"),DisplayName("Carico")]
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
        [Persistent("CARICO_INSITO"), DisplayName("Carico In Sito"), MemberDesignTimeVisibility(false)]
        [Appearance("GhostDettaglioGG.CaricoInSito", Enabled = false)]
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
        [Persistent("CARICO_TRASFERIMENTO"), DisplayName("Carico Trasferimento"), MemberDesignTimeVisibility(false)]
        [Appearance("GhostDettaglioGG.CaricoTrasferimento", Enabled = false)]
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
        [Persistent("SATURAZIONE"),
        DisplayName("Saturo")]
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



        private int fNrGiorniSaturi;
        [Persistent("NR_GIORNISATURI"),
        Size(5),
        DisplayName("Nr. Giorni Saturi")]
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
        #endregion
       
        //private string fItineranteImpianto;
        //[Persistent("ITINERIMPIANTO"),
        //Size(2),
        //DisplayName("Itinerante su Impianti")]
        //[DbType("varchar(2)")]
        //[Appearance("GhostDettaglio.ItineranteImpianto", Enabled = false)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string ItineranteImpianto
        //{
        //    get
        //    {
        //        return fItineranteImpianto;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("ItineranteImpianto", ref fItineranteImpianto, value);
        //    }
        //}

        //private string fItineranteCluster;
        //[Persistent("ITINERCLUSTER"),
        //Size(2),
        //DisplayName("Itinerante su Cluster")]
        //[DbType("varchar(2)")]
        //[Appearance("GhostDettaglio.ItineranteCluster", Enabled = false)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string ItineranteCluster
        //{
        //    get
        //    {
        //        return fItineranteCluster;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("ItineranteCluster", ref fItineranteCluster, value);
        //    }
        //}

        private Ghost fGhost;
        [MemberDesignTimeVisibility(false),
        Association(@"Ghost_Dettaglio", typeof(Ghost))]
        [Persistent("MPGHOST"),        DisplayName("Ghost")]
        [Appearance("GhostDettaglio.Ghost", Enabled = false)]
        public Ghost Ghost
        {
            get
            {
                return fGhost;
            }
            set
            {
                SetPropertyValue<Ghost>("Ghost", ref fGhost, value);
            }
        }

        [Association("GhostDettaglio_GG", typeof(GhostDettaglioGG)),
        DisplayName("Elenco Ghosts Giornalieri")]
        //[Appearance("GhostDettaglio.gg", Enabled = false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<GhostDettaglioGG> GhostDettaglioGGs
        {
            get
            {
                return GetCollection<GhostDettaglioGG>("GhostDettaglioGGs");
            }
        }

        [Association("Ghost_SETT_AttivitaPianiDett", typeof(MpAttivitaPianificateDett)),
       Aggregated]
        public XPCollection<MpAttivitaPianificateDett> MPAttivitaPianiDetts
        {
            get
            {
                return GetCollection<MpAttivitaPianificateDett>("MPAttivitaPianiDetts");
            }
        }


        /// <summary>
        /// 
        /// </summary>
        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        //[  Association(@"TeamRisorse_GhostSet", typeof(GhostDettaglio)) ]
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

        [DevExpress.Xpo.DisplayName("NR Ghost Giornalieri"), VisibleInLookupListView(false)]
        [PersistentAlias("GhostDettaglioGGs.Count")]
        public int nrGhostGG
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("nrGhostGG");
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


        public override string ToString()
        {
            return string.Format("Mansione: {0}, Coppia Linkata: {1}", Mansione, NumMan);
        }
    }
}
