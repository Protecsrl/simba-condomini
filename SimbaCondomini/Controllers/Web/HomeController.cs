using Simba.Businness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }


        public ActionResult Index2(int? id)
        {
            if(id.HasValue){
                Bacheca b = new Bacheca(id.Value);
                return RedirectToAction("ScegliCondominio", "Comunicazioni", new { id = 1 });
            }
            return View();
        }
        public ActionResult ComunicazioniController(){
            return View();
        }
    }
}