using Simba.Businness.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(Simba.Businness.Models.Login data)
        {
            FakeVariebiles d = new FakeVariebiles();
            d.UserId = 5;
            d.UserRole = Roles.Cond;
            return View();
        }
    }
}