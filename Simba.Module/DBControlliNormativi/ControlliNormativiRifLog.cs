using CAMS.Module.Classi;
using DevExpress.ExpressApp.ConditionalAppearance;
using DevExpress.ExpressApp.Editors;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;

namespace CAMS.Module.DBControlliNormativi
{
    [DefaultClassOptions, Persistent("CONTROLLINORMATIVI_LOG")]
    [System.ComponentModel.DisplayName("Registro Avvisi Inviati")]
    [ImageName("NewMail")]
    [NavigationItem("Avvisi Periodici")]
    public class ControlliNormativiRifLog : XPObject
    {
        public ControlliNormativiRifLog()  : base()   {    }

        public ControlliNormativiRifLog(Session session)  : base(session)  {     }

        public override void AfterConstruction()    {  base.AfterConstruction();  }
        
        private ControlliNormativi fControlliNormativi;
        [Association(@"COntrolliNormativi_Log"), Persistent("CONTROLLINORMATIVI"), DisplayName("Avviso Normativo")]
        // [Appearance("Destinatari.DestinatariControlliNormativis", Visibility = DevExpress.ExpressApp.Editors.ViewItemVisibility.Hide)]
        public ControlliNormativi ControlliNormativi
        {
            get
            {
                return fControlliNormativi;
            }
            set
            {
                SetPropertyValue<ControlliNormativi>("ControlliNormativi", ref fControlliNormativi, value);
            }
        }

        private LogEmailCtrlNorm fLogEmailCtrlNorm;
        [Association(@"LogEmailCtrlNorm_Controlli"), Persistent("LOGEMAILCTRLNORM"), DisplayName("eMail Inviate")]
        [Appearance("ControlliNormativiRifLog.LogEmailCtrlNorm", Enabled = false)]
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


       // [Appearance("ControlliNormativiRifLog.DataInvio.Hide", Criteria = "LogEmailCtrlNorm is null", Visibility = ViewItemVisibility.Hide)]
        [PersistentAlias("LogEmailCtrlNorm.DataInvio"),  DisplayName("Data Invio")]
        public string LogEmailCtrlNormDataInvio
        {
            get
            {
                var tempObject = EvaluateAlias("LogEmailCtrlNormDataInvio");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }

       // [Appearance("ControlliNormativiRifLog.esito.Hide", Criteria = "LogEmailCtrlNorm is null", Visibility = ViewItemVisibility.Hide)]
        [PersistentAlias("LogEmailCtrlNorm.EsitoInvioMailSMS"), DisplayName("Esito Invio")]
        public string LogEmailCtrlNormEsitoInvioMail
        {
            get
            {
                
                var tempObject = EvaluateAlias("LogEmailCtrlNormEsitoInvioMail");
                if (tempObject != null)
                {
                    return (string)tempObject.ToString();
                }
                else
                {
                    return null;
                }
            }
        }


        public override string ToString()
        {
            return string.Format("Cod.{0} ", this.Oid );
        }
    }

}