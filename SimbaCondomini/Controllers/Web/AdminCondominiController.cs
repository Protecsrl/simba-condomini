using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class AdminCondominiController : Controller
    {
        // GET: AdminCondomini
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Edit()
        {
            var model = new Simba.Businness.Models.Admin.Condominio(0, 0, string.Empty, string.Empty, string.Empty, 0, 0, string.Empty);
            return View(model);
        }
    }
}