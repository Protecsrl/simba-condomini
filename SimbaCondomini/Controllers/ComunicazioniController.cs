using SimbaCondomini.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class ComunicazioniController : Controller
    {
        // GET: Comunicazioni
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NuovoTicket()
        {
            AddTicket model = new AddTicket();
            model.Number = new Simba.Businness.ComunicazioniTicket().getNewId();
            model.Owner = new Simba.Businness.ComunicazioniTicket().getEnvironmenti(1).Text;
            return View(model);
        }
    }
}