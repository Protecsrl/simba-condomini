//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace CAMS.Module.DBTask.Vista
//{
//    class RegRdLListViewMPCompleta
//    {
//    }
//}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using DevExpress.ExpressApp.ConditionalAppearance;
// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;


namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_REGRDL_MP_COMPLETA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Agenda Interventi MP Dettaglio")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone                

    
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_Ultimi3mesi", "DateDiffMonth([DataPianificata],Today()) In(0,1,2,3)", "Ultimi 3 Mesi", true, Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_Ultimi6mesi", "[DataPianificata] >= AddMonths(LocalDateTimeThisMonth(), -6) And [DataPianificata] <= LocalDateTimeThisMonth()", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_Ultimi6mesi", "DateDiffMonth([DataPianificata],Today()) In(0,1,2,3,4,5,6)", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_Ultimi9mesi", "DateDiffMonth([DataPianificata],Today()) In(0,1,2,3,4,5,6,7,8,9)", "Ultimi 9 Mesi", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_AnnoinCorso", "IsThisYear([DataPianificata])", "Anno in Corso", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_AnnoScorso", "IsLastYear([DataPianificata])", "1 Anno fa", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_Anno2indietro", "DateDiffYear([DataPianificata],Today()) = 2", "2 Anni fa", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_Anno3indietro", "DateDiffYear([DataPianificata],Today()) = 3", "3 Anni fa", Index = 6)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewTutto", "", "Tutto", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_1TrimAnnoinCorso", "GetMonth([DataPianificata]) In(1,2,3) And IsThisYear([DataPianificata])", @"1° Trimestre", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_2TrimAnnoinCorso", "GetMonth([DataPianificata]) In(4,5,6) And IsThisYear([DataPianificata])", @"2° Trimestre", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_3TrimAnnoinCorso", "GetMonth([DataPianificata]) In(7,8,9) And IsThisYear([DataPianificata])", @"3° Trimestre", Index = 9)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_4TrimAnnoinCorso", "GetMonth([DataPianificata]) In(10,11,12) And IsThisYear([DataPianificata])", @"4° Trimestre", Index = 10)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_1TrimAnnoScorso", "GetMonth([DataPianificata]) In(1,2,3) And IsLastYear([DataPianificata])", @"1° Trimestre (Anno Scorso)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_2TrimAnnoScorso", "GetMonth([DataPianificata]) In(4,5,6) And IsLastYear([DataPianificata])", @"2° Trimestre (Anno Scorso)", Index = 12)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_3TrimAnnoScorso", "GetMonth([DataPianificata]) In(7,8,9) And IsLastYear([DataPianificata])", @"3° Trimestre (Anno Scorso)", Index = 13)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_4TrimAnnoScorso", "GetMonth([DataPianificata]) In(10,11,12) And IsLastYear([DataPianificata])", @"4° Trimestre (Anno Scorso)", Index = 14)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RegRdLListViewMPCom_futuro", "DateDiffMonth([DataPianificata],LocalDateTimeThisMonth()) < 0", "Mesi prossimi", Index = 15)]

    #endregion

    public class RegRdLListViewMPCompleta : XPLiteObject
    {
        public RegRdLListViewMPCompleta() : base() { }
        public RegRdLListViewMPCompleta(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fCod;
        [Key, Persistent("COD"), System.ComponentModel.DisplayName("Cod")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        
        //[Appearance("RegRdLListViewMP.Codice.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(1)", BackColor = "Yellow")]
        //[Appearance("RegRdLListViewMP.Codice.ColoreRosso", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(10)", BackColor = "Red")]
        //[Appearance("RegRdLListViewMP.Codice.ColoreVerde", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(2,3,11)", BackColor = "LightGreen")]
        //[Appearance("RegRdLListViewMP.Codice.ColoreArancio", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(4,5)", BackColor = "0xfff0f0")]
        //[Appearance("RegRdLListViewMP.Codice.ColoreBlue", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[OidSmistamento] In(6, 7)", BackColor = "LightSteelBlue")]
        
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public string Cod
        {
            get
            {
                return fCod;
            }
            set
            {
                SetPropertyValue<string>("Cod", ref fCod, value);
            }
        }




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


        private int fCodice;
        [Persistent("CODICE"), System.ComponentModel.DisplayName("Codice")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Codice
        {
            get
            {
                return fCodice;
            }
            set
            {
                SetPropertyValue<int>("Codice", ref fCodice, value);
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(200)")]
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

        private string fCodApparato;
        [Persistent("CODAPPARATO"), System.ComponentModel.DisplayName("Cod Apparato")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        public string CodApparato
        {
            get
            {
                return fCodApparato;
            }
            set
            {
                SetPropertyValue<string>("CodApparato", ref fCodApparato, value);
            }
        }


       
        private string fCodSchedeMP;
        [Persistent("CODSCHEDEMP"), System.ComponentModel.DisplayName("Cod SchedaMP")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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

        private string fFrequenza;
        [Persistent("FREQUENZA"), System.ComponentModel.DisplayName("Frequenza")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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
        
        private string fDescrizioneSchedaMP;
        [Persistent("DESCRIZIONESCHEDAMP"), System.ComponentModel.DisplayName("Descrizione SchedaMP")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(4000)")]
        public string DescrizioneSchedaMP
        {
            get
            {
                return fDescrizioneSchedaMP;
            }
            set
            {
                SetPropertyValue<string>("DescrizioneSchedaMP", ref fDescrizioneSchedaMP, value);
            }
        }

        private string fPassoSchedaMP;
        [Persistent("PASSOSCHEDAMP"), System.ComponentModel.DisplayName("Descrizione Passo SchedaMP")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
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

        private int fMeseDataPianificata;
        [Persistent("MESE"), System.ComponentModel.DisplayName("Mese Data Pianificata")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int MeseDataPianificata
        {
            get
            {
                return fMeseDataPianificata;
            }
            set
            {
                SetPropertyValue<int>("MeseDataPianificata", ref fMeseDataPianificata, value);
            }
        }

        private int fAnnoDataPianificata;
        [Persistent("ANNO"), System.ComponentModel.DisplayName("Anno Data Pianificata")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public int AnnoDataPianificata
        {
            get
            {
                return fAnnoDataPianificata;
            }
            set
            {
                SetPropertyValue<int>("AnnoDataPianificata", ref fAnnoDataPianificata, value);
            }
        }


        private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
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

      

        private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(250)")]
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

        private DateTime fDataCompletamento;
        [Persistent("DATACOMPLETAMENTO"), System.ComponentModel.DisplayName("Data Completamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Completamento Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
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


        private int fOidAreadipolo;
        [Persistent("OIDAREADIPOLO"), System.ComponentModel.DisplayName("OidAreadipolo")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
        public int OidAreadipolo
        {
            get
            {
                return fOidAreadipolo;
            }
            set
            {
                SetPropertyValue<int>("OidAreadipolo", ref fOidAreadipolo, value);
            }
        }




        //oidareadipolo
        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCommessa")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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


     


        private int fOidEdificio;
        [Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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
        [VisibleInDetailView(false), VisibleInListView(true), VisibleInLookupListView(false)]
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




    }

}
