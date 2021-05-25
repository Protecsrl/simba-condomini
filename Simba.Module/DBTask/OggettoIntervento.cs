using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
   Persistent("RDLOGGETTOINTERVENTO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Oggetto Intervento")]
    [ImageName("Time")]
    //[NavigationItem(false)]
    public class OggettoIntervento : XPObject
    {
        
        public OggettoIntervento()
            : base()
        {
        }
        public OggettoIntervento(Session session)
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
