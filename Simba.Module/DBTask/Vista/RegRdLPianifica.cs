//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace CAMS.Module.DBTask.Vista
//{
//    class RegRdLPianifica
//    {
//    }
//}


using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_MP_PIAN_COMPLETE_X_PIAN")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Carichi Pianifica")]
    [ImageName("Action_Debug_Step")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    //#region filtro tampone
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewTutto", "", "Tutto", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 1)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 2)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 3)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 4)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewCategoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 5)]
    //// [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    //// [ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    //#endregion

    public class RegRdLPianifica : XPLiteObject
    {
        public RegRdLPianifica() : base() { }
        public RegRdLPianifica(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        //Data_e_Ora_Min_EditMask CAMSEditorCostantFormat
        private int fcodice;
        [Key, Persistent("CODICE")]       
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
//DESCRIZIONE	Anno 2018
//DATA	20/08/2018
//ANNOSET	2018-34
//DATA_START	20/08/2018
//DATA_END	26/08/2018
//ANNOMESE	2018-08


               
     

        private string fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string Commessa
        {
            get
            {
                return fCommessa;
            }
            set
            {
                SetPropertyValue<string>("Commessa", ref fCommessa, value);
            }
        }

        
        //private string fEdificio;
        //[Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Immobile
        //{
        //    get
        //    {
        //        return fEdificio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Immobile", ref fEdificio, value);
        //    }
        //}

        private string fAnnoSet;
        [Persistent("ANNOSET"), System.ComponentModel.DisplayName("Anno-Settimana")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string AnnoSet
        {
            get
            {
                return fAnnoSet;
            }
            set
            {
                SetPropertyValue<string>("AnnoSet", ref fAnnoSet, value);
            }
        }

        private string fAnnoMese;
        [Persistent("ANNOMESE"), System.ComponentModel.DisplayName("Anno-Mese")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(4000)")]
        public string AnnoMese
        {
            get
            {
                return fAnnoMese;
            }
            set
            {
                SetPropertyValue<string>("AnnoMese", ref fAnnoMese, value);
            }
        }

        //private string fCodEdificio;
        //[Persistent("CODEDIFICIO"), System.ComponentModel.DisplayName("Cod Immobile")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string CodEdificio
        //{
        //    get
        //    {
        //        return fCodEdificio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("CodEdificio", ref fCodEdificio, value);
        //    }
        //}

      


        //private string fServizio;
        //[Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Impianto
        //{
        //    get
        //    {
        //        return fServizio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Impianto", ref fServizio, value);
        //    }
        //}

  
        //private string fApparato;
        //[Persistent("APPARATO"), System.ComponentModel.DisplayName("Apparato")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Apparato
        //{
        //    get
        //    {
        //        return fApparato;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Apparato", ref fApparato, value);
        //    }
        //}

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
        
        
        //private string fCategoria;
        //[Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.DisplayName("Categoria Manutenzione")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Categoria
        //{
        //    get
        //    {
        //        return fCategoria;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Categoria", ref fCategoria, value);
        //    }
        //}


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

        //private string fUtente;
        //[Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        //public string Utente
        //{
        //    get
        //    {
        //        return fUtente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Utente", ref fUtente, value);
        //    }
        //}

        #region Date
        private DateTime fData;
        [Persistent("DATA"), System.ComponentModel.DisplayName("Data Riferimento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Riferimento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime Data
        {
            get
            {
                return fData;
            }
            set
            {
                SetPropertyValue<DateTime>("Data", ref fData, value);
            }
        }

        private DateTime fDataStart;
        [Persistent("DATA_START"), System.ComponentModel.DisplayName("Data Inizio Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Inizio Settimana", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataStart
        {
            get
            {
                return fDataStart;
            }
            set
            {
                SetPropertyValue<DateTime>("DataStart", ref fDataStart, value);
            }
        }

        private DateTime fDataEnd;
        [Persistent("DATA_END"), System.ComponentModel.DisplayName("Data Fine Settimana")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Fine Settimana", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataEnd
        {
            get
            {
                return fDataEnd;
            }
            set
            {
                SetPropertyValue<DateTime>("DataEnd", ref fDataEnd, value);
            }
        }

     
        private int fPianificate;
        [Persistent("PIANIFICATE"), System.ComponentModel.DisplayName("Pianificate")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Pianificate
        {
            get
            {
                return fPianificate;
            }
            set
            {
                SetPropertyValue<int>("Pianificate", ref fPianificate, value);
            }
        }

        private int fCompxPianifica;
        [Persistent("COMP_X_PIANIFICA"), System.ComponentModel.DisplayName("COMP_X_PIANIFICA")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CompxPianifica
        {
            get
            {
                return fCompxPianifica;
            }
            set
            {
                SetPropertyValue<int>("CompxPianifica", ref fCompxPianifica, value);
            }
        }
       
        private int fCompxPianificaNT;
        [Persistent("COMP_X_PIAN_NEI_TEMPI"), System.ComponentModel.DisplayName("COMP_X_PIAN_NEI_TEMPI")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CompxPianificaNT
        {
            get
            {
                return fCompxPianificaNT;
            }
            set
            {
                SetPropertyValue<int>("CompxPianificaNT", ref fCompxPianificaNT, value);
            }
        }
        
        private int fCompxPianificaNNT;
        [Persistent("COMP_X_PIAN_NON_NEI_TEMPI"), System.ComponentModel.DisplayName("COMP_X_PIAN_NON_NEI_TEMPI")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int CompxPianificaNNT
        {
            get
            {
                return fCompxPianificaNNT;
            }
            set
            {
                SetPropertyValue<int>("CompxPianificaNNT", ref fCompxPianificaNNT, value);
            }
        }

        private int fPianificaS;
        [Persistent("PIANIFICATE_S"), System.ComponentModel.DisplayName("SOMMA PIANIFICATE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PianificaS
        {
            get
            {
                return fPianificaS;
            }
            set
            {
                SetPropertyValue<int>("PianificaS", ref fPianificaS, value);
            }
        }

        private int fPianificaSCP;
        [Persistent("COMP_X_PIANIFICA_S"), System.ComponentModel.DisplayName("SOMMA COMP_X_PIANIFICA")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PianificaSCP
        {
            get
            {
                return fPianificaSCP;
            }
            set
            {
                SetPropertyValue<int>("PianificaSCP", ref fPianificaSCP, value);
            }
        }

        private int fPianificaSCNT;
        [Persistent("COMP_X_PIAN_NEI_TEMPI_S"), System.ComponentModel.DisplayName("SOMMA COMP_X_PIAN_NEI_TEMPI")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PianificaSCNT
        {
            get
            {
                return fPianificaSCNT;
            }
            set
            {
                SetPropertyValue<int>("PianificaSCNT", ref fPianificaSCNT, value);
            }
        }

        private int fPianificaSCNNT;
        [Persistent("COMP_X_PIAN_NON_NEI_TEMPI_S"), System.ComponentModel.DisplayName("SOMMA COMP_X_PIAN_NON_NEI_TEMPI")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PianificaSCNNT
        {
            get
            {
                return fPianificaSCNNT;
            }
            set
            {
                SetPropertyValue<int>("PianificaSCNNT", ref fPianificaSCNNT, value);
            }
        }
                
        private int fResiduoPianifica;
        [Persistent("RESIDUO_X_PIANIFICA"), System.ComponentModel.DisplayName("SOMMA RESIDUO_X_PIANIFICA")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int ResiduoPianifica
        {
            get
            {
                return fResiduoPianifica;
            }
            set
            {
                SetPropertyValue<int>("ResiduoPianifica", ref fResiduoPianifica, value);
            }
        }

        private int fPianificaScaduteS;
        [Persistent("PIANIFICA_SCADUTE_S"), System.ComponentModel.DisplayName("SOMMA PIANIFICA_SCADUTE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PianificaScaduteS
        {
            get
            {
                return fPianificaScaduteS;
            }
            set
            {
                SetPropertyValue<int>("PianificaScaduteS", ref fPianificaScaduteS, value);
            }
        }

        private int fPianificaNCS;
        [Persistent("NONCOMP_X_PIANIFICA_S"), System.ComponentModel.DisplayName("SOMMA NONCOMP_X_PIANIFICA")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int PianificaNCS
        {
            get
            {
                return fPianificaNCS;
            }
            set
            {
                SetPropertyValue<int>("PianificaNCS", ref fPianificaNCS, value);
            }
        }

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



        //private string fTeam;
        //[Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string Team
        //{
        //    get
        //    {
        //        return fTeam;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Team", ref fTeam, value);
        //    }
        //}

        //private string fTeamMansione;
        //[Persistent("TEAMMANSIONE"), System.ComponentModel.DisplayName("Mansione")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
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


        //private string fStatoSmistamento;
        //[Persistent("ULTIMOSTATOSMISTAMENTO"), System.ComponentModel.DisplayName("Stato Smistamento")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(4000)")]
        //public string StatoSmistamento
        //{
        //    get
        //    {
        //        return fStatoSmistamento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("UltimoStatoSmistamento", ref fStatoSmistamento, value);
        //    }
        //}

        //private string fStatoOperativo;
        //[Persistent("ULTIMOSTATOOPERATIVO"), System.ComponentModel.DisplayName("Stato Operativo")]
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
        //        SetPropertyValue<string>("UltimoStatoOperativo", ref fStatoOperativo, value);
        //    }
        //}
             
        
        //private string fRichiedente;
        //[Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        //[DbType("varchar(4000)")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public string Richiedente
        //{
        //    get
        //    {
        //        return fRichiedente;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Richiedente", ref fRichiedente, value);
        //    }
        //}

        //private string fPriorita;
        //[Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(150)")]
        //public string Priorita
        //{
        //    get
        //    {
        //        return fPriorita;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("Priorita", ref fPriorita, value);
        //    }
        //}

        //private string fPrioritaTIntervento;
        //[Persistent("PRIORITATIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        //[VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(true)]
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
        
        //private string fNoteCompletamento;
        //[Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("Note Completamento")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        //public string NoteCompletamento
        //{
        //    get
        //    {
        //        return fNoteCompletamento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("NoteCompletamento", ref fNoteCompletamento, value);
        //    }
        //}

        #region OIDEDIFICIO, OIDREFERENTECOFELY
     

        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCommessa")] // pm
        //[MemberDesignTimeVisibility(false)]
        public int OidCommessa
        {
            get
            {
                return fOidCommessa;
            }
            set
            {
                SetPropertyValue<int>("OidCommessa", ref fOidCommessa, value);
            }
        }



        

        
        #endregion

    }
}

