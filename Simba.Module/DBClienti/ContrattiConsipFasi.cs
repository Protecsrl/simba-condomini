using DevExpress.ExpressApp.DC;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
 
namespace CAMS.Module.DBClienti
{
    [DefaultClassOptions, Persistent("CONTRATTICONSIPFASI")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Fase Contrattuale")]
    [ImageName("PackageProduct")]
    [NavigationItem("Contratti")]
    public class ContrattiConsipFasi : XPObject
    {
        public ContrattiConsipFasi() : base() { }
        public ContrattiConsipFasi(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fDescrizione;
        [Size(100), Persistent("DESCRIZIONE"), XafDisplayName("Descrizione")]
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

  

        public override string ToString()
        {
            return string.Format("{0}", this.Descrizione);
        }

    }
}

