
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBClienti
{
    [DefaultClassOptions, Persistent("CLIENTI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Clienti")]
    [ImageName("PackageProduct")]
    [NavigationItem("Contratti")]
    public class Clienti : XPObject
    {
        public Clienti() : base() { }
        public Clienti(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }
                
        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"),   DisplayName("Descrizione")]
        [DbType("varchar(100)")]
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

        private string fIndirizzo;
        [Size(250), Persistent("INDIRIZZO"), DisplayName("Indirizzo")]
        [DbType("varchar(250)")]
        [ExplicitLoading]
        public string Indirizzo
        {
            get
            {
                return fIndirizzo;
            }
            set
            {
                SetPropertyValue<string>("Indirizzo", ref fIndirizzo, value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }

    }
}

