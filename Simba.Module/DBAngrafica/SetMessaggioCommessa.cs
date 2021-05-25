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
    [System.ComponentModel.DefaultProperty("Set Messaggio Commessa")]
    [VisibleInDashboards(false)]
    public class SetMessaggioCommessa : BaseObject
    {
        public SetMessaggioCommessa(Session session)
            : base(session)
        {
        }
     private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"), XafDisplayName("Descrizione")]
        [RuleRequiredField("RReqField.SetMessaggioCommessa.Descrizione", DefaultContexts.Save, "Descrizione è un campo obbligatorio")]
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
        [RuleRequiredField("RuleReq.SetMessaggioCommessa.Commesse", DefaultContexts.Save, "Commesse è un campo obbligatorio")]
        [Appearance("SetMessaggioCommessa.Commesse.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Commesse)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
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
        [RuleRequiredField("RuleReq.SetMessaggioCommessa.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        //[DataSourceCriteria("Oid In(2,3,4,5)"), 
        [ImmediatePostData(true)]
        //[Appearance("SetReportCommessa.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Appearance("SetMessaggioCommessa.Categoria.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
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

        private Servizio fServizio;
        [Persistent("SERVIZIO"), XafDisplayName("Servizio")]
        [RuleRequiredField("RuleReq.SetMessaggioCommessa.Servizio", DefaultContexts.Save, "Servizio è un campo obbligatorio")]
        //[DataSourceCriteria("Oid In(2,3,4,5)"), 
        [ImmediatePostData(true)]
        //[Appearance("SetReportCommessa.Abilita.Categoria", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        [Appearance("SetMessaggioCommessa.Impianto.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Categoria)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                //return Impianto;
                return GetDelayedPropertyValue<Servizio>("Impianto");
            }
            set
            { 
                  SetDelayedPropertyValue<Servizio>("Impianto", value);
                //SetPropertyValue<Impianto>("Impianto", ref Impianto, value);
            }
        }

          

       [Persistent("OBJECTTYPE"), XafDisplayName("Tipo Dati")]
       [RuleRequiredField("RReqField.SetMessaggioCommessa.ObjectType", DefaultContexts.Save, "Tipo Dati è un campo obbligatorio")]
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


       private string fDisplayMessaggio;
       [Size(4000), Persistent("DISPLAYREPORT"), XafDisplayName("Messaggio")]
       [RuleRequiredField("RReqField.SetMessaggioCommessa.DisplayMessaggio", DefaultContexts.Save, "Display Report è un campo obbligatorio")]
       [DbType("varchar(4000)")]
       public string DisplayMessaggio
       {
           get
           {
               return fDisplayMessaggio;

           }
           set
           {
               SetPropertyValue<string>("DisplayMessaggio", ref fDisplayMessaggio, value);
           }
       }

       [Persistent("DISPLAYIMAGE"), DevExpress.Xpo.DisplayName("Display Messaggio Immagine")]         
       //[DevExpress.Xpo.Size(SizeAttribute.Unlimited), ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited) ]
        [VisibleInListViewAttribute(true)]
       [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
           ListViewImageEditorCustomHeight = 80, DetailViewImageEditorFixedHeight = 160)]
       [Delayed(true)]
       public byte[] DisplayMessaggioImage
       {
           //get
           //{
           //    return GetDelayedPropertyValue<Image>("DisplayMessaggioImage");
           //}
           //set
           //{
           //    SetDelayedPropertyValue<Image>("DisplayMessaggioImage", value);
           //}
            get
            {
                return GetDelayedPropertyValue<byte[]>("DisplayMessaggioImage");
            }
            set
            {
                SetDelayedPropertyValue<byte[]>("DisplayMessaggioImage", value);
            }
        }

    }
}

 

