//RegRdLListViewAzioniMassiveMaster

using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.SystemModule;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using CAMS.Module.DBPlant;


namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_REGRDL_LIST_MASSIVE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Azioni Massive Registri RdL")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone

    [ListViewFilter("RegRdLListViewAzioniMassive.MPM_nC.1-5-4", "OidCategoria In(1,5) And OidSmistamento In(11,6)", "Manutenzione Programmata non Completate", true, Index = 1)]
    [ListViewFilter("RegRdLListViewAzioniMassive.MC_nC.4-4", "OidCategoria  = 4 And OidSmistamento In(11,6)", "Manutenzione Guasto non Completate", Index = 2)]
    [ListViewFilter("RegRdLListViewAzioniMassive.MC_nC.3-4", "OidCategoria  = 3 And OidSmistamento In(11,6)", "Manutenzione a Condizione non Completate", Index = 3)]
    [ListViewFilter("RegRdLListViewAzioniMassive.MC_nC.2-4", "OidCategoria  = 2 And OidSmistamento In(11,6)", "Manutenzione Conduzione non Completate", Index = 4)]

    //[ListViewFilter("RegRdLListViewAzioniMassive.MPM_C.1-5-4", "OidCategoria In(1,5) And OidSmistamento = 4", "Manutenzione Programmata Completate", Index = 5)]
    //[ListViewFilter("RegRdLListViewAzioniMassive.MC_C.4-4", "OidCategoria  = 4 And OidSmistamento = 4", "Manutenzione Guasto Completate", Index = 6)]
    //[ListViewFilter("RegRdLListViewAzioniMassive.MC_C.3-4", "OidCategoria  = 3 And OidSmistamento = 4", "Manutenzione a Condizione Completate", Index = 7)]
    //[ListViewFilter("RegRdLListViewAzioniMassive.MC_C.2-4", "OidCategoria  = 2 And OidSmistamento = 4", "Manutenzione Conduzione Completate", Index = 8)]

   // [ListViewFilter("RegRdLListViewAzioniMassiveutto", "", "Tutto",  Index = 9)]

    #endregion

    public class RegRdLListViewAzioniMassive : XPLiteObject
    {
        public RegRdLListViewAzioniMassive() : base() { }
        public RegRdLListViewAzioniMassive(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Appearance("RegRdLListViewAzioniMassive.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1,10, 12)", BackColor = "Yellow")]
        [Appearance("RegRdLListViewAzioniMassive.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,11)", BackColor = "LightGreen")]
        //[Appearance("RegRdLListViewAzioniMassive.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RegRdLListViewAzioniMassive.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6)", BackColor = "LightSteelBlue")]
        public int Codice
        {
            get
            {

                return fcodice;
            }
            set
            {
                SetPropertyValue<int>("Codice", ref fcodice, value);
            }
        }


        private string fDescrizione;
        [Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
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


        private string fCommesse;
        [Persistent("COMMESSE"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Commesse
        {
            get
            {
                return fCommesse;
            }
            set
            {
                SetPropertyValue<string>("Commesse", ref fCommesse, value);
            }
        }


        private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Immobile
        {
            get
            {
                return fEdificio;
            }
            set
            {
                SetPropertyValue<string>("Immobile", ref fEdificio, value);
            }
        }


        
        private string fCodEdificio;
        [Persistent("CODEDIFICIO"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string CodEdificio
        {
            get
            {
                return fCodEdificio;
            }
            set
            {
                SetPropertyValue<string>("CodEdificio", ref fCodEdificio, value);
            }
        }


        //private CentroOperativo FCentroOperativo;
        //[Persistent("CENTROOPERATIVO"), System.ComponentModel.DisplayName("Centroperativo")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public CentroOperativo CentroOperativo
        //{
        //get
        //    {
        //        return FCentroOperativo;
        // }
        //set
        //    {
        //SetPropertyValue<CentroOperativo>("CentroOperativo", ref FCentroOperativo, value);
        //}
        //}


        private string FCentroOperativo;
        [Persistent("CENTROOPERATIVO"), System.ComponentModel.DisplayName("Centroperativo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        public string CentroOperativo
        {
            get
            {
                return FCentroOperativo;
            }
            set
            {
                SetPropertyValue<string>("CentroOperativo", ref FCentroOperativo, value);
            }
        }

       
        //private string fComune;
        //[Persistent("COMUNE"), System.ComponentModel.DisplayName("Comune")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Comune
        //{
        //    get
        //    {
        //        return fComune;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Comune", ref fComune, value);
        //    }
        //}

 



        //private string fPiano;
        //[Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Piano
        //{
        //    get
        //    {
        //        return fPiano;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Piano", ref fPiano, value);
        //    }
        //}



        //private string fStanza;
        //[Persistent("STANZA"), System.ComponentModel.DisplayName("Locale")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Stanza
        //{
        //    get
        //    {
        //        return fStanza;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Stanza", ref fStanza, value);
        //    }
        //}

        //private string fReparto;
        //[Persistent("REPARTO"), System.ComponentModel.DisplayName("Reparto")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Reparto
        //{
        //    get
        //    {
        //        return fReparto;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Reparto", ref fReparto, value);
        //    }
        //}


        private string fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Impianto
        {
            get
            {
                return fImpianto;
            }
            set
            {
                SetPropertyValue<string>("Impianto", ref fImpianto, value);
            }
        }

        private string fApparato;
        [Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Apparato
        {
            get
            {
                return fApparato;
            }
            set
            {
                SetPropertyValue<string>("Apparato", ref fApparato, value);
            }
        }

        //private string fTipoApparato;
        //[Persistent("TIPOAPPARATO"), System.ComponentModel.DisplayName("Tipo Apparato")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        //public string TipoApparato
        //{
        //    get
        //    {
        //        return fTipoApparato;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("TipoApparato", ref fTipoApparato, value);
        //    }
        //}
        private string fCategoria;
        [Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Categoria
        {
            get
            {
                return fCategoria;
            }
            set
            {
                SetPropertyValue<string>("Categoria", ref fCategoria, value);
            }
        }

        private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Team
        {
            get
            {
                return fTeam;
            }
            set
            {
                SetPropertyValue<string>("Team", ref fTeam, value);
            }
        }

        private string fStatoSmistamento;
        [Persistent("ULTIMOSTATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string StatoSmistamento
        {
            get
            {
                return fStatoSmistamento;
            }
            set
            {
                SetPropertyValue<string>("StatoSmistamento", ref fStatoSmistamento, value);
            }
        }

        //private string fStatoOperativo;
        //[Persistent("ULTIMOSTATOOPERATIVO"), System.ComponentModel.DisplayName("StatoOperativo")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string StatoOperativo
        //{
        //    get
        //    {
        //        return fStatoOperativo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("StatoOperativo", ref fStatoOperativo, value);
        //    }
        //}
       
        #region Date
        //private DateTime fDataCreazione;
        //[Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data Creazione dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public DateTime DataCreazione
        //{
        //    get
        //    {
        //        return fDataCreazione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
        //    }
        //}

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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


        private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataPianificata
        {
            get
            {
                return fDataPianificata;
            }
            set
            {
                SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value);
            }
        }

        //private DateTime fDataAssegnazione;
        //[Persistent("DATAASSEGNAZIONE"), System.ComponentModel.DisplayName("Data Assegnazione")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public DateTime DataAssegnazione
        //{
        //    get
        //    {
        //        return fDataAssegnazione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataAssegnazione", ref fDataAssegnazione, value);
        //    }
        //}

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataCompletamento
        {
            get
            {
                return fDataCompletamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCompletamento", ref fDataCompletamento, value);
            }
        }

 

        #endregion
       

        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(50)")]
        public string Priorita
        {
            get
            {
                return fPriorita;
            }
            set
            {
                SetPropertyValue<string>("Priorita", ref fPriorita, value);
            }
        }

     
        
       
        private string fNoteCompletamento;
        [Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("NoteCompletamento")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string NoteCompletamento
        {
            get
            {
                return fNoteCompletamento;
            }
            set
            {
                SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value);
            }
        }

       
        // Area di Polo
        private string fAreadiPolo;
        [Persistent("AREADIPOLO"), System.ComponentModel.DisplayName("Area di Polo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        public string AreadiPolo
        {
            get
            {
                return fAreadiPolo;
            }
            set
            {
                SetPropertyValue<string>("AreadiPolo", ref fAreadiPolo, value);
            }
        }

        private string fCentrodiCosto;
        [Persistent("CENTROCOSTO"), System.ComponentModel.DisplayName("Centro di Costo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        public string CentrodiCosto
        {
            get
            {
                return fCentrodiCosto;
            }
            set
            {
                SetPropertyValue<string>("CentrodiCosto", ref fCentrodiCosto, value);
            }
        }


        //private string fTeamMansione;
        //[Persistent("TEAMMANSIONE"), System.ComponentModel.DisplayName("Mansione Team")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(100)")]
        //public string TeamMansione
        //{
        //    get
        //    {
        //        return fTeamMansione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("TeamMansione", ref fTeamMansione, value);
        //    }
        //}



        #region  settimana mese anno
        
        //private int fSettimana;
        //[Persistent("SETTIMANA"), System.ComponentModel.DisplayName("Settimana")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        // [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        // public int Settimana
        //{
        // get
        // {
        //    return fSettimana;
        //}
        //set
        //{
        //SetPropertyValue<int>("Settimana", ref fSettimana, value);
        //}
        // }

        private int fMese;
        [Persistent("MESE"), System.ComponentModel.DisplayName("Mese")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
            }
        }

        private int fAnno;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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

        #endregion

        private string fUtente;
        [Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Utente
        {
            get
            {
                return fUtente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref fUtente, value);
            }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAAGGIORNAMENTO"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Aggiornamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
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

        private int fOidCommesse;
        [Persistent("OIDCOMMESSE"), System.ComponentModel.DisplayName("OidCommesse")]
        [MemberDesignTimeVisibility(true)]
        public int OidCommesse
        {
            get
            {
                return fOidCommesse;
            }
            set
            {
                SetPropertyValue<int>("OidCommesse", ref fOidCommesse, value);
            }
        }

        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [MemberDesignTimeVisibility(true)]
        public int OidEdificio
        {
            get
            {
                return fOidEdificio;
            }
            set
            {
                SetPropertyValue<int>("OidEdificio", ref fOidEdificio, value);
            }
        }
        private int FOidCentroOperativo;
        [Persistent("OIDCENTROOPERATIVO"), System.ComponentModel.DisplayName("OidCentroperativo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        public int OidCentroOperativo
        {
            get
            {
                return FOidCentroOperativo;
            }
            set
            {
                SetPropertyValue<int>("OidCentroOperativo", ref FOidCentroOperativo, value);
            }
        }



        //private int fOidReferenteCofely;
        //[Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        //[MemberDesignTimeVisibility(false)]
        //public int OidReferenteCofely
        //{
        //    get
        //    {
        //        return fOidReferenteCofely;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidReferenteCofely", ref fOidReferenteCofely, value);
        //    }
        //}
        private int fOidCategoria;
        [Persistent("OIDCATEGORIA"), System.ComponentModel.DisplayName("OidCategoria")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidCategoria
        {
            get
            {
                return fOidCategoria;
            }
            set
            {
                SetPropertyValue<int>("OidCategoria", ref fOidCategoria, value);
            }
        }

        private int fOidSmistamento;
        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidSmistamento
        {
            get
            {
                return fOidSmistamento;
            }
            set
            {
                SetPropertyValue<int>("OidSmistamento", ref fOidSmistamento, value);
            }
        }

      



    }

}


