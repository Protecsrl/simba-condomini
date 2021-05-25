
using CAMS.Module.Classi;
using CAMS.Module.DBMisure;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Drawing;

namespace CAMS.Module.DBSpazi
{
    [DefaultClassOptions, Persistent("LOCALIFINITURE")]
    [RuleCombinationOfPropertiesIsUnique("Unique.LocaliFiniture", DefaultContexts.Save, "Locale,TipoFinituraLocali")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Finiture Locali")]
    [Appearance("LocaliFiniture.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
                 FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Gestione Spazi")]
    public class LocaliFiniture : XPObject
    {
        public LocaliFiniture() : base() { }
        public LocaliFiniture(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            Abilitato = FlgAbilitato.Si;

            this.AbilitazioneEreditata = FlgAbilitato.Si;
        }

        private Locali fLocale;//Aggregated,
        [Persistent("LOCALI"),Association(@"Locali_LocaliFiniture"),   DisplayName("Locale")]
        [ExplicitLoading()]
        public Locali Locale
        {
            get
            {
                return fLocale;
            }
            set
            {
                SetPropertyValue<Locali>("Locale", ref fLocale, value);
            }
        }

        private TipoFinituraLocali fTipoFinituraLocali;
        [Persistent("TIPOFINITURA"), DisplayName("Tipo Finitura")]
        [ImmediatePostData]
        public TipoFinituraLocali TipoFinituraLocali
        {
            get
            {
                return fTipoFinituraLocali;
            }
            set
            {
                SetPropertyValue<TipoFinituraLocali>("TipoFinituraLocali", ref fTipoFinituraLocali, value);

            }
        }

        private UnitaMisura fUnitaMisura;
        [Persistent("UNITAMISURA"), DisplayName("Unità di misura")]//Association(@"Master_UnitaMisura"), 
        [ExplicitLoading()]
        public UnitaMisura UnitaMisura
        {
            get
            {
                return fUnitaMisura;
            }
            set
            {
                SetPropertyValue<UnitaMisura>("UnitaMisura", ref fUnitaMisura, value);
            }
        }

        private double fValore;
        [Persistent("VALORE")]
        //[RuleRange("RuleRange.kCondizione", "Save", 1, 2)]
        public double Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<double>("Valore", ref fValore, value);
            }
        }



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


    }
}

 