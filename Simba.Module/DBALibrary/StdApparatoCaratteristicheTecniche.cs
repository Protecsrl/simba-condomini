using CAMS.Module.Classi;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;

namespace CAMS.Module.DBALibrary
{
    [DefaultClassOptions, Persistent("APPARATOSTDCARTECNICHE"), System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Caratteristiche Tecniche Tipiche")]
    [RuleCombinationOfPropertiesIsUnique("RuleCombIsUnique_StdApparatoCarTecniche", DefaultContexts.Save, "StandardApparato,Descrizione",
    CustomMessageTemplate = @"Attenzione è stato già inserito una Descrizione({Descrizione}) già presente questo Tipo di Apparato ({StandardApparato}). \r\nInserire Nuovamente.",
    SkipNullOrEmptyValues = false)]

    // Context = "Apparato_DetailView_Gestione;Apparato_DetailView", 
    [Appearance("stdApparatoCaratteristicheTecniche.visibile.valoretendina", AppearanceItemType.LayoutItem,
                    @"[TipoValoreCaratteristicaTecnica] != 'Tendina'",
                    TargetItems = "AppCaratteristicaValoreSeleziones",
                   Priority = 1,
                    Visibility = ViewItemVisibility.Hide)]

    //[ImageName("Kdimensione")]
    [NavigationItem("Patrimonio")]

    public class StdApparatoCaratteristicheTecniche : XPObject
    {

        public StdApparatoCaratteristicheTecniche()
            : base()
        {
            //this.TipoValoreCaratteristicaTecnica
        }

        public StdApparatoCaratteristicheTecniche(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
            //if (this.Oid == -1)
            //{

            //}
        }

        private StdAsset fStandardApparato;
        [Persistent("EQSTD"), Association(@"StdApparato_CaratteristicheTecniche"), DevExpress.Xpo.DisplayName("Tipo Apparato")]
        [RuleRequiredField("RuleReq.StdApparato.CarattTecn", DefaultContexts.Save, "Lo Standard dell'Apparato è un campo obbligatorio")]
        [ExplicitLoading()]
        public StdAsset StandardApparato
        {
            get
            {
                return fStandardApparato;
            }
            set
            {
                SetPropertyValue<StdAsset>("StandardApparato", ref fStandardApparato, value);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(150), DevExpress.Xpo.DisplayName("Descrizione")]
        [RuleRequiredField("RuleReq.StdApparato.CarattTecn.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [DbType("varchar(150)")]
        public string Descrizione
        {
            get            {                return fDescrizione;            }
            set            {                SetPropertyValue<string>("Descrizione", ref fDescrizione, value);            }
        }



        private UnitaMisura fUnitaMisura;
        [Persistent("UNITAMISURA"), XafDisplayName("Unità di misura")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public UnitaMisura UnitaMisura
        {
            get            {                return fUnitaMisura;            }
            set            {                SetPropertyValue<UnitaMisura>("UnitaMisura", ref fUnitaMisura, value);            }
        }


        private TipoValoreCaratteristicaTecnica fTipoValoreCaratteristicaTecnica;
        [Persistent("TIPOCARATTERISTICATECNICA"), XafDisplayName("Tipo Valore Caratteristica Tecnica")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        //[ExplicitLoading()]
        public TipoValoreCaratteristicaTecnica TipoValoreCaratteristicaTecnica
        {
            get            {                return fTipoValoreCaratteristicaTecnica;            }
            set            {                SetPropertyValue<TipoValoreCaratteristicaTecnica>("TipoValoreCaratteristicaTecnica", ref fTipoValoreCaratteristicaTecnica, value);            }
        }


        [Association(@"StdApparatoCaratteristicheTecniche_AppCaratteristichaValoreSelezione", typeof(AppCaratteristicaValoreSelezione)),DevExpress.Xpo.Aggregated]
        [XafDisplayName("Valori Selezionabili")]
        public XPCollection<AppCaratteristicaValoreSelezione> AppCaratteristicaValoreSeleziones
        {
            get
            {
                return GetCollection<AppCaratteristicaValoreSelezione>("AppCaratteristicaValoreSeleziones");
            }
        }




    }
}




