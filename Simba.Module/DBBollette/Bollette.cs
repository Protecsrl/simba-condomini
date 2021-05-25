
using System;

using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Persistent.Validation;
using CAMS.Module.PropertyEditors;
using CAMS.Module.DBMisure;
using CAMS.Module.Classi;
using DevExpress.ExpressApp.Model;
using CAMS.Module.DBPlant;



namespace CAMS.Module.DBBollette
{
    [DefaultClassOptions, Persistent("BOLLETTE")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Bollette")]
    [ImageName("Action_Inline_Edit")]
    //[Appearance("RegMisure.Disabilita.DettaglioMisureMagZero", TargetItems = "Immobile;Impianto;Master;DataInserimento", Criteria = "RegMisureDettaglios.Count() > 0", Enabled = false)]
    [NavigationItem(true)]
    public class Bollette : XPObject
    {
        public Bollette() : base() { }

        public Bollette(Session session) : base(session) { }

        public override void AfterConstruction() { base.AfterConstruction(); }

        private RegistroBollette fRegistroBollette;
        [Association(@"RegBollette_Bellette"), Persistent("BOLLETTEREG")]
        [DisplayName("Registro Bollette")]        //[System.ComponentModel.Browsable(false)]
        [ExplicitLoading()]
        public RegistroBollette RegistroBollette
        {
            get
            {
                return fRegistroBollette;
            }
            set
            {
                SetPropertyValue<RegistroBollette>("RegistroBollette", ref fRegistroBollette, value);
            }
        }

 
        private string fNrBolletta;
        [Size(50), Persistent("NRBOLLETTA"), DisplayName("NrBolletta")]
        [DbType("varchar(50)")]
        public string NrBolletta
        {
            get
            {
                return fNrBolletta;
            }
            set
            {
                SetPropertyValue<string>("NrBolletta", ref fNrBolletta, value);
            }
        }

        private string fContratto;
        [Size(50), Persistent("CONTRATTO"), DisplayName("Contratto")]
        [DbType("varchar(50)")]
        public string Contratto
        {
            get
            {
                return fContratto;
            }
            set
            {
                SetPropertyValue<string>("Contratto", ref fContratto, value);
            }
        }

        private string fTensioneN;
        [Size(50), Persistent("TENSIONE"), DisplayName("Tensione")]
        [DbType("varchar(50)")]
        public string TensioneN
        {
            get
            {
                return fTensioneN;
            }
            set
            {
                SetPropertyValue<string>("TensioneN", ref fTensioneN, value);
            }
        }


        private double fPotenzaContratto;
        [Persistent("POTENZACONTRATTO"), DisplayName("Potenza Contratto")]
        [ModelDefault("DisplayFormat", "{0:N} kW")]
        [ModelDefault("EditMask", "N")]
        [ImmediatePostData(true)]
        public double PotenzaContratto
        {
            get
            {
                return fPotenzaContratto;
            }
            set
            {
                SetPropertyValue<double>("PotenzaContratto", ref fPotenzaContratto, value);

            }
        }



        private string fPOD;
        [Size(100), Persistent("POD"), DisplayName("POD")]
        [DbType("varchar(100)")]
        public string POD
        {
            get
            {
                return fPOD;
            }
            set
            {
                SetPropertyValue<string>("POD", ref fPOD, value);
            }
        }

        private string fDescrizionePOD;
        [Size(1000), Persistent("PODDESCRIZIONE"), DisplayName("Descrizione POD")]
        [DbType("varchar(1000)")]
        public string DescrizionePOD
        {
            get
            {
                return fDescrizionePOD;
            }
            set
            {
                SetPropertyValue<string>("DescrizionePOD", ref fDescrizionePOD, value);
            }
        }

        private DateTime fDataInserimento;
        [Persistent("DATAINSERIMENTO"), DisplayName("Data Inserimento")]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        public DateTime DataInserimento
        {
            get
            {
                return fDataInserimento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataInserimento", ref fDataInserimento, value);
            }
        }

        private DateTime fDataBolletta;
        [Persistent("DATABOLLETTA"), DisplayName("Data Bolletta")]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        public DateTime DataBolletta
        {
            get
            {
                return fDataBolletta;
            }
            set
            {
                SetPropertyValue<DateTime>("DataBolletta", ref fDataBolletta, value);
            }
        }

        private DateTime fDataScadenza;
        [Persistent("DATASCADENZA"), DisplayName("Data Scadenza")]
        //[Appearance("RegMisure.Abilita.DataInserimento", Criteria = "RegMisureDettaglios.Count() = 0", Enabled = false)]
        public DateTime DataScadenza
        {
            get
            {
                return fDataScadenza;
            }
            set
            {
                SetPropertyValue<DateTime>("DataScadenza", ref fDataScadenza, value);
            }
        }


        private string fPeriodo;
        [Size(200), Persistent("PERIODO"), DisplayName("Periodo")]
        [DbType("varchar(200)")]
        public string Periodo
        {
            get
            {
                return fPeriodo;
            }
            set
            {
                SetPropertyValue<string>("Periodo", ref fPeriodo, value);
            }
        }

        private TipologiaFornituraBolletta fTipologiaFornituraBolletta;
        [Size(50), Persistent("TIPOFORNITURA"), DisplayName("Tipo Fornitura")]
        public TipologiaFornituraBolletta TipologiaFornituraBolletta
        {
            get
            {
                return fTipologiaFornituraBolletta;
            }
            set
            {
                SetPropertyValue<TipologiaFornituraBolletta>("TipologiaFornituraBolletta", ref fTipologiaFornituraBolletta, value);
            }
        }

        private TipoMercato fTipoMercato;
        [Persistent("TIPOMERCATO"), DisplayName("Tipo Mercato")]
        public TipoMercato TipoMercato
        {
            get
            {
                return fTipoMercato;
            }
            set
            {
                SetPropertyValue<TipoMercato>("TipoMercato", ref fTipoMercato, value);
            }
        }

        private string fAnno;
        [Size(5), Persistent("ANNO"), DisplayName("Anno")]
        [DbType("varchar(5)")]
        public string Anno
        {
            get
            {
                return fAnno;
            }
            set
            {
                SetPropertyValue<string>("Anno", ref fAnno, value);
            }
        }


        //private StatoControlloNormativo fStatoControlloNormativo;  //TipoContattoCliente
        //[Persistent("STATO"), DisplayName("Stato Lavorazione")]
        //public StatoControlloNormativo StatoControlloNormativo
        //{
        //    get
        //    {
        //        return fStatoControlloNormativo;
        //    }
        //    set
        //    {
        //        SetPropertyValue<StatoControlloNormativo>("StatoControlloNormativo", ref fStatoControlloNormativo, value);
        //    }
        //}


        //private Mesi fMese;
        //[Persistent("MESE"), DisplayName("Mese")]
        //[RuleRequiredField("RReqField.Bollette.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        ////[RuleRange("Bollette.Mese", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        //public Mesi Mese
        //{
        //    get
        //    {
        //        return fMese;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Mesi>("Mese", ref fMese, value);
        //    }
        //}

        private int fMese;
        [Persistent("MESEENUM"), DisplayName("Mese")]
        [RuleRequiredField("RReqField.Bollette.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        //[RuleRange("Bollette.Mese", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        public int Mese
        {
            get
            {
                return fMese;
            }
            set
            {
                SetPropertyValue<int>("Mese", ref fMese, value);
            }
        }
        
        
        //private Meserif fMese;  //TipoContattoCliente
        //[Persistent("MESE"), DisplayName("Mese")]
        //[RuleRequiredField("RReqField.Bollette.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        //[RuleRange("Bollette.Mese", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        //public Meserif Mese
        //{
        //    get
        //    {
        //        return fMese;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Meserif>("Mese", ref fMese, value);
        //    }
        //}

        //public override object ConvertToStorageType(object value)
        //{
        //    return Convert.ToBoolean(value) ? "T" : "F";
        //}



        //private Meserif fMese;
        //[Persistent("MESE"), DisplayName("Mese")]
        //[RuleRequiredField("RReqField.Bollette.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        ////[EditorAlias(CAMSEditorAliases.CustomRangeMese)]
        ////[RuleRange("Bollette.Mese", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        //[ImmediatePostData(true)]
        //public Meserif Mese
        //{
        //    get
        //    {
        //        return fMese;
        //    }
        //    set
        //    {
        //        SetPropertyValue<Meserif>("Mese", ref fMese, value);
        //    }
        //}

        //vecchia definizione del mese
        //private int fMese;
        //[Persistent("MESE"), DisplayName("Mese")]
        //[RuleRequiredField("RReqField.Bollette.Mese", DefaultContexts.Save, "Il Mese è un campo obbligatorio")]
        //[DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:D}")]
        //[DevExpress.ExpressApp.Model.ModelDefault("EditMask", "N")]
        //[EditorAlias(CAMSEditorAliases.CustomRangeMese)]
        //[RuleRange("Bollette.Mese", DefaultContexts.Save, 1, 12, CustomMessageTemplate = "Il Valore deve Essere Compreso tra 1 e 12.")]
        //[ImmediatePostData(true)]
        //public int Mese
        //{
        //    get
        //    {
        //        return fMese;
        //    }
        //    set
        //    {
        //        SetPropertyValue<int>("Mese", ref fMese, value);
        //    }
        //}


        


        private DateTime fDataDal;
        [Persistent("DATADAL"), DisplayName("Consumi Data Dal:")]
        [RuleRequiredField("bolletta.DataDal", "Save", @"Data Obligata", SkipNullOrEmptyValues = false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataDal
        {
            get
            {
                return fDataDal;
            }
            set
            {
                SetPropertyValue<DateTime>("DataDal", ref fDataDal, value);
            }
        }
        private DateTime fDataAl;
        [Persistent("DATAAL"), DisplayName("Consumi Data Al:")]
        [RuleRequiredField("bolletta.DataAl", "Save", @"Data Obligata", SkipNullOrEmptyValues = false)]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_nomeGG_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_nomeGG_EditMask)]
        [EditorAlias(CAMSEditorAliases.Pers_DataOrario_Editor)]//
        [VisibleInDetailView(true), VisibleInListView(false), VisibleInLookupListView(false)]
        public DateTime DataAl
        {
            get
            {
                return fDataAl;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAl", ref fDataAl, value);
            }
        }

        private TipoLetturaBolletta fTipoLetturaBolletta;
        [ Persistent("TIPOLETTURA"), DisplayName("Tipo Lettura Bolletta")]
        public TipoLetturaBolletta TipoLetturaBolletta
        {
            get
            {
                return fTipoLetturaBolletta;
            }
            set
            {
                SetPropertyValue<TipoLetturaBolletta>("TipoLetturaBolletta", ref fTipoLetturaBolletta, value);
            }
        }


 

        private UnitaMisura fUnMisuraConsumi;
        [Persistent("UNITAMISURA"), DisplayName("Un.Misura")]
        public UnitaMisura UnMisuraConsumi
        {
            get
            {
                return fUnMisuraConsumi;
            }
            set
            {
                SetPropertyValue<UnitaMisura>("UnMisuraConsumi", ref fUnMisuraConsumi, value);
            }
        }

        private double fConsumi;
        [Persistent("CONSUMI"), DisplayName("Consumi")]
        //[ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double Consumi
        {
            get
            {
                return fConsumi;
            }
            set
            {
                SetPropertyValue<double>("Consumi", ref fConsumi, value);

            }
        }
     
  
        private double fImporti;
        [Persistent("IMPORTI"), DisplayName("Importi")]
        [ModelDefault("DisplayFormat", "{0:C}")]
        [ModelDefault("EditMask", "C")]
        [ImmediatePostData(true)]
        public double Importi
        {
            get
            {
                return fImporti;
            }
            set
            {
                SetPropertyValue<double>("Importi", ref fImporti, value);
          
            }
        }
        //private FileData fDocum;
        [Persistent("DOCUMENTO"), DisplayName("Inserimento Documento")]
        [FileTypeFilter("Document files", 1, "*.pdf", "*.doc")]
        [FileTypeFilter("Image files", 2, "*.png", "*.gif", "*.jpg")]
        [Delayed(true)]
        public FileData Documento
        {
            get
            {
                return GetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("Documento");
            }
            set
            {
                SetDelayedPropertyValue<DevExpress.Persistent.BaseImpl.FileData>("Documento", value);
            }
        }

        [Association(@"Bollette_BolletteDettaglio", typeof(BolletteDettaglio)), Aggregated, DevExpress.Xpo.DisplayName("Dettaglio")]
        //[Appearance("RegMisure.Abilita.RegMisureDettaglios", Criteria = "Oid < 0", Enabled = false)]
        public XPCollection<BolletteDettaglio> BolletteDettaglios
        {
            get
            {
                return GetCollection<BolletteDettaglio>("BolletteDettaglios");
            }
        }


        private Asset fAsset;
        [Persistent("ASSET"), DevExpress.ExpressApp.DC.XafDisplayName("Apparato")]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Asset
        {
            get
            {
                return GetDelayedPropertyValue<Asset>("Asset");
            }
            set
            {
                SetDelayedPropertyValue<Asset>("Asset", value);
            }

        }



        private string fUtente;
        [Size(100), Persistent("UTENTE"), DevExpress.Xpo.DisplayName("Utente")]
        [Appearance("RegBollette.Abilita.Utente", Enabled = false)]
        [DbType("varchar(100)")]
        [System.ComponentModel.Browsable(false)]
        public string Utente
        {
            get { return fUtente; }
            set { SetPropertyValue<string>("Utente", ref fUtente, value); }
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"), DevExpress.Xpo.DisplayName("Data Upadate")]
        [Appearance("RegBollette.Abilita.DataAggiornamento", Enabled = false)]
        [System.ComponentModel.Browsable(false)]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }

    }
}
