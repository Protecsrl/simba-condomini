using CAMS.Module.DBPlant;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPDATAPLURIANNUALI")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impostazione data PluriAnnuali")]
    [ImageName("Today")]
   // [NavigationItem(Captions.PropertyEditorsGroup),
        //DefaultListViewOptions(true, NewItemRowPosition.Top), 
        //System.ComponentModel.DisplayName(Captions.PropertyEditors_BooleanProperties)]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPAttivitaPluriAnnuali : XPObject
    {
        public MPAttivitaPluriAnnuali()
            : base()
        {
        }

        public MPAttivitaPluriAnnuali(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
         
        private RegPianificazioneMP fRegPianificazioneMP;
        [MemberDesignTimeVisibility(false),
        Association(@"RegPianoMP_MPAttivitaPluriAnnuali", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANO"),        DisplayName("Registro Pianificazione")]
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

        private AssetSchedaMP fApparatoSchedaMP;
        [Persistent("APPSCHEDAMP"),     DisplayName(@"Procedura Attività MP")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
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

        private Asset fAsset;
        [Persistent("ASSET"), DisplayName("Asset")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        //[DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        //[ExplicitLoading]
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



        #region Alias
        [DisplayName("Frequenza"), PersistentAlias("ApparatoSchedaMP.FrequenzaOpt.Descrizione")]
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
        [DisplayName("Mansione"), PersistentAlias("ApparatoSchedaMP.MansioniOpt.Descrizione")]
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


        private DateTime fData;
        [Persistent("DATA"), DisplayName("Data Cadenza Obbligata"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", " ddd dd/MM/yyyy")]
      //  [EditorAlias( FeatureCenterEditorAliases.CustomDateTimeEditor)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [ImmediatePostData(true)]
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

        [PersistentAlias("'Cluster(' + Apparato.Impianto.Immobile.ClusterEdifici.Descrizione + '), Immobile(' + Apparato.Impianto.Immobile.Descrizione + '), Impianto(' + Apparato.Impianto.Descrizione + ')'")]
        [Size(SizeAttribute.Unlimited)]
        public string FullName
        {
            get
            {
                object tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }


        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (this.Oid > 1)
                {//fDataFermo
                    if (newValue != null && propertyName == "mio")
                    {
                        int newV = (int)(newValue);
                        //if (newV > 1 && newV < 25)
                        //{
                        //    cambiato = true;      
                        //}
                    }
                    if (newValue != null && propertyName == "DataUltimaLettura")
                    {
                        DateTime newV = (DateTime)(newValue);
                        int result = DateTime.Compare(newV, DateTime.Now); //Compare(date1, date2);  8/1/2009 12:00:00 AM is earlier than 8/1/2009 12:00:00 PM
                        string relationship;
                        //if (result < 0)
                        //    cambiato = true;
                       
                    }
                    if (newValue != null && propertyName == "UltimaLettura")
                    {
                        //int newV = (int)(newValue);
                        //if (newV > this.AppValoreUltimaLettura)
                        //{
                        //    cambiato = true;
                            
                        //}
                    }
                }
            }
            //Richiedente

        }


        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;

        }

    }
}
