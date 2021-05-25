using CAMS.Module.Classi;
using CAMS.Module.DBPlant;
using CAMS.Module.DBTLC;
using CAMS.Module.PropertyEditors;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.ComponentModel;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions, Persistent("REGMISUREDETTAGLIO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dettaglio Misure")]
    [System.ComponentModel.DisplayName("Dettaglio Misure")]
    [ImageName("Action_Inline_Edit")]
    [NavigationItem(false)]
    //  FullName
    [System.ComponentModel.DefaultProperty("FullName")]
    public class RegMisureDettaglio : XPObject
    {
        public RegMisureDettaglio()
            : base()
        {
        }

        public RegMisureDettaglio(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private RegMisure fRegMisure;
        [Association(@"RegMisureDettaglio_RegMisure"),
        Persistent("REGMISURE"),
        DevExpress.Xpo.DisplayName("Registro Misure")]
        [ExplicitLoading()]
        [ImmediatePostData(true)]
        public RegMisure RegMisure
        {
            get { return fRegMisure; }
            set { SetPropertyValue<RegMisure>("RegMisure", ref fRegMisure, value); }
        }
        #region  persintent alias
        private Immobile fImmobile;
        [PersistentAlias("Apparato.Impianto.Immobile"), DevExpress.Xpo.DisplayName("Immobile")]
        [Appearance("RegMisureDettaglio.Immobile.Visibility", Criteria = "[Apparato] Is Null", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public Immobile Immobile
        {
            get
            {
                var tempObject = EvaluateAlias("Immobile");
                if (tempObject != null)
                {
                    return (Immobile)tempObject;
                }
                else
                {
                    return null;
                }
            }
        }

        private Servizio fServizio;
        [PersistentAlias("Apparato.Servizio"), DevExpress.Xpo.DisplayName("Impianto")]
        [Appearance("RegMisureDettaglio.Impianto.Visibility", Criteria = "[Apparato] Is Null", Visibility =             DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide )]
        public Servizio Servizio
        {
            get
            {
                var tempObject = EvaluateAlias("Servizio");
                if (tempObject != null)
                {
                    return (Servizio)tempObject;
                }
                else
                {
                    return fServizio;
                }
            }
        }
        #endregion

        [Association(@"RegMisureDettaglio_Asset"), Persistent("ASSET"), DevExpress.Xpo.DisplayName("Asset")]
        //[DataSourceCriteria("Oid = '@This.MasterDettaglio.Apparato.Oid'")]
        [DataSourceCriteria("Iif(RegMisure is not null,    [<MasterDettaglio>][^.Oid == Asset.Oid And  Master.Oid == '@This.RegMisure.Master.Oid'],1==1)")]
        //[Appearance("regmisuredettaglio.Apparato", Criteria = "[MasterDettaglio] Is Not Null", Enabled = false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public Asset Asset
        {
            get { return GetDelayedPropertyValue<Asset>("Asset"); }
            set { SetDelayedPropertyValue<Asset>("Asset", value); }
        }

        private UnitaMisura fUnitaMisura;
        [DataSourceCriteria("Iif(RegMisure is not null,    [<MasterTipoMisure>][^.Oid == UnitaMisura.Oid And  Master.Oid == '@This.RegMisure.Master.Oid'],1==1)")]
        //[DataSourceProperty("[<MasterTipoMisure>][Master.Oid == '@This.RegMisure.Master.Oid'].Single(UnitaMisura)")]
        [Persistent("UNITAMISURA"), DevExpress.Xpo.DisplayName("Unità di misura")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public UnitaMisura UnitaMisura
        {
            get { return fUnitaMisura; }
            set { SetPropertyValue<UnitaMisura>("UnitaMisura", ref fUnitaMisura, value); }
        }

        private Double fValore;
        [Persistent("VALORE"), DevExpress.Xpo.DisplayName("Valore")]
        [RuleRequiredField("RReqFieldObJ.RegMisureDettaglio.Valore", DefaultContexts.Save, "Il Valore è un campo obligatorio")]
        public Double Valore
        {
            get { return fValore; }
            set { SetPropertyValue<Double>("Valore", ref fValore, value); }
        }

        private string fstrValore;
        [Persistent("VALORETXT"), DevExpress.Xpo.DisplayName("Valore txt")]
        [RuleRequiredField("RReqFieldObJ.RegMisureDettaglio.strValore", DefaultContexts.Save, "Il Valore è un campo obligatorio")]
        public string strValore
        {
            get { return fstrValore; }
            set { SetPropertyValue<string>("strValore", ref fstrValore, value); }
        }

        private DateTime? fDataMisura;
        [Persistent("DATAMISURA"), DevExpress.Xpo.DisplayName("Data Misura")]
        [DevExpress.ExpressApp.Model.ModelDefault("DisplayFormat", "{0:" + CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask + "}")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", CAMSEditorCostantFormat.Data_e_Ora_Min_EditMask)]
        [RuleRequiredField("RReqFO.RegMisure.RegMisureDettaglio.DataMisura", DefaultContexts.Save, "La Data Misura è un campo obligatorio")]
        public DateTime? DataMisura
        {
            get { return fDataMisura; }
            set { SetPropertyValue<DateTime?>("DataMisura", ref fDataMisura, value); }
        }

        private InManutenzione fFlag;
        [Persistent("FLAGMANUTENZIONE"), DevExpress.Xpo.DisplayName("Manutenzione")]
        public InManutenzione Flag
        {
            get { return fFlag; }
            set { SetPropertyValue<InManutenzione>("Flag", ref fFlag, value); }
        }

        private TipologiaFornituraBolletta fTipologiaFornituraBolletta;
        [Size(50), Persistent("TIPOFORNITURA"), DevExpress.Xpo.DisplayName("Fornitura in Bolletta")]
        public TipologiaFornituraBolletta TipologiaFornituraBolletta
        {
            get { return fTipologiaFornituraBolletta; }
            set { SetPropertyValue<TipologiaFornituraBolletta>("TipologiaFornituraBolletta", ref fTipologiaFornituraBolletta, value); }
        }

        private String fNote;
        [Size(1000), Persistent("NOTE"), DevExpress.Xpo.DisplayName("Note")]
        [DbType("varchar(1000)")]
        public String Note
        {
            get { return fNote; }
            set { SetPropertyValue<String>("Note", ref fNote, value); }
        }

        private IEPlantList fIEPlantList;     //[Association(@"RegMisureDettaglio_DettaglioMaster"),
        [Persistent("TLC_IEPLANTLIST"), DevExpress.Xpo.DisplayName("TLC Plant List")]    //[DataSourceCriteria("Master = '@This.RegMisure.Master'")]
        [Appearance("RegMisureDettaglio.IEPlantList.Visibility", Criteria = "RegMisure.Master.TipoMasterMisura != 2", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [Appearance("regmisuredettaglio.IEPlantList.Enabled", Criteria = "1==1", Enabled = false)]
        [VisibleInDetailView(true), VisibleInListView(false)]
        [ExplicitLoading()]
        [Delayed(true)]
        public IEPlantList IEPlantList
        {
            get { return GetDelayedPropertyValue<IEPlantList>("IEPlantList"); }
            set { SetDelayedPropertyValue<IEPlantList>("IEPlantList", value); }
        }
        

        [Association(@"RegMisureDettaglio_Dettaglio", typeof(RegMisureDettaglioDett)), Aggregated, DevExpress.Xpo.DisplayName("Dettaglio")]
        [Appearance("RegMisureDettaglioDett.Abilita.RegMisureDettaglioDetts", Criteria = "Oid < 0", Enabled = false)]
        public XPCollection<RegMisureDettaglioDett> RegMisureDettaglioDetts
        {
            get
            {
                return GetCollection<RegMisureDettaglioDett>("RegMisureDettaglioDetts");
            }
        }

   [PersistentAlias("Iif(IEPlantList is not null And Apparato is not null And DataMisura is not null,Apparato.Descrizione + ', ' +  IEPlantList.Descrizione + ', ' + DataMisura  ,null)")]
        [DevExpress.Xpo.DisplayName("FullName")]
        [Browsable(false)]
        public string FullName
        {
            get
            {
                //IEPlantList.Descrizione        
                var tempObject = EvaluateAlias("FullName");
                if (tempObject != null)
                {
                    return tempObject.ToString();
                }
                return null;
            }
        }

        public override string ToString()
        {
            return this.FullName;
        }

    }
}
