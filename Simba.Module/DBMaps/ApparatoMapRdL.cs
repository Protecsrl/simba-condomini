using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using DevExpress.Data.Filtering;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.ExpressApp.Model;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.BaseImpl;
using DevExpress.Xpo;
using System.ComponentModel;
using System.Drawing;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using CAMS.Module.DBPlant.Vista;
using CAMS.Module.DBPlant;
using DevExpress.Persistent.Validation;
//namespace CAMS.Modu
namespace CAMS.Module.DBMaps
{
    //    class ApparatoMapRdL
    //    {
    //    }
    //}
    //class AssetMap    //{
    [DefaultClassOptions, Persistent("AppMAP_RdL")]
    [System.ComponentModel.DefaultProperty("Asset in Mappa")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Ricerca Apparato in Mappa")]
    [ImageName("BO_Country_v92")]
    [NavigationItem("Patrimonio")]
    public class ApparatoMapRdL : XPObject
    {
        public ApparatoMapRdL() : base() { }

        public ApparatoMapRdL(Session session) : base(session) { }

        private string title;
        public string Title
        {
            get { return title; }
            set { SetPropertyValue(nameof(Title), ref title, value); }
        }
        private string fOBJWEB;
        [NonPersistent, Size(100), DevExpress.Xpo.DisplayName("web")]
        [VisibleInListView(false)]
        public string OBJWEB
        {
            get { return fOBJWEB; }
            set { SetPropertyValue<string>("OBJWEB", ref fOBJWEB, value); }
        }

        private Immobile fImmobile;
        [Persistent("IMMOBILE"), System.ComponentModel.DisplayName("Immobile")]
        [VisibleInListView(false)]
         [ImmediatePostData(true)] 
        [Delayed(true)]
        public Immobile Immobile
        {
            get { return GetDelayedPropertyValue<Immobile>("Immobile"); }
            set { SetDelayedPropertyValue<Immobile>("Immobile", value); }
        }


        private Servizio fServizio;
        [Persistent("SERVIZIO"), System.ComponentModel.DisplayName("Servizio")]
        // [RuleRequiredField("RuleReq.ApparatoMapRdL.Impianto", DefaultContexts.Save, "Impianto è un campo obbligatorio")]
        //  [Appearance("ApparatoMapRdL.Abilita.Impianto", Criteria = "(Immobile  is null) OR (not (StdApparato is null))", FontColor = "Black", Enabled = false)]
        [ImmediatePostData(true)]
        [DataSourceCriteria("Immobile.Oid = '@This.Immobile.Oid'")]
        [Delayed(true)]
        public Servizio Servizio
        {
            get { return GetDelayedPropertyValue<Servizio>("Servizio"); }
            set { SetDelayedPropertyValue<Servizio>("Servizio", value); }
        }


        [NonPersistent]
        private XPCollection<AssetoMap> _ApparatoMaps;
        [DevExpress.ExpressApp.DC.XafDisplayName("Mappa")]
        [DevExpress.ExpressApp.CollectionOperationSet(AllowAdd = false, AllowRemove = false)]
        public XPCollection<AssetoMap> ApparatoMaps
        {
            get
            {
                //OperandValue[] parameters = new OperandValue();    //parameters[0].Value = "Saloon";        //parameters[1].Value = 100000;
                //string ParCriteria = string.Format("[<Apparato>][^.Oid == GeoLocalizzazione And Oid == {0}]", );
                if (Servizio == null)
                {
                    _ApparatoMaps = null;
                    return _ApparatoMaps;
                }

                if (_ApparatoMaps == null)
                {
                    BinaryOperator bOp = new BinaryOperator("OidImpianto", Evaluate("Impianto.Oid"));// Impianto.Oid);
                    _ApparatoMaps = new XPCollection<AssetoMap>(Session, bOp);             //ParCriteria));CriteriaOperator.Parse(       "OidApparato = ? Or OidApparatoPadre = ?", Evaluate("OidApparato"), Evaluate("OidApparato")
                }
                return _ApparatoMaps;
            }
        }
    }
}