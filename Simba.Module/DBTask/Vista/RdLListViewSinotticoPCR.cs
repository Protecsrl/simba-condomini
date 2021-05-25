
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Xpo.Utils;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System.ComponentModel;

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_RDL_LIST_SINOTTICO_PCR")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "RdL Sinottico PCR")]
    [ImageName("CustomerInfoCards")]
    [NavigationItem(true)]
    //[System.ComponentModel.DefaultProperty("Descrizione")]"Interventi"
    //[ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPCR.AnnoScorso", "GetYear(Now()) = [Anno]", "Anno in corso", Index = 0)]
    //[DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Tutto", "", "Tutto", Index = 1)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Ultimi3mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2)))", "Ultimi 3 Mesi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Ultimi6mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2))) Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3)))  Or ([Mese] = GetMonth(AddMonths(Now(), -4)) And [Anno] = GetYear(AddMonths(Now(), -4)))  Or ([Mese] = GetMonth(AddMonths(Now(), -5)) And [Anno] = GetYear(AddMonths(Now(), -5)))  Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3)))", "Ultimi 6 Mesi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Ultimi9mesi", "([Mese] = GetMonth(Now()) And [Anno] = GetYear(Now()))  Or ([Mese] = GetMonth(AddMonths(Now(), -1)) And [Anno] = GetYear(AddMonths(Now(), -1))) Or ([Mese] = GetMonth(AddMonths(Now(), -2)) And [Anno] = GetYear(AddMonths(Now(), -2))) Or ([Mese] = GetMonth(AddMonths(Now(), -3)) And [Anno] = GetYear(AddMonths(Now(), -3))) Or ([Mese] = GetMonth(AddMonths(Now(), -4)) And [Anno] = GetYear(AddMonths(Now(), -4))) Or ([Mese] = GetMonth(AddMonths(Now(), -5)) And [Anno] = GetYear(AddMonths(Now(), -5))) Or ([Mese] = GetMonth(AddMonths(Now(), -6)) And [Anno] = GetYear(AddMonths(Now(), -6))) Or ([Mese] = GetMonth(AddMonths(Now(), -7)) And [Anno] = GetYear(AddMonths(Now(), -7))) Or ([Mese] = GetMonth(AddMonths(Now(), -8)) And [Anno] = GetYear(AddMonths(Now(), -8)))", "Ultimi Nove Mesi", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.AnnoinCorso", "[Anno] = GetYear(Now())", "Anno in Corso", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.AnnoScorso", "[Anno] = AddYears(Now(), -1)", "Anno Scorso", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Tutto", "", "Tutto", Index = 5)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.1TrimAnnoinCorso", "[Mese] In(1,2,3) And [Anno] = GetYear(Now())", @"1° Trimestre", Index = 6)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.2TrimAnnoinCorso", "[Mese] In(4,5,6) And [Anno] = GetYear(Now())", @"2° Trimestre", Index = 7)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.3TrimAnnoinCorso", "[Mese] In(7,8,9) And [Anno] = GetYear(Now())", @"3° Trimestre", Index = 8)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.4TrimAnnoinCorso", "[Mese] In(10,11,12) And [Anno] = GetYear(Now())", @"4° Trimestre", Index = 9)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.1TrimAnnoScorso", "[Mese] In(1,2,3) And [Anno] = AddYears(Now(), -1)", @"1° Trimestre (Anno Scorso)", Index = 10)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.2TrimAnnoScorso", "[Mese] In(4,5,6) And [Anno] = AddYears(Now(), -1)", @"2° Trimestre (Anno Scorso)", Index = 11)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.3TrimAnnoScorso", "[Mese] In(7,8,9) And [Anno] = AddYears(Now(), -1)", @"3° Trimestre (Anno Scorso)", Index = 12)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.4TrimAnnoScorso", "[Mese] In(10,11,12) And [Anno] = AddYears(Now(), -1)", @"4° Trimestre (Anno Scorso)", Index = 13)]

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.AreaCategoria.manprog", "OidCategoria = 1", "Manutenzione Programmata", Index = 14)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.conduzione", "OidCategoria = 2", "Conduzione", Index = 15)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.mancond", "OidCategoria = 3", "Manutenzione A Condizione", Index = 16)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.mangu", "OidCategoria = 4", "Manutenzione Guasto", Index = 17)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("RdLLtVieSinotticoPRC.Categoria.manprogspot", "OidCategoria = 5", "Manutenzione Programmata Spot", Index = 18)]

    #endregion

    public class RdLListViewSinotticoPCR : XPLiteObject
    {
        //public RdLListVieSinotticoArea() : base() { }
        public RdLListViewSinotticoPCR(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private int fcodice;
        [Key, Persistent("CODICE"), System.ComponentModel.Browsable(false)]
        public RdLListViewSinotticoPCRKey key;
 
        //a
        [XafDisplayName("Priorita")]
        public string Priorita { get { return key.Priorita; } }
        //a
        [XafDisplayName("Anno")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        public int Anno { get { return key.Anno; } }
        //a
        [XafDisplayName("Mese")]
        public int Mese { get { return key.Mese; } }
        //a
        [XafDisplayName("Settimana")]
        public int Settimana { get { return key.Settimana; } }

        [XafDisplayName("Richiedente")]
        public string Richiedente { get { return key.Richiedente; } }
        //[XafDisplayName("Richiedente")]
        //[Persistent("RICHIEDENTE")]
        //public string Richiedente;

        [XafDisplayName("Reparto")]
        public string Reparto { get { return key.Reparto; } }
        //[XafDisplayName("Reparto")]
        //[Persistent("REPARTO")]
        //public string Reparto;
        [XafDisplayName("Piano")]
        public string Piano { get { return key.Piano; } }
        //[XafDisplayName("Piano")]
        //[Persistent("PIANO")]
        //public string Piano;
        [XafDisplayName("Locale")]
        public string Locale { get { return key.Locale; } }
        //[XafDisplayName("Locale")]
        //[Persistent("LOCALE")]
        //public string Locale;

        [XafDisplayName("nr RdL")]
        [Persistent("NRRDL")]
        public int nrRdL;

        [XafDisplayName("Area di Polo")]
        [Persistent("AREADIPOLO")]
        public string AreadiPolo;
        //a
        [XafDisplayName("Contratto")]
        [Persistent("CONTRATTO")]
        public string Commessa;
        //a
        [XafDisplayName("Immobile")]
        [Persistent("IMMOBILE")]
        public string Immobile;
        //a
        [XafDisplayName("Impianto")]
        [Persistent("IMPIANTO")]
        public string Impianto;
        //a
        [XafDisplayName("Apparato")]
        [Persistent("APPARATO")]
        public string Apparato;
        //-----------------------------------aa-
        //  [XafDisplayName("Categoria")]
        //[Persistent("CATEGORIAMANUTENZIONE")]
        //public string Categoria;
        //a
        //[XafDisplayName("Richiedente")]
        //[Persistent("RICHIEDENTE")]
        //public string Richiedente;
        //a
        [XafDisplayName("Tipo Appararato")]
        [Persistent("TIPOAPPARATO")]
        public string TipoAppararato;

        //[XafDisplayName("Reparto")]
        //[Persistent("REPARTO")]
        //public string Reparto;

        //[XafDisplayName("Piano")]
        //[Persistent("PIANO")]
        //public string Piano;

        //[XafDisplayName("Locale")]
        //[Persistent("LOCALE")]
        //public string Locale;

        //[XafDisplayName("Team")]
        //[Persistent("TEAM")]
        //public string Team;


        [XafDisplayName("Problema")]
        [Persistent("PROBLEMA")]
        public string Problema;
        [XafDisplayName("Causa")]
        [Persistent("CAUSA")]
        public string Causa;
        [XafDisplayName("Rimedio")]
        [Persistent("RIMEDIO")]
        public string Rimedio;


        //-------------------------------------
        [XafDisplayName("OidCommessa")]
        [Persistent("OIDCOMMESSA")]
        public int OidCommessa;

        [XafDisplayName("OidEdificio")]
        [Persistent("OIDEDIFICIO")]
        public int OidEdificio;  

    }
    [TypeConverter(typeof(StructTypeConverter<RdLListViewSinotticoPCRKey>))]
    public struct RdLListViewSinotticoPCRKey
    {
        [Persistent("OIDAPPARATO"), System.ComponentModel.Browsable(false)]
        public int OidApparato;

        //[Persistent("OIDCATEGORIA"), System.ComponentModel.Browsable(false)]
        //public int OidCategoria;

        [Persistent("PRIORITA"), System.ComponentModel.Browsable(false)]//--
        public string Priorita;

        [Persistent("RICHIEDENTE"), System.ComponentModel.Browsable(false)]
        public string Richiedente;


        [Persistent("LOCALE"), System.ComponentModel.Browsable(false)]//--
        public string Locale;
        [Persistent("PIANO"), System.ComponentModel.Browsable(false)]
        public string Piano;
        [Persistent("REPARTO"), System.ComponentModel.Browsable(false)]
        public string Reparto;

        //----------------------------------
        [Persistent("OIDPROBLEMA"), System.ComponentModel.Browsable(false)]
        public int OidProblema;
        [Persistent("OIDCAUSA"), System.ComponentModel.Browsable(false)]
        public int OidCausa;
        [Persistent("OIDRIMEDIO"), System.ComponentModel.Browsable(false)]
        public int OidRimedio;

        [Persistent("ANNO"), System.ComponentModel.Browsable(false)]//-a
        public int Anno;
        [Persistent("MESE"), System.ComponentModel.Browsable(false)]//-a
        public int Mese;
        [Persistent("SETTIMANA"), System.ComponentModel.Browsable(false)]//-a
        public int Settimana;
    }


}