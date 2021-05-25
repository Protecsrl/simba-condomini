
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;
 
namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("RDL_LIST_SINOTTICO_AREA")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Sinottico Area")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem(true)]
    //[System.ComponentModel.DefaultProperty("Descrizione")]"Interventi"
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Ultimi3mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2)))", "Ultimi 3 Mesi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Ultimi6mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2))) Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3)))  Or ([Mese] = GetMonth(AddMonths(Now(), -4)) And [Anno] = GetYear(AddMonths(Now(), -4)))  Or ([Mese] = GetMonth(AddMonths(Now(), -5)) And [Anno] = GetYear(AddMonths(Now(), -5)))  Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3)))", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Ultimi9mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2))) Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3))) Or ([Mese] = GetMonth(AddMonths(Now(), -4)) And [Anno] = GetYear(AddMonths(Now(), -4))) Or ([Mese] = GetMonth(AddMonths(Now(), -5)) And [Anno] = GetYear(AddMonths(Now(), -5))) Or ([Mese] = GetMonth(AddMonths(Now(), -6)) And [Anno] = GetYear(AddMonths(Now(), -6))) Or ([Mese] = GetMonth(AddMonths(Now(), -7)) And [Anno] = GetYear(AddMonths(Now(), -7))) Or ([Mese] = GetMonth(AddMonths(Now(), -8)) And [Anno] = GetYear(AddMonths(Now(), -8)))", "Ultimi Nove Mesi", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.AnnoinCorso", "[Anno] = GetYear(Now())", "Anno in Corso", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.AnnoScorso", "[Anno] = GetYear(AddYears(Now(), -1))", "Anno Scorso", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Tutto", "", "Tutto", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.1TrimAnnoinCorso", "[Mese] In(1,2,3) And [Anno] = GetYear(Now())", @"1° Trimestre", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.2TrimAnnoinCorso", "[Mese] In(4,5,6) And [Anno] = GetYear(Now())", @"2° Trimestre", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.3TrimAnnoinCorso", "[Mese] In(7,8,9) And [Anno] = GetYear(Now())", @"3° Trimestre", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.4TrimAnnoinCorso", "[Mese] In(10,11,12) And [Anno] = GetYear(Now())", @"4° Trimestre", Index = 9)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.1TrimAnnoScorso", "[Mese] In(1,2,3) And [Anno] = GetYear(AddYears(Now(), -1))", @"1° Trimestre (Anno Scorso)", Index = 10)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.2TrimAnnoScorso", "[Mese] In(4,5,6) And [Anno] = GetYear(AddYears(Now(), -1))", @"2° Trimestre (Anno Scorso)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.3TrimAnnoScorso", "[Mese] In(7,8,9) And [Anno] = GetYear(AddYears(Now(), -1))", @"3° Trimestre (Anno Scorso)", Index = 12)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.4TrimAnnoScorso", "[Mese] In(10,11,12) And [Anno] = GetYear(AddYears(Now(), -1))", @"4° Trimestre (Anno Scorso)", Index = 13)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.AreaCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 14)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 15)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 16)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 17)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 18)]

    #endregion

    public class RdLListViewSinotticoArea : XPLiteObject
    {
        //public RdLListVieSinotticoArea() : base() { }
        public RdLListViewSinotticoArea(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.Browsable(false)]
        public RdLListViewSinotticoAreaKey key;

        [XafDisplayName("Categoria")]
        public string Categoria { get { return key.Categoria; } }

        [XafDisplayName("Priorita")]
        public string Priorita { get { return key.Priorita; } }

        [XafDisplayName("Team")]
        public string Team { get { return key.Team; } }

        [XafDisplayName("StatoSmistamento")]
        public string StatoSmistamento { get { return key.StatoSmistamento; } }

        [XafDisplayName("Utente")]
        public string Utente { get { return key.Utente; } }

        [XafDisplayName("TipoIntervento")]
        public string TipoIntervento { get { return key.TipoIntervento; } }

        [XafDisplayName("Sopralluogo")]
        public string FatttoSopralluogo { get { return key.FatttoSopralluogo; } }

        [XafDisplayName("Sollecito")]
        public string Sollecito { get { return key.Sollecito; } }

        [XafDisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public int Anno { get { return key.Anno; } }

        [XafDisplayName("Mese")]
        public int Mese { get { return key.Mese; } }

        [XafDisplayName("Settimana")]
        public int Settimana { get { return key.Settimana; } }

        [XafDisplayName("Area di Polo")]
        [Persistent("AREADIPOLO")]
        public string AreadiPolo;

        [XafDisplayName("Contratto")]
        [Persistent("CONTRATTO")]
        public string Commessa;

        [XafDisplayName("Immobile")]
        [Persistent("IMMOBILE")]
        public string Immobile;      


        [XafDisplayName("nr RdL")]
        [Persistent("NRRDL")]
        public int nrRdL;

        [XafDisplayName("OidEdificio")]
        [Browsable(false)]
        public int OidEdificio { get { return key.OidEdificio; } }

        [XafDisplayName("OidCategoria")]
        [Persistent("OIDCATEGORIA")]
        public int OidCategoria;

        [XafDisplayName("OidCommessa")]
        [Persistent("OIDCOMMESSA")]
        public int OidCommessa;

        [XafDisplayName("OidSmistamento")]
        [Persistent("OIDSMISTAMENTO")]
        public int OidSmistamento;



    }
    [TypeConverter(typeof(StructTypeConverter<RdLListViewSinotticoAreaKey>))]
    public struct RdLListViewSinotticoAreaKey
    {
        [Persistent("OIDEDIFICIO"), System.ComponentModel.Browsable(false)]
        public int OidEdificio;

        [Persistent("CATEGORIAMANUTENZIONE"), System.ComponentModel.Browsable(false)]
        public string Categoria;

        [Persistent("STATOSMISTAMENTO"), System.ComponentModel.Browsable(false)]
        public string StatoSmistamento;

        [Persistent("ANNO"), System.ComponentModel.Browsable(false)]
        public int Anno;
        [Persistent("MESE"), System.ComponentModel.Browsable(false)]
        public int Mese;
        [Persistent("SETTIMANA"), System.ComponentModel.Browsable(false)]
        public int Settimana;

        [Persistent("PRIORITA"), System.ComponentModel.Browsable(false)]
        public string Priorita;

        [Persistent("TEAM"), System.ComponentModel.Browsable(false)]
        public string Team;


        [Persistent("UTENTE"), System.ComponentModel.Browsable(false)]
        public string Utente;

        [Persistent("TIPOINTERVENTO"), System.ComponentModel.Browsable(false)]
        public string TipoIntervento;

        [Persistent("FATTO_SOPRALLUOGO"), System.ComponentModel.Browsable(false)]
        public string FatttoSopralluogo;

        [Persistent("SOLLECITI"), System.ComponentModel.Browsable(false)]
        public string Sollecito;

 

 
    }


}