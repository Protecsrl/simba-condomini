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

        public ActionResult NuovaComunicazioneTicket()
        {
            return View();
        }
    }
}