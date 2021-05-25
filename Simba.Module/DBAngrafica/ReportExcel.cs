using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.ExpressApp.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione"), System.ComponentModel.DisplayName("Parametri Report Excel")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Parametri Report Excel")]
    [DefaultClassOptions, Persistent("PARAMETRIREPORTXLS")]
    [ImageName("GroupFieldCollection")]
    [DefaultProperty("Descrizione")]
    [Appearance("ReportExcel.conFiltrodipopUp.LayoutItemVisibility", AppearanceItemType.LayoutItem,
               @"ReportExcelFiltro == 1", TargetItems = "ReportExcel_3", Visibility = ViewItemVisibility.Hide)]
    [VisibleInDashboards(false)]
    public class ReportExcel : XPObject
    {
        public ReportExcel() : base() { }

        public ReportExcel(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Session.IsNewObject(this))
            {
                var a = this.Session.GetObjectByKey<SecuritySystemUser>(DevExpress.ExpressApp.SecuritySystem.CurrentUserId);
                this.SecurityUser = (SecuritySystemUser)a; //a;//  this.Session.;
            }
        }

        private string fDescrizione;
        [Size(150), Persistent("DESCRIZIONE"), DevExpress.Xpo.DisplayName("Descrizione")]
        [RuleRequiredField("RReqField.ReportExcel.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
        [DbType("varchar(150)")]
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

        [Persistent("OBJECTTYPE"), DevExpress.Xpo.DisplayName("Tipo Dati")]
        [RuleRequiredField("RReqField.ReportExcel.ObjectType", DefaultContexts.Save, "Tipo Dati è un campo obbligatorio")]
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type ObjectType
        {
            get { return GetPropertyValue<Type>("ObjectType"); }
            set { SetPropertyValue<Type>("ObjectType", value); }
        }

        //[PersistentAlias("ObjectType")]
        //public string ObjectType_Name
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("ObjectType_Name");
        //        if (tempObject != null)
        //        {
        //            //ObjectType.GUID
        //            return (string)tempObject.ToString();
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}


        [Persistent("OBJECTTYPEPOPUP"), DevExpress.Xpo.DisplayName("Tipo Dati Finestra di PopUp")]
        //[RuleRequiredField("RReqField.ReportExcel.ObjectTypePopUp", DefaultContexts.Save, "Tipo Dati PopUp è un campo obbligatorio")]
        [ValueConverter(typeof(TypeToStringConverter)), ImmediatePostData]
        [TypeConverter(typeof(LocalizedClassInfoTypeConverter))]
        public Type ObjectTypePopUp
        {
            get { return GetPropertyValue<Type>("ObjectTypePopUp"); }
            set { SetPropertyValue<Type>("ObjectTypePopUp", value); }
        }

        //[Persistent("CRITERION"), XafDisplayName("Criteri di Filtro")]
        //[CriteriaOptions("ObjectType"), Size(SizeAttribute.Unlimited)]
        //[EditorAlias(EditorAliases.PopupCriteriaPropertyEditor)]
        //public string Criterion
        //{
        //    get { return GetPropertyValue<string>("Criterion"); }
        //    set { SetPropertyValue<string>("Criterion", value); }
        //}

        private string fCampoJoinLookUp;
        [Size(250), Persistent("CAMPOJOINLOOKUP"), DevExpress.Xpo.DisplayName("Join Campo LookUp")]
        //[RuleRequiredField("RReqField.ReportExcel.ObjectType", DefaultContexts.Save, "Tipo Dati è un campo obbligatorio")]
        //[EditorAlias(CAMSEditorAliases.CustomListPersistentProperties)]
        [DbType("varchar(250)")]
        public string CampoJoinLookUp
        {
            get { return fCampoJoinLookUp; }
            set { SetPropertyValue<string>("CampoJoinLookUp", ref fCampoJoinLookUp, value); }
        }

        private string fCampoObjectType;
        [Size(250), Persistent("CAMPOJOINOBJ"), DevExpress.Xpo.DisplayName("Join Campo")]
        //[RuleRequiredField("RReqField.ReportExcel.ObjectType", DefaultContexts.Save, "Tipo Dati è un campo obbligatorio")]
        //[EditorAlias(CAMSEditorAliases.CustomListPersistentProperties)]
        [DbType("varchar(250)")]
        public string CampoObjectType
        {
            get { return fCampoObjectType; }
            set { SetPropertyValue<string>("CampoObjectType", ref fCampoObjectType, value); }
        }
 

        [Persistent("FILE"), DevExpress.Xpo.DisplayName("File")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData File
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("File");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("File", value);
            }
        }

        private SecuritySystemUser fSecurityUser;
        [Persistent("SECURITYUSERID"), DevExpress.Xpo.DisplayName("Utente")]
        [RuleRequiredField("RReqField.ReportExcel.SecurityUser", DefaultContexts.Save, "Utente è un campo obbligatorio")]
        public SecuritySystemUser SecurityUser
        {
            get { return fSecurityUser; }
            set { SetPropertyValue<SecuritySystemUser>("SecurityUser", ref fSecurityUser, value); }
        }

        //  ReportExcelFiltro
        private ReportExcelFiltro fReportExcelFiltro;
        [Persistent("CRITERIFILTRO"), DevExpress.Xpo.DisplayName("Criteri di Filtro")]
        [RuleRequiredField("RReqField.ReportExcel.ReportExcelFiltro", DefaultContexts.Save, "Criteri di Filtro è un campo obbligatorio")]
        [ImmediatePostData(true)]
        public ReportExcelFiltro ReportExcelFiltro
        {
            get { return fReportExcelFiltro; }
            set { SetPropertyValue<ReportExcelFiltro>("ReportExcelFiltro", ref fReportExcelFiltro, value); }
        }

        [Association(@"ReportExcel-dettagli", typeof(ReportExcelDettagli)), DevExpress.Xpo.Aggregated]
        [DevExpress.Xpo.DisplayName("Parametri Associati")]
        public XPCollection<ReportExcelDettagli> ReportExcelDettaglis
        {
            get
            {
                return GetCollection<ReportExcelDettagli>("ReportExcelDettaglis");
            }
        }



      

    }
}
