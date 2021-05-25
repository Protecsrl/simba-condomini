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
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.ExpressApp.ConditionalAppearance' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.ConditionalAppearance;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.ExpressApp.ConditionalAppearance' è già presente in questo spazio dei nomi
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Persistent.Base' è già presente in questo spazio dei nomi
using DevExpress.Persistent.Base;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Persistent.Base' è già presente in questo spazio dei nomi
using DevExpress.Persistent.Validation;
#pragma warning disable CS0105 // La direttiva using per 'DevExpress.Xpo' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
#pragma warning restore CS0105 // La direttiva using per 'DevExpress.Xpo' è già presente in questo spazio dei nomi

namespace CAMS.Module.DBTask.Vista
{
    [DefaultClassOptions, Persistent("V_REGOPERATIVORISORSE")]  //v_regoperativorisorse
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Registro Operativo Risorse")]
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

    public class RegOperativoRisorsa : XPLiteObject
    {
        public RegOperativoRisorsa() : base() { }
        public RegOperativoRisorsa(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";
        //Data_e_Ora_Min_EditMask CAMSEditorCostantFormat
        private string fcodice;
        [Key, Persistent("CODICE")]
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
        //private DateTime fDataGiorno;
        //[Persistent("DATAGIORNO"), System.ComponentModel.DisplayName("Data Giorno")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
        //[ToolTip("Data Giorno ", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public DateTime DataGiorno
        //{
        //    get
        //    {
        //        return fDataGiorno;
        //    }
        //    set
        //    {
        //        SetPropertyValue<DateTime>("DataGiorno", ref fDataGiorno, value);
        //    }
        //}

        private DateTime fDataOra;
        [Persistent("DATAORA"), System.ComponentModel.DisplayName("Data Ora")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data Ora", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataOra
        {
            get
            {
                return fDataOra;
            }
            set
            {
                SetPropertyValue<DateTime>("DataOra", ref fDataOra, value);
            }
        }

        //private int fOraMinNum;
        //[Persistent("ORAMINNUM"), System.ComponentModel.DisplayName("Ora Minuto numerico")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        //public int OraMinNum
        //{
        //    get
        //    {
        //        return fOraMinNum;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("OraMinNum", ref fOraMinNum, value);
        //    }
        //}

        private int fRegRdL;
        [Persistent("REGRDL"), System.ComponentModel.DisplayName("RegRdL")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RegRdL
        {
            get
            {
                return fRegRdL;
            }
            set
            {
                SetPropertyValue<int>("RegRdL", ref fRegRdL, value);
            }
        }

        private int fRisorsaOid;
        [Persistent("RISORSA"), System.ComponentModel.DisplayName("RisorsaOid")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int RisorsaOid
        {
            get
            {
                return fRisorsaOid;
            }
            set
            {
                SetPropertyValue<int>("RisorsaOid", ref fRisorsaOid, value);
            }
        }

        private string fRisorsaDesc;
        [Persistent("RISORSADESC"), System.ComponentModel.DisplayName("Risorsa")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string RisorsaDesc
        {
            get
            {
                return fRisorsaDesc;
            }
            set
            {
                SetPropertyValue<string>("RisorsaDesc", ref fRisorsaDesc, value);
            }
        }

        private string fUtente;
        [Persistent("UTENTE"), System.ComponentModel.DisplayName("Utente Smartphone")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
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

        private string fCentroOperativo;
        [Persistent("CENTROOPERATIVO"), System.ComponentModel.DisplayName("Centro Operativo")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string CentroOperativo
        {
            get
            {
                return fCentroOperativo;
            }
            set
            {
                SetPropertyValue<string>("CentroOperativo", ref fCentroOperativo, value);
            }
        }

        private double fLatitude;
        [Size(50), Persistent("LATITUDE"), DevExpress.Xpo.DisplayName("Latitudine")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public double Latitude
        {
            get
            {
                return GetDelayedPropertyValue<double>("Latitude");
            }
            set
            {
                SetDelayedPropertyValue<double>("Latitude", value);
            }

            //get
            //{
            //    return fLatitude;
            //}
            //set
            //{
            //    SetPropertyValue<double>("Latitude", ref fLatitude, value);
            //}
        }
        // public double Longitude { get; set; }



        private double fLongitude;
        [Size(50), Persistent("LONGITUDE"), DevExpress.Xpo.DisplayName("Longitude")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        [Delayed(true)]
        public double Longitude
        {
            get
            {
                return GetDelayedPropertyValue<double>("Longitude");
            }
            set
            {
                SetDelayedPropertyValue<double>("Longitude", value);
            }

            //get
            //{
            //    return fLongitude;
            //}
            //set
            //{
            //    SetPropertyValue<double>("Longitude", ref fLongitude, value);
            //}
        }


        private int fDisponibile;
        [Persistent("DISPONIBILE"), System.ComponentModel.DisplayName("Disponibile")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Disponibile
        {
            get
            {
                return fDisponibile;
            }
            set
            {
                SetPropertyValue<int>("Disponibile", ref fDisponibile, value);
            }
        }

        private string fTipostatoconnessione;
        [Persistent("TIPOSTATOCONNESSIONE"), System.ComponentModel.DisplayName("Tipo Stato Connessione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Tipostatoconnessione
        {
            get
            {
                return fTipostatoconnessione;
            }
            set
            {
                SetPropertyValue<string>("Tipostatoconnessione", ref fTipostatoconnessione, value);
            }
        }

        private int fUltimostatooperativo;
        [Persistent("ULTIMOSTATOOPERATIVO"), System.ComponentModel.DisplayName("Disponibile")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        [VisibleInDetailView(false), VisibleInListView(false), VisibleInLookupListView(false)]
        public int Ultimostatooperativo
        {
            get
            {
                return fUltimostatooperativo;
            }
            set
            {
                SetPropertyValue<int>("Ultimostatooperativo", ref fUltimostatooperativo, value);
            }
        }

        private string fUltimostatooperativodesc;
        [Persistent("ULTIMOSTATOOPERATIVODESC"), System.ComponentModel.DisplayName("Stato Operativo Descrizione")]
        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        [DbType("varchar(150)")]
        public string Ultimostatooperativodesc
        {
            get
            {
                return fUltimostatooperativodesc;
            }
            set
            {
                SetPropertyValue<string>("Ultimostatooperativodesc", ref fUltimostatooperativodesc, value);
            }
        }

        // rr.ultimostatooperativo,
        //so.codstato ultimostatooperativodesc

        //ri.disponibile,
        //case 
        //when rt.tipostatoconnessione=0 then 'Connessione non definita'
        //when rt.tipostatoconnessione= 1 then 'Non Connesso'
        //when rt.tipostatoconnessione= 2 then 'Connesso non in Lavorazione'
        //when rt.tipostatoconnessione= 3 then 'Connesso in Lavorazione'
        //when rt.tipostatoconnessione= 4 then 'Connesso in Pausa'
        //when rt.tipostatoconnessione= 5 then 'Connesso in attività accessoria' 
        //else    'Connessione non definita'
        //end  as  tipostatoconnessione
        //,


        //private string fH01P;
        //[Persistent("H01P"), System.ComponentModel.DisplayName("H01P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H01P
        //{
        //    get
        //    {
        //        return fH01P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H01P", ref fH01P, value);
        //    }
        //}

        //private string fH01D;
        //[Persistent("H01D"), System.ComponentModel.DisplayName("H01D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H01D
        //{
        //    get
        //    {
        //        return fH01D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H01D", ref fH01D, value);
        //    }
        //}

        //private string fH02P;
        //[Persistent("H02P"), System.ComponentModel.DisplayName("H02P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H02P
        //{
        //    get
        //    {
        //        return fH02P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H02P", ref fH02P, value);
        //    }
        //}

        //private string fH02D;
        //[Persistent("H02D"), System.ComponentModel.DisplayName("H02D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H02D
        //{
        //    get
        //    {
        //        return fH02D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H02D", ref fH02D, value);
        //    }
        //}
        //private string fH03P;
        //[Persistent("H03P"), System.ComponentModel.DisplayName("H03P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H03P
        //{
        //    get
        //    {
        //        return fH03P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H03P", ref fH03P, value);
        //    }
        //}

        //private string fH03D;
        //[Persistent("H03D"), System.ComponentModel.DisplayName("H03D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H03D
        //{
        //    get
        //    {
        //        return fH03D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H03D", ref fH03D, value);
        //    }
        //}
        //private string fH04P;
        //[Persistent("H04P"), System.ComponentModel.DisplayName("H04P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H04P
        //{
        //    get
        //    {
        //        return fH04P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H04P", ref fH04P, value);
        //    }
        //}

        //private string fH04D;
        //[Persistent("H04D"), System.ComponentModel.DisplayName("H04D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H04D
        //{
        //    get
        //    {
        //        return fH04D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H04D", ref fH04D, value);
        //    }
        //}
        //private string fH05P;
        //[Persistent("H05P"), System.ComponentModel.DisplayName("H05P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H05P
        //{
        //    get
        //    {
        //        return fH05P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H05P", ref fH05P, value);
        //    }
        //}

        //private string fH05D;
        //[Persistent("H05D"), System.ComponentModel.DisplayName("H05D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H05D
        //{
        //    get
        //    {
        //        return fH05D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H05D", ref fH05D, value);
        //    }
        //}
        //private string fH06P;
        //[Persistent("H06P"), System.ComponentModel.DisplayName("H06P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H06P
        //{
        //    get
        //    {
        //        return fH06P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H06P", ref fH06P, value);
        //    }
        //}

        //private string fH06D;
        //[Persistent("H06D"), System.ComponentModel.DisplayName("H06D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H06D
        //{
        //    get
        //    {
        //        return fH06D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H06D", ref fH06D, value);
        //    }
        //}
        //private string fH07P;
        //[Persistent("H07P"), System.ComponentModel.DisplayName("H07P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H07P
        //{
        //    get
        //    {
        //        return fH07P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H07P", ref fH07P, value);
        //    }
        //}

        //private string fH07D;
        //[Persistent("H07D"), System.ComponentModel.DisplayName("H07D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H07D
        //{
        //    get
        //    {
        //        return fH07D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H07D", ref fH07D, value);
        //    }
        //}
        //private string fH08P;
        //[Persistent("H08P"), System.ComponentModel.DisplayName("H08P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H08P
        //{
        //    get
        //    {
        //        return fH08P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H08P", ref fH08P, value);
        //    }
        //}

        //private string fH08D;
        //[Persistent("H08D"), System.ComponentModel.DisplayName("H08D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H08D
        //{
        //    get
        //    {
        //        return fH08D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H08D", ref fH08D, value);
        //    }
        //}
        //private string fH09P;
        //[Persistent("H09P"), System.ComponentModel.DisplayName("H09P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H09P
        //{
        //    get
        //    {
        //        return fH09P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H09P", ref fH09P, value);
        //    }
        //}

        //private string fH09D;
        //[Persistent("H09D"), System.ComponentModel.DisplayName("H09D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H09D
        //{
        //    get
        //    {
        //        return fH09D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H09D", ref fH09D, value);
        //    }
        //}
        //private string fH10P;
        //[Persistent("H10P"), System.ComponentModel.DisplayName("H10P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H10P
        //{
        //    get
        //    {
        //        return fH10P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H10P", ref fH10P, value);
        //    }
        //}

        //private string fH10D;
        //[Persistent("H10D"), System.ComponentModel.DisplayName("H10D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H10D
        //{
        //    get
        //    {
        //        return fH10D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H10D", ref fH10D, value);
        //    }
        //}
        //private string fH11P;
        //[Persistent("H11P"), System.ComponentModel.DisplayName("H11P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H11P
        //{
        //    get
        //    {
        //        return fH11P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H11P", ref fH11P, value);
        //    }
        //}

        //private string fH11D;
        //[Persistent("H11D"), System.ComponentModel.DisplayName("H11D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H11D
        //{
        //    get
        //    {
        //        return fH11D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H11D", ref fH11D, value);
        //    }
        //}
        //private string fH12P;
        //[Persistent("H12P"), System.ComponentModel.DisplayName("H12P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H12P
        //{
        //    get
        //    {
        //        return fH12P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H12P", ref fH12P, value);
        //    }
        //}

        //private string fH12D;
        //[Persistent("H12D"), System.ComponentModel.DisplayName("H12D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H12D
        //{
        //    get
        //    {
        //        return fH12D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H12D", ref fH12D, value);
        //    }
        //}
        //private string fH13P;
        //[Persistent("H13P"), System.ComponentModel.DisplayName("H13P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H13P
        //{
        //    get
        //    {
        //        return fH13P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H13P", ref fH13P, value);
        //    }
        //}

        //private string fH13D;
        //[Persistent("H13D"), System.ComponentModel.DisplayName("H13D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H13D
        //{
        //    get
        //    {
        //        return fH13D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H13D", ref fH13D, value);
        //    }
        //}
        //private string fH14P;
        //[Persistent("H14P"), System.ComponentModel.DisplayName("H14P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H14P
        //{
        //    get
        //    {
        //        return fH14P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H14P", ref fH14P, value);
        //    }
        //}

        //private string fH14D;
        //[Persistent("H14D"), System.ComponentModel.DisplayName("H14D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H14D
        //{
        //    get
        //    {
        //        return fH14D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H14D", ref fH14D, value);
        //    }
        //}
        //private string fH15P;
        //[Persistent("H15P"), System.ComponentModel.DisplayName("H15P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H15P
        //{
        //    get
        //    {
        //        return fH15P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H15P", ref fH15P, value);
        //    }
        //}

        //private string fH15D;
        //[Persistent("H15D"), System.ComponentModel.DisplayName("H15D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H15D
        //{
        //    get
        //    {
        //        return fH15D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H15D", ref fH15D, value);
        //    }
        //}
        //private string fH16P;
        //[Persistent("H16P"), System.ComponentModel.DisplayName("H16P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H16P
        //{
        //    get
        //    {
        //        return fH16P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H16P", ref fH16P, value);
        //    }
        //}

        //private string fH16D;
        //[Persistent("H16D"), System.ComponentModel.DisplayName("H16D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H16D
        //{
        //    get
        //    {
        //        return fH16D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H16D", ref fH16D, value);
        //    }
        //}
        //private string fH17P;
        //[Persistent("H17P"), System.ComponentModel.DisplayName("H17P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H17P
        //{
        //    get
        //    {
        //        return fH17P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H17P", ref fH17P, value);
        //    }
        //}

        //private string fH17D;
        //[Persistent("H17D"), System.ComponentModel.DisplayName("H17D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H17D
        //{
        //    get
        //    {
        //        return fH17D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H17D", ref fH17D, value);
        //    }
        //}
        //private string fH18P;
        //[Persistent("H18P"), System.ComponentModel.DisplayName("H18P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H18P
        //{
        //    get
        //    {
        //        return fH18P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H18P", ref fH18P, value);
        //    }
        //}

        //private string fH18D;
        //[Persistent("H18D"), System.ComponentModel.DisplayName("H18D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H18D
        //{
        //    get
        //    {
        //        return fH18D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H18D", ref fH18D, value);
        //    }
        //}
        //private string fH19P;
        //[Persistent("H19P"), System.ComponentModel.DisplayName("H19P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H19P
        //{
        //    get
        //    {
        //        return fH19P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H19P", ref fH19P, value);
        //    }
        //}

        //private string fH19D;
        //[Persistent("H19D"), System.ComponentModel.DisplayName("H19D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H19D
        //{
        //    get
        //    {
        //        return fH19D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H19D", ref fH19D, value);
        //    }
        //}
        //private string fH20P;
        //[Persistent("H20P"), System.ComponentModel.DisplayName("H20P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H20P
        //{
        //    get
        //    {
        //        return fH20P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H20P", ref fH20P, value);
        //    }
        //}

        //private string fH20D;
        //[Persistent("H20D"), System.ComponentModel.DisplayName("H20D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H20D
        //{
        //    get
        //    {
        //        return fH20D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H20D", ref fH20D, value);
        //    }
        //}
        //private string fH21P;
        //[Persistent("H21P"), System.ComponentModel.DisplayName("H21P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H21P
        //{
        //    get
        //    {
        //        return fH21P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H21P", ref fH21P, value);
        //    }
        //}

        //private string fH21D;
        //[Persistent("H21D"), System.ComponentModel.DisplayName("H21D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H21D
        //{
        //    get
        //    {
        //        return fH21D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H21D", ref fH21D, value);
        //    }
        //}
        //private string fH22P;
        //[Persistent("H22P"), System.ComponentModel.DisplayName("H22P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H22P
        //{
        //    get
        //    {
        //        return fH22P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H22P", ref fH22P, value);
        //    }
        //}

        //private string fH22D;
        //[Persistent("H22D"), System.ComponentModel.DisplayName("H22D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H22D
        //{
        //    get
        //    {
        //        return fH22D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H22D", ref fH22D, value);
        //    }
        //}
        //private string fH23P;
        //[Persistent("H23P"), System.ComponentModel.DisplayName("H23P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H23P
        //{
        //    get
        //    {
        //        return fH23P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H23P", ref fH23P, value);
        //    }
        //}

        //private string fH23D;
        //[Persistent("H23D"), System.ComponentModel.DisplayName("H23D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H23D
        //{
        //    get
        //    {
        //        return fH23D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H23D", ref fH23D, value);
        //    }
        //}
        //private string fH24P;
        //[Persistent("H24P"), System.ComponentModel.DisplayName("H24P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H24P
        //{
        //    get
        //    {
        //        return fH24P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H24P", ref fH24P, value);
        //    }
        //}

        //private string fH24D;
        //[Persistent("H24D"), System.ComponentModel.DisplayName("H24D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H24D
        //{
        //    get
        //    {
        //        return fH24D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H24D", ref fH24D, value);
        //    }
        //}
        //private string fH25P;
        //[Persistent("H25P"), System.ComponentModel.DisplayName("H25P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H25P
        //{
        //    get
        //    {
        //        return fH25P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H25P", ref fH25P, value);
        //    }
        //}

        //private string fH25D;
        //[Persistent("H25D"), System.ComponentModel.DisplayName("H25D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(250)")]
        //public string H25D
        //{
        //    get
        //    {
        //        return fH25D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H25D", ref fH25D, value);
        //    }
        //}
        //private string fH26P;
        //[Persistent("H26P"), System.ComponentModel.DisplayName("H26P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(260)")]
        //public string H26P
        //{
        //    get
        //    {
        //        return fH26P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H26P", ref fH26P, value);
        //    }
        //}

        //private string fH26D;
        //[Persistent("H26D"), System.ComponentModel.DisplayName("H26D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(260)")]
        //public string H26D
        //{
        //    get
        //    {
        //        return fH26D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H26D", ref fH26D, value);
        //    }
        //}
        //private string fH27P;
        //[Persistent("H27P"), System.ComponentModel.DisplayName("H27P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(270)")]
        //public string H27P
        //{
        //    get
        //    {
        //        return fH27P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H27P", ref fH27P, value);
        //    }
        //}

        //private string fH27D;
        //[Persistent("H27D"), System.ComponentModel.DisplayName("H27D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(270)")]
        //public string H27D
        //{
        //    get
        //    {
        //        return fH27D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H27D", ref fH27D, value);
        //    }
        //}
        //private string fH28P;
        //[Persistent("H28P"), System.ComponentModel.DisplayName("H28P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(280)")]
        //public string H28P
        //{
        //    get
        //    {
        //        return fH28P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H28P", ref fH28P, value);
        //    }
        //}

        //private string fH28D;
        //[Persistent("H28D"), System.ComponentModel.DisplayName("H28D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(280)")]
        //public string H28D
        //{
        //    get
        //    {
        //        return fH28D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H28D", ref fH28D, value);
        //    }
        //}
        //private string fH29P;
        //[Persistent("H29P"), System.ComponentModel.DisplayName("H29P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(290)")]
        //public string H29P
        //{
        //    get
        //    {
        //        return fH29P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H29P", ref fH29P, value);
        //    }
        //}

        //private string fH29D;
        //[Persistent("H29D"), System.ComponentModel.DisplayName("H29D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(290)")]
        //public string H29D
        //{
        //    get
        //    {
        //        return fH29D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H29D", ref fH29D, value);
        //    }
        //}
        //private string fH30P;
        //[Persistent("H30P"), System.ComponentModel.DisplayName("H30P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(300)")]
        //public string H30P
        //{
        //    get
        //    {
        //        return fH30P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H30P", ref fH30P, value);
        //    }
        //}

        //private string fH30D;
        //[Persistent("H30D"), System.ComponentModel.DisplayName("H30D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(300)")]
        //public string H30D
        //{
        //    get
        //    {
        //        return fH30D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H30D", ref fH30D, value);
        //    }
        //}
        //private string fH31P;
        //[Persistent("H31P"), System.ComponentModel.DisplayName("H31P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H31P
        //{
        //    get
        //    {
        //        return fH31P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H31P", ref fH31P, value);
        //    }
        //}

        //private string fH31D;
        //[Persistent("H31D"), System.ComponentModel.DisplayName("H31D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H31D
        //{
        //    get
        //    {
        //        return fH31D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H31D", ref fH31D, value);
        //    }
        //}
        //private string fH32P;
        //[Persistent("H32P"), System.ComponentModel.DisplayName("H32P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H32P
        //{
        //    get
        //    {
        //        return fH32P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H32P", ref fH32P, value);
        //    }
        //}

        //private string fH32D;
        //[Persistent("H32D"), System.ComponentModel.DisplayName("H32D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H32D
        //{
        //    get
        //    {
        //        return fH32D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H32D", ref fH32D, value);
        //    }
        //}

        //private string fH33P;
        //[Persistent("H33P"), System.ComponentModel.DisplayName("H33P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H33P
        //{
        //    get
        //    {
        //        return fH33P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H33P", ref fH33P, value);
        //    }
        //}

        //private string fH33D;
        //[Persistent("H33D"), System.ComponentModel.DisplayName("H33D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H33D
        //{
        //    get
        //    {
        //        return fH33D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H33D", ref fH33D, value);
        //    }
        //}
        //private string fH34P;
        //[Persistent("H34P"), System.ComponentModel.DisplayName("H34P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H34P
        //{
        //    get
        //    {
        //        return fH34P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H34P", ref fH34P, value);
        //    }
        //}

        //private string fH34D;
        //[Persistent("H34D"), System.ComponentModel.DisplayName("H34D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H34D
        //{
        //    get
        //    {
        //        return fH34D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H34D", ref fH34D, value);
        //    }
        //}
        //private string fH35P;
        //[Persistent("H35P"), System.ComponentModel.DisplayName("H35P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H35P
        //{
        //    get
        //    {
        //        return fH35P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H35P", ref fH35P, value);
        //    }
        //}

        //private string fH35D;
        //[Persistent("H35D"), System.ComponentModel.DisplayName("H35D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H35D
        //{
        //    get
        //    {
        //        return fH35D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H35D", ref fH35D, value);
        //    }
        //}
        //private string fH36P;
        //[Persistent("H36P"), System.ComponentModel.DisplayName("H36P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H36P
        //{
        //    get
        //    {
        //        return fH36P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H36P", ref fH36P, value);
        //    }
        //}

        //private string fH36D;
        //[Persistent("H36D"), System.ComponentModel.DisplayName("H36D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H36D
        //{
        //    get
        //    {
        //        return fH36D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H36D", ref fH36D, value);
        //    }
        //}
        //private string fH37P;
        //[Persistent("H37P"), System.ComponentModel.DisplayName("H37P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H37P
        //{
        //    get
        //    {
        //        return fH37P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H37P", ref fH37P, value);
        //    }
        //}

        //private string fH37D;
        //[Persistent("H37D"), System.ComponentModel.DisplayName("H37D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H37D
        //{
        //    get
        //    {
        //        return fH37D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H37D", ref fH37D, value);
        //    }
        //}
        //private string fH38P;
        //[Persistent("H38P"), System.ComponentModel.DisplayName("H38P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H38P
        //{
        //    get
        //    {
        //        return fH38P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H38P", ref fH38P, value);
        //    }
        //}

        //private string fH38D;
        //[Persistent("H38D"), System.ComponentModel.DisplayName("H38D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H38D
        //{
        //    get
        //    {
        //        return fH38D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H38D", ref fH38D, value);
        //    }
        //}
        //private string fH39P;
        //[Persistent("H39P"), System.ComponentModel.DisplayName("H39P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H39P
        //{
        //    get
        //    {
        //        return fH39P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H39P", ref fH39P, value);
        //    }
        //}

        //private string fH39D;
        //[Persistent("H39D"), System.ComponentModel.DisplayName("H39D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H39D
        //{
        //    get
        //    {
        //        return fH39D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H39D", ref fH39D, value);
        //    }
        //}
        //private string fH40P;
        //[Persistent("H40P"), System.ComponentModel.DisplayName("H40P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H40P
        //{
        //    get
        //    {
        //        return fH40P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H40P", ref fH40P, value);
        //    }
        //}

        //private string fH40D;
        //[Persistent("H40D"), System.ComponentModel.DisplayName("H40D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H40D
        //{
        //    get
        //    {
        //        return fH40D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H40D", ref fH40D, value);
        //    }
        //}
        //private string fH41P;
        //[Persistent("H41P"), System.ComponentModel.DisplayName("H41P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H41P
        //{
        //    get
        //    {
        //        return fH41P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H41P", ref fH41P, value);
        //    }
        //}

        //private string fH41D;
        //[Persistent("H41D"), System.ComponentModel.DisplayName("H41D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H41D
        //{
        //    get
        //    {
        //        return fH41D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H41D", ref fH41D, value);
        //    }
        //}
        //private string fH42P;
        //[Persistent("H42P"), System.ComponentModel.DisplayName("H42P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H42P
        //{
        //    get
        //    {
        //        return fH42P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H42P", ref fH42P, value);
        //    }
        //}

        //private string fH42D;
        //[Persistent("H42D"), System.ComponentModel.DisplayName("H42D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H42D
        //{
        //    get
        //    {
        //        return fH42D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H42D", ref fH42D, value);
        //    }
        //}
        //private string fH43P;
        //[Persistent("H43P"), System.ComponentModel.DisplayName("H43P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H43P
        //{
        //    get
        //    {
        //        return fH43P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H43P", ref fH43P, value);
        //    }
        //}

        //private string fH43D;
        //[Persistent("H43D"), System.ComponentModel.DisplayName("H43D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H43D
        //{
        //    get
        //    {
        //        return fH43D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H43D", ref fH43D, value);
        //    }
        //}
        //private string fH44P;
        //[Persistent("H44P"), System.ComponentModel.DisplayName("H44P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H44P
        //{
        //    get
        //    {
        //        return fH44P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H44P", ref fH44P, value);
        //    }
        //}

        //private string fH44D;
        //[Persistent("H44D"), System.ComponentModel.DisplayName("H44D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H44D
        //{
        //    get
        //    {
        //        return fH44D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H44D", ref fH44D, value);
        //    }
        //}
        //private string fH45P;
        //[Persistent("H45P"), System.ComponentModel.DisplayName("H45P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H45P
        //{
        //    get
        //    {
        //        return fH45P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H45P", ref fH45P, value);
        //    }
        //}

        //private string fH45D;
        //[Persistent("H45D"), System.ComponentModel.DisplayName("H45D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H45D
        //{
        //    get
        //    {
        //        return fH45D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H45D", ref fH45D, value);
        //    }
        //}
        //private string fH46P;
        //[Persistent("H46P"), System.ComponentModel.DisplayName("H46P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H46P
        //{
        //    get
        //    {
        //        return fH46P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H46P", ref fH46P, value);
        //    }
        //}

        //private string fH46D;
        //[Persistent("H46D"), System.ComponentModel.DisplayName("H46D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H46D
        //{
        //    get
        //    {
        //        return fH46D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H46D", ref fH46D, value);
        //    }
        //}
        //private string fH47P;
        //[Persistent("H47P"), System.ComponentModel.DisplayName("H47P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H47P
        //{
        //    get
        //    {
        //        return fH47P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H47P", ref fH47P, value);
        //    }
        //}

        //private string fH47D;
        //[Persistent("H47D"), System.ComponentModel.DisplayName("H47D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H47D
        //{
        //    get
        //    {
        //        return fH47D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H47D", ref fH47D, value);
        //    }
        //}
        //private string fH48P;
        //[Persistent("H48P"), System.ComponentModel.DisplayName("H48P")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H48P
        //{
        //    get
        //    {
        //        return fH48P;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H48P", ref fH48P, value);
        //    }
        //}

        //private string fH48D;
        //[Persistent("H48D"), System.ComponentModel.DisplayName("H48D")]
        //[VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true)]
        //[DbType("varchar(320)")]
        //public string H48D
        //{
        //    get
        //    {
        //        return fH48D;
        //    }
        //    set
        //    {
        //        SetPropertyValue<string>("H48D", ref fH48D, value);
        //    }
        //}

        //COD	VARCHAR2(150)
        //DATAGIORNO	DATE
        //ORAMINNUM	NUMBER
        //REGRDL	NUMBER
        //RISORSA	NUMBER
        //RISORSADESC	VARCHAR2(150)
        //UTENTE	VARCHAR2(150)
        //CENTROOPERATIVO	VARCHAR2(150)
        //H01P	VARCHAR2(250)
        //H01D	VARCHAR2(250)
        //H02P	VARCHAR2(250)
        //H02D	VARCHAR2(250)
        //H03P	VARCHAR2(250)
        //H03D	VARCHAR2(250)
        //H04P	VARCHAR2(250)
        //H04D	VARCHAR2(250)
        //H05P	VARCHAR2(250)
        //H05D	VARCHAR2(250)
        //H06P	VARCHAR2(250)
        //H06D	VARCHAR2(250)
        //H07P	VARCHAR2(250)
        //H07D	VARCHAR2(250)
        //H08P	VARCHAR2(250)
        //H08D	VARCHAR2(250)
        //H09P	VARCHAR2(250)
        //H09D	VARCHAR2(250)
        //H10P	VARCHAR2(250)
        //H10D	VARCHAR2(250)
        //H11P	VARCHAR2(250)
        //H11D	VARCHAR2(250)
        //H12P	VARCHAR2(250)
        //H12D	VARCHAR2(250)
        //H13P	VARCHAR2(250)
        //H13D	VARCHAR2(250)
        //H14P	VARCHAR2(250)
        //H14D	VARCHAR2(250)
        //H15P	VARCHAR2(250)
        //H15D	VARCHAR2(250)
        //H16P	VARCHAR2(250)
        //H16D	VARCHAR2(250)
        //H17P	VARCHAR2(250)
        //H17D	VARCHAR2(250)
        //H18P	VARCHAR2(250)
        //H18D	VARCHAR2(250)
        //H19P	VARCHAR2(250)
        //H19D	VARCHAR2(250)
        //H20P	VARCHAR2(250)
        //H20D	VARCHAR2(250)
        //H21P	VARCHAR2(250)
        //H21D	VARCHAR2(250)
        //H22P	VARCHAR2(250)
        //H22D	VARCHAR2(250)
        //H23P	VARCHAR2(250)
        //H23D	VARCHAR2(250)
        //H24P	VARCHAR2(250)
        //H24D	VARCHAR2(250)
        //H25P	VARCHAR2(250)
        //H25D	VARCHAR2(250)
        //H26P	VARCHAR2(250)
        //H26D	VARCHAR2(250)
        //H27P	VARCHAR2(250)
        //H27D	VARCHAR2(250)
        //H28P	VARCHAR2(250)
        //H28D	VARCHAR2(250)
        //H29P	VARCHAR2(250)
        //H29D	VARCHAR2(250)
        //H30P	VARCHAR2(250)
        //H30D	VARCHAR2(250)
        //H31P	VARCHAR2(250)
        //H31D	VARCHAR2(250)
        //H32P	VARCHAR2(250)
        //H32D	VARCHAR2(250)
        //H33P	VARCHAR2(250)
        //H33D	VARCHAR2(250)
        //H34P	VARCHAR2(250)
        //H34D	VARCHAR2(250)
        //H35P	VARCHAR2(250)
        //H35D	VARCHAR2(250)
        //H36P	VARCHAR2(250)
        //H36D	VARCHAR2(250)
        //H37P	VARCHAR2(250)
        //H37D	VARCHAR2(250)
        //H38P	VARCHAR2(250)
        //H38D	VARCHAR2(250)
        //H39P	VARCHAR2(250)
        //H39D	VARCHAR2(250)
        //H40P	VARCHAR2(250)
        //H40D	VARCHAR2(250)
        //H41P	VARCHAR2(250)
        //H41D	VARCHAR2(250)
        //H42P	VARCHAR2(250)
        //H42D	VARCHAR2(250)
        //H43P	VARCHAR2(250)
        //H43D	VARCHAR2(250)
        //H44P	VARCHAR2(250)
        //H44D	VARCHAR2(250)
        //H45P	VARCHAR2(250)
        //H45D	VARCHAR2(250)
        //H46P	VARCHAR2(250)
        //H46D	VARCHAR2(250)
        //H47P	VARCHAR2(250)
        //H47D	VARCHAR2(250)
        //H48P	VARCHAR2(250)
        //H48D	VARCHAR2(250)
        //DATAORA	DATE







    }
}


