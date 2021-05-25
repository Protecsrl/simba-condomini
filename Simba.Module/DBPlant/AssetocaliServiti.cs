using CAMS.Module.DBSpazi;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System.Drawing;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("ASSETLOCALISERVITI")]
    [System.ComponentModel.DefaultProperty("Locale")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Luoghi serviti da Asset")]
    [ImageName("Action_EditModel")]
    [NavigationItem(false)]
    [RuleCombinationOfPropertiesIsUnique("ApparatoLocaliServiti_Unico", DefaultContexts.Save, @"Apparato;Locale",
      CustomMessageTemplate = "Attenzione:Locale già esistente per questo Apparato",
      SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Error)]
    //[Appearance("Apparato.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
    //            FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]
    public class AssetocaliServiti : XPObject
    {
        public AssetocaliServiti() : base() { }
        public AssetocaliServiti(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.Descrizione = "Locale Asservito";
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(150), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(150)")]
        [RuleRequiredField("RReqField.ApparatoLocaliServiti.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [Index(1)]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }

        private Asset fAsset;  
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset")]
        [RuleRequiredField("RuleReq.ApparatoLocaliServiti.Asset", DefaultContexts.Save, "Asset è un campo obbligatorio")]
        [Association(@"Asset_AssetLocaliServiti", typeof(Asset))]
        [Index(2)]
        [ExplicitLoading()]
        public Asset Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                SetPropertyValue<Asset>("Asset", ref fAsset, value);
            }
        }

        private Locali fLocale;
        [Association(@"ApparatoLocaliServiti_Locale"), Persistent("LOCALE"), DevExpress.Xpo.DisplayName("Locale")]
        //[Appearance("Apparato.Locali", Enabled = false, Criteria = "!IsNullOrEmpty(Locale)", Context = "DetailView")]                    
        [DataSourceCriteria("Iif(!IsNullOrEmpty('@This.Asset')," +
                             "Iif(!IsNullOrEmpty('@This.Asset.Impianto')," +
                               "Iif(!IsNullOrEmpty('@This.Asset.Servizio.Immobile'),Piano.Immobile = '@This.Apparato.Servizio.Immobile',null)" +
                             ",null)" +
                            ",null)"
                            )]
        [Index(3)]
        [ExplicitLoading()]
        [ImmediatePostData(true)]
        public Locali Locale
        {
            //This.Impianto.Immobile
            get { return fLocale; }
            set { SetPropertyValue<Locali>("Locale", ref fLocale, value); }
        }
    }
}



