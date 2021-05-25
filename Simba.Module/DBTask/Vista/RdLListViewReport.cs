using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LIST_REPORT")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Report")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    public class RdLListViewReport : XPLiteObject
    {
        public RdLListViewReport() : base() { }
        public RdLListViewReport(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        //CodEdificio
        private string fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
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

        private int fcodiceRdL;
        [ Persistent("CODICERDL"), System.ComponentModel.DisplayName("codice RdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        public int codiceRdL
        {
            get
            {
                return fcodiceRdL;
            }
            set
            {
                SetPropertyValue<int>("codiceRdL", ref fcodiceRdL, value);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione")]
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

        private string fRegRdLDescrizione;
        [Persistent("REGRDLDESCRIZIONE"), System.ComponentModel.DisplayName("RegRdLDescrizione")]
        [DbType("varchar(4000)")]
        public string RegRdLDescrizione
        {
            get
            {
                return fRegRdLDescrizione;
            }
            set
            {
                SetPropertyValue<string>("RegRdLDescrizione", ref fRegRdLDescrizione, value);
            }
        }

        private string fCodEdificio;
        [Persistent("CODEDIFICIO"), System.ComponentModel.DisplayName("Codice Immobile")]
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

        private string fImpianto;
        [Persistent("IMPIANTO"), XafDisplayName("Impianto")]
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
        [Persistent("TIPOASSET"), System.ComponentModel.DisplayName("Tipo Asset")]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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


        private string fTeamMansione;
        [Persistent("TEAMMANSIONE"), System.ComponentModel.DisplayName("Team Mansione")]
        [DbType("varchar(4000)")]
        public string TeamMansione
        {
            get
            {
                return fTeamMansione;
            }
            set
            {
                SetPropertyValue<string>("TeamMansione", ref fTeamMansione, value);
            }
        }

        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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

        private DateTime fDataRichiesta;
        [Persistent("DATARICHIESTA"), System.ComponentModel.DisplayName("Data Richiesta")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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


        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [Persistent("RICHIEDENTE"), System.ComponentModel.DisplayName("Richiedente")]
        [DbType("varchar(4000)")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
 

    

        private int fCodRegRdL;
        [Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]       
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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

        //		

        private string fCodSchedeMP;
        [Persistent("CODSCHEDEMP"), System.ComponentModel.DisplayName("Cod Schede MP")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string CodSchedeMP
        {
            get
            {
                return fCodSchedeMP;
            }
            set
            {
                SetPropertyValue<string>("CodSchedeMP", ref fCodSchedeMP, value);
            }
        }

        private string fCodSchedeMPUNI;
        [Persistent("CODSCHEDAMPUNI"), System.ComponentModel.DisplayName("Cod Schede MP UNI")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string CodSchedeMPUNI
        {
            get
            {
                return fCodSchedeMPUNI;
            }
            set
            {
                SetPropertyValue<string>("CodSchedeMPUNI", ref fCodSchedeMPUNI, value);
            }
        }
        //		

        private string fDescrzManutenzione;
        [Persistent("DESCRIZIONEMANUTENZIONE"), System.ComponentModel.DisplayName("Descrz Manutenzione")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string DescrzManutenzione
        {
            get
            {
                return fDescrzManutenzione;
            }
            set
            {
                SetPropertyValue<string>("DescrzManutenzione", ref fDescrzManutenzione, value);
            }
        }

        //		
        //		
       

        private string fFreqDescrizione;
        [Persistent("FREQUENZADESCRIZIONE"), System.ComponentModel.DisplayName("Freq Descrizione ")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string FreqDescrizione
        {
            get
            {
                return fFreqDescrizione;
            }
            set
            {
                SetPropertyValue<string>("FreqDescrizione", ref fFreqDescrizione, value);
            }
        }


        //private string fFreqCodDescrizione;
        //[Persistent("FREQUENZACOD_DESCRIZIONE"), System.ComponentModel.DisplayName("Freq CodDescrizione")]
        //[VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        //public string FreqCodDescrizione
        //{
        //    get
        //    {
        //        return fFreqCodDescrizione;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("FreqCodDescrizione", ref fFreqCodDescrizione, value);
        //    }
        //}

        //		
        //		

        
        private string fPassoSchedaMP;
        [Persistent("PASSOSCHEDAMP"), System.ComponentModel.DisplayName("Passo Scheda MP")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string PassoSchedaMP
        {
            get
            {
                return fPassoSchedaMP;
            }
            set
            {
                SetPropertyValue<string>("PassoSchedaMP", ref fPassoSchedaMP, value);
            }
        }

                private string fNrOrdine;
        [Persistent("NORDINE"), System.ComponentModel.DisplayName("NrOrdine")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string NrOrdine
        {
            get
            {
                return fNrOrdine;
            }
            set
            {
                SetPropertyValue<string>("NrOrdine", ref fNrOrdine, value);
            }
        }

        private string finSorcing;
        [Persistent("INSOURCING"), System.ComponentModel.DisplayName("inSorcing")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(50)")]
        public string inSorcing
        {
            get
            {
                return finSorcing;
            }
            set
            {
                SetPropertyValue<string>("inSorcing", ref finSorcing, value);
            }
        }     

        private string fApparatoPadre;
        [Persistent("APPARATOPADRE"), System.ComponentModel.DisplayName("Apparato Padre")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string ApparatoPadre
        {
            get
            {
                return fApparatoPadre;
            }
            set
            {
                SetPropertyValue<string>("ApparatoPadre", ref fApparatoPadre, value);
            }
        }
                private string fApparatoSostegno;
        [Persistent("APPARATOSOSTEGNO"), System.ComponentModel.DisplayName("Apparato Sostegno")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
                public string ApparatoSostegno
        {
            get
            {
                return fApparatoSostegno;
            }
            set
            {
                SetPropertyValue<string>("ApparatoSostegno", ref fApparatoSostegno, value);
            }
        }

        private string fComponentiManutenzione;
        [Persistent("COMPONENTI_MANUTENZIONE"), System.ComponentModel.DisplayName("Componenti Manutenzione")]
        [DbType("varchar(4000)")]
        public string ComponentiManutenzione
        {
            get
            {
                return fComponentiManutenzione;
            }
            set
            {
                SetPropertyValue<string>("ComponentiManutenzione", ref fComponentiManutenzione, value);
            }
        }


        private string fComponentiSostegno;
        [Persistent("COMPONENTI_SOSTEGNO"), System.ComponentModel.DisplayName("Componenti Sostegno")]
        [DbType("varchar(4000)")]
        public string ComponentiSostegno
        {
            get
            {
                return fComponentiSostegno;
            }
            set
            {
                SetPropertyValue<string>("ComponentiSostegno", ref fComponentiSostegno, value);
            }
        }

        private int fOidCategoria;
        [Persistent("OIDCATEGORIA"), System.ComponentModel.DisplayName("OidCategoria")] // pm
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

    }

}


