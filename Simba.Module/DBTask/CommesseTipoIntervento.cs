using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using CAMS.Module.DBPlant;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using CAMS.Module.Classi;

namespace CAMS.Module.DBTask
{

    [DefaultClassOptions,
    Persistent("COMMESSETINTERVENTO")]
    [VisibleInDashboards(false)]
    //[System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Commesse Tipo Intervento")]
    [ImageName("Time")]
    [NavigationItem("Tabelle Anagrafiche")]
    public class ContrattoTipoIntervento : XPObject
    {

            public ContrattoTipoIntervento()
            : base()
        {
        }
            public ContrattoTipoIntervento(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        [Persistent("COMMESSE"), DisplayName("Commesse")]
        [Association("Contratti_ContrattiTIntervento")]
        [RuleRequiredField("RReq.CommesseTIntervento.Commesse", DefaultContexts.Save, "è un campo obligatorio")]
        [ToolTip("Identificazione della Commesse")]
        [ExplicitLoading]
        [Delayed(true)]
        public Contratti Commesse
        {
            get { return GetDelayedPropertyValue<Contratti>("Commesse"); }
            set { SetDelayedPropertyValue<Contratti>("Commesse", value); }
 
        }

        [Persistent("TIPOINTERVENTO"), System.ComponentModel.DisplayName("Tipo Intervento")]
        [RuleRequiredField("RuleReq.CommesseTIntervento.TipoIntervento", DefaultContexts.Save, "Priorita è un campo obbligatorio")]
        [ExplicitLoading()]
        [Delayed(true)]
        public TipoIntervento TipoIntervento
        {
            get { return GetDelayedPropertyValue<TipoIntervento>("TipoIntervento"); }
            set { SetDelayedPropertyValue<TipoIntervento>("TipoIntervento", value); }
         }

        //[Persistent("DEFAULTVALUE"), System.ComponentModel.DisplayName("Valore di Default")]
        //[Delayed(true)]
        //FlgAbilitato Default
        //{
        //    get { return GetDelayedPropertyValue<FlgAbilitato>("Default"); }
        //    set { SetDelayedPropertyValue<FlgAbilitato>("Default", value); }
        //}
        private FlgAbilitato fDefault;
        [Persistent("DEFAULTVALUE"), DisplayName("Valore di Default")]
        [VisibleInListView(false), VisibleInLookupListView(false)]
        [ExplicitLoading()]
        public FlgAbilitato Default
        {
            get
            {
                return fDefault;
            }
            set
            {
                SetPropertyValue<FlgAbilitato>("Default", ref fDefault, value);
            }
        }

    }
}
