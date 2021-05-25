using DevExpress.Data.Filtering;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.Drawing;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBPlant;

namespace CAMS.Module.DBMaps
{
    [DefaultClassOptions, Persistent("AppMAP_RdL_UC")]
    [System.ComponentModel.DefaultProperty("Mappa Ricerca Apparato")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Mappa Ricerca Apparato")]
    [ImageName("BO_Country_v92")]
    [NavigationItem("Patrimonio")]
    public class ucApparatoMapRdL : XPObject
    {

        public ucApparatoMapRdL() : base() { }

        public ucApparatoMapRdL(Session session) : base(session) { }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetPropertyValue(nameof(Title), ref title, value); }
        }
        //private string fOBJWEB;
        //[NonPersistent, Size(100), DevExpress.Xpo.DisplayName("web")]
        //[VisibleInListView(false)]
        //public string OBJWEB
        //{
        //    get { return fOBJWEB; }
        //    set { SetPropertyValue<string>("OBJWEB", ref fOBJWEB, value); }
        //}

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInListView(false)]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }


        private Servizio fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile.Oid = '@This.Immobile.Oid'")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
            }
        }

        private int fApparato;
        [Persistent("OIDAPPARATO"), System.ComponentModel.DisplayName("Oid Apparato")]
        [Delayed(true)]
        public int OidApparato
        {
            get { return GetDelayedPropertyValue<int>("OidApparato"); }
            set { SetDelayedPropertyValue<int>("OidApparato", value); }
        }

        [NonPersistent]
        private XPCollection<AssetoMap> _ApparatoMaps;
        [DevExpress.ExpressApp.DC.XafDisplayName("Mappa")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AssetoMap> ApparatoMaps
        {
            get
            {
                if (Servizio == null)
                {
                    _ApparatoMaps = null;
                    return _ApparatoMaps;
                }

                if (_ApparatoMaps == null)
                {
                    BinaryOperator bOp = new BinaryOperator("OidImpianto", Evaluate("Impianto.Oid"));// Impianto.Oid);
                    _ApparatoMaps = new XPCollection<AssetoMap>(Session, bOp);             //ParCriteria));CriteriaOperator.Parse(       "OidApparato = ? Or OidApparatoPadre = ?", Evaluate("OidApparato"), Evaluate("OidApparato")
                }
                return _ApparatoMaps;
            }
        }
    }
}