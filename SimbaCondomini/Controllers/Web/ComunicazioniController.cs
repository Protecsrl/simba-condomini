using Simba.Businness.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class ComunicazioniController : Controller
    {
        int faketicket = 5;
        // GET: Comunicazioni
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NuovoTicket()
        {
            AddTicket model = new AddTicket();

            model.Oid = faketicket;
            model.Number = new Simba.Businness.ComunicazioniTicket().getNewId();
            model.Owner = new Simba.Businness.ComunicazioniTicket().getEnvironmenti(1).Text;
            var ticketStatuse = new Simba.Businness.Ticket().GetTicketStatuses(5);
            model.StoricoStati = new List<TicketStatus>();
            foreach (var ts in ticketStatuse)
            {
                model.StoricoStati.Add(new TicketStatus(ts.Data, ts.IdStatus.Name, ts.Descrizione));
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult NuovoTicket(AddTicket model)
        {
            var ticketStatuse = new Simba.Businness.Ticket().GetTicketStatuses(faketicket);
            model.StoricoStati = new List<TicketStatus>();
            foreach (var ts in ticketStatuse)
            {
                model.StoricoStati.Add(new TicketStatus(ts.Data, ts.IdStatus.Name, ts.Descrizione));
            }
            new Simba.Businness.Ticket().SaveTicket( model.ToXpoModel(model));
            return View(model);
        }
    }
}