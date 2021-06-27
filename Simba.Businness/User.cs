using DevExpress.Xpo;
using Simba.Businness.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace Simba.Businness
{
    public enum Roles
    {
        SysAdmin = 1,
        CondAdmin = 2,
        Cond = 3,
        Supplier = 4

    }
    public class User : BusinnessBase
    {
        public static int GetUserId()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            var id = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Oid")
                               .Select(c => c.Value).SingleOrDefault());
            return id;
        }

        public static Roles GetUserType()
        {
            var identity = (ClaimsPrincipal)Thread.CurrentPrincipal;

            var type = Convert.ToInt32(identity.Claims.Where(c => c.Type == "Type")
                               .Select(c => c.Value).SingleOrDefault());
            return (Roles)type;
        }

        public Simba.DataLayer.simba_condomini.User GetUser()
        {
            using (UnitOfWork uw = new UnitOfWork())
            {
                var identity = (System.Security.Claims.ClaimsPrincipal)HttpContext.Current.User;
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

                var user = uw.Query<DataLayer.simba_condomini.User>().Where(d => d.Username == userid && d.Password == password);
                return user.FirstOrDefault();
            }
        }
    }
}
