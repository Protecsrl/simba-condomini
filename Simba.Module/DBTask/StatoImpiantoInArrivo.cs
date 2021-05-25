using DevExpress.Persistent.Base;
using DevExpress.Xpo;


namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
   Persistent("RDLSTATOINARRIVO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Stato Impianto In Arrivo")]
    [ImageName("Time")]
    //[NavigationItem(false)]
    public class StatoImpiantoInArrivo : XPObject
    {

        public StatoImpiantoInArrivo()
            : base()
        {
        }
        public StatoImpiantoInArrivo(Session session)
            : base(session)
        {
        }
        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }


        private string fDescrizione;
        [Size(60),
        Persistent("DESCRIZIONE"),
        DisplayName("Descrizione")]
        [DbType("varchar(60)")]
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
        private int fValore;
        [Persistent("VALORE"),
        DisplayName("Valore")]
        public int Valore
        {
            get
            {
                return fValore;
            }
            set
            {
                SetPropertyValue<int>("Valore", ref fValore, value);
            }
        }
    }
}
