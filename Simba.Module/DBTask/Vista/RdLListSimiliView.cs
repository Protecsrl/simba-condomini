using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LIST_SIMILI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Elenco Richieste di Lavoro Simili")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
   // [ DevExpress.ExpressApp.DefaultListViewOptions( )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]
    public class RdLListSimiliView : XPLiteObject
    {
        public RdLListSimiliView() : base() { }
        public RdLListSimiliView(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [MemberDesignTimeVisibility(false)]
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
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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

   

        private string fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio"),Size(250)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Servizio
        {
            get
            {
                return fServizio;
            }
            set
            {
                SetPropertyValue<string>("Servizio", ref fServizio, value);
            }
        }

        private string fAsset;
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset"), Size(250)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Asset
        {
            get
            {
                return fAsset;
            }
            set
            {
                SetPropertyValue<string>("Asset", ref fAsset, value);
            }
        }

    
        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento"), Size(35)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta"), Size(30)]
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


     
      

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento"), Size(30)]
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
        private string fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente"), Size(250)]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Richiedente
        {
            get
            {
                return fRichiedente;
            }
            set
            {
                SetPropertyValue<string>("Richiedente", ref fRichiedente, value);
            }
        }

        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita"), Size(50)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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

        private string fPrioritaTIntervento;
        [Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento"), Size(50)]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(50)")]
        public string PrioritaTIntervento
        {
            get
            {
                return fPrioritaTIntervento;
            }
            set
            {
                SetPropertyValue<string>("PrioritaTIntervento", ref fPrioritaTIntervento, value);
            }
        }
        private string fNoteCompletamento;
        [Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("NoteCompletamento")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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
        private int fCodRegRdL;
        [Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public int CodRegRdL
        {
            get
            {
                return fCodRegRdL;
            }
            set
            {
                SetPropertyValue<int>("CodRegRdL", ref fCodRegRdL, value);
            }
        }

        //private int fCodOdL;
        //[Persistent("CODICEODL"), System.ComponentModel.DisplayName("CodOdL")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public int CodOdL
        //{
        //    get
        //    {
        //        return fCodOdL;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CodOdL", ref fCodOdL, value);
        //    }
        //}

    

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

        //private DateTime fDataAggiornamento;
        //[Persistent("DATAAGGIORNAMENTO"), System.ComponentModel.DisplayName("Data Aggiornamento")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data Aggiornamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[System.ComponentModel.Browsable(false)]
        //public DateTime DataAggiornamento
        //{
        //    get
        //    {
        //        return fDataAggiornamento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
        //    }
        //}


        #region  oid delle classe

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

        //private int fOidSmistamento;
        //[Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] 
        //[MemberDesignTimeVisibility(false)]
        //public int OidSmistamento
        //{
        //    get
        //    {
        //        return fOidSmistamento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidSmistamento", ref fOidSmistamento, value);
        //    }
        //}

        //private int fOidTipoIntervento;
        //[Persistent("OIDTIPOINTERVENTO"), System.ComponentModel.DisplayName("OidTipoIntervento")] 
        //[MemberDesignTimeVisibility(false)]
        //public int OidTipoIntervento
        //{
        //    get
        //    {
        //        return fOidTipoIntervento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidTipoIntervento", ref fOidTipoIntervento, value);
        //    }
        //}

        //private int fOidPriorita;
        //[Persistent("OIDPRIORITA"), System.ComponentModel.DisplayName("OidPriorita")] 
        //[MemberDesignTimeVisibility(false)]
        //public int OidPriorita
        //{
        //    get
        //    {
        //        return fOidPriorita;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidPriorita", ref fOidPriorita, value);
        //    }
        //}

        //private int fOidRichiedente;
        //[Persistent("OIDRICHIEDENTE"), System.ComponentModel.DisplayName("OidRichiedente")] 
        //[MemberDesignTimeVisibility(false)]
        //public int OidRichiedente
        //{
        //    get
        //    {
        //        return fOidRichiedente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidRichiedente", ref fOidRichiedente, value);
        //    }
        //}

        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")] 
        [MemberDesignTimeVisibility(false)]
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

        private int fOidImpianto;
        [Persistent("OIDIMPIANTO"), System.ComponentModel.DisplayName("OidImpianto")] 
        [MemberDesignTimeVisibility(false)]
        public int OidImpianto
        {
            get
            {
                return fOidImpianto;
            }
            set
            {
                SetPropertyValue<int>("OidImpianto", ref fOidImpianto, value);
            }
        }

        private int fOidApparato;
        [Persistent("OIDAPPARATO"), System.ComponentModel.DisplayName("OidApparato")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidApparato
        {
            get
            {
                return fOidApparato;
            }
            set
            {
                SetPropertyValue<int>("OidApparato", ref fOidApparato, value);
            }
        }
        #endregion
    }

}



//select t.oid as Codice,
//       e.descrizione as Immobile,
//       i.descrizione as Impianto,
//       a.descrizione as Apparato,
//       ap.descrizione || '(' || ap.cod_uni || ')' as TipoApparato,      
//       c.descrizione as CategoriaManutenzione,
//       t.descrizione,
//       t.utenteinserimento as Utente,
//       t.datacreazione,
//       t.datarichiesta,
//       t.dataupdate dataaggiornamento,    
//       t.datacompletamento,
//       /*t.datariavvio
//       t.datafermo,*/
//       --rt.team,
//       sm.statosmistamento,     
//       --t.notecompletamento,
//       ri.nomecognome      as Richiedente,
//       p.descrizione       as Priorita,
//       tp.descrizione      as PrioritaTipoIntervento,
//       t.odl    as CodiceOdL,
//       t.regrdl as CodRegRdL,
//       t.immobile as Oidedificio,
//       t.impianto as oidimpianto,
//       t.apparato as oidapparato,



//       t.tipointervento as oidtipointervento,
