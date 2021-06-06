using Simba.Businness;
using Simba.Businness.Models;
using Simba.Businness.Security;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SimbaCondomini.Controllers
{
    public class ComunicazioniController : Controller
    {
        int faketicket;

        public ComunicazioniController(){
            faketicket = 5;
        }

        private ActionResult NuovoTicket()
        {
            int number = new Simba.Businness.ComunicazioniTicket().getNewId();
            string owner = new Simba.Businness.ComunicazioniTicket().getEnvironmenti(1).Text;
            var user = new Simba.Businness.User().GetUser();
            LookupItem locale = new LookupItem() { Id = user.Environment.Oid };
            AddTicket model = new AddTicket(0, number, owner, 0, 1, null, null, null, null, locale, null, null, new List<TicketStatus>(), null);

            return View(model);
        }

        public ActionResult NuovoTicket(int? id)
        {
            if (id <= 0 || id == null)
            {
                return NuovoTicket();
            }

            int oid = id.HasValue ? id.Value : 0;
            int number = new Simba.Businness.ComunicazioniTicket().getNewId();
            string owner = new Simba.Businness.ComunicazioniTicket().getEnvironmenti(1).Text;
            var ticketStatuses = new Simba.Businness.Ticket().GetTicketStatuses(oid).OrderByDescending(s => s.Oid);
            var ticket = new Simba.Businness.Ticket().getTicketById(oid);
            string titolo = ticket.Titolo;
            string descrizione = ticket.Descrizione;
            var storicoStati = new List<TicketStatus>();
            foreach (var ts in ticketStatuses)
            {
                storicoStati.Add(new TicketStatus(ts.Data, ts.IdStatus.Name, ts.Descrizione));
            }
            int stato = 0;
            if (ticketStatuses.Any()) { stato = ticketStatuses.First().Oid; }

            var user = new Simba.Businness.User().GetUser();

            LookupItem locale = new LookupItem() { Id = ticket.Enviroment.Oid, Text= ticket.Enviroment.Name };

            AddTicket model = new AddTicket(oid, number, owner, 0, 1, null, titolo, descrizione, ticket.Note, locale, null, null, storicoStati, null);
            return View(model);
        }

        [HttpPost]
        public ActionResult NuovoTicket(AddTicket model, HttpPostedFileBase files)
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


        [HttpPost]
        public ActionResult Upload()
        {
            // Learn to use the entire functionality of the dxFileUploader widget.
            // http://js.devexpress.com/Documentation/Guide/UI_Widgets/UI_Widgets_-_Deep_Dive/dxFileUploader/

            var myFile = Request.Files["Files"];
            var targetLocation = Server.MapPath("~/Documents/");
            DirectoryInfo di = new DirectoryInfo(targetLocation);
            if (!di.GetDirectories().ToList().Contains(new DirectoryInfo("1")))
            {
                di.CreateSubdirectory("1");
            }
            try
            {
                var path = Path.Combine(targetLocation, "1", myFile.FileName);

                //Uncomment to save the file
                myFile.SaveAs(path);
            }
            catch
            {
                Response.StatusCode = 400;
            }

            return new EmptyResult();
        }
    }
}