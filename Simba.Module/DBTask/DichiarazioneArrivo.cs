using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBTask
{
    [DefaultClassOptions,
   Persistent("RDLDICHIARAZIONEARRIVO")]
    [VisibleInDashboards(false)]
    [System.ComponentModel.DefaultProperty("Descrizione")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Dichiarazione di Arrivo")]
    [ImageName("Time")]
    //[NavigationItem(false)]
    public class DichiarazioneArrivo : XPObject
    {
        
        public DichiarazioneArrivo()
            : base()
        {
        }
        public DichiarazioneArrivo(Session session)
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
