using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
    Persistent("TIPOMANUTENZIONE")]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Tipo Manutenzione")]
    [ImageName("IDE")]
    [NavigationItem("Ticket")]
    public class TipoManutenzione : XPObject
    {
        public TipoManutenzione()
            : base()
        {
        }
        public TipoManutenzione(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(50),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(50)")]
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
