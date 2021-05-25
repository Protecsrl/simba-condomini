using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlant;
using CAMS.Module.DBALibrary;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPDATAFISSADETTAGLIO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Data Fissa")]
    [ImageName("WeekView")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDataFissaDettaglio : XPObject
    {
        public MPDataFissaDettaglio()
            : base()
        {
        }

        public MPDataFissaDettaglio(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private Asset fAsset;
        [Persistent("ASSET"),
        DisplayName("Asset")]
        [ExplicitLoading]
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

        private SchedaMp fSchedaMP;
        [Persistent("SCHEDAMP"),       DisplayName("Scheda Mp")]
        [MemberDesignTimeVisibility(false)]
        public SchedaMp SchedaMp
        {
            get
            {
                return fSchedaMP;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMP, value);
            }
        }

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPARATOSCHEDAMP"), DisplayName("Procedura Attività Mp")]
        [ExplicitLoading]
        public AssetSchedaMP ApparatoSchedaMP
        {
            get
            {
                return fApparatoSchedaMP;
            }
            set
            {
                SetPropertyValue<AssetSchedaMP>("ApparatoSchedaMP", ref fApparatoSchedaMP, value);
            }
        }
        private DateTime fData;
        [Persistent("DATA"),     DisplayName("Data Cadenza Calcolata"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
        public DateTime Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }


        private MPDataFissa fMPDataFissa;
        [MemberDesignTimeVisibility(false),
        Association("MPDataFissa_Dettaglio", typeof(MPDataFissa))]
        [Persistent("MPDATAFISSA"),
        DisplayName("MP Data Fissa")]
        public MPDataFissa MPDataFissa
        {
            get
            {
                return fMPDataFissa;
            }
            set
            {
                SetPropertyValue("MPDataFissa", ref fMPDataFissa, value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0} - {1} ", fAsset, SchedaMp);
        }
    }
}
