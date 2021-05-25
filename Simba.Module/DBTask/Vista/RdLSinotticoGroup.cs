using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
 
namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("RDL_SINOTTICO_GROUP")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Sinottico Group")]
    //[Indices("Anno", "Mese", "Anno;Mese", "Settimana", "Anno;Mese;Settimana", "StatoSmistamento", "AreadiPolo", "CentrodiCosto", "StatoSmistamento;AreadiPolo;CentrodiCosto", "Categoria",  "DataPianificata",  "OidCategoria", "OidCommessa", "OidEdificio")]
    [Indices("StatoSmistamento", "AreadiPolo", "CentrodiCosto", "DataPianificata", "OidCommessa")]

    [ImageName("CustomerInfoCards")]
    [NavigationItem("Ticket")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]


    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_Ultimi3mesi", "DateDiffMonth([DataPianificata],Today()) In(0,1,2,3)", "Ultimi 3 Mesi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_Ultimi6mesi", "DateDiffMonth([DataPianificata],Today()) In(0,1,2,3,4,5,6)", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_Ultimi9mesi", "DateDiffMonth([DataPianificata],Today()) In(0,1,2,3,4,5,6,7,8,9)", "Ultimi 9 Mesi", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_AnnoinCorso", "IsThisYear([DataPianificata])", "Anno in Corso", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_AnnoScorso", "IsLastYear([DataPianificata])", "Anno Scorso", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_Anno2indietro", "DateDiffYear([DataPianificata],Today()) = 2", "2 Anni Scorsi", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_Anno3indietro", "DateDiffYear([DataPianificata],Today()) = 3", "3 Anni Scorsi", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_1TrimAnnoinCorso", "GetMonth([DataPianificata]) In(1,2,3) And IsThisYear([DataPianificata])", @"1° Trimestre", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_2TrimAnnoinCorso", "GetMonth([DataPianificata]) In(4,5,6) And IsThisYear([DataPianificata])", @"2° Trimestre", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_3TrimAnnoinCorso", "GetMonth([DataPianificata]) In(7,8,9) And IsThisYear([DataPianificata])", @"3° Trimestre", Index = 9)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_4TrimAnnoinCorso", "GetMonth([DataPianificata]) In(10,11,12) And IsThisYear([DataPianificata])", @"4° Trimestre", Index = 10)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_1TrimAnnoScorso", "GetMonth([DataPianificata]) In(1,2,3) And IsLastYear([DataPianificata])", @"1° Trimestre (Anno Scorso)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_2TrimAnnoScorso", "GetMonth([DataPianificata]) In(4,5,6) And IsLastYear([DataPianificata])", @"2° Trimestre (Anno Scorso)", Index = 12)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_3TrimAnnoScorso", "GetMonth([DataPianificata]) In(7,8,9) And IsLastYear([DataPianificata])", @"3° Trimestre (Anno Scorso)", Index = 13)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_4TrimAnnoScorso", "GetMonth([DataPianificata]) In(10,11,12) And IsLastYear([DataPianificata])", @"4° Trimestre (Anno Scorso)", Index = 14)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotticoGroup_futuro", "DateDiffMonth([DataPianificata],LocalDateTimeThisMonth()) < 0", "Mesi prossimi", Index = 15)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 13)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 14)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 15)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 16)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLSinotViewCategoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 17)]

    #endregion


    public class RdLSinotticoGroup : XPObject
    {
        public RdLSinotticoGroup() : base() { }
        public RdLSinotticoGroup(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fAreadiPolo;
        [Persistent("AREADIPOLO"), System.ComponentModel.DisplayName("Area di Polo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(300)")]
        public string AreadiPolo        { get; set; }
         

        private string fCentrodiCosto;
        [Persistent("CENTROCOSTO"), System.ComponentModel.DisplayName("Centro di Costo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(300)")]
        public string CentrodiCosto        { get; set; }
   

        private string fCommessa;
        [Persistent("CONTRATTO"), System.ComponentModel.DisplayName("Commessa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(1024)")]
        public string Commessa        { get; set; }



        private string fEdificio;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(1024)")]
        public string Immobile { get; set; }

        private string fImpianto;
        [Persistent("IMPIANTO"), System.ComponentModel.DisplayName("Impianto")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(1024)")]
        public string Impianto { get; set; }


        //private string fStatoSmistamento;
        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.DisplayName("StatoSmistamento")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(300)")]
        public string StatoSmistamento { get; set; }
        //{
        //    get
        //    {
        //        return fStatoSmistamento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("StatoSmistamento", ref fStatoSmistamento, value);
        //    }
        //}
          
       
        [Persistent("CATEGORIA"), System.ComponentModel.DisplayName("Categoria")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        [DbType("varchar(100)")]
        public string Categoria { get; set; }
        
        
        //private string fTeam;
        [Persistent("TEAM"), System.ComponentModel.DisplayName("Team")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(1024)")]
        public string Team { get; set; }

        //private string fTeam;
        [Persistent("RESPTEAM"), System.ComponentModel.DisplayName("Responsabile Tecnico")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(1024)")]
        public string ResponsabileTecnico { get; set; }



        //private string fResponsabileTecnico;
        //[Persistent("UTENTE"), Size(150), DisplayName("Responsabile Tecnico")]
        //[DbType("varchar(150)")]
        //public string ResponsabileTecnico
        //{
        //    get { return fResponsabileTecnico; }
        //    set { SetPropertyValue<string>("ResponsabileTecnico", ref fResponsabileTecnico, value); }
        //}


        //private string fSolleciti;
        [Persistent("SOLLECITI"), System.ComponentModel.DisplayName("Solleciti")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(10)")]
        public string Solleciti { get; set; }
        

        [Persistent("ISREPERIBILITA"), System.ComponentModel.DisplayName("Reperibilita")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //[DbType("varchar(10)")]
        public Boolean IsReperibilita { get; set; }
            
            
            //private DateTime fDataPianificata;
        [Persistent("DATAPIANIFICATA"), System.ComponentModel.DisplayName("Data Pianificata")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Pianificata dell'Intervento ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(false)]
        public DateTime DataPianificata { get; set; }
        //{
        //    get
        //    {
        //        return fDataPianificata;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataPianificata", ref fDataPianificata, value);
        //    }
        //}
 

        //private string fPriorita;
        [Persistent("PRIORITA"), System.ComponentModel.DisplayName("Priorita")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(200)")]
        public string Priorita { get; set; }
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

        //private string fTIntervento;
        [Persistent("TIPOINTERVENTO"), System.ComponentModel.DisplayName("TIntervento")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
         //[DbType("varchar(200)")]
        public string TIntervento { get; set; }
        //{
        //    get
        //    {
        //        return fTIntervento;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("TIntervento", ref fTIntervento, value);
        //    }
        //} 

        
 //private string fStatoOperativo;
        [Persistent("STATOOPERATIVO"), System.ComponentModel.DisplayName("StatoOperativo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [Appearance("RdLListViewGuasto.StatoOperativo.ColoreGiallo", AppearanceItemType = "ViewItem", Context = "ListView", Criteria = "[StatoOperativo] == 'In attesa approvazione preventivo'", BackColor = "Yellow")]
        [DbType("varchar(400)")]
        public string StatoOperativo { get; set; }
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

        
        [Persistent("TOTALE"), System.ComponentModel.DisplayName("Totale")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        public int Totale { get; set; }
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
  
        [PersistentAlias("Iif(DataPianificata is not null, GetMonth(DataPianificata),0)"), DevExpress.Xpo.DisplayName("Mese")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Mese
        {
            get
            {
                var tempObject = EvaluateAlias("Mese");
                if (tempObject != null)
                    return Convert.ToInt32(tempObject);
                else
                    return 0;
            }
        }

    [PersistentAlias("Iif(DataPianificata is not null, GetYear(DataPianificata),0)"), DevExpress.Xpo.DisplayName("Anno")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Anno
        {
            get
            {
                var tempObject = EvaluateAlias("Anno");
                if (tempObject != null)
                    return Convert.ToInt32(tempObject);
                else
                    return 0;
            }
        }



        #region OIDEDIFICIO, OIDREFERENTECOFELY, OIDAREADIPOLO

        //private int fOidEdificio;
        //[Persistent("OIDEDIFICIO"), System.ComponentModel.DisplayName("OidEdificio")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public int OidEdificio { get; set; }
        //{
        //    get
        //    {
        //        return fOidEdificio;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidEdificio", ref fOidEdificio, value);
        //    }
        //}

        //private int fOidReferenteCofely;
        //[Persistent("OIDREFERENTECOFELY"), System.ComponentModel.DisplayName("OidReferenteCofely")] // pm
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
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
        //oidareadipolo
        private int fOidCommessa;
        [Persistent("OIDCOMMESSA"), System.ComponentModel.DisplayName("OidCOMMESSA")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OidCommessa { get; set; }
        //{
        //    get
        //    {
        //        return fOidCommessa;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OidCommessa", ref fOidCommessa, value);
        //    }
        //}

        private int fOidSmistamento;
        [Persistent("OIDSMISTAMENTO"), System.ComponentModel.DisplayName("OidSmistamento")] // pm
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int OidSmistamento { get; set; }
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



        #endregion
        private XPCollection<RdLListViewSinottico> noAssociation;
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RdLListViewSinottico> NoAssociation
        {
            get
            {
                if (noAssociation == null)
                {
                    noAssociation = new XPCollection<RdLListViewSinottico>(Session);
                    noAssociation.BindingBehavior = CollectionBindingBehavior.AllowNone;
                    CriteriaOperator op = CriteriaOperator.Parse(
                              string.Format("OidCommessa == '{0}'", this.OidCommessa));
                    noAssociation.Criteria= op;
                }
                return noAssociation;
            }
        }

    }

}
