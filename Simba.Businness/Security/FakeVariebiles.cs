using System;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Simba.Businness.Security
{

    public class FakeVariebiles
    {
        public int UserId
        {
            get
            {
                if (HttpContext.Current.Session["UserId"] == null)
                {
                    return 5;
                }
                return (int)HttpContext.Current.Session["UserId"];
            }
            set { HttpContext.Current.Session["UserId"] = value; }
        }

        public Roles UserRole
        {
            get
            {
                if (HttpContext.Current.Session["UserRole"] == null)
                {
                    return Roles.Cond;
                }
                return (Roles)HttpContext.Current.Session["UserRole"];
            }
            set { HttpContext.Current.Session["UserRole"] = value; }
        }
    }
}
