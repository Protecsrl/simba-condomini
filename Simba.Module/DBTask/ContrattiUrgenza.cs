using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("CONTRATTIURGENZA")]
    [VisibleInDashboards(false)]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Contratti Urgenza")]
    [ImageName("Time")]
    [NavigationItem("Tabelle Anagrafiche")]
    public class ContrattiUrgenza : XPObject
    {

        public ContrattiUrgenza()
        : base()
        {
        }
        public ContrattiUrgenza(Session session)
        : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private Contratti fContratti;
        [Persistent("CONTRATTI"), DisplayName("Contratti")]
        [Association("Contratti_ContrattiUrgenza")]
        [RuleRequiredField("RReq.ContrattiUrgenza.Contratti", DefaultContexts.Save, "è un campo obligatorio")]
        [ToolTip("Identificazione del Contratto")]
        [ExplicitLoading]
        [Delayed(true)]
        public Contratti Contratti
        {
            get { return GetDelayedPropertyValue<Contratti>("Contratti"); }
            set { SetDelayedPropertyValue<Contratti>("Contratti", value); }

        }

        private Urgenza fUrgenza;
        [Persistent("URGENZA"), System.ComponentModel.DisplayName("Urgenza")]
        //   [Association("Urgenza_ContrattiUrgenza")]
        [RuleRequiredField("RuleReq.ContrattiUrgenza.Urgenza", DefaultContexts.Save, "Urgenza è un campo obbligatorio")]
        //[Appearance("RdL.Urgenza.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Urgenza)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[Appearance("RdL.Abilita.Urgenza", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Urgenza Urgenza
        {
            get { return GetDelayedPropertyValue<Urgenza>("Urgenza"); }
            set { SetDelayedPropertyValue<Urgenza>("Urgenza", value); }
        }

        //[Persistent("DEFAULTVALUE"), System.ComponentModel.DisplayName("Valore di Default")]
        //[Delayed(true)]
        //FlgAbilitato Default
        //{
        //    get { return GetDelayedPropertyValue<FlgAbilitato>("Default"); }
        //    set { SetDelayedPropertyValue<FlgAbilitato>("Default", value); }
        //}
        private FlgAbilitato fDefault;
        [Persistent("DEFAULTVALUE"), DisplayName("Valore di Default")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public FlgAbilitato Default
        {
            get
            {
                return fDefault;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Default", ref fDefault, value);
            }
        }

        private string fGruppo;
        [Persistent("GRUPPO"), DisplayName("Gruppo")]
        [DbType("varchar(20)")]
        // [Delayed(true)]
        //[VisibleInListViewAttribute(false), VisibleInDetailView(true)]
        public string Gruppo
        {
            get { return fGruppo; }
            set { SetPropertyValue<string>("Gruppo", ref fGruppo, value); }

        }



    }
}
