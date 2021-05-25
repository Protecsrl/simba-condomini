using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DevExpress.ExpressApp.Security.Strategy;

namespace CAMS.Module.Classi
{
    public class UtenteRuolo : IDisposable
    {
        public void Dispose()
        {
            System.GC.SuppressFinalize(this);
        }

        bool GetIsAmministratore(SecuritySystemUser Utente )
        {
            int IsAdmin = Utente.Roles.Where(w=> w.IsAdministrative == true).Count();
            if (IsAdmin > 0) return true;
            
            return false ;
        }

        public bool GetIsTipoRuolo(DevExpress.ExpressApp.Security.Strategy.SecuritySystemUser Utente,string TipoRuolo)
        {
            int IsCC = Utente.Roles.Where(w => w.Name.ToUpper().Contains(TipoRuolo.ToUpper())).Count(); //Contains("CALLCENTER")).Count();
            if (IsCC > 0) return true;

            return false;
        }
    }
}
