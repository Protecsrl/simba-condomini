using CAMS.Module.Classi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.DBDocument;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant.Coefficienti;
using CAMS.Module.DBSpazi;
using CAMS.Module.DBTask;
using CAMS.Module.PropertyEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Base.General;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("ASSET")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Asset")]
    [Indices("AbilitazioneEreditata;Abilitato")]
    [Appearance("Asset.inCreazione.noVisibile", TargetItems = "Name;Children;Parent;Apparatis;AssetPadre;AppSchedaMpes;Documentis;ControlliNormativis;RegMisureDettaglios;MasterDettaglios", Criteria = @"Oid == -1", Visibility = ViewItemVisibility.Hide)]
    [RuleCombinationOfPropertiesIsUnique("Unique.Asset.Descrizione", DefaultContexts.Save, "Servizio,CodDescrizione, Descrizione")]
    // [Appearance("Asset.BackColor.Salmon", TargetItems = "*", BackColor = "Salmon", FontColor = "Black", Priority = 1, Criteria = "SumTempoMp = 0")]
    //[Appearance("Asset.BackColor.Salmon", TargetItems = "*", FontStyle = FontStyle.Bold, FontColor = "Salmon", Priority = 1, Criteria = "NumApp = 0")]
    [Appearance("Asset.Abilitato.No", TargetItems = "*;Abilitato", Enabled = false, Criteria = "Abilitato = 'No' Or Servizio.Abilitato = 'No' Or Servizio.Immobile.Abilitato = 'No'")]

    [Appearance("Asset.Abilitato.BackColor", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
        FontColor = "Salmon", Priority = 1, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No'")]

    [Appearance("Asset.Abilitato.BackColorSostituito", TargetItems = "*;Abilitato", FontStyle = FontStyle.Strikeout,
    FontColor = "Brown", Priority = 2, Criteria = "Abilitato = 'No' Or AbilitazioneEreditata == 'No' Or AssetSostituitoDa is not null")]

    #region  REGOLE VISUALIZZAZIONE PER IMèIANTI DI GEIOLOCALIZZAZIONE APPARATO
    [Appearance("Asset.non.GeoLocalizzazione.DetailView.visibile", AppearanceItemType.LayoutItem,
     @"[Servizio.ServizioGeoreferenziato] != 'Si'",
    TargetItems = "Geolocalizzazione",
    Context = "Any", Priority = 1,
    Visibility = ViewItemVisibility.Hide)]

    //Context = "Asset_DetailView_Gestione;Asset_DetailView",    @@@@@@@@@@@   SE è DIVERSO 
    [Appearance("Asset.non.GeoLocalizzazione.visibile", AppearanceItemType.LayoutItem,
 @"[Servizio.ServizioGeoreferenziato] != 'Si'",
TargetItems = "AssetSostegno",
 Priority = 1,
Visibility = ViewItemVisibility.Hide)]


    //Context = "Asset_LookupListView_GestioneRDL",              @@@@@@   SE è UGUALE
    [Appearance("Asset.GeoLocalizzazione.nonvisibile", AppearanceItemType.LayoutItem,
 @"[Servizio.ServizioGeoreferenziato] = 'Si'",
TargetItems = "Locale;AssetLocaliServitis;FluidoPrimario",
Context = "Any", Priority = 1,
Visibility = ViewItemVisibility.Hide)]


    #endregion
    [Appearance("Asset.Locali.nonvisibile", AppearanceItemType.LayoutItem,
@"Iif(Immobile is null,true,not(Immobile.Commesse.MostraPianiLocali))",
TargetItems = "Locale",
Context = "Any", Priority = 1,
Visibility = ViewItemVisibility.Hide)]

    #region filtri
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_Reali", "Abilitato == 1 And AbilitazioneEreditata == 1 And EntitaApparato == 1", "Attivi solo Reali", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_Virtuali", "Abilitato == 1 And AbilitazioneEreditata == 1 And EntitaApparato == 0", "Attivi solo Raggruppamento", Index = 2)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo_mp", "Abilitato == 1 And AbilitazioneEreditata == 1 And EntitaApparato == 2", "Attivi solo Virtuale", Index = 3)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", Index = 4)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 5)]
    #endregion

    [ImageName("LoadPageSetup")]
    [NavigationItem("Patrimonio")]
    public class Asset : XPObject, ITreeNode
    {

        public const string NA = "N/A";
        public const string FormattazioneCodice = "{0:000}";
        private const string DateAndTimeOfDayEditMask = "dd/MM/yyyy H:mm:ss";
        public Asset() : base() { }
        public Asset(Session session) : base(session) { }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {
                this.EntitaAsset = Classi.EntitaAsset.Reale;
                this.Quantita = 1;
                this.DateInService = DateTime.Now;
                this.Abilitato = FlgAbilitato.Si;

                this.AbilitazioneEreditata = FlgAbilitato.Si;

                OidAssetSostituito = 0;
            }
        }
        public Asset(Session session, string name) : base(session) { Descrizione = name; }

        #region Struttura  Intefaccia ad Albero
        [VisibleInDetailView(false)]
        public string Name
        {
            get
            {
                return Descrizione;
            }
            set
            {
                SetPropertyValue("Name", ref fDescrizione, value);
            }
        }
        [Browsable(false)]
        public IBindingList Children
        {
            get
            {
                return Apparatis;
            }
        }
        [Browsable(false)]
        public ITreeNode Parent
        {
            get
            {
                return AssetoPadre;
            }
        }

        [Association("AssetPadreFiglioTree-AssetPadreFiglioTree"),
        DevExpress.Xpo.DisplayName("Apparati Figli")] //Aggregated
        [Appearance("Asset.Apparatis", Criteria = "Apparatis.Count == 0", Visibility = ViewItemVisibility.Hide)]
        [ExplicitLoading()]
        public XPCollection<Asset> Apparatis
        {
            get
            {
                return GetCollection<Asset>("Apparatis");
            }
        }
        #endregion

        #region Metodi ed Eventi

        #region
        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "Abilitato")
                {
                    FlgAbilitato newV = (FlgAbilitato)(newValue);
                    if (newV == FlgAbilitato.Si)
                    {
                        this.DateUnService = DateTime.MinValue;
                    }

                    foreach (AssetSchedaMP ApSK in this.AppSchedaMpes)
                    {
                        ApSK.AbilitazioneEreditata = newV;
                    }
                }

                if (newValue != null && propertyName == "AssetSostituito")
                {
                    //Asset newV = (Asset)(newValue);
                    //if (newV != null) //!IsDeleted
                    //{
                    //    Asset Assetsostituito = Session.GetObjectByKey<Asset>(AssetSostituito.Oid);
                    //    Assetsostituito.Abilitato = FlgAbilitato.No;
                    //    Assetsostituito.DateUnService = DateTime.Now;
                    //    Assetsostituito.AssetSostituitoDa = this;
                    //}


                }


            }

            if (this.Oid == -1)
            {
                if (newValue != null && propertyName == "Servizio")
                {
                    Servizio newV = (Servizio)(newValue);

                    if (this.CodDescrizione == null && newV != null && StdAsset != null)
                    {
                        this.CodDescrizione = String.Format("{0}_{1}_{2}", Servizio.CodDescrizione, StdAsset.CodDescrizione,
                                   String.Format(FormattazioneCodice,
                                   Convert.ToInt32(Session.Evaluate<Asset>(CriteriaOperator.Parse("Count"),
                                   new BinaryOperator(Servizio.Fields.Oid.PropertyName.ToString(), Servizio.Oid))) + 1));
                    }
                }
                if (newValue != null && propertyName == "StdApparato")
                {
                    StdAsset newV = (StdAsset)(newValue);

                    if (this.CodDescrizione == null && newV != null && Servizio != null)
                    {
                        this.CodDescrizione = String.Format("{0}_{1}_{2}", Servizio.CodDescrizione, StdAsset.CodDescrizione,
                                   String.Format(FormattazioneCodice,
                                   Convert.ToInt32(Session.Evaluate<Asset>(CriteriaOperator.Parse("Count"),
                                   new BinaryOperator(Servizio.Fields.Oid.PropertyName.ToString(), Servizio.Oid))) + 1));
                    }

                }
            }

        }


        #endregion


        protected override void OnDeleting()
        {
            base.OnDeleting();
            SetVarSessione.OidServizioDaAggiornare = Servizio.Oid;
        }

        protected override void OnDeleted()
        {
            var db = new DB();
            Session.CommitTransaction();
            db.AggiornaTempi(SetVarSessione.OidServizioDaAggiornare, "IMPIANTO");
            SetVarSessione.OidServizioDaAggiornare = 0;
            base.OnDeleted();
        }
        public Asset CloneFrom(Asset assetSelezionato)
        {
            CodDescrizione = assetSelezionato.CodDescrizione;
            Descrizione = assetSelezionato.Descrizione;
            StdAsset = assetSelezionato.StdAsset;
            Servizio = assetSelezionato.Servizio;
            Locale = assetSelezionato.Locale;
            Strada = assetSelezionato.Strada;
            GeoLocalizzazione = assetSelezionato.GeoLocalizzazione;

            DataSheet = assetSelezionato.DataSheet;
            Quantita = assetSelezionato.Quantita;

            Marca = assetSelezionato.Marca;
            Modello = assetSelezionato.Modello;
            CarattTecniche = assetSelezionato.CarattTecniche;
            Note = assetSelezionato.Note;
            Matricola = assetSelezionato.Matricola;
            EntitaAsset = assetSelezionato.EntitaAsset;
            Tag = assetSelezionato.Tag;
           

            KeyPlans = assetSelezionato.KeyPlans;
            AssetoPadre = assetSelezionato.AssetoPadre;
            AssetSostegno = assetSelezionato.AssetSostegno;
            AssetMP = assetSelezionato.AssetMP;

            Abilitato = assetSelezionato.Abilitato;
            AbilitazioneEreditata = assetSelezionato.AbilitazioneEreditata;
            AssetkTempo = assetSelezionato.AssetkTempo;
            TotaleCoefficienti = assetSelezionato.TotaleCoefficienti;
            DataLettura = assetSelezionato.DataLettura;
            ValoreUltimaLettura = assetSelezionato.ValoreUltimaLettura;
            OreMedieSetEsercizio = assetSelezionato.OreMedieSetEsercizio;
            OidServizio = assetSelezionato.OidServizio;
            OidAssetSostituito = assetSelezionato.OidAssetSostituito;
            Utente = SecuritySystem.CurrentUserName.ToString();
            DataAggiornamento = DateTime.Now;

            OidAssetSostituito = assetSelezionato.Oid;
            return this;


        }

        /// <param name="NuovoAsset"> l'Asset a cui caricare le schede</param>
        /// <param name="OidStdAsset"> lo standard Asset da cui ricavare le schede</param>
        public void InsertSchedeMPsuAsset(ref Asset NuovoAsset, RdL RdL, IList<SchedaMp> OidSchedeMP)
        {
            RdL RdLnul = null;
            this.InsertSchedeMPsuAsset(ref NuovoAsset, RdL, 0, OidSchedeMP);
        }

        public void InsertSchedeMPsuAsset(ref Asset NuovoAsset, int OidStdAsset)
        {
            RdL RdLnul = null;
            // int [] Vet = new int [0];
            IList<SchedaMp> Vet;
            this.InsertSchedeMPsuAsset(ref NuovoAsset, RdLnul, OidStdAsset, Vet = null);
        }

        public void InsertSchedeMPsuAsset(ref Asset NuovoAsset, RdL RdL, int OidStdAsset, IList<SchedaMp> ListSchedeMP)
        {
            IList<SchedaMp> listaSKFiltrata = new XPCollection<SchedaMp>(this.Session).Where(sk => sk.StdAsset.Oid == OidStdAsset).ToList();
            if (OidStdAsset == 0)
                listaSKFiltrata = ListSchedeMP;


            foreach (SchedaMp item in listaSKFiltrata)
            {
                NuovoAsset.AppSchedaMpes.Add(new AssetSchedaMP(Session)
                {
                    SchedaMp = item,
                    CodSchedaMp = item.CodSchedaMp,  //item.CodPmp,
                    CodUni = item.CodUni,
                    Sistema = item.Sistema,
                    SistemaClassi = item.SistemaClassi,
                    SistemaTecnologico = item.SistemaTecnologico,
                    StdAssetClassi = item.StdAssetClassi,
                    Categoria = item.Categoria, /// varia 
                    StdAsset = item.StdAsset,    //, item.Eqstd,  Eqstd = item.StdApparato ,
                    SottoComponente = item.SottoComponente,
                    DescrizioneManutenzione = item.DescrizioneManutenzione,  // item.Manutenzione,
                    FrequenzaOpt = item.FrequenzaOpt,
                    MansioniOpt = item.MansioniOpt,
                    NumMan = item.NumMan, //item.NumMan,
                    TempoOpt = item.TempoOpt,
                    //FrequenzaContatore = item.FrequenzaContatore,
                    Insourcing = item.InSourcing,
                    TipologiaIntervento = item.TipologiaIntervento,
                    StatoComponente = item.StatoComponente,
                    ManualeAsset = item.ManualeAsset,
                    RiferimentiNormativi = item.RiferimentiNormativi,
                    Abilitato = FlgAbilitato.Si,
                    Utente = SecuritySystem.CurrentUser.ToString(),
                    DataAggiornamento = DateTime.Now,
                    CogenzaNormativa = item.CogenzaNormativa,
                    Agg_Rdl = item.Agg_Rdl,
                    Agg_RegRdl = item.Agg_RegRdl
                    // nuovo        
                });
            }
        }

        #endregion

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(250)]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.Asset.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
        // impostare da impianto codice descrittivo
        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(50), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        [RuleRequiredField("RReqField.Asset.CodDescrizione", DefaultContexts.Save, "Cod. Descrizione è un campo obbligatorio")]
        [DbType("varchar(50)")]
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

        [PersistentAlias("Iif(Servizio is not null,Servizio.Immobile,null)")]
        [DevExpress.ExpressApp.DC.XafDisplayName("Immobile")]
        public Immobile Immobile
        {
            get
            {
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                {
                    return (Immobile)tempObject;
                }
                return null;
            }
        }


        private Asset fAssetStd;
        [Size(1000),
        Persistent("ASSETSTD"),
        DevExpress.Xpo.DisplayName("Tipo Apparato")]
        [RuleRequiredField("Asset.StdAsset", DefaultContexts.Save, "Lo Standard Asset è un campo obbligatorio")]
        [Appearance("Asset.StdAsset", Enabled = false, Criteria = "!IsNullOrEmpty(StdAsset)", Context = "DetailView")]
        [DataSourceCriteria("StdAssetClassi.Sistema = '@This.Servizio.Sistema'")]
        [ImmediatePostData]
        [ExplicitLoading()]
        [Delayed(true)]
        public StdAsset StdAsset
        {
            get { return GetDelayedPropertyValue<StdAsset>("StdAsset"); }
            set { SetDelayedPropertyValue<StdAsset>("StdAsset", value); }
        }

        private int fQuantita;
        [Persistent("QUANTITA"),
        DevExpress.Xpo.DisplayName("Quantità"),
        DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "0")]
        [ExplicitLoading()]
        [Delayed(true)]
        public int Quantita
        {
            get { return GetDelayedPropertyValue<int>("Quantita"); }
            set { SetDelayedPropertyValue<int>("Quantita", value); }
            //get
            //{
            //    return fQuantita;
            //}
            //set
            //{
            //    SetPropertyValue<int>("Quantita", ref fQuantita, value);
            //}
        }

        private string fMarca;
        [Persistent("MARCA"), Size(100), DevExpress.Xpo.DisplayName("Marca")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Marca
        {
            get { return GetDelayedPropertyValue<string>("Marca"); }
            set { SetDelayedPropertyValue<string>("Marca", value); }
            //get
            //{
            //    return fMarca;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Marca", ref fMarca, value);
            //}
        }

        private string fModello;
        [Persistent("MODELLO"), Size(100), DevExpress.Xpo.DisplayName("Modello")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Modello
        {
            get { return GetDelayedPropertyValue<string>("Modello"); }
            set { SetDelayedPropertyValue<string>("Modello", value); }
            //get
            //{
            //    return fModello;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Modello", ref fModello, value);
            //}
        }


        private string fCarattTecniche;
        [Persistent("CARATTERISTICHETECNICHE"), Size(250), DevExpress.Xpo.DisplayName("Caratteristiche Tecniche")]
        [DbType("varchar(250)")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string CarattTecniche
        {
            get { return GetDelayedPropertyValue<string>("CarattTecniche"); }
            set { SetDelayedPropertyValue<string>("CarattTecniche", value); }

            //get
            //{
            //    return fCarattTecniche;
            //}
            //set
            //{
            //    SetPropertyValue<string>("CarattTecniche", ref fCarattTecniche, value);
            //}
        }

        private string fNote;
        [Persistent("NOTE"), Size(100), DevExpress.Xpo.DisplayName("Note")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Note
        {
            get { return GetDelayedPropertyValue<string>("Note"); }
            set { SetDelayedPropertyValue<string>("Note", value); }

            //get
            //{
            //    return fNote;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Note", ref fNote, value);
            //}
        }

        #region caratteristiche tecniche
        private string fMatricola;
        [Persistent("MATRICOLA"),
        DevExpress.Xpo.DisplayName("Matricola")]
        [VisibleInListView(false),
        VisibleInLookupListView(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Matricola
        {
            get { return GetDelayedPropertyValue<string>("Matricola"); }
            set { SetDelayedPropertyValue<string>("Matricola", value); }
            //get
            //{
            //    return fMatricola;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Matricola", ref fMatricola, value);
            //}
        }

        private EntitaAsset fEntitaAsset;
        [Persistent("ENTITASSET"),
        DevExpress.Xpo.DisplayName("Classe")]
        [VisibleInListView(false),
        VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public EntitaAsset EntitaAsset
        {
            get
            {
                return fEntitaAsset;
            }
            set
            {
                SetPropertyValue<EntitaAsset>("EntitaAsset", ref fEntitaAsset, value);
            }
        }

        private string fTag;
        [Persistent("TARGHETTA"),
        DevExpress.Xpo.DisplayName(@"Tag P&I")]
        [VisibleInListView(false),
        VisibleInLookupListView(false)]
        [Delayed(true)]
        public string Tag
        {
            get
            {
                return GetDelayedPropertyValue<string>("Tag");
                //return fTag;
            }
            set
            {
                SetDelayedPropertyValue<string>("Tag", value);
                //SetPropertyValue<string>("Tag", ref fTag, value);
            }
        }

     


      



        //private DevExpress.Persistent.BaseImpl.FileData fDataSheet;
        [Persistent("DATASHEET"), DevExpress.Xpo.DisplayName("Data Sheet")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData DataSheet
        {
            get
            {
                return GetDelayedPropertyValue<FileData>("DataSheet");
            }
            set
            {
                SetDelayedPropertyValue<FileData>("DataSheet", value);
            }
        }

        [Persistent("KEYPLAN"), DevExpress.Xpo.DisplayName("Immagine")]
        //[DevExpress.Xpo.Size(SizeAttribute.Unlimited), ValueConverter(typeof(DevExpress.Xpo.Metadata.ImageValueConverter))]
        [DevExpress.Xpo.Size(SizeAttribute.Unlimited)]
        [VisibleInListViewAttribute(true)]
        [ImageEditor(ListViewImageEditorMode = ImageEditorMode.PictureEdit, DetailViewImageEditorMode = ImageEditorMode.PictureEdit,
            ListViewImageEditorCustomHeight = 60, DetailViewImageEditorFixedHeight = 160)]
        [Delayed(true)]
        public byte[] KeyPlans // public Image KeyPlans
        {
            //get            //{            //    return GetDelayedPropertyValue<Image>("KeyPlans");            //}
            //set            //{            //    SetDelayedPropertyValue<Image>("KeyPlans", value);            //}
            get { return GetDelayedPropertyValue<byte[]>("KeyPlans"); }
            set { SetDelayedPropertyValue<byte[]>("KeyPlans", value); }
        }

        #endregion

        #region    ----------   RELAZIONE PADE FIGLIO   ------------------------------------------------------------------------------------
        private Asset fAssetPadre;
        // [Browsable(false)]
        [Persistent("ASSETPADRE"), DevExpress.Xpo.DisplayName("Asset Padre")]
        [Association("AssetPadreFiglioTree-AssetPadreFiglioTree")]
        [DataSourceCriteria("Servizio.Oid = '@This.Servizio.Oid' And Oid != '@This.Oid'")]
        [VisibleInDetailView(true), VisibleInListView(false)]
        //[ExplicitLoading()]
        [Delayed(true)]
        public Asset AssetoPadre
        {
            get
            {
                return GetDelayedPropertyValue<Asset>("AssetPadre");
                //return fApparatoPadre;
            }
            set
            {
                SetDelayedPropertyValue<Asset>("AssetPadre", value);
                //SetPropertyValue<Apparato>("ApparatoPadre", ref fApparatoPadre, value);
            }
        }

        private Asset fApparatoSostegno;
        // [Browsable(false)]
        [Persistent("ASSETSOSTEGNO"), DevExpress.Xpo.DisplayName("Asset Sostegno")]
        [DataSourceCriteria("Servizio.Oid = '@This.Servizio.Oid' And Oid != '@This.Oid' And StdApparato.StdApparatoClassi.Sistema.Descrizione == 'Strutture'")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset AssetSostegno
        {
            get
            {
                if (this.Servizio == null) return null;
                if (this.Servizio.ServizioGeoreferenziato == null) return null;
                if (this.Servizio.ServizioGeoreferenziato != FlgAbilitato.Si) return null;

                return GetDelayedPropertyValue<Asset>("AssetSostegno");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("AssetSostegno", value);
            }
        }


        // [Browsable(false)]
        [Persistent("ASSETMP"), DevExpress.Xpo.DisplayName("Asset di Aggregazione Manutenzione")]
        [DataSourceCriteria("Servizio.Oid = '@This.Servizio.Oid' And Oid != '@This.Oid' And Oid != '@This.Oid'")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset AssetMP
        {
            get
            {
                if (this.Servizio == null) return null;

                return GetDelayedPropertyValue<Asset>("AssetMP");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("AssetMP", value);
            }
        }


        [Persistent("ASSETSOSTITUITODA"), DevExpress.Xpo.DisplayName("Apparato Sostituito Da")]
        [DataSourceCriteria("Servizio.Oid = '@This.Servizio.Oid' And Oid != '@This.Oid' And Oid != '@This.Oid'")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset AssetSostituitoDa
        {
            get
            {
                if (this.Servizio == null) return null;

                return GetDelayedPropertyValue<Asset>("AssetSostituitoDa");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("AssetSostituitoDa", value);
            }
        }
        [Persistent("ASSETSOSTITUITO"), DevExpress.Xpo.DisplayName("Asset Sostituito")]
        //[DataSourceCriteria("Servizio.Oid = '@This.Servizio.Oid' And Oid != '@This.Oid' And Oid != '@This.Oid'")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset AssetSostituito
        {
            get
            {
                if (this.Servizio == null) return null;

                return GetDelayedPropertyValue<Asset>("AssetSostituito");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("AssetSostituito", value);
            }
        }

        #endregion

        #region     ----------   DATE VALIDITA'   ------------------------------------------------------------------------------------
        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [Appearance("Asset.Abilita.DateUnService", Criteria = "DateInService  is null", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public DateTime DateUnService
        {
            get { return GetDelayedPropertyValue<DateTime>("DateUnService"); }
            set { SetDelayedPropertyValue<DateTime>("DateUnService", value); }

            //get            //{
            //    return fDateUnService;            //}
            //set            //{
            //    SetPropertyValue<DateTime>("DateUnService", ref fDateUnService, value);            //}
        }

        private DateTime fDateInService;
        [Persistent("DATAINSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data in Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [RuleRequiredField("RuleReq.Asset.DateInService", DefaultContexts.Save, "Data  in Servizio  è un campo obbligatorio")]
        [Appearance("Asset.DateInService.EvidenzaObligatorio", AppearanceItemType.LayoutItem, "IsNullOrEmpty(DateInService)", FontStyle = FontStyle.Bold, FontColor = "Brown")]
        [Appearance("Asset.Abilita.DateInService", Criteria = "not(DateUnService is null)", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public DateTime DateInService
        {
            get { return GetDelayedPropertyValue<DateTime>("DateInService"); }
            set { SetDelayedPropertyValue<DateTime>("DateInService", value); }
            //get
            //{
            //    return fDateInService;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DateInService", ref fDateInService, value);
            //}
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
        [Appearance("Asset.AbilitazioneEreditata", FontColor = "Black", Enabled = false)]
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

        #region PARAMETRI SCHEDULAZIONE


        private AssetkTempo fAssetkTempo;
        [Persistent("ASSETKTEMPO"),
        DevExpress.Xpo.DisplayName("Coefficiente Tempo")]
        [Appearance("Apparato.ApparatokTempo", Enabled = false)]
        [DevExpress.Persistent.Base.VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public AssetkTempo AssetkTempo
        {
            get { return GetDelayedPropertyValue<AssetkTempo>("AssetkTempo"); }
            set { SetDelayedPropertyValue<AssetkTempo>("AssetkTempo", value); }
           
        }

        private double fTotaleCoefficienti;
        [Persistent("TOTALECOEFFICIENTI")]
        [Appearance("Asset.TotaleCoefficienti", Enabled = false)]
        [DevExpress.Persistent.Base.VisibleInListView(false)]
        [Delayed(true)]
        public double TotaleCoefficienti
        {
            get { return GetDelayedPropertyValue<double>("TotaleCoefficienti"); }
            set { SetDelayedPropertyValue<double>("TotaleCoefficienti", value); }
            //get
            //{
            //    return fTotaleCoefficienti;
            //}
            //set
            //{
            //    SetPropertyValue<double>("TotaleCoefficienti", ref fTotaleCoefficienti, value);
            //}
        }

        //[Persistent("SUMTEMPOSCHEDEMP")]
        //private int fSumTempoMp;

        //[PersistentAlias("fSumTempoMp")]
        [Persistent("SUMTEMPOSCHEDEMP")]
        [DevExpress.Xpo.DisplayName("Somma Tempo SchedeMP [min.]")]
        [Delayed(true)]
        public int SumTempoMp
        {
            get { return GetDelayedPropertyValue<int>("SumTempoMp"); }
            set { SetDelayedPropertyValue<int>("SumTempoMp", value); }
            //get { return fSumTempoMp; }
        }


        //[Persistent("SKSUMTEMPOSCHEDEMP")]
        //private int fSKSumTempoMp;

        //[PersistentAlias("fSKSumTempoMp")]
        //[System.ComponentModel.Browsable(false)]
        //public int SKSumTempoMp
        //{
        //    get { return fSumTempoMp; }
        //}
        [Persistent("SKSUMTEMPOSCHEDEMP")]
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public int SKSumTempoMp
        {
            get { return GetDelayedPropertyValue<int>("SKSumTempoMp"); }
            set { SetDelayedPropertyValue<int>("SKSumTempoMp", value); }
        }

        #endregion


        #region PARAMETRI Attività a ContaORE
        private DateTime fDataLettura;
        [Persistent("DATAULTIMALETTURA"), DevExpress.Xpo.DisplayName("Data Lettura")]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        [Delayed(true)]
        public DateTime DataLettura
        {
            get { return GetDelayedPropertyValue<DateTime>("DataLettura"); }
            set { SetDelayedPropertyValue<DateTime>("DataLettura", value); }
            //get
            //{
            //    return fDataLettura;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataLettura", ref fDataLettura, value);
            //}
        }
        private Double fValoreUltimaLettura;
        [Persistent("ULTIMALETTURA"), DevExpress.Xpo.DisplayName(@"Ultima Lettura(h)")]
        //[RuleRequiredField("RReqFObJ.Asset.RegMisureDettaglio.Valore", DefaultContexts.Save, "Il Valore è un campo obligatorio")]  IsNullOrEmpty(Piani)
        [RuleRequiredField("RReqFObJ.Asset.RegMisureDettaglio.Valore", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "!IsNullOrEmpty([DataLettura])")]
        [Delayed(true)]
        public Double ValoreUltimaLettura
        {
            get { return GetDelayedPropertyValue<Double>("ValoreUltimaLettura"); }
            set { SetDelayedPropertyValue<Double>("ValoreUltimaLettura", value); }
            //get
            //{
            //    return fValoreUltimaLettura;
            //}
            //set
            //{
            //    SetPropertyValue<Double>("ValoreUltimaLettura", ref fValoreUltimaLettura, value);
            //}
        }

        private int fOreMedieSetEsercizio;
        [Persistent("OREMEDSETESERCIZIO"), DevExpress.Xpo.DisplayName("Ore Esercizio Settimanali")]
        [Delayed(true)]
        public int OreMedieSetEsercizio
        {
            get { return GetDelayedPropertyValue<int>("OreMedieSetEsercizio"); }
            set { SetDelayedPropertyValue<int>("OreMedieSetEsercizio", value); }

            //get
            //{
            //    return fOreMedieSetEsercizio;
            //}
            //set
            //{
            //    SetPropertyValue<int>("OreMedieSetEsercizio", ref fOreMedieSetEsercizio, value);
            //}
        }


        private int fPotenzaConsumoW;
        [Persistent("POTENZACONSUMO"), Size(100), DevExpress.Xpo.DisplayName("Potenza Consumo (W)")]
        [DbType("varchar(100)")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public int PotenzaConsumoW
        {
            get { return GetDelayedPropertyValue<int>("PotenzaConsumoW"); }
            set { SetDelayedPropertyValue<int>("PotenzaConsumoW", value); }
        }

        #endregion

        #region SCENARIO E CLUSTEREDIFICI  popolati per riconoscere da chi sono schedulati

        [Persistent("SCENARIO")]
        private Scenario fScenario;

        [PersistentAlias("fScenario")]
        [System.ComponentModel.Browsable(false)]
        [DevExpress.Xpo.DisplayName("Scenario")]
        public Scenario Scenario
        {
            get { return fScenario; }
        }
        ////private Scenario fScenario;
        //[Persistent("SCENARIO"),
        //DevExpress.Xpo.DisplayName("Scenario")]
        //[Appearance("Apparato.Scenario")]//, Enabled = false)]
        //[Browsable(false)]
        //[ExplicitLoading()]
        //[Delayed(true)]
        //public Scenario Scenario
        //{
        //    get
        //    {
        //        return GetDelayedPropertyValue<Scenario>("Scenario");
        //        //return fScenario;
        //    }
        //    set
        //    {
        //        SetDelayedPropertyValue<Scenario>("Scenario", value);
        //        //SetPropertyValue<Scenario>("Scenario", ref fScenario, value);
        //    }
        //}

        [Persistent("CLUSTEREDIFICI")]
        private ClusterEdifici fClusterEdifici;

        [PersistentAlias("fClusterEdifici")]
        [System.ComponentModel.Browsable(false)]
        [DevExpress.Xpo.DisplayName("Cluster Edifici")]
        public ClusterEdifici ClusterEdifici
        {
            get { return fClusterEdifici; }
        }
        //private ClusterEdifici fClusterEdifici;
        //[Persistent("CLUSTEREDIFICI"),
        //DevExpress.Xpo.DisplayName("Cluster Edifici")]
        //[Appearance("Apparato.ClusterEdifici")]//, Enabled = false)]
        //[DevExpress.Persistent.Base.VisibleInListView(false)]
        //[ExplicitLoading()]
        ////[Delayed(true)]
        //public ClusterEdifici ClusterEdifici
        //{
        //    get
        //    {
        //        return fClusterEdifici;
        //    }
        //    set
        //    {
        //        SetPropertyValue<ClusterEdifici>("ClusterEdifici", ref fClusterEdifici, value);
        //    }
        //}

        #endregion

      #region non persistent associato e abilitato
        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public int OidServizio { get; set; }

        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        public int OidAssetSostituito { get; set; }

        #endregion

        #region associazioni CON SCHEDE MP  -  APPARATO SCHEDA MP
        [Association(@"ASSETRefASSSCHEDAMP", typeof(AssetSchedaMP)), DevExpress.Xpo.DisplayName(@"Attività PMP Associate")]
        [Appearance("Asset_AppSchedaMpes.HideLayoutItem", AppearanceItemType.LayoutItem, @"'@This.AssetMP' != null", Visibility = ViewItemVisibility.Hide)]
        //[ImmediatePostData(true)]
        [ExplicitLoading()]
        public XPCollection<AssetSchedaMP> AppSchedaMpes
        {
            get { return GetCollection<AssetSchedaMP>("AppSchedaMpes"); }
        }

        [DevExpress.ExpressApp.DC.XafDisplayName(@"Attività PMP Associate all'Assemblaggio")]
        [Appearance("Asset_AssetMP.AssetSchedaMP.HideLayoutItem", AppearanceItemType.LayoutItem, @"'@This.AssetMP' == null", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AssetSchedaMP> AssetSchedaMP
        {
            get
            {
                if (this.AssetMP == null)
                    return null;
                XPCollection<AssetSchedaMP> skMp = new XPCollection<AssetSchedaMP>(Session,
                    CriteriaOperator.Parse("Asset.Oid = ?", this.AssetMP.Oid));
                return skMp;// GetCollection<ApparatoSchedaMP>("ApparatoSchedaMP");
            }
        }

        //private XPCollection<Apparato> apparatoAssociati;
        [DevExpress.ExpressApp.DC.XafDisplayName(@"Apparati associati all'Assemblaggio")]
        [Appearance("Asset_EntitaAsset.ApparatoAssociati.HideLayoutItem", AppearanceItemType.LayoutItem, @"'@This.EntitaAsset' == 1", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<Asset> AssetAssociati
        {
            get
            {

                if (this.Oid == -1)
                    return null;

                if (this.EntitaAsset == EntitaAsset.Reale)
                    return null;

                //if (this.apparatoAssociati == null)
                //{
                XPCollection<Asset> AssetAssociati = new XPCollection<Asset>(Session,
                CriteriaOperator.Parse("AssetMP.Oid = ?", this.Oid));
                //}
                return AssetAssociati;
            }
        }
        #endregion
        #region associazioni con altre classi

        private Servizio fServizio;
        [Association(@"SERVIZIORefASSET"), Persistent("SERVIZIO"), DevExpress.Xpo.DisplayName("Servizio")]
        [Appearance("Apparato.Servizio", Enabled = false, Criteria = "!IsNullOrEmpty(Servizio)", Context = "DetailView")]
        [ExplicitLoading()]
        public Servizio Servizio
        {
            get { return fServizio; }
            set { SetPropertyValue<Servizio>("Servizio", ref fServizio, value); }
        }


        [Association(@"Documenti_Asset", typeof(Documenti)), DevExpress.Xpo.DisplayName("Documenti")]
        [ExplicitLoading()]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }

        [Association(@"ControlliNormativi_Asset", typeof(ControlliNormativi)), DevExpress.Xpo.DisplayName("Avvisi Periodici")]
        [ExplicitLoading()]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ControlliNormativi> ControlliNormativis
        {
            get
            {
                return GetCollection<ControlliNormativi>("ControlliNormativis");
            }
        }


        [Association(@"RegMisureDettaglio_Asset", typeof(RegMisureDettaglio)),
        DevExpress.Xpo.DisplayName("Dettaglio Misure")]
        [ExplicitLoading()]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegMisureDettaglio> RegMisureDettaglios
        {
            get
            {
                return GetCollection<RegMisureDettaglio>("RegMisureDettaglios");
            }
        }



        //[Association(@"RdL_Apparato", typeof(RdL))]
        //[DevExpress.Xpo.DisplayName("Storico Interventi")]
        //// [Browsable(false)]
        //[ExplicitLoading()]
        //[CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        //public XPCollection<RdL> RdLs
        //{
        //    get
        //    {
        //        return GetCollection<RdL>("RdLs").Where(w=>w.Apparato.Oid == this.Oid);
        //    }

        //}


        private Locali fLocale;
        [Association(@"LOCALI_ASSET"), Persistent("LOCALI"), DevExpress.Xpo.DisplayName("Locali")]
        //[Appearance("Apparato.Locali", Enabled = false, Criteria = "!IsNullOrEmpty(Locale)", Context = "DetailView")]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Iif(!IsNullOrEmpty('@This.Servizio')," +
                               "Iif(!IsNullOrEmpty('@This.Servizio.Immobile'),Piano.Immobile = '@This.Servizio.Immobile',null)" +
                            ",null)"
                            )]
        [ExplicitLoading()]
        [Delayed(true)]
        public Locali Locale
        {
            get { return GetDelayedPropertyValue<Locali>("Locale"); }
            set { SetDelayedPropertyValue<Locali>("Locale", value); }
            //get { return fLocale; }
            //set { SetPropertyValue<Locali>("Locale", ref fLocale, value); }
        }

        [Association(@"AsettCaratteristicheTecniche", typeof(AsettCaratteristicheTecniche)), Aggregated, DevExpress.Xpo.DisplayName(@"Caratteristiche Tecniche")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public XPCollection<AsettCaratteristicheTecniche> AssetCaratteristicheTecniches
        {
            get { return GetCollection<AsettCaratteristicheTecniche>("AsettCaratteristicheTecniches"); }
        }

        //[Association(@"Apparato_CaratteristicheTecnicheTree", typeof(ApparatoCaratteristicheTecniche)), Aggregated, DevExpress.Xpo.DisplayName(@"Caratteristiche Tecniche tree")]
        //[ImmediatePostData(true)]
        //[ExplicitLoading()]
        //public XPCollection<ApparatoCaratteristicheTecniche> Apparato_CaratteristicheTecnicheTrees
        //{
        //    get { return GetCollection<ApparatoCaratteristicheTecniche>("Apparato_CaratteristicheTecnicheTrees"); }
        //}

        //[Association(@"Apparato_ApparatoFoto", typeof(ApparatoFoto)), Aggregated, DevExpress.Xpo.DisplayName(@"Foto")]
        ////[ImmediatePostData(true)]
        //[ExplicitLoading()]
        //public XPCollection<ApparatoFoto> ApparatoFoto
        //{
        //    get { return GetCollection<ApparatoFoto>("ApparatoFoto"); }
        //}

        [Association(@"Asset_AssetImage", typeof(AssetImage)), Aggregated, DevExpress.Xpo.DisplayName(@"Foto ")]
        //[ImmediatePostData(true)]
        [ExplicitLoading()]
        public XPCollection<AssetImage> AssetImages
        {
            get { return GetCollection<AssetImage>("AssetImages"); }
        }
        [Association(@"Asset_AssetLocaliServiti", typeof(AssetocaliServiti)), Aggregated, DevExpress.Xpo.DisplayName(@"Locali Serviti")]
        [ExplicitLoading()]
        public XPCollection<AssetocaliServiti> ApparatoLocaliServitis
        {
            get { return GetCollection<AssetocaliServiti>("ApparatoLocaliServitis"); }
        }

        [Association(@"Asset_AssetOrariEsercizio", typeof(AssetOrariEsercizio)), Aggregated, DevExpress.Xpo.DisplayName(@"Orari Esercizio")]
        [ExplicitLoading()]
        public XPCollection<AssetOrariEsercizio> ApparatoOrariEsercizios
        {
            get { return GetCollection<AssetOrariEsercizio>("AssetOrariEsercizios"); }
        }

        #endregion

        #region geolocalizzazione

        private Strade fStrada;
        [Persistent("STRADA")]
        [Size(500), DevExpress.Xpo.DisplayName("Strada")]
        [DbType("varchar(500)")]
        [VisibleInListView(false)]
        [Delayed(true)]
        public Strade Strada
        {
            get { return GetDelayedPropertyValue<Strade>("Strada"); }
            set { SetDelayedPropertyValue<Strade>("Strada", value); }

            //get
            //{
            //    return fStrada;
            //}
            //set
            //{
            //    SetPropertyValue<Strade>("Strada", ref fStrada, value);
            //}
        }


        private GeoLocalizzazione fGeoLocalizzazione;
        [Persistent("GEOLOCALIZZAZIONE"), DevExpress.ExpressApp.DC.XafDisplayName("GeoLocalizzazione")]
        [VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public GeoLocalizzazione GeoLocalizzazione
        {
            get { return GetDelayedPropertyValue<GeoLocalizzazione>("GeoLocalizzazione"); }
            set { SetDelayedPropertyValue<GeoLocalizzazione>("GeoLocalizzazione", value); }
            //get { return fGeoLocalizzazione; }
            //set { SetPropertyValue<GeoLocalizzazione>("GeoLocalizzazione", ref fGeoLocalizzazione, value); }
        }



        #endregion



        #region REGISTRO MODIFICHE SULLA CLASSE
        //private XPCollection<AuditDataItemPersistent> changeHistory;
        ////[MemberDesignTimeVisibility(false)]
        //public XPCollection<AuditDataItemPersistent> ChangeHistory
        //{
        //    get
        //    {
        //        if (changeHistory == null)
        //        {
        //            changeHistory = AuditedObjectWeakReference.GetAuditTrail(Session, this);
        //        }
        //        return changeHistory;
        //    }
        //}

        #endregion

        #region non persistent associato e abilitato

        [PersistentAlias("Servizio.Sistema"),
        DevExpress.Xpo.DisplayName("Sistema")]
        [MemberDesignTimeVisibility(false)]
        public Sistema Sistema
        {
            get
            {
                var tempObject = EvaluateAlias("Sistema");
                if (tempObject != null)
                {
                    return (Sistema)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        #endregion

        #region utente e data aggiornamento
        private string f_Utente;
        [Persistent("UTENTE"),
        Size(100),
        DevExpress.Xpo.DisplayName("Utente")]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(100)")]
        [ExplicitLoading()]
        [Delayed(true)]
        public string Utente
        {
            get { return GetDelayedPropertyValue<string>("Utente"); }
            set { SetDelayedPropertyValue<string>("Utente", value); }
            //get
            //{
            //    return f_Utente;
            //}
            //set
            //{
            //    SetPropertyValue<string>("Utente", ref f_Utente, value);
            //}
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + DateAndTimeOfDayEditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", DateAndTimeOfDayEditMask)]
#pragma warning disable CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [ToolTip("Data di Aggiornamento della Richiesta di Intervento", null, DevExpress.Persistent.Base.ToolTipIconType.Information)]
#pragma warning restore CS1503 // Argomento 3: non è possibile convertire da 'DevExpress.Utils.ToolTipIconType' a 'DevExpress.Persistent.Base.ToolTipIconType'
        [System.ComponentModel.Browsable(false)]
        [Delayed(true)]
        public DateTime DataAggiornamento
        {
            get { return GetDelayedPropertyValue<DateTime>("DataAggiornamento"); }
            set { SetDelayedPropertyValue<DateTime>("DataAggiornamento", value); }

            //get
            //{
            //    return fDataAggiornamento;
            //}
            //set
            //{
            //    SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            //}
        }

        #endregion

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
