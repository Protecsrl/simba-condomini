using CAMS.Module.Classi;
using CAMS.Module.Costi;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBAngrafica;
using CAMS.Module.DBControlliNormativi;
using CAMS.Module.DBDocument;
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBSpazi;
using CAMS.Module.PropertyEditors;
using DevExpress.Data.Filtering;
using DevExpress.ExpressApp;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Drawing;
using System.Linq;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("IMMOBILE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Immobile")]
    [Indices("AbilitazioneEreditata;Abilitato")]
    [ImageName("BO_Organization")]
    //[DeferredDeletion(true)]
    //[Appearance("Immobile.Indirizzo.Geo.nonPresente", AppearanceItemType = "ViewItem",
    //        TargetItems = "Indirizzo", Criteria = "IndirizzoGeoValorizzato = 0",
    //            Context = "Edificio_DetailView", BackColor = "Yellow")]
    [RuleCombinationOfPropertiesIsUnique("Unique.Immobile.Descrizione", DefaultContexts.Save, "Contratti,CodDescrizione, Descrizione")]
    [RuleCriteria("RuleWarning.Ed.Indirizzo.Geo.nonPre", DefaultContexts.Save, @"IndirizzoGeoValorizzato > 0",
      CustomMessageTemplate =
      "Attenzione:La L'indirizzo Selezionato non ha riferimenti di Georeferenziazione, \n\r inserirli prima di continuare!{IndirizzoGeoValorizzato}",
       SkipNullOrEmptyValues = true, ResultType = ValidationResultType.Warning)]

    //[Appearance("Impianto.BackColor.Salmon", TargetItems = "*", BackColor = "Salmon", FontColor = "Black", Priority = 1, Criteria = "SumTempoMp = 0")]
    [Appearance("Immobile.BackColor.Salmon", TargetItems = "*", FontStyle = FontStyle.Bold, FontColor = "Blue", Priority = 1, Criteria = "NumApp = 0")]

    // [Appearance("Immobile.Abilitato.No", TargetItems = "*;Abilitato;DateUnService;AbilitatoGlobale", Enabled = false, Criteria = "Abilitato = 'No'")]
    [Appearance("Immobile.Abilitato.BackColor", TargetItems = "*;Abilitato;DateUnService;AbilitatoGlobale",
                 FontStyle = FontStyle.Strikeout, FontColor = "Salmon", Priority = 1, Criteria = "Abilitato == 'No' Or AbilitazioneEreditata == 'No'")]

    // [RuleRequiredField("immobile.Abilita.DateUnService.Obblig", DefaultContexts.Save, TargetCriteria = "Abilitato = 'No'")]

    #region Abilitazione

    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloAttivo", "Abilitato == 1 And AbilitazioneEreditata == 1", "Attivi", true, Index = 0)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("SoloNonAttivo", "Abilitato == 0 Or AbilitazioneEreditata == 0", "non Attivi", Index = 1)]
    [DevExpress.ExpressApp.SystemModule.ListViewFilter("Tutto", "", "Tutto", Index = 2)]

    #endregion


    [NavigationItem("Patrimonio")]
    public class Immobile : XPObject
    {
        private const string NA = "N/A";
        private const string FormattazioneCodice = "{0:000}";
        public Immobile(Session session) : base(session) { }
        #region Metodi ed eventi
        public override void AfterConstruction()
        {
            base.AfterConstruction();
            if (this.Oid == -1)
            {

                this.Abilitato = FlgAbilitato.Si;
                this.AbilitazioneEreditata = FlgAbilitato.Si;
            }
        }

        protected override void OnSaving()
        {
            if (!IsDeleted)
            {
                if (CodDescrizione == NA || String.IsNullOrEmpty(CodDescrizione))
                {
                    CodDescrizione = String.Format(FormattazioneCodice, Convert.ToInt32(Session.Evaluate<Immobile>(CriteriaOperator.Parse("Count"), null)) + 1);
                }

                if (NumeroCopie > 0)
                {
                    CopyFrom(this, NumeroCopie);
                }
            }
        }

        //protected override void OnSaved()
        //{
        //    base.OnSaved();
        //    //var db = new DB();
        //    //db.AggiornaTempi(Oid, "IMMOBILE");

        //    //using (var db = new DB())
        //    //{
        //    //    db.AggiornaTempi(Oid, "IMMOBILE");
        //    //}

        //}



        /// Clona l'immobile nVolte
        /// <param name="edificioSelezionato">Immobile selezionato</param> <param name="nCopie">Numero di volte di quanto copiare l'immobile</param>
        public void CopyFrom(Immobile edificioSelezionato, uint nCopie)
        {
            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

            var Conta = Convert.ToInt32(Session.Evaluate<Immobile>(CriteriaOperator.Parse("Count"), null)) + 1;

            for (var i = 0u; i < nCopie; i++)
            {
                var NuovoEdificio = xpObjectSpace.CreateObject<Immobile>();
                NuovoEdificio.CodDescrizione = String.Format(FormattazioneCodice, Conta + i + 1);
                NuovoEdificio.Descrizione = String.Concat(edificioSelezionato.Descrizione, String.Format(" - Copia {0}", i + 1));
                NuovoEdificio.Indirizzo = edificioSelezionato.Indirizzo;
                NuovoEdificio.DataAggiornamento = edificioSelezionato.DataAggiornamento;
                NuovoEdificio.Utente = edificioSelezionato.Utente;

                CreaImpiantiCollegati(NuovoEdificio);
            }
        }

        /// Clona un immobile dal un'altro immobile
        /// <param name="edificioSelezionato"></param>
        public Immobile CloneFrom(Immobile edificioSelezionato)
        {
            Descrizione = edificioSelezionato.Descrizione;
            Indirizzo = edificioSelezionato.Indirizzo;
            OidEdificioSelezionato = edificioSelezionato.Oid;

            CreaImpiantiCollegati(this);

            return this;
        }

        /// Crea tutti gli impianti collegati all'immobile selezionato
        /// <param name="NuovoEdificio">Immobile dove associare gli impianti</param> <param name="edificioSelezionato">Immobile dove ricavare la lista di impianti</param>
        public void CreaImpiantiCollegati(Immobile NuovoEdificio)
        {
            var xpObjectSpace = DevExpress.ExpressApp.Xpo.XPObjectSpace.FindObjectSpaceByObject(this);

            var lstImpianti = xpObjectSpace.GetObjects<Servizio>().Where(imp => imp.Immobile.Oid == OidEdificioSelezionato).ToList();

            foreach (Servizio servizio in lstImpianti)
            {
                var NuovoImpianto = xpObjectSpace.CreateObject<Servizio>();
                NuovoImpianto = NuovoImpianto.CloneFrom(servizio);
                NuovoImpianto.Immobile = NuovoEdificio;
            }
        }
        #endregion

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(250), DevExpress.Xpo.DisplayName("Descrizione")]
        [DbType("varchar(250)")]
        [RuleRequiredField("RReqField.Immobile.Descrizione", DefaultContexts.Save, @"La Descrizione è un campo obbligatorio")]
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

        private string fCodDescrizione;
        [Persistent("COD_DESCRIZIONE"), Size(50), DevExpress.Xpo.DisplayName("Cod Descrizione")]
        [RuleRequiredField("RReqField.Immobile.CodDescrizione", DefaultContexts.Save, @"Codice Descrizione è un campo obbligatorio")]
        [DbType("varchar(50)")]
        public string CodDescrizione
        {
            get
            {
                return fCodDescrizione;
            }
            set
            {
                SetPropertyValue<string>("CodDescrizione", ref fCodDescrizione, value);
            }
        }

        private string fCodOut;
        [Persistent("COD_OUT"), Size(50), DevExpress.Xpo.DisplayName("Cod Out")]
        [DbType("varchar(50)")]
        public string CodOut
        {
            get { return fCodOut; }
            set { SetPropertyValue<string>("CodOut", ref fCodOut, value); }
        }

        private Indirizzo fIndirizzo;
        [Association(@"Indirizzo_Edificio"), Persistent("INDIRIZZO"),
        DevExpress.Xpo.DisplayName("Indirizzo")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Indirizzo Indirizzo
        {
            get
            {
                return GetDelayedPropertyValue<Indirizzo>("Indirizzo");
            }
            set
            {
                SetDelayedPropertyValue<Indirizzo>("Indirizzo", value);
            }

            //get
            //{
            //    return fIndirizzo;
            //}
            //set
            //{
            //    SetPropertyValue<Indirizzo>("Indirizzo", ref fIndirizzo, value);
            //}
        }

        private Contratti fContratti;
        [Persistent("CONTRATTI"), DevExpress.ExpressApp.DC.XafDisplayName("Contratti")]
        [RuleRequiredField("Immobile.Contratti", DefaultContexts.Save, "Il contratto è un campo obbligatorio")]
        [Association(@"Contratti_Edificio")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Contratti Contratti
        {
            get
            {
                return GetDelayedPropertyValue<Contratti>("Contratti");
            }
            set
            {
                SetDelayedPropertyValue<Contratti>("Contratti", value);
            }
        }

        private CentroOperativo fCentroOperativoBase;
        [Persistent("CENTROOPERATIVO"), DevExpress.ExpressApp.DC.XafDisplayName("Centro Operativo Base")]
        [DataSourceCriteria(@"AreaDiPolo.Oid == '@This.Contratti.AreaDiPolo.Oid'")]
        [Association(@"CentroOperativo_Edificio")]
        [Appearance("Immobile.CentroOperativoBase.enable", AppearanceItemType.LayoutItem, "IsNullOrEmpty(Contratti)", Enabled = false)]
        [Delayed(true)]
        [ExplicitLoading()]
        public CentroOperativo CentroOperativoBase
        {
            get
            {
                return GetDelayedPropertyValue<CentroOperativo>("CentroOperativoBase");
            }
            set
            {
                SetDelayedPropertyValue<CentroOperativo>("CentroOperativoBase", value);
            }

            //get { return fCentroOperativoBase; }
            //set
            //{
            //    SetPropertyValue<CentroOperativo>("CentroOperativoBase", ref fCentroOperativoBase, value);
            //}
        }

        [VisibleInDetailView(true), VisibleInListView(true), VisibleInLookupListView(true), Size(150)]
        [PersistentAlias("Iif(Contratti is not null, Iif(Contratti.ClienteReferenteContratto is not null, Contratti.ClienteReferenteContratto.Denominazione,null),null)"), DevExpress.ExpressApp.DC.XafDisplayName("Amministrazione Cliente")]
        [Appearance("RdL.Contratti_ClienteReferenteContratto.BlackBlue", Criteria = "not (Contratti_ClienteReferenteContratto  is null)", FontColor = "Blue")]
        public string Contratti_ClienteReferenteContratto
        {
            get
            {
                if (this.Contratti == null) return null;
                if (this.Contratti.ClienteReferenteContratto == null) return null;
                return string.Format("{0}", this.Contratti.ClienteReferenteContratto.Denominazione);
            }
        }

        private string fNote;
        [Persistent("NOTE"), Size(4000), DevExpress.Xpo.DisplayName("Note (Rif.Chiave)")]
        [DbType("varchar(4000)")]
        public string Note
        {
            get
            {
                return fNote;
            }
            set
            {
                SetPropertyValue<string>("Note", ref fNote, value);
            }
        }

        private string fRifReperibile;
        [Persistent("RIFREPERIBILE"), Size(1000), DevExpress.Xpo.DisplayName("Riferimenti Reperibilità")]
        [DbType("varchar(1000)")]
        public string RifReperibile
        {
            get
            {
                return fRifReperibile;
            }
            set
            {
                SetPropertyValue<string>("RifReperibile", ref fRifReperibile, value);
            }
        }

        [Persistent("COUNTIMPIANTI")]
        private int fNumImp;
        [PersistentAlias("fNumImp")]
        [DevExpress.Xpo.DisplayName(@"N° Impianti")]
        public int NumImp
        {
            get
            {
                return fNumImp;
            }
        }

        [Persistent("COUNTAPPARATI")]
        private int fNumApp;

        [PersistentAlias("fNumApp")]
        [DevExpress.Xpo.DisplayName(@"N° Apparati")]
        public int NumApp
        {
            get
            {
                return fNumApp;
            }
        }

        //[Persistent("SUMTEMPOSCHEDEMP")] //this.<EdificioMansioneCarico> ;  //Impianti.Sum(APPARATOes.Sum(AppSchedaMpes.Sum(
        //private int fSumTempoMp;

        ////[PersistentAlias("EdificioMansioneCaricos.Sum(Carico)")]
        //[PersistentAlias("fSumTempoMp")]
        //[DevExpress.Xpo.DisplayName(@"Somma Tempo Attività [min.]")]
        //public int SumTempoMp
        //{
        //    get
        //    {
        //        return fNumApp;
        //    }
        //    //object tempObject = EvaluateAlias("SumTempoMp");
        //    //if (tempObject != null)
        //    //{
        //    //    return (int)tempObject;
        //    //}
        //    //else
        //    //{ return 0; }

        //}


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
        public byte[] KeyPlans   //public Image KeyPlans
        {
            //get            //{            //    return GetDelayedPropertyValue<Image>("KeyPlans");            //}
            //set            //{            //    SetDelayedPropertyValue<Image>("KeyPlans", value);            //}
            get { return GetDelayedPropertyValue<byte[]>("KeyPlans"); }
            set { SetDelayedPropertyValue<byte[]>("KeyPlans", value); }
        }

        [Persistent("SISTEMATECNOLOGICO"), DevExpress.Xpo.DisplayName("Sistema Tecnologico")]
        [RuleRequiredField("RReqField.immobile.SistemaTecnologico", DefaultContexts.Save, "Sistema Tecnologico è un campo obbligatorio")]
        [Appearance("immobile.Abilita.SistemaTecnologico", Criteria = "Impianti.Count() > 0", Enabled = false)]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        [Delayed(true)]
        public SistemaTecnologico SistemaTecnologico
        {
            get { return GetDelayedPropertyValue<SistemaTecnologico>("SistemaTecnologico"); }
            set { SetDelayedPropertyValue<SistemaTecnologico>("SistemaTecnologico", value); }
        }

        #region CLUSTEREDIFICI


        [Association(@"ClusterEdifici_Edificio"), Persistent("CLUSTEREDIFICI"), DevExpress.Xpo.DisplayName("Cluster Edifici")]
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading(), Delayed(true)]
        public ClusterEdifici ClusterEdifici
        {
            get
            {
                return GetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici");
            }
            set
            {
                SetDelayedPropertyValue<ClusterEdifici>("ClusterEdifici", value);
            }
        }

        #endregion
        #region abilitazione
        private FlgAbilitato fAbilitato;
        [Persistent("ABILITATO"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
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
        private DateTime fDateUnService;
        [Persistent("DATAUNSERVICE"), DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy"), DevExpress.Xpo.DisplayName("Data Fuori Servizio")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [EditorAlias(CAMSEditorAliases.CustomDateTimeEditor)]
        [Appearance("immobile.Abilita.DateUnService", Criteria = "Abilitato = 'Si'", Enabled = false)]
        //[RuleRequiredField("immobile.Abilita.DateUnService.Obblig", DefaultContexts.Save,  TargetCriteria = "Abilitato = 'No'")]
        [RuleRequiredField("immobile.Abilita.Obbligata", DefaultContexts.Save, SkipNullOrEmptyValues = false, TargetCriteria = "[Abilitato] == 'No'")]
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


        private FlgAbilitato fAbilitazioneEreditata;
        [Persistent("ABILITAZIONETRASMESSA"), DevExpress.ExpressApp.DC.XafDisplayName("Attivo da Gerarchia")]
        [VisibleInListView(false), VisibleInLookupListView(false), VisibleInDetailView(true)]
        [Appearance("immobile.AbilitazioneEreditata", FontColor = "Black", Enabled = false)]
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

        #region associazioni con altre classi
        [Association(@"RegistroLavori_Edificio", typeof(RegistroLavori)), DevExpress.Xpo.DisplayName("Registro Lavori")]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<RegistroLavori> RegistroLavoris
        {
            get
            {
                return GetCollection<RegistroLavori>("RegistroLavoris");
            }
        }

        [Association(@"DestinatariControlliNormativi_Edificio", typeof(DestinatariControlliNormativi)), DevExpress.Xpo.DisplayName("Destinatari Avvisi")]
        [Appearance("Immobile.DestinatariControlliNormativi.Hide", Criteria = "DestinatariControlliNormativis.Count() = 0", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<DestinatariControlliNormativi> DestinatariControlliNormativis
        {
            get
            {
                return GetCollection<DestinatariControlliNormativi>("DestinatariControlliNormativis");
            }
        }

        [Association(@"Documenti_Edificio", typeof(Documenti)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Documenti")]
        //[Association(@"Documenti_RdL"     , typeof(Documenti)), DevExpress.Xpo.Aggregated, System.ComponentModel.DisplayName("Documenti")]
        //[ExplicitLoading()]
        public XPCollection<Documenti> Documentis
        {
            get
            {
                return GetCollection<Documenti>("Documentis");
            }
        }

        [Association(@"Planimetrie_Immobile", typeof(Planimetrie)), Aggregated, DevExpress.ExpressApp.DC.XafDisplayName("Planimetrie")]
        //[ExplicitLoading()]
        public XPCollection<Planimetrie> Planimetries
        {
            get
            {
                return GetCollection<Planimetrie>("Planimetries");
            }
        }


        [Association(@"Edificio_MansioneCarico", typeof(EdificioMansioneCarico)), DevExpress.ExpressApp.DC.XafDisplayName("Carico per Mansione")]
        [Appearance("Immobile.EdificioMansioneCaricos.Hide", Criteria = "EdificioMansioneCaricos.Count() = 0", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<EdificioMansioneCarico> EdificioMansioneCaricos
        {
            get
            {
                return GetCollection<EdificioMansioneCarico>("EdificioMansioneCaricos");
            }
        }

        [Association(@"ControlliNormativi_Edificio", typeof(ControlliNormativi)), DevExpress.Xpo.DisplayName("Avvisi Periodici")]
        [Appearance("Immobile.ControlliNormativi.Hide", Criteria = "ControlliNormativis.Count() = 0", Visibility = ViewItemVisibility.Hide)]
        [CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<ControlliNormativi> ControlliNormativis
        {
            get
            {
                return GetCollection<ControlliNormativi>("ControlliNormativis");
            }
        }

        [Association(@"Immobile_Impianti", typeof(Servizio)), Aggregated, DevExpress.Xpo.DisplayName("Impianti")]
        [ExplicitLoading()]
        public XPCollection<Servizio> Impianti
        {
            get
            {
                return GetCollection<Servizio>("Impianti");
            }
        }

        [Association(@"Edificio_TRisorseConduttori", typeof(Conduttori)), Aggregated, DevExpress.Xpo.DisplayName("Conduttori")]
        public XPCollection<Conduttori> Conduttoris
        {
            get
            {
                return GetCollection<Conduttori>("Conduttoris");
            }
        }


        #endregion
        #region non persistent associato e abilitato
        //[NonPersistent, DevExpress.Xpo.DisplayName("Associato")]
        //[VisibleInListView(false)]
        //public bool Associato
        //{
        //    get
        //    {
        //        object tempClImpianto = this.ClusterEdifici;
        //        if (tempClImpianto != null)
        //        {
        //            return true;
        //        }
        //        return false;

        //    }
        //}

        [PersistentAlias("ClusterEdifici")]
        [DevExpress.Xpo.DisplayName("Associato")]
        public bool Associato
        {
            get
            {
                object tempObject = EvaluateAlias("Associato");
                if (tempObject != null)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }


        #endregion
        #region campi per PopUp

        [NonPersistent]
        public uint NumeroCopie { get; set; }

        [System.ComponentModel.DefaultValue(-1)]
        [NonPersistent]
        [System.ComponentModel.Browsable(false)]
        private int OidEdificioSelezionato { get; set; }


        [PersistentAlias("Indirizzo.Latitude * Indirizzo.Longitude")]
        [System.ComponentModel.DefaultValue(false)]
        [System.ComponentModel.Browsable(false), MemberDesignTimeVisibility(false)]
        public int IndirizzoGeoValorizzato
        {
            get
            {
                var tempObject = EvaluateAlias("IndirizzoGeoValorizzato");
                if (tempObject != null)
                {
                    double dOut = 0;
                    double.TryParse(tempObject.ToString(), out dOut);
                    return dOut > 0 ? 1 : 0;
                }
                else
                {
                    return 0;
                }
            }
        }


        #endregion

        #region  metodi
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

                    foreach (Servizio im in this.Impianti)
                    {
                        im.AbilitazioneEreditata = newV;

                        foreach (Asset Ap in im.APPARATOes)
                        {
                            Ap.AbilitazioneEreditata = newV;

                            foreach (AssetSchedaMP ApSK in Ap.AppSchedaMpes)
                            {
                                ApSK.AbilitazioneEreditata = newV;
                            }
                        }
                    }

                    foreach (Piani pi in this.Pianies)
                    {
                        pi.AbilitazioneEreditata = newV;
                        foreach (Locali lo in pi.Localies)
                        {
                            lo.AbilitazioneEreditata = newV;
                        }
                    }
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
        public string Utente
        {
            get
            {
                return f_Utente;
            }
            set
            {
                SetPropertyValue<string>("Utente", ref f_Utente, value);
            }
        }

        private DateTime? fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Aggiornamento")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "dd/MM/yyyy H:mm:ss tt")]
        [System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        public DateTime? DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime?>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        #endregion

        [Association(@"EDIFICIO_PIANI", typeof(Piani)), Aggregated, DevExpress.Xpo.DisplayName("Piani")]
        [ExplicitLoading()]
        public XPCollection<Piani> Pianies
        {
            get
            {
                return GetCollection<Piani>("Pianies");
            }
        }


        //[Persistent("AREA")]
        //private double fArea;

        //[PersistentAlias("fArea")]
        //[DevExpress.Xpo.DisplayName(@"Superficie Netta")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:N}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //public double Area
        //{
        //    get { return fArea; }
        //}          

        //-------------------------------------  Misure dai LOCALI NETTE
        [PersistentAlias("Pianies.Sum(Localies.Sum(Iif(Area is not null,Area,0)))")]
        [DevExpress.Xpo.DisplayName("Area Netta")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double Area
        {
            get
            {
                object tempObject = EvaluateAlias("Area");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("Pianies.Sum(Localies.Sum(Iif(Area is not null,Area,0) * Iif(Altezza is not null,Altezza,0)))")]
        [DevExpress.Xpo.DisplayName("Volume Netto Locali")]
        [ModelDefault("DisplayFormat", "{0:N}")]
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
        [PersistentAlias("Pianies.Sum(Localies.Sum(Iif(Perimetro is not null,Perimetro,0) * Iif(Altezza is not null,Altezza,0)))")]
        [DevExpress.Xpo.DisplayName("Superficie Pareti Locali")]
        [ModelDefault("DisplayFormat", "{0:N}")]
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

        //-----------------++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++        /// <summary>

        ///                      misure su PIANO --------         +++++++++++++++++++++++++++++++++++++++++

        [PersistentAlias("Pianies.Sum(Iif(AreaLorda is not null,AreaLorda,0))")]
        [DevExpress.Xpo.DisplayName("Superficie Lorda")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double SuperficieLorda
        {
            get
            {
                object tempObject = EvaluateAlias("SuperficieLorda");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("Pianies.Sum(Iif(AreaLorda is not null,AreaLorda,0) * Iif(AltezzaLorda is not null,AltezzaLorda,0))")]
        [DevExpress.Xpo.DisplayName("Volume Lordo")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double VolumeLordo
        {
            get
            {
                object tempObject = EvaluateAlias("VolumeLordo");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

        [PersistentAlias("0+Pianies.Sum(Iif(PerimetroEsterno is not null,PerimetroEsterno,0) * Iif(AltezzaLorda is not null,AltezzaLorda,0))")]
        [DevExpress.Xpo.DisplayName("Superficie Parete Esterna")]
        [ModelDefault("DisplayFormat", "{0:N}")]
        [ModelDefault("EditMask", "N")]
        public double SuperficiePareteEsterna
        {
            get
            {
                object tempObject = EvaluateAlias("SuperficiePareteEsterna");
                if (tempObject != null)
                {
                    return (double)tempObject;
                }
                else { return 0; }
            }
        }

    }
}









