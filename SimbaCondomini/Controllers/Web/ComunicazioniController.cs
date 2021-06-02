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

        private ActionResult NuovoTicket()
        {
            AddTicket model = new AddTicket();
            model.StoricoStati = new List<TicketStatus>();
            model.Number = new Simba.Businness.ComunicazioniTicket().getNewId();
            model.Owner = new Simba.Businness.ComunicazioniTicket().getEnvironmenti(1).Text;

            return View(model);
        }

        public ActionResult NuovoTicket(int? id)
        {
            if (id <= 0 || id==null)
            {
                return NuovoTicket();
            }
            AddTicket model = new AddTicket();

            model.Oid = id.HasValue ? id.Value : 0;
            model.Number = new Simba.Businness.ComunicazioniTicket().getNewId();
            model.Owner = new Simba.Businness.ComunicazioniTicket().getEnvironmenti(1).Text;
            var ticketStatuse = new Simba.Businness.Ticket().GetTicketStatuses(model.Oid);
            var ticket = new Simba.Businness.Ticket().getTicketById(model.Oid);
            model.Titolo = ticket.Titolo;
            model.Descrizione = ticket.Descrizione;
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
            if (model.Oid > 0)
            {
                new Simba.Businness.Ticket().UpdateTicket(model);
            }
            else
            {
                new Simba.Businness.Ticket().SaveTicket(model);
            }
            return View(model);
        }
    }
}