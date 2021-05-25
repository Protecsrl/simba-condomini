using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;




using CAMS.Module.Classi;
using CAMS.Module.DBMisure;
using CAMS.Module.DBPlant;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.DC;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
//namespace CAMS.Module.DBMisure
//{
//    class MasterTipoMisure
//    {
//    }
//}
namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions, Persistent("MISMASTERTIPOMISURE"), System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Misure Tipiche")]
    //[RuleCombinationOfPropertiesIsUnique("RuleCombIsUnique_StdApparatoCarTecniche", DefaultContexts.Save, "StandardApparato,Descrizione",
    //CustomMessageTemplate = @"Attenzione è stato già inserito una Descrizione({Descrizione}) già presente questo Tipo di Apparato ({StandardApparato}). \r\nInserire Nuovamente.",
    //SkipNullOrEmptyValues = false)]

    // Context = "Apparato_DetailView_Gestione;Apparato_DetailView", 
    //[Appearance("stdApparatoCaratteristicheTecniche.visibile.valoretendina", AppearanceItemType.LayoutItem,
    //                @"[TipoValoreCaratteristicaTecnica] != 'Tendina'",
    //                TargetItems = "AppCaratteristicaValoreSeleziones",
    //               Priority = 1,
    //                Visibility = ViewItemVisibility.Hide)]

    //[ImageName("Kdimensione")]
    [NavigationItem(false)]

    public class MasterTipoMisure : XPObject
    {

        public MasterTipoMisure()
            : base()
        {
            //this.TipoValoreCaratteristicaTecnica
        }

        public MasterTipoMisure(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private Master fMaster;
        [Persistent("MISMASTER"), Association(@"TipoMisure_Master"), DevExpress.Xpo.DisplayName("Master Misure")]
        [RuleRequiredField("RuleReq.MasterTipoMisure.Master Misure", DefaultContexts.Save, "il Master è un campo obbligatorio")]
        [ExplicitLoading()]
        public Master Master
        {
            get
            {
                return fMaster;
            }
            set
            {
                SetPropertyValue<Master>("Master", ref fMaster, value);
            }
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"), Size(150), DevExpress.Xpo.DisplayName("Descrizione")]
        [RuleRequiredField("RuleReq.MasterTipoMisure.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
        [DbType("varchar(150)")]
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



        private UnitaMisura fUnitaMisura;
        [Persistent("UNITAMISURA"), XafDisplayName("Unità di misura")]
        [ImmediatePostData(true)]
        [ExplicitLoading()]
        public UnitaMisura UnitaMisura
        {
            get
            {
                return fUnitaMisura;
            }
            set
            {
                SetPropertyValue<UnitaMisura>("UnitaMisura", ref fUnitaMisura, value);
            }
        }


        private TipoValoreCaratteristicaTecnica fTipoValoreMisura;
        [Persistent("TIPOCARATTERISTICATECNICA"), XafDisplayName("Tipo Valore Misura")]
        [VisibleInListView(false), VisibleInDetailView(true)]
        [ImmediatePostData(true)]
        //[ExplicitLoading()]
        public TipoValoreCaratteristicaTecnica TipoValoreMisura
        {
            get
            {
                return fTipoValoreMisura;
            }
            set
            {
                SetPropertyValue<TipoValoreCaratteristicaTecnica>("TipoValoreMisura", ref fTipoValoreMisura, value);
            }
        }

        //[Association(@"StdApparatoCaratteristicheTecniche_AppCaratteristichaValoreSelezione", typeof(AppCaratteristicaValoreSelezione)), DevExpress.Xpo.Aggregated]
        //[XafDisplayName("Valori Selezionabili")]
        //public XPCollection<AppCaratteristicaValoreSelezione> AppCaratteristicaValoreSeleziones
        //{
        //    get
        //    {
        //        return GetCollection<AppCaratteristicaValoreSelezione>("AppCaratteristicaValoreSeleziones");
        //    }
        //}
    }
}





