using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//namespace CAMS.Module.DBTask.Vista
//{
//    class RdLListViewGuastoAREA
//    {
//    }
//}
using DevExpress.ExpressApp.ConditionalAppearance;// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.Drawing;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LIST_GUASTO_AREA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Sinottico RdL Cliente")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]

    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone                     "OidSmistamento = 1", "In Attesa di Assegnazione", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_1-2-3-10-11", "OidSmistamento In(1,2,11,10,3) ", "In Elaborazione SLA", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_1", "OidSmistamento = 1", "In Attesa di Assegnazione", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA", "", "Tutto", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_2", "OidSmistamento = 2", "Assegnata", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_3", "OidSmistamento = 3", "Emessa In lavorazione", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_6", "OidSmistamento = 6", "Sospesa SO", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_10", "OidSmistamento = 10", "In Emergenza da Assegnare", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLListViewGuastoAREA.OidSmistamento_11", "OidSmistamento = 11", "Gestione da Sala Operativa", Index = 7)]
    // [Appearance("BoldDetailView", AppearanceItemType = "LayoutItem", TargetItems = "*", Context = "BusinessGoals_DetailView", FontStyle = FontStyle.Bold)]
    // [ListViewFilter("Open Goals", "dtDeleted is null", true)] --ListViewFilter("Deleted Goals", "dtDeleted is not null")] [ListViewFilter("All Goals", "")]

    #endregion

    public class RdLListViewGuastoAREA : XPLiteObject
    {
        public RdLListViewGuastoAREA() : base() { }
        public RdLListViewGuastoAREA(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [Appearance("RdLListViewGuasto.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        [Appearance("RdLListViewGuasto.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        [Appearance("RdLListViewGuasto.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdLListViewGuasto.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdLListViewGuasto.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
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

        private int fCodRegRdL;
        [Persistent("CODREGRDL"), System.ComponentModel.DisplayName("CodRegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("RdLListViewGuasto.CodRegRdL.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        [Appearance("RdLListViewGuasto.CodRegRdL.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        [Appearance("RdLListViewGuasto.CodRegRdL.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        [Appearance("RdLListViewGuasto.CodRegRdL.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        [Appearance("RdLListViewGuasto.CodRegRdL.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
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
        //     2 rossi 3 gialli
        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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



        private string fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(400)")]
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


        private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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

        private string fDescrizione;
        [Size(4000), Persistent("DESCRIZIONE"), System.ComponentModel.DisplayName("Descrizione Intervento")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
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

        private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
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


        private string fRitardo;
        [Persistent("RITARDO"), System.ComponentModel.DisplayName("Ritardo")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        [Appearance("RdLGuasto.Ritardo.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([Ritardo],'(1)')")]
        [Appearance("RdLGuasto.Ritardo.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([Ritardo],'(2)')")]
        [Appearance("RdLGuasto.Ritardo.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([Ritardo],'(3)')")]
        [Appearance("RdLGuasto.Ritardo.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([Ritardo],'(4)')")]
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


        private string fSolleciti;
        [Persistent("SOLLECITI"), System.ComponentModel.DisplayName("Solleciti")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(10)")]
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

        private DateTime fDataSopralluogo;
        [Persistent("DATASOPRALLUOGO"), System.ComponentModel.DisplayName("Data Sopralluogo")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Sopralluogo ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataSopralluogo
        {
            get
            {
                return fDataSopralluogo;
            }
            set
            {
                SetPropertyValue<DateTime>("DataSopralluogo", ref fDataSopralluogo, value);
            }
        }

        private DateTime fDataInizioLavori;
        [Persistent("DATAINIZIOLAVORI"), System.ComponentModel.DisplayName("Data Inizio Lavori")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Inizio Lavori", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataInizioLavori
        {
            get
            {
                return fDataInizioLavori;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInizioLavori", ref fDataInizioLavori, value);
            }
        }

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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

        private string fNoteCompletamento;
        [Size(4000), Persistent("NOTECOMPLETAMENTO"), System.ComponentModel.DisplayName("Note Completamento")]
        [DbType("varchar(4000)")]
        [VisibleInListView(false)]
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

        //        
        //1	Urgenza (24 h)
        //5	Emergenza (2 h) c.Sem.
        //6	Urgenza (16 h)
        //2	Non Urgente(36 h)
        //4	Urgenza (8 h) c.Sem.
        //0	Programmata
        //95	Emergenza(1:30 Semaf.)
        //        OrangeRed
        //IndianRed
        //Gold
        //yallo
        //NavajoWhite
        //Gr    FontStyle = FontStyle.Bold,

        private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("RdLListViewGuasto.Priorita.ColoreRosso1", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 5", FontStyle = FontStyle.Bold, BackColor = "Red")]
        [Appearance("RdLListViewGuasto.Priorita.ColoreRosso2", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 95", FontStyle = FontStyle.Bold, BackColor = "OrangeRed")]
        [Appearance("RdLListViewGuasto.Priorita.ColoreGiallo1", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 4", BackColor = "Gold")]
        [Appearance("RdLListViewGuasto.Priorita.ColoreGiallo2", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 6", BackColor = "Yellow")]
        [Appearance("RdLListViewGuasto.Priorita.ColoreGiallo3", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 1", BackColor = "NavajoWhite")]
        [Appearance("RdLListViewGuasto.Priorita.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 2", BackColor = "LightGreen")]
        [Appearance("RdLListViewGuasto.Priorita.ColoreVerde1", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidPriorita] = 0", BackColor = "Green")]
        //[DbType("varchar(200)")]
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

        //1	Differito
        //6	Emergenza
        //5	Fuori Casa
        //4	In Casa
        //3	In Trasferta
        //109	Indifferibile                                            rosso
        //2	Normale, Programmato breve e lungo termine
        //88	Programmabile a Breve Termine                          marrone
        //108	Programmabile a Lungo Termine                          verde
        //110	Programmabile a Medio Termine                          oro
        //0	Programmato	

        private string fTIntervento;
        [Persistent("TIPOINTERVENTO"), System.ComponentModel.DisplayName("TIntervento")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("RdLListViewGuasto.TIntervento.ColoreRosso1", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] = 109", FontStyle = FontStyle.Bold, BackColor = "Red")]//Indifferibile
        [Appearance("RdLListViewGuasto.TIntervento.ColoreRosso2", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] = 88", FontStyle = FontStyle.Bold, FontColor = "Yellow", BackColor = "Brown")]//Breve Termine 
        [Appearance("RdLListViewGuasto.TIntervento.ColoreRosso3", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] = 6", FontStyle = FontStyle.Bold, BackColor = "OrangeRed")]//emergenza
        [Appearance("RdLListViewGuasto.TIntervento.ColoreGiallo1", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] = 110", BackColor = "Gold")]//Medio Termine
        [Appearance("RdLListViewGuasto.TIntervento.ColoreGiallo2", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] In(3,4,5)", BackColor = "Yellow")]
        [Appearance("RdLListViewGuasto.TIntervento.ColoreGiallo3", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] = 1", BackColor = "NavajoWhite")]//differito
        [Appearance("RdLListViewGuasto.TIntervento.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] In(0,2)", BackColor = "LightGreen")]//programmato
        [Appearance("RdLListViewGuasto.TIntervento.ColoreVerde1", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidTipoIntervento] = 108", FontStyle = FontStyle.Bold, BackColor = "Green")]//Lungo Termine  
        //[DbType("varchar(200)")]
        public string TIntervento
        {
            get
            {
                return fTIntervento;
            }
            set
            {
                SetPropertyValue<string>("TIntervento", ref fTIntervento, value);
            }
        }

        private string fSLASopralluogo;
        [Persistent("SLASOPRALLUOGO"), System.ComponentModel.DisplayName("SLA Tempo Intervento")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        [Appearance("RdLGuasto.SLASopralluogo.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([SLASopralluogo],'(1)')")]
        [Appearance("RdLGuasto.SLASopralluogo.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([SLASopralluogo],'(2)')")]
        [Appearance("RdLGuasto.SLASopralluogo.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([SLASopralluogo],'(3)')")]
        [Appearance("RdLGuasto.SLASopralluogo.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([SLASopralluogo],'(4)')")]
        public string SLASopralluogo
        {
            get
            {
                return fSLASopralluogo;
            }
            set
            {
                SetPropertyValue<string>("SLASopralluogo", ref fSLASopralluogo, value);
            }
        }
        private string fSLARipristino;
        [Persistent("SLARIPRISTINO"), System.ComponentModel.DisplayName("SLA Tempo Ripristino")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(4000)")]
        [Appearance("RdLGuasto.SLARipristino.BackColor.Red1", BackColor = "Green", FontColor = "Black", Priority = 1, Criteria = "Contains([SLARipristino],'(1)')")]
        [Appearance("RdLGuasto.SLARipristino.BackColor.Red2", BackColor = "Yellow", FontColor = "Black", Priority = 2, Criteria = "Contains([SLARipristino],'(2)')")]
        [Appearance("RdLGuasto.SLARipristino.BackColor.Red3", BackColor = "Salmon", FontColor = "Black", Priority = 3, Criteria = "Contains([SLARipristino],'(3)')")]
        [Appearance("RdLGuasto.SLARipristino.BackColor.Red4", BackColor = "Red", FontColor = "Black", Priority = 4, Criteria = "Contains([SLARipristino],'(4)')")]
        public string SLARipristino
        {
            get
            {
                return fSLARipristino;
            }
            set
            {
                SetPropertyValue<string>("SLARipristino", ref fSLARipristino, value);
            }
        }
   


        private string fStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("StatoOperativo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("RdLListViewGuasto.StatoOperativo.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[StatoOperativo] == 'In attesa approvazione preventivo'", BackColor = "Yellow")]
        [DbType("varchar(400)")]
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
     
       

        #region OIDEDIFICIO, OIDREFERENTECOFELY, OIDAREADIPOLO

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
        //oidareadipolo
        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCOMMESSA")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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

        private int fOidSmistamento;
        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] // pm
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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


        private int fOidPriorita;
        [Persistent("OIDPRIORITA"), System.ComponentModel.DisplayName("OidPriorita")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidPriorita
        {
            get
            {
                return fOidPriorita;
            }
            set
            {
                SetPropertyValue<int>("OidPriorita", ref fOidPriorita, value);
            }
        }

        private int fOidTipoIntervento;
        [Persistent("OIDTIPOINTERVENTO"), System.ComponentModel.DisplayName("Oid TipoIntervento")] // pm
        [MemberDesignTimeVisibility(false)]
        public int OidTipoIntervento
        {
            get
            {
                return fOidTipoIntervento;
            }
            set
            {
                SetPropertyValue<int>("OidTipoIntervento", ref fOidTipoIntervento, value);
            }
        }

        #endregion



    }

}
