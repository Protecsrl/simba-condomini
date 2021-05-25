using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.DBPlant;
using CAMS.Module.DBAngrafica;
using CAMS.Module.PropertyEditors;
namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPDATASTAGIONALE")]
    // [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impostazione Data Stagionale")]
    [ImageName("Today")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDateStagionali : XPObject
    {
        public MPDateStagionali() : base() { }
        public MPDateStagionali(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegPianificazioneMP fRegPianificazioneMP;
        [MemberDesignTimeVisibility(false),
        Association(@"RegPianoMP_MPDateStagionali", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANO"), DisplayName("Registro Pianificazione")]
        [Appearance("MPAttivitaPluriAnnuali.RegPianificazioneMP", Enabled = false)]
        [ImmediatePostData]
        public RegPianificazioneMP RegPianificazioneMP
        {
            get
            {
                return fRegPianificazioneMP;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value);
            }
        }

        private AssetSchedaMP fAssetSchedaMP;
        [Persistent("APPSCHEDAMP"), DisplayName("Procedura Attività MP")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [ImmediatePostData]
        [ExplicitLoading]
        public AssetSchedaMP AssetSchedaMP
        {
            get
            {
                return fAssetSchedaMP;
            }
            set
            {
                SetPropertyValue<AssetSchedaMP>("AssetSchedaMP", ref fAssetSchedaMP, value);
            }
        }


        private DateTime fData;
        [Persistent("DATA"), DisplayName("Data Cadenza Stimata"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
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
        [DisplayName("Frequenza"), PersistentAlias("AssetSchedaMP.FrequenzaOpt.Descrizione")]
        public string strFrequenza
        {
            get
            {
                if (IsInvalidated)
                    return null;
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
        [DisplayName("Mansione"), PersistentAlias("AssetSchedaMP.MansioniOpt.Descrizione")]
        public string strMansione
        {
            get
            {
                if (IsInvalidated)
                    return null;
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


       

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;

        }

    }
}
