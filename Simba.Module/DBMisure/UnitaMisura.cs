using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBMisure
{
    [DefaultClassOptions,
    Persistent("MISUREUNITAMISURA")]
    [NavigationItem(false)]
    public class UnitaMisura : XPObject
    {
        public UnitaMisura()
            : base()
        {
        }

        public UnitaMisura(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fDescrizione;
        [Size(200),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
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

        private string fSimbolo;
        [Size(10),
        Persistent("SIMBOLO"),
        DisplayName("Simbolo")]
        [DbType("varchar(10)")]
        public string Simbolo
        {
            get
            {
                return fSimbolo;
            }
            set
            {
                SetPropertyValue<string>("Simbolo", ref fSimbolo, value);
            }
        }


        [Association(@"Master_UnitaMisura", typeof(Master)),
        DisplayName("Master")]
        [MemberDesignTimeVisibility(false)]
        public XPCollection<Master> Masters
        {
            get
            {
                return GetCollection<Master>("Masters");
            }
        }

        public override string ToString()
        {
            return String.Format("{0}({1})", Descrizione, Simbolo);
        }
    }
}
