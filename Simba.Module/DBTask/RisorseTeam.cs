using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBPlanner;
using System.Linq;
using System.Collections.Generic;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.DBPlant;
using DevExpress.Data.Filtering;
using CAMS.Module.DBTask.AppsCAMS;
using DevExpress.ExpressApp;
using System.Drawing;
using CAMS.Module.PropertyEditors;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RISORSETEAM")]
    [Indices("DataLetturaMG")]
    [System.ComponentModel.DefaultProperty("FullName")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Team di Risorse")]
    //Conduttore   RisorseTeam_LookupListView_GestioneRDL
    [Appearance("RisirseTeam.COnduttore.evidenzia.FontColorRed", AppearanceItemType = "ViewItem", TargetItems = "*",
    Context = "RisorseTeam_LookupListView_GestioneRDL", Criteria = "Conduttore", BackColor = "Yellow", FontColor = "Black", Enabled = false)]
    [ImageName("TeamRisorse")]
    [NavigationItem("Ticket")]

    public class RisorseTeam : XPObject
    {
        public RisorseTeam()
            : base()
        {
        }

        public RisorseTeam(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        /// <summary>
        /// Link a RegistroRdL
        /// </summary>
        ///    , CENTROOPERATIVO, OIDCENTROOPERATIVO, OIDRISORSATEAM, RISORSACAPOSQUADRA, MANSIONE, NRATTAGENDA, NRATTSOSPESE, TELEFONO
        /// <param name="RegistroRdLSelezionato"></param>
        /// <returns></returns>
        public RisorseTeam LinkRdLFrom(RegistroRdL RegistroRdLSelezionato)
        {
            var lstRegistroRdLSelezionati = new List<RegistroRdL>();
            lstRegistroRdLSelezionati.Add(RegistroRdLSelezionato);
            return LinkRdLFrom(lstRegistroRdLSelezionati);
        }
        /// <summary>
        /// Link al RDL
        /// </summary>
        /// <param name="lstRegistroRdLSelezionati"></param>
        /// <returns></returns>
        public RisorseTeam LinkRdLFrom(IEnumerable<RegistroRdL> lstRegistroRdLSelezionati)
        {
            var IDs = lstRegistroRdLSelezionati.Select(r => r.Oid).ToList();
            var lstRegistri = Session.Query<RegistroRdL>().Where(r => IDs.Contains(r.Oid));

            return this;
        }


        //public double EdificioLatitudineCalcDistanza { get; set; }
        //public double EdificioLongitudineCalcDistanza { get; set; }

        private StatoOperativo fUltimoStatoOperativo;
        [Persistent("ULTIMOSTATOOPERATIVO"), DisplayName("Ultimo stato operativo")]
        [Appearance("RisirseTeam.UltimoStatoOperativo", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public StatoOperativo UltimoStatoOperativo
        {
            get
            {
                return GetDelayedPropertyValue<StatoOperativo>("UltimoStatoOperativo");
            }
            set
            {
                SetDelayedPropertyValue<StatoOperativo>("UltimoStatoOperativo", value);
            }
        }

        private int fNumeroAttivitaSospese;
        // [NonPersistent]
        [DisplayName("Numero di attività sospese")]
        //  [PersistentAlias("RdLs[UltimoStatoSmistamento.Oid == 3 And UltimoStatoOperativo.Oid In(6, 7, 8, 9, 10)].Count")]
        // "[<Immobile>][^.Oid = Indirizzo And ClusterEdifici.Oid = {0}].Count() > 0"
        [PersistentAlias("[<RdL>][^.Oid = RisorseTeam And UltimoStatoSmistamento.Oid = 3 And UltimoStatoOperativo.Oid In(6, 7, 8, 9, 10)].Count")]
        public int NumeroAttivitaSospese
        {
            get
            {
                var tempObject = EvaluateAlias("NumeroAttivitaSospese");
                if (tempObject != null)
                {
                    return (int)tempObject;
                }
                else
                {
                    return 0;
                }
                //var conta = 0;
                //if (RdLs.Count > 0)
                //{
                //    6,7,8,9,10 - sospeso
                //    conta = RdLs.Cast<RdL>()
                //                .Where(r => r.UltimoStatoOperativo != null && r.UltimoStatoOperativo != null)
                //                .Where(r1 => r1.UltimoStatoSmistamento.Oid == 3)
                //                .Where(reg => new[] { 6, 7, 8, 9, 10 }.Contains(reg.UltimoStatoOperativo.Oid)).Count();
                //}
                //return conta;
            }
            //set
            //{
            //    SetPropertyValue<int>("NumeroAttivitaSospese", ref fNumeroAttivitaSospese, value);
            //}
        }

        private int fNumeroAttivitaAgenda;
        //[NonPersistent]
        [DisplayName("Attività in Agenda")]
        //[PersistentAlias("RdLs[UltimoStatoSmistamento.Oid == 2 And UltimoStatoOperativo.Oid == 19].Count")]
        [PersistentAlias("[<RdL>][^.Oid = RisorseTeam And UltimoStatoSmistamento.Oid = 2 And UltimoStatoOperativo.Oid = 19 And DataPianificata < AddMonths(LocalDateTimeToday(), 1) ].Count")]
        public int NumeroAttivitaAgenda
        {
            get
            {
                var tempObject = EvaluateAlias("NumeroAttivitaAgenda");
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

        //     private int fNumeroAttivitaEmergenza;
        // [NonPersistent ]

        //nrMT = xpoSession.Query<NotificheEmergenze>()
        //                .Where(w => w.Team.Oid == teamID)
        //                .Where(w => w.RegNotificheEmergenze.RegStatoNotifica == RegStatiNotificaEmergenza.daAssegnare ||
        //                w.RegNotificheEmergenze.RegStatoNotifica == RegStatiNotificaEmergenza.daAssegnare_Ripetuto)
        //                .Where(reg => new[] { 10, 12 }.Contains(reg.RegNotificheEmergenze.RegistroRdL.UltimoStatoSmistamento.Oid))
        //                .Where(reg =>
        //                reg.StatoNotifica == StatiNotificaEmergenza.NonVisualizzato ||
        //                reg.StatoNotifica == StatiNotificaEmergenza.Visualizzato

        //public enum RegStatiNotificaEmergenza
        //{
        //    [ImageName("State_Task_WaitingForSomeoneElse")]
        //    [XafDisplayName("da Assegnare")]
        //    daAssegnare,
        //    [ImageName("State_Task_NotStarted")]
        //    [XafDisplayName("Assegnato")]
        //    Assegnato,
        //    [ImageName("State_Task_Completed")]
        //    [XafDisplayName("Completato")]
        //    Completato,
        //    [ImageName("State_Task_Completed")]
        //    [XafDisplayName("Rifutato")]
        //    Rifutato,
        //    [ImageName("State_Task_WaitingForSomeoneElse")]
        //    [XafDisplayName("da Assegnare (ripetuto)")]
        //    daAssegnare_Ripetuto,
        //    [ImageName("State_Task_Completed")]
        //    [XafDisplayName("Annullato")]
        //    Annullato

        //}

        [DisplayName("Attività in Emergenza")]
        [PersistentAlias("[<NotificheEmergenze>][^.Oid = Team And (StatoNotifica = 1 Or StatoNotifica = 0 Or StatoNotifica = 4)].Count")]
        //[PersistentAlias("[<RdL>][^.Oid = RisorseTeam And UltimoStatoSmistamento.Oid = 2 And UltimoStatoOperativo.Oid = 19].Count()]")]
        public string NumeroAttivitaEmergenza  // int NumeroAttivitaEmergenza
        {
            get
            {
                var tempObject = EvaluateAlias("NumeroAttivitaEmergenza");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return "0";
                }


                //var conta = 0;
                //if (RdLs.Count > 0)
                //{
                //    conta = RdLs.Where(r => r.UltimoStatoSmistamento != null)
                //                           .Where(r => r.UltimoStatoSmistamento.Oid == 10).Count();
                //}
                //return conta;
            }
        }
        private Risorse fRisorsaCapo;
        [Persistent("RISORSACAPO"), DisplayName("Risorsa Capo Squadra")]
        [Appearance("RisirseTeam.RisorsaCapo", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Risorse RisorsaCapo
        {
            get
            {
                return GetDelayedPropertyValue<Risorse>("RisorsaCapo");
            }
            set
            {
                SetDelayedPropertyValue<Risorse>("RisorsaCapo", value);
            }

        }

        private string fTelefono;
        [PersistentAlias("Iif(RisorsaCapo != null,RisorsaCapo.Telefono,'NA')"), DisplayName("Telefono")]
        [Appearance("RisirseTeam.Telefono", Enabled = false)]
        public string Telefono
        {
            get
            {
                var tempObject = EvaluateAlias("Telefono");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return "NA";
                }
            }

        }


        private RegistroRdL fRegistroRdL;
        [Persistent("REGRDL"), DisplayName("Reg.RdL in Lavorazione")]
        [Appearance("RisirseTeam.RegistroRdL", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return GetDelayedPropertyValue<RegistroRdL>("RegistroRdL");
            }
            set
            {
                SetDelayedPropertyValue<RegistroRdL>("RegistroRdL", value);
            }

        }

        private Ghost fGhost;
        [Persistent("MPGHOST"),
        DisplayName("Ghost Linkato")]
        //[Association(@"RisorseTeam_Ghost")]
        [Appearance("RisirseTeam.Ghost", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Ghost Ghost
        {
            get
            {
                return GetDelayedPropertyValue<Ghost>("Ghost");
            }
            set
            {
                SetDelayedPropertyValue<Ghost>("Ghost", value);
            }
        }

        private int fAnno;
        [Persistent("ANNO"), DisplayName("Anno"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [Appearance("RisirseTeam.Anno", Enabled = false)]
        public int Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<int>("Anno", ref fAnno, value);
            }
        }

        private Color color;
        [ValueConverter(typeof(ColorValueConverter))]
        [Persistent("COLORE")]
        public Color Color
        {
            get { return color; }
            set { SetPropertyValue("Color", ref color, value); }
        }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        private DateTime fDataLetturaMG;
        [Persistent("DATALETTURAMG"), DevExpress.ExpressApp.DC.XafDisplayName("Data Lettura MG")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Lettura in degli interventi in App", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInDetailView(true)]
        //[Delayed(true)]
        public DateTime DataLetturaMG
        {
            //get { return GetDelayedPropertyValue<DateTime>("DataLetturaMG"); }
            //set { SetDelayedPropertyValue<DateTime>("DataLetturaMG", value); }
            get { return fDataLetturaMG; }
            set { SetPropertyValue<DateTime>("DataLetturaMG", ref fDataLetturaMG, value); }
        }

        private DateTime fDataLogInGiorno;
        [Persistent("DATAORALOGIN"), DevExpress.ExpressApp.DC.XafDisplayName("Data Ora Login")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data e Ora giornaliera di prima connessione con App", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInDetailView(true)]
        //[Delayed(true)]
        public DateTime DataLogInGiorno
        {
            //get { return GetDelayedPropertyValue<DateTime>("DataLogInGiorno"); }
            //set { SetDelayedPropertyValue<DateTime>("DataLogInGiorno", value); }
            get { return fDataLogInGiorno; }
            set { SetPropertyValue<DateTime>("DataLogInGiorno", ref fDataLogInGiorno, value); }
        }

        private DateTime fDataOraUpdateGiorno;
        [Persistent("DATAORAUPDATE"), DevExpress.ExpressApp.DC.XafDisplayName("Data Ora Ultimo Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data e Ora giornaliera di ultimo aggiornamento dell'App", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInListView(false), VisibleInDetailView(true)]
        //[Delayed(true)]
        public DateTime DataOraUpdateGiorno
        {
            //get { return GetDelayedPropertyValue<DateTime>("DataOraUpdateGiorno"); }
            //set { SetDelayedPropertyValue<DateTime>("DataOraUpdateGiorno", value); }
            get { return fDataOraUpdateGiorno; }
            set { SetPropertyValue<DateTime>("DataOraUpdateGiorno", ref fDataOraUpdateGiorno, value); }
        }


        [Persistent("CONNESSO"), DevExpress.ExpressApp.DC.XafDisplayName("Connesso")]
        public Boolean Connesso
        {
            get { return GetDelayedPropertyValue<Boolean>("Connesso"); }
            set { SetDelayedPropertyValue<Boolean>("Connesso", value); }

        }

        //[Persistent("STATOCONNESSIONE"), DevExpress.ExpressApp.DC.XafDisplayName("Stato Connessione")]
        //public StatoConnessione StatoConnessione
        //{
        //    get { return GetDelayedPropertyValue<StatoConnessione>("StatoConnessione"); }
        //    set { SetDelayedPropertyValue<StatoConnessione>("StatoConnessione", value); }

        //}
        //     [XafDisplayName("Connesso non in Lavorazione")]       //Connesso,
        //[XafDisplayName("Connesso in Lavorazione")]        //ConnessoinLavorazione,
        //[XafDisplayName("Connesso in Pausa")]        //ConnessoinPausa,
        //[XafDisplayName("Connesso in Supporto a Risorsa")]        //ConnessoinSupportoaRisorsa

        private TipoStatoConnessione fTipoStatoConnessione;
        [DevExpress.ExpressApp.DC.XafDisplayName("Tipo Stato Connessione"), ToolTip(" Tipo Stato Connessione")]
        //[ModelDefault("EditMask", "(000)-00")] 
        [VisibleInListView(false)]
        [Persistent("TIPOSTATOCONNESSIONE")]
        public TipoStatoConnessione TipoStatoConnessione
        {
            get { return fTipoStatoConnessione; }
            set { SetPropertyValue<TipoStatoConnessione>("TipoStatoConnessione", ref fTipoStatoConnessione, value); }
        }



        private RegistroAttivitaAccessorie fRegistroAttivitaAccessorie;
        [Persistent("REGATTIVITAACCESSORIE"),
        DisplayName("Registro Attivita Accessorie")]           //[Association(@"RisorseTeam_Ghost")]
        [Appearance("RisirseTeam.REGATTIVITAACCESSORIE", Enabled = false)]
        [Delayed(true)]
        public RegistroAttivitaAccessorie RegistroAttivitaAccessorie
        {
            get { return GetDelayedPropertyValue<RegistroAttivitaAccessorie>("RegistroAttivitaAccessorie"); }
            set { SetDelayedPropertyValue<RegistroAttivitaAccessorie>("RegistroAttivitaAccessorie", value); }
        }


        [Association(@"TEAMRISORSE_RISORSE", typeof(AssRisorseTeam)), DisplayName("Risorse Collegate")]
        [Appearance("RisirseTeam.AssRisorseTeam", Enabled = false)]
        public XPCollection<AssRisorseTeam> AssRisorseTeam
        {
            get
            {
                return GetCollection<AssRisorseTeam>("AssRisorseTeam");
            }
        }

        //[NonPersistent]
        [DisplayName("Coppia Linkata")]
        //[PersistentAlias("Iif(AssRisorseTeam.Count==2,'duePersone',Iif(AssRisorseTeam.Count==1,'unaPersona',Iif(AssRisorseTeam.Count==3,'trePersone','moltePersone')))")]
        [PersistentAlias("AssRisorseTeam.Count")]
        public TipoNumeroManutentori CoppiaLinkata
        {
            get
            {
                var tempObject = EvaluateAlias("CoppiaLinkata");
                if (tempObject != null)
                {
                    return (TipoNumeroManutentori)tempObject;
                }
                else
                {
                    return TipoNumeroManutentori.NonDefinito;
                }
            }

            //get
            //{     
            //    var TotRisosrse = TipoNumeroManutentori.NonDefinito;
            //    if (AssRisorseTeam.Count == 1)
            //    {
            //        TotRisosrse = TipoNumeroManutentori.unaPersona;
            //    }
            //    else
            //    {
            //        if (AssRisorseTeam.Count == 2)
            //        {
            //            TotRisosrse = TipoNumeroManutentori.duePersone;
            //        }
            //        else
            //        {
            //            if (AssRisorseTeam.Count > 2)
            //            {
            //                TotRisosrse = TipoNumeroManutentori.NonDefinito;
            //            }

            //        }
            //    }
            //    return TotRisosrse;
            //}
        }

        //[NonPersistent, DisplayName("Mansione")]
        [PersistentAlias("Iif(AssRisorseTeam.Count = 0 , 'NA', " +
                              "Iif(AssRisorseTeam.Count = 1 , RisorsaCapo.Mansione.Descrizione, 'Misto'))")]
        [DisplayName("Mansione")]
        public string Mansione
        {
            get
            {
                var tempObject = EvaluateAlias("Mansione");
                if (tempObject != null)
                {
                    if (tempObject.ToString() == "Misto")
                    {
                        string MansioneRisorse = string.Empty;
                        foreach (var ele in AssRisorseTeam.Where(w => w.Risorsa != null && w.Risorsa.Mansione != null)
                                                          .Select(s => s.Risorsa.Mansione.Descrizione).Distinct().ToList())
                        {
                            if (!MansioneRisorse.ToString().Contains(ele))
                            {
                                if (ele != null)
                                {
                                    if (MansioneRisorse == string.Empty)
                                    {
                                        MansioneRisorse = string.Format("{0}", ele);
                                    }
                                    else
                                    {
                                        MansioneRisorse = MansioneRisorse + string.Format(", {0}", ele);
                                    }
                                }
                            }
                        }
                        return MansioneRisorse;
                    }
                    return (string)tempObject;
                }
                else
                {
                    return "NA";
                }
            }

            //get
            //{
            //    if (this.Oid == -1) return null;
            //    if (RisorsaCapo.Mansione == null) return null;
            //    var MansioneRisorse = string.Empty;
            //    if (AssRisorseTeam.Count == 0  )
            //    {
            //        return RisorsaCapo.Mansione.Descrizione.ToString();
            //    }

            //    if (AssRisorseTeam.Count == 1)
            //    {
            //        return RisorsaCapo.Mansione.Descrizione.ToString();
            //    }
            //    else
            //    {
            //        foreach (var ele in AssRisorseTeam)
            //        {
            //            if (!MansioneRisorse.ToString().Contains(ele.Risorsa.Mansione.Descrizione))
            //            {
            //                if ( ele.Risorsa != null)
            //                {
            //                    if (MansioneRisorse == string.Empty)
            //                    {
            //                        MansioneRisorse = string.Format("{0}", ele.Risorsa.Mansione.Descrizione.ToString());
            //                    }
            //                    else
            //                    {
            //                        MansioneRisorse = MansioneRisorse + string.Format(",{0}", ele.Risorsa.Mansione.Descrizione.ToString());
            //                    }
            //                }
            //            }
            //        }
            //    }
            //    return MansioneRisorse;
            //}
        }

        [PersistentAlias("RisorsaCapo.CentroOperativo"), DisplayName("CentroOperativo")]
        [MemberDesignTimeVisibility(true)]
        public CentroOperativo CentroOperativo
        {
            get
            {
                var tempObject = EvaluateAlias("CentroOperativo");
                if (tempObject != null)
                {
                    return (CentroOperativo)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        [PersistentAlias("RisorsaCapo.SecurityUser.UserName"), DisplayName("User")]
        public string UserName
        {
            get
            {
                var tempObject = EvaluateAlias("UserName");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        /// Capacita Lavoro
        [PersistentAlias("RisorsaCapo.CentroOperativo.USLG"), DisplayName("Unità Standard Lavoro Giornaliera")]
        [Appearance("RisorsaCapo.CentroOperativo.USLG.Nascondi", Criteria = "USLG==0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        public double USLG
        {
            get
            {
                var tempObject = EvaluateAlias("USLG");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }

        [PersistentAlias("RisorsaCapo.CentroOperativo.USLS"), DisplayName("Unità Standard Lavoro Settimanale")]
        [Appearance("RisorsaCapo.CentroOperativo.USLS.Nascondi", Criteria = "USLS==0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [VisibleInListView(false)]
        public double USLS
        {
            get
            {
                var tempObject = EvaluateAlias("USLS");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else
                {
                    return 0;
                }
            }
        }


        #region  STATISTICHE MEDIA MINIMO MEDIANA MODA
        //private double fMinCapacita;
        //[Persistent("MIN"),
        //DisplayName("Min")]
        //[Appearance("TeamRisore.MinCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //[Delayed(true)]
        //public double MinCapacita
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<double>("MinCapacita");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<double>("MinCapacita", value);
        //    }
        //}

        //private double fMaxCapacita;
        //[Persistent("MAX"),
        //DisplayName("Max")]
        //[Appearance("TeamRisore.MaxCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //[Delayed(true)]
        //public double MaxCapacita
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<double>("MaxCapacita");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<double>("MaxCapacita", value);
        //    }
        //}

        //private double fMediaCapacita;
        //[Persistent("MEDIA"),
        //DisplayName("Media")]
        //[Appearance("TeamRisore.MediaCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //[Delayed(true)]
        //public double MediaCapacita
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<double>("MediaCapacita");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<double>("MediaCapacita", value);
        //    }
        //}

        //private double fModaCapacita;
        //[Persistent("MODA"),
        //DisplayName("Moda")]
        //[Appearance("TeamRisore.ModaCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //[Delayed(true)]
        //public double ModaCapacita
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<double>("ModaCapacita");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<double>("ModaCapacita", value);
        //    }
        //}

        //private double fMedianaCapacita;
        //[Persistent("MEDIANA"),
        //DisplayName("Mediana")]
        //[Appearance("TeamRisore.MedianaCapacita.Nascondi", Criteria = "Ghost Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        //[Delayed(true)]
        //public double MedianaCapacita
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<double>("MedianaCapacita");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<double>("MedianaCapacita", value);
        //    }
        //}
        #endregion
        #region  lista posizioni VISTA

        [Association(@"RisorsaTeam_RegPosizioniDettVista", typeof(RegistroPosizioniDettVista)), DisplayName("Registro Posizioni")]
        [Appearance("RisorseTeam.ListaPosizioniRisorses.Visible", Criteria = "(Oid = -1 Or RisorsaCapo  is null Or RegistroPosizioniDettVistas.Count() = 0)", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroPosizioniDettVista> RegistroPosizioniDettVistas
        {
            get
            {
                return GetCollection<RegistroPosizioniDettVista>("RegistroPosizioniDettVistas");
            }
        }
        #endregion


        ////[Association(@"TeamRisorse_GhostSet", typeof(GhostDettaglio)) ]
        //[Association(@"TeamRisorse_GhostSet", typeof(GhostDettaglio)), DisplayName("Elenco Ghost Settimanali Assegnate")]
        ////[Appearance("RisirseTeam.GhostDettaglio", Enabled = false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<GhostDettaglio> GhostDettaglios
        //{
        //    get
        //    {
        //        return GetCollection<GhostDettaglio>("GhostDettaglios");
        //    }
        //}


        //[Association(@"TeamRisorse_GhostGG", typeof(GhostDettaglioGG)), DisplayName("Elenco Ghost Giornalieri Assegnati")]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<GhostDettaglioGG> GhostDettaglioGGs
        //{
        //    get
        //    {
        //        return GetCollection<GhostDettaglioGG>("GhostDettaglioGGs");
        //    }
        //}

        //[Association(@"TEAMRISORSE_RdL", typeof(RdL)),
        //DisplayName("Elenco RdL Assegnate")]
        //[Appearance("RisirseTeam.RdL", Enabled = false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RdL> RdLs
        //{
        //    get
        //    {
        //        return GetCollection<RdL>("RdLs");
        //    }
        //}

        [Association(@"RegistroRdL_RisorseTeam", typeof(RegistroRdL)), DisplayName("Registro RdL")]
        [Appearance("RisorseTeam.RegRdL.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1 Or RegistroRdLs.Count = 0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroRdL> RegistroRdLs
        {
            get
            {
                return GetCollection<RegistroRdL>("RegistroRdLs");
            }
        }

        [Association(@"Notifiche_TeamRisorse", typeof(NotificheEmergenze)),
        DisplayName("Elenco Notifiche Emergenze")]
        [Appearance("RisirseTeam.NotificheEmergenze", Enabled = false)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<NotificheEmergenze> NotificheEmergenzes
        {
            get
            {
                return GetCollection<NotificheEmergenze>("NotificheEmergenzes");
            }
        }

        private XPCollection<RegistroAttivitaAccessorie> RegAttAccessorie_noAssociation;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        [DisplayName("Elenco Attivita Accessorie")]
        public XPCollection<RegistroAttivitaAccessorie> RegAttivitaAccessories
        {
            get
            {
                if (RegAttAccessorie_noAssociation == null)
                {
                    CriteriaOperator op = CriteriaOperator.Parse("RisorseTeam.Oid == ?", this.Oid);
                    RegAttAccessorie_noAssociation = new XPCollection<RegistroAttivitaAccessorie>(Session, op);
                    RegAttAccessorie_noAssociation.BindingBehavior = CollectionBindingBehavior.AllowNone;
                }
                return RegAttAccessorie_noAssociation;
            }
        }


        // sono quelle accettate quindi non servono 
        //[Association(@"RegNotifiche_TeamRisorse", typeof(RegNotificheEmergenze)),
        //DisplayName("Elenco Registro Notifiche Emergenze")]
        ////[Appearance("RisirseTeam.RegNotificheEmergenze", Enabled = false)]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RegNotificheEmergenze> RegNotificheEmergenzes
        //{
        //    get
        //    {
        //        return GetCollection<RegNotificheEmergenze>("RegNotificheEmergenzes");
        //    }
        //}

        #region calcolo distanze

        //private string fDistanzaImpianto;
        //[NonPersistent, DisplayName("Distanza Impianto"), Size(50)]
        //public string DistanzaImpianto
        //{
        //    get;
        //    set;
        //}

        //[NonPersistent, DisplayName("Interventi In Immobile"), Size(50)]
        //public string InterventiInEdificio
        //{
        //    get;
        //    set;
        //}

        //private Immobile fUltimoEdificio;
        //[NonPersistent, DisplayName("Ultimo Immobile Visitato"), Size(50)]
        //public Immobile UltimoEdificio
        //{
        //    get;
        //    set;
        //}


        //private string GetDistanze()
        //{
        //    double dis = 0D;
        //    if (CAMS.Module.Classi.SetVarSessione.OidEdificioCalcoloDistanze > 0 && this.RegistroPosizioniDettVistas.Count() > 0)
        //    {
        //        try
        //        {
        //            double p = EdificioLatitudineCalcDistanza;//http://www.faureragani.it/mygps/getlatlonita.html
        //            double pp = EdificioLongitudineCalcDistanza;
        //            //  Immobile ed = Session.GetObjectByKey<Immobile>(CAMS.Module.Classi.SetVarSessione.OidEdificioCalcoloDistanze);
        //            double Pa_lat1 = SetVarSessione.OidEdificioCalcoloDistanzeLatitudine;//= Pa_lat1;= Pa_lon1; double.Parse(ed.Indirizzo.GeoLat.ToString());
        //            double Pa_lon1 = SetVarSessione.OidEdificioCalcoloDistanzeLongitudine; //double.Parse(ed.Indirizzo.GeoLng.ToString());

        //            // int Massimoindice = this.ListaPosizioniRisorses.Max(m => m.Oid);
        //            //int Massimoindice = this.RegistroPosizioniDettVistas.Max(m => m.Codice);
        //            // var lp = this.RegistroPosizioniDettVistas.Where(w => w.Oid == Massimoindice).FirstOrDefault();
        //            var lp = this.RegistroPosizioniDettVistas.OrderByDescending(ob => ob.DataOra).FirstOrDefault();    //Where(w => w.Oid == Massimoindice).FirstOrDefault();
        //            double Ar_lat2 = lp.Latitude < 1 ? 0.0 : lp.Latitude;
        //            double Ar_lon2 = lp.Longitude < 1 ? 0.0 : lp.Longitude;

        //            using (CAMS.Module.Classi.Util ut = new CAMS.Module.Classi.Util())
        //            { //double Pa_lat1, double Pa_lon1, double Ar_lat2, double Ar_lon2)

        ////                dis = ut.CalcolaDistanzaInkiloMeteri(Pa_lat1, Pa_lon1, Ar_lat2, Ar_lon2);
        //            }
        //        }
        //        catch
        //        {
        //            return "par assenti";
        //        }
        //        if (dis == 0) return "non Calcolabile";
        //        if (dis > 0 && dis < 25) return "in Prossimità";
        //        if (dis > 25 && dis < 50) return "vicino";
        //        if (dis > 50 && dis < 100) return "Lontano";
        //        return "molto Lontano";
        //    }
        //    return "non calcolabile";

        //}


        //private Immobile GetUltimoEdificio()
        //{
        //    double dis = 0D;
        //    Immobile u_edificio = null;
        //    if (this.RegistroPosizioniDettVistas.Count() > 0)
        //    {
        //        try
        //        {

        //            var lp = this.RegistroPosizioniDettVistas.OrderByDescending(ob => ob.DataOra).FirstOrDefault();    //Where(w => w.Oid == Massimoindice).FirstOrDefault();
        //            return lp.Immobile;
        //        }
        //        catch
        //        {
        //            return u_edificio;
        //        }

        //        return u_edificio;
        //    }
        //    return u_edificio;

        //}


        //private string GetInterventiInEdificio()
        //{
        //    string NrInterventi = "";
        //    string Messaggio = "";
        //    if (CAMS.Module.Classi.SetVarSessione.OidEdificioCalcoloDistanze > 0 && this.RegistroPosizioniDettVistas.Count() > 0)
        //    {
        //        try
        //        {
        //            using (CAMS.Module.Classi.DB db = new CAMS.Module.Classi.DB())
        //            { //double Pa_lat1, double Pa_lon1, double Ar_lat2, double Ar_lon2)

        //                Messaggio = db.NrInterventiInEdificio(this.Oid, SetVarSessione.OidEdificioCalcoloDistanze, ref  NrInterventi);
        //                return NrInterventi;
        //            }
        //        }
        //        catch
        //        {
        //            return "par assenti";
        //        }

        //        return "par assenti";
        //    }
        //    return "non calcolabile";
        //}

        //  private Immobile fUltimoEdificio;
        //[NonPersistent, DisplayName("Intervento su Immobile"), Size(50)]
        //public Immobile EdificiosuIntervento
        //{
        //    get
        //    {
        //        Immobile ED = Session.GetObjectByKey<Immobile>(CAMS.Module.Classi.SetVarSessione.OidEdificiosuIntervento);
        //        return ED;
        //    }
        //}
        ///   @@@@@@@@@@@   27.03.218
        //[NonPersistent, DisplayName("Mansione Ghost"), Size(550)]
        //[Appearance("RTeam.StessaMansioneGhst.Red", BackColor = "Red", FontColor = "Black", Priority = 1, Criteria = "StessaMansione = 'Stesso Skill'")]
        //[VisibleInListView(false)]
        //public string StessaMansione
        //{
        //    get
        //    {
        //        return GetStessaMansioneGhost();
        //    }

        //}
        //private string GetStessaMansioneGhost()
        //{
        //    if (this.Oid == -1) return "non calcolabile";
        //    if (CAMS.Module.Classi.SetVarSessione.OidMansioneGhost == 0) return "non calcolabile";
        //    if (RisorsaCapo == null) return "non calcolabile";
        //    if (RisorsaCapo.Mansione == null) return "non calcolabile";
        //    if (CAMS.Module.Classi.SetVarSessione.OidMansioneGhost > 0)
        //    {
        //        try
        //        {
        //            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);
        //            var gho = xpObjectSpace.FindObject<Ghost>(new BinaryOperator("Oid", SetVarSessione.OidMansioneGhost));
        //            bool StessaMansione = RisorsaCapo.Mansione == gho.Mansione;
        //            if (StessaMansione)
        //                return "Stessa Mansione";
        //            else
        //                return "Stesso Skill";
        //        }
        //        catch
        //        {
        //            return "par assenti";
        //        }
        //    }
        //    return "non calcolabile";
        //}

        #endregion

        //[PersistentAlias("Iif(Data is not null, ToInt(Ceiling(ToFloat(GetDayOfYear(Data) - GetDayOfWeek(Data) - 1) / 7) + 1), -1)")]
        [PersistentAlias("RisorsaCapo.Nome + ' ' + RisorsaCapo.Cognome + Iif(RisorsaCapo.SecurityUser is not null, '(' +RisorsaCapo.SecurityUser.UserName  + ')',' ' )")]
        [DisplayName("Nome")]
        public string FullName
        {
            get
            {

                var tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return (string)tempObject;
                }
                else
                {
                    return string.Empty;
                }
            }
        }


        public override string ToString()
        {
            if (RisorsaCapo != null)
            {
                if (RisorsaCapo.SecurityUser != null)
                {
                    if (Anno > 2000)
                        return string.Format("{0}({1})", RisorsaCapo, RisorsaCapo.SecurityUser.UserName);
                    else
                        return string.Format("{0}({1}-{2})", RisorsaCapo, Anno, RisorsaCapo.SecurityUser.UserName);
                }
                else
                {
                    if (Anno < 2000)
                        return string.Format("{0}", RisorsaCapo);
                    else
                        return string.Format("{0}({1})", RisorsaCapo, Anno);
                }
            }
            return null;
        }

    }
}

