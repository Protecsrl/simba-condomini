using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Simba.Businness;


namespace SimbaCondomini.Controllers
{
    public class CondominiumDatiController : ApiController
    {
        [HttpGet]
        public HttpResponseMessage Get(DataSourceLoadOptions loadOptions)
        {
            Condomini c = new Condomini();
            var data = c.GetAllByUser();
            var datac = new List<Simba.Businness.Models.Condominio>();
            foreach (var cc in data)
            {
                datac.Add(new Simba.Businness.Models.Condominio(cc.Oid, cc.Comune, cc.NomeCondominio, cc.Indirizzo, cc.PartitaIva, cc.Latitudine, cc.Longitudine));
            }

            // return Request.CreateResponse(DataSourceLoader.Load(datac, loadOptions));
            return Request.CreateResponse(datac);

        }
    }
}
