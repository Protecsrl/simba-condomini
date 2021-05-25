using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBAux
{
    [NavigationItem(false), System.ComponentModel.DisplayName("Giorni")]
    [DefaultClassOptions, Persistent("GIORNI")]
    [VisibleInDashboards(false)]
    public class Giorni : XPObject
    {
        public Giorni()
            : base()
        {
        }
        public Giorni(Session session)
            : base(session)
        {
        }

        private int fgiorni;
        [Persistent("GIORNO"),  DisplayName("giorni")]
        public int NrGiorno
        {
            get
            {
                return fgiorni;
            }
            set
            {
                SetPropertyValue<int>("NrGiorno", ref fgiorni, value);
            }
        }

        private string fstrgiorni;
        [Persistent("STRGIORNO"), DisplayName("giorni")]
        public string strGiorno
        {
            get
            {
                return fstrgiorni;
            }
            set
            {
                SetPropertyValue<string>("strGiorno", ref fstrgiorni, value);
            }
        }

        public override string ToString()
        {
            return string.Format("{0}", this.NrGiorno);
        }
    }
}


 
           