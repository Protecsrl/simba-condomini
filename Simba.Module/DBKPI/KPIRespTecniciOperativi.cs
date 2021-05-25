using CAMS.Module.Classi;
using CAMS.Module.DBTask;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;

namespace CAMS.Module.DBKPI
{

    [DefaultClassOptions, Persistent("KPIRESPTECNICIOPERATIVI")]  //v_regoperativorisorse
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "KPI Responsabili Tecnici")]
    [NavigationItem("KPI")]
    [System.ComponentModel.DefaultProperty("DataSettimanaAnno")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    [Appearance("KPIRespTecniciOperativi.PER.Utilizzo.Red", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "darkred", BackColor = "ivory", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "TmpUtilizzo < (TmpRifWeek * 0.6) ")]
    [Appearance("KPIRespTecniciOperativi.PER.Utilizzo.Salmon", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "orangered", BackColor = "papayawhip", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "TmpUtilizzo >= (TmpRifWeek * 0.6) And TmpUtilizzo < (TmpRifWeek * 0.7)")]
    [Appearance("KPIRespTecniciOperativi.PER.Utilizzo.green", TargetItems = "PrcUtilizzo;TmpUtilizzo", FontColor = "darkolivegreen", BackColor = "palegreen", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "TmpUtilizzo >= (TmpRifWeek * 0.7)")]
   
    [Appearance("KPIRespTecniciOperativi.PER.InLavorazione.Red", TargetItems = "PrcInLavorazione;TmpInLavorazione", FontColor = "Red", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "TmpInLavorazione < (TmpUtilizzo * 0.6) ")]
    [Appearance("KPIRespTecniciOperativi.PER.InLavorazione.Salmon", TargetItems = "PrcInLavorazione;TmpInLavorazione", FontColor = "Salmon", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "TmpInLavorazione >= (TmpUtilizzo * 0.6) And TmpInLavorazione < (TmpUtilizzo * 0.7)")]
    [Appearance("KPIRespTecniciOperativi.PER.InLavorazione.green", TargetItems = "PrcInLavorazione;TmpInLavorazione", FontColor = "Green", FontStyle = FontStyle.Bold, Context = "Any", Criteria = "TmpInLavorazione >= (TmpUtilizzo * 0.7)")]


    public class KPIRespTecniciOperativi : XPObject
    {
        public KPIRespTecniciOperativi()
            : base()
        {
        }

        public KPIRespTecniciOperativi(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private const string DateAndTimeOfDayEditMaskhhss = "dddd dd/MM/yyyy";
        #region
        private DateTime fDataSettimanaAnno;
        [Persistent("DATASETTIMANAANNO"), DisplayName("Data Anno Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMaskhhss + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMaskhhss)]
        //[VisibleInListView(false)]
        public DateTime DataSettimanaAnno
        {
            get { return fDataSettimanaAnno; }
            set { SetPropertyValue<DateTime>("DataSettimanaAnno", ref fDataSettimanaAnno, value); }
        }

        //  [PersistentAlias("Iif(DataSettimanaAnno is not null, GetYear(DataSettimanaAnno) + '-'+ (ToInt(Ceiling(ToFloat(GetDayOfYear(DataSettimanaAnno) - GetDayOfWeek(DataSettimanaAnno) - 1) / 7) + 1) ), Null)")]
        private string fSettimana;
        [Persistent("SETTIMANAANNO")]
        [System.ComponentModel.DisplayName("Settimana")]
        [Appearance("KPIRespTecniciOperativi.Settimana.nonvisibile", AppearanceItemType.LayoutItem, @"[DataSettimanaAnno] is not null", TargetItems = "Settimana", Context = "Any", Priority = 1, Visibility = ViewItemVisibility.Hide)]
        public string Settimana
        {
            get { return fSettimana; }
            set { SetPropertyValue<string>("Settimana", ref fSettimana, value); }
            //get
            //{
            //    var tempObject = EvaluateAlias("Settimana");
            //    if (tempObject != null)
            //    {
            //        return (string)tempObject;
            //    }
            //    else
            //    {
            //        return null;
            //    }
            //}
        }

        private string fResponsabileTecnico;
        [Persistent("UTENTE"), Size(150), DisplayName("Responsabile Tecnico")]
        [DbType("varchar(150)")]
        public string ResponsabileTecnico
        {
            get { return fResponsabileTecnico; }
            set { SetPropertyValue<string>("ResponsabileTecnico", ref fResponsabileTecnico, value); }
        }


        private string fAreaDiPolo;
        [Persistent("AREADIPOLO"), Size(150), DisplayName("Area DI Polo")]
        [DbType("varchar(150)")]
        public string AreaDiPolo
        {
            get { return fAreaDiPolo; }
            set { SetPropertyValue<string>("AreaDiPolo", ref fAreaDiPolo, value); }
        }
        private TipoOrario fTipoOrario;
        [Persistent("TIPOORARIO"), DisplayName("TipoOrario")]
        //[VisibleInListView(false)]
        public TipoOrario TipoOrario
        {
            get { return fTipoOrario; }
            set { SetPropertyValue<TipoOrario>("TipoOrario", ref fTipoOrario, value); }
        }

        private Risorse fRisorsa;
        [Persistent("RISORSA"), DisplayName("Risorsa")]
        [VisibleInListView(false)]
        public Risorse Risorsa
        {
            get { return fRisorsa; }
            set { SetPropertyValue<Risorse>("Risorsa", ref fRisorsa, value); }
        }
        #endregion
        #region
        private int fPrcUtilizzo;
        [Persistent("PRC_UTILIZZO"), DisplayName("% Utilizzo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N0}%")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "{0:N0}%")]
        public int PrcUtilizzo
        {
            get { return fPrcUtilizzo; }
            set { SetPropertyValue<int>("PrcUtilizzo", ref fPrcUtilizzo, value); }
        }

        private int fPrcInLavorazione;
        [Persistent("PRC_INLAVORAZIONE"), DisplayName("% Lavorazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N0}%")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "{0:N0}%")]
        public int PrcInLavorazione
        {
            get { return fPrcInLavorazione; }
            set { SetPropertyValue<int>("PrcInLavorazione", ref fPrcInLavorazione, value); }
        }

        private int fPrcPausa;
        [Persistent("PRC_PAUSA"), DisplayName("% Pausa")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N0}%")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "{0:N0}%")]
        public int PrcPausa
        {
            get { return fPrcPausa; }
            set { SetPropertyValue<int>("PrcPausa", ref fPrcPausa, value); }
        }

        private int fPrcNonAttivo;
        [Persistent("PRC_NONATTIVO"), DisplayName("% NonAttivo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N0}%")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "{0:N0}%")]
        public int PrcNonAttivo
        {
            get { return fPrcNonAttivo; }
            set { SetPropertyValue<int>("PrcNonAttivo", ref fPrcNonAttivo, value); }
        }
        //--------------------------------
        private int fTmpUtilizzo;
        [Persistent("TMP_UTILIZZO"), DisplayName("tempo Utilizzo")]
        public int TmpUtilizzo
        {
            get { return fTmpUtilizzo; }
            set { SetPropertyValue<int>("TmpUtilizzo", ref fTmpUtilizzo, value); }
        }

        private int fTmpInLavorazione;
        [Persistent("TMP_INLAVORAZIONE"), DisplayName("tempo Lavorazione")]
        [VisibleInListView(false)]
        public int TmpInLavorazione
        {
            get { return fTmpInLavorazione; }
            set { SetPropertyValue<int>("TmpInLavorazione", ref fTmpInLavorazione, value); }
        }

        private int fTmpPausa;
        [Persistent("TMP_PAUSA"), DisplayName("tempo Pausa")]
        [VisibleInListView(false)]
        public int TmpPausa
        {
            get { return fTmpPausa; }
            set { SetPropertyValue<int>("TmpPausa", ref fTmpPausa, value); }
        }

        private int fTmpNonAttivo;
        [Persistent("TMP_NONATTIVO"), DisplayName("tempo NonAttivo")]
        [VisibleInListView(false)]
        public int TmpNonAttivo
        {
            get { return fTmpNonAttivo; }
            set { SetPropertyValue<int>("TmpNonAttivo", ref fTmpNonAttivo, value); }
        }

        private int fTmpRifWeek;
        [Persistent("TMP_RIFWEEK"), DisplayName("tempo Rif. Settimanale")]
        [VisibleInListView(false)]
        public int TmpRifWeek
        {
            get { return fTmpRifWeek; }
            set { SetPropertyValue<int>("TmpRifWeek", ref fTmpRifWeek, value); }
        }

        #endregion

        private int fNrTecnici;
        [Persistent("NR_TECNICI"), DisplayName("Nr Tecnici")]
        [VisibleInListView(false)]
        public int NrTecnici
        {
            get { return fNrTecnici; }
            set { SetPropertyValue<int>("NrTecnici", ref fNrTecnici, value); }
        }

        private int fTmpReserva;
        [Persistent("TMP_RISERVA"), DisplayName("Reserva")]
        [VisibleInListView(false)]
        public int TmpReserva
        {
            get { return fTmpReserva; }
            set { SetPropertyValue<int>("TmpReserva", ref fTmpReserva, value); }
        }
        private int fTmpReserva1;
        [Persistent("TMP_RISERVA1"), DisplayName("Reserva1")]
        [VisibleInListView(false)]
        public int TmpReserva1
        {
            get { return fTmpReserva1; }
            set { SetPropertyValue<int>("TmpReserva1", ref fTmpReserva1, value); }
        }

        // CAMPI PARTICOLARI
        [Association(@"KPIRespTecniciOperativi_KPITecniciOperativi", typeof(KPITecniciOperativi)), Aggregated, DisplayName("Dettaglio")]
        [Appearance("KPIRespTecniciOperativi.Abilita.KPITecniciOperativiDettaglios", Criteria = "Oid < 0", Enabled = false)]
        public XPCollection<KPITecniciOperativi> KPITecniciOperativiS
        {
            get
            {
                return GetCollection<KPITecniciOperativi>("KPITecniciOperativiS");
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