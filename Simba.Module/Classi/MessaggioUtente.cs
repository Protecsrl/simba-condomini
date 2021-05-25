using DevExpress.Persistent.Base;
using DevExpress.Xpo;




namespace CAMS.Module.Classi
{
    [DefaultClassOptions, Persistent("MESSAGGIUTENTE"), NavigationItem(false)]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "MessaggiUtente")]
    public class MessaggioUtente : XPObject
    {
        public MessaggioUtente()
            : base()
        {
        }
        public MessaggioUtente(Session session)
            : base(session)
        {
        }


        private string fMessaggio;
        [Persistent("MESSAGGIO"), Size(4000), DisplayName("Messaggio")]
        [DbType("varchar(4000)")]
        public string Messaggio
        {
            get
            {
                return fMessaggio;
            }
            set
            {
                SetPropertyValue<string>("Messaggio", ref fMessaggio, value);
            }
        }

        private string fSessione;
        [Persistent("SESSIONE"), Size(4000), DisplayName("Messaggio")]
        public string Sessione
        {
            get
            {
                return fSessione;
            }
            set
            {
                SetPropertyValue<string>("Sessione", ref fSessione, value);
            }
        }
    }
}
