using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;
using CAMS.Module.DBALibrary;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using CAMS.Module.Classi;
using CAMS.Module.DBTLC;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions, Persistent("MISMASTER")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Master Misure")]
    [Appearance("Master.Disabilita.Dettagliomaggioredizero", TargetItems = "Descrizione;Immobile;UnitaMisura", Criteria = "MasterDettaglios.Count() > 0", Enabled = false)]
    [NavigationItem("Misure")]
    [ImageName("Action_CreateDashboard")]
    public class Master : XPObject
    {
        public Master()
            : base()
        {
        }
        public Master(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();

        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), DisplayName("Immobile")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public Immobile Immobile
        {
            get
            {
                return fImmobile;
            }
            set
            {
                SetPropertyValue<Immobile>("Immobile", ref fImmobile, value);
            }
        }

        private string fDescrizione;
        [Size(200), Persistent("DESCRIZIONE")]
        [DbType("varchar(200)")]
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

        private string fLineaFunzionale;
        [Size(200), Persistent("LINEAFUNZIONALE"), DisplayName("Linea Funzionale")]
        [DbType("varchar(200)")]
        public string LineaFunzionale
        {
            get
            {
                return fLineaFunzionale;
            }
            set
            {
                SetPropertyValue<string>("LineaFunzionale", ref fLineaFunzionale, value);
            }
        }

        private StdAsset fStdApparato;
        [Size(200), Persistent("STDAPPARATO"), DisplayName("Tipo Apparato")]
        [Appearance("Master.StdApparato", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        [System.ComponentModel.Browsable(false)]
        [DbType("varchar(200)")]
        [ExplicitLoading()]
        public StdAsset StdApparato
        {
            get
            {
                return fStdApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StdApparato", ref fStdApparato, value);
            }
        }

        private UnitaMisura fUnitaMisura;
        [Association(@"Master_UnitaMisura"), Persistent("UNITAMISURA"), DisplayName("Unità di misura")]
        [ExplicitLoading()]
        public UnitaMisura UnitaMisura
        {
            get { return fUnitaMisura; }
            set { SetPropertyValue<UnitaMisura>("UnitaMisura", ref fUnitaMisura, value); }
        }


        private TipologiaFornituraBolletta fTipologiaFornituraBolletta;
        [Size(50), Persistent("TIPOFORNITURA"), DisplayName("Fornitura in Bolletta")]
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

        [Association(@"MasterDettaglio_Master", typeof(MasterDettaglio)), Aggregated, DisplayName("Elenco Apparati Assegnati")]
        [Appearance("Master.MasterDettaglios.Abilita", Criteria = "Immobile is null", Enabled = false)]
        //[Appearance("Master.MasterDettaglios.Visibility", Criteria = "TipoMasterMisura != 1", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public XPCollection<MasterDettaglio> MasterDettaglios
        {
            get
            {
                return GetCollection<MasterDettaglio>("MasterDettaglios");
            }
        }

        [Association(@"TipoMisure_Master", typeof(MasterTipoMisure)), Aggregated, DisplayName("Elenco TipoMisure")]
        [Appearance("Master.MasterTipoMisures.Abilita", Criteria = "Immobile is null", Enabled = false)]
        //[Appearance("Master.MasterTipoMisures.Visibility", Criteria = "TipoMasterMisura != 1", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public XPCollection<MasterTipoMisure> MasterTipoMisures
        {
            get
            {
                return GetCollection<MasterTipoMisure>("MasterTipoMisures");
            }
        }

        private TipoMasterMisura fTipoMasterMisura;
        [Persistent("TIPOMISURA"), DisplayName("Tipo Misura")]
        public TipoMasterMisura TipoMasterMisura
        {
            get { return fTipoMasterMisura; }
            set { SetPropertyValue<TipoMasterMisura>("TipoMasterMisura", ref fTipoMasterMisura, value); }
        }

        [Association(@"IEPlantList_Master"), Aggregated]
        [DisplayName("Elenco Punti Misura")]
        [Appearance("Master.ElencoPuntis.Abilita", Criteria = "Immobile is null", Enabled = false)]
        //[Appearance("Master.ElencoPuntis.Visibility", Criteria = "TipoMasterMisura != 2", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public XPCollection<IEPlantList> ElencoPuntis
        {
            get
            {
                return GetCollection<IEPlantList>("ElencoPuntis");
            }
        }


        public override string ToString()
        {
            if (this.Descrizione != null)
                return String.Format("{0}-{1}({2})", Immobile.CodDescrizione, Descrizione, LineaFunzionale);

            return null;
        }
    }
}
