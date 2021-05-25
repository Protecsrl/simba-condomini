
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;
using System.Drawing;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions]
    [NavigationItem("Amministrazione")]
    [System.ComponentModel.DefaultProperty("Set Report Commessa")]
    [VisibleInDashboards(false)]
    public class SetReportCommessa : BaseObject
    {
        public SetReportCommessa(Session session)
            : base(session)
        {
        }

        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"), XafDisplayName("Descrizione")]
        [RuleRequiredField("RReqField.SetReportCommessa.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        [DbType("varchar(100)")]
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

        private Contratti fCommesse;
        [Persistent("CONTRATTO"), XafDisplayName("Contratto")]
        [RuleRequiredField("RuleReq.SetReportCommessa.Commesse", DefaultContexts.Save, "Commesse è un campo obbligatorio")]
        [Appearance("SetReportCommessa.Commesse.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Commesse)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Contratti Commesse
        {
            get
            {
                return fCommesse;
            }
            set
            {
                SetPropertyValue<Contratti>("Commesse", ref fCommesse, value);
            }
        }


        private Categoria fCategoria;
        [Persistent("CATEGORIA"), XafDisplayName("Categoria Manutenzione")]
        [RuleRequiredField("RuleReq.SetReportCommessa.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        //[DataSourceCriteria("Oid In(2,3,4,5)"), 
        [ImmediatePostData(true)]
        //[Appearance("SetReportCommessa.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Appearance("SetReportCommessa.Categoria.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        public Categoria Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<Categoria>("Categoria", ref fCategoria, value);
            }
        }

        [Persistent("OBJECTTYPE"), XafDisplayName("Tipo Dati")]
        [RuleRequiredField("RReqField.SetReportCommessa.ObjectType", DefaultContexts.Save, "Tipo Dati è un campo obbligatorio")]
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type ObjectType
        {
            get { return GetPropertyValue<Type>("ObjectType"); }
            set
            {
                SetPropertyValue<Type>("ObjectType", value);
            }
        }

        private string fDisplayReport;
        [Size(150), Persistent("DISPLAYREPORT"), XafDisplayName("Report")]
        [RuleRequiredField("RReqField.SetReportCommessa.DisplayReport", DefaultContexts.Save, "Display Report è un campo obbligatorio")]
        [DbType("varchar(150)")]
        public string DisplayReport
        {
            get
            {
                return fDisplayReport;
    
            }
            set
            {
                SetPropertyValue<string>("DisplayReport", ref fDisplayReport, value);
            }
        }

    }
}




//namespace FeatureCenter.Module {
//    public abstract class NamedBaseObject : BaseObject {
//        private string name;
//        public NamedBaseObject(Session session) : base(session) { }
//        public NamedBaseObject(Session session, string name)
//            : this(session) {
//            this.name = name;
//        }
//        public string Name {
//            get { return name; }
//            set { SetPropertyValue("Name", ref name, value); }
//        }
//    }
//}



