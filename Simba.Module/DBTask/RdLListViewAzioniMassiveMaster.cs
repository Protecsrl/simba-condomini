using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.SystemModule;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using CAMS.Module.Classi;
using DevExpress.Data.Filtering;
namespace CAMS.Module.DBTask
{
    [DefaultClassOptions, Persistent("RDL_LIST_MASSIVE_MASTER")]
    [VisibleInDashboards(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Master Azioni Massive")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]


    #region filtro tampone           [Utente] = CurrentUserId().UserName

    [ListViewFilter("RdLLViewAzioniMassiveMaster.MPM_nC.1-5-4", "OidCategoria In(1,5) And OidSmistamento != 4", "Manutenzione Programmata non Completate", Index = 1)]
    [ListViewFilter("RdLLViewAzioniMassiveMaster.MC_nC.4-4", "OidCategoria  = 4 And OidSmistamento != 4", "Manutenzione Guasto non Completate", Index = 2)]
    [ListViewFilter("RdLLViewAzioniMassiveMaster.MC_nC.3-4", "OidCategoria  = 3 And OidSmistamento != 4", "Manutenzione a Condizione non Completate", Index = 3)]
    [ListViewFilter("RdLLViewAzioniMassiveMaster.MC_nC.2-4", "OidCategoria  = 2 And OidSmistamento != 4", "Manutenzione Conduzione non Completate", Index = 4)]

    [ListViewFilter("RdLLViewAzioniMassiveMaster.MPM_C.1-5-4", "OidCategoria In(1,5) And OidSmistamento = 4", "Manutenzione Programmata Completate", Index = 5)]
    [ListViewFilter("RdLLViewAzioniMassiveMaster.MC_C.4-4", "OidCategoria  = 4 And OidSmistamento = 4", "Manutenzione Guasto Completate", Index = 6)]
    [ListViewFilter("RdLLViewAzioniMassiveMaster.MC_C.3-4", "OidCategoria  = 3 And OidSmistamento = 4", "Manutenzione a Condizione Completate", Index = 7)]
    [ListViewFilter("RdLLViewAzioniMassiveMaster.MC_C.2-4", "OidCategoria  = 2 And OidSmistamento = 4", "Manutenzione Conduzione Completate", Index = 8)]

    [ListViewFilter("RdLListViewAzioniMassiveMaster.Tutto", "", "Tutto", true, Index = 9)]

    #endregion
    [Appearance("RdL.RdLListViewAzioniMassiveMaster.COLORE", TargetItems = "*", Criteria = @"Congruo = 1", BackColor = "Salmon", FontColor = "Black")]  //  nrRdL
 


    public class RdLListViewAzioniMassiveMaster : XPObject
    {
                public RdLListViewAzioniMassiveMaster() : base() { }
                public RdLListViewAzioniMassiveMaster(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [  Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[Appearance("RdLListViewAzioniMassive.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1,10)", BackColor = "Yellow")]
        //[Appearance("RdLListViewAzioniMassive.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        //[Appearance("RdLListViewAzioniMassive.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        //[Appearance("RdLListViewAzioniMassive.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
        public int Codice
        {
            get
            {
 //this.Congruo
                return fcodice;
            }
            set
            {
                SetPropertyValue<int>("Codice", ref fcodice, value);
            }
        }

        private Congruo fCongruo;
        [NonPersistent, System.ComponentModel.DisplayName("Congruo")] // pm
        public Congruo Congruo
        {
            get
            {
                int contardl = this.Session.Query<RdL>()
                    .Where(W => W.Immobile.Oid == OidImmobile)
                    .Where(W => W.Categoria.Oid == OidCategoria)
                    .Where(W => W.Oid == Codice)
                    .Count();
                if (contardl > 0)
                    return Module.Classi.Congruo.Conforme;
                else
                    return Module.Classi.Congruo.nonConforme;
            }

        }
        //private string fDescrizione;
        //[Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Descrizione
        //{
        //    get
        //    {
        //        return fDescrizione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Descrizione", ref fDescrizione, value);
        //    }
        //}

        private string fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue<string>("Immobile", ref fImmobile, value);
            }
        }


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

        private string fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [Persistent("ASSET"), System.ComponentModel.DisplayName("Asset")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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



        private string fNewTeam;
        [Persistent("NEWTEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string NewTeam
        {
            get
            {
                return fNewTeam;
            }
            set
            {
                SetPropertyValue<string>("NewTeam", ref fNewTeam, value);
            }
        }

        private int fNewOidTeam;
        [Persistent("NEWOIDTEAM"), System.ComponentModel.DisplayName("OID Team")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public int NewOidTeam
        {
            get
            {
                return fNewOidTeam;
            }
            set
            {
                SetPropertyValue<int>("NewOidTeam", ref fNewOidTeam, value);
            }
        }


        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
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

        private string fStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("StatoOperativo")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string StatoOperativo
        {
            get
            {
                return fStatoOperativo;
            }
            set
            {
                SetPropertyValue<string>("StatoOperativo", ref fStatoOperativo, value);
            }
        }
        #region problema causa rimedio
        private string fProblema;
        [Persistent("PROBLEMA"), System.ComponentModel.DisplayName("Problema")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Problema
        {
            get
            {
                return fProblema;
            }
            set
            {
                SetPropertyValue<string>("Problema", ref fProblema, value);
            }
        }

        private string fCausa;
        [Persistent("CAUSA"), System.ComponentModel.DisplayName("Causa")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Causa
        {
            get
            {
                return fCausa;
            }
            set
            {
                SetPropertyValue<string>("Causa", ref fCausa, value);
            }
        }

        private string fRimedio;
        [Persistent("RIMEDIO"), System.ComponentModel.DisplayName("Rimedio")]
        [ VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Rimedio
        {
            get
            {
                return fRimedio;
            }
            set
            {
                SetPropertyValue<string>("Rimedio", ref fRimedio, value);
            }
        }
        #endregion

        #region Date
        private DateTime fDataCreazione;
        [Persistent("DATACREAZIONE"), System.ComponentModel.DisplayName("Data Creazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Creazione dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]  
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime DataCreazione
        {
            get
            {
                return fDataCreazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataCreazione", ref fDataCreazione, value);
            }
        }

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
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

        private DateTime fDataAssegnazione;
        [Persistent("DATAASSEGNAZIONE"), System.ComponentModel.DisplayName("Data Assegnazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        public DateTime DataAssegnazione
        {
            get
            {
                return fDataAssegnazione;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAssegnazione", ref fDataAssegnazione, value);
            }
        }

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
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

        private DateTime fData_Sopralluogo;
        [Persistent("DATA_SOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Sopralluogo ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        public DateTime Data_Sopralluogo
        {
            get
            {
                return fData_Sopralluogo;
            }
            set
            {
                SetPropertyValue<DateTime>("Data_Sopralluogo", ref fData_Sopralluogo, value);
            }
        }





        #endregion
        private string fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
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

        //private string fPrioritaTIntervento;
        //[Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        //[ VisibleInListView(false), VisibleInLookupListView(true)]
        //[DbType("varchar(50)")]
        //public string PrioritaTIntervento
        //{
        //    get
        //    {
        //        return fPrioritaTIntervento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("PrioritaTIntervento", ref fPrioritaTIntervento, value);
        //    }
        //}
        private string fNoteCompletamento;
        [Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("NoteCompletamento")]
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

        //private string fRefAmministrativoCliente;
        //[Persistent("REFAMMINISTRATIVO"), System.ComponentModel.DisplayName("Amministrazione Cliente")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(250)")]
        //public string RefAmministrativoCliente
        //{
        //    get
        //    {
        //        return fRefAmministrativoCliente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("RefAmministrativoCliente", ref fRefAmministrativoCliente, value);
        //    }
        //}
        // Area di Polo
        //private string fAreadiPolo;
        //[Persistent("AREADIPOLO"), System.ComponentModel.DisplayName("Area di Polo")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(100)")]
        //public string AreadiPolo
        //{
        //    get
        //    {
        //        return fAreadiPolo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("AreadiPolo", ref fAreadiPolo, value);
        //    }
        //}

        //private string fCentrodiCosto;
        //[Persistent("CENTROCOSTO"), System.ComponentModel.DisplayName("Centro di Costo")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(100)")]
        //public string CentrodiCosto
        //{
        //    get
        //    {
        //        return fCentrodiCosto;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("CentrodiCosto", ref fCentrodiCosto, value);
        //    }
        //}

        //private int fCodRegRdL;
        //[Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //public int CodRegRdL
        //{
        //    get
        //    {
        //        return fCodRegRdL;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("CodRegRdL", ref fCodRegRdL, value);
        //    }
        //}

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


   
        #region OIDIMMOBILE
        private int fOidImmobile;
        [Persistent("OIDIMMOBILE"), System.ComponentModel.DisplayName("OidImmobile")]
        [MemberDesignTimeVisibility(false)]
        public int OidImmobile
        {
            get
            {
                return fOidImmobile;
            }
            set
            {
                SetPropertyValue<int>("OidImmobile", ref fOidImmobile, value);
            }
        }

        private int _ToDelete;
        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        [MemberDesignTimeVisibility(false)]
        public int ToDelete
        {
            get
            {
                return _ToDelete;
            }
            set
            {
                SetPropertyValue<int>("ToDelete", ref _ToDelete, value);
            }
        }
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

        #endregion


    }
}

 