using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RDLAPPARATOSCHEDEMP")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL SchedeMp")]
    [NavigationItem(false)]
    public class RdLApparatoSchedeMP : XPObject
    {
        public RdLApparatoSchedeMP()
            : base()
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public RdLApparatoSchedeMP(Session session)
            : base(session)
        {
            // This constructor is used when an object is loaded from a persistent storage.
            // Do not place any code here.
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }


        private RdL fRdL;
        [Persistent("RDL"), Association(@"RdLApparatoSchedeMp_RdL"), DisplayName("RdL")]
        //[ExplicitLoading()]
        [Delayed(true)]
        public RdL RdL
        {
            get
            {
                return GetDelayedPropertyValue<RdL>("RdL");
                //return fRdL;
            }
            set
            {
                SetDelayedPropertyValue<RdL>("RdL", value);
                //SetPropertyValue<RdL>("RdL", ref fRdL, value);
            }
        }     

//Association(@"RdLApparatoSchedeMp_ApparatoSkMP"),
        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPARATOSCHEDAMP"),  DisplayName("Procedura MP Associata")]
        //[ExplicitLoading]
        [Delayed(true)]
        public AssetSchedaMP ApparatoSchedaMP
        {
            get
            {
                return GetDelayedPropertyValue<AssetSchedaMP>("ApparatoSchedaMP");
                //return fApparatoSchedaMP;
            }
            set
            {
                SetDelayedPropertyValue<AssetSchedaMP>("ApparatoSchedaMP", value);
                //SetPropertyValue<ApparatoSchedaMP>("ApparatoSchedaMP", ref fApparatoSchedaMP, value);
            }
        }
        #region associazioni relative alle attività pianificate dettaglio
        [NonPersistent]
        private XPCollection<SchedaMpPassi> fSchedaMpPassis;
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [XafDisplayName("Passi Attività MP")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<SchedaMpPassi> SchedaMpPassis
        {
            get
            {
                if (this.Oid == -1) return null;
                if (this.ApparatoSchedaMP != null && this.ApparatoSchedaMP.SchedaMp != null)
                {
                    string ParCriteria = string.Format("SchedaMp.Oid == {0}", Evaluate("ApparatoSchedaMP.SchedaMp.Oid"));

                    fSchedaMpPassis = new XPCollection<SchedaMpPassi>(Session, CriteriaOperator.Parse(ParCriteria));
                    //fSchedaMpPassis.Criteria = CriteriaOperator.Parse(ParCriteria);
                }
                return fSchedaMpPassis;
            }
        }

        [PersistentAlias("ApparatoSchedaMP.CodSchedaMp + '(' + ApparatoSchedaMP.CodUni + ')'")]
        [XafDisplayName("Cod. Attività MP")]
        public string CodSchedaMPUni
        {
            get
            {
                    object tempObject = EvaluateAlias("CodSchedaMPUni");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
            }
        }
        #endregion
        public override string ToString()
        {
            if(ApparatoSchedaMP!=null)
            return string.Format("{0}({1}) - {2}", ApparatoSchedaMP.CodSchedaMp, ApparatoSchedaMP.CodUni,ApparatoSchedaMP.DescrizioneManutenzione);
            else
                return null;

        }


    }

}