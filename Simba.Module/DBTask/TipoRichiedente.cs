using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Validation;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("RICHIEDENTETIPO")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Richiedenti")]
    [ImageName("TopCustomers")]
    [NavigationItem("Contratti")]
    public class TipoRichiedente : XPObject
    {
        public TipoRichiedente()
            : base()
        {
        }

        public TipoRichiedente(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Persistent("DESCRIZIONE"),
        Size(100)]
        [DbType("varchar(100)")]
        [RuleRequiredField("RReqField.TipoRichiedente.Descrizione", DefaultContexts.Save, "La Descrizione è un campo obbligatorio")]
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
    }
}
