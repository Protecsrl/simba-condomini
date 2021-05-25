using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System.Drawing;
using DevExpress.ExpressApp.Model;

namespace CAMS.Module.DBPlant.Vista
{
    [DefaultClassOptions, Persistent("V_APPARATO_LIST_QRCODE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Asset Qr-Code")]
    [ImageName("LoadPageSetup")]
    [NavigationItem("Patrimonio")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]      [ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone
 

    #endregion


    public class ApparatoListViewQrCode : XPLiteObject
    {
        public ApparatoListViewQrCode() : base() { }
        public ApparatoListViewQrCode(Session session) : base(session) { }

        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

        private string fcodice;
        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
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

        
        private int foidcommessa;
        [Persistent("OIDCOMMESSA"), Size(100), DevExpress.Xpo.DisplayName("Oid Commessa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [Delayed(true)]
        public int OidCommessa
        {
            get { return GetDelayedPropertyValue<int>("OidCommessa"); }
            set { SetDelayedPropertyValue<int>("OidCommessa", value); }
        }

        private string fcommessa;
        [Persistent("COMMESSA_DESCRIZIONE"), DevExpress.Xpo.DisplayName("Commessa")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Commessa
        {
            get { return GetDelayedPropertyValue<string>("Commessa"); }
            set { SetDelayedPropertyValue<string>("Commessa", value); }
        }


        [Persistent("OIDEDIFICIO")]
        private int foidedificio;

        [PersistentAlias("foidedificio")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int OidEdificio
        {
            get { return foidedificio; }
        }


        [Persistent("EDIFICIO_DESCRIZIONE")]
        [DbType("varchar(500)"), Size(500)]
        private string fedificio_descrizione;

        [PersistentAlias("fedificio_descrizione")]
        [DevExpress.Xpo.DisplayName("Immobile")]

        public string Edificio_Descrizione
        {
            get { return fedificio_descrizione; }
        }


        [Persistent("OIDIMPIANTO")]
        private int foidimpianto;

        [PersistentAlias("foidimpianto")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(false)]
        public int OidImpianto
        {
            get { return foidimpianto; }
        }


        [Persistent("IMPIANTO_DESCRIZIONE")]
        [DbType("varchar(500)"), Size(500)]
        private string fimpianto_descrizione;

        [PersistentAlias("fimpianto_descrizione")]
        [DevExpress.Xpo.DisplayName("Impianto")]
        public string Impianto_Descrizione
        {
            get { return fimpianto_descrizione; }
        }

        [Persistent("OIDAPPARATO")]
        private int foidapparato;

        [Persistent("OIDAPPARATOSTDCLASSI")]
        private int fOidstdapparatoclassi;

        [PersistentAlias("fOidstdapparatoclassi")]
        [DevExpress.Xpo.DisplayName("Oid Classe di Tipo Apparato")]
        [System.ComponentModel.Browsable(false)]
        public int OidStdApparatoClassi
        {
            get { return fOidstdapparatoclassi; }
        }

        [PersistentAlias("foidapparato")]
        [System.ComponentModel.Browsable(false)]
        public int OidApparato
        {
            get { return foidapparato; }
        }


        [Persistent("DESCRIZIONE")]
        [DbType("varchar(500)"), Size(500)]
        private string fdescrizione;

        [PersistentAlias("fdescrizione")]
        [DevExpress.Xpo.DisplayName("Descrizione")]
        public string Descrizione
        {
            get { return fdescrizione; }
        }

        [Persistent("COD_DESCRIZIONE")]
        [DbType("varchar(50)"), Size(50)]
        private string fCodDescrizione;

        [PersistentAlias("fCodDescrizione")]
        [DevExpress.Xpo.DisplayName("Cod Descrizione")]
        public string CodDescrizione
        {
            get { return fCodDescrizione; }
        }


        private string fStdApparato;
        [Persistent("STDAPPARATO"), DevExpress.Xpo.DisplayName("Tipo Apparato")]
        [DbType("varchar(400)"), Size(400)]
        [Delayed(true)]
        public string StdApparato
        {
            get { return GetDelayedPropertyValue<string>("StdApparato"); }
            set { SetDelayedPropertyValue<string>("StdApparato", value); }
        }

        private int fQuantita;
        [Persistent("QUANTITA"), DevExpress.Xpo.DisplayName("Quantità"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [Delayed(true)]
        public int Quantita
        {
            get { return GetDelayedPropertyValue<int>("Quantita"); }
            set { SetDelayedPropertyValue<int>("Quantita", value); }
        }

               private string fStrada;
        [Persistent("STRADA")]
        [DevExpress.Xpo.DisplayName("Strada")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Strada
        {
            get { return GetDelayedPropertyValue<string>("Strada"); }
            set { SetDelayedPropertyValue<string>("Strada", value); }
        }

        private string fApparatoPadre;
        [Persistent("APPARATOPADRE"), DevExpress.Xpo.DisplayName("Apparato Padre")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string ApparatoPadre
        {
            get
            {
                return GetDelayedPropertyValue<string>("ApparatoPadre");
            }
            set
            {
                SetDelayedPropertyValue<string>("ApparatoPadre", value);
            }
        }

        private string fApparatoSostegno;
        // [Browsable(false)]
        [Persistent("APPARATOSOSTEGNO"), DevExpress.Xpo.DisplayName("Apparato di Sostegno")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string ApparatoSostegno
        {
            get
            {
                return GetDelayedPropertyValue<string>("ApparatoSostegno");
            }
            set
            {
                SetDelayedPropertyValue<string>("ApparatoSostegno", value);
            }
        }


        private const string UrlStringEditMask = @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})";
        private string _UrlQrCode;
        [Persistent("URLQRCODE")]
        //[System.ComponentModel.Browsable(false)]
        [VisibleInListView(false), VisibleInDetailView(false)]
        [ModelDefault("EditMaskType", "RegEx")]
        [ModelDefault("EditMask", UrlStringEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Specify a web or email address in the following format: " + UrlStringEditMask, null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [Delayed(true)]
        public string UrlQrCode
        {
            get { return GetDelayedPropertyValue<string>("UrlQrCode"); }
            set { SetDelayedPropertyValue<string>("UrlQrCode", value); }
        }


        public override string ToString()
        {
            if (this == null) return null;
            if (this.Descrizione == null) return null;
            if (this.CodDescrizione == null) return Descrizione.ToString();

            //if (this.Tag == null) return Descrizione.ToString();
            return string.Format("{0}({1})", Descrizione, CodDescrizione);
        }


    }
}


//namespace CAMS.Module.DBPlant.Vista
//{
//    public class ApparatoListView : XPLiteObject
//    {
//        public ApparatoListView() : base() { }
//        public ApparatoListView(Session session) : base(session) { }

//        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm";

//        private string fcodice;
//        [Key, Persistent("CODICE"), MemberDesignTimeVisibility(false)]
//        [DbType("varchar(250)")]
//        public string Codice
//        {
//            get
//            {
//                return fcodice;
//            }
//            set
//            {
//                SetPropertyValue<string>("Codice", ref fcodice, value);
//            }
//        }


//        private string fMarca;
//        [Persistent("MARCA"), DevExpress.Xpo.DisplayName("Marca")]
//        [DbType("varchar(100)"), Size(100)]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [Delayed(true)]
//        public string Marca
//        {
//            get { return GetDelayedPropertyValue<string>("Marca"); }
//            set { SetDelayedPropertyValue<string>("Marca", value); }
//        }

//        private string fModello;
//        [Persistent("MODELLO"), DevExpress.Xpo.DisplayName("Modello")]
//        [DbType("varchar(100)"), Size(100)]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [Delayed(true)]
//        public string Modello
//        {
//            get { return GetDelayedPropertyValue<string>("Modello"); }
//            set { SetDelayedPropertyValue<string>("Modello", value); }
//        }



//        #region caratteristiche tecniche
//        private string fMatricola;
//        [Persistent("MATRICOLA"), DevExpress.Xpo.DisplayName("Matricola")]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [DbType("varchar(100)"), Size(100)]
//        [Delayed(true)]
//        public string Matricola
//        {
//            get { return GetDelayedPropertyValue<string>("Matricola"); }
//            set { SetDelayedPropertyValue<string>("Matricola", value); }
//        }

//        private EntitaApparato fEntitaApparato;
//        [Persistent("ENTITAAPPARATO"), DevExpress.Xpo.DisplayName("Classe")]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [ExplicitLoading()]
//        [Delayed(true)]
//        public EntitaApparato EntitaApparato
//        {
//            get { return GetDelayedPropertyValue<EntitaApparato>("EntitaApparato"); }
//            set { SetDelayedPropertyValue<EntitaApparato>("EntitaApparato", value); }
//        }

//        private string fTag;
//        [Persistent("TARGHETTA"), DevExpress.Xpo.DisplayName(@"Tag P&I")]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [DbType("varchar(100)"), Size(100)]
//        [Delayed(true)]
//        public string Tag
//        {
//            get { return GetDelayedPropertyValue<string>("Tag"); }
//            set { SetDelayedPropertyValue<string>("Tag", value); }
//        }

//        private string fFluidoPrimario;
//        [Persistent("FLUIDOPRIMARIO"), DevExpress.Xpo.DisplayName("Fluido Primario")]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [DbType("varchar(100)"), Size(100)]
//        [Delayed(true)]
//        public string FluidoPrimario
//        {
//            get { return GetDelayedPropertyValue<string>("FluidoPrimario"); }
//            set { SetDelayedPropertyValue<string>("FluidoPrimario", value); }
//        }


//        private string fLocale;
//        [Persistent("LOCALE"), DevExpress.Xpo.DisplayName("Locali")]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [DbType("varchar(500)"), Size(500)]
//        [Delayed(true)]
//        public string Locale
//        {
//            get { return GetDelayedPropertyValue<string>("Locale"); }
//            set { SetDelayedPropertyValue<string>("Locale", value); }
//        }

//        #endregion

//        #region    ----------   RELAZIONE PADE FIGLIO   ------------------------------------------------------------------------------------
     

//        [Persistent("APPARATOMP"), DevExpress.Xpo.DisplayName("Apparato di Aggregazione MP")]
//        [VisibleInDetailView(true), VisibleInListView(false)]
//        [DbType("varchar(500)"), Size(500)]
//        [Delayed(true)]
//        public string ApparatoMP
//        {
//            get
//            {
//                return GetDelayedPropertyValue<string>("ApparatoMP");
//            }
//            set
//            {
//                SetDelayedPropertyValue<string>("ApparatoMP", value);
//            }
//        }


//        [Persistent("APPARATOSOSTITUITODA"), DevExpress.Xpo.DisplayName("Apparato Sostituito Da")]
//        [VisibleInDetailView(true), VisibleInListView(false)]
//        [DbType("varchar(500)"), Size(500)]
//        [Delayed(true)]
//        public string ApparatoSostituitoDa
//        {
//            get
//            {
//                return GetDelayedPropertyValue<string>("ApparatoSostituitoDa");
//            }
//            set
//            {
//                SetDelayedPropertyValue<string>("ApparatoSostituitoDa", value);
//            }
//        }


//        #endregion

//        #region     ----------   DATE VALIDITA'   ------------------------------------------------------------------------------------
//        private DateTime fDateUnService;
//        [Persistent("DATAUNSERVICE")]
//        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
//        [VisibleInDetailView(true), VisibleInListView(false)]
//        [Appearance("ApparatoListView.Abilita.DateUnService", Criteria = "DateInService  is null", FontColor = "Black", Enabled = false)]
//        [Delayed(true)]
//        public DateTime DateUnService
//        {
//            get { return GetDelayedPropertyValue<DateTime>("DateUnService"); }
//            set { SetDelayedPropertyValue<DateTime>("DateUnService", value); }
//        }

//        private DateTime fDateInService;
//        [Persistent("DATAINSERVICE")]
//        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data in Servizio")]
//        [VisibleInListView(false), VisibleInLookupListView(false)]
//        [Appearance("ApparatoListView.DateInService.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(DateInService)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
//        [Appearance("ApparatoListView.Abilita.DateInService", Criteria = "not(DateUnService is null)", FontColor = "Black", Enabled = false)]
//        [Delayed(true)]
//        public DateTime DateInService
//        {
//            get
//            {
//                return GetDelayedPropertyValue<DateTime>("DateInService");
//            }
//            set
//            {
//                SetDelayedPropertyValue<DateTime>("DateInService", value);
//            }

//        }


//        private FlgAbilitato fAbilitato;
//        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
//        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
//        public FlgAbilitato Abilitato
//        {
//            get
//            {
//                return fAbilitato;
//            }
//            set
//            {
//                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
//            }
//        }

//        #endregion


//        #region SCENARIO E CLUSTEREDIFICI  popolati per riconoscere da chi sono schedulati

//        [Persistent("SCENARIO_CLUSTER"), DevExpress.Xpo.DisplayName("Scenario e Cluster")]
//        [VisibleInDetailView(true), VisibleInListView(false)]
//        [DbType("varchar(500)"), Size(500)]
//        [Delayed(true)]
//        public string ScenarioCluster
//        {
//            get
//            {
//                return GetDelayedPropertyValue<string>("ScenarioCluster");
//            }
//            set
//            {
//                SetDelayedPropertyValue<string>("ScenarioCluster", value);
//            }
//        }
//        #endregion



//        #region geolocalizzazione

 

//        //private GeoLocalizzazione fGeoLocalizzazione;
//        //[Persistent("GEOLOCALIZZAZIONE"), DevExpress.ExpressApp.DC.XafDisplayName("GeoLocalizzazione")]
//        //[VisibleInListView(false)]
//        //[ExplicitLoading()]
//        //public GeoLocalizzazione GeoLocalizzazione
//        //{

//        //    get { return fGeoLocalizzazione; }
//        //    set { SetPropertyValue<GeoLocalizzazione>("GeoLocalizzazione", ref fGeoLocalizzazione, value); }
//        //}



//        #endregion



  

//    }

//}

