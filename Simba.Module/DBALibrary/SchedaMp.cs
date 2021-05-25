using System;
using CAMS.Module.Classi;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Diagnostics;
using DevExpress.Persistent.Validation;
//using System.ComponentModel;


namespace CAMS.Module.DBALibrary
{///  NON VISUALIZZA CAMPO NASCOSTI
    [DefaultClassOptions, Persistent("SCHEDEMP")] //  System.ComponentModel.DefaultProperty("CodPmp")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", @"Procedure Attività")]
    [ImageName("BO_List")]
    [NavigationItem("Procedure Attivita")]
    //[System.ComponentModel.DefaultProperty("FullName")]
    [Appearance("schedemp_evidenza_cogenza_normativa", TargetItems = "*", Criteria = "CogenzaNormativa == 'Si'", FontColor = "Red")]
  
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "All Data", true, Index = 0)]
   [DevExpress.ExpressApp.SystemModule.ListViewFilter("skmpConduzione", "TipologiaIntervento.Descrizione = 'Conduzione'", "Conduzione",  Index = 1)]
   [DevExpress.ExpressApp.SystemModule.ListViewFilter("skmpCiclica", "TipologiaIntervento.Descrizione = 'Ciclica'", "Ciclica", Index = 2)]
   [DevExpress.ExpressApp.SystemModule.ListViewFilter("skmpa_Condizione", "TipologiaIntervento.Descrizione = 'a Condizione'", "a Condizione", Index = 3)]
   [DevExpress.ExpressApp.SystemModule.ListViewFilter("skmpa_Contaore", "TipologiaIntervento.Descrizione = 'a Contaore'", "a Contaore", Index = 4)]

    public class SchedaMp : XPObject
    {
        public SchedaMp()            : base()        {        }
        public SchedaMp(Session session)    : base(session) {   }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //this.StdApparato
            if(this.Oid ==-1)
            {
                this.SuddividiAtt = false;
                this.CogenzaNormativa = TipoCogenzaNormativa.No;
            }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fCodSchedaMp; ///  String.Format("{0}{1}", this.Categoria, this.Sistema
        [Persistent("CODSCHEDEMP"),     Size(30),     DisplayName("Cod.Attività")]
        [DbType("varchar(30)")]
        [ImmediatePostData]
        [RuleUniqueValue("UniqCodPMP", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction,
            CustomMessageTemplate = "Questo Campo deve essere Univoco") ,
        ToolTip("Codice Descrizione della Scheda di Manutenzione Programmata")]
        [Appearance("SchedaMp.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        public string CodSchedaMp
        {
            get
            {
                return fCodSchedaMp;
            }
            set
            {
                SetPropertyValue<string>("CodSchedaMp", ref fCodSchedaMp, value);
            }
        }

        private string fCodUni;
        [Size(20),
        Persistent("COD_UNI")]
        [DbType("varchar(20)")]
        [RuleRequiredField("RuleReq.SchedaMp.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]

        public string CodUni
        {
            get
            {
                return fCodUni;
            }
            set
            {
                SetPropertyValue<string>("CodUni", ref fCodUni, value);
            }
        }

        private SchedeMPOwner fSchedeMPOwner;
        [Persistent("SCHEDEMPOWNER"),    DisplayName("Gestore")]
        [ExplicitLoading()]
        public SchedeMPOwner SchedeMPOwner
        {
            get
            {
                return fSchedeMPOwner;
            }
            set
            {
                SetPropertyValue<SchedeMPOwner>("SchedeMPOwner", ref fSchedeMPOwner, value);
            }
        }

        private SistemaTecnologico fSistemaTecnologico;
        [Persistent("SISTEMATECNOLOGICO"),DisplayName("Sistema Tecnologico")]
        [RuleRequiredField("RReqField.SchedaMp.SistemaTecnologico", DefaultContexts.Save, "Sistema Tecnologico è un campo obbligatorio")]
        [Appearance("SchedaMp.Abilita.SistemaTecnologico", Criteria = "not (SistemaClassi  is null)", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public SistemaTecnologico SistemaTecnologico
        {
            get
            {
                return fSistemaTecnologico;
            }
            set
            {
                SetPropertyValue<SistemaTecnologico>("SistemaTecnologico", ref fSistemaTecnologico, value);
            }
        }

        private SistemaClassi fSistemaClassi;
        [Persistent("SISTEMACLASSI"),    DisplayName(@"Classi Unità Tecnologica")]
        [RuleRequiredField("RReqField.SchedaMp.SistemaClassi", DefaultContexts.Save, @"Sistema Classi è un campo obbligatorio")]
        [Appearance("SchedaMp.Abilita.SistemaClassi", Criteria = "(SistemaTecnologico  is null) OR (not (SistemaClassi is null)) ", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("SistemaTecnologico = '@This.SistemaTecnologico'")]
        [ExplicitLoading()]
        public SistemaClassi SistemaClassi
        {
            get
            {
                return fSistemaClassi;
            }
            set
            {
                SetPropertyValue<SistemaClassi>("SistemaClassi", ref fSistemaClassi, value);
            }
        }

        private Sistema fSistema;
        [Persistent("SISTEMA"), DisplayName(@"Unità Tecnologica")]
        [RuleRequiredField("RReqField.SchedaMp.Sistema", DefaultContexts.Save, @"Sistema è un campo obbligatorio")]
        [Appearance("SchedaMp.Abilita.Sistema", Criteria = "(SistemaClassi  is null) OR (not (StdApparatoClassi is null))", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("SistemaClassi = '@This.SistemaClassi'")]
        [ExplicitLoading()]
        public Sistema Sistema
        {
            get
            {
                return fSistema;
            }
            set
            {
                SetPropertyValue<Sistema>("Sistema", ref fSistema, value);
            }
        }

        private StdApparatoClassi fStdApparatoClassi;
        [Persistent("APPARATOSTDCLASSI"),
        DisplayName("Classi Tipo Apparato")]
        [RuleRequiredField("RReqField.SchedaMp.StdApparatoClassi", DefaultContexts.Save, "StdApparatoClassi è un campo obbligatorio")]
        [Appearance("SchedaMp.Abilita.StdApparatoClassi", Criteria = "(Sistema is null) OR (not (StdApparato is null))", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Sistema = '@This.Sistema'")]
        [ExplicitLoading()]
        public StdApparatoClassi StdAssetClassi
        {
            get
            {
                return fStdApparatoClassi;
            }
            set
            {
                SetPropertyValue<StdApparatoClassi>("StdApparatoClassi", ref fStdApparatoClassi, value);
            }
        }

        private Categoria fCategoria;
        [Persistent("CATEGORIA"),
        DisplayName("Categoria Servizio")]
        [RuleRequiredField("RReqField.SchedaMp.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        [ExplicitLoading()]
        public Categoria Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<Categoria>("Categoria", ref fCategoria, value);
            }
        }

        private StdAsset fStdApparato;
        [Association(@"StdApparato_SchedeMP"), Persistent("APPARATOSTD"), DisplayName("Tipo Apparato")]
        [ImmediatePostData]
        [RuleRequiredField("RReqFieldObject.SchedaMp.StdApparato", DefaultContexts.Save, "La Tipologia di Apparato è un campo obligatorio")]
        [ToolTip("Valorizzando il tipo di Apparecchiatura saranno inseriti i valori di Default ai K Correttivi")]
        [Appearance("SchedaMp.Abilita.StdApparato", Criteria = "(StdApparatoClassi is null)", Enabled = false)]
        [DataSourceCriteria("StdApparatoClassi = '@This.StdApparatoClassi'")]
        [ExplicitLoading()]
        public StdAsset StdAsset
        {
            get
            {
                return fStdApparato;
            }
            set
            {
                if( SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value) )
                 {
                //    if (RuoloMio == "RuoloSTFInsert" || RuoloMio == "RuoloGdLInsert")
                //    {
                //        object TempFreq = value;
                //        if (TempFreq != null && Oid == -1)
                //        {
                //            if (!IsSaving)
                //            {
                //                SetKCorrettivi(value);
                //            }
                //        }
                //    }
                     OnChanged("TempoTotAnno");
                 }
            }
        }

        private string fSottoComponente;
        [Persistent("SOTTOCOMPONENTE"), Size(100), DisplayName("Sotto Componente")]
        [DbType("varchar(100)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string SottoComponente
        {
            get
            {
                return fSottoComponente;
            }
            set
            {
                SetPropertyValue<string>("SottoComponente", ref fSottoComponente, value);
            }
        }

        private string fDescrizioneManutenzione;
        [Persistent("DESCRIZIONEMANUTENZIONE"),
        Size(4000),
        DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        [RuleRequiredField("RReqFieldObject.SchedaMp.fDescrizioneManutenzione", DefaultContexts.Save, "La Descrizione Manutenzione è un campo obbligatorio")]
        public string DescrizioneManutenzione
        {
            get
            {
                return fDescrizioneManutenzione;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneManutenzione", ref fDescrizioneManutenzione, value);
            }
        }


        private Frequenze fFrequenzaOpt;
        [Persistent("FREQUENZAOPT"),
        DisplayName("Frequenza")]
        [ImmediatePostData]
        [RuleRequiredField("RReqFieldObject.SchedaMp.FrequenzaOpt", DefaultContexts.Save, "La Frequenza è un campo obbligatorio")]
        [ExplicitLoading()]
        public Frequenze FrequenzaOpt
        {
            get
            {
                return fFrequenzaOpt;
            }
            set
            {
                  // SetPropertyValue<Frequenze>("FrequenzaOpt", ref fFrequenzaOpt, value);
                 if (SetPropertyValue<Frequenze>("FrequenzaOpt", ref fFrequenzaOpt, value))
                 {
                //    if (RuoloMio == "RuoloGdLInsert")
                //    {
                //        object TempFrq = value;
                //        if (TempFrq != null && Oid == -1)
                //        {
                //            if (!IsSaving)
                //            {
                //                SetPropertyValue<Frequenze>("FrequenzaBase", ref fFrequenzaBase, value);
                //            }
                //        }
                //    }
                     OnChanged("TempoTotAnno");
                 }
            }
        }

        private Mansioni fMansioniOpt;
        [Persistent("MANSIONIOPT"),
        DisplayName("Mansione")]
        [ImmediatePostData]
        [ExplicitLoading()]
        public Mansioni MansioniOpt
        {
            get
            {
                return fMansioniOpt;
            }
            set
            {
                SetPropertyValue<Mansioni>("MansioniOpt", ref fMansioniOpt, value);
                //if (SetPropertyValue<Mansioni>("MansioniOpt", ref fMansioniOpt, value))
                //{
                //    if (RuoloMio == "RuoloGdLInsert")
                //    {
                //        object TempFrq = value;
                //        if (TempFrq != null && Oid == -1)
                //        {
                //            if (!IsSaving)
                //            {
                //                SetPropertyValue<Skill>("SkillBase", ref fSkillBase, value.Skill);
                //            }
                //        }
                //    }
                //}
            }
        }


        private int fTempoOpt = 0;
        [Persistent("TEMPOOPT"),
        DisplayName("Tempo [min.]")]
        [ImmediatePostData]
        public int TempoOpt
        {
            get
            {
                return fTempoOpt;
            }
            set
            {
                if (SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value))  //  SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value);
                {
                //    if (RuoloMio == "RuoloGdLInsert")
                //    {
                //        if (value != null && Oid == -1)
                //        {
                //            if (!IsSaving)
                //            {
                //                SetPropertyValue<int>("TempoBase", ref fTempoBase, value);
                //            }
                //        }
                //    }
                //    OnChanged("TempoTotOPT");
                      OnChanged("TempoTotAnno");
                 }
            }
        }

        #region Dati a supporto del Polo Energia

        private RiferimentiNormativi fRiferimentiNormativi;
        [Persistent("RIFERIMENTINORMATIVI"),
        DisplayName("Riferimenti Normativi")]
        [ExplicitLoading()]
        public RiferimentiNormativi RiferimentiNormativi
        {
            get
            {
                return fRiferimentiNormativi;
            }
            set
            {
                SetPropertyValue<RiferimentiNormativi>("RiferimentiNormativi", ref fRiferimentiNormativi, value);
            }
        }
        
        //private string fFrequenzaContatore;
        //[Persistent("FREQUENZAACONTATORE"), Size(100), DevExpress.Xpo.DisplayName("Frequenza a Contatore"), MemberDesignTimeVisibility(false)]
        //[DbType("varchar(100)")]
        //public string FrequenzaContatore
        //{
        //    get
        //    {
        //        return fFrequenzaContatore;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("FrequenzaContatore", ref fFrequenzaContatore, value);
        //    }
        //}

        private Insourcing fInSourcing;
        [Persistent("INSOURCING"), DisplayName("Insourcing")]
        [ExplicitLoading()]
        public Insourcing InSourcing
        {
            get
            {
                return fInSourcing;
            }
            set
            {
                SetPropertyValue<Insourcing>("InSourcing", ref fInSourcing, value);
            }
        }

        private TipologiaIntervento fTipologiaIntervento;
        [Persistent("TIPOLOGIAINTERVENTO"), DisplayName("Tipologia Intervento")]
        [ExplicitLoading()]
        public TipologiaIntervento TipologiaIntervento
        {
            get
            {
                return fTipologiaIntervento;
            }
            set
            {
                SetPropertyValue<TipologiaIntervento>("TipologiaIntervento", ref fTipologiaIntervento, value);
            }
        }

        private StatoComponente fStatoComponente;
        [Persistent("STATOCOMPONENTE"), DisplayName("Stato Componente")]
        [ExplicitLoading()]
        public StatoComponente StatoComponente
        {
            get
            {
                return fStatoComponente;
            }
            set
            {
                SetPropertyValue<StatoComponente>("StatoComponente", ref fStatoComponente, value);
            }
        }

        [Persistent("MANUALEAPPARATO"), DisplayName("Manuale Uso e Manutenzione")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData ManualeAsset
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("ManualeApparato");
               // return fManualeApparato;
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("ManualeApparato", value);
                //SetPropertyValue<FileData>("ManualeApparato", ref fManualeApparato, value);
            }
        }
        #endregion
        #region associazioni
        [Association(@"SchedeMp_SchedeMpPassi", typeof(SchedaMpPassi)),Aggregated, DisplayName("Passi")] 
        public XPCollection<SchedaMpPassi> SchedaMpPassis { get { return GetCollection<SchedaMpPassi>("SchedaMpPassis"); } }

        #endregion
        private TipoNumeroManutentori fNumMan; // = TipoNumeroManutentori.unaPersona;
        [Persistent("NUMMAN"), DisplayName("N° Addetto")]
        [ImmediatePostData]       // [RuleRange("RuleRangeObject.SchedaMp.NumMan", "Save", 1, 3)]
        public TipoNumeroManutentori NumMan
        {
            get
            {
                return fNumMan;
            }
            set
            {
                if (SetPropertyValue<TipoNumeroManutentori>("NumMan", ref fNumMan, value))
                {
                    OnChanged("TempoTotAnno");
                }
            }
        }


        private TipoCogenzaNormativa fCogenzaNormativa;  
        [Persistent("NORMACOGENTE"), DisplayName("Norma Cogente")]
        [ImmediatePostData]
        public TipoCogenzaNormativa CogenzaNormativa
        {
            get
            {
                return fCogenzaNormativa;
            }
            set
            {
                SetPropertyValue<TipoCogenzaNormativa>("CogenzaNormativa", ref fCogenzaNormativa, value);
              
            }
        }
        private bool fSuddividiAtt;
        [Persistent("SUDDIVIDIATT"), DisplayName(@"Suddividi Attività")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public bool SuddividiAtt
        {
            get
            {
                return fSuddividiAtt;
            }
            set
            {
                SetPropertyValue<bool>("SuddividiAtt", ref fSuddividiAtt, value);
            }
        }

        #region campi calcolati TEMPO Totale nell'anno e Coefficienti Correttivi Default
        /// Tempo di Manutenzione Base Effettivo per ANNO
        [PersistentAlias("TempoOpt * FrequenzaOpt.CadenzeAnno * StdApparato.KTotale"),
        DisplayName("Tot Tempo x Anno")]
        [Appearance("TempoSTFMaggiore.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "TempoTotAnno = 0")]
        [MemberDesignTimeVisibility(false)]
        public double TempoTotAnno
        {
            get
            {
                var tempObject = EvaluateAlias("TempoTotAnno");
                if (tempObject != null)
                {
                    var numManutentori = 1;
                    if (this.NumMan == Classi.TipoNumeroManutentori.duePersone) numManutentori = 2;
                    return (double)tempObject * numManutentori;
                }
                else
                {
                    return 0;
                }
            }
        }
        //DescrizioneKTotale
        //[PersistentAlias("StdApparato.DescrizioneKTotale"), DisplayName("Descrizione K Totale")]
        //[VisibleInListView(false)]
        //public string DescrizioneKTotale
        //{
        //    get
        //    {
        //        var tempObject = EvaluateAlias("DescrizioneKTotale");
        //        if (tempObject != null)
        //        {
        //            return (string)tempObject;//+ " Descrizione: " + TempoTotAnno
        //        }
        //        else
        //        {
        //            return "";
        //        }
        //    }
        //}

        #endregion


        private string fAgg_RegRdl;
        [Persistent("AGG_REGRDL"), Size(250), DisplayName("Agg RegRdl")]
        [DbType("varchar(250)")]
        [ImmediatePostData]
        [ToolTip("Descrizione della aggregazione da elaborare in creazione Registro di Lavoro")]
        //[Appearance("ApparatoSchedaMP.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Agg_RegRdl
        {
            get
            {
                return fAgg_RegRdl;
            }
            set
            {
                SetPropertyValue<string>("Agg_RegRdl", ref fAgg_RegRdl, value);
            }
        }

        private string fAgg_Rdl;
        [Persistent("AGG_RDL"), Size(250), DisplayName("Agg Rdl")]
        [DbType("varchar(250)")]
        [ImmediatePostData]
        [ToolTip("Descrizione della aggregazione da elaborare in creazione Richiesta di Lavoro")]
        //[Appearance("ApparatoSchedaMP.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public string Agg_Rdl
        {
            get
            {
                return fAgg_Rdl;
            }
            set
            {
                SetPropertyValue<string>("Agg_Rdl", ref fAgg_Rdl, value);
            }
        }




        #region Tempo e data Aggiornamento
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
       // [Appearance("SchedaMp.Utente", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
       // [Appearance("SchedaMp.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        #endregion


        #region Metodi
        //private string fRuoloMio;
        //[NonPersistent]
        //[Appearance("SchedeMP.RuoloMioVis", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Show)]
        //public string RuoloMio
        //{
        //    get
        //    {
        //        return fRuoloMio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("RuoloMio", ref fRuoloMio, value);
        //    }
        //}
        protected override void OnLoaded()
        {
            base.OnLoaded();
        }
        //protected override void OnSaving()
        //{
        //    base.OnSaving();

        //    Debug.WriteLine(String.Format("e salvato {0} is saving {1}", IsInvalidated, IsSaving));
        //    if (Session.IsNewObject(this))
        //    {
        //        SetCodicePMP();
        //    }
        //}
        //protected override void OnSaved()
        //{
        //    base.OnSaved();


        //}

        //public void SetCodicePMP()
        //{
        //    try
        //    {
        //        if ((Categoria != null) ||
        //            (StdApparato != null) ||   //(Eqstd != null)
        //             (Sistema != null))
        //        {
        //            var db = new Classi.DB(); // var CodicePMP = db.GetCodicePMP(Classi.SetVarSessione.CorrenteUser, Categoria.Oid, Eqstd.Oid, Sistema.Oid);
        //            var CodicePMP = db.GetCodicePMP(Classi.SetVarSessione.CorrenteUser, Categoria.Oid, StdApparato.Oid, Sistema.Oid);
        //            this.CodSchedaMp = CodicePMP;
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(string.Format("Errore: Connessione al DB non eseguita correttamente;" + ex.Message));
        //    }
        //}

        #endregion
        //      [PersistentAlias("CodSchedaMp +  ' (' + CodUni +')'"),
        //DisplayName("Nome")]
        //      public string FullName
        //      {
        //          get
        //          {

        //              //var tempObject = EvaluateAlias("FullName");
        //              //if (tempObject != null)
        //              //{
        //                  return (string)FullName.ToString();
        //              //}
        //              //else
        //              //{
        //              //    return string.Empty;
        //              //}
        //          }
        //      }

        //[PersistentAlias("Iif(CodSchedaMp is not null And CodUni is not null, CodSchedaMp + ' - ' + CodUni,CodSchedaMp)")]
        //[System.ComponentModel.DisplayName("FullName")]
        //[System.ComponentModel.Browsable(false)]
        //public string FullName
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("FullName");
        //        if (tempObject != null)
        //            return (string)tempObject;
        //        else
        //            return null;
        //    }
        //}



        public override string ToString()
        {
            return string.Format("Cod.{0}({1})", CodSchedaMp, CodUni);
        }

    }
}

#region escluse
        //private Frequenze fFrequenzaBase;
        //[Persistent("FREQUENZA"),
        //DisplayName("Frequenza Base")]
        //[ImmediatePostData]
        //[MemberDesignTimeVisibility(false)]
        //public Frequenze FrequenzaBase
        //{
        //    get
        //    {
        //        return fFrequenzaBase;
        //    }
        //    set
        //    {
        //        if (SetPropertyValue<Frequenze>("FrequenzaBase", ref fFrequenzaBase, value))
        //        {
        //            if (RuoloMio == "RuoloSTFInsert")
        //            {
        //                object TempFreq = value;
        //                if (TempFreq != null && Oid == -1)
        //                {
        //                    if (!IsSaving)
        //                    {
        //                        SetPropertyValue<Frequenze>("FrequenzaOpt", ref fFrequenzaOpt, value);
        //                    }
        //                }
        //            }
        //            OnChanged("TempoTotBASE");
        //        }
        //    }
        //}


        //private Skill fSkillBase;
        //[Persistent("SKILLBASE"),
        //DisplayName("Skill Base")]
        //[ImmediatePostData]
        //[MemberDesignTimeVisibility(false)]
        //public Skill SkillBase
        //{
        //    get
        //    {
        //        return fSkillBase;
        //    }
        //    set
        //    {
        //        if (SetPropertyValue<Skill>("SkillBase", ref fSkillBase, value))
        //        {
        //            if (RuoloMio == "RuoloSTFInsert")
        //            {
        //                object TempSkill = value;
        //                if (TempSkill != null && Oid == -1)
        //                {
        //                    if (!IsSaving)
        //                    {
        //                        var db = new Classi.DB();
        //                        var OidMansione = db.GetMasionebySkillBase(  Classi.SetVarSessione.CorrenteUser, ((Skill)TempSkill).Oid);
        //                        var ma = Session.FindObject<Mansioni>(new BinaryOperator("Oid", OidMansione));
        //                        SetPropertyValue<Mansioni>("MansioniOpt", ref fMansioniOpt, ma);
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        //private int fTempoBase;
        //[Persistent("TEMPO"),
        //DisplayName("Tempo Base")]
        //[ImmediatePostData]
        //[RuleRange("RuleRangeObject.SchedaMp.Tempo", "Save", 0, 480)]
        //[MemberDesignTimeVisibility(false)]
        //public int TempoBase
        //{
        //    get
        //    {
        //        return fTempoBase;
        //    }
        //    set
        //    {
        //        if (SetPropertyValue<int>("TempoBase", ref fTempoBase, value))
        //        {
        //            if (RuoloMio == "RuoloSTFInsert")
        //            {
        //                object TempSkill = value;
        //                if (TempSkill != null && Oid == -1)
        //                {
        //                    if (!IsSaving)
        //                    {
        //                        SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value);
        //                    }
        //                }
        //                OnChanged("TempoTotBASE");
        //            }
        //        }
        //    }
//}

#endregion
//[PersistentAlias("(TempoOpt * NumMan) * (1 + ((KUtenza - 1) + (KGuasto - 1) + (KDimensione - 1) + (KCondizione - 1) + (KUbicazione - 1) + (KTrasferimento - 1)))")]
//[Appearance("TempoMTZMaggiorediOPT.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "TempoMTZ = 0")]
//[DisplayName("Tempo Corf.Cor.[min.]")]
//public double TempoMTZ
//{
//    get
//    {
//        var tempObject = EvaluateAlias("TempoMTZ");
//        if (tempObject != null )
//        {
//            return (double)tempObject;
//        }
//        else
//        {
//            return 0;
//        }
//    }
//}

///  tempo totale ottimizzato in base all'ANNO
//[PersistentAlias("(FrequenzaOpt.CadenzeAnno * TempoOpt * NumMan) * (1 + ((KUtenza - 1) + (KGuasto - 1) + (KDimensione - 1) + (KCondizione - 1) + (KUbicazione - 1) + (KTrasferimento - 1)))")]
//[Appearance("TempoTotOPTMaggiorediOPT.BackColor.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "TempoTotOPT = 0")]
//[DisplayName("Tempo x Anno[min.]")]
//public double TempoTotOPT
//{
//    get
//    {
//        var tempObject = EvaluateAlias("TempoTotOPT");
//        if (tempObject != null )
//        {
//            return (double)tempObject;
//        }
//        else
//        {
//            return 0;
//        }
//    }
//}
//private double fKDimensione = 1; ///  default per apparato  quindi prendi quello di default per tipo                                
//[Persistent("KDIMENSIONE"),
//DisplayName("KDimensione")]
//[RuleRange("RuleRangeObject.SchedaMp.KDimensione", "Save",   INT_ConstantMin, INT_ConstantMax)]
//public double KDimensione
//{
//    get
//    {
//        return fKDimensione;
//    }
//    set
//    {
//        SetPropertyValue<double>("KDimensione", ref fKDimensione, value);
//    }
//}

//private double fKGuasto = 1;
//[Persistent("KGUASTO"),
//DisplayName("KGuasto")]
//[ImmediatePostData]
//[RuleRange("RuleRangeObject.SchedaMp.KGuasto", "Save", INT_ConstantMin, INT_ConstantMax)]
//public double KGuasto
//{
//    get
//    {
//        return fKGuasto;
//    }
//    set
//    {
//        if (SetPropertyValue<double>("KGuasto", ref fKGuasto, value))
//        {
//            OnChanged("TempoTotOPT");
//            OnChanged("TempoMTZ");
//        }
//    }
//}

//private double fKCondizione = 1;
//[Persistent("KCONDIZIONE"),
//DisplayName("KCondizione")]
//[RuleRange("RuleRangeObject.SchedaMp.KCondizione", "Save", INT_ConstantMin, INT_ConstantMax)]
//public double KCondizione
//{
//    get
//    {
//        return fKCondizione;
//    }
//    set
//    {
//        SetPropertyValue<double>("KCondizione", ref fKCondizione, value);
//    }
//}

//private double fKUtenza = 1;
//[Persistent("KUTENZA"),
//DisplayName("KUtenza")]
//[RuleRange("RuleRangeObject.SchedaMp.KUtenza", "Save", INT_ConstantMin, INT_ConstantMax)]
//public double KUtenza
//{
//    get
//    {
//        return fKUtenza;
//    }
//    set
//    {
//        SetPropertyValue<double>("KUtenza", ref fKUtenza, value);
//    }
//}

//private double fKUbicazione = 1;
//[Persistent("KUBICAZIONE"),
//DisplayName("Kubicazione")]
//[RuleRange("RuleRangeObject.SchedaMp.KUbicazione", "Save", INT_ConstantMin, INT_ConstantMax)]
//public double KUbicazione
//{
//    get
//    {
//        return fKUbicazione;
//    }
//    set
//    {
//        SetPropertyValue<double>("KUbicazione", ref fKUbicazione, value);
//    }
//}

//private double fKTrasferimento = 1;
//[Persistent("KTRASFERIMENTO"),
//DisplayName("KTrasferimento")]
//[RuleRange("RuleRangeObject.SchedaMp.KTrasferimento", "Save", INT_ConstantMin, INT_ConstantMax)]
//[MemberDesignTimeVisibility(false)]
//public double KTrasferimento
//{
//    get
//    {
//        return fKTrasferimento;
//    }
//    set
//    {
//        SetPropertyValue<double>("KTrasferimento", ref fKTrasferimento, value);
//    }
//}

//private double fKOttimizzazione = 1;
//[Persistent("KOTTIMIZZAZIONE"), DisplayName("KOttimizzazione")]
//[RuleRange("RuleRangeObject.SchedaMp.KOttimizzazione", "Save", INT_ConstantMin, INT_ConstantMax)]
//public double KOttimizzazione
//{
//    get
//    {
//        return fKOttimizzazione;
//    }
//    set
//    {
//        SetPropertyValue<double>("KOttimizzazione", ref fKOttimizzazione, value);
//    }
//}