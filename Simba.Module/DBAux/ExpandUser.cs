using DevExpress.ExpressApp.Security.Strategy;
using DevExpress.Persistent.Base;
using DevExpress.Xpo;
using System;
//namespace CAMS.Module.DBAux
//{
//    class Cittadini
//    {
//    }
//}
//namespace CAMS.Module.DBAux
//{
//    class ExpandUser
//    {
//    }
//}
namespace CAMS.Module.DBAux
{
    [DefaultClassOptions]
    [Persistent("ExpandUser1")]
    [System.ComponentModel.DefaultProperty("NomeUtente")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Cittadini")]
    [ImageName("BO_Employee")]
    [NavigationItem(true)]
    [VisibleInDashboards(false)]
    public class ExpandUser : SecuritySystemUser
    {

        //public ExpandUser()
        //    : base()
        //{
        //}
        public ExpandUser(Session session)
            : base(session)
        {
        }

   

        private DateTime fDataAggiornamento;
        [Persistent("DATAUPDATE"),
        DisplayName("Data Aggiornamento")]
        public DateTime DataAggiornamento
        {
            get
            {
                return fDataAggiornamento;
            }
            set
            {
                SetPropertyValue<DateTime>("DataAggiornamento", ref fDataAggiornamento, value);
            }
        }

        private DateTime fDataLogIn;
        [Persistent("DATALOGIN"),
        DisplayName("Data LogIn")]
        public DateTime DataLogIn
        {
            get
            {
                return fDataLogIn;
            }
            set
            {
                SetPropertyValue<DateTime>("DataLogIn", ref fDataLogIn, value);
            }
        }


        private string fSessionID;
        [Size(50),
        Persistent("SESSIONID"), DevExpress.ExpressApp.DC.XafDisplayName("SessioneAttiva")]
        [DbType("varchar(50)")]
        //[Browsable(false)]
        public string SessionID
        {
            get
            { return fSessionID; }
            set
            { SetPropertyValue<string>("SessionID", ref fSessionID, value); }
        }

 
    }
}


