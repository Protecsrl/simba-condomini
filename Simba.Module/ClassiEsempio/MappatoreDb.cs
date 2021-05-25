using CAMS.Module.ClassiMSDB;
using CAMS.Module.ClassiORADB;
using CAMS.Module.DBControlliNormativi;
using DevExpress.ExpressApp;
using DevExpress.Xpo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CAMS.Module.ClassiEsempio
{
    public class MappatoreDb
    {

        public static LogEmailCtrlNorm InstantiateLogEmailCtrlNorm(IObjectSpace xpObjectSpace, string Corpo2)
        {
            LogEmailCtrlNorm NuovoLog = xpObjectSpace.CreateObject<LogEmailCtrlNorm>();
            if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
            {
                NuovoLog = xpObjectSpace.CreateObject<EsempioMsSql>();
                var objMsDb = (EsempioMsSql)NuovoLog;
                objMsDb.Corpo = Corpo2;
                return objMsDb;
            }
            else
            {
                NuovoLog = xpObjectSpace.CreateObject<EsempioOraDb>();
                var objOraDb = (EsempioOraDb)NuovoLog;
                objOraDb.Corpo = Corpo2;
                return objOraDb;
            }

        }
        public static void SaveCorpo2(LogEmailCtrlNorm NuovoLog)
        {
            if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
            {
                var objMsDb = (EsempioMsSql)NuovoLog;
                objMsDb.Save();

            }
            else
            {

                var objOraDb = (EsempioOraDb)NuovoLog;
                objOraDb.Save();
            }
        }

        public static string GetCorpo2(LogEmailCtrlNorm NuovoLog)
        {
            if (CAMS.Module.Classi.SetVarSessione.IsMSSQLDB)
            {
                var objMsDb = (EsempioMsSql)NuovoLog;
                return objMsDb.Corpo;

            }
            else
            {

                var objOraDb = (EsempioOraDb)NuovoLog;
                return objOraDb.Corpo;
            }
        }
    }
}
