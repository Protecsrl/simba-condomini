using Simba.Businness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }



        [Authorize]
        public ActionResult Index2(int? id)
        {
            if (Simba.Businness.User.GetUserType() == Simba.Businness.Roles.CondAdmin && !id.HasValue)
            {
                return RedirectToAction("ScegliCondominio", "Comunicazioni", new { id = Simba.Businness.User.GetUserId() });
            }
            if(id.HasValue)
            {
                Bacheca b = new Bacheca(id.Value);
                return View(b);
            }
            return View();
        }

        [Authorize]
        public ActionResult ComunicazioniController()
        {
            return View();
        }
    }
}