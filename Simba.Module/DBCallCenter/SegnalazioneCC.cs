using CAMS.Module.DBAux;
using CAMS.Module.DBPlant;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Diagnostics;
using System.Drawing;

namespace CAMS.Module.DBCallCenter
{
    [DefaultClassOptions, Persistent("SEGNALAZIONECC")]  //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Segnalazioni Call Center")]
    [ImageName("ShowTestReport")]
    [NavigationItem("Segnalazioni")]
    #region
  
    #endregion

    public class SegnalazioneCC : XPObject
    {// private const int GiorniRitardoRicerca = -7;
        public SegnalazioneCC() : base() { }
        public SegnalazioneCC(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
           
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public Immobile Immobile
        {
            get
            {
                return GetDelayedPropertyValue<Immobile>("Immobile");
            }
            set
            {
                SetDelayedPropertyValue<Immobile>("Immobile", value);
            }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [VisibleInDashboards(false)]
        public string RicercaEdificio
        {
            get;
            set;
        }


        private Servizio fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        //[Appearance("SegnalazioneCC.Abilita.Impianto", Criteria = "(Immobile  is null) OR (not (Apparato is null))", FontColor = "Black", Enabled = false)]
        //[RuleRequiredField("SegnalazioneCC.RuleRequiredField.Impianto", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        //[Appearance("SegnalazioneCC.Impianto.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Impianto)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[DataSourceCriteria("Immobile = '@This.Immobile'")] '@This.Piano' is null And '@This.Locale' is null,Immobile = '@This.Immobile'," +
        //[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Immobile = '@This.Immobile'," +
        //                      "Iif(!IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Immobile = '@This.Immobile' And APPARATOes[Locale.Piano = '@This.Piano']," +
        //                       "Iif(!IsNullOrEmpty('@This.Piano') And !IsNullOrEmpty('@This.Locale'),Immobile = '@This.Immobile' And APPARATOes[Locale = '@This.Locale' Or CodDescrizione == 'ND']," +
        //                       "Immobile = '@This.Immobile' And APPARATOes[Locale = '@This.Locale'])" +
        //                      ")" +
        //                     ")"
        //                     )]
        [Delayed(true)]
        public Servizio Servizio
        {
            get
            {
                return GetDelayedPropertyValue<Servizio>("Servizio");
            }
            set
            {
                SetDelayedPropertyValue<Servizio>("Servizio", value);
            }
        }

        private Asset fApparato;  ///[UltimoStatoSmistamento.Oid] > 1
        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        //[Appearance("SegnalazioneCC.Abilita.Apparato", Criteria = "(Impianto is null) OR (not (Problema is null)) Or  [UltimoStatoSmistamento.Oid] In(2,3,4,5,6,7,8,9,10)", FontColor = "Black", Enabled = false)]
        //[Appearance("SegnalazioneCC.Apparato.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Apparato)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[RuleRequiredField("SegnalazioneCC.RuleRequiredField.Apparato", DefaultContexts.Save, "Apparato è un campo obbligatorio")]
        //[DataSourceCriteria("Impianto = '@This.Impianto'")]
        //[DataSourceCriteria("Iif(IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Impianto.Immobile = '@This.Immobile' And Impianto = '@This.Impianto'," +
        //              "Iif(!IsNullOrEmpty('@This.Piano') And IsNullOrEmpty('@This.Locale'),Impianto.Immobile = '@This.Immobile' And Locale.Piano = '@This.Piano' And Impianto = '@This.Impianto'," +
        //               "Iif(!IsNullOrEmpty('@This.Piano') And !IsNullOrEmpty('@This.Locale'),Impianto.Immobile = '@This.Immobile' And Locale = '@This.Locale' And Impianto = '@This.Impianto'," +
        //               "Impianto.Immobile = '@This.Immobile' And Locale = '@This.Locale')" +
        //              ")" +
        //             ")"
        //             )]
        //[Association(@"RdL_Apparato", typeof(Apparato))]
        [Delayed(true)]
        public Asset Apparato
        {
            get
            {
                return GetDelayedPropertyValue<Asset>("Apparato");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("Apparato", value);
            }
        }
        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        [VisibleInDashboards(false)]
        public string RicercaApparato
        {
            get;
            set;
        }
          
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        //  [RuleRequiredField("RuleReq.RdL.Richiedente", DefaultContexts.Save, "Richiedente è un campo obbligatorio")]
        //[DataSourceCriteria("Commesse = '@This.Immobile.Commesse'")]
        //[RuleRequiredField("SegnalazioneCC.Rihiedente.RuleRequiredField.su.Guasto", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "1==1")]
       // [Appearance("SegnalazioneCC.Abilita.Richiedente", Criteria = "Immobile is null Or [UltimoStatoSmistamento.Oid] > 2", FontColor = "Black", Enabled = false)]
        //[Appearance("SegnalazioneCC.Richiedente.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Richiedente)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public Richiedente Richiedente
        {
            get
            {
                return GetDelayedPropertyValue<Richiedente>("Richiedente");
            }
            set
            {
                SetDelayedPropertyValue<Richiedente>("Richiedente", value);
            }
        }
        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaRichiedente
        {
            get;
            set;
        }

        #region Piano e stanza

        private Piani fPiano;
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        ////   [RuleRequiredField("RuleReq.RdL.Piano", DefaultContexts.Save, "Piano è un campo obbligatorio")]
        //   [RuleRequiredField("RuleReq.RdL.Piano", DefaultContexts.Save, SkipNullOrEmptyValues = false,
        //                        TargetCriteria = "Iif(Immobile is null,true,Immobile.Commesse.MostraPianiLocali)")]
    //    [Appearance("SegnalazioneCC.Piano.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Immobile)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
     ////   [Appearance("SegnalazioneCC.Abilita.Piano", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
     //   [DataSourceCriteria("Immobile = '@This.Immobile'")]// QUESTO SIGNIFICA CHE TI DA SOLO I PIANI CHE SONO DELL'IMMOBILE SELEZIONATO
        [ExplicitLoading()]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Piani Piano
        {
            get
            {
                return GetDelayedPropertyValue<Piani>("Piano");
            }
            set
            {
                SetDelayedPropertyValue<Piani>("Piano", value);
            }

            //get
            //{
            //    return fPiano;
            //}
            //set
            //{
            //    SetPropertyValue<Piani>("Piano", ref fPiano, value);
            //}
        }

        //   Association(@"LOCALI_APPARATO"),
        private Locali fLocale;
        [Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
      ///  [Appearance("SegnalazioneCC.Locali", Enabled = false, Criteria = "!IsNullOrEmpty(Piano)", Context = "DetailView")]
     ///   [DataSourceCriteria("Piano = '@This.Piano'")]// su rdl si chiama piano, su locali piani
        //[ExplicitLoading()]
        [ImmediatePostData(true)]
        [Delayed(true)]
        public Locali Locale
        {
            get { return GetDelayedPropertyValue<Locali>("Locale"); }
            set { SetDelayedPropertyValue<Locali>("Locale", value); }

            //get { return fLocale; }
            //set { SetPropertyValue<Locali>("Locale", ref fLocale, value); }
        }

        [NonPersistent, Size(25)]
        [VisibleInListView(false), VisibleInDetailView(true), VisibleInLookupListView(false)]
        public string RicercaLocale
        {
            get;
            set;
        }

        //private string fReparto;
        //[Size(250), Persistent("REPARTO"), System.ComponentModel.DisplayName("Reparto")]
        //[DbType("varchar(250)")]
        //[VisibleInListView(false)]
        //[Delayed(true)]
        //public string Reparto
        //{
        //    get { return GetDelayedPropertyValue<string>("Reparto"); }
        //    set { SetDelayedPropertyValue<string>("Reparto", value); } 
           
        //}

        #endregion


        private Urgenza fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorità")]
        //  [DataSourceCriteria("Iif(IsNullOrEmpty('@This.Immobile.Commesse'),Oid > 0,Immobile.Commesse.CommessePrioritas.Single(Priorita))")]
        //[DataSourceCriteria("[<CommessePriorita>][^.Oid == Priorita.Oid And Commesse == '@This.Immobile.Commesse']")]
     //   [RuleRequiredField("RuleRequiredField.SegnalazioneCC.Priorita", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
     ///   [Appearance("SegnalazioneCC.Priorita.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Priorita)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
     ///   [Appearance("SegnalazioneCC.Abilita.Priorita", FontColor = "Black", Enabled = false, Criteria = "Oid > 0")]
        //[ExplicitLoading()]
        [Delayed(true)]
        public Urgenza Priorita
        {
            get { return GetDelayedPropertyValue<Urgenza>("Priorita"); }
            set { SetDelayedPropertyValue<Urgenza>("Priorita", value); }

        }



        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Intervento")]
      ///  [RuleRequiredField("SegnalazioneCC.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
      //  [Appearance("SegnalazioneCC.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
        //[Delayed(true)]
        public string Descrizione
        {
            get
            {
                return fDescrizione;
            }
            set
            {
                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
            }
        }


        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //  [ToolTip("Data di Inserimento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]   In(1,2,11))
      //  [Appearance("SegnalazioneCC.DataRichiesta.Enabled", @"Oid > 0 And UtenteCreatoRichiesta != CurrentUserId() And !([UltimoStatoSmistamento.Oid] In(1))", FontColor = "Black", Enabled = false)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        //[Delayed(true)]
        public DateTime DataRichiesta
        {
            get
            {
                return fDataRichiesta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataRichiesta", ref fDataRichiesta, value);
            }
        }


        private RdL fRdL;
        [Persistent("RDL"),        DisplayName("RdL")]
        [ExplicitLoading()]
        public RdL RdL
        {
            get
            {
                return fRdL;
            }
            set
            {
                SetPropertyValue<RdL>("RdL", ref fRdL, value);
            }
        }

        private FileDataMail fFilDataMail;
        [Persistent("FILEEMAIL"), DisplayName("File eMail")]
        public FileDataMail FilDataMail
        {
            get { return fFilDataMail; }
            set { SetPropertyValue<FileDataMail>("FilDataMail", ref fFilDataMail, value); }
        }



        //protected override void OnChanged(string propertyName, object oldValue, object newValue)
        //{
        //    base.OnChanged(propertyName, oldValue, newValue);
        //    if (!this.IsLoading)
        //    {
        //        if (this.Oid == -1)
        //        {
        //            if (newValue != null && propertyName == "Categoria")
        //            {
        //                int newOid = ((DevExpress.Xpo.XPObject)(newValue)).Oid;
        //                //if (newOid != 4)
        //                //{
        //                //    this.TipoIntervento = Session.GetObjectByKey<TipoIntervento>(0);
        //                //}
        //            }
        //            //
        //            if (newValue != null && propertyName == "Immobile")
        //            {
        //                Immobile newEdificio = ((Immobile)(newValue));
        //                if (newValue != oldValue && newValue != null)
        //                {
        //                    //this.Richiedente = (Richiedente)Session.Query<Richiedente>().Where(w => w.Commesse == newEdificio.Commesse).FirstOrDefault();
        //                    ////this.Session.GetObjects<Richiedente>().Where(w => w.Commesse == newEdificio.Commesse).FirstOrDefault();
        //                    //CAMS.Module.Classi.SetVarSessione.OidEdificioCalcoloDistanze = newEdificio.Oid;
        //                    //SetVarSessione.OidEdificioCalcoloDistanzeLatitudine = double.Parse(newEdificio.Indirizzo.Latitude.ToString());
        //                    //SetVarSessione.OidEdificioCalcoloDistanzeLongitudine = double.Parse(newEdificio.Indirizzo.Longitude.ToString());
        //                }
        //            }

        //            if (newValue != null && propertyName == "Apparato")
        //            {
        //                Apparato newApparato = (Apparato)(newValue);
        //                if (newValue != oldValue || newValue != null)
        //                {
        //                    //this.Problema = null;
        //                    //this.ProblemaCausa = null;
        //                }
        //            }
        //        }

        //        if (this.Oid > 1)
        //        {
        //            if (newValue != null && propertyName == "UltimoStatoSmistamento")
        //            {
        //                if (newValue != oldValue && newValue != null)
        //                {

        //                }
        //            }
        //        }

        //    }
        //    //Richiedente

        //}


        protected override void OnSaved()
        {
            base.OnSaved();

        }

        public override string ToString()
        {
           return this.Descrizione;
        }
    }
}
