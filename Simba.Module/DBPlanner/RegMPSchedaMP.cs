using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.Drawing;
using CAMS.Module.DBPlanner;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using CAMS.Module.DBALibrary;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using DevExpress.Persistent.Validation;
using CAMS.Module.DBTask;
using CAMS.Module.DBPlant;

//namespace CAMS.Module.DBPlanner
//{
//    class RegMPSchedaMP
//    {
//    }
//}

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("REGMPSCHEDAMP")]
    [RuleCombinationOfPropertiesIsUnique("Unique.RegPianificazione_MPSchedaMP", DefaultContexts.Save, "RegistroPianificazioneMP, SchedaMp")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Schede MP in Registro MP")]


    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMPSchedaMP.SoloAttivo", "Abilitato == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMPSchedaMP.SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegMPSchedaMP.Tutto", "", "Tutto", Index = 2)]

    #endregion
    //[Appearance("Apparato.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
    //FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]


    [ImageName("ModelEditor_Categorized")]
    [NavigationItem("Planner")]

    public class RegMPSchedaMP : XPObject
    {
        public RegMPSchedaMP() : base() { }
        public RegMPSchedaMP(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if(Oid == -1)
            Abilitato = FlgAbilitato.Si;         
        }


        private RegPianificazioneMP fRegistroPianificazioneMP;
     //[MemberDesignTimeVisibility(false)]
        [Association(@"RegPianiMP_RegMPSchedaMP", typeof(RegPianificazioneMP))]
        [Persistent("MPREGPIANIFICAZIONE")]   
        [DisplayName("Registro Pianificazione MP")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public RegPianificazioneMP RegistroPianificazioneMP
        {
            get
            {
                return fRegistroPianificazioneMP;
            }
            set
            {
                SetPropertyValue<RegPianificazioneMP>("RegistroPianificazioneMP", ref fRegistroPianificazioneMP, value);
            }
        }  
         

        private SchedaMp fSchedaMp;
        [Persistent("SCHEDAMP"),       DisplayName("Scheda MP")]
        [DataSourceCriteria("StdApparato = '@This.Apparato.StdApparato'")]
        [ImmediatePostData]
        [ExplicitLoading()]
        public SchedaMp SchedaMp
        {
            get
            {
                return fSchedaMp;
            }
            set
            {
                SetPropertyValue<SchedaMp>("SchedaMp", ref fSchedaMp, value);
            }
        }

        private string fCodSchedaMp; 
        [Persistent("CODSCHEDEMP"), Size(30), DisplayName("Cod. Attività")]
        [DbType("varchar(30)")]
        [ImmediatePostData]
         [ToolTip("Codice Descrizione della Scheda di Manutenzione Programmata")]
        //[Appearance("ApparatoSchedaMP.fCodSchedaMp", Criteria = "not(CodSchedaMp is null)", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        //[RuleRequiredField("RuleReq.ApparatoSchedaMP.CodUni", DefaultContexts.Save, "Codice Uni è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
       
           

        private StdApparatoClassi fStdApparatoClassi;
        [Persistent("APPARATOSTDCLASSI"),
        DisplayName("Classi Tipo Apparato")]
        //[RuleRequiredField("RReqField.ApparatoSchedaMP.StdApparatoClassi", DefaultContexts.Save, "StdApparatoClassi è un campo obbligatorio")]
        //[Appearance("ApparatoSchedaMP.Abilita.StdApparatoClassi", Criteria = "(Sistema is null) OR (not (StdApparato is null))", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Sistema = '@This.Sistema'")]
        [ExplicitLoading()]
        public StdApparatoClassi StdApparatoClassi
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
        //[RuleRequiredField("RReqField.ApparatoSchedaMp.Categoria", DefaultContexts.Save, "Categoria è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [Persistent("APPARATOSTD"),   DisplayName("Tipo Apparato")]
        //[RuleRequiredField("RReqFieldObject.ApparatoSchedaMp.StdApparato", DefaultContexts.Save, "La Tipologia di Apparato è un campo obligatorio")]
        [ToolTip("Valorizzando il tipo di Apparecchiatura saranno inseriti i valori di Default ai K Correttivi")]
        //[Appearance("ApparatoSchedaMp.Abilita.StdApparato", Criteria = "(StdApparatoClassi is null)", Enabled = false)]
        [DataSourceCriteria("StdApparatoClassi = '@This.StdApparatoClassi'")]
        [ExplicitLoading()]
        public StdAsset StdApparato
        {
            get
            {
                return fStdApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value);
            }
        }

        private string fSottoComponente;
        [Persistent("SOTTOCOMPONENTE"),
        Size(100),
        DisplayName("SottoComponente")]
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
        [Persistent("DESCRIZIONEMANUTENZIONE"), Size(4000), DisplayName("Descrizione")]
        [DbType("varchar(4000)")]
        //[RuleRequiredField("RReqFieldObject.ApparatoSchedaMp.Manutenzione", DefaultContexts.Save, "La Descrizione Manutenzione è un campo obbligatorio")]
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

        private TipoCogenzaNormativa fCogenzaNormativa;
        [Persistent("NORMACOGENTE"), DisplayName("Norma Cogente")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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

        #region PARAMETRI SCHEDULAZIONE
        private Frequenze fFrequenzaOpt;
        [Persistent("FREQUENZAOPT"),
        DisplayName("Frequenza")]
        //[RuleRequiredField("RReqFieldObject.ApparatoSchedaMp.FrequenzaOpt", DefaultContexts.Save, "La Frequenza è un campo obbligatorio")]
        [ExplicitLoading()]
        public Frequenze FrequenzaOpt
        {
            get
            {
                return fFrequenzaOpt;
            }
            set
            {
                SetPropertyValue<Frequenze>("Frequenze", ref fFrequenzaOpt, value);
            }
        }

        private Mansioni fMansioniOpt;
        [Persistent("MANSIONIOPT"),
        DisplayName("Mansione")]
        public Mansioni MansioniOpt
        {
            get
            {
                return fMansioniOpt;
            }
            set
            {
                SetPropertyValue<Mansioni>("Mansioni", ref fMansioniOpt, value);
            }
        }

        private int fTempoOpt = 0;
        [Persistent("TEMPOOPT"),
        DisplayName("Tempo [min.]")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public int TempoOpt
        {
            get
            {
                return fTempoOpt;
            }
            set
            {
                SetPropertyValue<int>("TempoOpt", ref fTempoOpt, value);
            }
        }

        #region Dati a supporto del Polo Energia
        //private RiferimentiNormativi fRiferimentiNormativi;
        //[Persistent("RIFERIMENTINORMATIVI"),
        //DisplayName("Riferimenti Normativi")]
        //public RiferimentiNormativi RiferimentiNormativi
        //{
        //    get
        //    {
        //        return fRiferimentiNormativi;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RiferimentiNormativi>("RiferimentiNormativi", ref fRiferimentiNormativi, value);
        //    }
        //} 

        //private Insourcing fInsourcing;
        //[Persistent("INSOURCING"), DisplayName("Insourcing")]
        //public Insourcing Insourcing
        //{
        //    get
        //    {
        //        return fInsourcing;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Insourcing>("Insourcing", ref fInsourcing, value);
        //    }
        //}

        private TipologiaIntervento fTipologiaIntervento;
        [Persistent("TIPOLOGIAINTERVENTO"), DisplayName("Tipologia Intervento")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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


        //[Persistent("MANUALEAPPARATO"), DisplayName("Manuale Uso e Manutenzione dell'Apparato")]
        //[FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        //[FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        //[Delayed(true)]
        //public FileData ManualeApparato
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("ManualeApparato");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("ManualeApparato", value);
        //    }
        //}
        #endregion

        

        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"),
        DisplayName("Attivo")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }


  


        private TipoNumeroManutentori fNumMan;// = TipoNumeroManutentori.unaPersona;
        [Persistent("NUMMAN"), DisplayName("N° Addetto")]
        [ImmediatePostData]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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


 
        #endregion
           
          

        private bool fSuddividiAtt;
        [Persistent("SUDDIVIDIATT"), DisplayName("Suddividi Attività")]
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

  

        #region data aggiornamento + utente
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
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

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
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

        #region proprieta di default


        public override string ToString()
        {
            return string.Format("{0}({1}) - {2}", CodSchedaMp, CodUni, DescrizioneManutenzione);
        }
        #endregion
    }
}
