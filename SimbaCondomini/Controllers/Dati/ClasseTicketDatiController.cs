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
    public class StatiTicketDatiController : ApiController
    {
        // GET: TicketDati
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            StatiTicket cc = new StatiTicket();
            var ccc = cc.GetAll();
            List<Simba.Businness.Models.TicketStatus> list = new List<Simba.Businness.Models.TicketStatus>();
            foreach (var c in ccc)
            {
                list.Add(new Simba.Businness.Models.TicketStatus(c.Oid, c.Name, c.Descrizione));
            }
            return Request.CreateResponse(DataSourceLoader.Load(list, loadOptions));
        }
    }
}