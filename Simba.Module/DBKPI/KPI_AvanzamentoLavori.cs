using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;
//using DevExpress.ExpressApp.SystemModule;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBKPI
{
    [DefaultClassOptions, Persistent("KPI_AVANZAMENTOLAVORI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI di Avanzamento Lavori")]
    [NavigationItem("KPI")]
    [ImageName("StackedLine")]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "All Data", Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Ultimi3mesi", "[DataCreazione] >= AddMonths(LocalDateTimeThisMonth(), -3) And [DataCreazione] <= LocalDateTimeThisMonth()", "Ultimi tre Mesi", true, Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Storico", "[DataCreazione] <= AddMonths(LocalDateTimeThisMonth(), -3)", "Storico", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPI_AvanzamentoLavori.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPI_AvanzamentoLavori.conduzione", "OidCategoria = 2", "Conduzione", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPI_AvanzamentoLavori.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPI_AvanzamentoLavori.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPI_AvanzamentoLavori.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 7)]
         

    public class KPI_AvanzamentoLavori : XPObject
    {

        public KPI_AvanzamentoLavori() : base() { }

        public KPI_AvanzamentoLavori(Session session) : base(session) { }


        private string fCodRegRdL;
        [Persistent("CODREGRDL"), DisplayName("Cod RegRdL")]
        [DbType("varchar(10)"), Size(10)]
        [Appearance("KPI_AvanzamentoLavori.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[idCompleto] == 'ok'", BackColor = "LightGreen")] 
        public string CodRegRdL
        {
            get
            {
                
                return fCodRegRdL;
            }
            set
            {
                SetPropertyValue<string>("CodRegRdL", ref fCodRegRdL, value);
            }
        }

        private string fRegistroRdL;
        [Persistent("REGRDL"), DisplayName("RegRdL")]
        [DbType("varchar(4000)"), Size(4000)]
        public string RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<string>("RegistroRdL", ref fRegistroRdL, value);
            }
        }

        private string fCategoria;
        [Persistent("CATEGORIA"), System.ComponentModel.DisplayName("Categoria")] // pm
        [DbType("varchar(150)"), Size(150)]
        public string Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<string>("Categoria", ref fCategoria, value);
            }
        }


        private string fOrdine;
        [Persistent("ORDINE"), DisplayName("Ordinamento")]
        [DbType("varchar(150)"), Size(150)]
        public string Ordine
        {
            get
            {
                return fOrdine;
            }
            set
            {
                SetPropertyValue<string>("Ordine", ref fOrdine, value);
            }
        }



        private string fRisorseTeam;
        [Persistent("RISORSATEAM"), DisplayName("Team")]
        [DbType("varchar(400)"), Size(400)]
        public string RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<string>("RisorseTeam", ref fRisorseTeam, value);
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

        private string fDescrizioneCambioSmistamento;
        [Persistent("DESCR_CG_SMISTAMENTO"), System.ComponentModel.DisplayName("Descrizione cambio Smistamento")]
        [DbType("varchar(1000)"), Size(1000)]
        public string DescrizioneCambioSmistamento
        {
            get { return fDescrizioneCambioSmistamento; }
            set { SetPropertyValue<string>("DescrizioneCambioSmistamento", ref fDescrizioneCambioSmistamento, value); }
        }

        private string fDescrizioneCambioOperativo;
        [Persistent("DESCR_CG_OPERATIVO"), System.ComponentModel.DisplayName("Descrizione cambio Operativo")]
        [DbType("varchar(2000)"), Size(2000)]
        public string DescrizioneCambioOperativo
        {
            get { return fDescrizioneCambioOperativo; }
            set { SetPropertyValue<string>("DescrizioneCambioOperativo", ref fDescrizioneCambioOperativo, value); }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        #region data e tempo smistamento
        private DateTime fDataOraSmistamento;
        [Persistent("DATAORA_CGSMISTAMENTO"), DisplayName("Data Cambio Stato Smistamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOraSmistamento
        {
            get
            {
                return fDataOraSmistamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOraSmistamento", ref fDataOraSmistamento, value);
            }
        }
        private int fDeltaTempoSmistamento;
        [Persistent("DELTATEMPO_CGSMISTAMENTO"), DisplayName("Tempo Sm.(min)")]
        [ModelDefault("DisplayFormat", "{0:D}")]
        [DbType("number")]
        public int DeltaTempoSmistamento
        {
            get
            {
                return fDeltaTempoSmistamento;
            }
            set
            {
                SetPropertyValue<int>("DeltaTempoSmistamento", ref fDeltaTempoSmistamento, value);
            }
        }
        #endregion
        private DateTime fDataOraOperativo;
        [Persistent("DATAORA_CGOPERATIVO"), DisplayName("Data Cambio Stato Operativo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataOraOperativo
        {
            get
            {
                return fDataOraOperativo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOraOperativo", ref fDataOraOperativo, value);
            }
        }

        private int fDeltaTempoOperativo;
        [Persistent("DELTATEMPO_CGOPERATIVO"), DisplayName("Tempo Op. (min)")]
        [ModelDefault("DisplayFormat", "{0:D}")]
        [DbType("number")]
        public int DeltaTempoOperativo
        {
            get
            {
                return fDeltaTempoOperativo;
            }
            set
            {
                SetPropertyValue<int>("DeltaTempoOperativo", ref fDeltaTempoOperativo, value);
            }
        }

        private string fSettimana;
        [Persistent("SETTIMANA"), DisplayName("Settimana")]
        [DbType("varchar(2)"), Size(2)]
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
        [DbType("varchar(4)"), Size(4)]
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
        [DbType("varchar(2)"), Size(2)]
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


        private string fCentroOperativo;
        [Persistent("CENTROOPERATIVO"), DisplayName("Centro Operativo")]
        [DbType("varchar(150)"), Size(150)]
        public string CentroOperativo
        {
            get
            {
                return fCentroOperativo;
            }
            set
            {
                SetPropertyValue<string>("CentroOperativo", ref fCentroOperativo, value);
            }
        }

        private string fAreaDiPolo;
        [Persistent("AREADIPOLO"), DisplayName("Area Di Polo")]
        [DbType("varchar(150)"), Size(150)]
        public string AreaDiPolo
        {
            get
            {
                return fAreaDiPolo;
            }
            set
            {
                SetPropertyValue<string>("AreaDiPolo", ref fAreaDiPolo, value);
            }

        }
        private string fPolo;
        [Persistent("POLO"), DisplayName("Polo")]
        [DbType("varchar(150)"), Size(150)]
        public string Polo
        {
            get
            {
                return fPolo;
            }
            set
            {
                SetPropertyValue<string>("Polo", ref fPolo, value);
            }

        }

        private string fCommessa;
        [Persistent("COMMESSA"), DisplayName("Commessa")]
        [DbType("varchar(150)"), Size(150)]
        public string Commessa
        {
            get
            {
                return fCommessa;
            }
            set
            {
                SetPropertyValue<string>("Commessa", ref fCommessa, value);
            }
        }

        private string fEdificio;
        [Persistent("EDIFICIO"), DisplayName("Edificio")]
        [DbType("varchar(150)"), Size(150)]
        public string Edificio
        {
            get
            {
                return fEdificio;
            }
            set
            {
                SetPropertyValue<string>("Edificio", ref fEdificio, value);
            }
        }

        private string fCCosto;
        [Persistent("CCOSTO"), DisplayName("Centro di Costo")]
        [DbType("varchar(150)"), Size(150)]
        public string CCosto
        {
            get
            {
                return fCCosto;
            }
            set
            {
                SetPropertyValue<string>("CCosto", ref fCCosto, value);
            }

        }

        private string fNote;
        [Persistent("NOTE"), DisplayName("Note")]
        [DbType("varchar(1000)"), Size(1000)]
        public string Note
        {
            get
            {
                return fNote;
            }
            set
            {
                SetPropertyValue<string>("Note", ref fNote, value);
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

        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(50)")]
        public string Priorita
        {
            get
            {
                return fPriorita;
            }
            set
            {
                SetPropertyValue<string>("Priorita", ref fPriorita, value);
            }
        }

        private string f_Utente;
        [Persistent("UTENTE"),   Size(1000),    DisplayName("Utente Smistamento")]
        [DbType("varchar(1000)")]
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

        //   da qui opzionali

        private string fClasse;
        [Persistent("CLASSE"), System.ComponentModel.DisplayName("Classe")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string Classe
        {
            get
            {
                return fClasse;
            }
            set
            {
                SetPropertyValue<string>("Classe", ref fClasse, value);
            }
        }

        private int fClasseValore;
        [Persistent("CLASSEVALORE"), System.ComponentModel.DisplayName("Classe Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int ClasseValore
        {
            get
            {
                return fClasseValore;
            }
            set
            {
                SetPropertyValue<int>("ClasseValore", ref fClasseValore, value);
            }
        }



        private string fSerie;
        [Persistent("SERIE"), System.ComponentModel.DisplayName("Serie")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string Serie
        {
            get
            {
                return fSerie;
            }
            set
            {
                SetPropertyValue<string>("Serie", ref fSerie, value);
            }
        }

        private int fSerieValore;
        [Persistent("SERIEVALORE"), System.ComponentModel.DisplayName("Serie Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int SerieValore
        {
            get
            {
                return fSerieValore;
            }
            set
            {
                SetPropertyValue<int>("SerieValore", ref fSerieValore, value);
            }
        }

        //-------------



        private string fEvasioneIntervento;
        [Persistent("TEVASIONE_INT"), System.ComponentModel.DisplayName("Evasione Intervento")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string EvasioneIntervento
        {
            get
            {
                return fEvasioneIntervento;
            }
            set
            {
                SetPropertyValue<string>("EvasioneIntervento", ref fEvasioneIntervento, value);
            }
        }

        private int fEvasioneInterventoVal;
        [Persistent("TEVASIONE_INT_VAL"), System.ComponentModel.DisplayName("Evasione Intervento Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int EvasioneInterventoVal
        {
            get
            {
                return fEvasioneInterventoVal;
            }
            set
            {
                SetPropertyValue<int>("EvasioneInterventoVal", ref fEvasioneInterventoVal, value);
            }
        }

        //-------------
        private string fAssegnazioneIntervento;
        [Persistent("TASSEGNAZIONE_INT"), System.ComponentModel.DisplayName("Assegnazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string AssegnazioneIntervento
        {
            get
            {
                return fAssegnazioneIntervento;
            }
            set
            {
                SetPropertyValue<string>("AssegnazioneIntervento", ref fAssegnazioneIntervento, value);
            }
        }

        private int fAssegnazioneValoreVal;
        [Persistent("TASSEGNAZIONE_INT_VAL"), System.ComponentModel.DisplayName("Assegnazione Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int AssegnazioneValoreVal
        {
            get
            {
                return fAssegnazioneValoreVal;
            }
            set
            {
                SetPropertyValue<int>("AssegnazioneValoreVal", ref fAssegnazioneValoreVal, value);
            }
        }


        private string fOperativitaIntervento;
        [Persistent("TOPERATIVO_INT"), System.ComponentModel.DisplayName("Operatività")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string OperativitaIntervento
        {
            get
            {
                return fOperativitaIntervento;
            }
            set
            {
                SetPropertyValue<string>("OperativitaIntervento", ref fOperativitaIntervento, value);
            }
        }

        private int fOperativitaInterventoVal;
        [Persistent("TOPERATIVO_INT_VAL"), System.ComponentModel.DisplayName("Operativita Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int OperativitaInterventoVal
        {
            get
            {
                return fOperativitaInterventoVal;
            }
            set
            {
                SetPropertyValue<int>("OperativitaInterventoVal", ref fOperativitaInterventoVal, value);
            }
        }

        private string fTrasferimentoIntervento;
        [Persistent("TOPERATIVOTRASF_INT"), System.ComponentModel.DisplayName("Op. Trasferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string TrasferimentoIntervento
        {
            get
            {
                return fTrasferimentoIntervento;
            }
            set
            {
                SetPropertyValue<string>("TrasferimentoIntervento", ref fTrasferimentoIntervento, value);
            }
        }

        private int fTrasferimentoInterventoVal;
        [Persistent("TOPERATIVOTRASF_INT_VAL"), System.ComponentModel.DisplayName("Trasferimento Valore")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TrasferimentoInterventoVal
        {
            get
            {
                return fTrasferimentoInterventoVal;
            }
            set
            {
                SetPropertyValue<int>("TrasferimentoInterventoVal", ref fTrasferimentoInterventoVal, value);
            }
        }

        private string fPresaCaricoIntervento;
        [Persistent("TTOTPRESAINCARICO_INT"), System.ComponentModel.DisplayName("Presa in Carico")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(100)"), Size(100)]
        public string PresaCaricoIntervento
        {
            get
            {
                return fPresaCaricoIntervento;
            }
            set
            {
                SetPropertyValue<string>("PresaCaricoIntervento", ref fPresaCaricoIntervento, value);
            }
        }

 

        private int fPresaCaricoInterventoVal;
        [Persistent("TTOTPRESAINCARICO_INT_VAL"), System.ComponentModel.DisplayName("Presa in Carico Val")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int PresaCaricoInterventoVal
        {
            get
            {
                return fPresaCaricoInterventoVal;
            }
            set
            {
                SetPropertyValue<int>("PresaCaricoInterventoVal", ref fPresaCaricoInterventoVal, value);
            }
        }


        private int fTotImpianti;
        [Persistent("TOTIMPIANTI"), System.ComponentModel.DisplayName("Tot Impianti")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TotImpianti
        {
            get
            {
                return fTotImpianti;
            }
            set
            {
                SetPropertyValue<int>("TotImpianti", ref fTotImpianti, value);
            }
        }


        private int fTotEdifici;
        [Persistent("TOTEDIFICI"), System.ComponentModel.DisplayName("Tot Edifici")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TotEdifici
        {
            get
            {
                return fTotEdifici;
            }
            set
            {
                SetPropertyValue<int>("TotEdifici", ref fTotEdifici, value);
            }
        }

        private int fTotInterventi;
        [Persistent("TOTINTERVENTI"), System.ComponentModel.DisplayName("Tot Interventi")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int TotInterventi
        {
            get
            {
                return fTotInterventi;
            }
            set
            {
                SetPropertyValue<int>("TotInterventi", ref fTotInterventi, value);
            }
        }





//-----------




        #region OIDEDIFICIO, OIDREFERENTECOFELY
        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [MemberDesignTimeVisibility(false)]
        public int OidEdificio
        {
            get
            {
                return fOidEdificio;
            }
            set
            {
                SetPropertyValue<int>("OidEdificio", ref fOidEdificio, value);
            }
        }

        private int fOidReferenteCofely;
        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidReferenteCofely
        {
            get
            {
                return fOidReferenteCofely;
            }
            set
            {
                SetPropertyValue<int>("OidReferenteCofely", ref fOidReferenteCofely, value);
            }
        }
        private int fOidCategoria;
        [Persistent("OIDCATEGORIA"), System.ComponentModel.DisplayName("OidCategoria")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidCategoria
        {
            get
            {
                return fOidCategoria;
            }
            set
            {
                SetPropertyValue<int>("OidCategoria", ref fOidCategoria, value);
            }
        }

        private int fOidTeamRisorsa;
        [Persistent("OIDTEAMRISORSA"), System.ComponentModel.DisplayName("Risorsa")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidTeamRisorsa
        {
            get
            {
                return fOidTeamRisorsa;
            }
            set
            {
                SetPropertyValue<int>("OidTeamRisorsa", ref fOidTeamRisorsa, value);
            }
        }

        private int fOidSmistamento;
        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidSmistamento
        {
            get
            {
                return fOidSmistamento;
            }
            set
            {
                SetPropertyValue<int>("OidSmistamento", ref fOidSmistamento, value);
            }
        }

        private int fOidOperativo;
        [Persistent("OIDOPERATIVO"), System.ComponentModel.DisplayName("OidOPERATIVO")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidOperativo
        {
            get
            {
                return fOidOperativo;
            }
            set
            {
                SetPropertyValue<int>("OidOperativo", ref fOidOperativo, value);
            }
        }
        private string fidCompleto;
        [Persistent("IDCOMPLETO"), System.ComponentModel.DisplayName("Completo")]
        [DbType("varchar(10)"), Size(10)]
       // [MemberDesignTimeVisibility(false)]
        public string idCompleto
        {
            get
            {
                return fidCompleto;
            }
            set
            {
                SetPropertyValue<string>("idCompleto", ref fidCompleto, value);
            }
        }

        #endregion


    }
}









