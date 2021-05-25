using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions, Persistent("FREQUENZASTAGIONALE"), System.ComponentModel.DefaultProperty("Name")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Frequenza Stagionale")]
    [RuleCombinationOfPropertiesIsUnique("UniqueFrequenzaStagionale", DefaultContexts.Save, "Frequenza;Mese")]
    [NavigationItem(false)]
    [ImageName("Frequenze")]
    public class FrequenzaStagionale : XPObject
    {
        public FrequenzaStagionale()
            : base()
        {
        }

        public FrequenzaStagionale(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
           // this.Mese = 1;
        }


        private Frequenze fFrequenza;
        [Persistent("FREQUENZA"), Association(@"FrequenzaStagionale_Frequenza")]
        [ExplicitLoading()]
        public Frequenze Frequenza
        {
            get
            {
                return fFrequenza;
            }
            set
            {
                SetPropertyValue<Frequenze>("Frequenza", ref fFrequenza, value);
            }
        }

        
        private int fMese;
        [Persistent("MESE"), XafDisplayName("Mese")]
        [RuleRequiredField("RReqField.FrequenzaStagionale.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [EditorAlias(CAMSEditorAliases.CustomRangeMese)]
        [RuleRange("FrequenzaStagionale.Mese", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        [ImmediatePostData(true)]
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

        private int fCadenza;
        [Persistent("CADENZA"), XafDisplayName("Cadenza")]
        [RuleRequiredField("RReqField.FrequenzaStagionale.Cadenza", DefaultContexts.Save, @"La Cadenza è un campo obbligatorio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [EditorAlias(CAMSEditorAliases.CustomRangeMese)]
       // [RuleRange("FrequenzaStagionale.Cadenza", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        [ImmediatePostData(true)]
        public int Cadenza
        {
            get
            {
                return fCadenza;
            }
            set
            {
                SetPropertyValue<int>("Cadenza", ref fCadenza, value);
            }
        }

        [VisibleInDetailView(false),VisibleInListView(false),VisibleInLookupListView(false)]
        [PersistentAlias("Frequenza + '(' + Mese + ')'")]  
        public string Name
        {
            get
            {
                if (this.Oid == -1) return null;
                return string.Format("{0}({1})", this.Frequenza, this.Mese);
            }
        }

        public override string ToString()
        {
            return Name;
        }

    }
}




 