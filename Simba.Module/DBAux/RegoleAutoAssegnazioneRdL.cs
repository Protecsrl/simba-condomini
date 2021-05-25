using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
namespace CAMS.Module.DBAux
{
    [DefaultClassOptions]
    [Persistent("REGOLEAUTOASSEGNAZIONE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Regole Auto Assegnazione RdL")]
    //[ImageName("BO_Employee")]
    [NavigationItem(true)]
    #region filtri
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegoleAutoAssegnazioneRdL_TipoRegola_1", "TipoRegola == 1", "Regola Automatismi Assegnazione", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegoleAutoAssegnazioneRdL_TipoRegola_2", "TipoRegola == 2", "Regola Selezione RisorseTeam",  Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegoleAutoAssegnazioneRdL.Tutto", "", "Tutto", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegoleAutoAssegnazioneRdL_TipoRegola_0", "TipoRegola == 0", "Regola non definita", Index = 3)]

    #endregion
    public class RegoleAutoAssegnazioneRdL : XPObject
    {

        public RegoleAutoAssegnazioneRdL()
            : base()
        {
        }
        public RegoleAutoAssegnazioneRdL(Session session)
            : base(session)
        {
        }

        private TipoRegola fTipoRegola;
        [Persistent("TIPOREGOLA"), DisplayName("Tipo Regola")]
        public TipoRegola TipoRegola
        {
            get
            {
                return fTipoRegola;
            }
            set
            {
                SetPropertyValue<TipoRegola>("TipoRegola", ref fTipoRegola, value);
            }
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [RuleRequiredField("RuleReq.RegoleAutoAssegnazioneRdL.Immobile", DefaultContexts.Save, "Immobile è un campo obbligatorio")]
        //[Appearance("RegoleAutoAssegnazioneRdL.Abilita.Immobile", Criteria = "not (Impianto  is null)", Enabled = false)]
        [ImmediatePostData(true)]
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

        private Servizio fImpianto;
        [Persistent("SERVIZIO"), DisplayName("Servizio")]
        [Appearance("RegoleAutoAssegnazioneRdL.Abilita.Servizio", Criteria = "(Immobile  is null) OR (not (RisorseTeam is null))", Enabled = false)]
        [RuleRequiredField("RuleReq.RegoleAutoAssegnazioneRdL.Servizio", DefaultContexts.Save, "Servizio è un campo obbligatorio")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile = '@This.Immobile'")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get
            {
                return fImpianto;
            }
            set
            {
                SetPropertyValue<Servizio>("Impianto", ref fImpianto, value);
            }
        }


        private StatoTLCServizio fStatoTLCImpianto;
        [Persistent("STATOTLC"), DevExpress.Xpo.DisplayName("Stato Impianto TLC")]
        public StatoTLCServizio StatoTLCImpianto
        {
            get
            {
                return fStatoTLCImpianto;
            }
            set
            {
                SetPropertyValue<StatoTLCServizio>("StatoTLCImpianto", ref fStatoTLCImpianto, value);
            }
        }


        [PersistentAlias("Immobile.Commesse"), DevExpress.Xpo.DisplayName("Commesse")]
        [VisibleInListView(true), VisibleInDetailView(false)]
        [ExplicitLoading()]
        public CAMS.Module.DBPlant.Contratti Commesse
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("Commesse");
                if (tempObject != null)
                {
                    return (CAMS.Module.DBPlant.Contratti)tempObject;
                }
                else
                {
                    return null;
                }

            }
        }


        private RisorseTeam fRisorseTeam;  //  @@@@@@@@@@  obligatoria solo in assegnazione
        [Persistent("RISORSATEAM"), System.ComponentModel.DisplayName("Risorsa Team")]
        [RuleRequiredField("RuleReq.RegoleAutoAssegnazioneRdL.RisorseTeam", DefaultContexts.Save, "RisorseTeam è un campo obbligatorio")]
        //[DataSourceProperty("ListaFiltraComboRisorseTeam")]
        [ImmediatePostData(true)]
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

        private TipoAssegnazione FTipoAssegnazione;
        [Persistent("TIPOASSEGNAZIONE"), DisplayName("Tipo Assegnazione")]
        public TipoAssegnazione TipoAssegnazione
        {
            get
            {
                return FTipoAssegnazione;
            }
            set
            {
                SetPropertyValue<TipoAssegnazione>("TipoAssegnazione", ref FTipoAssegnazione, value);
            }
        }

        private Categoria fCategoria;
        [Persistent("CATEGORIA"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [RuleRequiredField("RuleReq.Regoleautomatiche.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        //[DataSourceCriteria("Oid In(2,3,4,5)")]
        [ImmediatePostData(true)]
        //[Appearance("Regoleautomatiche.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        //[Appearance("Regoleautomatiche.Categoria.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        [Delayed(true)]
        //[DataSourceCriteria("Oid in (1,2,3,4,5)")]
        public Categoria Categoria
        {
            get { return GetDelayedPropertyValue<Categoria>("Categoria"); }
            set { SetDelayedPropertyValue<Categoria>("Categoria", value); }

        }

        [Persistent("CALENDARIOCADENZE"), System.ComponentModel.DisplayName("Calendario Cadenze")]
        [RuleRequiredField("RuleReq.Regoleautomatiche.CalendarioCadenze", DefaultContexts.Save, "CalendarioCadenze è un campo obbligatorio")]
        //[DataSourceCriteria("Oid In(2,3,4,5)")]
        [ImmediatePostData(true)]
        //[Appearance("Regoleautomatiche.Abilita.CalendarioCadenze", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        //[Appearance("Regoleautomatiche.Categoria.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        [Delayed(true)]
        //[DataSourceCriteria("Oid in (2,3,4,5)")]
        public CalendarioCadenze CalendarioCadenze
        {
            get { return GetDelayedPropertyValue<CalendarioCadenze>("CalendarioCadenze"); }
            set { SetDelayedPropertyValue<CalendarioCadenze>("CalendarioCadenze", value); }

        }

        private bool fFesteNazionali;
        [Persistent("FESTENAZIONALI"), DisplayName("Feste Nazionali")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public bool FesteNazionali
        {
            get
            {
                return fFesteNazionali;
            }
            set
            {
                SetPropertyValue<bool>("FesteNazionali", ref fFesteNazionali, value);
            }
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM";
        private DateTime? fSantoPadrono;
        [Persistent("DATAUNSERVICE")]
        [DevExpress.Xpo.DisplayName("Data Santo Patrono")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Data Santo Patrono ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
         [VisibleInListView(false), VisibleInLookupListView(false)]
 
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public DateTime? SantoPadrono
        {
            get
            {
                return fSantoPadrono;
            }
            set
            {
                SetPropertyValue<DateTime?>("SantoPadrono", ref fSantoPadrono, value);
            }
        }



        [Association(@"RegoleAutoAssegnazioneRdL_RegoleAutoAssegnazioneRdL", typeof(RegoleAutoAssegnazioneRisorseTeam)), DevExpress.Xpo.Aggregated]
        [XafDisplayName("Assegnazione RisorseTeam")]
        [Appearance("RegoleAutoAssegnazioneRdL.RegoleAutoAssegnazioneRisorseTeam.Count.HideLayoutItem",
                                         AppearanceItemType.LayoutItem, "Oid = -1 And TipoAssegnazione != 3", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RegoleAutoAssegnazioneRisorseTeam> RegoleAutoAssegnazioneRisorseTeams
        {
            get
            {
                return GetCollection<RegoleAutoAssegnazioneRisorseTeam>("RegoleAutoAssegnazioneRisorseTeams");
            }
        }


        [Association(@"RegoleAutoAssegnazioneRdL_RegoleAutoAssegnazioneRdLA", typeof(RegoleAutossegnazioneApparato)), DevExpress.Xpo.Aggregated]
        [XafDisplayName("Assegnazione Apparato")]
        [Appearance("RegoleAutoAssegnazioneRdL.RegoleAutoAssegnazioneApparato.Count.HideLayoutItem",
                                      AppearanceItemType.LayoutItem, "Oid = -1 And TipoAssegnazione != 3", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RegoleAutossegnazioneApparato> RegoleAutoAssegnazioneApparatos
        {
            get
            {
                return GetCollection<RegoleAutossegnazioneApparato>("RegoleAutoAssegnazioneApparatos");
            }
        }




        private AggiungiRisorsaVicina fAggiungiRisorsaVicina;
        [Persistent("AGGIUNGIRISORSAVICINA"), DisplayName("AutoAssegnazione Risorsa Vicina")]
        public AggiungiRisorsaVicina AggiungiRisorsaVicina
        {
            get
            {
                return fAggiungiRisorsaVicina;
            }
            set
            {
                SetPropertyValue<AggiungiRisorsaVicina>("AggiungiRisorsaVicina", ref fAggiungiRisorsaVicina, value);
            }
        }

        // regole di riassegnazione a tempo

        private RiassegnazioneRisorsa fRiassegnazioneRisorsa;
        [Persistent("RIASSEGNAZIONERISORSA"), DisplayName("Automatismo Riassegnazione Risorsa")]
        public RiassegnazioneRisorsa RiassegnazioneRisorsa
        {
            get
            {
                return fRiassegnazioneRisorsa;
            }
            set
            {
                SetPropertyValue<RiassegnazioneRisorsa>("RiassegnazioneRisorsa", ref fRiassegnazioneRisorsa, value);
            }
        }

        private string fTempoLivelloAutorizzazioneGuasto;
        [Persistent("TEMPOLIVELLOAUTORIZZOGUASTO"), DisplayName("Tempo Livello Autorizzazione Guasto RdL")]
        [Appearance("RegoleAuto.Hide.tempoLivelliScalata", Criteria = "(RiassegnazioneRisorsa == 2)", Enabled = false)]
        [ToolTip("separare con punto e virgola i valori di livelli previsti")]
        [DbType("varchar(50)"), Size(50)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public string TempoLivelloAutorizzazioneGuasto
        {
            get
            {
                return fTempoLivelloAutorizzazioneGuasto;
            }
            set
            {
                SetPropertyValue<string>("TempoLivelloAutorizzazioneGuasto", ref fTempoLivelloAutorizzazioneGuasto, value);
            }
        }


        public override string ToString()
        {
            return string.Format("{0}", 1);
        }
    }
}

