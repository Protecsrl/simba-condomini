using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPDATACONTATOREDETTAGLIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Interventi")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    public class MPDataContatoreDettaglio : XPObject
    {
        public MPDataContatoreDettaglio() : base() { }
        public MPDataContatoreDettaglio(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPSCHEDAMP"), DisplayName("Procedura Attività MP")]
        //[RuleRequiredField("RReqField.MPDataContatoreDettaglio.ApparatoSchedaMP", DefaultContexts.Save, "La Procedure Attività MP è un campo obbligatorio")]
        //[Appearance("MPDataContatoreDettaglio.ApparatoSchedaMP", Criteria = "[Apparato] Is Null Or [Data] Is Not Null", Enabled = false)]
        //  [DataSourceCriteria("Apparato = '@This.Apparato' And Categoria.Oid = 1 And FrequenzaOpt.TipoCadenze !='Giorno'")] //And TipologiaIntervento >= 1
        [ImmediatePostData]
        [VisibleInDashboards(false)]
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
        [Persistent("DATA"), DisplayName("Data Cadenza Stimata"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]

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
        [DisplayName("Frequenza"), PersistentAlias("ApparatoSchedaMP.FrequenzaOpt.Descrizione")]
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
        [DisplayName("Mansione"), PersistentAlias("ApparatoSchedaMP.MansioniOpt.Descrizione")]
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
        private MPDataContatore fMPDataContatore;
        [Association(@"MPDataContatore_Dettaglio", typeof(MPDataContatore))]
        [Persistent("MPDATACONTATORE"), DisplayName("Rifermento Padre")]
        [MemberDesignTimeVisibility(false)]
        public MPDataContatore MPDataContatore
        {
            get
            {
                return fMPDataContatore;
            }
            set
            {

                SetPropertyValue<MPDataContatore>("MPDataContatore", ref fMPDataContatore, value);

            }
        }
        #endregion
        #region Associazioni FIGLIO
        [Association(@"MPDataContatoreDettaglio_DATE", typeof(MPDataContatoreDettaglioDate)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName(@"Date Cadenza Stimata")]
        [Appearance("MPDataContatoreDettaglioDates.dettaglio.Visible", Criteria = "MPDataContatoreDettaglioDates.Count==0", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<MPDataContatoreDettaglioDate> MPDataContatoreDettaglioDates
        {
            get
            {
                return GetCollection<MPDataContatoreDettaglioDate>("MPDataContatoreDettaglioDates");
            }
        }
        #endregion


    }
}
