using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions,
    Persistent("MISUREVALINSERIMENTO")]
    [System.ComponentModel.DefaultProperty("Valori Inseribili")]
    [NavigationItem(false)]
    public class ValoriRegoleInsertDati : XPObject
    {
        public ValoriRegoleInsertDati()
            : base()
        {
        }

        public ValoriRegoleInsertDati(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(40),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione Valore")]
        [DbType("varchar(40)")]
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

        private string fValore;
        [Size(10),
        Persistent("VALORE"),
        DisplayName("Valore")]
        [DbType("varchar(10)")]
        public string Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<string>("Valore", ref fValore, value);
            }
        }
    }
}
