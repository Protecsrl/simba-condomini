using System;
using DevExpress.Xpo;
using System.Collections.Generic;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;


using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using CAMS.Module.Classi;
using CAMS.Module.DBControlliNormativi;
using DevExpress.ExpressApp.Filtering;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions,
    Persistent("FREQUENZE"),
    System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Frequenze")]
     [SearchClassOptions(SearchMemberMode.Include)]
    [RuleCombinationOfPropertiesIsUnique("UniqueFrequenze", DefaultContexts.Save, "CodDescrizione, Descrizione")]
    [Appearance("Frequenze.FrequenzeStagionale.Visible", TargetItems = "FrequenzaStagionales", Criteria = @"TipoCadenze != 'Stagionale'", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Frequenze.ControlliPeriodici.Visible", TargetItems = "ControlliNormativis", Criteria = @"ControlliNormativis.Count = 0 Or Oid = -1", Visibility = ViewItemVisibility.Hide)]
    [Appearance("Frequenze.ControlliPeriodici.Visible1", TargetItems = "ControlliNormativis", Criteria = @"1=1", Context = "Frequenze_DetailView",Priority=1, Visibility = ViewItemVisibility.Hide)]   
    [NavigationItem("Procedure Attivita")]
    [ImageName("Frequenze")]
    public class Frequenze : XPObject
    {
        public Frequenze()
            : base()
        {
        }

        public Frequenze(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(150),
        DisplayName("Descrizione")]
        [DbType("varchar(150)")]
        [RuleUniqueValue("UniqDescrizioneFrequenza", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, CustomMessageTemplate = "Questo Campo deve essere Univoco")
        ,
        ToolTip("Descrizione della Frequenza")]
        [RuleRequiredField("RReqField.Frequenze.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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

        private string fProvenienza;
        [Persistent("PROVENIENZA"),
        Size(10),
        DisplayName("Provenienza")]
        [DbType("varchar(10)")]
        [RuleRequiredField("RReqField.Frequenze.Provenienza", DefaultContexts.Save, "La Provenienza è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Provenienza
        {
            get
            {
                return fProvenienza;
            }
            set
            {
                SetPropertyValue<string>("Provenienza", ref fProvenienza, value);
            }
        }

        private TipoCadenze fTipoCadenze;
        [Persistent("TIPOCADENZE"),
        DisplayName("Tipo Cadenza"),
        ToolTip("il Tipo Cadenza è legato alle cadenze indicate nel campo Cadenze")]
        [RuleRequiredField("RReqField.Freq.TipoCadenze", DefaultContexts.Save, "il Tipo Cadenza è un campo obbligatorio")]
        public TipoCadenze TipoCadenze
        {
            get
            {
                
                return fTipoCadenze;
            }
            set
            {
                SetPropertyValue<TipoCadenze>("TipoCadenze", ref fTipoCadenze, value);
            }
        }

        private double fRicorrenzeCadenza;
        [Persistent("RICORRENZETCADENZE"),
        DisplayName("Ricorrenze"),
        ToolTip("Ricorrenze delle Cadenza")]
        [RuleRequiredField("RReqField.Freq.RicorrenzeCadenza", DefaultContexts.Save, "Le Ricorrenze delle Cadenza è un campo obbligatorio")]
        public double RicorrenzeCadenza
        {
            get
            {
                return fRicorrenzeCadenza;
            }
            set
            {
                SetPropertyValue<double>("RicorrenzeCadenza", ref fRicorrenzeCadenza, value);
            }
        }

        private double fCadenzeAnno;
        [Persistent("CADENZE_ANNO"),
        DisplayName("Cadenze Anno"),
        ToolTip("Cadenze Rapportate all Anno")]
        [RuleRequiredField("RReqField.Freq.CadenzeAnno", DefaultContexts.Save, "La Cadenza nell Anno è un campo obbligatorio")]
        public double CadenzeAnno
        {
            get
            {
                return fCadenzeAnno;
            }
            set
            {
                SetPropertyValue<double>("CadenzeAnno", ref fCadenzeAnno, value);
            }
        }
        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"),
        Size(10),
        DisplayName("Cod Descrizione")]
        [DbType("varchar(10)")]
        [RuleUniqueValue("UniqCodDescrizione", DefaultContexts.Save, CriteriaEvaluationBehavior = CriteriaEvaluationBehavior.BeforeTransaction, CustomMessageTemplate = "Questo Campo deve essere Univoco")
        ,
        ToolTip("Descrizione Breve della Frequenza")]
        [RuleRequiredField("RReqField.Frequenze.CodDescrizione", DefaultContexts.Save, "Il Codice Descrizione è un campo obbligatorio")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private TraslazioneSchedulazione fTraslazioneSchedulazione;
        [Persistent("TRASLASCHEDULA"),
        DisplayName("Traslazione Schedulazione")]
        [ToolTip("Descrizione della Traslazione settimanale nella Schedulazione, per la ottimizzazione della distribuzione dei carichi")]
        [RuleRequiredField("RReqField.Frequenze.TSchedulazione", DefaultContexts.Save, "La Traslazione Schedulazione è un campo obbligatorio")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public TraslazioneSchedulazione TraslazioneSchedulazione
        {
            get
            {
                return fTraslazioneSchedulazione;
            }
            set
            {
                SetPropertyValue<TraslazioneSchedulazione>("TraslazioneSchedulazione", ref fTraslazioneSchedulazione, value);
            }
        }

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

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
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
         
        [Association(@"ControlliNormativi_Frequenza", typeof(ControlliNormativi)),
        DisplayName("Controlli Normativi")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ControlliNormativi> ControlliNormativis
        {
            get
            {
                return GetCollection<ControlliNormativi>("ControlliNormativis");
            }
        }


        [Association(@"FrequenzaStagionale_Frequenza", typeof(FrequenzaStagionale)), Aggregated, DisplayName("Frequenza Stagionale")]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<FrequenzaStagionale> FrequenzaStagionales
        {
            get
            {
                return GetCollection<FrequenzaStagionale>("FrequenzaStagionales");
            }
        }

    }
}
