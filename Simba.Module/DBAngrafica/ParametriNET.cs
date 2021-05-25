using System;
using DevExpress.Xpo;
using DevExpress.Persistent.Base;

namespace CAMS.Module.DBAngrafica
{
    [NavigationItem("Amministrazione"),
    System.ComponentModel.DisplayName("Parametri Spedizione e-mail")]
    [DevExpress.ExpressApp.Model.ModelDefault("Caption", "Parametri Spedizione  e-mail1")]
    [DefaultClassOptions,   Persistent("PARAMETRINET")]
    [ImageName("GroupFieldCollectionDett")]
    [VisibleInDashboards(false)]
    public class ParametriNET : XPObject
    {
        public ParametriNET()
            : base()
        {
        }

        public ParametriNET(Session session)
            : base(session)
        {
        }

        public override void AfterConstruction()
        {
            base.AfterConstruction();
        }

        private string fServerSMTP;
        [Size(100),
        Persistent("SERVERSMTP"),
        DisplayName("Server SMTP")]
        [DbType("varchar(100)")]
        public string ServerSMTP
        {
            get
            {
                return fServerSMTP;
            }
            set
            {
                SetPropertyValue<string>("ServerSMTP", ref fServerSMTP, value);
            }
        }

        private string fUserSMTP;
        [Size(100),
        Persistent("USERSSMTP"),
        DisplayName("User SMTP")]
        [DbType("varchar(100)")]
        public string UserSMTP
        {
            get
            {
                return fUserSMTP;
            }
            set
            {
                SetPropertyValue<string>("UserSMTP", ref fUserSMTP, value);
            }
        }

        private string fPwSMTP;
        [Size(100),
        Persistent("PWSSMTP"),
        DisplayName("Password SMTP")]
        [DbType("varchar(100)")]
        public string PwSMTP
        {
            get
            {
                return fPwSMTP;
            }
            set
            {
                SetPropertyValue<string>("PwSMTP", ref fPwSMTP, value);
            }
        }

        private int fPortaSMTP;
        [ Persistent("PORTASMTP"),
        DisplayName("Password SMTP")]
        //[DbType("varchar(100)")]
        public int PortaSMTP
        {
            get
            {
                return fPortaSMTP;
            }
            set
            {
                SetPropertyValue<int>("PortaSMTP", ref fPortaSMTP, value);
            }
        }

        private string fMailFrom;
        private const string UrlStringEditMask = @"(((http|https|ftp)\://)?[a-zA-Z0-9\-\.]+\.[a-zA-Z]{2,3}(:[a-zA-Z0-9]*)?/?([a-zA-Z0-9\-\._\?\,\'/\\\+&amp;amp;%\$#\=~])*)|([a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,6})";
        [VisibleInListView(true)]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", UrlStringEditMask)]
        [ToolTip("Specifica una email, con il seguente formato: " + UrlStringEditMask, null)]
        [DbType("varchar(100)")]
        [Size(100),
        Persistent("MAILFROM"),
        DisplayName("indirizzo Posta di Spedizione")]
        public string MailFrom
        {
            get
            {
                return fMailFrom;
            }
            set
            {
                SetPropertyValue<string>("MailFrom", ref fMailFrom, value);
            }
        }

        private string fMailToCC;
        [VisibleInListView(true)]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", UrlStringEditMask)]
        [ToolTip("Specifica una email, con il seguente formato: " + UrlStringEditMask, null)]
        [DbType("varchar(100)")]
        [Size(100),
        Persistent("MAILTOCC"),
        DisplayName("indirizzo Posta di Destinazione CC")]
        public string MailToCC
        {
            get
            {
                return fMailToCC;
            }
            set
            {
                SetPropertyValue<string>("MailToCC", ref fMailToCC, value);
            }
        }

        private string fEndPoint;
        private const string urlRx = @"^(http|https)://([\w+?\.\w+])+([a-zA-Z0-9\~\!\@\#\$\%\^\&\*\(\)_\-\=\+\\\/\?\.\:\;\'\,]*)?$";
        [VisibleInListView(true)]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMaskType", "RegEx")]
        [DevExpress.ExpressApp.Model.ModelDefault("EditMask", urlRx)]
        [ToolTip("Specifica una URL, con il seguente formato: " + urlRx, null)]
        [DbType("varchar(255)")]
        [Size(100),
        Persistent("END_POINT"),
        DisplayName("End point per WS")]
        public string EndPOint
        {
            get
            {
                return fEndPoint;
            }
            set
            {
                SetPropertyValue<string>("EndPoint", ref fEndPoint, value);
            }
        }

        private int fMinuti;
        [Persistent("MINUTI"),
        DisplayName("Intervallo chiamate WS")]
        //[DbType("number(6)")]    commetato x passaggio a MSSQL
        public int Minuti
        {
            get
            {
                return fMinuti;
            }
            set
            {
                SetPropertyValue<int>("Minuti", ref fMinuti, value);
            }
        }
    }
}
