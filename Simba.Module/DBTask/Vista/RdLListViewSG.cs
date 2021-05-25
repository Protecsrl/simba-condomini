﻿using DevExpress.ExpressApp.ConditionalAppearance;

using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LISTSG")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Naviga Richieste di Lavoro")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGTutto", "", "Tutto", true, Index = 0)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.DaAssegnare", "[OidSmistamento] In(1,10)", "RdL da Assegnare", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.inLavorazione", "[OidSmistamento] In(2,3,11)", "RdL in Lavorazione", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.Completate", "[OidSmistamento] In(4,5)", "RdL Completate", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.AnnullateSospese", "[OidSmistamento] In(6, 7)", "RdL Annullate e SospeseSO", Index = 4)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewSGCategoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 9)]

 

    // [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    // [ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    #endregion

    public class RdLListViewSG : XPLiteObject
    {
        public RdLListViewSG() : base() { }
        public RdLListViewSG(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Appearance("RdLListViewSG.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1,10)", BackColor = "Yellow")]
        [Appearance("RdLListViewSG.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdLListViewSG.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdLListViewSG.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]

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


        private int fCodiceProgressivo;
        [Persistent("CODICEPROGRESSIVO"), System.ComponentModel.DisplayName("Codice Progressivo")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
       // [DbType("varchar(4000)")]
        public int CodiceProgressivo
        {
            get
            {
                return fCodiceProgressivo;
            }
            set
            {
                SetPropertyValue<int>("CodiceProgressivo", ref fCodiceProgressivo, value);
            }
        }

        private int fCodifica;
        [Persistent("CODIFICA"), System.ComponentModel.DisplayName("Codifica")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        public int Codifica
        {
            get
            {
                return fCodifica;
            }
            set
            {
                SetPropertyValue<int>("Codifica", ref fCodifica, value);
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

        private string fComplesso;
        [Persistent("COMPLESSO"), System.ComponentModel.DisplayName("Complesso")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Complesso
        {
            get
            {
                return fComplesso;
            }
            set
            {
                SetPropertyValue<string>("Complesso", ref fComplesso, value);
            }
        }



        private string fPiano;
        [Persistent("PIANO"), System.ComponentModel.DisplayName("Piano")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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

        private string fTipoAsset;
        [Persistent("TIPOASSET"), System.ComponentModel.DisplayName("TipoAsset")]
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
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        private DateTime fDataAssegnazione;
        [Persistent("DATAASSEGNAZIONE"), System.ComponentModel.DisplayName("Data Assegnazione")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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

        private DateTime fData_Sopralluogo;
        [Persistent("DATA_SOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Sopralluogo ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [System.ComponentModel.Browsable(true)]
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


        //private DateTime fDataSopralluogo;
        //[Persistent("DATASOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data Sopralluogo ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public DateTime DataSopralluogo
        //{
        //    get
        //    {
        //        return fDataSopralluogo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value);
        //    }
        //}



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



        private string fRichiedenteTelefono;
        [Persistent("RICHIEDENTETELEFONO"), System.ComponentModel.DisplayName("Richiedente Telefono")]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string RichiedenteTelefono
        {
            get
            {
                return fRichiedenteTelefono;
            }
            set
            {
                SetPropertyValue<string>("RichiedenteTelefono", ref fRichiedenteTelefono, value);
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

        private string fPrioritaTIntervento;
        [Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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

        private string fRefAmministrativoCliente;
        [Persistent("REFAMMINISTRATIVO"), System.ComponentModel.DisplayName("Amministrazione Cliente")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(250)")]
        public string RefAmministrativoCliente
        {
            get
            {
                return fRefAmministrativoCliente;
            }
            set
            {
                SetPropertyValue<string>("RefAmministrativoCliente", ref fRefAmministrativoCliente, value);
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

        private int fCodRegRdL;
        [Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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

        private int fCodOdL;
        [Persistent("CODICEODL"), System.ComponentModel.DisplayName("CodOdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CodOdL
        {
            get
            {
                return fCodOdL;
            }
            set
            {
                SetPropertyValue<int>("CodOdL", ref fCodOdL, value);
            }
        }

        #region  settimana mese anno
        private int fSettimana;
        [Persistent("SETTIMANA"), System.ComponentModel.DisplayName("Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Settimana
        {
            get
            {
                return fSettimana;
            }
            set
            {
                SetPropertyValue<int>("Settimana", ref fSettimana, value);
            }
        }

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

       


        // SOLLECITI
        private string fSolleciti;
        [Persistent("SOLLECITI"), System.ComponentModel.DisplayName("Solleciti")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string Solleciti
        {
            get
            {
                return fSolleciti;
            }
            set
            {
                SetPropertyValue<string>("Solleciti", ref fSolleciti, value);
            }
        }
        //MINUTI_PASSATI   RITARDO
        private int fMinutiPassati;
        [Persistent("MINUTI_PASSATI"), System.ComponentModel.DisplayName("Minuti Passati")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int MinutiPassati
        {
            get
            {
                return fMinutiPassati;
            }
            set
            {
                SetPropertyValue<int>("MinutiPassati", ref fMinutiPassati, value);
            }
        }

        private string fRitardo;
        [Persistent("RITARDO"), System.ComponentModel.DisplayName("Ritardo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        [Appearance("Ritardo.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([Ritardo],'(1)')")]
        [Appearance("Ritardo.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([Ritardo],'(2)')")]
        [Appearance("Ritardo.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([Ritardo],'(3)')")]
        [Appearance("Ritardo.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([Ritardo],'(4)')")]

        public string Ritardo
        {
            get
            {
                return fRitardo;
            }
            set
            {
                SetPropertyValue<string>("Ritardo", ref fRitardo, value);
            }
        }
        #region OIDEDIFICIO, OIDREFERENTECOFELY
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

        private int fOidReferenteCofely;
        [Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        [MemberDesignTimeVisibility(false)]
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


//[Indices("Name", "Name;Age", "Age;ChildCount")]
//public class Person : XPObject
//{   [Size(32)]
//    public String Name;
//    [Indexed(Unique = true), Size(64)]
//    public String FullName;
//    public int Age;
