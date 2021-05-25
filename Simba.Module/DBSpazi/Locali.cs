using CAMS.Module.Classi;
using CAMS.Module.DBDocument;
using CAMS.Module.DBPlant;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;

using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using DevExpress.Xpo.Metadata;
using System;
using System.Drawing;

namespace CAMS.Module.DBSpazi
{
    [DefaultClassOptions, Persistent("LOCALI")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Locali")]
    [ImageName("Action_EditModel")]
    [NavigationItem("Gestione Spazi")]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    [Appearance("Locali.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
                FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]
    public class Locali : XPObject
    {
        public Locali() : base() { }
        public Locali(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            this.DateInService = DateTime.Now;
            this.Abilitato = FlgAbilitato.Si;
            this.AbilitazioneEreditata = FlgAbilitato.Si;
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(100), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.Locali.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string Descrizione
        {
            get { return fDescrizione; }
            set { SetPropertyValue<string>("Descrizione", ref fDescrizione, value); }
        }
        //aggiornamento dati

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(7), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        //[Appearance("Locali.CodDescrizione", Enabled = false)]
        [DbType("varchar(100)")]
        public string CodDescrizione
        {
            get { return fCodDescrizione; }
            set { SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value); }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

        [Persistent("KEYPLAN"), DisplayName("Key Plans")]
        //[DevExpress.Xpo.Size(SizeAttribute.Unlimited), ValueConverter(typeof(ImageValueConverter))]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        [VisibleInListViewAttribute(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
            ListViewImageEditorCustomHeight = 80, DetailViewImageEditorFixedHeight = 240)]
        [Delayed(true)]
        public byte[] KeyPlans // Image KeyPlans
        {
            //get { return GetDelayedPropertyValue<Image>("KeyPlans"); }
            //set { SetDelayedPropertyValue<Image>("KeyPlans", value); }
            get { return GetDelayedPropertyValue<byte[]>("KeyPlans"); }
            set { SetDelayedPropertyValue<byte[]>("KeyPlans", value); }
        }


        private string fAreaMacro;
        [Persistent("AREA_MACRO"), Size(100), DevExpress.Xpo.DisplayName("Area Macro")]
        [DbType("varchar(100)")]
        //[RuleRequiredField("RReqField.Locali.AreaMacro", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        public string AreaMacro
        {
            get { return fAreaMacro; }
            set { SetPropertyValue<string>("AreaMacro", ref fAreaMacro, value); }
        }


        private string fAreaOmogenea;
        [Persistent("AREA_OMOGENEA"), Size(100), DevExpress.Xpo.DisplayName("Area Omogenea")]
        [DbType("varchar(100)")]
        public string AreaOmogenea
        {
            get { return fAreaOmogenea; }
            set { SetPropertyValue<string>("AreaOmogenea", ref fAreaOmogenea, value); }
        }

        private string fDwgName;
        [Persistent("DWGNAME"), Size(50), DevExpress.Xpo.DisplayName("Dwg Name")]
        [DbType("varchar(50)")]
        public string DwgName
        {
            get { return fDwgName; }
            set { SetPropertyValue<string>("DwgName", ref fDwgName, value); }
        }

        private string fLayerName;
        [Persistent("LAYERNAME"), Size(50), DevExpress.Xpo.DisplayName("LayerName")]
        [DbType("varchar(50)")]
        public string LayerName
        {
            get { return fLayerName; }
            set { SetPropertyValue<string>("LayerName", ref fLayerName, value); }
        }

        private string fEhandle;
        [Persistent("EHANDLE"), Size(50), DevExpress.Xpo.DisplayName("Ehandle")]
        [DbType("varchar(50)")]
        public string Ehandle
        {
            get { return fEhandle; }
            set { SetPropertyValue<string>("Ehandle", ref fEhandle, value); }
        }

        private double fArea;
        [Persistent("AREA"), DevExpress.Xpo.DisplayName("Area")]
        [ModelDefault("DisplayFormat", "{0:N} mq")]
        [ModelDefault("EditMask", "N")]
        public double Area
        {
            get { return fArea; }
            set { SetPropertyValue<double>("Area", ref fArea, value); }
        }

        private double fAltezza;
        [Persistent("ALTEZZA"), DevExpress.Xpo.DisplayName("Altezza")]
        [ModelDefault("DisplayFormat", "{0:N} m")]
        [ModelDefault("EditMask", "N")]
        public double Altezza
        {
            get { return fAltezza; }
            set { SetPropertyValue<double>("Altezza", ref fAltezza, value); }
        }


        private double fPerimetro;
        [Persistent("PERIMETRO"), DevExpress.Xpo.DisplayName("Perimetro")]
        [ModelDefault("DisplayFormat", "{0:N} m")]
        [ModelDefault("EditMask", "N")]
        public double Perimetro
        {
            get { return fPerimetro; }
            set { SetPropertyValue<double>("Perimetro", ref fPerimetro, value); }
        }

        #region
        [PersistentAlias("Iif(Perimetro is not null,Perimetro,0) * Iif(Altezza is not null,Altezza,0)")]
        [DevExpress.Xpo.DisplayName("Superficie Parete")]
        [ModelDefault("DisplayFormat", "{0:N} mq")]
        [ModelDefault("EditMask", "N")]
        public double SuperficieParete
        {
            get
            {
                object tempObject = EvaluateAlias("SuperficieParete");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("Iif(Area is not null,Area,0) * Iif(Altezza is not null,Altezza,0)")]
        [DevExpress.Xpo.DisplayName("Volume Locale")]
        [ModelDefault("DisplayFormat", "{0:N} mc")]
        [ModelDefault("EditMask", "N")]
        public double Volume
        {
            get
            {
                object tempObject = EvaluateAlias("Volume");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        #endregion
        private LocaliCategoria fLocaliCategoria;
        [Persistent("LOCALICATEGORIA"), DevExpress.Xpo.DisplayName("Locali Categoria")]
        //  [Appearance("Locali.LocaliCategoria", Enabled = false, Criteria = "!IsNullOrEmpty(Piani)", Context = "DetailView")]
        [ExplicitLoading()]
        public LocaliCategoria LocaliCategoria
        {
            get { return fLocaliCategoria; }
            set { SetPropertyValue<LocaliCategoria>("LocaliCategoria", ref fLocaliCategoria, value); }
        }

        private LocaliUso fLocaliUso;
        [Persistent("LOCALIUSO"), DevExpress.Xpo.DisplayName("Locali Uso")]
        //  [Appearance("Locali.LocaliCategoria", Enabled = false, Criteria = "!IsNullOrEmpty(Piani)", Context = "DetailView")]
        [ExplicitLoading()]
        public LocaliUso LocaliUso
        {
            get { return fLocaliUso; }
            set { SetPropertyValue<LocaliUso>("LocaliUso", ref fLocaliUso, value); }
        }


        private Piani fPiano;
        [Association(@"PIANI_LOCALI"), Persistent("PIANI"), DevExpress.Xpo.DisplayName("Piano")]
        [Appearance("Locali.Piani", Enabled = false, Criteria = "!IsNullOrEmpty(Piani)", Context = "DetailView")]
        [ExplicitLoading()]
        //[DataSourceCriteria("Iif(!IsNullOrEmpty('@This.Impianto')," +
        //               "Iif(!IsNullOrEmpty('@This.Impianto.Immobile'),Piano.Immobile = '@This.Impianto.Immobile',null)" +
        //            ",null)"
        //            )]
        public Piani Piano
        {
            get { return fPiano; }
            set { SetPropertyValue<Piani>("Piano", ref fPiano, value); }
        }

        [Association(@"LOCALI_ASSET", typeof(Asset)), DevExpress.Xpo.DisplayName("Asset")]
        [ExplicitLoading()]
        public XPCollection<Asset> Apparaties
        {
            get
            {
                return GetCollection<Asset>("Asseties");
            }
        }

        [Association(@"Documenti_Locale", typeof(Documenti)), DevExpress.Xpo.DisplayName("Documenti")]
        [Appearance("Locale.Documentis.Count.HideLayoutItem", AppearanceItemType.LayoutItem, "Oid = -1", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }


        [Association(@"Locali_LocaliFiniture", typeof(LocaliFiniture)), Aggregated, DisplayName("Finiture Locali")]
        public XPCollection<LocaliFiniture> LocaliFinitures
        {
            get
            {
                return GetCollection<LocaliFiniture>("LocaliFinitures");
            }
        }
        //Aggregated,
        [Association(@"ApparatoLocaliServiti_Locale", typeof(AssetocaliServiti)), DisplayName("Asservito da")]
        public XPCollection<AssetocaliServiti> ApparatoLocaliServitis
        {
            get
            {
                return GetCollection<AssetocaliServiti>("ApparatoLocaliServitis");
            }
        }

        #region     ----------   DATE VALIDITA'   ------------------------------------------------------------------------------------
        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [Appearance("locali.Abilita.DateUnService", Criteria = "DateInService  is null", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public DateTime DateUnService
        {
            get
            {
                return fDateUnService;
            }
            set
            {
                SetPropertyValue<DateTime>("DateUnService", ref fDateUnService, value);
            }
        }

        private DateTime fDateInService;
        [Persistent("DATAINSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data in Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [RuleRequiredField("RuleReq.locali.DateInService", DefaultContexts.Save, "Data  in Servizio  è un campo obbligatorio")]
        [Appearance("locali.DateInService.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(DateInService)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("locali.Abilita.DateInService", Criteria = "not(DateUnService is null)", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public DateTime DateInService
        {
            get
            {
                return fDateInService;
            }
            set
            {
                SetPropertyValue<DateTime>("DateInService", ref fDateInService, value);
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



        private FlgAbilitato fAbilitazioneEreditata;
        [Persistent("ABILITAZIONETRASMESSA"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo da Gerarchia")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [Appearance("locali.AbilitazioneEreditata", FontColor = "Black", Enabled = false)]
        public FlgAbilitato AbilitazioneEreditata
        {
            get
            {
                return fAbilitazioneEreditata;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("AbilitazioneEreditata", ref fAbilitazioneEreditata, value);
            }
        }

        #endregion



        protected override void OnSaving()
        {
            base.OnSaving();
            string NuovoCodice = this.Oid.ToString();
            if (Session.IsNewObject(this))
            {
                if (CodDescrizione == null)
                {
                    NuovoCodice = (Convert.ToInt32(Session.Evaluate<Locali>(DevExpress.Data.Filtering.CriteriaOperator.Parse("Max(Oid)"), null)) + 1).ToString();
                    string Codice = string.Format("RM{0}", NuovoCodice);
                    this.CodDescrizione = Codice;
                }
            }

        }

    }
}

