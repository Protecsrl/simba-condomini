using System;
using System.Collections.Generic;
using System.Linq;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Validation;
using System.ComponentModel;
using DevExpress.ExpressApp.DC;

namespace CAMS.Module.DBPlant
{
    [NavigationItem(false), System.ComponentModel.DisplayName("Parametri Impianto da creare")]
    [DefaultClassOptions, NonPersistent]
    [VisibleInDashboards(false)]
    [RuleCriteria("NuovoImpianto.Valida.Descrizione", DefaultContexts.Save, @"VerificaUnivocita",
   CustomMessageTemplate = "Descrizione o Cod Descrizione non univoca!, Modificare",
   SkipNullOrEmptyValues = false, InvertResult = true, ResultType = ValidationResultType.Error)]

    public class NuovoImpianto : XPObject
    {
        public NuovoImpianto()
            : base()
        {
        }
        public NuovoImpianto(Session session)
            : base(session)
        {
        }

        private string fDescrizione;
        [NonPersistent, XafDisplayName("Descrizione")]
        [RuleRequiredField("RReqField.NuovoImpianto.Descrizione", DefaultContexts.Save, @"Descrizione è un campo obbligatorio")]
        [ImmediatePostData(true)]
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
        [NonPersistent, XafDisplayName("Cod Descrizione")]
        [RuleRequiredField("RReqField.NuovoImpianto.CodDescrizione", DefaultContexts.Save, @"CodDescrizione è un campo obbligatorio")]
        [ImmediatePostData(true)]
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


        private Servizio fVecchioImpianto;
        [NonPersistent, DevExpress.Xpo.DisplayName("Impianto")]
        [Appearance("NuovoImpianto.VecchioImpianto.Visibility", Visibility = ViewItemVisibility.Hide)]
        public Servizio VecchioImpianto
        {
            get
            {
                return fVecchioImpianto;
            }
            set
            {
                SetPropertyValue<Servizio>("VecchioImpianto", ref fVecchioImpianto, value);
            }
        }

        [NonPersistent]
        [Browsable(false)]
        public bool VerificaUnivocita
        {
            get
            {
                int v = Session.Query<Servizio>().Where(w => w.CodDescrizione == this.CodDescrizione).Count();
                v = v + Session.Query<Servizio>().Where(w => w.Descrizione == this.Descrizione).Count();
                if (v > 0)
                    return true;

                return false;
            }

        }

        protected override void OnChanged(string propertyName, object oldValue, object newValue)
        {
            base.OnChanged(propertyName, oldValue, newValue);

            if (IsInvalidated)
                return;

            if (!this.IsLoading)
            {
                if (newValue != null && propertyName == "CodDescrizione")
                {
                    Evaluate("VerificaUnivocita");
                }
                if (newValue != null && propertyName == "Descrizione")
                {
                    Evaluate("VerificaUnivocita");
                }
            }
        }



    }
}
