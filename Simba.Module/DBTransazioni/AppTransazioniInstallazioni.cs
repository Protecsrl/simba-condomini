using DevExpress.Persistent.Base;
using DevExpress.Xpo;
namespace CAMS.Module.DBTransazioni
{
    [DefaultClassOptions, Persistent("TRANSAZIONIAPPINSTAL")]
    [System.ComponentModel.DisplayName("Applicazioni Transazioni Installate")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Applicazioni Transazioni Installate")]
    [System.ComponentModel.DefaultProperty("NomeServer")]
    [ImageName("GroupFieldCollection")]
    public class AppTransazioniInstallazioni : XPObject
    {
        public AppTransazioniInstallazioni() : base() { }
        public AppTransazioniInstallazioni(Session session) : base(session) { }
        public override void AfterConstruction() { base.AfterConstruction(); }

        [Association(@"AppTransazioniInstallazioni.AppTransazioni")]
        [Persistent("TRANSAZIONIAPP"), DisplayName("App Transazioni")]
        [Delayed(true)]
        public AppTransazioni AppTransazioni
        {
            get { return GetDelayedPropertyValue<AppTransazioni>("AppTransazioni"); }
            set { SetDelayedPropertyValue<AppTransazioni>("AppTransazioni", value); }
        }

        private string fNomeServer;
        [Size(100), Persistent("NOMESERVER")]
        [DbType("varchar(100)")]
        public string NomeServer
        {
            get { return fNomeServer; }
            set { SetPropertyValue<string>("NomeServer", ref fNomeServer, value); }
        }

        private string fPercorso;
        [Size(1000), Persistent("PERCORSO")]
        [DbType("varchar(1000)")]
        public string Percorso
        {
            get { return fPercorso; }
            set { SetPropertyValue<string>("Percorso", ref fPercorso, value); }
        }

        private string fParametro;
        [Size(100), Persistent("PARAMETRO")]
        [DbType("varchar(100)")]
        public string Parametro
        {
            get { return fParametro; }
            set { SetPropertyValue<string>("Parametro", ref fParametro, value); }
        }
        private string fParValore;
        [Size(2000), Persistent("PARVALORE")]
        [DbType("varchar(2000)")]
        public string ParValore
        {
            get { return fParValore; }
            set { SetPropertyValue<string>("ParValore", ref fParValore, value); }
        }

    }
}
