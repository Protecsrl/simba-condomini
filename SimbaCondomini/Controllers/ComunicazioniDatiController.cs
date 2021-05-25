using System.Collections.Generic;
using System.Net.Http;
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
            var data = c.GetUserCommunication(1);
            var datac = new List<SimbaCondomini.Models.Comunicazioni>();
            foreach (var cc in data)
            {
                datac.Add(new SimbaCondomini.Models.Comunicazioni(cc.Oid, cc.Testo, cc.ParentCommunication, cc.User.Nome, cc.Condominium.NomeCondominio, cc.DateInsert));
            }
            return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));

        }
    }
}