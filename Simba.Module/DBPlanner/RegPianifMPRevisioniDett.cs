using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("REGPIANIFICAZIONEREVDETT")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Pianificazione MP Revisione Dettaglio")]
    [NavigationItem(false)]
    public class RegPianifMPRevisioniDett : XPObject
    {
        public RegPianifMPRevisioniDett(): base() {}

        public RegPianifMPRevisioniDett(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            // Place here your initialization code.
        }
        private RegPianificazioneMPRevisioni fRegPianificazioneMPRevisioni;
        [Persistent("REGPIANIFICAZIONEREV"), DisplayName("Reg. Pianificazione MP")]
        [Association(@"RegPianificazioneMPRevisioni_RegPianifMPRevisioniDett", typeof(RegPianificazioneMPRevisioni))]
        public RegPianificazioneMPRevisioni RegPianificazioneMPRevisioni
        {
            get
            {
                return fRegPianificazioneMPRevisioni;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMPRevisioni>("RegPianificazioneMPRevisioni", ref fRegPianificazioneMPRevisioni, value);
            }
        }

        [Persistent("SCENARIO"), DisplayName("Scenario")]
        //[Association(@"PianificazioneSchedeMP_Scenario", typeof(Scenario))]
        [ExplicitLoading()]
        [Delayed(true)]
        public Scenario Scenario
        {
            get
            {
                return GetDelayedPropertyValue<Scenario>("Scenario");
                //return fScenario;
            }
            set
            {
                SetDelayedPropertyValue<Scenario>("Scenario", value);
                //SetPropertyValue<Scenario>("Scenario", ref fScenario, value);
            }
        }

      
        [Persistent("CLUSTEREDIFICI"), DisplayName("Cluster Edifici")]
        //[Association(@"PianificazioneSchedeMP_ClusterImpianti", typeof(ClusterImpianti))]
        [ExplicitLoading() ]
        [Delayed(true)]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
                //return fClusterEdifici;
            }
            set
            {
                SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
                //SetPropertyValue<ClusterEdifici>("ClusterEdifici", ref fClusterEdifici, value);
            }
        }

        private Servizio fServizio;
        [Persistent("SERVIZIO"), DisplayName("Servizio")]
        //[Association(@"PianificazioneSchedeMP_Impianto", typeof(Impianto))]
        public Servizio Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue<Servizio>("Servizio", ref fServizio, value);
            }
        }

        private Asset fApparato;
        [Persistent("APPARATO"), DisplayName("Apparato")]
        //[Association(@"PianificazioneSchedeMP_Apparato", typeof(Apparato))]
        [ExplicitLoading]
        public Asset Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<Asset>("Apparato", ref fApparato, value);
            }
        }

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPARATOSCHEDAMP"), DisplayName("Apparato Scheda MP")]
        //[Association(@"PianificazioneSchedeMP_ApparatoSchedaMP", typeof(ApparatoSchedaMP))]
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

    }
}
