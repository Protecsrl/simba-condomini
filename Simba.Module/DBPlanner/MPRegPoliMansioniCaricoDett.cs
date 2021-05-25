using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,  Persistent("MPREGPOLIMANSIONIDETT")]
    [System.ComponentModel.DefaultProperty("ScenarioMansioneCarico")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gruppo Mansioni Dettaglio")]
    [RuleCombinationOfPropertiesIsUnique("UniqMPRegPoliMansioniCaricoDett", DefaultContexts.Save, "MPRegPoliMansioniCarico,Mansioni", SkipNullOrEmptyValues = false)]
    [ImageName("Today")]
    [NavigationItem(false)]
    public class MPRegPoliMansioniCaricoDett : XPObject
    {
        public MPRegPoliMansioniCaricoDett() : base() { }

        public MPRegPoliMansioniCaricoDett(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private MPRegPoliMansioniCarico fMPRegPoliMansioniCarico;
        [MemberDesignTimeVisibility(false)]
        [Association(@"MPRegPoliMansioniCarico_Dettaglio", typeof(MPRegPoliMansioniCarico))]
        [Persistent("MPREGPOLIMANSIONI"), DevExpress.Xpo.DisplayName("Registro Mansioni Carico")]
        [Appearance("MPRegPoliMansioniCaricoDett.MPRegPoliMansioniCarico", Enabled = false)]
        [ImmediatePostData]
        public MPRegPoliMansioniCarico MPRegPoliMansioniCarico
        {
            get
            {
                return fMPRegPoliMansioniCarico;
            }
            set
            {

                SetPropertyValue<MPRegPoliMansioniCarico>("MPRegPoliMansioniCarico", ref fMPRegPoliMansioniCarico, value);
              
            }
        }
        //ScenarioMansioneCarico

        private Mansioni fMansioni;
        [Persistent("MANSIONI"), DevExpress.Xpo.DisplayName("Mansioni")]
        //[Appearance("ScenarioMansioneCarico.", Enabled = false)]
        public Mansioni Mansioni
        {
            get
            {
                return fMansioni;
            }
            set
            {
                SetPropertyValue<Mansioni>("Mansioni", ref fMansioni, value);
            }
        }

      //  private ScenarioMansioneCarico fScenarioMansioneCarico;
      ////  [Association(@"RegPianoMP_PoliMansioni", typeof(ScenarioMansioneCarico))]
      //  [Persistent("SCENARIO_MANSIONE"), DevExpress.Xpo.DisplayName("Mansione Carico")]
      ////  [Appearance("MPDataFissa.RegPianificazioneMP", Enabled = false)]
      //  [DataSourceCriteria("Scenario = '@This.MPRegPoliMansioniCarico.RegPianificazioneMP.Scenario'")]
      //  [ImmediatePostData]
      //  public ScenarioMansioneCarico ScenarioMansioneCarico
      //  {
      //      get
      //      {
                 
      //          return fScenarioMansioneCarico;
      //      }
      //      set
      //      {
      //          if (SetPropertyValue<ScenarioMansioneCarico>("ScenarioMansioneCarico", ref fScenarioMansioneCarico, value))
      //          {
      //             // OnChanged("Scenario");
      //          }
      //      }
      //  }
       

       
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;

        }    

    }
}







