using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using CAMS.Module.DBAngrafica;

namespace CAMS.Module.DBControlliNormativi
{
     [DefaultClassOptions, Persistent("LOGEMAILCTRLNORM_DEST")]
     [NavigationItem(false)]
     [System.ComponentModel.DisplayName("Destinatatri eMail Inviate")]
     [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Destinatatri eMail Inviate")]
    [ImageName("NewMail")]
    public class LogEmailCtrlNormRifDestinatari : XPObject    {
         public LogEmailCtrlNormRifDestinatari() : base() { }
          public LogEmailCtrlNormRifDestinatari(Session session)  : base(session) { }
        public override void AfterConstruction()   {   base.AfterConstruction(); }

        private Destinatari fDestinatari;
        [ Persistent("DESTINATARI"), DisplayName("Destinatari")]
        [Appearance("LogEmailCtrlNormRifDestinatari.Destinatari", Enabled = false)]
        [ExplicitLoading()]
        public Destinatari Destinatari
        {
            get
            {
                return fDestinatari;
            }
            set
            {
                SetPropertyValue<Destinatari>("Destinatari", ref fDestinatari, value);
            }
        }

        private LogEmailCtrlNorm fLogEmailCtrlNorm;
        [Association(@"LogEmailCtrlNorm_Destinatari"),Persistent("LOGEMAILCTRLNORM"), DisplayName("Registro Invii Controllo Normativo")]
        [Appearance("LogEmailCtrlNormRifDestinatari.LogEmailCtrlNorm", Enabled = false)]
        [ExplicitLoading()]
        public LogEmailCtrlNorm LogEmailCtrlNorm
        {
            get
            {
                return fLogEmailCtrlNorm;
            }
            set
            {
                SetPropertyValue<LogEmailCtrlNorm>("LogEmailCtrlNorm", ref fLogEmailCtrlNorm, value);
            }
        }
        public override string ToString()
        {
            return string.Format("Destinatari {0} ", this.Destinatari);
        }
    }
}
