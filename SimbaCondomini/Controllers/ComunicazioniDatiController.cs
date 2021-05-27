using System.Collections.Generic;
using System.Net.Http;
using System.Linq;
using System.Web.Http;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Simba.Businness;
namespace SimbaCondomini.Controllers
{
    public class ComunicazioniDatiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            Comunicazioni c = new Comunicazioni();
            var data = c.GetUserCommunication(5);
            var datac = new List<SimbaCondomini.Models.ComunicazioniTicket>();
            foreach (var cc in data)
            {
                datac.Add(new SimbaCondomini.Models.ComunicazioniTicket(cc.Oid, cc.Testo, cc.ParentCommunication, cc.User.Nome, cc.Condominium.NomeCondominio, cc.DateInsert));
            }



            Ticket t = new Ticket();
            var dataT = t.GetUserTicket(5);

            foreach (var cc in dataT)
            {
                var cond = cc.Condominium != null ? cc.Condominium.NomeCondominio : string.Empty;
                datac.Add(new SimbaCondomini.Models.ComunicazioniTicket(cc.Oid, cc.Note, 0, cc.User.Nome, cond, cc.DateCreation));
            }

            datac.Sort();

            return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));

        }
    }
}