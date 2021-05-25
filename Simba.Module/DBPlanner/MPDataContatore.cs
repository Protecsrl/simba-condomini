using CAMS.Module.DBPlant;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions,
    Persistent("MPDATACONTATORE")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Impostazione data Contatore")]
    //[DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [ImageName("Today")]
    [NavigationItem(false)]
    [VisibleInDashboards(false)]
    public class MPDataContatore : XPObject
    {
        public MPDataContatore()
            : base()
        {
        }

        public MPDataContatore(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private RegPianificazioneMP fRegPianificazioneMP;
        [MemberDesignTimeVisibility(false)]
        [Association(@"RegPianoMP_MPDataContatore", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANO"), DisplayName("Registro Pianificazione")]
        [Appearance("MPDataContatore.RegPianificazioneMP", Enabled = false)]
        [ImmediatePostData]
        public RegPianificazioneMP RegPianificazioneMP
        {
            get
            {
                return fRegPianificazioneMP;
            }
            set
            {
                //SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value);
                if (SetPropertyValue<RegPianificazioneMP>("RegPianificazioneMP", ref fRegPianificazioneMP, value))
                {
                    OnChanged("Scenario");

                }
            }
        }


        [PersistentAlias("RegPianificazioneMP.Scenario"), DevExpress.Xpo.DisplayName("Scenario")]
        [VisibleInListView(false),VisibleInDetailView(false)]
        [ExplicitLoading()]
        public Scenario Scenario
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("Scenario");
                if (tempObject != null)
                {
                    return (Scenario)tempObject;
                }
                else
                {
                    return null;
                }
        
            }
        }

        #region
        #endregion

        private Asset fAsset;
        [Persistent("ASSET"), DisplayName("Asset")]
        [RuleRequiredField("RReqField.MPDataContatore.Asset", DefaultContexts.Save, "Asset è un campo obbligatorio")]
        [DataSourceCriteria("Servizio.Immobile.ClusterEdifici.Scenario = '@This.RegPianificazioneMP.Scenario'")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
        [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
        [ImmediatePostData]
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

        [PersistentAlias("Apparato.Tag"), DevExpress.Xpo.DisplayName("Tag")]
        public string Tag
        {
            get
            {

                if (IsInvalidated)
                    return "";
                var tempObject = EvaluateAlias("Tag");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return "";
                }
            }
        }

        private DateTime fDataUltimaLettura;
        [Persistent("DATAULTIMALETTURA"), DevExpress.Xpo.DisplayName("Set Data Ultima Lettura")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        public DateTime DataUltimaLettura
        {
            get
            {
                return fDataUltimaLettura;
            }
            set
            {
                SetPropertyValue<DateTime>("DataUltimaLettura", ref fDataUltimaLettura, value);
                OnChanged("AppLetturaDettaglio");
            }
        }
        private int fUltimaLettura;
        [Persistent("ULTIMALETTURA"), DisplayName("Set Ultima Lettura(h)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int UltimaLettura
        {
            get
            {
                return fUltimaLettura;
            }
            set
            {
                SetPropertyValue<int>("UltimaLettura", ref fUltimaLettura, value);
                OnChanged("AppLetturaDettaglio");
            }
        }

        private int fOreEsercizioSettimanali;
        [Persistent("ORESETTESERCIZIO"), DisplayName("Set Ore Esercizio Settimanali")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [EditorAlias(CAMSEditorAliases.CustomRangeIntEditor)]
        public int OreEsercizioSettimanali
        {
            get
            {
                return fOreEsercizioSettimanali;
            }
            set
            {
                SetPropertyValue<int>("OreEsercizioSettimanali", ref fOreEsercizioSettimanali, value);
                OnChanged("AppLetturaDettaglio");
            }
        }


        #region Alias di Apparato
        [PersistentAlias("Apparato.DataLettura"), DevExpress.Xpo.DisplayName("Data Lettura")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public string AppDataLettura
        {
            get
            {
                if (IsInvalidated)
                    return null;
                var tempObject = EvaluateAlias("AppDataLettura");
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
        [PersistentAlias("Apparato.ValoreUltimaLettura"), DevExpress.Xpo.DisplayName("Ultima Lettura(h)")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        public Double AppValoreUltimaLettura
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("AppValoreUltimaLettura");
                if (tempObject != null)
                {
                    return (Double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("Apparato.OreMedieSetEsercizio"), DevExpress.Xpo.DisplayName("Ore Esercizio Settimanali")]
        [VisibleInDetailView(false),VisibleInListView(false)]
        public int AppOreMedieSetEsercizio
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("AppOreMedieSetEsercizio");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        private string fAppLetturaDettaglio;
        [PersistentAlias("'Data:' + Apparato.DataLettura + ', Valore:' + Apparato.ValoreUltimaLettura + ', H Med.Sett. Esercizio:' +Apparato.OreMedieSetEsercizio"), System.ComponentModel.DisplayName("Ultima Lettura")]
       // [VisibleInDetailView(false)]
        [System.ComponentModel.Browsable(false)]
        public string AppLetturaDettaglio
        {
            get
            {
                if (this.Oid == -1) return string.Empty;

                object tempObject = EvaluateAlias("AppLetturaDettaglio");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
                ////  return string.Format("Data:{0} ,Valore:{1} ,H Med.Sett. Esercizio:{2}", sAppDataLettura, sAppValoreUltimaLettura, sAppOreMedieSetEsercizio);  
                //using (Classi.UtilController u = new Classi.UtilController())
                //{
                //    fAppLetturaDettaglio = u.GetDettaglioApparatoUltimaLettura(Evaluate("AppDataLettura"), Evaluate("AppValoreUltimaLettura"), Evaluate("AppOreMedieSetEsercizio"));
                //}
                //return fAppLetturaDettaglio;
            }
        }

        #endregion

        #region  Alias
        [DisplayName("Num interventi Calcolati"), VisibleInLookupListView(false)]
        [PersistentAlias("MPDataContatoreDettaglios.Count")]
        public int nrInterventi
        {
            get
            {
                if (IsInvalidated)
                    return 0;
                var tempObject = EvaluateAlias("nrInterventi");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        #endregion

        #region Associazioni
        [Association(@"MPDataContatore_Dettaglio", typeof(MPDataContatoreDettaglio)), Aggregated]
        [DevExpress.ExpressApp.DC.XafDisplayName(@"Attività a Contaore")]
        [Appearance("mpdatacontatore.dettaglio.Visible", Criteria = "MPDataContatoreDettaglios.Count==0", Visibility = ViewItemVisibility.Hide)]
        [ImmediatePostData]
        public XPCollection<MPDataContatoreDettaglio> MPDataContatoreDettaglios
        {
            get
            {
                return GetCollection<MPDataContatoreDettaglio>("MPDataContatoreDettaglios");
            }
        }
        #endregion

      

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (this.Oid > 1)
                {//fDataFermo
                    if (newValue != null && propertyName == "OreEsercizioSettimanali")
                    {
                        int newV = (int)(newValue);
                        if (newV > 1 && newV < 25)
                        {
                            cambiato = true;
                            EvaluateAlias("AppLetturaDettaglio");
                            //Apparato CurApparato = Session.GetObjectByKey<Apparato>(this.Apparato.Oid);
                            //CurApparato.OreMedieSetEsercizio = newV;
                            //CurApparato.Save();
                            //CurApparato.Session.CommitTransaction();
                            //this.Save();
                            //this.Session.CommitTransaction();
                            //using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                            //{
                            //    //db.SetParContatoreAppMP(this.Oid, "OreMedieSetEsercizio1", newV.ToString());
                            //}

                        }
                    }
                    if (newValue != null && propertyName == "DataUltimaLettura")
                    {
                        DateTime newV = (DateTime)(newValue);
                        int result = DateTime.Compare(newV, DateTime.Now); //Compare(date1, date2);  8/1/2009 12:00:00 AM is earlier than 8/1/2009 12:00:00 PM
                        string relationship;
                        if (result < 0)
                        {
                            cambiato = true;
                            EvaluateAlias("AppLetturaDettaglio");
                        }
                        ////    relationship = "is earlier than";
                        ////else if (result == 0)
                        ////    relationship = "is the same time as";
                        ////else                            
                        //{
                        //    //   Apparato CurApparato = Session.GetObjectByKey<Apparato>(this.Apparato.Oid);
                        //    //   CurApparato.DataLettura = newV;
                        //    //   CurApparato.Save();
                        //    //CurApparato.Session.CommitTransaction();
                        //    //this.Save();
                        //    //this.Session.CommitTransaction();
                        //    //using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                        //    //{
                        //    //    //db.SetParContatoreAppMP(this.Oid, "OreMedieSetEsercizio1", newV.ToString());
                        //    //    //  db.SetParContatoreAppMP(this.Oid, "OreMedieSetEsercizio1", newV.ToString());
                        //    //}

                        //}
                    }
                    if (newValue != null && propertyName == "UltimaLettura")
                    {
                        int newV = (int)(newValue);
                        if (newV > this.AppValoreUltimaLettura)
                        {
                            cambiato = true;
                            EvaluateAlias("AppLetturaDettaglio");
                            //Apparato CurApparato = Session.GetObjectByKey<Apparato>(this.Apparato.Oid);
                            //CurApparato.ValoreUltimaLettura = newV;
                            //CurApparato.Save();
                            //CurApparato.Session.CommitTransaction();
                        }
                    }
                }
            }
          
        }


        [ImmediatePostData]
        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public bool cambiato { get; set; }

        

        protected override void OnSaved()
        {
            base.OnSaved();       
            if (this.cambiato)
            {
                SalvaMPDataContatore();
                this.cambiato = false;

            }

        }

        private void SalvaMPDataContatore()
        { 
            try
            {
                //using (DB db = new DB())
                //{
                //    //db.SetParContatoreAppMP(this.Oid);
                //}
            }
            catch
            {
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
        //public override string ToString()
        //{
        //    return FullName;
        //}
    }
}
