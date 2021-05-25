using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DevExpress.ExpressApp.Model;
using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Persistent.Validation;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;

//namespace CAMS.Module.DBAux
//{
//    class SecuritySystemUserLog
//    {
//    }
//}

namespace CAMS.Module.DBAux
{
    [DefaultClassOptions]
    [Persistent("SecuritySystemUserLog")]
    [System.ComponentModel.DefaultProperty("NomeUtente")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "SecuritySystemUserLog")]
    [ImageName("BO_Employee")]
    [NavigationItem(true)]
    [VisibleInDashboards(false)]
    public class SecuritySystemUserLog : SecuritySystemUser
    {
        //public ExpandUser() : base()   //{  //}
        public SecuritySystemUserLog(Session session)
            : base(session)
        {
        }

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        public DateTime DataAggiornamento
        {
            get { return fDataAggiornamento; }
            set { SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value); }
        }

        private DateTime fDataLogIn;
        [Persistent("DATALOGIN"),
        DisplayName("Data LogIn")]
        public DateTime DataLogIn
        {
            get { return fDataLogIn; }
            set { SetPropertyValue<DateTime>("DataLogIn", ref fDataLogIn, value); }
        }

        private string fTipologiaRuolo;
        [Size(150), Persistent("TIPOLOGIARUOLO"), DevExpress.ExpressApp.DC.XafDisplayName("Tipologia Ruolo")]
        [DbType("varchar(150)")]
        public string TipologiaRuolo
        {
            get { return fTipologiaRuolo; }
            set { SetPropertyValue<string>("TipologiaRuolo", ref fTipologiaRuolo, value); }
        }

        private string fSessionID;
        [Size(50),
        Persistent("SESSIONID"), DevExpress.ExpressApp.DC.XafDisplayName("SessioneAttiva")]
        [DbType("varchar(50)")]
        //[Browsable(false)]
        public string SessionID
        {
            get { return fSessionID; }
            set { SetPropertyValue<string>("SessionID", ref fSessionID, value); }
        }


    }
}

