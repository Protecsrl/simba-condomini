using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;// 
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
using System.Drawing;

namespace CAMS.Module.DBPlant.Vista
{
    [DefaultClassOptions, Persistent("V_APPARATO_LIST")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Sfoglia Apparato")]
    [ImageName("LoadPageSetup")]
    [NavigationItem("Patrimonio")]
    //[System.ComponentModel.DefaultProperty("Descrizione")]      [ DefaultListViewOptions(true, )]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowEdit", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowNew", "False")]
    [DevExpress.ExpressApp.Model.ModelDefault("AllowClear", "False")]

    #region filtro tampone

    [Appearance("ApparatoListView.Abilitato.BKColor.Red", TargetItems = "*", FontStyle = FontStyle.Strikeout, FontColor = "Salmon", Priority = 2,
        Criteria = "(Abilitato = 'No')")]

    [Appearance("ApparatoListView.Abilitato.BKColor.Brown", TargetItems = "*", FontColor = "Brown", Priority = 1,
        Criteria = "(Abilitato = 'No') And IsNullOrEmpty(ApparatoSostituitoDa)")]

 
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_Reali", "Abilitato == 1 And EntitaApparato == 1", "Attivi solo Reali", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_Virtuali", "Abilitato == 1 And EntitaApparato == 0", "Attivi solo Raggruppamento", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_mp", "Abilitato == 1 And EntitaApparato == 2", "Attivi solo Virtuale", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1", "Attivi",  Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0", "non Attivi", Index = 5)] 

    #endregion

    public class ApparatoListView : XPLiteObject
    {
        public ApparatoListView() : base() { }
        public ApparatoListView(Session session) : base(session) { }

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



        [Persistent("OIDAPPARATO")]
        private int foidapparato;

        [PersistentAlias("foidapparato")]
        [System.ComponentModel.Browsable(false)]
        [DevExpress.Xpo.DisplayName("OidApparato")]
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


        [Persistent("EDIFICIO_DESCRIZIONE")]
        [DbType("varchar(250)"), Size(250)]
        private string fEdificio_Descrizione;

        [PersistentAlias("fEdificio_Descrizione")]
        [DevExpress.Xpo.DisplayName("Immobile")]
        public string Edificio_Descrizione
        {
            get { return fEdificio_Descrizione; }
        }

        //Commessa_Descrizione
        [Persistent("COMMESSA_DESCRIZIONE")]
        [DbType("varchar(250)"), Size(250)]
        private string fCommessa_Descrizione;

        [PersistentAlias("fCommessa_Descrizione")]
        [DevExpress.Xpo.DisplayName("Commessa")]
        public string Commessa_Descrizione
        {
            get { return fCommessa_Descrizione; }
        }

        //Commessa_Descrizione
        [Persistent("SISTEMA")]
        [DbType("varchar(250)"), Size(250)]
        private string fSistema;

        [PersistentAlias("fSistema")]
        [DevExpress.Xpo.DisplayName("Impianto")]
        public string Sistema
        {
            get { return fSistema; }
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

        private string fMarca;
        [Persistent("MARCA"), DevExpress.Xpo.DisplayName("Marca")]
        [DbType("varchar(100)"), Size(100)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string Marca
        {
            get { return GetDelayedPropertyValue<string>("Marca"); }
            set { SetDelayedPropertyValue<string>("Marca", value); }
        }

        private string fModello;
        [Persistent("MODELLO"), DevExpress.Xpo.DisplayName("Modello")]
        [DbType("varchar(100)"), Size(100)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string Modello
        {
            get { return GetDelayedPropertyValue<string>("Modello"); }
            set { SetDelayedPropertyValue<string>("Modello", value); }
        }



        #region caratteristiche tecniche
        private string fMatricola;
        [Persistent("MATRICOLA"), DevExpress.Xpo.DisplayName("Matricola")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)"), Size(100)]
        [Delayed(true)]
        public string Matricola
        {
            get { return GetDelayedPropertyValue<string>("Matricola"); }
            set { SetDelayedPropertyValue<string>("Matricola", value); }
        }

        private EntitaAsset fEntitaApparato;
        [Persistent("ENTITAAPPARATO"), DevExpress.Xpo.DisplayName("Classe")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public EntitaAsset EntitaApparato
        {
            get { return GetDelayedPropertyValue<EntitaAsset>("EntitaApparato"); }
            set { SetDelayedPropertyValue<EntitaAsset>("EntitaApparato", value); }
        }

        private string fTag;
        [Persistent("TARGHETTA"), DevExpress.Xpo.DisplayName(@"Tag P&I")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)"), Size(100)]
        [Delayed(true)]
        public string Tag
        {
            get { return GetDelayedPropertyValue<string>("Tag"); }
            set { SetDelayedPropertyValue<string>("Tag", value); }
        }

        private string fFluidoPrimario;
        [Persistent("FLUIDOPRIMARIO"), DevExpress.Xpo.DisplayName("Fluido Primario")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(100)"), Size(100)]
        [Delayed(true)]
        public string FluidoPrimario
        {
            get { return GetDelayedPropertyValue<string>("FluidoPrimario"); }
            set { SetDelayedPropertyValue<string>("FluidoPrimario", value); }
        }
      
        private string fLocale;
        [Persistent("LOCALE"), DevExpress.Xpo.DisplayName("Locali")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string Locale
        {
            get { return GetDelayedPropertyValue<string>("Locale"); }
            set { SetDelayedPropertyValue<string>("Locale", value); }
        }

        #endregion

        #region    ----------   RELAZIONE PADE FIGLIO   ------------------------------------------------------------------------------------
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


        [Persistent("APPARATOMP"), DevExpress.Xpo.DisplayName("Apparato di Aggregazione MP")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string ApparatoMP
        {
            get
            {
                return GetDelayedPropertyValue<string>("ApparatoMP");
            }
            set
            {
                SetDelayedPropertyValue<string>("ApparatoMP", value);
            }
        }


        [Persistent("APPARATOSOSTITUITODA"), DevExpress.Xpo.DisplayName("Apparato Sostituito Da")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string ApparatoSostituitoDa
        {
            get
            {
                return GetDelayedPropertyValue<string>("ApparatoSostituitoDa");
            }
            set
            {
                SetDelayedPropertyValue<string>("ApparatoSostituitoDa", value);
            }
        }


        #endregion

        #region oid criteria
       
        [Persistent("OIDEDIFICIO"), DevExpress.Xpo.DisplayName("OidEdificio")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [Delayed(true)]
        public int OidEdificio
        {
            get
            {
                return GetDelayedPropertyValue<int>("OidEdificio");
            }
            set
            {
                SetDelayedPropertyValue<int>("OidEdificio", value);
            }
        }

        [Persistent("OIDCOMMESSA"), DevExpress.Xpo.DisplayName("OidCommessa")]
        [VisibleInDetailView(false), VisibleInListView(false)]
        [Delayed(true)]
        public int OidCommessa
        {
            get
            {
                return GetDelayedPropertyValue<int>("OidCommessa");
            }
            set
            {
                SetDelayedPropertyValue<int>("OidCommessa", value);
            }
        }

        private string fCentroCosto;
        [Persistent("CENTROCOSTO"), DevExpress.Xpo.DisplayName("Centro di Costo")]
        [DbType("varchar(50)"), Size(50)]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Delayed(true)]
        public string CentroCosto
        {
            get { return GetDelayedPropertyValue<string>("CentroCosto"); }
            set { SetDelayedPropertyValue<string>("CentroCosto", value); }
        }

        #endregion

        #region     ----------   DATE VALIDITA'   ------------------------------------------------------------------------------------
        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [Appearance("ApparatoListView.Abilita.DateUnService", Criteria = "DateInService  is null", FontColor = "Black", Enabled = false)]
        [Delayed(true)]
        public DateTime DateUnService
        {
            get { return GetDelayedPropertyValue<DateTime>("DateUnService"); }
            set { SetDelayedPropertyValue<DateTime>("DateUnService", value); }
        }

        private DateTime fDateInService;
        [Persistent("DATAINSERVICE")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data in Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [Appearance("ApparatoListView.DateInService.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(DateInService)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("ApparatoListView.Abilita.DateInService", Criteria = "not(DateUnService is null)", FontColor = "Black", Enabled = false)]
        [Delayed(true)]
        public DateTime DateInService
        {
            get
            {
                return GetDelayedPropertyValue<DateTime>("DateInService");
            }
            set
            {
                SetDelayedPropertyValue<DateTime>("DateInService", value);
            }

        }


        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        public FlgAbilitato Abilitato
        {
            get
            {
                return fAbilitato;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Abilitato", ref fAbilitato, value);
            }
        }

        #endregion


        #region SCENARIO E CLUSTEREDIFICI  popolati per riconoscere da chi sono schedulati

        [Persistent("SCENARIO_CLUSTER"), DevExpress.Xpo.DisplayName("Scenario e Cluster")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [DbType("varchar(500)"), Size(500)]
        [Delayed(true)]
        public string ScenarioCluster
        {
            get
            {
                return GetDelayedPropertyValue<string>("ScenarioCluster");
            }
            set
            {
                SetDelayedPropertyValue<string>("ScenarioCluster", value);
            }
        }
        #endregion



        #region geolocalizzazione

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

        private string fLongitude;
        [Persistent("LONGITUDE"),
        Size(50)]
        [DbType("varchar(50)")]
        //[RuleRequiredField("RReqField.Apparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Longitude
        {
            get
            {
                return fLongitude;
            }
            set
            {
                SetPropertyValue<string>("Longitude", ref fLongitude, value);
            }
        }


        private string fLatitude;
        [Persistent("LATITUDE"),
        Size(50)]
        [DbType("varchar(50)")]
        //[RuleRequiredField("RReqField.Apparato.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Latitude
        {
            get
            {
                return fLatitude;
            }
            set
            {
                SetPropertyValue<string>("Latitude", ref fLatitude, value);
            }
        }




        //private GeoLocalizzazione fGeoLocalizzazione;
        //[Persistent("GEOLOCALIZZAZIONE"), DevExpress.ExpressApp.DC.XafDisplayName("GeoLocalizzazione")]
        //[VisibleInListView(false)]
        //[ExplicitLoading()]
        //public GeoLocalizzazione GeoLocalizzazione
        //{

        //    get { return fGeoLocalizzazione; }
        //    set { SetPropertyValue<GeoLocalizzazione>("GeoLocalizzazione", ref fGeoLocalizzazione, value); }
        //}



        #endregion


        //[Persistent("OIDEDIFICIO"), DevExpress.Xpo.DisplayName("OidEdificio")]
        //[VisibleInDetailView(false), VisibleInListView(false)]
        //[Delayed(true)]
        //public int OidEdificio
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<int>("OidEdificio");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<int>("OidEdificio", value);
        //    }
        //}


        //[Persistent("OIDCOMMESSA"), DevExpress.Xpo.DisplayName("OidCommessa")]
        //[VisibleInDetailView(false), VisibleInListView(false)]
        //[Delayed(true)]
        //public int OidCommessa
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<int>("OidCommessa");
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<int>("OidCommessa", value);
        //    }
        //}

        //private string fCentroCosto;
        //[Persistent("CENTROCOSTO"), DevExpress.Xpo.DisplayName("Centro di Costo")]
        //[DbType("varchar(50)"), Size(50)]
        //[VisibleInListView(false), VisibleInLookupListView(false)]
        //[Delayed(true)]
        //public string CentroCosto
        //{
        //    get { return GetDelayedPropertyValue<string>("CentroCosto"); }
        //    set { SetDelayedPropertyValue<string>("CentroCosto", value); }
        //}


        public override string ToString()
        {
            if (this == null) return null;
            if (this.Descrizione == null) return null;
            if (this.CodDescrizione == null) return Descrizione.ToString();
            if (this.Tag == null) return Descrizione.ToString();

            return string.Format("{0}({1})", Descrizione, Tag);
        }

    }

}

