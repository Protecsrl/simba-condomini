using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBClienti
{
    [DefaultClassOptions, Persistent("CONTRATTICONSIPREG")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Contratti Consip")]
    [ImageName("PackageProduct")]
    [NavigationItem("Contratti")]

//    [RuleCriteria("RuleInfo.ContrattiConsipRegistro.SKMpNonAssegnate", DefaultContexts.Save, @"(Categoria.Oid In(3,5,2) And RdLApparatoSchedaMPs.Count() = 0)",
//CustomMessageTemplate = "Informazione: Richiesta di Lavoro di tipo ({Categoria}) Senza Selezione di Procdure di Attività di Manutenzione, sei sicuro di voler Salvare?.",
//SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Information)]

    public class ContrattiConsipRegistro : XPObject
    {
        public ContrattiConsipRegistro() : base() { }
        public ContrattiConsipRegistro(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE"), DisplayName("Descrizione")]
        [DbType("varchar(200)")]
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
        private TipoContrattoConsip fTipoContrattoConsip;
        [Persistent("CONTRATTICONSIPTIPO"), DisplayName("Tipo Contratto Consip")]
        [ExplicitLoading]
        public TipoContrattoConsip TipoContrattoConsip
        {
            get
            {
                return fTipoContrattoConsip;
            }
            set
            {
                SetPropertyValue<TipoContrattoConsip>("TipoContrattoConsip", ref fTipoContrattoConsip, value);
            }
        }

        private FasiContrattualiConsip fFasiContrattualiConsip;
        [Persistent("CONTRATTICONSIPFASE"), DisplayName("Fasi Contrattuali Consip")]
        [ExplicitLoading]
        public FasiContrattualiConsip FasiContrattualiConsip
        {
            get
            {
                return fFasiContrattualiConsip;
            }
            set
            {
                SetPropertyValue<FasiContrattualiConsip>("FasiContrattualiConsip", ref fFasiContrattualiConsip, value);
            }
        }

        private Contratti fCommesse;
        [Size(250), Persistent("COMMESSE"), DisplayName("Commessa")]
        [DbType("varchar(250)")]
        [ExplicitLoading]
        public Contratti Commesse
        {
            get
            {
                return fCommesse;
            }
            set
            {
                SetPropertyValue<Contratti>("Commesse", ref fCommesse, value);
            }
        }
        ///////////////////////////////////////////////////
        

        private string fProtRPF;
        [Persistent("PROTRPF"), DisplayName("N. Protocollo RPF")]
        [Size(250),DbType("varchar(250)")]
        public string ProtRPF
        {
            get { return fProtRPF; }
            set { SetPropertyValue<string>("ProtRPF", ref fProtRPF, value); }
        }

        private DateTime fDataRicezioneRPF;
        [Persistent("DATARICEZIONERPF"), DisplayName("Data Ricezione RPF")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Ricezione RPF", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataRicezioneRPF
        {
            get { return fDataRicezioneRPF; }
            set { SetPropertyValue<DateTime>("DataRicezioneRPF", ref fDataRicezioneRPF, value); }
        }

        private DateTime fDataComValiditaFormale;
        [Persistent("DATACOMFORMALE"), DisplayName("Data Comunicazione Validità Formale")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data comunicazione validità formale","RPF", DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataComValiditaFormale
        {
            get
            {
                return fDataComValiditaFormale;
            }
            set
            {
                SetPropertyValue<DateTime>("DataComValiditaFormale", ref fDataComValiditaFormale, value);
            }
        }


        private DateTime fDataPrimoSopralluogo;
        [Persistent("DATAPRIMOSOPRALLUOGO"), DisplayName("Data Primo Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data primo sopralluogo", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataPrimoSopralluogo
        {
            get { return fDataPrimoSopralluogo; }
            set { SetPropertyValue<DateTime>("DataPrimoSopralluogo", ref fDataPrimoSopralluogo, value); }
        }


        private DateTime fDataInvioVPV;
        [Persistent("DATAINVIOVPV"), DisplayName("Data Invio del Verbale")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataInvioVPV
        {
            get { return fDataInvioVPV; }
            set { SetPropertyValue<DateTime>("DataInvioVPV", ref fDataInvioVPV, value); }
        }

        private DateTime fDataControfirmaVPV;
        [Persistent("DATACONTROFIRMAVPV"), DisplayName("Data Controfirma del Verbale")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("data controfirma", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataControfirmaVPV
        {
            get { return fDataControfirmaVPV; }
            set { SetPropertyValue<DateTime>("DataControfirmaVPV", ref fDataControfirmaVPV, value); }
        }

        private DateTime fTermineTrasmPTE;
        [Persistent("TERMINETRASMPTE"), DisplayName("Termine ultimo trasmissione PTE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("termine ultimo trasmissione PTE", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime TermineTrasmPTE
        {
            get { return fTermineTrasmPTE; }
            set { SetPropertyValue<DateTime>("TermineTrasmPTE", ref fTermineTrasmPTE, value); }
        }

        private DateTime fDataTrasmPTE;
        [Persistent("DATATRASMPTE"), DisplayName("Data trasmissione PTE all'Amm.ne")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data trasmissione PTE all'Amm.ne", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataTrasmPTE
        {
            get { return fDataTrasmPTE; }
            set { SetPropertyValue<DateTime>("DataTrasmPTE", ref fDataTrasmPTE, value); }
        }

        private string fOsservazioniPA;
        [Persistent("OSSERVAZIONIPA"), DisplayName("Osservazioni della PA (SI/NO)")]
        [Size(250), DbType("varchar(250)")]
        public string OsservazioniPA
        {
            get { return fOsservazioniPA; }
            set { SetPropertyValue<string>("OsservazioniPA", ref fOsservazioniPA, value); }
        }

        private DateTime fDataApprovazionePTE;
        [Persistent("DATAAPPROVAZIONEPTE"), DisplayName("Data approvazione PTE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data approvazione PTE", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataApprovazionePTE
        {
            get { return fDataApprovazionePTE; }
            set { SetPropertyValue<DateTime>("DataApprovazionePTE", ref fDataApprovazionePTE, value); }
        }

        private int fPuntiLuce;
        [Persistent("PUNTILUCE"), DisplayName("Punti Luce (Numero)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int PuntiLuce
        {
            get { return fPuntiLuce; }
            set { SetPropertyValue<int>("PuntiLuce", ref fPuntiLuce, value); }
        }


        private int fPuntiSemaforici;
        [Persistent("PUNTISEMAFORICI"), DisplayName("Impianti Semaforici (Numero)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int PuntiSemaforici
        {
            get { return fPuntiSemaforici; }
            set { SetPropertyValue<int>("PuntiSemaforici", ref fPuntiSemaforici, value); }
        }

        private double fCSL;
        [Persistent("CSL"), DisplayName("CSL= Canone annuo stimato per gli impianti di illuminazione pubblica (€)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")] 
        public double CSL
        {
            get { return fCSL; }
            set { SetPropertyValue<double>("CSL", ref fCSL, value); }
        }


        private double fCGS;
        [Persistent("CGS"), DisplayName("CGS= Canone annuo per gli impianti semaforici e di segnaletica luminosa (€) ")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")] 
        public double CGS
        {
            get { return fCGS; }
            set { SetPropertyValue<double>("CGS", ref fCGS, value); }
        }

        private double fExtraCanone;
        [Persistent("EXTRACANONE"), DisplayName("Extra Canone (€)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")] 
        public double ExtraCanone
        {
            get { return fExtraCanone; }
            set { SetPropertyValue<double>("ExtraCanone", ref fExtraCanone, value); }
        }

        private double fValoreOPF5Anni;
        [Persistent("VALOREOPF5ANNI"), DisplayName("Valore Ordinativo Principale di Fornitura (OPF) a 5 anni (€)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")] 
        public double ValoreOPF5Anni
        {
            get { return fValoreOPF5Anni; }
            set { SetPropertyValue<double>("ValoreOPF5Anni", ref fValoreOPF5Anni, value); }
        }

        private double fValoreOPF9Anni;
        [Persistent("VALOREOPF9ANNI"), DisplayName("Valore Ordinativo Principale di Fornitura (OPF) a 9 anni (€)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")] 
        public double ValoreOPF9Anni
        {
            get { return fValoreOPF9Anni; }
            set { SetPropertyValue<double>("ValoreOPF9Anni", ref fValoreOPF9Anni, value); }
        }

        private string fCodiceOPF;
        [Persistent("CODICEOPF"), DisplayName("Codice OPF")]
        [Size(250), DbType("varchar(250)")]
        public string CodiceOPF
        {
            get { return fCodiceOPF; }
            set { SetPropertyValue<string>("CodiceOPF", ref fCodiceOPF, value); }
        }

        private DateTime fDataStipulaOPF;
        [Persistent("DATASTIPULAOPF"), DisplayName("Data stipula OPF")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataStipulaOPF
        {
            get { return fDataStipulaOPF; }
            set { SetPropertyValue<DateTime>("DataStipulaOPF", ref fDataStipulaOPF, value); }
        }

        private int fDurataAnniOPF;
        [Persistent("DURATAOPF"), DisplayName("Durata OPF(5 o 9 anni)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public int DurataAnniOPF
        {
            get { return fDurataAnniOPF; }
            set { SetPropertyValue<int>("DurataAnniOPF", ref fDurataAnniOPF, value); }
        }

        private double fValoreOPF;
        [Persistent("VALOREOPF"), DisplayName("Valore OPF (€)")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:C}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "C")] 
        public double ValoreOPF
        {
            get { return fValoreOPF; }
            set { SetPropertyValue<double>("ValoreOPF", ref fValoreOPF, value); }
        }

        private DateTime fDataAttivazioneServizio;
        [Persistent("DATAATTIVAZIONESERVIZIO"), DisplayName("Data attivazione del servizio")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataAttivazioneServizio
        {
            get { return fDataAttivazioneServizio; }
            set { SetPropertyValue<DateTime>("DataAttivazioneServizio", ref fDataAttivazioneServizio, value); }
        }


        /// <summary>
        /// /////////////////
        /// </summary>
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DisplayName("Utente")]
        [DbType("varchar(100)")]
        //  [Appearance("SchedeMPOwner.Utente", Enabled = false)]
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
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        //[Appearance("SchedeMPOwner.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }


        [Association(@"RegistroContrattiConsip_Dettagli", typeof(ContrattiConsipDettagli)), Aggregated]
        [DevExpress.Xpo.DisplayName("Dettagli")]
        [ExplicitLoading()]
        public XPCollection<ContrattiConsipDettagli> ContrattiConsipDettaglis
        {
            get
            {
                return GetCollection<ContrattiConsipDettagli>("ContrattiConsipDettaglis");
            }
        }


        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }

    }
}

