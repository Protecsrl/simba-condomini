using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,    Persistent("MPREGPOLIMANSIONI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Gruppo Mansioni")]
    [RuleCombinationOfPropertiesIsUnique("UniqMPRegPoliMansioniCarico", DefaultContexts.Save, "Descrizione,RegPianificazioneMP", SkipNullOrEmptyValues = false)]
    [ImageName("Today")]
    [NavigationItem(false)]
    public class MPRegPoliMansioniCarico : XPObject
    {
        public MPRegPoliMansioniCarico() : base() { }

        public MPRegPoliMansioniCarico(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100)]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.MPRegPoliMansioniCarico.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[ImmediatePostData]
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

        private RegPianificazioneMP fRegPianificazioneMP;
        [MemberDesignTimeVisibility(false)]
        [Association(@"RegPianoMP_PoliMansioni", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANO"), DevExpress.Xpo.DisplayName("Registro Pianificazione")]
        [Appearance("MPRegPoliMansioniCarico.RegPianificazioneMP", Enabled = false)]
        [ImmediatePostData]
        public RegPianificazioneMP RegPianificazioneMP
        {
            get
            {
                return fRegPianificazioneMP;
            }
            set
            {
                //SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value);
                if (SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value))
                {
                   // OnChanged("Scenario");
                }
            }
        }

        [Association(@"MPRegPoliMansioniCarico_Dettaglio", typeof(MPRegPoliMansioniCaricoDett)),  DevExpress.Xpo.DisplayName("Impostazioni Mansioni")]
        // [Appearance("RegPianoMP_MPDataIniziale.Enable", @"[MPStatoPianificazione.Oid] != 7", Enabled = false)]
        [Appearance("RegPianoMP_PoliMansioni.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 8", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<MPRegPoliMansioniCaricoDett> MPRegPoliMansioniCaricoDett
        {
            get
            {
                return GetCollection<MPRegPoliMansioniCaricoDett>("MPRegPoliMansioniCaricoDett");
            }
        }

      


        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;

        }
      
    }
}







