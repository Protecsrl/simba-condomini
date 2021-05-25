using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.Xpo;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Validation;
using CAMS.Module.Classi;
using CAMS.Module.DBPlanner.SK;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPDATAINIZIALEDETTAGLIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Cadenze Stimate")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Dettaglio Cadenze Stimate")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDataInizialeDettaglio : XPObject
    {
        public MPDataInizialeDettaglio()      : base()        {        }
        public MPDataInizialeDettaglio(Session session) : base(session) { }
        public override void AfterConstruction()        {            base.AfterConstruction();        }

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPSCHEDAMP"),   DisplayName("Procedura Attività MP")]
         [ImmediatePostData]
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
        [Persistent("DATA"),        DisplayName("Data Cadenza Stimata"),        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
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

        #region Alias
        [DisplayName("Frequenza"),     PersistentAlias("ApparatoSchedaMP.FrequenzaOpt.Descrizione")]       
        public string strFrequenza
        {
            get
            {
                var tempObject = EvaluateAlias("strFrequenza");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }
        [DisplayName("Mansione"),  PersistentAlias("ApparatoSchedaMP.MansioniOpt.Descrizione")]
        public string strMansione
        {
            get
            {
                var tempObject = EvaluateAlias("strMansione");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region ASSOCIAZIONI PADRE
        private MPDataIniziale fMPDataIniziale;
        [Persistent("MPDATAINIZIALE"), DisplayName("Rif. Padre")]
        [Association(@"MPDataIniziale_Dettaglio", typeof(MPDataIniziale))]
        [MemberDesignTimeVisibility(false)]
        public MPDataIniziale MPDataIniziale
        {
            get
            {
                return fMPDataIniziale;
            }
            set
            {

                SetPropertyValue<MPDataIniziale>("MPDataIniziale", ref fMPDataIniziale, value);
  
            }
        }
        #endregion

        #region Associazioni FIGLIO
        [Association(@"MPDataInizialeDettaglio_DATE", typeof(MPDataInizialeDettaglioDate)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName(@"Date Cadenza Stimata")]
        public XPCollection<MPDataInizialeDettaglioDate> MPDataInizialeDettaglioDates
        {
            get
            {
                return GetCollection<MPDataInizialeDettaglioDate>("MPDataInizialeDettaglioDates");
            }
        }
        #endregion


    }
}
