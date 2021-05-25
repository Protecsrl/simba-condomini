using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
     [DefaultClassOptions, Persistent("V_CLUSTEREDIFICIINSERIBILI")]
     [NavigationItem(false)]

     public class ClusterEdificiInseribili: XPLiteObject
    {
         public ClusterEdificiInseribili()
            : base()
        {
        }

         public ClusterEdificiInseribili(Session session)
            : base(session)
        {
        }


        private string fcodice;
        [Key, Size(50), Persistent("CODICE"), MemberDesignTimeVisibility(false)]
        [DbType("varchar(50)")]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
            }
        }

        private Scenario fScenario;
        [Persistent("SCENARIO"), DisplayName("Scenario")]
        [ExplicitLoading()]
        public Scenario Scenario
        {
            get
            {
                return fScenario;
            }
            set
            {
                SetPropertyValue("Scenario", ref fScenario, value);
            }
        }

        //private ClusterImpianti fClusterImpianti;
        //[Persistent("CLUSTERIMPIANTI"), DisplayName("Cluster Impianti")]
        //public ClusterImpianti ClusterImpianti
        //{
        //    get
        //    {
        //        return fClusterImpianti;
        //    }
        //    set
        //    {
        //        SetPropertyValue("ClusterImpianti", ref fClusterImpianti, value);
        //    }
        //}
        [Persistent("CLUSTEREDIFICI"), DisplayName("Cluster Edifici")]
        [ExplicitLoading]
        [Delayed(true)]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
            }
            set
            {
                SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
            }
        }



        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue("Immobile", ref fImmobile, value);
            }
        }

        private string fDistanza;
        [Size(100), Persistent("DISTANZA"), DisplayName("Distanza")]
        [DbType("varchar(100)")]
        public string Distanza
        {
            get
            {
                return fDistanza;
            }
            set
            {
                SetPropertyValue<string>("Distanza", ref fDistanza, value);
            }
        }

    }
}
