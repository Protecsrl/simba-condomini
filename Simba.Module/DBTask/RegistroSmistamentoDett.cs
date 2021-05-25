using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("REGSMISTAMENTODETT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Reg. Smistamento")]
    [ImageName("BO_StateMachine")]
    [NavigationItem(false)]
    public class RegistroSmistamentoDett : XPObject
    {
        public RegistroSmistamentoDett()
            : base()
        {
        }
        public RegistroSmistamentoDett(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegistroRdL fRegistroRdL;
        [Association(@"REGISTRORDLRefDETTAGLIOSMISTAMENTO"),
        Persistent("REGRDL"),
        DisplayName("Registro RdL")]
        [ExplicitLoading]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }


        private RisorseTeam fRisorseTeam;
        [Persistent("RISORSATEAM"), DisplayName("Team")]
        [ExplicitLoading]
        public RisorseTeam RisorseTeam
        {
            get
            {
                return fRisorseTeam;
            }
            set
            {
                SetPropertyValue<RisorseTeam>("RisorseTeam", ref fRisorseTeam, value);
            }
        }

        private StatoSmistamento fStatoSmistamento;
        [Association(@"REGISTROSMISTAMENTODETTRefSTATOSMISTAMENTO"),
        Persistent("STATOSMISTAMENTO"),
        DisplayName("Stato Smistamento")]
        [ExplicitLoading]
        public StatoSmistamento StatoSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("StatoSmistamento", ref fStatoSmistamento, value);
            }
        }

        private StatoSmistamento fSStatoSmistamento_Old;
        [Persistent("STATOSMISTAMENTO_OLD"),
        DisplayName("Stato Smistamento Precedente")]
        [ExplicitLoading]
        public StatoSmistamento SStatoSmistamento_Old
        {
            get
            {
                return fSStatoSmistamento_Old;
            }
            set
            {
                SetPropertyValue<StatoSmistamento>("SStatoSmistamento_Old", ref fSStatoSmistamento_Old, value);
            }
        }
        /// <summary>
        /// /
        /// </summary>
        private StatoOperativo fStatoOperativo;
        [Persistent("STATOOPERATIVO"),
        DisplayName("Stato Operativo")]
        [ExplicitLoading]
        public StatoOperativo StatoOperativo
        {
            get
            {
                return fStatoOperativo;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("StatoOperativo", ref fStatoOperativo, value);
            }
        }

        private StatoOperativo fStatoOperativo_Old;
        [Persistent("STATOOPERATIVO_OLD"),
        DisplayName("Stato Operativo Precedente")]
        [ExplicitLoading]
        public StatoOperativo StatoOperativo_Old
        {
            get
            {
                return fStatoOperativo_Old;
            }
            set
            {
                SetPropertyValue<StatoOperativo>("StatoOperativo_Old", ref fStatoOperativo_Old, value);
            }
        }
        /// <summary>
        /// /
        /// </summary>
        private DateTime fDataOra;
        [Persistent("DATAORA"),
        DisplayName("Data Ora"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataOra
        {
            get
            {
                return fDataOra;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra", ref fDataOra, value);
            }
        }
        private DateTime fDataOra_Old;
        [Persistent("DATAORA_OLD"),
        DisplayName("Data Ora Precedente"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        public DateTime DataOra_Old
        {
            get
            {
                return fDataOra_Old;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra_Old", ref fDataOra_Old, value);
            }
        }

        private int fDeltaTempoStato;
        [Persistent("DELTATEMPO"), DisplayName("Tempo di Stato"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        public int DeltaTempoStato
        {
            get
            {
                return fDeltaTempoStato;
            }
            set
            {
                SetPropertyValue<int>("DeltaTempoStato", ref fDeltaTempoStato, value);
            }
        }


        private sbyte fIcona;
        [ Persistent("ICONA"),
        DisplayName("Icona")]
        [MemberDesignTimeVisibility(false)]
        public sbyte Icona
        {
            get
            {
                return fIcona;
            }
            set
            {
                SetPropertyValue<sbyte>("Icona", ref fIcona, value);
            }
        }
        
        private string fDescrizione;
        [Size(500), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Variazione Stato Smistamento")]
        [DbType("varchar(500)")]
        [VisibleInListView(false)]
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

        private string fDescrizioneVariazioneRisorsaTeam;
        [Size(500), Persistent("DESCRIZIONETRISORSA"), System.ComponentModel.DisplayName("Descrizione Variazione RisorsaTeam")]
        //[RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(500)")]
        [VisibleInListView(false)]
        public string DescrizioneVariazioneRisorsaTeam
        {
            get
            {
                return fDescrizioneVariazioneRisorsaTeam;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneVariazioneRisorsaTeam", ref fDescrizioneVariazioneRisorsaTeam, value);
            }
        }

        private string fDescrizioneVariazioneStatoOperativo;
        [Size(1000), Persistent("DESCRIZIONESOPERATIVO"), System.ComponentModel.DisplayName("Descrizione Variazione Stato Operativo")]
        //[RuleRequiredField("RdL.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        //[Appearance("RdL.Descrizione.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Descrizione)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [DbType("varchar(1000)")]
        [VisibleInListView(false)]
        public string DescrizioneVariazioneStatoOperativo
        {
            get
            {
                return fDescrizioneVariazioneStatoOperativo;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneVariazioneStatoOperativo", ref fDescrizioneVariazioneStatoOperativo, value);
            }
        }

        #region associazioni

        [Association(@"RegistroSmistamentoDett_RegistroOperativoDettaglio", typeof(RegistroOperativoDettaglio)),
        DevExpress.ExpressApp.DC.XafDisplayName("Registro Operativo")]
        [Appearance("smistamento.RegistroOperativo.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or RegistroOperativoDettaglios.Count = 0", Visibility = ViewItemVisibility.Hide)]
        //[Appearance("smistamento.RegistroOperativo.Enabled", Enabled = false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroOperativoDettaglio> RegistroOperativoDettaglios
        {
            get
            {
                return GetCollection<RegistroOperativoDettaglio>("RegistroOperativoDettaglios");
            }
        }

        private string fUtenteSO;
        [Size(250), Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente SO")]
        [DbType("varchar(250)")]
        public string UtenteSO
        {
            get
            {
                return fUtenteSO;
            }
            set
            {
                SetPropertyValue<string>("UtenteSO", ref fUtenteSO, value);
            }
        }

        #endregion
        public override string ToString()
        {
            return string.Format("{0} - {1}", RegistroRdL.Descrizione, StatoSmistamento.SSmistamento.ToString());
        }
    }
}
