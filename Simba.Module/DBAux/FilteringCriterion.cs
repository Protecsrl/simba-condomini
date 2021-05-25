using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBAux
{
    [DefaultClassOptions, Persistent("FILTRIPERSONALIZZATI"), ImageName("Action_Filter")]
    [System.ComponentModel.DefaultProperty("Description")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Filtri Personalizzati")]

    [Appearance("Disabilita.nuovo", AppearanceItemType = "ViewItem", TargetItems = "SecurityUser",
        Context = "FilteringCriterion_DetailView_Nuovo", Enabled = false)]

    [RuleCombinationOfPropertiesIsUnique("Unique_SUser_Descr_ObType", DefaultContexts.Save, "SecurityUser;Description;myObjectType")]
    //[RuleCombinationOfPropertiesIsUnique("Unique_SecurityUser_Description_ObjectType_Criterion", DefaultContexts.Save, "SecurityUser;Description;Criterion")]
    [VisibleInDashboards(false)]
    public class FilteringCriterion : BaseObject
    {
        public FilteringCriterion(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Session.IsNewObject(this))
            {
                this.SecurityUser = (SecuritySystemUser)this.Session.GetObjectByKey<SecuritySystemUser>(SecuritySystem.CurrentUserId);// SecuritySystem.CurrentUser;//  this.Session.;
            }
        }

        [Persistent("DESCRIZIONE"), XafDisplayName("Denominazione filtro")]
        [RuleRequiredField("RReqField.FilteringCriterion.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        public string Description
        {
            get { return GetPropertyValue<string>("Description"); }
            set { SetPropertyValue<string>("Description", value); }
        }

        [Persistent("OBJECTTYPE"), XafDisplayName("Tipo Dati")]
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type myObjectType
        {
            get { return GetPropertyValue<Type>(nameof(myObjectType)); }
            set
            {
                SetPropertyValue<Type>(nameof(myObjectType), value);
                Criterion = String.Empty;
            }
        }

        [Persistent("CRITERION"), XafDisplayName("Criteri di Filtro")]
        [CriteriaOptions("myObjectType"), Size(SizeAttribute.Unlimited)]
        [EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        public string Criterion
        {
            get { return GetPropertyValue<string>("Criterion"); }
            set { SetPropertyValue<string>("Criterion", value); }
        }

        private SecuritySystemUser fSecurityUser;
        [Persistent("SECURITYUSERID"), XafDisplayName("Utente")]
        [RuleRequiredField("RReqField.FilteringCriterion.SecurityUser", DefaultContexts.Save, "Utente è un campo obbligatorio")]
        public SecuritySystemUser SecurityUser
        {
            get { return fSecurityUser; }
            set { SetPropertyValue<SecuritySystemUser>("SecurityUser", ref fSecurityUser, value); }
        }

        [Persistent("DESCRIZIONEFILTRO"), XafDisplayName("Descrizione Filtro")]
        [Size(SizeAttribute.Unlimited)]
        public string DescrizioneFiltro
        {
            get { return GetPropertyValue<string>("DescrizioneFiltro"); }
            set { SetPropertyValue<string>("DescrizioneFiltro", value); }
        }
    }
}


