
using CAMS.Module.Classi;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBPlant
{
    [DefaultClassOptions, Persistent("REFERENTECONTRATTO")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Referente Contratto")]
    [ImageName("PackageProduct")]
    [NavigationItem("Contratti")]

    public class ReferenteContratto : XPObject
    {
        public ReferenteContratto() : base() { }
        public ReferenteContratto(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        private string fNomeCognome;
        [Size(250), Persistent("NOMECOGNOME"), DisplayName("Nome e Cognome")]
        [DbType("varchar(250)")]
        public string NomeCognome
        {
            get
            {
                return fNomeCognome;
            }
            set
            {
                SetPropertyValue<string>("NomeCognome", ref fNomeCognome, value);
            }
        }
           
        private string fTelefono;
        [Size(250), Persistent("TELEFONO"), DisplayName("Telefono")]
        [DbType("varchar(250)")]
        public string Telefono
        {
            get
            {
                return fTelefono;
            }
            set
            {
                SetPropertyValue<string>("Telefono", ref fTelefono, value);
            }
        }


        private TipoReferenteContratto fTipoReferente; 
        [Persistent("TIPOREFERENTE"), DisplayName("Tipo Referente")]
        public TipoReferenteContratto TipoReferente
        {
            get
            {
                return fTipoReferente;
            }
            set
            {
                SetPropertyValue<TipoReferenteContratto>("TipoReferenteContratto", ref fTipoReferente, value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.NomeCognome);
        }

    }
}