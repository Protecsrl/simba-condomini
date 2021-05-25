using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.DC;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPDATAINIZIALE"), System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impostazione data Iniziale")]
    [DevExpress.ExpressApp.DC.XafDisplayName("Impostazione data Iniziale")]
    // [ DefaultListViewOptions(NewItemRowPosition.Top)]
    [ImageName("Today")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDataIniziale : XPObject
    {
        public MPDataIniziale() : base() { }
        public MPDataIniziale(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy";

        private RegPianificazioneMP fRegPianificazioneMP;
        [Association(@"RegPianoMP_MPDataIniziale", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANO"), DisplayName("Registro Pianificazione")]
        //[Appearance("MPDataIniziale.RegPianificazioneMP", Enabled = false)]
        [MemberDesignTimeVisibility(false)]
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


        private Asset fAsset;
        [Persistent("ASSET"), DisplayName("Asset")]
        [ImmediatePostData]
        [RuleRequiredField("RReqField.MPDataIniziale.Asset", DefaultContexts.Save, "Asset è un campo obbligatorio")]
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

        [PersistentAlias("MPDataInizialeDettaglios.Min(Data)")]
        [DevExpress.Xpo.DisplayName("Data Prima Cadenza Prevista")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataPrimaCadenzaPrevista
        {
            get
            {
                if (this.Oid == -1) return DateTime.MinValue;

                var tempObject = EvaluateAlias("DataPrimaCadenzaPrevista");
                if (tempObject != null)
                {
                    return (DateTime)tempObject;
                }
                else
                {
                    return DateTime.MinValue;
                }


            }

        }
        [XafDisplayName(@"Cluster/Immobile/Impianto")]
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

        private int fNr_Interventi;
        [Persistent("NR_INTERVENTI"), DevExpress.Xpo.DisplayName("Num interventi Calcolati"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [Delayed(true)]
        public int Nr_Interventi
        {
            get { return GetDelayedPropertyValue<int>("Nr_Interventi"); }
            set { SetDelayedPropertyValue<int>("Nr_Interventi", value); }
        }


        private string fFrequenze_Comprese;
        [Persistent("FREQUENZE_COMPRESE"), Size(250), DevExpress.Xpo.DisplayName("Frequenze Comprese")]
        [DbType("varchar(500)")]
        //[RuleRequiredField("RReqField.Immobile.Descrizione", DefaultContexts.Save, @"La Descrizione è un campo obbligatorio")]
        public string Frequenze_Comprese
        {
            get
            {
                return fFrequenze_Comprese;
            }
            set
            {
                SetPropertyValue<string>("Frequenze_Comprese", ref fFrequenze_Comprese, value);
            }
        }

        //private DateTime fDataUltimaAttivita;
        // [Persistent("DATAULTIMAMP"),  DevExpress.Xpo.DisplayName("Data  cadenza prevista")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[System.ComponentModel.Browsable(false)]
        //public DateTime DataUltimaAttivita
        //{
        //    get
        //    {

        //        return fDataUltimaAttivita;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataUltimaAttivita", ref fDataUltimaAttivita, value);
        //    }
        //} 

        #region  Alias
        //[DisplayName("Num interventi Calcolati"), VisibleInLookupListView(false)]
        //[PersistentAlias("MPDataInizialeDettaglios.Count")]
        //public int nrInterventi
        //{
        //    get
        //    {
        //        if (IsInvalidated)
        //            return 0;

        //        var tempObject = EvaluateAlias("nrInterventi");
        //        if (tempObject != null)
        //        {
        //            return (int)tempObject;
        //        }
        //        else
        //        {
        //            return 0;
        //        }
        //    }
        //}









        private ClusterEdifici fClusterEdifici;
        [PersistentAlias("Apparato.Impianto.Immobile.ClusterEdifici"), DisplayName("Cluster")]
        [ExplicitLoading()]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("ClusterEdifici");
                if (tempObject != null)
                {
                    return (ClusterEdifici)tempObject;
                }
                else
                {
                    return null;
                }
                return fClusterEdifici;
            }
        }

        //private Immobile fEdificio;
        [PersistentAlias("Apparato.Impianto.Immobile"), DisplayName("Immobile")]
        public Immobile Immobile
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                {
                    return (Immobile)tempObject;
                }
                else
                {
                    return null;
                }
                return null;
            }
        }

        



        //[NonPersistent, DisplayName("Frequenze Comprese")]
        ////[ModelDefault("DisplayFormat", "{0:C}")]
        ////[ModelDefault("EditMask", "C")]
        //public string FrequenzeComprese
        //{
        //    get
        //    { 
        //        StringBuilder sbFreq = new StringBuilder("", 32000);
        //       var FreList = Apparato.AppSchedaMpes.Select(s => s.FrequenzaOpt.CodDescrizione).Distinct();
        //       foreach (string Codice in FreList)
        //       {
        //           if (sbFreq.ToString() == "")
        //           { 
        //               sbFreq.Append(Codice);                      
        //           }
        //           else
        //           {
                        
        //                sbFreq.Append(",");
        //               sbFreq.Append(Codice); 
        //           }
        //       }
        //       return sbFreq.ToString();
        //    }

        //}



        //private Impianto fServizio;
        [PersistentAlias("Apparato.Servizio"), DisplayName("Servizio")]
        public Servizio Servizio
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("Servizio");
                if (tempObject != null)
                {
                    return (Servizio)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }
        #endregion

        #region Associazioni
        [Association(@"MPDataIniziale_Dettaglio", typeof(MPDataInizialeDettaglio)), DevExpress.Xpo.Aggregated, DisplayName("Cadenze Stimate")]
        public XPCollection<MPDataInizialeDettaglio> MPDataInizialeDettaglios
        {
            get
            {
                return GetCollection<MPDataInizialeDettaglio>("MPDataInizialeDettaglios");
            }
        }
        #endregion
        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (!this.IsLoading)
            {
                if (this.Oid > 1)
                {//fDataFermo
                    //if (newValue != null && propertyName == "OreMedieSetEsercizio1")
                    //{
                    //    int newV = (int)(newValue);
                    //    if (newV > 1 && newV < 25)
                    //    {
                    //        Apparato CurApparato = Session.GetObjectByKey<Apparato>(this.Apparato.Oid);
                    //        CurApparato.OreMedieSetEsercizio = newV;
                    //        CurApparato.Save();
                    //        CurApparato.Session.CommitTransaction();
                    //    }
                    //}
                    //if (newValue != null && propertyName == "DataLettura1")
                    //{
                    //    DateTime newV = (DateTime)(newValue);
                    //    int result = DateTime.Compare(newV, DateTime.Now); //Compare(date1, date2);  8/1/2009 12:00:00 AM is earlier than 8/1/2009 12:00:00 PM
                    //    string relationship;
                    //    if (result < 0)
                    //    //    relationship = "is earlier than";
                    //    //else if (result == 0)
                    //    //    relationship = "is the same time as";
                    //    //else                            
                    //    {
                    //        Apparato CurApparato = Session.GetObjectByKey<Apparato>(this.Apparato.Oid);
                    //        CurApparato.DataLettura = newV;
                    //        CurApparato.Save();
                    //        CurApparato.Session.CommitTransaction();
                    //    }
                    //}
                    //if (newValue != null && propertyName == "ValoreUltimaLettura1")
                    //{
                    //    Double newV = (Double)(newValue);
                    //    if (newV > this.ValoreUltimaLettura)
                    //    {
                    //        Apparato CurApparato = Session.GetObjectByKey<Apparato>(this.Apparato.Oid);
                    //        CurApparato.ValoreUltimaLettura = newV;
                    //        CurApparato.Save();
                    //        CurApparato.Session.CommitTransaction();
                    //    }
                    //}
                }
            }
            //Richiedente

        }


    }
}




