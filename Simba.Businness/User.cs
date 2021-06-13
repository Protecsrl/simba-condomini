using DevExpress.Xpo;
using Simba.Businness.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Simba.Businness
{
    public class User : BusinnessBase
    {
        public Simba.DataLayer.simba_condomini.User GetUser()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var identity = (System.Security.Claims.ClaimsPrincipal) HttpContext.Current.User;
                IEnumerable<Claim> claims = identity.Claims;
                var claim = claims.Where(c => c.Type == ClaimTypes.NameIdentifier).First();
                var user = uw.Query<DataLayer.simba_condomini.User>().Where(d => d.Username == claim.Value).First();
                return user;
            }
        }

        public Simba.DataLayer.simba_condomini.User GetUser(string userid, string password)
        {
            using (UnitOfWork uw = new UnitOfWork())
            {

                var user = uw.Query<DataLayer.simba_condomini.User>().Where(d=> d.Username == userid && d.Password == password);
                return user.FirstOrDefault();
            }
        }
    }
}
