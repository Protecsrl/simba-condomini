using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

using DevExpress.Persistent.Validation;
using CAMS.Module.DBPlant;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.Editors;
using System.Drawing;
using CAMS.Module.DBAngrafica;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
//using DevExpress.ExpressApp.DC;

namespace CAMS.Module.DBPlanner
{
    [DefaultClassOptions, Persistent("MPREGPIANIFICAZIONE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Reg. Pianificazione MP")]
    [Appearance("RegPianificazioneMP.Mai.Editabili.FontColor", TargetItems = "DescrizioneAnnulla;DescrizioneAzione;DescrizioneAvanti;MPStatoPianificazione", Criteria = "1=1", FontStyle = FontStyle.Bold, FontColor = "Black")]
    [Appearance("RegPianificazioneMP.Mai.Editabili", TargetItems = "MPStatoPianificazione", Criteria = @"[MPStatoPianificazione.Oid] < 4", FontColor = "Black", Enabled = false)]
    [Appearance("RegPianificazioneMP.si.Editabili", TargetItems = "MPStatoPianificazione", Criteria = @"[MPStatoPianificazione.Oid] >= 4", FontColor = "Black")]
    [Appearance("RegPianificazioneMP.MPStatoPianificazione.piudi4", TargetItems = "AnnoSchedulazione;Scenario;Descrizione", Criteria = @"[MPStatoPianificazione.Oid] > 2", FontColor = "Black", Enabled = false)]
    [Appearance("RegPianificazioneMP.MPStatoPianificazione.inCreazione", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] < 4",
        TargetItems = "RegPianMPDetts;RPMPGhosts;DettAzioniSKs;DettCreazOdLs;MpAttivitaPianificate;RegPianificazioneMPRevisionis;MPDataIniziales;MPAttivitaPluriAnnualis;MPDataContatores;MPDFissas",
         Visibility = ViewItemVisibility.Hide)]
    // riga in rosso quando validità nulla
    [Appearance("RegPianiMP.Validita.Descrizione.Rosso", "!IsNullOrEmpty(Validita)", TargetItems = "Descrizione", FontStyle = FontStyle.Bold, FontColor = "Red", Priority = 1)]
    [Appearance("RegPianiMP.Validita.Rosso", AppearanceItemType.ViewItem, "!IsNullOrEmpty(Validita)", TargetItems = "Validita", BackColor = "Yellow", FontStyle = FontStyle.Bold, FontColor = "Red", Priority = 1)]


    //[Appearance("RegPianificazioneMP.ghostnonvisibili", AppearanceItemType.LayoutItem, @"1=1", TargetItems = "MPRegPoliMansioniCaricos", Visibility = ViewItemVisibility.Hide)]

    [ImageName("BO_Appearance")]
    [NavigationItem("Planner")]
    public class RegPianificazioneMP : XPObject
    {
        private const string STR_Format = "{N}";
        public RegPianificazioneMP() : base() { }
        public RegPianificazioneMP(Session session) : base(session) { }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(0);
                int aa = DateTime.Now.Year;
                AnnoSchedulazione = Session.GetObjectByKey<Anni>(aa);
                this.fTipoAggregazioneRegMP = Classi.TipoAggregazioneRegMP.ManualexAggregazioni;
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [ImmediatePostData(true)]
        [RuleRequiredField("RegPianiMP.RReqField.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        // [ToolTip(" {Oid}", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]

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
        private Anni fAnnoSchedulazione;
        [Persistent("ANNOSK"), DisplayName("Anno Schedulazione")]
        // [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", STR_Format)]
        //[ DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Appearance("RegPianiMP.Anno.Enabled", Enabled = false, Criteria = "IsNullOrEmpty(Descrizione) Or Scenario != null", Priority = 1)]
        [ImmediatePostData]
        [DataSourceCriteria("Anno > 2014")]
        public Anni AnnoSchedulazione
        {
            get
            {
                return fAnnoSchedulazione;
            }
            set
            {
                SetPropertyValue<Anni>("AnnoSchedulazione", ref fAnnoSchedulazione, value);
            }
        }

        [Persistent("SCENARIO"), DisplayName("Scenario"), Association(@"RegPianiMP_Scenario", typeof(Scenario))]
        [Appearance("RegPianiMP.Scenario.Enabled", Enabled = false, Criteria = "IsNullOrEmpty(Descrizione) Or IsNullOrEmpty(AnnoSchedulazione)", Priority = 1)]
        //[DataSourceCriteria("InseribilesuRegPiano = 1")]
        //[DataSourceCriteria("[<ApparatoSchedaMP>][^.Oid != RegistroPianificazioneMP  And RegistroPianificazioneMP is null]")]
        [ImmediatePostData]
        [ExplicitLoading()]
        [Delayed(true)]
        public Scenario Scenario
        {
            get
            {

                return GetDelayedPropertyValue<Scenario>("Scenario");
            }
            set
            {
                SetDelayedPropertyValue<Scenario>("Scenario", value);
            }
        }


        private MPStatoPianificazione fMPStatoPianificazione;
        [Persistent("MPSTATOPIANIFICA"), DisplayName("Stato Pianificazione")]    // [Appearance("RegPianiMP.MPStatoPianificazione.Visibility", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianiMP.MPStatoPianificazione.Evidenza", AppearanceItemType.LayoutItem, @"1=1", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        //[DataSourceProperty("ListaComboStatoPiana")]
        //[DataSourceProperty("Iif(Oid = -1,null, Oid = '@This.MPStatoPianificazione.NextAnnulla' Or Oid = '@This.MPStatoPianificazione.Oid' Or Oid = '@This.MPStatoPianificazione.NextAvanti' )")]
        [ImmediatePostData(true)]
        public MPStatoPianificazione MPStatoPianificazione
        {
            get
            {
                return fMPStatoPianificazione;
            }
            set
            {
                SetPropertyValue<MPStatoPianificazione>("MPStatoPianificazione", ref fMPStatoPianificazione, value);
                if (Oid != -1)
                {
                    OnChanged("DescrizioneAzione");
                    OnChanged("DescrizioneAnnulla");
                    OnChanged("DescrizioneAvanti");
                }
            }
        }

        private XPCollection<MPStatoPianificazione> fListaComboStatoPiana;
        [System.ComponentModel.Browsable(false)] // Prohibits showing the AvailableAccessories collection separately 
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<MPStatoPianificazione> ListaComboStatoPiana
        {
            get
            {
                if (this.MPStatoPianificazione != null && this.Oid != -1)
                {
                    if (fListaComboStatoPiana == null)
                    {   ///   Toglie gli sk prec
                        // List<int> TQOidObjAnnulla= Session.QueryInTransaction<MPStatoPianificazione>()
                        //.Where(w => w.Oid != this.MPStatoPianificazione.Oid)                      
                        //.Select(s => s.NextAnnulla).Distinct().ToList();

                        // List<int> TQOidAvanti = Session.QueryInTransaction<MPStatoPianificazione>()
                        //.Where(w => w.Oid != this.MPStatoPianificazione.Oid)
                        //.Select(s => s.NextAvanti).Distinct().ToList();
                        // /// sel gli app che hanno contaore
                        // List<int> OidSel = TQOidObjAnnulla.Concat(TQOidAvanti).ToList();
                        int OidThis = this.MPStatoPianificazione.Oid;
                        int OidAnnulla = this.MPStatoPianificazione.NextAnnulla;
                        int OidAvanti = this.MPStatoPianificazione.NextAvanti;
                        List<int> OidSel = new List<int> { OidAnnulla, OidThis, OidAvanti };

                        CriteriaOperator charFiltert = new InOperator("Oid", OidSel);
                        GroupOperator goc = new GroupOperator(GroupOperatorType.And, charFiltert);
                        fListaComboStatoPiana = new XPCollection<MPStatoPianificazione>(Session, goc);
                    }

                }
                return fListaComboStatoPiana; // Return the filtered collection of Accessory objects 
            }
        }

        private TipoAggregazioneRegMP fTipoAggregazioneRegMP;
        [Persistent("TIPOAGGREGAZIONEREGRDL"), DisplayName("Tipo Aggregazione")]
        [VisibleInListView(false)]
        public TipoAggregazioneRegMP TipoAggregazioneRegMP
        {
            get
            {
                return fTipoAggregazioneRegMP;
            }
            set
            {
                SetPropertyValue<TipoAggregazioneRegMP>("TipoAggregazioneRegMP", ref fTipoAggregazioneRegMP, value);
            }
        }



        [Association(@"RegPianMPrefDett", typeof(RegPianificazioneMPDett))]
        [DisplayName("Dettaglio del Registro di Pianificazione")]
        [MemberDesignTimeVisibility(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegPianificazioneMPDett> RegPianMPDetts
        {
            get
            {
                return GetCollection<RegPianificazioneMPDett>("RegPianMPDetts");
            }
        }


        [Association(@"RegPianiMP_DettagliAzioniSK", typeof(DettagliAzioniSK))]
        [DisplayName("Dettaglio Azioni di Schedulazione")]
        [MemberDesignTimeVisibility(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<DettagliAzioniSK> DettAzioniSKs
        {
            get
            {
                return GetCollection<DettagliAzioniSK>("DettAzioniSKs");
            }
        }

        [Association(@"RegPianiMP_DettCreazOdL", typeof(DettaglioCreazioneOdL))]
        [DisplayName("Dettaglio Creazione OdL MP"), MemberDesignTimeVisibility(false)]
        public XPCollection<DettaglioCreazioneOdL> DettCreazOdLs
        {
            get
            {
                return GetCollection<DettaglioCreazioneOdL>("DettCreazOdLs");
            }
        }


        [Association("RegPianiMP_AttivitaPianiDettaglio", typeof(MpAttivitaPianificateDett)), Aggregated]
        [DisplayName("Attività Pianificate Dettaglio")]
        [Appearance("RegPianiMP.MpAttivitaPianificateDett", Enabled = false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<MpAttivitaPianificateDett> MpAttivitaPianificateDetts
        {
            get
            {
                return GetCollection<MpAttivitaPianificateDett>("MpAttivitaPianificateDetts");
            }
        }



        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DisplayName("Ultima Data Aggiornamento"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [Appearance("RegPianiMP.UltimaDataAggi.Enabled", Enabled = false)]
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
        [PersistentAlias("MPStatoPianificazione.Oid"),
        DisplayName("Stato Corrente"),
        VisibleInDetailView(true),
        VisibleInListView(true),
        VisibleInLookupListView(false)]
        [Appearance("RegPianiMP.DescrizioneStato.Fontbold", FontStyle = FontStyle.Bold)]
        public string DescrizioneStato
        {
            get
            {
                var tempObject = EvaluateAlias("DescrizioneStato");
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
        [Appearance("RegPianiMP.Avanti.Hide", Criteria = "DescrizioneAvanti is null", Visibility = ViewItemVisibility.Hide)]
        //[Appearance("RegPianiMP.DescrizioneAvanti.Fontbold", FontStyle = FontStyle.Bold)]
        [PersistentAlias("MPStatoPianificazione.DescrizioneAvanti"),
        DisplayName("Prossima Azione"),
        VisibleInDetailView(true),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public string DescrizioneAvanti
        {
            get
            {
                var tempObject = EvaluateAlias("DescrizioneAvanti");
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
        [Appearance("RegPianiMP.Azione.Hide", Criteria = "DescrizioneAzione is null", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianiMP.MPStatoPianificazione.DescrizioneAzione.Evidenza", AppearanceItemType.LayoutItem, @"1=1", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [PersistentAlias("MPStatoPianificazione.DescrizioneAzione"),
        DisplayName("Azione da Eseguire"),
        VisibleInDetailView(true),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public string DescrizioneAzione
        {
            get
            {
                var tempObject = EvaluateAlias("DescrizioneAzione");
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
        [Appearance("RegPianiMP.Annulla.Hide", Criteria = "DescrizioneAnnulla is null", Visibility = ViewItemVisibility.Hide)]
        //[Appearance("RegPianiMP.DescrizioneAnnulla.Fontbold", FontStyle = FontStyle.Bold)]
        [PersistentAlias("MPStatoPianificazione.DescrizioneAnnulla"),
        DisplayName("Azione Precedente"),
        VisibleInDetailView(true),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        public string DescrizioneAnnulla
        {
            get
            {
                var tempObject = EvaluateAlias("DescrizioneAnnulla");
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

        [NonPersistent, Size(250), DisplayName(@"Validità")]
        //[Appearance("RegPianiMP.Validita.Hide", Criteria = "IsNullOrEmpty(Validita)", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianiMP.Validita.font", FontStyle = FontStyle.Italic, FontColor = "Red")]
        public string Validita
        {
            get
            {
                string Messaggio = string.Empty;
                try
                {
                    //int conta = Session.Query<ApparatoSchedaMP>()
                    //    .Where(d => d.RegistroPianificazioneMP.Oid == this.Oid)
                    //    .Where(s => s.Abilitato == FlgAbilitato.Si)
                    //    .Count();
                    //if (conta == 0)
                    return "";

                    //using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
                    //{
                    //    return db.GetMessaggioRegistroSchedulazioneVuoto(this.Oid);
                    //}
                }
                catch
                {
                    return "Errore nel Registro Pianificazione";
                }
                return Messaggio;
            }
        }

        private int fAzioneEseguita;
        //[Appearance("RegPianiMP.AzioneEseguita.Hide", Visibility = ViewItemVisibility.Hide)]
        [MemberDesignTimeVisibility(false),
        VisibleInDetailView(false),
        VisibleInListView(false),
        VisibleInLookupListView(false)]
        [NonPersistent,
        ImmediatePostData]
        public int AzioneEseguita
        {
            get
            {
                return fAzioneEseguita;
            }
            set
            {
                SetPropertyValue<int>("AzioneEseguita", ref fAzioneEseguita, value);
            }
        }

        private string fMessaggioRitorno;
        [Persistent("MESSAGGIORITORNO"), Size(250), DisplayName("Esito Azione")]
        [DbType("varchar(250)")]
        [Appearance("RegPianiMP.MessaggioRitorno.Hide", Criteria = "MessaggioRitorno  is null Or MessaggioRitorno = ''", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianiMP.MessaggioRitorno.font", FontStyle = FontStyle.Italic, FontColor = "Red")]
        [VisibleInDetailView(true),
      VisibleInListView(false),
      VisibleInLookupListView(false)]
        public string MessaggioRitorno
        {
            get
            {
                return fMessaggioRitorno;
            }
            set
            {
                SetPropertyValue<string>("MessaggioRitorno", ref fMessaggioRitorno, value);
            }
        }



        // private int fSettimana;
        // [Appearance("RegPianiMP.Settimana.Hide", Visibility = ViewItemVisibility.Hide)]
        // [VisibleInDetailView(false),
        //VisibleInListView(false),
        //VisibleInLookupListView(false),
        //ImmediatePostData]
        // [Persistent("SETTIMANA"),
        // DisplayName("Settimana Pianificata")]
        // public int Settimana
        // {
        //     get
        //     {
        //         return fSettimana;
        //     }
        //     set
        //     {
        //         SetPropertyValue<int>("Settimana", ref fSettimana, value);
        //     }
        // }

        #region RELAZIONE PIANIFICAZIONE MP REVISIONI


        [Association(@"RegPianificazioneMPRevisioni_RegPianificazioneMP", typeof(RegPianificazioneMPRevisioni)), DisplayName("Reg Pianificazione MP Revisioni")]
        [MemberDesignTimeVisibility(false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegPianificazioneMPRevisioni> RegPianificazioneMPRevisionis
        {
            get
            {
                return GetCollection<RegPianificazioneMPRevisioni>("RegPianificazioneMPRevisionis");
            }
        }
        #endregion

        #region CLASSI ASSOCIAZIONI EX  REGISTRO VINCOLI (MPDFissas,CalVincolis,MPAttivitaPluriAnnualis)
        [Association(@"RegPianoMP_MPDataFissa", typeof(MPDataFissa)), Aggregated, DisplayName("Impostazioni Data Fissa")]
        [Appearance("RegPianoMP_MPDataFissa.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 4", Visibility = ViewItemVisibility.Hide)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<MPDataFissa> MPDFissas
        {
            get
            {
                return GetCollection<MPDataFissa>("MPDFissas");
            }
        }

        [Association(@"RegPianoMP_MPDataContatore", typeof(MPDataContatore)), Aggregated, DisplayName("Impostazioni Contatore")]
        //   [Appearance("RegPianoMP_MPDataContatore.Enable", @"[MPStatoPianificazione.Oid] != 5", Enabled = false)]
        //[Appearance("RegPianoMP_MPDataContatore.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 5", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianoMP_MPDataContatore.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 5", Visibility = ViewItemVisibility.Hide)]

        public XPCollection<MPDataContatore> MPDataContatores
        {
            get
            {
                return GetCollection<MPDataContatore>("MPDataContatores");
            }
        }



        [Association(@"RegPianoMP_MPAttivitaPluriAnnuali", typeof(MPAttivitaPluriAnnuali)), Aggregated, DisplayName("Impostazioni primo anno cadenza Pluriennali")]
        //  [Appearance("RegPianoMP_MPAttivitaPluriAnnuali.Enable", @"[MPStatoPianificazione.Oid] != 6", Enabled = false)]
        //[Appearance("RegPianoMP_MPAttivitaPluriAnnuali.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 6", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianoMP_MPAttivitaPluriAnnuali.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 6", Visibility = ViewItemVisibility.Hide)]

        public XPCollection<MPAttivitaPluriAnnuali> MPAttivitaPluriAnnualis
        {
            get
            {
                return GetCollection<MPAttivitaPluriAnnuali>("MPAttivitaPluriAnnualis");
            }
        }

        [Association(@"RegPianoMP_MPDateStagionali", typeof(MPDateStagionali)), Aggregated, DisplayName("Impostazioni Data Stagionale")]
        //  [Appearance("RegPianoMP_MPAttivitaPluriAnnuali.Enable", @"[MPStatoPianificazione.Oid] != 6", Enabled = false)]
        //[Appearance("RegPianoMP_MPAttivitaPluriAnnuali.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 6", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianoMP_MPDateStagionali.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 7", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<MPDateStagionali> MPDateStagionalis
        {
            get
            {
                return GetCollection<MPDateStagionali>("MPDateStagionalis");
            }
        }

        [Association(@"RegPianoMP_MPDataIniziale", typeof(MPDataIniziale)), DisplayName("Impostazioni Iniziali")]
        // [Appearance("RegPianoMP_MPDataIniziale.Enable", @"[MPStatoPianificazione.Oid] != 7", Enabled = false)]
        [Appearance("RegPianoMP_MPDataIniziale.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 8", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<MPDataIniziale> MPDataIniziales
        {
            get
            {
                return GetCollection<MPDataIniziale>("MPDataIniziales");
            }
        }

        [Association(@"RegPianoMP_PoliMansioni", typeof(MPRegPoliMansioniCarico)), DisplayName("Gruppo Mansioni")] 
        //[Appearance("RegPianoMP_PoliMansioni.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 9", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianoMP_PoliMansioni.HideLayoutItem", AppearanceItemType.LayoutItem, @"9 = 9", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<MPRegPoliMansioniCarico> MPRegPoliMansioniCaricos
        {
            get
            {
                return GetCollection<MPRegPoliMansioniCarico>("MPRegPoliMansioniCaricos");
            }
        }

        [Association(@"RegPianiMP_Ghost", typeof(Ghost)), DevExpress.ExpressApp.DC.XafDisplayName("Ghost Creati in Schedulazione")]
        [Appearance("RegPianoMP_RPMPGhosts.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 12", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<Ghost> RPMPGhosts
        {
            get
            {
                return GetCollection<Ghost>("RPMPGhosts");
            }
        }
        // apparato schede MP 
        //[Association(@"RegPianiMP_Ghost", typeof(ApparatoSchedaMP))]
        [  DevExpress.ExpressApp.DC.XafDisplayName("ApparatoSchedaMPs  ")]
        [Appearance("RegPianoMP_ApparatoSchedaMP.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 9", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<AssetSchedaMP> ApparatoSchedaMP
        {
            get
            {
                XPCollection<AssetSchedaMP> deliveries = new XPCollection<AssetSchedaMP>(Session, CriteriaOperator.Parse("RegistroPianificazioneMP = ?", this));
                return deliveries;// GetCollection<ApparatoSchedaMP>("ApparatoSchedaMP");
            }
        }

        [Association(@"RegPianiMP_RegMPSchedaMP", typeof(RegMPSchedaMP)), DevExpress.ExpressApp.DC.XafDisplayName("SchedaMP")]
        [Appearance("RegPianiMP_RegMPSchedaMP.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 9", Visibility = ViewItemVisibility.Hide)]
        public XPCollection<RegMPSchedaMP> RegMPSchedaMPs
        {
            get
            {
                return GetCollection<RegMPSchedaMP>("RegMPSchedaMPs");
            }
        }


        private DateTime fDatapianificazione;
        [Persistent("DATAPIANIFICAZIONE"), DisplayName("Data Pianificazione Creazione Ordini di Lavoro")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy")]
        [Appearance("RegPianiMP.Datapianificazione.Enabled", Enabled = false)]
        [Appearance("RegPianiMP_Datapianificazione.HideLayoutItem", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 124", Visibility = ViewItemVisibility.Hide)]
        [VisibleInDetailView(true),VisibleInListView(false),VisibleInLookupListView(false)]
        public DateTime Datapianificazione
        {
            get
            {
                return fDatapianificazione;
            }
            set
            {
                SetPropertyValue<DateTime>("Datapianificazione", ref fDatapianificazione, value);
            }
        }
        private string fMessaggioSchedulazione;
        [Persistent("MESSAGGIOSCHEDULAZIONEODL"), Size(250), DisplayName("Esito Creazione OdL")]
        [DbType("varchar(250)")]
        [Appearance("RegPianiMP.MessaggioSchedulazione.Hide", AppearanceItemType.LayoutItem, @"[MPStatoPianificazione.Oid] != 124", Visibility = ViewItemVisibility.Hide)]
        [Appearance("RegPianiMP.MessaggioSchedulazione.Enabled", Enabled = false)]
        [VisibleInDetailView(true),      VisibleInListView(false),      VisibleInLookupListView(false)]
        public string MessaggioSchedulazione
        {
            get
            {
                return fMessaggioSchedulazione;
            }
            set
            {
                SetPropertyValue<string>("MessaggioSchedulazione", ref fMessaggioSchedulazione, value);
            }
        }
               
        private StatoElaborazioneJob fStatoElaborazioneJob;
        [Persistent("STATOELABORAZIONEJOB"),
        DisplayName("Stato Elaborazione Job")]
        public StatoElaborazioneJob StatoElaborazioneJob
        {
            get
            {
                return fStatoElaborazioneJob;
            }
            set
            {
                SetPropertyValue<StatoElaborazioneJob>("StatoElaborazioneJob", ref fStatoElaborazioneJob, value);
            }
        }

        //  imposta se lassociazione della risorsa è per mansione o per skill
        private TipoAssociazioneGostRisorseTeam fTipoAssociazioneGostRisorseTeam;
        [Persistent("TIPOCAMBIORISORSA"),
        DisplayName("Tipo Associazione Ghost Risorse")]
        [MemberDesignTimeVisibility(false)]
        //[ImmediatePostData]
        public TipoAssociazioneGostRisorseTeam TipoAssociazioneGostRisorseTeam
        {
            get
            {
                return fTipoAssociazioneGostRisorseTeam;
            }
            set
            {
                SetPropertyValue<TipoAssociazioneGostRisorseTeam>("TipoAssociazioneGostRisorseTeam", ref fTipoAssociazioneGostRisorseTeam, value);
            }
        }
        #endregion

        #region Metodi Save  E CHANGE

        protected override void OnLoaded()
        {
            base.OnLoaded();
            if (IsInvalidated)
                return;



        }

        protected override void OnSaving()
        {
            base.OnSaving();
            if (Session.IsNewObject(this))
            {
                return;
            }
            else
            {
                if (this.MPStatoPianificazione != null &&  this.MPStatoPianificazione.Oid < 3)
                    if (this.AnnoSchedulazione != null && this.Scenario != null)
                        this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(2);
            }
        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);
            if (this.MPStatoPianificazione != null)
            {
                if (this.MPStatoPianificazione.Oid < 3)
                {
                    if (newValue != null && propertyName == "AnnoSchedulazione")
                    {
                        Anni newV = (Anni)(newValue);
                        if (newV.Oid > 2014 && newV.Oid < 2026)
                        {
                            if (this.Scenario != null)
                            {
                                this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(2);
                            }
                            else
                            {
                                this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(1);
                                if (Scenario == null)
                                    this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(0);
                            }

                        }
                    }
                    if (newValue != null && propertyName == "Scenario")
                    {
                        Scenario newV = (Scenario)(newValue);
                        if (newV.ClusterEdificis.Count > 0 && newV.CentroOperativo != null)
                        {
                            if (this.Scenario != null)
                            {

                                this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(2);
                            }
                            else
                            {
                                this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(1);
                                if (AnnoSchedulazione == null)
                                    this.MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(0);
                            }
                        }
                    }

                }
                if (this.MPStatoPianificazione.Oid > 3)
                {
                    if (newValue != null && propertyName == "MPStatoPianificazione")
                    {
                        MPStatoPianificazione newV = (MPStatoPianificazione)(newValue);
                        if (newV.Oid > 3)
                        {
                            accendiprimo = true;
                            accendisecondo = true;
                            accenditerzo = true;

                            if (newV.Oid == 4)
                                accendiprimo = false;

                            if (newV.Oid == 5)
                                accendisecondo = false;

                            if (newV.Oid == 6)
                                accenditerzo = false;
                        }
                    }


                }
            }
        }

        [MemberDesignTimeVisibility(false)]
        bool accendiprimo { get; set; }
        [MemberDesignTimeVisibility(false)]
        bool accendisecondo { get; set; }
        [MemberDesignTimeVisibility(false)]
        bool accenditerzo { get; set; }

        #endregion

        #region Vecchi  -  RegVincoli
        //private RegVincoli fRegVincoli;
        //[Persistent("REGVINCOLI"), DisplayName("Registro Vincoli")]
        //[Appearance("RegPianiMP.RegVincoli.Enabled", Enabled = false, Criteria = "[MPStatoPianificazione.Oid] > 9 Or Scenario = null", Context = "DetailView", Priority = 1)]
        //[MemberDesignTimeVisibility(false)]
        //public RegVincoli RegVincoli
        //{
        //    get
        //    {
        //        return fRegVincoli;
        //    }
        //    set
        //    {
        //        SetPropertyValue<RegVincoli>("RegVincoli", ref fRegVincoli, value);
        //    }
        //}
        #endregion

    }
}


//[PersistentAlias("MPStatoPianificazione.SettimanaSk"),
//DisplayName("SettimanaSk"),
//VisibleInDetailView(false),
//VisibleInListView(false),
//VisibleInLookupListView(false)]
//public int SettimanaSk
//{
//    get
//    {
//        var tempObject = EvaluateAlias("SettimanaSk");
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


//private int fSettimana;
//      [Appearance("RegPianiMP.Settimana.Hide", Visibility = ViewItemVisibility.Hide)]
//      [VisibleInDetailView(false),
//     VisibleInListView(false),
//     VisibleInLookupListView(false),
//     ImmediatePostData]
//      [Persistent("SETTIMANA"),
//      DisplayName("Settimana Pianificata")]
//      public int Settimana
//      {
//          get
//          {
//              return fSettimana;
//          }
//          set
//          {
//              SetPropertyValue<int>("Settimana", ref fSettimana, value);
//          }
//      }
//protected override void OnSaving()
//{
//    var OidStato = 0;
//    if (MPStatoPianificazione == null)
//    {
//        OidStato = 0;
//    }
//    //else
//    //{
//    //    OidStato = MPStatoPianificazione.Oid;
//    //}

//    //if (Session.IsNewObject(this))
//    //{
//    //    if (Descrizione != null)
//    //    {
//    //        OidStato = 1;
//    //    }
//    //    //if (Descrizione != null && AnnoSchedulazione != null)
//    //    //{
//    //    //    OidStato = 3;
//    //    //}
//    //    //if (Descrizione != null && AnnoSchedulazione != null && Scenario != null)
//    //    //{
//    //    //    OidStato = 6;
//    //    //}
//    //    //if (Descrizione != null && AnnoSchedulazione != null && Scenario != null && RegVincoli != null)
//    //    //{
//    //    //    OidStato = 9;
//    //    //}
//    //}

//    //if (!IsDeleted && MPStatoPianificazione.Oid < 10)
//    //{
//    //    if (Descrizione != null)
//    //    {
//    //        OidStato = 1;
//    //    }
//    //    //if (Descrizione != null && AnnoSchedulazione != null)
//    //    //{
//    //    //    OidStato = 3;
//    //    //}
//    //    //if (Descrizione != null && AnnoSchedulazione != null && Scenario != null)
//    //    //{
//    //    //    OidStato = 6;
//    //    //}
//    //    //if (Descrizione != null && AnnoSchedulazione != null && Scenario != null && RegVincoli != null)
//    //    //{
//    //    //    OidStato = 9;
//    //    //}
//    //}

//    //MPStatoPianificazione = Session.GetObjectByKey<MPStatoPianificazione>(OidStato);
//}

























































































