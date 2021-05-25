using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using DevExpress.ExpressApp;
#pragma warning disable CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
using System;
#pragma warning restore CS0105 // La direttiva using per 'System' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
using System.Collections.Generic;
#pragma warning restore CS0105 // La direttiva using per 'System.Collections.Generic' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
using System.Linq;
#pragma warning restore CS0105 // La direttiva using per 'System.Linq' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
using System.Text;
#pragma warning restore CS0105 // La direttiva using per 'System.Text' è già presente in questo spazio dei nomi
#pragma warning disable CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using System.Threading.Tasks;
#pragma warning restore CS0105 // La direttiva using per 'System.Threading.Tasks' è già presente in questo spazio dei nomi
using DevExpress.Xpo;
using CAMS.Module.DBAngrafica;
//namespace CAMS.Module.Classi
//{
//    class Logger
//    {
//    }
//}
namespace CAMS.Module.Classi
{

    public static class Logger
    {
        public static void AddLog(IObjectSpace xpObjectSpace, string UserName, string Descrizione, string SessioneWEB)
        {
            LogSystemTrace logdb = xpObjectSpace.CreateObject<LogSystemTrace>(); // new Event() { Subject = "test" };
            logdb.Utente = UserName;  //user.UserName;
            logdb.Descrizione = Descrizione;
            logdb.DataAggiornamento = DateTime.Now;
            //logdb.Corpo = System.Web.HttpContext.Current.Request.Headers.ToString();
            if (System.Web.HttpContext.Current != null)
                if (System.Web.HttpContext.Current.Request != null)
                    if (System.Web.HttpContext.Current.Request.Browser != null)
                        logdb.Corpo = string.Format("{0}_{1}",
                System.Web.HttpContext.Current.Request.Browser.Id.ToString(),
                System.Web.HttpContext.Current.Request.Browser.Version.ToString());

            logdb.SessioneWEB = SessioneWEB;
            logdb.Save();
            xpObjectSpace.CommitChanges();
        }

        public static void AddTestLog(IObjectSpace xpObjectSpace, string Descrizione)
        {
            test tes = xpObjectSpace.CreateObject<test>();
            tes.DATA = DateTime.Now;
            tes.PersistentProperty = Descrizione;
            tes.Save();
            xpObjectSpace.CommitChanges();
        }
    }
}
