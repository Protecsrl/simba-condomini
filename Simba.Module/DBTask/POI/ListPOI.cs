using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBTask.POI
{
    [DefaultClassOptions, Persistent("V_RDL_POI"), System.ComponentModel.DisplayName("Programma Operativo Interventi")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Programma Operativo Interventi")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
 
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOITutto", "", "Tutto", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI.Categoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI.Categoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI.Categoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI.Categoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("ListPOI.Categoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 5)]

  

    #endregion

    public class ListPOI : XPLiteObject
    {
        public ListPOI() : base() { }
        public ListPOI(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        

        private string fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [Browsable(false)]
        [DbType("varchar(250)")]
        public string Codice
        {
            get
            {
                return fcodice;
            }
            set
            {
                SetPropertyValue<string>("Codice", ref fcodice, value);
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

        private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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


        private string fPiano;
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Piano
        {
            get
            {
                return fPiano;
            }
            set
            {
                SetPropertyValue<string>("Piano", ref fPiano, value);
            }
        }



        private string fStanza;
        [Persistent("STANZA"), System.ComponentModel.DisplayName("Locale")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Stanza
        {
            get
            {
                return fStanza;
            }
            set
            {
                SetPropertyValue<string>("Stanza", ref fStanza, value);
            }
        }

        private string fReparto;
        [Persistent("REPARTO"), System.ComponentModel.DisplayName("Reparto")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Reparto
        {
            get
            {
                return fReparto;
            }
            set
            {
                SetPropertyValue<string>("Reparto", ref fReparto, value);
            }
        }




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
        private string fCodAsset;
        [Persistent("COD_ASSET"), Size(50), DevExpress.Xpo.DisplayName("Codice Asset")]
        [DbType("varchar(50)")]
        public string CodAsset
        {
            get { return fCodAsset; }
            set { SetPropertyValue<string>("CodAsset", ref fCodAsset, value); }
        }

        private string fTipoAsset;
        [Persistent("TIPOASSET"), System.ComponentModel.DisplayName("Tipo Asset")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string TipoAsset
        {
            get
            {
                return fTipoAsset;
            }
            set
            {
                SetPropertyValue<string>("TipoAsset", ref fTipoAsset, value);
            }
        }
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


        private string fCodProcedura;
        [Persistent("COD_PROCEDURA"), System.ComponentModel.DisplayName("CodProcedura")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string CodProcedura
        {
            get
            {
                return fCodProcedura;
            }
            set
            {
                SetPropertyValue<string>("CodProcedura", ref fCodProcedura, value);
            }
        }

        private string fFrequenza;
        [Persistent("FREQUENZA"), System.ComponentModel.DisplayName("Frequenza")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Frequenza
        {
            get
            {
                return fFrequenza;
            }
            set
            {
                SetPropertyValue<string>("Frequenza", ref fFrequenza, value);
            }
        }

        private string fGennaio;
        [Persistent("GENNAIO"), System.ComponentModel.DisplayName("Gennaio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Gennaio
        {
            get { return fGennaio; }
            set { SetPropertyValue<string>("Gennaio", ref fGennaio, value); }
        }

        private string fFebbraio;
        [Persistent("FEBBRAIO"), System.ComponentModel.DisplayName("Febbraio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Febbraio
        {
            get { return fFebbraio; }
            set { SetPropertyValue<string>("Febbraio", ref fFebbraio, value); }
        }


        private string fMarzo;
        [Persistent("MARZO"), System.ComponentModel.DisplayName("Marzo")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Marzo
        {
            get { return fMarzo; }
            set { SetPropertyValue<string>("Marzo", ref fMarzo, value); }
        }

        private string fAprile;
        [Persistent("APRILE"), System.ComponentModel.DisplayName("Aprile")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Aprile
        {
            get { return fAprile; }
            set { SetPropertyValue<string>("Aprile", ref fAprile, value); }
        }

        private string fMaggio;
        [Persistent("MAGGIO"), System.ComponentModel.DisplayName("Maggio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Maggio
        {
            get { return fMaggio; }
            set { SetPropertyValue<string>("Maggio", ref fMaggio, value); }
        }

        private string fGiugno;
        [Persistent("GIUGNO"), System.ComponentModel.DisplayName("Giugno")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Giugno
        {
            get { return fGiugno; }
            set { SetPropertyValue<string>("Giugno", ref fGiugno, value); }
        }

        private string fLuglio;
        [Persistent("LUGLIO"), System.ComponentModel.DisplayName("Luglio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Luglio
        {
            get { return fLuglio; }
            set { SetPropertyValue<string>("Luglio", ref fLuglio, value); }
        }

        private string fAgosto;
        [Persistent("AGOSTO"), System.ComponentModel.DisplayName("Agosto")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Agosto
        {
            get { return fAgosto; }
            set { SetPropertyValue<string>("Agosto", ref fAgosto, value); }
        }

        private string fSettembre;
        [Persistent("SETTEMBRE"), System.ComponentModel.DisplayName("Settembre")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Settembre
        {
            get { return fSettembre; }
            set { SetPropertyValue<string>("Settembre", ref fSettembre, value); }
        }

        private string fOttobre;
        [Persistent("OTTOBRE"), System.ComponentModel.DisplayName("Ottobre")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Ottobre
        {
            get { return fOttobre; }
            set { SetPropertyValue<string>("Ottobre", ref fOttobre, value); }
        }

        private string fNovembre;
        [Persistent("NOVEMBRE"), System.ComponentModel.DisplayName("Novembre")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Novembre
        {
            get { return fNovembre; }
            set { SetPropertyValue<string>("Novembre", ref fNovembre, value); }
        }

        private string fDicembre;
        [Persistent("DICEMBRE"), System.ComponentModel.DisplayName("Dicembre")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Dicembre
        {
            get { return fDicembre; }
            set { SetPropertyValue<string>("Dicembre", ref fDicembre, value); }
        }

        private string fDurataAttivita;
        [Persistent("DURATA_INTERVENTO"), System.ComponentModel.DisplayName("Durata Attivita'")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(150)")]
        public string DurataAttivita
        {
            get { return fDurataAttivita; }
            set { SetPropertyValue<string>("DurataAttivita'", ref fDurataAttivita, value); }
        }


        private string fMaterialeUtilizzato;
        [Persistent("MATERIALE"), System.ComponentModel.DisplayName("Materiale Utilizzato")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(150)")]
        public string MaterialeUtilizzato
        {
            get { return fMaterialeUtilizzato; }
            set { SetPropertyValue<string>("MaterialeUtilizzato", ref fMaterialeUtilizzato, value); }
        }
        private string fNote;
        [Persistent("NOTE"), System.ComponentModel.DisplayName("Note")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(150)")]
        public string Note
        {
            get { return fNote; }
            set { SetPropertyValue<string>("Note", ref fNote, value); }
        }



        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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

        private string fPrioritaIntervento;
        [Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(50)")]
        public string PrioritaIntervento
        {
            get
            {
                return fPrioritaIntervento;
            }
            set
            {
                SetPropertyValue<string>("PrioritaIntervento", ref fPrioritaIntervento, value);
            }
        }
 

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
        private string fRichiedente;
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("fRichiedente")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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
        #region  settimana mese anno

        private string fTrimestre;
        [Persistent("TRIMESTRE"), System.ComponentModel.DisplayName("Trimestre")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        public string Trimestre
        {
            get
            {
                return fTrimestre;
            }
            set
            {
                SetPropertyValue<string>("Trimestre", ref fTrimestre, value);
            }
        }


        private string fAnno;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(10)")]
        public string Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<string>("Anno", ref fAnno, value);
            }
        }

        //private DateTime fDataConferma;
        //[Persistent("DATACONFERMA"), System.ComponentModel.DisplayName("Data Conferma")]
        ////[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        ////[DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        ////[EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]
        //[ToolTip("La data si riferisce alle ore 24:00", "Data di Conferma del supervisore primo giorno del trimestre", DevExpress.Persistent.Base.ToolTipIconType.Information)]
        ////[Appearance("RegistroPOI.Abilita.ReportExcel", Criteria = "StatoPOI = 'Eseguito' Or StatoPOI = 'Trasmesso'", FontColor = "Black", Enabled = false)]
        ////[ImmediatePostData]
        //public DateTime DataConferma
        //{
        //    get
        //    {
        //        return fDataConferma;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataConferma", ref fDataConferma, value);
        //    }
        //}

        #endregion



        #region OIDEDIFICIO, OIDREFERENTECOFELY
        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
     [VisibleInDetailView(false),VisibleInListView(false),VisibleInLookupListView(false)]
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
        private int fOidReferenteCofely;
        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OidReferenteCofely
        {
            get
            {
                return fOidReferenteCofely;
            }
            set
            {
                SetPropertyValue<int>("OidReferenteCofely", ref fOidReferenteCofely, value);
            }
        }
        private int fOidCategoria;
        [Persistent("OIDCATEGORIA"), System.ComponentModel.DisplayName("OidCategoria")] // pm
        [Browsable(false)]
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

        

        #endregion



    }

}


