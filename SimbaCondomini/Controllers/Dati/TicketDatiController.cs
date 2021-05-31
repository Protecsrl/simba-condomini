using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Simba.Businness;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SimbaCondomini.Controllers
{
    public class TicketDatiController : ApiController
    {
        // GET: TicketDati
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            ClassificazioneTicket cc = new ClassificazioneTicket();
            var ccc = cc.GetAll();
            List<Simba.Businness.Models.TicketCommunicationClassification> list = new List<Simba.Businness.Models.TicketCommunicationClassification>();
            foreach (var c in ccc)
            {
                list.Add(new Simba.Businness.Models.TicketCommunicationClassification(c.Oid, c.Nome, c.Descrizione));
            }
            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }
    }
}