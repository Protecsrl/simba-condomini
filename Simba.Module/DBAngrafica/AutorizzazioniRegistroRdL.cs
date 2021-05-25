using System;
using DevExpress.Xpo;
using System.Linq;
using DevExpress.Persistent.Base;
using System.Collections.Generic;
using CAMS.Module.DBTask;
using CAMS.Module.Classi;

namespace CAMS.Module.DBAngrafica
{
    [DefaultClassOptions, Persistent("AUTORIZZAZIONIREGRDL")]  // [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Autorizzazioni Registro RDL")]
    [ImageName("Action_Debug_Step")]
    [NavigationItem(false)]
    public class AutorizzazioniRegistroRdL : XPObject
    {
        public AutorizzazioniRegistroRdL()
            : base()
        {
        }

        public AutorizzazioniRegistroRdL(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private RegistroRdL fRegistroRdL;
        [Persistent("REGRDL"),
        Size(1000),
        Association(@"REGISTRORDL_AutorizzazioniRegistroRdL"),Aggregated,
        DisplayName("Registro RdL")]
        public RegistroRdL RegistroRdL
        {
            get
            {
                return fRegistroRdL;
            }
            set
            {
                SetPropertyValue<RegistroRdL>("RegistroRdL", ref fRegistroRdL, value);
            }
        }

        private TipoAutorizzazioniRegRdL fTipoAutorizzazioniRegRdL;
        [Persistent("TIPOAUTORIZZAZIONI"),
        Size(1000),
        DisplayName("Regola di Autorizzazione")]
        public TipoAutorizzazioniRegRdL TipoAutorizzazioniRegRdL
        {
            get
            {
                return fTipoAutorizzazioniRegRdL;
            }
            set
            {
                SetPropertyValue<TipoAutorizzazioniRegRdL>("TipoAutorizzazioniRegRdL", ref fTipoAutorizzazioniRegRdL, value);
            }
        }
    }
}
