using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;

using CAMS.Module.PropertyEditors;
using CAMS.Module.DBTask;
using System.Drawing;
using DevExpress.Persistent.Validation;
using CAMS.Module.Classi;

namespace CAMS.Module.DBKPI
{

    [DefaultClassOptions, Persistent("KPITECNICIOPERATIVI")]  //v_regoperativorisorse
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI Tecnici Operativi")]
    [NavigationItem("KPI")]
    [System.ComponentModel.DefaultProperty("Data Riferimento")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
    //---
    [Appearance("KPITecniciOperativi.nonlavorato.FontColor.Grey", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "Blue", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "(TmpTra + TmpSit + TmpPP + TmpNatt) < 1")]

    [Appearance("KPITecniciOperativi.PER.FontColor.Red", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "Red", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "(TmpTra + TmpSit + TmpPP + TmpNatt) < (OraUSLG * 0.6) ")]
    [Appearance("KPITecniciOperativi.PER.FontColor.Salmon", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "Salmon", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "(TmpTra + TmpSit + TmpPP + TmpNatt) >= (OraUSLG * 0.6) And (TmpTra + TmpSit + TmpPP + TmpNatt) < (OraUSLG * 0.7)")]
    [Appearance("KPITecniciOperativi.PER.FontColor.gree", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "Green", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "(TmpTra + TmpSit + TmpPP + TmpNatt) >= (OraUSLG * 0.7)")]

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "All Data", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPITecniciOperativi.lavorati", "(TmpTra + TmpSit + TmpPP + TmpNatt) < 1", "non Lavorato", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("KPITecniciOperativi.nonLavorati", "(TmpTra + TmpSit + TmpPP + TmpNatt) > 0", "Lavorato", Index = 2)]

    // [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    // [DevExpress.ExpressApp.SystemModule.ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    #endregion
    //[RuleCriteria("RuleError.ATTACC_COM.TMPE", DefaultContexts.Save, @"1=1", CustomMessageTemplate = "Nr Registri RdL Attività Assistenza Completati",
    //                    SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Information)]
    public class KPITecniciOperativi : XPObject
    {
        public KPITecniciOperativi()
            : base()
        {
        }

        public KPITecniciOperativi(Session session)
            : base(session)
        {
        }


        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        //DATAGIORNO	N	DATE
        //RISORSA	N	NUMBER
        //RISORSADESC	N	VARCHAR2(150)
        //UTENTE	N	NVARCHAR2(150)
        //CENTROOPERATIVO	N	VARCHAR2(150)


        private const string DateAndTimeOfDayEditMaskhhss = "dddd dd/MM/yyyy";
        private DateTime fDataGiorno;
        [Persistent("DATAGIORNO"), DisplayName("Data Riferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskhhss + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskhhss)]
        [ToolTip("il Lunedi della settimana in riferimento")]
        public DateTime DataGiorno
        {
            get { return fDataGiorno; }
            set { SetPropertyValue<DateTime>("DataGiorno", ref fDataGiorno, value); }
        }

        private string fUtenteTecnico;

        [Persistent("UTENTE"), Size(150), DisplayName("Utente Tecnico")]
        [DbType("varchar(150)")]
        [VisibleInListView(false)]
        public string UtenteTecnico
        {
            get { return fUtenteTecnico; }
            set { SetPropertyValue<string>("UtenteTecnico", ref fUtenteTecnico, value); }
        }
        private string fRisorsaDesc;
        [Persistent("RISORSADESC"), Size(150), DisplayName("Tecnico")]
        [DbType("varchar(150)")]

        public string RisorsaDesc
        {
            get { return fRisorsaDesc; }
            set { SetPropertyValue<string>("RisorsaDesc", ref fRisorsaDesc, value); }
        }

        private string fCentroOperativo;
        [Persistent("CENTROOPERATIVO"), Size(150), DisplayName("Centro Operativo")]
        [DbType("varchar(150)")]
        public string CentroOperativo
        {
            get { return fCentroOperativo; }
            set { SetPropertyValue<string>("CentroOperativo", ref fCentroOperativo, value); }
        }

        private Risorse fRisorse;
        [Persistent("RISORSA"), DisplayName("Risorsa")]
        [VisibleInListView(false)]
        public Risorse Risorse
        {
            get { return fRisorse; }
            set { SetPropertyValue<Risorse>("Risorse", ref fRisorse, value); }
        }
        // DATAGIORNO	DATE
        //UTENTE	VARCHAR2(150)
        //RISORSADESC	VARCHAR2(150)
        //CENTROOPERATIVO	VARCHAR2(150)
        //RISORSA	INTEGER
        //OptimisticLockField	INTEGER
        //GCRecord	INTEGER
        //TMP_TRA	INTEGER
        //TMP_SIT	INTEGER
        //TMP_PP	INTEGER
        //TMP_NATT	INTEGER

        private int fOraUSLG;
        [Persistent("ORA_SLG"), DisplayName("Lav. Standard Gionaliero(min)")]
        [VisibleInListView(false)]
        [ToolTip("Tempo di lavoro giornaliero standard (minuti)")]
        public int OraUSLG
        {
            get { return fOraUSLG; }
            set { SetPropertyValue<int>("OraUSLG", ref fOraUSLG, value); }
        }

        private int fPrcUtilizzo;
        [Persistent("PRC_UTILIZZO"), DisplayName("% Utilizzo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N0}%")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "{0:N0}%")]
        [ToolTip("Percentuale di Utilizzo (connessione) del Sistema - Calcolata tempo di Utilizzo su Tempo di lavoro giornaliero standard)")]
        public int PrcUtilizzo
        {
            get { return fPrcUtilizzo; }
            set { SetPropertyValue<int>("PrcUtilizzo", ref fPrcUtilizzo, value); }
        }

        private int fTmpUtilizzo;
        [Persistent("TMP_UTILIZZO"), DisplayName("tempo Utilizzo")]
        //  [VisibleInListView(false)]
        [ToolTip("Totale Tempo (in minuti) di Utilizzo del sistema (tempo di connessione al sistema - data prima connessione e ultima connessione)")]
        public int TmpUtilizzo
        {
            get { return fTmpUtilizzo; }
            set { SetPropertyValue<int>("TmpUtilizzo", ref fTmpUtilizzo, value); }
        }

        private int fTmpTra;
        [Persistent("TMP_TRA"), DisplayName("tempo Trasferimento")]
        // [VisibleInListView(false)]
        [ToolTip("Totale Tempo (in minuti) in Trasferimento")]
        public int TmpTra
        {
            get { return fTmpTra; }
            set { SetPropertyValue<int>("TmpTra", ref fTmpTra, value); }
        }

        private int fTmpSit;
        [Persistent("TMP_SIT"), DisplayName("tempo In Sito")]
        // [VisibleInListView(false)]
        [ToolTip("Totale Tempo (in minuti) in Sito")]
        public int TmpSit
        {
            get { return fTmpSit; }
            set { SetPropertyValue<int>("TmpSit", ref fTmpSit, value); }
        }

        private int fTmpPP;
        [Persistent("TMP_PP"), DisplayName("tempo In Pausa")]
        //  [VisibleInListView(false)]
        [ToolTip("Totale Tempo (in minuti) di Pausa")]
        public int TmpPP
        {
            get { return fTmpPP; }
            set { SetPropertyValue<int>("TmpPP", ref fTmpPP, value); }
        }


        private int fTmpNatt;
        [Persistent("TMP_NATT"), DisplayName("tempo Inattivo")]
        [ToolTip("Totale Tempo (in minuti) di non Attività (non in fase di Trasferimento e in Sito)")]
        //  [VisibleInListView(false)]
        public int TmpNatt
        {
            get { return fTmpNatt; }
            set { SetPropertyValue<int>("TmpNatt", ref fTmpNatt, value); }
        }

        //MP_COM	INTEGER
        //TT_COM	INTEGER
        //MPS_MS_COM	INTEGER
        //ATTACC_COM	INTEGER 
        private int fMPCom;
        [Persistent("MP_COM"), DisplayName("Nr RdL MP Cmp")]
        [ToolTip("Nr Registri RdL Manutenzione Programmata  Completati")]
        public int MPCom
        {
            get { return fMPCom; }
            set { SetPropertyValue<int>("MPCom", ref fMPCom, value); }
        }

        private int fTTCom;
        [Persistent("TT_COM"), DisplayName("Nr RdL TT Cmp")]
        [ToolTip("Nr Registri RdL Manutenzione a Guasto  Completati")]
        public int TTCom
        {
            get { return fTTCom; }
            set { SetPropertyValue<int>("TTCom", ref fTTCom, value); }
        }




        private int fMPSMSCom;
        [Persistent("MPS_MS_COM"), DisplayName("Nr RdL MS Cmp")]
        [ToolTip("Nr Registri RdL Manutenzione Straordinaria  Completati")]
        public int MPSMSCom
        {
            get { return fMPSMSCom; }
            set { SetPropertyValue<int>("MPSMSCom", ref fMPSMSCom, value); }
        }

        private int fATTACCCom;
        [Persistent("ATTACC_COM"), DisplayName("Nr Att-ACC Cmp")]
        [ToolTip("Nr Registri RdL Attività Assistenza Completati")]
        public int ATTACCCom
        {
            get { return fATTACCCom; }
            set { SetPropertyValue<int>("ATTACCCom", ref fATTACCCom, value); }
        }

        //MPJT_COM	INTEGER
        //TTJT_COM	INTEGER
        //MPSMSJT_COM	INTEGER
        //NRJTTT	INTEGER da eliminare
        private int fMPJTCom;
        [Persistent("MPJT_COM"), DisplayName("Nr JT-MP Cmp")]
        [ToolTip("Nr Registri RdL Acquisiti in Jest in Time di Manutenzione Programmata Completati")]
        public int MPJTCom
        {
            get { return fMPJTCom; }
            set { SetPropertyValue<int>("MPJTCom", ref fMPJTCom, value); }
        }

        private int fTTJTCom;
        [Persistent("TTJT_COM"), DisplayName("Nr JT-TT Cmp")]
        [ToolTip("Nr Registri RdL Acquisiti in Jest in Time di Manutenzione a Guasto Completati")]
        public int TTJTCom
        {
            get { return fTTJTCom; }
            set { SetPropertyValue<int>("TTJTCom", ref fTTJTCom, value); }
        }

        private int fMPSMSJTCom;
        [Persistent("MPSMSJT_COM"), DisplayName("Nr JT-MS Cmp")]
        [ToolTip("Nr Registri RdL Acquisiti in Jest in Time di Manutenzione Straordinaria Completati")]
        public int MPSMSJTCom
        {
            get { return fMPSMSJTCom; }
            set { SetPropertyValue<int>("MPSMSJTCom", ref fMPSMSJTCom, value); }
        }

        //AG_TT	INTEGER
        //AG_MP	INTEGER
        //AG_MPSMS	INTEGER
        //DATAORA_CONNESSIONE	DATE
        //DATAORA_ULTIMACONN	DATE

        private int fAGTT;
        [Persistent("AG_TT"), DisplayName("Nr TT AGENDA")]
        [ToolTip("Nr Registri RdL in Agenda di Manutenzione a Guasto (da prendere in carico e sospesi)")]
        public int AGTT
        {
            get { return fAGTT; }
            set { SetPropertyValue<int>("AGTT", ref fAGTT, value); }
        }

        private int fAGMP;
        [Persistent("AG_MP"), DisplayName("Nr MP AGENDA")]
        [ToolTip("Nr Registri RdL in Agenda di Manutenzione Programmata (da prendere in carico e sospesi)")]
        public int AGMP
        {
            get { return fAGMP; }
            set { SetPropertyValue<int>("AGMP", ref fAGMP, value); }
        }

        private int fAGMPSMP;
        [Persistent("AG_MPSMS"), DisplayName("Nr MPS-SM AGENDA")]
        [ToolTip("Nr Registri RdL in Agenda di Manutenzione Straordinaria (da prendere in carico e sospesi)")]
        public int AGMPSMP
        {
            get { return fAGMPSMP; }
            set { SetPropertyValue<int>("AGMPSMP", ref fAGMPSMP, value); }
        }

        private const string DateAndTimeOfDayEditMaskC = "dd/MM/yyyy H:mm:ss";
        private DateTime fDataOraConnessione;
        [Persistent("DATAORA_CONNESSIONE"), DisplayName("Data Ora Connessione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskC + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskC)]
        [ToolTip("Data Ora di Connessione - la prima della giornata")]
        public DateTime DataOraConnessione
        {
            get { return fDataOraConnessione; }
            set { SetPropertyValue<DateTime>("DataOraConnessione", ref fDataOraConnessione, value); }
        }

        private const string DateAndTimeOfDayEditMaskUC = "dd/MM/yyyy H:mm:ss";
        private DateTime fDataOraUltimaConnessione;
        [Persistent("DATAORA_ULTIMACONN"), DisplayName("Data Ora Ultima Connessione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskUC + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskUC)]
        [ToolTip("Data Ora di ultima Connessione - l'ultima della giornata")]
        public DateTime DataOraUltimaConnessione
        {
            get { return fDataOraUltimaConnessione; }
            set { SetPropertyValue<DateTime>("DataOraUltimaConnessione", ref fDataOraUltimaConnessione, value); }
        }

        [Association(@"KPIRespTecniciOperativi_KPITecniciOperativi"), Persistent("KPIRESPTECNICIOPERATIVI"), DisplayName("Responsabile Tecnico Operativo")]
        [Delayed(true)]
        public KPIRespTecniciOperativi KPIRespTecniciOperativi
        {
            get { return GetDelayedPropertyValue<KPIRespTecniciOperativi>("KPIRespTecniciOperativi"); }
            set { SetDelayedPropertyValue<KPIRespTecniciOperativi>("KPIRespTecniciOperativi", value); }
        }


        [Association(@"KPITecniciOperativi_Dett", typeof(KPITecniciOperativiDett)), Aggregated, DisplayName("Dettaglio")]
        [Appearance("KPITecniciOperativi.Abilita.KPITecniciOperativiDettaglios", Criteria = "Oid < 0", Enabled = false)]
        public XPCollection<KPITecniciOperativiDett> KPITecniciOperativiDettS
        {
            get
            {
                return GetCollection<KPITecniciOperativiDett>("KPITecniciOperativiDettS");
            }
        }

        private TipoBloccoDati fBlocco;  // 0=non bloccato , 1 Bloccato Tutto, 2 cloccato solo inserimento, 3 Bloccato solo aggiornamento
        [Persistent("BLOCCO"), DisplayName("Blocco")]
        [VisibleInListView(false)]
        public TipoBloccoDati Blocco
        {
            get { return fBlocco; }
            set { SetPropertyValue<TipoBloccoDati>("Blocco", ref fBlocco, value); }
        }
        //public enum TipoBloccoDati
        //{
        //    [XafDisplayName("Sbloccato")]
        //    Sbloccato,
        //    [XafDisplayName("Bloccato")]
        //    Bloccato,
        //    [XafDisplayName("Bloccato Solo Inserimento")]
        //    BloccatoSoloInserimento,
        //    [XafDisplayName("Bloccato Solo Modifica")]
        //    BloccatoSoloModifica
        //}

        //[PersistentAlias("[<AccountTransaction>][^.Record = SysID].Single()")]
        //public AccountTransaction Transaction
        //{
        //    get
        //    {
        //        return Session.Query<AccountTransaction>().First(at => at.SysID == this.Record);
        //    }
        //}
        //'Parser error at line 0, character 20: syntax error; ("[<Risorse>][Oid == ^{FAILED HERE}Risorse].Single()")'
        //[PersistentAlias("[<Risorse>][Oid == ^.Risorse].Single()")]
        //public Risorse Risorsa
        //{
        //    get
        //    {
        //        object tempObject = EvaluateAlias("Risorsa");
        //        if (tempObject != null)
        //        {
        //            return (Risorse)tempObject;
        //        }
        //        else
        //        {
        //            return null;
        //        }
        //    }
        //}

    }
}

//DATA	A
//CO	B
//USER	C
//DATAORA_CONNESSIONE	D
//DATAORA_ULTIMACONN	E
//TT_COM	G
//MP_COM	H
//MPS_MS_COM	I
//ATTACC_COM	J
//TTJT_COM	K
//MPJT_COM	L
//MPSMSJT_COM	M
//    N
//TMP_TRA	O
//TMP_SIT	P
//TMP_PP	Q
//TMP_NATT	R
//    S
//AG_TT	T
//AG_MP	U
//AG_MPSMS	V




//TOT_TRA	N	NUMBER
//TOT_SIT	N	NUMBER
//TOT_PAUSA	N	NUMBER
//TOT_SPENTO	N	NUMBER
//TOT_SOS	N	NUMBER
//TOT_COM	N	NUMBER

//NRMP	N	NUMBER
//NRTT	N	NUMBER
//NRJTTT	N	NUMBER
//NRJTMP	N	NUMBER
//NRACC	N	NUMBER

//TOT	N	NUMBER

//private int fCentroOperativo;